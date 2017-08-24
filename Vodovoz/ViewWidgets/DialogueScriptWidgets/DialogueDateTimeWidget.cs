using System;
using QSOrmProject;
using Vodovoz.Domain;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueDateTimeWidget : Gtk.Bin, IDialogueWidget
	{
		DateTime resultDateTime;
		IUnitOfWork UoW;

		public DialogueDateTimeWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void RefreshDependency(ScriptTreeObject ste)
		{
			
		}

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
