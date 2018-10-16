﻿using System;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity;
using QSOrmProject;

namespace Vodovoz.Domain.Cash
{
	[OrmSubject (Gender = GrammaticalGender.Feminine,
		NominativePlural = "статьи расхода",
		Nominative = "статья расхода")]
	public class ExpenseCategory : DomainTreeNodeBase<ExpenseCategory>, IDomainObject
	{
		#region Свойства

		string name;

		[Required (ErrorMessage = "Название статьи должно быть заполнено.")]
		[Display (Name = "Название")]
		public virtual string Name {
			get { return name; }
			set { SetField (ref name, value, () => Name); }
		}

		#endregion

		public ExpenseCategory ()
		{
			Name = String.Empty;
		}

	}
}

