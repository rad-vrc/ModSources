using System;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.GameContent.Creative
{
	// Token: 0x020002B6 RID: 694
	public class CreativePowerManager
	{
		// Token: 0x06002223 RID: 8739 RVA: 0x00541AF7 File Offset: 0x0053FCF7
		private CreativePowerManager()
		{
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00541B18 File Offset: 0x0053FD18
		public void Register<T>(string nameInServerConfig) where T : ICreativePower, new()
		{
			T t = Activator.CreateInstance<T>();
			CreativePowerManager.PowerTypeStorage<T>.Power = t;
			CreativePowerManager.PowerTypeStorage<T>.Id = this._powersCount;
			CreativePowerManager.PowerTypeStorage<T>.Name = nameInServerConfig;
			t.DefaultPermissionLevel = PowerPermissionLevel.CanBeChangedByEveryone;
			t.CurrentPermissionLevel = PowerPermissionLevel.CanBeChangedByEveryone;
			this._powersById[this._powersCount] = t;
			this._powersByName[nameInServerConfig] = t;
			t.PowerId = this._powersCount;
			t.ServerConfigName = nameInServerConfig;
			this._powersCount += 1;
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00541BB7 File Offset: 0x0053FDB7
		public T GetPower<T>() where T : ICreativePower
		{
			return CreativePowerManager.PowerTypeStorage<T>.Power;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x00541BBE File Offset: 0x0053FDBE
		public ushort GetPowerId<T>() where T : ICreativePower
		{
			return CreativePowerManager.PowerTypeStorage<T>.Id;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00541BC5 File Offset: 0x0053FDC5
		public bool TryGetPower(ushort id, out ICreativePower power)
		{
			return this._powersById.TryGetValue(id, out power);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00541BD4 File Offset: 0x0053FDD4
		public static void TryListingPermissionsFrom(string line)
		{
			int length = "journeypermission_".Length;
			if (line.Length < length)
			{
				return;
			}
			if (!line.ToLower().StartsWith("journeypermission_"))
			{
				return;
			}
			string[] array = line.Substring(length).Split(new char[]
			{
				'='
			});
			if (array.Length != 2)
			{
				return;
			}
			int value;
			if (!int.TryParse(array[1].Trim(), out value))
			{
				return;
			}
			PowerPermissionLevel powerPermissionLevel = (PowerPermissionLevel)Utils.Clamp<int>(value, 0, 2);
			string key = array[0].Trim().ToLower();
			CreativePowerManager.Initialize();
			ICreativePower creativePower;
			if (!CreativePowerManager.Instance._powersByName.TryGetValue(key, out creativePower))
			{
				return;
			}
			creativePower.DefaultPermissionLevel = powerPermissionLevel;
			creativePower.CurrentPermissionLevel = powerPermissionLevel;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00541C80 File Offset: 0x0053FE80
		public static void Initialize()
		{
			if (CreativePowerManager._initialized)
			{
				return;
			}
			CreativePowerManager.Instance.Register<CreativePowers.FreezeTime>("time_setfrozen");
			CreativePowerManager.Instance.Register<CreativePowers.StartDayImmediately>("time_setdawn");
			CreativePowerManager.Instance.Register<CreativePowers.StartNoonImmediately>("time_setnoon");
			CreativePowerManager.Instance.Register<CreativePowers.StartNightImmediately>("time_setdusk");
			CreativePowerManager.Instance.Register<CreativePowers.StartMidnightImmediately>("time_setmidnight");
			CreativePowerManager.Instance.Register<CreativePowers.GodmodePower>("godmode");
			CreativePowerManager.Instance.Register<CreativePowers.ModifyWindDirectionAndStrength>("wind_setstrength");
			CreativePowerManager.Instance.Register<CreativePowers.ModifyRainPower>("rain_setstrength");
			CreativePowerManager.Instance.Register<CreativePowers.ModifyTimeRate>("time_setspeed");
			CreativePowerManager.Instance.Register<CreativePowers.FreezeRainPower>("rain_setfrozen");
			CreativePowerManager.Instance.Register<CreativePowers.FreezeWindDirectionAndStrength>("wind_setfrozen");
			CreativePowerManager.Instance.Register<CreativePowers.FarPlacementRangePower>("increaseplacementrange");
			CreativePowerManager.Instance.Register<CreativePowers.DifficultySliderPower>("setdifficulty");
			CreativePowerManager.Instance.Register<CreativePowers.StopBiomeSpreadPower>("biomespread_setfrozen");
			CreativePowerManager.Instance.Register<CreativePowers.SpawnRateSliderPerPlayerPower>("setspawnrate");
			CreativePowerManager._initialized = true;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x00541D7C File Offset: 0x0053FF7C
		public void Reset()
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				keyValuePair.Value.CurrentPermissionLevel = keyValuePair.Value.DefaultPermissionLevel;
				IPersistentPerWorldContent persistentPerWorldContent = keyValuePair.Value as IPersistentPerWorldContent;
				if (persistentPerWorldContent != null)
				{
					persistentPerWorldContent.Reset();
				}
				IPersistentPerPlayerContent persistentPerPlayerContent = keyValuePair.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.Reset();
				}
			}
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00541E0C File Offset: 0x0054000C
		public void SaveToWorld(BinaryWriter writer)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				IPersistentPerWorldContent persistentPerWorldContent = keyValuePair.Value as IPersistentPerWorldContent;
				if (persistentPerWorldContent != null)
				{
					writer.Write(true);
					writer.Write(keyValuePair.Key);
					persistentPerWorldContent.Save(writer);
				}
			}
			writer.Write(false);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00541E8C File Offset: 0x0054008C
		public void LoadFromWorld(BinaryReader reader, int versionGameWasLastSavedOn)
		{
			while (reader.ReadBoolean())
			{
				ushort key = reader.ReadUInt16();
				ICreativePower creativePower;
				if (!this._powersById.TryGetValue(key, out creativePower))
				{
					break;
				}
				IPersistentPerWorldContent persistentPerWorldContent = creativePower as IPersistentPerWorldContent;
				if (persistentPerWorldContent == null)
				{
					break;
				}
				persistentPerWorldContent.Load(reader, versionGameWasLastSavedOn);
			}
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x00541ECC File Offset: 0x005400CC
		public void ValidateWorld(BinaryReader reader, int versionGameWasLastSavedOn)
		{
			while (reader.ReadBoolean())
			{
				ushort key = reader.ReadUInt16();
				ICreativePower creativePower;
				if (!this._powersById.TryGetValue(key, out creativePower))
				{
					break;
				}
				IPersistentPerWorldContent persistentPerWorldContent = creativePower as IPersistentPerWorldContent;
				if (persistentPerWorldContent == null)
				{
					break;
				}
				persistentPerWorldContent.ValidateWorld(reader, versionGameWasLastSavedOn);
			}
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00541F0C File Offset: 0x0054010C
		public void SyncThingsToJoiningPlayer(int playerIndex)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				NetPacket packet = NetCreativePowerPermissionsModule.SerializeCurrentPowerPermissionLevel(keyValuePair.Key, (int)keyValuePair.Value.CurrentPermissionLevel);
				NetManager.Instance.SendToClient(packet, playerIndex);
			}
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair2 in this._powersById)
			{
				IOnPlayerJoining onPlayerJoining = keyValuePair2.Value as IOnPlayerJoining;
				if (onPlayerJoining != null)
				{
					onPlayerJoining.OnPlayerJoining(playerIndex);
				}
			}
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00541FD4 File Offset: 0x005401D4
		public void SaveToPlayer(Player player, BinaryWriter writer)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = keyValuePair.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					writer.Write(true);
					writer.Write(keyValuePair.Key);
					persistentPerPlayerContent.Save(player, writer);
				}
			}
			writer.Write(false);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00542054 File Offset: 0x00540254
		public void LoadToPlayer(Player player, BinaryReader reader, int versionGameWasLastSavedOn)
		{
			while (reader.ReadBoolean())
			{
				ushort key = reader.ReadUInt16();
				ICreativePower creativePower;
				if (!this._powersById.TryGetValue(key, out creativePower))
				{
					break;
				}
				IPersistentPerPlayerContent persistentPerPlayerContent = creativePower as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.Load(player, reader, versionGameWasLastSavedOn);
				}
			}
			if (player.difficulty != 3)
			{
				this.ResetPowersForPlayer(player);
			}
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x005420A8 File Offset: 0x005402A8
		public void ApplyLoadedDataToPlayer(Player player)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = keyValuePair.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.ApplyLoadedDataToOutOfPlayerFields(player);
				}
			}
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0054210C File Offset: 0x0054030C
		public void ResetPowersForPlayer(Player player)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = keyValuePair.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.ResetDataForNewPlayer(player);
				}
			}
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x00542170 File Offset: 0x00540370
		public void ResetDataForNewPlayer(Player player)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = keyValuePair.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.Reset();
					persistentPerPlayerContent.ResetDataForNewPlayer(player);
				}
			}
		}

		// Token: 0x040047B2 RID: 18354
		public static readonly CreativePowerManager Instance = new CreativePowerManager();

		// Token: 0x040047B3 RID: 18355
		private Dictionary<ushort, ICreativePower> _powersById = new Dictionary<ushort, ICreativePower>();

		// Token: 0x040047B4 RID: 18356
		private Dictionary<string, ICreativePower> _powersByName = new Dictionary<string, ICreativePower>();

		// Token: 0x040047B5 RID: 18357
		private ushort _powersCount;

		// Token: 0x040047B6 RID: 18358
		private static bool _initialized = false;

		// Token: 0x040047B7 RID: 18359
		private const string _powerPermissionsLineHeader = "journeypermission_";

		// Token: 0x02000696 RID: 1686
		private class PowerTypeStorage<T> where T : ICreativePower
		{
			// Token: 0x040061B6 RID: 25014
			public static ushort Id;

			// Token: 0x040061B7 RID: 25015
			public static string Name;

			// Token: 0x040061B8 RID: 25016
			public static T Power;
		}
	}
}
