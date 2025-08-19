// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Other.DebuffDamageInfoDisplay
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.UI.Other;

public class DebuffDamageInfoDisplay : InfoDisplay
{
  public virtual string Texture => "QoLCompendium/Assets/InfoDisplayIcons/DebuffDamageInfoDisplay";

  public virtual bool Active() => Main.LocalPlayer.GetModPlayer<InfoPlayer>().deteriorationDisplay;

  public virtual string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
  {
    int dps = DebuffDamageInfoDisplay.GetDPS(Main.myPlayer);
    if (dps > 0)
      return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.DDPS", (object) dps);
    displayColor = InfoDisplay.InactiveInfoTextColor;
    return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.NoDPS");
  }

  private static int GetDPS(int player)
  {
    double dps = 0.0;
    for (int index = 0; index < Main.maxNPCs; ++index)
    {
      NPC npc = Main.npc[index];
      if (((Entity) npc).active && npc.lifeRegen < 0 && npc.playerInteraction[player])
        dps -= (double) npc.lifeRegen / 2.0;
    }
    return (int) dps;
  }
}
