﻿using System;
using Vodovoz.Domain.Logistic;
using FluentNHibernate.Mapping;

namespace Vodovoz.HibernateMapping
{
	public class CarMap : ClassMap<Car>
	{
		public CarMap ()
		{
			Table("cars");

			Id(x => x.Id).Column ("id").GeneratedBy.Native();

			Map(x => x.Model)				.Column ("model");
			Map(x => x.RegistrationNumber)	.Column ("reg_number");
			Map(x => x.FuelConsumption)		.Column ("fuel_consumption");
			Map(x => x.IsArchive)			.Column ("is_archive");
			Map(x => x.Photo)				.Column ("photo").CustomSqlType ("BinaryBlob").LazyLoad ();
			Map(x => x.IsCompanyHavings)	.Column ("is_company_havings");
			Map(x => x.TypeOfUse)			.Column ("type_of_use").CustomType<CarTypeOfUseStringType>();	
			Map(x => x.MaxVolume)			.Column ("max_volume");
			Map(x => x.MaxWeight)			.Column ("max_weight");
			Map(x => x.MinBottles)			.Column ("min_bottles");
			Map(x => x.MaxBottles)			.Column ("max_bottles");
			Map(x => x.MinRouteAddresses)	.Column ("min_route_addresses");
			Map(x => x.MaxRouteAddresses)	.Column ("max_route_addresses");
			Map(x => x.IsRaskat)			.Column ("is_raskat");
			Map(x => x.VIN)					.Column ("VIN");
			Map(x => x.ManufactureYear)		.Column ("manufacture_year");
			Map(x => x.MotorNumber)			.Column ("motor_number");
			Map(x => x.ChassisNumber)		.Column ("chassis_number");
			Map(x => x.Carcase)				.Column ("carcase");
			Map(x => x.Color)				.Column ("color");
			Map(x => x.DocSeries)			.Column ("doc_series");
			Map(x => x.DocNumber)			.Column ("doc_number");
			Map(x => x.DocIssuedOrg)		.Column ("doc_issued_org");
			Map(x => x.DocIssuedDate)		.Column ("doc_issued_date");

			References(x => x.Driver)		.Column ("driver_id");
			References(x => x.FuelType)		.Column ("fuel_type_id");
		}
	}
}

