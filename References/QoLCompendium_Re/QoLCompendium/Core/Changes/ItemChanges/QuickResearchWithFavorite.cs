using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ItemChanges
{
	// Token: 0x02000261 RID: 609
	public class QuickResearchWithFavorite : GlobalItem
	{
		// Token: 0x06000E27 RID: 3623 RVA: 0x00071E30 File Offset: 0x00070030
		public override void UpdateInventory(Item item, Player player)
		{
			bool researched;
			int numResearch;
			if (item.favorited && item.stack >= CreativeUI.GetSacrificeCount(item.type, out researched) && CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(item.type, out numResearch) && !researched && player.difficulty == 3 && item.stack >= numResearch && QoLCompendium.mainClientConfig.FavoriteResearching)
			{
				if (item.type == 5437 || item.type == 5361 || item.type == 5360 || item.type == 5359 || item.type == 5358)
				{
					return;
				}
				CreativeUI.ResearchItem(item.type);
				SoundEngine.PlaySound(SoundID.ResearchComplete, null, null);
				item.stack -= numResearch;
				if (item.stack <= 0)
				{
					item.TurnToAir(false);
				}
			}
		}
	}
}
