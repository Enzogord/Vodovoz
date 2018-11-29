using System;
using System.Collections.Generic;
using QS.DomainModel.UoW;
using Vodovoz.Domain.WagesCalculation;

namespace Vodovoz.Repositories.WageCalculation
{
	public static class WageCalculationRepository
	{
		public static IList<WageCalcParameter> GetActualParameters(IUnitOfWork uow)
		{
			var queryList = uow.Session.QueryOver<WageCalcParameter>()
							   .Where(p => p.PeriodEnd == null || p.PeriodEnd != null && p.PeriodEnd >= DateTime.Today)
							   .List();
			return queryList;
		}

		public static IList<WageCalcParameter> GetAllParametersWithName(IUnitOfWork uow, WageCalcParameterName name)
		{
			var queryList = uow.Session.QueryOver<WageCalcParameter>()
							   .Where(p => p.Name == name)
							   .List();
			return queryList;
		}
	}
}
