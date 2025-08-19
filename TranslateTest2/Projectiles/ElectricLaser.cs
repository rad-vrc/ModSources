// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Projectiles.ElectricLaser
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace TranslateTest2.Projectiles
{
    public class ElectricLaser : ModProjectile
    {
        public int frame;
        public int framecounter;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Electric Laser");
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
            Projectile.alpha = 0;
        }

        public override void AI()
        {
            if (++framecounter >= 6)
            {
                framecounter = 0;
                if (++frame >= 4)
                    frame = 0;
            }
            
            if (!Projectile.TryGetOwner(out Player owner) || owner == null || !owner.active)
            {
                Projectile.Kill();
                return;
            }

            Vector2 targetPos = new Vector2(Projectile.ai[0], Projectile.ai[1]);
            Projectile.position = Vector2.Lerp(Projectile.position, targetPos, 0.1f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / 4;
            Rectangle sourceRectangle = new Rectangle(0, frame * frameHeight, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, 
                Color.White * (1f - Projectile.alpha / 255f), Projectile.rotation, origin, 
                Projectile.scale, SpriteEffects.None, 0);
            
            return false;
        }
    }
}
