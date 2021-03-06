
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ServiceDialogs
{
	public partial class LoadFrom1cClientsAndDeliveryPoints
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.FileChooserButton filechooser;

		private global::Gtk.ProgressBar progressbar;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Button button1;

		private global::Gtk.Button buttonLoad;

		private global::Gtk.Button buttonSave;

		private global::Gtk.Button button2;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Table tableCounteparties;

		private global::Gtk.Label labelCounterparties;

		private global::Gtk.Label labelCounterpartyErrors;

		private global::Gtk.Label labelCounterpartyErrorsValue;

		private global::Gtk.Label labelCounterpartyFails;

		private global::Gtk.Label labelCounterpartyFailsValue;

		private global::Gtk.Label labelCounterpartySuccess;

		private global::Gtk.Label labelCounterpartySuccessValue;

		private global::Gtk.Label labelCounterpartyTotal;

		private global::Gtk.Label labelCounterpartyTotalValue;

		private global::Gtk.Table tableCounteparties1;

		private global::Gtk.Label labelDP;

		private global::Gtk.Label labelDPErrors;

		private global::Gtk.Label labelDPErrorsValue;

		private global::Gtk.Label labelDPFails;

		private global::Gtk.Label labelDPFailsValue;

		private global::Gtk.Label labelDPSuccess;

		private global::Gtk.Label labelDPSuccessValue;

		private global::Gtk.Label labelDPTotal;

		private global::Gtk.Label labelDPTotalValue;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ServiceDialogs.LoadFrom1cClientsAndDeliveryPoints
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ServiceDialogs.LoadFrom1cClientsAndDeliveryPoints";
			// Container child Vodovoz.ServiceDialogs.LoadFrom1cClientsAndDeliveryPoints.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.filechooser = new global::Gtk.FileChooserButton(global::Mono.Unix.Catalog.GetString("Выберите XML файл с данными из 1с"), ((global::Gtk.FileChooserAction)(0)));
			this.filechooser.Name = "filechooser";
			this.vbox2.Add(this.filechooser);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.filechooser]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.progressbar = new global::Gtk.ProgressBar();
			this.progressbar.Name = "progressbar";
			this.vbox2.Add(this.progressbar);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.progressbar]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.button1 = new global::Gtk.Button();
			this.button1.CanFocus = true;
			this.button1.Name = "button1";
			this.button1.UseUnderline = true;
			this.button1.Label = global::Mono.Unix.Catalog.GetString("Counterparties");
			this.hbox1.Add(this.button1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.button1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonLoad = new global::Gtk.Button();
			this.buttonLoad.CanFocus = true;
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.UseUnderline = true;
			this.buttonLoad.Label = global::Mono.Unix.Catalog.GetString("Load");
			this.hbox1.Add(this.buttonLoad);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonLoad]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString("Save");
			this.hbox1.Add(this.buttonSave);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonSave]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.button2 = new global::Gtk.Button();
			this.button2.CanFocus = true;
			this.button2.Name = "button2";
			this.button2.UseUnderline = true;
			this.button2.Label = global::Mono.Unix.Catalog.GetString("Start ALL");
			this.hbox1.Add(this.button2);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.button2]));
			w6.Position = 3;
			w6.Expand = false;
			w6.Fill = false;
			this.vbox2.Add(this.hbox1);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.tableCounteparties = new global::Gtk.Table(((uint)(5)), ((uint)(2)), false);
			this.tableCounteparties.Name = "tableCounteparties";
			this.tableCounteparties.RowSpacing = ((uint)(6));
			this.tableCounteparties.ColumnSpacing = ((uint)(6));
			// Container child tableCounteparties.Gtk.Table+TableChild
			this.labelCounterparties = new global::Gtk.Label();
			this.labelCounterparties.Name = "labelCounterparties";
			this.labelCounterparties.LabelProp = global::Mono.Unix.Catalog.GetString("Контрагенты");
			this.tableCounteparties.Add(this.labelCounterparties);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.tableCounteparties[this.labelCounterparties]));
			w8.RightAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties.Gtk.Table+TableChild
			this.labelCounterpartyErrors = new global::Gtk.Label();
			this.labelCounterpartyErrors.Name = "labelCounterpartyErrors";
			this.labelCounterpartyErrors.Xalign = 1F;
			this.labelCounterpartyErrors.LabelProp = global::Mono.Unix.Catalog.GetString("С ошибками:");
			this.tableCounteparties.Add(this.labelCounterpartyErrors);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.tableCounteparties[this.labelCounterpartyErrors]));
			w9.TopAttach = ((uint)(3));
			w9.BottomAttach = ((uint)(4));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties.Gtk.Table+TableChild
			this.labelCounterpartyErrorsValue = new global::Gtk.Label();
			this.labelCounterpartyErrorsValue.Name = "labelCounterpartyErrorsValue";
			this.labelCounterpartyErrorsValue.Xalign = 0F;
			this.labelCounterpartyErrorsValue.LabelProp = global::Mono.Unix.Catalog.GetString("0");
			this.tableCounteparties.Add(this.labelCounterpartyErrorsValue);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.tableCounteparties[this.labelCounterpartyErrorsValue]));
			w10.TopAttach = ((uint)(3));
			w10.BottomAttach = ((uint)(4));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties.Gtk.Table+TableChild
			this.labelCounterpartyFails = new global::Gtk.Label();
			this.labelCounterpartyFails.Name = "labelCounterpartyFails";
			this.labelCounterpartyFails.Xalign = 1F;
			this.labelCounterpartyFails.LabelProp = global::Mono.Unix.Catalog.GetString("Не загружено:");
			this.tableCounteparties.Add(this.labelCounterpartyFails);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.tableCounteparties[this.labelCounterpartyFails]));
			w11.TopAttach = ((uint)(4));
			w11.BottomAttach = ((uint)(5));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties.Gtk.Table+TableChild
			this.labelCounterpartyFailsValue = new global::Gtk.Label();
			this.labelCounterpartyFailsValue.Name = "labelCounterpartyFailsValue";
			this.labelCounterpartyFailsValue.Xalign = 0F;
			this.labelCounterpartyFailsValue.LabelProp = global::Mono.Unix.Catalog.GetString("0");
			this.tableCounteparties.Add(this.labelCounterpartyFailsValue);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.tableCounteparties[this.labelCounterpartyFailsValue]));
			w12.TopAttach = ((uint)(4));
			w12.BottomAttach = ((uint)(5));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties.Gtk.Table+TableChild
			this.labelCounterpartySuccess = new global::Gtk.Label();
			this.labelCounterpartySuccess.Name = "labelCounterpartySuccess";
			this.labelCounterpartySuccess.Xalign = 1F;
			this.labelCounterpartySuccess.LabelProp = global::Mono.Unix.Catalog.GetString("Успешно:");
			this.tableCounteparties.Add(this.labelCounterpartySuccess);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.tableCounteparties[this.labelCounterpartySuccess]));
			w13.TopAttach = ((uint)(2));
			w13.BottomAttach = ((uint)(3));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties.Gtk.Table+TableChild
			this.labelCounterpartySuccessValue = new global::Gtk.Label();
			this.labelCounterpartySuccessValue.Name = "labelCounterpartySuccessValue";
			this.labelCounterpartySuccessValue.Xalign = 0F;
			this.labelCounterpartySuccessValue.LabelProp = global::Mono.Unix.Catalog.GetString("0");
			this.tableCounteparties.Add(this.labelCounterpartySuccessValue);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.tableCounteparties[this.labelCounterpartySuccessValue]));
			w14.TopAttach = ((uint)(2));
			w14.BottomAttach = ((uint)(3));
			w14.LeftAttach = ((uint)(1));
			w14.RightAttach = ((uint)(2));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties.Gtk.Table+TableChild
			this.labelCounterpartyTotal = new global::Gtk.Label();
			this.labelCounterpartyTotal.Name = "labelCounterpartyTotal";
			this.labelCounterpartyTotal.Xalign = 1F;
			this.labelCounterpartyTotal.LabelProp = global::Mono.Unix.Catalog.GetString("Всего:");
			this.tableCounteparties.Add(this.labelCounterpartyTotal);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.tableCounteparties[this.labelCounterpartyTotal]));
			w15.TopAttach = ((uint)(1));
			w15.BottomAttach = ((uint)(2));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties.Gtk.Table+TableChild
			this.labelCounterpartyTotalValue = new global::Gtk.Label();
			this.labelCounterpartyTotalValue.Name = "labelCounterpartyTotalValue";
			this.labelCounterpartyTotalValue.Xalign = 0F;
			this.labelCounterpartyTotalValue.LabelProp = global::Mono.Unix.Catalog.GetString("0");
			this.tableCounteparties.Add(this.labelCounterpartyTotalValue);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.tableCounteparties[this.labelCounterpartyTotalValue]));
			w16.TopAttach = ((uint)(1));
			w16.BottomAttach = ((uint)(2));
			w16.LeftAttach = ((uint)(1));
			w16.RightAttach = ((uint)(2));
			w16.XOptions = ((global::Gtk.AttachOptions)(7));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			this.hbox2.Add(this.tableCounteparties);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.tableCounteparties]));
			w17.Position = 0;
			w17.Expand = false;
			w17.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.tableCounteparties1 = new global::Gtk.Table(((uint)(5)), ((uint)(2)), false);
			this.tableCounteparties1.Name = "tableCounteparties1";
			this.tableCounteparties1.RowSpacing = ((uint)(6));
			this.tableCounteparties1.ColumnSpacing = ((uint)(6));
			// Container child tableCounteparties1.Gtk.Table+TableChild
			this.labelDP = new global::Gtk.Label();
			this.labelDP.Name = "labelDP";
			this.labelDP.Xalign = 0F;
			this.labelDP.LabelProp = global::Mono.Unix.Catalog.GetString("Точки доставки");
			this.tableCounteparties1.Add(this.labelDP);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.tableCounteparties1[this.labelDP]));
			w18.RightAttach = ((uint)(2));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties1.Gtk.Table+TableChild
			this.labelDPErrors = new global::Gtk.Label();
			this.labelDPErrors.Name = "labelDPErrors";
			this.labelDPErrors.Xalign = 1F;
			this.labelDPErrors.LabelProp = global::Mono.Unix.Catalog.GetString("С ошибками:");
			this.tableCounteparties1.Add(this.labelDPErrors);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.tableCounteparties1[this.labelDPErrors]));
			w19.TopAttach = ((uint)(3));
			w19.BottomAttach = ((uint)(4));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties1.Gtk.Table+TableChild
			this.labelDPErrorsValue = new global::Gtk.Label();
			this.labelDPErrorsValue.Name = "labelDPErrorsValue";
			this.labelDPErrorsValue.Xalign = 0F;
			this.labelDPErrorsValue.LabelProp = global::Mono.Unix.Catalog.GetString("0");
			this.tableCounteparties1.Add(this.labelDPErrorsValue);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.tableCounteparties1[this.labelDPErrorsValue]));
			w20.TopAttach = ((uint)(3));
			w20.BottomAttach = ((uint)(4));
			w20.LeftAttach = ((uint)(1));
			w20.RightAttach = ((uint)(2));
			w20.XOptions = ((global::Gtk.AttachOptions)(4));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties1.Gtk.Table+TableChild
			this.labelDPFails = new global::Gtk.Label();
			this.labelDPFails.Name = "labelDPFails";
			this.labelDPFails.Xalign = 1F;
			this.labelDPFails.LabelProp = global::Mono.Unix.Catalog.GetString("Не загружено:");
			this.tableCounteparties1.Add(this.labelDPFails);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.tableCounteparties1[this.labelDPFails]));
			w21.TopAttach = ((uint)(4));
			w21.BottomAttach = ((uint)(5));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties1.Gtk.Table+TableChild
			this.labelDPFailsValue = new global::Gtk.Label();
			this.labelDPFailsValue.Name = "labelDPFailsValue";
			this.labelDPFailsValue.Xalign = 0F;
			this.labelDPFailsValue.LabelProp = global::Mono.Unix.Catalog.GetString("0");
			this.tableCounteparties1.Add(this.labelDPFailsValue);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.tableCounteparties1[this.labelDPFailsValue]));
			w22.TopAttach = ((uint)(4));
			w22.BottomAttach = ((uint)(5));
			w22.LeftAttach = ((uint)(1));
			w22.RightAttach = ((uint)(2));
			w22.XOptions = ((global::Gtk.AttachOptions)(4));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties1.Gtk.Table+TableChild
			this.labelDPSuccess = new global::Gtk.Label();
			this.labelDPSuccess.Name = "labelDPSuccess";
			this.labelDPSuccess.Xalign = 1F;
			this.labelDPSuccess.LabelProp = global::Mono.Unix.Catalog.GetString("Успешно:");
			this.tableCounteparties1.Add(this.labelDPSuccess);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.tableCounteparties1[this.labelDPSuccess]));
			w23.TopAttach = ((uint)(2));
			w23.BottomAttach = ((uint)(3));
			w23.XOptions = ((global::Gtk.AttachOptions)(4));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties1.Gtk.Table+TableChild
			this.labelDPSuccessValue = new global::Gtk.Label();
			this.labelDPSuccessValue.Name = "labelDPSuccessValue";
			this.labelDPSuccessValue.Xalign = 0F;
			this.labelDPSuccessValue.LabelProp = global::Mono.Unix.Catalog.GetString("0");
			this.tableCounteparties1.Add(this.labelDPSuccessValue);
			global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.tableCounteparties1[this.labelDPSuccessValue]));
			w24.TopAttach = ((uint)(2));
			w24.BottomAttach = ((uint)(3));
			w24.LeftAttach = ((uint)(1));
			w24.RightAttach = ((uint)(2));
			w24.XOptions = ((global::Gtk.AttachOptions)(4));
			w24.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties1.Gtk.Table+TableChild
			this.labelDPTotal = new global::Gtk.Label();
			this.labelDPTotal.Name = "labelDPTotal";
			this.labelDPTotal.Xalign = 1F;
			this.labelDPTotal.LabelProp = global::Mono.Unix.Catalog.GetString("Всего:");
			this.tableCounteparties1.Add(this.labelDPTotal);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.tableCounteparties1[this.labelDPTotal]));
			w25.TopAttach = ((uint)(1));
			w25.BottomAttach = ((uint)(2));
			w25.XOptions = ((global::Gtk.AttachOptions)(4));
			w25.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableCounteparties1.Gtk.Table+TableChild
			this.labelDPTotalValue = new global::Gtk.Label();
			this.labelDPTotalValue.Name = "labelDPTotalValue";
			this.labelDPTotalValue.Xalign = 0F;
			this.labelDPTotalValue.LabelProp = global::Mono.Unix.Catalog.GetString("0");
			this.tableCounteparties1.Add(this.labelDPTotalValue);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.tableCounteparties1[this.labelDPTotalValue]));
			w26.TopAttach = ((uint)(1));
			w26.BottomAttach = ((uint)(2));
			w26.LeftAttach = ((uint)(1));
			w26.RightAttach = ((uint)(2));
			w26.XOptions = ((global::Gtk.AttachOptions)(7));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			this.hbox2.Add(this.tableCounteparties1);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.tableCounteparties1]));
			w27.Position = 1;
			w27.Expand = false;
			w27.Fill = false;
			this.vbox2.Add(this.hbox2);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox2]));
			w28.Position = 3;
			w28.Expand = false;
			w28.Fill = false;
			this.Add(this.vbox2);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.filechooser.SelectionChanged += new global::System.EventHandler(this.OnFilechooserXMLSelectionChanged);
			this.button1.Clicked += new global::System.EventHandler(this.OnButton1Clicked);
			this.buttonLoad.Clicked += new global::System.EventHandler(this.OnButtonLoadClicked);
			this.buttonSave.Clicked += new global::System.EventHandler(this.OnButtonSaveClicked);
			this.button2.Clicked += new global::System.EventHandler(this.OnButton2Clicked);
		}
	}
}
