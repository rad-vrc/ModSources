using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x0200034D RID: 845
	internal class JofairdenHeadGlow : JofairdenArmorGlowLayer
	{
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x00533F52 File Offset: 0x00532152
		public override bool IsHeadLayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x00533F55 File Offset: 0x00532155
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			return drawInfo.drawPlayer.head == ModContent.GetInstance<Jofairden_Head>().Item.headSlot && base.GetDefaultVisibility(drawInfo);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x00533F7C File Offset: 0x0053217C
		public override DrawDataInfo GetData(PlayerDrawSet info)
		{
			if (this._glowTexture == null)
			{
				this._glowTexture = ModContent.Request<Texture2D>("ModLoader/Developer.Jofairden.Jofairden_Head_Head_Glow", 2);
			}
			return JofairdenArmorDrawLayer.GetHeadDrawDataInfo(info, this._glowTexture.Value);
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x00533FA8 File Offset: 0x005321A8
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.AfterParent(PlayerDrawLayers.Head);
		}

		// Token: 0x04001C93 RID: 7315
		private Asset<Texture2D> _glowTexture;
	}
}
