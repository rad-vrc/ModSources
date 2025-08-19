using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x0200047C RID: 1148
	public class CameraModifierStack
	{
		// Token: 0x06003795 RID: 14229 RVA: 0x00589BE2 File Offset: 0x00587DE2
		public void Add(ICameraModifier modifier)
		{
			this.RemoveIdenticalModifiers(modifier);
			this._modifiers.Add(modifier);
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x00589BF8 File Offset: 0x00587DF8
		private void RemoveIdenticalModifiers(ICameraModifier modifier)
		{
			string uniqueIdentity = modifier.UniqueIdentity;
			if (uniqueIdentity == null)
			{
				return;
			}
			for (int num = this._modifiers.Count - 1; num >= 0; num--)
			{
				if (this._modifiers[num].UniqueIdentity == uniqueIdentity)
				{
					this._modifiers.RemoveAt(num);
				}
			}
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x00589C50 File Offset: 0x00587E50
		public void ApplyTo(ref Vector2 cameraPosition)
		{
			CameraInfo cameraPosition2 = new CameraInfo(cameraPosition);
			this.ClearFinishedModifiers();
			for (int i = 0; i < this._modifiers.Count; i++)
			{
				this._modifiers[i].Update(ref cameraPosition2);
			}
			cameraPosition = cameraPosition2.CameraPosition;
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x00589CA8 File Offset: 0x00587EA8
		private void ClearFinishedModifiers()
		{
			for (int num = this._modifiers.Count - 1; num >= 0; num--)
			{
				if (this._modifiers[num].Finished)
				{
					this._modifiers.RemoveAt(num);
				}
			}
		}

		// Token: 0x04005149 RID: 20809
		private List<ICameraModifier> _modifiers = new List<ICameraModifier>();
	}
}
