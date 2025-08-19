using System;
using Microsoft.Xna.Framework;
using Terraria.Enums;

namespace Terraria.DataStructures
{
	// Token: 0x0200071D RID: 1821
	public struct NPCAimedTarget
	{
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x0064E6C0 File Offset: 0x0064C8C0
		public bool Invalid
		{
			get
			{
				return this.Type == NPCTargetType.None;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060049E5 RID: 18917 RVA: 0x0064E6CB File Offset: 0x0064C8CB
		public Vector2 Center
		{
			get
			{
				return this.Position + this.Size / 2f;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060049E6 RID: 18918 RVA: 0x0064E6E8 File Offset: 0x0064C8E8
		public Vector2 Size
		{
			get
			{
				return new Vector2((float)this.Width, (float)this.Height);
			}
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x0064E700 File Offset: 0x0064C900
		public NPCAimedTarget(NPC npc)
		{
			this.Type = NPCTargetType.NPC;
			this.Hitbox = npc.Hitbox;
			this.Width = npc.width;
			this.Height = npc.height;
			this.Position = npc.position;
			this.Velocity = npc.velocity;
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x0064E750 File Offset: 0x0064C950
		public NPCAimedTarget(Player player, bool ignoreTank = true)
		{
			this.Type = NPCTargetType.Player;
			this.Hitbox = player.Hitbox;
			this.Width = player.width;
			this.Height = player.height;
			this.Position = player.position;
			this.Velocity = player.velocity;
			if (!ignoreTank && player.tankPet > -1)
			{
				Projectile projectile = Main.projectile[player.tankPet];
				this.Type = NPCTargetType.PlayerTankPet;
				this.Hitbox = projectile.Hitbox;
				this.Width = projectile.width;
				this.Height = projectile.height;
				this.Position = projectile.position;
				this.Velocity = projectile.velocity;
			}
		}

		// Token: 0x04005F11 RID: 24337
		public NPCTargetType Type;

		// Token: 0x04005F12 RID: 24338
		public Rectangle Hitbox;

		// Token: 0x04005F13 RID: 24339
		public int Width;

		// Token: 0x04005F14 RID: 24340
		public int Height;

		// Token: 0x04005F15 RID: 24341
		public Vector2 Position;

		// Token: 0x04005F16 RID: 24342
		public Vector2 Velocity;
	}
}
