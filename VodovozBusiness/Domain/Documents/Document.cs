﻿using System;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using QSOrmProject;
using Vodovoz.Domain.Employees;

namespace Vodovoz.Domain.Documents
{
	public class Document : PropertyChangedBase, IDomainObject
	{
		public virtual int Id { get; set; }

		DateTime timeStamp = DateTime.Now;

		public virtual DateTime TimeStamp {
			get { return timeStamp; }
			set { SetField (ref timeStamp, value, () => TimeStamp); }
		}

		Employee author;

		[Display (Name = "Автор")]
		public virtual Employee Author {
			get { return author; }
			set { SetField (ref author, value, () => Author); }
		}

		Employee lastEditor;

		[Display (Name = "Последний редактор")]
		public virtual Employee LastEditor {
			get { return lastEditor; }
			set { SetField (ref lastEditor, value, () => LastEditor); }
		}

		DateTime lastEditedTime;

		[Display (Name = "Последние изменения")]
		public virtual DateTime LastEditedTime {
			get { return lastEditedTime; }
			set { SetField (ref lastEditedTime, value, () => LastEditedTime); }
		}

		public virtual string DateString { get { return TimeStamp.ToShortDateString () + " " + TimeStamp.ToShortTimeString (); } }

		public virtual string Number { get { return Id.ToString (); } }

		#region static

		public static Type GetDocClass(DocumentType docType)
		{
			switch(docType)
			{
				case DocumentType.IncomingInvoice:
					return typeof(IncomingInvoice);
				case DocumentType.IncomingWater:
					return typeof(IncomingWater);
				case DocumentType.MovementDocument:
					return typeof(MovementDocument);
				case DocumentType.WriteoffDocument:
					return typeof(WriteoffDocument);
				case DocumentType.SelfDeliveryDocument:
					return typeof(SelfDeliveryDocument);
				case DocumentType.CarLoadDocument:
					return typeof(CarLoadDocument);
				case DocumentType.CarUnloadDocument:
					return typeof(CarUnloadDocument);
				case DocumentType.InventoryDocument:
					return typeof(InventoryDocument);
				case DocumentType.ShiftChangeDocument:
					return typeof(ShiftChangeWarehouseDocument);
				case DocumentType.RegradingOfGoodsDocument:
					return typeof(RegradingOfGoodsDocument);
			}
			throw new NotSupportedException();
		}

		#endregion
	}

	public enum DocumentType
	{
		[Display(Name = "Входящая накладная")]
		IncomingInvoice,
		[Display(Name = "Документ производства")]
		IncomingWater,
		[Display(Name = "Документ перемещения")]
		MovementDocument,
		[Display(Name = "Акт списания")]
		WriteoffDocument,
		[Display(Name = "Отпуск самовывоза")]
		SelfDeliveryDocument,
		[Display(Name = "Талон погрузки")]
		CarLoadDocument,
		[Display(Name = "Талон разгрузки")]
		CarUnloadDocument,
		[Display(Name = "Инвентаризация")]
		InventoryDocument,
		[Display(Name = "Акт передачи склада")]
		ShiftChangeDocument,
		[Display (Name = "Пересортица товаров")]
		RegradingOfGoodsDocument
	}

	public class DocumentTypeStringType : NHibernate.Type.EnumStringType
	{
		public DocumentTypeStringType () : base (typeof(DocumentType))
		{
		}
	}

	public enum DefectSource
	{
		[Display(Name = "")]
		None,
		[Display(Name = "Водитель")]
		Driver,
		[Display(Name = "Клиент")]
		Client,
		[Display(Name = "Производство")]
		Production,
		[Display(Name = "Склад")]
		Warehouse
	}

	public class DefectSourceStringType : NHibernate.Type.EnumStringType
	{
		public DefectSourceStringType() : base(typeof(DefectSource))
		{
		}
	}
}

