﻿using System;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QSOrmProject;

namespace Vodovoz.Domain.Client
{

	[OrmSubject (Gender = GrammaticalGender.Neuter,
		NominativePlural = "доп. соглашения сервиса",
		Nominative = "доп. соглашение сервиса")]
	public class RepairAgreement : AdditionalAgreement
	{
		public static IUnitOfWorkGeneric<RepairAgreement> Create (CounterpartyContract contract)
		{
			var uow = UnitOfWorkFactory.CreateWithNewRoot<RepairAgreement> ();
			uow.Root.Contract = uow.GetById<CounterpartyContract>(contract.Id);
			uow.Root.DeliveryPoint = null;
			uow.Root.AgreementNumber = AdditionalAgreement.GetNumberWithType (uow.Root.Contract, AgreementType.Repair);
			return uow;
		}
	}
	
}
