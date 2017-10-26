﻿using System;
using System.Collections.Generic;
using Gamma.ColumnConfig;
using NHibernate.Criterion;
using NHibernate.Transform;
using QSOrmProject;
using QSOrmProject.RepresentationModel;
using Vodovoz.Domain;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Goods;

namespace Vodovoz.ViewModel
{
	public class EquipmentForRepairVM : RepresentationModelWithoutEntityBase<EquipmentForRepairVMNode>
	{

		public EquipmentForRepairVM()
				: this(UnitOfWorkFactory.CreateWithoutRoot())
		{ }

		public EquipmentForRepairVM(IUnitOfWork uow) : base(typeof(Nomenclature), typeof(WarehouseMovementOperation))
		{
			this.UoW = uow;
		}

		#region implemented abstract members of RepresentationModelBase

		public override void UpdateNodes()
		{
			Nomenclature nomenclatureAlias = null;
			EquipmentType equipmentTypeAlias = null;
			EquipmentForRepairVMNode resultAlias = null;
			WarehouseMovementOperation operationAddAlias = null;
			WarehouseMovementOperation operationRemoveAlias = null;
			Vodovoz.Domain.Orders.Order orderAlias = null;
			OrderEquipment orderEquipmentAlias = null;
			Equipment equipmentAlias = null;

			var subqueryAdded = QueryOver.Of<WarehouseMovementOperation>(() => operationAddAlias)
				.JoinAlias(() => operationAddAlias.Nomenclature, () => nomenclatureAlias)
				.Where(() => nomenclatureAlias.Type.Id == equipmentTypeAlias.Id)
				.Where(Restrictions.IsNotNull(Projections.Property<WarehouseMovementOperation>(o => o.IncomingWarehouse)))
				.Select(Projections.Sum<WarehouseMovementOperation>(o => o.Amount));

			var subqueryRemoved = QueryOver.Of<WarehouseMovementOperation>(() => operationRemoveAlias)
				.JoinAlias(() => operationRemoveAlias.Nomenclature, () => nomenclatureAlias)
				.Where(() => nomenclatureAlias.Type.Id == equipmentTypeAlias.Id)
				.Where(Restrictions.IsNotNull(Projections.Property<WarehouseMovementOperation>(o => o.WriteoffWarehouse)))
				.Select(Projections.Sum<WarehouseMovementOperation>(o => o.Amount));

			var subqueryReserved = QueryOver.Of<Vodovoz.Domain.Orders.Order>(() => orderAlias)
				.JoinAlias(() => orderAlias.OrderEquipments, () => orderEquipmentAlias)
				.JoinAlias(() => orderEquipmentAlias.Equipment, () => equipmentAlias)
				.JoinAlias(() => equipmentAlias.Nomenclature, () => nomenclatureAlias)
				.Where(() => nomenclatureAlias.Type.Id == equipmentTypeAlias.Id)
				.Where(() => orderEquipmentAlias.Direction == Direction.Deliver)
				.Where(() => orderAlias.OrderStatus == OrderStatus.Accepted)
				.Select(Projections.Count(() => orderEquipmentAlias.Id));

			var equipment = UoW.Session.QueryOver<EquipmentType>(() => equipmentTypeAlias)
				.SelectList(list => list
					.SelectGroup(() => equipmentTypeAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => equipmentTypeAlias.Name).WithAlias(() => resultAlias.Name)
					.SelectSubQuery(subqueryAdded).WithAlias(() => resultAlias.Added)
					.SelectSubQuery(subqueryRemoved).WithAlias(() => resultAlias.Removed)
					.SelectSubQuery(subqueryReserved).WithAlias(() => resultAlias.Reserved)
				)
				.TransformUsing(Transformers.AliasToBean<EquipmentForRepairVMNode>())
				.List<EquipmentForRepairVMNode>();

			SetItemsSource(equipment);
		}

		static Gdk.Color colorBlack = new Gdk.Color(0, 0, 0);
		static Gdk.Color colorRed = new Gdk.Color(0xff, 0, 0);

		IColumnsConfig columnsConfig = FluentColumnsConfig<EquipmentForRepairVMNode>.Create()
			.AddColumn("Тип оборудования").SetDataProperty(node => node.Name)
			.AddColumn("На складе").AddTextRenderer(node => node.InStockText)
			.AddSetter((cell, node) => cell.ForegroundGdk = node.Available > 0 ? colorBlack : colorRed)
			.Finish();

		public override IColumnsConfig ColumnsConfig
		{
			get { return columnsConfig; }
		}

		#endregion


		#region implemented abstract members of RepresentationModelWithoutEntityBase

		protected override bool NeedUpdateFunc(object updatedSubject)
		{
			return true;
		}

		#endregion
	}

	public class EquipmentForRepairVMNode
	{
		public int Id { get; set; }

		[UseForSearch]
		public string Name { get; set; }
		public decimal InStock { get { return Added - Removed; } }
		public int Reserved { get; set; }
		public decimal Available { get { return InStock - Reserved; } }
		public decimal Added { get; set; }
		public decimal Removed { get; set; }
		public string UnitName { get; set; }
		public short UnitDigits { get; set; }

		public string InStockText { get { return InStock.ToString("N0"); } }
		public string ReservedText { get { return Reserved.ToString(); } }
		public string AvailableText { get { return Available.ToString("N0"); } }


	}
 
}