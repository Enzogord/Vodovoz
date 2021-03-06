
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class ResidueDlg
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Button buttonSave;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Table table1;

		private global::QSOrmProject.DisableSpinButton disablespinBottlesDeposit;

		private global::QSOrmProject.DisableSpinButton disablespinBottlesResidue;

		private global::QSOrmProject.DisableSpinButton disablespinEquipmentDeposit;

		private global::QSOrmProject.DisableSpinButton disablespinMoneyDebt;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTextView ytextviewComment;

		private global::Gtk.Label label1;

		private global::Gtk.Label label11;

		private global::Gtk.Label label13;

		private global::Gtk.Label label2;

		private global::Gtk.Label label3;

		private global::Gtk.Label label4;

		private global::Gtk.Label label5;

		private global::Gtk.Label label6;

		private global::Gtk.Label label7;

		private global::Gtk.Label label8;

		private global::Gtk.Label labelCurrent;

		private global::Gtk.Label labelCurrentBootle;

		private global::Gtk.Label labelCurrentBottleDeposit;

		private global::Gtk.Label labelCurrentEquipmentDeposit;

		private global::Gtk.Label labelCurrentMoneyDebt;

		private global::Gtk.Label moneyLabel;

		private global::Gamma.Widgets.yEnumComboBox yenumcomboDebtPaymentType;

		private global::Gamma.Widgets.yDatePicker ypickerDocDate;

		private global::Gamma.Widgets.yEntryReferenceVM yreferenceClientSelector;

		private global::Gamma.Widgets.yEntryReferenceVM yreferenceDeliveryPoint;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ResidueDlg
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ResidueDlg";
			// Container child Vodovoz.ResidueDlg.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString("Сохранить");
			global::Gtk.Image w1 = new global::Gtk.Image();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-save", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w1;
			this.hbox4.Add(this.buttonSave);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.buttonSave]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString("Отменить");
			global::Gtk.Image w3 = new global::Gtk.Image();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w3;
			this.hbox4.Add(this.buttonCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.buttonCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox2.Add(this.hbox4);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox4]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(8)), ((uint)(4)), false);
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.disablespinBottlesDeposit = new global::QSOrmProject.DisableSpinButton();
			this.disablespinBottlesDeposit.Events = ((global::Gdk.EventMask)(256));
			this.disablespinBottlesDeposit.Name = "disablespinBottlesDeposit";
			this.disablespinBottlesDeposit.Active = false;
			this.disablespinBottlesDeposit.Upper = 10000000D;
			this.disablespinBottlesDeposit.Lower = -10000000D;
			this.disablespinBottlesDeposit.Digits = ((uint)(2));
			this.table1.Add(this.disablespinBottlesDeposit);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.disablespinBottlesDeposit]));
			w6.TopAttach = ((uint)(2));
			w6.BottomAttach = ((uint)(3));
			w6.LeftAttach = ((uint)(3));
			w6.RightAttach = ((uint)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.disablespinBottlesResidue = new global::QSOrmProject.DisableSpinButton();
			this.disablespinBottlesResidue.Events = ((global::Gdk.EventMask)(256));
			this.disablespinBottlesResidue.Name = "disablespinBottlesResidue";
			this.disablespinBottlesResidue.Active = false;
			this.disablespinBottlesResidue.Upper = 10000000D;
			this.disablespinBottlesResidue.Lower = -10000000D;
			this.disablespinBottlesResidue.Digits = ((uint)(0));
			this.table1.Add(this.disablespinBottlesResidue);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.disablespinBottlesResidue]));
			w7.TopAttach = ((uint)(2));
			w7.BottomAttach = ((uint)(3));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.disablespinEquipmentDeposit = new global::QSOrmProject.DisableSpinButton();
			this.disablespinEquipmentDeposit.Events = ((global::Gdk.EventMask)(256));
			this.disablespinEquipmentDeposit.Name = "disablespinEquipmentDeposit";
			this.disablespinEquipmentDeposit.Active = false;
			this.disablespinEquipmentDeposit.Upper = 100000D;
			this.disablespinEquipmentDeposit.Lower = -100000D;
			this.disablespinEquipmentDeposit.Digits = ((uint)(2));
			this.table1.Add(this.disablespinEquipmentDeposit);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.disablespinEquipmentDeposit]));
			w8.TopAttach = ((uint)(4));
			w8.BottomAttach = ((uint)(5));
			w8.LeftAttach = ((uint)(3));
			w8.RightAttach = ((uint)(4));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.disablespinMoneyDebt = new global::QSOrmProject.DisableSpinButton();
			this.disablespinMoneyDebt.Events = ((global::Gdk.EventMask)(256));
			this.disablespinMoneyDebt.Name = "disablespinMoneyDebt";
			this.disablespinMoneyDebt.Active = false;
			this.disablespinMoneyDebt.Upper = 10000000D;
			this.disablespinMoneyDebt.Lower = -10000000D;
			this.disablespinMoneyDebt.Digits = ((uint)(2));
			this.table1.Add(this.disablespinMoneyDebt);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.disablespinMoneyDebt]));
			w9.TopAttach = ((uint)(4));
			w9.BottomAttach = ((uint)(5));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytextviewComment = new global::Gamma.GtkWidgets.yTextView();
			this.ytextviewComment.CanFocus = true;
			this.ytextviewComment.Name = "ytextviewComment";
			this.GtkScrolledWindow.Add(this.ytextviewComment);
			this.table1.Add(this.GtkScrolledWindow);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow]));
			w11.TopAttach = ((uint)(7));
			w11.BottomAttach = ((uint)(8));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(4));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Дата документа:");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label11 = new global::Gtk.Label();
			this.label11.Name = "label11";
			this.label11.Xalign = 1F;
			this.label11.LabelProp = global::Mono.Unix.Catalog.GetString("Текущий долг:");
			this.table1.Add(this.label11);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1[this.label11]));
			w13.TopAttach = ((uint)(6));
			w13.BottomAttach = ((uint)(7));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label13 = new global::Gtk.Label();
			this.label13.Name = "label13";
			this.label13.Xalign = 1F;
			this.label13.LabelProp = global::Mono.Unix.Catalog.GetString("Текущие залоги:");
			this.table1.Add(this.label13);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1[this.label13]));
			w14.TopAttach = ((uint)(6));
			w14.BottomAttach = ((uint)(7));
			w14.LeftAttach = ((uint)(2));
			w14.RightAttach = ((uint)(3));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.Xalign = 1F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Клиент:");
			this.table1.Add(this.label2);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1[this.label2]));
			w15.LeftAttach = ((uint)(2));
			w15.RightAttach = ((uint)(3));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Остаток по таре:");
			this.table1.Add(this.label3);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table1[this.label3]));
			w16.TopAttach = ((uint)(2));
			w16.BottomAttach = ((uint)(3));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.Xalign = 1F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Точка доставки:");
			this.table1.Add(this.label4);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1[this.label4]));
			w17.TopAttach = ((uint)(1));
			w17.BottomAttach = ((uint)(2));
			w17.LeftAttach = ((uint)(2));
			w17.RightAttach = ((uint)(3));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.Xalign = 1F;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Залог за тару:");
			this.table1.Add(this.label5);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table1[this.label5]));
			w18.TopAttach = ((uint)(2));
			w18.BottomAttach = ((uint)(3));
			w18.LeftAttach = ((uint)(2));
			w18.RightAttach = ((uint)(3));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label6 = new global::Gtk.Label();
			this.label6.Name = "label6";
			this.label6.Xalign = 1F;
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString("Залог за оборудование:");
			this.table1.Add(this.label6);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1[this.label6]));
			w19.TopAttach = ((uint)(4));
			w19.BottomAttach = ((uint)(5));
			w19.LeftAttach = ((uint)(2));
			w19.RightAttach = ((uint)(3));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label();
			this.label7.Name = "label7";
			this.label7.Xalign = 1F;
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString("Текущий остаток:");
			this.table1.Add(this.label7);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table1[this.label7]));
			w20.TopAttach = ((uint)(3));
			w20.BottomAttach = ((uint)(4));
			w20.XOptions = ((global::Gtk.AttachOptions)(4));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label8 = new global::Gtk.Label();
			this.label8.Name = "label8";
			this.label8.Xalign = 1F;
			this.label8.Yalign = 0F;
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString("Комментарий:");
			this.table1.Add(this.label8);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table1[this.label8]));
			w21.TopAttach = ((uint)(7));
			w21.BottomAttach = ((uint)(8));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCurrent = new global::Gtk.Label();
			this.labelCurrent.Name = "labelCurrent";
			this.labelCurrent.Xalign = 1F;
			this.labelCurrent.LabelProp = global::Mono.Unix.Catalog.GetString("Текущие залоги:");
			this.table1.Add(this.labelCurrent);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table1[this.labelCurrent]));
			w22.TopAttach = ((uint)(3));
			w22.BottomAttach = ((uint)(4));
			w22.LeftAttach = ((uint)(2));
			w22.RightAttach = ((uint)(3));
			w22.XOptions = ((global::Gtk.AttachOptions)(4));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCurrentBootle = new global::Gtk.Label();
			this.labelCurrentBootle.Name = "labelCurrentBootle";
			this.table1.Add(this.labelCurrentBootle);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table1[this.labelCurrentBootle]));
			w23.TopAttach = ((uint)(3));
			w23.BottomAttach = ((uint)(4));
			w23.LeftAttach = ((uint)(1));
			w23.RightAttach = ((uint)(2));
			w23.XOptions = ((global::Gtk.AttachOptions)(4));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCurrentBottleDeposit = new global::Gtk.Label();
			this.labelCurrentBottleDeposit.Name = "labelCurrentBottleDeposit";
			this.labelCurrentBottleDeposit.LabelProp = global::Mono.Unix.Catalog.GetString("label4");
			this.table1.Add(this.labelCurrentBottleDeposit);
			global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.table1[this.labelCurrentBottleDeposit]));
			w24.TopAttach = ((uint)(3));
			w24.BottomAttach = ((uint)(4));
			w24.LeftAttach = ((uint)(3));
			w24.RightAttach = ((uint)(4));
			w24.XOptions = ((global::Gtk.AttachOptions)(4));
			w24.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCurrentEquipmentDeposit = new global::Gtk.Label();
			this.labelCurrentEquipmentDeposit.Name = "labelCurrentEquipmentDeposit";
			this.labelCurrentEquipmentDeposit.LabelProp = global::Mono.Unix.Catalog.GetString("label8");
			this.table1.Add(this.labelCurrentEquipmentDeposit);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table1[this.labelCurrentEquipmentDeposit]));
			w25.TopAttach = ((uint)(6));
			w25.BottomAttach = ((uint)(7));
			w25.LeftAttach = ((uint)(3));
			w25.RightAttach = ((uint)(4));
			w25.XOptions = ((global::Gtk.AttachOptions)(4));
			w25.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCurrentMoneyDebt = new global::Gtk.Label();
			this.labelCurrentMoneyDebt.Name = "labelCurrentMoneyDebt";
			this.labelCurrentMoneyDebt.LabelProp = global::Mono.Unix.Catalog.GetString("label6");
			this.table1.Add(this.labelCurrentMoneyDebt);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table1[this.labelCurrentMoneyDebt]));
			w26.TopAttach = ((uint)(6));
			w26.BottomAttach = ((uint)(7));
			w26.LeftAttach = ((uint)(1));
			w26.RightAttach = ((uint)(2));
			w26.XOptions = ((global::Gtk.AttachOptions)(4));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.moneyLabel = new global::Gtk.Label();
			this.moneyLabel.Name = "moneyLabel";
			this.moneyLabel.Xalign = 1F;
			this.moneyLabel.LabelProp = global::Mono.Unix.Catalog.GetString("Долги по деньгам:");
			this.table1.Add(this.moneyLabel);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.table1[this.moneyLabel]));
			w27.TopAttach = ((uint)(4));
			w27.BottomAttach = ((uint)(5));
			w27.XOptions = ((global::Gtk.AttachOptions)(4));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yenumcomboDebtPaymentType = new global::Gamma.Widgets.yEnumComboBox();
			this.yenumcomboDebtPaymentType.Name = "yenumcomboDebtPaymentType";
			this.yenumcomboDebtPaymentType.ShowSpecialStateAll = false;
			this.yenumcomboDebtPaymentType.ShowSpecialStateNot = false;
			this.yenumcomboDebtPaymentType.UseShortTitle = false;
			this.yenumcomboDebtPaymentType.DefaultFirst = false;
			this.table1.Add(this.yenumcomboDebtPaymentType);
			global::Gtk.Table.TableChild w28 = ((global::Gtk.Table.TableChild)(this.table1[this.yenumcomboDebtPaymentType]));
			w28.TopAttach = ((uint)(5));
			w28.BottomAttach = ((uint)(6));
			w28.LeftAttach = ((uint)(1));
			w28.RightAttach = ((uint)(2));
			w28.XOptions = ((global::Gtk.AttachOptions)(4));
			w28.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ypickerDocDate = new global::Gamma.Widgets.yDatePicker();
			this.ypickerDocDate.Events = ((global::Gdk.EventMask)(256));
			this.ypickerDocDate.Name = "ypickerDocDate";
			this.ypickerDocDate.WithTime = false;
			this.ypickerDocDate.Date = new global::System.DateTime(0);
			this.ypickerDocDate.IsEditable = true;
			this.ypickerDocDate.AutoSeparation = false;
			this.table1.Add(this.ypickerDocDate);
			global::Gtk.Table.TableChild w29 = ((global::Gtk.Table.TableChild)(this.table1[this.ypickerDocDate]));
			w29.LeftAttach = ((uint)(1));
			w29.RightAttach = ((uint)(2));
			w29.XOptions = ((global::Gtk.AttachOptions)(4));
			w29.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yreferenceClientSelector = new global::Gamma.Widgets.yEntryReferenceVM();
			this.yreferenceClientSelector.Events = ((global::Gdk.EventMask)(256));
			this.yreferenceClientSelector.Name = "yreferenceClientSelector";
			this.table1.Add(this.yreferenceClientSelector);
			global::Gtk.Table.TableChild w30 = ((global::Gtk.Table.TableChild)(this.table1[this.yreferenceClientSelector]));
			w30.LeftAttach = ((uint)(3));
			w30.RightAttach = ((uint)(4));
			w30.XOptions = ((global::Gtk.AttachOptions)(4));
			w30.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yreferenceDeliveryPoint = new global::Gamma.Widgets.yEntryReferenceVM();
			this.yreferenceDeliveryPoint.Events = ((global::Gdk.EventMask)(256));
			this.yreferenceDeliveryPoint.Name = "yreferenceDeliveryPoint";
			this.table1.Add(this.yreferenceDeliveryPoint);
			global::Gtk.Table.TableChild w31 = ((global::Gtk.Table.TableChild)(this.table1[this.yreferenceDeliveryPoint]));
			w31.TopAttach = ((uint)(1));
			w31.BottomAttach = ((uint)(2));
			w31.LeftAttach = ((uint)(3));
			w31.RightAttach = ((uint)(4));
			w31.XOptions = ((global::Gtk.AttachOptions)(4));
			w31.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox2.Add(this.table1);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.table1]));
			w32.Position = 1;
			this.Add(this.vbox2);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.yreferenceDeliveryPoint.Changed += new global::System.EventHandler(this.OnYreferenceDeliveryPointChanged);
			this.yreferenceClientSelector.Changed += new global::System.EventHandler(this.OnYentryreferencevmClientSelectorChanged);
			this.disablespinMoneyDebt.ActiveChanged += new global::System.EventHandler(this.OnDisablespinMoneyDebtActiveChanged);
		}
	}
}
