using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.Utilities
{
	// Token: 0x0200008F RID: 143
	public static class NPCUtils
	{
		// Token: 0x0600147C RID: 5244 RVA: 0x004A2375 File Offset: 0x004A0575
		public static NPCUtils.TargetSearchResults SearchForTarget(Vector2 position, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
		{
			return NPCUtils.SearchForTarget(null, position, flags, playerFilter, npcFilter);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x004A2381 File Offset: 0x004A0581
		public static NPCUtils.TargetSearchResults SearchForTarget(NPC searcher, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
		{
			return NPCUtils.SearchForTarget(searcher, searcher.Center, flags, playerFilter, npcFilter);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x004A2394 File Offset: 0x004A0594
		public static NPCUtils.TargetSearchResults SearchForTarget(NPC searcher, Vector2 position, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
		{
			float num = float.MaxValue;
			int nearestNPCIndex = -1;
			float num2 = float.MaxValue;
			float nearestTankDistance = float.MaxValue;
			int nearestTankIndex = -1;
			NPCUtils.TargetType tankType = NPCUtils.TargetType.Player;
			if (flags.HasFlag(NPCUtils.TargetSearchFlag.NPCs))
			{
				for (int i = 0; i < 200; i++)
				{
					NPC nPC = Main.npc[i];
					if (nPC.active && nPC.whoAmI != searcher.whoAmI && (npcFilter == null || npcFilter(nPC)))
					{
						float num3 = Vector2.DistanceSquared(position, nPC.Center);
						if (num3 < num)
						{
							nearestNPCIndex = i;
							num = num3;
						}
					}
				}
			}
			if (flags.HasFlag(NPCUtils.TargetSearchFlag.Players))
			{
				for (int j = 0; j < 255; j++)
				{
					Player player = Main.player[j];
					if (player.active && !player.dead && !player.ghost && (playerFilter == null || playerFilter(player)))
					{
						float num4 = Vector2.Distance(position, player.Center);
						float num5 = num4 - (float)player.aggro;
						bool flag = searcher != null && player.npcTypeNoAggro[searcher.type];
						if (searcher != null && flag && searcher.direction == 0)
						{
							num5 += 1000f;
						}
						if (num5 < num2)
						{
							nearestTankIndex = j;
							num2 = num5;
							nearestTankDistance = num4;
							tankType = NPCUtils.TargetType.Player;
						}
						if (player.tankPet >= 0 && !flag)
						{
							Vector2 center = Main.projectile[player.tankPet].Center;
							num4 = Vector2.Distance(position, center);
							num5 = num4 - 200f;
							if (num5 < num2 && num5 < 200f && Collision.CanHit(position, 0, 0, center, 0, 0))
							{
								nearestTankIndex = j;
								num2 = num5;
								nearestTankDistance = num4;
								tankType = NPCUtils.TargetType.TankPet;
							}
						}
					}
				}
			}
			return new NPCUtils.TargetSearchResults(searcher, nearestNPCIndex, (float)Math.Sqrt((double)num), nearestTankIndex, nearestTankDistance, num2, tankType);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x004A2570 File Offset: 0x004A0770
		public static void TargetClosestOldOnesInvasion(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All;
			NPCUtils.SearchFilter<Player> playerFilter = NPCUtils.SearchFilters.OnlyPlayersInCertainDistance(searcher.Center, 200f);
			NPCUtils.SearchFilter<NPC> npcFilter;
			if ((npcFilter = NPCUtils.<>O.<0>__OnlyCrystal) == null)
			{
				npcFilter = (NPCUtils.<>O.<0>__OnlyCrystal = new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.OnlyCrystal));
			}
			NPCUtils.TargetSearchResults searchResults = NPCUtils.SearchForTarget(searcher, flags, playerFilter, npcFilter);
			if (searchResults.FoundTarget)
			{
				searcher.target = searchResults.NearestTargetIndex;
				searcher.targetRect = searchResults.NearestTargetHitbox;
				if (searcher.ShouldFaceTarget(ref searchResults, null) && faceTarget)
				{
					searcher.FaceTarget();
				}
			}
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x004A25F0 File Offset: 0x004A07F0
		public static void TargetClosestNonBees(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All;
			NPCUtils.SearchFilter<Player> playerFilter = null;
			NPCUtils.SearchFilter<NPC> npcFilter;
			if ((npcFilter = NPCUtils.<>O.<1>__NonBeeNPCs) == null)
			{
				npcFilter = (NPCUtils.<>O.<1>__NonBeeNPCs = new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.NonBeeNPCs));
			}
			NPCUtils.TargetSearchResults searchResults = NPCUtils.SearchForTarget(searcher, flags, playerFilter, npcFilter);
			if (searchResults.FoundTarget)
			{
				searcher.target = searchResults.NearestTargetIndex;
				searcher.targetRect = searchResults.NearestTargetHitbox;
				if (searcher.ShouldFaceTarget(ref searchResults, null) && faceTarget)
				{
					searcher.FaceTarget();
				}
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x004A2660 File Offset: 0x004A0860
		public static void TargetClosestDownwindFromNPC(NPC searcher, float distanceMaxX, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchResults searchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.Players, NPCUtils.SearchFilters.DownwindFromNPC(searcher, distanceMaxX), null);
			if (searchResults.FoundTarget)
			{
				searcher.target = searchResults.NearestTargetIndex;
				searcher.targetRect = searchResults.NearestTargetHitbox;
				if (searcher.ShouldFaceTarget(ref searchResults, null) && faceTarget)
				{
					searcher.FaceTarget();
				}
			}
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x004A26BB File Offset: 0x004A08BB
		public static void TargetClosestCommon(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			searcher.TargetClosest(faceTarget);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x004A26C4 File Offset: 0x004A08C4
		public static void TargetClosestBetsy(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All;
			NPCUtils.SearchFilter<Player> playerFilter = null;
			NPCUtils.SearchFilter<NPC> npcFilter;
			if ((npcFilter = NPCUtils.<>O.<0>__OnlyCrystal) == null)
			{
				npcFilter = (NPCUtils.<>O.<0>__OnlyCrystal = new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.OnlyCrystal));
			}
			NPCUtils.TargetSearchResults searchResults = NPCUtils.SearchForTarget(searcher, flags, playerFilter, npcFilter);
			if (searchResults.FoundTarget)
			{
				NPCUtils.TargetType value = searchResults.NearestTargetType;
				if (searchResults.FoundTank && !searchResults.NearestTankOwner.dead)
				{
					value = NPCUtils.TargetType.Player;
				}
				searcher.target = searchResults.NearestTargetIndex;
				searcher.targetRect = searchResults.NearestTargetHitbox;
				if (searcher.ShouldFaceTarget(ref searchResults, new NPCUtils.TargetType?(value)) && faceTarget)
				{
					searcher.FaceTarget();
				}
			}
		}

		// Token: 0x0200085B RID: 2139
		// (Invoke) Token: 0x0600511D RID: 20765
		public delegate bool SearchFilter<T>(T entity) where T : Entity;

		// Token: 0x0200085C RID: 2140
		// (Invoke) Token: 0x06005121 RID: 20769
		public delegate void NPCTargetingMethod(NPC searcher, bool faceTarget, Vector2? checkPosition);

		// Token: 0x0200085D RID: 2141
		public static class SearchFilters
		{
			// Token: 0x06005124 RID: 20772 RVA: 0x00695F33 File Offset: 0x00694133
			public static bool OnlyCrystal(NPC npc)
			{
				return npc.type == 548 && !npc.dontTakeDamageFromHostiles;
			}

			// Token: 0x06005125 RID: 20773 RVA: 0x00695F4D File Offset: 0x0069414D
			public static NPCUtils.SearchFilter<Player> OnlyPlayersInCertainDistance(Vector2 position, float maxDistance)
			{
				return (Player player) => player.Distance(position) <= maxDistance;
			}

			// Token: 0x06005126 RID: 20774 RVA: 0x00695F6D File Offset: 0x0069416D
			public static bool NonBeeNPCs(NPC npc)
			{
				return npc.type != 211 && npc.type != 210 && npc.type != 222 && npc.CanBeChasedBy(null, false);
			}

			// Token: 0x06005127 RID: 20775 RVA: 0x00695FA0 File Offset: 0x006941A0
			public static NPCUtils.SearchFilter<Player> DownwindFromNPC(NPC npc, float maxDistanceX)
			{
				return delegate(Player player)
				{
					float windSpeedCurrent = Main.windSpeedCurrent;
					float num = player.Center.X - npc.Center.X;
					float num2 = Math.Abs(num);
					float num3 = Math.Abs(player.Center.Y - npc.Center.Y);
					return player.active && !player.dead && num3 < 100f && num2 < maxDistanceX && ((num > 0f && windSpeedCurrent > 0f) || (num < 0f && windSpeedCurrent < 0f));
				};
			}
		}

		// Token: 0x0200085E RID: 2142
		public enum TargetType
		{
			// Token: 0x040068F8 RID: 26872
			None,
			// Token: 0x040068F9 RID: 26873
			NPC,
			// Token: 0x040068FA RID: 26874
			Player,
			// Token: 0x040068FB RID: 26875
			TankPet
		}

		// Token: 0x0200085F RID: 2143
		public struct TargetSearchResults
		{
			// Token: 0x170008AF RID: 2223
			// (get) Token: 0x06005128 RID: 20776 RVA: 0x00695FC0 File Offset: 0x006941C0
			public int NearestTargetIndex
			{
				get
				{
					NPCUtils.TargetType nearestTargetType = this._nearestTargetType;
					if (nearestTargetType == NPCUtils.TargetType.NPC)
					{
						return this.NearestNPC.WhoAmIToTargettingIndex;
					}
					if (nearestTargetType - NPCUtils.TargetType.Player <= 1)
					{
						return this._nearestTankIndex;
					}
					return -1;
				}
			}

			// Token: 0x170008B0 RID: 2224
			// (get) Token: 0x06005129 RID: 20777 RVA: 0x00695FF4 File Offset: 0x006941F4
			public Rectangle NearestTargetHitbox
			{
				get
				{
					switch (this._nearestTargetType)
					{
					case NPCUtils.TargetType.NPC:
						return this.NearestNPC.Hitbox;
					case NPCUtils.TargetType.Player:
						return this.NearestTankOwner.Hitbox;
					case NPCUtils.TargetType.TankPet:
						return Main.projectile[this.NearestTankOwner.tankPet].Hitbox;
					default:
						return Rectangle.Empty;
					}
				}
			}

			// Token: 0x170008B1 RID: 2225
			// (get) Token: 0x0600512A RID: 20778 RVA: 0x00696052 File Offset: 0x00694252
			public NPCUtils.TargetType NearestTargetType
			{
				get
				{
					return this._nearestTargetType;
				}
			}

			// Token: 0x170008B2 RID: 2226
			// (get) Token: 0x0600512B RID: 20779 RVA: 0x0069605A File Offset: 0x0069425A
			public bool FoundTarget
			{
				get
				{
					return this._nearestTargetType > NPCUtils.TargetType.None;
				}
			}

			// Token: 0x170008B3 RID: 2227
			// (get) Token: 0x0600512C RID: 20780 RVA: 0x00696065 File Offset: 0x00694265
			public NPC NearestNPC
			{
				get
				{
					if (this._nearestNPCIndex != -1)
					{
						return Main.npc[this._nearestNPCIndex];
					}
					return null;
				}
			}

			// Token: 0x170008B4 RID: 2228
			// (get) Token: 0x0600512D RID: 20781 RVA: 0x0069607E File Offset: 0x0069427E
			public bool FoundNPC
			{
				get
				{
					return this._nearestNPCIndex != -1;
				}
			}

			// Token: 0x170008B5 RID: 2229
			// (get) Token: 0x0600512E RID: 20782 RVA: 0x0069608C File Offset: 0x0069428C
			public int NearestNPCIndex
			{
				get
				{
					return this._nearestNPCIndex;
				}
			}

			// Token: 0x170008B6 RID: 2230
			// (get) Token: 0x0600512F RID: 20783 RVA: 0x00696094 File Offset: 0x00694294
			public float NearestNPCDistance
			{
				get
				{
					return this._nearestNPCDistance;
				}
			}

			// Token: 0x170008B7 RID: 2231
			// (get) Token: 0x06005130 RID: 20784 RVA: 0x0069609C File Offset: 0x0069429C
			public Player NearestTankOwner
			{
				get
				{
					if (this._nearestTankIndex != -1)
					{
						return Main.player[this._nearestTankIndex];
					}
					return null;
				}
			}

			// Token: 0x170008B8 RID: 2232
			// (get) Token: 0x06005131 RID: 20785 RVA: 0x006960B5 File Offset: 0x006942B5
			public bool FoundTank
			{
				get
				{
					return this._nearestTankIndex != -1;
				}
			}

			// Token: 0x170008B9 RID: 2233
			// (get) Token: 0x06005132 RID: 20786 RVA: 0x006960C3 File Offset: 0x006942C3
			public int NearestTankOwnerIndex
			{
				get
				{
					return this._nearestTankIndex;
				}
			}

			// Token: 0x170008BA RID: 2234
			// (get) Token: 0x06005133 RID: 20787 RVA: 0x006960CB File Offset: 0x006942CB
			public float NearestTankDistance
			{
				get
				{
					return this._nearestTankDistance;
				}
			}

			// Token: 0x170008BB RID: 2235
			// (get) Token: 0x06005134 RID: 20788 RVA: 0x006960D3 File Offset: 0x006942D3
			public float AdjustedTankDistance
			{
				get
				{
					return this._adjustedTankDistance;
				}
			}

			// Token: 0x170008BC RID: 2236
			// (get) Token: 0x06005135 RID: 20789 RVA: 0x006960DB File Offset: 0x006942DB
			public NPCUtils.TargetType NearestTankType
			{
				get
				{
					return this._nearestTankType;
				}
			}

			// Token: 0x06005136 RID: 20790 RVA: 0x006960E4 File Offset: 0x006942E4
			public TargetSearchResults(NPC searcher, int nearestNPCIndex, float nearestNPCDistance, int nearestTankIndex, float nearestTankDistance, float adjustedTankDistance, NPCUtils.TargetType tankType)
			{
				this._nearestNPCIndex = nearestNPCIndex;
				this._nearestNPCDistance = nearestNPCDistance;
				this._nearestTankIndex = nearestTankIndex;
				this._adjustedTankDistance = adjustedTankDistance;
				this._nearestTankDistance = nearestTankDistance;
				this._nearestTankType = tankType;
				if (this._nearestNPCIndex != -1 && this._nearestTankIndex != -1)
				{
					if (this._nearestNPCDistance < this._adjustedTankDistance)
					{
						this._nearestTargetType = NPCUtils.TargetType.NPC;
						return;
					}
					this._nearestTargetType = tankType;
					return;
				}
				else
				{
					if (this._nearestNPCIndex != -1)
					{
						this._nearestTargetType = NPCUtils.TargetType.NPC;
						return;
					}
					if (this._nearestTankIndex != -1)
					{
						this._nearestTargetType = tankType;
						return;
					}
					this._nearestTargetType = NPCUtils.TargetType.None;
					return;
				}
			}

			// Token: 0x040068FC RID: 26876
			private NPCUtils.TargetType _nearestTargetType;

			// Token: 0x040068FD RID: 26877
			private int _nearestNPCIndex;

			// Token: 0x040068FE RID: 26878
			private float _nearestNPCDistance;

			// Token: 0x040068FF RID: 26879
			private int _nearestTankIndex;

			// Token: 0x04006900 RID: 26880
			private float _nearestTankDistance;

			// Token: 0x04006901 RID: 26881
			private float _adjustedTankDistance;

			// Token: 0x04006902 RID: 26882
			private NPCUtils.TargetType _nearestTankType;
		}

		// Token: 0x02000860 RID: 2144
		[Flags]
		public enum TargetSearchFlag
		{
			// Token: 0x04006904 RID: 26884
			None = 0,
			// Token: 0x04006905 RID: 26885
			NPCs = 1,
			// Token: 0x04006906 RID: 26886
			Players = 2,
			// Token: 0x04006907 RID: 26887
			All = 3
		}

		// Token: 0x02000861 RID: 2145
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006908 RID: 26888
			public static NPCUtils.SearchFilter<NPC> <0>__OnlyCrystal;

			// Token: 0x04006909 RID: 26889
			public static NPCUtils.SearchFilter<NPC> <1>__NonBeeNPCs;
		}
	}
}
