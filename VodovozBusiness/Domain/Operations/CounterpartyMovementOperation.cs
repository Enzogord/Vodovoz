﻿using System;
using System.ComponentModel.DataAnnotations;
using QSOrmProject;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Operations;

namespace Vodovoz
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Neuter,
		NominativePlural = "передвижения товаров",
		Nominative = "передвижение товаров")]
	public class CounterpartyMovementOperation : OperationBase
	{
		Nomenclature nomenclature;

		[Required (ErrorMessage = "Номенклатура должна быть заполнена.")]
		[Display (Name = "Номенклатура")]
		public virtual Nomenclature Nomenclature {
			get { return nomenclature; }
			set { SetField (ref nomenclature, value, () => Nomenclature); }
		}

		Equipment equipment;

		[Display (Name = "Оборудование")]
		public virtual Equipment Equipment {
			get { return equipment; }
			set { SetField (ref equipment, value, () => Equipment); }
		}

		decimal amount;

		public virtual decimal Amount {
			get { return amount; }
			set { SetField (ref amount, value, () => Amount); }
		}

		Counterparty incomingCounterparty;

		public virtual Counterparty IncomingCounterparty {
			get { return incomingCounterparty; }
			set { SetField (ref incomingCounterparty, value, () => IncomingCounterparty); }
		}

		DeliveryPoint incomingDeliveryPoint;

		public virtual DeliveryPoint IncomingDeliveryPoint {
			get { return incomingDeliveryPoint; }
			set { SetField (ref incomingDeliveryPoint, value, () => IncomingDeliveryPoint); }
		}

		Counterparty writeoffCounterparty;

		public virtual Counterparty WriteoffCounterparty {
			get { return writeoffCounterparty; }
			set { SetField (ref writeoffCounterparty, value, () => WriteoffCounterparty); }
		}

		DeliveryPoint writeoffDeliveryPoint;

		public virtual DeliveryPoint WriteoffDeliveryPoint {
			get { return writeoffDeliveryPoint; }
			set { SetField (ref writeoffDeliveryPoint, value, () => WriteoffDeliveryPoint); }
		}

		bool forRent;

		public virtual bool ForRent {
			get { return forRent; }
			set { SetField (ref forRent, value, () => ForRent); }
		}

		#region Вычисляемые

		public virtual string Title{
			get{
				if (IncomingCounterparty != null && WriteoffCounterparty != null)
					return string.Format("Перемещение из {0}({4}) в {1}({5}), {2} - {3}", WriteoffCounterparty.Name, IncomingCounterparty.Name, Nomenclature.Name, Nomenclature.Unit.MakeAmountShortStr(Amount), IncomingDeliveryPoint.CompiledAddress, WriteoffDeliveryPoint.CompiledAddress);
				else if (IncomingCounterparty != null)
					return string.Format("Поступление клиенту {0}, {1} - {2}", IncomingCounterparty.Name, Nomenclature.Name, Nomenclature.Unit.MakeAmountShortStr(Amount));
				else if (WriteoffCounterparty != null)
					return string.Format("Забор от клиента {0}, {1} - {2}", WriteoffCounterparty.Name, Nomenclature.Name, Nomenclature.Unit.MakeAmountShortStr(Amount));
				else
					return null;
			}
		}

		#endregion

	}
}
