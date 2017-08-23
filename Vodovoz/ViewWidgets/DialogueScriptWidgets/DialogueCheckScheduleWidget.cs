using System;
namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueCheckScheduleWidget : Gtk.Bin, IDialogueWidget
	{
		public DialogueCheckScheduleWidget()
		{
			this.Build();
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;
	}
}
