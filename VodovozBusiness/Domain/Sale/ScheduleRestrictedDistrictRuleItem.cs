﻿using System;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;

namespace Vodovoz.Domain.Sale
{
	public class ScheduleRestrictedDistrictRuleItem : PropertyChangedBase, IDomainObject
	{
		public virtual int Id { get; set; }

		ScheduleRestrictedDistrict scheduleRestrictedDistrict;
		[Display(Name = "Район доставки")]
		public virtual ScheduleRestrictedDistrict ScheduleRestrictedDistrict {
			get { return scheduleRestrictedDistrict; }
			set { SetField(ref scheduleRestrictedDistrict, value, () => ScheduleRestrictedDistrict); }
		}

		DeliveryPriceRule deliveryPriceRule;
		[Display(Name = "Правило цены доставки")]
		public virtual DeliveryPriceRule DeliveryPriceRule {
			get { return deliveryPriceRule; }
			set { SetField(ref deliveryPriceRule, value, () => DeliveryPriceRule); }
		}

		Decimal deliveryPrice;
		[Display(Name = "Цена доставки")]
		public virtual Decimal DeliveryPrice {
			get { return deliveryPrice; }
			set { SetField(ref deliveryPrice, value, () => DeliveryPrice); }
		}
	}
}
