// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ItemChanges.QuickResearchWithFavorite
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ItemChanges;

public class QuickResearchWithFavorite : GlobalItem
{
  public virtual void UpdateInventory(Item item, Player player)
  {
    bool flag;
    int num;
    if (!item.favorited || item.stack < CreativeUI.GetSacrificeCount(item.type, ref flag) || !CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(item.type, ref num) || flag || player.difficulty != (byte) 3 || item.stack < num || !QoLCompendium.QoLCompendium.mainClientConfig.FavoriteResearching || item.type == 5437 || item.type == 5361 || item.type == 5360 || item.type == 5359 || item.type == 5358)
      return;
    CreativeUI.ResearchItem(item.type);
    SoundEngine.PlaySound(ref SoundID.ResearchComplete, new Vector2?(), (SoundUpdateCallback) null);
    item.stack -= num;
    if (item.stack > 0)
      return;
    item.TurnToAir(false);
  }
}
