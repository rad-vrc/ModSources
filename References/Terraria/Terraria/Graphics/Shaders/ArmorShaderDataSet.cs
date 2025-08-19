using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020000FE RID: 254
	public class ArmorShaderDataSet
	{
		// Token: 0x06001641 RID: 5697 RVA: 0x004C7CE0 File Offset: 0x004C5EE0
		public T BindShader<T>(int itemId, T shaderData) where T : ArmorShaderData
		{
			Dictionary<int, int> shaderLookupDictionary = this._shaderLookupDictionary;
			int num = this._shaderDataCount + 1;
			this._shaderDataCount = num;
			shaderLookupDictionary[itemId] = num;
			this._shaderData.Add(shaderData);
			return shaderData;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x004C7D1C File Offset: 0x004C5F1C
		public void Apply(int shaderId, Entity entity, DrawData? drawData = null)
		{
			if (shaderId >= 1 && shaderId <= this._shaderDataCount)
			{
				this._shaderData[shaderId - 1].Apply(entity, drawData);
				return;
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x004C7D5C File Offset: 0x004C5F5C
		public void ApplySecondary(int shaderId, Entity entity, DrawData? drawData = null)
		{
			if (shaderId >= 1 && shaderId <= this._shaderDataCount)
			{
				this._shaderData[shaderId - 1].GetSecondaryShader(entity).Apply(entity, drawData);
				return;
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x004C7DAC File Offset: 0x004C5FAC
		public ArmorShaderData GetShaderFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderData[this._shaderLookupDictionary[type] - 1];
			}
			return null;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x004C7DD7 File Offset: 0x004C5FD7
		public int GetShaderIdFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderLookupDictionary[type];
			}
			return 0;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x004C7DF5 File Offset: 0x004C5FF5
		public ArmorShaderData GetSecondaryShader(int id, Player player)
		{
			if (id != 0 && id <= this._shaderDataCount && this._shaderData[id - 1] != null)
			{
				return this._shaderData[id - 1].GetSecondaryShader(player);
			}
			return null;
		}

		// Token: 0x0400133E RID: 4926
		protected List<ArmorShaderData> _shaderData = new List<ArmorShaderData>();

		// Token: 0x0400133F RID: 4927
		protected Dictionary<int, int> _shaderLookupDictionary = new Dictionary<int, int>();

		// Token: 0x04001340 RID: 4928
		protected int _shaderDataCount;
	}
}
