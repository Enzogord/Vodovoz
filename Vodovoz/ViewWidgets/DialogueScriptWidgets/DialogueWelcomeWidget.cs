using System;
using QSOrmProject;
using Vodovoz.Domain;
using Vodovoz.Repository;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueWelcomeWidget : Gtk.Bin, IDialogueWidget
	{
		string correctionString;
		string resultString;
		IUnitOfWork UoW;

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public DialogueWelcomeWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
			Configure();
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

		public void RefreshDependency(ScriptTreeObject ste)
		{
			correctionString = EmployeeRepository.GetEmployeeForCurrentUser(UoW).Name;
			CorrectText();
		}

		public void CorrectText()
		{
			string[] corrections = { correctionString };
			this.TextCorrectionsPresent?.Invoke(this, new TextCorrectionsPresentEventArgs(corrections));
		}
	}
}
