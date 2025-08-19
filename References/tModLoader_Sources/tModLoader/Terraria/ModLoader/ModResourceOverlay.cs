using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.ResourceSets;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves as a place for you to customize how the vanilla resource displays (Classic, Fancy and Bars) are drawn.<br />
	/// For implementing your own resource displays, use <see cref="T:Terraria.ModLoader.ModResourceDisplaySet" />.
	/// </summary>
	// Token: 0x020001C8 RID: 456
	[Autoload(true, Side = ModSide.Client)]
	public abstract class ModResourceOverlay : ModType
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x004E90CB File Offset: 0x004E72CB
		// (set) Token: 0x060023C7 RID: 9159 RVA: 0x004E90D3 File Offset: 0x004E72D3
		public int Type { get; internal set; }

		// Token: 0x060023C8 RID: 9160 RVA: 0x004E90DC File Offset: 0x004E72DC
		protected sealed override void Register()
		{
			ModTypeLookup<ModResourceOverlay>.Register(this);
			this.Type = ResourceOverlayLoader.Add(this);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x004E90F0 File Offset: 0x004E72F0
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to draw below any resource (hearts, stars, bars or panels) in a display set
		/// </summary>
		/// <param name="context">Contains the drawing data for the resource being drawn.  You should use the ResourceOverlayDrawContext.Draw method for all drawing</param>
		/// <returns><see langword="true" /> if the intended resource sprite should draw, <see langword="false" /> otherwise.</returns>
		// Token: 0x060023CA RID: 9162 RVA: 0x004E90F8 File Offset: 0x004E72F8
		public virtual bool PreDrawResource(ResourceOverlayDrawContext context)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw on top of any resource (hearts, stars, bars or panels) in a display set
		/// </summary>
		/// <param name="context">Contains the drawing data for the resource being drawn.  You should use the ResourceOverlayDrawContext.Draw method for all drawing</param>
		// Token: 0x060023CB RID: 9163 RVA: 0x004E90FB File Offset: 0x004E72FB
		public virtual void PostDrawResource(ResourceOverlayDrawContext context)
		{
		}

		/// <summary>
		/// Allows you to draw before the resources (hearts, stars, bars and/or panels) in a display set are drawn.<br />
		/// If you want to implement your own display set, it is recommended to use <see cref="T:Terraria.ModLoader.ModResourceDisplaySet" /> instead of this hook.
		/// </summary>
		/// <param name="snapshot">A snapshot of the stats from Main.LocalPlayer</param>
		/// <param name="displaySet">The display set being drawn</param>
		/// <param name="drawingLife">
		/// Whether the life or mana display is going to be drawn.
		/// <see langword="true" /> if the life display is going to be drawn, <see langword="false" /> if the mana display is going to be drawn.
		/// </param>
		/// <param name="textColor">The color to draw the text above the resources with.  Only applies to the Classic display set.</param>
		/// <param name="drawText">Whether the text above the resources should draw.  Only applies to the Classic display set.</param>
		/// <returns>Whether the resources in the display set are drawn</returns>
		// Token: 0x060023CC RID: 9164 RVA: 0x004E90FD File Offset: 0x004E72FD
		public virtual bool PreDrawResourceDisplay(PlayerStatsSnapshot snapshot, IPlayerResourcesDisplaySet displaySet, bool drawingLife, ref Color textColor, out bool drawText)
		{
			drawText = true;
			return true;
		}

		/// <summary>
		/// Allows you to draw after the resources (hearts, stars, bars and/or panels) in a display set are drawn
		/// </summary>
		/// <param name="snapshot">A snapshot of the stats from Main.LocalPlayer</param>
		/// <param name="displaySet">The display set that was drawn</param>
		/// <param name="drawingLife">
		/// Whether the life or mana display was drawn.
		/// <see langword="true" /> if the life display was drawn, <see langword="false" /> if the mana display was drawn.
		/// </param>
		/// <param name="textColor">The color the text above the resources was drawn with.  Only applies to the Class display set.</param>
		/// <param name="drawText">Whether the text above the resources was drawn.  Only applies to the Classic display set.</param>
		// Token: 0x060023CD RID: 9165 RVA: 0x004E9104 File Offset: 0x004E7304
		public virtual void PostDrawResourceDisplay(PlayerStatsSnapshot snapshot, IPlayerResourcesDisplaySet displaySet, bool drawingLife, Color textColor, bool drawText)
		{
		}

		/// <summary>
		/// Allows you to specify if the hover text for a resource (life or mana) should be displayed
		/// </summary>
		/// <param name="snapshot">A snapshot of the stats from Main.LocalPlayer</param>
		/// <param name="displaySet">The display set that was drawn</param>
		/// <param name="drawingLife">
		/// Whether the life or mana display was drawn.
		/// <see langword="true" /> if the life display was drawn, <see langword="false" /> if the mana display was drawn.
		/// </param>
		/// <returns>Whether the hover text should be displayed</returns>
		// Token: 0x060023CE RID: 9166 RVA: 0x004E9106 File Offset: 0x004E7306
		public virtual bool DisplayHoverText(PlayerStatsSnapshot snapshot, IPlayerResourcesDisplaySet displaySet, bool drawingLife)
		{
			return true;
		}
	}
}
