﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Gamma.Utilities;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QSOrmProject;
using Vodovoz.Domain.Goods;

namespace Vodovoz.Domain.Client
{
	[OrmSubject(
		Gender = GrammaticalGender.Masculine,
		NominativePlural = "договоры контрагента",
		Nominative = "договор",
		Genitive = " договора",
		Accusative = "договор"
	)]
	public class CounterpartyContract : BusinessObjectBase<CounterpartyContract>, IDomainObject, IValidatableObject
	{
		#region Сохраняемые поля

		private IList<AdditionalAgreement> agreements { get; set; }

		[Display(Name = "Дополнительные соглашения")]
		public virtual IList<AdditionalAgreement> AdditionalAgreements {
			get { return agreements; }
			set { agreements = value; }
		}

		GenericObservableList<AdditionalAgreement> observableAdditionalAgreements;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<AdditionalAgreement> ObservableAdditionalAgreements {
			get {
				if(observableAdditionalAgreements == null)
					observableAdditionalAgreements = new GenericObservableList<AdditionalAgreement>(AdditionalAgreements);
				return observableAdditionalAgreements;
			}
		}

		public virtual int Id { get; set; }

		int maxDelay;

		[Display(Name = "Максимальный срок отсрочки")]
		public virtual int MaxDelay {
			get { return maxDelay; }
			set { SetField(ref maxDelay, value, () => MaxDelay); }
		}

		bool isArchive;

		[Display(Name = "Архивный")]
		public virtual bool IsArchive {
			get { return isArchive; }
			set { SetField(ref isArchive, value, () => IsArchive); }
		}

		bool onCancellation;

		[Display(Name = "На расторжении")]
		public virtual bool OnCancellation {
			get { return onCancellation; }
			set { SetField(ref onCancellation, value, () => OnCancellation); }
		}

		[Display(Name = "Номер")]
		public virtual string Number { 
			get => String.Format("{0}-{1}", Counterparty.VodovozInternalId, ContractSubNumber);
			set { }
		}

		DateTime issueDate;

		[Display(Name = "Дата подписания")]
		public virtual DateTime IssueDate {
			get { return issueDate; }
			set { SetField(ref issueDate, value, () => IssueDate); }
		}

		Organization organization;

		[Required(ErrorMessage = "Организация должна быть заполнена.")]
		[Display(Name = "Организация")]
		public virtual Organization Organization {
			get { return organization; }
			set { SetField(ref organization, value, () => Organization); }
		}

		Counterparty counterparty;

		[Required(ErrorMessage = "Контрагент должен быть указан.")]
		[Display(Name = "Контрагент")]
		public virtual Counterparty Counterparty {
			get { return counterparty; }
			protected set { SetField(ref counterparty, value, () => Counterparty); }
		}

		int contractSubNumber;

		[Display(Name = "Номер договора внутренний")]
		public virtual int ContractSubNumber {
			get { return contractSubNumber; }
			set { SetField(ref contractSubNumber, value, () => ContractSubNumber); }
		}

		ContractType contractType;

		[Display(Name = "Тип договора")]
		public virtual ContractType ContractType {
			get { return contractType; }
			set { SetField(ref contractType, value, () => ContractType); }
		}

		DocTemplate contractTemplate;

		[Display(Name = "Шаблон договора")]
		public virtual DocTemplate DocumentTemplate {
			get { return contractTemplate; }
			protected set { SetField(ref contractTemplate, value, () => DocumentTemplate); }
		}

		byte[] changedTemplateFile;

		[Display(Name = "Измененный договор")]
		//[PropertyChangedAlso("FileSize")]
		public virtual byte[] ChangedTemplateFile {
			get { return changedTemplateFile; }
			set { SetField(ref changedTemplateFile, value, () => ChangedTemplateFile); }
		}

		[Display(Name = "Полный номер договора")]
		//FIXME Удалить дублирование в ContractFullNumber, протому как есть аналогичное посто Number
		public virtual string ContractFullNumber {
			get => String.Format("{0}-{1}", Counterparty.VodovozInternalId, ContractSubNumber);
			set { }
		}

		#endregion

#region Вычисляемые

		public virtual string Title { 
			get { return String.Format ("Договор №{0}-{1} от {2:d}", Counterparty.VodovozInternalId, ContractSubNumber, IssueDate); }
		}

		public virtual string TitleIn1c {
			get { return String.Format("{0}-{1} от {2:d}", Counterparty.VodovozInternalId, ContractSubNumber, IssueDate); }
		}

#endregion

		#region IValidatableObject implementation

		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!IsArchive && !OnCancellation)
			{
				var contracts = Repository.CounterpartyContractRepository.GetActiveContractsWithOrganization(UnitOfWorkFactory.CreateWithoutRoot(), Counterparty, Organization, ContractType);
				if (contracts.Any(c => c.Id != Id))
					yield return new ValidationResult(
						String.Format("У контрагента '{0}' уже есть активный договор с организацией '{1}'", Counterparty.Name, Organization.Name),
						new[] { this.GetPropertyName(o => o.Organization) });
			}
		}

		#endregion

		/// <summary>
		/// Вычисляет номер для нового договора.
		/// </summary>
		/// <returns>Максимальный внутренний номер договора у передаваемого клиента</returns>
		/// <param name="counterparty">Клиент</param>
		public static int GenerateSubNumber(Counterparty counterparty)
		{
			if(counterparty.CounterpartyContracts.Any())
				return counterparty.CounterpartyContracts.Max(c => c.ContractSubNumber) + 1;
			return 1;
		}

		//Конструкторы
		public static IUnitOfWorkGeneric<CounterpartyContract> Create (Counterparty counterparty)
		{
			var uow = UnitOfWorkFactory.CreateWithNewRoot<CounterpartyContract> ();
			uow.Root.Counterparty = counterparty;
			uow.Root.ContractSubNumber = GenerateSubNumber(counterparty);
			return uow;
		}

		#region Функции

		/// <summary>
		/// Проверяет, не создано ли уже подобное доп. соглашение.
		/// </summary>
		/// <returns><c>true</c>, если такое доп. соглашение уже существует, <c>false</c> иначе.</returns>
		/// <param name="id">Id доп. соглашения, для исключения совпадения с самим собой.</param>
		/// <param name="deliveryPoint">Точка доставки.</param>
		public virtual bool CheckWaterSalesAgreementExists (int id, DeliveryPoint deliveryPoint)
		{
			if (AdditionalAgreements == null || AdditionalAgreements.Count < 1) {
				return false;
			}
			if (deliveryPoint != null) {
				return AdditionalAgreements.Any (
					a => a.Id != id &&
					a.DeliveryPoint != null &&
					a.DeliveryPoint.Id == deliveryPoint.Id &&
					a.Type == AgreementType.WaterSales &&
					!a.IsCancelled);
			}
			return AdditionalAgreements.Any (
				a => a.Id != id &&
				a.DeliveryPoint == null &&
				a.Type == AgreementType.WaterSales &&
				!a.IsCancelled);
		}

		public virtual WaterSalesAgreement GetWaterSalesAgreement (DeliveryPoint deliveryPoint, Nomenclature nomenclature)
		{
			if (AdditionalAgreements == null || AdditionalAgreements.Count < 1) {
				return null;
			}
			AdditionalAgreement agreement = null;
			if (deliveryPoint != null) {
				agreement = AdditionalAgreements.Select(x => x.Self).OfType<WaterSalesAgreement>().FirstOrDefault (a => 
					a.DeliveryPoint != null &&
					a.DeliveryPoint.Id == deliveryPoint.Id &&
					!a.IsCancelled
					&& a.HasFixedPrice
					&& a.FixedPrices.Any(x => x.Nomenclature.Id == nomenclature.Id)
				);
				if (agreement == null)
				{
					agreement = AdditionalAgreements.FirstOrDefault(a => 
						a.DeliveryPoint != null &&
						a.DeliveryPoint.Id == deliveryPoint.Id &&
						a.Type == AgreementType.WaterSales &&
						!a.IsCancelled);
				}
			}
			if (agreement == null) {
				agreement = AdditionalAgreements.FirstOrDefault (a => 
					a.DeliveryPoint == null &&
				a.Type == AgreementType.WaterSales &&
				!a.IsCancelled);
			}
			return agreement?.Self as WaterSalesAgreement;
		}


		/// <summary>
		/// Возвращает допсоглашение на воду по текущей точке доставке
		/// </summary>
		public virtual WaterSalesAgreement GetWaterSalesAgreement(DeliveryPoint deliveryPoint)
		{
			if(AdditionalAgreements == null || AdditionalAgreements.Count < 1) {
				return null;
			}
			return AdditionalAgreements
				.Select(x => x.Self)
				.OfType<WaterSalesAgreement>()
				.Where(x => x.DeliveryPoint == deliveryPoint && !x.IsCancelled)
				.FirstOrDefault();
		}

		public virtual bool RepairAgreementExists ()
		{
			if (AdditionalAgreements == null || AdditionalAgreements.Count < 1)
				return false;
			return AdditionalAgreements.Any (a => a.Type == AgreementType.Repair && !a.IsCancelled);
		}

		/// <summary>
		/// Updates template for the contract.
		/// </summary>
		/// <returns><c>true</c>, in case of successful update, <c>false</c> if template for the contract was not found.</returns>
		/// <param name="uow">Unit of Work.</param>
		public virtual bool UpdateContractTemplate(IUnitOfWork uow)
		{
			if (Organization == null)
			{
				DocumentTemplate = null;
				ChangedTemplateFile = null;
			}
			else
			{
				var newTemplate = Repository.Client.DocTemplateRepository.GetTemplate(uow, TemplateType.Contract, Organization, ContractType);
				if(newTemplate == null) {
					DocumentTemplate = null;
					ChangedTemplateFile = null;
					return false;
				}
				if(!DomainHelper.EqualDomainObjects(newTemplate, DocumentTemplate))
				{
					DocumentTemplate = newTemplate;
					ChangedTemplateFile = null;
					return true;
				}
			}
			return false;
		}

		#endregion
	}

	public interface IContractSaved
	{
		event EventHandler<ContractSavedEventArgs> ContractSaved;
	}

	public class ContractSavedEventArgs : EventArgs
	{
		public CounterpartyContract Contract { get; private set; }

		public ContractSavedEventArgs (CounterpartyContract contract)
		{
			Contract = contract;
		}
	}
}

