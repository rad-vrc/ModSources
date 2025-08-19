// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.UltimateChecklist
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class UltimateChecklist : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.UltimateChecklist;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 12;
    ((Entity) this.Item).height = 15;
    this.Item.useStyle = 4;
    this.Item.UseSound = new SoundStyle?(SoundID.Item4);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.SetShopValues((ItemRarityColor) -1, Item.buyPrice(0, 0, 0, 0));
  }

  public virtual bool AltFunctionUse(Player player) => true;

  public virtual bool? UseItem(Player player)
  {
    if (player.altFunctionUse == 2)
    {
      for (int key = 0; key < NPCLoader.NPCCount; ++key)
      {
        string creditIdsByNpcNetId = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[key];
        Main.BestiaryTracker.Kills.SetKillCountDirectly(creditIdsByNpcNetId, 50);
        Main.BestiaryTracker.Sights.SetWasSeenDirectly(creditIdsByNpcNetId);
      }
    }
    else
    {
      for (int index = 0; index < ItemLoader.ItemCount; ++index)
        CreativeUI.ResearchItem(index);
    }
    return new bool?(true);
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.UltimateChecklist);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.UltimateChecklist), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(3467, 12);
    itemRecipe.AddIngredient(225, 8);
    itemRecipe.AddIngredient(1050, 1);
    itemRecipe.AddTile(412);
    itemRecipe.Register();
  }
}
