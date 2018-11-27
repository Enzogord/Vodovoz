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
			get => uow;
			set {
				if(uow == value)
					return;
				uow = value;
			}
		}

		public WageParametersView()
		{
			this.Build();
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
					.AddSetter<CellRendererText>((c, n) => c.Foreground = "grey")
				.Finish();
			ytwParameters.ItemsDataSource = WageCalculationRepository.GetActualParameters(UoW);

			ebtnAddParameter.EnumItemClicked += EbtnAddParameter_EnumItemClicked;
		}


		void EbtnAddParameter_EnumItemClicked(object sender, QSOrmProject.EnumItemClickedEventArgs e)
		{
			WageCalcParameterName name = (WageCalcParameterName)e.ItemEnum;
			var dlg = new WageCalcParametersDlg();
			this.OpenSlaveTab(dlg);
		}

	}
}
