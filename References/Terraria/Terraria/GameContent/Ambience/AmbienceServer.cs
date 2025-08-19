using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.GameContent.Ambience
{
	// Token: 0x0200032D RID: 813
	public class AmbienceServer
	{
		// Token: 0x060024DE RID: 9438 RVA: 0x00565D2B File Offset: 0x00563F2B
		private static bool IsSunnyDay()
		{
			return !Main.IsItRaining && Main.dayTime && !Main.eclipse;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x00565D45 File Offset: 0x00563F45
		private static bool IsSunset()
		{
			return Main.dayTime && Main.time > 40500.0;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00565D60 File Offset: 0x00563F60
		private static bool IsCalmNight()
		{
			return !Main.IsItRaining && !Main.dayTime && !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00565D88 File Offset: 0x00563F88
		public AmbienceServer()
		{
			this.ResetSpawnTime();
			this._spawnConditions[SkyEntityType.BirdsV] = new Func<bool>(AmbienceServer.IsSunnyDay);
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

		// Token: 0x060024E2 RID: 9442 RVA: 0x005661F3 File Offset: 0x005643F3
		private bool IsPlayerAtRightHeightForType(SkyEntityType type, Player plr)
		{
			if (type == SkyEntityType.Hellbats)
			{
				return AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceHell(plr);
			}
			return AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceSky(plr);
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x00566208 File Offset: 0x00564408
		public void Update()
		{
			this.SpawnForcedEntities();
			if (this._updatesUntilNextAttempt > 0)
			{
				this._updatesUntilNextAttempt -= Main.dayRate;
				return;
			}
			this.ResetSpawnTime();
			IEnumerable<SkyEntityType> source = from pair in this._spawnConditions
			where pair.Value()
			select pair.Key;
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
			if (num == 0)
			{
				return;
			}
			SkyEntityType type2 = source2.ElementAt(Main.rand.Next(num));
			this.SpawnForPlayer(player, type2);
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0056637D File Offset: 0x0056457D
		public void ResetSpawnTime()
		{
			this._updatesUntilNextAttempt = Main.rand.Next(600, 7200);
			if (Main.tenthAnniversaryWorld)
			{
				this._updatesUntilNextAttempt /= 2;
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x005663AE File Offset: 0x005645AE
		public void ForceEntitySpawn(AmbienceServer.AmbienceSpawnInfo info)
		{
			this._forcedSpawns.Add(info);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x005663BC File Offset: 0x005645BC
		private void SpawnForcedEntities()
		{
			if (this._forcedSpawns.Count == 0)
			{
				return;
			}
			for (int i = this._forcedSpawns.Count - 1; i >= 0; i--)
			{
				AmbienceServer.AmbienceSpawnInfo ambienceSpawnInfo = this._forcedSpawns[i];
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
				this._forcedSpawns.RemoveAt(i);
			}
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00566448 File Offset: 0x00564648
		private static void FindPlayerThatCanSeeBackgroundAmbience(out Player player)
		{
			player = null;
			int num = Main.player.Count((Player plr) => plr.active && AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbience(plr));
			if (num == 0)
			{
				return;
			}
			player = (from plr in Main.player
			where plr.active && AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbience(plr)
			select plr).ElementAt(Main.rand.Next(num));
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x005664C1 File Offset: 0x005646C1
		private static bool IsPlayerInAPlaceWhereTheyCanSeeAmbience(Player plr)
		{
			return AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceSky(plr) || AmbienceServer.IsPlayerInAPlaceWhereTheyCanSeeAmbienceHell(plr);
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x005664D3 File Offset: 0x005646D3
		private static bool IsPlayerInAPlaceWhereTheyCanSeeAmbienceSky(Player plr)
		{
			return (double)plr.position.Y <= Main.worldSurface * 16.0 + 1600.0;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x005664FF File Offset: 0x005646FF
		private static bool IsPlayerInAPlaceWhereTheyCanSeeAmbienceHell(Player plr)
		{
			return plr.position.Y >= (float)((Main.UnderworldLayer - 100) * 16);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0056651D File Offset: 0x0056471D
		private void SpawnForPlayer(Player player, SkyEntityType type)
		{
			NetManager.Instance.BroadcastOrLoopback(NetAmbienceModule.SerializeSkyEntitySpawn(player, type));
		}

		// Token: 0x040048DB RID: 18651
		private const int MINIMUM_SECONDS_BETWEEN_SPAWNS = 10;

		// Token: 0x040048DC RID: 18652
		private const int MAXIMUM_SECONDS_BETWEEN_SPAWNS = 120;

		// Token: 0x040048DD RID: 18653
		private readonly Dictionary<SkyEntityType, Func<bool>> _spawnConditions = new Dictionary<SkyEntityType, Func<bool>>();

		// Token: 0x040048DE RID: 18654
		private readonly Dictionary<SkyEntityType, Func<Player, bool>> _secondarySpawnConditionsPerPlayer = new Dictionary<SkyEntityType, Func<Player, bool>>();

		// Token: 0x040048DF RID: 18655
		private int _updatesUntilNextAttempt;

		// Token: 0x040048E0 RID: 18656
		private List<AmbienceServer.AmbienceSpawnInfo> _forcedSpawns = new List<AmbienceServer.AmbienceSpawnInfo>();

		// Token: 0x02000715 RID: 1813
		public struct AmbienceSpawnInfo
		{
			// Token: 0x04006306 RID: 25350
			public SkyEntityType skyEntityType;

			// Token: 0x04006307 RID: 25351
			public int targetPlayer;
		}
	}
}
