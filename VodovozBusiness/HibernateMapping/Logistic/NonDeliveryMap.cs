using Vodovoz.Domain.Logistic;
using FluentNHibernate.Mapping;

namespace Vodovoz.HMap
{
	public class NonDeliveryMap : ClassMap<NonDelivery>
	{
		public NonDeliveryMap ()
		{
            Table ("non_delivered_addresses");

			Id (x => x.Id).Column ("id").GeneratedBy.Native();

			//Map (x => x.InvoceMaker)            .Column ("invoce_maker");
			Map (x => x.Guilt)                  .Column ("guilt");
			Map (x => x.CallToOffice)  			.Column ("call_to_office");
			Map (x => x.CallToClient)   	    .Column ("call_to_client");
		
			//Map (x => x.ForwarderWageSurcharge) .Column ("forwarder_wage_surcharge");
			//Map (x => x.WithForwarder)          .Column ("with_forwarder");
			//Map (x => x.StatusLastUpdate)       .Column ("status_last_update");
			//Map (x => x.Comment)                .Column ("comment").Length (150);
			//Map (x => x.Status)                 .Column ("status").CustomType<RouteListItemStatusStringType>();
			//Map (x => x.NeedToReload)           .Column ("need_to_reload");
			//Map (x => x.WasTransfered)          .Column ("was_transfered");
			//Map (x => x.CashierComment)         .Column ("cashier_comment").Length (150);
			//Map (x => x.CashierCommentCreateDate).Column ("cashier_comment_create_date");
			//Map (x => x.CashierCommentLastUpdate).Column ("cashier_comment_last_update");
			//Map (x => x.Notified30Minutes)      .Column ("notified_30minutes");
			//Map (x => x.NotifiedTimeout)        .Column ("notified_timeout");

			References (x => x.RouteList)           .Column ("route_list_id").Not.Nullable ();
			References (x => x.Order)               .Column ("order_id").Cascade.SaveUpdate();
			References (x => x.Driver)      	    .Column ("driver_id");

			//References (x => x.CashierCommentAuthor).Column ("cashier_comment_author");
		}
	}
}
