﻿//************************************************************************************************
// Copyright © 2016 Steven M Cohn.  All rights reserved.
//************************************************************************************************

namespace River.OneMoreAddIn.Commands
{
	using River.OneMoreAddIn.Styles;
	using River.OneMoreAddIn.UI;
	using System.Threading.Tasks;
	using System.Windows.Forms;


	/// <summary>
	/// Edit custom styles
	/// </summary>
	internal class EditStylesCommand : Command
	{
		public EditStylesCommand()
		{
			// prevent replay
			IsCancelled = true;
		}


		public override async Task Execute(params object[] args)
		{
			using var one = new OneNote(out var page, out _, OneNote.PageDetail.Basic);
			var pageColor = page.GetPageColor(out _, out var black);
			if (black)
			{
				// if Office Black theme, translate to softer Black Shadow
				pageColor = BasicColors.BlackSmoke;
			}

			var theme = new ThemeProvider().Theme;

			var dialog = new StyleDialog(theme, pageColor);
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				ThemeProvider.Save(dialog.Theme);
				ThemeProvider.RecordTheme(dialog.Theme.Key);

				ribbon.Invalidate();
			}

			ribbon.Invalidate();

			await Task.Yield();
		}
	}
}
