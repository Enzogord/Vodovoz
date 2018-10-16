﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using NHibernate.Criterion;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QSBusinessCommon.Domain;
using QSOrmProject;
using QSOrmProject.RepresentationModel;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Orders;
using Vodovoz.JournalFilters;

namespace Vodovoz.ViewModel
{
	public class NomenclatureForSaleVM : RepresentationModelWithoutEntityBase<NomenclatureForSaleVMNode>
	{

		public NomenclatureRepFilter Filter {
			get {
				return RepresentationFilter as NomenclatureRepFilter;
			}
			set {
				RepresentationFilter = value as IRepresentationFilter;
			}
		}

		#region implemented abstract members of RepresentationModelBase

		public override void UpdateNodes()
		{
			Nomenclature nomenclatureAlias = null;
			MeasurementUnits unitAlias = null;
			NomenclatureForSaleVMNode resultAlias = null;
			WarehouseMovementOperation operationAddAlias = null;
			WarehouseMovementOperation operationRemoveAlias = null;
			Vodovoz.Domain.Orders.Order orderAlias = null;
			OrderItem orderItemsAlias = null;

			var subqueryAdded = QueryOver.Of<WarehouseMovementOperation>(() => operationAddAlias)
				.Where(() => operationAddAlias.Nomenclature.Id == nomenclatureAlias.Id)
				.Where(Restrictions.IsNotNull(Projections.Property<WarehouseMovementOperation>(o => o.IncomingWarehouse)))
				.Select(Projections.Sum<WarehouseMovementOperation>(o => o.Amount));

			var subqueryRemoved = QueryOver.Of<WarehouseMovementOperation>(() => operationRemoveAlias)
				.Where(() => operationRemoveAlias.Nomenclature.Id == nomenclatureAlias.Id)
				.Where(Restrictions.IsNotNull(Projections.Property<WarehouseMovementOperation>(o => o.WriteoffWarehouse)))
				.Select(Projections.Sum<WarehouseMovementOperation>(o => o.Amount));

			var subqueryReserved = QueryOver.Of<Vodovoz.Domain.Orders.Order>(() => orderAlias)
				.JoinAlias(() => orderAlias.OrderItems, () => orderItemsAlias)
				.Where(() => orderItemsAlias.Nomenclature.Id == nomenclatureAlias.Id)
				.Where(() => nomenclatureAlias.DoNotReserve == false)
				.Where(() => orderAlias.OrderStatus == OrderStatus.Accepted
					   || orderAlias.OrderStatus == OrderStatus.InTravelList
					   || orderAlias.OrderStatus == OrderStatus.OnLoading)
				.Select(Projections.Sum(() => orderItemsAlias.Count));

			var itemsQuery = UoW.Session.QueryOver<Nomenclature>(() => nomenclatureAlias)
						   .Where(() => !nomenclatureAlias.IsArchive);

			if(!Filter.ShowDilers)
				itemsQuery.Where(() => !nomenclatureAlias.IsDiler);

			itemsQuery.Where(n => n.Category.IsIn(Filter.SelectedCategories));

			if(Filter.SelectedCategories.Count() == 1 && Filter.SelectedCategories.Contains(NomenclatureCategory.equipment))
				itemsQuery.Where(n => n.SubTypeOfEquipmentCategory.IsIn(Filter.SelectedSubCategories));
				
			itemsQuery.Left.JoinAlias(() => nomenclatureAlias.Unit, () => unitAlias)
				.Where(() => !nomenclatureAlias.IsSerial)
				.SelectList(list => list
					.SelectGroup(() => nomenclatureAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => nomenclatureAlias.Name).WithAlias(() => resultAlias.Name)
					.Select(() => nomenclatureAlias.Category).WithAlias(() => resultAlias.Category)
					.Select(() => unitAlias.Name).WithAlias(() => resultAlias.UnitName)
					.Select(() => unitAlias.Digits).WithAlias(() => resultAlias.UnitDigits)
					.SelectSubQuery(subqueryAdded).WithAlias(() => resultAlias.Added)
					.SelectSubQuery(subqueryRemoved).WithAlias(() => resultAlias.Removed)
					.SelectSubQuery(subqueryReserved).WithAlias(() => resultAlias.Reserved)
				)
				.TransformUsing(Transformers.AliasToBean<NomenclatureForSaleVMNode>());
			
			var items = itemsQuery.List<NomenclatureForSaleVMNode>();

			List<NomenclatureForSaleVMNode> forSale = new List<NomenclatureForSaleVMNode>();
			forSale.AddRange(items);
			forSale.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.CurrentCulture));
			SetItemsSource(forSale);
		}

		static Gdk.Color colorBlack = new Gdk.Color(0, 0, 0);
		static Gdk.Color colorRed = new Gdk.Color(0xff, 0, 0);

		IColumnsConfig columnsConfig = FluentColumnsConfig<NomenclatureForSaleVMNode>.Create()
			.AddColumn("Код").AddTextRenderer(node => node.Id.ToString())
			.AddColumn("Номенклатура").SetDataProperty(node => node.Name)
			.AddColumn("Категория").SetDataProperty(node => node.Category.GetEnumTitle())
			.AddColumn("Кол-во").AddTextRenderer(node => node.InStockText)
			.AddColumn("Зарезервировано").AddTextRenderer(node => node.ReservedText)
			.AddColumn("Доступно").AddTextRenderer(node => node.AvailableText)
			.AddSetter((cell, node) => cell.ForegroundGdk = node.Available > 0 ? colorBlack : colorRed)
			.Finish();

		public override IColumnsConfig ColumnsConfig {
			get { return columnsConfig; }
		}

		#endregion

		public NomenclatureForSaleVM()
			: this(UnitOfWorkFactory.CreateWithoutRoot())
		{ }

		public NomenclatureForSaleVM(IUnitOfWork uow) : base(typeof(Nomenclature), typeof(WarehouseMovementOperation))
		{
			this.UoW = uow;
		}

		public NomenclatureForSaleVM(NomenclatureRepFilter filter) : this(filter.UoW)
		{
			Filter = filter;
		}

		#region implemented abstract members of RepresentationModelWithoutEntityBase

		protected override bool NeedUpdateFunc(object updatedSubject)
		{
			return true;
		}

		#endregion
	}

	public class NomenclatureForSaleVMNode
	{
		[UseForSearch]
		public int Id { get; set; }

		[UseForSearch]
		public string Name { get; set; }
		public NomenclatureCategory Category { get; set; }
		public decimal InStock { get { return Added - Removed; } }
		public int? Reserved { get; set; }
		public decimal Available { get { return InStock - Reserved.GetValueOrDefault(); } }
		public decimal Added { get; set; }
		public decimal Removed { get; set; }
		public string UnitName { get; set; }
		public short UnitDigits { get; set; }
		public bool IsEquipmentWithSerial { get; set; }
		private string Format(decimal value)
		{
			return String.Format("{0:F" + UnitDigits + "} {1}", value, UnitName);
		}

		private bool UsedStock {
			get {
				return Nomenclature.GetCategoriesForGoods().Contains(Category);
			}
		}

		public string InStockText { get { return UsedStock ? Format(InStock) : String.Empty; } }
		public string ReservedText { get { return UsedStock && Reserved.HasValue ? Format(Reserved.Value) : String.Empty; } }
		public string AvailableText { get { return UsedStock ? Format(Available) : String.Empty; } }
	}
}

