
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Dialogs.OnlineStore
{
	public partial class ExportToSiteDlg
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.ProgressBar progressbarTotal;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Button buttonRunToFile;

		private global::Gtk.ScrolledWindow GtkScrolledWindowErrors;

		private global::Gtk.TextView textviewErrors;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Dialogs.OnlineStore.ExportToSiteDlg
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Dialogs.OnlineStore.ExportToSiteDlg";
			// Container child Vodovoz.Dialogs.OnlineStore.ExportToSiteDlg.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.progressbarTotal = new global::Gtk.ProgressBar();
			this.progressbarTotal.Name = "progressbarTotal";
			this.vbox1.Add(this.progressbarTotal);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.progressbarTotal]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.buttonRunToFile = new global::Gtk.Button();
			this.buttonRunToFile.CanFocus = true;
			this.buttonRunToFile.Name = "buttonRunToFile";
			this.buttonRunToFile.UseUnderline = true;
			this.buttonRunToFile.Label = global::Mono.Unix.Catalog.GetString("Экспортировать в файл");
			this.hbox3.Add(this.buttonRunToFile);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.buttonRunToFile]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			this.vbox1.Add(this.hbox3);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox3]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindowErrors = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindowErrors.Name = "GtkScrolledWindowErrors";
			this.GtkScrolledWindowErrors.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindowErrors.Gtk.Container+ContainerChild
			this.textviewErrors = new global::Gtk.TextView();
			this.textviewErrors.CanFocus = true;
			this.textviewErrors.Name = "textviewErrors";
			this.textviewErrors.Editable = false;
			this.GtkScrolledWindowErrors.Add(this.textviewErrors);
			this.vbox1.Add(this.GtkScrolledWindowErrors);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindowErrors]));
			w5.Position = 2;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.GtkScrolledWindowErrors.Hide();
			this.Hide();
			this.buttonRunToFile.Clicked += new global::System.EventHandler(this.OnButtonRunToFileClicked);
		}
	}
}
