using System;
using System.Linq;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using Gtk;
using QS.Dialog.Gtk;
using QS.DomainModel.UoW;
using Vodovoz.Domain.WagesCalculation;
using Vodovoz.Repositories.WageCalculation;

namespace Vodovoz.Dialogs.WageCalculation
{
	[System.ComponentModel.ToolboxItem(false)]
	public partial class WageCalcParametersDlg : EntityDialogBase<WageCalcParameter>
	{
		WageCalcParameter actualParameter;
		public WageCalcParametersDlg(WageCalcParameterName name)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<WageCalcParameter>();
			TabName = "Новый параметр расчёта зарплаты";
			Entity.Name = name;
			ConfigureDlg();
		}

		public WageCalcParametersDlg(int id)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<WageCalcParameter>(id);
			ConfigureDlg();
		}

		public WageCalcParametersDlg(WageCalcParameter parameter) : this(parameter.Id) { }

		void ConfigureDlg()
		{
			//удалить, после того как починим Session.IsDirty()
			HasChanges = true;
			UoW.CanCheckIfDirty = false;

			//yentryName.Binding.AddBinding(Entity, e => e.ParameterName, w => w.Text).InitializeFromSource();
			yentryName.Text = Entity.Name.GetEnumTitle();
			spnValue.Binding.AddBinding(Entity, e => e.ParamValue, w => w.ValueAsDecimal).InitializeFromSource();
			ydtPeriodStart.IsEditable = true;
			ydtPeriodStart.Binding.AddBinding(Entity, e => e.PeriodStart, w => w.DateOrNull).InitializeFromSource();
			ydtPeriodStart.DateChangedByUser += YdtPeriodStart_DateChangedByUser;

			ytwAnotherParameters.ColumnsConfig = FluentColumnsConfig<WageCalcParameter>
				.Create()
				.AddColumn("Значение")
					.AddTextRenderer(p => p.ParamValue.ToString())
				.AddColumn("Период действия")
					.AddTextRenderer(
						p => p.PeriodEnd.HasValue
								? String.Format(
									"с {0} по {1}",
									p.PeriodStart.ToString("d"),
									p.PeriodEnd.Value.ToString("d")
								)
								: String.Format(
									"с {0}",
									p.PeriodStart.ToString("d")
								)
					)
				.RowCells()
					.AddSetter<CellRendererText>((c, n) => { c.Foreground = n.IsForFutureUse ? "blue" : "grey"; })
				.Finish();
			ytwAnotherParameters.ItemsDataSource = WageCalculationRepository.GetAllParametersWithName(UoW, Entity.Name)
																			.Where(p => p != Entity)
																			.ToList();

			if(Entity.Id <= 0)
				ydtPeriodStart.Date = DateTime.Today.AddDays(1);

			if(Entity.Id > 0 && Entity.PeriodStart <= DateTime.Today) {
				yentryName.Sensitive = false;
				spnValue.Sensitive = false;
				ydtPeriodStart.Sensitive = false;
				HasChanges = false;
			}

			if(!Entity.IsForFutureUse) {
				tblCurrentParameter.Sensitive = false;
				MessageDialogHelper.RunInfoDialog("Параметр доступен только для просмотра, т.к. является действующим.\nЕсли необходимо изменить данный параметр, то создайте новый, который начнёт действовать не раньше завтрашнего дня.");
			}
		}

		void YdtPeriodStart_DateChangedByUser(object sender, EventArgs e)
		{
			//устанавливаем действующему параметру конец периода действия
			if(actualParameter == null)
				actualParameter = WageCalculationRepository.GetAllParametersWithName(UoW, Entity.Name)
													   .FirstOrDefault(p => p.IsActual);
			if(actualParameter != null)
				actualParameter.PeriodEnd = Entity.PeriodStart.AddDays(-1);
		}

		public override bool Save()
		{
			var valid = new QSValidation.QSValidator<WageCalcParameter>(Entity);
			if(valid.RunDlgIfNotValid((Gtk.Window)this.Toplevel))
				return false;

			//сохраняем действующему параметру конец периода действия
			if(actualParameter != null)
				UoW.Save(actualParameter);

			UoW.Save(Entity);
			UoW.Commit();
			return true;
		}
	}
}
