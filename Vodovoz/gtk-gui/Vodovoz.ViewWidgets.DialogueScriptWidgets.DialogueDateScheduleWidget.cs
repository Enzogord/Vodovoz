
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	public partial class DialogueDateScheduleWidget
	{
		private global::Gtk.HBox hbox1;

		private global::Gamma.Widgets.yDatePicker pickerDate;

		private global::Gamma.Widgets.yEntryReference referenceSchedule;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ViewWidgets.DialogueScriptWidgets.DialogueDateScheduleWidget
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ViewWidgets.DialogueScriptWidgets.DialogueDateScheduleWidget";
			// Container child Vodovoz.ViewWidgets.DialogueScriptWidgets.DialogueDateScheduleWidget.Gtk.Container+ContainerChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.pickerDate = new global::Gamma.Widgets.yDatePicker();
			this.pickerDate.Events = ((global::Gdk.EventMask)(256));
			this.pickerDate.Name = "pickerDate";
			this.pickerDate.WithTime = false;
			this.pickerDate.Date = new global::System.DateTime(0);
			this.pickerDate.IsEditable = true;
			this.pickerDate.AutoSeparation = true;
			this.hbox1.Add(this.pickerDate);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.pickerDate]));
			w1.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.referenceSchedule = new global::Gamma.Widgets.yEntryReference();
			this.referenceSchedule.Events = ((global::Gdk.EventMask)(256));
			this.referenceSchedule.Name = "referenceSchedule";
			this.referenceSchedule.DisplayFields = new string[] {
					"Name"};
			this.referenceSchedule.DisplayFormatString = "{0}";
			this.hbox1.Add(this.referenceSchedule);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.referenceSchedule]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			this.Add(this.hbox1);
			if((this.Child != null)) {
				this.Child.ShowAll();
			}
			this.Hide();
			this.pickerDate.DateChanged += new global::System.EventHandler(this.OnPickerDateDateChanged);
			this.referenceSchedule.Changed += new global::System.EventHandler(this.OnReferenceScheduleChanged);
		}
	}
}