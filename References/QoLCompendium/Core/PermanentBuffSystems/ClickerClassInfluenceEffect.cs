// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.ClickerClassInfluenceEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using ClickerClass;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Clicker;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems;

[JITWhenModsEnabled(new string[] {"ClickerClass"})]
[ExtendsFromMod(new string[] {"ClickerClass"})]
public class ClickerClassInfluenceEffect : ModPlayer
{
  public readonly int RadiusIncrease = 20;

  public virtual void PreUpdateBuffs()
  {
    if (!this.Player.HasItemInAnyInventory(ModContent.ItemType<PermanentInfluence>()))
      return;
    this.Player.GetModPlayer<ClickerPlayer>().clickerRadius += (float) (2 * this.RadiusIncrease) / 100f;
  }
}
