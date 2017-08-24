using System;
using QSOrmProject;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueCounterpartyWidget : Gtk.Bin, IDialogueWidget
	{
		Counterparty resultCounterparty;
		IUnitOfWork UoW;
		
		public DialogueCounterpartyWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
			Configure();
		}

		public void Configure()
		{
			referenceClient.RepresentationModel = new ViewModel.CounterpartyVM();
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void SendResult()
		{
			var result = new ScriptTreeObject {
				ResultObjectType = resultCounterparty.GetType(),
				ResultObject = resultCounterparty as object
			};
			this.SubWidgetDone(this, new SubWidgetDoneEventArgs(result));
		}

		protected void OnReferenceClientChanged(object sender, EventArgs e)
		{
			if(referenceClient.GetSubject<Counterparty>() == resultCounterparty)
			{
				return;
			}

			resultCounterparty = referenceClient.GetSubject<Counterparty>();
			SendResult();
		}

		public void RefreshDependency(ScriptTreeObject ste)
		{
			
		}
	}
}
