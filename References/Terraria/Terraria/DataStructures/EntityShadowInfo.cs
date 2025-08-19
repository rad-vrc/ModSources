using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x020003FE RID: 1022
	public struct EntityShadowInfo
	{
		// Token: 0x06002AF6 RID: 10998 RVA: 0x0059D424 File Offset: 0x0059B624
		public void CopyPlayer(Player player)
		{
			this.Position = player.position;
			this.Rotation = player.fullRotation;
			this.Origin = player.fullRotationOrigin;
			this.Direction = player.direction;
			this.GravityDirection = (int)player.gravDir;
			this.BodyFrameIndex = player.bodyFrame.Y / player.bodyFrame.Height;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x0059D48B File Offset: 0x0059B68B
		public Vector2 HeadgearOffset
		{
			get
			{
				return Main.OffsetsPlayerHeadgear[this.BodyFrameIndex];
			}
		}

		// Token: 0x04004F37 RID: 20279
		public Vector2 Position;

		// Token: 0x04004F38 RID: 20280
		public float Rotation;

		// Token: 0x04004F39 RID: 20281
		public Vector2 Origin;

		// Token: 0x04004F3A RID: 20282
		public int Direction;

		// Token: 0x04004F3B RID: 20283
		public int GravityDirection;

		// Token: 0x04004F3C RID: 20284
		public int BodyFrameIndex;
	}
}
