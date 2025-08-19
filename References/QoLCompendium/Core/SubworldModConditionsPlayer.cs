// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.SubworldModConditionsPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core;

public class SubworldModConditionsPlayer : ModPlayer
{
  public virtual void OnEnterWorld()
  {
    if (!SubworldModConditions.downedBereftVassal)
      return;
    ModConditions.downedBereftVassal = true;
  }
}
