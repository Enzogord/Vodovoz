using Gamma.ColumnConfig;
using Gamma.Utilities;
using Gtk;
using QS.DomainModel.UoW;
using QSOrmProject.RepresentationModel;
using Vodovoz.Domain.WagesCalculation;
using Vodovoz.Repositories.WageCalculation;

namespace Vodovoz.Representations
{
	public class WageCalcParameterVM : RepresentationModelEntityBase<WageCalcParameter, WageCalcParameter>
	{
		public WageCalcParameterVM(IUnitOfWork uow)
		{
			this.UoW = uow;
		}

		public WageCalcParameterVM() : this(UnitOfWorkFactory.CreateWithoutRoot()) { }

		#region IRepresentationModel implementation

		IColumnsConfig columnsConfig = FluentColumnsConfig<WageCalcParameter>
			.Create()
			.AddColumn("Имя")
				.AddTextRenderer(p => p.Name.GetEnumTitle())
			.AddColumn("Значение")
				.AddTextRenderer(p => p.ParamValue.ToString())
			.AddColumn("Действует с")
				.AddTextRenderer(p => p.PeriodStart.ToString("d"))
			.RowCells()
				.AddSetter<CellRendererText>((c, n) => c.Foreground = n.IsForFutureUse ? "blue" : "black")
			.Finish();

		public override IColumnsConfig ColumnsConfig => columnsConfig;

		public override void UpdateNodes() => SetItemsSource(WageCalculationRepository.GetActualParameters(UoW));

		#endregion

		#region implemented abstract members of RepresentationModelBase

		protected override bool NeedUpdateFunc(WageCalcParameter updatedSubject) => true;

		#endregion
	}
}
