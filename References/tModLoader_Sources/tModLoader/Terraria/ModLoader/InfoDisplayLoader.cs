using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x02000181 RID: 385
	public static class InfoDisplayLoader
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x004D52CB File Offset: 0x004D34CB
		public static int InfoDisplayCount
		{
			get
			{
				return InfoDisplayLoader.InfoDisplays.Count;
			}
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x004D52D8 File Offset: 0x004D34D8
		static InfoDisplayLoader()
		{
			InfoDisplayLoader.RegisterDefaultDisplays();
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x004D53A2 File Offset: 0x004D35A2
		internal static int Add(InfoDisplay infoDisplay)
		{
			InfoDisplayLoader.InfoDisplays.Add(infoDisplay);
			return InfoDisplayLoader.InfoDisplays.Count - 1;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x004D53BB File Offset: 0x004D35BB
		internal static void Unload()
		{
			InfoDisplayLoader.InfoDisplays.RemoveRange(InfoDisplayLoader.DefaultDisplayCount, InfoDisplayLoader.InfoDisplays.Count - InfoDisplayLoader.DefaultDisplayCount);
			InfoDisplayLoader.globalInfoDisplays.Clear();
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x004D53E8 File Offset: 0x004D35E8
		internal static void RegisterDefaultDisplays()
		{
			int i = 0;
			foreach (InfoDisplay infoDisplay in InfoDisplayLoader.InfoDisplays)
			{
				infoDisplay.Type = i++;
				ContentInstance.Register(infoDisplay);
				ModTypeLookup<InfoDisplay>.Register(infoDisplay);
			}
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x004D544C File Offset: 0x004D364C
		public static int ActiveDisplays()
		{
			int activeDisplays = 0;
			for (int i = 0; i < InfoDisplayLoader.InfoDisplays.Count; i++)
			{
				if (InfoDisplayLoader.InfoDisplays[i].Active())
				{
					activeDisplays++;
				}
			}
			return activeDisplays;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x004D5487 File Offset: 0x004D3687
		public static void AddGlobalInfoDisplay(GlobalInfoDisplay globalInfoDisplay)
		{
			InfoDisplayLoader.globalInfoDisplays.Add(globalInfoDisplay);
			ModTypeLookup<GlobalInfoDisplay>.Register(globalInfoDisplay);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x004D549C File Offset: 0x004D369C
		public static int ActivePages()
		{
			int activePages = 1;
			for (int activeDisplays = InfoDisplayLoader.ActiveDisplays(); activeDisplays > 12; activeDisplays -= 12)
			{
				activePages++;
			}
			return activePages;
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x004D54C4 File Offset: 0x004D36C4
		public static bool Active(InfoDisplay info)
		{
			bool active = info.Active();
			foreach (GlobalInfoDisplay globalInfoDisplay in InfoDisplayLoader.globalInfoDisplays)
			{
				bool? val = globalInfoDisplay.Active(info);
				if (val != null)
				{
					active &= val.Value;
				}
			}
			return active;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x004D552C File Offset: 0x004D372C
		[Obsolete("Use ModifyDisplayParameters instead")]
		public static void ModifyDisplayName(InfoDisplay info, ref string displayName)
		{
			foreach (GlobalInfoDisplay globalInfoDisplay in InfoDisplayLoader.globalInfoDisplays)
			{
				globalInfoDisplay.ModifyDisplayName(info, ref displayName);
			}
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x004D5578 File Offset: 0x004D3778
		[Obsolete("Use ModifyDisplayParameters instead")]
		public static void ModifyDisplayValue(InfoDisplay info, ref string displayName)
		{
			foreach (GlobalInfoDisplay globalInfoDisplay in InfoDisplayLoader.globalInfoDisplays)
			{
				globalInfoDisplay.ModifyDisplayValue(info, ref displayName);
			}
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x004D55C4 File Offset: 0x004D37C4
		[Obsolete("Use ModifyDisplayParameters instead")]
		public static void ModifyDisplayColor(InfoDisplay info, ref Color displayColor, ref Color displayShadowColor)
		{
			foreach (GlobalInfoDisplay globalInfoDisplay in InfoDisplayLoader.globalInfoDisplays)
			{
				globalInfoDisplay.ModifyDisplayColor(info, ref displayColor, ref displayShadowColor);
			}
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x004D5610 File Offset: 0x004D3810
		public static void ModifyDisplayParameters(InfoDisplay info, ref string displayValue, ref string displayName, ref Color displayColor, ref Color displayShadowColor)
		{
			foreach (GlobalInfoDisplay globalInfoDisplay in InfoDisplayLoader.globalInfoDisplays)
			{
				globalInfoDisplay.ModifyDisplayParameters(info, ref displayValue, ref displayName, ref displayColor, ref displayShadowColor);
			}
		}

		// Token: 0x040015CC RID: 5580
		internal static readonly List<InfoDisplay> InfoDisplays = new List<InfoDisplay>
		{
			InfoDisplay.Watches,
			InfoDisplay.WeatherRadio,
			InfoDisplay.Sextant,
			InfoDisplay.FishFinder,
			InfoDisplay.MetalDetector,
			InfoDisplay.LifeformAnalyzer,
			InfoDisplay.Radar,
			InfoDisplay.TallyCounter,
			InfoDisplay.Dummy,
			InfoDisplay.DPSMeter,
			InfoDisplay.Stopwatch,
			InfoDisplay.Compass,
			InfoDisplay.DepthMeter
		};

		// Token: 0x040015CD RID: 5581
		internal static readonly int DefaultDisplayCount = InfoDisplayLoader.InfoDisplays.Count;

		// Token: 0x040015CE RID: 5582
		public static int InfoDisplayPage = 0;

		// Token: 0x040015CF RID: 5583
		internal static readonly IList<GlobalInfoDisplay> globalInfoDisplays = new List<GlobalInfoDisplay>();
	}
}
