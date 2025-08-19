// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ItemChanges.EncumberingStoneAllowsCoins
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ItemChanges;

public class EncumberingStoneAllowsCoins : ModPlayer
{
  public virtual void PostUpdateMiscEffects()
  {
    for (int index = 0; index < Common.CoinIDs.Count; ++index)
      ItemID.Sets.IgnoresEncumberingStone[Common.CoinIDs.ElementAt<int>(index)] = QoLCompendium.QoLCompendium.mainConfig.EncumberingStoneAllowsCoins;
  }
}
