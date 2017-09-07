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
	public partial class DialogueOrderRepeatWidget : Gtk.Bin, IDialogueWidget
	{
		IUnitOfWork UoW;
		DeliveryPoint dependencyDeliveryPoint = new DeliveryPoint();
		List<OrderItemWithSelect> resultOrderItems;

		public DialogueOrderRepeatWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
			Configure();
		}

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

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		DeliveryPoint GetDependency(ScriptTreeObject dependencyObject)
		{
			if(dependencyObject != null && dependencyObject.ResultObject is DeliveryPoint)
			{
				var dependency = dependencyObject.ResultObject as DeliveryPoint;
				return dependency;
			}

			return null;
		}

		public void RefreshDependency(ScriptTreeObject ste)
		{
			dependencyDeliveryPoint = GetDependency(ste);
			ShowOrderItems(GetLastWaterOrder());
		}

		public void SendResults()
		{
			var resultList = new List<OrderItem>();

			foreach(OrderItemWithSelect item in resultOrderItems)
			{
				if(item.Selected)
				{
					resultList.Add(item.Item);
				}
			}

			if(resultList.Count == 0)
			{
				return;
			}

			var result = new ScriptTreeObject {
				ResultObjectType = resultList.GetType(),
				ResultObject = resultList as object
			};
			this.SubWidgetDone?.Invoke(this, new SubWidgetDoneEventArgs(result));
		}

		Order GetLastWaterOrder()
		{
			var order = OrderRepository.GetLatestWaterOrderForDeliveryPoint(UoW, dependencyDeliveryPoint);
			return order;
		}

		void ShowOrderItems(Order order)
		{
			var items = order.OrderItems.ToList();
			resultOrderItems = new List<OrderItemWithSelect>();

			foreach(OrderItem item in items)
			{
				resultOrderItems.Add(new OrderItemWithSelect(item));
			}
				
			treeItems.SetItemsSource<OrderItemWithSelect>(resultOrderItems);
		}

		protected void OnButtonAcceptClicked(object sender, EventArgs e)
		{
			SendResults();
		}
	}

	public class OrderItemWithSelect
	{
		bool selected;

		public bool Selected
		{
			get { return selected; }
			set { selected = value; }
		}

		OrderItem item;

		public OrderItem Item
		{
			get { return item; }
			set { item = value; }
		}

		public OrderItemWithSelect()
		{
			
		}

		public OrderItemWithSelect(OrderItem oItem)
		{
			Selected = true;
			Item = oItem;
		}
	}
}
