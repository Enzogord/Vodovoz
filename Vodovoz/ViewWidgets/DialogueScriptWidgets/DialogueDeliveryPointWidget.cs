using System;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueDeliveryPointWidget : Gtk.Bin, IDialogueWidget
	{
		DeliveryPoint resultDeliveryPoint;

		public DialogueDeliveryPointWidget()
		{
			this.Build();
		}

		public void Configure()
		{
			referenceDeliveryPoint.RepresentationModel = new ViewModel.DeliveryPointsVM();
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void SendResult()
		{
			var result = new ScriptTreeObject {
				ResultObjectType = resultDeliveryPoint.GetType(),
				ResultObject = resultDeliveryPoint as object
			};
			this.SubWidgetDone(this, new SubWidgetDoneEventArgs(result));
		}

		protected void OnReferenceDeliveryPointChanged(object sender, EventArgs e)
		{
			if(referenceDeliveryPoint.GetSubject<DeliveryPoint>() == resultDeliveryPoint)
			{
				return;
			}

			resultDeliveryPoint = referenceDeliveryPoint.GetSubject<DeliveryPoint>();
			SendResult();
		}
	}
}
