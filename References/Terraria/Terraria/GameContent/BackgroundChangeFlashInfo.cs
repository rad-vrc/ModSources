using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x020001F2 RID: 498
	public class BackgroundChangeFlashInfo
	{
		// Token: 0x06001CE9 RID: 7401 RVA: 0x004FE624 File Offset: 0x004FC824
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

		// Token: 0x06001CEA RID: 7402 RVA: 0x004FE6D1 File Offset: 0x004FC8D1
		private void UpdateVariation(int areaId, int newVariationValue)
		{
			int num = this._variations[areaId];
			this._variations[areaId] = newVariationValue;
			if (num != newVariationValue)
			{
				this.ValueChanged(areaId);
			}
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x004FE6EE File Offset: 0x004FC8EE
		private void ValueChanged(int areaId)
		{
			if (Main.gameMenu)
			{
				return;
			}
			this._flashPower[areaId] = 1f;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x004FE708 File Offset: 0x004FC908
		public void UpdateFlashValues()
		{
			for (int i = 0; i < this._flashPower.Length; i++)
			{
				this._flashPower[i] = MathHelper.Clamp(this._flashPower[i] - 0.05f, 0f, 1f);
			}
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x004FE74D File Offset: 0x004FC94D
		public float GetFlashPower(int areaId)
		{
			return this._flashPower[areaId];
		}

		// Token: 0x040043DE RID: 17374
		private int[] _variations = new int[TreeTopsInfo.AreaId.Count];

		// Token: 0x040043DF RID: 17375
		private float[] _flashPower = new float[TreeTopsInfo.AreaId.Count];
	}
}
