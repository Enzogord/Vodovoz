
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Reports
{
	public partial class ShortfallBattlesReport
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.HBox hbox3;
		
		private global::Gtk.Label label2;
		
		private global::Gamma.Widgets.yDatePicker ydatepicker;
		
		private global::Gtk.Button buttonCreateRepot;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Vodovoz.Reports.ShortfallBattlesReport
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Vodovoz.Reports.ShortfallBattlesReport";
			// Container child Vodovoz.Reports.ShortfallBattlesReport.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Дата:");
			this.hbox3.Add (this.label2);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label2]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.ydatepicker = new global::Gamma.Widgets.yDatePicker ();
			this.ydatepicker.Events = ((global::Gdk.EventMask)(256));
			this.ydatepicker.Name = "ydatepicker";
			this.ydatepicker.WithTime = false;
			this.ydatepicker.Date = new global::System.DateTime (0);
			this.ydatepicker.IsEditable = true;
			this.ydatepicker.AutoSeparation = false;
			this.hbox3.Add (this.ydatepicker);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.ydatepicker]));
			w2.Position = 1;
			this.vbox1.Add (this.hbox3);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox3]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.buttonCreateRepot = new global::Gtk.Button ();
			this.buttonCreateRepot.CanFocus = true;
			this.buttonCreateRepot.Name = "buttonCreateRepot";
			this.buttonCreateRepot.UseUnderline = true;
			this.buttonCreateRepot.Label = global::Mono.Unix.Catalog.GetString ("Сформировать отчет");
			this.vbox1.Add (this.buttonCreateRepot);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.buttonCreateRepot]));
			w4.PackType = ((global::Gtk.PackType)(1));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonCreateRepot.Clicked += new global::System.EventHandler (this.OnButtonCreateRepotClicked);
		}
	}
}