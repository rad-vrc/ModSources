using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Developer
{
	// Token: 0x02000341 RID: 833
	internal abstract class DeveloperItem : ModLoaderModItem
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x0053362F File Offset: 0x0053182F
		public virtual string TooltipBrief { get; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x00533637 File Offset: 0x00531837
		public virtual string SetSuffix
		{
			get
			{
				return "'s";
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x0053363E File Offset: 0x0053183E
		public string InternalSetName
		{
			get
			{
				return base.GetType().Name.Split('_', StringSplitOptions.None)[0];
			}
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x00533655 File Offset: 0x00531855
		public override void SetStaticDefaults()
		{
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x00533657 File Offset: 0x00531857
		public override void SetDefaults()
		{
			base.Item.rare = 11;
			base.Item.vanity = true;
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x00533674 File Offset: 0x00531874
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(base.Mod, "DeveloperSetNote", this.TooltipBrief + "Developer Item")
			{
				OverrideColor = new Color?(Color.OrangeRed)
			};
			tooltips.Add(line);
		}
	}
}
