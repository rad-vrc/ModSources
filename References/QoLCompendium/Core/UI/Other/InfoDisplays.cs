// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Other.RegenInfoDisplay
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

public class RegenInfoDisplay : InfoDisplay
{
  public virtual string Texture => "QoLCompendium/Assets/InfoDisplayIcons/RegenInfoDisplay";

  public virtual bool Active() => Main.LocalPlayer.GetModPlayer<InfoPlayer>().regenerator;

  public virtual string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
  {
    float num = (float) Math.Round((double) ((float) Main.LocalPlayer.lifeRegen * 0.5f), 2);
    return Main.LocalPlayer.statLife >= Main.LocalPlayer.statLifeMax2 ? Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.FullHealth") : $"{num.ToString()} {Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.Regeneration")}";
  }
}
