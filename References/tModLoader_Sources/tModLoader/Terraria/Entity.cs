using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Entity
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000215D6 File Offset: 0x0001F7D6
		public virtual Vector2 VisualPosition
		{
			get
			{
				return this.position;
			}
		}

		/// <summary>
		/// The center position of this entity in world coordinates. Calculated from <see cref="F:Terraria.Entity.position" />, <see cref="F:Terraria.Entity.width" />, and <see cref="F:Terraria.Entity.height" />.
		/// </summary>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000215DE File Offset: 0x0001F7DE
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0002160F File Offset: 0x0001F80F
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0002163C File Offset: 0x0001F83C
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00021663 File Offset: 0x0001F863
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00021686 File Offset: 0x0001F886
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000216B5 File Offset: 0x0001F8B5
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

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000216E0 File Offset: 0x0001F8E0
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00021707 File Offset: 0x0001F907
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0002172A File Offset: 0x0001F92A
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00021732 File Offset: 0x0001F932
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0002173B File Offset: 0x0001F93B
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00021760 File Offset: 0x0001F960
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00021781 File Offset: 0x0001F981
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000217B0 File Offset: 0x0001F9B0
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000217DB File Offset: 0x0001F9DB
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00021800 File Offset: 0x0001FA00
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00021821 File Offset: 0x0001FA21
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0002184E File Offset: 0x0001FA4E
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00021877 File Offset: 0x0001FA77
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0002188C File Offset: 0x0001FA8C
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000218A8 File Offset: 0x0001FAA8
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000218D3 File Offset: 0x0001FAD3
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

		// Token: 0x060001E3 RID: 483 RVA: 0x00021906 File Offset: 0x0001FB06
		public float AngleTo(Vector2 Destination)
		{
			return (float)Math.Atan2((double)(Destination.Y - this.Center.Y), (double)(Destination.X - this.Center.X));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00021934 File Offset: 0x0001FB34
		public float AngleFrom(Vector2 Source)
		{
			return (float)Math.Atan2((double)(this.Center.Y - Source.Y), (double)(this.Center.X - Source.X));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00021962 File Offset: 0x0001FB62
		public float Distance(Vector2 Other)
		{
			return Vector2.Distance(this.Center, Other);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00021970 File Offset: 0x0001FB70
		public float DistanceSQ(Vector2 Other)
		{
			return Vector2.DistanceSquared(this.Center, Other);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0002197E File Offset: 0x0001FB7E
		public Vector2 DirectionTo(Vector2 Destination)
		{
			return Vector2.Normalize(Destination - this.Center);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00021991 File Offset: 0x0001FB91
		public Vector2 DirectionFrom(Vector2 Source)
		{
			return Vector2.Normalize(this.Center - Source);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000219A4 File Offset: 0x0001FBA4
		public bool WithinRange(Vector2 Target, float MaxRange)
		{
			return Vector2.DistanceSquared(this.Center, Target) <= MaxRange * MaxRange;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000219BA File Offset: 0x0001FBBA
		public IEntitySource GetSource_FromThis([Nullable(2)] string context = null)
		{
			return new EntitySource_Parent(this, context);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000219C3 File Offset: 0x0001FBC3
		public IEntitySource GetSource_FromAI([Nullable(2)] string context = null)
		{
			return new EntitySource_Parent(this, context);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000219CC File Offset: 0x0001FBCC
		public IEntitySource GetSource_DropAsItem([Nullable(2)] string context = null)
		{
			return new EntitySource_DropAsItem(this, context);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000219D5 File Offset: 0x0001FBD5
		public IEntitySource GetSource_Loot([Nullable(2)] string context = null)
		{
			return new EntitySource_Loot(this, context);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000219DE File Offset: 0x0001FBDE
		public IEntitySource GetSource_GiftOrReward([Nullable(2)] string context = null)
		{
			return new EntitySource_Gift(this, context);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000219E7 File Offset: 0x0001FBE7
		public IEntitySource GetSource_OnHit(Entity victim, [Nullable(2)] string context = null)
		{
			return new EntitySource_OnHit(this, victim, context);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000219F1 File Offset: 0x0001FBF1
		[NullableContext(2)]
		[return: Nullable(1)]
		public IEntitySource GetSource_OnHurt(Entity attacker, string context = null)
		{
			return new EntitySource_OnHurt(this, attacker, context);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000219FB File Offset: 0x0001FBFB
		public IEntitySource GetSource_Death([Nullable(2)] string context = null)
		{
			return new EntitySource_Death(this, context);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00021A04 File Offset: 0x0001FC04
		public IEntitySource GetSource_Misc(string context)
		{
			return new EntitySource_Misc(context);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00021A0C File Offset: 0x0001FC0C
		public IEntitySource GetSource_TileInteraction(int tileCoordsX, int tileCoordsY, [Nullable(2)] string context = null)
		{
			return new EntitySource_TileInteraction(this, tileCoordsX, tileCoordsY, context);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00021A17 File Offset: 0x0001FC17
		public IEntitySource GetSource_ReleaseEntity([Nullable(2)] string context = null)
		{
			return new EntitySource_Parent(this, context);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00021A20 File Offset: 0x0001FC20
		public IEntitySource GetSource_CatchEntity(Entity caughtEntity, [Nullable(2)] string context = null)
		{
			return new EntitySource_Caught(this, caughtEntity, context);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00021A2A File Offset: 0x0001FC2A
		[NullableContext(2)]
		public static IEntitySource GetSource_None()
		{
			return null;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00021A2D File Offset: 0x0001FC2D
		[return: Nullable(2)]
		public static IEntitySource InheritSource(Entity entity)
		{
			if (entity == null)
			{
				return Entity.GetSource_None();
			}
			return entity.GetSource_FromThis(null);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00021A3F File Offset: 0x0001FC3F
		public static IEntitySource GetSource_NaturalSpawn()
		{
			return new EntitySource_SpawnNPC(null);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00021A47 File Offset: 0x0001FC47
		public static IEntitySource GetSource_TownSpawn()
		{
			return new EntitySource_SpawnNPC(null);
		}

		/// <summary>
		/// The index of this Entity within its specific array. These arrays track the entities in the world.
		/// <br /> Item: unused
		/// <br /> Projectile: <see cref="F:Terraria.Main.projectile" />
		/// <br /> NPC: <see cref="F:Terraria.Main.npc" />
		/// <br /> Player: <see cref="F:Terraria.Main.player" />
		/// <para /> Note that Projectile.whoAmI is not consistent between clients in multiplayer for the same projectile.
		/// </summary>
		// Token: 0x040001B9 RID: 441
		public int whoAmI;

		/// <summary>
		/// If true, the Entity actually exists within the game world. Within the specific entity array, if active is false, the entity is junk data. Always check active if iterating over the entity array. Another option for iterating is to use <see cref="P:Terraria.Main.ActivePlayers" />, <see cref="P:Terraria.Main.ActiveNPCs" />, <see cref="P:Terraria.Main.ActiveProjectiles" />, or <see cref="P:Terraria.Main.ActiveItems" /> instead for simpler code.
		/// </summary>
		// Token: 0x040001BA RID: 442
		public bool active;

		// Token: 0x040001BB RID: 443
		internal long entityId;

		/// <summary>
		/// The position of this Entity in world coordinates. Note that this corresponds to the top left corner of the entity. Use <see cref="P:Terraria.Entity.Center" /> instead for logic that needs the position at the center of the entity.
		/// </summary>
		// Token: 0x040001BC RID: 444
		public Vector2 position;

		/// <summary>
		/// The velocity of this Entity in world coordinates per tick.
		/// </summary>
		// Token: 0x040001BD RID: 445
		public Vector2 velocity;

		/// <summary>
		/// The <see cref="F:Terraria.Entity.position" /> of this Entity during the previous tick. For projectiles with <see cref="F:Terraria.Projectile.extraUpdates" />, this will be the position during the previous extra update, not necessarily the position during the previous tick.
		/// </summary>
		// Token: 0x040001BE RID: 446
		public Vector2 oldPosition;

		/// <summary>
		/// The <see cref="F:Terraria.Entity.velocity" /> of this Entity during the previous tick. For projectiles with <see cref="F:Terraria.Projectile.extraUpdates" />, this will be the velocity during the previous extra update, not necessarily the velocity during the previous tick.
		/// </summary>
		// Token: 0x040001BF RID: 447
		public Vector2 oldVelocity;

		/// <summary>
		/// The <see cref="F:Terraria.Entity.direction" /> of this Entity during the previous tick. For projectiles with <see cref="F:Terraria.Projectile.extraUpdates" />, this will be the direction during the previous extra update, not necessarily the direction during the previous tick.
		/// </summary>
		// Token: 0x040001C0 RID: 448
		public int oldDirection;

		/// <summary>
		/// The direction this entity is facing. A value of 1 means the entity is facing to the right. -1 means facing to the left.
		/// </summary>
		// Token: 0x040001C1 RID: 449
		public int direction = 1;

		/// <summary>
		/// The width of this Entity's hitbox, in pixels.
		/// </summary>
		// Token: 0x040001C2 RID: 450
		public int width;

		/// <summary>
		/// The height of this Entity's hitbox, in pixels.
		/// </summary>
		// Token: 0x040001C3 RID: 451
		public int height;

		/// <summary>
		/// The Entity is currently in water.
		/// <br /> Projectile: Affects movement speed and some projectiles die when wet. <see cref="F:Terraria.Projectile.ignoreWater" /> prevents this.
		/// </summary>
		// Token: 0x040001C4 RID: 452
		public bool wet;

		// Token: 0x040001C5 RID: 453
		public bool shimmerWet;

		// Token: 0x040001C6 RID: 454
		public bool honeyWet;

		// Token: 0x040001C7 RID: 455
		public byte wetCount;

		// Token: 0x040001C8 RID: 456
		public bool lavaWet;
	}
}
