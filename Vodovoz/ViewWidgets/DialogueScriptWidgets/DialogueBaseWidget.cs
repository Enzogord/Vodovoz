﻿using System;
using System.Collections.Generic;
using QSOrmProject;
using QSWidgetLib;
using Vodovoz.Domain;
using System.Linq;
using Gamma.Utilities;
using NHibernate.Criterion;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueBaseWidget : Gtk.Bin
	{
		ScriptTreeElement ste;
		List<RadioButtonId<ScriptTreeElement>> subElements = new List<RadioButtonId<ScriptTreeElement>>();
		IUnitOfWork UoW;
		ScriptTreeElement next;
		ScriptTreeObject result = null;
		bool nextElementSet = true, 		// Устанавливает, выбран ли следующий элемент. Становится false, если есть саб-элементы.
				customActionDone = true,    // Устанавливает, выполнено ли действие в саб-виджете (специфические функции вроде выбора контрагента и т.д.)
				widgetDone = false;			// Устанавливает, закончена ли работа с виджетом.

		Gtk.Label labelText;
		Gtk.Button buttonNext;

		public event EventHandler<ScriptElementDoneEventArgs> ScriptElementDone;

		public DialogueBaseWidget(ScriptTreeElement ste)
		{
			this.Build();
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			this.ste = ste;
			this.Configure();
		}

		public void Configure()
		{
			labelText = new Gtk.Label(ste.Text);
			labelText.Show();

			buttonNext = new Gtk.Button();
			buttonNext.Clicked += OnNextButtonPressed;
			buttonNext.Show();

			var hboxBottom = new Gtk.HBox();
			hboxBottom.PackEnd(buttonNext, false, true, 0);
			hboxBottom.Show();

			vboxContainer.PackStart(labelText, true, true, 0);
			vboxContainer.PackEnd(hboxBottom, true, true, 0);

			FillSubElements();
			SetButtonNextParameters();
		}

		/// <summary>
		/// Заполняет саб-элементы (возможность выбора варианта), если указано более одного последующего элемента.
		/// </summary>
		public void FillSubElements()
		{
			List<string> subElementStrings = ste.ParseNextElements();
			if(subElementStrings.Count < 1) 
			{
				next = null;
				return;
			}

			if(subElementStrings.Count == 1)
			{
				SetNextElement(subElementStrings.First());
				return;
			}

			var subElementsQuery = new List<ScriptTreeElement>();

			foreach(string subElementString in subElementStrings)
			{
				subElementsQuery.AddRange(UoW.Session.QueryOver<ScriptTreeElement>()
				                     .Where(x => x.Name == subElementString)
				                     .List());
			}

		//	var subElementsQuery = UoW.Session.QueryOver<ScriptTreeElement>()
		//	                          .Where(x => subElementStrings.Contains(x.Name))
		//	                          .List();

			var hboxSubElements = new Gtk.HBox();

			foreach(ScriptTreeElement subElement in subElementsQuery)
			{
				var radioSubElement = new RadioButtonId<ScriptTreeElement>(subElement.Text);
				radioSubElement.ID = subElement;
				radioSubElement.Show();

				if(subElements.Count > 0)
				{
					radioSubElement.Group = subElements.First().Group;
				}

				radioSubElement.Active = false;
				radioSubElement.Clicked += OnSubElementRadioButtonActivated;
				hboxSubElements.PackStart(radioSubElement, true, true, 0);
				subElements.Add(radioSubElement);
			}

			hboxSubElements.Show();
			vboxContainer.PackEnd(hboxSubElements, true, true, 0);
			nextElementSet = false;
		}

		/// <summary>
		/// Устанавливает элемент, следующий после текущего.
		/// </summary>
		/// <param name="stringNext">Следующий элемент.</param>
		public void SetNextElement(string stringNext)
		{
			var nextElementQuery = UoW.Session.QueryOver<ScriptTreeElement>()
										  .Where(x => x.Name == stringNext)
										  .List().First();

			this.next = nextElementQuery;
			nextElementSet = true;
		}

		void OnNextButtonPressed(object sender, EventArgs e)
		{
			widgetDone = true;
			SetButtonNextParameters();
			ScriptElementDone(this, new ScriptElementDoneEventArgs(next, result));
		}

		void OnSubElementRadioButtonActivated(object sender, EventArgs e)
		{
			if(sender is RadioButtonId<ScriptTreeElement>)
			{
				string nextElement = (sender as RadioButtonId<ScriptTreeElement>).ID.NextElementsUnparced;

				if(nextElement.Contains(",")) // Так быть не должно, по крайней мере, пока что.
				{
					return;
				}

				SetNextElement(nextElement);
				SetButtonNextParameters();
			}

		}

		/// <summary>
		/// Устанавливает параметры кнопки "Далее" (возможность нажать и надпись).
		/// </summary>
		public void SetButtonNextParameters()
		{
			buttonNext.Label = ste.NextElementsUnparced == null || ste.NextElementsUnparced == "" ? "Завершить" : "Далее";

			buttonNext.Sensitive = nextElementSet && customActionDone && !widgetDone;
		}
	}

	public class ScriptElementDoneEventArgs : EventArgs
	{
		public ScriptTreeElement NextElement { get; private set; }
		public ScriptTreeObject Result { get; private set; }

		public ScriptElementDoneEventArgs(ScriptTreeElement nextElement, ScriptTreeObject result)
		{
			NextElement = nextElement;
			Result = result;
		}
	}
}
