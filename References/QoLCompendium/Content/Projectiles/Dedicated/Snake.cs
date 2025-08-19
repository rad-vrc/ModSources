// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Dedicated.Snake
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Dedicated;

public class Snake : ModProjectile
{
  public virtual void SetStaticDefaults()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ProjectileID.Sets.CharacterPreviewAnimations[this.Projectile.type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[this.Projectile.type], 6, false).WithOffset(-10f, -20f).WithSpriteDirection(-1).WithCode(Snake.\u003C\u003EO.\u003C0\u003E__Float ?? (Snake.\u003C\u003EO.\u003C0\u003E__Float = new SettingsForCharacterPreview.CustomAnimationCode((object) null, __methodptr(Float))));
  }

  public virtual void SetDefaults()
  {
    this.Projectile.CloneDefaults(774);
    this.AIType = 774;
    ((Entity) this.Projectile).width = 38;
    ((Entity) this.Projectile).height = 52;
  }

  public virtual bool PreAI()
  {
    Main.player[this.Projectile.owner].petFlagBabyShark = false;
    return true;
  }

  public virtual void AI()
  {
    Player player = Main.player[this.Projectile.owner];
    if (player.dead || !player.HasBuff(ModContent.BuffType<SnakeBuff>()))
      return;
    this.Projectile.timeLeft = 2;
  }

  public virtual bool? CanCutTiles() => new bool?(false);
}
