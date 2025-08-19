using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200071F RID: 1823
	public class NPCFollowState
	{
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060049EB RID: 18923 RVA: 0x0064E891 File Offset: 0x0064CA91
		public Vector2 BreadcrumbPosition
		{
			get
			{
				return this._floorBreadcrumb;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060049EC RID: 18924 RVA: 0x0064E899 File Offset: 0x0064CA99
		public bool IsFollowingPlayer
		{
			get
			{
				return this._playerIndexBeingFollowed != null;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060049ED RID: 18925 RVA: 0x0064E8A6 File Offset: 0x0064CAA6
		public Player PlayerBeingFollowed
		{
			get
			{
				if (this._playerIndexBeingFollowed != null)
				{
					return Main.player[this._playerIndexBeingFollowed.Value];
				}
				return null;
			}
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x0064E8C8 File Offset: 0x0064CAC8
		public void FollowPlayer(int playerIndex)
		{
			this._playerIndexBeingFollowed = new int?(playerIndex);
			this._floorBreadcrumb = Main.player[playerIndex].Bottom;
			this._npc.netUpdate = true;
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x0064E8F4 File Offset: 0x0064CAF4
		public void StopFollowing()
		{
			this._playerIndexBeingFollowed = null;
			this.MoveNPCBackHome();
			this._npc.netUpdate = true;
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x0064E914 File Offset: 0x0064CB14
		public void Clear(NPC npcToBelongTo)
		{
			this._npc = npcToBelongTo;
			this._playerIndexBeingFollowed = null;
			this._floorBreadcrumb = default(Vector2);
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x0064E935 File Offset: 0x0064CB35
		private bool ShouldSync()
		{
			return this._npc.isLikeATownNPC;
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x0064E944 File Offset: 0x0064CB44
		public void WriteTo(BinaryWriter writer)
		{
			int num = (this._playerIndexBeingFollowed != null) ? this._playerIndexBeingFollowed.Value : -1;
			writer.Write((short)num);
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x0064E978 File Offset: 0x0064CB78
		public void ReadFrom(BinaryReader reader)
		{
			short num = reader.ReadInt16();
			if (Main.player.IndexInRange((int)num))
			{
				this._playerIndexBeingFollowed = new int?((int)num);
			}
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x0064E9A8 File Offset: 0x0064CBA8
		private void MoveNPCBackHome()
		{
			this._npc.ai[0] = 20f;
			this._npc.ai[1] = 0f;
			this._npc.ai[2] = 0f;
			this._npc.ai[3] = 0f;
			this._npc.netUpdate = true;
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x0064EA0C File Offset: 0x0064CC0C
		public void Update()
		{
			if (this.IsFollowingPlayer)
			{
				Player playerBeingFollowed = this.PlayerBeingFollowed;
				if (!playerBeingFollowed.active || playerBeingFollowed.dead)
				{
					this.StopFollowing();
					return;
				}
				this.UpdateBreadcrumbs(playerBeingFollowed);
				Dust.QuickDust(this._floorBreadcrumb, Color.Red);
			}
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x0064EA58 File Offset: 0x0064CC58
		private void UpdateBreadcrumbs(Player player)
		{
			Vector2? vector = null;
			if (player.velocity.Y == 0f && player.gravDir == 1f)
			{
				vector = new Vector2?(player.Bottom);
			}
			int num = 8;
			if (vector != null && Vector2.Distance(vector.Value, this._floorBreadcrumb) >= (float)num)
			{
				this._floorBreadcrumb = vector.Value;
				this._npc.netUpdate = true;
			}
		}

		// Token: 0x04005F1A RID: 24346
		private NPC _npc;

		// Token: 0x04005F1B RID: 24347
		private int? _playerIndexBeingFollowed;

		// Token: 0x04005F1C RID: 24348
		private Vector2 _floorBreadcrumb;
	}
}
