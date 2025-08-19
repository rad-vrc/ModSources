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
	// Token: 0x02000492 RID: 1170
	public class CoinLossRevengeSystem
	{
		// Token: 0x060038F7 RID: 14583 RVA: 0x005958F4 File Offset: 0x00593AF4
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

		// Token: 0x060038F8 RID: 14584 RVA: 0x00595964 File Offset: 0x00593B64
		private void AddMarker(CoinLossRevengeSystem.RevengeMarker marker)
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				this._markers.Add(marker);
			}
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x005959AC File Offset: 0x00593BAC
		public void DestroyMarker(int markerUniqueID)
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				this._markers.RemoveAll((CoinLossRevengeSystem.RevengeMarker x) => x.UniqueID == markerUniqueID);
			}
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x00595A0C File Offset: 0x00593C0C
		public CoinLossRevengeSystem()
		{
			this._markers = new List<CoinLossRevengeSystem.RevengeMarker>();
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x00595A2C File Offset: 0x00593C2C
		public void CacheEnemy(NPC npc)
		{
			if (npc.boss || npc.realLife != -1 || npc.rarity > 0 || npc.extraValue < CoinLossRevengeSystem.MinimumCoinsForCaching || npc.position.X < Main.leftWorld + 640f + 16f || npc.position.X + (float)npc.width > Main.rightWorld - 640f - 32f || npc.position.Y < Main.topWorld + 640f + 16f || npc.position.Y > Main.bottomWorld - 640f - 32f - (float)npc.height)
			{
				return;
			}
			int num = npc.netID;
			int value;
			if (NPCID.Sets.RespawnEnemyID.TryGetValue(num, out value))
			{
				num = value;
			}
			if (num != 0)
			{
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
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x00595B8C File Offset: 0x00593D8C
		public void Reset()
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				this._markers.Clear();
			}
			this._gameTime = 0;
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x00595BD8 File Offset: 0x00593DD8
		public void Update()
		{
			this._gameTime++;
			if (Main.netMode == 1 && this._gameTime % 60 == 0)
			{
				this.RemoveExpiredOrInvalidMarkers();
			}
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x00595C04 File Offset: 0x00593E04
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
					bool flag = false;
					Tuple<int, Rectangle, Rectangle> tuple = null;
					foreach (Tuple<int, Rectangle, Rectangle> item in list)
					{
						if (revengeMarker.Intersects(item.Item2, item.Item3))
						{
							tuple = item;
							flag = true;
							break;
						}
					}
					if (!flag)
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

		// Token: 0x060038FF RID: 14591 RVA: 0x00595E30 File Offset: 0x00594030
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
					foreach (CoinLossRevengeSystem.RevengeMarker item in enumerable)
					{
						NetMessage.SendData(127, -1, -1, null, item.UniqueID, 0f, 0f, 0f, 0, 0, 0);
					}
					foreach (CoinLossRevengeSystem.RevengeMarker item2 in enumerable2)
					{
						NetMessage.SendData(127, -1, -1, null, item2.UniqueID, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x00595FD0 File Offset: 0x005941D0
		public CoinLossRevengeSystem.RevengeMarker DrawMapIcons(SpriteBatch spriteBatch, Vector2 mapTopLeft, Vector2 mapX2Y2AndOff, Rectangle? mapRect, float mapScale, float drawScale, ref string unused)
		{
			CoinLossRevengeSystem.RevengeMarker result = null;
			object markersLock = this._markersLock;
			CoinLossRevengeSystem.RevengeMarker result2;
			lock (markersLock)
			{
				foreach (CoinLossRevengeSystem.RevengeMarker marker in this._markers)
				{
					if (marker.DrawMapIcon(spriteBatch, mapTopLeft, mapX2Y2AndOff, mapRect, mapScale, drawScale, this._gameTime))
					{
						result = marker;
					}
				}
				result2 = result;
			}
			return result2;
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x00596068 File Offset: 0x00594268
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

		// Token: 0x04005227 RID: 21031
		public static bool DisplayCaching = false;

		// Token: 0x04005228 RID: 21032
		public static int MinimumCoinsForCaching = Item.buyPrice(0, 0, 10, 0);

		// Token: 0x04005229 RID: 21033
		private const int PLAYER_BOX_WIDTH_INNER = 1968;

		// Token: 0x0400522A RID: 21034
		private const int PLAYER_BOX_HEIGHT_INNER = 1200;

		// Token: 0x0400522B RID: 21035
		private const int PLAYER_BOX_WIDTH_OUTER = 2608;

		// Token: 0x0400522C RID: 21036
		private const int PLAYER_BOX_HEIGHT_OUTER = 1840;

		// Token: 0x0400522D RID: 21037
		private static readonly Vector2 _playerBoxSizeInner = new Vector2(1968f, 1200f);

		// Token: 0x0400522E RID: 21038
		private static readonly Vector2 _playerBoxSizeOuter = new Vector2(2608f, 1840f);

		// Token: 0x0400522F RID: 21039
		private List<CoinLossRevengeSystem.RevengeMarker> _markers;

		// Token: 0x04005230 RID: 21040
		private readonly object _markersLock = new object();

		// Token: 0x04005231 RID: 21041
		private int _gameTime;

		// Token: 0x02000B9B RID: 2971
		public class RevengeMarker
		{
			// Token: 0x17000950 RID: 2384
			// (get) Token: 0x06005D5B RID: 23899 RVA: 0x006C7E15 File Offset: 0x006C6015
			public bool RespawnAttemptLocked
			{
				get
				{
					return this._attemptedRespawn;
				}
			}

			// Token: 0x17000951 RID: 2385
			// (get) Token: 0x06005D5C RID: 23900 RVA: 0x006C7E1D File Offset: 0x006C601D
			public int UniqueID
			{
				get
				{
					return this._uniqueID;
				}
			}

			// Token: 0x06005D5D RID: 23901 RVA: 0x006C7E25 File Offset: 0x006C6025
			public void SetToExpire()
			{
				this._forceExpire = true;
			}

			// Token: 0x06005D5E RID: 23902 RVA: 0x006C7E2E File Offset: 0x006C602E
			public void SetRespawnAttemptLock(bool state)
			{
				this._attemptedRespawn = state;
			}

			// Token: 0x06005D5F RID: 23903 RVA: 0x006C7E38 File Offset: 0x006C6038
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

			// Token: 0x06005D60 RID: 23904 RVA: 0x006C7ED0 File Offset: 0x006C60D0
			public bool IsInvalid()
			{
				int nPCInvasionGroup = NPC.GetNPCInvasionGroup(this._npcTypeAgainstDiscouragement);
				switch (nPCInvasionGroup)
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
					return nPCInvasionGroup != Main.invasionType;
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

			// Token: 0x06005D61 RID: 23905 RVA: 0x006C7FF4 File Offset: 0x006C61F4
			public bool IsExpired(int gameTime)
			{
				return this._forceExpire || this._expirationTime <= gameTime;
			}

			// Token: 0x06005D62 RID: 23906 RVA: 0x006C800C File Offset: 0x006C620C
			private int CalculateExpirationTime(int gameCacheTime, int coinValue)
			{
				int num = (coinValue < CoinLossRevengeSystem.RevengeMarker._expirationCompSilver) ? ((int)MathHelper.Lerp(0f, 3600f, Utils.GetLerpValue((float)CoinLossRevengeSystem.RevengeMarker._expirationCompCopper, (float)CoinLossRevengeSystem.RevengeMarker._expirationCompSilver, (float)coinValue, false))) : ((coinValue < CoinLossRevengeSystem.RevengeMarker._expirationCompGold) ? ((int)MathHelper.Lerp(36000f, 108000f, Utils.GetLerpValue((float)CoinLossRevengeSystem.RevengeMarker._expirationCompSilver, (float)CoinLossRevengeSystem.RevengeMarker._expirationCompGold, (float)coinValue, false))) : ((coinValue >= CoinLossRevengeSystem.RevengeMarker._expirationCompPlat) ? 432000 : ((int)MathHelper.Lerp(108000f, 216000f, Utils.GetLerpValue((float)CoinLossRevengeSystem.RevengeMarker._expirationCompSilver, (float)CoinLossRevengeSystem.RevengeMarker._expirationCompGold, (float)coinValue, false)))));
				num += 18000;
				return gameCacheTime + num;
			}

			// Token: 0x06005D63 RID: 23907 RVA: 0x006C80B6 File Offset: 0x006C62B6
			public bool Intersects(Rectangle rectInner, Rectangle rectOuter)
			{
				return rectOuter.Intersects(this._hitbox);
			}

			// Token: 0x06005D64 RID: 23908 RVA: 0x006C80C8 File Offset: 0x006C62C8
			public void SpawnEnemy()
			{
				int num = NPC.NewNPC(new EntitySource_RevengeSystem(null), (int)this._location.X, (int)this._location.Y, this._npcNetID, 0, 0f, 0f, 0f, 0f, 255);
				NPC nPC = Main.npc[num];
				if (this._npcNetID < 0)
				{
					nPC.SetDefaults(this._npcNetID, default(NPCSpawnParams));
				}
				int value;
				if (NPCID.Sets.SpecialSpawningRules.TryGetValue(this._npcNetID, out value) && value == 0)
				{
					Point point = nPC.position.ToTileCoordinates();
					nPC.ai[0] = (float)point.X;
					nPC.ai[1] = (float)point.Y;
					nPC.netUpdate = true;
				}
				nPC.timeLeft += 3600;
				nPC.extraValue = this._coinsValue;
				nPC.value = this._baseValue;
				nPC.SpawnedFromStatue = this._spawnedFromStatue;
				float num2 = Math.Max(0.5f, this._npcHPPercent);
				nPC.life = (int)((float)nPC.lifeMax * num2);
				if (num < 200)
				{
					if (Main.netMode == 0)
					{
						nPC.moneyPing(this._location);
					}
					else
					{
						NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.SendData(92, -1, -1, null, num, (float)this._coinsValue, this._location.X, this._location.Y, 0, 0, 0);
					}
				}
				if (CoinLossRevengeSystem.DisplayCaching)
				{
					Main.NewText("Spawned " + nPC.GivenOrTypeName, byte.MaxValue, byte.MaxValue, byte.MaxValue);
				}
			}

			// Token: 0x06005D65 RID: 23909 RVA: 0x006C8274 File Offset: 0x006C6474
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

			// Token: 0x06005D66 RID: 23910 RVA: 0x006C8364 File Offset: 0x006C6564
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
				value = ((this._coinsValue < 100) ? TextureAssets.Coin[0].Value : ((this._coinsValue < 10000) ? TextureAssets.Coin[1].Value : ((this._coinsValue >= 1000000) ? TextureAssets.Coin[3].Value : TextureAssets.Coin[2].Value)));
				Rectangle rectangle = value.Frame(1, 8, 0, 0, 0, 0);
				spriteBatch.Draw(value, vector, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() / 2f, drawScale, 0, 0f);
				return Utils.CenteredRectangle(vector, rectangle.Size() * drawScale).Contains(Main.MouseScreen.ToPoint());
			}

			// Token: 0x06005D67 RID: 23911 RVA: 0x006C8484 File Offset: 0x006C6684
			public void UseMouseOver(SpriteBatch spriteBatch, ref string mouseTextString, float drawScale = 1f)
			{
				mouseTextString = "";
				Vector2 vector = Main.MouseScreen / drawScale + new Vector2(-28f) + new Vector2(4f, 0f);
				ItemSlot.DrawMoney(spriteBatch, "", vector.X, vector.Y, Utils.CoinsSplit((long)this._coinsValue), true);
			}

			// Token: 0x06005D68 RID: 23912 RVA: 0x006C84EC File Offset: 0x006C66EC
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

			// Token: 0x04007689 RID: 30345
			private static int _uniqueIDCounter = 0;

			// Token: 0x0400768A RID: 30346
			private static readonly int _expirationCompCopper = Item.buyPrice(0, 0, 0, 1);

			// Token: 0x0400768B RID: 30347
			private static readonly int _expirationCompSilver = Item.buyPrice(0, 0, 1, 0);

			// Token: 0x0400768C RID: 30348
			private static readonly int _expirationCompGold = Item.buyPrice(0, 1, 0, 0);

			// Token: 0x0400768D RID: 30349
			private static readonly int _expirationCompPlat = Item.buyPrice(1, 0, 0, 0);

			// Token: 0x0400768E RID: 30350
			private const int ONE_MINUTE = 3600;

			// Token: 0x0400768F RID: 30351
			private const int ENEMY_BOX_WIDTH = 2160;

			// Token: 0x04007690 RID: 30352
			private const int ENEMY_BOX_HEIGHT = 1440;

			// Token: 0x04007691 RID: 30353
			public static readonly Vector2 EnemyBoxSize = new Vector2(2160f, 1440f);

			// Token: 0x04007692 RID: 30354
			private readonly Vector2 _location;

			// Token: 0x04007693 RID: 30355
			private readonly Rectangle _hitbox;

			// Token: 0x04007694 RID: 30356
			private readonly int _npcNetID;

			// Token: 0x04007695 RID: 30357
			private readonly float _npcHPPercent;

			// Token: 0x04007696 RID: 30358
			private readonly float _baseValue;

			// Token: 0x04007697 RID: 30359
			private readonly int _coinsValue;

			// Token: 0x04007698 RID: 30360
			private readonly int _npcTypeAgainstDiscouragement;

			// Token: 0x04007699 RID: 30361
			private readonly int _npcAIStyleAgainstDiscouragement;

			// Token: 0x0400769A RID: 30362
			private readonly int _expirationTime;

			// Token: 0x0400769B RID: 30363
			private readonly bool _spawnedFromStatue;

			// Token: 0x0400769C RID: 30364
			private readonly int _uniqueID;

			// Token: 0x0400769D RID: 30365
			private bool _forceExpire;

			// Token: 0x0400769E RID: 30366
			private bool _attemptedRespawn;
		}
	}
}
