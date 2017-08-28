using System;
using QSOrmProject;
using Vodovoz.Domain;
using Vodovoz.Domain.Logistic;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueDateScheduleWidget : Gtk.Bin, IDialogueWidget
	{
		IUnitOfWork UoW;
		DateSchedule resultDateSchedule = new DateSchedule();
		Boolean dateSet = false, scheduleSet = false;

		public DialogueDateScheduleWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
			this.Configure();
		}

		public void Configure()
		{
			referenceSchedule.SubjectType = typeof(DeliverySchedule);
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void RefreshDependency(ScriptTreeObject ste)
		{
			
		}

		public void SendResults()
		{
			if(!dateSet 
			   || !scheduleSet 
			   || resultDateSchedule.Date == null 
			   || resultDateSchedule.Schedule == null)
			{
				return;
			}

			var result = new ScriptTreeObject {
				ResultObjectType = resultDateSchedule.GetType(),
				ResultObject = resultDateSchedule as object
			};
			this.SubWidgetDone(this, new SubWidgetDoneEventArgs(result));
		}

		protected void OnReferenceScheduleChanged(object sender, EventArgs e)
		{
			resultDateSchedule.Schedule = (DeliverySchedule)referenceSchedule.Subject;
			scheduleSet = true;
			SendResults();
		}

		protected void OnPickerDateDateChanged(object sender, EventArgs e)
		{
			resultDateSchedule.Date = pickerDate.Date;
			dateSet = true;
			SendResults();
		}
	}
}
