// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.MobileStorages.AllInOneAccess
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.PlayerChanges;
using QoLCompendium.Core.UI.Panels;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.MobileStorages;

public class AllInOneAccess : ModItem
{
  public int Mode;

  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.MobileStorages;
  }

  public virtual void SetStaticDefaults()
  {
    Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(4, 8, false));
    this.Item.ResearchUnlockCount = 1;
  }

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 16 /*0x10*/;
    ((Entity) this.Item).height = 24;
    this.Item.maxStack = 1;
    this.Item.consumable = false;
    this.Item.useStyle = 4;
    this.Item.UseSound = new SoundStyle?(SoundID.MenuOpen);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.SetShopValues((ItemRarityColor) 3, Item.buyPrice(0, 10, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.MobileStorages);
  }

  public virtual void OnConsumeItem(Player player) => ++this.Item.stack;

  public virtual bool CanRightClick() => true;

  public virtual void RightClick(Player player)
  {
    SoundEngine.PlaySound(ref SoundID.Item130, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
    ++this.Mode;
    if (this.Mode <= 1)
      return;
    this.Mode = 0;
  }

  public virtual void SaveData(TagCompound tag) => tag["AllInOneAccessMode"] = (object) this.Mode;

  public virtual void LoadData(TagCompound tag) => this.Mode = tag.GetInt("AllInOneAccessMode");

  public virtual void HoldItem(Player player)
  {
    if (!AllInOneAccessUI.visible)
      return;
    player.GetModPlayer<BankPlayer>().chests = true;
  }

  public virtual void UpdateInventory(Player player)
  {
    if (AllInOneAccessUI.visible)
      player.GetModPlayer<BankPlayer>().chests = true;
    if (this.Mode == 0)
    {
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.AllInOneAccess.Open"));
      player.IsVoidVaultEnabled = true;
    }
    if (this.Mode != 1)
      return;
    this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.AllInOneAccess.Closed"));
    player.IsVoidVaultEnabled = false;
  }

  public virtual bool? UseItem(Player player)
  {
    if (!AllInOneAccessUI.visible)
      AllInOneAccessUI.visible = true;
    return new bool?(true);
  }

  public virtual bool CanUseItem(Player player) => !AllInOneAccessUI.visible;

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.MobileStorages), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(3213, 1);
    itemRecipe.AddIngredient(ModContent.ItemType<EtherianConstruct>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<FlyingSafe>(), 1);
    itemRecipe.AddIngredient(4131, 1);
    itemRecipe.AddTile(114);
    itemRecipe.Register();
  }
}
