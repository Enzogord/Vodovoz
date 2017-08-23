using System;
namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueDateScheduleWidget : Gtk.Bin, IDialogueWidget
	{
		public DialogueDateScheduleWidget()
		{
			this.Build();
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;
	}
}
