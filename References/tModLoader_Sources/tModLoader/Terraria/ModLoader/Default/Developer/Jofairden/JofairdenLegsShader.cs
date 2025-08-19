using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x02000350 RID: 848
	internal class JofairdenLegsShader : JofairdenArmorShaderLayer
	{
		// Token: 0x06002F5D RID: 12125 RVA: 0x00534066 File Offset: 0x00532266
		public override DrawDataInfo GetData(PlayerDrawSet info)
		{
			if (this._shaderTexture == null)
			{
				this._shaderTexture = ModContent.Request<Texture2D>("ModLoader/Developer.Jofairden.Jofairden_Legs_Legs_Shader", 2);
			}
			return JofairdenArmorDrawLayer.GetLegDrawDataInfo(info, this._shaderTexture.Value);
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x00534092 File Offset: 0x00532292
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.BeforeParent(PlayerDrawLayers.Leggings);
		}

		// Token: 0x04001C96 RID: 7318
		private Asset<Texture2D> _shaderTexture;
	}
}
