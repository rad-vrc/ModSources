// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.SuperDummy
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class SuperDummy : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.SuperDummy;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 20;
    ((Entity) this.Item).height = 30;
    this.Item.useTime = 15;
    this.Item.useAnimation = 15;
    this.Item.useStyle = 1;
    this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 0, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.SuperDummy);
  }

  public virtual bool AltFunctionUse(Player player) => true;

  public virtual bool? UseItem(Player player)
  {
    if (((Entity) player).whoAmI == Main.myPlayer)
    {
      if (player.altFunctionUse == 2)
      {
        switch (Main.netMode)
        {
          case 0:
            for (int index = 0; index < Main.maxNPCs; ++index)
            {
              if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<QoLCompendium.Content.NPCs.SuperDummy>())
              {
                NPC npc = Main.npc[index];
                npc.life = 0;
                npc.HitEffect(0, 10.0, new bool?());
                Main.npc[index].SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
              }
            }
            break;
          case 1:
            ModPacket packet = ((ModType) this).Mod.GetPacket(256 /*0x0100*/);
            ((BinaryWriter) packet).Write((byte) 5);
            packet.Send(-1, -1);
            break;
        }
      }
      else if (NPC.CountNPCS(ModContent.NPCType<QoLCompendium.Content.NPCs.SuperDummy>()) < 100)
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector((float) (int) Main.MouseWorld.X, (float) (int) Main.MouseWorld.Y);
        int num = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int) vector2.X, (int) vector2.Y, ModContent.NPCType<QoLCompendium.Content.NPCs.SuperDummy>(), 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
        if (num != Main.maxNPCs && Main.netMode == 2)
          NetMessage.SendData(23, -1, -1, (NetworkText) null, num, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
    }
    return new bool?(true);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.SuperDummy), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(3202, 1);
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
    itemRecipe.AddIngredient(75, 1);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
