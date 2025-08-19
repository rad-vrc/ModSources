using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.Net;

namespace Terraria.GameContent
{
	// Token: 0x020004BA RID: 1210
	public class TeleportPylonsSystem : IOnPlayerJoining
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x005A4CE6 File Offset: 0x005A2EE6
		public IReadOnlyList<TeleportPylonInfo> Pylons
		{
			get
			{
				return this._pylons;
			}
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x005A4CEE File Offset: 0x005A2EEE
		public void Update()
		{
			if (Main.netMode != 1)
			{
				if (this._cooldownForUpdatingPylonsList > 0)
				{
					this._cooldownForUpdatingPylonsList--;
					return;
				}
				this._cooldownForUpdatingPylonsList = int.MaxValue;
				this.UpdatePylonsListAndBroadcastChanges();
			}
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x005A4D24 File Offset: 0x005A2F24
		public bool HasPylonOfType(TeleportPylonType pylonType)
		{
			return this._pylons.Any((TeleportPylonInfo x) => x.TypeOfPylon == pylonType);
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x005A4D55 File Offset: 0x005A2F55
		public bool HasAnyPylon()
		{
			return this._pylons.Count > 0;
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x005A4D65 File Offset: 0x005A2F65
		public void RequestImmediateUpdate()
		{
			if (Main.netMode != 1)
			{
				this._cooldownForUpdatingPylonsList = int.MaxValue;
				this.UpdatePylonsListAndBroadcastChanges();
			}
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x005A4D80 File Offset: 0x005A2F80
		private void UpdatePylonsListAndBroadcastChanges()
		{
			Utils.Swap<List<TeleportPylonInfo>>(ref this._pylons, ref this._pylonsOld);
			this._pylons.Clear();
			foreach (TileEntity value in TileEntity.ByPosition.Values)
			{
				if (value is IPylonTileEntity)
				{
					TeleportPylonInfo teleportPylonInfo = default(TeleportPylonInfo);
					teleportPylonInfo.PositionInTiles = value.Position;
					TETeleportationPylon vanillaPylon = value as TETeleportationPylon;
					TeleportPylonType vanillaType;
					if (vanillaPylon != null && vanillaPylon.TryGetPylonType(out vanillaType))
					{
						teleportPylonInfo.TypeOfPylon = vanillaType;
					}
					else
					{
						ModPylon modPylon;
						if (!TEModdedPylon.GetModPylonFromCoords((int)value.Position.X, (int)value.Position.Y, out modPylon))
						{
							continue;
						}
						teleportPylonInfo.TypeOfPylon = modPylon.PylonType;
					}
					TeleportPylonInfo item = teleportPylonInfo;
					this._pylons.Add(item);
				}
			}
			IEnumerable<TeleportPylonInfo> enumerable = this._pylonsOld.Except(this._pylons);
			foreach (TeleportPylonInfo item2 in this._pylons.Except(this._pylonsOld))
			{
				NetManager.Instance.BroadcastOrLoopback(NetTeleportPylonModule.SerializePylonWasAddedOrRemoved(item2, NetTeleportPylonModule.SubPacketType.PylonWasAdded));
			}
			foreach (TeleportPylonInfo item3 in enumerable)
			{
				NetManager.Instance.BroadcastOrLoopback(NetTeleportPylonModule.SerializePylonWasAddedOrRemoved(item3, NetTeleportPylonModule.SubPacketType.PylonWasRemoved));
			}
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x005A4F20 File Offset: 0x005A3120
		public void AddForClient(TeleportPylonInfo info)
		{
			if (!this._pylons.Contains(info))
			{
				this._pylons.Add(info);
			}
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x005A4F3C File Offset: 0x005A313C
		public void RemoveForClient(TeleportPylonInfo info)
		{
			this._pylons.RemoveAll((TeleportPylonInfo x) => x.Equals(info));
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x005A4F70 File Offset: 0x005A3170
		public unsafe void HandleTeleportRequest(TeleportPylonInfo info, int playerIndex)
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
				bool? flag3 = PylonLoader.ValidTeleportCheck_PreAnyDanger(info);
				bool flag4;
				if (flag3 != null)
				{
					bool value = flag3.GetValueOrDefault();
					flag4 = value;
				}
				else
				{
					ModPylon modPylon = info.ModPylon;
					flag4 = ((modPylon != null) ? modPylon.ValidTeleportCheck_AnyDanger(info) : flag);
				}
				flag = flag4;
				if (!flag)
				{
					key = "Net.CannotTeleportToPylonBecauseThereIsDanger";
				}
			}
			if (flag)
			{
				if (!NPC.downedPlantBoss && (double)info.PositionInTiles.Y > Main.worldSurface && *Framing.GetTileSafely((int)info.PositionInTiles.X, (int)info.PositionInTiles.Y).wall == 87)
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
			ModPylon destinationPylon = info.ModPylon;
			if (destinationPylon != null)
			{
				destinationPylon.ValidTeleportCheck_DestinationPostCheck(info, ref flag, ref key);
			}
			TeleportPylonInfo nearbyInfo = default(TeleportPylonInfo);
			bool flag2 = false;
			int num = 0;
			for (int i = 0; i < this._pylons.Count; i++)
			{
				TeleportPylonInfo info2 = this._pylons[i];
				if (player.InInteractionRange((int)info2.PositionInTiles.X, (int)info2.PositionInTiles.Y, TileReachCheckSettings.Pylons))
				{
					nearbyInfo = info2;
					if (num < 1)
					{
						num = 1;
					}
					int necessaryNPCCount2 = this.HowManyNPCsDoesPylonNeed(info2, player);
					if (this.DoesPylonHaveEnoughNPCsAroundIt(info2, necessaryNPCCount2))
					{
						if (num < 2)
						{
							num = 2;
						}
						SceneMetrics sceneMetrics2 = this._sceneMetrics;
						SceneMetricsScanSettings settings = new SceneMetricsScanSettings
						{
							VisualScanArea = null,
							BiomeScanCenterPositionInWorld = new Vector2?(info2.PositionInTiles.ToWorldCoordinates(8f, 8f)),
							ScanOreFinderData = false
						};
						sceneMetrics2.ScanAndExportToMain(settings);
						if (this.DoesPylonAcceptTeleportation(info2, player))
						{
							flag2 = true;
							break;
						}
					}
				}
			}
			if (!flag2)
			{
				if (num != 1)
				{
					if (num != 2)
					{
						key = "Net.CannotTeleportToPylonBecausePlayerIsNotNearAPylon";
					}
					else
					{
						key = "Net.CannotTeleportToPylonBecauseNotMeetingBiomeRequirements";
					}
				}
				else
				{
					key = "Net.CannotTeleportToPylonBecauseNotEnoughNPCsAtCurrentPylon";
				}
			}
			ModPylon nearbyPylon = nearbyInfo.ModPylon;
			if (nearbyPylon != null)
			{
				nearbyPylon.ValidTeleportCheck_NearbyPostCheck(nearbyInfo, ref flag, ref flag2, ref key);
			}
			PylonLoader.PostValidTeleportCheck(info, nearbyInfo, ref flag, ref flag2, ref key);
			if (flag && flag2)
			{
				Vector2 newPos = info.PositionInTiles.ToWorldCoordinates(8f, 8f) - new Vector2(0f, (float)player.HeightOffsetBoost);
				ModPylon finalDestinationPylon = info.ModPylon;
				if (finalDestinationPylon != null)
				{
					finalDestinationPylon.ModifyTeleportationPosition(info, ref newPos);
				}
				int num2 = 9;
				int typeOfPylon = (int)info.TypeOfPylon;
				int number = 0;
				player.Teleport(newPos, num2, typeOfPylon);
				player.velocity = Vector2.Zero;
				if (Main.netMode == 2)
				{
					RemoteClient.CheckSection(player.whoAmI, player.position, 1);
					NetMessage.SendData(65, -1, -1, null, 0, (float)player.whoAmI, newPos.X, newPos.Y, num2, number, typeOfPylon);
					return;
				}
			}
			else
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromKey(key, Array.Empty<object>()), new Color(255, 240, 20), playerIndex);
			}
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x005A52E8 File Offset: 0x005A34E8
		public static bool IsPlayerNearAPylon(Player player)
		{
			return TileID.Sets.CountsAsPylon.Any((int id) => player.IsTileTypeInInteractionRange(id, TileReachCheckSettings.Pylons));
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x005A5318 File Offset: 0x005A3518
		private bool DoesPylonHaveEnoughNPCsAroundIt(TeleportPylonInfo info, int necessaryNPCCount)
		{
			bool? flag = PylonLoader.ValidTeleportCheck_PreNPCCount(info, ref necessaryNPCCount);
			if (flag != null)
			{
				return flag.GetValueOrDefault();
			}
			ModPylon pylon = info.ModPylon;
			if (pylon != null)
			{
				return pylon.ValidTeleportCheck_NPCCount(info, necessaryNPCCount);
			}
			Point16 positionInTiles = info.PositionInTiles;
			return TeleportPylonsSystem.DoesPositionHaveEnoughNPCs(necessaryNPCCount, positionInTiles);
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x005A5364 File Offset: 0x005A3564
		public static bool DoesPositionHaveEnoughNPCs(int necessaryNPCCount, Point16 centerPoint)
		{
			if (necessaryNPCCount <= 0)
			{
				return true;
			}
			Rectangle rectangle;
			rectangle..ctor((int)centerPoint.X - Main.buffScanAreaWidth / 2, (int)centerPoint.Y - Main.buffScanAreaHeight / 2, Main.buffScanAreaWidth, Main.buffScanAreaHeight);
			int num = necessaryNPCCount;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.active && nPC.isLikeATownNPC && !nPC.homeless && rectangle.Contains(nPC.homeTileX, nPC.homeTileY))
				{
					Vector2 vector = new Vector2((float)nPC.homeTileX, (float)nPC.homeTileY);
					Vector2 value2;
					value2..ctor(nPC.Center.X / 16f, nPC.Center.Y / 16f);
					if (Vector2.Distance(vector, value2) < 100f)
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

		// Token: 0x06003A1D RID: 14877 RVA: 0x005A5445 File Offset: 0x005A3645
		public void RequestTeleportation(TeleportPylonInfo info, Player player)
		{
			NetManager.Instance.SendToServerOrLoopback(NetTeleportPylonModule.SerializeUseRequest(info));
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x005A5458 File Offset: 0x005A3658
		private bool DoesPylonAcceptTeleportation(TeleportPylonInfo info, Player player)
		{
			if (Main.netMode != 2 && Main.DroneCameraTracker != null && Main.DroneCameraTracker.IsInUse())
			{
				return false;
			}
			bool? flag5 = PylonLoader.ValidTeleportCheck_PreBiomeRequirements(info, this._sceneMetrics);
			if (flag5 != null)
			{
				return flag5.GetValueOrDefault();
			}
			ModPylon pylon = info.ModPylon;
			if (pylon != null)
			{
				return pylon.ValidTeleportCheck_BiomeRequirements(info, this._sceneMetrics);
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
				return flag && !flag2 && !this._sceneMetrics.EnoughTilesForJungle && !this._sceneMetrics.EnoughTilesForSnow && !this._sceneMetrics.EnoughTilesForDesert && !this._sceneMetrics.EnoughTilesForGlowingMushroom && !this._sceneMetrics.EnoughTilesForHallow && !this._sceneMetrics.EnoughTilesForCrimson && !this._sceneMetrics.EnoughTilesForCorruption;
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

		// Token: 0x06003A1F RID: 14879 RVA: 0x005A573F File Offset: 0x005A393F
		private int HowManyNPCsDoesPylonNeed(TeleportPylonInfo info, Player player)
		{
			if (info.TypeOfPylon != TeleportPylonType.Victory)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x005A574D File Offset: 0x005A394D
		public void Reset()
		{
			this._pylons.Clear();
			this._cooldownForUpdatingPylonsList = 0;
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x005A5764 File Offset: 0x005A3964
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (TeleportPylonInfo pylon in this._pylons)
			{
				NetManager.Instance.SendToClient(NetTeleportPylonModule.SerializePylonWasAddedOrRemoved(pylon, NetTeleportPylonModule.SubPacketType.PylonWasAdded), playerIndex);
			}
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x005A57C4 File Offset: 0x005A39C4
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

		// Token: 0x0400529D RID: 21149
		private List<TeleportPylonInfo> _pylons = new List<TeleportPylonInfo>();

		// Token: 0x0400529E RID: 21150
		private List<TeleportPylonInfo> _pylonsOld = new List<TeleportPylonInfo>();

		// Token: 0x0400529F RID: 21151
		private int _cooldownForUpdatingPylonsList;

		// Token: 0x040052A0 RID: 21152
		private const int CooldownTimePerPylonsListUpdate = 2147483647;

		// Token: 0x040052A1 RID: 21153
		internal SceneMetrics _sceneMetrics = new SceneMetrics();
	}
}
