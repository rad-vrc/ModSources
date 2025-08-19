// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium.ThoriumBardEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.ThoriumBossRework;
using Terraria.ModLoader;
using ThoriumRework;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;

public class ThoriumBardEffect : IPermanentModdedBuff
{
  [JITWhenModsEnabled(new string[] {"ThoriumRework"})]
  public static bool ThoriumReworkPotionsEnabled
  {
    get => ModContent.GetInstance<CompatConfig>().extraPotions;
  }

  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.thoriumLoaded)
      return;
    new CreativityEffect().ApplyEffect(player);
    new EarwormEffect().ApplyEffect(player);
    new InspirationalReachEffect().ApplyEffect(player);
    if (!ModConditions.thoriumBossReworkLoaded || !ThoriumBardEffect.ThoriumReworkPotionsEnabled)
      return;
    new DeathsingerEffect().ApplyEffect(player);
    new InspirationRegenerationEffect().ApplyEffect(player);
  }
}
