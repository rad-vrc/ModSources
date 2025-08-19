using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameInput
{
	// Token: 0x02000486 RID: 1158
	public class SmartSelectGamepadPointer
	{
		// Token: 0x06003829 RID: 14377 RVA: 0x00592D90 File Offset: 0x00590F90
		public bool ShouldBeUsed()
		{
			return PlayerInput.UsingGamepad && Main.LocalPlayer.controlTorch && Main.SmartCursorIsUsed;
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x00592DAC File Offset: 0x00590FAC
		public void SmartSelectLookup_GetTargetTile(Player player, out int tX, out int tY)
		{
			tX = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
			tY = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
			if (player.gravDir == -1f)
			{
				tY = (int)((Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16f);
			}
			if (this.ShouldBeUsed())
			{
				Point point = this.GetPointerPosition().ToPoint();
				tX = (int)(((float)point.X + Main.screenPosition.X) / 16f);
				tY = (int)(((float)point.Y + Main.screenPosition.Y) / 16f);
				if (player.gravDir == -1f)
				{
					tY = (int)((Main.screenPosition.Y + (float)Main.screenHeight - (float)point.Y) / 16f);
				}
			}
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x00592E94 File Offset: 0x00591094
		public void UpdateSize(Vector2 size)
		{
			this._size = size;
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x00592E9D File Offset: 0x0059109D
		public void UpdateCenter(Vector2 center)
		{
			this._center = center;
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x00592EA8 File Offset: 0x005910A8
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

		// Token: 0x040051BA RID: 20922
		private Vector2 _size;

		// Token: 0x040051BB RID: 20923
		private Vector2 _center;

		// Token: 0x040051BC RID: 20924
		private Vector2 _distUniform = new Vector2(80f, 64f);
	}
}
