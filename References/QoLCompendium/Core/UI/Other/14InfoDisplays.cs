// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Other.QuestFishInfoDisplay
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.UI.Other;

public class QuestFishInfoDisplay : InfoDisplay
{
  public virtual string Texture => "QoLCompendium/Assets/InfoDisplayIcons/QuestFishInfoDisplay";

  public virtual bool Active() => Main.LocalPlayer.GetModPlayer<InfoPlayer>().anglerRadar;

  public virtual string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
  {
    if (!Main.anglerQuestFinished && Main.anglerQuest != -1 && Main.anglerQuest < Main.anglerQuestItemNetIDs.Length && NPC.AnyNPCs(369))
      return Lang.GetItemNameValue(Main.anglerQuestItemNetIDs[Main.anglerQuest]);
    displayColor = InfoDisplay.InactiveInfoTextColor;
    return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.QuestFinished");
  }
}
