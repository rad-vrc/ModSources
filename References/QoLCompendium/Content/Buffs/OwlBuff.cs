// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Buffs.OwlBuff
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Projectiles.Dedicated;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Buffs;

public class OwlBuff : ModBuff
{
  public virtual void SetStaticDefaults()
  {
    Main.buffNoTimeDisplay[this.Type] = true;
    Main.vanityPet[this.Type] = true;
  }

  public virtual void Update(Player player, ref int buffIndex)
  {
    bool flag = false;
    player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref flag, ModContent.ProjectileType<Owl>(), 18000);
  }
}
