﻿using System;
using System.Collections.Generic;
using QS.Print;
using QS.Report;

namespace Vodovoz.Domain.Orders.Documents
{
	public class DoneWorkDocument:OrderDocument, IPrintableRDLDocument
	{
		#region implemented abstract members of OrderDocument

		public override OrderDocumentType Type {
			get {
				return OrderDocumentType.DoneWorkReport;
			}
		}

		public virtual ReportInfo GetReportInfo ()
		{
			return new ReportInfo {
				Title = Name,
				Identifier = "Documents.DoneWorkReport",
				Parameters = new Dictionary<string, object> {
					{ "order_id",  Order.Id }
				}
			};
		}

		#endregion


		public override string Name {
			get { return String.Format ("Акт выполненных работ"); }
		}

		public override DateTime? DocumentDate {
			get { return Order?.DeliveryDate; }
		}

		public override PrinterType PrintType {
			get {
				return PrinterType.RDL;
			}
		}

		public override DocumentOrientation Orientation {
			get {
				return DocumentOrientation.Portrait;
			}
		}
	}
}

