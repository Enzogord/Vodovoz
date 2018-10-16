﻿using QS.DomainModel.UoW;
using QSBanks;
using Vodovoz.Domain.Accounting;
using Vodovoz.Domain.Client;

namespace Vodovoz.Repository
{
	public static class AccountIncomeRepository
	{
		public static bool AccountIncomeExists (IUnitOfWork uow, int year, int number, string counterpartyInn, string accountNumber)
		{
			Account accountAlias = null;
			Counterparty counterpartyAlias = null;

			var income = uow.Session.QueryOver<AccountIncome> ()
				.JoinAlias (ai => ai.CounterpartyAccount, () => accountAlias)
				.JoinAlias (ai => ai.Counterparty, () => counterpartyAlias)
				.Where (ai => ai.Date.Year == year &&
			             ai.Number == number &&
			             accountAlias.Number == accountNumber &&
			             counterpartyAlias.INN == counterpartyInn)
				.SingleOrDefault<AccountIncome> ();
			return income != null;
		}
	}
}

