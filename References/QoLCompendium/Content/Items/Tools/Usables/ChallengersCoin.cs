// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.ChallengersCoin
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Core;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class ChallengersCoin : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.ChallengersCoin;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 32 /*0x20*/;
    ((Entity) this.Item).height = 32 /*0x20*/;
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.useStyle = 4;
    this.Item.UseSound = new SoundStyle?(SoundID.Item4);
    this.Item.consumable = false;
    this.Item.SetShopValues((ItemRarityColor) -1, Item.buyPrice(0, 2, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.ChallengersCoin);
  }

  public virtual bool AltFunctionUse(Player player) => true;

  public virtual bool CanUseItem(Player player)
  {
    for (int index = 0; index < Main.maxNPCs; ++index)
    {
      if (((Entity) Main.npc[index]).active && Main.npc[index].boss)
        return false;
    }
    return true;
  }

  public virtual bool? UseItem(Player player)
  {
    string textValue;
    if (player.altFunctionUse == 2)
    {
      if (Main.GameMode == 3)
      {
        Main.GameMode = 1;
        ChallengersCoin.ChangeAllPlayerDifficulty((byte) 0);
        textValue = Language.GetTextValue("Mods.QoLCompendium.Messages.WorldExpert");
      }
      else
      {
        Main.GameMode = 3;
        ChallengersCoin.ChangeAllPlayerDifficulty((byte) 3);
        textValue = Language.GetTextValue("Mods.QoLCompendium.Messages.WorldJourney");
      }
    }
    else
    {
      switch (Main.GameMode)
      {
        case 0:
          Main.GameMode = 1;
          ChallengersCoin.ChangeAllPlayerDifficulty((byte) 0);
          textValue = Language.GetTextValue("Mods.QoLCompendium.Messages.WorldExpert");
          break;
        case 1:
          Main.GameMode = 2;
          ChallengersCoin.ChangeAllPlayerDifficulty((byte) 0);
          textValue = Language.GetTextValue("Mods.QoLCompendium.Messages.WorldMaster");
          break;
        default:
          Main.GameMode = 0;
          ChallengersCoin.ChangeAllPlayerDifficulty((byte) 0);
          textValue = Language.GetTextValue("Mods.QoLCompendium.Messages.WorldNormal");
          break;
      }
    }
    switch (Main.netMode)
    {
      case 0:
        Main.NewText((object) textValue, new Color?(new Color(175, 75, (int) byte.MaxValue)));
        break;
      case 2:
        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(textValue), new Color(175, 75, (int) byte.MaxValue), -1);
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        break;
    }
    SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
    return new bool?(true);
  }

  private static void ChangeAllPlayerDifficulty(byte diff)
  {
    for (int index = 0; index < (int) byte.MaxValue; ++index)
    {
      Player player = Main.player[index];
      if (((Entity) player).active)
      {
        player.difficulty = diff;
        NetMessage.SendData(4, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
    }
  }

  public virtual void UpdateInventory(Player player)
  {
    if (Main.GameMode == 0)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.ChallengersCoin.Normal"));
    if (Main.GameMode == 1)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.ChallengersCoin.Expert"));
    if (Main.GameMode == 2)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.ChallengersCoin.Master"));
    if (Main.GameMode != 3)
      return;
    this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.ChallengersCoin.Journey"));
  }

  public virtual bool PreDrawInInventory(
    SpriteBatch spriteBatch,
    Vector2 position,
    Rectangle frame,
    Color drawColor,
    Color itemColor,
    Vector2 origin,
    float scale)
  {
    Texture2D texture2D = Asset<Texture2D>.op_Explicit(ModContent.Request<Texture2D>($"QoLCompendium/Assets/Items/ChallengersCoin_{Main.GameMode}", (AssetRequestMode) 2));
    spriteBatch.Draw(texture2D, position, new Rectangle?(frame), drawColor, 0.0f, origin, scale, (SpriteEffects) 0, 0.0f);
    return false;
  }

  public virtual bool PreDrawInWorld(
    SpriteBatch spriteBatch,
    Color lightColor,
    Color alphaColor,
    ref float rotation,
    ref float scale,
    int whoAmI)
  {
    Texture2D texture2D = Asset<Texture2D>.op_Explicit(ModContent.Request<Texture2D>($"QoLCompendium/Assets/Items/ChallengersCoin_{Main.GameMode}", (AssetRequestMode) 2));
    Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Item).position, Main.screenPosition), new Vector2(16f, 16f));
    Rectangle rectangle;
    // ISSUE: explicit constructor call
    ((Rectangle) ref rectangle).\u002Ector(0, 0, 32 /*0x20*/, 32 /*0x20*/);
    spriteBatch.Draw(texture2D, vector2, new Rectangle?(rectangle), lightColor, rotation, new Vector2(16f, 16f), scale, (SpriteEffects) 0, 0.0f);
    return false;
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.ChallengersCoin), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 16 /*0x10*/);
    itemRecipe.AddIngredient(73, 1);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
