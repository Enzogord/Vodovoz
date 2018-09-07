
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Dialogs.OnlineStore
{
	public partial class ExportToSiteDlg
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.Table table1;

		private global::Gtk.Entry entryPassword;

		private global::Gtk.Entry entrySitePath;

		private global::Gtk.Entry entryUser;

		private global::Gtk.Label label1;

		private global::Gtk.Label label2;

		private global::Gtk.Label label3;

		private global::Gtk.Label label4;

		private global::Gtk.ProgressBar progressbarTotal;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Button buttonRunToFile;

		private global::Gtk.Button buttonExportToSite;

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
			this.table1 = new global::Gtk.Table(((uint)(3)), ((uint)(4)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.entryPassword = new global::Gtk.Entry();
			this.entryPassword.CanFocus = true;
			this.entryPassword.Name = "entryPassword";
			this.entryPassword.IsEditable = true;
			this.entryPassword.InvisibleChar = '●';
			this.table1.Add(this.entryPassword);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.entryPassword]));
			w1.TopAttach = ((uint)(2));
			w1.BottomAttach = ((uint)(3));
			w1.LeftAttach = ((uint)(3));
			w1.RightAttach = ((uint)(4));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entrySitePath = new global::Gtk.Entry();
			this.entrySitePath.CanFocus = true;
			this.entrySitePath.Name = "entrySitePath";
			this.entrySitePath.IsEditable = true;
			this.entrySitePath.InvisibleChar = '●';
			this.table1.Add(this.entrySitePath);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.entrySitePath]));
			w2.TopAttach = ((uint)(1));
			w2.BottomAttach = ((uint)(2));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryUser = new global::Gtk.Entry();
			this.entryUser.CanFocus = true;
			this.entryUser.Name = "entryUser";
			this.entryUser.IsEditable = true;
			this.entryUser.InvisibleChar = '●';
			this.table1.Add(this.entryUser);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.entryUser]));
			w3.TopAttach = ((uint)(2));
			w3.BottomAttach = ((uint)(3));
			w3.LeftAttach = ((uint)(1));
			w3.RightAttach = ((uint)(2));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Адрес сайта:");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w4.TopAttach = ((uint)(1));
			w4.BottomAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Внимание</b>. Настройки указанный здесь так же используютмя при автоматической" +
					" выгруке сервером.");
			this.label2.UseMarkup = true;
			this.table1.Add(this.label2);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.label2]));
			w5.RightAttach = ((uint)(4));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Пользователь:");
			this.table1.Add(this.label3);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.label3]));
			w6.TopAttach = ((uint)(2));
			w6.BottomAttach = ((uint)(3));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.Xalign = 1F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Пароль:");
			this.table1.Add(this.label4);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.label4]));
			w7.TopAttach = ((uint)(2));
			w7.BottomAttach = ((uint)(3));
			w7.LeftAttach = ((uint)(2));
			w7.RightAttach = ((uint)(3));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox1.Add(this.table1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.table1]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.progressbarTotal = new global::Gtk.ProgressBar();
			this.progressbarTotal.Name = "progressbarTotal";
			this.vbox1.Add(this.progressbarTotal);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.progressbarTotal]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
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
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.buttonRunToFile]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.buttonExportToSite = new global::Gtk.Button();
			this.buttonExportToSite.CanFocus = true;
			this.buttonExportToSite.Name = "buttonExportToSite";
			this.buttonExportToSite.UseUnderline = true;
			this.buttonExportToSite.Label = global::Mono.Unix.Catalog.GetString("Экспортировать на сайт");
			this.hbox3.Add(this.buttonExportToSite);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.buttonExportToSite]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			this.vbox1.Add(this.hbox3);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox3]));
			w12.Position = 2;
			w12.Expand = false;
			w12.Fill = false;
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
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindowErrors]));
			w14.Position = 3;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.GtkScrolledWindowErrors.Hide();
			this.Hide();
			this.entryUser.FocusOutEvent += new global::Gtk.FocusOutEventHandler(this.OnEntryUserFocusOutEvent);
			this.entrySitePath.FocusOutEvent += new global::Gtk.FocusOutEventHandler(this.OnEntrySitePathFocusOutEvent);
			this.entryPassword.FocusOutEvent += new global::Gtk.FocusOutEventHandler(this.OnEntryPasswordFocusOutEvent);
			this.buttonRunToFile.Clicked += new global::System.EventHandler(this.OnButtonRunToFileClicked);
			this.buttonExportToSite.Clicked += new global::System.EventHandler(this.OnButtonExportToSiteClicked);
		}
	}
}