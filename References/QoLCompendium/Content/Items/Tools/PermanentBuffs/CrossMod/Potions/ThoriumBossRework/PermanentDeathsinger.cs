// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.ThoriumBossRework.PermanentDeathsinger
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using QoLCompendium.Core.PermanentBuffSystems;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.ThoriumBossRework;
using System;
using Terraria;
using Terraria.ModLoader;
using ThoriumRework;
using ThoriumRework.Buffs;
using ThoriumRework.Items;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.ThoriumBossRework;

[JITWhenModsEnabled(new string[] {"ThoriumRework"})]
[ExtendsFromMod(new string[] {"ThoriumRework"})]
public class PermanentDeathsinger : IPermanentModdedBuffItem
{
  public override bool IsLoadingEnabled(Mod mod)
  {
    return ModContent.GetInstance<CompatConfig>().extraPotions;
  }

  public virtual string Texture
  {
    get
    {
      return Common.ModBuffAsset(ModConditions.thoriumBossReworkMod, ModContent.BuffType<Deathsinger>());
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
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PermanentBuffs), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(ModContent.ItemType<DeathsingerPotion>(), 30);
    itemRecipe.AddTile(96 /*0x60*/);
    itemRecipe.Register();
  }

  internal override void ApplyBuff(PermanentBuffPlayer player)
  {
    player.buffActive = true;
    player.modPotionEffects.Add((IPermanentModdedBuff) new DeathsingerEffect());
  }
}
