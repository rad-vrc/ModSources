// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Other.SpawnInfoDisplay
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.UI.Other;

public class SpawnInfoDisplay : InfoDisplay
{
  public virtual string Texture => "QoLCompendium/Assets/InfoDisplayIcons/SpawnInfoDisplay";

  public virtual bool Active() => Main.LocalPlayer.GetModPlayer<InfoPlayer>().headCounter;

  public virtual string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
  {
    int spawnRate = Main.LocalPlayer.GetModPlayer<QoLCPlayer>().spawnRate;
    return spawnRate == 0 || Main.LocalPlayer.GetModPlayer<QoLCPlayer>().noSpawns ? Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.SpawnsDisabled") : $"{((float) Math.Round((double) (60f / (float) spawnRate), 2)).ToString()} {Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.SpawnRate")}";
  }
}
