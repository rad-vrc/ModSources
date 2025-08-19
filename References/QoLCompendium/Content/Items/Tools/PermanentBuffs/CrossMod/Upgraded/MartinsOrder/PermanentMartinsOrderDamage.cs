// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.MartinsOrder.PermanentMartinsOrderDamage
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.MartinsOrder;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.MartinsOrder;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.MartinsOrder;

[JITWhenModsEnabled(new string[] {"MartainsOrder"})]
[ExtendsFromMod(new string[] {"MartainsOrder"})]
public class PermanentMartinsOrderDamage : IPermanentModdedBuffItem
{
  public virtual string Texture => "QoLCompendium/Assets/Items/PermanentMartinsOrderDamage";

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
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentDefender>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentEmpowerment>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentEvocation>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentHaste>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentShooter>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentSpellCaster>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentStarreach>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentSweeper>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentThrower>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentWhipper>(), 1);
    itemRecipe.AddTile(96 /*0x60*/);
    itemRecipe.Register();
  }

  internal override void ApplyBuff(PermanentBuffPlayer player)
  {
    player.buffActive = true;
    player.modPotionEffects.Add((IPermanentModdedBuff) new MartinsOrderDamageEffect());
  }
}
