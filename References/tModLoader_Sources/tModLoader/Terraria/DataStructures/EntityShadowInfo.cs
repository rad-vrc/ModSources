using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x020006E0 RID: 1760
	public struct EntityShadowInfo
	{
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06004942 RID: 18754 RVA: 0x0064D69E File Offset: 0x0064B89E
		public Vector2 HeadgearOffset
		{
			get
			{
				return Main.OffsetsPlayerHeadgear[this.BodyFrameIndex];
			}
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x0064D6B0 File Offset: 0x0064B8B0
		public void CopyPlayer(Player player)
		{
			this.Position = player.position;
			this.Rotation = player.fullRotation;
			this.Origin = player.fullRotationOrigin;
			this.Direction = player.direction;
			this.GravityDirection = (int)player.gravDir;
			this.BodyFrameIndex = player.bodyFrame.Y / player.bodyFrame.Height;
		}

		// Token: 0x04005EA7 RID: 24231
		public Vector2 Position;

		// Token: 0x04005EA8 RID: 24232
		public float Rotation;

		// Token: 0x04005EA9 RID: 24233
		public Vector2 Origin;

		// Token: 0x04005EAA RID: 24234
		public int Direction;

		// Token: 0x04005EAB RID: 24235
		public int GravityDirection;

		// Token: 0x04005EAC RID: 24236
		public int BodyFrameIndex;
	}
}
