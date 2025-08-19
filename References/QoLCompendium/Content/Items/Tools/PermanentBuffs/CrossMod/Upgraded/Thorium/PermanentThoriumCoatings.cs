// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium.PermanentThoriumCoatings
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Flasks.Thorium;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.Thorium;

[JITWhenModsEnabled(new string[] {"ThoriumMod"})]
[ExtendsFromMod(new string[] {"ThoriumMod"})]
public class PermanentThoriumCoatings : IPermanentModdedBuffItem
{
  public virtual string Texture => "QoLCompendium/Assets/Items/PermanentThoriumCoatings";

  public override void SetDefaults() => Common.SetDefaultsToPermanentBuff(this.Item);

  public virtual void UpdateInventory(Player player)
  {
    PermanentBuffPlayer player1;
    if (player.TryGetModPlayer<PermanentBuffPlayer>(ref player1))
      this.ApplyBuff(player1);
    if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 0)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentThoriumCoatings.DeepFreezeCoating"));
    if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 1)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentThoriumCoatings.ExplosiveCoating"));
    if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 2)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentThoriumCoatings.GorgonCoating"));
    if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 3)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentThoriumCoatings.SporeCoating"));
    if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode != 4)
      return;
    this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PermanentThoriumCoatings.ToxicCoating"));
  }

  public virtual void OnConsumeItem(Player player) => ++this.Item.stack;

  public virtual bool CanRightClick() => true;

  public virtual void RightClick(Player player)
  {
    ++player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode;
    if (player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode <= 4)
      return;
    player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode = 0;
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentDeepFreezeCoating>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentExplosiveCoating>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentGorgonCoating>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentSporeCoating>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentToxicCoating>(), 1);
    itemRecipe.AddTile(96 /*0x60*/);
    itemRecipe.Register();
  }

  internal override void ApplyBuff(PermanentBuffPlayer player)
  {
    player.buffActive = true;
    player.modPotionEffects.Add((IPermanentModdedBuff) new ThoriumCoatingEffect());
  }
}
