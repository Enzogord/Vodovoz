using System;
using NLog;
using QSOrmProject;
using QSValidation;
using Vodovoz.Domain.Client;

namespace Vodovoz.Dialogs
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CreateCommentTemplateDlg : OrmGtkDialogBase<Comments>
	{
		protected static Logger logger = LogManager.GetCurrentClassLogger();



		public CreateCommentTemplateDlg( )
		{
			this.Build();
	 
		 	UoWGeneric = Comments.Create();
			ConfigureDlg();
		}

		public CreateCommentTemplateDlg(Comments sub) : this (sub.Id)
		{
		}

		public CreateCommentTemplateDlg(int id)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<Comments>(id);
			ConfigureDlg();
		}


		void ConfigureDlg()
		{
			
		    referenceGroup.SubjectType = typeof(CommentsGroups);
		    referenceGroup.Binding.AddBinding(Entity, e => e.CommentsGroups, w => w.Subject).InitializeFromSource();

			entryName.Binding.AddBinding(Entity, e => e.Author, w => w.Text).InitializeFromSource();
			entryComment.Binding.AddBinding(Entity, e => e.Text, w => w.Text).InitializeFromSource();
			 
		}

		public override bool Save()
		{
			var valid = new QSValidator<Comments>(UoWGeneric.Root);
			if(valid.RunDlgIfNotValid((Gtk.Window)this.Toplevel))
				return false;

			logger.Info("Сохраняем ...");
		
			UoWGeneric.Save();
			logger.Info("Ok");
			return true;
		}
	}
}
