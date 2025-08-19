// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.PotionCrate
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Humanizer;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class PotionCrate : ModItem
{
  public static List<int> BuffIDList = new List<int>();
  public static List<int> ItemIDList = new List<int>();

  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.PotionCrate;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 16 /*0x10*/;
    ((Entity) this.Item).height = 16 /*0x10*/;
    this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 0, 20, 0));
  }

  public virtual void SaveData(TagCompound tag)
  {
    tag.Add("BuffIDList", (object) PotionCrate.BuffIDList);
    tag.Add("ItemIDList", (object) PotionCrate.ItemIDList);
  }

  public virtual void LoadData(TagCompound tag)
  {
    PotionCrate.BuffIDList = (List<int>) tag.GetList<int>("BuffIDList");
    PotionCrate.ItemIDList = (List<int>) tag.GetList<int>("ItemIDList");
  }

  public virtual bool ConsumeItem(Player player) => false;

  public virtual bool CanRightClick() => true;

  public virtual void RightClick(Player player)
  {
    if (Main.mouseItem.buffType > 0 && Main.mouseItem.stack >= QoLCompendium.QoLCompendium.mainConfig.EndlessBuffAmount && !PotionCrate.BuffIDList.Contains(Main.mouseItem.buffType) && !PotionCrate.ItemIDList.Contains(Main.mouseItem.type))
    {
      PotionCrate.BuffIDList.Add(Main.mouseItem.buffType);
      PotionCrate.ItemIDList.Add(Main.mouseItem.type);
      Main.mouseItem.stack -= QoLCompendium.QoLCompendium.mainConfig.EndlessBuffAmount;
      if (Main.mouseItem.stack != 0)
        return;
      Main.mouseItem.TurnToAir(false);
    }
    else
    {
      if (PotionCrate.BuffIDList.Count == 0 && PotionCrate.ItemIDList.Count == 0 || PotionCrate.BuffIDList.Count <= 0 || PotionCrate.ItemIDList.Count <= 0)
        return;
      Item.NewItem(((Entity) this.Item).GetSource_FromThis((string) null), ((Entity) player).position, PotionCrate.ItemIDList.Last<int>(), QoLCompendium.QoLCompendium.mainConfig.EndlessBuffAmount, false, 0, false, false);
      PotionCrate.BuffIDList.RemoveAt(PotionCrate.BuffIDList.Count - 1);
      PotionCrate.ItemIDList.RemoveAt(PotionCrate.ItemIDList.Count - 1);
    }
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine1 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "Tooltip0"));
    TooltipLine tooltipLine2 = new TooltipLine(((ModType) this).Mod, "PotionCrateTooltip", StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.PotionCrateTooltip"), new object[1]
    {
      (object) QoLCompendium.QoLCompendium.mainConfig.EndlessBuffAmount
    }));
    tooltips.Insert(tooltips.IndexOf(tooltipLine1) + 1, tooltipLine2);
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.PotionCrate);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PotionCrate), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(9, 12);
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
