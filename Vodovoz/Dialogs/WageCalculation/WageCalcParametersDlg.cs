using System;
using QS.Dialog.Gtk;
using QS.DomainModel.UoW;
using Vodovoz.Domain.WagesCalculation;

namespace Vodovoz.Dialogs.WageCalculation
{
	[System.ComponentModel.ToolboxItem(false)]
	public partial class WageCalcParametersDlg : EntityDialogBase<WageCalcParameter>
	{
		WageCalcParameterName paramName = WageCalcParameterName.PhoneServiceCompensationRate;

		public WageCalcParametersDlg(WageCalcParameterName name)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<WageCalcParameter>();
			paramName = name;
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
			//удалить, после того как починим Session.IsDirty()
			HasChanges = false;
			UoW.CanCheckIfDirty = false;

			enumCmbName.ItemsEnum = typeof(WageCalcParameterName);
			enumCmbName.Binding.AddBinding(Entity, e => e.Name, w => w.SelectedItem).InitializeFromSource();
			enumCmbName.SelectedItem = paramName;
			yentValue.Binding.AddBinding(Entity, e => e.ParamValue, w => w.Text).InitializeFromSource();
			ydtPeriodStart.IsEditable = true;
			ydtPeriodStart.Binding.AddBinding(Entity, e => e.PeriodStart, w => w.DateOrNull).InitializeFromSource();

			if(Entity.Id <= 0)
				ydtPeriodStart.Date = DateTime.Today.AddDays(1);

			if(Entity.Id > 0 && Entity.PeriodStart <= DateTime.Today) {
				enumCmbName.Sensitive = false;
				yentValue.Sensitive = false;
				ydtPeriodStart.Sensitive = false;
			}
			tblCurrentParameter.Sensitive = Entity.IsEditable;
		}

		public override bool Save()
		{
			UoW.Save(Entity);
			UoW.Commit();
			return true;
		}
	}
}
