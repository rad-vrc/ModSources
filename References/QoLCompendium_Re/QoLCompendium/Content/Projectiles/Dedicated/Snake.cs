using System;
using System.Runtime.CompilerServices;
using QoLCompendium.Content.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Dedicated
{
	// Token: 0x0200003C RID: 60
	public class Snake : ModProjectile
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00007E98 File Offset: 0x00006098
		public override void SetStaticDefaults()
		{
			SettingsForCharacterPreview[] characterPreviewAnimations = ProjectileID.Sets.CharacterPreviewAnimations;
			int type = base.Projectile.type;
			SettingsForCharacterPreview settingsForCharacterPreview = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Projectile.type], 6, false).WithOffset(-10f, -20f).WithSpriteDirection(-1);
			SettingsForCharacterPreview.CustomAnimationCode customAnimation;
			if ((customAnimation = Snake.<>O.<0>__Float) == null)
			{
				customAnimation = (Snake.<>O.<0>__Float = new SettingsForCharacterPreview.CustomAnimationCode(DelegateMethods.CharacterPreview.Float));
			}
			characterPreviewAnimations[type] = settingsForCharacterPreview.WithCode(customAnimation);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007F04 File Offset: 0x00006104
		public override void SetDefaults()
		{
			base.Projectile.CloneDefaults(774);
			base.AIType = 774;
			base.Projectile.width = 38;
			base.Projectile.height = 52;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007F3B File Offset: 0x0000613B
		public override bool PreAI()
		{
			Main.player[base.Projectile.owner].petFlagBabyShark = false;
			return true;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007F58 File Offset: 0x00006158
		public override void AI()
		{
			Player player = Main.player[base.Projectile.owner];
			if (!player.dead && player.HasBuff(ModContent.BuffType<SnakeBuff>()))
			{
				base.Projectile.timeLeft = 2;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanCutTiles()
		{
			return new bool?(false);
		}

		// Token: 0x020003A8 RID: 936
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040007B2 RID: 1970
			public static SettingsForCharacterPreview.CustomAnimationCode <0>__Float;
		}
	}
}
