using System;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using SDL2;

namespace Terraria.ModLoader.Engine
{
	// Token: 0x020002B0 RID: 688
	internal static class FNAFixes
	{
		// Token: 0x06002D10 RID: 11536 RVA: 0x0052BC8C File Offset: 0x00529E8C
		internal static void Init()
		{
			if (OperatingSystem.IsWindows())
			{
				SDL.SDL_SetHintWithPriority("SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS", "0", 2);
			}
			string steamDeckValue = Environment.GetEnvironmentVariable("SteamDeck");
			if (steamDeckValue != null && steamDeckValue == "1")
			{
				Logging.FNA.Info("SteamDeck detected, configuring keyboard input workaround.");
				SDL.SDL_SetHintWithPriority("SDL_ENABLE_SCREEN_KEYBOARD", "0", 2);
				SDL.SDL_StartTextInput();
			}
			FNAFixes.ConfigureDrivers();
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x0052BCF8 File Offset: 0x00529EF8
		private static void ConfigureDrivers()
		{
			string sdlHintName = "SDL_VIDEODRIVER";
			string launchArg = "-videodriver";
			int numDrivers = SDL.SDL_GetNumVideoDrivers();
			Func<int, string> getDriver;
			if ((getDriver = FNAFixes.<>O.<0>__SDL_GetVideoDriver) == null)
			{
				getDriver = (FNAFixes.<>O.<0>__SDL_GetVideoDriver = new Func<int, string>(SDL.SDL_GetVideoDriver));
			}
			FNAFixes.ConfigureDrivers(sdlHintName, launchArg, numDrivers, getDriver);
			string sdlHintName2 = "SDL_AUDIODRIVER";
			string launchArg2 = "-audiodriver";
			int numDrivers2 = SDL.SDL_GetNumAudioDrivers();
			Func<int, string> getDriver2;
			if ((getDriver2 = FNAFixes.<>O.<1>__SDL_GetAudioDriver) == null)
			{
				getDriver2 = (FNAFixes.<>O.<1>__SDL_GetAudioDriver = new Func<int, string>(SDL.SDL_GetAudioDriver));
			}
			FNAFixes.ConfigureDrivers(sdlHintName2, launchArg2, numDrivers2, getDriver2);
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x0052BD64 File Offset: 0x00529F64
		private static void ConfigureDrivers(string sdlHintName, string launchArg, int numDrivers, Func<int, string> getDriver)
		{
			string[] drivers = (from n in Enumerable.Range(0, numDrivers).Select(getDriver)
			where n != null
			select n).ToArray<string>();
			string defaultDriverString = string.Join(",", drivers);
			string launchArgValue;
			if (Program.LaunchParameters.TryGetValue(launchArg, out launchArgValue))
			{
				Environment.SetEnvironmentVariable(sdlHintName, launchArgValue);
			}
			string envVarValue = Environment.GetEnvironmentVariable(sdlHintName);
			if (envVarValue != null)
			{
				ILog fna = Logging.FNA;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(55, 3);
				defaultInterpolatedStringHandler.AppendLiteral("Detected ");
				defaultInterpolatedStringHandler.AppendFormatted(sdlHintName);
				defaultInterpolatedStringHandler.AppendLiteral("=");
				defaultInterpolatedStringHandler.AppendFormatted(envVarValue);
				defaultInterpolatedStringHandler.AppendLiteral(". Appending default driver list as fallback: ");
				defaultInterpolatedStringHandler.AppendFormatted(defaultDriverString);
				fna.Info(defaultInterpolatedStringHandler.ToStringAndClear());
				SDL.SDL_SetHintWithPriority(sdlHintName, envVarValue + "," + defaultDriverString, 2);
			}
		}

		// Token: 0x02000A56 RID: 2646
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006CED RID: 27885
			public static Func<int, string> <0>__SDL_GetVideoDriver;

			// Token: 0x04006CEE RID: 27886
			public static Func<int, string> <1>__SDL_GetAudioDriver;
		}
	}
}
