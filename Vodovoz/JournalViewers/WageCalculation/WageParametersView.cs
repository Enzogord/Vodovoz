using System;
using System.Linq;
using QS.Dialog.Gtk;
using QS.DomainModel.UoW;
using Vodovoz.Dialogs.WageCalculation;
using Vodovoz.Domain.WagesCalculation;
using Vodovoz.Repositories.WageCalculation;
using Vodovoz.Representations;

namespace Vodovoz.JournalViewers.WageCalculation
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WageParametersView : TdiTabBase
	{
		WageCalcParameter selectedParameter;
		WageCalcParameterVM wageCalcParameterModel;

		IUnitOfWork uow;
		public IUnitOfWork UoW {
			get => uow;
			set {
				if(uow == value)
					return;
				uow = value;
				wageCalcParameterModel = new WageCalcParameterVM(value);
				repTwParameters.RepresentationModel = wageCalcParameterModel;
			}
		}

		public WageParametersView()
		{
			this.Build();
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			TabName = "Журнал действующих параметров расчёта ЗП";
			ConfigureWidget();
		}

		void ConfigureWidget()
		{
			ebtnAddParameter.ItemsEnum = typeof(WageCalcParameterName);
			SetControlsAcessibility();

			ebtnAddParameter.EnumItemClicked += EbtnAddParameter_EnumItemClicked;
			btnEdit.Clicked += BtnEdit_Clicked;
			repTwParameters.RowActivated += BtnEdit_Clicked;
			repTwParameters.Selection.Changed += YtwParameters_SelectionChanged;
			btnDelete.Clicked += BtnDelete_Clicked;
			btnRefresh.Clicked += (sender, e) => Refresh();
		}

		void YtwParameters_SelectionChanged(object sender, EventArgs e)
		{
			if(repTwParameters.GetSelectedObject() is WageCalcParameter)
				selectedParameter = repTwParameters.GetSelectedObject<WageCalcParameter>();
			SetControlsAcessibility();
		}

		void EbtnAddParameter_EnumItemClicked(object sender, QSOrmProject.EnumItemClickedEventArgs e)
		{
			WageCalcParameterName name = (WageCalcParameterName)e.ItemEnum;

			var anyFutureParams = WageCalculationRepository.GetAllParametersWithName(UoW, name)
														   .Any(p => p.IsForFutureUse);
			if(anyFutureParams) {
				MessageDialogHelper.RunWarningDialog("Нельзя содать более одного будущего параметра одного типа. Отредактируйте уже имеющийся.");
				return;
			}

			var dlg = new WageCalcParametersDlg(name);
			dlg.CloseTab += (s, ea) => Refresh();
			this.OpenSlaveTab(dlg);
		}

		void BtnEdit_Clicked(object sender, EventArgs e)
		{
			if(selectedParameter != null) {
				var dlg = new WageCalcParametersDlg(selectedParameter);
				TabParent.OpenTab(
					DialogHelper.GenerateDialogHashName<WageCalcParameter>(selectedParameter.Id),
					() => dlg
				);
				//dlg.CloseTab += (s, ea) => Refresh();
			}
		}

		void Refresh() => repTwParameters.RepresentationModel.UpdateNodes();

		void BtnDelete_Clicked(object sender, EventArgs e)
		{
			if(selectedParameter != null && selectedParameter.IsForFutureUse) {
				var actualParameter = WageCalculationRepository.GetAllParametersWithName(UoW, selectedParameter.Name)
															   .FirstOrDefault(p => p.IsActual);

				if(actualParameter != null) {
					UoW.Session.Refresh(actualParameter);
					actualParameter.PeriodEnd = null;
				}
				UoW.Delete(selectedParameter);
				UoW.Commit();
				Refresh();
			}
		}

		void SetControlsAcessibility()
		{
			btnEdit.Sensitive = btnDelete.Sensitive = selectedParameter != null;

			foreach(WageCalcParameterName param in Enum.GetValues(typeof(WageCalcParameterName))) {
				switch(param) {
					case WageCalcParameterName.EmptyBottleRate:
						ebtnAddParameter.SetSensitive(param, false);
						break;
					default:
						break;
				}
			}
		}

		//public override void Dispose()
		//{
		//	UoW.Dispose();
		//	base.Dispose();
		//}
	}
}
