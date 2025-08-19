using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.ResourceSets;

namespace Terraria.ModLoader
{
	// Token: 0x020001F5 RID: 501
	public static class ResourceOverlayLoader
	{
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x00501CCA File Offset: 0x004FFECA
		public static int OverlayCount
		{
			get
			{
				return ResourceOverlayLoader.overlays.Count;
			}
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x00501CD6 File Offset: 0x004FFED6
		internal static int Add(ModResourceOverlay overlay)
		{
			ResourceOverlayLoader.overlays.Add(overlay);
			return ResourceOverlayLoader.OverlayCount - 1;
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x00501CEA File Offset: 0x004FFEEA
		public static ModResourceOverlay GetOverlay(int type)
		{
			if (type < 0 || type >= ResourceOverlayLoader.OverlayCount)
			{
				return null;
			}
			return ResourceOverlayLoader.overlays[type];
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x00501D05 File Offset: 0x004FFF05
		internal static void Unload()
		{
			ResourceOverlayLoader.overlays.Clear();
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x00501D14 File Offset: 0x004FFF14
		public static bool PreDrawResource(ResourceOverlayDrawContext context)
		{
			bool result = true;
			foreach (ModResourceOverlay overlay in ResourceOverlayLoader.overlays)
			{
				result &= overlay.PreDrawResource(context);
			}
			return result;
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x00501D68 File Offset: 0x004FFF68
		public static void PostDrawResource(ResourceOverlayDrawContext context)
		{
			foreach (ModResourceOverlay modResourceOverlay in ResourceOverlayLoader.overlays)
			{
				modResourceOverlay.PostDrawResource(context);
			}
		}

		/// <summary>
		/// Draws a resource, typically life or mana
		/// </summary>
		/// <param name="drawContext">The drawing context</param>
		// Token: 0x0600270E RID: 9998 RVA: 0x00501DB4 File Offset: 0x004FFFB4
		public static void DrawResource(ResourceOverlayDrawContext drawContext)
		{
			if (ResourceOverlayLoader.PreDrawResource(drawContext))
			{
				drawContext.Draw();
			}
			ResourceOverlayLoader.PostDrawResource(drawContext);
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x00501DCC File Offset: 0x004FFFCC
		public static bool PreDrawResourceDisplay(PlayerStatsSnapshot snapshot, IPlayerResourcesDisplaySet displaySet, bool drawingLife, ref Color textColor, out bool drawText)
		{
			bool result = true;
			drawText = true;
			foreach (ModResourceOverlay overlay in ResourceOverlayLoader.overlays)
			{
				bool draw;
				result &= overlay.PreDrawResourceDisplay(snapshot, displaySet, drawingLife, ref textColor, out draw);
				drawText = (drawText && draw);
			}
			return result;
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x00501E30 File Offset: 0x00500030
		public static void PostDrawResourceDisplay(PlayerStatsSnapshot snapshot, IPlayerResourcesDisplaySet displaySet, bool drawingLife, Color textColor, bool drawText)
		{
			foreach (ModResourceOverlay modResourceOverlay in ResourceOverlayLoader.overlays)
			{
				modResourceOverlay.PostDrawResourceDisplay(snapshot, displaySet, drawingLife, textColor, drawText);
			}
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x00501E80 File Offset: 0x00500080
		public static bool DisplayHoverText(PlayerStatsSnapshot snapshot, IPlayerResourcesDisplaySet displaySet, bool drawingLife)
		{
			bool result = true;
			foreach (ModResourceOverlay overlay in ResourceOverlayLoader.overlays)
			{
				result &= overlay.DisplayHoverText(snapshot, displaySet, drawingLife);
			}
			return result;
		}

		// Token: 0x040018AB RID: 6315
		internal static readonly IList<ModResourceOverlay> overlays = new List<ModResourceOverlay>();
	}
}
