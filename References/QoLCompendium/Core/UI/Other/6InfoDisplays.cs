// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Other.WingTimeInfoDisplay
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.UI.Other;

public class WingTimeInfoDisplay : InfoDisplay
{
  public virtual string Texture => "QoLCompendium/Assets/InfoDisplayIcons/WingTimeInfoDisplay";

  public virtual bool Active() => Main.LocalPlayer.GetModPlayer<InfoPlayer>().wingTimer;

  public virtual string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
  {
    float num = Main.LocalPlayer.wingTime / 60f;
    return Main.LocalPlayer.empressBrooch && !ModConditions.calamityLoaded ? Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.InfiniteWingTime") : num.ToString("0.00") + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.WingTime");
  }
}
