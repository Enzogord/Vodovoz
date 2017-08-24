using System;
using Vodovoz.Domain;

namespace Vodovoz.ViewWidgets.DialogueScriptWidgets
{
	public interface IDialogueWidget
	{
		event EventHandler<SubWidgetDoneEventArgs> SubWidgetDone;
		event EventHandler<TextCorrectionsPresentEventArgs> TextCorrectionsPresent;

		void RefreshDependency(ScriptTreeObject ste);
	}

	public class SubWidgetDoneEventArgs : EventArgs
	{
		public ScriptTreeObject Result;

		public SubWidgetDoneEventArgs(ScriptTreeObject result)
		{
			Result = result;
		}
	}

	public class TextCorrectionsPresentEventArgs : EventArgs
	{
		public string Corrections { get; private set; }

		public TextCorrectionsPresentEventArgs(string corrections)
		{
			Corrections = corrections;
		}
	}
}
