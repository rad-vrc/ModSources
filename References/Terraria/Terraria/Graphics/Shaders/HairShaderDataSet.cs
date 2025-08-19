using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020000FF RID: 255
	public class HairShaderDataSet
	{
		// Token: 0x06001648 RID: 5704 RVA: 0x004C7E48 File Offset: 0x004C6048
		public T BindShader<T>(int itemId, T shaderData) where T : HairShaderData
		{
			if (this._shaderDataCount == 255)
			{
				throw new Exception("Too many shaders bound.");
			}
			Dictionary<int, short> shaderLookupDictionary = this._shaderLookupDictionary;
			byte b = this._shaderDataCount + 1;
			this._shaderDataCount = b;
			shaderLookupDictionary[itemId] = (short)b;
			this._shaderData.Add(shaderData);
			return shaderData;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x004C7E9D File Offset: 0x004C609D
		public void Apply(short shaderId, Player player, DrawData? drawData = null)
		{
			if (shaderId != 0 && shaderId <= (short)this._shaderDataCount)
			{
				this._shaderData[(int)(shaderId - 1)].Apply(player, drawData);
				return;
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x004C7EDB File Offset: 0x004C60DB
		public Color GetColor(short shaderId, Player player, Color lightColor)
		{
			if (shaderId != 0 && shaderId <= (short)this._shaderDataCount)
			{
				return this._shaderData[(int)(shaderId - 1)].GetColor(player, lightColor);
			}
			return new Color(lightColor.ToVector4() * player.hairColor.ToVector4());
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x004C7F1B File Offset: 0x004C611B
		public HairShaderData GetShaderFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderData[(int)(this._shaderLookupDictionary[type] - 1)];
			}
			return null;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x004C7F46 File Offset: 0x004C6146
		public short GetShaderIdFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderLookupDictionary[type];
			}
			return -1;
		}

		// Token: 0x04001341 RID: 4929
		protected List<HairShaderData> _shaderData = new List<HairShaderData>();

		// Token: 0x04001342 RID: 4930
		protected Dictionary<int, short> _shaderLookupDictionary = new Dictionary<int, short>();

		// Token: 0x04001343 RID: 4931
		protected byte _shaderDataCount;
	}
}
