// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded.PermanentFlasks
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Flasks;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded;

public class PermanentFlasks : IPermanentBuffItem
{
  public override string Texture => "QoLCompendium/Assets/Items/PermanentFlasks";

  public virtual void SetDefaults() => Common.SetDefaultsToPermanentBuff(this.Item);

  public virtual void UpdateInventory(Player player)
  {
    PermanentBuffPlayer player1;
    if (player.TryGetModPlayer<PermanentBuffPlayer>(ref player1))
      this.ApplyBuff(player1);
    if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 0)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfCursedFlames"));
    if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 1)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfFire"));
    if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 2)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfGold"));
    if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 3)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfIchor"));
    if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 4)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfNanites"));
    if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 5)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfParty"));
    if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 6)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfPoison"));
    if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode != 7)
      return;
    this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentFlasks.FlaskOfVenom"));
  }

  public virtual void OnConsumeItem(Player player) => ++this.Item.stack;

  public virtual bool CanRightClick() => true;

  public virtual void RightClick(Player player)
  {
    ++player.GetModPlayer<QoLCPlayer>().flaskEffectMode;
    if (player.GetModPlayer<QoLCPlayer>().flaskEffectMode <= 7)
      return;
    player.GetModPlayer<QoLCPlayer>().flaskEffectMode = 0;
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfCursedFlames>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfFire>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfGold>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfIchor>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfNanites>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfParty>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfPoison>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentFlaskOfVenom>(), 1);
    itemRecipe.AddTile(96 /*0x60*/);
    itemRecipe.Register();
  }

  internal override void ApplyBuff(PermanentBuffPlayer player)
  {
    player.buffActive = true;
    player.potionEffects.Add((IPermanentBuff) new FlaskEffect());
  }
}
