// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.WorldChanges.AlwaysSeasonalEvents
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.WorldChanges;

public class AlwaysSeasonalEvents : ModSystem
{
  public virtual void PostUpdateWorld()
  {
    if (QoLCompendium.QoLCompendium.mainConfig.HalloweenActive)
      Main.halloween = true;
    if (!QoLCompendium.QoLCompendium.mainConfig.ChristmasActive)
      return;
    Main.xMas = true;
  }
}
