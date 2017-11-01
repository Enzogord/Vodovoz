﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using NHibernate.Criterion;
using QSOrmProject;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Orders;
using VodovozOrder = Vodovoz.Domain.Orders.Order;

namespace Vodovoz.Repository
{
	public static class OrderRepository
	{
		public static ListStore GetListStoreSumDifferenceReasons (IUnitOfWork uow)
		{
			Vodovoz.Domain.Orders.Order order = null;

			var reasons = uow.Session.QueryOver<VodovozOrder> (() => order)
				.Select (Projections.Distinct (Projections.Property (() => order.SumDifferenceReason)))
				.List<string> ();

			var store = new ListStore (typeof(string));
			foreach (string s in reasons) {
				store.AppendValues (s);
			}
			return store;
		}
			
		public static QueryOver<VodovozOrder> GetOrdersForRLEditingQuery (DateTime date, bool showShipped)
		{
			var query = QueryOver.Of<VodovozOrder>();
			if (!showShipped)
				query.Where(order => order.OrderStatus == OrderStatus.Accepted || order.OrderStatus == OrderStatus.InTravelList);
			else
				query.Where(order => order.OrderStatus != OrderStatus.Canceled && order.OrderStatus != OrderStatus.NewOrder && order.OrderStatus != OrderStatus.WaitForPayment);
			return query.Where(order => order.DeliveryDate == date.Date && !order.SelfDelivery);
		}

		public static IList<VodovozOrder> GetAcceptedOrdersForRegion (IUnitOfWork uow, DateTime date, LogisticsArea area)
		{
			DeliveryPoint point = null;
			return uow.Session.QueryOver<VodovozOrder> ()
				.JoinAlias (o => o.DeliveryPoint, () => point)
				.Where (o => o.DeliveryDate == date.Date && point.LogisticsArea.Id == area.Id 
					&& !o.SelfDelivery && o.OrderStatus == Vodovoz.Domain.Orders.OrderStatus.Accepted)
				.List<Vodovoz.Domain.Orders.Order> ();
		}

		public static VodovozOrder GetLatestCompleteOrderForCounterparty(IUnitOfWork UoW, Counterparty counterparty)
		{
			VodovozOrder orderAlias = null;
			var queryResult = UoW.Session.QueryOver<Vodovoz.Domain.Orders.Order>(() => orderAlias)
				.Where(() => orderAlias.Client.Id == counterparty.Id)
				.Where(() => orderAlias.OrderStatus == OrderStatus.Closed)
				.OrderBy(() => orderAlias.Id).Desc
				.Take(1).List();
			return queryResult.FirstOrDefault();
		}

		public static IList<VodovozOrder> GetCurrentOrders(IUnitOfWork UoW, Counterparty counterparty)
		{
			VodovozOrder orderAlias = null;
			return UoW.Session.QueryOver<VodovozOrder>(() => orderAlias)
				.Where(() => orderAlias.Client.Id == counterparty.Id)
				.Where(() => orderAlias.DeliveryDate >= DateTime.Today)
				.Where(() => orderAlias.OrderStatus != OrderStatus.Closed 
					&& orderAlias.OrderStatus != OrderStatus.Canceled
					&& orderAlias.OrderStatus != OrderStatus.DeliveryCanceled
					&& orderAlias.OrderStatus != OrderStatus.NotDelivered)
				.List();
		}

		public static IList<VodovozOrder> GetCompleteOrdersBetweenDates(IUnitOfWork UoW, DateTime startDate, DateTime endDate)
		{
			VodovozOrder orderAlias = null;
			return UoW.Session.QueryOver<VodovozOrder>(() => orderAlias)
				.Where(() => orderAlias.OrderStatus == OrderStatus.Closed)
				.Where(() => startDate <= orderAlias.DeliveryDate && orderAlias.DeliveryDate <= endDate).List();
		}

		public static IList<VodovozOrder> GetOrdersBetweenDates(IUnitOfWork UoW, DateTime startDate, DateTime endDate)
		{
			VodovozOrder orderAlias = null;
			return UoW.Session.QueryOver<VodovozOrder>(() => orderAlias)
				.Where(() => startDate <= orderAlias.DeliveryDate && orderAlias.DeliveryDate <= endDate).List();
		}

		public static IList<VodovozOrder> GetOrdersByCode1c (IUnitOfWork uow, string[] codes1c)
		{
			return uow.Session.QueryOver<VodovozOrder> ()
				.Where(c => c.Code1c.IsIn(codes1c))
				.List<VodovozOrder> ();
		}

		/// <summary>
		/// Получить самый последний заказ с водой в номенклатуре.
		/// </summary>
		/// <returns>Самый последний заказ с водой в номенклатуре, если такого нет - null.</returns>
		/// <param name="UoW">IUnitOfWork.</param>
		/// <param name="deliveryPoint">Точка доставки.</param>
		public static VodovozOrder GetLatestWaterOrderForDeliveryPoint(IUnitOfWork UoW, DeliveryPoint deliveryPoint)
		{
			VodovozOrder orderAlias = null;
			var queryResult = UoW.Session.QueryOver<VodovozOrder>(() => orderAlias)
								 .Where(() => orderAlias.DeliveryPoint.Id == deliveryPoint.Id)
			                     .OrderBy(() => orderAlias.Id).Desc
								 .List();
			
			foreach(VodovozOrder order in queryResult)
			{
				foreach(OrderItem item in order.OrderItems)
				{
					if(item.Nomenclature.Category == NomenclatureCategory.water)
					{
						return order;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Получить долг по бутылкам.
		/// </summary>
		/// <returns>Количество бутылок.</returns>
		/// <param name="UoW">IUnitOfWork.</param>
		/// <param name="deliveryPoint">Точка доставки.</param>
		public static int GetDebtBottlesForDeliveryPoint(IUnitOfWork UoW, DeliveryPoint deliveryPoint)
		{
			//VodovozOrder orderAlias = null;
			//var queryResult = UoW.Session.QueryOver<VodovozOrder>(() => orderAlias)
			//					 .Where(() => orderAlias.DeliveryPoint.Id == deliveryPoint.Id)
			//					 .OrderBy(() => orderAlias.Id).Desc
			//					 .List();

			//foreach(VodovozOrder order in queryResult) {
			//	foreach(OrderItem item in order.OrderItems) {
			//		if(item.Nomenclature.Category == NomenclatureCategory.water) {
			//			return order;
			//		}
			//	}
			//}

			return 0;
		}

		/// <summary>
		/// Получить самый последний заказ с водой в номенклатуре.
		/// </summary>
		/// <returns>Самый последний заказ с водой в номенклатуре, если такого нет - null.</returns>
		/// <param name="UoW">IUnitOfWork.</param>
		/// <param name="deliveryPoint">Точка доставки.</param>
		public static VodovozOrder GetLatestSanitizationForDeliveryPoint(IUnitOfWork UoW, DeliveryPoint deliveryPoint)
		{
			VodovozOrder orderAlias = null;
			var queryResult = UoW.Session.QueryOver<VodovozOrder>(() => orderAlias)
								 .Where(() => orderAlias.DeliveryPoint.Id == deliveryPoint.Id)
								 .OrderBy(() => orderAlias.Id).Desc
								 .List();

			foreach(VodovozOrder order in queryResult) {
				foreach(OrderItem item in order.OrderItems) {
					if(item.Nomenclature.Category == NomenclatureCategory.service) {
						return order;
					}
				}
			}

			return null;
		}
	}
}

