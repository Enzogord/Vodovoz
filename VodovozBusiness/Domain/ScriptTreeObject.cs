using System;
using Vodovoz.Domain.Logistic;

namespace Vodovoz.Domain
{
	public class ScriptTreeObject
	{
		object resultObject;

		public object ResultObject
		{
			get { return resultObject; }
			set { resultObject = value; }
		}

		Type resultObjectType;

		public Type ResultObjectType
		{
			get { return resultObjectType; }
			set { resultObjectType = value; }
		}
	}

	public class DateSchedule
	{
		DateTime date;

		public DateTime Date {
			get { return date; }
			set { date = value; }
		}

		DeliverySchedule schedule;

		public DeliverySchedule Schedule
		{
			get { return schedule; }
			set { schedule = value; }
		}

	}
}
