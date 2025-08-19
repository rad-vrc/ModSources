// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Other.LuckInfoDisplay
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

public class LuckInfoDisplay : InfoDisplay
{
  public virtual string Texture => "QoLCompendium/Assets/InfoDisplayIcons/LuckInfoDisplay";

  public virtual bool Active() => Main.LocalPlayer.GetModPlayer<InfoPlayer>().metallicClover;

  public virtual string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
  {
    return $"{Math.Round((double) Main.LocalPlayer.luck, 3).ToString()} {Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.Luck")}";
  }
}
