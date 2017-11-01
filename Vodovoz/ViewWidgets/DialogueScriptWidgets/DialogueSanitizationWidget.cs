using System;
using System.Collections.Generic;
using System.Linq;
using Gamma.GtkWidgets;
using Gtk;
using QSOrmProject;
using QSProjectsLib;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Repository;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueSanitizationWidget : Gtk.Bin, IDialogueWidget
	{
		string resultString;
		IUnitOfWork UoW;
		DeliveryPoint dependencyDeliveryPoint = new DeliveryPoint();
		List<OrderItemWithSelect> resultOrderItems;


		public DialogueSanitizationWidget(IUnitOfWork UoW)
		DeliveryPoint dependencyDeliveryPoint = new DeliveryPoint();
		List<OrderItemWithSelect> resultOrderItems;


		public DialogueSanitizationWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
			Configure();
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void Configure()
		{
			treeItems.ColumnsConfig = ColumnsConfigFactory.Create<OrderItemWithSelect>()
				.AddColumn("").AddToggleRenderer(node => node.Selected)
				.AddColumn("Номенклатура").SetDataProperty(node => node.Item.NomenclatureString)
				.AddColumn("Кол-во").AddNumericRenderer(node => node.Item.Count)
				.Adjustment(new Adjustment(0, 0, 1000000, 1, 100, 0))
				.AddSetter((c, node) => c.Digits = node.Item.Nomenclature.Unit == null ? 0 : (uint)node.Item.Nomenclature.Unit.Digits)
				.AddSetter((c, node) => c.Editable = node.Item.CanEditAmount).WidthChars(10)
				.AddTextRenderer(node => node.Item.Nomenclature.Unit == null ? String.Empty : node.Item.Nomenclature.Unit.Name, false)
				.AddColumn("Цена").AddNumericRenderer(node => node.Item.Price).Digits(2).WidthChars(10)
				.Adjustment(new Adjustment(0, 0, 1000000, 1, 100, 0)).Editing(true)
				.AddSetter((c, node) => c.Editable = Nomenclature.GetCategoriesWithEditablePrice().Contains(node.Item.Nomenclature.Category))
				.AddTextRenderer(node => CurrencyWorks.CurrencyShortName, false)
				.AddColumn("В т.ч. НДС").AddTextRenderer(node => CurrencyWorks.GetShortCurrencyString(node.Item.IncludeNDS))
				.AddColumn("Сумма").AddTextRenderer(node => CurrencyWorks.GetShortCurrencyString(node.Item.Sum))
				.AddColumn("Скидка %").AddNumericRenderer(node => node.Item.Discount)
				.Adjustment(new Adjustment(0, 0, 100, 1, 100, 1)).Editing(true)
				.Finish();
		}

		public void SendResult()
		{
			var result = new ScriptTreeObject {
				ResultObjectType = resultString.GetType(),
				ResultObject = resultString as object
			};
			this.SubWidgetDone(this, new SubWidgetDoneEventArgs(result));
		}

		DeliveryPoint GetDependency(ScriptTreeObject dependencyObject)
		{
			if(dependencyObject != null && dependencyObject.ResultObject is DeliveryPoint) {
				return dependencyObject.ResultObject as DeliveryPoint;
			}

			return null;
		}

		public void RefreshDependency(ScriptTreeObject ste)
		{
			dependencyDeliveryPoint = GetDependency(ste);
			ShowOrderItems(GetLastSanitization());
		}

		void ShowOrderItems(Order order)
		{
			if(order == null)
				return;
			var items = order.OrderItems.ToList();
			resultOrderItems = new List<OrderItemWithSelect>();

			foreach(OrderItem item in items) {
				resultOrderItems.Add(new OrderItemWithSelect(item));
			}

			treeItems.SetItemsSource<OrderItemWithSelect>(resultOrderItems);
		}

		Order GetLastSanitization()
		{
			return OrderRepository.GetLatestSanitizationForDeliveryPoint(UoW, dependencyDeliveryPoint);
		}
	}
}
