﻿using System;
using QS.DomainModel.UoW;
using QSOrmProject;
using QSTDI;
using Vodovoz.Domain.Logistic;

namespace Vodovoz
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class RouteListKeepingView : TdiTabBase
	{
		private IUnitOfWork uow;

		ViewModel.RouteListsVM viewModel;

		public IUnitOfWork UoW {
			get {
				return uow;
			}
			set {
				if (uow == value)
					return;
				uow = value;
				viewModel = new ViewModel.RouteListsVM (value);
				viewModel.Filter = new RouteListsFilter(uow);
				viewModel.Filter.SetAndRefilterAtOnce(x => x.RestrictStatus = RouteListStatus.EnRoute);
				treeRouteLists.RepresentationModel = viewModel;
				treeRouteLists.RepresentationModel.UpdateNodes ();
			}
		}

		public RouteListKeepingView()
		{
			this.Build();
			this.TabName = "Ведение маршрутных листов";
			UoW = UnitOfWorkFactory.CreateWithoutRoot ();
			buttonOpen.Sensitive = false;
			treeRouteLists.Selection.Changed += OnSelectionChanged;
		}

		void OnSelectionChanged(object sender, EventArgs args)
		{
			buttonOpen.Sensitive = treeRouteLists.Selection.CountSelectedRows() > 0;
		}

		protected void OnButtonOpenClicked (object sender, EventArgs e)
		{
			var node = treeRouteLists.GetSelectedNode () as ViewModel.RouteListsVMNode;
			var dlg = new RouteListKeepingDlg (node.Id);
			TabParent.AddTab (dlg, this);
		}

		protected void OnRouteListActivated (object o, Gtk.RowActivatedArgs args)
		{
			OnButtonOpenClicked(o, args);
		}			

		protected void OnButtonRefreshClicked (object sender, EventArgs e)
		{
			treeRouteLists.RepresentationModel.UpdateNodes ();
		}
	}
}

