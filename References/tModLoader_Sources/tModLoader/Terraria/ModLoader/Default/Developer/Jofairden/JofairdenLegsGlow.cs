using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x0200034F RID: 847
	internal class JofairdenLegsGlow : JofairdenArmorGlowLayer
	{
		// Token: 0x06002F59 RID: 12121 RVA: 0x00533FFF File Offset: 0x005321FF
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			return drawInfo.drawPlayer.legs == ModContent.GetInstance<Jofairden_Legs>().Item.legSlot && base.GetDefaultVisibility(drawInfo);
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x00534026 File Offset: 0x00532226
		public override DrawDataInfo GetData(PlayerDrawSet info)
		{
			if (this._glowTexture == null)
			{
				this._glowTexture = ModContent.Request<Texture2D>("ModLoader/Developer.Jofairden.Jofairden_Legs_Legs_Glow", 2);
			}
			return JofairdenArmorDrawLayer.GetLegDrawDataInfo(info, this._glowTexture.Value);
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x00534052 File Offset: 0x00532252
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.AfterParent(PlayerDrawLayers.Leggings);
		}

		// Token: 0x04001C95 RID: 7317
		private Asset<Texture2D> _glowTexture;
	}
}
