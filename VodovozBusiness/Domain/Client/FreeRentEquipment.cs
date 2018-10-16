﻿using System;
using QS.DomainModel.Entity;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using QSProjectsLib;
using Vodovoz.Domain.Goods;

namespace Vodovoz.Domain.Client
{
	[OrmSubject (Gender = GrammaticalGender.Feminine,
		NominativePlural = "строки БА соглашения",
		Nominative = "строка БА соглашения")]
	public class FreeRentEquipment : PropertyChangedBase, IDomainObject, IValidatableObject
	{
		public virtual int Id { get; set; }

		FreeRentPackage freeRentPackage;

		[Display (Name = "Пакет бесплатной аренды")]
		public virtual FreeRentPackage FreeRentPackage {
			get { return freeRentPackage; }
			set { SetField (ref freeRentPackage, value, () => FreeRentPackage); }
		}

		Equipment equipment;

		[Display (Name = "Оборудование")]
		public virtual Equipment Equipment {
			get { return equipment; }
			set { SetField (ref equipment, value, () => Equipment); }
		}

		Nomenclature nomenclature;

		[Display(Name = "Номенклатура")]
		public virtual Nomenclature Nomenclature {
			get { return nomenclature; }
			set { SetField(ref nomenclature, value, () => Nomenclature); }
		}

		int count;

		[Display(Name = "Количество")]
		public virtual int Count {
			get { return count; }
			set { SetField(ref count, value, () => Count); }
		}

		Decimal deposit;

		[Display (Name = "Залог")]
		public virtual Decimal Deposit {
			get { return deposit; }
			set { SetField (ref deposit, value, () => Deposit); }
		}

		int waterAmount;

		[Display (Name = "Кол бутылей")]
		public virtual int WaterAmount {
			get { return waterAmount; }
			set { SetField (ref waterAmount, value, () => WaterAmount); }
		}

		bool isNew;

		public virtual bool IsNew {
			get { return isNew; }
			set { SetField (ref isNew, value, () => IsNew); }
		}

		public virtual string PackageName { get { return FreeRentPackage != null ? FreeRentPackage.Name : ""; } }

		/// <summary>
		/// Выводит имя из оборудования если посерийный учет, иначе из номенклатуры 
		/// </summary>
		/// <value>The name of the equipment.</value>
		public virtual string EquipmentName { 
			get {
				if(Equipment != null) {
					return Equipment.NomenclatureName;
				} else if(Nomenclature != null){
					return Nomenclature.Name;
				}else {
					return String.Empty;
				}
			} 
		}

		public virtual string EquipmentSerial { get { return Equipment != null && Equipment.Nomenclature.IsSerial ? Equipment.Serial : ""; } }

		public virtual string DepositString { get { return CurrencyWorks.GetShortCurrencyString (Deposit); } }

		public virtual string WaterAmountString { get { return String.Format ("{0} " + RusNumber.Case (WaterAmount, "бутыль", "бутыли", "бутылей"), WaterAmount); } }

		public virtual string Title {get { return String.Format("Бесплатная аренда {0}", EquipmentName); }}

		#region IValidatableObject implementation

		public virtual IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
		{
			if (FreeRentPackage == null)
				yield return new ValidationResult ("Не выбран пакет бесплатной аренды.", new[] { "FreeRentPackage" });
			
			if (Equipment == null)
				yield return new ValidationResult ("Не выбрано оборудование.", new[] { "Equipment" });
		}

		#endregion

		public FreeRentEquipment ()
		{
		}
	}
}

