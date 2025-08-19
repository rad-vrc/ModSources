// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic.SpiritClassicArenaEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.SpiritClassic;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SpiritClassic;

public class SpiritClassicArenaEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.spiritLoaded)
      return;
    new CoiledEnergizerEffect().ApplyEffect(player);
    new KoiTotemEffect().ApplyEffect(player);
    new SunPotEffect().ApplyEffect(player);
    new TheCouchEffect().ApplyEffect(player);
  }
}
