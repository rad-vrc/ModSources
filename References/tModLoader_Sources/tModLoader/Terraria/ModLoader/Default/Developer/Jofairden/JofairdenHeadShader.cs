using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x0200034E RID: 846
	internal class JofairdenHeadShader : JofairdenArmorShaderLayer
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06002F55 RID: 12117 RVA: 0x00533FBC File Offset: 0x005321BC
		public override bool IsHeadLayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x00533FBF File Offset: 0x005321BF
		public override DrawDataInfo GetData(PlayerDrawSet info)
		{
			if (this._shaderTexture == null)
			{
				this._shaderTexture = ModContent.Request<Texture2D>("ModLoader/Developer.Jofairden.Jofairden_Head_Head_Shader", 2);
			}
			return JofairdenArmorDrawLayer.GetHeadDrawDataInfo(info, this._shaderTexture.Value);
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x00533FEB File Offset: 0x005321EB
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.BeforeParent(PlayerDrawLayers.Torso);
		}

		// Token: 0x04001C94 RID: 7316
		private Asset<Texture2D> _shaderTexture;
	}
}
