// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Clamity.PermanentSupremeLuck
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Clamity;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Clamity;

[JITWhenModsEnabled(new string[] {"Clamity"})]
[ExtendsFromMod(new string[] {"Clamity"})]
public class PermanentSupremeLuck : IPermanentModdedBuffItem
{
  public virtual string Texture
  {
    get
    {
      return Common.ModBuffAsset(ModConditions.clamityAddonMod, Common.GetModBuff(ModConditions.clamityAddonMod, "SupremeLucky"));
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
    if (!ModConditions.clamityAddonLoaded)
      return;
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(Common.GetModItem(ModConditions.clamityAddonMod, "SupremeLuckPotion"), 30);
    itemRecipe.AddTile(96 /*0x60*/);
    itemRecipe.Register();
  }

  internal override void ApplyBuff(PermanentBuffPlayer player)
  {
    player.buffActive = true;
    player.modPotionEffects.Add((IPermanentModdedBuff) new SupremeLuckEffect());
  }
}
