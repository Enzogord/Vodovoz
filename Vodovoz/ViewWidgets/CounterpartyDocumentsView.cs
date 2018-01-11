﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gamma.Binding;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using NHibernate.Proxy;
using NHibernate.Transform;
using QSOrmProject;
using QSReport;
using QSTDI;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;

namespace Vodovoz.ViewWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CounterpartyDocumentsView : Gtk.Bin
	{
		private IUnitOfWorkGeneric<Counterparty> counterpartyUoW;

		public IUnitOfWorkGeneric<Counterparty> CounterpartyUoW {
			get {
				return counterpartyUoW;
			}
			set {
				if(counterpartyUoW == value)
					return;
				counterpartyUoW = value;

				Counterparty counterparty = CounterpartyUoW.Root;

				OrderDocument orderDocumentAlias = null;
				CounterpartyContract contractAlias = null;
				Order orderAlias = null;
				AdditionalAgreement agreementAlias = null;

				//получаем список документов
				var orderDocuments = UnitOfWorkFactory
					.CreateWithoutRoot()
					.Session.QueryOver<OrderDocument>(() => orderDocumentAlias)
					.JoinAlias(x => x.Order, () => orderAlias, NHibernate.SqlCommand.JoinType.InnerJoin)
					.JoinAlias(() => orderAlias.Contract, () => contractAlias, NHibernate.SqlCommand.JoinType.InnerJoin)
					.Where(() => orderAlias.Id == orderDocumentAlias.Order.Id && contractAlias.Id == orderAlias.Contract.Id)
					.Where(() => contractAlias.Counterparty.Id == counterparty.Id)
					.List();

				//получаем список доп. соглашений
				var agreements = UnitOfWorkFactory
					.CreateWithoutRoot()
					.Session.QueryOver<AdditionalAgreement>(() => agreementAlias)
					.JoinAlias(x => x.Contract, () => contractAlias, NHibernate.SqlCommand.JoinType.InnerJoin)
					.Where(() => agreementAlias.Contract.Id == contractAlias.Id && contractAlias.Counterparty.Id == counterparty.Id)
					.List();

				//получаем список контрактов
				var contracts = UnitOfWorkFactory
					.CreateWithoutRoot()
					.Session.QueryOver<CounterpartyContract>(() => contractAlias)
					.Where(() => contractAlias.Counterparty.Id == counterparty.Id)
					.List();

				List<CounterpartyDocumentNode> CounterpartyDocs = new List<CounterpartyDocumentNode>();
				foreach(var contract in contracts) {
					CounterpartyDocumentNode contractNode = new CounterpartyDocumentNode();
					contractNode.Document = contract;
					contractNode.Documents = new List<CounterpartyDocumentNode>();

					//добавление доп. соглашений к документам договора
					var contractAgreements = agreements.Where(x => x.Contract.Id == contract.Id).ToList();
					foreach(var agreement in contractAgreements) {
						CounterpartyDocumentNode agreementNode = new CounterpartyDocumentNode();
						agreementNode.Document = agreement;
						agreementNode.Parent = contractNode;
						agreementNode.Documents = new List<CounterpartyDocumentNode>();

						//добавление гарантийных листов к документам доп. соглашения
						List<OrderDocument> agreementWarranties = new List<OrderDocument>();
						var agreementCoolers = orderDocuments
									.OfType<CoolerWarrantyDocument>()
									.Where(x => x.Contract != null && x.AdditionalAgreement != null)
									.Where(x => x.Contract.Id == contract.Id)
									.Where(x => x.AdditionalAgreement.Id == agreement.Id)
									.Cast<OrderDocument>()
									.ToList();
						var agreementPumps = orderDocuments
									.OfType<PumpWarrantyDocument>()
									.Where(x => x.Contract != null && x.AdditionalAgreement != null)
									.Where(x => x.Order.Contract.Id == contract.Id)
									.Where(x => x.AdditionalAgreement.Id == agreement.Id)
									.Cast<OrderDocument>()
									.ToList();
						agreementWarranties.AddRange(agreementCoolers);
						agreementWarranties.AddRange(agreementPumps);
						foreach(var warranty in agreementWarranties) {
							CounterpartyDocumentNode warrantyNode = new CounterpartyDocumentNode();
							warrantyNode.Document = warranty;
							warrantyNode.Parent = agreementNode;
							agreementNode.Documents.Add(warrantyNode);
						}

						contractNode.Documents.Add(agreementNode);
					}

					//добавление гарантийных листов к документам договора
					List<OrderDocument> contractWarranties = new List<OrderDocument>();
					var contractCoolers = orderDocuments
							.OfType<CoolerWarrantyDocument>()
							.Where(x => x.Contract != null)
							.Where(x => x.Order.Contract.Id == contract.Id)
							.Where(x => x.AdditionalAgreement == null)
							.Cast<OrderDocument>()
							.ToList();
					var contractPumps = orderDocuments
							.OfType<PumpWarrantyDocument>()
							.Where(x => x.Contract != null)
							.Where(x => x.Order.Contract.Id == contract.Id)
							.Where(x => x.AdditionalAgreement == null)
							.Cast<OrderDocument>()
							.ToList();
					contractWarranties.AddRange(contractCoolers);
					contractWarranties.AddRange(contractPumps);
					foreach(var warranty in contractWarranties) {
						CounterpartyDocumentNode warrantyNode = new CounterpartyDocumentNode();
						warrantyNode.Document = warranty;
						warrantyNode.Parent = contractNode;
						contractNode.Documents.Add(warrantyNode);
					}

					CounterpartyDocs.Add(contractNode);
				}

				ytreeDocuments.YTreeModel = new RecursiveTreeModel<CounterpartyDocumentNode>(CounterpartyDocs, x => x.Parent, x => x.Documents);
			}
		}

		public CounterpartyDocumentsView()
		{
			this.Build();
			ytreeDocuments.Selection.Mode = Gtk.SelectionMode.Single;
			ytreeDocuments.Selection.Changed += OnSelectionChanged;
			ytreeDocuments.RowActivated += (o, args) => buttonViewDocument.Click();

			ytreeDocuments.ColumnsConfig = FluentColumnsConfig<CounterpartyDocumentNode>.Create()
				.AddColumn("Документ").AddTextRenderer(x => x.Title)
				.AddColumn("Номер").AddTextRenderer(x => x.Number)
				.AddColumn("Дата").AddTextRenderer(x => x.Date)
				.AddColumn("Количество кулеров/помп").AddTextRenderer(x => x.EquipmentCount)
				.Finish();
		}

		void OnSelectionChanged(object sender, EventArgs e)
		{
			
		}


		protected void OnTreeCounterpartyContractsRowActivated(object o, Gtk.RowActivatedArgs args)
		{
			
		}

		protected void OnButtonViewDocumentClicked(object sender, EventArgs e)
		{
			ITdiTab mytab = TdiHelper.FindMyTab(this);
			if(mytab == null)
				return;

			CounterpartyDocumentNode selectedPrintableDocuments = (ytreeDocuments.GetSelectedObject() as CounterpartyDocumentNode);
			if(selectedPrintableDocuments.Document is CounterpartyContract) {
				int contractID = (selectedPrintableDocuments.Document as CounterpartyContract).Id;
				ITdiDialog dlg = new CounterpartyContractDlg(contractID);
				mytab.TabParent.AddTab(dlg, mytab);
			}
			if(selectedPrintableDocuments.Document is AdditionalAgreement) {
				AdditionalAgreement agreement = (selectedPrintableDocuments.Document as AdditionalAgreement);
				var type = NHibernateProxyHelper.GuessClass(agreement);
				mytab.TabParent.OpenTab(
					OrmMain.GenerateDialogHashName(type, agreement.Id),
					() => OrmMain.CreateObjectDialog(type, agreement.Id)
				);
			}
			if(selectedPrintableDocuments.Document is OrderDocument) {
				OrderDocument orderDoc = (selectedPrintableDocuments.Document as OrderDocument);
				if(orderDoc.PrintType != PrinterType.None){
					mytab.TabParent.AddTab(DocumentPrinter.GetPreviewTab(orderDoc), mytab);
				}
			}
		}
	}

	public class CounterpartyDocumentNode
	{
		public string Title {
			get{
				if(Document is CounterpartyContract) {
					return String.Format("Договор");
				}

				if(Document is AdditionalAgreement) {
					return String.Format("Доп. соглашение {0}",
					                     (Document as AdditionalAgreement).AgreementTypeTitle);
				}

				if(Document is CoolerWarrantyDocument) {
					return String.Format("Гарантийный талон на кулера");
				}

				if(Document is PumpWarrantyDocument) {
					return String.Format("Гарантийный талон на помпы");
				}

				return "";
			}
		}

		public string Number {
			get {
				if(Document is CounterpartyContract) {
					return (Document as CounterpartyContract).Id.ToString();
				}

				if(Document is AdditionalAgreement) {
					return (Document as AdditionalAgreement).FullNumberText;
				}

				if(Document is CoolerWarrantyDocument) {
					return (Document as CoolerWarrantyDocument).WarrantyFullNumber;
				}

				if(Document is PumpWarrantyDocument) {
					return (Document as PumpWarrantyDocument).WarrantyFullNumber;
				}

				return "";
			}
		}

		public string Date {
			get {
				if(Document is CounterpartyContract) {
					return (Document as CounterpartyContract).IssueDate.ToShortDateString();
				}

				if(Document is AdditionalAgreement) {
					return (Document as AdditionalAgreement).StartDate.ToShortDateString();
				}

				if(Document is CoolerWarrantyDocument) {
					return (Document as CoolerWarrantyDocument).Date;
				}

				if(Document is PumpWarrantyDocument) {
					return (Document as PumpWarrantyDocument).Date;
				}

				return "";
			}
		}

		public string EquipmentCount {
			get {
				if(Document is CoolerWarrantyDocument) {
					return (Document as CoolerWarrantyDocument)
						.Order.OrderItems
						.Where(x => x.AdditionalAgreement == (Document as CoolerWarrantyDocument).AdditionalAgreement)
						.Where(x => x.Nomenclature.Type != null)
						.Where(x => x.Nomenclature.Type.WarrantyCardType == WarrantyCardType.CoolerWarranty)
						.Select(x => x.Count)
						.Sum()
						.ToString();
				}
				if(Document is PumpWarrantyDocument) {
					return (Document as PumpWarrantyDocument)
						.Order.OrderItems
						.Where(x => x.AdditionalAgreement == (Document as PumpWarrantyDocument).AdditionalAgreement)
						.Where(x => x.Nomenclature.Type != null)
						.Where(x => x.Nomenclature.Type.WarrantyCardType == WarrantyCardType.PumpWarranty)
						.Select(x => x.Count)
						.Sum()
						.ToString();
				}
				return "";
			}
		}

		public object Document { get; set; }

		public CounterpartyDocumentNode Parent { get; set; }

		public List<CounterpartyDocumentNode> Documents { get; set; }

	}

	public enum CounterpartyDocumentsTypes
	{
		Contract,
		AdditionalAgreement,
		Warranty
	}
}