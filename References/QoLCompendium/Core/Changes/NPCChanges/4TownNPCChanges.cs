// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.CensusSupport
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.NPCs;
using System;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class CensusSupport : ModSystem
{
  public virtual void PostSetupContent()
  {
    Mod mod;
    if (!Terraria.ModLoader.ModLoader.TryGetMod("Census", ref mod))
      return;
    if (QoLCompendium.QoLCompendium.mainConfig.BlackMarketDealerCanSpawn)
      mod.Call(new object[3]
      {
        (object) "TownNPCCondition",
        (object) ModContent.NPCType<BMDealerNPC>(),
        (object) ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType) ModContent.GetInstance<BMDealerNPC>(), "Census.SpawnCondition", (Func<string>) null)
      });
    if (!QoLCompendium.QoLCompendium.mainConfig.EtherealCollectorCanSpawn)
      return;
    mod.Call(new object[3]
    {
      (object) "TownNPCCondition",
      (object) ModContent.NPCType<EtherealCollectorNPC>(),
      (object) ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType) ModContent.GetInstance<EtherealCollectorNPC>(), "Census.SpawnCondition", (Func<string>) null)
    });
  }
}
