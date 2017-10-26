﻿﻿using FluentNHibernate.Mapping;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;

namespace Vodovoz.HibernateMapping
{
	public class OrderMap : ClassMap<Domain.Orders.Order>
	{
		public OrderMap()
		{
			Table("orders");

			Id(x => x.Id).Column("id").GeneratedBy.Native();

			Map(x => x.Comment)				.Column("comment");
			Map(x => x.DeliveryDate)		.Column("delivery_date");
			Map(x => x.SelfDelivery)		.Column("self_delivery");
			Map(x => x.BottlesReturn)		.Column("bottles_return");
			Map(x => x.ExtraMoney)			.Column("extra_money");
			Map(x => x.SumToReceive)		.Column("sum_to_receive");
			Map(x => x.Shipped)				.Column("shipped");
			Map(x => x.SumDifferenceReason)	.Column("sum_difference_reason");
			Map(x => x.CollectBottles)		.Column("collect_bottles");
			Map(x => x.Code1c)				.Column("code1c");
			Map(x => x.Address1c)			.Column("address_1c");
			Map(x => x.Address1cCode)		.Column("address_1c_code");
			Map(x => x.FromClientText)		.Column("from_client_text");
			Map(x => x.ToClientText)		.Column("to_client_text");
			Map(x => x.DeliverySchedule1c)	.Column("delivery_schedule_1c");
			Map(x => x.DailyNumber1c)		.Column("daily_number_1c");
			Map(x => x.ClientPhone)			.Column("client_phone");
			Map(x => x.LastEditedTime)		.Column("last_edited_time");
			Map(x => x.CommentManager)		.Column("comment_manager");
			Map(x => x.ReturnedTare)		.Column("returned_tare");
			Map(x => x.InformationOnTara)	.Column("information_on_tara");
			Map(x => x.DriverCallId)		.Column("driver_call_id");
			Map(x => x.IsService)			.Column("service");

			Map (x => x.OrderStatus)		.Column ("order_status").CustomType<OrderStatusStringType> ();
			Map (x => x.SignatureType)		.Column ("signature_type").CustomType<OrderSignatureTypeStringType> ();
			Map (x => x.PaymentType)		.Column ("payment_type").CustomType<PaymentTypeStringType> ();
			Map (x => x.DocumentType)		.Column ("document_type").CustomType<DefaultDocumentTypeStringType> ();
			Map (x => x.ReasonType)			.Column ("reason_type").CustomType<ReasonTypeStringType> (); 
			Map (x => x.DriverCallType)	    .Column ("driver_call_type").CustomType<DriverCallTypeStringType>();

			References(x => x.Client).Column("client_id");
			References(x => x.Contract).Column("counterparty_contract_id");
			References(x => x.Author).Column("author_employee_id");
			References(x => x.DeliveryPoint).Column("delivery_point_id");
			References(x => x.DeliverySchedule).Column("delivery_schedule_id");
			References(x => x.PreviousOrder).Column("previous_order_id");
			References(x => x.BottlesMovementOperation).Column("bottles_movement_operation_id");
			References(x => x.MoneyMovementOperation).Column("money_movement_operation_id");
			References(x => x.LastEditor).Column("editor_employee_id");

			HasMany(x => x.OrderDocuments).Cascade.AllDeleteOrphan().LazyLoad().Inverse().KeyColumn("attached_to_order_id");
#if !SHORT
			HasMany (x => x.OrderDepositItems)	.Cascade.AllDeleteOrphan ().LazyLoad ().Inverse ().KeyColumn ("order_id");
#endif
			HasMany (x => x.OrderItems)			.Cascade.AllDeleteOrphan  ().Inverse ().LazyLoad ().KeyColumn ("order_id");
			HasMany (x => x.OrderEquipments)	.Cascade.AllDeleteOrphan  ().Inverse ().LazyLoad ().KeyColumn ("order_id");
			HasMany (x => x.InitialOrderService).Cascade.None ().LazyLoad ().Inverse ().KeyColumn ("initial_order_id");
			HasMany (x => x.FinalOrderService)	.Cascade.None ().LazyLoad ().Inverse ().KeyColumn ("final_order_id");
			HasMany (x => x.DepositOperations)  .Cascade.AllDeleteOrphan  ().Inverse ().LazyLoad ().KeyColumn ("order_id");
		}
	}
}