using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002D6 RID: 726
	public sealed class UnloadedPrefix : ModPrefix
	{
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x00530A93 File Offset: 0x0052EC93
		public override LocalizedText DisplayName
		{
			get
			{
				return LocalizedText.Empty;
			}
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x00530A9A File Offset: 0x0052EC9A
		public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
		{
			UnloadedGlobalItem unloadedGlobalItem = item.GetGlobalItem<UnloadedGlobalItem>();
			yield return new TooltipLine("UnloadedPrefix", this.GetLocalization("Tooltip", null).Format(new object[]
			{
				unloadedGlobalItem.ModPrefixMod + "/" + unloadedGlobalItem.ModPrefixName
			}))
			{
				IsModifier = true,
				OverrideColor = new Color?(new Color(215, 123, 186))
			};
			yield break;
		}
	}
}
