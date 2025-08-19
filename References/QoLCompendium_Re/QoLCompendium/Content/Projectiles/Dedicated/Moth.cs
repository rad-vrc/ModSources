using System;
using System.Runtime.CompilerServices;
using QoLCompendium.Content.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Dedicated
{
	// Token: 0x02000039 RID: 57
	public class Moth : ModProjectile
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00007B24 File Offset: 0x00005D24
		public override void SetStaticDefaults()
		{
			Main.projFrames[base.Projectile.type] = 4;
			SettingsForCharacterPreview[] characterPreviewAnimations = ProjectileID.Sets.CharacterPreviewAnimations;
			int type = base.Projectile.type;
			SettingsForCharacterPreview settingsForCharacterPreview = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Projectile.type], 6, false).WithOffset(-10f, -20f).WithSpriteDirection(-1);
			SettingsForCharacterPreview.CustomAnimationCode customAnimation;
			if ((customAnimation = Moth.<>O.<0>__Float) == null)
			{
				customAnimation = (Moth.<>O.<0>__Float = new SettingsForCharacterPreview.CustomAnimationCode(DelegateMethods.CharacterPreview.Float));
			}
			characterPreviewAnimations[type] = settingsForCharacterPreview.WithCode(customAnimation);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007BA2 File Offset: 0x00005DA2
		public override void SetDefaults()
		{
			base.Projectile.CloneDefaults(380);
			base.AIType = 380;
			base.Projectile.width = 48;
			base.Projectile.height = 34;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007AC9 File Offset: 0x00005CC9
		public override bool PreAI()
		{
			Main.player[base.Projectile.owner].zephyrfish = false;
			return true;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007BDC File Offset: 0x00005DDC
		public override void AI()
		{
			Player player = Main.player[base.Projectile.owner];
			if (!player.dead && player.HasBuff(ModContent.BuffType<MothBuff>()))
			{
				base.Projectile.timeLeft = 2;
			}
			Projectile projectile = base.Projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 15)
			{
				base.Projectile.frameCounter = 0;
				Projectile projectile2 = base.Projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= Main.projFrames[base.Projectile.type])
				{
					base.Projectile.frame = 0;
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanCutTiles()
		{
			return new bool?(false);
		}

		// Token: 0x020003A6 RID: 934
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040007B0 RID: 1968
			public static SettingsForCharacterPreview.CustomAnimationCode <0>__Float;
		}
	}
}
