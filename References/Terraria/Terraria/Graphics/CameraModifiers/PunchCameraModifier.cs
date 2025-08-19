using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x02000133 RID: 307
	public class PunchCameraModifier : ICameraModifier
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x004D5074 File Offset: 0x004D3274
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x004D507C File Offset: 0x004D327C
		public string UniqueIdentity { get; private set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x004D5085 File Offset: 0x004D3285
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x004D508D File Offset: 0x004D328D
		public bool Finished { get; private set; }

		// Token: 0x060017AE RID: 6062 RVA: 0x004D5096 File Offset: 0x004D3296
		public PunchCameraModifier(Vector2 startPosition, Vector2 direction, float strength, float vibrationCyclesPerSecond, int frames, float distanceFalloff = -1f, string uniqueIdentity = null)
		{
			this._startPosition = startPosition;
			this._direction = direction;
			this._strength = strength;
			this._vibrationCyclesPerSecond = vibrationCyclesPerSecond;
			this._framesToLast = frames;
			this._distanceFalloff = distanceFalloff;
			this.UniqueIdentity = uniqueIdentity;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x004D50D4 File Offset: 0x004D32D4
		public void Update(ref CameraInfo cameraInfo)
		{
			float scaleFactor = (float)Math.Cos((double)((float)this._framesLasted / 60f * this._vibrationCyclesPerSecond * 6.2831855f));
			float scaleFactor2 = Utils.Remap((float)this._framesLasted, 0f, (float)this._framesToLast, 1f, 0f, true);
			float scaleFactor3 = Utils.Remap(Vector2.Distance(this._startPosition, cameraInfo.OriginalCameraCenter), 0f, this._distanceFalloff, 1f, 0f, true);
			if (this._distanceFalloff == -1f)
			{
				scaleFactor3 = 1f;
			}
			cameraInfo.CameraPosition += this._direction * scaleFactor * this._strength * scaleFactor2 * scaleFactor3;
			this._framesLasted++;
			if (this._framesLasted >= this._framesToLast)
			{
				this.Finished = true;
			}
		}

		// Token: 0x04001458 RID: 5208
		private int _framesToLast;

		// Token: 0x04001459 RID: 5209
		private Vector2 _startPosition;

		// Token: 0x0400145A RID: 5210
		private Vector2 _direction;

		// Token: 0x0400145B RID: 5211
		private float _distanceFalloff;

		// Token: 0x0400145C RID: 5212
		private float _strength;

		// Token: 0x0400145D RID: 5213
		private float _vibrationCyclesPerSecond;

		// Token: 0x0400145E RID: 5214
		private int _framesLasted;
	}
}
