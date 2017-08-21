using System;


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
}
