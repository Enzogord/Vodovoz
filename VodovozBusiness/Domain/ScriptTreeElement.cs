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

		string label;

		public virtual string Label {
			get { return label; }
			set { SetField(ref label, value, () => Label); }
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

		string dependency;

		public virtual string Dependency
		{
			get { return dependency; }
			set { SetField(ref dependency, value, () => Dependency);}
		}

	    bool buttons;

		public virtual bool Buttons 
		{
			get { return buttons; }
			set { SetField(ref buttons, value, () => Buttons); }
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
		welcome,
		nowidget,
		text,
		counterparty,
		deliverypoint,
		datetime,
		dateschedule,
		checkschedule,
		orderrepeat,
		checkemptybottles,
		constructor,
		leaveandprint,
		neworder,
		equipments,
		additional,
		freerent,
		paidrent,
		sanitization,
		changeandstamp
	}
}