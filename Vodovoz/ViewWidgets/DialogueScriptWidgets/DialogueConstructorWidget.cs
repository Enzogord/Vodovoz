﻿using System;
using System.Collections.Generic;
using QSOrmProject;
using QSTDI;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueConstructorWidget : Gtk.Bin, IDialogueWidget
	{
		Dictionary<string, ScriptTreeObject> dialogueResults = new Dictionary<string, ScriptTreeObject>();
		IUnitOfWork UoW;
		ITdiDialog dialogue;

		public DialogueConstructorWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void RefreshDependency(ScriptTreeObject ste)
		{
			dialogue = CreateDlg(GetDialogueResults(ste));
			SendResults();
		}

		public void SendResults() // ???
		{
			if(dialogue == null) {
				return;
			}

			var result = new ScriptTreeObject {
				ResultObjectType = dialogue.GetType(),
				ResultObject = dialogue as object
			};
			this.SubWidgetDone?.Invoke(this, new SubWidgetDoneEventArgs(result));
		}

		Dictionary<string, ScriptTreeObject> GetDialogueResults(ScriptTreeObject results)
		{
			if(results != null && results.ResultObject is Dictionary<string, ScriptTreeObject>)
			{
				var resultsDictionary = results.ResultObject as Dictionary<string, ScriptTreeObject>;
				return resultsDictionary;
			}

			return null;
		}

		ITdiDialog CreateDlg(Dictionary<string, ScriptTreeObject> results)
		{
			var order = new Order {
				Client = GetCounterparty(results),
				DeliveryPoint = GetDeliveryPoint(results),
				DeliveryDate = GetDateSchedule(results).Date,
				DeliverySchedule = GetDateSchedule(results).Schedule,
				OrderItems = GetOrderItems(results)
			};

			var dlg = OrmMain.CreateObjectDialog(order);

			return dlg;
		}

		Counterparty GetCounterparty(Dictionary<string, ScriptTreeObject> results)
		{
			foreach(KeyValuePair<string, ScriptTreeObject> pair in results)
			{
				if(pair.Value.ResultObject is Counterparty)
				{
					return pair.Value.ResultObject as Counterparty;
				}
			}

			return null;
		}

		DeliveryPoint GetDeliveryPoint(Dictionary<string, ScriptTreeObject> results)
		{
			foreach(KeyValuePair<string, ScriptTreeObject> pair in results) {
				if(pair.Value.ResultObject is DeliveryPoint) {
					return pair.Value.ResultObject as DeliveryPoint;
				}
			}

			return null;
		}

		DateSchedule GetDateSchedule(Dictionary<string, ScriptTreeObject> results)
		{
			foreach(KeyValuePair<string, ScriptTreeObject> pair in results) {
				if(pair.Value.ResultObject is DateSchedule) {
					return pair.Value.ResultObject as DateSchedule;
				}
			}

			return null;
		}

		List<OrderItem> GetOrderItems(Dictionary<string, ScriptTreeObject> results)
		{
			foreach(KeyValuePair<string, ScriptTreeObject> pair in results) {
				if(pair.Value.ResultObject is List<OrderItem>) {
					return pair.Value.ResultObject as List<OrderItem>;
				}
			}

			return null;
		}
	}
}
