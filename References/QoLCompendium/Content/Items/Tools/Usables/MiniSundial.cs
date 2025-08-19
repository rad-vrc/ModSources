// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.MiniSundial
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class MiniSundial : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.MiniSundial;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 12;
    ((Entity) this.Item).height = 12;
    this.Item.useStyle = 4;
    this.Item.UseSound = new SoundStyle?(SoundID.Item4);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.SetShopValues((ItemRarityColor) 3, Item.buyPrice(0, 0, 90, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.MiniSundial);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.MiniSundial), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(173, 12);
    itemRecipe.AddIngredient(824, 12);
    itemRecipe.AddTile(305);
    itemRecipe.Register();
  }

  public virtual bool AltFunctionUse(Player player) => true;

  public virtual bool CanUseItem(Player player) => !Main.IsFastForwardingTime();

  public virtual bool? UseItem(Player player)
  {
    if (player.altFunctionUse == 2)
    {
      Main.sundialCooldown = 0;
      SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      if (Main.netMode == 1)
      {
        NetMessage.SendData(51, -1, -1, (NetworkText) null, Main.myPlayer, 3f, 0.0f, 0.0f, 0, 0, 0);
        return new bool?(true);
      }
      if (Main.dayTime)
        Main.fastForwardTimeToDusk = true;
      else
        Main.fastForwardTimeToDawn = true;
      NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }
    else
    {
      int num1 = 27000;
      int num2 = 16200;
      if (Main.dayTime && Main.time < (double) num1)
        Main.time = (double) num1;
      else if (Main.time < (double) num2)
      {
        Main.time = (double) num2;
      }
      else
      {
        Main.dayTime = !Main.dayTime;
        Main.time = 0.0;
        if (Main.dayTime)
        {
          BirthdayParty.CheckMorning();
          Chest.SetupTravelShop();
        }
        else
        {
          BirthdayParty.CheckNight();
          if (!Main.dayTime && ++Main.moonPhase > 7)
            Main.moonPhase = 0;
        }
      }
    }
    return new bool?(true);
  }
}
