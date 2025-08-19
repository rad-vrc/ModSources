using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.GameContent
{
	// Token: 0x020004A8 RID: 1192
	public class PlayerHeadDrawRenderTargetContent : AnOutlinedDrawRenderTargetContent
	{
		// Token: 0x06003986 RID: 14726 RVA: 0x005985DD File Offset: 0x005967DD
		public void UsePlayer(Player player)
		{
			this._player = player;
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x005985E8 File Offset: 0x005967E8
		internal override void DrawTheContent(SpriteBatch spriteBatch)
		{
			if (this._player != null && !this._player.ShouldNotDraw)
			{
				Main.PlayerRenderer.DrawPlayerHead(Main.Camera, this._player, new Vector2((float)this.width * 0.5f, (float)this.height * 0.5f), 1f, 1f, default(Color));
			}
		}

		// Token: 0x04005261 RID: 21089
		private Player _player;

		// Token: 0x04005262 RID: 21090
		private readonly List<DrawData> _drawData = new List<DrawData>();

		// Token: 0x04005263 RID: 21091
		private readonly List<int> _dust = new List<int>();

		// Token: 0x04005264 RID: 21092
		private readonly List<int> _gore = new List<int>();
	}
}
