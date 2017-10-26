using System;
using QSOrmProject;
using Vodovoz.Domain;
using Vodovoz.Repository;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueWelcomeWidget : Gtk.Bin, IDialogueWidget
	{
		string resultString;
		IUnitOfWork UoW;

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public DialogueWelcomeWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
		}

		public void Configure()
		{
			var employeeVar = EmployeeRepository.ActiveEmployeeOrderedQuery();
			label1.LabelProp = employeeVar.ToString();
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
 
		}

		public void RefreshDependency(ScriptTreeObject ste)
		{

		}
	}
}
