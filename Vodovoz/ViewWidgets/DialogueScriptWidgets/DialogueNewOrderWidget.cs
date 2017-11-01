using System;
using System.Collections.Generic;
using System.Linq;
using Gamma.GtkWidgets;
using Gtk;
using QSOrmProject;
using QSProjectsLib;
using QSTDI;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.JournalFilters;
using Vodovoz.Repository;
using Vodovoz.Repository.Operations;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueNewOrderWidget : Gtk.Bin, IDialogueWidget
	{
	#region fields
		IUnitOfWork UoW;
		Order newOrder;
		List<OrderItem> newOrderList = new List<OrderItem>();
		DeliveryPoint dependencyDeliveryPoint = new DeliveryPoint();
	 	List<OrderItemWithSelect> resultOrderItems;

		Gtk.Label label;
		Gtk.Label dialogue;
		Gtk.Button buttonSell;
		Gtk.Button buttonAccept;

		//Gtk.Button buttonYes;
		//Gtk.Button buttonNo;

		Gtk.ScrolledWindow ScrolledWindowNew;
		Gamma.GtkWidgets.yTreeView treeItemsNew;

		Gtk.Label labelTara;
	#endregion

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public DialogueNewOrderWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
			Configure();
		}

		public void Configure()
		{
	#region AddUIElements
			ScrolledWindowNew = new global::Gtk.ScrolledWindow();
			ScrolledWindowNew.Name = "GtkScrolledWindow";
			ScrolledWindowNew.VscrollbarPolicy = ((Gtk.PolicyType)(2));
			ScrolledWindowNew.ShadowType = ((Gtk.ShadowType)(1));

			treeItemsNew = new Gamma.GtkWidgets.yTreeView();
			treeItemsNew.CanFocus = true;
			treeItemsNew.Name = "treeItemsNew";

			ScrolledWindowNew.Add(this.treeItemsNew);
			ScrolledWindowNew.ShowAll();

			vbox1.Add(ScrolledWindowNew);
			vbox1.PackEnd(ScrolledWindowNew, true, true, 0);
			vbox1.ShowAll();
	#endregion
		}
		 
		public void ConfigureNewOrder()
		{
			buttonSell = new Gtk.Button();
			buttonSell.Label = "Выбрать воду";
			buttonSell.Clicked += OnButtonAddForSaleClicked;
			buttonSell.Show();

			var hboxTop = new Gtk.HBox();
			hboxTop.PackStart(buttonSell, false, true, 0);
			hboxTop.Show();

			vbox1.PackStart(hboxTop, true, true, 0);

			CompleateOrderButton(false);

			treeItemsNew.ColumnsConfig = ColumnsConfigFactory.Create<OrderItem>()
				.AddColumn("Номенклатура").SetDataProperty(node => node.NomenclatureString)
				.AddColumn("Кол-во").AddNumericRenderer(node => node.Count)
				.Adjustment(new Adjustment(0, 0, 1000000, 1, 100, 0))
				.AddSetter((c, node) => c.Digits = node.Nomenclature.Unit == null ? 0 : (uint)node.Nomenclature.Unit.Digits)
				.AddSetter((c, node) => c.Editable = node.CanEditAmount).WidthChars(10)
				.AddTextRenderer(node => node.Nomenclature.Unit == null ? String.Empty : node.Nomenclature.Unit.Name, false)
				.AddColumn("Цена").AddNumericRenderer(node => node.Price).Digits(2).WidthChars(10)
				.Adjustment(new Adjustment(0, 0, 1000000, 1, 100, 0)).Editing(true)
				.AddSetter((c, node) => c.Editable = Nomenclature.GetCategoriesWithEditablePrice().Contains(node.Nomenclature.Category))
				.AddTextRenderer(node => CurrencyWorks.CurrencyShortName, false)
				.AddColumn("В т.ч. НДС").AddTextRenderer(node => CurrencyWorks.GetShortCurrencyString(node.IncludeNDS))
				.AddColumn("Сумма").AddTextRenderer(node => CurrencyWorks.GetShortCurrencyString(node.Sum))
				.AddColumn("Скидка %").AddNumericRenderer(node => node.Discount)
				.Adjustment(new Adjustment(0, 0, 100, 1, 100, 1)).Editing(true)
				.Finish();

			newOrder = new Order();
		}

		private void CompleateOrderButton(Boolean buttonSensitive)
		{
			buttonAccept = new Gtk.Button();
			buttonAccept.Label = "Сформировать заказ";
			buttonAccept.Clicked += OnButtonAcceptClicked;
			buttonAccept.Sensitive = buttonSensitive;
			buttonAccept.Show();
			 
			var hboxBottom = new Gtk.HBox();
			hboxBottom.PackStart(buttonAccept, false, true, 0);
			hboxBottom.Show();
			vbox1.PackStart(hboxBottom, true, true, 0);
		}

		public void ConfigureRepeateOrder()
		{

			treeItemsNew.ColumnsConfig = ColumnsConfigFactory.Create<OrderItemWithSelect>()
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

			ShowOrderItems(GetLastWaterOrder());

			CompleateOrderButton(true);
		}

		public void ConfiguringChekBottles()
		{
			int bottles = 0;

			if(newOrderList.Count > 0)
				foreach(OrderItem item in newOrderList) {
					if(item.Nomenclature.Category == NomenclatureCategory.water) {
						bottles += item.Count;
						item.ReturnedCount = item.Count;
					}
				}


			var taraDebt = BottlesRepository.GetBottlesAtDeliveryPoint(UoW, dependencyDeliveryPoint);

	#region AddUIElements
			labelTara = new Label();
			labelTara.LabelProp = "пустых бутылок на обмен будет у Вас?";
			labelTara.Show();

			Gtk.SpinButton spinButton = new SpinButton(0, 65535, 1);
			spinButton.Value = bottles;
			spinButton.Show();

			var hboxBottom2 = new Gtk.HBox();
			hboxBottom2.PackStart(labelTara, false, true, 0);
			hboxBottom2.PackStart(spinButton, false, true, 0);
			hboxBottom2.Show();

			var labelTaraDebtText = new Label();
			labelTaraDebtText.LabelProp = "долг по точке доставки:";
			labelTaraDebtText.Show();

			var labelTaraDebt = new Label();
			labelTaraDebt.LabelProp = taraDebt.ToString();
			labelTaraDebt.Show();

			var hboxBottom3 = new Gtk.HBox();
			hboxBottom3.PackStart(labelTaraDebtText, false, true, 0);
			hboxBottom3.PackStart(labelTaraDebt, false, true, 0);
			hboxBottom3.Show();

			vbox1.PackStart(hboxBottom2, true, true, 0);
			vbox1.PackEnd(hboxBottom3, true, true, 0);
	#endregion
		}



		DeliveryPoint GetDependency(ScriptTreeObject dependencyObject)
		{
			if(dependencyObject != null && dependencyObject.ResultObject is DeliveryPoint) {
				var dependency = dependencyObject.ResultObject as DeliveryPoint;
				return dependency;
			}

			return null;
		}

		public void RefreshDependency(ScriptTreeObject ste)
		{
			dependencyDeliveryPoint = GetDependency(ste);

		}


		void ShowOrderItems()
		{
			treeItemsNew.SetItemsSource<OrderItem>(newOrderList);
		}

		protected void OnButtonAddForSaleClicked(object sender, EventArgs e)
		{
			ITdiTab mytab = TdiHelper.FindMyTab(this);
			if(mytab == null)
				return;

			var nomenclatureFilter = new NomenclatureRepFilter(UoW);
			nomenclatureFilter.NomenCategory = NomenclatureCategory.water;
			ReferenceRepresentation SelectDialog = new ReferenceRepresentation(new ViewModel.NomenclatureForSaleVM(nomenclatureFilter));
			SelectDialog.Mode = OrmReferenceMode.Select;
			SelectDialog.TabName = "Номенклатура на продажу";
			SelectDialog.ObjectSelected += NomenclatureForSaleSelected;
			mytab.TabParent.AddSlaveTab(mytab, SelectDialog);
		}

		void NomenclatureForSaleSelected(object sender, ReferenceRepresentationSelectedEventArgs e)
		{
			AddNomenclature(UoW.Session.Get<Nomenclature>(e.ObjectId));
		}

		void AddNomenclature(Nomenclature nomenclature)
		{

			newOrderList.Add(new OrderItem {
				//Order = newOrder,
				//AdditionalAgreement = wsa,
				Count = 0,
				Equipment = null,
				Nomenclature = nomenclature,
				Price = nomenclature.GetPrice(1)
			});

			ShowOrderItems();

			buttonAccept.Sensitive = true;
		}

		public void SendResults()
		{
			var result = new ScriptTreeObject {
				ResultObjectType = newOrderList.GetType(),
				ResultObject = newOrderList as object
			};
			this.SubWidgetDone?.Invoke(this, new SubWidgetDoneEventArgs(result));
		}

		protected void OnButtonAcceptClicked(object sender, EventArgs e)
		{
			SendResults();
			ConfiguringChekBottles();
		}

		protected void OnButtonNewOrderClicked(object sender, EventArgs e)
		{
			ConfigureNewOrder();
			buttonRepeateOrder.Sensitive = false;
		}

		protected void OnButtonRepeateOrderClicked(object sender, EventArgs e)
		{
			ConfigureRepeateOrder();
			buttonNewOrder.Sensitive = false;
		}

		Order GetLastWaterOrder()
		{
			return OrderRepository.GetLatestWaterOrderForDeliveryPoint(UoW, dependencyDeliveryPoint);
		}

		void ShowOrderItems(Order order)
		{
			if(order == null) {
				MessageDialogWorks.RunInfoDialog(String.Format("По точке доставки не было заказов :\n*{0}", String.Join("\n*", dependencyDeliveryPoint.Address1c)));
				buttonRepeateOrder.Sensitive = false;
				buttonNewOrder.Sensitive = true;
				return;
			}
			
			var items = order.OrderItems.ToList();
			resultOrderItems = new List<OrderItemWithSelect>();

			foreach(OrderItem item in items) {
				resultOrderItems.Add(new OrderItemWithSelect(item));

				newOrderList.Add(item);
			}

			treeItemsNew.SetItemsSource<OrderItemWithSelect>(resultOrderItems);
		}
	}

	public class OrderItemWithSelect
	{
		bool selected;

		public bool Selected {
			get { return selected; }
			set { selected = value; }
		}

		OrderItem item;

		public OrderItem Item {
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
