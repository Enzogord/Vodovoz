using System;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using Gamma.Utilities;

namespace Vodovoz.Domain.WagesCalculation
{

	[Appellative(Gender = GrammaticalGender.Masculine,
		NominativePlural = "параметры расчёта заработной платы",
		Nominative = "параметр расчёта заработной платы")]
	public class WageCalcParameter : PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		WageCalcParameterName name;
		[Display(Name = "Имя параметра")]
		public virtual WageCalcParameterName Name {
			get => name;
			protected set => SetField(ref name, value, () => Name);
		}

		string paramValue;
		[Display(Name = "Значение")]
		public virtual string ParamValue {
			get => paramValue;
			set => SetField(ref paramValue, value, () => ParamValue);
		}

		DateTime periodStart;
		[Display(Name = "Время начала действия параметра")]
		public virtual DateTime PeriodStart {
			get => periodStart;
			set => SetField(ref periodStart, value, () => PeriodStart);
		}

		DateTime? periodEnd;
		[Display(Name = "Время окончания действия параметра")]
		public virtual DateTime? PeriodEnd {
			get => periodEnd;
			set => SetField(ref periodEnd, value, () => PeriodEnd);
		}

		#endregion

		public override string ToString()
		{
			string title = "Неизвестное состояние параметра";
			if(PeriodStart > DateTime.Today)
				title = String.Format(
					"\"{0}\" {1} с {2}",
					Name.GetEnumTitle(), 
					"будет действовать",
					PeriodStart.ToString("d")
				);
			else if(PeriodStart <= DateTime.Today && !PeriodEnd.HasValue)
				title = String.Format(
					"\"{0}\" {1} с {2}",
					Name.GetEnumTitle(),
					"действует",
					PeriodStart.ToString("d")
				);
			else if(PeriodEnd.HasValue)
				title = String.Format(
					"\"{0}\" {1} с {2} по {3}",
					Name.GetEnumTitle(),
					"действовал",
					PeriodStart.ToString("d"),
					PeriodEnd.Value.ToString("d")
				);

			return title;
		}

		public virtual bool IsEditable => PeriodStart > DateTime.Today && !PeriodEnd.HasValue;

	}

	public enum WageCalcParameterName
	{
		[Display(Name = "Оплата мобильной связи")]
		PhoneServiceCompensationRate,
		[Display(Name = "Ставка за доставку полной бутыли")]
		FullBottleRate,
		[Display(Name = "Ставка за доставку маленькой бутыли")]
		SmallBottleRate,
		[Display(Name = "Ставка за доставку пустой бутыли")]
		EmptyBottleRate,
		[Display(Name = "Ставка за доставку кулера")]
		CoolerRate,
		[Display(Name = "Ставка за адрес")]
		PaymentPerAddress,
		[Display(Name = "Ставка за большой заказ с полными бутылями")]
		LargeOrderFullBottleRate,
		[Display(Name = "Ставка за большой заказ с пустыми бутылями")]
		LargeOrderEmptyBottleRate,
		[Display(Name = "Минимальное кол-во бутылей в большом заказе")]
		LargeOrderMinimumBottles,
		[Display(Name = "Ставка за малый заказ с полными бутылями")]
		SmallFullBottleRate,
		[Display(Name = "Ставка за расторжение договора")]
		ContractCancelationRate,
		[Display(Name = "Оплата при расторжении")]
		PaymentWithRast
	}

	public class WageCalcParameterNameStringType : NHibernate.Type.EnumStringType
	{
		public WageCalcParameterNameStringType() : base(typeof(WageCalcParameterName)) { }
	}
}