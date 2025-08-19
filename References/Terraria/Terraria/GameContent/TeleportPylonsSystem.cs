using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.Tile_Entities;
using Terraria.Localization;
using Terraria.Net;

namespace Terraria.GameContent
{
	// Token: 0x020001EB RID: 491
	public class TeleportPylonsSystem : IOnPlayerJoining
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x004FD020 File Offset: 0x004FB220
		public List<TeleportPylonInfo> Pylons
		{
			get
			{
				return this._pylons;
			}
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x004FD028 File Offset: 0x004FB228
		public void Update()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			if (this._cooldownForUpdatingPylonsList > 0)
			{
				this._cooldownForUpdatingPylonsList--;
				return;
			}
			this._cooldownForUpdatingPylonsList = int.MaxValue;
			this.UpdatePylonsListAndBroadcastChanges();
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x004FD05C File Offset: 0x004FB25C
		public bool HasPylonOfType(TeleportPylonType pylonType)
		{
			return this._pylons.Any((TeleportPylonInfo x) => x.TypeOfPylon == pylonType);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x004FD08D File Offset: 0x004FB28D
		public bool HasAnyPylon()
		{
			return this._pylons.Count > 0;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x004FD09D File Offset: 0x004FB29D
		public void RequestImmediateUpdate()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			this._cooldownForUpdatingPylonsList = int.MaxValue;
			this.UpdatePylonsListAndBroadcastChanges();
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x004FD0BC File Offset: 0x004FB2BC
		private void UpdatePylonsListAndBroadcastChanges()
		{
			Utils.Swap<List<TeleportPylonInfo>>(ref this._pylons, ref this._pylonsOld);
			this._pylons.Clear();
			foreach (TileEntity tileEntity in TileEntity.ByPosition.Values)
			{
				TETeleportationPylon teteleportationPylon = tileEntity as TETeleportationPylon;
				TeleportPylonType typeOfPylon;
				if (teteleportationPylon != null && teteleportationPylon.TryGetPylonType(out typeOfPylon))
				{
					TeleportPylonInfo item = new TeleportPylonInfo
					{
						PositionInTiles = teteleportationPylon.Position,
						TypeOfPylon = typeOfPylon
					};
					this._pylons.Add(item);
				}
			}
			IEnumerable<TeleportPylonInfo> enumerable = this._pylonsOld.Except(this._pylons);
			foreach (TeleportPylonInfo info in this._pylons.Except(this._pylonsOld))
			{
				NetManager.Instance.BroadcastOrLoopback(NetTeleportPylonModule.SerializePylonWasAddedOrRemoved(info, NetTeleportPylonModule.SubPacketType.PylonWasAdded));
			}
			foreach (TeleportPylonInfo info2 in enumerable)
			{
				NetManager.Instance.BroadcastOrLoopback(NetTeleportPylonModule.SerializePylonWasAddedOrRemoved(info2, NetTeleportPylonModule.SubPacketType.PylonWasRemoved));
			}
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x004FD21C File Offset: 0x004FB41C
		public void AddForClient(TeleportPylonInfo info)
		{
			if (this._pylons.Contains(info))
			{
				return;
			}
			this._pylons.Add(info);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x004FD23C File Offset: 0x004FB43C
		public void RemoveForClient(TeleportPylonInfo info)
		{
			this._pylons.RemoveAll((TeleportPylonInfo x) => x.Equals(info));
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x004FD270 File Offset: 0x004FB470
		public void HandleTeleportRequest(TeleportPylonInfo info, int playerIndex)
		{
			Player player = Main.player[playerIndex];
			string key = null;
			bool flag = true;
			if (flag)
			{
				flag &= TeleportPylonsSystem.IsPlayerNearAPylon(player);
				if (!flag)
				{
					key = "Net.CannotTeleportToPylonBecausePlayerIsNotNearAPylon";
				}
			}
			if (flag)
			{
				int necessaryNPCCount = this.HowManyNPCsDoesPylonNeed(info, player);
				flag &= this.DoesPylonHaveEnoughNPCsAroundIt(info, necessaryNPCCount);
				if (!flag)
				{
					key = "Net.CannotTeleportToPylonBecauseNotEnoughNPCs";
				}
			}
			if (flag)
			{
				flag &= !NPC.AnyDanger(false, true);
				if (!flag)
				{
					key = "Net.CannotTeleportToPylonBecauseThereIsDanger";
				}
			}
			if (flag)
			{
				if (!NPC.downedPlantBoss && (double)info.PositionInTiles.Y > Main.worldSurface && Framing.GetTileSafely((int)info.PositionInTiles.X, (int)info.PositionInTiles.Y).wall == 87)
				{
					flag = false;
				}
				if (!flag)
				{
					key = "Net.CannotTeleportToPylonBecauseAccessingLihzahrdTempleEarly";
				}
			}
			if (flag)
			{
				SceneMetrics sceneMetrics = this._sceneMetrics;
				SceneMetricsScanSettings settings = new SceneMetricsScanSettings
				{
					VisualScanArea = null,
					BiomeScanCenterPositionInWorld = new Vector2?(info.PositionInTiles.ToWorldCoordinates(8f, 8f)),
					ScanOreFinderData = false
				};
				sceneMetrics.ScanAndExportToMain(settings);
				flag = this.DoesPylonAcceptTeleportation(info, player);
				if (!flag)
				{
					key = "Net.CannotTeleportToPylonBecauseNotMeetingBiomeRequirements";
				}
			}
			if (flag)
			{
				bool flag2 = false;
				int num = 0;
				for (int i = 0; i < this._pylons.Count; i++)
				{
					TeleportPylonInfo teleportPylonInfo = this._pylons[i];
					if (player.InInteractionRange((int)teleportPylonInfo.PositionInTiles.X, (int)teleportPylonInfo.PositionInTiles.Y, TileReachCheckSettings.Pylons))
					{
						if (num < 1)
						{
							num = 1;
						}
						int necessaryNPCCount2 = this.HowManyNPCsDoesPylonNeed(teleportPylonInfo, player);
						if (this.DoesPylonHaveEnoughNPCsAroundIt(teleportPylonInfo, necessaryNPCCount2))
						{
							if (num < 2)
							{
								num = 2;
							}
							SceneMetrics sceneMetrics2 = this._sceneMetrics;
							SceneMetricsScanSettings settings = new SceneMetricsScanSettings
							{
								VisualScanArea = null,
								BiomeScanCenterPositionInWorld = new Vector2?(teleportPylonInfo.PositionInTiles.ToWorldCoordinates(8f, 8f)),
								ScanOreFinderData = false
							};
							sceneMetrics2.ScanAndExportToMain(settings);
							if (this.DoesPylonAcceptTeleportation(teleportPylonInfo, player))
							{
								flag2 = true;
								break;
							}
						}
					}
				}
				if (!flag2)
				{
					flag = false;
					switch (num)
					{
					default:
						key = "Net.CannotTeleportToPylonBecausePlayerIsNotNearAPylon";
						break;
					case 1:
						key = "Net.CannotTeleportToPylonBecauseNotEnoughNPCsAtCurrentPylon";
						break;
					case 2:
						key = "Net.CannotTeleportToPylonBecauseNotMeetingBiomeRequirements";
						break;
					}
				}
			}
			if (flag)
			{
				Vector2 vector = info.PositionInTiles.ToWorldCoordinates(8f, 8f) - new Vector2(0f, (float)player.HeightOffsetBoost);
				int num2 = 9;
				int typeOfPylon = (int)info.TypeOfPylon;
				int number = 0;
				player.Teleport(vector, num2, typeOfPylon);
				player.velocity = Vector2.Zero;
				if (Main.netMode == 2)
				{
					RemoteClient.CheckSection(player.whoAmI, player.position, 1);
					NetMessage.SendData(65, -1, -1, null, 0, (float)player.whoAmI, vector.X, vector.Y, num2, number, typeOfPylon);
					return;
				}
			}
			else
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromKey(key, new object[0]), new Color(255, 240, 20), playerIndex);
			}
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x004FD555 File Offset: 0x004FB755
		public static bool IsPlayerNearAPylon(Player player)
		{
			return player.IsTileTypeInInteractionRange(597, TileReachCheckSettings.Pylons);
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x004FD568 File Offset: 0x004FB768
		private bool DoesPylonHaveEnoughNPCsAroundIt(TeleportPylonInfo info, int necessaryNPCCount)
		{
			if (necessaryNPCCount <= 0)
			{
				return true;
			}
			Point16 positionInTiles = info.PositionInTiles;
			return TeleportPylonsSystem.DoesPositionHaveEnoughNPCs(necessaryNPCCount, positionInTiles);
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x004FD58C File Offset: 0x004FB78C
		public static bool DoesPositionHaveEnoughNPCs(int necessaryNPCCount, Point16 centerPoint)
		{
			Rectangle rectangle = new Rectangle((int)centerPoint.X - Main.buffScanAreaWidth / 2, (int)centerPoint.Y - Main.buffScanAreaHeight / 2, Main.buffScanAreaWidth, Main.buffScanAreaHeight);
			int num = necessaryNPCCount;
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.isLikeATownNPC && !npc.homeless && rectangle.Contains(npc.homeTileX, npc.homeTileY))
				{
					Vector2 value = new Vector2((float)npc.homeTileX, (float)npc.homeTileY);
					Vector2 value2 = new Vector2(npc.Center.X / 16f, npc.Center.Y / 16f);
					if (Vector2.Distance(value, value2) < 100f)
					{
						num--;
						if (num == 0)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x004FD667 File Offset: 0x004FB867
		public void RequestTeleportation(TeleportPylonInfo info, Player player)
		{
			NetManager.Instance.SendToServerOrLoopback(NetTeleportPylonModule.SerializeUseRequest(info));
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x004FD67C File Offset: 0x004FB87C
		private bool DoesPylonAcceptTeleportation(TeleportPylonInfo info, Player player)
		{
			if (Main.netMode != 2 && Main.DroneCameraTracker != null && Main.DroneCameraTracker.IsInUse())
			{
				return false;
			}
			switch (info.TypeOfPylon)
			{
			case TeleportPylonType.SurfacePurity:
			{
				bool flag = (double)info.PositionInTiles.Y <= Main.worldSurface;
				if (Main.remixWorld)
				{
					flag = ((double)info.PositionInTiles.Y > Main.rockLayer && (int)info.PositionInTiles.Y < Main.maxTilesY - 350);
				}
				bool flag2 = (int)info.PositionInTiles.X >= Main.maxTilesX - 380 || info.PositionInTiles.X <= 380;
				return flag && !flag2 && (!this._sceneMetrics.EnoughTilesForJungle && !this._sceneMetrics.EnoughTilesForSnow && !this._sceneMetrics.EnoughTilesForDesert && !this._sceneMetrics.EnoughTilesForGlowingMushroom && !this._sceneMetrics.EnoughTilesForHallow && !this._sceneMetrics.EnoughTilesForCrimson && !this._sceneMetrics.EnoughTilesForCorruption);
			}
			case TeleportPylonType.Jungle:
				return this._sceneMetrics.EnoughTilesForJungle;
			case TeleportPylonType.Hallow:
				return this._sceneMetrics.EnoughTilesForHallow;
			case TeleportPylonType.Underground:
				return (double)info.PositionInTiles.Y >= Main.worldSurface;
			case TeleportPylonType.Beach:
			{
				bool flag3 = (double)info.PositionInTiles.Y <= Main.worldSurface && (double)info.PositionInTiles.Y > Main.worldSurface * 0.3499999940395355;
				bool flag4 = (int)info.PositionInTiles.X >= Main.maxTilesX - 380 || info.PositionInTiles.X <= 380;
				if (Main.remixWorld)
				{
					flag3 |= ((double)info.PositionInTiles.Y > Main.rockLayer && (int)info.PositionInTiles.Y < Main.maxTilesY - 350);
					flag4 |= ((double)info.PositionInTiles.X < (double)Main.maxTilesX * 0.43 || (double)info.PositionInTiles.X > (double)Main.maxTilesX * 0.57);
				}
				return flag4 && flag3;
			}
			case TeleportPylonType.Desert:
				return this._sceneMetrics.EnoughTilesForDesert;
			case TeleportPylonType.Snow:
				return this._sceneMetrics.EnoughTilesForSnow;
			case TeleportPylonType.GlowingMushroom:
				return (!Main.remixWorld || (int)info.PositionInTiles.Y < Main.maxTilesY - 200) && this._sceneMetrics.EnoughTilesForGlowingMushroom;
			case TeleportPylonType.Victory:
				return true;
			default:
				return true;
			}
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x004FD924 File Offset: 0x004FBB24
		private int HowManyNPCsDoesPylonNeed(TeleportPylonInfo info, Player player)
		{
			TeleportPylonType typeOfPylon = info.TypeOfPylon;
			if (typeOfPylon != TeleportPylonType.Victory)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x004FD93F File Offset: 0x004FBB3F
		public void Reset()
		{
			this._pylons.Clear();
			this._cooldownForUpdatingPylonsList = 0;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x004FD954 File Offset: 0x004FBB54
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (TeleportPylonInfo info in this._pylons)
			{
				NetManager.Instance.SendToClient(NetTeleportPylonModule.SerializePylonWasAddedOrRemoved(info, NetTeleportPylonModule.SubPacketType.PylonWasAdded), playerIndex);
			}
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x004FD9B4 File Offset: 0x004FBBB4
		public static void SpawnInWorldDust(int tileStyle, Rectangle dustBox)
		{
			float r = 1f;
			float g = 1f;
			float b = 1f;
			switch ((byte)tileStyle)
			{
			case 0:
				r = 0.05f;
				g = 0.8f;
				b = 0.3f;
				break;
			case 1:
				r = 0.7f;
				g = 0.8f;
				b = 0.05f;
				break;
			case 2:
				r = 0.5f;
				g = 0.3f;
				b = 0.7f;
				break;
			case 3:
				r = 0.4f;
				g = 0.4f;
				b = 0.6f;
				break;
			case 4:
				r = 0.2f;
				g = 0.2f;
				b = 0.95f;
				break;
			case 5:
				r = 0.85f;
				g = 0.45f;
				b = 0.1f;
				break;
			case 6:
				r = 1f;
				g = 1f;
				b = 1.2f;
				break;
			case 7:
				r = 0.4f;
				g = 0.7f;
				b = 1.2f;
				break;
			case 8:
				r = 0.7f;
				g = 0.7f;
				b = 0.7f;
				break;
			}
			int num = Dust.NewDust(dustBox.TopLeft(), dustBox.Width, dustBox.Height, 43, 0f, 0f, 254, new Color(r, g, b, 1f), 0.5f);
			Main.dust[num].velocity *= 0.1f;
			Dust dust = Main.dust[num];
			dust.velocity.Y = dust.velocity.Y - 0.2f;
		}

		// Token: 0x040043C3 RID: 17347
		private List<TeleportPylonInfo> _pylons = new List<TeleportPylonInfo>();

		// Token: 0x040043C4 RID: 17348
		private List<TeleportPylonInfo> _pylonsOld = new List<TeleportPylonInfo>();

		// Token: 0x040043C5 RID: 17349
		private int _cooldownForUpdatingPylonsList;

		// Token: 0x040043C6 RID: 17350
		private const int CooldownTimePerPylonsListUpdate = 2147483647;

		// Token: 0x040043C7 RID: 17351
		private SceneMetrics _sceneMetrics = new SceneMetrics();
	}
}
