using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004D3 RID: 1235
	public class WorldUIAnchor
	{
		// Token: 0x06003ACF RID: 15055 RVA: 0x005AD2D3 File Offset: 0x005AB4D3
		public WorldUIAnchor()
		{
			this.type = WorldUIAnchor.AnchorType.None;
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x005AD2F8 File Offset: 0x005AB4F8
		public WorldUIAnchor(Entity anchor)
		{
			this.type = WorldUIAnchor.AnchorType.Entity;
			this.entity = anchor;
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x005AD324 File Offset: 0x005AB524
		public WorldUIAnchor(Vector2 anchor)
		{
			this.type = WorldUIAnchor.AnchorType.Pos;
			this.pos = anchor;
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x005AD350 File Offset: 0x005AB550
		public WorldUIAnchor(int topLeftX, int topLeftY, int width, int height)
		{
			this.type = WorldUIAnchor.AnchorType.Tile;
			this.pos = new Vector2((float)topLeftX + (float)width / 2f, (float)topLeftY + (float)height / 2f) * 16f;
			this.size = new Vector2((float)width, (float)height) * 16f;
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x005AD3C8 File Offset: 0x005AB5C8
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

		// Token: 0x040054C9 RID: 21705
		public WorldUIAnchor.AnchorType type;

		// Token: 0x040054CA RID: 21706
		public Entity entity;

		// Token: 0x040054CB RID: 21707
		public Vector2 pos = Vector2.Zero;

		// Token: 0x040054CC RID: 21708
		public Vector2 size = Vector2.Zero;

		// Token: 0x02000BD7 RID: 3031
		public enum AnchorType
		{
			// Token: 0x04007759 RID: 30553
			Entity,
			// Token: 0x0400775A RID: 30554
			Tile,
			// Token: 0x0400775B RID: 30555
			Pos,
			// Token: 0x0400775C RID: 30556
			None
		}
	}
}
