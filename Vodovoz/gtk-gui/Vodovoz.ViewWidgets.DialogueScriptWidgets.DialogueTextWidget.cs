
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	public partial class DialogueTextWidget
	{
		private global::Gtk.HBox hbox1;

		private global::Gtk.Entry entryText;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ViewWidgets.DialogueScriptWidgets.DialogueTextWidget
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ViewWidgets.DialogueScriptWidgets.DialogueTextWidget";
			// Container child Vodovoz.ViewWidgets.DialogueScriptWidgets.DialogueTextWidget.Gtk.Container+ContainerChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.entryText = new global::Gtk.Entry();
			this.entryText.CanFocus = true;
			this.entryText.Name = "entryText";
			this.entryText.IsEditable = true;
			this.entryText.InvisibleChar = '•';
			this.hbox1.Add(this.entryText);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.entryText]));
			w1.Position = 0;
			this.Add(this.hbox1);
			if((this.Child != null)) {
				this.Child.ShowAll();
			}
			this.Hide();
			this.entryText.Activated += new global::System.EventHandler(this.OnEntryTextActivated);
		}
	}
}
