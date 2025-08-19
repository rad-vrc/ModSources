using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.UI
{
	// Token: 0x0200033D RID: 829
	public class WorldUIAnchor
	{
		// Token: 0x06002547 RID: 9543 RVA: 0x0056ACDE File Offset: 0x00568EDE
		public WorldUIAnchor()
		{
			this.type = WorldUIAnchor.AnchorType.None;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0056AD03 File Offset: 0x00568F03
		public WorldUIAnchor(Entity anchor)
		{
			this.type = WorldUIAnchor.AnchorType.Entity;
			this.entity = anchor;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x0056AD2F File Offset: 0x00568F2F
		public WorldUIAnchor(Vector2 anchor)
		{
			this.type = WorldUIAnchor.AnchorType.Pos;
			this.pos = anchor;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x0056AD5C File Offset: 0x00568F5C
		public WorldUIAnchor(int topLeftX, int topLeftY, int width, int height)
		{
			this.type = WorldUIAnchor.AnchorType.Tile;
			this.pos = new Vector2((float)topLeftX + (float)width / 2f, (float)topLeftY + (float)height / 2f) * 16f;
			this.size = new Vector2((float)width, (float)height) * 16f;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x0056ADD4 File Offset: 0x00568FD4
		public bool InRange(Vector2 target, float tileRangeX, float tileRangeY)
		{
			switch (this.type)
			{
			case WorldUIAnchor.AnchorType.Entity:
				return Math.Abs(target.X - this.entity.Center.X) <= tileRangeX * 16f + (float)this.entity.width / 2f && Math.Abs(target.Y - this.entity.Center.Y) <= tileRangeY * 16f + (float)this.entity.height / 2f;
			case WorldUIAnchor.AnchorType.Tile:
				return Math.Abs(target.X - this.pos.X) <= tileRangeX * 16f + this.size.X / 2f && Math.Abs(target.Y - this.pos.Y) <= tileRangeY * 16f + this.size.Y / 2f;
			case WorldUIAnchor.AnchorType.Pos:
				return Math.Abs(target.X - this.pos.X) <= tileRangeX * 16f && Math.Abs(target.Y - this.pos.Y) <= tileRangeY * 16f;
			default:
				return true;
			}
		}

		// Token: 0x040049BB RID: 18875
		public WorldUIAnchor.AnchorType type;

		// Token: 0x040049BC RID: 18876
		public Entity entity;

		// Token: 0x040049BD RID: 18877
		public Vector2 pos = Vector2.Zero;

		// Token: 0x040049BE RID: 18878
		public Vector2 size = Vector2.Zero;

		// Token: 0x02000720 RID: 1824
		public enum AnchorType
		{
			// Token: 0x0400634A RID: 25418
			Entity,
			// Token: 0x0400634B RID: 25419
			Tile,
			// Token: 0x0400634C RID: 25420
			Pos,
			// Token: 0x0400634D RID: 25421
			None
		}
	}
}
