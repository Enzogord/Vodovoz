﻿using FluentNHibernate.Mapping;
using QSOsm.DTO;
using Vodovoz.Domain.Client;

namespace Vodovoz.HibernateMapping
{
	public class DeliveryPointMap : ClassMap<DeliveryPoint>
	{
		public DeliveryPointMap()
		{
			Table("delivery_points");
			Not.LazyLoad();

			Id(x => x.Id).Column("id").GeneratedBy.Native();

			Map(x => x.CompiledAddress)  		.Column("compiled_address");
			Map(x => x.ShortAddress)	  		.Column("compiled_address_short");
			Map(x => x.MinutesToUnload)  		.Column("minutes_to_unload");
			Map(x => x.Floor)			  		.Column("floor");
			Map(x => x.EntranceType)			.Column("entrance_type").CustomType<EntranceTypeStringType>();
			Map(x => x.Entrance)				.Column("entrance");
			Map(x => x.City)			  		.Column("city");
			Map(x => x.LocalityType)	  		.Column("locality_type").CustomType<LocalityTypeStringType>();
			Map(x => x.CityDistrict)	  		.Column("city_district");
			Map(x => x.Street)			  		.Column("street");
			Map(x => x.StreetDistrict)	  		.Column("street_district");
			Map(x => x.Building)		  		.Column("building");
			Map(x => x.RoomType)		  		.Column("room_type").CustomType<RoomTypeStringType>();
			Map(x => x.Room)			    	.Column("room");
			Map(x => x.Letter)					.Column("letter");
			Map(x => x.Placement)		 		.Column("placement");
			Map(x => x.АddressAddition)  		.Column("address_addition");
			Map(x => x.Comment)		  			.Column("comment");
			Map(x => x.FoundOnOsm)		  		.Column("found_on_osm");
			Map(x => x.ManualCoordinates)		.Column("manual_coordinates");
			Map(x => x.IsFixedInOsm)	 	 	.Column("is_fixed_in_osm");
			Map(x => x.Latitude)		 	 	.Column("latitude");
			Map(x => x.Longitude)			  	.Column("longitude");
			Map(x => x.IsActive)			  	.Column("is_active");
			Map(x => x.Address1c)			  	.Column("address_1c");
			Map(x => x.Code1c)			  		.Column("code1c");
			Map(x => x.BottleReserv)	  		.Column("bottle_reserv");
			Map(x => x.DistanceFromBaseMeters)	.Column("distance_from_center_meters");
			Map(x => x.HaveResidue)				.Column("have_residue");

			Map(x => x.FixPrice1)				.Column("fix_price1");
			Map(x => x.FixPrice2)				.Column("fix_price2");
			Map(x => x.FixPrice3)				.Column("fix_price3");
			Map(x => x.FixPrice4)				.Column("fix_price4");
			Map(x => x.FixPrice5)				.Column("fix_price5");

			References(x => x.Counterparty)				.Column("counterparty_id");
			References(x => x.LogisticsArea)			.Column("logistic_area_id");
			References(x => x.DeliverySchedule)			.Column("delivery_schedule_id");
			References(x => x.СoordsLastChangeUser)		.Column("coords_lastchange_user_id");
			References(x => x.DefaultWaterNomenclature)	.Column("default_nomenclature_id");

			HasManyToMany(x => x.Contacts).Table("counterparty_delivery_point_contacts")
				.ParentKeyColumn("delivery_point_id")
				.ChildKeyColumn("contact_person_id")
				.LazyLoad();

			HasMany(x => x.Phones).Cascade.AllDeleteOrphan().LazyLoad()
			                      .KeyColumn("delivery_point_id");
		}
	}
}