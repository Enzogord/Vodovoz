using FluentNHibernate.Mapping;
using Vodovoz.Domain.WagesCalculation;

namespace Vodovoz.HibernateMapping.WageCalculation
{
	public class WageCalcParameterMap : ClassMap<WageCalcParameter>
	{
		public WageCalcParameterMap()
		{
			Table("wage_calc_parameters");

			Id(x => x.Id).Column("id").GeneratedBy.Native();

			Map(x => x.Name).Column("name");
			Map(x => x.ParamValue).Column("value");
			Map(x => x.PeriodStart).Column("period_start");
			Map(x => x.PeriodEnd).Column("period_end");
		}
	}
}