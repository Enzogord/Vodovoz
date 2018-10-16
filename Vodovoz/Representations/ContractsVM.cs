﻿using System;
using Gamma.ColumnConfig;
using Gtk;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QSOrmProject.RepresentationModel;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;

namespace Vodovoz.ViewModel
{
	public class ContractsVM : RepresentationModelEntitySubscribingBase<CounterpartyContract, ContractsVMNode>, IRepresentationModelWithParent
	{
		public IUnitOfWorkGeneric<Counterparty> CounterpartyUoW {
			get {
				return UoW as IUnitOfWorkGeneric<Counterparty>;
			}
		}

		Counterparty counterparty;

		public Counterparty Counterparty {
			get {
				if (CounterpartyUoW != null)
					return CounterpartyUoW.Root;
				else
					return counterparty;
			}
			private set {
				counterparty = value;
			}
		}

		#region IRepresentationModelWithParent implementation

		public object GetParent
		{
			get
			{
				return Counterparty;
			}
		}

		#endregion

		#region IRepresentationModel implementation

		public override void UpdateNodes ()
		{

			CounterpartyContract contractAlias = null;
			Counterparty counterpartyAlias = null;
			Organization organizationAlias = null;
			ContractsVMNode resultAlias = null;
			AdditionalAgreement agreementAlias = null;

			var subquery = NHibernate.Criterion.QueryOver.Of<AdditionalAgreement>(() => agreementAlias)
				.Where(() => agreementAlias.Contract.Id == contractAlias.Id)
				.ToRowCountQuery();

			var contractslist = UoW.Session.QueryOver<CounterpartyContract> (() => contractAlias)
				.JoinAlias (c => c.Counterparty, () => counterpartyAlias)
				.JoinAlias (c => c.Organization, () => organizationAlias)
				.Where (() => counterpartyAlias.Id == Counterparty.Id)
				.SelectList(list => list
					.Select(() => contractAlias.Id).WithAlias(() => resultAlias.Id)
			        .Select(() => counterpartyAlias.VodovozInternalId).WithAlias(() => resultAlias.CounterpartyInternalNumber)
			        .Select(() => contractAlias.ContractSubNumber).WithAlias(() => resultAlias.ContractSubNumber)
			        .Select(() => contractAlias.IssueDate).WithAlias(() => resultAlias.IssueDate)
					.Select(() => contractAlias.IsArchive).WithAlias(() => resultAlias.IsArchive)
					.Select(() => contractAlias.OnCancellation).WithAlias(() => resultAlias.OnCancellation)
					.Select(() => organizationAlias.Name).WithAlias(() => resultAlias.Organization)
					.SelectSubQuery(subquery).WithAlias(() => resultAlias.AdditionalAgreements)
				)
				.TransformUsing(Transformers.AliasToBean<ContractsVMNode>())
				.List<ContractsVMNode>();

			SetItemsSource (contractslist);
		}

		IColumnsConfig columnsConfig = FluentColumnsConfig <ContractsVMNode>.Create ()
			.AddColumn("Номер").SetDataProperty (node => node.Title)
			.AddColumn ("Организация").SetDataProperty (node => node.Organization)
			.AddColumn ("Кол-во доп. соглашений").SetDataProperty (node => node.AdditionalAgreements)
			.RowCells ().AddSetter<CellRendererText> ((c, n) => c.Foreground = n.RowColor)
			.Finish ();

		public override IColumnsConfig ColumnsConfig {
			get { return columnsConfig; }
		}

		#endregion

		#region implemented abstract members of RepresentationModelBase

		protected override bool NeedUpdateFunc (CounterpartyContract updatedSubject)
		{
			return Counterparty.Id == updatedSubject.Counterparty.Id;
		}
			
		#endregion

		#region implemented abstract members of RepresentationModelEntitySubscribingBase

		protected override bool NeedUpdateFunc(object updatedSubject)
		{
			var agreement = updatedSubject as AdditionalAgreement;
			return Counterparty.Id == agreement.Contract.Counterparty.Id;
		}

		#endregion

		public ContractsVM (IUnitOfWorkGeneric<Counterparty> uow) : base(
			typeof(DailyRentAgreement), 
			typeof(FreeRentAgreement), 
			typeof(NonfreeRentAgreement),
			typeof(RepairAgreement),
			typeof(WaterSalesAgreement))
		{
			this.UoW = uow;
		}

		public ContractsVM (IUnitOfWork uow, Counterparty counterparty) : base(
			typeof(DailyRentAgreement), 
			typeof(FreeRentAgreement), 
			typeof(NonfreeRentAgreement),
			typeof(RepairAgreement),
			typeof(WaterSalesAgreement))
		{
			UoW = uow;
			Counterparty = counterparty;
		}
	}
		
	public class ContractsVMNode
	{

		public int Id{ get; set;}

		public DateTime IssueDate{ get; set;}

		public bool IsArchive{ get; set;}

		public bool OnCancellation{ get; set;}

		public int CounterpartyInternalNumber{ get; set; }

		public int ContractSubNumber{ get; set; }

		public string Title {
			get { return String.Format("{0}-{1} от {2:d}", CounterpartyInternalNumber, ContractSubNumber, IssueDate); }
		}
			
		public string Organization { get; set;}

		public int AdditionalAgreements { get; set;}

		public string RowColor {
			get {
				if (IsArchive)
					return "grey";
				if (OnCancellation)
					return "blue";
				return "black";

			}
		}
	}
}

