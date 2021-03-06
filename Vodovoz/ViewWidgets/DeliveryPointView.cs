﻿using System;
using System.Collections.Generic;
using QS.DomainModel.UoW;
using QSOrmProject;
using QSProjectsLib;
using QS.Tdi;
using Vodovoz.Domain.Client;
using Vodovoz.ViewModel;

namespace Vodovoz
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DeliveryPointView : QS.Dialog.Gtk.WidgetOnDialogBase
	{
		IUnitOfWorkGeneric<Counterparty> deliveryPointUoW;

		public IUnitOfWorkGeneric<Counterparty> DeliveryPointUoW {
			get { return deliveryPointUoW; }
			set {
				if(deliveryPointUoW == value)
					return;
				deliveryPointUoW = value;
				if(DeliveryPointUoW.Root.DeliveryPoints == null)
					DeliveryPointUoW.Root.DeliveryPoints = new List<DeliveryPoint>();
				treeDeliveryPoints.RepresentationModel = new ViewModel.ClientDeliveryPointsVM(value);
				treeDeliveryPoints.RepresentationModel.UpdateNodes();
			}
		}

		bool CanDelete()
		{
			return QSMain.User.Permissions["can_delete_counterparty_and_deliverypoint"] 
				         && treeDeliveryPoints.Selection.CountSelectedRows() > 0;
		}


		public DeliveryPointView()
		{
			this.Build();

			treeDeliveryPoints.Selection.Changed += OnSelectionChanged;
		}

		void OnSelectionChanged(object sender, EventArgs e)
		{
			bool selected = treeDeliveryPoints.Selection.CountSelectedRows() > 0;
			buttonEdit.Sensitive = selected;
			buttonDelete.Sensitive = CanDelete();
		}

		void OnButtonAddClicked(object sender, EventArgs e)
		{
			if(MyOrmDialog.UoW.IsNew) {
				if(CommonDialogs.SaveBeforeCreateSlaveEntity(MyEntityDialog.EntityObject.GetType(), typeof(DeliveryPoint))) {
					if(!MyTdiDialog.Save())
						return;
				} else
					return;
			}

			ITdiDialog dlg = new DeliveryPointDlg(DeliveryPointUoW.Root);
			MyTab.TabParent.AddSlaveTab(MyTab, dlg);
			treeDeliveryPoints.RepresentationModel.UpdateNodes();
		}

		protected void OnButtonEditClicked(object sender, EventArgs e)
		{
			ITdiDialog dlg = new DeliveryPointDlg((treeDeliveryPoints.GetSelectedObjects()[0] as ClientDeliveryPointVMNode).Id);
			MyTab.TabParent.AddSlaveTab(MyTab, dlg);
		}

		protected void OnTreeDeliveryPointsRowActivated(object o, Gtk.RowActivatedArgs args)
		{
			buttonEdit.Click();
		}

		protected void OnButtonDeleteClicked(object sender, EventArgs e)
		{
			if(OrmMain.DeleteObject(typeof(DeliveryPoint),
				treeDeliveryPoints.GetSelectedId())) {
				treeDeliveryPoints.RepresentationModel.UpdateNodes();
			}
		}
	}
}

