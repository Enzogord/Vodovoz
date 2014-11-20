﻿using System;
using System.Data.Bindings;
using QSOrmProject;
using QSTDI;
using NHibernate;
using NLog;
using System.Collections.Generic;

namespace Vodovoz
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class FreeRentPackageDlg : Gtk.Bin, QSTDI.ITdiDialog, IOrmDialog
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private ISession session;
		private Adaptor adaptor = new Adaptor();
		private FreeRentPackage subject;
		private bool NewItem = false;

		public ITdiTabParent TabParent { set; get;}

		public event EventHandler<TdiTabNameChangedEventArgs> TabNameChanged;
		public event EventHandler<TdiTabCloseEventArgs> CloseTab;
		public bool HasChanges { 
			get{return NewItem || Session.IsDirty();}
		}

		private string _tabName = "Новый пакет бесплатных услуг";
		public string TabName
		{
			get{return _tabName;}
			set{
				if (_tabName == value)
					return;
				_tabName = value;
				if (TabNameChanged != null)
					TabNameChanged(this, new TdiTabNameChangedEventArgs(value));
			}

		}

		public ISession Session {
			get {
				if (session == null)
					Session = OrmMain.Sessions.OpenSession();
				return session;
			}
			set {
				session = value;
			}
		}

		public object Subject
		{
			get {return subject;}
			set {
				if (value is FreeRentPackage)
					subject = value as FreeRentPackage;
			}
		}

		public FreeRentPackageDlg()
		{
			this.Build();
			NewItem = true;
			subject = new FreeRentPackage();
			ConfigureDlg();
		}

		public FreeRentPackageDlg(int id)
		{
			this.Build();
			subject = Session.Load<FreeRentPackage>(id);
			TabName = subject.Name;
			ConfigureDlg();
		}

		public FreeRentPackageDlg(FreeRentPackage sub)
		{
			this.Build();
			subject = Session.Load<FreeRentPackage>(sub.Id);
			TabName = subject.Name;
			ConfigureDlg();
		}

		private void ConfigureDlg()
		{
			adaptor.Target = subject;
			datatable1.DataSource = adaptor;
			referenceDepositService.SubjectType = typeof(Nomenclature);
			referenceEquipmentType.SubjectType = typeof(EquipmentType);
		}

		public bool Save()
		{
			logger.Info("Сохраняем пакет бесплатных услуг...");
			Session.SaveOrUpdate(subject);
			Session.Flush();
			OrmMain.NotifyObjectUpdated(subject);
			return true;
		}

		public override void Destroy()
		{
			Session.Close();
			adaptor.Disconnect ();
			base.Destroy();
		}

		protected void OnButtonSaveClicked(object sender, EventArgs e)
		{
			if (!this.HasChanges || Save())
				OnCloseTab(false);
		}

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			OnCloseTab(false);
		}

		protected void OnCloseTab(bool askSave)
		{
			if (CloseTab != null)
				CloseTab(this, new TdiTabCloseEventArgs(askSave));
		}
	}
}

