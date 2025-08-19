using System;
using System.Collections.Generic;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems
{
	// Token: 0x0200029D RID: 669
	public abstract class IPermanentBuffItem : ModItem, IComparable
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x0008709B File Offset: 0x0008529B
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.PermanentBuffs;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0008707E File Offset: 0x0008527E
		public int CompareTo(object obj)
		{
			return base.GetType().Name.CompareTo(obj.GetType().Name);
		}

		// Token: 0x06001147 RID: 4423
		internal abstract void ApplyBuff(PermanentBuffPlayer player);

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x000870B5 File Offset: 0x000852B5
		public override string Texture
		{
			get
			{
				return "Terraria/Images/Buff_" + this.buffIDForSprite.ToString();
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000870CC File Offset: 0x000852CC
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.PermanentBuffs);
		}

		// Token: 0x0400075A RID: 1882
		public int buffIDForSprite = 204;
	}
}
