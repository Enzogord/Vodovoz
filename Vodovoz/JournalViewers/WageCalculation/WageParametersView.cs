using System;
using Gamma.ColumnConfig;
using QS.DomainModel.UoW;
using Vodovoz.Domain.WagesCalculation;
using Vodovoz.Repositories.WageCalculation;
using Gamma.Utilities;
using Gtk;
using QS.Dialog.Gtk;
using Vodovoz.Dialogs.WageCalculation;

namespace Vodovoz.JournalViewers.WageCalculation
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WageParametersView : TdiTabBase
	{
		IUnitOfWork uow;
		public IUnitOfWork UoW {
			get {
				uow = uow ?? UnitOfWorkFactory.CreateWithoutRoot();
				return uow;
			}
			set {
				if(uow == value)
					return;
				uow = value;
			}
		}

		public WageParametersView()
		{
			this.Build();
			TabName = "Журнал действующих параметров расчёта ЗП";
			ConfigureWidget();
		}

		void ConfigureWidget(){
			ebtnAddParameter.ItemsEnum = typeof(WageCalcParameterName);
			ytwParameters.ColumnsConfig = FluentColumnsConfig<WageCalcParameter>
				.Create()
				.AddColumn("Имя")
					.AddTextRenderer(p => p.Name.GetEnumTitle())
				.AddColumn("Значение")
					.AddTextRenderer(p => p.ParamValue)
				.AddColumn("Действует с")
					.AddTextRenderer(p => p.PeriodStart.ToString("d"))
				.RowCells()
					.AddSetter<CellRendererText>(
						(c, n) => {
							if(n.PeriodStart < DateTime.Today)
								c.Foreground = "grey";
						}
					)
				.Finish();

			//using(var uow = UnitOfWorkFactory.CreateWithoutRoot()) {
			//	ytwParameters.ItemsDataSource = WageCalculationRepository.GetActualParameters(uow);
			//}

			ytwParameters.ItemsDataSource = WageCalculationRepository.GetActualParameters(UoW);
			ebtnAddParameter.EnumItemClicked += EbtnAddParameter_EnumItemClicked;
			btnEdit.Clicked += BtnEdit_Clicked;
			//ytwParameters.RowActivated += 
		}

		void EbtnAddParameter_EnumItemClicked(object sender, QSOrmProject.EnumItemClickedEventArgs e)
		{
			WageCalcParameterName name = (WageCalcParameterName)e.ItemEnum;
			var dlg = new WageCalcParametersDlg(name);
			this.OpenSlaveTab(dlg);
		}

		void BtnEdit_Clicked(object sender, EventArgs e)
		{

		}

		public override void Dispose()
		{
			UoW.Dispose();
			base.Dispose();
		}
	}
}
