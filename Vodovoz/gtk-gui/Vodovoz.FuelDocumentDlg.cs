
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class FuelDocumentDlg
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox8;

		private global::Gtk.Button buttonSave;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Table table3;

		private global::Gtk.HBox hbox2;

		private global::Gamma.GtkWidgets.ySpinButton yspinFuelTicketLiters;

		private global::Gtk.Button buttonFuelBy10;

		private global::Gtk.Button buttonFuelBy20;

		private global::Gtk.HBox hbox3;

		private global::Gamma.GtkWidgets.ySpinButton spinFuelPrice;

		private global::Gtk.HBox hbox4;

		private global::QSOrmProject.DisableSpinButton disablespinMoney;

		private global::Gtk.Button buttonSetRemain;

		private global::Gtk.HBox hbox5;

		private global::Gtk.HBox hbox7;

		private global::Gtk.Label labelExpenseInfo;

		private global::Gtk.Button buttonOpenExpense;

		private global::Gtk.Label label1;

		private global::Gtk.Label label2;

		private global::Gtk.Label label3;

		private global::Gtk.Label label4;

		private global::Gtk.Label label5;

		private global::Gtk.Label label6;

		private global::Gtk.Label label7;

		private global::Gtk.Label labelResultInfo;

		private global::Gamma.Widgets.yDatePicker ydatepicker;

		private global::Gamma.Widgets.yEntryReference yentryCar;

		private global::Gamma.Widgets.yEntryReferenceVM yentrydriver;

		private global::Gamma.Widgets.yEntryReference yentryfuel;

		private global::Gamma.GtkWidgets.yTextView ytextviewFuelInfo;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.FuelDocumentDlg
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.FuelDocumentDlg";
			// Container child Vodovoz.FuelDocumentDlg.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox8 = new global::Gtk.HBox();
			this.hbox8.Name = "hbox8";
			this.hbox8.Spacing = 6;
			// Container child hbox8.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString("Сохранить");
			global::Gtk.Image w1 = new global::Gtk.Image();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-save", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w1;
			this.hbox8.Add(this.buttonSave);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.buttonSave]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox8.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString("Отменить");
			global::Gtk.Image w3 = new global::Gtk.Image();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w3;
			this.hbox8.Add(this.buttonCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.buttonCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox1.Add(this.hbox8);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox8]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.table3 = new global::Gtk.Table(((uint)(8)), ((uint)(3)), false);
			this.table3.Name = "table3";
			this.table3.RowSpacing = ((uint)(6));
			this.table3.ColumnSpacing = ((uint)(6));
			// Container child table3.Gtk.Table+TableChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.yspinFuelTicketLiters = new global::Gamma.GtkWidgets.ySpinButton(0D, 1000D, 10D);
			this.yspinFuelTicketLiters.WidthRequest = 90;
			this.yspinFuelTicketLiters.CanFocus = true;
			this.yspinFuelTicketLiters.Name = "yspinFuelTicketLiters";
			this.yspinFuelTicketLiters.Adjustment.PageIncrement = 20D;
			this.yspinFuelTicketLiters.ClimbRate = 1D;
			this.yspinFuelTicketLiters.Numeric = true;
			this.yspinFuelTicketLiters.ValueAsDecimal = 0m;
			this.yspinFuelTicketLiters.ValueAsInt = 0;
			this.hbox2.Add(this.yspinFuelTicketLiters);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.yspinFuelTicketLiters]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonFuelBy10 = new global::Gtk.Button();
			this.buttonFuelBy10.CanFocus = true;
			this.buttonFuelBy10.Name = "buttonFuelBy10";
			this.buttonFuelBy10.UseUnderline = true;
			this.buttonFuelBy10.Label = global::Mono.Unix.Catalog.GetString("По 10");
			this.hbox2.Add(this.buttonFuelBy10);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.buttonFuelBy10]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonFuelBy20 = new global::Gtk.Button();
			this.buttonFuelBy20.CanFocus = true;
			this.buttonFuelBy20.Name = "buttonFuelBy20";
			this.buttonFuelBy20.UseUnderline = true;
			this.buttonFuelBy20.Label = global::Mono.Unix.Catalog.GetString("По 20");
			this.hbox2.Add(this.buttonFuelBy20);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.buttonFuelBy20]));
			w8.Position = 2;
			w8.Expand = false;
			w8.Fill = false;
			this.table3.Add(this.hbox2);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table3[this.hbox2]));
			w9.TopAttach = ((uint)(4));
			w9.BottomAttach = ((uint)(5));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.spinFuelPrice = new global::Gamma.GtkWidgets.ySpinButton(0D, 200D, 0.01D);
			this.spinFuelPrice.WidthRequest = 100;
			this.spinFuelPrice.Sensitive = false;
			this.spinFuelPrice.CanFocus = true;
			this.spinFuelPrice.Name = "spinFuelPrice";
			this.spinFuelPrice.Adjustment.PageIncrement = 0.01D;
			this.spinFuelPrice.ClimbRate = 1D;
			this.spinFuelPrice.Digits = ((uint)(2));
			this.spinFuelPrice.Numeric = true;
			this.spinFuelPrice.ValueAsDecimal = 0m;
			this.spinFuelPrice.ValueAsInt = 0;
			this.hbox3.Add(this.spinFuelPrice);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.spinFuelPrice]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			this.table3.Add(this.hbox3);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table3[this.hbox3]));
			w11.TopAttach = ((uint)(5));
			w11.BottomAttach = ((uint)(6));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.disablespinMoney = new global::QSOrmProject.DisableSpinButton();
			this.disablespinMoney.Events = ((global::Gdk.EventMask)(256));
			this.disablespinMoney.Name = "disablespinMoney";
			this.disablespinMoney.Active = false;
			this.disablespinMoney.Upper = 100000D;
			this.disablespinMoney.Lower = 0D;
			this.disablespinMoney.Digits = ((uint)(2));
			this.hbox4.Add(this.disablespinMoney);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.disablespinMoney]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.buttonSetRemain = new global::Gtk.Button();
			this.buttonSetRemain.CanFocus = true;
			this.buttonSetRemain.Name = "buttonSetRemain";
			this.buttonSetRemain.UseUnderline = true;
			this.buttonSetRemain.Label = global::Mono.Unix.Catalog.GetString("Весь остаток");
			this.hbox4.Add(this.buttonSetRemain);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.buttonSetRemain]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			this.table3.Add(this.hbox4);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table3[this.hbox4]));
			w14.TopAttach = ((uint)(6));
			w14.BottomAttach = ((uint)(7));
			w14.LeftAttach = ((uint)(1));
			w14.RightAttach = ((uint)(2));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			this.table3.Add(this.hbox5);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table3[this.hbox5]));
			w15.TopAttach = ((uint)(5));
			w15.BottomAttach = ((uint)(6));
			w15.LeftAttach = ((uint)(2));
			w15.RightAttach = ((uint)(3));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.hbox7 = new global::Gtk.HBox();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.labelExpenseInfo = new global::Gtk.Label();
			this.labelExpenseInfo.Name = "labelExpenseInfo";
			this.labelExpenseInfo.LabelProp = global::Mono.Unix.Catalog.GetString("Расходный ордер");
			this.hbox7.Add(this.labelExpenseInfo);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.labelExpenseInfo]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.buttonOpenExpense = new global::Gtk.Button();
			this.buttonOpenExpense.CanFocus = true;
			this.buttonOpenExpense.Name = "buttonOpenExpense";
			this.buttonOpenExpense.UseUnderline = true;
			this.buttonOpenExpense.Label = global::Mono.Unix.Catalog.GetString("Открыть расходный ордер");
			this.hbox7.Add(this.buttonOpenExpense);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.buttonOpenExpense]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			this.table3.Add(this.hbox7);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table3[this.hbox7]));
			w18.TopAttach = ((uint)(7));
			w18.BottomAttach = ((uint)(8));
			w18.LeftAttach = ((uint)(1));
			w18.RightAttach = ((uint)(2));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Дата:");
			this.table3.Add(this.label1);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table3[this.label1]));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.Xalign = 1F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Водитель:");
			this.table3.Add(this.label2);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table3[this.label2]));
			w20.TopAttach = ((uint)(1));
			w20.BottomAttach = ((uint)(2));
			w20.XOptions = ((global::Gtk.AttachOptions)(4));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Вид топлива:");
			this.table3.Add(this.label3);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table3[this.label3]));
			w21.TopAttach = ((uint)(3));
			w21.BottomAttach = ((uint)(4));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.Xalign = 1F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Автомобиль:");
			this.table3.Add(this.label4);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table3[this.label4]));
			w22.TopAttach = ((uint)(2));
			w22.BottomAttach = ((uint)(3));
			w22.XOptions = ((global::Gtk.AttachOptions)(4));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.Xalign = 1F;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Выдано деньгами:");
			this.table3.Add(this.label5);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table3[this.label5]));
			w23.TopAttach = ((uint)(6));
			w23.BottomAttach = ((uint)(7));
			w23.XOptions = ((global::Gtk.AttachOptions)(4));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.label6 = new global::Gtk.Label();
			this.label6.Name = "label6";
			this.label6.Xalign = 1F;
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString("Выдано талонами(л):");
			this.table3.Add(this.label6);
			global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.table3[this.label6]));
			w24.TopAttach = ((uint)(4));
			w24.BottomAttach = ((uint)(5));
			w24.XOptions = ((global::Gtk.AttachOptions)(4));
			w24.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label();
			this.label7.Name = "label7";
			this.label7.Xalign = 1F;
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString("Стоимость, р/литр:");
			this.table3.Add(this.label7);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table3[this.label7]));
			w25.TopAttach = ((uint)(5));
			w25.BottomAttach = ((uint)(6));
			w25.XOptions = ((global::Gtk.AttachOptions)(4));
			w25.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.labelResultInfo = new global::Gtk.Label();
			this.labelResultInfo.Name = "labelResultInfo";
			this.labelResultInfo.LabelProp = global::Mono.Unix.Catalog.GetString("Итог");
			this.table3.Add(this.labelResultInfo);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table3[this.labelResultInfo]));
			w26.TopAttach = ((uint)(6));
			w26.BottomAttach = ((uint)(7));
			w26.LeftAttach = ((uint)(2));
			w26.RightAttach = ((uint)(3));
			w26.XOptions = ((global::Gtk.AttachOptions)(4));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.ydatepicker = new global::Gamma.Widgets.yDatePicker();
			this.ydatepicker.Events = ((global::Gdk.EventMask)(256));
			this.ydatepicker.Name = "ydatepicker";
			this.ydatepicker.WithTime = false;
			this.ydatepicker.Date = new global::System.DateTime(0);
			this.ydatepicker.IsEditable = false;
			this.ydatepicker.AutoSeparation = false;
			this.table3.Add(this.ydatepicker);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.table3[this.ydatepicker]));
			w27.LeftAttach = ((uint)(1));
			w27.RightAttach = ((uint)(2));
			w27.XOptions = ((global::Gtk.AttachOptions)(4));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.yentryCar = new global::Gamma.Widgets.yEntryReference();
			this.yentryCar.Sensitive = false;
			this.yentryCar.Events = ((global::Gdk.EventMask)(256));
			this.yentryCar.Name = "yentryCar";
			this.table3.Add(this.yentryCar);
			global::Gtk.Table.TableChild w28 = ((global::Gtk.Table.TableChild)(this.table3[this.yentryCar]));
			w28.TopAttach = ((uint)(2));
			w28.BottomAttach = ((uint)(3));
			w28.LeftAttach = ((uint)(1));
			w28.RightAttach = ((uint)(2));
			w28.XOptions = ((global::Gtk.AttachOptions)(4));
			w28.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.yentrydriver = new global::Gamma.Widgets.yEntryReferenceVM();
			this.yentrydriver.Sensitive = false;
			this.yentrydriver.Events = ((global::Gdk.EventMask)(256));
			this.yentrydriver.Name = "yentrydriver";
			this.table3.Add(this.yentrydriver);
			global::Gtk.Table.TableChild w29 = ((global::Gtk.Table.TableChild)(this.table3[this.yentrydriver]));
			w29.TopAttach = ((uint)(1));
			w29.BottomAttach = ((uint)(2));
			w29.LeftAttach = ((uint)(1));
			w29.RightAttach = ((uint)(2));
			w29.XOptions = ((global::Gtk.AttachOptions)(4));
			w29.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.yentryfuel = new global::Gamma.Widgets.yEntryReference();
			this.yentryfuel.Sensitive = false;
			this.yentryfuel.Events = ((global::Gdk.EventMask)(256));
			this.yentryfuel.Name = "yentryfuel";
			this.table3.Add(this.yentryfuel);
			global::Gtk.Table.TableChild w30 = ((global::Gtk.Table.TableChild)(this.table3[this.yentryfuel]));
			w30.TopAttach = ((uint)(3));
			w30.BottomAttach = ((uint)(4));
			w30.LeftAttach = ((uint)(1));
			w30.RightAttach = ((uint)(2));
			w30.XOptions = ((global::Gtk.AttachOptions)(4));
			w30.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.ytextviewFuelInfo = new global::Gamma.GtkWidgets.yTextView();
			this.ytextviewFuelInfo.CanFocus = true;
			this.ytextviewFuelInfo.Name = "ytextviewFuelInfo";
			this.ytextviewFuelInfo.Editable = false;
			this.table3.Add(this.ytextviewFuelInfo);
			global::Gtk.Table.TableChild w31 = ((global::Gtk.Table.TableChild)(this.table3[this.ytextviewFuelInfo]));
			w31.BottomAttach = ((uint)(4));
			w31.LeftAttach = ((uint)(2));
			w31.RightAttach = ((uint)(3));
			w31.XOptions = ((global::Gtk.AttachOptions)(4));
			w31.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox1.Add(this.table3);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.table3]));
			w32.Position = 1;
			w32.Expand = false;
			w32.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.buttonSave.Clicked += new global::System.EventHandler(this.OnButtonSaveClicked);
			this.buttonCancel.Clicked += new global::System.EventHandler(this.OnButtonCancelClicked);
			this.buttonOpenExpense.Clicked += new global::System.EventHandler(this.OnButtonOpenExpenseClicked);
			this.disablespinMoney.ValueChanged += new global::System.EventHandler(this.OnDisablespinMoneyValueChanged);
			this.buttonSetRemain.Clicked += new global::System.EventHandler(this.OnButtonSetRemainClicked);
			this.buttonFuelBy10.Clicked += new global::System.EventHandler(this.OnButtonFuelBy10Clicked);
			this.buttonFuelBy20.Clicked += new global::System.EventHandler(this.OnButtonFuelBy20Clicked);
		}
	}
}
