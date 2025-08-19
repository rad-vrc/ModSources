// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded.PermanentDefense
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded;

public class PermanentDefense : IPermanentBuffItem
{
  public override string Texture => "QoLCompendium/Assets/Items/PermanentDefense";

  public virtual void SetDefaults() => Common.SetDefaultsToPermanentBuff(this.Item);

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
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentEndurance>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentExquisitelyStuffed>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentHeartreach>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentInferno>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentIronskin>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentLifeforce>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentObsidianSkin>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentRegeneration>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentThorns>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentWarmth>(), 1);
    itemRecipe.AddTile(96 /*0x60*/);
    itemRecipe.Register();
  }

  internal override void ApplyBuff(PermanentBuffPlayer player)
  {
    player.buffActive = true;
    player.potionEffects.Add((IPermanentBuff) new DefenseEffect());
  }
}
