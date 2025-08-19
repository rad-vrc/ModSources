using System;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Wrapper class for a LocalizedText and visibility field that has intended use with modification
	/// of Game Tips.
	/// </summary>
	// Token: 0x02000163 RID: 355
	public sealed class GameTipData
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x004D31FB File Offset: 0x004D13FB
		// (set) Token: 0x06001C51 RID: 7249 RVA: 0x004D3203 File Offset: 0x004D1403
		public LocalizedText TipText { get; internal set; }

		/// <summary>
		/// The mod instance this tip belongs to. This value is null
		/// for vanilla tips.
		/// </summary>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x004D320C File Offset: 0x004D140C
		// (set) Token: 0x06001C53 RID: 7251 RVA: 0x004D3214 File Offset: 0x004D1414
		public Mod Mod { get; internal set; }

		/// <summary>
		/// Retrieves the "name" of this GameTip, which is the Key excluding the beginning Mods.ModName.GameTips portion.
		/// For example, if the key was "Mods.ExampleMod.GameTips.ExampleTip", this would return "ExampleTip".
		/// </summary>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x004D321D File Offset: 0x004D141D
		// (set) Token: 0x06001C55 RID: 7253 RVA: 0x004D3225 File Offset: 0x004D1425
		public string Name { get; internal set; }

		/// <summary>
		/// Retrieves the FULL "name" of this GameTip, which includes the Mod and this tip's Name.
		/// For example, if this tip was from ExampleMod and was named "ExampleTip", this would
		/// return "ExampleMod/ExampleTip"
		/// </summary>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x004D322E File Offset: 0x004D142E
		// (set) Token: 0x06001C57 RID: 7255 RVA: 0x004D3236 File Offset: 0x004D1436
		public string FullName { get; internal set; }

		// Token: 0x06001C58 RID: 7256 RVA: 0x004D3240 File Offset: 0x004D1440
		public GameTipData(LocalizedText text, Mod mod)
		{
			this.TipText = text;
			this.Mod = mod;
			this.Name = text.Key.Replace("Mods." + mod.Name + ".GameTips.", "");
			this.FullName = this.Mod.Name + "/" + this.Name;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x004D32B4 File Offset: 0x004D14B4
		internal GameTipData(LocalizedText text)
		{
			this.TipText = text;
			this.Mod = null;
			this.Name = text.Key;
			this.FullName = "Terraria/" + this.Name;
		}

		/// <summary>
		/// Until reload, prevents this tip from ever appearing during loading screens.
		/// </summary>
		// Token: 0x06001C5A RID: 7258 RVA: 0x004D32F3 File Offset: 0x004D14F3
		public void Hide()
		{
			this.isVisible = false;
		}

		// Token: 0x04001529 RID: 5417
		internal bool isVisible = true;
	}
}
