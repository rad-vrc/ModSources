using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x0200047E RID: 1150
	public class PunchCameraModifier : ICameraModifier
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x00589CFF File Offset: 0x00587EFF
		// (set) Token: 0x0600379E RID: 14238 RVA: 0x00589D07 File Offset: 0x00587F07
		public string UniqueIdentity { get; private set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x00589D10 File Offset: 0x00587F10
		// (set) Token: 0x060037A0 RID: 14240 RVA: 0x00589D18 File Offset: 0x00587F18
		public bool Finished { get; private set; }

		// Token: 0x060037A1 RID: 14241 RVA: 0x00589D21 File Offset: 0x00587F21
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

		// Token: 0x060037A2 RID: 14242 RVA: 0x00589D60 File Offset: 0x00587F60
		public void Update(ref CameraInfo cameraInfo)
		{
			float num = (float)Math.Cos((double)((float)this._framesLasted / 60f * this._vibrationCyclesPerSecond * 6.2831855f));
			float num2 = Utils.Remap((float)this._framesLasted, 0f, (float)this._framesToLast, 1f, 0f, true);
			float num3 = Utils.Remap(Vector2.Distance(this._startPosition, cameraInfo.OriginalCameraCenter), 0f, this._distanceFalloff, 1f, 0f, true);
			if (this._distanceFalloff == -1f)
			{
				num3 = 1f;
			}
			cameraInfo.CameraPosition += this._direction * num * this._strength * num2 * num3;
			this._framesLasted++;
			if (this._framesLasted >= this._framesToLast)
			{
				this.Finished = true;
			}
		}

		// Token: 0x0400514A RID: 20810
		private int _framesToLast;

		// Token: 0x0400514B RID: 20811
		private Vector2 _startPosition;

		// Token: 0x0400514C RID: 20812
		private Vector2 _direction;

		// Token: 0x0400514D RID: 20813
		private float _distanceFalloff;

		// Token: 0x0400514E RID: 20814
		private float _strength;

		// Token: 0x0400514F RID: 20815
		private float _vibrationCyclesPerSecond;

		// Token: 0x04005150 RID: 20816
		private int _framesLasted;
	}
}
