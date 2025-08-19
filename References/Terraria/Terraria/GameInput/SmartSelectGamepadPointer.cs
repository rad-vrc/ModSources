﻿using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameInput
{
	// Token: 0x02000134 RID: 308
	public class SmartSelectGamepadPointer
	{
		// Token: 0x060017B0 RID: 6064 RVA: 0x004D51C3 File Offset: 0x004D33C3
		public bool ShouldBeUsed()
		{
			return PlayerInput.UsingGamepad && Main.LocalPlayer.controlTorch && Main.SmartCursorIsUsed;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x004D51E0 File Offset: 0x004D33E0
		public void SmartSelectLookup_GetTargetTile(Player player, out int tX, out int tY)
		{
			tX = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
			tY = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
			if (player.gravDir == -1f)
			{
				tY = (int)((Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16f);
			}
			if (!this.ShouldBeUsed())
			{
				return;
			}
			Point point = this.GetPointerPosition().ToPoint();
			tX = (int)(((float)point.X + Main.screenPosition.X) / 16f);
			tY = (int)(((float)point.Y + Main.screenPosition.Y) / 16f);
			if (player.gravDir == -1f)
			{
				tY = (int)((Main.screenPosition.Y + (float)Main.screenHeight - (float)point.Y) / 16f);
			}
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x004D52C9 File Offset: 0x004D34C9
		public void UpdateSize(Vector2 size)
		{
			this._size = size;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x004D52D2 File Offset: 0x004D34D2
		public void UpdateCenter(Vector2 center)
		{
			this._center = center;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x004D52DC File Offset: 0x004D34DC
		public Vector2 GetPointerPosition()
		{
			Vector2 vector = (new Vector2((float)Main.mouseX, (float)Main.mouseY) - this._center) / this._size;
			float num = Math.Abs(vector.X);
			if (num < Math.Abs(vector.Y))
			{
				num = Math.Abs(vector.Y);
			}
			if (num > 1f)
			{
				vector /= num;
			}
			vector *= Main.GameViewMatrix.Zoom.X;
			return vector * this._distUniform + this._center;
		}

		// Token: 0x04001461 RID: 5217
		private Vector2 _size;

		// Token: 0x04001462 RID: 5218
		private Vector2 _center;

		// Token: 0x04001463 RID: 5219
		private Vector2 _distUniform = new Vector2(80f, 64f);
	}
}
