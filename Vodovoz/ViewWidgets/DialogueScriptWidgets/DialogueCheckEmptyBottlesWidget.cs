using System;
using System.Collections.Generic;
using QSOrmProject;
using Vodovoz.Domain;
using Vodovoz.Domain.Orders;
using Vodovoz.Repository.Operations;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueCheckEmptyBottlesWidget : Gtk.Bin, IDialogueWidget
	{
		IUnitOfWork UoW;
		List<OrderItem> dependencyItems;
		int bottles = 0;

		public DialogueCheckEmptyBottlesWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
			Configure();
		}

		public void Configure()
		{
			//taraDebt.LabelProp = "0";
			//taraDebt = BottlesRepository.GetBottlesAtDeliveryPoint(UoW, DeliveryPoint);
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void RefreshDependency(ScriptTreeObject ste)
		{
			dependencyItems = GetDependency(ste);

		 	CorrectText();
		}

		List<OrderItem> GetDependency(ScriptTreeObject dependencyObject)
		{
			if(dependencyObject != null && dependencyObject.ResultObject is List<OrderItem>)
			{
				return dependencyObject.ResultObject as List<OrderItem>;
			}

			return null;
		}

		void CorrectText( )
		{
			if(dependencyItems == null || dependencyItems.Count == 0)
			{
				return;
			}
			 
			foreach(OrderItem item in dependencyItems)
			{
				if(item.Nomenclature.Category == Domain.Goods.NomenclatureCategory.water)
				{
					bottles += item.Count;
					item.ReturnedCount = item.Count;
				}
			}

			spinBottlesReturn.ValueAsInt = bottles;

			string[] corrections = { bottles.ToString() };
			this.TextCorrectionsPresent?.Invoke(this, new TextCorrectionsPresentEventArgs(corrections));
			this.SubWidgetDone?.Invoke(this, new SubWidgetDoneEventArgs(null));
		}

		protected void OnSpinBottlesReturnValueChanged(object sender, EventArgs e)
		{
		//	CorrectText(spinBottlesReturn.ValueAsInt);
		}

		//public void SendResults()
		//{
		//	var result = new ScriptTreeObject {
		//		ResultObjectType = newOrderList.GetType(),
		//		ResultObject = newOrderList as object
		//	};
		//	this.SubWidgetDone?.Invoke(this, new SubWidgetDoneEventArgs(result));
		//}
	}
}
