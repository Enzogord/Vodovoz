using System;
using QSOrmProject;
using QSTDI;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueleaveAndPrintWidgets : Gtk.Bin, IDialogueWidget
	{
		DeliveryPoint resultDeliveryPoint;
		Counterparty dependencyCounterparty = new Counterparty();
		DateSchedule resultDateSchedule = new DateSchedule();
		IUnitOfWork UoW;

		public DialogueleaveAndPrintWidgets(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void RefreshDependency(ScriptTreeObject ste)
		{
			var dependency = GetDependency(ste);
			labelDate.LabelProp = dependency.Date.ToString();
		}

		protected void OnButtonPrintClicked(object sender, EventArgs e)
		{
			ITdiTab mytab = TdiHelper.FindMyTab(this);
			if(mytab == null)
				return;
			
			//var document = Additions.Logistic.PrintRouteListHelper.GetRDLRouteList(UoW, Entity);
			//mytab.TabParent.OpenTab(
			//TdiTabBase.GenerateHashName<QSReport.ReportViewDlg>(),
			//() => new QSReport.ReportViewDlg(document));


			//var allList = treeDocuments.GetSelectedObjects().Cast<OrderDocument>().ToList();
			//if(allList.Count <= 0)
			//	return;

			//allList.OfType<ITemplateOdtDocument>().ToList().ForEach(x => x.PrepareTemplate(UoW));

			//string whatToPrint = allList.Count > 1
			//	? "документов"
			//	: "документа \"" + allList.First().Type.GetEnumTitle() + "\"";
			//if(UoWGeneric.HasChanges && CommonDialogs.SaveBeforePrint(typeof(Order), whatToPrint))
			//	UoWGeneric.Save();

			//var selectedPrintableRDLDocuments = treeDocuments.GetSelectedObjects().Cast<OrderDocument>()
			//	.Where(doc => doc.PrintType == PrinterType.RDL).ToList();
			//if(selectedPrintableRDLDocuments.Count > 0) {
			//	DocumentPrinter.PrintAll(selectedPrintableRDLDocuments);
			//}

			//var selectedPrintableODTDocuments = treeDocuments.GetSelectedObjects()
			//	.OfType<ITemplatePrntDoc>().ToList();
			//if(selectedPrintableODTDocuments.Count > 0) {
			//	TemplatePrinter.PrintAll(selectedPrintableODTDocuments);
			//}

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

		//protected void OnReferenceDeliveryPointChanged(object sender, EventArgs e)
		//{
		//	//if(referenceDeliveryPoint.GetSubject<DeliveryPoint>() == resultDeliveryPoint) {
		//	//	return;
		//	//}

		//	//resultDeliveryPoint = referenceDeliveryPoint.GetSubject<DeliveryPoint>();
		//	SendResult();
		//}

		DateSchedule GetDependency(ScriptTreeObject dependencyObject)
		{
			if(dependencyObject != null && dependencyObject.ResultObject is DateSchedule) {
				var dependency = dependencyObject.ResultObject as DateSchedule;
				return dependency;
			}

			return null;
		}

	}
}
