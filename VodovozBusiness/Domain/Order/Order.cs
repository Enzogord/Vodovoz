﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Gamma.Utilities;
using NHibernate.Util;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.HistoryLog;
using QSOrmProject;
using QSProjectsLib;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Documents;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Service;
using Vodovoz.Repositories;
using Vodovoz.Repositories.Client;
using Vodovoz.Repository;
using Vodovoz.Repository.Client;
using Vodovoz.Repository.Logistics;

namespace Vodovoz.Domain.Orders
{

	[OrmSubject(Gender = GrammaticalGender.Masculine,
		NominativePlural = "заказы",
		Nominative = "заказ",
		Prepositional = "заказе",
		PrepositionalPlural = "заказах"
	)]
	[HistoryTrace]
	public class Order : BusinessObjectBase<Order>, IDomainObject, IValidatableObject
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		#region Cвойства

		public virtual int Id { get; set; }

		DateTime version;
		[Display(Name = "Версия")]
		public virtual DateTime Version {
			get { return version; }
			set { SetField(ref version, value, () => Version); }
		}

		DateTime? createDate;
		[Display(Name = "Дата создания")]
		public virtual DateTime? CreateDate {
			get { return createDate; }
			set { SetField(ref createDate, value, () => CreateDate); }
		}

		bool isFirstOrder;
		[Display(Name = "Первый заказ")]
		public virtual bool IsFirstOrder {
			get { return isFirstOrder; }
			set { SetField(ref isFirstOrder, value, () => IsFirstOrder); }
		}

		OrderStatus orderStatus;

		[Display(Name = "Статус заказа")]
		public virtual OrderStatus OrderStatus {
			get { return orderStatus; }
			set { SetField(ref orderStatus, value, () => OrderStatus); }
		}

		Employee author;

		[Display(Name = "Создатель заказа")]
		[IgnoreHistoryTrace]
		public virtual Employee Author {
			get { return author; }
			set { SetField(ref author, value, () => Author); }
		}

		Counterparty client;

		[Display(Name = "Клиент")]
		public virtual Counterparty Client {
			get { return client; }
			set {
				if(value == client)
					return;
				if(OrderRepository.GetOnClosingOrderStatuses().Contains(OrderStatus)) {
					OnChangeCounterparty(value);
				} else if(client != null && !CanChangeContractor()) {
					throw new InvalidOperationException("Нельзя изменить клиента для заполненного заказа.");
				}
				SetField(ref client, value, () => Client);
				if(DeliveryPoint != null && NHibernate.NHibernateUtil.IsInitialized(Client.DeliveryPoints) && !Client.DeliveryPoints.Any(d => d.Id == DeliveryPoint.Id)) {
					//FIXME Убрать когда поймем что проблемы с пропаданием точек доставки нет.
					logger.Warn("Очищаем точку доставки, при установке клиента. Возможно это не нужно.");
					DeliveryPoint = null;
				}
			}
		}

		DeliveryPoint deliveryPoint;

		[Display(Name = "Точка доставки")]
		public virtual DeliveryPoint DeliveryPoint {
			get { return deliveryPoint; }
			set {
				//Для изменения уже закрытого или завершенного заказа из закртытия МЛ
				if(OrderRepository.GetOnClosingOrderStatuses().Contains(OrderStatus)
				   //чтобы не обрабатывались действия при изменении только точки доставки
				   //когда меняется клиент (так как вместе с ним обязательно будет менять еще и точка доставки)
				   && deliveryPoint != null && Client.DeliveryPoints.Any(d => d.Id == deliveryPoint.Id)) {
					OnChangeDeliveryPoint();
				}

				SetField(ref deliveryPoint, value, () => DeliveryPoint);
				if(value != null && DeliverySchedule == null) {
					DeliverySchedule = value.DeliverySchedule;
				}
			}
		}

		DateTime? deliveryDate;

		[Display(Name = "Дата доставки")]
		[HistoryDateOnly]
		public virtual DateTime? DeliveryDate {
			get { return deliveryDate; }
			set {
				SetField(ref deliveryDate, value, () => DeliveryDate);
				if(NHibernate.NHibernateUtil.IsInitialized(OrderDocuments) && value.HasValue) {
					foreach(OrderDocument document in OrderDocuments) {
						if(document.Type == OrderDocumentType.AdditionalAgreement) {
							(document as OrderAgreement).AdditionalAgreement.IssueDate = value.Value;
							(document as OrderAgreement).AdditionalAgreement.StartDate = value.Value;
						}
						//TODO FIXME Когда сделаю добавление документов для печати - фильтровать их здесь и не менять им дату.
					}
				}
			}
		}

		DateTime billDate = DateTime.Now;

		[Display(Name = "Дата счета")]
		[HistoryDateOnly]
		public virtual DateTime BillDate {
			get { return billDate; }
			set { SetField(ref billDate, value, () => BillDate); }
		}

		DeliverySchedule deliverySchedule;

		[Display(Name = "Время доставки")]
		public virtual DeliverySchedule DeliverySchedule {
			get { return deliverySchedule; }
			set { SetField(ref deliverySchedule, value, () => DeliverySchedule); }
		}

		private string deliverySchedule1c;

		[Display(Name = "Время доставки из 1С")]
		public virtual string DeliverySchedule1c {
			get {
				return string.IsNullOrWhiteSpace(deliverySchedule1c)
				  ? "Время доставки из 1С не загружено"
				  : deliverySchedule1c;
			}
			set { SetField(ref deliverySchedule1c, value, () => DeliverySchedule1c); }
		}

		bool selfDelivery;

		[Display(Name = "Самовывоз")]
		public virtual bool SelfDelivery {
			get { return selfDelivery; }
			set { SetField(ref selfDelivery, value, () => SelfDelivery); }
		}

		Order previousOrder;

		[Display(Name = "Предыдущий заказ")]
		public virtual Order PreviousOrder {
			get { return previousOrder; }
			set { SetField(ref previousOrder, value, () => PreviousOrder); }
		}

		int? bottlesReturn;

		[Display(Name = "Бутылей на возврат")]
		public virtual int? BottlesReturn {
			get { return bottlesReturn; }
			set { SetField(ref bottlesReturn, value, () => BottlesReturn); }
		}

		string comment;

		[Display(Name = "Комментарий")]
		public virtual string Comment {
			get { return comment; }
			set { SetField(ref comment, value, () => Comment); }
		}

		string commentLogist;

		[Display(Name = "Комментарий логиста")]
		public virtual string CommentLogist {
			get { return commentLogist; }
			set { SetField(ref commentLogist, value, () => CommentLogist); }
		}

		string clientPhone;

		[Display(Name = "Номер телефона")]
		public virtual string ClientPhone {
			get { return clientPhone; }
			set { SetField(ref clientPhone, value, () => ClientPhone); }
		}

		OrderSignatureType? signatureType;

		[Display(Name = "Подписание документов")]
		public virtual OrderSignatureType? SignatureType {
			get { return signatureType; }
			set { SetField(ref signatureType, value, () => SignatureType); }
		}

		private Decimal extraMoney;

		[Display(Name = "Доплата\\Переплата")]
		[PropertyChangedAlso(nameof(SumToReceive))]
		public virtual Decimal ExtraMoney {
			get { return extraMoney; }
			set { SetField(ref extraMoney, value, () => ExtraMoney); }
		}

		[Display(Name = "Наличных к получению")]
		public virtual Decimal SumToReceive {
			get {
				decimal money = TotalSum;
				if(OrderRepository.GetStatusesForActualCount().Contains(OrderStatus))
					money = ActualTotalSum;
				return (PaymentType == PaymentType.cash || PaymentType == PaymentType.BeveragesWorld) ? money + ExtraMoney : 0;
			}
			protected set {; }
		}

		string sumDifferenceReason;

		[Display(Name = "Причина переплаты/недоплаты")]
		public virtual string SumDifferenceReason {
			get { return sumDifferenceReason; }
			set { SetField(ref sumDifferenceReason, value, () => SumDifferenceReason); }
		}

		bool shipped;

		[Display(Name = "Отгружено по платежке")]
		public virtual bool Shipped {
			get { return shipped; }
			set { SetField(ref shipped, value, () => Shipped); }
		}

		PaymentType paymentType;

		[Display(Name = "Форма оплаты")]
		public virtual PaymentType PaymentType {
			get { return paymentType; }
			set {
				if(value == paymentType)
					return;
				//Для изменения уже закрытого или завершенного заказа из закртытия МЛ
				if(Client != null && OrderRepository.GetOnClosingOrderStatuses().Contains(OrderStatus)) {
					OnChangePaymentType();
				}
				SetField(ref paymentType, value, () => PaymentType);
			}
		}

		CounterpartyContract contract;

		[Display(Name = "Договор")]
		public virtual CounterpartyContract Contract {
			get { return contract; }
			set { SetField(ref contract, value, () => Contract); }
		}

		MoneyMovementOperation moneyMovementOperation;
		[IgnoreHistoryTrace]
		public virtual MoneyMovementOperation MoneyMovementOperation {
			get { return moneyMovementOperation; }
			set {
				SetField(ref moneyMovementOperation, value, () => MoneyMovementOperation);
			}
		}

		BottlesMovementOperation bottlesMovementOperation;
		[IgnoreHistoryTrace]
		public virtual BottlesMovementOperation BottlesMovementOperation {
			get {
				return bottlesMovementOperation;
			}
			set {
				SetField(ref bottlesMovementOperation, value, () => BottlesMovementOperation);
			}
		}

		IList<DepositOperation> depositOperations;

		public virtual IList<DepositOperation> DepositOperations {
			get { return depositOperations; }
			set { SetField(ref depositOperations, value, () => DepositOperations); }
		}

		bool collectBottles;

		public virtual bool CollectBottles {
			get {
				return collectBottles;
			}
			set {
				SetField(ref collectBottles, value, () => CollectBottles);
			}
		}

		DefaultDocumentType? documentType;

		[Display(Name = "Тип безналичных документов")]
		public virtual DefaultDocumentType? DocumentType {
			get { return documentType; }
			set { SetField(ref documentType, value, () => DocumentType); }
		}

		private string code1c;

		[Display(Name = "Код 1С")]
		public virtual string Code1c {
			get { return code1c; }
			set { SetField(ref code1c, value, () => Code1c); }
		}

		private string address1c;

		[Display(Name = "Адрес 1С")]
		public virtual string Address1c {
			get { return address1c; }
			set { SetField(ref address1c, value, () => Address1c); }
		}

		private string address1cCode;

		[Display(Name = "Код адреса 1С")]
		public virtual string Address1cCode {
			get { return address1cCode; }
			set { SetField(ref address1cCode, value, () => Address1cCode); }
		}

		private string toClientText;

		[Display(Name = "Оборудование к клиенту")]
		public virtual string ToClientText {
			get { return toClientText; }
			set { SetField(ref toClientText, value, () => ToClientText); }
		}

		private string fromClientText;

		[Display(Name = "Оборудование от клиента")]
		public virtual string FromClientText {
			get { return fromClientText; }
			set { SetField(ref fromClientText, value, () => FromClientText); }
		}


		[Display(Name = "Колонка МЛ от клиента")]
		public virtual string EquipmentsFromClient {
			get {
				string result = "";
				List<OrderEquipment> equipments = OrderEquipments.Where(x => x.Direction == Direction.PickUp).ToList();
				foreach(var equip in equipments) {
					result += equip.NameString;
					result += " " + equip.Count.ToString() + "шт.";
					if(equip != equipments.Last()) {
						result += ", ";
					}
				}
				return result;
			}
		}

		[Display(Name = "Колонка МЛ к клиенту")]
		public virtual string EquipmentsToClient {
			get {
				string result = "";
				List<OrderEquipment> equipments = OrderEquipments.Where(x => x.Direction == Direction.Deliver).ToList();
				foreach(var equip in equipments) {
					result += equip.NameString;
					result += " " + equip.Count.ToString() + "шт.";
					if(equip != equipments.Last()) {
						result += ", ";
					}
				}
				return result;
			}
		}

		private int? dailyNumber;

		/// <summary>
		/// Уникапльный номер в передлах одного дня.
		/// ВАЖНО! Номер генерируется и изменяется на стороне БД
		/// </summary>
		[Display(Name = "Ежедневный номер")]
		public virtual int? DailyNumber {
			get { return dailyNumber; }
			set { SetField(ref dailyNumber, value, () => DailyNumber); }
		}

		Employee lastEditor;

		[Display(Name = "Последний редактор")]
		[IgnoreHistoryTrace]
		public virtual Employee LastEditor {
			get { return lastEditor; }
			set { SetField(ref lastEditor, value, () => LastEditor); }
		}

		DateTime lastEditedTime;

		[Display(Name = "Последние изменения")]
		[IgnoreHistoryTrace]
		public virtual DateTime LastEditedTime {
			get { return lastEditedTime; }
			set { SetField(ref lastEditedTime, value, () => LastEditedTime); }
		}

		string commentManager;
		/// <summary>
		/// Комментарий менеджера ответственного за водительский телефон
		/// </summary>
		[Display(Name = "Комментарий менеджера")]
		public virtual string CommentManager {
			get { return commentManager; }
			set { SetField(ref commentManager, value, () => CommentManager); }
		}

		int? returnedTare;

		[Display(Name = "Возвратная тара")]
		public virtual int? ReturnedTare {
			get { return returnedTare; }
			set { SetField(ref returnedTare, value, () => ReturnedTare); }
		}

		string informationOnTara;

		[Display(Name = "Информация о таре")]
		public virtual string InformationOnTara {
			get { return informationOnTara; }
			set { SetField(ref informationOnTara, value, () => InformationOnTara); }
		}

		string onRouteEditReason;

		[Display(Name = "Причина редактирования заказа")]
		public virtual string OnRouteEditReason {
			get { return onRouteEditReason; }
			set { SetField(ref onRouteEditReason, value, () => OnRouteEditReason); }
		}

		ReasonType resonType;

		[Display(Name = "Тип причины")]
		public virtual ReasonType ReasonType {
			get { return resonType; }
			set { SetField(ref resonType, value, () => ReasonType); }
		}

		DriverCallType driverCallType;

		[Display(Name = "Водитель отзвонился")]
		public virtual DriverCallType DriverCallType {
			get { return driverCallType; }
			set { SetField(ref driverCallType, value, () => DriverCallType); }
		}

		int? driverCallId;

		[Display(Name = "Номер звонка водителя")]
		public virtual int? DriverCallId {
			get { return driverCallId; }
			set { SetField(ref driverCallId, value, () => DriverCallId); }
		}

		bool isService;

		[Display(Name = "Сервисное обслуживание")]
		public virtual bool IsService {
			get { return isService; }
			set { SetField(ref isService, value, () => IsService); }
		}

		int? trifle;

		[Display(Name = "Сдача")]
		public virtual int? Trifle {
			get { return trifle; }
			set { SetField(ref trifle, value, () => Trifle); }
		}

		private int? onlineOrder;

		[Display(Name = "Номер онлайн заказа")]
		public virtual int? OnlineOrder {
			get { return onlineOrder; }
			set { SetField(ref onlineOrder, value, () => OnlineOrder); }
		}

		private bool isContractCloser;

		[Display(Name = "Заказ - закрывашка по контракту?")]
		public virtual bool IsContractCloser {
			get { return isContractCloser; }
			set { SetField(ref isContractCloser, value, () => IsContractCloser); }
		}

		#endregion

		public virtual bool CanChangeContractor()
		{
			if((NHibernate.NHibernateUtil.IsInitialized(OrderDocuments) && OrderDocuments.Count > 0) ||
				(NHibernate.NHibernateUtil.IsInitialized(InitialOrderService) && InitialOrderService.Count > 0) ||
				(NHibernate.NHibernateUtil.IsInitialized(FinalOrderService) && FinalOrderService.Count > 0))
				return false;
			return true;
		}

		IList<OrderDepositItem> orderDepositItems = new List<OrderDepositItem>();

		[Display(Name = "Залоги заказа")]
		public virtual IList<OrderDepositItem> OrderDepositItems {
			get { return orderDepositItems; }
			set { SetField(ref orderDepositItems, value, () => OrderDepositItems); }
		}

		GenericObservableList<OrderDepositItem> observableOrderDepositItems;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderDepositItem> ObservableOrderDepositItems {
			get {
				if(observableOrderDepositItems == null) {
					observableOrderDepositItems = new GenericObservableList<OrderDepositItem>(OrderDepositItems);
					observableOrderDepositItems.ListContentChanged += ObservableOrderDepositItems_ListContentChanged;
				}
				return observableOrderDepositItems;
			}
		}

		IList<OrderDocument> orderDocuments = new List<OrderDocument>();

		[Display(Name = "Документы заказа")]
		public virtual IList<OrderDocument> OrderDocuments {
			get { return orderDocuments; }
			set { SetField(ref orderDocuments, value, () => OrderDocuments); }
		}

		GenericObservableList<OrderDocument> observableOrderDocuments;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderDocument> ObservableOrderDocuments {
			get {
				if(observableOrderDocuments == null)
					observableOrderDocuments = new GenericObservableList<OrderDocument>(OrderDocuments);
				return observableOrderDocuments;
			}
		}

		IList<OrderItem> orderItems = new List<OrderItem>();

		[Display(Name = "Строки заказа")]
		public virtual IList<OrderItem> OrderItems {
			get { return orderItems; }
			set { SetField(ref orderItems, value, () => OrderItems); }
		}

		GenericObservableList<OrderItem> observableOrderItems;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderItem> ObservableOrderItems {
			get {
				if(observableOrderItems == null) {
					observableOrderItems = new GenericObservableList<OrderItem>(orderItems);
					observableOrderItems.ListContentChanged += ObservableOrderItems_ListContentChanged;
				}

				return observableOrderItems;
			}
		}

		IList<OrderEquipment> orderEquipments = new List<OrderEquipment>();

		[Display(Name = "Список оборудования")]
		public virtual IList<OrderEquipment> OrderEquipments {
			get { return orderEquipments; }
			set { SetField(ref orderEquipments, value, () => OrderEquipments); }
		}

		GenericObservableList<OrderEquipment> observableOrderEquipments;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderEquipment> ObservableOrderEquipments {
			get {
				if(observableOrderEquipments == null)
					observableOrderEquipments = new GenericObservableList<OrderEquipment>(orderEquipments);
				return observableOrderEquipments;
			}
		}

		IList<ServiceClaim> initialOrderService = new List<ServiceClaim>();

		[Display(Name = "Список заявок на сервис")]
		public virtual IList<ServiceClaim> InitialOrderService {
			get { return initialOrderService; }
			set { SetField(ref initialOrderService, value, () => InitialOrderService); }
		}

		GenericObservableList<ServiceClaim> observableInitialOrderService;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<ServiceClaim> ObservableInitialOrderService {
			get {
				if(observableInitialOrderService == null)
					observableInitialOrderService = new GenericObservableList<ServiceClaim>(InitialOrderService);
				return observableInitialOrderService;
			}
		}

		IList<ServiceClaim> finalOrderService = new List<ServiceClaim>();

		[Display(Name = "Список заявок на сервис")]
		public virtual IList<ServiceClaim> FinalOrderService {
			get { return finalOrderService; }
			set { SetField(ref finalOrderService, value, () => FinalOrderService); }
		}

		GenericObservableList<ServiceClaim> observableFinalOrderService;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<ServiceClaim> ObservableFinalOrderService {
			get {
				if(observableFinalOrderService == null)
					observableFinalOrderService = new GenericObservableList<ServiceClaim>(FinalOrderService);
				return observableFinalOrderService;
			}
		}


		public Order()
		{
			Comment = String.Empty;
			OrderStatus = OrderStatus.NewOrder;
			SumDifferenceReason = String.Empty;
			ClientPhone = String.Empty;
		}

		public static Order CreateFromServiceClaim(ServiceClaim service, Employee author)
		{
			var order = new Order();
			order.client = service.Counterparty;
			order.DeliveryPoint = service.DeliveryPoint;
			order.DeliveryDate = service.ServiceStartDate;
			order.PaymentType = service.Payment;
			order.Author = author;
			service.InitialOrder = order;
			order.AddServiceClaimAsInitial(service);
			return order;
		}

		#region IValidatableObject implementation

		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if(validationContext.Items.ContainsKey("NewStatus")) {
				OrderStatus newStatus = (OrderStatus)validationContext.Items["NewStatus"];
				if(newStatus == OrderStatus.Accepted || newStatus == OrderStatus.WaitForPayment) {
					var key = new OrderStateKey(this, newStatus);
					var messages = new List<string>();
					if(!OrderAcceptProhibitionRulesRepository.CanAcceptOrder(key, ref messages)) {
						foreach(var msg in messages) {
							yield return new ValidationResult(msg);
						}
					}

					if(DeliveryDate == null || DeliveryDate == default(DateTime))
						yield return new ValidationResult("В заказе не указана дата доставки.",
							new[] { this.GetPropertyName(o => o.DeliveryDate) });
					if(!SelfDelivery && DeliverySchedule == null)
						yield return new ValidationResult("В заказе не указано время доставки.",
							new[] { this.GetPropertyName(o => o.DeliverySchedule) });

					if(!IsLoadedFrom1C && PaymentType == PaymentType.cashless && Client.TypeOfOwnership != "ИП" && !SignatureType.HasValue)
						yield return new ValidationResult("В заказе не указано как будут подписаны документы.",
							new[] { this.GetPropertyName(o => o.SignatureType) });

					if(!IsLoadedFrom1C && bottlesReturn == null && this.OrderItems.Any(x => x.Nomenclature.Category == NomenclatureCategory.water))
						yield return new ValidationResult("В заказе не указана планируемая тара.",
							new[] { this.GetPropertyName(o => o.Contract) });
					if(!IsLoadedFrom1C && trifle == null && (PaymentType == PaymentType.cash || PaymentType == PaymentType.BeveragesWorld) && this.TotalSum > 0m)
						yield return new ValidationResult("В заказе не указана сдача.",
							new[] { this.GetPropertyName(o => o.Trifle) });
					if(ObservableOrderItems.Any(x => x.Count <= 0) || ObservableOrderEquipments.Any(x => x.Count <= 0))
						yield return new ValidationResult("В заказе должно быть указано количество во всех позициях товара и оборудования");
					//если ни у точки доставки, ни у контрагента нет ни одного номера телефона
					if(!IsLoadedFrom1C && !((DeliveryPoint != null && DeliveryPoint.Phones.Any()) || Client.Phones.Any()))
						yield return new ValidationResult("Ни для контрагента, ни для точки доставки заказа не указано ни одного номера телефона.");
					
					if(!IsLoadedFrom1C && DeliveryPoint != null){
						if(string.IsNullOrWhiteSpace(DeliveryPoint.Entrance)) {
							yield return new ValidationResult("Не заполнена парадная в точке доставки");
						}
						if(string.IsNullOrWhiteSpace(DeliveryPoint.Floor)) {
							yield return new ValidationResult("Не заполнен этаж в точке доставки");
						}
						if(string.IsNullOrWhiteSpace(DeliveryPoint.Room)) {
							yield return new ValidationResult("Не заполнен номер помещения в точке доставки");
						}
					}

					//В случае, если редактируется заказ "В пути", то должен быть оставлен комментарий, поясняющий причину редактирования.
					//Если заказ "В пути" редактируется больше одного раза, то комментарий должен отличаться от предыдущего.
					var order = UnitOfWorkFactory.CreateWithoutRoot().GetById<Order>(Id);
					if(OrderStatus == OrderStatus.OnTheWay && 
					   (order == null && OnRouteEditReason == null
					    || order != null && order.OnRouteEditReason == OnRouteEditReason))
						yield return new ValidationResult("При изменении заказа в статусе 'В пути' необходимо указывать причину редактирования",
														  new[] { this.GetPropertyName(o => o.OnRouteEditReason) });
					// Проверка соответствия цен в заказе ценам в номенклатуре
					string priceResult = "В заказе неверно указаны цены на следующие товары:\n";
					List<string> incorrectPriceItems = new List<string>();
					foreach(OrderItem item in ObservableOrderItems) {
						decimal fixedPrice = GetFixedPrice(item);
						decimal nomenclaturePrice = GetNomenclaturePrice(item);
						if(fixedPrice > 0m) {
							if(item.Price < fixedPrice) {
								incorrectPriceItems.Add(String.Format("{0} - цена: {1}, должна быть: {2}\n",
																	  item.NomenclatureString,
																	  item.Price,
																	  fixedPrice));
							}
						} else if(nomenclaturePrice > default(decimal) && item.Price < nomenclaturePrice) {
							incorrectPriceItems.Add(String.Format("{0} - цена: {1}, должна быть: {2}\n",
																  item.NomenclatureString,
																  item.Price,
																  nomenclaturePrice));
						}
					}
					if(incorrectPriceItems.Any()) {
						foreach(string item in incorrectPriceItems) {
							priceResult += item;
						}
						yield return new ValidationResult(priceResult);
					}
					// Конец проверки цен

					//создание нескольких заказов на одну дату и точку доставки
					if(!SelfDelivery && DeliveryPoint != null) {
						var ordersForDeliveryPoints = OrderRepository.GetLatestOrdersForDeliveryPoint(UoW, DeliveryPoint)
																	 .Where(
							                                             o => o.Id != Id
							                                             && o.DeliveryDate == DeliveryDate
							                                             && !OrderRepository.GetGrantedStatusesToCreateSeveralOrders().Contains(o.OrderStatus)
							                                             && !o.IsService
							                                            );

						bool hasMaster = ObservableOrderItems.Any(i => i.Nomenclature.Category == NomenclatureCategory.master);

						if(!hasMaster
						   && !QSMain.User.Permissions["can_create_several_orders_for_date_and_deliv_point"]
						   && ordersForDeliveryPoints.Any()
						   && validationContext.Items.ContainsKey("IsCopiedFromUndelivery") && !(bool)validationContext.Items["IsCopiedFromUndelivery"]) {
							yield return new ValidationResult(
								String.Format("Создать заказ нельзя, т.к. для этой даты и точки доставки уже создан заказ №{0}", ordersForDeliveryPoints.First().Id),
								new[] { this.GetPropertyName(o => o.OrderEquipments) });
						}
					}
				}

				if(newStatus == OrderStatus.Closed) {
					foreach(var equipment in OrderEquipments.Where(x => x.Direction == Direction.PickUp)) {
						if(!equipment.Confirmed && String.IsNullOrWhiteSpace(equipment.ConfirmedComment))
							yield return new ValidationResult(
								String.Format("Забор оборудования {0} по заказу {1} не произведен, а в комментарии не указана причина.",
									equipment.NameString, Id),
								new[] { this.GetPropertyName(o => o.OrderEquipments) });
					}
				}

				if(IsService && PaymentType == PaymentType.cashless
				   && newStatus == OrderStatus.Accepted 
				   && !QSMain.User.Permissions["can_accept_cashles_service_orders"]) {
					yield return new ValidationResult(
						"Недостаточно прав для подтверждения безнального сервисного заказа. Обратитесь к руководителю.",
						new[] { this.GetPropertyName(o => o.OrderStatus) }
					);
				}
			}

			if(ObservableOrderItems.Any(x => x.Discount > 0 && x.DiscountReason == null))
				yield return new ValidationResult("Если в заказе указана скидка на товар, то обязательно должно быть заполнено поле 'Основание'.");

			if(DeliveryDate == null || DeliveryDate == default(DateTime))
				yield return new ValidationResult("В заказе не указана дата доставки.",
					new[] { this.GetPropertyName(o => o.DeliveryDate) });
			if(!SelfDelivery && DeliveryPoint == null)
				yield return new ValidationResult("В заказе необходимо заполнить точку доставки.",
					new[] { this.GetPropertyName(o => o.DeliveryPoint) });
			if(Client == null)
				yield return new ValidationResult("В заказе необходимо заполнить поле \"клиент\".",
					new[] { this.GetPropertyName(o => o.Client) });

			if(PaymentType == PaymentType.ByCard && OnlineOrder == null)
				yield return new ValidationResult("Если в заказе выбран тип оплаты по карте, необходимо заполнить номер онлайн заказа.",
												  new[] { this.GetPropertyName(o => o.OnlineOrder) });

			if(ObservableOrderEquipments
			   .Where(x => x.Nomenclature.Category == NomenclatureCategory.equipment)
			   .Where(x => x.OwnType == OwnTypes.None)
			   .Any()
			  )
				yield return new ValidationResult("У оборудования в заказе должна быть выбрана принадлежность.");

			if(ObservableOrderEquipments
			   .Where(x => x.Nomenclature.Category == NomenclatureCategory.equipment)
			   .Where(x => x.DirectionReason == DirectionReason.None && x.OwnType != OwnTypes.Duty)
			   .Where(x => x.Nomenclature?.SubTypeOfEquipmentCategory != SubtypeOfEquipmentCategory.forSale)
			   .Any()
			  )
				yield return new ValidationResult("У оборудования в заказе должна быть указана причина забор-доставки.");

			if(ObservableOrderDepositItems.Any(x => x.Total < 0)) {
				yield return new ValidationResult("В возврате залогов в заказе необходимо вводить положительную сумму.");
			}

			if(!IsLoadedFrom1C && ObservableOrderItems.Any(x => x.Nomenclature.Category == NomenclatureCategory.water) &&
			   //Если нет ни одного допсоглашения на воду подходящего на точку доставку в заказе 
			   //(или без точки доставки если относится на все точки)
			   !HaveActualWaterSaleAgreementByDeliveryPoint() ) {
				yield return new ValidationResult("В заказе выбрана точка доставки для которой нет актуального дополнительного соглашения по доставке воды");
			}

			if(!QSMain.User.Permissions["can_can_create_order_in_advance"]
			   && DeliveryDate.HasValue && DeliveryDate.Value < DateTime.Today
			   && OrderStatus <= OrderStatus.Accepted) {
				yield return new ValidationResult(
					"Указана дата заказа более ранняя чем сегодняшняя. Укажите правильную дату доставки.",
					new[] { this.GetPropertyName(o => o.DeliveryDate) }
				);
			}
		}

		#endregion

		#region Вычисляемые

		public virtual bool IsLoadedFrom1C
		{
			get{
				return !string.IsNullOrEmpty(Code1c);
			}
		}

		public override string ToString()
		{
			return String.Format("Заказ №{0}({1})", Id, Code1c);
		}

		public virtual string Title {
			get { return String.Format("Заказ №{0} от {1:d}", Id, DeliveryDate); }
		}

		public virtual int TotalDeliveredBottles {
			get {
				return OrderItems.Where(x => x.Nomenclature.Category == NomenclatureCategory.water).Sum(x => x.Count);
			}
		}

		public virtual int TotalDeliveredBottlesSix {
			get {
				return OrderItems.Where(x => x.Nomenclature.Category == NomenclatureCategory.disposableBottleWater && x.Nomenclature.Weight > 5).Sum(x => x.Count);
			}
		}

		public virtual int TotalDeliveredBottlesSmall {
			get {
				return OrderItems.Where(x => x.Nomenclature.Category == NomenclatureCategory.disposableBottleWater && x.Nomenclature.Weight <= 5).Sum(x => x.Count);
			}
		}

		public virtual int TotalWeight {
			get {
				return (int)OrderItems.Sum(x => x.Count * x.Nomenclature.Weight);
			}
		}

		public virtual string RowColor { get { return PreviousOrder == null ? "black" : "red"; } }

		[PropertyChangedAlso(nameof(SumToReceive))]
		public virtual decimal TotalSum {
			get {
				Decimal sum = 0;
				foreach(OrderItem item in ObservableOrderItems) {
					sum += item.Price * item.Count - item.DiscountMoney;
				}
				foreach(OrderDepositItem dep in ObservableOrderDepositItems) {
					if(dep.PaymentDirection == PaymentDirection.ToClient)
						sum -= dep.Deposit * dep.Count;
				}
				return sum;
			}
		}

		public virtual decimal ActualTotalSum {
			get {
				Decimal sum = 0;
				foreach(OrderItem item in ObservableOrderItems) {
					sum += item.Price * item.ActualCount - item.DiscountMoney;
				}
				foreach(OrderDepositItem dep in ObservableOrderDepositItems) {
					if(dep.PaymentDirection == PaymentDirection.ToClient)
						sum -= dep.Deposit * dep.Count;
				}
				return sum;
			}
		}

		public virtual decimal MoneyForMaster =>
		ObservableOrderItems.Where(i => i.Nomenclature.Category == NomenclatureCategory.master)
							.Sum(i => (decimal)i.Nomenclature.PercentForMaster / 100 * i.ActualCount * i.Price);

		public virtual decimal ActualGoodsTotalSum =>
		OrderItems.Sum(item => item.Price * item.ActualCount - item.DiscountMoney);

		/// <summary>
		/// Количество 19л бутылей в заказе
		/// </summary>
		public virtual int TotalWaterBottles {
			get {
				return OrderItems.Where(x => x.Nomenclature.Category == NomenclatureCategory.water).Sum(x => x.Count);
			}
		}

		public virtual bool CanBeMovedFromClosedToAcepted => RouteListItemRepository.WasOrderInAnyRouteList(UoW, this)
		                                                     	&& QSMain.User.Permissions["can_move_order_from_closed_to_acepted"];
		
		#endregion

		#region Автосоздание договоров, допсоглашений при изменении подтвержденного заказа

		private void OnChangeCounterparty(Counterparty newClient)
		{
			if(newClient == null || Client == null || newClient.Id == Client.Id) {
				return;
			}

			Contract = FindOrCreateContract(newClient);
			OnChangeContract(true);
		}

		private void OnChangeContract(bool changedClient = false)
		{
			foreach(var item in ObservableOrderItems
					.Where(x => x.AdditionalAgreement != null)) {

				if(item.AdditionalAgreement.Self is WaterSalesAgreement) {
					var waterAgreement = Contract.GetWaterSalesAgreement(DeliveryPoint);
					if(waterAgreement == null) {
						waterAgreement = ClientDocumentsRepository.CreateDefaultWaterAgreement(UoW, DeliveryPoint, DeliveryDate, Contract);
						Contract.ObservableAdditionalAgreements.Add(waterAgreement);
					}
					item.AdditionalAgreement = waterAgreement;
				}

				if(item.AdditionalAgreement.Self is SalesEquipmentAgreement
				  || item.AdditionalAgreement.Self is NonfreeRentAgreement
				  || item.AdditionalAgreement.Self is DailyRentAgreement
				  || item.AdditionalAgreement.Self is FreeRentAgreement
				  ) {
					item.AdditionalAgreement.Self.Contract = Contract;
					if(changedClient && item.AdditionalAgreement.Self.DeliveryPoint != null) {
						item.AdditionalAgreement.Self.DeliveryPoint = DeliveryPoint;
					}
				}
			}
			UpdateDocuments();
		}

		private void OnChangePaymentType()
		{
			ChangeContractOnChangePaymentType();
			if(Contract == null) {
				Contract = FindOrCreateContract(Client);
			}
			OnChangeContract();
		}

		private void OnChangeDeliveryPoint()
		{
			foreach(OrderItem item in ObservableOrderItems.Where(x => x.AdditionalAgreement != null)) {
				//меняем только у тех соглашений у которых ранее была указана точка доставки
				if(item.AdditionalAgreement.Self.DeliveryPoint != null) {
					item.AdditionalAgreement.Self.DeliveryPoint = DeliveryPoint;
				}
			}

		}

		#endregion

		#region Функции

		public virtual QSContacts.Email GetEmailAddressForBill()
		{
			return Client.Emails.FirstOrDefault(x => x.EmailType == null || (x.EmailType.Name == "Для счетов"));
		}

		public virtual bool NeedSendBill()
		{
			if(OrderStatus != OrderStatus.Accepted && OrderStatus != OrderStatus.WaitForPayment) {
				return false;
			}
			var docType = OrderDocumentType.Bill;
			//Проверка должен ли формироваться счет для текущего заказа
			var docTypes = GetRequirementDocTypes();
			if(!docTypes.Contains(docType)) {
				return false;
			}
			return true;
		}

		public virtual void ParseTareReason()
		{
			ReasonType = ReasonType.Unknown;
			if(!string.IsNullOrWhiteSpace(Comment)) {
				if(Comment.ToUpper().Contains("НОВЫЙ АДРЕС"))
					ReasonType = ReasonType.NewAddress;
				if(Comment.ToUpper().Contains("УВЕЛИЧЕНИЕ ЗАКАЗА"))
					ReasonType = ReasonType.OrderIncrease;
				if(Comment.ToUpper().Contains("ПЕРВЫЙ ЗАКАЗ"))
					ReasonType = ReasonType.FirstOrder;
			}
		}

		public virtual void AddContractDocument(CounterpartyContract contract)
		{
			var orderDocuments = ObservableOrderDocuments;
			orderDocuments.Add(new OrderContract {
				Order = this,
				AttachedToOrder = this,
				Contract = contract
			});
		}

		public virtual void ChangeOrderContract()
		{
			if(Client == null || DeliveryPoint == null || DeliveryDate == null) {
				return;
			}
			var oldContract = Contract;

			//Нахождение подходящего существующего договора
			var actualContract = CounterpartyContractRepository.GetCounterpartyContractByPaymentType(UoW, Client, Client.PersonType, PaymentType);

			//Нахождение договора созданного в текущем заказе, 
			//который можно изменить под необходимые параметры
			var contractInOrder = OrderDocuments.OfType<OrderContract>()
												.Where(x => x.Order.Id == Id)
												.Select(x => x.Contract)
												.FirstOrDefault();

			//Поиск других заказов, в которых мог использоваться этот договор
			IList<Order> ordersByContract = null;
			bool otherOrdersForContractExist = true;
			if(contractInOrder != null) {
				ordersByContract = UoW.Session.QueryOver<Order>()
									  .Where(o => o.Contract.Id == contractInOrder.Id)
									  .List();
				otherOrdersForContractExist = ordersByContract.Any(o => o.Id != Id);

				Contract = contractInOrder;
			}

			//Изменение существующего договора под необходимые параметры
			//если он не был использован в других заказах
			if(
				actualContract == null
				&& !otherOrdersForContractExist
				&& OrderDocuments.OfType<OrderContract>().Any(
					x => x.Order.Id == Id
					&& x.Contract?.Id == Contract?.Id
				)
			){
				using(var uowContract = UnitOfWorkFactory.CreateForRoot<CounterpartyContract>(Contract.Id)){
					uowContract.Root.ContractType = DocTemplateRepository.GetContractTypeForPaymentType(Client.PersonType, PaymentType);
					uowContract.Root.Organization = OrganizationRepository.GetOrganizationByPaymentType(uowContract, Client.PersonType, PaymentType);
					uowContract.Save();
				}
				UoW.Session.Refresh(Contract);
				return;
			}

			if(actualContract == null) {
				actualContract = ClientDocumentsRepository.CreateDefaultContract(UoW, Client, PaymentType, DeliveryDate);
				Contract = actualContract;
				AddContractDocument(actualContract);
			}

			if(actualContract != oldContract) {
				Contract = actualContract;
				ChangeWaterAgreement(actualContract, oldContract);
				ChangeOrderBasedAgreements(actualContract);
			}
			UpdateDocuments();
		}

		/// <summary>
		/// При смене договора переделывает связанные с доп соглашением на воду
		/// все елементы в заказе на актуальное доп соглашение измененного договора.
		/// ВНИМАНИЕ! Использовать только при смене договора
		/// </summary>
		private void ChangeWaterAgreement(CounterpartyContract actualContract, CounterpartyContract oldContract)
		{
			if(oldContract == null) {
				return;
			}
			var waterCategories = Nomenclature.GetCategoriesRequirementForWaterAgreement();
			var oldWaterAgreement = oldContract.GetWaterSalesAgreement(DeliveryPoint);
			var waterItems = OrderItems.Where(x => waterCategories.Contains(x.Nomenclature.Category))
			                           .ToList();
			var waterDocuments = OrderDocuments.OfType<OrderAgreement>()
			                                   .Where(x => (x.AdditionalAgreement.Self as WaterSalesAgreement) == oldWaterAgreement)
			                                   .ToList();

			if(!HasWater()) {
				return;
			}

			var actualWaterAgreement = actualContract.GetWaterSalesAgreement(DeliveryPoint);
			if(actualWaterAgreement == null) {
				//Создаем новое доп соглашение воды.
				var newWaterAgreement = ClientDocumentsRepository.CreateDefaultWaterAgreement(UoW, DeliveryPoint, DeliveryDate, actualContract);

				//Переносим фикс цены.
				if(oldWaterAgreement.HasFixedPrice) {
					newWaterAgreement = WaterPricesRepository.FillWaterFixedPrices(UoW, newWaterAgreement, oldWaterAgreement.FixedPrices.ToList());
				}
				//Привязываем товары и документы на новое доп соглашение
				waterItems.ForEach(x => x.AdditionalAgreement = newWaterAgreement);
				waterDocuments.ForEach(x => x.AdditionalAgreement = newWaterAgreement);
				
			} else {
				//Привязываем товары и документы на новое доп соглашение
				waterItems.ForEach(x => x.AdditionalAgreement = actualWaterAgreement);
				waterDocuments.ForEach(x => x.AdditionalAgreement = actualWaterAgreement);
				//Обновляем цены на воду в заказе по доп соглашению.
				UpdatePrices(actualWaterAgreement);
			}

		}

		/// <summary>
		/// При смене договора перепривязывает доп соглашения созданные для 
		/// заказа на неизмененный договор.
		/// ВНИМАНИЕ! Использовать только при смене контракта
		/// </summary>
		private void ChangeOrderBasedAgreements(CounterpartyContract actualContract)
		{
			var agreementTypes = AdditionalAgreement.GetOrderBasedAgreementTypes();
			var agreementsForChange = OrderItems.Where(x => x.AdditionalAgreement != null)
			                                    .Select(x => x.AdditionalAgreement.Self)
			                                    .Where(x => agreementTypes.Contains(x.Type))
			                                    .Distinct()
			                                    .OrderBy(x => x.AgreementNumber)
			                                    .ToList();
			foreach(var agr in agreementsForChange) {
				IUnitOfWork uowAggr = null;
				switch(agr.Type) {
					case AgreementType.NonfreeRent:
						AgreementTransfer<NonfreeRentAgreement>(out uowAggr, agr.Id, actualContract);
						break;
					case AgreementType.DailyRent:
						AgreementTransfer<NonfreeRentAgreement>(out uowAggr, agr.Id, actualContract);
						break;
					case AgreementType.FreeRent:
						AgreementTransfer<NonfreeRentAgreement>(out uowAggr, agr.Id, actualContract);
						break;
					case AgreementType.EquipmentSales:
						AgreementTransfer<NonfreeRentAgreement>(out uowAggr, agr.Id, actualContract);
						break;
				}
				uowAggr.Save();
				uowAggr.Dispose();
			}

			var agreements = UoW.Session.QueryOver<AdditionalAgreement>()
								 .Where(x => x.Contract.Id == actualContract.Id)
								 .List<AdditionalAgreement>().ToList();
			actualContract.AdditionalAgreements.Clear();
			agreements.ForEach(x => actualContract.AdditionalAgreements.Add(x));
		}

		private void AgreementTransfer<TAgreement>(out IUnitOfWork blankUoW, int agreementId, CounterpartyContract toContract)
			where TAgreement : AdditionalAgreement, new()
		{
			var uow = UnitOfWorkFactory.CreateForRoot<TAgreement>(agreementId);
			uow.Root.AgreementNumber = AdditionalAgreement.GetNumberWithTypeFromDB<TAgreement>(toContract);
			uow.Root.Contract = toContract;
			blankUoW = uow;
		}

		public virtual void UpdatePrices()
		{
			var agreement = Contract.GetWaterSalesAgreement(DeliveryPoint);
			UoW.Session.Refresh(agreement);
			UpdatePrices(agreement);
		}

		public virtual void UpdatePrices(int[] agrIds)
		{
			var agreements = Contract.AdditionalAgreements.Select(a => a.Self).OfType<WaterSalesAgreement>().Where(a => agrIds.Contains(a.Id));
			foreach(var item in agreements) {
				UoW.Session.Refresh(item);
				UpdatePrices(item);
			}
		}

		public virtual void UpdatePrices(WaterSalesAgreement agreement)
		{
			var pricesMap = agreement.FixedPrices.ToDictionary(p => (int)p.Nomenclature.Id, p => (decimal)p.Price);

			foreach(OrderItem oItem in ObservableOrderItems) {
				if(pricesMap.ContainsKey(oItem.Nomenclature.Id) && oItem.Price != pricesMap[oItem.Nomenclature.Id])
					oItem.Price = pricesMap[oItem.Nomenclature.Id];
			}
		}

		public virtual bool HasWater()
		{
			var categories = Nomenclature.GetCategoriesRequirementForWaterAgreement();
			return ObservableOrderItems.Any(x => categories.Contains(x.Nomenclature.Category));
		}

		public virtual void CheckAndSetOrderIsService()
		{
			if(OrderItems.Any(x => x.Nomenclature.Category == NomenclatureCategory.master))
				IsService = true;
			else
				IsService = false;
		}

		public virtual void SetOrderCreationDate()
		{
			if(Id == 0 && !CreateDate.HasValue) {
				CreateDate = DateTime.Now;
			}
		}

		public virtual void SetFirstOrder()
		{
			if(Id == 0 && Client.FirstOrder == null) {
				IsFirstOrder = true;
				Client.FirstOrder = this;
			}
		}

		public virtual void CreateDefaultContract()
		{
			Contract = FindOrCreateContract(Client);
			ObservableOrderDocuments.Add(new OrderContract {
				Order = this,
				AttachedToOrder = this,
				Contract = Contract
			});
			OnChangeContract(false);
		}

		public virtual void RecalculateItemsPrice()
		{
			foreach(OrderItem item in ObservableOrderItems) {
				if(item.Nomenclature.Category == NomenclatureCategory.water) {
					item.RecalculatePrice();
				}
			}
		}

		public virtual int GetTotalWaterCount()
		{
			return ObservableOrderItems
				.Where(x => x.Nomenclature.Category == NomenclatureCategory.water)
				.Sum(x => x.Count);
		}

		/// <summary>
		/// Находит соответсвующий типу клиента и типу оплаты контракт, если не найден создает новый
		/// </summary>
		private CounterpartyContract FindOrCreateContract(Counterparty client)
		{
			var contractType = DocTemplateRepository.GetContractTypeForPaymentType(client.PersonType, PaymentType);
			var newContract = client.CounterpartyContracts.FirstOrDefault(x => x.ContractType == contractType && !x.IsArchive);
			if(newContract == null) {
				newContract = ClientDocumentsRepository.CreateDefaultContract(UoW, client, PaymentType, DeliveryDate);
			}
			return newContract;
		}

		//Выбирает договор соответствующий форме оплаты если такой найден
		public virtual void ChangeContractOnChangePaymentType()
		{
			var org = OrganizationRepository.GetOrganizationByPaymentType(UoW, Client.PersonType, PaymentType);
			if((Contract == null || Contract.Organization.Id != org.Id) && Client != null)
				Contract = CounterpartyContractRepository.GetCounterpartyContractByPaymentType(UoW, Client, Client.PersonType, PaymentType);
		}

		public virtual bool HaveActualWaterSaleAgreementByDeliveryPoint()
		{
			if(Contract == null) {
				return false;
			}
			Contract.AdditionalAgreements.Where(x => !x.IsCancelled).OfType<WaterSalesAgreement>();
			var waterSalesAgreementList = Contract.AdditionalAgreements
												   .Where(x => !x.IsCancelled)
												   .Select(x => x.Self)
												   .OfType<WaterSalesAgreement>();
			return waterSalesAgreementList.Any(x => x.DeliveryPoint == null || x.DeliveryPoint.Id == DeliveryPoint.Id);
		}

		/// <summary>
		/// Adds the equipment nomenclature for sale.
		/// </summary>
		/// <param name="nomenclature">Nomenclature.</param>
		/// <param name="UoW">Uo w.</param>
		public virtual void AddEquipmentNomenclatureForSale(AdditionalAgreement agreement, Nomenclature nomenclature, IUnitOfWork UoW)
		{
			AddEquipmentForSale(agreement, nomenclature, UoW);
			UpdateDocuments();
		}

		public virtual void AddEquipmentNomenclatureForSale(AdditionalAgreement agreement, IEnumerable<Nomenclature> nomenclatures, IUnitOfWork UoW)
		{
			foreach(var item in nomenclatures) {
				AddEquipmentForSale(agreement, item, UoW);
			}
			UpdateDocuments();
		}

		void AddEquipmentForSale(AdditionalAgreement agreement, Nomenclature nomenclature, IUnitOfWork UoW)
		{
			if(nomenclature.Category != NomenclatureCategory.equipment)
				return;
			if(!nomenclature.IsSerial) {
				ObservableOrderItems.Add(new OrderItem {
					Order = this,
					AdditionalAgreement = agreement,
					Count = 0,
					Equipment = null,
					Nomenclature = nomenclature,
					Price = nomenclature.GetPrice(1)
				});
			} else {
				Equipment eq = EquipmentRepository.GetEquipmentForSaleByNomenclature(UoW, nomenclature);
				ObservableOrderItems.AddWithReturn(new OrderItem {
					Order = this,
					AdditionalAgreement = agreement,
					Count = 1,
					Equipment = eq,
					Nomenclature = nomenclature,
					Price = nomenclature.GetPrice(1)
				});
			}
		}

		public virtual void AddEquipmentNomenclatureToClient(Nomenclature nomenclature, IUnitOfWork UoW)
		{
			if(!nomenclature.IsSerial) {
				ObservableOrderEquipments.Add(new OrderEquipment {

					Order = this,
					Direction = Direction.Deliver,
					Equipment = null,
					OrderItem = null,
					Reason = Reason.Service,
					Confirmed = true,
					Nomenclature = nomenclature
				});
			}
			UpdateDocuments();
		}

		public virtual void AddEquipmentNomenclatureFromClient(Nomenclature nomenclature, IUnitOfWork UoW)
		{
			if(!nomenclature.IsSerial) {
				ObservableOrderEquipments.Add(new OrderEquipment {

					Order = this,
					Direction = Direction.PickUp,
					Equipment = null,
					OrderItem = null,
					Reason = Reason.Service,
					Confirmed = true,
					Nomenclature = nomenclature
				});
			}
			UpdateDocuments();
		}

		public virtual void AddAnyGoodsNomenclatureForSale(Nomenclature nomenclature, bool isChangeOrder = false, int? cnt = null)
		{
			var acceptCategories = Nomenclature.GetCategoriesForSale();
			if(!acceptCategories.Contains(nomenclature.Category)) {
				return;
			}

			var count = (nomenclature.Category == NomenclatureCategory.service
						 || nomenclature.Category == NomenclatureCategory.deposit) && !isChangeOrder ? 1 : 0;

			if(cnt.HasValue)
				count = cnt.Value;

			ObservableOrderItems.Add(new OrderItem {
				Order = this,
				AdditionalAgreement = null,
				Count = count,
				Equipment = null,
				Nomenclature = nomenclature,
				Price = nomenclature.GetPrice(1)
			});

			//UpdateDocuments();
		}

		/// <summary>
		/// Добавление в заказ номенклатуры типа "Выезд мастера"
		/// </summary>
		/// <param name="nomenclature">Номенклатура типа "Выезд мастера"</param>
		/// <param name="count">Количество</param>
		/// <param name="quantityOfFollowingNomenclatures">Колличество номенклатуры, указанной в параметрах БД,
		/// которые будут добавлены в заказ вместе с мастером</param>
		public virtual void AddMasterNomenclature(Nomenclature nomenclature, int count, int quantityOfFollowingNomenclatures = 0)
		{
			if(nomenclature.Category != NomenclatureCategory.master) {
				return;
			}

			ObservableOrderItems.Add(new OrderItem {
				Order = this,
				AdditionalAgreement = null,
				Count = count,
				Equipment = null,
				Nomenclature = nomenclature,
				Price = nomenclature.GetPrice(1)
			});

			Nomenclature followingNomenclature = NomenclatureRepository.GetNomenclatureToAddWithMaster(UoW);
			if(quantityOfFollowingNomenclatures > 0 && !ObservableOrderItems.Any(i => i.Nomenclature.Id == followingNomenclature.Id))
				AddAnyGoodsNomenclatureForSale(followingNomenclature, false, 1);
		}

		public virtual void AddWaterForSale(Nomenclature nomenclature, WaterSalesAgreement wsa, int count)
		{
			if(!(nomenclature.Category == NomenclatureCategory.water 
			     || nomenclature.Category == NomenclatureCategory.disposableBottleWater))
				return;

			decimal price;
			//влияющая номенклатура
			Nomenclature infuentialNomenclature = nomenclature?.DependsOnNomenclature;

			if(wsa.HasFixedPrice && wsa.FixedPrices.Any(x => x.Nomenclature.Id == nomenclature.Id && infuentialNomenclature == null)) {
				price = wsa.FixedPrices.First(x => x.Nomenclature.Id == nomenclature.Id).Price;
			} else if(wsa.HasFixedPrice && wsa.FixedPrices.Any(x => x.Nomenclature.Id == infuentialNomenclature?.Id)) {
				price = wsa.FixedPrices.First(x => x.Nomenclature.Id == infuentialNomenclature?.Id).Price;
			} else {
				price = nomenclature.GetPrice(1);
			}

			ObservableOrderItems.Add(new OrderItem {
				Order = this,
				AdditionalAgreement = wsa,
				Count = count,
				Equipment = null,
				Nomenclature = nomenclature,
				Price = price
			});
			//UpdateDocuments();
		}

		#region test_methods_for_sidebar

		/// <summary>
		/// Добавить оборудование из выбранного предыдущего заказа.
		/// </summary>
		/// <param name="orderItem">Элемент заказа.</param>
		/// <param name="UoW">IUnitOfWork</param>
		public virtual void AddNomenclatureForSaleFromPreviousOrder(OrderItem orderItem, IUnitOfWork UoW)
		{
			if(orderItem.Nomenclature.Category != NomenclatureCategory.additional)
				return;
			ObservableOrderItems.Add(new OrderItem {
				Order = this,
				Count = orderItem.Count,
				Nomenclature = orderItem.Nomenclature,
				Price = orderItem.Price
			});
		}

		/// <summary>
		/// Добавить номенклатуру (не вода и не оборудование из выбранного предыдущего заказа).
		/// </summary>
		/// <param name="orderItem">Элемент заказа.</param>
		public virtual void AddAnyGoodsNomenclatureForSaleFromPreviousOrder(OrderItem orderItem)
		{
			if(orderItem.Nomenclature.Category != NomenclatureCategory.additional && orderItem.Nomenclature.Category != NomenclatureCategory.bottle &&
				orderItem.Nomenclature.Category != NomenclatureCategory.service && orderItem.Nomenclature.Category != NomenclatureCategory.disposableBottleWater)
				return;
			ObservableOrderItems.Add(new OrderItem {
				Order = this,
				AdditionalAgreement = orderItem.AdditionalAgreement,
				Count = orderItem.Nomenclature.Category == NomenclatureCategory.service ? 1 : 0,
				Equipment = orderItem.Equipment,
				Nomenclature = orderItem.Nomenclature,
				Price = orderItem.Price
			});
			//UpdateDocuments();
		}

		public virtual void ClearOrderItemsList()
		{
			ObservableOrderItems.Clear();
			UpdateDocuments();
		}

		/// <summary>
		/// Наполнение списка товаров нового заказа элементами списка другого заказа.
		/// </summary>
		/// <param name="order">Заказ, из которого будет производится копирование товаров</param>
		public virtual void CopyItemsFrom(Order order)
		{
			if(Id > 0)
				throw new InvalidOperationException("Копирование списка товаров из другого заказа недопустимо, если этот заказ не новый.");

			foreach(OrderItem orderItem in order.OrderItems){
				ObservableOrderItems.Add(
					new OrderItem {
						Order = this,
						AdditionalAgreement = orderItem.AdditionalAgreement,
						Nomenclature = orderItem.Nomenclature,
						Equipment = orderItem.Equipment,
						Price = orderItem.Price,
						IsUserPrice = orderItem.IsUserPrice,
						Count = orderItem.Count,
						ActualCount = orderItem.ActualCount,
						IncludeNDS = orderItem.IncludeNDS,
						IsDiscountInMoney = orderItem.IsDiscountInMoney,
						Discount = orderItem.Discount,
						DiscountMoney = orderItem.DiscountMoney,
						DiscountReason = orderItem.DiscountReason,
						FreeRentEquipment = orderItem.FreeRentEquipment,
						PaidRentEquipment = orderItem.PaidRentEquipment
					}
				);
			}
		}

		/// <summary>
		/// Наполнение списка оборудования нового заказа элементами списка другого заказа.
		/// </summary>
		/// <param name="order">Заказ, из которого будет производится копирование оборудования</param>
		public virtual void CopyEquipmentFrom(Order order)
		{
			if(Id > 0)
				throw new InvalidOperationException("Копирование списка оборудования из другого заказа недопустимо, если этот заказ не новый.");
			
			foreach(OrderEquipment orderEquipment in order.OrderEquipments) {
				ObservableOrderEquipments.Add(
					new OrderEquipment {
						Order = this,
						Direction = orderEquipment.Direction,
						DirectionReason = orderEquipment.DirectionReason,
						OrderItem = orderEquipment.OrderItem,
						Equipment = orderEquipment.Equipment,
						OwnType = orderEquipment.OwnType,
						Nomenclature = orderEquipment.Nomenclature,
						Reason = orderEquipment.Reason,
						Confirmed = orderEquipment.Confirmed,
						ConfirmedComment = orderEquipment.ConfirmedComment,
						ActualCount = orderEquipment.ActualCount,
						Count = orderEquipment.Count
					}
				);
			}
		}

		/// <summary>
		/// Копирует таблицу залогов в новый заказа из другого заказа
		/// </summary>
		/// <param name="order">Order.</param>
		public virtual void CopyDepositItemsFrom(Order order)
		{
			if(Id > 0)
				throw new InvalidOperationException("Копирование списка залогов из другого заказа недопустимо, если этот заказ не новый.");

			foreach(OrderDepositItem oDepositItem in order.OrderDepositItems){
				ObservableOrderDepositItems.Add(
					new OrderDepositItem {
						Order = this,
						Count = oDepositItem.Count,
						ActualCount = oDepositItem.ActualCount,
						Deposit = oDepositItem.Deposit,
						PaymentDirection = oDepositItem.PaymentDirection,
						DepositType = oDepositItem.DepositType,
						EquipmentNomenclature = oDepositItem.EquipmentNomenclature
					}
				);
			}
		}

		public virtual void CopyDocumentsFrom(Order order)
		{
			if(Id > 0)
				throw new InvalidOperationException("Копирование списка документов из другого заказа недопустимо, если этот заказ не новый.");

			var counterpartyDocTypes = typeof(OrderDocumentType).GetFields()
													   .Where(x => !x.GetCustomAttributes(typeof(DocumentOfOrderAttribute), false).Any())
			                                           .Where(x => !x.Name.Equals("value__"))
													   .Select(x => (OrderDocumentType)x.GetValue(null))
			                                           .ToArray();

			var orderDocTypes = typeof(OrderDocumentType).GetFields()
			                                           .Where(x => x.GetCustomAttributes(typeof(DocumentOfOrderAttribute), false).Any())
			                                           .Select(x => (OrderDocumentType)x.GetValue(null))
			                                           .ToArray();

			var counterpartyDocs = order.OrderDocuments.Where(d => counterpartyDocTypes.Contains(d.Type)).ToList();
			var orderDocs = order.OrderDocuments.Where(d => orderDocTypes.Contains(d.Type) && d.AttachedToOrder.Id != d.Order.Id).ToList();
			AddAdditionalDocuments(counterpartyDocs);
			AddAdditionalDocuments(orderDocs);
		}

		/// <summary>
		/// Удаляет дополнительные документы выделенные пользователем, которые не относятся к текущему заказу.
		/// </summary>
		/// <returns>Документы текущего заказа, которые не были удалены.</returns>
		/// <param name="documents">Список документов для удаления.</param>
		public virtual List<OrderDocument> RemoveAdditionalDocuments(OrderDocument[] documents)
		{
			List<OrderDocument> thisOrderDocuments = null;
			if(documents != null && documents.Any()) {
				thisOrderDocuments = new List<OrderDocument>();
				foreach(OrderDocument doc in documents) {
					if(doc.Order != this)
						ObservableOrderDocuments.Remove(doc);
					else
						thisOrderDocuments.Add(doc);
				}
			}
			return thisOrderDocuments;
		}

		/// <summary>
		/// Добавляет дополнительные документы выбранные пользователем в диалоге,
		/// с проверкой их наличия в текущем заказе
		/// </summary>
		public virtual void AddAdditionalDocuments(List<OrderDocument> documentsList)
		{
			foreach(var item in documentsList) {
				switch(item.Type) {
					case OrderDocumentType.Contract:
						OrderContract oc = (item as OrderContract);
						if(observableOrderDocuments
						   .OfType<OrderContract>()
						   .FirstOrDefault(x => x.Contract == oc.Contract
										   && x.Order == oc.Order)
						   == null) {
							ObservableOrderDocuments.Add(new OrderContract {
								Order = item.Order,
								AttachedToOrder = this,
								Contract = oc.Contract
							});
						}
						break;
					case OrderDocumentType.AdditionalAgreement:
						OrderAgreement oa = (item as OrderAgreement);
						if(observableOrderDocuments
						   .OfType<OrderAgreement>()
						   .FirstOrDefault(x => x.AdditionalAgreement == oa.AdditionalAgreement
										   && x.Order == oa.Order)
						   == null) {
							ObservableOrderDocuments.Add(new OrderAgreement {
								Order = item.Order,
								AttachedToOrder = this,
								AdditionalAgreement = oa.AdditionalAgreement
							});
						}
						break;
					case OrderDocumentType.M2Proxy:
						OrderM2Proxy m2 = (item as OrderM2Proxy);
						if(observableOrderDocuments
						   .OfType<OrderM2Proxy>()
						   .FirstOrDefault(x => x.M2Proxy == m2.M2Proxy
										   && x.Order == m2.Order)
						   == null) {
							ObservableOrderDocuments.Add(new OrderM2Proxy {
								Id = item.Id,
								Order = item.Order,
								AttachedToOrder = this,
								M2Proxy = m2.M2Proxy
							});
						}
						break;
					case OrderDocumentType.CoolerWarranty:
						CoolerWarrantyDocument cwd = (item as CoolerWarrantyDocument);
						if(observableOrderDocuments
						   .OfType<CoolerWarrantyDocument>()
						   .FirstOrDefault(x => x.AdditionalAgreement == cwd.AdditionalAgreement
										   && x.Contract == cwd.Contract
										   && x.WarrantyNumber == cwd.WarrantyNumber
										   && x.Order == cwd.Order)
						   == null) {
							ObservableOrderDocuments.Add(new CoolerWarrantyDocument {
								Order = item.Order,
								AttachedToOrder = this,
								AdditionalAgreement = cwd.AdditionalAgreement,
								Contract = cwd.Contract,
								WarrantyNumber = cwd.WarrantyNumber
							});
						}
						break;
					case OrderDocumentType.PumpWarranty:
						PumpWarrantyDocument pwd = (item as PumpWarrantyDocument);
						if(observableOrderDocuments
						   .OfType<PumpWarrantyDocument>()
						   .FirstOrDefault(x => x.AdditionalAgreement == pwd.AdditionalAgreement
										   && x.Contract == pwd.Contract
										   && x.WarrantyNumber == pwd.WarrantyNumber
										   && x.Order == pwd.Order)
						   == null) {
							ObservableOrderDocuments.Add(new PumpWarrantyDocument {
								Order = item.Order,
								AttachedToOrder = this,
								AdditionalAgreement = pwd.AdditionalAgreement,
								Contract = pwd.Contract,
								WarrantyNumber = pwd.WarrantyNumber
							});
						}
						break;
					case OrderDocumentType.Bill:
						//case OrderDocumentType.BillWithoutSignature:
						if(observableOrderDocuments
						   .OfType<BillDocument>()
						   .FirstOrDefault(x => x.Order == item.Order)
						   == null) {
							ObservableOrderDocuments.Add(new BillDocument {
								Order = item.Order,
								AttachedToOrder = this
							});
						}
						break;
					case OrderDocumentType.DoneWorkReport:
						if(observableOrderDocuments
						   .OfType<DoneWorkDocument>()
						   .FirstOrDefault(x => x.Order == item.Order)
						   == null) {
							ObservableOrderDocuments.Add(new DoneWorkDocument {
								Order = item.Order,
								AttachedToOrder = this
							});
						}
						break;
					case OrderDocumentType.EquipmentTransfer:
						if(observableOrderDocuments
						   .OfType<EquipmentTransferDocument>()
						   .FirstOrDefault(x => x.Order == item.Order)
						   == null) {
							ObservableOrderDocuments.Add(new EquipmentTransferDocument {
								Order = item.Order,
								AttachedToOrder = this
							});
						}
						break;
					case OrderDocumentType.Invoice:
						if(observableOrderDocuments
						   .OfType<InvoiceDocument>()
						   .FirstOrDefault(x => x.Order == item.Order)
						   == null) {
							ObservableOrderDocuments.Add(new InvoiceDocument {
								Order = item.Order,
								AttachedToOrder = this
							});
						}
						break;
					case OrderDocumentType.InvoiceBarter:
						if(observableOrderDocuments
						   .OfType<InvoiceBarterDocument>()
						   .FirstOrDefault(x => x.Order == item.Order)
						   == null) {
							ObservableOrderDocuments.Add(new InvoiceBarterDocument {
								Order = item.Order,
								AttachedToOrder = this
							});
						}
						break;
					case OrderDocumentType.InvoiceContractDoc:
						if(observableOrderDocuments
						   .OfType<InvoiceContractDoc>()
						   .FirstOrDefault(x => x.Order == item.Order)
						   == null) {
							ObservableOrderDocuments.Add(new InvoiceContractDoc {
								Order = item.Order,
								AttachedToOrder = this
							});
						}
						break;
					case OrderDocumentType.UPD:
						if(observableOrderDocuments
						   .OfType<UPDDocument>()
						   .FirstOrDefault(x => x.Order == item.Order)
						   == null) {
							ObservableOrderDocuments.Add(new UPDDocument {
								Order = item.Order,
								AttachedToOrder = this
							});
						}
						break;
					case OrderDocumentType.DriverTicket:
						if(observableOrderDocuments
						   .OfType<DriverTicketDocument>()
						   .FirstOrDefault(x => x.Order == item.Order)
						   == null) {
							ObservableOrderDocuments.Add(new DriverTicketDocument {
								Order = item.Order,
								AttachedToOrder = this
							});
						}
						break;
					case OrderDocumentType.Torg12:
						if(observableOrderDocuments
						   .OfType<Torg12Document>()
						   .FirstOrDefault(x => x.Order == item.Order)
						   == null) {
							ObservableOrderDocuments.Add(new Torg12Document {
								Order = item.Order,
								AttachedToOrder = this
							});
						}
						break;
					case OrderDocumentType.ShetFactura:
						if(observableOrderDocuments
						   .OfType<ShetFacturaDocument>()
						   .FirstOrDefault(x => x.Order == item.Order)
						   == null) {
							ObservableOrderDocuments.Add(new ShetFacturaDocument {
								Order = item.Order,
								AttachedToOrder = this
							});
						}
						break;
					default:
						break;
				}
			}
		}

		#endregion

		public virtual void RecalcBottlesDeposits(IUnitOfWork uow)
		{
			/* Отключено в связи с изменением работы возврата залогов
			var expectedBottleDepositsCount = GetExpectedBottlesDepositsCount();
			var bottleDeposit = NomenclatureRepository.GetBottleDeposit(uow);
			if(bottleDeposit == null)
				throw new InvalidProgramException("В параметрах базы не настроена номенклатура залога за бутыли.");
			var depositPaymentItem = ObservableOrderItems.FirstOrDefault(item => item.Nomenclature.Id == bottleDeposit.Id);
			var depositRefundItem = ObservableOrderDepositItems.FirstOrDefault(item => item.DepositType == DepositType.Bottles);

			//Надо создать услугу залога
			if(expectedBottleDepositsCount > 0) {
				if(depositRefundItem != null) {
					depositRefundItem.Count = expectedBottleDepositsCount;
					depositRefundItem.PaymentDirection = PaymentDirection.FromClient;
				}
				if(depositPaymentItem != null)
					depositPaymentItem.Count = expectedBottleDepositsCount;
				else {*/
			/* Временно отключил взятие с клиента залогов за бутыли. Удалить если залоги так и не вернутся.
			 * 					ObservableOrderItems.Add (new OrderItem {
									Order = this,
									AdditionalAgreement = null,
									Count = expectedBottleDepositsCount,
									Equipment = null,
									Nomenclature = NomenclatureRepository.GetBottleDeposit (uow),
									Price = NomenclatureRepository.GetBottleDeposit (uow).GetPrice (expectedBottleDepositsCount)
								});
								ObservableOrderDepositItems.Add (new OrderDepositItem {
									Order = this,
									Count = expectedBottleDepositsCount,
									Deposit = NomenclatureRepository.GetBottleDeposit (uow).GetPrice (expectedBottleDepositsCount),
									DepositOperation = null,
									DepositType = DepositType.Bottles,
									FreeRentItem = null,
									PaidRentItem = null,
									PaymentDirection = PaymentDirection.FromClient
								}); *//*
		}
		return;
	}

	if(expectedBottleDepositsCount == 0) {
		if(depositRefundItem != null)
			ObservableOrderDepositItems.Remove(depositRefundItem);
		if(depositPaymentItem != null)
			ObservableOrderItems.Remove(depositPaymentItem);
		return;
	}
	if(expectedBottleDepositsCount < 0) {
		if(depositPaymentItem != null)
			ObservableOrderItems.Remove(depositPaymentItem);
		//Проверяем, сколько надо отдать клиенту залог за бутыли
		decimal clientDeposit = default(decimal);
		decimal deposit = bottleDeposit.GetPrice(-expectedBottleDepositsCount);
		int count = -expectedBottleDepositsCount;
		if(Client != null)
			clientDeposit = Repository.Operations.DepositRepository.GetDepositsAtCounterparty(UoW, Client, DepositType.Bottles);
		if(clientDeposit - deposit * count >= 0)
			if(depositRefundItem != null) {
				depositRefundItem.Deposit = deposit;
				depositRefundItem.Count = count;
			} else
				ObservableOrderDepositItems.Add(new OrderDepositItem {
					Order = this,
					DepositOperation = null,
					DepositType = DepositType.Bottles,
					Deposit = deposit,
					PaidRentItem = null,
					FreeRentItem = null,
					PaymentDirection = PaymentDirection.ToClient,
					Count = count
				});
		return;
	}*/
		}

		/// <summary>
		/// Ожидаемое количество залогов за бутыли
		/// </summary>
		public virtual int GetExpectedBottlesDepositsCount()
		{
			if(Client == null || Client.PersonType == PersonType.legal)
				return 0;

			var waterItemsCount = ObservableOrderItems.Select(item => item)
				.Where(item => item.Nomenclature.Category == NomenclatureCategory.water)
				.Sum(item => item.Count);

			return waterItemsCount - BottlesReturn ?? 0;
		}

		public virtual void FillItemsFromAgreement(AdditionalAgreement a)
		{
			if(a.Type == AgreementType.DailyRent || a.Type == AgreementType.NonfreeRent) {
				IList<PaidRentEquipment> paidRentEquipmentList;
				bool IsDaily = false;
				int RentCount = 0;
				if(a.Type == AgreementType.DailyRent) {
					paidRentEquipmentList = (a.Self as DailyRentAgreement).Equipment;
					RentCount = (a.Self as DailyRentAgreement).RentDays;
					IsDaily = true;
				} else {
					paidRentEquipmentList = (a.Self as NonfreeRentAgreement).PaidRentEquipments;
					RentCount = (a.Self as NonfreeRentAgreement).RentMonths ?? 0;
				}

				foreach(PaidRentEquipment paidRentEquipment in paidRentEquipmentList) {
					int ItemId;
					//Добавляем номенклатуру залога
					OrderItem orderItem = null;
					if((orderItem = ObservableOrderItems.FirstOrDefault<OrderItem>(
							item => item.AdditionalAgreement?.Id == a.Self.Id &&
							item.Nomenclature.Id == paidRentEquipment.PaidRentPackage.DepositService.Id)) != null) {
						orderItem.Count = paidRentEquipment.Count;
						orderItem.Price = paidRentEquipment.Deposit;
					} else {
						ObservableOrderItems.Add(
							new OrderItem {
								Order = this,
								AdditionalAgreement = a.Self,
								Count = paidRentEquipment.Count,
								Equipment = null,
								Nomenclature = paidRentEquipment.PaidRentPackage.DepositService,
								Price = paidRentEquipment.Deposit,
								PaidRentEquipment = paidRentEquipment
							}
						);
					}
					//Добавляем услугу аренды
					orderItem = null;
					if((orderItem = ObservableOrderItems.FirstOrDefault<OrderItem>(
							item => item.AdditionalAgreement?.Id == a.Self.Id &&
						item.Nomenclature.Id == (IsDaily ? paidRentEquipment.PaidRentPackage.RentServiceDaily.Id : paidRentEquipment.PaidRentPackage.RentServiceMonthly.Id)
							)) != null) {
						orderItem.Count = paidRentEquipment.Count * RentCount;
						orderItem.Price = paidRentEquipment.Price;
						ItemId = ObservableOrderItems.IndexOf(orderItem);
					} else {
						Nomenclature nomenclature = IsDaily ? paidRentEquipment.PaidRentPackage.RentServiceDaily : paidRentEquipment.PaidRentPackage.RentServiceMonthly;
						ItemId = ObservableOrderItems.AddWithReturn(
							new OrderItem {
								Order = this,
								AdditionalAgreement = a.Self,
								Count = paidRentEquipment.Count * RentCount,
								Equipment = null,
								Nomenclature = nomenclature,
								Price = paidRentEquipment.Price,
								PaidRentEquipment = paidRentEquipment
							}
						);
					}
					//Добавляем оборудование
					OrderEquipment orderEquip = ObservableOrderEquipments.FirstOrDefault(
						x => x.Equipment == paidRentEquipment.Equipment
						&& x.OrderItem != null
						&& x.OrderItem == orderItem
					);
					if(orderEquip != null) {
						orderEquip.Count = paidRentEquipment.Count;
					} else {
						ObservableOrderEquipments.Add(
						new OrderEquipment {
							Order = this,
							Direction = Direction.Deliver,
							Count = paidRentEquipment.Count,
							Equipment = paidRentEquipment.Equipment,
							Nomenclature = paidRentEquipment.Nomenclature,
							Reason = Reason.Rent,
							DirectionReason = DirectionReason.Rent,
							OrderItem = ObservableOrderItems[ItemId],
							OwnType = OwnTypes.Rent
						}
						);
					}

					OnPropertyChanged(nameof(TotalSum));
					OnPropertyChanged(nameof(SumToReceive));
				}
			} else if(a.Type == AgreementType.FreeRent) {
				FreeRentAgreement agreement = a.Self as FreeRentAgreement;
				foreach(FreeRentEquipment equipment in agreement.Equipment) {
					int ItemId;
					//Добавляем номенклатуру залога.
					ItemId = ObservableOrderItems.AddWithReturn(
						new OrderItem {
							Order = this,
							AdditionalAgreement = agreement,
							Count = equipment.Count,
							Equipment = null,
							Nomenclature = equipment.FreeRentPackage.DepositService,
							Price = equipment.Deposit,
							FreeRentEquipment = equipment
						}
					);
					//Добавляем оборудование.
					ObservableOrderEquipments.Add(
						new OrderEquipment {
							Order = this,
							Direction = Direction.Deliver,
							Count = equipment.Count,
							Equipment = equipment.Equipment,
							Nomenclature = equipment.Nomenclature,
							Reason = Reason.Rent,
							DirectionReason = DirectionReason.Rent,
							OrderItem = ObservableOrderItems[ItemId],
							OwnType = OwnTypes.Rent
						}
					);
				}
			} else if(a.Type == AgreementType.EquipmentSales) {
				SalesEquipmentAgreement agreement = a.Self as SalesEquipmentAgreement;
				foreach(SalesEquipment equipment in agreement.SalesEqipments) {
					var oi = observableOrderItems.FirstOrDefault(x => x.AdditionalAgreement != null
					                                             && x.AdditionalAgreement.Self == agreement
					                                             && x.Nomenclature.Id == equipment.Nomenclature.Id);
					if(oi != null) {
						oi.Price = equipment.Price;
						oi.Price = equipment.Count;
					} else {
						int ItemId;
						//Добавляем номенклатуру продажи оборудования.
						ItemId = ObservableOrderItems.AddWithReturn(
							new OrderItem {
								Order = this,
								AdditionalAgreement = agreement,
								Count = equipment.Count,
								Equipment = null,
								Nomenclature = equipment.Nomenclature,
								Price = equipment.Price
							}
							);
					}
				}
			}
			UpdateDocuments();
		}

		public virtual void RemoveAloneItem(OrderItem item)
		{
			if(item.Count == 0 
			   && (item.AdditionalAgreement == null 
			       || (item.AdditionalAgreement != null && item.AdditionalAgreement.Self is WaterSalesAgreement)
			      )
			   && !OrderEquipments.Any(x => x.OrderItem == item)) {
				ObservableOrderItems.Remove(item);
			}
		}

		public virtual void RemoveItem(OrderItem item)
		{
			if(item.AdditionalAgreement != null) {
				ObservableOrderItems.Remove(item);
				DeleteOrderEquipmentOnOrderItem(item);
				DeleteOrderAgreementDocumentOnOrderItem(item);
			}else {
				ObservableOrderItems.Remove(item);
				DeleteOrderEquipmentOnOrderItem(item);
			}
			UpdateDocuments();
		}

		public virtual void RemoveEquipment(OrderEquipment item)
		{
			ObservableOrderEquipments.Remove(item);
			UpdateDocuments();
		}

		private void RemoveRentItems(OrderItem item)
		{
			if(item.FreeRentEquipment != null) // Для бесплатной аренды.
			{
				foreach(OrderItem orderItem in ObservableOrderItems.ToList()) {
					if(orderItem.FreeRentEquipment == item.FreeRentEquipment && orderItem != item) {
						ObservableOrderItems.Remove(orderItem);
						DeleteOrderEquipmentOnOrderItem(orderItem);
						DeleteOrderAgreementDocumentOnOrderItem(orderItem);
					}
				}
			}

			if(item.PaidRentEquipment != null) // Для помесячной и посуточной аренды.
			{
				foreach(OrderItem orderItem in ObservableOrderItems.ToList()) {
					if(orderItem.PaidRentEquipment == item.PaidRentEquipment && orderItem != item) {
						ObservableOrderItems.Remove(orderItem);
						DeleteOrderEquipmentOnOrderItem(orderItem);
						DeleteOrderAgreementDocumentOnOrderItem(orderItem);
					}
				}
			}

			foreach(OrderItem orderItem in ObservableOrderItems.ToList()) {
				if(orderItem.AdditionalAgreement == item.AdditionalAgreement && orderItem != item) {
					DeleteOrderEquipmentOnOrderItem(orderItem);
					DeleteOrderAgreementDocumentOnOrderItem(orderItem);
				}
			}
		}

		/// <summary>
		/// Удаляет оборудование в заказе связанное с товаром в заказе
		/// </summary>
		/// <param name="orderItem">Товар в заказе по которому будет удалятся оборудование</param>
		private void DeleteOrderEquipmentOnOrderItem(OrderItem orderItem)
		{
			var orderEquipments = ObservableOrderEquipments
				.Where(x => x.OrderItem == orderItem)
				.ToList();
			foreach(var orderEquipment in orderEquipments) {
				ObservableOrderEquipments.Remove(orderEquipment);
			}
		}

		/// <summary>
		/// Удаляет документы дополнительного соглашения в заказе связанные с товаром в заказе
		/// </summary>
		/// <param name="orderItem">Товар в заказе по которому будет удалятся документ</param>
		private void DeleteOrderAgreementDocumentOnOrderItem(OrderItem orderItem)
		{
			var orderDocuments = ObservableOrderDocuments
				.OfType<OrderAgreement>()
				.Where(x => x.AdditionalAgreement == orderItem.AdditionalAgreement)
				.ToList();
			foreach(var orderDocument in orderDocuments) {
				ObservableOrderDocuments.Remove(orderDocument);
			}
		}


		public virtual void RemoveDepositItem(OrderDepositItem item)
		{
			ObservableOrderDepositItems.Remove(item);
			UpdateDocuments();
		}

		public virtual void AddServiceClaimAsInitial(ServiceClaim service)
		{
			if(service.InitialOrder != null && service.InitialOrder.Id == Id) {
				if(service.Equipment == null || ObservableOrderEquipments.FirstOrDefault(eq => eq.Equipment.Id == service.Equipment.Id) == null) {
					ObservableOrderEquipments.Add(new OrderEquipment {
						Order = this,
						Direction = Direction.PickUp,
						Equipment = service.Equipment,
						Nomenclature = service.Equipment == null ? service.Nomenclature : null,
						OrderItem = null,
						Reason = Reason.Service,
						ServiceClaim = service
					});
				}
				if(service.ReplacementEquipment != null) {
					ObservableOrderEquipments.Add(new OrderEquipment {
						Order = this,
						Direction = Direction.Deliver,
						Equipment = service.ReplacementEquipment,
						Nomenclature = null,
						OrderItem = null,
						Reason = Reason.Service
					});
				}
				if(ObservableInitialOrderService.FirstOrDefault(sc => sc.Id == service.Id) == null)
					ObservableInitialOrderService.Add(service);
				if(ObservableOrderDocuments.Where(doc => doc.Type == OrderDocumentType.EquipmentTransfer).Cast<EquipmentTransferDocument>()
					.FirstOrDefault() == null) {
					ObservableOrderDocuments.Add(new EquipmentTransferDocument {
						Order = this,
						AttachedToOrder = this
					});
				}
			}
		}

		public virtual void AddServiceClaimAsFinal(ServiceClaim service)
		{
			//TODO FIXME скрыто до реализации работы заявок на сервисное обслуживание, 
			//в связи с изменением работы документов DoneWorkDocument, EquipmentTransfer
			/*if(service.FinalOrder != null && service.FinalOrder.Id == Id) {
				if(ObservableOrderEquipments.FirstOrDefault(eq => eq.Equipment.Id == service.Equipment.Id) == null) {
					ObservableOrderEquipments.Add(new OrderEquipment {
						Order = this,
						Direction = Direction.Deliver,
						Equipment = service.Equipment,
						OrderItem = null,
						Reason = Reason.Service
					});
				}
				if(ObservableOrderDocuments.Where(doc => doc.Type == OrderDocumentType.DoneWorkReport).Cast<DoneWorkDocument>()
					.FirstOrDefault(doc => doc.ServiceClaim.Id == service.Id) == null) {
					ObservableOrderDocuments.Add(new DoneWorkDocument {
						Order = this,
						AttachedToOrder = this,
						ServiceClaim = service
					});
				}
			}*/
			//TODO FIXME Добавить строку сервиса OrderItems
			//И вообще много чего тут сделать.
		}

		public virtual void FillNewEquipment(Equipment registeredEquipment)
		{
			var newEquipment = ObservableOrderEquipments
				.Where(orderEq => orderEq.Nomenclature != null)
				.FirstOrDefault(orderEq => orderEq.Nomenclature.Id == registeredEquipment.Nomenclature.Id);
			if(newEquipment != null) {
				newEquipment.Equipment = registeredEquipment;
				newEquipment.Nomenclature = null;
			}
		}

		/// <summary>
		/// Присвоение текущему заказу статуса недовоза
		/// </summary>
		/// <param name="guilty">Виновный в недовезении заказа</param>
		public virtual void SetUndeliveredStatus(GuiltyTypes? guilty = GuiltyTypes.Client)
		{
			var routeListItem = RouteListItemRepository.GetRouteListItemForOrder(UoW, this);

			switch(OrderStatus) {
				case OrderStatus.NewOrder:
				case OrderStatus.WaitForPayment:
				case OrderStatus.Accepted:
				case OrderStatus.InTravelList:
				case OrderStatus.OnLoading:
					ChangeStatus(OrderStatus.Canceled);
					routeListItem?.SetStatusWithoutOrderChange(RouteListItemStatus.Overdue);
					break;
				case OrderStatus.OnTheWay:
				case OrderStatus.DeliveryCanceled:
				case OrderStatus.Shipped:
				case OrderStatus.UnloadingOnStock:
				case OrderStatus.NotDelivered:
				case OrderStatus.Closed:
					if(guilty == GuiltyTypes.Client) {
						ChangeStatus(OrderStatus.DeliveryCanceled);
						routeListItem?.SetStatusWithoutOrderChange(RouteListItemStatus.Canceled);
					} else {
						ChangeStatus(OrderStatus.NotDelivered);
						routeListItem?.SetStatusWithoutOrderChange(RouteListItemStatus.Overdue);
					}
					break;
			}
		}

		public virtual void ChangeStatus(OrderStatus newStatus)
		{
			var initialStatus = OrderStatus;
			OrderStatus = newStatus;

			switch(newStatus) {
				case OrderStatus.NewOrder:
					break;
				case OrderStatus.WaitForPayment:
					OnWaitingPaymentOrder();
					break;
				case OrderStatus.Accepted:
					//Удаляем операции перемещения тары, если возвращаем 
					//из "закрыт" без доставки в "принят"
					if(initialStatus == OrderStatus.Closed)
						UpdateBottlesMovementOperation(UoW, deleteOperation: true);
					OnAcceptOrder();
					break;
				case OrderStatus.InTravelList:
				case OrderStatus.OnLoading:
				case OrderStatus.OnTheWay:
				case OrderStatus.DeliveryCanceled:
				case OrderStatus.Shipped:
				case OrderStatus.UnloadingOnStock:
				case OrderStatus.NotDelivered:
					break;
				case OrderStatus.Closed:
					OnClosedOrder();
					break;
				case OrderStatus.Canceled:
				default:
					break;
			}

			if(Id == 0 
			   || newStatus == OrderStatus.Canceled
			   || newStatus == OrderStatus.NotDelivered
			   || initialStatus == newStatus)
				return;
			
			var undeliveries = UndeliveredOrdersRepository.GetListOfUndeliveriesForOrder(UoW, this);
			if(undeliveries.Any()) {
				var text = String.Format(
					"сменил(а) статус заказа\nс \"{0}\" на \"{1}\"",
					initialStatus.GetEnumTitle(),
					newStatus.GetEnumTitle()
				);
				undeliveries.ForEach(u => u.AddCommentToTheField(UoW, CommentedFields.Reason, text));
			}
		}

		/// <summary>
		/// Действия при закрытии заказа
		/// </summary>
		public virtual void OnClosedOrder()
		{
			SetDepositsActualCounts();
		}

		/// <summary>
		/// Действия при переводе заказа в ожидание оплаты
		/// </summary>
		public virtual void OnWaitingPaymentOrder()
		{
			//Создается счет
			var billDoc = ObservableOrderDocuments.FirstOrDefault(x => x.Order == this && x.Type == OrderDocumentType.Bill) as BillDocument;
			if(billDoc == null) {
				ObservableOrderDocuments.Add(CreateDocumentOfOrder(OrderDocumentType.Bill));
			}
		}

		/// <summary>
		/// Действия при подтверждении заказа
		/// </summary>
		public virtual void OnAcceptOrder()
		{
			UpdateDocuments();
		}


		/// <summary>
		/// Устанавливает количество для каждого залога как actualCount, 
		/// если заказ был создан только для залога.
		/// Для отображения этих данных в отчете "Акт по бутылям и залогам"
		/// </summary>
		public virtual void SetDepositsActualCounts()
		{
			if(OrderItems.All(x => x.Nomenclature.Id == 157)) {
				foreach(var oi in orderItems) {
					oi.ActualCount = oi.Count;
				}
			}
		}

		#region Работа с документами

		public virtual OrderDocumentType[] GetRequirementDocTypes()
		{
			//создаём объект-ключ на основе текущего заказа. Этот ключ содержит набор свойств, 
			//по которым будет происходить подбор правила для создания набора документов
			var key = new OrderStateKey(this);

			//обращение к хранилищу правил для получения массива типов документов по ключу
			return OrderDocumentRulesRepository.GetSetOfDocumets(key);
		}

		public virtual void UpdateDocuments()
		{
			CheckAndCreateDocuments(GetRequirementDocTypes());
		}

		[Obsolete("Метод устарел после внедрения функционала в рамках задачи I-1173", true)]
		private void AddDepositDocuments(List<OrderDocumentType> list)
		{
			if(ObservableOrderDepositItems.Any(x => x.DepositType == DepositType.Bottles
															&& x.PaymentDirection == PaymentDirection.ToClient)) {
				list.Add(OrderDocumentType.RefundBottleDeposit);
			}

			if(ObservableOrderDepositItems.Any(x => x.DepositType == DepositType.Equipment
													&& x.PaymentDirection == PaymentDirection.ToClient)) {
				list.Add(OrderDocumentType.RefundEquipmentDeposit);
			}
		}

		[Obsolete("Метод устарел после внедрения функционала в рамках задачи I-1173", true)]
		private void AddEquipmentDocuments(List<OrderDocumentType> list)
		{
			//Доставка оборудования в собственности клиента после обслуживания
			if(ObservableOrderEquipments
			   .Any(x => x.OrderItem == null
					&& x.Direction == Direction.Deliver
					&& x.OwnType == OwnTypes.Client)
			  ) {
				list.Add(OrderDocumentType.DoneWorkReport);
			}

			bool equipmentTransfer =
			//Дежурное оборудование
			ObservableOrderEquipments.Any(x => x.OwnType == OwnTypes.Duty)
			//Забор оборудования клиента
			|| ObservableOrderEquipments.Any(x => x.Direction == Direction.PickUp && x.OwnType == OwnTypes.Client)
			//Оборудование в аренду, если оно добавлено вручную а не через доп соглашение
			|| ObservableOrderEquipments.Any(x => x.OrderItem == null && x.OwnType == OwnTypes.Rent);

			if(equipmentTransfer) {
				list.Add(OrderDocumentType.EquipmentTransfer);
			}
		}

		public virtual void CreateOrderAgreementDocument(AdditionalAgreement agreement)
		{
			if(ObservableOrderDocuments.OfType<OrderAgreement>().Any(x => x.AdditionalAgreement.Id == agreement.Id)) {
				return;
			}
			ObservableOrderDocuments.Add(new OrderAgreement {
				Order = this,
				AttachedToOrder = this,
				AdditionalAgreement = agreement
			});
		}

		/// <summary>
		/// Создает необходимые гарантийные талоны
		/// </summary>
		protected virtual void CreateWarrantyDocuments()
		{
			// Кулера
			var orderItemsWithCoolerWarranty = ObservableOrderItems
				.Where(orderItem => orderItem.Nomenclature?.Type?.WarrantyCardType == WarrantyCardType.CoolerWarranty);

			// Кулера для продажи
			var orderItemsCoolerWarrantyForSale = orderItemsWithCoolerWarranty.Where(x => x.AdditionalAgreement == null);
			if(orderItemsCoolerWarrantyForSale.Count() > 0 && OrderStatus == OrderStatus.Accepted) {
				AddWarrantyDocumentIfNotExist(new CoolerWarrantyDocument {
					WarrantyNumber = CoolerWarrantyDocument.GetNumber(this),
					Order = this,
					AttachedToOrder = this,
					Contract = this.Contract,
					AdditionalAgreement = null
				});
			}

			// Кулера в аренду
			var orderItemsCoolerWarrantyForRent = orderItemsWithCoolerWarranty.Where(x => x.AdditionalAgreement != null);
			if(orderItemsCoolerWarrantyForRent.Count() > 0 && OrderStatus == OrderStatus.Accepted) {
				var orderItemsDistinctAgreements = orderItemsWithCoolerWarranty.Select(x => x.AdditionalAgreement).Distinct().ToList();
				foreach(var oItem in orderItemsDistinctAgreements) {
					AddWarrantyDocumentIfNotExist(new CoolerWarrantyDocument {
						WarrantyNumber = CoolerWarrantyDocument.GetNumber(this, oItem),
						Order = this,
						AttachedToOrder = this,
						Contract = this.Contract,
						AdditionalAgreement = oItem
					});
				}
			}

			// Помпы
			var orderItemsWithPumpWarranty = ObservableOrderItems
				.Where(orderItem => orderItem.Nomenclature?.Type?.WarrantyCardType == WarrantyCardType.PumpWarranty);

			// Помпы для продажи
			var orderItemsPumpWarrantyForSale = orderItemsWithPumpWarranty.Where(x => x.AdditionalAgreement == null);
			if(orderItemsPumpWarrantyForSale.Count() > 0 && OrderStatus == OrderStatus.Accepted) {
				AddWarrantyDocumentIfNotExist(new PumpWarrantyDocument {
					WarrantyNumber = PumpWarrantyDocument.GetNumber(this),
					Order = this,
					AttachedToOrder = this,
					Contract = this.Contract,
					AdditionalAgreement = null
				});
			}

			// Помпы в аренду
			var orderItemsPumpWarrantyForRent = orderItemsWithPumpWarranty.Where(x => x.AdditionalAgreement != null);
			if(orderItemsPumpWarrantyForRent.Count() > 0 && OrderStatus == OrderStatus.Accepted) {
				var orderItemsDistinctAgreements = orderItemsWithPumpWarranty.Select(x => x.AdditionalAgreement).Distinct().ToList();
				foreach(var oItem in orderItemsDistinctAgreements) {
					AddWarrantyDocumentIfNotExist(new PumpWarrantyDocument {
						WarrantyNumber = PumpWarrantyDocument.GetNumber(this, oItem),
						Order = this,
						AttachedToOrder = this,
						Contract = this.Contract,
						AdditionalAgreement = oItem
					});
				}
			}
		}

		protected virtual void AddWarrantyDocumentIfNotExist(OrderDocument document)
		{
			bool haveDocuments = true;
			if(document is CoolerWarrantyDocument) {
				haveDocuments = ObservableOrderDocuments
					.Where(doc => doc.Order.Id == Id)
					.OfType<CoolerWarrantyDocument>()
					.Where(x => x.AdditionalAgreement == (document as CoolerWarrantyDocument).AdditionalAgreement)
					.Any();
			}
			if(document is PumpWarrantyDocument) {
				haveDocuments = ObservableOrderDocuments
					.Where(doc => doc.Order.Id == Id)
					.OfType<PumpWarrantyDocument>()
					.Where(x => x.AdditionalAgreement == (document as PumpWarrantyDocument).AdditionalAgreement)
					.Any();
			}
			if(!haveDocuments) {
				ObservableOrderDocuments.Add(document);
			}

		}

		private void CheckAndCreateDocuments(params OrderDocumentType[] needed)
		{
			var docsOfOrder = typeof(OrderDocumentType).GetFields()
													   .Where(x => x.GetCustomAttributes(typeof(DocumentOfOrderAttribute), false).Any())
													   .Select(x => (OrderDocumentType)x.GetValue(null))
													   .ToArray();

			if(needed.Any(x => !docsOfOrder.Contains(x)))
				throw new ArgumentException($"В метод можно передавать только типы документов помеченные атрибутом {nameof(DocumentOfOrderAttribute)}", nameof(needed));

			var needCreate = needed.ToList();
			foreach(var doc in OrderDocuments.Where(d => d.Order?.Id == Id && docsOfOrder.Contains(d.Type)).ToList()) {
				if(needed.Contains(doc.Type))
					needCreate.Remove(doc.Type);
				else
					ObservableOrderDocuments.Remove(doc);
				if(OrderDocuments.Any(x => x.Order?.Id == Id && x.Id != doc.Id && x.Type == doc.Type)) {
					ObservableOrderDocuments.Remove(doc);
				}
			}
			//Создаем отсутствующие
			foreach(var type in needCreate) {
				if(ObservableOrderDocuments.Any(x => x.Order?.Id == Id && x.Type == type)) {
					continue;
				}
				ObservableOrderDocuments.Add(CreateDocumentOfOrder(type));
			}
		}

		private OrderDocument CreateDocumentOfOrder(OrderDocumentType type)
		{
			OrderDocument newDoc;
			switch(type) {
				case OrderDocumentType.Bill:
					newDoc = new BillDocument();
					break;
				case OrderDocumentType.UPD:
					newDoc = new UPDDocument();
					break;
				case OrderDocumentType.Invoice:
					newDoc = new InvoiceDocument();
					break;
				case OrderDocumentType.InvoiceBarter:
					newDoc = new InvoiceBarterDocument();
					break;
				case OrderDocumentType.InvoiceContractDoc:
					newDoc = new InvoiceContractDoc();
					break;
				case OrderDocumentType.Torg12:
					newDoc = new Torg12Document();
					break;
				case OrderDocumentType.ShetFactura:
					newDoc = new ShetFacturaDocument();
					break;
				case OrderDocumentType.DriverTicket:
					newDoc = new DriverTicketDocument();
					break;
				case OrderDocumentType.RefundBottleDeposit:
					newDoc = new RefundBottleDepositDocument();
					break;
				case OrderDocumentType.RefundEquipmentDeposit:
					newDoc = new RefundEquipmentDepositDocument();
					break;
				case OrderDocumentType.BottleTransfer:
					newDoc = new BottleTransferDocument();
					break;
				case OrderDocumentType.DoneWorkReport:
					newDoc = new DoneWorkDocument();
					break;
				case OrderDocumentType.EquipmentTransfer:
					newDoc = new EquipmentTransferDocument();
					break;
				case OrderDocumentType.EquipmentReturn:
					newDoc = new EquipmentReturnDocument();
					break;

				default:
					throw new NotImplementedException();
			}
			newDoc.Order = newDoc.AttachedToOrder = this;
			return newDoc;
		}

		#endregion

		/// <summary>
		/// Закрывает заказ с самовывозом если по всем документам самовывоза со склада все отгружено
		/// </summary>
		public virtual bool TryCloseSelfDeliveryOrder(IUnitOfWork uow, SelfDeliveryDocument closingDocument)
		{
			// Закрывает заказ и создает операцию движения бутылей если все товары в заказе отгружены
			var unloadedItems = Repository.Store.SelfDeliveryRepository.OrderItemUnloaded(uow, this, closingDocument);
			var unloadedEquipments = Repository.Store.SelfDeliveryRepository.OrderEquipmentsUnloaded(uow, this, closingDocument);
			bool canCloseOrder = true;
			var shipmentCats = Nomenclature.GetCategoriesForShipment();
			foreach(var item in OrderItems.Where(x => shipmentCats.Contains(x.Nomenclature.Category))) {
				decimal totalCount = default(decimal);
				var deliveryItem = closingDocument.Items
				                                  .Where(x => x.OrderItem != null)
				                                  .FirstOrDefault(x => x.OrderItem.Id == item.Id);
				if(deliveryItem != null) {
					totalCount += deliveryItem.Amount;
				}
				if(unloadedItems.ContainsKey(item.Id)) {
					totalCount += unloadedItems[item.Id];
				}
				if((int)totalCount != item.Count) {
					canCloseOrder = false;
				}
			}

			foreach(var equipment in orderEquipments.Where(x => shipmentCats.Contains(x.Nomenclature.Category))) {
				decimal totalCount = default(decimal);
				var deliveryItem = closingDocument.Items
				                                  .Where(x => x.OrderEquipment != null)
				                                  .FirstOrDefault(x => x.OrderEquipment.Id == equipment.Id);
				if(deliveryItem != null) {
					totalCount += deliveryItem.Amount;
				}
				if(unloadedEquipments.ContainsKey(equipment.Id)) {
					totalCount += unloadedEquipments[equipment.Id];
				}
				if((int)totalCount != equipment.Count) {
					canCloseOrder = false;
				}
			}
			if(canCloseOrder) {
				UpdateBottlesMovementOperation(uow);
				ChangeStatus(OrderStatus.Closed);
			}
			return canCloseOrder;
		}

		public virtual void UpdateBottlesMovementOperation(IUnitOfWork uow, bool deleteOperation = false)
		{
			//По заказам, у которых проставлен крыжик "Закрывашка по контракту", 
			//не должны создаваться операции перемещения тары
			if(IsContractCloser)
				return;

			if(deleteOperation){
				if(BottlesMovementOperation != null) {
					uow.Delete(BottlesMovementOperation);
					BottlesMovementOperation = null;
				}
				return;
			}

			foreach(OrderItem item in OrderItems) {
				item.ActualCount = item.Count;
			}

			int amountDelivered = OrderItems
					.Where(item => item.Nomenclature.Category == NomenclatureCategory.water)
					.Sum(item => item.ActualCount);

			if(amountDelivered != 0 || (ReturnedTare != 0 && ReturnedTare != null)) {
				if(BottlesMovementOperation == null) {
					var bottlesOperation = new BottlesMovementOperation {
						OperationTime = DeliveryDate.Value.Date.AddHours(23).AddMinutes(59),
						Order = this,
						Delivered = amountDelivered,
						Returned = ReturnedTare.GetValueOrDefault(),
						Counterparty = Client,
						DeliveryPoint = DeliveryPoint
					};
					uow.Save(bottlesOperation);
					BottlesMovementOperation = bottlesOperation;
				} else {
					BottlesMovementOperation.OperationTime = DeliveryDate.Value.Date.AddHours(23).AddMinutes(59);
					BottlesMovementOperation.Delivered = amountDelivered;
					BottlesMovementOperation.Returned = ReturnedTare.GetValueOrDefault();
					uow.Save(BottlesMovementOperation);
				}
			} 
		}

		public virtual void SaveOrderComment(){
			if(Id == 0) return;
			
			using(var uow = UnitOfWorkFactory.CreateForRoot<Order>(Id)) {
				uow.Root.Comment = Comment;
				uow.Save();
				uow.Commit();
			}
		}

		public virtual void SaveEntity(IUnitOfWork uow)
		{
			SetOrderCreationDate();
			SetFirstOrder();
			LastEditor = EmployeeRepository.GetEmployeeForCurrentUser(UoW);
			LastEditedTime = DateTime.Now;
			ParseTareReason();
			uow.Save();
		}

		#endregion

		#region работа со скидками
		public virtual void SetDiscountUnitsForAll(DiscountUnits unit){
			ObservableOrderItems.ForEach(i => i.IsDiscountInMoney = unit == DiscountUnits.money);
		}

		/// <summary>
		/// Устанавливает скидку в рублях или процентах.
		/// Если скидка в %, то просто применяется к каждой строке заказа,
		/// а если в рублях - расчитывается % в зависимости от суммы заказа и рублёвой скидки
		/// и применяется этот % аналогично случаю с процентной скидкой.
		/// </summary>
		/// <param name="reason">Причина для скидки.</param>
		/// <param name="discount">Значение скидки.</param>
		/// <param name="unit">рубли или %.</param>
		public virtual void SetDiscount(DiscountReason reason, decimal discount, DiscountUnits unit)
		{
			if(unit == DiscountUnits.money) {
				var sum = ObservableOrderItems.Sum(i => i.CurrentCount * i.Price);
				if(sum == 0)
					return;
				discount = 100 * discount / sum;
			}
			foreach(OrderItem item in ObservableOrderItems) {
				if(unit == DiscountUnits.money)
					item.DiscountForPreview = discount * item.Price * item.CurrentCount / 100;
				else
					item.DiscountForPreview = discount;
				item.DiscountReason = reason;
			}
		}
		#endregion

		#region	Внутренние функции

		decimal GetFixedPrice(OrderItem item)
		{
			var fixedPrice = item.GetWaterFixedPrice();
			if(fixedPrice.HasValue) {
				return fixedPrice.Value;
			}
			return default(decimal);
		}

		decimal GetNomenclaturePrice(OrderItem item)
		{
			decimal nomenclaturePrice = 0M;
			if(item.Nomenclature.Category == NomenclatureCategory.water) {
				int totalWaterCount = ObservableOrderItems
											   .Where(x => x.Nomenclature.Category == NomenclatureCategory.water)
											   .Sum(x => x.Count);
				nomenclaturePrice = item.Nomenclature.GetPrice(totalWaterCount);
			} else {
				nomenclaturePrice = item.Nomenclature.GetPrice(item.Count);
			}

			return nomenclaturePrice;
		}

		void ObservableOrderDepositItems_ListContentChanged(object sender, EventArgs e)
		{
			OnPropertyChanged(nameof(TotalSum));
		}

		void ObservableOrderItems_ListContentChanged(object sender, EventArgs e)
		{
			OnPropertyChanged(nameof(TotalSum));
			UpdateDocuments();
		}

		#endregion

		#region Для расчетов в логистике

		/// <summary>
		/// Время разгрузки в секундах.
		/// </summary>
		public virtual int CalculateTimeOnPoint(bool hasForwarder)
		{
			int byFormula = 3 * 60; //На подпись документво 3 мин.
			int bottels = TotalDeliveredBottles;
			if(!hasForwarder)
				byFormula += CalculateGoDoorCount(bottels, 2) * 100; //100 секун(5/3 минуты) на 2 бутыли без экспедитора.
			else
				byFormula += CalculateGoDoorCount(bottels, 4) * 1 * 60; //1 минута на 4 бутыли c экспедитором.

			if(byFormula < 5 * 60) // минимальное время нахождения на адресе.
				return 5 * 60;
			else
				return byFormula;
		}

		private int CalculateGoDoorCount(int bottles, int atTime)
		{
			return bottles / atTime + (bottles % atTime > 0 ? 1 : 0);
		}

		/// <summary>
		/// Расчёт веса товаров и оборудования к клиенту для этого заказа
		/// </summary>
		/// <returns>Вес</returns>
		/// <param name="includeGoods">Если <c>true</c>, то в расчёт веса будут включены товары.</param>
		/// <param name="includeEquipment">Если <c>true</c>, то в расчёт веса будет включено оборудование.</param>
		public virtual double GetWeight(bool includeGoods = true, bool includeEquipment = true)
		{
			double weight = 0;
			if(includeGoods)
				weight += OrderItems.Sum(x => x.Nomenclature.Weight * x.Count);
			if(includeEquipment)
				weight += OrderEquipments.Where(x => x.Direction == Direction.Deliver)
										 .Sum(x => x.Nomenclature.Weight * x.Count);
			return weight;
		}

		#endregion

		#region Статические

		public static OrderStatus[] StatusesToExport1c => new[] {
			OrderStatus.Accepted,
			OrderStatus.Closed,
			OrderStatus.InTravelList,
			OrderStatus.OnLoading,
			OrderStatus.OnTheWay,
			OrderStatus.Shipped,
			OrderStatus.UnloadingOnStock
		};

		#endregion
	
		#region Операции

		public virtual void UpdateDepositOperations(IUnitOfWork uow)
		{
			var bottleRefundDeposit = ObservableOrderDepositItems.Where(x => x.DepositType == Operations.DepositType.Bottles).Sum(x => x.Total);
			var equipmentRefundDeposit = ObservableOrderDepositItems.Where(x => x.DepositType == Operations.DepositType.Equipment).Sum(x => x.Total);
			var operations = UpdateDepositOperations(uow, equipmentRefundDeposit, bottleRefundDeposit);
			operations.ForEach(x => uow.Save(x));
		}

		public virtual List<DepositOperation> UpdateDepositOperations(IUnitOfWork uow, decimal equipmentRefundDeposit, decimal bottleRefundDeposit)
		{
			var result = new List<DepositOperation>();
			DepositOperation bottlesOperation = null;
			DepositOperation equipmentOperation = null;
			bottlesOperation = DepositOperations.Where(x => x.DepositType == DepositType.Bottles).FirstOrDefault();
			equipmentOperation = DepositOperations.Where(x => x.DepositType == DepositType.Equipment).FirstOrDefault();

			//Залоги
			var bottleReceivedDeposit = OrderItems.Where(x => x.Nomenclature.TypeOfDepositCategory == TypeOfDepositCategory.BottleDeposit)
			                                      .Sum(x => x.ActualSum);
			var equipmentReceivedDeposit = OrderItems.Where(x => x.Nomenclature.TypeOfDepositCategory == TypeOfDepositCategory.EquipmentDeposit)
			                                         .Sum(x => x.ActualSum);

			//ЗАЛОГИ ЗА БУТЫЛИ
			if(bottleReceivedDeposit != 0m || bottleRefundDeposit != 0m) {
				if(bottlesOperation == null) {
					bottlesOperation = new DepositOperation {
						Order = this,
						OperationTime = DeliveryDate.Value.Date.AddHours(23).AddMinutes(59),
						DepositType = DepositType.Bottles,
						Counterparty = Client,
						DeliveryPoint = DeliveryPoint
					};
				}
				bottlesOperation.ReceivedDeposit = bottleReceivedDeposit;
				bottlesOperation.RefundDeposit = bottleRefundDeposit;
				result.Add(bottlesOperation);
			} else {
				if(bottlesOperation != null) {
					DepositOperations.Remove(bottlesOperation);
					UoW.Delete(bottlesOperation);
				}
			}

			//ЗАЛОГИ ЗА ОБОРУДОВАНИЕ
			if(equipmentReceivedDeposit != 0m || equipmentRefundDeposit != 0m) {
				if(equipmentOperation == null) {
					equipmentOperation = new DepositOperation {
						Order = this,
						OperationTime = DeliveryDate.Value.Date.AddHours(23).AddMinutes(59),
						DepositType = DepositType.Equipment,
						Counterparty = Client,
						DeliveryPoint = DeliveryPoint
					};
				}
				equipmentOperation.ReceivedDeposit = equipmentReceivedDeposit;
				equipmentOperation.RefundDeposit = equipmentRefundDeposit;
				result.Add(equipmentOperation);
			} else {
				if(equipmentOperation != null) {
					DepositOperations.Remove(equipmentOperation);
					UoW.Delete(equipmentOperation);
				}
			}
			return result;
		}

		#endregion
	}
}

