using System;
namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueCheckEmptyBottlesWidget : Gtk.Bin, IDialogueWidget
	{
		public DialogueCheckEmptyBottlesWidget()
		{
			this.Build();
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;
	}
}
