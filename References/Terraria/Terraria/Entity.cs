using System;
using Microsoft.Xna.Framework;

namespace Terraria
{
	// Token: 0x0200001F RID: 31
	public abstract class Entity
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000EB88 File Offset: 0x0000CD88
		public virtual Vector2 VisualPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000EB90 File Offset: 0x0000CD90
		public float AngleTo(Vector2 Destination)
		{
			return (float)Math.Atan2((double)(Destination.Y - this.Center.Y), (double)(Destination.X - this.Center.X));
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000EBBE File Offset: 0x0000CDBE
		public float AngleFrom(Vector2 Source)
		{
			return (float)Math.Atan2((double)(this.Center.Y - Source.Y), (double)(this.Center.X - Source.X));
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		public float Distance(Vector2 Other)
		{
			return Vector2.Distance(this.Center, Other);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000EBFA File Offset: 0x0000CDFA
		public float DistanceSQ(Vector2 Other)
		{
			return Vector2.DistanceSquared(this.Center, Other);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000EC08 File Offset: 0x0000CE08
		public Vector2 DirectionTo(Vector2 Destination)
		{
			return Vector2.Normalize(Destination - this.Center);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000EC1B File Offset: 0x0000CE1B
		public Vector2 DirectionFrom(Vector2 Source)
		{
			return Vector2.Normalize(this.Center - Source);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000EC2E File Offset: 0x0000CE2E
		public bool WithinRange(Vector2 Target, float MaxRange)
		{
			return Vector2.DistanceSquared(this.Center, Target) <= MaxRange * MaxRange;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000EC44 File Offset: 0x0000CE44
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000EC75 File Offset: 0x0000CE75
		public Vector2 Center
		{
			get
			{
				return new Vector2(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2));
			}
			set
			{
				this.position = new Vector2(value.X - (float)(this.width / 2), value.Y - (float)(this.height / 2));
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000ECA2 File Offset: 0x0000CEA2
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000ECC9 File Offset: 0x0000CEC9
		public Vector2 Left
		{
			get
			{
				return new Vector2(this.position.X, this.position.Y + (float)(this.height / 2));
			}
			set
			{
				this.position = new Vector2(value.X, value.Y - (float)(this.height / 2));
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000ED1B File Offset: 0x0000CF1B
		public Vector2 Right
		{
			get
			{
				return new Vector2(this.position.X + (float)this.width, this.position.Y + (float)(this.height / 2));
			}
			set
			{
				this.position = new Vector2(value.X - (float)this.width, value.Y - (float)(this.height / 2));
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000ED46 File Offset: 0x0000CF46
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000ED6D File Offset: 0x0000CF6D
		public Vector2 Top
		{
			get
			{
				return new Vector2(this.position.X + (float)(this.width / 2), this.position.Y);
			}
			set
			{
				this.position = new Vector2(value.X - (float)(this.width / 2), value.Y);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000EB88 File Offset: 0x0000CD88
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000ED90 File Offset: 0x0000CF90
		public Vector2 TopLeft
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000ED99 File Offset: 0x0000CF99
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000EDBE File Offset: 0x0000CFBE
		public Vector2 TopRight
		{
			get
			{
				return new Vector2(this.position.X + (float)this.width, this.position.Y);
			}
			set
			{
				this.position = new Vector2(value.X - (float)this.width, value.Y);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000EDDF File Offset: 0x0000CFDF
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000EE0E File Offset: 0x0000D00E
		public Vector2 Bottom
		{
			get
			{
				return new Vector2(this.position.X + (float)(this.width / 2), this.position.Y + (float)this.height);
			}
			set
			{
				this.position = new Vector2(value.X - (float)(this.width / 2), value.Y - (float)this.height);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000EE39 File Offset: 0x0000D039
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000EE5E File Offset: 0x0000D05E
		public Vector2 BottomLeft
		{
			get
			{
				return new Vector2(this.position.X, this.position.Y + (float)this.height);
			}
			set
			{
				this.position = new Vector2(value.X, value.Y - (float)this.height);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000EE7F File Offset: 0x0000D07F
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000EEAC File Offset: 0x0000D0AC
		public Vector2 BottomRight
		{
			get
			{
				return new Vector2(this.position.X + (float)this.width, this.position.Y + (float)this.height);
			}
			set
			{
				this.position = new Vector2(value.X - (float)this.width, value.Y - (float)this.height);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000EED5 File Offset: 0x0000D0D5
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000EEEA File Offset: 0x0000D0EA
		public Vector2 Size
		{
			get
			{
				return new Vector2((float)this.width, (float)this.height);
			}
			set
			{
				this.width = (int)value.X;
				this.height = (int)value.Y;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000EF06 File Offset: 0x0000D106
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000EF31 File Offset: 0x0000D131
		public Rectangle Hitbox
		{
			get
			{
				return new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
			}
			set
			{
				this.position = new Vector2((float)value.X, (float)value.Y);
				this.width = value.Width;
				this.height = value.Height;
			}
		}

		// Token: 0x040000EF RID: 239
		public int whoAmI;

		// Token: 0x040000F0 RID: 240
		public bool active;

		// Token: 0x040000F1 RID: 241
		internal long entityId;

		// Token: 0x040000F2 RID: 242
		public Vector2 position;

		// Token: 0x040000F3 RID: 243
		public Vector2 velocity;

		// Token: 0x040000F4 RID: 244
		public Vector2 oldPosition;

		// Token: 0x040000F5 RID: 245
		public Vector2 oldVelocity;

		// Token: 0x040000F6 RID: 246
		public int oldDirection;

		// Token: 0x040000F7 RID: 247
		public int direction = 1;

		// Token: 0x040000F8 RID: 248
		public int width;

		// Token: 0x040000F9 RID: 249
		public int height;

		// Token: 0x040000FA RID: 250
		public bool wet;

		// Token: 0x040000FB RID: 251
		public bool shimmerWet;

		// Token: 0x040000FC RID: 252
		public bool honeyWet;

		// Token: 0x040000FD RID: 253
		public byte wetCount;

		// Token: 0x040000FE RID: 254
		public bool lavaWet;
	}
}
