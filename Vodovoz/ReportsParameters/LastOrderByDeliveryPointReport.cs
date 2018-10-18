﻿using System;
using System.Collections.Generic;
using QS.DomainModel.UoW;
using QS.Report;
using QSOrmProject;
using QSReport;

namespace Vodovoz.ReportsParameters
{
	public partial class LastOrderByDeliveryPointReport : Gtk.Bin, IOrmDialog, IParametersWidget
	{
		public LastOrderByDeliveryPointReport()
		{
			this.Build();
			ydatepicker.Date = DateTime.Now.Date;
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

		public event EventHandler<LoadReportEventArgs> LoadReport;

		public string Title {
			get {
				return "Отчет по последнему заказу";
			}
		}

		#endregion

		private ReportInfo GetReportInfo()
		{
			return new ReportInfo {
				Identifier = buttonSanitary.Active?"Orders.SanitaryReport":"Orders.OrdersByDeliveryPoint",
				Parameters = new Dictionary<string, object>
				{
					{ "date", ydatepicker.Date }
				}
			};
		}

		void OnUpdate(bool hide = false)
		{
			if(LoadReport != null) {
				LoadReport(this, new LoadReportEventArgs(GetReportInfo(), hide));
			}
		}

		protected void OnButtonCreateRepotClicked(object sender, EventArgs e)
		{
			OnUpdate(true);
		}
	}
}
