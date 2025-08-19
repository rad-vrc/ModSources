using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x020006DF RID: 1759
	public class DroneCameraTracker
	{
		// Token: 0x0600493B RID: 18747 RVA: 0x0064D5C6 File Offset: 0x0064B7C6
		public void Track(Projectile proj)
		{
			this._trackedProjectile = proj;
			this._lastTrackedType = proj.type;
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x0064D5DB File Offset: 0x0064B7DB
		public void Clear()
		{
			this._trackedProjectile = null;
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x0064D5E4 File Offset: 0x0064B7E4
		public void WorldClear()
		{
			this._lastTrackedType = 0;
			this._trackedProjectile = null;
			this._inUse = false;
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x0064D5FC File Offset: 0x0064B7FC
		private void ValidateTrackedProjectile()
		{
			if (this._trackedProjectile == null || !this._trackedProjectile.active || this._trackedProjectile.type != this._lastTrackedType || this._trackedProjectile.owner != Main.myPlayer || !Main.LocalPlayer.remoteVisionForDrone)
			{
				this.Clear();
			}
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x0064D655 File Offset: 0x0064B855
		public bool IsInUse()
		{
			return this._inUse;
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x0064D65D File Offset: 0x0064B85D
		public bool TryTracking(out Vector2 cameraPosition)
		{
			this.ValidateTrackedProjectile();
			cameraPosition = default(Vector2);
			if (this._trackedProjectile == null)
			{
				this._inUse = false;
				return false;
			}
			cameraPosition = this._trackedProjectile.Center;
			this._inUse = true;
			return true;
		}

		// Token: 0x04005EA4 RID: 24228
		private Projectile _trackedProjectile;

		// Token: 0x04005EA5 RID: 24229
		private int _lastTrackedType;

		// Token: 0x04005EA6 RID: 24230
		private bool _inUse;
	}
}
