using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.GameContent
{
	// Token: 0x020001D6 RID: 470
	public class PlayerHeadDrawRenderTargetContent : AnOutlinedDrawRenderTargetContent
	{
		// Token: 0x06001C1C RID: 7196 RVA: 0x004F1786 File Offset: 0x004EF986
		public void UsePlayer(Player player)
		{
			this._player = player;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x004F1790 File Offset: 0x004EF990
		internal override void DrawTheContent(SpriteBatch spriteBatch)
		{
			if (this._player == null)
			{
				return;
			}
			if (this._player.ShouldNotDraw)
			{
				return;
			}
			this._drawData.Clear();
			this._dust.Clear();
			this._gore.Clear();
			PlayerDrawHeadSet playerDrawHeadSet = default(PlayerDrawHeadSet);
			playerDrawHeadSet.BoringSetup(this._player, this._drawData, this._dust, this._gore, (float)(this.width / 2), (float)(this.height / 2), 1f, 1f);
			PlayerDrawHeadLayers.DrawPlayer_00_BackHelmet(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_01_FaceSkin(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_02_DrawArmorWithFullHair(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_03_HelmetHair(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_04_HatsWithFullHair(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_05_TallHats(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_06_NormalHats(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_07_JustHair(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_08_FaceAcc(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_RenderAllLayers(ref playerDrawHeadSet);
		}

		// Token: 0x0400436C RID: 17260
		private Player _player;

		// Token: 0x0400436D RID: 17261
		private readonly List<DrawData> _drawData = new List<DrawData>();

		// Token: 0x0400436E RID: 17262
		private readonly List<int> _dust = new List<int>();

		// Token: 0x0400436F RID: 17263
		private readonly List<int> _gore = new List<int>();
	}
}
