using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader.UI;

namespace Terraria.ModLoader.Core
{
	// Token: 0x02000361 RID: 865
	internal static class MemoryTracking
	{
		// Token: 0x06002FFA RID: 12282 RVA: 0x00537E98 File Offset: 0x00536098
		internal static void InGameUpdate()
		{
			if (MemoryTracking.CheckRAMUsageTimer.Elapsed.TotalSeconds > 60.0)
			{
				MemoryTracking.CheckRAMUsageTimer.Restart();
				Process process = Process.GetCurrentProcess();
				if (MemoryTracking.previousTotalMemory < process.PrivateMemorySize64 && MemoryTracking.previousTotalMemory >> 30 != process.PrivateMemorySize64 >> 30)
				{
					Logging.tML.Info("tModLoader RAM usage has increased: " + UIMemoryBar.SizeSuffix(process.PrivateMemorySize64, 1));
				}
				MemoryTracking.previousTotalMemory = process.PrivateMemorySize64;
			}
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x00537F1D File Offset: 0x0053611D
		internal static void Clear()
		{
			MemoryTracking.modMemoryUsageEstimates.Clear();
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x00537F2C File Offset: 0x0053612C
		internal static ModMemoryUsage Update(string modName)
		{
			ModMemoryUsage usage;
			if (!MemoryTracking.modMemoryUsageEstimates.TryGetValue(modName, out usage))
			{
				usage = (MemoryTracking.modMemoryUsageEstimates[modName] = new ModMemoryUsage());
			}
			long newMemory = GC.GetTotalMemory(MemoryTracking.accurate);
			usage.managed += Math.Max(0L, newMemory - MemoryTracking.previousMemory);
			MemoryTracking.previousMemory = newMemory;
			return usage;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x00537F87 File Offset: 0x00536187
		internal static void Checkpoint()
		{
			MemoryTracking.previousMemory = GC.GetTotalMemory(MemoryTracking.accurate);
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x00537F98 File Offset: 0x00536198
		internal static void Finish()
		{
			MemoryTracking.CheckRAMUsageTimer.Restart();
			foreach (Mod mod in ModLoader.Mods)
			{
				ModMemoryUsage modMemoryUsage = MemoryTracking.modMemoryUsageEstimates[mod.Name];
				modMemoryUsage.textures = (from asset in mod.Assets.GetLoadedAssets().OfType<Asset<Texture2D>>()
				select asset.Value into val
				where val != null
				select val).Sum((Texture2D tex) => (long)(tex.Width * tex.Height * 4));
				modMemoryUsage.sounds = (from asset in mod.Assets.GetLoadedAssets().OfType<Asset<SoundEffect>>()
				select asset.Value into val
				where val != null
				select val).Sum((SoundEffect sound) => (long)(sound.Duration.TotalSeconds * 44100.0 * 2.0 * 2.0));
			}
			if (ModLoader.Mods.Length > 1)
			{
				Logging.tML.Info("Mods using the most RAM: " + string.Join(", ", from x in (from x in MemoryTracking.modMemoryUsageEstimates
				orderby x.Value.total descending
				where x.Key != "ModLoader"
				select x).Take(3)
				select x.Key + " " + UIMemoryBar.SizeSuffix(x.Value.total, 1)));
			}
			long totalRamUsage = -1L;
			long totalCommit = -1L;
			try
			{
				totalRamUsage = Process.GetProcesses().Sum((Process x) => x.WorkingSet64);
				totalCommit = Process.GetProcesses().Sum((Process x) => x.PrivateMemorySize64);
			}
			catch
			{
			}
			Process process = Process.GetCurrentProcess();
			process.Refresh();
			ILog tML = Logging.tML;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(87, 4);
			defaultInterpolatedStringHandler.AppendLiteral("RAM physical: tModLoader usage: ");
			defaultInterpolatedStringHandler.AppendFormatted(UIMemoryBar.SizeSuffix(process.WorkingSet64, 1));
			defaultInterpolatedStringHandler.AppendLiteral(", All processes usage: ");
			defaultInterpolatedStringHandler.AppendFormatted((totalRamUsage == -1L) ? "Unknown" : UIMemoryBar.SizeSuffix(totalRamUsage, 1));
			defaultInterpolatedStringHandler.AppendLiteral(", Available: ");
			defaultInterpolatedStringHandler.AppendFormatted(UIMemoryBar.SizeSuffix(UIMemoryBar.GetTotalMemory() - totalRamUsage, 1));
			defaultInterpolatedStringHandler.AppendLiteral(", Total Installed: ");
			defaultInterpolatedStringHandler.AppendFormatted(UIMemoryBar.SizeSuffix(UIMemoryBar.GetTotalMemory(), 1));
			tML.Info(defaultInterpolatedStringHandler.ToStringAndClear());
			Logging.tML.Info("RAM virtual: tModLoader usage: " + UIMemoryBar.SizeSuffix(process.PrivateMemorySize64, 1) + ", All processes usage: " + ((totalCommit == -1L) ? "Unknown" : UIMemoryBar.SizeSuffix(totalCommit, 1)));
			MemoryTracking.previousTotalMemory = process.PrivateMemorySize64;
			if (totalCommit > UIMemoryBar.GetTotalMemory())
			{
				Logging.tML.Warn("Total system memory usage exceeds installed physical memory, tModLoader will likely experience performance issues due to frequent page file access.");
			}
		}

		// Token: 0x04001CDD RID: 7389
		internal static Dictionary<string, ModMemoryUsage> modMemoryUsageEstimates = new Dictionary<string, ModMemoryUsage>();

		// Token: 0x04001CDE RID: 7390
		private static long previousMemory;

		// Token: 0x04001CDF RID: 7391
		internal static bool accurate = false;

		// Token: 0x04001CE0 RID: 7392
		internal static Stopwatch CheckRAMUsageTimer = new Stopwatch();

		// Token: 0x04001CE1 RID: 7393
		private static long previousTotalMemory;
	}
}
