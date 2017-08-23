using System;
using Vodovoz.Domain;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueTextWidget : Gtk.Bin, IDialogueWidget
	{
		string resultString;

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public DialogueTextWidget()
		{
			this.Build();
		}

		public void Configure()
		{
			
		}

		public void SendResult()
		{
			var result = new ScriptTreeObject {
				ResultObjectType = resultString.GetType(),
				ResultObject = resultString as object
			};
			this.SubWidgetDone(this, new SubWidgetDoneEventArgs(result));
		}

		protected void OnEntryTextActivated(object sender, EventArgs e)
		{
			if(entryText.Text == resultString) 
			{
				return;
			}

			resultString = entryText.Text;
			SendResult();
		}
	}
}
