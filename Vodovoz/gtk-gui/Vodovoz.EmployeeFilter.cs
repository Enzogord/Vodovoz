
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class EmployeeFilter
	{
		private global::Gtk.Table table1;

		private global::Gtk.CheckButton checkFired;

		private global::Gamma.Widgets.yEnumComboBox enumcomboCategory;

		private global::Gtk.Label label1;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.EmployeeFilter
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.EmployeeFilter";
			// Container child Vodovoz.EmployeeFilter.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table(((uint)(1)), ((uint)(3)), false);
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.checkFired = new global::Gtk.CheckButton();
			this.checkFired.CanFocus = true;
			this.checkFired.Name = "checkFired";
			this.checkFired.Label = global::Mono.Unix.Catalog.GetString("Показывать уволенных");
			this.checkFired.DrawIndicator = true;
			this.checkFired.UseUnderline = true;
			this.table1.Add(this.checkFired);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.checkFired]));
			w1.LeftAttach = ((uint)(2));
			w1.RightAttach = ((uint)(3));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.enumcomboCategory = new global::Gamma.Widgets.yEnumComboBox();
			this.enumcomboCategory.Name = "enumcomboCategory";
			this.enumcomboCategory.ShowSpecialStateAll = true;
			this.enumcomboCategory.ShowSpecialStateNot = false;
			this.enumcomboCategory.UseShortTitle = false;
			this.enumcomboCategory.DefaultFirst = false;
			this.table1.Add(this.enumcomboCategory);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.enumcomboCategory]));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Тип:");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			this.Add(this.table1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.enumcomboCategory.Changed += new global::System.EventHandler(this.OnEnumcomboCategoryChanged);
			this.checkFired.Toggled += new global::System.EventHandler(this.OnCheckFiredToggled);
		}
	}
}
