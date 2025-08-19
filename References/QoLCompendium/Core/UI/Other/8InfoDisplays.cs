// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Other.MiningSpeedInfoDisplay
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.UI.Other;

public class MiningSpeedInfoDisplay : InfoDisplay
{
  public virtual string Texture => "QoLCompendium/Assets/InfoDisplayIcons/MiningSpeedInfoDisplay";

  public virtual bool Active() => Main.LocalPlayer.GetModPlayer<InfoPlayer>().kettlebell;

  public virtual string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
  {
    return ((float) ((1.0 - (double) Main.LocalPlayer.pickSpeed) * 100.0)).ToString("N0") + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.MiningSpeed");
  }
}
