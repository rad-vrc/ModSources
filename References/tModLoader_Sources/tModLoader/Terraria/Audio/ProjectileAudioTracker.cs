using System;

namespace Terraria.Audio
{
	// Token: 0x02000768 RID: 1896
	public class ProjectileAudioTracker
	{
		// Token: 0x06004CA3 RID: 19619 RVA: 0x00670845 File Offset: 0x0066EA45
		public ProjectileAudioTracker(Projectile proj)
		{
			this._expectedIndex = proj.whoAmI;
			this._expectedType = proj.type;
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x00670868 File Offset: 0x0066EA68
		public bool IsActiveAndInGame()
		{
			if (Main.gameMenu)
			{
				return false;
			}
			Projectile projectile = Main.projectile[this._expectedIndex];
			return projectile.active && projectile.type == this._expectedType;
		}

		// Token: 0x04006114 RID: 24852
		private int _expectedType;

		// Token: 0x04006115 RID: 24853
		private int _expectedIndex;
	}
}
