using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using log4net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDL2;

namespace Terraria.ModLoader.Engine
{
	/// <summary>
	/// Attempt to track spurious device resets, backbuffer flickers and resizes
	/// Also setup some FNA logging
	/// </summary>
	// Token: 0x020002B1 RID: 689
	internal static class FNALogging
	{
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x0052BE3F File Offset: 0x0052A03F
		// (set) Token: 0x06002D14 RID: 11540 RVA: 0x0052BE46 File Offset: 0x0052A046
		public static string DriverIdentifier { get; internal set; }

		// Token: 0x06002D15 RID: 11541 RVA: 0x0052BE50 File Offset: 0x0052A050
		internal static void RedirectLogs()
		{
			FNALoggerEXT.LogInfo = delegate(string s)
			{
				if (FNALogging.DriverIdentifier == null && s.StartsWith("FNA3D Driver: "))
				{
					FNALogging.DriverIdentifier = s.Substring("FNA3D Driver: ".Length);
					Logging.FNA.Info("SDL Video Diver: " + SDL.SDL_GetCurrentVideoDriver());
				}
				Logging.FNA.Info(s);
			};
			FNALoggerEXT.LogWarn = delegate(string s)
			{
				Logging.FNA.Warn(s);
			};
			FNALoggerEXT.LogError = delegate(string s)
			{
				Logging.FNA.Error(s);
			};
			Logging.FNA.Debug("Querying linked library versions...");
			SDL.SDL_version sdl_version;
			SDL.SDL_GetVersion(ref sdl_version);
			ILog fna = Logging.FNA;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 3);
			defaultInterpolatedStringHandler.AppendLiteral("SDL v");
			defaultInterpolatedStringHandler.AppendFormatted<byte>(sdl_version.major);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted<byte>(sdl_version.minor);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted<byte>(sdl_version.patch);
			fna.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			uint fna3d_version = FNA3D.FNA3D_LinkedVersion();
			ILog fna2 = Logging.FNA;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 3);
			defaultInterpolatedStringHandler.AppendLiteral("FNA3D v");
			defaultInterpolatedStringHandler.AppendFormatted<uint>(fna3d_version / 10000U);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted<uint>(fna3d_version / 100U % 100U);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted<uint>(fna3d_version % 100U);
			fna2.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			uint faudio_version = FAudio.FAudioLinkedVersion();
			ILog fna3 = Logging.FNA;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 3);
			defaultInterpolatedStringHandler.AppendLiteral("FAudio v");
			defaultInterpolatedStringHandler.AppendFormatted<uint>(faudio_version / 10000U);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted<uint>(faudio_version / 100U % 100U);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted<uint>(faudio_version % 100U);
			fna3.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x0052C01C File Offset: 0x0052A21C
		public static void GraphicsInit(GraphicsDeviceManager graphics)
		{
			EventHandler<EventArgs> eventHandler;
			if ((eventHandler = FNALogging.<>O.<0>__LogDeviceReset) == null)
			{
				eventHandler = (FNALogging.<>O.<0>__LogDeviceReset = new EventHandler<EventArgs>(FNALogging.LogDeviceReset));
			}
			graphics.DeviceReset += eventHandler;
			graphics.DeviceCreated += delegate([Nullable(2)] object s, EventArgs e)
			{
				FNALogging.creating = true;
			};
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x0052C070 File Offset: 0x0052A270
		private static void LogDeviceReset(object sender, EventArgs e)
		{
			GraphicsDevice graphicsDevice = ((GraphicsDeviceManager)sender).GraphicsDevice;
			StringBuilder sb = new StringBuilder("Device " + (FNALogging.creating ? "Created" : "Reset"));
			foreach (FNALogging.DeviceParam deviceParam in FNALogging.Params)
			{
				deviceParam.LogChange(graphicsDevice, sb, FNALogging.creating);
			}
			Logging.Terraria.Debug(sb);
			FNALogging.creating = false;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x0052C108 File Offset: 0x0052A308
		internal static void PostAudioInit()
		{
			Logging.FNA.Info("SDL Audio Driver: " + SDL.SDL_GetCurrentAudioDriver());
		}

		// Token: 0x04001C1E RID: 7198
		private static List<FNALogging.DeviceParam> Params = new List<FNALogging.DeviceParam>
		{
			new FNALogging.DeviceParam<GraphicsAdapter>("Adapter", (GraphicsDevice g) => g.Adapter, (GraphicsAdapter a) => a.Description),
			new FNALogging.DeviceParam<DisplayMode>("DisplayMode", (GraphicsDevice g) => g.Adapter.CurrentDisplayMode, null),
			new FNALogging.DeviceParam<GraphicsProfile>("Profile", (GraphicsDevice g) => g.GraphicsProfile, null),
			new FNALogging.DeviceParam<int>("Width", (GraphicsDevice g) => g.PresentationParameters.BackBufferWidth, null),
			new FNALogging.DeviceParam<int>("Height", (GraphicsDevice g) => g.PresentationParameters.BackBufferHeight, null),
			new FNALogging.DeviceParam<bool>("Fullscreen", (GraphicsDevice g) => g.PresentationParameters.IsFullScreen, null),
			new FNALogging.DeviceParam<string>("Display", (GraphicsDevice g) => g.Adapter.DeviceName, null)
		};

		// Token: 0x04001C20 RID: 7200
		private static bool creating;

		// Token: 0x02000A58 RID: 2648
		private abstract class DeviceParam
		{
			// Token: 0x06005891 RID: 22673 RVA: 0x0069FFF9 File Offset: 0x0069E1F9
			public DeviceParam(string name)
			{
				this.name = name;
			}

			// Token: 0x06005892 RID: 22674
			public abstract void LogChange(GraphicsDevice g, StringBuilder sb, bool creating);

			// Token: 0x04006CF1 RID: 27889
			public readonly string name;
		}

		// Token: 0x02000A59 RID: 2649
		private class DeviceParam<T> : FNALogging.DeviceParam
		{
			// Token: 0x17000908 RID: 2312
			// (get) Token: 0x06005893 RID: 22675 RVA: 0x006A0008 File Offset: 0x0069E208
			private string Desc
			{
				get
				{
					Func<T, string> func = this.getDescription;
					return ((func != null) ? func(this.value) : null) ?? this.value.ToString();
				}
			}

			// Token: 0x06005894 RID: 22676 RVA: 0x006A0037 File Offset: 0x0069E237
			public DeviceParam(string name, Func<GraphicsDevice, T> getter, Func<T, string> getDescription = null) : base(name)
			{
				this.getter = getter;
				this.getDescription = getDescription;
			}

			// Token: 0x06005895 RID: 22677 RVA: 0x006A0050 File Offset: 0x0069E250
			public override void LogChange(GraphicsDevice g, StringBuilder changes, bool creating)
			{
				if (creating)
				{
					this.value = this.getter(g);
				}
				changes.Append(", ").Append(this.name).Append(": ").Append(this.Desc);
				T newValue = this.getter(g);
				if (!EqualityComparer<T>.Default.Equals(this.value, newValue))
				{
					this.value = newValue;
					changes.Append(" -> ").Append(this.Desc);
				}
			}

			// Token: 0x04006CF2 RID: 27890
			private readonly Func<GraphicsDevice, T> getter;

			// Token: 0x04006CF3 RID: 27891
			private readonly Func<T, string> getDescription;

			// Token: 0x04006CF4 RID: 27892
			private T value;
		}

		// Token: 0x02000A5A RID: 2650
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006CF5 RID: 27893
			public static EventHandler<EventArgs> <0>__LogDeviceReset;
		}
	}
}
