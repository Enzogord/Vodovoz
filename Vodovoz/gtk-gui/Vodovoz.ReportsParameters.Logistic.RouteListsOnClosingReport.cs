
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ReportsParameters.Logistic
{
	public partial class RouteListsOnClosingReport
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.VBox vbox2;

		private global::Gamma.GtkWidgets.yCheckButton chkRemTodayRLs;

		private global::Gamma.GtkWidgets.yCheckButton chkRemTruckRLs;

		private global::Gamma.GtkWidgets.yCheckButton chkRemServiceRLs;

		private global::Gamma.GtkWidgets.yCheckButton chkRemMercRLs;

		private global::Gtk.Button buttonCreateReport;

		private global::Gtk.HSeparator hseparator1;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ReportsParameters.Logistic.RouteListsOnClosingReport
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ReportsParameters.Logistic.RouteListsOnClosingReport";
			// Container child Vodovoz.ReportsParameters.Logistic.RouteListsOnClosingReport.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.chkRemTodayRLs = new global::Gamma.GtkWidgets.yCheckButton();
			this.chkRemTodayRLs.CanFocus = true;
			this.chkRemTodayRLs.Name = "chkRemTodayRLs";
			this.chkRemTodayRLs.Label = global::Mono.Unix.Catalog.GetString("cегодняшние МЛ");
			this.chkRemTodayRLs.DrawIndicator = false;
			this.chkRemTodayRLs.UseUnderline = true;
			this.vbox2.Add(this.chkRemTodayRLs);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.chkRemTodayRLs]));
			w1.Position = 0;
			w1.Expand = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.chkRemTruckRLs = new global::Gamma.GtkWidgets.yCheckButton();
			this.chkRemTruckRLs.CanFocus = true;
			this.chkRemTruckRLs.Name = "chkRemTruckRLs";
			this.chkRemTruckRLs.Label = global::Mono.Unix.Catalog.GetString("МЛ фур");
			this.chkRemTruckRLs.DrawIndicator = false;
			this.chkRemTruckRLs.UseUnderline = true;
			this.vbox2.Add(this.chkRemTruckRLs);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.chkRemTruckRLs]));
			w2.Position = 1;
			w2.Expand = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.chkRemServiceRLs = new global::Gamma.GtkWidgets.yCheckButton();
			this.chkRemServiceRLs.CanFocus = true;
			this.chkRemServiceRLs.Name = "chkRemServiceRLs";
			this.chkRemServiceRLs.Label = global::Mono.Unix.Catalog.GetString("МЛ мастеров");
			this.chkRemServiceRLs.DrawIndicator = false;
			this.chkRemServiceRLs.UseUnderline = true;
			this.vbox2.Add(this.chkRemServiceRLs);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.chkRemServiceRLs]));
			w3.Position = 2;
			w3.Expand = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.chkRemMercRLs = new global::Gamma.GtkWidgets.yCheckButton();
			this.chkRemMercRLs.CanFocus = true;
			this.chkRemMercRLs.Name = "chkRemMercRLs";
			this.chkRemMercRLs.Label = global::Mono.Unix.Catalog.GetString("МЛ наёмников");
			this.chkRemMercRLs.DrawIndicator = false;
			this.chkRemMercRLs.UseUnderline = true;
			this.vbox2.Add(this.chkRemMercRLs);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.chkRemMercRLs]));
			w4.Position = 3;
			w4.Expand = false;
			this.vbox1.Add(this.vbox2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.vbox2]));
			w5.Position = 0;
			// Container child vbox1.Gtk.Box+BoxChild
			this.buttonCreateReport = new global::Gtk.Button();
			this.buttonCreateReport.CanFocus = true;
			this.buttonCreateReport.Name = "buttonCreateReport";
			this.buttonCreateReport.UseUnderline = true;
			this.buttonCreateReport.Label = global::Mono.Unix.Catalog.GetString("Сформировать отчет");
			this.vbox1.Add(this.buttonCreateReport);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.buttonCreateReport]));
			w6.PackType = ((global::Gtk.PackType)(1));
			w6.Position = 1;
			w6.Expand = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator();
			this.hseparator1.Name = "hseparator1";
			this.vbox1.Add(this.hseparator1);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hseparator1]));
			w7.PackType = ((global::Gtk.PackType)(1));
			w7.Position = 2;
			w7.Expand = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.buttonCreateReport.Clicked += new global::System.EventHandler(this.OnButtonCreateReportClicked);
		}
	}
}
