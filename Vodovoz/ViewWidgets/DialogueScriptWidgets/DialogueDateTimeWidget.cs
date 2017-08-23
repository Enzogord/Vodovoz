using System;
using Vodovoz.Domain;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueDateTimeWidget : Gtk.Bin, IDialogueWidget
	{
		DateTime resultDateTime;

		public DialogueDateTimeWidget()
		{
			this.Build();
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void SendResult()
		{
			var result = new ScriptTreeObject {
				ResultObjectType = resultDateTime.GetType(),
				ResultObject = resultDateTime as object
			};
			this.SubWidgetDone(this, new SubWidgetDoneEventArgs(result));
		}
	}
}
