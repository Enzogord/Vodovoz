using System;
using System.Collections.Generic;
using QSOrmProject;
using QSWidgetLib;
using Vodovoz.Domain;
using System.Linq;
using Gamma.Utilities;
using NHibernate.Criterion;
using Vodovoz.Dialogs;
using QSTDI;
using Vodovoz.Repository;
using QSProjectsLib;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DialogueBaseWidget : WidgetOnTdiTabBase
	{
		#region fields
		ScriptTreeElement ste;
		List<RadioButtonId<ScriptTreeElement>> subElements = new List<RadioButtonId<ScriptTreeElement>>();
		IUnitOfWork UoW;
		ScriptTreeElement next;
		ScriptTreeObject result = null,
						 dependencyObject;
		bool nextElementSet = true,         // Устанавливает, выбран ли следующий элемент. Становится false, если есть саб-элементы.
			 customActionDone = true,       // Устанавливает, выполнено ли действие в саб-виджете (специфические функции вроде выбора контрагента и т.д.)
			 widgetDone = false;            // Устанавливает, закончена ли работа с виджетом.

		Gtk.Label  label;
		Gtk.Label  dialogue;
		Gtk.Button buttonNext;

		Gtk.Button buttonYes;

		IDialogueWidget subWidget;
		#endregion

		public event EventHandler<ScriptElementDoneEventArgs> ScriptElementDone;
		public event EventHandler<ScriptElementDoneEventArgs> ScriptElementChanged;

		public DialogueBaseWidget(IUnitOfWork UoW, ScriptTreeElement ste, ScriptTreeObject dependencyObject)
		{
			this.Build();
			this.UoW = UoW;
			this.ste = ste;
			this.dependencyObject = dependencyObject;
			this.Configure();


		}

		public void Configure()
		{
			//var employeeVar = EmployeeRepository.ActiveEmployeeOrderedQuery().Take(1);
			//employeeVar
			//var employeeVar2 = QSMain.User.Name;
			var employeeName = EmployeeRepository.GetEmployeeForCurrentUser(UoW).Name;

			label = new Gtk.Label(ste.Label);
			label.Show();

			//dialogue = new Gtk.Label(ste.Text.Replace("!+", employeeName));
			dialogue = new Gtk.Label(ste.Text);
			dialogue.Show();

			buttonNext = new Gtk.Button();
			buttonNext.Clicked += OnNextButtonPressed;
			buttonNext.Show();

			var hboxBottom = new Gtk.HBox();
			hboxBottom.PackEnd(buttonNext, false, true, 0);
			hboxBottom.Show();

			//var hboxTop = new Gtk.HBox();
			//hboxBottom.PackEnd(dialogue, false, true, 0);
			//hboxBottom.Show();

			vboxContainer.PackStart(label, true, true, 0);
			vboxContainer.PackStart(dialogue, false, false, 0);
			//vboxContainer.PackStart(hboxTop, false, true, 0);
			vboxContainer.PackEnd(hboxBottom, true, true, 0);

			if(CheckButtons()) {
				AddButtons();
			} else {
				FillSubElements();
			}

			AddSubWidget();

			SetButtonNextParameters();
		}

		private bool CheckButtons()
		{
			var getWidget = UoW.Session.QueryOver<ScriptTreeElement>()
							   .Where(x => x.Buttons == true).List().FirstOrDefault();
			//  .Select(x => x.Buttons).List().First();
			if(getWidget != null)
				return getWidget.Buttons;

			return false;
		}

		private void AddButtons()
		{
			buttonYes = new Gtk.Button();
			buttonYes.Clicked += OnbuttonOnePressed;
			buttonYes.Label = "да";
			//hboxBottom.PackEnd(buttonNext, false, true, 0);
			buttonYes.Show();
		}

		private void OnbuttonOnePressed(object sender, EventArgs e)
		{
			buttonNext.Sensitive = true;
		}

		/// <summary>
		/// Заполняет саб-элементы (возможность выбора варианта), если указано более одного последующего элемента.
		/// </summary>
		public void FillSubElements()
		{
			List<string> subElementStrings = ste.ParseNextElements();
			if(subElementStrings.Count < 1) {
				next = null;
				return;
			}

			if(subElementStrings.Count == 1) {
				SetNextElement(subElementStrings.First());
				return;
			}

			var subElementsQuery = new List<ScriptTreeElement>();

			foreach(string subElementString in subElementStrings) {
				subElementsQuery.AddRange(UoW.Session.QueryOver<ScriptTreeElement>()
									 .Where(x => x.Name == subElementString)
									 .List());
			}

			//	var subElementsQuery = UoW.Session.QueryOver<ScriptTreeElement>()
			//	                          .Where(x => subElementStrings.Contains(x.Name))
			//	                          .List();

			var hboxSubElements = new Gtk.HBox();

			foreach(ScriptTreeElement subElement in subElementsQuery) {
				var radioSubElement = new RadioButtonId<ScriptTreeElement>(subElement.Text);
				radioSubElement.ID = subElement;
				radioSubElement.Show();

				if(subElements.Count > 0) {
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

		public void AddSubWidget()
		{
			subWidget = GetSubWidgetType(ste.Widget);

			if(subWidget != null) {
				subWidget.SubWidgetDone += OnSubWidgetDone;
				subWidget.TextCorrectionsPresent += OnTextCorrectionsPresent;
				(subWidget as Gtk.Widget).Show();
				vboxContainer.PackEnd(subWidget as Gtk.Widget);
				//customActionDone = false;
				subWidget.RefreshDependency(dependencyObject);
			}
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

		//Обработчик кнопки перехода
		void OnNextButtonPressed(object sender, EventArgs e)
		{
			widgetDone = true;
			SetButtonNextParameters();
			this.ScriptElementDone(this, new ScriptElementDoneEventArgs(ste.Name, next, result));
		}

		//непонятная проверка, есть ли в субэлементе 1 уровня ссылка на МНОЖЕСТВО субэлементов 2 уровня 
		void OnSubElementRadioButtonActivated(object sender, EventArgs e)
		{
			if(sender is RadioButtonId<ScriptTreeElement>) {
				string nextElement = (sender as RadioButtonId<ScriptTreeElement>).ID.NextElementsUnparced;

				if(nextElement.Contains(",")) // Так быть не должно, по крайней мере, пока что.
				{
					return;
				}

				SetNextElement(nextElement);
				SetButtonNextParameters();
			}

		}

		void OnSubWidgetDone(object sender, SubWidgetDoneEventArgs e)
		{
			result = e.Result;

			if(customActionDone && widgetDone) {
				this.ScriptElementChanged?.Invoke(this, new ScriptElementDoneEventArgs(ste.Name, next, result));
			}

			customActionDone = true;
			//(sender as Gtk.Widget).Sensitive = false; // Запрещает редактировать данные в саб-виджете. 
			SetButtonNextParameters();
		}

		//TODO переписать на нормальный вывод текста

		/// <summary>
		/// Коректируем текст, если дергаем событие из виджета
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">коректирующий текст.</param>
		void OnTextCorrectionsPresent(object sender, TextCorrectionsPresentEventArgs e)
		{
			for(int i = 0; i < e.Corrections.Length; i++) {
				//label.Text = label.Text.Replace("#" + i.ToString() + "#", e.Corrections[i]);
				dialogue.Text = dialogue.Text.Replace("!+", e.Corrections[i]);
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

		/// <summary>
		/// Получить тип сабвиджета.
		/// </summary>
		/// <returns>Тип сабвиджета, если он определён, и null, если нет или nowidget.</returns>
		/// <param name="widgetType">Енам с типом виджета.</param>
		IDialogueWidget GetSubWidgetType(DialogueScriptWidget widgetType)
		{
			switch(widgetType) {
				case DialogueScriptWidget.welcome:
					return new DialogueWelcomeWidget(UoW);
				case DialogueScriptWidget.text:
					return new DialogueTextWidget(UoW);
				case DialogueScriptWidget.counterparty:
					return new DialogueCounterpartyWidget(UoW);
				case DialogueScriptWidget.deliverypoint:
					return new DialogueDeliveryPointWidget(UoW);
				case DialogueScriptWidget.datetime:
					return new DialogueDateTimeWidget(UoW);
				case DialogueScriptWidget.dateschedule:
					return new DialogueDateScheduleWidget(UoW);
				case DialogueScriptWidget.checkschedule:
					return new DialogueCheckScheduleWidget(UoW);
				case DialogueScriptWidget.neworder:
					return new DialogueNewOrderWidget(UoW);
				case DialogueScriptWidget.orderrepeat:
					return new DialogueOrderRepeatWidget(UoW);
				case DialogueScriptWidget.checkemptybottles:
					return new DialogueCheckEmptyBottlesWidget(UoW);
				case DialogueScriptWidget.constructor:
					return new DialogueConstructorWidget(UoW);
				case DialogueScriptWidget.leaveandprint:
					return new DialogueleaveAndPrintWidgets(UoW);
				case DialogueScriptWidget.equipments:
					return new DialogueEquipmentWidget(UoW);
				case DialogueScriptWidget.additional:
					return new DialogueAdditionalWidget(UoW);
				
				case DialogueScriptWidget.sanitization:
					return new DialogueSanitizationWidget(UoW);

				case DialogueScriptWidget.changeandstamp:
					return new DialogueChangeAndStampWidget(UoW);

				default:
					return null;
			}
		}

		/// <summary>
		/// Обновляем зависимости у сабвиджетов.
		/// </summary>
		/// <param name="dependencyObject">Dependency object.</param>
		public void RefreshDependency(ScriptTreeObject dependencyObject)
		{
			this.dependencyObject = dependencyObject;
			subWidget.RefreshDependency(this.dependencyObject);
		}
	}

	public class ScriptElementDoneEventArgs : EventArgs
	{
		public string CurrentElement { get; private set; }
		public ScriptTreeElement NextElement { get; private set; }
		public ScriptTreeObject Result { get; private set; }

		public ScriptElementDoneEventArgs(string currentElement, ScriptTreeElement nextElement, ScriptTreeObject result)
		{
			CurrentElement = currentElement;
			NextElement = nextElement;
			Result = result;
		}
	}
}
