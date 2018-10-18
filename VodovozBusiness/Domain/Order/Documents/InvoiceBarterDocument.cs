﻿using System;
using System.Collections.Generic;
using QS.Print;
using QS.Report;

namespace Vodovoz.Domain.Orders.Documents
{
	public class InvoiceBarterDocument : OrderDocument, IPrintableRDLDocument
	{
		#region implemented abstract members of OrderDocument
		public virtual ReportInfo GetReportInfo ()
		{
			return new ReportInfo {
				Title = String.Format ("Накладная №{0} от {1:d} (безденежно)", Order.Id, Order.DeliveryDate),
				Identifier = "Documents.InvoiceBarter",
				Parameters = new Dictionary<string, object> {
					{ "order_id",  Order.Id }
				}
			};
		}
		public override OrderDocumentType Type {
			get {
				return OrderDocumentType.InvoiceBarter;
			}
		}
		#endregion

		public override string Name { get { return String.Format ("Накладная №{0} (бартер)",Order.Id); }  }

		public override DateTime? DocumentDate {
			get { return Order?.DeliveryDate; }
		}

		public override PrinterType PrintType {
			get {
				return PrinterType.RDL;
			}
		}
	}
}

