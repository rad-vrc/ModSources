using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x02000448 RID: 1096
	public class HairShaderDataSet
	{
		// Token: 0x0600361B RID: 13851 RVA: 0x0057AAC4 File Offset: 0x00578CC4
		public T BindShader<T>(int itemId, T shaderData) where T : HairShaderData
		{
			Dictionary<int, int> shaderLookupDictionary = this._shaderLookupDictionary;
			int num = this._shaderDataCount + 1;
			this._shaderDataCount = num;
			shaderLookupDictionary[itemId] = num;
			this._reverseShaderLookupDictionary[this._shaderLookupDictionary[itemId]] = itemId;
			this._shaderData.Add(shaderData);
			return shaderData;
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x0057AB18 File Offset: 0x00578D18
		public void Apply(int shaderId, Player player, DrawData? drawData = null)
		{
			if (shaderId != 0 && shaderId <= this._shaderDataCount)
			{
				this._shaderData[shaderId - 1].Apply(player, drawData);
				return;
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x0057AB56 File Offset: 0x00578D56
		public Color GetColor(int shaderId, Player player, Color lightColor)
		{
			if (shaderId != 0 && shaderId <= this._shaderDataCount)
			{
				return this._shaderData[shaderId - 1].GetColor(player, lightColor);
			}
			return new Color(lightColor.ToVector4() * player.hairColor.ToVector4());
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x0057AB96 File Offset: 0x00578D96
		public HairShaderData GetShaderFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderData[this._shaderLookupDictionary[type] - 1];
			}
			return null;
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x0057ABC1 File Offset: 0x00578DC1
		public int GetShaderIdFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderLookupDictionary[type];
			}
			return -1;
		}

		// Token: 0x0400500C RID: 20492
		protected internal List<HairShaderData> _shaderData = new List<HairShaderData>();

		// Token: 0x0400500D RID: 20493
		protected internal Dictionary<int, int> _shaderLookupDictionary = new Dictionary<int, int>();

		// Token: 0x0400500E RID: 20494
		protected internal int _shaderDataCount;

		// Token: 0x0400500F RID: 20495
		internal Dictionary<int, int> _reverseShaderLookupDictionary = new Dictionary<int, int>();
	}
}
