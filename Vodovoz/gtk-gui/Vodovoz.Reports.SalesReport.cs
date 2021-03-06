
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Reports
{
	public partial class SalesReport
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Label label1;

		private global::QSWidgetLib.DatePeriodPicker dateperiodpicker;

		private global::Gtk.HBox hboxNomType1;

		private global::Gamma.GtkWidgets.yLabel ylabel8;

		private global::Gamma.GtkWidgets.yCheckButton ycheckbuttonDetail;

		private global::Gtk.HBox hboxNomType;

		private global::Gamma.GtkWidgets.yLabel ylabel3;

		private global::Gamma.GtkWidgets.yLabel ylabelNomType;

		private global::Gtk.Button buttonNomTypeSelect;

		private global::Gtk.Button buttonNomTypeUnselect;

		private global::Gtk.HBox hboxNomen;

		private global::Gamma.GtkWidgets.yLabel ylabel4;

		private global::Gamma.GtkWidgets.yLabel ylabelNomen;

		private global::Gtk.Button buttonNomenSelect;

		private global::Gtk.Button buttonNomenUnselect;

		private global::Gtk.HBox hboxClient;

		private global::Gamma.GtkWidgets.yLabel ylabel5;

		private global::Gamma.GtkWidgets.yLabel ylabelClient;

		private global::Gtk.Button buttonClientSelect;

		private global::Gtk.Button buttonClientUnselect;

		private global::Gtk.HBox hboxOrg;

		private global::Gamma.GtkWidgets.yLabel ylabel6;

		private global::Gamma.GtkWidgets.yLabel ylabelOrg;

		private global::Gtk.Button buttonOrgSelect;

		private global::Gtk.Button buttonOrgUnselect;

		private global::Gtk.HBox hboxDiscountReason;

		private global::Gamma.GtkWidgets.yLabel ylabel7;

		private global::Gamma.GtkWidgets.yLabel ylabelDiscountReason;

		private global::Gtk.Button buttonDiscountReasonSelect;

		private global::Gtk.Button buttonDiscountReasonUnselect;

		private global::Gtk.HBox hboxSubdivizion;

		private global::Gamma.GtkWidgets.yLabel ylabel10;

		private global::Gamma.GtkWidgets.yLabel yLblSubdivision;

		private global::Gtk.Button btnSubdivisionSelect;

		private global::Gtk.Button btnSubdivisionDeselect;

		private global::Gtk.HBox hboxOrderAuthor;

		private global::Gamma.GtkWidgets.yLabel ylabel9;

		private global::Gamma.GtkWidgets.yLabel yLblOrderAuthor;

		private global::Gtk.Button btnOrderAuthorSelect;

		private global::Gtk.Button btnOrderAuthorDeselect;

		private global::Gtk.HSeparator hseparator1;

		private global::Gtk.Label labelTableTitle;

		private global::QSWidgetLib.SearchEntity searchEntityInSelectedList;

		private global::Gtk.Table table1;

		private global::Gtk.Button buttonSelectAll;

		private global::Gtk.Button buttonUnselectAll;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTreeView ytreeviewSelectedList;

		private global::Gtk.Button buttonCreateReport;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Reports.SalesReport
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Reports.SalesReport";
			// Container child Vodovoz.Reports.SalesReport.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 0F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Период:");
			this.hbox1.Add(this.label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.label1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.dateperiodpicker = new global::QSWidgetLib.DatePeriodPicker();
			this.dateperiodpicker.Events = ((global::Gdk.EventMask)(256));
			this.dateperiodpicker.Name = "dateperiodpicker";
			this.dateperiodpicker.StartDate = new global::System.DateTime(0);
			this.dateperiodpicker.EndDate = new global::System.DateTime(0);
			this.hbox1.Add(this.dateperiodpicker);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.dateperiodpicker]));
			w2.Position = 1;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hboxNomType1 = new global::Gtk.HBox();
			this.hboxNomType1.Name = "hboxNomType1";
			this.hboxNomType1.Spacing = 6;
			// Container child hboxNomType1.Gtk.Box+BoxChild
			this.ylabel8 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel8.Name = "ylabel8";
			this.ylabel8.Xalign = 0F;
			this.ylabel8.LabelProp = global::Mono.Unix.Catalog.GetString("Подробный отчет: ");
			this.hboxNomType1.Add(this.ylabel8);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hboxNomType1[this.ylabel8]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hboxNomType1.Gtk.Box+BoxChild
			this.ycheckbuttonDetail = new global::Gamma.GtkWidgets.yCheckButton();
			this.ycheckbuttonDetail.CanFocus = true;
			this.ycheckbuttonDetail.Name = "ycheckbuttonDetail";
			this.ycheckbuttonDetail.Label = "";
			this.ycheckbuttonDetail.DrawIndicator = true;
			this.ycheckbuttonDetail.UseUnderline = true;
			this.hboxNomType1.Add(this.ycheckbuttonDetail);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hboxNomType1[this.ycheckbuttonDetail]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			this.vbox1.Add(this.hboxNomType1);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hboxNomType1]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hboxNomType = new global::Gtk.HBox();
			this.hboxNomType.Name = "hboxNomType";
			this.hboxNomType.Spacing = 6;
			// Container child hboxNomType.Gtk.Box+BoxChild
			this.ylabel3 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel3.Name = "ylabel3";
			this.ylabel3.Xalign = 0F;
			this.ylabel3.LabelProp = global::Mono.Unix.Catalog.GetString("Категория: ");
			this.hboxNomType.Add(this.ylabel3);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hboxNomType[this.ylabel3]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hboxNomType.Gtk.Box+BoxChild
			this.ylabelNomType = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelNomType.Name = "ylabelNomType";
			this.ylabelNomType.Xalign = 1F;
			this.hboxNomType.Add(this.ylabelNomType);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hboxNomType[this.ylabelNomType]));
			w8.Position = 1;
			// Container child hboxNomType.Gtk.Box+BoxChild
			this.buttonNomTypeSelect = new global::Gtk.Button();
			this.buttonNomTypeSelect.CanFocus = true;
			this.buttonNomTypeSelect.Name = "buttonNomTypeSelect";
			this.buttonNomTypeSelect.UseUnderline = true;
			this.buttonNomTypeSelect.Label = global::Mono.Unix.Catalog.GetString("Вкл.");
			this.hboxNomType.Add(this.buttonNomTypeSelect);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hboxNomType[this.buttonNomTypeSelect]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hboxNomType.Gtk.Box+BoxChild
			this.buttonNomTypeUnselect = new global::Gtk.Button();
			this.buttonNomTypeUnselect.CanFocus = true;
			this.buttonNomTypeUnselect.Name = "buttonNomTypeUnselect";
			this.buttonNomTypeUnselect.UseUnderline = true;
			this.buttonNomTypeUnselect.Label = global::Mono.Unix.Catalog.GetString("Искл.");
			this.hboxNomType.Add(this.buttonNomTypeUnselect);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hboxNomType[this.buttonNomTypeUnselect]));
			w10.Position = 3;
			w10.Expand = false;
			w10.Fill = false;
			this.vbox1.Add(this.hboxNomType);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hboxNomType]));
			w11.Position = 2;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hboxNomen = new global::Gtk.HBox();
			this.hboxNomen.Name = "hboxNomen";
			this.hboxNomen.Spacing = 6;
			// Container child hboxNomen.Gtk.Box+BoxChild
			this.ylabel4 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel4.Name = "ylabel4";
			this.ylabel4.Xalign = 0F;
			this.ylabel4.LabelProp = global::Mono.Unix.Catalog.GetString("Номенклатуры: ");
			this.hboxNomen.Add(this.ylabel4);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hboxNomen[this.ylabel4]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hboxNomen.Gtk.Box+BoxChild
			this.ylabelNomen = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelNomen.Name = "ylabelNomen";
			this.ylabelNomen.Xalign = 1F;
			this.hboxNomen.Add(this.ylabelNomen);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hboxNomen[this.ylabelNomen]));
			w13.Position = 1;
			// Container child hboxNomen.Gtk.Box+BoxChild
			this.buttonNomenSelect = new global::Gtk.Button();
			this.buttonNomenSelect.CanFocus = true;
			this.buttonNomenSelect.Name = "buttonNomenSelect";
			this.buttonNomenSelect.UseUnderline = true;
			this.buttonNomenSelect.Label = global::Mono.Unix.Catalog.GetString("Вкл.");
			this.hboxNomen.Add(this.buttonNomenSelect);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hboxNomen[this.buttonNomenSelect]));
			w14.Position = 2;
			w14.Expand = false;
			w14.Fill = false;
			// Container child hboxNomen.Gtk.Box+BoxChild
			this.buttonNomenUnselect = new global::Gtk.Button();
			this.buttonNomenUnselect.CanFocus = true;
			this.buttonNomenUnselect.Name = "buttonNomenUnselect";
			this.buttonNomenUnselect.UseUnderline = true;
			this.buttonNomenUnselect.Label = global::Mono.Unix.Catalog.GetString("Искл.");
			this.hboxNomen.Add(this.buttonNomenUnselect);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hboxNomen[this.buttonNomenUnselect]));
			w15.Position = 3;
			w15.Expand = false;
			w15.Fill = false;
			this.vbox1.Add(this.hboxNomen);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hboxNomen]));
			w16.Position = 3;
			w16.Expand = false;
			w16.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hboxClient = new global::Gtk.HBox();
			this.hboxClient.Name = "hboxClient";
			this.hboxClient.Spacing = 6;
			// Container child hboxClient.Gtk.Box+BoxChild
			this.ylabel5 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel5.Name = "ylabel5";
			this.ylabel5.Xalign = 0F;
			this.ylabel5.LabelProp = global::Mono.Unix.Catalog.GetString("Контрагенты: ");
			this.hboxClient.Add(this.ylabel5);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hboxClient[this.ylabel5]));
			w17.Position = 0;
			w17.Expand = false;
			w17.Fill = false;
			// Container child hboxClient.Gtk.Box+BoxChild
			this.ylabelClient = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelClient.Name = "ylabelClient";
			this.ylabelClient.Xalign = 1F;
			this.hboxClient.Add(this.ylabelClient);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hboxClient[this.ylabelClient]));
			w18.Position = 1;
			// Container child hboxClient.Gtk.Box+BoxChild
			this.buttonClientSelect = new global::Gtk.Button();
			this.buttonClientSelect.CanFocus = true;
			this.buttonClientSelect.Name = "buttonClientSelect";
			this.buttonClientSelect.UseUnderline = true;
			this.buttonClientSelect.Label = global::Mono.Unix.Catalog.GetString("Вкл.");
			this.hboxClient.Add(this.buttonClientSelect);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hboxClient[this.buttonClientSelect]));
			w19.Position = 2;
			w19.Expand = false;
			w19.Fill = false;
			// Container child hboxClient.Gtk.Box+BoxChild
			this.buttonClientUnselect = new global::Gtk.Button();
			this.buttonClientUnselect.CanFocus = true;
			this.buttonClientUnselect.Name = "buttonClientUnselect";
			this.buttonClientUnselect.UseUnderline = true;
			this.buttonClientUnselect.Label = global::Mono.Unix.Catalog.GetString("Искл.");
			this.hboxClient.Add(this.buttonClientUnselect);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hboxClient[this.buttonClientUnselect]));
			w20.Position = 3;
			w20.Expand = false;
			w20.Fill = false;
			this.vbox1.Add(this.hboxClient);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hboxClient]));
			w21.Position = 4;
			w21.Expand = false;
			w21.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hboxOrg = new global::Gtk.HBox();
			this.hboxOrg.Name = "hboxOrg";
			this.hboxOrg.Spacing = 6;
			// Container child hboxOrg.Gtk.Box+BoxChild
			this.ylabel6 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel6.Name = "ylabel6";
			this.ylabel6.Xalign = 0F;
			this.ylabel6.LabelProp = global::Mono.Unix.Catalog.GetString("Поставщики: ");
			this.hboxOrg.Add(this.ylabel6);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.hboxOrg[this.ylabel6]));
			w22.Position = 0;
			w22.Expand = false;
			w22.Fill = false;
			// Container child hboxOrg.Gtk.Box+BoxChild
			this.ylabelOrg = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelOrg.Name = "ylabelOrg";
			this.ylabelOrg.Xalign = 1F;
			this.hboxOrg.Add(this.ylabelOrg);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.hboxOrg[this.ylabelOrg]));
			w23.Position = 1;
			// Container child hboxOrg.Gtk.Box+BoxChild
			this.buttonOrgSelect = new global::Gtk.Button();
			this.buttonOrgSelect.CanFocus = true;
			this.buttonOrgSelect.Name = "buttonOrgSelect";
			this.buttonOrgSelect.UseUnderline = true;
			this.buttonOrgSelect.Label = global::Mono.Unix.Catalog.GetString("Вкл.");
			this.hboxOrg.Add(this.buttonOrgSelect);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.hboxOrg[this.buttonOrgSelect]));
			w24.Position = 2;
			w24.Expand = false;
			w24.Fill = false;
			// Container child hboxOrg.Gtk.Box+BoxChild
			this.buttonOrgUnselect = new global::Gtk.Button();
			this.buttonOrgUnselect.CanFocus = true;
			this.buttonOrgUnselect.Name = "buttonOrgUnselect";
			this.buttonOrgUnselect.UseUnderline = true;
			this.buttonOrgUnselect.Label = global::Mono.Unix.Catalog.GetString("Искл.");
			this.hboxOrg.Add(this.buttonOrgUnselect);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.hboxOrg[this.buttonOrgUnselect]));
			w25.Position = 3;
			w25.Expand = false;
			w25.Fill = false;
			this.vbox1.Add(this.hboxOrg);
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hboxOrg]));
			w26.Position = 5;
			w26.Expand = false;
			w26.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hboxDiscountReason = new global::Gtk.HBox();
			this.hboxDiscountReason.Name = "hboxDiscountReason";
			this.hboxDiscountReason.Spacing = 6;
			// Container child hboxDiscountReason.Gtk.Box+BoxChild
			this.ylabel7 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel7.Name = "ylabel7";
			this.ylabel7.Xalign = 0F;
			this.ylabel7.LabelProp = global::Mono.Unix.Catalog.GetString("Основания скидок: ");
			this.hboxDiscountReason.Add(this.ylabel7);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hboxDiscountReason[this.ylabel7]));
			w27.Position = 0;
			w27.Expand = false;
			w27.Fill = false;
			// Container child hboxDiscountReason.Gtk.Box+BoxChild
			this.ylabelDiscountReason = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelDiscountReason.Name = "ylabelDiscountReason";
			this.ylabelDiscountReason.Xalign = 1F;
			this.hboxDiscountReason.Add(this.ylabelDiscountReason);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.hboxDiscountReason[this.ylabelDiscountReason]));
			w28.Position = 1;
			// Container child hboxDiscountReason.Gtk.Box+BoxChild
			this.buttonDiscountReasonSelect = new global::Gtk.Button();
			this.buttonDiscountReasonSelect.CanFocus = true;
			this.buttonDiscountReasonSelect.Name = "buttonDiscountReasonSelect";
			this.buttonDiscountReasonSelect.UseUnderline = true;
			this.buttonDiscountReasonSelect.Label = global::Mono.Unix.Catalog.GetString("Вкл.");
			this.hboxDiscountReason.Add(this.buttonDiscountReasonSelect);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.hboxDiscountReason[this.buttonDiscountReasonSelect]));
			w29.Position = 2;
			w29.Expand = false;
			w29.Fill = false;
			// Container child hboxDiscountReason.Gtk.Box+BoxChild
			this.buttonDiscountReasonUnselect = new global::Gtk.Button();
			this.buttonDiscountReasonUnselect.CanFocus = true;
			this.buttonDiscountReasonUnselect.Name = "buttonDiscountReasonUnselect";
			this.buttonDiscountReasonUnselect.UseUnderline = true;
			this.buttonDiscountReasonUnselect.Label = global::Mono.Unix.Catalog.GetString("Искл.");
			this.hboxDiscountReason.Add(this.buttonDiscountReasonUnselect);
			global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.hboxDiscountReason[this.buttonDiscountReasonUnselect]));
			w30.Position = 3;
			w30.Expand = false;
			w30.Fill = false;
			this.vbox1.Add(this.hboxDiscountReason);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hboxDiscountReason]));
			w31.Position = 6;
			w31.Expand = false;
			w31.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hboxSubdivizion = new global::Gtk.HBox();
			this.hboxSubdivizion.Name = "hboxSubdivizion";
			this.hboxSubdivizion.Spacing = 6;
			// Container child hboxSubdivizion.Gtk.Box+BoxChild
			this.ylabel10 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel10.Name = "ylabel10";
			this.ylabel10.Xalign = 0F;
			this.ylabel10.LabelProp = global::Mono.Unix.Catalog.GetString("Отдел:");
			this.hboxSubdivizion.Add(this.ylabel10);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.hboxSubdivizion[this.ylabel10]));
			w32.Position = 0;
			w32.Expand = false;
			w32.Fill = false;
			// Container child hboxSubdivizion.Gtk.Box+BoxChild
			this.yLblSubdivision = new global::Gamma.GtkWidgets.yLabel();
			this.yLblSubdivision.Name = "yLblSubdivision";
			this.yLblSubdivision.Xalign = 1F;
			this.hboxSubdivizion.Add(this.yLblSubdivision);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.hboxSubdivizion[this.yLblSubdivision]));
			w33.Position = 1;
			// Container child hboxSubdivizion.Gtk.Box+BoxChild
			this.btnSubdivisionSelect = new global::Gtk.Button();
			this.btnSubdivisionSelect.CanFocus = true;
			this.btnSubdivisionSelect.Name = "btnSubdivisionSelect";
			this.btnSubdivisionSelect.UseUnderline = true;
			this.btnSubdivisionSelect.Label = global::Mono.Unix.Catalog.GetString("Вкл.");
			this.hboxSubdivizion.Add(this.btnSubdivisionSelect);
			global::Gtk.Box.BoxChild w34 = ((global::Gtk.Box.BoxChild)(this.hboxSubdivizion[this.btnSubdivisionSelect]));
			w34.Position = 2;
			w34.Expand = false;
			w34.Fill = false;
			// Container child hboxSubdivizion.Gtk.Box+BoxChild
			this.btnSubdivisionDeselect = new global::Gtk.Button();
			this.btnSubdivisionDeselect.CanFocus = true;
			this.btnSubdivisionDeselect.Name = "btnSubdivisionDeselect";
			this.btnSubdivisionDeselect.UseUnderline = true;
			this.btnSubdivisionDeselect.Label = global::Mono.Unix.Catalog.GetString("Искл.");
			this.hboxSubdivizion.Add(this.btnSubdivisionDeselect);
			global::Gtk.Box.BoxChild w35 = ((global::Gtk.Box.BoxChild)(this.hboxSubdivizion[this.btnSubdivisionDeselect]));
			w35.Position = 3;
			w35.Expand = false;
			w35.Fill = false;
			this.vbox1.Add(this.hboxSubdivizion);
			global::Gtk.Box.BoxChild w36 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hboxSubdivizion]));
			w36.Position = 7;
			w36.Expand = false;
			w36.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hboxOrderAuthor = new global::Gtk.HBox();
			this.hboxOrderAuthor.Name = "hboxOrderAuthor";
			this.hboxOrderAuthor.Spacing = 6;
			// Container child hboxOrderAuthor.Gtk.Box+BoxChild
			this.ylabel9 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel9.Name = "ylabel9";
			this.ylabel9.Xalign = 0F;
			this.ylabel9.LabelProp = global::Mono.Unix.Catalog.GetString("Автор заказа:");
			this.hboxOrderAuthor.Add(this.ylabel9);
			global::Gtk.Box.BoxChild w37 = ((global::Gtk.Box.BoxChild)(this.hboxOrderAuthor[this.ylabel9]));
			w37.Position = 0;
			w37.Expand = false;
			w37.Fill = false;
			// Container child hboxOrderAuthor.Gtk.Box+BoxChild
			this.yLblOrderAuthor = new global::Gamma.GtkWidgets.yLabel();
			this.yLblOrderAuthor.Name = "yLblOrderAuthor";
			this.yLblOrderAuthor.Xalign = 1F;
			this.hboxOrderAuthor.Add(this.yLblOrderAuthor);
			global::Gtk.Box.BoxChild w38 = ((global::Gtk.Box.BoxChild)(this.hboxOrderAuthor[this.yLblOrderAuthor]));
			w38.Position = 1;
			// Container child hboxOrderAuthor.Gtk.Box+BoxChild
			this.btnOrderAuthorSelect = new global::Gtk.Button();
			this.btnOrderAuthorSelect.CanFocus = true;
			this.btnOrderAuthorSelect.Name = "btnOrderAuthorSelect";
			this.btnOrderAuthorSelect.UseUnderline = true;
			this.btnOrderAuthorSelect.Label = global::Mono.Unix.Catalog.GetString("Вкл.");
			this.hboxOrderAuthor.Add(this.btnOrderAuthorSelect);
			global::Gtk.Box.BoxChild w39 = ((global::Gtk.Box.BoxChild)(this.hboxOrderAuthor[this.btnOrderAuthorSelect]));
			w39.Position = 2;
			w39.Expand = false;
			w39.Fill = false;
			// Container child hboxOrderAuthor.Gtk.Box+BoxChild
			this.btnOrderAuthorDeselect = new global::Gtk.Button();
			this.btnOrderAuthorDeselect.CanFocus = true;
			this.btnOrderAuthorDeselect.Name = "btnOrderAuthorDeselect";
			this.btnOrderAuthorDeselect.UseUnderline = true;
			this.btnOrderAuthorDeselect.Label = global::Mono.Unix.Catalog.GetString("Искл.");
			this.hboxOrderAuthor.Add(this.btnOrderAuthorDeselect);
			global::Gtk.Box.BoxChild w40 = ((global::Gtk.Box.BoxChild)(this.hboxOrderAuthor[this.btnOrderAuthorDeselect]));
			w40.Position = 3;
			w40.Expand = false;
			w40.Fill = false;
			this.vbox1.Add(this.hboxOrderAuthor);
			global::Gtk.Box.BoxChild w41 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hboxOrderAuthor]));
			w41.Position = 8;
			w41.Expand = false;
			w41.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator();
			this.hseparator1.Name = "hseparator1";
			this.vbox1.Add(this.hseparator1);
			global::Gtk.Box.BoxChild w42 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hseparator1]));
			w42.Position = 9;
			w42.Expand = false;
			w42.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.labelTableTitle = new global::Gtk.Label();
			this.labelTableTitle.Name = "labelTableTitle";
			this.labelTableTitle.Xalign = 0F;
			this.vbox1.Add(this.labelTableTitle);
			global::Gtk.Box.BoxChild w43 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.labelTableTitle]));
			w43.Position = 10;
			w43.Expand = false;
			w43.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.searchEntityInSelectedList = new global::QSWidgetLib.SearchEntity();
			this.searchEntityInSelectedList.Events = ((global::Gdk.EventMask)(256));
			this.searchEntityInSelectedList.Name = "searchEntityInSelectedList";
			this.vbox1.Add(this.searchEntityInSelectedList);
			global::Gtk.Box.BoxChild w44 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.searchEntityInSelectedList]));
			w44.Position = 11;
			w44.Expand = false;
			w44.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(2)), ((uint)(2)), false);
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.buttonSelectAll = new global::Gtk.Button();
			this.buttonSelectAll.CanFocus = true;
			this.buttonSelectAll.Name = "buttonSelectAll";
			this.buttonSelectAll.UseUnderline = true;
			this.buttonSelectAll.Label = global::Mono.Unix.Catalog.GetString("Выбрать всех");
			this.table1.Add(this.buttonSelectAll);
			global::Gtk.Table.TableChild w45 = ((global::Gtk.Table.TableChild)(this.table1[this.buttonSelectAll]));
			w45.TopAttach = ((uint)(1));
			w45.BottomAttach = ((uint)(2));
			w45.XOptions = ((global::Gtk.AttachOptions)(4));
			w45.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.buttonUnselectAll = new global::Gtk.Button();
			this.buttonUnselectAll.CanFocus = true;
			this.buttonUnselectAll.Name = "buttonUnselectAll";
			this.buttonUnselectAll.UseUnderline = true;
			this.buttonUnselectAll.Label = global::Mono.Unix.Catalog.GetString("Снять выделение");
			this.table1.Add(this.buttonUnselectAll);
			global::Gtk.Table.TableChild w46 = ((global::Gtk.Table.TableChild)(this.table1[this.buttonUnselectAll]));
			w46.TopAttach = ((uint)(1));
			w46.BottomAttach = ((uint)(2));
			w46.LeftAttach = ((uint)(1));
			w46.RightAttach = ((uint)(2));
			w46.XOptions = ((global::Gtk.AttachOptions)(4));
			w46.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.HscrollbarPolicy = ((global::Gtk.PolicyType)(2));
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytreeviewSelectedList = new global::Gamma.GtkWidgets.yTreeView();
			this.ytreeviewSelectedList.CanFocus = true;
			this.ytreeviewSelectedList.Name = "ytreeviewSelectedList";
			this.GtkScrolledWindow.Add(this.ytreeviewSelectedList);
			this.table1.Add(this.GtkScrolledWindow);
			global::Gtk.Table.TableChild w48 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow]));
			w48.RightAttach = ((uint)(2));
			this.vbox1.Add(this.table1);
			global::Gtk.Box.BoxChild w49 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.table1]));
			w49.Position = 12;
			// Container child vbox1.Gtk.Box+BoxChild
			this.buttonCreateReport = new global::Gtk.Button();
			this.buttonCreateReport.CanFocus = true;
			this.buttonCreateReport.Name = "buttonCreateReport";
			this.buttonCreateReport.UseUnderline = true;
			this.buttonCreateReport.Label = global::Mono.Unix.Catalog.GetString("Сформировать отчет");
			this.vbox1.Add(this.buttonCreateReport);
			global::Gtk.Box.BoxChild w50 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.buttonCreateReport]));
			w50.PackType = ((global::Gtk.PackType)(1));
			w50.Position = 13;
			w50.Expand = false;
			w50.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.buttonNomTypeSelect.Clicked += new global::System.EventHandler(this.OnButtonNomTypeSelectClicked);
			this.buttonNomTypeUnselect.Clicked += new global::System.EventHandler(this.OnButtonNomTypeUnselectClicked);
			this.buttonNomenSelect.Clicked += new global::System.EventHandler(this.OnButtonNomenSelectClicked);
			this.buttonNomenUnselect.Clicked += new global::System.EventHandler(this.OnButtonNomenUnselectClicked);
			this.buttonClientSelect.Clicked += new global::System.EventHandler(this.OnButtonClientSelectClicked);
			this.buttonClientUnselect.Clicked += new global::System.EventHandler(this.OnButtonClientUnselectClicked);
			this.buttonOrgSelect.Clicked += new global::System.EventHandler(this.OnButtonOrgSelectClicked);
			this.buttonOrgUnselect.Clicked += new global::System.EventHandler(this.OnButtonOrgUnselectClicked);
			this.buttonDiscountReasonSelect.Clicked += new global::System.EventHandler(this.OnButtonDiscountReasonSelectClicked);
			this.buttonDiscountReasonUnselect.Clicked += new global::System.EventHandler(this.OnButtonDiscountReasonUnselectClicked);
			this.btnSubdivisionSelect.Clicked += new global::System.EventHandler(this.OnBtnSubdivisionSelectClicked);
			this.btnSubdivisionDeselect.Clicked += new global::System.EventHandler(this.OnBtnSubdivisionDeselectClicked);
			this.btnOrderAuthorSelect.Clicked += new global::System.EventHandler(this.OnBtnOrderAuthorSelectClicked);
			this.btnOrderAuthorDeselect.Clicked += new global::System.EventHandler(this.OnBtnOrderAuthorDeselectClicked);
			this.searchEntityInSelectedList.TextChanged += new global::System.EventHandler(this.OnSearchEntityInSelectedListTextChanged);
			this.buttonUnselectAll.Clicked += new global::System.EventHandler(this.OnButtonUnselectAllClicked);
			this.buttonSelectAll.Clicked += new global::System.EventHandler(this.OnButtonSelectAllClicked);
			this.buttonCreateReport.Clicked += new global::System.EventHandler(this.OnButtonCreateReportClicked);
		}
	}
}
