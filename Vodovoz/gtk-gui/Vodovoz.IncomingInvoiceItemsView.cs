
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class IncomingInvoiceItemsView
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.Label label2;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTreeView treeItemsList;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Button buttonAdd;

		private global::Gtk.Button buttonCreate;

		private global::Gtk.Button buttonDelete;

		private global::Gtk.Label labelSum;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.IncomingInvoiceItemsView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.IncomingInvoiceItemsView";
			// Container child Vodovoz.IncomingInvoiceItemsView.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.Xalign = 0F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Список номенклатур");
			this.vbox1.Add(this.label2);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.label2]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeItemsList = new global::Gamma.GtkWidgets.yTreeView();
			this.treeItemsList.CanFocus = true;
			this.treeItemsList.Name = "treeItemsList";
			this.GtkScrolledWindow.Add(this.treeItemsList);
			this.vbox1.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
			w3.Position = 1;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonAdd = new global::Gtk.Button();
			this.buttonAdd.TooltipMarkup = "Добавить номенклатуру в накладную.";
			this.buttonAdd.CanFocus = true;
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.UseUnderline = true;
			this.buttonAdd.Label = global::Mono.Unix.Catalog.GetString("Добавить");
			global::Gtk.Image w4 = new global::Gtk.Image();
			w4.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-add", global::Gtk.IconSize.Menu);
			this.buttonAdd.Image = w4;
			this.hbox1.Add(this.buttonAdd);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonAdd]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonCreate = new global::Gtk.Button();
			this.buttonCreate.TooltipMarkup = "Создать серийные номера для оборудования и добавить в накладную.";
			this.buttonCreate.Sensitive = false;
			this.buttonCreate.CanFocus = true;
			this.buttonCreate.Name = "buttonCreate";
			this.buttonCreate.UseUnderline = true;
			this.buttonCreate.Label = global::Mono.Unix.Catalog.GetString("Новое оборудование");
			global::Gtk.Image w6 = new global::Gtk.Image();
			w6.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-add", global::Gtk.IconSize.Menu);
			this.buttonCreate.Image = w6;
			this.hbox1.Add(this.buttonCreate);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonCreate]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonDelete = new global::Gtk.Button();
			this.buttonDelete.CanFocus = true;
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.UseUnderline = true;
			this.buttonDelete.Label = global::Mono.Unix.Catalog.GetString("Удалить");
			global::Gtk.Image w8 = new global::Gtk.Image();
			w8.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-delete", global::Gtk.IconSize.Menu);
			this.buttonDelete.Image = w8;
			this.hbox1.Add(this.buttonDelete);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonDelete]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.labelSum = new global::Gtk.Label();
			this.labelSum.Name = "labelSum";
			this.labelSum.Xalign = 1F;
			this.hbox1.Add(this.labelSum);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.labelSum]));
			w10.PackType = ((global::Gtk.PackType)(1));
			w10.Position = 3;
			w10.Expand = false;
			w10.Fill = false;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w11.Position = 2;
			w11.Expand = false;
			w11.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Show();
			this.buttonAdd.Clicked += new global::System.EventHandler(this.OnButtonAddClicked);
			this.buttonCreate.Clicked += new global::System.EventHandler(this.OnButtonCreateClicked);
			this.buttonDelete.Clicked += new global::System.EventHandler(this.OnButtonDeleteClicked);
		}
	}
}
