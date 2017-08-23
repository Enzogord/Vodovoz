using System;
namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueOrderRepeatWidget : Gtk.Bin, IDialogueWidget
	{
		public DialogueOrderRepeatWidget()
		{
			this.Build();
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;
	}
}
