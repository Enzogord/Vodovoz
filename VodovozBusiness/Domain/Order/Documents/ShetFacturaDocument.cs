﻿using System;
using System.Collections.Generic;
using QS.Print;
using QS.Report;

namespace Vodovoz.Domain.Orders.Documents
{
	public class ShetFacturaDocument:OrderDocument, IPrintableRDLDocument
	{
		#region implemented abstract members of OrderDocument

		public virtual ReportInfo GetReportInfo ()
		{
			return new ReportInfo {
				Title = String.Format ("Счет-Фактура {0} от {1:d}", Order.Id, Order.DeliveryDate),
				Identifier = "Documents.ShetFactura",
				Parameters = new Dictionary<string, object> {
					{ "order_id", Order.Id }
				}
			};
		}

		public override OrderDocumentType Type {
			get {
				return OrderDocumentType.ShetFactura;
			}
		}

		#endregion

		public override string Name { get { return String.Format ("Счет-Фактура №{0}", Order.Id); } }

		public override DateTime? DocumentDate {
			get { return Order?.DeliveryDate; }
		}

		public override PrinterType PrintType {
			get {
				return PrinterType.RDL;
			}
		}

		public override DocumentOrientation Orientation
		{
			get
			{
				return DocumentOrientation.Landscape;
			}
		}
	}
}

