using System;
using System.Collections.Generic;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems
{
	// Token: 0x0200029F RID: 671
	public abstract class IPermanentModdedBuffItem : ModItem, IComparable
	{
		// Token: 0x0600114F RID: 4431 RVA: 0x0008709B File Offset: 0x0008529B
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.PermanentBuffs;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0008707E File Offset: 0x0008527E
		public int CompareTo(object obj)
		{
			return base.GetType().Name.CompareTo(obj.GetType().Name);
		}

		// Token: 0x06001151 RID: 4433
		internal abstract void ApplyBuff(PermanentBuffPlayer player);

		// Token: 0x06001152 RID: 4434 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00008B59 File Offset: 0x00006D59
		public override void SetDefaults()
		{
			Common.SetDefaultsToPermanentBuff(base.Item);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00087108 File Offset: 0x00085308
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (this.Texture == "QoLCompendium/Assets/Items/PermanentBuff")
			{
				TooltipLine name = tooltips.Find((TooltipLine l) => l.Name == "ItemName");
				TooltipLine tooltipAssetNotFound = new TooltipLine(QoLCompendium.instance, "AssetNotFound", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.AssetNotFound"));
				tooltips.AddAfter(name, tooltipAssetNotFound);
			}
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.PermanentBuffs);
		}
	}
}
