// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SOTS.PermanentSecretsOfTheShadows
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SOTS;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Stations.SOTS;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.SOTS;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SOTS;

[JITWhenModsEnabled(new string[] {"SOTS"})]
[ExtendsFromMod(new string[] {"SOTS"})]
public class PermanentSecretsOfTheShadows : IPermanentModdedBuffItem
{
  public virtual string Texture => "QoLCompendium/Assets/Items/PermanentSecretsOfTheShadows";

  public override void SetDefaults() => Common.SetDefaultsToPermanentBuff(this.Item);

  public virtual void UpdateInventory(Player player)
  {
    PermanentBuffPlayer player1;
    if (!player.TryGetModPlayer<PermanentBuffPlayer>(ref player1))
      return;
    this.ApplyBuff(player1);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentAssassination>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentBluefire>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentBrittle>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentDoubleVision>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentHarmony>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentNightmare>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentRipple>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentRoughskin>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentSoulAccess>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentVibe>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentDigitalDisplay>(), 1);
    itemRecipe.AddTile(96 /*0x60*/);
    itemRecipe.Register();
  }

  internal override void ApplyBuff(PermanentBuffPlayer player)
  {
    player.buffActive = true;
    player.modPotionEffects.Add((IPermanentModdedBuff) new SecretsOfTheShadowsEffect());
  }
}
