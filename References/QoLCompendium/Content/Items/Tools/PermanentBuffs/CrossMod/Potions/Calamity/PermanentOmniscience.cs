// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity.PermanentOmniscience
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Potions;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Calamity;

[JITWhenModsEnabled(new string[] {"CalamityMod"})]
[ExtendsFromMod(new string[] {"CalamityMod"})]
public class PermanentOmniscience : IPermanentModdedBuffItem
{
  public virtual string Texture
  {
    get
    {
      return Common.ModBuffAsset(ModConditions.calamityMod, Common.GetModBuff(ModConditions.calamityMod, "Omniscience"));
    }
  }

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
    Recipe itemRecipe1 = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe1.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "PotionofOmniscience"), 30);
    itemRecipe1.AddTile(96 /*0x60*/);
    itemRecipe1.Register();
    Recipe itemRecipe2 = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe2.AddIngredient(304, 30);
    itemRecipe2.AddIngredient(296, 30);
    itemRecipe2.AddIngredient(2329, 30);
    itemRecipe2.AddTile(96 /*0x60*/);
    itemRecipe2.Register();
    Recipe itemRecipe3 = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe3.AddIngredient(ModContent.ItemType<PermanentHunter>(), 1);
    itemRecipe3.AddIngredient(ModContent.ItemType<PermanentSpelunker>(), 1);
    itemRecipe3.AddIngredient(ModContent.ItemType<PermanentDangersense>(), 1);
    itemRecipe3.AddTile(96 /*0x60*/);
    itemRecipe3.Register();
  }

  internal override void ApplyBuff(PermanentBuffPlayer player)
  {
    player.buffActive = true;
    player.modPotionEffects.Add((IPermanentModdedBuff) new OmniscienceEffect());
  }
}
