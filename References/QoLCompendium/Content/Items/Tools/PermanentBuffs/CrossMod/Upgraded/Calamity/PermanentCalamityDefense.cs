// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Calamity.PermanentCalamityDefense
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Calamity;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Calamity;

[JITWhenModsEnabled(new string[] {"CalamityMod"})]
[ExtendsFromMod(new string[] {"CalamityMod"})]
public class PermanentCalamityDefense : IPermanentModdedBuffItem
{
  public virtual string Texture => "QoLCompendium/Assets/Items/PermanentCalamityDefense";

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
    if (!ModConditions.calamityLoaded)
      return;
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentBaguette>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentBloodfin>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentPhotosynthesis>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentTesla>(), 1);
    itemRecipe.AddTile(96 /*0x60*/);
    itemRecipe.Register();
  }

  internal override void ApplyBuff(PermanentBuffPlayer player)
  {
    player.buffActive = true;
    player.modPotionEffects.Add((IPermanentModdedBuff) new CalamityDefenseEffect());
  }
}
