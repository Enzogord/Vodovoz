﻿using System;
using System.Linq;
using Gamma.Utilities;
using Gtk;
using NLog;
using QS.DomainModel.UoW;
using QSOrmProject;
using QSOrmProject.UpdateNotification;
using QSTDI;
using Vodovoz.Additions.Store;
using Vodovoz.Core;
using Vodovoz.Core.Permissions;
using Vodovoz.Dialogs.DocumentDialogs;
using Vodovoz.Domain.Documents;
using Vodovoz.ViewModel;

namespace Vodovoz
{
	public partial class WarehouseDocumentsView : TdiTabBase
	{
		static Logger logger = LogManager.GetCurrentClassLogger ();

		IUnitOfWork uow;

		public WarehouseDocumentsView ()
		{
			this.Build ();
			this.TabName = "Журнал документов";
			tableDocuments.RepresentationModel = new DocumentsVM ();
			hboxFilter.Add (tableDocuments.RepresentationModel.RepresentationFilter as Widget);
			(tableDocuments.RepresentationModel.RepresentationFilter as Widget).Show ();
			uow = tableDocuments.RepresentationModel.UoW;
			tableDocuments.Selection.Changed += OnSelectionChanged;
			buttonEdit.Sensitive = buttonDelete.Sensitive = false;
			buttonAdd.ItemsEnum = typeof(DocumentType);

			var allPermissions = CurrentPermissions.Warehouse.AnyEntities();
			foreach(DocumentType doctype in Enum.GetValues(typeof(DocumentType))) {
				if(allPermissions.Any(x => x.GetAttributes<DocumentTypeAttribute>().Any(at => at.Type.Equals(doctype))))
					continue;
				buttonAdd.SetSensitive(doctype, false);
			}
		}

		void OnRefObjectUpdated (object sender, OrmObjectUpdatedEventArgs e)
		{
			tableDocuments.RepresentationModel.UpdateNodes ();
		}

		void OnSelectionChanged (object sender, EventArgs e)
		{
			bool isSensitive = tableDocuments.Selection.CountSelectedRows() > 0;
			buttonEdit.Sensitive = isSensitive;
			if(isSensitive) {
				var node = tableDocuments.GetSelectedObject<DocumentVMNode>();
				if(node.DocTypeEnum == DocumentType.ShiftChangeDocument) {
					var doc = uow.GetById<ShiftChangeWarehouseDocument>(node.Id);
					isSensitive = isSensitive && StoreDocumentHelper.CanEditDocument(WarehousePermissions.ShiftChangeEdit, doc.Warehouse);
				}
			}
			buttonDelete.Sensitive = isSensitive;
		}

		protected void OnButtonAddEnumItemClicked (object sender, EnumItemClickedEventArgs e)
		{
			DocumentType type = (DocumentType)e.ItemEnum;
			TabParent.OpenTab(
				OrmMain.GenerateDialogHashName(Document.GetDocClass(type), 0),
				() => OrmMain.CreateObjectDialog(Document.GetDocClass(type)),
				this);
		}

		protected void OnTableDocumentsRowActivated (object o, RowActivatedArgs args)
		{
			buttonEdit.Click ();
		}

		protected void OnButtonEditClicked (object sender, EventArgs e)
		{
			if (tableDocuments.GetSelectedObjects ().GetLength (0) > 0) {
				int id = (tableDocuments.GetSelectedObjects () [0] as ViewModel.DocumentVMNode).Id;
				DocumentType DocType = (tableDocuments.GetSelectedObjects () [0] as ViewModel.DocumentVMNode).DocTypeEnum;

				switch (DocType) {
					case DocumentType.IncomingInvoice:
						TabParent.OpenTab(
							OrmMain.GenerateDialogHashName<IncomingInvoice>(id),
							() => new IncomingInvoiceDlg (id),
							this);
						break;
					case DocumentType.IncomingWater:
						TabParent.OpenTab(
							OrmMain.GenerateDialogHashName<IncomingWater>(id),
							() => new IncomingWaterDlg (id),
							this);
						break;
					case DocumentType.MovementDocument: 
						TabParent.OpenTab(
							OrmMain.GenerateDialogHashName<MovementDocument>(id),
							() => new MovementDocumentDlg (id),
							this);
						break;
					case DocumentType.WriteoffDocument:
						TabParent.OpenTab(
							OrmMain.GenerateDialogHashName<WriteoffDocument>(id),
							() => new WriteoffDocumentDlg (id),
							this);
						break;
					case DocumentType.InventoryDocument:
						TabParent.OpenTab(
							OrmMain.GenerateDialogHashName<InventoryDocument>(id),
							() => new InventoryDocumentDlg (id),
							this);
						break;
					case DocumentType.ShiftChangeDocument:
						TabParent.OpenTab(
							OrmMain.GenerateDialogHashName<ShiftChangeWarehouseDocument>(id),
							() => new ShiftChangeWarehouseDocumentDlg(id),
							this);
						break;
					case DocumentType.RegradingOfGoodsDocument:
						TabParent.OpenTab(
							OrmMain.GenerateDialogHashName<RegradingOfGoodsDocument>(id),
							() => new RegradingOfGoodsDocumentDlg (id),
							this);
						break;
					case DocumentType.SelfDeliveryDocument:
						TabParent.OpenTab(
							OrmMain.GenerateDialogHashName<SelfDeliveryDocument>(id),
							() => new SelfDeliveryDocumentDlg (id),
							this);
						break;
					case DocumentType.CarLoadDocument:
						TabParent.OpenTab(
							OrmMain.GenerateDialogHashName<CarLoadDocument>(id),
							() => new CarLoadDocumentDlg (id),
							this);
						break;
					case DocumentType.CarUnloadDocument:
						TabParent.OpenTab(
							OrmMain.GenerateDialogHashName<CarUnloadDocument>(id),
							() => new CarUnloadDocumentDlg (id),
							this);
						break;
				default:
					throw new NotSupportedException ("Тип документа не поддерживается.");
				}
			}
		}

		protected void OnButtonDeleteClicked (object sender, EventArgs e)
		{
			var item = tableDocuments.GetSelectedObject<ViewModel.DocumentVMNode>();
			if(OrmMain.DeleteObject (Document.GetDocClass(item.DocTypeEnum), item.Id))
				tableDocuments.RepresentationModel.UpdateNodes ();
		}

		protected void OnButtonFilterToggled (object sender, EventArgs e)
		{
			hboxFilter.Visible = buttonFilter.Active;
		}

		protected void OnSearchentity1TextChanged(object sender, EventArgs e)
		{
			tableDocuments.SearchHighlightText = searchentity1.Text;
			tableDocuments.RepresentationModel.SearchString = searchentity1.Text;
		}

		protected void OnButtonRefreshClicked(object sender, EventArgs e)
		{
			tableDocuments.RepresentationModel.UpdateNodes ();
		}
	}
}

