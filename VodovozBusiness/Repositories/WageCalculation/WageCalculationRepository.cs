using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using FluentNHibernate.Utils;
using Vodovoz.Domain.WagesCalculation;

namespace Vodovoz.Repositories.WageCalculation
{
	public static class WageCalculationRepository
	{
		public static IList<WageCalcParameter> GetActualParameters(IUnitOfWork uow)
		{
			var queryList = uow.Session.QueryOver<WageCalcParameter>()
							   .Where(p => p.PeriodStart >= DateTime.Today)
			                   .List()
			                   //.FirstOrDefault()
			                   ;
			return queryList;
		}
	}
}
