using System;
using Microsoft.Xna.Framework;

namespace Terraria.Utilities
{
	// Token: 0x02000144 RID: 324
	public static class NPCUtils
	{
		// Token: 0x060018DF RID: 6367 RVA: 0x004DF272 File Offset: 0x004DD472
		public static NPCUtils.TargetSearchResults SearchForTarget(Vector2 position, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
		{
			return NPCUtils.SearchForTarget(null, position, flags, playerFilter, npcFilter);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x004DF27E File Offset: 0x004DD47E
		public static NPCUtils.TargetSearchResults SearchForTarget(NPC searcher, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
		{
			return NPCUtils.SearchForTarget(searcher, searcher.Center, flags, playerFilter, npcFilter);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x004DF290 File Offset: 0x004DD490
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
					NPC npc = Main.npc[i];
					if (npc.active && npc.whoAmI != searcher.whoAmI && (npcFilter == null || npcFilter(npc)))
					{
						float num3 = Vector2.DistanceSquared(position, npc.Center);
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

		// Token: 0x060018E2 RID: 6370 RVA: 0x004DF46C File Offset: 0x004DD66C
		public static void TargetClosestOldOnesInvasion(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchResults targetSearchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilters.OnlyPlayersInCertainDistance(searcher.Center, 200f), new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.OnlyCrystal));
			if (!targetSearchResults.FoundTarget)
			{
				return;
			}
			searcher.target = targetSearchResults.NearestTargetIndex;
			searcher.targetRect = targetSearchResults.NearestTargetHitbox;
			if (searcher.ShouldFaceTarget(ref targetSearchResults, null) && faceTarget)
			{
				searcher.FaceTarget();
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x004DF4DC File Offset: 0x004DD6DC
		public static void TargetClosestNonBees(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchResults targetSearchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.All, null, new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.NonBeeNPCs));
			if (!targetSearchResults.FoundTarget)
			{
				return;
			}
			searcher.target = targetSearchResults.NearestTargetIndex;
			searcher.targetRect = targetSearchResults.NearestTargetHitbox;
			if (searcher.ShouldFaceTarget(ref targetSearchResults, null) && faceTarget)
			{
				searcher.FaceTarget();
			}
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x004DF540 File Offset: 0x004DD740
		public static void TargetClosestDownwindFromNPC(NPC searcher, float distanceMaxX, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchResults targetSearchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.Players, NPCUtils.SearchFilters.DownwindFromNPC(searcher, distanceMaxX), null);
			if (!targetSearchResults.FoundTarget)
			{
				return;
			}
			searcher.target = targetSearchResults.NearestTargetIndex;
			searcher.targetRect = targetSearchResults.NearestTargetHitbox;
			if (searcher.ShouldFaceTarget(ref targetSearchResults, null) && faceTarget)
			{
				searcher.FaceTarget();
			}
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x004DF59C File Offset: 0x004DD79C
		public static void TargetClosestCommon(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			searcher.TargetClosest(faceTarget);
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x004DF5A8 File Offset: 0x004DD7A8
		public static void TargetClosestBetsy(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchResults targetSearchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.All, null, new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.OnlyCrystal));
			if (!targetSearchResults.FoundTarget)
			{
				return;
			}
			NPCUtils.TargetType value = targetSearchResults.NearestTargetType;
			if (targetSearchResults.FoundTank && !targetSearchResults.NearestTankOwner.dead)
			{
				value = NPCUtils.TargetType.Player;
			}
			searcher.target = targetSearchResults.NearestTargetIndex;
			searcher.targetRect = targetSearchResults.NearestTargetHitbox;
			if (searcher.ShouldFaceTarget(ref targetSearchResults, new NPCUtils.TargetType?(value)) && faceTarget)
			{
				searcher.FaceTarget();
			}
		}

		// Token: 0x020005A6 RID: 1446
		// (Invoke) Token: 0x06003268 RID: 12904
		public delegate bool SearchFilter<T>(T entity) where T : Entity;

		// Token: 0x020005A7 RID: 1447
		// (Invoke) Token: 0x0600326C RID: 12908
		public delegate void NPCTargetingMethod(NPC searcher, bool faceTarget, Vector2? checkPosition);

		// Token: 0x020005A8 RID: 1448
		public static class SearchFilters
		{
			// Token: 0x0600326F RID: 12911 RVA: 0x005EBF60 File Offset: 0x005EA160
			public static bool OnlyCrystal(NPC npc)
			{
				return npc.type == 548 && !npc.dontTakeDamageFromHostiles;
			}

			// Token: 0x06003270 RID: 12912 RVA: 0x005EBF7A File Offset: 0x005EA17A
			public static NPCUtils.SearchFilter<Player> OnlyPlayersInCertainDistance(Vector2 position, float maxDistance)
			{
				return (Player player) => player.Distance(position) <= maxDistance;
			}

			// Token: 0x06003271 RID: 12913 RVA: 0x005EBF9A File Offset: 0x005EA19A
			public static bool NonBeeNPCs(NPC npc)
			{
				return npc.type != 211 && npc.type != 210 && npc.type != 222 && npc.CanBeChasedBy(null, false);
			}

			// Token: 0x06003272 RID: 12914 RVA: 0x005EBFCD File Offset: 0x005EA1CD
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

		// Token: 0x020005A9 RID: 1449
		public enum TargetType
		{
			// Token: 0x04005A43 RID: 23107
			None,
			// Token: 0x04005A44 RID: 23108
			NPC,
			// Token: 0x04005A45 RID: 23109
			Player,
			// Token: 0x04005A46 RID: 23110
			TankPet
		}

		// Token: 0x020005AA RID: 1450
		public struct TargetSearchResults
		{
			// Token: 0x170003B9 RID: 953
			// (get) Token: 0x06003273 RID: 12915 RVA: 0x005EBFF0 File Offset: 0x005EA1F0
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

			// Token: 0x170003BA RID: 954
			// (get) Token: 0x06003274 RID: 12916 RVA: 0x005EC024 File Offset: 0x005EA224
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

			// Token: 0x170003BB RID: 955
			// (get) Token: 0x06003275 RID: 12917 RVA: 0x005EC082 File Offset: 0x005EA282
			public NPCUtils.TargetType NearestTargetType
			{
				get
				{
					return this._nearestTargetType;
				}
			}

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x06003276 RID: 12918 RVA: 0x005EC08A File Offset: 0x005EA28A
			public bool FoundTarget
			{
				get
				{
					return this._nearestTargetType > NPCUtils.TargetType.None;
				}
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x06003277 RID: 12919 RVA: 0x005EC095 File Offset: 0x005EA295
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

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x06003278 RID: 12920 RVA: 0x005EC0AE File Offset: 0x005EA2AE
			public bool FoundNPC
			{
				get
				{
					return this._nearestNPCIndex != -1;
				}
			}

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x06003279 RID: 12921 RVA: 0x005EC0BC File Offset: 0x005EA2BC
			public int NearestNPCIndex
			{
				get
				{
					return this._nearestNPCIndex;
				}
			}

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x0600327A RID: 12922 RVA: 0x005EC0C4 File Offset: 0x005EA2C4
			public float NearestNPCDistance
			{
				get
				{
					return this._nearestNPCDistance;
				}
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x0600327B RID: 12923 RVA: 0x005EC0CC File Offset: 0x005EA2CC
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

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x0600327C RID: 12924 RVA: 0x005EC0E5 File Offset: 0x005EA2E5
			public bool FoundTank
			{
				get
				{
					return this._nearestTankIndex != -1;
				}
			}

			// Token: 0x170003C3 RID: 963
			// (get) Token: 0x0600327D RID: 12925 RVA: 0x005EC0F3 File Offset: 0x005EA2F3
			public int NearestTankOwnerIndex
			{
				get
				{
					return this._nearestTankIndex;
				}
			}

			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x0600327E RID: 12926 RVA: 0x005EC0FB File Offset: 0x005EA2FB
			public float NearestTankDistance
			{
				get
				{
					return this._nearestTankDistance;
				}
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x0600327F RID: 12927 RVA: 0x005EC103 File Offset: 0x005EA303
			public float AdjustedTankDistance
			{
				get
				{
					return this._adjustedTankDistance;
				}
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x06003280 RID: 12928 RVA: 0x005EC10B File Offset: 0x005EA30B
			public NPCUtils.TargetType NearestTankType
			{
				get
				{
					return this._nearestTankType;
				}
			}

			// Token: 0x06003281 RID: 12929 RVA: 0x005EC114 File Offset: 0x005EA314
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

			// Token: 0x04005A47 RID: 23111
			private NPCUtils.TargetType _nearestTargetType;

			// Token: 0x04005A48 RID: 23112
			private int _nearestNPCIndex;

			// Token: 0x04005A49 RID: 23113
			private float _nearestNPCDistance;

			// Token: 0x04005A4A RID: 23114
			private int _nearestTankIndex;

			// Token: 0x04005A4B RID: 23115
			private float _nearestTankDistance;

			// Token: 0x04005A4C RID: 23116
			private float _adjustedTankDistance;

			// Token: 0x04005A4D RID: 23117
			private NPCUtils.TargetType _nearestTankType;
		}

		// Token: 0x020005AB RID: 1451
		[Flags]
		public enum TargetSearchFlag
		{
			// Token: 0x04005A4F RID: 23119
			None = 0,
			// Token: 0x04005A50 RID: 23120
			NPCs = 1,
			// Token: 0x04005A51 RID: 23121
			Players = 2,
			// Token: 0x04005A52 RID: 23122
			All = 3
		}
	}
}
