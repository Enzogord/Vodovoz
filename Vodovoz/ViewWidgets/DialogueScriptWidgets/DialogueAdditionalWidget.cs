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

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueAdditionalWidget : Gtk.Bin, IDialogueWidget
	{
		IUnitOfWork UoW;
		Order newOrder;
		List<OrderItem> newOrderList = new List<OrderItem>();
		DeliveryPoint dependencyDeliveryPoint = new DeliveryPoint();
 
		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public DialogueAdditionalWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
			Configure();
		}

		public void Configure()
		{
			treeItems.ColumnsConfig = ColumnsConfigFactory.Create<OrderItem>()
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

			treeItemsFreeRent.ColumnsConfig = ColumnsConfigFactory.Create<OrderItem>()
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

			treeItemsPaidRent.ColumnsConfig = ColumnsConfigFactory.Create<OrderItem>()
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
			treeItems.SetItemsSource<OrderItem>(newOrderList);
		}

		protected void OnButtonAddForSaleClicked(object sender, EventArgs e)
		{
			ITdiTab mytab = TdiHelper.FindMyTab(this);
			if(mytab == null)
				return;

			var nomenclatureFilter = new NomenclatureRepFilter(UoW);
			nomenclatureFilter.NomenCategory = NomenclatureCategory.additional;
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
		}
	}
}
