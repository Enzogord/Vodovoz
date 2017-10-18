using System;
using FluentNHibernate.Mapping;
using Vodovoz.Domain;

namespace Vodovoz.HibernateMapping
{
	public class ScriptTreeElementMap : ClassMap<ScriptTreeElement>
	{
		public ScriptTreeElementMap()
		{
			Table("script_tree_elements");

			Id(x => x.Name)						.Column("name");

			Map(x => x.Label)					.Column("lable");
			Map(x => x.Text)					.Column("text");
			Map(x => x.NextElementsUnparced)	.Column("next_element");
			Map(x => x.Widget)					.Column("widget");
			Map(x => x.Dependency)				.Column("dependency"); 
			Map(x => x.Buttons)					.Column("buttons");
		}
	}
}