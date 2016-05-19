using System;
using System.ComponentModel.DataAnnotations;
using QSOrmProject;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Store;

namespace Vodovoz.Domain.Documents
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Feminine,
		NominativePlural = "строки пересортицы",
		Nominative = "строка пересортицы")]
	public class RegradingOfGoodsDocumentItem: PropertyChangedBase, IDomainObject
	{
		public virtual int Id { get; set; }

		public virtual RegradingOfGoodsDocument Document { get; set; }

		Nomenclature nomenclatureOld;

		[Required (ErrorMessage = "Старая номенклатура должна быть заполнена.")]
		[Display (Name = "Старая номенклатура")]
		public virtual Nomenclature NomenclatureOld {
			get { return nomenclatureOld; }
			set {
				SetField (ref nomenclatureOld, value, () => NomenclatureOld);

				if (WarehouseWriteOffOperation != null && WarehouseWriteOffOperation.Nomenclature != nomenclatureOld)
					WarehouseWriteOffOperation.Nomenclature = nomenclatureOld;
			}
		}

		Nomenclature nomenclatureNew;

		[Required (ErrorMessage = "Новая номенклатура должна быть заполнена.")]
		[Display (Name = "Новая номенклатура")]
		public virtual Nomenclature NomenclatureNew {
			get { return nomenclatureNew; }
			set {
				SetField (ref nomenclatureNew, value, () => NomenclatureNew);

				if (WarehouseIncomeOperation != null && WarehouseIncomeOperation.Nomenclature != nomenclatureNew)
					WarehouseIncomeOperation.Nomenclature = nomenclatureNew;
			}
		}

		Equipment equipment;
		//FIXME убрать если не понадобится.
		/*		[Display (Name = "Оборудование")]
		public virtual Equipment Equipment {
			get { return equipment; }
			set {
				SetField (ref equipment, value, () => Equipment);
				if (WarehouseChangeOperation != null && WarehouseChangeOperation.Equipment != equipment)
					WarehouseChangeOperation.Equipment = equipment;

				if (CounterpartyWriteoffOperation != null && CounterpartyWriteoffOperation.Equipment != equipment)
					CounterpartyWriteoffOperation.Equipment = equipment;
			}
		}*/

		decimal amount;

		[Display (Name = "Количество")]
		public virtual decimal Amount {
			get { return amount; }
			set {
				SetField (ref amount, value, () => Amount);

				if (WarehouseIncomeOperation != null && WarehouseIncomeOperation.Amount != Amount)
					WarehouseIncomeOperation.Amount = Amount;
				if (WarehouseWriteOffOperation != null && WarehouseWriteOffOperation.Amount != Amount)
					WarehouseWriteOffOperation.Amount = Amount;
			}
		}

		string comment;

		[Display (Name = "Комментарий")]
		public virtual string Comment {
			get { return comment; }
			set { SetField (ref comment, value, () => Comment); }
		}

		WarehouseMovementOperation warehouseWriteOffOperation = new WarehouseMovementOperation();

		public virtual WarehouseMovementOperation WarehouseWriteOffOperation {
			get { return warehouseWriteOffOperation; }
			set { SetField (ref warehouseWriteOffOperation, value, () => WarehouseWriteOffOperation); }
		}

		WarehouseMovementOperation warehouseIncomeOperation = new WarehouseMovementOperation();

		public virtual WarehouseMovementOperation WarehouseIncomeOperation {
			get { return warehouseIncomeOperation; }
			set { SetField (ref warehouseIncomeOperation, value, () => WarehouseIncomeOperation); }
		}

		#region Не сохраняемые

		decimal amountInStock;

		[Display (Name = "Количество на складе")]
		public virtual decimal AmountInStock {
			get { return amountInStock; }
			set {
				SetField (ref amountInStock, value, () => AmountInStock);
			}
		}

		#endregion

		#region Расчетные

		public virtual string Title {
			get{
				return String.Format("{0} -> {1} - {2}", 
					NomenclatureOld.Name, 
					NomenclatureNew.Name,
					NomenclatureOld.Unit.MakeAmountShortStr(Amount));
			}
		}

		#endregion

		#region Функции

		public virtual void CreateOperation(Warehouse warehouse, DateTime time)
		{
			WarehouseWriteOffOperation = new WarehouseMovementOperation
				{
					WriteoffWarehouse = warehouse,
					Amount = Amount,
					OperationTime = time,
					Nomenclature = NomenclatureOld
				};
			WarehouseIncomeOperation = new WarehouseMovementOperation
				{
					IncomingWarehouse = warehouse,
					Amount = Amount,
					OperationTime = time,
					Nomenclature = NomenclatureNew
				};
		}

		#endregion
	}
}
