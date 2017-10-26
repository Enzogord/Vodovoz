using System;
using System.Collections.Generic;
using QSOrmProject;
using Vodovoz.Domain;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueChangeAndStampWidget : Gtk.Bin, IDialogueWidget
	{
		string resultString;
		IUnitOfWork UoW;

		Dictionary<string, ScriptTreeObject> dialogueResults = new Dictionary<string, ScriptTreeObject>();

		public DialogueChangeAndStampWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void Configure()
		{

		}

		public void SendResult()
		{
			var result = new ScriptTreeObject {
				ResultObjectType = resultString.GetType(),
				ResultObject = resultString as object
			};
			this.SubWidgetDone(this, new SubWidgetDoneEventArgs(result));
		}

		public void RefreshDependency(ScriptTreeObject ste)
		{
			//dialogueResults = GetDialogueResults(ste);
			var dialogueResults = GetDependency(ste);
			//label.LabelProp = GetOrderItems(dialogueResults).ToString();
		}

		DateSchedule GetDependency(ScriptTreeObject dependencyObject)
		{
			if(dependencyObject != null && dependencyObject.ResultObject is DateSchedule) {
				var dependency = dependencyObject.ResultObject as DateSchedule;
				return dependency;
			}

			return null;
		}

		//Dictionary<string, ScriptTreeObject> GetDialogueResults(ScriptTreeObject results)
		//{
		//	if(results != null && results.ResultObject is Dictionary<string, ScriptTreeObject>) {
		//		var resultsDictionary = results.ResultObject as Dictionary<string, ScriptTreeObject>;
		//		return resultsDictionary;
		//	}

		//	return null;
		//}

		decimal GetOrderItems(Dictionary<string, ScriptTreeObject> results)
		{
			decimal items = 0;
			foreach(KeyValuePair<string, ScriptTreeObject> pair in results) {
				if(pair.Value.ResultObject is List<OrderItem>) {
					foreach(OrderItem item in pair.Value.ResultObject as List<OrderItem>) {
						items += item.Price;
					}
				}
			}
			return items;
		}
	}
}
