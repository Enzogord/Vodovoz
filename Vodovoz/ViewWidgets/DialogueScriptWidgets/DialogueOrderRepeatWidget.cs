using System;
using QSOrmProject;
using Vodovoz.Domain;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueOrderRepeatWidget : Gtk.Bin, IDialogueWidget
	{
		IUnitOfWork UoW;

		public DialogueOrderRepeatWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void RefreshDependency(ScriptTreeObject ste)
		{
			
		}
	}
}
