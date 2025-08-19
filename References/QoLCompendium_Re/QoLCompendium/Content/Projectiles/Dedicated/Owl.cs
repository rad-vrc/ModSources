using System;
using System.Runtime.CompilerServices;
using QoLCompendium.Content.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Dedicated
{
	// Token: 0x0200003A RID: 58
	public class Owl : ModProjectile
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00007C78 File Offset: 0x00005E78
		public override void SetStaticDefaults()
		{
			Main.projFrames[base.Projectile.type] = 4;
			SettingsForCharacterPreview[] characterPreviewAnimations = ProjectileID.Sets.CharacterPreviewAnimations;
			int type = base.Projectile.type;
			SettingsForCharacterPreview settingsForCharacterPreview = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Projectile.type], 6, false).WithOffset(-10f, -20f).WithSpriteDirection(-1);
			SettingsForCharacterPreview.CustomAnimationCode customAnimation;
			if ((customAnimation = Owl.<>O.<0>__Float) == null)
			{
				customAnimation = (Owl.<>O.<0>__Float = new SettingsForCharacterPreview.CustomAnimationCode(DelegateMethods.CharacterPreview.Float));
			}
			characterPreviewAnimations[type] = settingsForCharacterPreview.WithCode(customAnimation);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007CF6 File Offset: 0x00005EF6
		public override void SetDefaults()
		{
			base.Projectile.CloneDefaults(380);
			base.AIType = 380;
			base.Projectile.width = 26;
			base.Projectile.height = 28;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007AC9 File Offset: 0x00005CC9
		public override bool PreAI()
		{
			Main.player[base.Projectile.owner].zephyrfish = false;
			return true;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007D30 File Offset: 0x00005F30
		public override void AI()
		{
			Player player = Main.player[base.Projectile.owner];
			if (!player.dead && player.HasBuff(ModContent.BuffType<OwlBuff>()))
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

		// Token: 0x06000107 RID: 263 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanCutTiles()
		{
			return new bool?(false);
		}

		// Token: 0x020003A7 RID: 935
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040007B1 RID: 1969
			public static SettingsForCharacterPreview.CustomAnimationCode <0>__Float;
		}
	}
}
