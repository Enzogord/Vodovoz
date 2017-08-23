﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Gamma.Utilities;
using NHibernate.Criterion;
using QSBusinessCommon.Domain;
using QSOrmProject;

namespace Vodovoz.Domain
{
	public class ScriptTreeElement : PropertyChangedBase
	{
		string name;

		public virtual string Name {
			get { return name; }
			set { SetField(ref name, value, () => Name); }
		}

		string text;

		public virtual string Text {
			get { return text; }
			set { SetField(ref text, value, () => Text); }
		}

		string nextElementsUnparced;

		public virtual string NextElementsUnparced {
			get { return nextElementsUnparced; }
			set { SetField(ref nextElementsUnparced, value, () => NextElementsUnparced); }
		}

		DialogueScriptWidget widget;

		public virtual DialogueScriptWidget Widget {
			get { return widget; }
			set { SetField(ref widget, value, () => Widget); }
		}

		public virtual List<string> ParseNextElements()
		{
			if(this.NextElementsUnparced == "")
			{
				return new List<string>();
			}

			return this.NextElementsUnparced.Split(',').ToList();
		}

		public virtual void SetNextElements(List<string> nextElementsParced)
		{
			this.NextElementsUnparced = string.Join(",", nextElementsParced.ToArray());
		}
	}

	public enum DialogueScriptWidget
	{
		nowidget,
		text,
		counterparty,
		deliverypoint,
		datetime,
		dateschedule,
		checkschedule,
		orderrepeat,
		checkemptybottles
	}
}