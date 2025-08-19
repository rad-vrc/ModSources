// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded.PermanentStation
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Stations;
using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.Vanilla.Upgraded;

public class PermanentStation : IPermanentBuffItem
{
  public override string Texture => "QoLCompendium/Assets/Items/PermanentStation";

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
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentAmmoBox>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentBewitchingTable>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentCrystalBall>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentSharpeningStation>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentSliceOfCake>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<PermanentWarTable>(), 1);
    itemRecipe.AddTile(96 /*0x60*/);
    itemRecipe.Register();
  }

  internal override void ApplyBuff(PermanentBuffPlayer player)
  {
    player.buffActive = true;
    player.potionEffects.Add((IPermanentBuff) new StationEffect());
  }
}
