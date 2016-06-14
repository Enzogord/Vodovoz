
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class CarLoadDocumentView
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.Label label1;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gamma.GtkWidgets.yTreeView ytreeviewItems;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.Button buttonFillAllItems;
		
		private global::Gtk.Button buttonFillWarehouseItems;
		
		private global::Gtk.Button buttonDelete;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Vodovoz.CarLoadDocumentView
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Vodovoz.CarLoadDocumentView";
			// Container child Vodovoz.CarLoadDocumentView.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.Xalign = 0F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Отгружаемая номенклатура:");
			this.vbox1.Add (this.label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.label1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytreeviewItems = new global::Gamma.GtkWidgets.yTreeView ();
			this.ytreeviewItems.CanFocus = true;
			this.ytreeviewItems.Name = "ytreeviewItems";
			this.GtkScrolledWindow.Add (this.ytreeviewItems);
			this.vbox1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow]));
			w3.Position = 1;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonFillAllItems = new global::Gtk.Button ();
			this.buttonFillAllItems.CanFocus = true;
			this.buttonFillAllItems.Name = "buttonFillAllItems";
			this.buttonFillAllItems.UseUnderline = true;
			this.buttonFillAllItems.Label = global::Mono.Unix.Catalog.GetString ("Заполнить весь МЛ");
			global::Gtk.Image w4 = new global::Gtk.Image ();
			w4.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("Vodovoz.icons.buttons.insert-object.png");
			this.buttonFillAllItems.Image = w4;
			this.hbox1.Add (this.buttonFillAllItems);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonFillAllItems]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonFillWarehouseItems = new global::Gtk.Button ();
			this.buttonFillWarehouseItems.CanFocus = true;
			this.buttonFillWarehouseItems.Name = "buttonFillWarehouseItems";
			this.buttonFillWarehouseItems.UseUnderline = true;
			this.buttonFillWarehouseItems.Label = global::Mono.Unix.Catalog.GetString ("Заполнить для склада");
			global::Gtk.Image w6 = new global::Gtk.Image ();
			w6.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("Vodovoz.icons.buttons.insert-object.png");
			this.buttonFillWarehouseItems.Image = w6;
			this.hbox1.Add (this.buttonFillWarehouseItems);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonFillWarehouseItems]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonDelete = new global::Gtk.Button ();
			this.buttonDelete.CanFocus = true;
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.UseUnderline = true;
			this.buttonDelete.Label = global::Mono.Unix.Catalog.GetString ("Удалить");
			global::Gtk.Image w8 = new global::Gtk.Image ();
			w8.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-delete", global::Gtk.IconSize.Menu);
			this.buttonDelete.Image = w8;
			this.hbox1.Add (this.buttonDelete);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonDelete]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w10.Position = 2;
			w10.Expand = false;
			w10.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonFillAllItems.Clicked += new global::System.EventHandler (this.OnButtonFillAllItemsClicked);
			this.buttonFillWarehouseItems.Clicked += new global::System.EventHandler (this.OnButtonFillWarehouseItemsClicked);
		}
	}
}