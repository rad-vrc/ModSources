using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Events
{
	// Token: 0x0200062D RID: 1581
	public class DD2Event
	{
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x0060F027 File Offset: 0x0060D227
		public static bool ReadyToFindBartender
		{
			get
			{
				return NPC.downedBoss2;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x0060F02E File Offset: 0x0060D22E
		public static bool DownedInvasionAnyDifficulty
		{
			get
			{
				return DD2Event.DownedInvasionT1 || DD2Event.DownedInvasionT2 || DD2Event.DownedInvasionT3;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x0060F045 File Offset: 0x0060D245
		// (set) Token: 0x0600452B RID: 17707 RVA: 0x0060F04C File Offset: 0x0060D24C
		public static int TimeLeftBetweenWaves
		{
			get
			{
				return DD2Event._timeLeftUntilSpawningBegins;
			}
			set
			{
				DD2Event._timeLeftUntilSpawningBegins = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x0060F054 File Offset: 0x0060D254
		public static bool EnemySpawningIsOnHold
		{
			get
			{
				return DD2Event._timeLeftUntilSpawningBegins != 0;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x0060F05E File Offset: 0x0060D25E
		public static bool EnemiesShouldChasePlayers
		{
			get
			{
				bool ongoing = DD2Event.Ongoing;
				return true;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x0060F067 File Offset: 0x0060D267
		public static bool ReadyForTier2
		{
			get
			{
				return Main.hardMode && NPC.downedMechBossAny;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600452F RID: 17711 RVA: 0x0060F077 File Offset: 0x0060D277
		public static bool ReadyForTier3
		{
			get
			{
				return Main.hardMode && NPC.downedGolemBoss;
			}
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x0060F087 File Offset: 0x0060D287
		public static void Save(BinaryWriter writer)
		{
			writer.Write(DD2Event.DownedInvasionT1);
			writer.Write(DD2Event.DownedInvasionT2);
			writer.Write(DD2Event.DownedInvasionT3);
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x0060F0AC File Offset: 0x0060D2AC
		public static void Load(BinaryReader reader, int gameVersionNumber)
		{
			if (gameVersionNumber < 178)
			{
				NPC.savedBartender = false;
				DD2Event.ResetProgressEntirely();
				return;
			}
			NPC.savedBartender = reader.ReadBoolean();
			DD2Event.DownedInvasionT1 = reader.ReadBoolean();
			DD2Event.DownedInvasionT2 = reader.ReadBoolean();
			DD2Event.DownedInvasionT3 = reader.ReadBoolean();
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x0060F0F9 File Offset: 0x0060D2F9
		public static void ResetProgressEntirely()
		{
			DD2Event.DownedInvasionT1 = (DD2Event.DownedInvasionT2 = (DD2Event.DownedInvasionT3 = false));
			DD2Event.Ongoing = false;
			DD2Event.ArenaHitbox = default(Rectangle);
			DD2Event._arenaHitboxingCooldown = 0;
			DD2Event._timeLeftUntilSpawningBegins = 0;
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x0060F12C File Offset: 0x0060D32C
		public static void ReportEventProgress()
		{
			int currentWave;
			int requiredKillCount;
			int currentKillCount;
			DD2Event.GetInvasionStatus(out currentWave, out requiredKillCount, out currentKillCount, false);
			Main.ReportInvasionProgress(currentKillCount, requiredKillCount, 3, currentWave);
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x0060F150 File Offset: 0x0060D350
		public static void SyncInvasionProgress(int toWho)
		{
			int currentWave;
			int requiredKillCount;
			int currentKillCount;
			DD2Event.GetInvasionStatus(out currentWave, out requiredKillCount, out currentKillCount, false);
			NetMessage.SendData(78, toWho, -1, null, currentKillCount, (float)requiredKillCount, 3f, (float)currentWave, 0, 0, 0);
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x0060F180 File Offset: 0x0060D380
		public static void SpawnNPC(ref int newNPC)
		{
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x0060F184 File Offset: 0x0060D384
		public static void UpdateTime()
		{
			if (!DD2Event.Ongoing && !Main.dedServ)
			{
				Filters.Scene.Deactivate("CrystalDestructionVortex", Array.Empty<object>());
				Filters.Scene.Deactivate("CrystalDestructionColor", Array.Empty<object>());
				Filters.Scene.Deactivate("CrystalWin", Array.Empty<object>());
				return;
			}
			if (Main.netMode != 1 && !NPC.AnyNPCs(548))
			{
				DD2Event.StopInvasion(false);
			}
			if (Main.netMode == 1)
			{
				if (DD2Event._timeLeftUntilSpawningBegins > 0)
				{
					DD2Event._timeLeftUntilSpawningBegins--;
				}
				if (DD2Event._timeLeftUntilSpawningBegins < 0)
				{
					DD2Event._timeLeftUntilSpawningBegins = 0;
				}
				return;
			}
			if (DD2Event._timeLeftUntilSpawningBegins > 0)
			{
				DD2Event._timeLeftUntilSpawningBegins--;
				if (DD2Event._timeLeftUntilSpawningBegins == 0)
				{
					int currentWave;
					int requiredKillCount;
					int currentKillCount;
					DD2Event.GetInvasionStatus(out currentWave, out requiredKillCount, out currentKillCount, false);
					if (!DD2Event.LostThisRun)
					{
						WorldGen.BroadcastText(Lang.GetInvasionWaveText(currentWave, DD2Event.GetEnemiesForWave(currentWave)), DD2Event.INFO_NEW_WAVE_COLOR);
						if (currentWave == 7 && DD2Event.OngoingDifficulty == 3)
						{
							DD2Event.SummonBetsy();
						}
					}
					else
					{
						DD2Event.LoseInvasionMessage();
					}
					if (Main.netMode != 1)
					{
						Main.ReportInvasionProgress(currentKillCount, requiredKillCount, 3, currentWave);
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(78, -1, -1, null, Main.invasionProgress, (float)Main.invasionProgressMax, 3f, (float)currentWave, 0, 0, 0);
					}
				}
			}
			if (DD2Event._timeLeftUntilSpawningBegins < 0)
			{
				DD2Event._timeLeftUntilSpawningBegins = 0;
			}
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x0060F2C8 File Offset: 0x0060D4C8
		public static void StartInvasion(int difficultyOverride = -1)
		{
			if (Main.netMode != 1)
			{
				DD2Event._crystalsDropping_toDrop = 0;
				DD2Event._crystalsDropping_alreadyDropped = 0;
				DD2Event._crystalsDropping_lastWave = 0;
				DD2Event._timeLeftUntilSpawningBegins = 0;
				DD2Event.Ongoing = true;
				DD2Event.FindProperDifficulty();
				if (difficultyOverride != -1)
				{
					DD2Event.OngoingDifficulty = difficultyOverride;
				}
				DD2Event._deadGoblinSpots.Clear();
				DD2Event._downedDarkMageT1 = false;
				DD2Event._downedOgreT2 = false;
				DD2Event._spawnedBetsyT3 = false;
				DD2Event.LostThisRun = false;
				DD2Event.WonThisRun = false;
				NPC.totalInvasionPoints = 0f;
				NPC.waveKills = 0f;
				NPC.waveNumber = 1;
				DD2Event.ClearAllTowersInGame();
				WorldGen.BroadcastText(NetworkText.FromKey("DungeonDefenders2.InvasionStart", Array.Empty<object>()), DD2Event.INFO_START_INVASION_COLOR);
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				if (Main.netMode != 1)
				{
					Main.ReportInvasionProgress(0, 1, 3, 1);
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(78, -1, -1, null, 0, 1f, 3f, 1f, 0, 0, 0);
				}
				DD2Event.SetEnemySpawningOnHold(300);
				DD2Event.WipeEntities();
			}
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x0060F3D0 File Offset: 0x0060D5D0
		public static void StopInvasion(bool win = false)
		{
			if (DD2Event.Ongoing)
			{
				if (win)
				{
					DD2Event.WinInvasionInternal();
				}
				DD2Event.Ongoing = false;
				DD2Event._deadGoblinSpots.Clear();
				if (Main.netMode != 1)
				{
					NPC.totalInvasionPoints = 0f;
					NPC.waveKills = 0f;
					NPC.waveNumber = 0;
					DD2Event.WipeEntities();
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x0060F440 File Offset: 0x0060D640
		private static void WinInvasionInternal()
		{
			if (DD2Event.OngoingDifficulty >= 1)
			{
				DD2Event.DownedInvasionT1 = true;
			}
			if (DD2Event.OngoingDifficulty >= 2)
			{
				DD2Event.DownedInvasionT2 = true;
			}
			if (DD2Event.OngoingDifficulty >= 3)
			{
				DD2Event.DownedInvasionT3 = true;
			}
			if (DD2Event.OngoingDifficulty == 1)
			{
				DD2Event.DropMedals(3);
			}
			if (DD2Event.OngoingDifficulty == 2)
			{
				DD2Event.DropMedals(15);
			}
			if (DD2Event.OngoingDifficulty == 3)
			{
				AchievementsHelper.NotifyProgressionEvent(23);
				DD2Event.DropMedals(60);
			}
			WorldGen.BroadcastText(NetworkText.FromKey("DungeonDefenders2.InvasionWin", Array.Empty<object>()), DD2Event.INFO_START_INVASION_COLOR);
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x0060F4C3 File Offset: 0x0060D6C3
		public static void LoseInvasionMessage()
		{
			WorldGen.BroadcastText(NetworkText.FromKey("DungeonDefenders2.InvasionLose", Array.Empty<object>()), DD2Event.INFO_FAILURE_INVASION_COLOR);
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x0060F4DE File Offset: 0x0060D6DE
		private static void FindProperDifficulty()
		{
			DD2Event.OngoingDifficulty = 1;
			if (DD2Event.ReadyForTier2)
			{
				DD2Event.OngoingDifficulty = 2;
			}
			if (DD2Event.ReadyForTier3)
			{
				DD2Event.OngoingDifficulty = 3;
			}
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x0060F500 File Offset: 0x0060D700
		public static void CheckProgress(int slainMonsterID)
		{
			if (Main.netMode == 1 || !DD2Event.Ongoing || DD2Event.LostThisRun || DD2Event.WonThisRun || DD2Event.EnemySpawningIsOnHold)
			{
				return;
			}
			int currentWave;
			int requiredKillCount;
			int currentKillCount;
			DD2Event.GetInvasionStatus(out currentWave, out requiredKillCount, out currentKillCount, false);
			float num = (float)DD2Event.GetMonsterPointsWorth(slainMonsterID);
			float waveKills = NPC.waveKills;
			NPC.waveKills += num;
			NPC.totalInvasionPoints += num;
			currentKillCount += (int)num;
			bool flag = false;
			int num2 = currentWave;
			if (NPC.waveKills >= (float)requiredKillCount && requiredKillCount != 0)
			{
				NPC.waveKills = 0f;
				NPC.waveNumber++;
				flag = true;
				DD2Event.GetInvasionStatus(out currentWave, out requiredKillCount, out currentKillCount, true);
				if (DD2Event.WonThisRun)
				{
					if ((float)currentKillCount != waveKills && num != 0f)
					{
						if (Main.netMode != 1)
						{
							Main.ReportInvasionProgress(currentKillCount, requiredKillCount, 3, currentWave);
						}
						if (Main.netMode == 2)
						{
							NetMessage.SendData(78, -1, -1, null, Main.invasionProgress, (float)Main.invasionProgressMax, 3f, (float)currentWave, 0, 0, 0);
						}
					}
					return;
				}
				int num3 = currentWave;
				string key = "DungeonDefenders2.WaveComplete";
				if (num3 == 2)
				{
					key = "DungeonDefenders2.WaveCompleteFirst";
				}
				WorldGen.BroadcastText(NetworkText.FromKey(key, Array.Empty<object>()), DD2Event.INFO_NEW_WAVE_COLOR);
				DD2Event.SetEnemySpawningOnHold(1800);
				if (DD2Event.OngoingDifficulty == 1)
				{
					if (num3 == 5)
					{
						DD2Event.DropMedals(1);
					}
					if (num3 == 4)
					{
						DD2Event.DropMedals(1);
					}
				}
				if (DD2Event.OngoingDifficulty == 2)
				{
					if (num3 == 7)
					{
						DD2Event.DropMedals(6);
					}
					if (num3 == 6)
					{
						DD2Event.DropMedals(3);
					}
					if (num3 == 5)
					{
						DD2Event.DropMedals(1);
					}
				}
				if (DD2Event.OngoingDifficulty == 3)
				{
					if (num3 == 7)
					{
						DD2Event.DropMedals(25);
					}
					if (num3 == 6)
					{
						DD2Event.DropMedals(11);
					}
					if (num3 == 5)
					{
						DD2Event.DropMedals(3);
					}
					if (num3 == 4)
					{
						DD2Event.DropMedals(1);
					}
				}
			}
			if ((float)currentKillCount == waveKills)
			{
				return;
			}
			if (flag)
			{
				int num4 = 1;
				int num5 = 1;
				if (Main.netMode != 1)
				{
					Main.ReportInvasionProgress(num4, num5, 3, num2);
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(78, -1, -1, null, num4, (float)num5, 3f, (float)num2, 0, 0, 0);
					return;
				}
			}
			else
			{
				if (Main.netMode != 1)
				{
					Main.ReportInvasionProgress(currentKillCount, requiredKillCount, 3, currentWave);
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(78, -1, -1, null, Main.invasionProgress, (float)Main.invasionProgressMax, 3f, (float)currentWave, 0, 0, 0);
				}
			}
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x0060F728 File Offset: 0x0060D928
		public static void StartVictoryScene()
		{
			DD2Event.WonThisRun = true;
			int num = NPC.FindFirstNPC(548);
			if (num == -1)
			{
				return;
			}
			Main.npc[num].ai[1] = 2f;
			Main.npc[num].ai[0] = 2f;
			Main.npc[num].netUpdate = true;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i] != null && Main.npc[i].active && Main.npc[i].type == 549)
				{
					Main.npc[i].ai[0] = 0f;
					Main.npc[i].ai[1] = 1f;
					Main.npc[i].netUpdate = true;
				}
			}
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x0060F7EB File Offset: 0x0060D9EB
		public static void ReportLoss()
		{
			DD2Event.LostThisRun = true;
			DD2Event.SetEnemySpawningOnHold(30);
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x0060F7FC File Offset: 0x0060D9FC
		private static void GetInvasionStatus(out int currentWave, out int requiredKillCount, out int currentKillCount, bool currentlyInCheckProgress = false)
		{
			currentWave = NPC.waveNumber;
			requiredKillCount = 10;
			currentKillCount = (int)NPC.waveKills;
			int ongoingDifficulty = DD2Event.OngoingDifficulty;
			if (ongoingDifficulty == 2)
			{
				requiredKillCount = DD2Event.Difficulty_2_GetRequiredWaveKills(ref currentWave, ref currentKillCount, currentlyInCheckProgress);
				return;
			}
			if (ongoingDifficulty == 3)
			{
				requiredKillCount = DD2Event.Difficulty_3_GetRequiredWaveKills(ref currentWave, ref currentKillCount, currentlyInCheckProgress);
				return;
			}
			requiredKillCount = DD2Event.Difficulty_1_GetRequiredWaveKills(ref currentWave, ref currentKillCount, currentlyInCheckProgress);
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x0060F84C File Offset: 0x0060DA4C
		private static short[] GetEnemiesForWave(int wave)
		{
			int ongoingDifficulty = DD2Event.OngoingDifficulty;
			if (ongoingDifficulty == 2)
			{
				return DD2Event.Difficulty_2_GetEnemiesForWave(wave);
			}
			if (ongoingDifficulty == 3)
			{
				return DD2Event.Difficulty_3_GetEnemiesForWave(wave);
			}
			return DD2Event.Difficulty_1_GetEnemiesForWave(wave);
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x0060F87C File Offset: 0x0060DA7C
		private static int GetMonsterPointsWorth(int slainMonsterID)
		{
			int ongoingDifficulty = DD2Event.OngoingDifficulty;
			if (ongoingDifficulty == 2)
			{
				return DD2Event.Difficulty_2_GetMonsterPointsWorth(slainMonsterID);
			}
			if (ongoingDifficulty == 3)
			{
				return DD2Event.Difficulty_3_GetMonsterPointsWorth(slainMonsterID);
			}
			return DD2Event.Difficulty_1_GetMonsterPointsWorth(slainMonsterID);
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x0060F8AC File Offset: 0x0060DAAC
		public static void SpawnMonsterFromGate(Vector2 gateBottom)
		{
			int ongoingDifficulty = DD2Event.OngoingDifficulty;
			if (ongoingDifficulty == 2)
			{
				DD2Event.Difficulty_2_SpawnMonsterFromGate(gateBottom);
				return;
			}
			if (ongoingDifficulty == 3)
			{
				DD2Event.Difficulty_3_SpawnMonsterFromGate(gateBottom);
				return;
			}
			DD2Event.Difficulty_1_SpawnMonsterFromGate(gateBottom);
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x0060F8DC File Offset: 0x0060DADC
		public static void SummonCrystal(int x, int y, int whoAsks)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendData(113, -1, -1, null, x, (float)y, 0f, 0f, 0, 0, 0);
				return;
			}
			DD2Event.SummonCrystalDirect(x, y, whoAsks);
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x0060F914 File Offset: 0x0060DB14
		public unsafe static void SummonCrystalDirect(int x, int y, int whoAsks)
		{
			if (!NPC.AnyNPCs(548))
			{
				Tile tileSafely = Framing.GetTileSafely(x, y);
				if (tileSafely.active() && *tileSafely.type == 466)
				{
					Point point;
					point..ctor(x * 16, y * 16);
					point.X -= (int)(*tileSafely.frameX / 18 * 16);
					point.Y -= (int)(*tileSafely.frameY / 18 * 16);
					point.X += 40;
					point.Y += 64;
					DD2Event.StartInvasion(-1);
					NPC.NewNPC(Main.player[whoAsks].GetNPCSource_TileInteraction(x, y), point.X, point.Y, 548, 0, 0f, 0f, 0f, 0f, 255);
					DD2Event.DropStarterCrystals();
				}
			}
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x0060F9FC File Offset: 0x0060DBFC
		public static bool WouldFailSpawningHere(int x, int y)
		{
			Point xLeftEnd;
			Point xRightEnd;
			StrayMethods.CheckArenaScore(new Point(x, y).ToWorldCoordinates(8f, 8f), out xLeftEnd, out xRightEnd, 5, 10);
			int num3 = xRightEnd.X - x;
			int num2 = x - xLeftEnd.X;
			return num3 < 60 || num2 < 60;
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x0060FA48 File Offset: 0x0060DC48
		public static void FailureMessage(int client)
		{
			LocalizedText text = Language.GetText("DungeonDefenders2.BartenderWarning");
			Color color;
			color..ctor(255, 255, 0);
			if (Main.netMode == 2)
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromKey(text.Key, Array.Empty<object>()), color, client);
				return;
			}
			Main.NewText(text.Value, color.R, color.G, color.B);
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x0060FAB4 File Offset: 0x0060DCB4
		public static void WipeEntities()
		{
			DD2Event.ClearAllTowersInGame();
			DD2Event.ClearAllDD2HostilesInGame();
			DD2Event.ClearAllDD2EnergyCrystalsInChests();
			if (Main.netMode == 2)
			{
				NetMessage.SendData(114, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x0060FAF8 File Offset: 0x0060DCF8
		public static void ClearAllTowersInGame()
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && ProjectileID.Sets.IsADD2Turret[Main.projectile[i].type])
				{
					Main.projectile[i].Kill();
				}
			}
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x0060FB44 File Offset: 0x0060DD44
		public static void ClearAllDD2HostilesInGame()
		{
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && NPCID.Sets.BelongsToInvasionOldOnesArmy[Main.npc[i].type])
				{
					Main.npc[i].active = false;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x0060FBB8 File Offset: 0x0060DDB8
		public static void ClearAllDD2EnergyCrystalsInGame()
		{
			for (int i = 0; i < 400; i++)
			{
				Item item = Main.item[i];
				if (item.active && item.type == 3822)
				{
					item.active = false;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x0060FC20 File Offset: 0x0060DE20
		public static void ClearAllDD2EnergyCrystalsInChests()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			List<int> currentlyOpenChests = Chest.GetCurrentlyOpenChests();
			for (int i = 0; i < 8000; i++)
			{
				Chest chest = Main.chest[i];
				if (chest != null && currentlyOpenChests.Contains(i))
				{
					for (int j = 0; j < 40; j++)
					{
						if (chest.item[j].type == 3822 && chest.item[j].stack > 0)
						{
							chest.item[j].TurnToAir(false);
							if (Main.netMode != 0)
							{
								NetMessage.SendData(32, -1, -1, null, i, (float)j, 0f, 0f, 0, 0, 0);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x0060FCC0 File Offset: 0x0060DEC0
		public static void AnnounceGoblinDeath(NPC n)
		{
			DD2Event._deadGoblinSpots.Add(n.Bottom);
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x0060FCD4 File Offset: 0x0060DED4
		public static bool CanRaiseGoblinsHere(Vector2 spot)
		{
			int num = 0;
			using (List<Vector2>.Enumerator enumerator = DD2Event._deadGoblinSpots.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (Vector2.DistanceSquared(enumerator.Current, spot) <= 640000f)
					{
						num++;
						if (num >= 3)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x0060FD3C File Offset: 0x0060DF3C
		public static void RaiseGoblins(NPC caller, Vector2 spot)
		{
			List<Vector2> list = new List<Vector2>();
			foreach (Vector2 deadGoblinSpot in DD2Event._deadGoblinSpots)
			{
				if (Vector2.DistanceSquared(deadGoblinSpot, spot) <= 722500f)
				{
					list.Add(deadGoblinSpot);
				}
			}
			foreach (Vector2 item in list)
			{
				DD2Event._deadGoblinSpots.Remove(item);
			}
			int num = 0;
			foreach (Vector2 vec in list)
			{
				Point origin = vec.ToTileCoordinates();
				origin.X += Main.rand.Next(-15, 16);
				Point result;
				if (WorldUtils.Find(origin, Searches.Chain(new Searches.Down(50), new GenCondition[]
				{
					new Conditions.IsSolid()
				}), out result))
				{
					if (DD2Event.OngoingDifficulty == 3)
					{
						NPC.NewNPC(caller.GetSpawnSourceForNPCFromNPCAI(), result.X * 16 + 8, result.Y * 16, 567, 0, 0f, 0f, 0f, 0f, 255);
					}
					else
					{
						NPC.NewNPC(caller.GetSpawnSourceForNPCFromNPCAI(), result.X * 16 + 8, result.Y * 16, 566, 0, 0f, 0f, 0f, 0f, 255);
					}
					if (++num >= 8)
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x0060FF04 File Offset: 0x0060E104
		public static void FindArenaHitbox()
		{
			if (DD2Event._arenaHitboxingCooldown > 0)
			{
				DD2Event._arenaHitboxingCooldown--;
				return;
			}
			DD2Event._arenaHitboxingCooldown = 60;
			Vector2 vector;
			vector..ctor(float.MaxValue, float.MaxValue);
			Vector2 vector2;
			vector2..ctor(0f, 0f);
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.active && (nPC.type == 549 || nPC.type == 548))
				{
					Vector2 topLeft = nPC.TopLeft;
					if (vector.X > topLeft.X)
					{
						vector.X = topLeft.X;
					}
					if (vector.Y > topLeft.Y)
					{
						vector.Y = topLeft.Y;
					}
					topLeft = nPC.BottomRight;
					if (vector2.X < topLeft.X)
					{
						vector2.X = topLeft.X;
					}
					if (vector2.Y < topLeft.Y)
					{
						vector2.Y = topLeft.Y;
					}
				}
			}
			Vector2 vector3 = new Vector2(16f, 16f) * 50f;
			vector -= vector3;
			vector2 += vector3;
			Vector2 vector4 = vector2 - vector;
			DD2Event.ArenaHitbox.X = (int)vector.X;
			DD2Event.ArenaHitbox.Y = (int)vector.Y;
			DD2Event.ArenaHitbox.Width = (int)vector4.X;
			DD2Event.ArenaHitbox.Height = (int)vector4.Y;
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x0061009A File Offset: 0x0060E29A
		public static bool ShouldBlockBuilding(Vector2 worldPosition)
		{
			return DD2Event.ArenaHitbox.Contains(worldPosition.ToPoint());
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x006100AC File Offset: 0x0060E2AC
		public static void DropMedals(int numberOfMedals)
		{
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 548)
				{
					Main.npc[i].DropItemInstanced(Main.npc[i].position, Main.npc[i].Size, 3817, numberOfMedals, false);
				}
			}
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x00610118 File Offset: 0x0060E318
		public static bool ShouldDropCrystals()
		{
			int currentWave;
			int requiredKillCount;
			int currentKillCount;
			DD2Event.GetInvasionStatus(out currentWave, out requiredKillCount, out currentKillCount, false);
			if (DD2Event._crystalsDropping_lastWave < currentWave)
			{
				DD2Event._crystalsDropping_lastWave++;
				if (DD2Event._crystalsDropping_alreadyDropped > 0)
				{
					DD2Event._crystalsDropping_alreadyDropped -= DD2Event._crystalsDropping_toDrop;
				}
				if (DD2Event.OngoingDifficulty == 1)
				{
					switch (currentWave)
					{
					case 1:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 2:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 3:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					case 4:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					case 5:
						DD2Event._crystalsDropping_toDrop = 40;
						break;
					}
				}
				else if (DD2Event.OngoingDifficulty == 2)
				{
					switch (currentWave)
					{
					case 1:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 2:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 3:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 4:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 5:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 6:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					case 7:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					}
				}
				else if (DD2Event.OngoingDifficulty == 3)
				{
					switch (currentWave)
					{
					case 1:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 2:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 3:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 4:
						DD2Event._crystalsDropping_toDrop = 20;
						break;
					case 5:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					case 6:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					case 7:
						DD2Event._crystalsDropping_toDrop = 30;
						break;
					}
				}
			}
			if (Main.netMode != 0 && Main.expertMode)
			{
				DD2Event._crystalsDropping_toDrop = (int)((float)DD2Event._crystalsDropping_toDrop * NPC.GetBalance());
			}
			float num = (float)currentKillCount / (float)requiredKillCount;
			if ((float)DD2Event._crystalsDropping_alreadyDropped < (float)DD2Event._crystalsDropping_toDrop * num)
			{
				DD2Event._crystalsDropping_alreadyDropped++;
				return true;
			}
			return false;
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x006102F4 File Offset: 0x0060E4F4
		private static void SummonBetsy()
		{
			if (!DD2Event._spawnedBetsyT3 && !NPC.AnyNPCs(551))
			{
				Vector2 position;
				position..ctor(1f, 1f);
				int num = NPC.FindFirstNPC(548);
				if (num != -1)
				{
					position = Main.npc[num].Center;
				}
				NPC.SpawnOnPlayer((int)Player.FindClosest(position, 1, 1), 551);
				DD2Event._spawnedBetsyT3 = true;
			}
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x0061035C File Offset: 0x0060E55C
		private static void DropStarterCrystals()
		{
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 548)
				{
					for (int j = 0; j < 5; j++)
					{
						Item.NewItem(new EntitySource_WorldEvent(null), Main.npc[i].position, Main.npc[i].width, Main.npc[i].height, 3822, 2, false, 0, false, false);
					}
					return;
				}
			}
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x006103E4 File Offset: 0x0060E5E4
		private static void SetEnemySpawningOnHold(int forHowLong)
		{
			DD2Event._timeLeftUntilSpawningBegins = forHowLong;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(116, -1, -1, null, DD2Event._timeLeftUntilSpawningBegins, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x00610420 File Offset: 0x0060E620
		private static short[] Difficulty_1_GetEnemiesForWave(int wave)
		{
			DD2Event.LaneSpawnRate = 60;
			switch (wave)
			{
			case 1:
				DD2Event.LaneSpawnRate = 90;
				return new short[]
				{
					552
				};
			case 2:
				return new short[]
				{
					552,
					555
				};
			case 3:
				DD2Event.LaneSpawnRate = 55;
				return new short[]
				{
					552,
					555,
					561
				};
			case 4:
				DD2Event.LaneSpawnRate = 50;
				return new short[]
				{
					552,
					555,
					561,
					558
				};
			case 5:
				DD2Event.LaneSpawnRate = 40;
				return new short[]
				{
					552,
					555,
					561,
					558,
					564
				};
			default:
				return new short[]
				{
					552
				};
			}
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x006104D8 File Offset: 0x0060E6D8
		private static int Difficulty_1_GetRequiredWaveKills(ref int waveNumber, ref int currentKillCount, bool currentlyInCheckProgress)
		{
			switch (waveNumber)
			{
			case -1:
				return 0;
			case 1:
				return 60;
			case 2:
				return 80;
			case 3:
				return 100;
			case 4:
				DD2Event._deadGoblinSpots.Clear();
				return 120;
			case 5:
				if (!DD2Event._downedDarkMageT1 && currentKillCount > 139)
				{
					currentKillCount = 139;
				}
				return 140;
			case 6:
				waveNumber = 5;
				currentKillCount = 1;
				if (currentlyInCheckProgress)
				{
					DD2Event.StartVictoryScene();
				}
				return 1;
			}
			return 10;
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x0061055C File Offset: 0x0060E75C
		private static void Difficulty_1_SpawnMonsterFromGate(Vector2 gateBottom)
		{
			int x = (int)gateBottom.X;
			int y = (int)gateBottom.Y;
			int num = 50;
			int num2 = 6;
			if (NPC.waveNumber > 4)
			{
				num2 = 12;
			}
			else if (NPC.waveNumber > 3)
			{
				num2 = 8;
			}
			int num3 = 6;
			if (NPC.waveNumber > 4)
			{
				num3 = 8;
			}
			for (int i = 1; i < Main.CurrentFrameFlags.ActivePlayersCount; i++)
			{
				num = (int)((double)num * 1.3);
				num2 = (int)((double)num2 * 1.3);
				num3 = (int)((double)num3 * 1.3);
			}
			int num4 = 200;
			switch (NPC.waveNumber)
			{
			case 1:
				if (NPC.CountNPCS(552) + NPC.CountNPCS(555) < num)
				{
					num4 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 552, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 2:
				if (NPC.CountNPCS(552) + NPC.CountNPCS(555) < num)
				{
					num4 = ((Main.rand.Next(7) != 0) ? NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 552, 0, 0f, 0f, 0f, 0f, 255) : NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 555, 0, 0f, 0f, 0f, 0f, 255));
				}
				break;
			case 3:
				if (Main.rand.Next(6) == 0 && NPC.CountNPCS(561) < num2)
				{
					num4 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 561, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(552) + NPC.CountNPCS(555) < num)
				{
					num4 = ((Main.rand.Next(5) != 0) ? NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 552, 0, 0f, 0f, 0f, 0f, 255) : NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 555, 0, 0f, 0f, 0f, 0f, 255));
				}
				break;
			case 4:
				if (Main.rand.Next(12) == 0 && NPC.CountNPCS(558) < num3)
				{
					num4 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 558, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(5) == 0 && NPC.CountNPCS(561) < num2)
				{
					num4 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 561, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(552) + NPC.CountNPCS(555) < num)
				{
					num4 = ((Main.rand.Next(5) != 0) ? NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 552, 0, 0f, 0f, 0f, 0f, 255) : NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 555, 0, 0f, 0f, 0f, 0f, 255));
				}
				break;
			case 5:
			{
				int num5;
				int requiredKillCount;
				int currentKillCount;
				DD2Event.GetInvasionStatus(out num5, out requiredKillCount, out currentKillCount, false);
				if ((float)currentKillCount > (float)requiredKillCount * 0.5f && !NPC.AnyNPCs(564))
				{
					num4 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 564, 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(10) == 0 && NPC.CountNPCS(558) < num3)
				{
					num4 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 558, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(4) == 0 && NPC.CountNPCS(561) < num2)
				{
					num4 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 561, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(552) + NPC.CountNPCS(555) < num)
				{
					num4 = ((Main.rand.Next(4) != 0) ? NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 552, 0, 0f, 0f, 0f, 0f, 255) : NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 555, 0, 0f, 0f, 0f, 0f, 255));
				}
				break;
			}
			default:
				num4 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 552, 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			if (Main.netMode == 2 && num4 < 200)
			{
				NetMessage.SendData(23, -1, -1, null, num4, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x00610AA8 File Offset: 0x0060ECA8
		private static int Difficulty_1_GetMonsterPointsWorth(int slainMonsterID)
		{
			if (NPC.waveNumber == 5 && NPC.waveKills >= 139f)
			{
				if (slainMonsterID == 564 || slainMonsterID == 565)
				{
					DD2Event._downedDarkMageT1 = true;
					return 1;
				}
				return 0;
			}
			else
			{
				if (slainMonsterID - 551 > 14 && slainMonsterID - 568 > 10)
				{
					return 0;
				}
				if (NPC.waveNumber == 5 && NPC.waveKills == 138f)
				{
					return 1;
				}
				if (!Main.expertMode)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x00610B1C File Offset: 0x0060ED1C
		private static short[] Difficulty_2_GetEnemiesForWave(int wave)
		{
			DD2Event.LaneSpawnRate = 60;
			switch (wave)
			{
			case 1:
				DD2Event.LaneSpawnRate = 90;
				return new short[]
				{
					553,
					562
				};
			case 2:
				DD2Event.LaneSpawnRate = 70;
				return new short[]
				{
					553,
					562,
					572
				};
			case 3:
				return new short[]
				{
					553,
					556,
					562,
					559,
					572
				};
			case 4:
				DD2Event.LaneSpawnRate = 55;
				return new short[]
				{
					553,
					559,
					570,
					572,
					562
				};
			case 5:
				DD2Event.LaneSpawnRate = 50;
				return new short[]
				{
					553,
					556,
					559,
					572,
					574,
					570
				};
			case 6:
				DD2Event.LaneSpawnRate = 45;
				return new short[]
				{
					553,
					556,
					562,
					559,
					568,
					570,
					572,
					574
				};
			case 7:
				DD2Event.LaneSpawnRate = 42;
				return new short[]
				{
					553,
					556,
					572,
					559,
					568,
					574,
					570,
					576
				};
			default:
				return new short[]
				{
					553
				};
			}
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x00610C14 File Offset: 0x0060EE14
		private static int Difficulty_2_GetRequiredWaveKills(ref int waveNumber, ref int currentKillCount, bool currentlyInCheckProgress)
		{
			switch (waveNumber)
			{
			case -1:
				return 0;
			case 1:
				return 60;
			case 2:
				return 80;
			case 3:
				return 100;
			case 4:
				return 120;
			case 5:
				return 140;
			case 6:
				return 180;
			case 7:
				if (!DD2Event._downedOgreT2 && currentKillCount > 219)
				{
					currentKillCount = 219;
				}
				return 220;
			case 8:
				waveNumber = 7;
				currentKillCount = 1;
				if (currentlyInCheckProgress)
				{
					DD2Event.StartVictoryScene();
				}
				return 1;
			}
			return 10;
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x00610CA0 File Offset: 0x0060EEA0
		private static int Difficulty_2_GetMonsterPointsWorth(int slainMonsterID)
		{
			if (NPC.waveNumber == 7 && NPC.waveKills >= 219f)
			{
				if (slainMonsterID == 576 || slainMonsterID == 577)
				{
					DD2Event._downedOgreT2 = true;
					return 1;
				}
				return 0;
			}
			else
			{
				if (slainMonsterID - 551 > 14 && slainMonsterID - 568 > 10)
				{
					return 0;
				}
				if (NPC.waveNumber == 7 && NPC.waveKills == 218f)
				{
					return 1;
				}
				if (!Main.expertMode)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x00610D14 File Offset: 0x0060EF14
		private static void Difficulty_2_SpawnMonsterFromGate(Vector2 gateBottom)
		{
			int x = (int)gateBottom.X;
			int y = (int)gateBottom.Y;
			int num = 50;
			int num2 = 5;
			if (NPC.waveNumber > 1)
			{
				num2 = 8;
			}
			if (NPC.waveNumber > 3)
			{
				num2 = 10;
			}
			if (NPC.waveNumber > 5)
			{
				num2 = 12;
			}
			int num3 = 5;
			if (NPC.waveNumber > 4)
			{
				num3 = 7;
			}
			int num4 = 2;
			int num5 = 8;
			if (NPC.waveNumber > 3)
			{
				num5 = 12;
			}
			int num6 = 3;
			if (NPC.waveNumber > 5)
			{
				num6 = 5;
			}
			for (int i = 1; i < Main.CurrentFrameFlags.ActivePlayersCount; i++)
			{
				num = (int)((double)num * 1.3);
				num2 = (int)((double)num2 * 1.3);
				num5 = (int)((double)num * 1.3);
				num6 = (int)((double)num * 1.35);
			}
			int num7 = 200;
			int num8 = 200;
			switch (NPC.waveNumber)
			{
			case 1:
				if (Main.rand.Next(20) == 0 && NPC.CountNPCS(562) < num2)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 562, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) < num)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 2:
				if (Main.rand.Next(3) == 0 && NPC.CountNPCS(572) < num5)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 572, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(8) == 0 && NPC.CountNPCS(562) < num2)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 562, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) < num)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 3:
				if (Main.rand.Next(7) == 0 && NPC.CountNPCS(572) < num5)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 572, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(10) == 0 && NPC.CountNPCS(559) < num3)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 559, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(8) == 0 && NPC.CountNPCS(562) < num2)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 562, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) + NPC.CountNPCS(556) < num)
				{
					if (Main.rand.Next(4) == 0)
					{
						num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 556, 0, 0f, 0f, 0f, 0f, 255);
					}
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 4:
				if (Main.rand.Next(10) == 0 && NPC.CountNPCS(570) < num6)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 570, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(12) == 0 && NPC.CountNPCS(559) < num3)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 559, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(6) == 0 && NPC.CountNPCS(562) < num2)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 562, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(3) == 0 && NPC.CountNPCS(572) < num5)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 572, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) < num)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 5:
				if (Main.rand.Next(7) == 0 && NPC.CountNPCS(570) < num6)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 570, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(10) == 0 && NPC.CountNPCS(559) < num3)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 559, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(4) == 0 && NPC.CountNPCS(572) + NPC.CountNPCS(574) < num5)
				{
					num7 = ((Main.rand.Next(2) != 0) ? NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 574, 0, 0f, 0f, 0f, 0f, 255) : NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 572, 0, 0f, 0f, 0f, 0f, 255));
				}
				else if (NPC.CountNPCS(553) + NPC.CountNPCS(556) < num)
				{
					if (Main.rand.Next(3) == 0)
					{
						num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 556, 0, 0f, 0f, 0f, 0f, 255);
					}
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 6:
				if (Main.rand.Next(7) == 0 && NPC.CountNPCS(570) < num6)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 570, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(17) == 0 && NPC.CountNPCS(568) < num4)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 568, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(5) == 0 && NPC.CountNPCS(572) + NPC.CountNPCS(574) < num5)
				{
					num7 = ((Main.rand.Next(2) == 0) ? NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 574, 0, 0f, 0f, 0f, 0f, 255) : NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 572, 0, 0f, 0f, 0f, 0f, 255));
				}
				else if (Main.rand.Next(9) == 0 && NPC.CountNPCS(559) < num3)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 559, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(3) == 0 && NPC.CountNPCS(562) < num2)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 562, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) + NPC.CountNPCS(556) < num)
				{
					if (Main.rand.Next(3) != 0)
					{
						num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 556, 0, 0f, 0f, 0f, 0f, 255);
					}
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 7:
			{
				int num9;
				int requiredKillCount;
				int currentKillCount;
				DD2Event.GetInvasionStatus(out num9, out requiredKillCount, out currentKillCount, false);
				if ((float)currentKillCount > (float)requiredKillCount * 0.1f && !NPC.AnyNPCs(576))
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 576, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(570) < num6)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 570, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(17) == 0 && NPC.CountNPCS(568) < num4)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 568, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(572) + NPC.CountNPCS(574) < num5)
				{
					num7 = ((Main.rand.Next(3) == 0) ? NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 574, 0, 0f, 0f, 0f, 0f, 255) : NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 572, 0, 0f, 0f, 0f, 0f, 255));
				}
				else if (Main.rand.Next(11) == 0 && NPC.CountNPCS(559) < num3)
				{
					num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 559, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(553) + NPC.CountNPCS(556) < num)
				{
					if (Main.rand.Next(2) == 0)
					{
						num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 556, 0, 0f, 0f, 0f, 0f, 255);
					}
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 553, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			}
			default:
				num7 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 553, 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			if (Main.netMode == 2 && num7 < 200)
			{
				NetMessage.SendData(23, -1, -1, null, num7, 0f, 0f, 0f, 0, 0, 0);
			}
			if (Main.netMode == 2 && num8 < 200)
			{
				NetMessage.SendData(23, -1, -1, null, num8, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x00611950 File Offset: 0x0060FB50
		private static short[] Difficulty_3_GetEnemiesForWave(int wave)
		{
			DD2Event.LaneSpawnRate = 60;
			switch (wave)
			{
			case 1:
				DD2Event.LaneSpawnRate = 85;
				return new short[]
				{
					554,
					557,
					563
				};
			case 2:
				DD2Event.LaneSpawnRate = 75;
				return new short[]
				{
					554,
					557,
					563,
					573,
					578
				};
			case 3:
				DD2Event.LaneSpawnRate = 60;
				return new short[]
				{
					554,
					563,
					560,
					573,
					571
				};
			case 4:
				DD2Event.LaneSpawnRate = 60;
				return new short[]
				{
					554,
					560,
					571,
					573,
					563,
					575,
					565
				};
			case 5:
				DD2Event.LaneSpawnRate = 55;
				return new short[]
				{
					554,
					557,
					573,
					575,
					571,
					569,
					577
				};
			case 6:
				DD2Event.LaneSpawnRate = 60;
				return new short[]
				{
					554,
					557,
					563,
					578,
					569,
					571,
					577,
					565
				};
			case 7:
				DD2Event.LaneSpawnRate = 90;
				return new short[]
				{
					554,
					557,
					563,
					569,
					571,
					551
				};
			default:
				return new short[]
				{
					554
				};
			}
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x00611A4C File Offset: 0x0060FC4C
		private static int Difficulty_3_GetRequiredWaveKills(ref int waveNumber, ref int currentKillCount, bool currentlyInCheckProgress)
		{
			switch (waveNumber)
			{
			case -1:
				return 0;
			case 1:
				return 60;
			case 2:
				return 80;
			case 3:
				return 100;
			case 4:
				return 120;
			case 5:
				return 140;
			case 6:
				return 180;
			case 7:
			{
				int num = NPC.FindFirstNPC(551);
				if (num == -1)
				{
					return 1;
				}
				currentKillCount = 100 - (int)((float)Main.npc[num].life / (float)Main.npc[num].lifeMax * 100f);
				return 100;
			}
			case 8:
				waveNumber = 7;
				currentKillCount = 1;
				if (currentlyInCheckProgress)
				{
					DD2Event.StartVictoryScene();
				}
				return 1;
			}
			return 10;
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x00611AF5 File Offset: 0x0060FCF5
		private static int Difficulty_3_GetMonsterPointsWorth(int slainMonsterID)
		{
			if (NPC.waveNumber == 7)
			{
				if (slainMonsterID == 551)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (slainMonsterID - 551 > 14 && slainMonsterID - 568 > 10)
				{
					return 0;
				}
				if (!Main.expertMode)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x00611B30 File Offset: 0x0060FD30
		private static void Difficulty_3_SpawnMonsterFromGate(Vector2 gateBottom)
		{
			int x = (int)gateBottom.X;
			int y = (int)gateBottom.Y;
			int num = 60;
			int num2 = 7;
			if (NPC.waveNumber > 1)
			{
				num2 = 9;
			}
			if (NPC.waveNumber > 3)
			{
				num2 = 12;
			}
			if (NPC.waveNumber > 5)
			{
				num2 = 15;
			}
			int num3 = 7;
			if (NPC.waveNumber > 4)
			{
				num3 = 10;
			}
			int num4 = 2;
			if (NPC.waveNumber > 5)
			{
				num4 = 3;
			}
			int num5 = 12;
			if (NPC.waveNumber > 3)
			{
				num5 = 18;
			}
			int num6 = 4;
			if (NPC.waveNumber > 5)
			{
				num6 = 6;
			}
			int num7 = 4;
			for (int i = 1; i < Main.CurrentFrameFlags.ActivePlayersCount; i++)
			{
				num = (int)((double)num * 1.3);
				num2 = (int)((double)num2 * 1.3);
				num5 = (int)((double)num * 1.3);
				num6 = (int)((double)num * 1.35);
				num7 = (int)((double)num7 * 1.3);
			}
			int num8 = 200;
			int num9 = 200;
			switch (NPC.waveNumber)
			{
			case 1:
				if (Main.rand.Next(18) == 0 && NPC.CountNPCS(563) < num2)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(554) < num)
				{
					if (Main.rand.Next(7) == 0)
					{
						num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 557, 0, 0f, 0f, 0f, 0f, 255);
					}
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 2:
				if (Main.rand.Next(3) == 0 && NPC.CountNPCS(578) < num7)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 578, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(563) < num2)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(3) == 0 && NPC.CountNPCS(573) < num5)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 573, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(554) < num)
				{
					if (Main.rand.Next(4) == 0)
					{
						num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 557, 0, 0f, 0f, 0f, 0f, 255);
					}
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 3:
				if (Main.rand.Next(13) == 0 && NPC.CountNPCS(571) < num6)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 571, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(573) < num5)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 573, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(10) == 0 && NPC.CountNPCS(560) < num3)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 560, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(8) == 0 && NPC.CountNPCS(563) < num2)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(554) + NPC.CountNPCS(557) < num)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 4:
				if (Main.rand.Next(24) == 0 && !NPC.AnyNPCs(565))
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 565, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(12) == 0 && NPC.CountNPCS(571) < num6)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 571, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(15) == 0 && NPC.CountNPCS(560) < num3)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 560, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(563) < num2)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(5) == 0 && NPC.CountNPCS(573) + NPC.CountNPCS(575) < num5)
				{
					num8 = ((Main.rand.Next(3) == 0) ? NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 575, 0, 0f, 0f, 0f, 0f, 255) : NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 573, 0, 0f, 0f, 0f, 0f, 255));
				}
				else if (NPC.CountNPCS(554) < num)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 5:
				if (Main.rand.Next(20) == 0 && !NPC.AnyNPCs(577))
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 577, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(17) == 0 && NPC.CountNPCS(569) < num4)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 569, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(8) == 0 && NPC.CountNPCS(571) < num6)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 571, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(573) + NPC.CountNPCS(575) < num5)
				{
					num8 = ((Main.rand.Next(4) == 0) ? NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 575, 0, 0f, 0f, 0f, 0f, 255) : NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 573, 0, 0f, 0f, 0f, 0f, 255));
				}
				else if (NPC.CountNPCS(554) + NPC.CountNPCS(557) < num)
				{
					if (Main.rand.Next(3) == 0)
					{
						num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 557, 0, 0f, 0f, 0f, 0f, 255);
					}
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 6:
				if (Main.rand.Next(20) == 0 && !NPC.AnyNPCs(577))
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 577, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(20) == 0 && !NPC.AnyNPCs(565))
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 565, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(12) == 0 && NPC.CountNPCS(571) < num6)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 571, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(25) == 0 && NPC.CountNPCS(569) < num4)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 569, 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(7) == 0 && NPC.CountNPCS(578) < num7)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 578, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(7) == 0 && NPC.CountNPCS(573) + NPC.CountNPCS(575) < num5)
				{
					num8 = ((Main.rand.Next(3) == 0) ? NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 575, 0, 0f, 0f, 0f, 0f, 255) : NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 573, 0, 0f, 0f, 0f, 0f, 255));
				}
				else if (Main.rand.Next(5) == 0 && NPC.CountNPCS(563) < num2)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(554) + NPC.CountNPCS(557) < num)
				{
					if (Main.rand.Next(3) == 0)
					{
						num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 557, 0, 0f, 0f, 0f, 0f, 255);
					}
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			case 7:
				if (Main.rand.Next(20) == 0 && NPC.CountNPCS(571) < num6)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 571, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(17) == 0 && NPC.CountNPCS(569) < num4)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 569, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (Main.rand.Next(10) == 0 && NPC.CountNPCS(563) < num2)
				{
					num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 563, 0, 0f, 0f, 0f, 0f, 255);
				}
				else if (NPC.CountNPCS(554) + NPC.CountNPCS(557) < num)
				{
					if (Main.rand.Next(5) == 0)
					{
						num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 557, 0, 0f, 0f, 0f, 0f, 255);
					}
					num9 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 554, 0, 0f, 0f, 0f, 0f, 255);
				}
				break;
			default:
				num8 = NPC.NewNPC(DD2Event.GetSpawnSource_OldOnesArmy(), x, y, 554, 0, 0f, 0f, 0f, 0f, 255);
				break;
			}
			if (Main.netMode == 2 && num8 < 200)
			{
				NetMessage.SendData(23, -1, -1, null, num8, 0f, 0f, 0f, 0, 0, 0);
			}
			if (Main.netMode == 2 && num9 < 200)
			{
				NetMessage.SendData(23, -1, -1, null, num9, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x006128E4 File Offset: 0x00610AE4
		public static bool IsStandActive(int x, int y)
		{
			Vector2 target;
			target..ctor((float)(x * 16 + 8), (float)(y * 16 + 8));
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC != null && nPC.active && nPC.type == 548)
				{
					return nPC.Bottom.Distance(target) < 36f;
				}
			}
			return false;
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x0061294C File Offset: 0x00610B4C
		public static void RequestToSkipWaitTime(int x, int y)
		{
			if (DD2Event.TimeLeftBetweenWaves > 60 && DD2Event.IsStandActive(x, y))
			{
				SoundEngine.PlaySound(new SoundStyle?(SoundID.NPCDeath7), x * 16 + 8, y * 16 + 8);
				if (Main.netMode == 0)
				{
					DD2Event.AttemptToSkipWaitTime();
					return;
				}
				if (Main.netMode != 2)
				{
					NetMessage.SendData(143, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06004564 RID: 17764 RVA: 0x006129BC File Offset: 0x00610BBC
		public static void AttemptToSkipWaitTime()
		{
			if (Main.netMode != 1 && DD2Event.TimeLeftBetweenWaves > 60)
			{
				DD2Event.SetEnemySpawningOnHold(60);
			}
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x006129D6 File Offset: 0x00610BD6
		private static IEntitySource GetSpawnSource_OldOnesArmy()
		{
			return new EntitySource_OldOnesArmy(null);
		}

		// Token: 0x04005AD6 RID: 23254
		private static readonly Color INFO_NEW_WAVE_COLOR = new Color(175, 55, 255);

		// Token: 0x04005AD7 RID: 23255
		private static readonly Color INFO_START_INVASION_COLOR = new Color(50, 255, 130);

		// Token: 0x04005AD8 RID: 23256
		private static readonly Color INFO_FAILURE_INVASION_COLOR = new Color(255, 0, 0);

		// Token: 0x04005AD9 RID: 23257
		private const int INVASION_ID = 3;

		// Token: 0x04005ADA RID: 23258
		public static bool DownedInvasionT1;

		// Token: 0x04005ADB RID: 23259
		public static bool DownedInvasionT2;

		// Token: 0x04005ADC RID: 23260
		public static bool DownedInvasionT3;

		// Token: 0x04005ADD RID: 23261
		public static bool LostThisRun;

		// Token: 0x04005ADE RID: 23262
		public static bool WonThisRun;

		// Token: 0x04005ADF RID: 23263
		public static int LaneSpawnRate = 60;

		// Token: 0x04005AE0 RID: 23264
		private static bool _downedDarkMageT1;

		// Token: 0x04005AE1 RID: 23265
		private static bool _downedOgreT2;

		// Token: 0x04005AE2 RID: 23266
		private static bool _spawnedBetsyT3;

		// Token: 0x04005AE3 RID: 23267
		public static bool Ongoing;

		// Token: 0x04005AE4 RID: 23268
		public static Rectangle ArenaHitbox;

		// Token: 0x04005AE5 RID: 23269
		private static int _arenaHitboxingCooldown;

		// Token: 0x04005AE6 RID: 23270
		public static int OngoingDifficulty;

		// Token: 0x04005AE7 RID: 23271
		private static List<Vector2> _deadGoblinSpots = new List<Vector2>();

		// Token: 0x04005AE8 RID: 23272
		private static int _crystalsDropping_lastWave;

		// Token: 0x04005AE9 RID: 23273
		private static int _crystalsDropping_toDrop;

		// Token: 0x04005AEA RID: 23274
		private static int _crystalsDropping_alreadyDropped;

		// Token: 0x04005AEB RID: 23275
		private static int _timeLeftUntilSpawningBegins;
	}
}
