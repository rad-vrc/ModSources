using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x02000445 RID: 1093
	public class ArmorShaderDataSet
	{
		// Token: 0x06003602 RID: 13826 RVA: 0x0057A52C File Offset: 0x0057872C
		public T BindShader<T>(int itemId, T shaderData) where T : ArmorShaderData
		{
			Dictionary<int, int> shaderLookupDictionary = this._shaderLookupDictionary;
			int num = this._shaderDataCount + 1;
			this._shaderDataCount = num;
			shaderLookupDictionary[itemId] = num;
			this._shaderData.Add(shaderData);
			return shaderData;
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x0057A568 File Offset: 0x00578768
		public void Apply(int shaderId, Entity entity, DrawData? drawData = null)
		{
			if (shaderId >= 1 && shaderId <= this._shaderDataCount)
			{
				this._shaderData[shaderId - 1].Apply(entity, drawData);
				return;
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x0057A5A8 File Offset: 0x005787A8
		public void ApplySecondary(int shaderId, Entity entity, DrawData? drawData = null)
		{
			if (shaderId >= 1 && shaderId <= this._shaderDataCount)
			{
				this._shaderData[shaderId - 1].GetSecondaryShader(entity).Apply(entity, drawData);
				return;
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x0057A5F8 File Offset: 0x005787F8
		public ArmorShaderData GetShaderFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderData[this._shaderLookupDictionary[type] - 1];
			}
			return null;
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x0057A623 File Offset: 0x00578823
		public int GetShaderIdFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderLookupDictionary[type];
			}
			return 0;
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x0057A641 File Offset: 0x00578841
		public ArmorShaderData GetSecondaryShader(int id, Player player)
		{
			if (id != 0 && id <= this._shaderDataCount && this._shaderData[id - 1] != null)
			{
				return this._shaderData[id - 1].GetSecondaryShader(player);
			}
			return null;
		}

		// Token: 0x04004FFF RID: 20479
		protected internal List<ArmorShaderData> _shaderData = new List<ArmorShaderData>();

		// Token: 0x04005000 RID: 20480
		protected internal Dictionary<int, int> _shaderLookupDictionary = new Dictionary<int, int>();

		// Token: 0x04005001 RID: 20481
		protected internal int _shaderDataCount;
	}
}
