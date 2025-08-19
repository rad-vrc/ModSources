using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x02000281 RID: 641
	public class SentryInfoDisplay : InfoDisplay
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00085A1B File Offset: 0x00083C1B
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/SentryInfoDisplay";
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00085A22 File Offset: 0x00083C22
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().battalionLog;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00085A34 File Offset: 0x00083C34
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			int num;
			UIElementsAndLayers.GetSentryNameToCount(out num, true);
			return string.Concat(new string[]
			{
				num.ToString(),
				"/",
				Main.LocalPlayer.maxTurrets.ToString(),
				" ",
				Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.SentrySlots")
			});
		}
	}
}
