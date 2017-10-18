using System;
using QSOrmProject;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueleaveAndPrintWidgets : Gtk.Bin, IDialogueWidget
	{
		DeliveryPoint resultDeliveryPoint;
		Counterparty dependencyCounterparty = new Counterparty();
		IUnitOfWork UoW;

		public DialogueleaveAndPrintWidgets(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		//public void RefreshDependency(ScriptTreeObject ste)
		//{
		//	//dependencyCounterparty = GetDependency(ste);
		//	//referenceDeliveryPoint.RepresentationModel = new ViewModel.ClientDeliveryPointsVM(UoW, dependencyCounterparty);
		//	//referenceDeliveryPoint.Subject = null;
		//}

		//public void SendResult()
		//{
		//	//if(resultDeliveryPoint == null) {
		//	//	this.SubWidgetDone(this, new SubWidgetDoneEventArgs(null));
		//	//	return;
		//	//}

		//	//var result = new ScriptTreeObject {
		//	//	ResultObjectType = resultDeliveryPoint.GetType(),
		//	//	ResultObject = resultDeliveryPoint as object
		//	//};
		//	//this.SubWidgetDone?.Invoke(this, new SubWidgetDoneEventArgs(result));
		//}

		protected void OnButtonPrintClicked(object sender, EventArgs e)
		{

			//var document = Additions.Logistic.PrintRouteListHelper.GetRDLRouteList(UoW, Entity);
			//this.TabParent.OpenTab(
			//QSTDI.TdiTabBase.GenerateHashName<QSReport.ReportViewDlg>(),
			//() => new QSReport.ReportViewDlg(document));

		}

		public void SendResult()
		{
			if(resultDeliveryPoint == null) {
				this.SubWidgetDone(this, new SubWidgetDoneEventArgs(null));
				return;
			}

			var result = new ScriptTreeObject {
				ResultObjectType = resultDeliveryPoint.GetType(),
				ResultObject = resultDeliveryPoint as object
			};
			this.SubWidgetDone?.Invoke(this, new SubWidgetDoneEventArgs(result));
		}

		protected void OnReferenceDeliveryPointChanged(object sender, EventArgs e)
		{
			//if(referenceDeliveryPoint.GetSubject<DeliveryPoint>() == resultDeliveryPoint) {
			//	return;
			//}

			//resultDeliveryPoint = referenceDeliveryPoint.GetSubject<DeliveryPoint>();
			SendResult();
		}

		Counterparty GetDependency(ScriptTreeObject dependencyObject)
		{
			if(dependencyObject != null && dependencyObject.ResultObject is Counterparty) {
				var dependency = dependencyObject.ResultObject as Counterparty;
				return dependency;
			}

			return null;
		}

		public void RefreshDependency(ScriptTreeObject ste)
		{
			dependencyCounterparty = GetDependency(ste);
			//referenceDeliveryPoint.RepresentationModel = new ViewModel.ClientDeliveryPointsVM(UoW, dependencyCounterparty);
			//referenceDeliveryPoint.Subject = null;
		}
	}
}
