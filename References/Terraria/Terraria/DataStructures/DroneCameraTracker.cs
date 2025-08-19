using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x020003FD RID: 1021
	public class DroneCameraTracker
	{
		// Token: 0x06002AEF RID: 10991 RVA: 0x0059D352 File Offset: 0x0059B552
		public void Track(Projectile proj)
		{
			this._trackedProjectile = proj;
			this._lastTrackedType = proj.type;
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0059D367 File Offset: 0x0059B567
		public void Clear()
		{
			this._trackedProjectile = null;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0059D370 File Offset: 0x0059B570
		public void WorldClear()
		{
			this._lastTrackedType = 0;
			this._trackedProjectile = null;
			this._inUse = false;
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x0059D388 File Offset: 0x0059B588
		private void ValidateTrackedProjectile()
		{
			if (this._trackedProjectile == null || !this._trackedProjectile.active || this._trackedProjectile.type != this._lastTrackedType || this._trackedProjectile.owner != Main.myPlayer || !Main.LocalPlayer.remoteVisionForDrone)
			{
				this.Clear();
			}
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0059D3E1 File Offset: 0x0059B5E1
		public bool IsInUse()
		{
			return this._inUse;
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0059D3E9 File Offset: 0x0059B5E9
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

		// Token: 0x04004F34 RID: 20276
		private Projectile _trackedProjectile;

		// Token: 0x04004F35 RID: 20277
		private int _lastTrackedType;

		// Token: 0x04004F36 RID: 20278
		private bool _inUse;
	}
}
