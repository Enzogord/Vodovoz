using System;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QSOrmProject.RepresentationModel;
using Vodovoz.Domain.Client;

namespace Vodovoz.ViewModel
{
	public class AdditionalAgreementsVM : RepresentationModelWithoutEntityBase<AdditionalAgreementVMNode>
	{
		public IUnitOfWorkGeneric<CounterpartyContract> CounterpartyUoW {
			get {
				return UoW as IUnitOfWorkGeneric<CounterpartyContract>;
			}
		}

		#region IRepresentationModel implementation

		public override void UpdateNodes ()
		{
			AdditionalAgreement additionalAgreementAlias = null;
			CounterpartyContract counterpartyContractAlias = null;
			AdditionalAgreementVMNode resultAlias = null;
			DeliveryPoint deliveryPointAlias = null;

			var additionalAgreementsList = UoW.Session.QueryOver<AdditionalAgreement> (() => additionalAgreementAlias)
				.JoinAlias (c => c.Contract, () => counterpartyContractAlias)
				.JoinAlias (c => c.DeliveryPoint, () => deliveryPointAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
				.Where (() => counterpartyContractAlias.Id == CounterpartyUoW.Root.Id)
				.SelectList (list => list
					.Select (() => additionalAgreementAlias.Id).WithAlias (() => resultAlias.Id)
					.Select (() => additionalAgreementAlias.AgreementNumber).WithAlias (() => resultAlias.Number)
					.Select (() => additionalAgreementAlias.IssueDate).WithAlias (() => resultAlias.IssueDate)
					.Select (() => additionalAgreementAlias.GetType()).WithAlias (() => resultAlias.TypeString)
					.Select (() => deliveryPointAlias.Building).WithAlias (() => resultAlias.Building)
					.Select (() => deliveryPointAlias.City).WithAlias (() => resultAlias.City)
					.Select (() => deliveryPointAlias.IsActive).WithAlias (() => resultAlias.IsActive)
					.Select (() => deliveryPointAlias.CompiledAddress).WithAlias (() => resultAlias.Name)
					.Select (() => deliveryPointAlias.Street).WithAlias (() => resultAlias.Street)
					.Select (() => deliveryPointAlias.Room).WithAlias (() => resultAlias.Room))
				.TransformUsing (Transformers.AliasToBean<AdditionalAgreementVMNode> ())
				.List<AdditionalAgreementVMNode> ();
			SetItemsSource (additionalAgreementsList);
		}

		IColumnsConfig columnsConfig = FluentColumnsConfig<AdditionalAgreementVMNode>.Create ()
			.AddColumn ("Номер").SetDataProperty (node => node.NumberString)
			.AddColumn ("Дата").SetDataProperty (node => node.IssueDateString)
			.AddColumn ("Тип").SetDataProperty (node => node.TypeTitle)
			.AddColumn ("Точка доставки").SetDataProperty (node => node.Point)
			.Finish ();

		public override IColumnsConfig ColumnsConfig {
			get { return columnsConfig; }
		}

		#endregion

		#region implemented abstract members of RepresentationModelBase

		protected override bool NeedUpdateFunc (object updatedSubject)
		{
			return (updatedSubject as AdditionalAgreement).Contract.Id == CounterpartyUoW.Root.Id;
		}

		#endregion

		public AdditionalAgreementsVM (IUnitOfWorkGeneric<CounterpartyContract> uow) : base (
				typeof(DailyRentAgreement), 
				typeof(FreeRentAgreement), 
				typeof(NonfreeRentAgreement),
				typeof(RepairAgreement),
				typeof(WaterSalesAgreement))
		{
			this.UoW = uow;
		}
	}

	public class AdditionalAgreementVMNode
	{

		public int Id { get; set; }

		public int Number { get; set; }

		public string NumberString { 
			get {
				return String.Format("{0}{1}", AdditionalAgreement.GetTypePrefix(AgreementType), Number);
				}
		}

		public DateTime IssueDate { get; set; }

		public string IssueDateString { get { return String.Format ("От {0}", IssueDate.ToShortDateString ()); } }

		public AgreementType AgreementType
		{
			get
			{
				AgreementType type;
				Enum.TryParse(TypeString, out type);
				return type;
			}
		}

		public string TypeString { get; set; }

		public string TypeTitle {
			get {
				return AgreementType.GetEnumTitle();
			}
		}

		public string Name { get; set; }

		public string City { get; set; }

		public string Street { get; set; }

		public string Building { get; set; }

		public string Room { get; set; }

		public bool IsActive { get; set; }

		public string RowColor { get { return IsActive ? "black" : "grey"; } }

		public string Point { 
			get { if (String.IsNullOrWhiteSpace (Name))
					return String.Empty;
				else
					return String.Format ("{0}", (Name == String.Empty ? "" : "\"" + Name + "\": ")); }
		}
	}
}

