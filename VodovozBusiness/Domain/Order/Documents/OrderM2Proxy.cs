﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QS.DomainModel.UoW;
using QS.Print;
using QSDocTemplates;
using Vodovoz.DocTemplates;
using Vodovoz.Domain.Employees;

namespace Vodovoz.Domain.Orders.Documents
{
	public class OrderM2Proxy : OrderDocument, IPrintableOdtDocument, ITemplateOdtDocument
	{
		#region implemented abstract members of OrderDocument

		public override OrderDocumentType Type {
			get {
				return OrderDocumentType.M2Proxy;
			}
		}

		#endregion

		M2ProxyDocument m2Proxy;

		[Display(Name = "Доверенность М-2")]
		public virtual M2ProxyDocument M2Proxy {
			get { return m2Proxy; }
			set { SetField(ref m2Proxy, value, () => M2Proxy); }
		}

		public override string Name {
			get {
				return String.Format("Доверенность М-2 №{0}", M2Proxy.Id);
			}
		}

		public override DateTime? DocumentDate {
			get { return M2Proxy?.Date; }
		}

		public virtual void PrepareTemplate(IUnitOfWork uow)
		{
			if(M2Proxy.DocumentTemplate == null)
				M2Proxy.UpdateM2ProxyDocumentTemplate(uow);

			if(M2Proxy.DocumentTemplate != null) {
				M2Proxy.DocumentTemplate.DocParser.SetDocObject(M2Proxy);
				var parser = (M2Proxy.DocumentTemplate.DocParser as M2ProxyDocumentParser);
				parser.AddTableEquipmentFromClient(Order.ObservableOrderEquipments.Where(eq => eq.Direction == Domain.Orders.Direction.PickUp).ToList<OrderEquipment>());
			}
		}

		public virtual IDocTemplate GetTemplate()
		{
			return M2Proxy.DocumentTemplate;
		}

		public override PrinterType PrintType => PrinterType.ODT;
	}
}
