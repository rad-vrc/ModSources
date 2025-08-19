using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x02000130 RID: 304
	public class CameraModifierStack
	{
		// Token: 0x060017A1 RID: 6049 RVA: 0x004D4F20 File Offset: 0x004D3120
		public void Add(ICameraModifier modifier)
		{
			this.RemoveIdenticalModifiers(modifier);
			this._modifiers.Add(modifier);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x004D4F38 File Offset: 0x004D3138
		private void RemoveIdenticalModifiers(ICameraModifier modifier)
		{
			string uniqueIdentity = modifier.UniqueIdentity;
			if (uniqueIdentity == null)
			{
				return;
			}
			for (int i = this._modifiers.Count - 1; i >= 0; i--)
			{
				if (this._modifiers[i].UniqueIdentity == uniqueIdentity)
				{
					this._modifiers.RemoveAt(i);
				}
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x004D4F90 File Offset: 0x004D3190
		public void ApplyTo(ref Vector2 cameraPosition)
		{
			CameraInfo cameraInfo = new CameraInfo(cameraPosition);
			this.ClearFinishedModifiers();
			for (int i = 0; i < this._modifiers.Count; i++)
			{
				this._modifiers[i].Update(ref cameraInfo);
			}
			cameraPosition = cameraInfo.CameraPosition;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x004D4FE8 File Offset: 0x004D31E8
		private void ClearFinishedModifiers()
		{
			for (int i = this._modifiers.Count - 1; i >= 0; i--)
			{
				if (this._modifiers[i].Finished)
				{
					this._modifiers.RemoveAt(i);
				}
			}
		}

		// Token: 0x04001454 RID: 5204
		private List<ICameraModifier> _modifiers = new List<ICameraModifier>();
	}
}
