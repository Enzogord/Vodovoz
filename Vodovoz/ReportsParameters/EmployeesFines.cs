﻿using System;
using System.Collections.Generic;
using QS.DomainModel.UoW;
using QSOrmProject;
using QSReport;
using Vodovoz.Domain.Employees;
using Vodovoz.ViewModel;

namespace Vodovoz.Reports
{
	public partial class EmployeesFines : Gtk.Bin, IOrmDialog, IParametersWidget
	{
		public EmployeesFines()
		{
			this.Build();
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			var filter = new EmployeeFilter(UoW);
			yentryDriver.RepresentationModel = new EmployeesVM(filter);
		}
		#region IOrmDialog implementation

		public IUnitOfWork UoW { get; private set; }

		public object EntityObject {
			get {
				return null;
			}
		}

		#endregion

		#region IParametersWidget implementation

		public string Title
		{
			get
			{
				return "Штрафы сотрудников";
			}
		}

		public event EventHandler<LoadReportEventArgs> LoadReport;

		#endregion

		void OnUpdate(bool hide = false)
		{
			LoadReport?.Invoke(this, new LoadReportEventArgs(GetReportInfo(), hide));
		}

		protected void OnButtonRunClicked(object sender, EventArgs e)
		{
			OnUpdate(true);
		}

		private ReportInfo GetReportInfo()
		{
			var parameters = new Dictionary<string, object>();

			if(yentryDriver.Subject != null)
				parameters.Add("drivers", (yentryDriver.Subject as Employee).Id);
			else {
				parameters.Add("drivers", -1);
			}
			if(dateperiodpicker1.EndDateOrNull != null && dateperiodpicker1.StartDateOrNull != null) {
				parameters.Add("startDate", dateperiodpicker1.StartDateOrNull.Value);
				parameters.Add("endDate", dateperiodpicker1.EndDateOrNull.Value);
			}else{
				parameters.Add("startDate", 0);
				parameters.Add("endDate", 0);
			}

			parameters.Add("showbottom", false);
			parameters.Add("routelist", 0);
			parameters.Add("category", GetCategory());

			return new ReportInfo
			{
				Identifier = "Employees.Fines",
				Parameters = parameters
			};
		}			

		protected void OnDateperiodpicker1PeriodChanged(object sender, EventArgs e)
		{
			ValidateParameters();
		}

		protected void OnYentryDriverChanged(object sender, EventArgs e)
		{
			ValidateParameters();
		}

		private void ValidateParameters()
		{
			var datePeriodSelected = dateperiodpicker1.EndDateOrNull != null && dateperiodpicker1.StartDateOrNull != null;
			var driverSelected = yentryDriver.Subject != null;
			buttonRun.Sensitive = datePeriodSelected || driverSelected;
		}

		protected void OnRadioCatAllToggled(object sender, EventArgs e)
		{
			var filter = new EmployeeFilter(UoW);
			if(radioCatDriver.Active) {
				filter.SetAndRefilterAtOnce(x => x.RestrictCategory = EmployeeCategory.driver);
			}

			if(radioCatForwarder.Active) {
				filter.SetAndRefilterAtOnce(x => x.RestrictCategory = EmployeeCategory.forwarder);
			}

			if(radioCatOffice.Active) {
				filter.SetAndRefilterAtOnce(x => x.RestrictCategory = EmployeeCategory.office);
			}
			yentryDriver.RepresentationModel = new EmployeesVM(filter);
		}

		protected string GetCategory()
		{
			string cat = "-1";

			if(radioCatDriver.Active)
			{
				cat = EmployeeCategory.driver.ToString();
			} else if(radioCatForwarder.Active)
			{
				cat = EmployeeCategory.forwarder.ToString();
			} else if(radioCatOffice.Active) 
			{
				cat = EmployeeCategory.office.ToString();
			}

			return cat;
		}
	}
}

