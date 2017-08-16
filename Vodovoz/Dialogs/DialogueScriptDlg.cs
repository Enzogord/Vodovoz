using System;
using System.Collections.Generic;
using QSOrmProject;
using QSTDI;
using Vodovoz.Domain;
using Vodovoz.ViewWidgets.DialogueScriptWidgets;

namespace Vodovoz.Dialogs
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueScriptDlg : Gtk.Bin, ITdiTab
	{
		Dictionary<string, ScriptTreeElement> scriptTreeElements;
		IUnitOfWork UoW;

		public DialogueScriptDlg()
		{
			this.Build();
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			scriptTreeElements = GetAllScriptElements();
			Configure();
		}

		public void Configure()
		{
			DialogueBaseWidget firstElement;

			if(scriptTreeElements.TryGetValue("START", out var element))
			{
				firstElement = new DialogueBaseWidget(element);
			} else
			{
				return;
			}

			firstElement.ScriptElementDone += OnScriptElementDone;
			firstElement.Show();
			vbox2.PackStart(firstElement, false, true, 0);
		}

		#region ITdiTab_implementation

		public HandleSwitchIn HandleSwitchIn { get; private set; }

		public HandleSwitchOut HandleSwitchOut { get; private set; }

		public string TabName { get { return "NYI"; } private set {; } }

		public ITdiTabParent TabParent { get ; set ; }

		public bool FailInitialize { get { return false;  } }

		public event EventHandler<TdiTabNameChangedEventArgs> TabNameChanged;
		public event EventHandler<TdiTabCloseEventArgs> CloseTab;

		public bool CompareHashName(string hashName)
		{
			throw new NotImplementedException();
		}

		#endregion

		public Dictionary<string, ScriptTreeElement> GetAllScriptElements()
		{
			Dictionary<string, ScriptTreeElement> allScriptElements = new Dictionary<string, ScriptTreeElement>();

			var steQuery = UoW.Session.CreateCriteria<ScriptTreeElement>()
							  .List<ScriptTreeElement>();

			foreach(ScriptTreeElement ste in steQuery) {
				allScriptElements.Add(ste.Name, ste);
			}

			return allScriptElements;
		}

		void OnScriptElementDone(object sender, ScriptElementDoneEventArgs e)
		{
			if(e.NextElement == null)
			{
				return;
			}

			NextElement(e.NextElement);
		}
	

		public void NextElement(ScriptTreeElement ste)
		{
			var element = new DialogueBaseWidget(ste);
			element.ScriptElementDone += OnScriptElementDone;
			element.Show();
			vbox2.PackStart(element, false, true, 0);
		}
	}
}
