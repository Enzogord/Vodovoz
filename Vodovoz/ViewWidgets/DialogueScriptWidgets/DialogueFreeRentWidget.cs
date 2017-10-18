using System;
using QSOrmProject;
using Vodovoz.Domain;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueFreeRentWidget : Gtk.Bin, IDialogueWidget
	{
		string resultString;
		IUnitOfWork UoW;

		public DialogueFreeRentWidget()
		{
			this.Build();
			this.UoW = UoW;
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

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
 
		}

		//void Tempo()
		//{
			//var allready = Repository.RentPackageRepository.GetFreeRentPackage(UoW, EquipmentType);
			//if(allready != null && allready.Id != Id) {
			//	yield return new ValidationResult(
			//		String.Format("Условия для оборудования {0} уже существуют.", EquipmentType.Name),
			//		new[] { this.GetPropertyName(o => o.EquipmentType) });
			//}
		//}
	}
}
