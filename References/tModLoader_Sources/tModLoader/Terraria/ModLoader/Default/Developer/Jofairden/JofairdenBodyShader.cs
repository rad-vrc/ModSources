using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x0200034C RID: 844
	internal class JofairdenBodyShader : JofairdenArmorShaderLayer
	{
		// Token: 0x06002F4C RID: 12108 RVA: 0x00533EEB File Offset: 0x005320EB
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			return drawInfo.drawPlayer.body == ModContent.GetInstance<Jofairden_Body>().Item.bodySlot && base.GetDefaultVisibility(drawInfo);
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x00533F12 File Offset: 0x00532112
		public override DrawDataInfo GetData(PlayerDrawSet info)
		{
			if (this._shaderTexture == null)
			{
				this._shaderTexture = ModContent.Request<Texture2D>("ModLoader/Developer.Jofairden.Jofairden_Body_Body_Shader", 2);
			}
			return JofairdenArmorDrawLayer.GetBodyDrawDataInfo(info, this._shaderTexture.Value);
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x00533F3E File Offset: 0x0053213E
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.BeforeParent(PlayerDrawLayers.Torso);
		}

		// Token: 0x04001C92 RID: 7314
		private Asset<Texture2D> _shaderTexture;
	}
}
