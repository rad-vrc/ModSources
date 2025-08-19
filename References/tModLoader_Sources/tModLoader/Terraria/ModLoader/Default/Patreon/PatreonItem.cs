using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000315 RID: 789
	internal abstract class PatreonItem : ModLoaderModItem
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x005323A5 File Offset: 0x005305A5
		// (set) Token: 0x06002EA7 RID: 11943 RVA: 0x005323AD File Offset: 0x005305AD
		public string InternalSetName { get; set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x005323B6 File Offset: 0x005305B6
		// (set) Token: 0x06002EA9 RID: 11945 RVA: 0x005323BE File Offset: 0x005305BE
		public string SetSuffix { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002EAA RID: 11946 RVA: 0x005323C7 File Offset: 0x005305C7
		public override LocalizedText Tooltip
		{
			get
			{
				return LocalizedText.Empty;
			}
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x005323D0 File Offset: 0x005305D0
		public PatreonItem()
		{
			this.InternalSetName = base.GetType().Name;
			int lastUnderscoreIndex = this.InternalSetName.LastIndexOf('_');
			if (lastUnderscoreIndex != -1)
			{
				this.InternalSetName = this.InternalSetName.Substring(0, lastUnderscoreIndex);
			}
			this.SetSuffix = (this.InternalSetName.EndsWith('s') ? "'" : "'s");
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x0053243A File Offset: 0x0053063A
		public override void SetStaticDefaults()
		{
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x0053243C File Offset: 0x0053063C
		public override void SetDefaults()
		{
			base.Item.rare = 9;
			base.Item.vanity = true;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x00532458 File Offset: 0x00530658
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(base.Mod, "PatreonThanks", Language.GetTextValue("tModLoader.PatreonSetTooltip"))
			{
				OverrideColor = new Color?(Color.Aquamarine)
			};
			tooltips.Add(line);
		}
	}
}
