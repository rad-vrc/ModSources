using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using SDL2;
using Terraria.Localization;

namespace Terraria.ModLoader.Engine
{
	/// <summary>
	/// This class handles displaying errors that require a OS-provided modal message box. Fatal errors and errors that happen in situations where a suitable place to display an error doesn't exist (such as when initially loading). 
	/// </summary>
	// Token: 0x020002AD RID: 685
	internal class ErrorReporting
	{
		// Token: 0x06002D01 RID: 11521 RVA: 0x0052B5E0 File Offset: 0x005297E0
		internal static void MessageBoxShow(string message, bool fatal = false)
		{
			string title = ModLoader.versionedName + (fatal ? " Error" : " Fatal Error");
			string logDir = Path.GetFullPath(Logging.LogDir);
			string logFileName = (Logging.LogPath == null) ? "Natives.log" : Path.GetFileName(Logging.LogPath);
			string logHint = Language.GetTextValue("tModLoader.LogPathHint", logFileName, logDir);
			if (Language.ActiveCulture == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(113, 2);
				defaultInterpolatedStringHandler.AppendLiteral("A ");
				defaultInterpolatedStringHandler.AppendFormatted(logFileName);
				defaultInterpolatedStringHandler.AppendLiteral(" file containing error information has been generated in\n");
				defaultInterpolatedStringHandler.AppendFormatted(logDir);
				defaultInterpolatedStringHandler.AppendLiteral("\n(You will need to share this file if asking for help)");
				logHint = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			message = message + "\n\n" + logHint;
			try
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Out.WriteLine(title + "\n" + message);
				SDL.SDL_ShowSimpleMessageBox(16, title, message, IntPtr.Zero);
			}
			catch
			{
			}
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x0052B6D4 File Offset: 0x005298D4
		public static void FatalExit(string message)
		{
			if (Logging.LogPath == null)
			{
				Console.Error.WriteLine(message);
			}
			else
			{
				Logging.tML.Fatal(message);
			}
			TerrariaSteamClient.Shutdown();
			ErrorReporting.MessageBoxShow(message, true);
			Environment.Exit(1);
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0052B708 File Offset: 0x00529908
		public static void FatalExit(string message, Exception e)
		{
			try
			{
				string error = SDL.SDL_GetError();
				if (error != null && !string.IsNullOrWhiteSpace(error))
				{
					message = message + "\n\nSDL Error: " + error;
				}
			}
			catch
			{
			}
			if (e.HelpLink != null)
			{
				try
				{
					Utils.OpenToURL(e.HelpLink);
				}
				catch
				{
				}
			}
			string tip = null;
			if (e is OutOfMemoryException)
			{
				tip = Language.GetTextValue("tModLoader.OutOfMemoryHint");
			}
			else if (e is InvalidOperationException || e is NullReferenceException || e is IndexOutOfRangeException || e is ArgumentNullException)
			{
				tip = Language.GetTextValue("tModLoader.ModExceptionHint");
			}
			else if (e is IOException && e.Message.Contains("cloud file provider"))
			{
				if (string.IsNullOrEmpty(e.HelpLink))
				{
					e.HelpLink = "https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Usage-FAQ#save-data-file-issues";
				}
				tip = Language.GetTextValue("tModLoader.OneDriveHint");
				if (Language.ActiveCulture == null)
				{
					tip = "Tip: Try installing/enabling OneDrive. Right click your Documents folder and enable \"Always save on this device\"";
				}
			}
			else if (e is SynchronizationLockException)
			{
				tip = Language.GetTextValue("tModLoader.AntivirusHint");
			}
			else if (e is TypeInitializationException)
			{
				tip = Language.GetTextValue("tModLoader.TypeInitializationHint");
			}
			if (e.HelpLink != null)
			{
				try
				{
					Utils.OpenToURL(e.HelpLink);
				}
				catch
				{
				}
			}
			if (tip != null)
			{
				message = message + "\n\n" + tip;
			}
			message = message + "\n\n" + ((e != null) ? e.ToString() : null);
			ErrorReporting.FatalExit(message);
		}

		/// <summary>
		/// Shows an OS-provided modal message box displaying a message and a number of buttons and returns the button index of the user-selected option. The first option will be mapped to return key and the last option to escape key. The options are displayed from right to left in order.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06002D04 RID: 11524 RVA: 0x0052B880 File Offset: 0x00529A80
		internal static int ShowMessageBoxWithChoices(string title, string message, string[] buttonLabels)
		{
			SDL.SDL_MessageBoxButtonData[] buttons = new SDL.SDL_MessageBoxButtonData[buttonLabels.Length];
			for (int i = 0; i < buttonLabels.Length; i++)
			{
				SDL.SDL_MessageBoxButtonData[] array = buttons;
				int num = i;
				SDL.SDL_MessageBoxButtonData sdl_MessageBoxButtonData = default(SDL.SDL_MessageBoxButtonData);
				sdl_MessageBoxButtonData.flags = 0;
				sdl_MessageBoxButtonData.buttonid = i;
				sdl_MessageBoxButtonData.text = buttonLabels[i];
				array[num] = sdl_MessageBoxButtonData;
				if (i == 0)
				{
					buttons[i].flags = 1;
				}
				else if (i == buttonLabels.Length - 1)
				{
					buttons[i].flags = 2;
				}
			}
			SDL.SDL_MessageBoxData sdl_MessageBoxData = default(SDL.SDL_MessageBoxData);
			sdl_MessageBoxData.flags = 64;
			sdl_MessageBoxData.window = IntPtr.Zero;
			sdl_MessageBoxData.title = title;
			sdl_MessageBoxData.message = message;
			sdl_MessageBoxData.numbuttons = buttons.Length;
			sdl_MessageBoxData.buttons = buttons;
			sdl_MessageBoxData.colorScheme = null;
			SDL.SDL_MessageBoxData messageBoxData = sdl_MessageBoxData;
			int buttonID;
			if (SDL.SDL_ShowMessageBox(ref messageBoxData, ref buttonID) < 0)
			{
				Logging.tML.Info("ShowMessageBoxWithChoices: Error displaying message box");
			}
			if (buttonID == -1)
			{
				Logging.tML.Info("ShowMessageBoxWithChoices: No selection");
			}
			return buttonID;
		}

		/// <summary> Writes an error to stderr using the <see href="https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-diagnostic-format-for-tasks?view=vs-2022">"MSBuild and Visual Studio format for diagnostic messages"</see>.
		/// <para /> This means the error will show up in "Error List" in VS directly. </summary>
		// Token: 0x06002D05 RID: 11525 RVA: 0x0052B974 File Offset: 0x00529B74
		internal static void LogStandardDiagnosticError(string message, ErrorReporting.TMLErrorCode errorCode, bool error = true, string origin = "tModLoader", string subCategory = "Mod Build")
		{
			TextWriter error2 = Console.Error;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 5);
			defaultInterpolatedStringHandler.AppendFormatted(origin);
			defaultInterpolatedStringHandler.AppendLiteral(": ");
			defaultInterpolatedStringHandler.AppendFormatted(subCategory);
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			defaultInterpolatedStringHandler.AppendFormatted(error ? "error" : "warning");
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			defaultInterpolatedStringHandler.AppendFormatted<ErrorReporting.TMLErrorCode>(errorCode);
			defaultInterpolatedStringHandler.AppendLiteral(": ");
			defaultInterpolatedStringHandler.AppendFormatted(message);
			error2.WriteLine(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		/// <summary> Various error codes to show in Visual Studio. Mainly used to cross reference with source code. Subject to change if more granular error codes are needed. </summary>
		// Token: 0x02000A53 RID: 2643
		internal enum TMLErrorCode
		{
			// Token: 0x04006CE5 RID: 27877
			TML001,
			// Token: 0x04006CE6 RID: 27878
			TML002,
			// Token: 0x04006CE7 RID: 27879
			TML003
		}
	}
}
