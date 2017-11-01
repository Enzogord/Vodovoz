﻿using System;
using System.Collections.Generic;
using System.Linq;
using QSOrmProject;
using QSTDI;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	/// <summary>
	/// Конструктор заказа
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueConstructorWidget : WidgetOnTdiTabBase, IDialogueWidget
	{
		Dictionary<string, ScriptTreeObject> dialogueResults = new Dictionary<string, ScriptTreeObject>();
		IUnitOfWork UoW;
		ITdiDialog dialogue;
		object resultEntity;

		public DialogueConstructorWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void RefreshDependency(ScriptTreeObject ste)
		{
			dialogueResults = GetDialogueResults(ste);
		}

		public void SendResults() // ???
		{
			if(resultEntity == null) {
				return;
			}

			var result = new ScriptTreeObject {
				ResultObjectType = resultEntity.GetType(),
				ResultObject = resultEntity as object
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

		ITdiDialog CreateDlg()
		{
			resultEntity = CreateOrder();

			var dlg = OrmMain.CreateObjectDialog(resultEntity);

			return dlg;
		}

		#region Result parse
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

		List<OrderItem> GetOrderItems(Dictionary<string, ScriptTreeObject> results, Order order)
		{
			var items = new List<OrderItem>();
			foreach(KeyValuePair<string, ScriptTreeObject> pair in results) {
				if(pair.Value.ResultObject is List<OrderItem>) {
					foreach(OrderItem item in pair.Value.ResultObject as List<OrderItem>)
					{
						items.Add(new OrderItem(){
							Order = order,
							AdditionalAgreement = item.AdditionalAgreement,
							Count = item.Count,
							Equipment = item.Equipment,
							Nomenclature = item.Nomenclature,
							Price = item.Price,
						});
					}
				}
			}
			return items;
			//return null;
		}

		private int GetReturnBottles(Dictionary<string, ScriptTreeObject> results)
		{
			var items = new List<OrderItem>();
			int returnBottles = 0;
			foreach(KeyValuePair<string, ScriptTreeObject> pair in results) {
				if(pair.Value.ResultObject is List<OrderItem>) {
					foreach(OrderItem item in (pair.Value.ResultObject as List<OrderItem>).Where(o => o.Nomenclature.Category == NomenclatureCategory.water)) {
						returnBottles += item.Count;
					}
				}
			}
			return returnBottles;
		}

		#endregion

		protected void OnButtonGenerateDlgClicked(object sender, EventArgs e)
		{
			dialogue = CreateDlg();
			dialogue.EntitySaved += OnDialogueEntitySaved;
			dialogue.CloseTab += OnDialogueTabClosed;
			OpenNewTab(dialogue);
		}

		void OnDialogueEntitySaved(object sender, EntitySavedEventArgs e)
		{
			if(e.Entity != null)
			{
				resultEntity = e.Entity;
			}
			SendResults();
		}

		void OnDialogueTabClosed(object sender, EventArgs e)
		{
		//	DeleteEntity(resultEntity);
		}

		object CreateOrder()
		{
			var order = new Order();

			order.Client = GetCounterparty(dialogueResults);
			order.DeliveryPoint = GetDeliveryPoint(dialogueResults);
			order.DeliveryDate = GetDateSchedule(dialogueResults).Date;
			order.DeliverySchedule = GetDateSchedule(dialogueResults).Schedule;
			order.OrderItems = GetOrderItems(dialogueResults, order);
			order.BottlesReturn = GetReturnBottles(dialogueResults);
				

			return order;
		}

		object SaveEntity(object entity)
		{
			UoW.TrySave(entity);
			UoW.Commit();

			return entity;
		}

		void DeleteEntity(object entity)
		{
			UoW.TryDelete(entity);
			UoW.Commit();
		}
	}
}
