using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.GameContent.Ambience
{
	// Token: 0x020006B4 RID: 1716
	public class AmbienceServer
	{
		// Token: 0x0600488F RID: 18575 RVA: 0x00649EC8 File Offset: 0x006480C8
		private static bool IsSunnyDay()
		{
			return !Main.IsItRaining && Main.dayTime && !Main.eclipse;
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x00649EE2 File Offset: 0x006480E2
		private static bool IsSunset()
		{
			return Main.dayTime && Main.time > 40500.0;
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x00649EFD File Offset: 0x006480FD
		private static bool IsCalmNight()
		{
			return !Main.IsItRaining && !Main.dayTime && !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon;
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x00649F28 File Offset: 0x00648128
		public AmbienceServer()
		{
			this.ResetSpawnTime();
			Dictionary<SkyEntityType, Func<bool>> spawnConditions = this._spawnConditions;
			SkyEntityType key = SkyEntityType.BirdsV;
			Func<bool> value;
			if ((value = AmbienceServer.<>O.<0>__IsSunnyDay) == null)
			{
				value = (AmbienceServer.<>O.<0>__IsSunnyDay = new Func<bool>(AmbienceServer.IsSunnyDay));
			}
			spawnConditions[key] = value;
			this._spawnConditions[SkyEntityType.Wyvern] = (() => AmbienceServer.IsSunnyDay() && Main.hardMode);
			this._spawnConditions[SkyEntityType.Airship] = (() => AmbienceServer.IsSunnyDay() && Main.IsItAHappyWindyDay);
			this._spawnConditions[SkyEntityType.AirBalloon] = (() => AmbienceServer.IsSunnyDay() && !Main.IsItAHappyWindyDay);
			this._spawnConditions[SkyEntityType.Eyeball] = (() => !Main.dayTime);
			this._spawnConditions[SkyEntityType.Butterflies] = (() => AmbienceServer.IsSunnyDay() && !Main.IsItAHappyWindyDay && !NPC.TooWindyForButterflies && NPC.butterflyChance < 6);
			this._spawnConditions[SkyEntityType.LostKite] = (() => Main.dayTime && !Main.eclipse && Main.IsItAHappyWindyDay);
			this._spawnConditions[SkyEntityType.Vulture] = (() => AmbienceServer.IsSunnyDay());
			this._spawnConditions[SkyEntityType.Bats] = (() => (AmbienceServer.IsSunset() && AmbienceServer.IsSunnyDay()) || AmbienceServer.IsCalmNight());
			this._spawnConditions[SkyEntityType.PixiePosse] = (() => AmbienceServer.IsSunnyDay() || AmbienceServer.IsCalmNight());
			this._spawnConditions[SkyEntityType.Seagulls] = (() => AmbienceServer.IsSunnyDay());
			this._spawnConditions[SkyEntityType.SlimeBalloons] = (() => AmbienceServer.IsSunnyDay() && Main.IsItAHappyWindyDay);
			this._spawnConditions[SkyEntityType.Gastropods] = (() => AmbienceServer.IsCalmNight());
			this._spawnConditions[SkyEntityType.Pegasus] = (() => AmbienceServer.IsSunnyDay());
			this._spawnConditions[SkyEntityType.EaterOfSouls] = (() => AmbienceServer.IsSunnyDay() || AmbienceServer.IsCalmNight());
			this._spawnConditions[SkyEntityType.Crimera] = (() => AmbienceServer.IsSunnyDay() || AmbienceServer.IsCalmNight());
			this._spawnConditions[SkyEntityType.Hellbats] = (() => true);
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Vulture] = ((Player player) => player.ZoneDesert);
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.PixiePosse] = ((Player player) => player.ZoneHallow);
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Seagulls] = ((Player player) => player.ZoneBeach);
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Gastropods] = ((Player player) => player.ZoneHallow);
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Pegasus] = ((Player player) => player.ZoneHallow);
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.EaterOfSouls] = ((Player player) => player.ZoneCorrupt);
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Crimera] = ((Player player) => player.ZoneCrimson);
			this._secondarySpawnConditionsPerPlayer[SkyEntityType.Bats] = ((Player player) => player.ZoneJungle);
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x0064A3A2 File Offset: 0x006485A2
		private bool IsPlayerAtRightHeightForType(SkyEntityType type, Player plr)
		{
			if (type == SkyEntityType.Hellbats)
			{
				return AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceHell(plr);
			}
			return AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceSky(plr);
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x0064A3B8 File Offset: 0x006485B8
		public void Update()
		{
			this.SpawnForcedEntities();
			if (this._updatesUntilNextAttempt > 0.0)
			{
				this._updatesUntilNextAttempt -= Main.desiredWorldEventsUpdateRate;
				return;
			}
			this.ResetSpawnTime();
			IEnumerable<SkyEntityType> source = this._spawnConditions.Where(delegate(KeyValuePair<SkyEntityType, Func<bool>> pair)
			{
				KeyValuePair<SkyEntityType, Func<bool>> keyValuePair = pair;
				return keyValuePair.Value();
			}).Select(delegate(KeyValuePair<SkyEntityType, Func<bool>> pair)
			{
				KeyValuePair<SkyEntityType, Func<bool>> keyValuePair = pair;
				return keyValuePair.Key;
			});
			if (source.Count((SkyEntityType type) => true) == 0)
			{
				return;
			}
			Player player;
			AmbienceServer.FindPlayerThatCanSeeBackgroundAmbience(out player);
			if (player == null)
			{
				return;
			}
			IEnumerable<SkyEntityType> source2 = from type in source
			where this.IsPlayerAtRightHeightForType(type, player) && this._secondarySpawnConditionsPerPlayer.ContainsKey(type) && this._secondarySpawnConditionsPerPlayer[type](player)
			select type;
			int num = source2.Count((SkyEntityType type) => true);
			if (num == 0 || Main.rand.Next(5) < 3)
			{
				source2 = from type in source
				where this.IsPlayerAtRightHeightForType(type, player) && (!this._secondarySpawnConditionsPerPlayer.ContainsKey(type) || this._secondarySpawnConditionsPerPlayer[type](player))
				select type;
				num = source2.Count((SkyEntityType type) => true);
			}
			if (num != 0)
			{
				SkyEntityType type2 = source2.ElementAt(Main.rand.Next(num));
				this.SpawnForPlayer(player, type2);
			}
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x0064A534 File Offset: 0x00648734
		public void ResetSpawnTime()
		{
			this._updatesUntilNextAttempt = (double)Main.rand.Next(600, 7200);
			if (Main.tenthAnniversaryWorld)
			{
				this._updatesUntilNextAttempt /= 2.0;
			}
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x0064A56E File Offset: 0x0064876E
		public void ForceEntitySpawn(AmbienceServer.AmbienceSpawnInfo info)
		{
			this._forcedSpawns.Add(info);
		}

		// Token: 0x06004897 RID: 18583 RVA: 0x0064A57C File Offset: 0x0064877C
		private void SpawnForcedEntities()
		{
			if (this._forcedSpawns.Count == 0)
			{
				return;
			}
			for (int num = this._forcedSpawns.Count - 1; num >= 0; num--)
			{
				AmbienceServer.AmbienceSpawnInfo ambienceSpawnInfo = this._forcedSpawns[num];
				Player player;
				if (ambienceSpawnInfo.targetPlayer == -1)
				{
					AmbienceServer.FindPlayerThatCanSeeBackgroundAmbience(out player);
				}
				else
				{
					player = Main.player[ambienceSpawnInfo.targetPlayer];
				}
				if (player != null && this.IsPlayerAtRightHeightForType(ambienceSpawnInfo.skyEntityType, player))
				{
					this.SpawnForPlayer(player, ambienceSpawnInfo.skyEntityType);
				}
				this._forcedSpawns.RemoveAt(num);
			}
		}

		// Token: 0x06004898 RID: 18584 RVA: 0x0064A608 File Offset: 0x00648808
		private static void FindPlayerThatCanSeeBackgroundAmbience(out Player player)
		{
			player = null;
			int num = Main.player.Count((Player plr) => plr.active && AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbience(plr));
			if (num != 0)
			{
				player = (from plr in Main.player
				where plr.active && AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbience(plr)
				select plr).ElementAt(Main.rand.Next(num));
			}
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x0064A680 File Offset: 0x00648880
		private static bool IsPlayerInAPlaceWhereTheyCanSeeAmbience(Player plr)
		{
			return AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceSky(plr) || AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceHell(plr);
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x0064A692 File Offset: 0x00648892
		private static bool IsPlayerInAPlaceWhereTheyCanSeeAmbienceSky(Player plr)
		{
			return (double)plr.position.Y <= Main.worldSurface * 16.0 + 1600.0;
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x0064A6BE File Offset: 0x006488BE
		private static bool IsPlayerInAPlaceWhereTheyCanSeeAmbienceHell(Player plr)
		{
			return plr.position.Y >= (float)((Main.UnderworldLayer - 100) * 16);
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x0064A6DC File Offset: 0x006488DC
		private void SpawnForPlayer(Player player, SkyEntityType type)
		{
			NetManager.Instance.BroadcastOrLoopback(NetAmbienceModule.SerializeSkyEntitySpawn(player, type));
		}

		// Token: 0x04005C49 RID: 23625
		private const int MINIMUM_SECONDS_BETWEEN_SPAWNS = 10;

		// Token: 0x04005C4A RID: 23626
		private const int MAXIMUM_SECONDS_BETWEEN_SPAWNS = 120;

		// Token: 0x04005C4B RID: 23627
		private readonly Dictionary<SkyEntityType, Func<bool>> _spawnConditions = new Dictionary<SkyEntityType, Func<bool>>();

		// Token: 0x04005C4C RID: 23628
		private readonly Dictionary<SkyEntityType, Func<Player, bool>> _secondarySpawnConditionsPerPlayer = new Dictionary<SkyEntityType, Func<Player, bool>>();

		// Token: 0x04005C4D RID: 23629
		private double _updatesUntilNextAttempt;

		// Token: 0x04005C4E RID: 23630
		private List<AmbienceServer.AmbienceSpawnInfo> _forcedSpawns = new List<AmbienceServer.AmbienceSpawnInfo>();

		// Token: 0x02000D48 RID: 3400
		public struct AmbienceSpawnInfo
		{
			// Token: 0x04007B54 RID: 31572
			public SkyEntityType skyEntityType;

			// Token: 0x04007B55 RID: 31573
			public int targetPlayer;
		}

		// Token: 0x02000D49 RID: 3401
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B56 RID: 31574
			public static Func<bool> <0>__IsSunnyDay;
		}
	}
}
