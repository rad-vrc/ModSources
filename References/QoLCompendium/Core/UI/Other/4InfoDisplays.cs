// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Other.MinionInfoDisplay
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

public class MinionInfoDisplay : InfoDisplay
{
  public virtual string Texture => "QoLCompendium/Assets/InfoDisplayIcons/MinionInfoDisplay";

  public virtual bool Active() => Main.LocalPlayer.GetModPlayer<InfoPlayer>().trackingDevice;

  public virtual string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
  {
    return $"{Math.Round((double) Main.LocalPlayer.slotsMinions, 2).ToString()}/{Main.LocalPlayer.maxMinions.ToString()} {Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.MinionSlots")}";
  }
}
