﻿using System;
using System.Data.Bindings.Collections.Generic;
using NHibernate;
using QSOrmProject;
using QSTDI;
using System.Collections.Generic;

namespace Vodovoz
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class DeliveryPointView : Gtk.Bin
	{
		private IDeliveryPointOwner deliveryPointOwner;
		private GenericObservableList<DeliveryPoint> DeliveryPoints;
		private ISession session;

		public ISession Session {
			get { return session; }
			set { session = value; }
		}

		public IDeliveryPointOwner DeliveryPointOwner {
			get { return deliveryPointOwner; }
			set {
				deliveryPointOwner = value;
				if (deliveryPointOwner.DeliveryPoints == null)
					DeliveryPointOwner.DeliveryPoints = new List<DeliveryPoint> ();
				DeliveryPoints = new GenericObservableList<DeliveryPoint> (DeliveryPointOwner.DeliveryPoints);
				treeDeliveryPoints.ItemsDataSource = DeliveryPoints;
			}
		}

		OrmParentReference parentReference;

		public OrmParentReference ParentReference {
			set {
				parentReference = value;
				if (parentReference != null) {
					Session = parentReference.Session;
					if (!(parentReference.ParentObject is IDeliveryPointOwner)) {
						throw new ArgumentException (String.Format ("Родительский объект в parentReference должен реализовывать интерфейс {0}", typeof(IDeliveryPointOwner)));
					}
					DeliveryPointOwner = (IDeliveryPointOwner)parentReference.ParentObject;
				}
			}
			get { return parentReference; }
		}

		public DeliveryPointView ()
		{
			this.Build ();
			treeDeliveryPoints.Selection.Changed += OnSelectionChanged;
		}

		void OnSelectionChanged (object sender, EventArgs e)
		{
			bool selected = treeDeliveryPoints.Selection.CountSelectedRows () > 0;
			buttonEdit.Sensitive = buttonDelete.Sensitive = selected;
		}

		void OnButtonAddClicked (object sender, EventArgs e)
		{
			ITdiTab mytab = TdiHelper.FindMyTab (this);
			if (mytab == null)
				return;

			DeliveryPoint point = new DeliveryPoint ();
			point.IsNew = true;
			DeliveryPoints.Add (point);

			ITdiDialog dlg = new DeliveryPointDlg (ParentReference, point);
			mytab.TabParent.AddSlaveTab (mytab, dlg);
		}

		protected void OnButtonEditClicked (object sender, EventArgs e)
		{
			ITdiTab mytab = TdiHelper.FindMyTab (this);
			if (mytab == null)
				return;

			ITdiDialog dlg = OrmMain.CreateObjectDialog (ParentReference, treeDeliveryPoints.GetSelectedObjects () [0]);
			mytab.TabParent.AddSlaveTab (mytab, dlg);
		}

		protected void OnTreeDeliveryPointsRowActivated (object o, Gtk.RowActivatedArgs args)
		{
			buttonEdit.Click ();
		}

		protected void OnButtonDeleteClicked (object sender, EventArgs e)
		{
			ITdiTab mytab = TdiHelper.FindMyTab (this);
			if (mytab == null)
				return;

			DeliveryPoints.Remove (treeDeliveryPoints.GetSelectedObjects () [0] as DeliveryPoint);
		}


	}
}

