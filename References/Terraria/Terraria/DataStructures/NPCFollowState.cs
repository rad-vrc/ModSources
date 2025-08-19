using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000411 RID: 1041
	public class NPCFollowState
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x0059DC95 File Offset: 0x0059BE95
		public Vector2 BreadcrumbPosition
		{
			get
			{
				return this._floorBreadcrumb;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x0059DC9D File Offset: 0x0059BE9D
		public bool IsFollowingPlayer
		{
			get
			{
				return this._playerIndexBeingFollowed != null;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x0059DCAA File Offset: 0x0059BEAA
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

		// Token: 0x06002B3A RID: 11066 RVA: 0x0059DCCC File Offset: 0x0059BECC
		public void FollowPlayer(int playerIndex)
		{
			this._playerIndexBeingFollowed = new int?(playerIndex);
			this._floorBreadcrumb = Main.player[playerIndex].Bottom;
			this._npc.netUpdate = true;
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x0059DCF8 File Offset: 0x0059BEF8
		public void StopFollowing()
		{
			this._playerIndexBeingFollowed = null;
			this.MoveNPCBackHome();
			this._npc.netUpdate = true;
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x0059DD18 File Offset: 0x0059BF18
		public void Clear(NPC npcToBelongTo)
		{
			this._npc = npcToBelongTo;
			this._playerIndexBeingFollowed = null;
			this._floorBreadcrumb = default(Vector2);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x0059DD39 File Offset: 0x0059BF39
		private bool ShouldSync()
		{
			return this._npc.isLikeATownNPC;
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x0059DD48 File Offset: 0x0059BF48
		public void WriteTo(BinaryWriter writer)
		{
			int num = (this._playerIndexBeingFollowed != null) ? this._playerIndexBeingFollowed.Value : -1;
			writer.Write((short)num);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x0059DD7C File Offset: 0x0059BF7C
		public void ReadFrom(BinaryReader reader)
		{
			short num = reader.ReadInt16();
			if (Main.player.IndexInRange((int)num))
			{
				this._playerIndexBeingFollowed = new int?((int)num);
			}
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x0059DDAC File Offset: 0x0059BFAC
		private void MoveNPCBackHome()
		{
			this._npc.ai[0] = 20f;
			this._npc.ai[1] = 0f;
			this._npc.ai[2] = 0f;
			this._npc.ai[3] = 0f;
			this._npc.netUpdate = true;
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x0059DE10 File Offset: 0x0059C010
		public void Update()
		{
			if (!this.IsFollowingPlayer)
			{
				return;
			}
			Player playerBeingFollowed = this.PlayerBeingFollowed;
			if (!playerBeingFollowed.active || playerBeingFollowed.dead)
			{
				this.StopFollowing();
				return;
			}
			this.UpdateBreadcrumbs(playerBeingFollowed);
			Dust.QuickDust(this._floorBreadcrumb, Color.Red);
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0059DE5C File Offset: 0x0059C05C
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

		// Token: 0x04004F64 RID: 20324
		private NPC _npc;

		// Token: 0x04004F65 RID: 20325
		private int? _playerIndexBeingFollowed;

		// Token: 0x04004F66 RID: 20326
		private Vector2 _floorBreadcrumb;
	}
}
