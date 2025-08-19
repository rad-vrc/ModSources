using System;

namespace Terraria.Audio
{
	// Token: 0x0200047A RID: 1146
	public class ProjectileAudioTracker
	{
		// Token: 0x06002D9E RID: 11678 RVA: 0x005BF16A File Offset: 0x005BD36A
		public ProjectileAudioTracker(Projectile proj)
		{
			this._expectedIndex = proj.whoAmI;
			this._expectedType = proj.type;
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x005BF18C File Offset: 0x005BD38C
		public bool IsActiveAndInGame()
		{
			if (Main.gameMenu)
			{
				return false;
			}
			Projectile projectile = Main.projectile[this._expectedIndex];
			return projectile.active && projectile.type == this._expectedType;
		}

		// Token: 0x0400514E RID: 20814
		private int _expectedType;

		// Token: 0x0400514F RID: 20815
		private int _expectedIndex;
	}
}
