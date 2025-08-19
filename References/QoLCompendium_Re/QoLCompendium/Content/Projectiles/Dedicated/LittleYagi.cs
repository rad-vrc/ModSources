using System;
using System.Runtime.CompilerServices;
using QoLCompendium.Content.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Dedicated
{
	// Token: 0x02000038 RID: 56
	public class LittleYagi : ModProjectile
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00007A40 File Offset: 0x00005C40
		public override void SetStaticDefaults()
		{
			SettingsForCharacterPreview[] characterPreviewAnimations = ProjectileID.Sets.CharacterPreviewAnimations;
			int type = base.Projectile.type;
			SettingsForCharacterPreview settingsForCharacterPreview = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Projectile.type], 6, false).WithOffset(-10f, -20f).WithSpriteDirection(-1);
			SettingsForCharacterPreview.CustomAnimationCode customAnimation;
			if ((customAnimation = LittleYagi.<>O.<0>__Float) == null)
			{
				customAnimation = (LittleYagi.<>O.<0>__Float = new SettingsForCharacterPreview.CustomAnimationCode(DelegateMethods.CharacterPreview.Float));
			}
			characterPreviewAnimations[type] = settingsForCharacterPreview.WithCode(customAnimation);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007AAC File Offset: 0x00005CAC
		public override void SetDefaults()
		{
			base.Projectile.CloneDefaults(703);
			base.AIType = 703;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007AC9 File Offset: 0x00005CC9
		public override bool PreAI()
		{
			Main.player[base.Projectile.owner].zephyrfish = false;
			return true;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public override void AI()
		{
			Player player = Main.player[base.Projectile.owner];
			if (!player.dead && player.HasBuff(ModContent.BuffType<LittleYagiBuff>()))
			{
				base.Projectile.timeLeft = 2;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanCutTiles()
		{
			return new bool?(false);
		}

		// Token: 0x020003A5 RID: 933
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040007AF RID: 1967
			public static SettingsForCharacterPreview.CustomAnimationCode <0>__Float;
		}
	}
}
