using System;
using Microsoft.Xna.Framework;
using Terraria.Enums;

namespace Terraria.DataStructures
{
	// Token: 0x0200044D RID: 1101
	public struct NPCAimedTarget
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x005A036C File Offset: 0x0059E56C
		public bool Invalid
		{
			get
			{
				return this.Type == NPCTargetType.None;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x005A0377 File Offset: 0x0059E577
		public Vector2 Center
		{
			get
			{
				return this.Position + this.Size / 2f;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x005A0394 File Offset: 0x0059E594
		public Vector2 Size
		{
			get
			{
				return new Vector2((float)this.Width, (float)this.Height);
			}
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x005A03AC File Offset: 0x0059E5AC
		public NPCAimedTarget(NPC npc)
		{
			this.Type = NPCTargetType.NPC;
			this.Hitbox = npc.Hitbox;
			this.Width = npc.width;
			this.Height = npc.height;
			this.Position = npc.position;
			this.Velocity = npc.velocity;
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x005A03FC File Offset: 0x0059E5FC
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

		// Token: 0x0400501B RID: 20507
		public NPCTargetType Type;

		// Token: 0x0400501C RID: 20508
		public Rectangle Hitbox;

		// Token: 0x0400501D RID: 20509
		public int Width;

		// Token: 0x0400501E RID: 20510
		public int Height;

		// Token: 0x0400501F RID: 20511
		public Vector2 Position;

		// Token: 0x04005020 RID: 20512
		public Vector2 Velocity;
	}
}
