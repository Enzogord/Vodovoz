using System;
using QS.Dialog.Gtk;
using QS.DomainModel.UoW;
using Vodovoz.Domain.WagesCalculation;

namespace Vodovoz.Dialogs.WageCalculation
{
	[System.ComponentModel.ToolboxItem(false)]
	public partial class WageCalcParametersDlg : EntityDialogBase<WageCalcParameter>
	{
		public WageCalcParametersDlg()
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<WageCalcParameter>();
			TabName = "Новый параметр расчёта зарплаты";
			ConfigureDlg();
		}

		public WageCalcParametersDlg(int id)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<WageCalcParameter>(id);
			ConfigureDlg();
		}

		void ConfigureDlg()
		{
			enumCmbName.ItemsEnum = typeof(WageCalcParameterName);
			enumCmbName.Binding.AddBinding(Entity, e => e.Name, w => w.SelectedItem).InitializeFromSource();
			yentValue.Binding.AddBinding(Entity, e => e.ParamValue, w => w.Text).InitializeFromSource();
			ydtPeriodStart.Binding.AddBinding(Entity, e => e.PeriodStart, w => w.DateOrNull).InitializeFromSource();

			if(Entity.Id <= 0)
				ydtPeriodStart.Date = DateTime.Today.AddDays(1);

			if(Entity.Id > 0 && Entity.PeriodStart <= DateTime.Today) {
				enumCmbName.Sensitive = false;
				yentValue.Sensitive = false;
				ydtPeriodStart.Sensitive = false;
			}
		}

		public override bool Save()
		{
			throw new NotImplementedException();
		}
	}
}
