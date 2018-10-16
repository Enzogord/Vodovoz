﻿using System.Linq;
using Gamma.ColumnConfig;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QSOrmProject;
using QSOrmProject.RepresentationModel;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Logistic;

namespace Vodovoz.ViewModel
{
	public class BottleReceptionVM:RepresentationModelWithoutEntityBase<BottleReceptionVMNode>
	{
		public BottleReceptionVM () : this (UnitOfWorkFactory.CreateWithoutRoot ())
		{
		}

		public BottleReceptionVM (IUnitOfWork uow) : base (typeof(RouteList))
		{
			this.UoW = uow;
		}

		public override void UpdateNodes ()
		{
			BottleReceptionVMNode resultAlias = null;
			Nomenclature nomenclatureAlias = null;

			var orderBottles = UoW.Session.QueryOver<Nomenclature> (() => nomenclatureAlias)
			                      .Where (n => n.Category == NomenclatureCategory.bottle)
			                      .Where(n => !n.IsDefectiveBottle)
				.SelectList (list => list
					.Select (() => nomenclatureAlias.Id).WithAlias (() => resultAlias.NomenclatureId)
					.Select (() => nomenclatureAlias.Name).WithAlias (() => resultAlias.Name)
				).TransformUsing (Transformers.AliasToBean<BottleReceptionVMNode> ())
				.List<BottleReceptionVMNode> ();

			SetItemsSource (orderBottles.ToList());			
		}

		IColumnsConfig columnsConfig = FluentColumnsConfig<BottleReceptionVMNode>.Create ()
			.AddColumn ("Номенклатура").AddTextRenderer (node => node.Name)
			.AddColumn ("Кол-во").AddNumericRenderer (node => node.Amount)
			.Adjustment (new Gtk.Adjustment (0, 0, 9999, 1, 100, 0))
			.Editing (true)
			.AddColumn("")
			.Finish ();

		public override IColumnsConfig ColumnsConfig {
			get { return columnsConfig; }
		}
		protected override bool NeedUpdateFunc (object updatedSubject)
		{
			return true;
		}
	}

	public class BottleReceptionVMNode{
		public int NomenclatureId{get;set;}
		public string Name{get;set;}
		public int Amount{get;set;}
	}
}

