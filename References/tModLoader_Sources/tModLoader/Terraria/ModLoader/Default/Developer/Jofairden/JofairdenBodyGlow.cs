using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x0200034B RID: 843
	internal class JofairdenBodyGlow : JofairdenArmorGlowLayer
	{
		// Token: 0x06002F49 RID: 12105 RVA: 0x00533EAB File Offset: 0x005320AB
		public override DrawDataInfo GetData(PlayerDrawSet info)
		{
			if (this._glowTexture == null)
			{
				this._glowTexture = ModContent.Request<Texture2D>("ModLoader/Developer.Jofairden.Jofairden_Body_Body_Glow", 2);
			}
			return JofairdenArmorDrawLayer.GetBodyDrawDataInfo(info, this._glowTexture.Value);
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x00533ED7 File Offset: 0x005320D7
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.AfterParent(PlayerDrawLayers.Torso);
		}

		// Token: 0x04001C91 RID: 7313
		private Asset<Texture2D> _glowTexture;
	}
}
