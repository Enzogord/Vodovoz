﻿using System;
using QSTDI;
using QSOrmProject;
using Vodovoz.Domain.Logistic;
using NHibernate.Criterion;
using Vodovoz.Domain.Documents;
using Gamma.ColumnConfig;
using Gamma.GtkWidgets;
using System.Collections.Generic;

namespace Vodovoz
{
	public partial class TransferGoodsBetweenRLDlg : TdiTabBase, ITdiDialog, IOrmDialog
	{
		#region Поля

		private IUnitOfWork uow = UnitOfWorkFactory.CreateWithoutRoot();

		IColumnsConfig colConfigFrom = ColumnsConfigFactory.Create<CarUnloadDocumentNode>()
			.AddColumn("Номенклатура").AddTextRenderer(d => d.Nomenclature)
			.AddColumn("Кол-во").AddNumericRenderer(d => d.ItemsCount)
			.AddColumn("Перенести").AddNumericRenderer(d => d.TransferCount)
				.Adjustment(new Gtk.Adjustment(0, 0, 1000, 1, 1, 1)).Editing()
			.AddColumn("Остаток").AddNumericRenderer(d => d.Residue)
			.Finish();

		IColumnsConfig colConfigTo = ColumnsConfigFactory.Create<CarUnloadDocumentNode>()
			.AddColumn("Номенклатура").AddTextRenderer(d => d.Nomenclature)
			.AddColumn("Кол-во").AddNumericRenderer(d => d.ItemsCount)
			.Finish();
		#endregion

		public TransferGoodsBetweenRLDlg()
		{
			this.Build();
			this.TabName = "Перенос разгрузок";
			ConfigureDlg();
		}

		#region ITdiDialog implementation

		public event EventHandler<EntitySavedEventArgs> EntitySaved;

		public bool Save()
		{
			return false;
		}

		public void SaveAndClose()
		{
			throw new NotImplementedException();
		}

		public bool HasChanges {
			get {
				return false;
			}
		}

		#endregion

		#region IOrmDialog implementation

		public IUnitOfWork UoW { get { return uow; } }

		public object EntityObject { get { return null; } }

		#endregion

		#region Методы

		private void ConfigureDlg()
		{
			//Настройка элементов откуда переносим
			RouteListsFilter filterFrom = new RouteListsFilter(UoW);
			//filterFrom.SetFilterDates(DateTime.Today.AddDays(-7), DateTime.Today.AddDays(1));
			yentryreferenceRouteListFrom.RepresentationModel = new ViewModel.RouteListsVM(filterFrom);
			yentryreferenceRouteListFrom.Changed += YentryreferenceRouteListFrom_Changed;

			ylistcomboReceptionTicketFrom.SetRenderTextFunc<CarUnloadDocument>(d => $"Талон разгрузки №{d.Id}. Склад {d.Warehouse.Name}");
			ylistcomboReceptionTicketFrom.ItemSelected += YlistcomboReceptionTicketFrom_ItemSelected;

			ytreeviewFrom.ColumnsConfig = colConfigFrom;

			//Настройка компонентов куда переносим
			RouteListsFilter filterTo = new RouteListsFilter(UoW);
			filterTo.SetFilterDates(DateTime.Today.AddDays(-7), DateTime.Today.AddDays(1));
			yentryreferenceRouteListTo.RepresentationModel = new ViewModel.RouteListsVM(filterTo);
		}
		
		#endregion

		#region Обработчики событий

		void YentryreferenceRouteListFrom_Changed (object sender, EventArgs e)
		{
			if (yentryreferenceRouteListFrom.Subject == null)
				return;

			RouteList routeList = (RouteList)yentryreferenceRouteListFrom.Subject;

			var unloadDocs = UoW.Session.QueryOver<CarUnloadDocument>()
				.Where(cud => cud.RouteList.Id == routeList.Id).List();
			
			ylistcomboReceptionTicketFrom.ItemsList = unloadDocs;
		}

		void YlistcomboReceptionTicketFrom_ItemSelected (object sender, Gamma.Widgets.ItemSelectedEventArgs e)
		{
			CarUnloadDocument selectedItem = (CarUnloadDocument)e.SelectedItem;

			var result = new List<CarUnloadDocumentNode>();
			foreach (var item in selectedItem.Items)
			{
				result.Add(new CarUnloadDocumentNode{DocumentItem = item});
			}

			ytreeviewFrom.SetItemsSource(result);
		}

		protected void OnButtonTransferClicked (object sender, EventArgs e)
		{
			
		}

		#endregion

		#region Внутренние классы

		private class CarUnloadDocumentNode {
			public string Id 			{ get {return DocumentItem.Id.ToString();} }
			public string Nomenclature 	{ get {return DocumentItem.MovementOperation.Nomenclature.OfficialName;} }
			public int 	  ItemsCount 	{ get {return (int)DocumentItem.MovementOperation.Amount;} }

			private int transferCount = 0;
			public int 	  TransferCount { get {return transferCount;}	
				set {
					transferCount = value;
					if (value < 0)
						transferCount = 0;
					if (value > ItemsCount)
						transferCount = ItemsCount;
				} }
			
			public int 	  Residue 		{ get { return ItemsCount - TransferCount;} }

			public CarUnloadDocumentItem DocumentItem { get; set; }
		}
		#endregion
	}
}
