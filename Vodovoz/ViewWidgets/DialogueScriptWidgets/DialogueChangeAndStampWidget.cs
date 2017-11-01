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
		string dependencyString;
		string resultString;
		IUnitOfWork UoW;

		Dictionary<string, ScriptTreeObject> dialogueResults = new Dictionary<string, ScriptTreeObject>();

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;


		public DialogueChangeAndStampWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
		}

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
			dependencyString = GetDependency(ste);
			CorrectText();
		}

		String GetDependency(ScriptTreeObject dependencyObject)
		{
			decimal summ = 0;
			foreach(var item in (List<OrderItem>)dependencyObject.ResultObject) {
				summ += item.Sum;
			}

			return summ.ToString();
		}

		void CorrectText()
		{
			string[] corrections = { dependencyString };
			TextCorrectionsPresent?.Invoke(this, new TextCorrectionsPresentEventArgs(corrections));
		}


		//Dictionary<string, ScriptTreeObject> GetDialogueResults(ScriptTreeObject results)
		//{
		//	if(results != null && results.ResultObject is Dictionary<string, ScriptTreeObject>) {
		//		var resultsDictionary = results.ResultObject as Dictionary<string, ScriptTreeObject>;
		//		return resultsDictionary;
		//	}
		//
		//	return null;
		//}

		//decimal GetOrderItems(Dictionary<string, ScriptTreeObject> results)
		//{
		//	decimal items = 0;
		//	foreach(KeyValuePair<string, ScriptTreeObject> pair in results) {
		//		if(pair.Value.ResultObject is List<OrderItem>) {
		//			foreach(OrderItem item in pair.Value.ResultObject as List<OrderItem>) {
		//				items += item.Price;
		//			}
		//		}
		//	}
		//	return items;
		//}

		protected void OnCheckChangeToggled(object sender, EventArgs e)
		{
			spinbuttonChange.Visible = true;
		}

		protected void OnSpinbuttonChangeChanged(object sender, EventArgs e)
		{
			resultString = spinbuttonChange.Value.ToString();
		}
	}
}
