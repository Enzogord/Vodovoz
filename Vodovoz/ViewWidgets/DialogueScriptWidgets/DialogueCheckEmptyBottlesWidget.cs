using System;
using System.Collections.Generic;
using QSOrmProject;
using Vodovoz.Domain;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueCheckEmptyBottlesWidget : Gtk.Bin, IDialogueWidget
	{
		IUnitOfWork UoW;
		List<OrderItem> dependencyItems;

		public DialogueCheckEmptyBottlesWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
			Configure();
		}

		public void Configure()
		{
			
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
				var dependency = dependencyObject.ResultObject as List<OrderItem>;
				return dependency;
			}

			return null;
		}

		void CorrectText()
		{
			if(dependencyItems == null || dependencyItems.Count == 0)
			{
				return;
			}

			int bottles = 0;

			foreach(OrderItem item in dependencyItems)
			{
				if(item.Nomenclature.Category == Domain.Goods.NomenclatureCategory.water)
				{
					bottles += item.Count;
				}
			}

			string[] corrections = { bottles.ToString() };
			this.TextCorrectionsPresent?.Invoke(this, new TextCorrectionsPresentEventArgs(corrections));
			this.SubWidgetDone?.Invoke(this, new SubWidgetDoneEventArgs(null));
		}
	}
}
