using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x0200048E RID: 1166
	public class BackgroundChangeFlashInfo
	{
		// Token: 0x060038D5 RID: 14549 RVA: 0x00594A64 File Offset: 0x00592C64
		public void UpdateCache()
		{
			this.UpdateVariation(0, WorldGen.treeBG1);
			this.UpdateVariation(1, WorldGen.treeBG2);
			this.UpdateVariation(2, WorldGen.treeBG3);
			this.UpdateVariation(3, WorldGen.treeBG4);
			this.UpdateVariation(4, WorldGen.corruptBG);
			this.UpdateVariation(5, WorldGen.jungleBG);
			this.UpdateVariation(6, WorldGen.snowBG);
			this.UpdateVariation(7, WorldGen.hallowBG);
			this.UpdateVariation(8, WorldGen.crimsonBG);
			this.UpdateVariation(9, WorldGen.desertBG);
			this.UpdateVariation(10, WorldGen.oceanBG);
			this.UpdateVariation(11, WorldGen.mushroomBG);
			this.UpdateVariation(12, WorldGen.underworldBG);
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x00594B11 File Offset: 0x00592D11
		private void UpdateVariation(int areaId, int newVariationValue)
		{
			int num = this._variations[areaId];
			this._variations[areaId] = newVariationValue;
			if (num != newVariationValue)
			{
				this.ValueChanged(areaId);
			}
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x00594B2E File Offset: 0x00592D2E
		private void ValueChanged(int areaId)
		{
			if (!Main.gameMenu)
			{
				this._flashPower[areaId] = 1f;
			}
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x00594B44 File Offset: 0x00592D44
		public void UpdateFlashValues()
		{
			for (int i = 0; i < this._flashPower.Length; i++)
			{
				this._flashPower[i] = MathHelper.Clamp(this._flashPower[i] - 0.05f, 0f, 1f);
			}
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x00594B89 File Offset: 0x00592D89
		public float GetFlashPower(int areaId)
		{
			return this._flashPower[areaId];
		}

		// Token: 0x04005210 RID: 21008
		private int[] _variations = new int[TreeTopsInfo.AreaId.Count];

		// Token: 0x04005211 RID: 21009
		private float[] _flashPower = new float[TreeTopsInfo.AreaId.Count];
	}
}
