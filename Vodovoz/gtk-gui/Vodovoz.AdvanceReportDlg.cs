
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class AdvanceReportDlg
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.HBox hbox2;
		
		private global::Gtk.Button buttonSave;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Table table1;
		
		private global::Gamma.Widgets.ySpecComboBox comboCategory;
		
		private global::Gtk.HBox hbox5;
		
		private global::Gamma.GtkWidgets.ySpinButton yspinMoney;
		
		private global::QSProjectsLib.CurrencyLabel currencylabel1;
		
		private global::Gtk.HBox hboxDebt;
		
		private global::Gtk.Label labelCurrentDebt;
		
		private global::Gtk.Label labelChangeType;
		
		private global::Gtk.Label labelChangeSum;
		
		private global::Gtk.Label label1;
		
		private global::Gtk.Label label2;
		
		private global::Gtk.Label label4;
		
		private global::Gtk.Label label5;
		
		private global::Gtk.Label label7;
		
		private global::Gtk.Label labelDebtTitle;
		
		private global::Gamma.Widgets.yDatePicker ydateDocument;
		
		private global::Gamma.Widgets.yEntryReference yentryCasher;
		
		private global::Gamma.Widgets.yEntryReference yentryEmploeey;
		
		private global::Gtk.CheckButton checkCreateChange;
		
		private global::Gtk.Label labelTableTitle;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		
		private global::QSOrmProject.RepresentationTreeView treeviewDebts;
		
		private global::Gtk.Label label6;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gamma.GtkWidgets.yTextView ytextviewDescription;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Vodovoz.AdvanceReportDlg
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Vodovoz.AdvanceReportDlg";
			// Container child Vodovoz.AdvanceReportDlg.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button ();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString ("Сохранить");
			global::Gtk.Image w1 = new global::Gtk.Image ();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-save", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w1;
			this.hbox2.Add (this.buttonSave);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonSave]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString ("Отменить");
			global::Gtk.Image w3 = new global::Gtk.Image ();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w3;
			this.hbox2.Add (this.buttonCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox1.Add (this.hbox2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox2]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(3)), ((uint)(4)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.comboCategory = new global::Gamma.Widgets.ySpecComboBox ();
			this.comboCategory.Name = "comboCategory";
			this.comboCategory.AddIfNotExist = false;
			this.comboCategory.ShowSpecialStateAll = false;
			this.comboCategory.ShowSpecialStateNot = false;
			this.table1.Add (this.comboCategory);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.comboCategory]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(2));
			w6.LeftAttach = ((uint)(3));
			w6.RightAttach = ((uint)(4));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.yspinMoney = new global::Gamma.GtkWidgets.ySpinButton (0, 1000000, 100);
			this.yspinMoney.CanFocus = true;
			this.yspinMoney.Name = "yspinMoney";
			this.yspinMoney.Adjustment.PageIncrement = 1000;
			this.yspinMoney.ClimbRate = 1;
			this.yspinMoney.Digits = ((uint)(2));
			this.yspinMoney.Numeric = true;
			this.yspinMoney.ValueAsDecimal = 0m;
			this.hbox5.Add (this.yspinMoney);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.yspinMoney]));
			w7.Position = 0;
			// Container child hbox5.Gtk.Box+BoxChild
			this.currencylabel1 = new global::QSProjectsLib.CurrencyLabel ();
			this.currencylabel1.Name = "currencylabel1";
			this.currencylabel1.LabelProp = global::Mono.Unix.Catalog.GetString ("currencylabel1");
			this.hbox5.Add (this.currencylabel1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.currencylabel1]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			this.table1.Add (this.hbox5);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1 [this.hbox5]));
			w9.TopAttach = ((uint)(2));
			w9.BottomAttach = ((uint)(3));
			w9.LeftAttach = ((uint)(3));
			w9.RightAttach = ((uint)(4));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hboxDebt = new global::Gtk.HBox ();
			this.hboxDebt.Name = "hboxDebt";
			this.hboxDebt.Spacing = 6;
			// Container child hboxDebt.Gtk.Box+BoxChild
			this.labelCurrentDebt = new global::Gtk.Label ();
			this.labelCurrentDebt.Name = "labelCurrentDebt";
			this.labelCurrentDebt.LabelProp = global::Mono.Unix.Catalog.GetString ("label2");
			this.hboxDebt.Add (this.labelCurrentDebt);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hboxDebt [this.labelCurrentDebt]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hboxDebt.Gtk.Box+BoxChild
			this.labelChangeType = new global::Gtk.Label ();
			this.labelChangeType.Name = "labelChangeType";
			this.labelChangeType.Xalign = 1F;
			this.labelChangeType.LabelProp = global::Mono.Unix.Catalog.GetString ("label4");
			this.hboxDebt.Add (this.labelChangeType);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hboxDebt [this.labelChangeType]));
			w11.Position = 1;
			// Container child hboxDebt.Gtk.Box+BoxChild
			this.labelChangeSum = new global::Gtk.Label ();
			this.labelChangeSum.Name = "labelChangeSum";
			this.labelChangeSum.LabelProp = global::Mono.Unix.Catalog.GetString ("label5");
			this.labelChangeSum.UseMarkup = true;
			this.hboxDebt.Add (this.labelChangeSum);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hboxDebt [this.labelChangeSum]));
			w12.Position = 2;
			w12.Expand = false;
			w12.Fill = false;
			this.table1.Add (this.hboxDebt);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1 [this.hboxDebt]));
			w13.TopAttach = ((uint)(2));
			w13.BottomAttach = ((uint)(3));
			w13.LeftAttach = ((uint)(1));
			w13.RightAttach = ((uint)(2));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Дата:");
			this.table1.Add (this.label1);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1 [this.label1]));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.Xalign = 1F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Кассир:");
			this.table1.Add (this.label2);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1 [this.label2]));
			w15.LeftAttach = ((uint)(2));
			w15.RightAttach = ((uint)(3));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.Xalign = 1F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Статья дохода:");
			this.table1.Add (this.label4);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table1 [this.label4]));
			w16.TopAttach = ((uint)(1));
			w16.BottomAttach = ((uint)(2));
			w16.LeftAttach = ((uint)(2));
			w16.RightAttach = ((uint)(3));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.Xalign = 1F;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Сумма:");
			this.table1.Add (this.label5);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1 [this.label5]));
			w17.TopAttach = ((uint)(2));
			w17.BottomAttach = ((uint)(3));
			w17.LeftAttach = ((uint)(2));
			w17.RightAttach = ((uint)(3));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.Xalign = 1F;
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Подотчетное лицо:");
			this.table1.Add (this.label7);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table1 [this.label7]));
			w18.TopAttach = ((uint)(1));
			w18.BottomAttach = ((uint)(2));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelDebtTitle = new global::Gtk.Label ();
			this.labelDebtTitle.Name = "labelDebtTitle";
			this.labelDebtTitle.Xalign = 1F;
			this.labelDebtTitle.LabelProp = global::Mono.Unix.Catalog.GetString ("Текущий долг:");
			this.table1.Add (this.labelDebtTitle);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelDebtTitle]));
			w19.TopAttach = ((uint)(2));
			w19.BottomAttach = ((uint)(3));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ydateDocument = new global::Gamma.Widgets.yDatePicker ();
			this.ydateDocument.Events = ((global::Gdk.EventMask)(256));
			this.ydateDocument.Name = "ydateDocument";
			this.ydateDocument.Date = new global::System.DateTime (0);
			this.ydateDocument.IsEditable = false;
			this.ydateDocument.AutoSeparation = true;
			this.table1.Add (this.ydateDocument);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table1 [this.ydateDocument]));
			w20.LeftAttach = ((uint)(1));
			w20.RightAttach = ((uint)(2));
			w20.XOptions = ((global::Gtk.AttachOptions)(4));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yentryCasher = new global::Gamma.Widgets.yEntryReference ();
			this.yentryCasher.Sensitive = false;
			this.yentryCasher.Events = ((global::Gdk.EventMask)(256));
			this.yentryCasher.Name = "yentryCasher";
			this.table1.Add (this.yentryCasher);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table1 [this.yentryCasher]));
			w21.LeftAttach = ((uint)(3));
			w21.RightAttach = ((uint)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yentryEmploeey = new global::Gamma.Widgets.yEntryReference ();
			this.yentryEmploeey.Events = ((global::Gdk.EventMask)(256));
			this.yentryEmploeey.Name = "yentryEmploeey";
			this.table1.Add (this.yentryEmploeey);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table1 [this.yentryEmploeey]));
			w22.TopAttach = ((uint)(1));
			w22.BottomAttach = ((uint)(2));
			w22.LeftAttach = ((uint)(1));
			w22.RightAttach = ((uint)(2));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox1.Add (this.table1);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.table1]));
			w23.Position = 1;
			w23.Expand = false;
			w23.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.checkCreateChange = new global::Gtk.CheckButton ();
			this.checkCreateChange.CanFocus = true;
			this.checkCreateChange.Name = "checkCreateChange";
			this.checkCreateChange.Label = global::Mono.Unix.Catalog.GetString ("checkbutton1");
			this.checkCreateChange.DrawIndicator = true;
			this.checkCreateChange.UseUnderline = true;
			this.vbox1.Add (this.checkCreateChange);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.checkCreateChange]));
			w24.Position = 2;
			w24.Expand = false;
			w24.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.labelTableTitle = new global::Gtk.Label ();
			this.labelTableTitle.Name = "labelTableTitle";
			this.labelTableTitle.Xalign = 0F;
			this.labelTableTitle.LabelProp = global::Mono.Unix.Catalog.GetString ("Последние выданные авансы:");
			this.vbox1.Add (this.labelTableTitle);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.labelTableTitle]));
			w25.Position = 3;
			w25.Expand = false;
			w25.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.treeviewDebts = new global::QSOrmProject.RepresentationTreeView ();
			this.treeviewDebts.CanFocus = true;
			this.treeviewDebts.Name = "treeviewDebts";
			this.GtkScrolledWindow1.Add (this.treeviewDebts);
			this.vbox1.Add (this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow1]));
			w27.Position = 4;
			// Container child vbox1.Gtk.Box+BoxChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.Xalign = 0F;
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Основание:");
			this.vbox1.Add (this.label6);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.label6]));
			w28.Position = 5;
			w28.Expand = false;
			w28.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytextviewDescription = new global::Gamma.GtkWidgets.yTextView ();
			this.ytextviewDescription.CanFocus = true;
			this.ytextviewDescription.Name = "ytextviewDescription";
			this.GtkScrolledWindow.Add (this.ytextviewDescription);
			this.vbox1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow]));
			w30.Position = 6;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonSave.Clicked += new global::System.EventHandler (this.OnButtonSaveClicked);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
			this.yentryEmploeey.Changed += new global::System.EventHandler (this.OnYentryEmploeeyChanged);
			this.yspinMoney.ValueChanged += new global::System.EventHandler (this.OnYspinMoneyValueChanged);
		}
	}
}
