// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.SOTSRippleEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.PermanentBuffs.All;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SOTS;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SOTS;
using SOTS;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems;

[JITWhenModsEnabled(new string[] {"SOTS"})]
[ExtendsFromMod(new string[] {"SOTS"})]
public class SOTSRippleEffect : ModPlayer
{
  public virtual void PreUpdateBuffs()
  {
    if (!this.Player.HasItemInAnyInventory(ModContent.ItemType<PermanentRipple>()) && !this.Player.HasItemInAnyInventory(ModContent.ItemType<PermanentSecretsOfTheShadows>()) && !this.Player.HasItemInAnyInventory(ModContent.ItemType<PermanentEverything>()))
      return;
    SOTSPlayer sotsPlayer = SOTSPlayer.ModPlayer(this.Player);
    sotsPlayer.rippleBonusDamage += 2;
    sotsPlayer.rippleEffect = true;
    ++sotsPlayer.rippleTimer;
  }
}
