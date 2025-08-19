using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	/// <summary>
	/// A class that is used to modify existing info displays (i.e. the things that the Cell Phone uses to communicate information).
	/// All vanilla displays can be accessed using InfoDisplay.(name of item).
	/// </summary>
	// Token: 0x0200016A RID: 362
	public abstract class GlobalInfoDisplay : ModType
	{
		// Token: 0x06001C85 RID: 7301 RVA: 0x004D377A File Offset: 0x004D197A
		protected sealed override void Register()
		{
			InfoDisplayLoader.AddGlobalInfoDisplay(this);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x004D3782 File Offset: 0x004D1982
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to modify whether or not a given InfoDisplay is active. Returns null (no change from default behavior) by default for all InfoDisplays.
		/// </summary>
		/// <param name="currentDisplay">The display you're deciding the active state for.</param>
		// Token: 0x06001C87 RID: 7303 RVA: 0x004D378C File Offset: 0x004D198C
		public virtual bool? Active(InfoDisplay currentDisplay)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the display name of an InfoDisplay (shown when hovering over said display in-game).
		/// </summary>
		/// <param name="currentDisplay">The display you're modifying the display name for.</param>
		/// <param name="displayName">The display name of the current display.</param>
		// Token: 0x06001C88 RID: 7304 RVA: 0x004D37A2 File Offset: 0x004D19A2
		[Obsolete("Use ModifyDisplayParameters instead", true)]
		public virtual void ModifyDisplayName(InfoDisplay currentDisplay, ref string displayName)
		{
		}

		/// <summary>
		/// Allows you to modify the display value (the text displayed next to the icon) of an InfoDisplay.
		/// </summary>
		/// <param name="currentDisplay">The display you're modifying the display value for.</param>
		/// <param name="displayValue">The display value of the current display</param>
		// Token: 0x06001C89 RID: 7305 RVA: 0x004D37A4 File Offset: 0x004D19A4
		[Obsolete("Use ModifyDisplayParameters instead", true)]
		public virtual void ModifyDisplayValue(InfoDisplay currentDisplay, ref string displayValue)
		{
		}

		/// <summary>
		/// Allows you to modify the display color (the color of the text displayed next to the icon) of an InfoDisplay.
		/// </summary>
		/// <param name="currentDisplay">The display you're modifying the display color for.</param>
		/// <param name="displayColor">The display color of the current display.</param>
		/// <param name="displayShadowColor">The text outline color of the current display.</param>
		// Token: 0x06001C8A RID: 7306 RVA: 0x004D37A6 File Offset: 0x004D19A6
		[Obsolete("Use ModifyDisplayParameters instead", true)]
		public virtual void ModifyDisplayColor(InfoDisplay currentDisplay, ref Color displayColor, ref Color displayShadowColor)
		{
		}

		/// <summary>
		/// Allows modifying the display value (the text displayed next to the icon), the display name (text shown when hovering over the InfoDisplay in-game), and the display colors of an InfoDisplay.
		/// </summary>
		/// <param name="currentDisplay">The display you're modifying the parameters for.</param>
		/// <param name="displayValue">The display value of the current display.</param>
		/// <param name="displayName">The display name of the current display.</param>
		/// <param name="displayColor">The display color of the current display.</param>
		/// <param name="displayShadowColor">The text outline color of the current display.</param>
		// Token: 0x06001C8B RID: 7307 RVA: 0x004D37A8 File Offset: 0x004D19A8
		public virtual void ModifyDisplayParameters(InfoDisplay currentDisplay, ref string displayValue, ref string displayName, ref Color displayColor, ref Color displayShadowColor)
		{
		}
	}
}
