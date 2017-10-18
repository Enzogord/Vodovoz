using System;
using System.Collections.Generic;
using GeoAPI.Geometries;
using GMap.NET;
using GMap.NET.GtkSharp;
using NetTopologySuite.Geometries;
using QSOrmProject;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Logistic;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueDateScheduleWidget : Gtk.Bin, IDialogueWidget
	{
		IUnitOfWork UoW;
		DateSchedule resultDateSchedule = new DateSchedule();
		DeliveryPoint dependencyDeliveryPoint;
		IList<ScheduleRestrictedDistrict> allRestrictions;
		ScheduleRestrictedDistrict district;
		Boolean dateSet = false, scheduleSet = false;

		public DialogueDateScheduleWidget(IUnitOfWork UoW)
		{
			this.Build();
			this.UoW = UoW;
			this.Configure();
		}

		public void Configure()
		{
			
			referenceSchedule.SubjectType = typeof(DeliverySchedule);

			pickerDate.Date = DateTime.Now.AddDays(1);
		}

		public event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		public event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		public void RefreshDependency(ScriptTreeObject ste)
		{
			
		}

		public void SendResults()
		{
			if(!dateSet 
			   || !scheduleSet 
			   || resultDateSchedule.Date == null 
			   || resultDateSchedule.Schedule == null)
			{
				return;
			}

			var result = new ScriptTreeObject {
				ResultObjectType = resultDateSchedule.GetType(),
				ResultObject = resultDateSchedule as object
			};
			this.SubWidgetDone(this, new SubWidgetDoneEventArgs(result));
		}

		protected void OnReferenceScheduleChanged(object sender, EventArgs e)
		{
			resultDateSchedule.Schedule = (DeliverySchedule)referenceSchedule.Subject;
			scheduleSet = true;
			SendResults();
		}

		protected void OnPickerDateDateChanged(object sender, EventArgs e)
		{
			resultDateSchedule.Date = pickerDate.Date;
			dateSet = true;
			SendResults();
		}

		IList<ScheduleRestrictedDistrict> GetRestrictions()
		{
			var restrictionsQuery = UoW.Session.QueryOver<ScheduleRestrictedDistrict>().List();

			return restrictionsQuery;
		}

		void CheckScheduleRestrictions()
		{
			var coord = new Coordinate() {
				X = dependencyDeliveryPoint.GmapPoint.Lat,
				Y = dependencyDeliveryPoint.GmapPoint.Lng
			};
			var deliveryPointCoord = new Point(coord);

			if(allRestrictions != null)
			{
				foreach(ScheduleRestrictedDistrict restriction in allRestrictions)
				{
					if(restriction.DistrictBorder.Contains(deliveryPointCoord))
					{
						district = restriction;
						return;
					}
				}
			}
		}
	}
}
