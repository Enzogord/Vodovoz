using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;
using Vodovoz.Domain.Employees;

namespace Vodovoz.Domain.Logistic
{
	public class NonDelivery : PropertyChangedBase, IDomainObject
	{
		public virtual int Id { get; set; }

		Employee invoceMaker;

		[Display (Name = "Кто сделал накладную")]
		public virtual Employee InvoceMaker {
			get { return invoceMaker; }
			set { SetField (ref invoceMaker, value, () => InvoceMaker); }
		}

		GuilPerson guilt;

		public virtual GuilPerson Guilt {
			get { return guilt; }
			protected set {
				SetField (ref guilt, value, () => Guilt);
			}
		}

		DateTime callToOffice;

		[Display (Name = "Время звонка в офис")]
		public virtual DateTime CallToOffice {
			get { return callToOffice; }
			set { SetField (ref callToOffice, value, () => CallToOffice); }
		}

		DateTime callToClient;

		[Display (Name = "Время звонка клиенту")]
		public virtual DateTime CallToClient {
			get { return callToClient; }
			set { SetField (ref callToClient, value, () => CallToClient); }
		}




		RouteList routeList;

		[Display (Name = "Маршрутный лист")]
		public virtual RouteList RouteList {
			get { return routeList; }
			set { SetField (ref routeList, value, () => RouteList); }
		}

		Orders.Order order;

		[Display (Name = "Заказ")]
		public virtual Orders.Order Order {
			get { return order; }
			set { SetField (ref order, value, () => Order); }
		}

		Employee driver;

		[Display (Name = "Водитель")]
		public virtual Employee Driver {
			get { return driver; }
			set { SetField (ref driver, value, () => Driver); }
		}

		public NonDelivery ()
		{

		}
	}

	public enum GuilPerson
	{
		[Display (Name = "Водитель")]
		Driver,
		[Display (Name = "Логист")]
		Logist,
		[Display (Name = "Клиент")]
		Client,
		[Display (Name = "Менеджер")]
		Manager
	}
}
