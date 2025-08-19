using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent
{
	// Token: 0x020001FA RID: 506
	public class CoinLossRevengeSystem
	{
		// Token: 0x06001D14 RID: 7444 RVA: 0x004FFF04 File Offset: 0x004FE104
		public void AddMarkerFromReader(BinaryReader reader)
		{
			int uniqueID = reader.ReadInt32();
			Vector2 coords = reader.ReadVector2();
			int npcNetId = reader.ReadInt32();
			float npcHPPercent = reader.ReadSingle();
			int npcType = reader.ReadInt32();
			int npcAiStyle = reader.ReadInt32();
			int coinValue = reader.ReadInt32();
			float baseValue = reader.ReadSingle();
			bool spawnedFromStatue = reader.ReadBoolean();
			CoinLossRevengeSystem.RevengeMarker marker = new CoinLossRevengeSystem.RevengeMarker(coords, npcNetId, npcHPPercent, npcType, npcAiStyle, coinValue, baseValue, spawnedFromStatue, this._gameTime, uniqueID);
			this.AddMarker(marker);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x004FFF74 File Offset: 0x004FE174
		private void AddMarker(CoinLossRevengeSystem.RevengeMarker marker)
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				this._markers.Add(marker);
			}
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x004FFFBC File Offset: 0x004FE1BC
		public void DestroyMarker(int markerUniqueID)
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				this._markers.RemoveAll((CoinLossRevengeSystem.RevengeMarker x) => x.UniqueID == markerUniqueID);
			}
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0050001C File Offset: 0x004FE21C
		public CoinLossRevengeSystem()
		{
			this._markers = new List<CoinLossRevengeSystem.RevengeMarker>();
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0050003C File Offset: 0x004FE23C
		public void CacheEnemy(NPC npc)
		{
			if (npc.boss || npc.realLife != -1 || npc.rarity > 0 || npc.extraValue < CoinLossRevengeSystem.MinimumCoinsForCaching)
			{
				return;
			}
			if (npc.position.X < Main.leftWorld + 640f + 16f || npc.position.X + (float)npc.width > Main.rightWorld - 640f - 32f || npc.position.Y < Main.topWorld + 640f + 16f || npc.position.Y > Main.bottomWorld - 640f - 32f - (float)npc.height)
			{
				return;
			}
			int num = npc.netID;
			int num2;
			if (NPCID.Sets.RespawnEnemyID.TryGetValue(num, out num2))
			{
				num = num2;
			}
			if (num == 0)
			{
				return;
			}
			CoinLossRevengeSystem.RevengeMarker marker = new CoinLossRevengeSystem.RevengeMarker(npc.Center, num, npc.GetLifePercent(), npc.type, npc.aiStyle, npc.extraValue, npc.value, npc.SpawnedFromStatue, this._gameTime, -1);
			this.AddMarker(marker);
			if (Main.netMode == 2)
			{
				NetMessage.SendCoinLossRevengeMarker(marker, -1, -1);
			}
			if (CoinLossRevengeSystem.DisplayCaching)
			{
				Main.NewText("Cached " + npc.GivenOrTypeName, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x00500190 File Offset: 0x004FE390
		public void Reset()
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				this._markers.Clear();
			}
			this._gameTime = 0;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x005001DC File Offset: 0x004FE3DC
		public void Update()
		{
			this._gameTime++;
			if (Main.netMode == 1 && this._gameTime % 60 == 0)
			{
				this.RemoveExpiredOrInvalidMarkers();
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00500208 File Offset: 0x004FE408
		public void CheckRespawns()
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				if (this._markers.Count == 0)
				{
					return;
				}
			}
			List<Tuple<int, Rectangle, Rectangle>> list = new List<Tuple<int, Rectangle, Rectangle>>();
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active && !player.dead)
				{
					list.Add(Tuple.Create<int, Rectangle, Rectangle>(i, Utils.CenteredRectangle(player.Center, CoinLossRevengeSystem._playerBoxSizeInner), Utils.CenteredRectangle(player.Center, CoinLossRevengeSystem._playerBoxSizeOuter)));
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			this.RemoveExpiredOrInvalidMarkers();
			markersLock = this._markersLock;
			lock (markersLock)
			{
				List<CoinLossRevengeSystem.RevengeMarker> list2 = new List<CoinLossRevengeSystem.RevengeMarker>();
				for (int j = 0; j < this._markers.Count; j++)
				{
					CoinLossRevengeSystem.RevengeMarker revengeMarker = this._markers[j];
					bool flag2 = false;
					Tuple<int, Rectangle, Rectangle> tuple = null;
					foreach (Tuple<int, Rectangle, Rectangle> tuple2 in list)
					{
						if (revengeMarker.Intersects(tuple2.Item2, tuple2.Item3))
						{
							tuple = tuple2;
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						revengeMarker.SetRespawnAttemptLock(false);
					}
					else if (!revengeMarker.RespawnAttemptLocked)
					{
						revengeMarker.SetRespawnAttemptLock(true);
						if (revengeMarker.WouldNPCBeDiscouraged(Main.player[tuple.Item1]))
						{
							revengeMarker.SetToExpire();
						}
						else
						{
							revengeMarker.SpawnEnemy();
							list2.Add(revengeMarker);
							if (Main.dedServ)
							{
								NetMessage.SendData(127, -1, -1, null, revengeMarker.UniqueID, 0f, 0f, 0f, 0, 0, 0);
							}
						}
					}
				}
				this._markers = this._markers.Except(list2).ToList<CoinLossRevengeSystem.RevengeMarker>();
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00500434 File Offset: 0x004FE634
		private void RemoveExpiredOrInvalidMarkers()
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				IEnumerable<CoinLossRevengeSystem.RevengeMarker> enumerable = from x in this._markers
				where x.IsExpired(this._gameTime)
				select x;
				IEnumerable<CoinLossRevengeSystem.RevengeMarker> enumerable2 = from x in this._markers
				where x.IsInvalid()
				select x;
				this._markers.RemoveAll((CoinLossRevengeSystem.RevengeMarker x) => x.IsInvalid());
				this._markers.RemoveAll((CoinLossRevengeSystem.RevengeMarker x) => x.IsExpired(this._gameTime));
				if (Main.dedServ)
				{
					foreach (CoinLossRevengeSystem.RevengeMarker revengeMarker in enumerable)
					{
						NetMessage.SendData(127, -1, -1, null, revengeMarker.UniqueID, 0f, 0f, 0f, 0, 0, 0);
					}
					foreach (CoinLossRevengeSystem.RevengeMarker revengeMarker2 in enumerable2)
					{
						NetMessage.SendData(127, -1, -1, null, revengeMarker2.UniqueID, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x005005D4 File Offset: 0x004FE7D4
		public CoinLossRevengeSystem.RevengeMarker DrawMapIcons(SpriteBatch spriteBatch, Vector2 mapTopLeft, Vector2 mapX2Y2AndOff, Rectangle? mapRect, float mapScale, float drawScale, ref string unused)
		{
			CoinLossRevengeSystem.RevengeMarker result = null;
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				foreach (CoinLossRevengeSystem.RevengeMarker revengeMarker in this._markers)
				{
					if (revengeMarker.DrawMapIcon(spriteBatch, mapTopLeft, mapX2Y2AndOff, mapRect, mapScale, drawScale, this._gameTime))
					{
						result = revengeMarker;
					}
				}
			}
			return result;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00500668 File Offset: 0x004FE868
		public void SendAllMarkersToPlayer(int plr)
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				foreach (CoinLossRevengeSystem.RevengeMarker marker in this._markers)
				{
					NetMessage.SendCoinLossRevengeMarker(marker, plr, -1);
				}
			}
		}

		// Token: 0x040043F8 RID: 17400
		public static bool DisplayCaching = false;

		// Token: 0x040043F9 RID: 17401
		public static int MinimumCoinsForCaching = Item.buyPrice(0, 0, 10, 0);

		// Token: 0x040043FA RID: 17402
		private const int PLAYER_BOX_WIDTH_INNER = 1968;

		// Token: 0x040043FB RID: 17403
		private const int PLAYER_BOX_HEIGHT_INNER = 1200;

		// Token: 0x040043FC RID: 17404
		private const int PLAYER_BOX_WIDTH_OUTER = 2608;

		// Token: 0x040043FD RID: 17405
		private const int PLAYER_BOX_HEIGHT_OUTER = 1840;

		// Token: 0x040043FE RID: 17406
		private static readonly Vector2 _playerBoxSizeInner = new Vector2(1968f, 1200f);

		// Token: 0x040043FF RID: 17407
		private static readonly Vector2 _playerBoxSizeOuter = new Vector2(2608f, 1840f);

		// Token: 0x04004400 RID: 17408
		private List<CoinLossRevengeSystem.RevengeMarker> _markers;

		// Token: 0x04004401 RID: 17409
		private readonly object _markersLock = new object();

		// Token: 0x04004402 RID: 17410
		private int _gameTime;

		// Token: 0x02000613 RID: 1555
		public class RevengeMarker
		{
			// Token: 0x06003361 RID: 13153 RVA: 0x00605CA3 File Offset: 0x00603EA3
			public void SetToExpire()
			{
				this._forceExpire = true;
			}

			// Token: 0x170003C8 RID: 968
			// (get) Token: 0x06003362 RID: 13154 RVA: 0x00605CAC File Offset: 0x00603EAC
			public bool RespawnAttemptLocked
			{
				get
				{
					return this._attemptedRespawn;
				}
			}

			// Token: 0x06003363 RID: 13155 RVA: 0x00605CB4 File Offset: 0x00603EB4
			public void SetRespawnAttemptLock(bool state)
			{
				this._attemptedRespawn = state;
			}

			// Token: 0x06003364 RID: 13156 RVA: 0x00605CC0 File Offset: 0x00603EC0
			public RevengeMarker(Vector2 coords, int npcNetId, float npcHPPercent, int npcType, int npcAiStyle, int coinValue, float baseValue, bool spawnedFromStatue, int gameTime, int uniqueID = -1)
			{
				this._location = coords;
				this._npcNetID = npcNetId;
				this._npcHPPercent = npcHPPercent;
				this._npcTypeAgainstDiscouragement = npcType;
				this._npcAIStyleAgainstDiscouragement = npcAiStyle;
				this._coinsValue = coinValue;
				this._baseValue = baseValue;
				this._spawnedFromStatue = spawnedFromStatue;
				this._hitbox = Utils.CenteredRectangle(this._location, CoinLossRevengeSystem.RevengeMarker.EnemyBoxSize);
				this._expirationTime = this.CalculateExpirationTime(gameTime, coinValue);
				if (uniqueID == -1)
				{
					this._uniqueID = CoinLossRevengeSystem.RevengeMarker._uniqueIDCounter++;
					return;
				}
				this._uniqueID = uniqueID;
			}

			// Token: 0x06003365 RID: 13157 RVA: 0x00605D58 File Offset: 0x00603F58
			public bool IsInvalid()
			{
				int npcinvasionGroup = NPC.GetNPCInvasionGroup(this._npcTypeAgainstDiscouragement);
				switch (npcinvasionGroup)
				{
				case -3:
					return !DD2Event.Ongoing;
				case -2:
					return !Main.pumpkinMoon || Main.dayTime;
				case -1:
					return !Main.snowMoon || Main.dayTime;
				case 1:
				case 2:
				case 3:
				case 4:
					return npcinvasionGroup != Main.invasionType;
				}
				int npcTypeAgainstDiscouragement = this._npcTypeAgainstDiscouragement;
				if (npcTypeAgainstDiscouragement <= 166)
				{
					if (npcTypeAgainstDiscouragement - 158 > 1 && npcTypeAgainstDiscouragement != 162 && npcTypeAgainstDiscouragement != 166)
					{
						return false;
					}
				}
				else if (npcTypeAgainstDiscouragement != 251 && npcTypeAgainstDiscouragement != 253)
				{
					switch (npcTypeAgainstDiscouragement)
					{
					case 460:
					case 461:
					case 462:
					case 463:
					case 466:
					case 467:
					case 468:
					case 469:
					case 477:
					case 478:
					case 479:
						break;
					case 464:
					case 465:
					case 470:
					case 471:
					case 472:
					case 473:
					case 474:
					case 475:
					case 476:
						return false;
					default:
						return false;
					}
				}
				if (!Main.eclipse || !Main.dayTime)
				{
					return true;
				}
				return false;
			}

			// Token: 0x06003366 RID: 13158 RVA: 0x00605E7C File Offset: 0x0060407C
			public bool IsExpired(int gameTime)
			{
				return this._forceExpire || this._expirationTime <= gameTime;
			}

			// Token: 0x06003367 RID: 13159 RVA: 0x00605E94 File Offset: 0x00604094
			private int CalculateExpirationTime(int gameCacheTime, int coinValue)
			{
				int num;
				if (coinValue < CoinLossRevengeSystem.RevengeMarker._expirationCompSilver)
				{
					num = (int)MathHelper.Lerp(0f, 3600f, Utils.GetLerpValue((float)CoinLossRevengeSystem.RevengeMarker._expirationCompCopper, (float)CoinLossRevengeSystem.RevengeMarker._expirationCompSilver, (float)coinValue, false));
				}
				else if (coinValue < CoinLossRevengeSystem.RevengeMarker._expirationCompGold)
				{
					num = (int)MathHelper.Lerp(36000f, 108000f, Utils.GetLerpValue((float)CoinLossRevengeSystem.RevengeMarker._expirationCompSilver, (float)CoinLossRevengeSystem.RevengeMarker._expirationCompGold, (float)coinValue, false));
				}
				else if (coinValue < CoinLossRevengeSystem.RevengeMarker._expirationCompPlat)
				{
					num = (int)MathHelper.Lerp(108000f, 216000f, Utils.GetLerpValue((float)CoinLossRevengeSystem.RevengeMarker._expirationCompSilver, (float)CoinLossRevengeSystem.RevengeMarker._expirationCompGold, (float)coinValue, false));
				}
				else
				{
					num = 432000;
				}
				num += 18000;
				return gameCacheTime + num;
			}

			// Token: 0x06003368 RID: 13160 RVA: 0x00605F41 File Offset: 0x00604141
			public bool Intersects(Rectangle rectInner, Rectangle rectOuter)
			{
				return rectOuter.Intersects(this._hitbox);
			}

			// Token: 0x06003369 RID: 13161 RVA: 0x00605F50 File Offset: 0x00604150
			public void SpawnEnemy()
			{
				int num = NPC.NewNPC(new EntitySource_RevengeSystem(), (int)this._location.X, (int)this._location.Y, this._npcNetID, 0, 0f, 0f, 0f, 0f, 255);
				NPC npc = Main.npc[num];
				if (this._npcNetID < 0)
				{
					npc.SetDefaults(this._npcNetID, default(NPCSpawnParams));
				}
				int num2;
				if (NPCID.Sets.SpecialSpawningRules.TryGetValue(this._npcNetID, out num2) && num2 == 0)
				{
					Point point = npc.position.ToTileCoordinates();
					npc.ai[0] = (float)point.X;
					npc.ai[1] = (float)point.Y;
					npc.netUpdate = true;
				}
				npc.timeLeft += 3600;
				npc.extraValue = this._coinsValue;
				npc.value = this._baseValue;
				npc.SpawnedFromStatue = this._spawnedFromStatue;
				float num3 = Math.Max(0.5f, this._npcHPPercent);
				npc.life = (int)((float)npc.lifeMax * num3);
				if (num < 200)
				{
					if (Main.netMode == 0)
					{
						npc.moneyPing(this._location);
					}
					else
					{
						NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.SendData(92, -1, -1, null, num, (float)this._coinsValue, this._location.X, this._location.Y, 0, 0, 0);
					}
				}
				if (CoinLossRevengeSystem.DisplayCaching)
				{
					Main.NewText("Spawned " + npc.GivenOrTypeName, byte.MaxValue, byte.MaxValue, byte.MaxValue);
				}
			}

			// Token: 0x0600336A RID: 13162 RVA: 0x006060FC File Offset: 0x006042FC
			public bool WouldNPCBeDiscouraged(Player playerTarget)
			{
				int num;
				switch (this._npcAIStyleAgainstDiscouragement)
				{
				case 2:
					return NPC.DespawnEncouragement_AIStyle2_FloatingEye_IsDiscouraged(this._npcTypeAgainstDiscouragement, playerTarget.position, 255);
				case 3:
					return !NPC.DespawnEncouragement_AIStyle3_Fighters_NotDiscouraged(this._npcTypeAgainstDiscouragement, playerTarget.position, null);
				case 6:
				{
					bool flag = false;
					num = this._npcTypeAgainstDiscouragement;
					if (num <= 95)
					{
						if (num != 10 && num != 39 && num != 95)
						{
							goto IL_97;
						}
					}
					else if (num != 117 && num != 510)
					{
						if (num == 513)
						{
							flag = !playerTarget.ZoneUndergroundDesert;
							goto IL_97;
						}
						goto IL_97;
					}
					flag = true;
					IL_97:
					return flag && (double)playerTarget.position.Y < Main.worldSurface * 16.0;
				}
				}
				num = this._npcNetID;
				if (num != 253)
				{
					return num == 490 && Main.dayTime;
				}
				return !Main.eclipse;
			}

			// Token: 0x0600336B RID: 13163 RVA: 0x006061EC File Offset: 0x006043EC
			public bool DrawMapIcon(SpriteBatch spriteBatch, Vector2 mapTopLeft, Vector2 mapX2Y2AndOff, Rectangle? mapRect, float mapScale, float drawScale, int gameTime)
			{
				Vector2 vector = this._location / 16f - mapTopLeft;
				vector *= mapScale;
				vector += mapX2Y2AndOff;
				if (mapRect != null && !mapRect.Value.Contains(vector.ToPoint()))
				{
					return false;
				}
				Texture2D value = TextureAssets.MapDeath.Value;
				if (this._coinsValue < 100)
				{
					value = TextureAssets.Coin[0].Value;
				}
				else if (this._coinsValue < 10000)
				{
					value = TextureAssets.Coin[1].Value;
				}
				else if (this._coinsValue < 1000000)
				{
					value = TextureAssets.Coin[2].Value;
				}
				else
				{
					value = TextureAssets.Coin[3].Value;
				}
				Rectangle rectangle = value.Frame(1, 8, 0, 0, 0, 0);
				spriteBatch.Draw(value, vector, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() / 2f, drawScale, SpriteEffects.None, 0f);
				return Utils.CenteredRectangle(vector, rectangle.Size() * drawScale).Contains(Main.MouseScreen.ToPoint());
			}

			// Token: 0x0600336C RID: 13164 RVA: 0x00606310 File Offset: 0x00604510
			public void UseMouseOver(SpriteBatch spriteBatch, ref string mouseTextString, float drawScale = 1f)
			{
				mouseTextString = "";
				Vector2 vector = Main.MouseScreen / drawScale + new Vector2(-28f) + new Vector2(4f, 0f);
				ItemSlot.DrawMoney(spriteBatch, "", vector.X, vector.Y, Utils.CoinsSplit((long)this._coinsValue), true);
			}

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x0600336D RID: 13165 RVA: 0x00606377 File Offset: 0x00604577
			public int UniqueID
			{
				get
				{
					return this._uniqueID;
				}
			}

			// Token: 0x0600336E RID: 13166 RVA: 0x00606380 File Offset: 0x00604580
			public void WriteSelfTo(BinaryWriter writer)
			{
				writer.Write(this._uniqueID);
				writer.WriteVector2(this._location);
				writer.Write(this._npcNetID);
				writer.Write(this._npcHPPercent);
				writer.Write(this._npcTypeAgainstDiscouragement);
				writer.Write(this._npcAIStyleAgainstDiscouragement);
				writer.Write(this._coinsValue);
				writer.Write(this._baseValue);
				writer.Write(this._spawnedFromStatue);
			}

			// Token: 0x04006073 RID: 24691
			private static int _uniqueIDCounter = 0;

			// Token: 0x04006074 RID: 24692
			private static readonly int _expirationCompCopper = Item.buyPrice(0, 0, 0, 1);

			// Token: 0x04006075 RID: 24693
			private static readonly int _expirationCompSilver = Item.buyPrice(0, 0, 1, 0);

			// Token: 0x04006076 RID: 24694
			private static readonly int _expirationCompGold = Item.buyPrice(0, 1, 0, 0);

			// Token: 0x04006077 RID: 24695
			private static readonly int _expirationCompPlat = Item.buyPrice(1, 0, 0, 0);

			// Token: 0x04006078 RID: 24696
			private const int ONE_MINUTE = 3600;

			// Token: 0x04006079 RID: 24697
			private const int ENEMY_BOX_WIDTH = 2160;

			// Token: 0x0400607A RID: 24698
			private const int ENEMY_BOX_HEIGHT = 1440;

			// Token: 0x0400607B RID: 24699
			public static readonly Vector2 EnemyBoxSize = new Vector2(2160f, 1440f);

			// Token: 0x0400607C RID: 24700
			private readonly Vector2 _location;

			// Token: 0x0400607D RID: 24701
			private readonly Rectangle _hitbox;

			// Token: 0x0400607E RID: 24702
			private readonly int _npcNetID;

			// Token: 0x0400607F RID: 24703
			private readonly float _npcHPPercent;

			// Token: 0x04006080 RID: 24704
			private readonly float _baseValue;

			// Token: 0x04006081 RID: 24705
			private readonly int _coinsValue;

			// Token: 0x04006082 RID: 24706
			private readonly int _npcTypeAgainstDiscouragement;

			// Token: 0x04006083 RID: 24707
			private readonly int _npcAIStyleAgainstDiscouragement;

			// Token: 0x04006084 RID: 24708
			private readonly int _expirationTime;

			// Token: 0x04006085 RID: 24709
			private readonly bool _spawnedFromStatue;

			// Token: 0x04006086 RID: 24710
			private readonly int _uniqueID;

			// Token: 0x04006087 RID: 24711
			private bool _forceExpire;

			// Token: 0x04006088 RID: 24712
			private bool _attemptedRespawn;
		}
	}
}
