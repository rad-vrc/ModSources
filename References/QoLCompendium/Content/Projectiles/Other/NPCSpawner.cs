// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Other.NPCSpawner
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Other;

public class NPCSpawner : ModProjectile
{
  public virtual string Texture => "QoLCompendium/Assets/Projectiles/Invisible";

  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 2;
    ((Entity) this.Projectile).height = 2;
    this.Projectile.aiStyle = -1;
    this.Projectile.timeLeft = 1;
    this.Projectile.tileCollide = false;
    this.Projectile.ignoreWater = true;
    this.Projectile.hide = true;
  }

  public virtual bool? CanDamage() => new bool?(false);

  public virtual void OnKill(int timeLeft)
  {
    if (Main.netMode == 1)
      return;
    int num = NPC.NewNPC(NPC.GetBossSpawnSource(this.Projectile.owner), (int) ((Entity) this.Projectile).Center.X, (int) ((Entity) this.Projectile).Center.Y, (int) this.Projectile.ai[0], 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
    if (num == Main.maxNPCs || Main.netMode != 2)
      return;
    NetMessage.SendData(23, -1, -1, (NetworkText) null, num, 0.0f, 0.0f, 0.0f, 0, 0, 0);
  }
}
