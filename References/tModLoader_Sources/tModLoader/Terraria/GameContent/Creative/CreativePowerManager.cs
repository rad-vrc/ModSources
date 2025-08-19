using System;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000640 RID: 1600
	public class CreativePowerManager
	{
		// Token: 0x0600464B RID: 17995 RVA: 0x00630F87 File Offset: 0x0062F187
		private CreativePowerManager()
		{
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x00630FA8 File Offset: 0x0062F1A8
		public void Register<T>(string nameInServerConfig) where T : ICreativePower, new()
		{
			T val = CreativePowerManager.PowerTypeStorage<T>.Power = Activator.CreateInstance<T>();
			CreativePowerManager.PowerTypeStorage<T>.Id = this._powersCount;
			CreativePowerManager.PowerTypeStorage<T>.Name = nameInServerConfig;
			val.DefaultPermissionLevel = PowerPermissionLevel.CanBeChangedByEveryone;
			val.CurrentPermissionLevel = PowerPermissionLevel.CanBeChangedByEveryone;
			this._powersById[this._powersCount] = val;
			this._powersByName[nameInServerConfig] = val;
			val.PowerId = this._powersCount;
			val.ServerConfigName = nameInServerConfig;
			this._powersCount += 1;
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x00631047 File Offset: 0x0062F247
		public T GetPower<T>() where T : ICreativePower
		{
			return CreativePowerManager.PowerTypeStorage<T>.Power;
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x0063104E File Offset: 0x0062F24E
		public ushort GetPowerId<T>() where T : ICreativePower
		{
			return CreativePowerManager.PowerTypeStorage<T>.Id;
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x00631055 File Offset: 0x0062F255
		public bool TryGetPower(ushort id, out ICreativePower power)
		{
			return this._powersById.TryGetValue(id, out power);
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x00631064 File Offset: 0x0062F264
		public static void TryListingPermissionsFrom(string line)
		{
			int length = "journeypermission_".Length;
			if (line.Length < length || !line.ToLower().StartsWith("journeypermission_"))
			{
				return;
			}
			string[] array = line.Substring(length).Split('=', StringSplitOptions.None);
			int result;
			if (array.Length == 2 && int.TryParse(array[1].Trim(), out result))
			{
				PowerPermissionLevel powerPermissionLevel = (PowerPermissionLevel)Utils.Clamp<int>(result, 0, 2);
				string key = array[0].Trim().ToLower();
				CreativePowerManager.Initialize();
				ICreativePower value;
				if (CreativePowerManager.Instance._powersByName.TryGetValue(key, out value))
				{
					value.DefaultPermissionLevel = powerPermissionLevel;
					value.CurrentPermissionLevel = powerPermissionLevel;
				}
			}
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x00631104 File Offset: 0x0062F304
		public static void Initialize()
		{
			if (!CreativePowerManager._initialized)
			{
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
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x00631204 File Offset: 0x0062F404
		public void Reset()
		{
			foreach (KeyValuePair<ushort, ICreativePower> item in this._powersById)
			{
				item.Value.CurrentPermissionLevel = item.Value.DefaultPermissionLevel;
				IPersistentPerWorldContent persistentPerWorldContent = item.Value as IPersistentPerWorldContent;
				if (persistentPerWorldContent != null)
				{
					persistentPerWorldContent.Reset();
				}
				IPersistentPerPlayerContent persistentPerPlayerContent = item.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.Reset();
				}
			}
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x00631294 File Offset: 0x0062F494
		public void SaveToWorld(BinaryWriter writer)
		{
			foreach (KeyValuePair<ushort, ICreativePower> item in this._powersById)
			{
				IPersistentPerWorldContent persistentPerWorldContent = item.Value as IPersistentPerWorldContent;
				if (persistentPerWorldContent != null)
				{
					writer.Write(true);
					writer.Write(item.Key);
					persistentPerWorldContent.Save(writer);
				}
			}
			writer.Write(false);
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x00631314 File Offset: 0x0062F514
		public void LoadFromWorld(BinaryReader reader, int versionGameWasLastSavedOn)
		{
			while (reader.ReadBoolean())
			{
				ushort key = reader.ReadUInt16();
				ICreativePower value;
				if (!this._powersById.TryGetValue(key, out value))
				{
					break;
				}
				IPersistentPerWorldContent persistentPerWorldContent = value as IPersistentPerWorldContent;
				if (persistentPerWorldContent == null)
				{
					break;
				}
				persistentPerWorldContent.Load(reader, versionGameWasLastSavedOn);
			}
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x00631354 File Offset: 0x0062F554
		public void ValidateWorld(BinaryReader reader, int versionGameWasLastSavedOn)
		{
			while (reader.ReadBoolean())
			{
				ushort key = reader.ReadUInt16();
				ICreativePower value;
				if (!this._powersById.TryGetValue(key, out value))
				{
					break;
				}
				IPersistentPerWorldContent persistentPerWorldContent = value as IPersistentPerWorldContent;
				if (persistentPerWorldContent == null)
				{
					break;
				}
				persistentPerWorldContent.ValidateWorld(reader, versionGameWasLastSavedOn);
			}
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x00631394 File Offset: 0x0062F594
		public void SyncThingsToJoiningPlayer(int playerIndex)
		{
			foreach (KeyValuePair<ushort, ICreativePower> item in this._powersById)
			{
				NetPacket packet = NetCreativePowerPermissionsModule.SerializeCurrentPowerPermissionLevel(item.Key, (int)item.Value.CurrentPermissionLevel);
				NetManager.Instance.SendToClient(packet, playerIndex);
			}
			foreach (KeyValuePair<ushort, ICreativePower> item2 in this._powersById)
			{
				IOnPlayerJoining onPlayerJoining = item2.Value as IOnPlayerJoining;
				if (onPlayerJoining != null)
				{
					onPlayerJoining.OnPlayerJoining(playerIndex);
				}
			}
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x0063145C File Offset: 0x0062F65C
		public void SaveToPlayer(Player player, BinaryWriter writer)
		{
			foreach (KeyValuePair<ushort, ICreativePower> item in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = item.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					writer.Write(true);
					writer.Write(item.Key);
					persistentPerPlayerContent.Save(player, writer);
				}
			}
			writer.Write(false);
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x006314DC File Offset: 0x0062F6DC
		public void LoadToPlayer(Player player, BinaryReader reader, int versionGameWasLastSavedOn)
		{
			while (reader.ReadBoolean())
			{
				ushort key = reader.ReadUInt16();
				ICreativePower value;
				if (!this._powersById.TryGetValue(key, out value))
				{
					break;
				}
				IPersistentPerPlayerContent persistentPerPlayerContent = value as IPersistentPerPlayerContent;
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

		// Token: 0x06004659 RID: 18009 RVA: 0x00631530 File Offset: 0x0062F730
		public void ApplyLoadedDataToPlayer(Player player)
		{
			foreach (KeyValuePair<ushort, ICreativePower> item in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = item.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.ApplyLoadedDataToOutOfPlayerFields(player);
				}
			}
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x00631594 File Offset: 0x0062F794
		public void ResetPowersForPlayer(Player player)
		{
			foreach (KeyValuePair<ushort, ICreativePower> item in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = item.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.ResetDataForNewPlayer(player);
				}
			}
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x006315F8 File Offset: 0x0062F7F8
		public void ResetDataForNewPlayer(Player player)
		{
			foreach (KeyValuePair<ushort, ICreativePower> item in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = item.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.Reset();
					persistentPerPlayerContent.ResetDataForNewPlayer(player);
				}
			}
		}

		// Token: 0x04005B6B RID: 23403
		public static readonly CreativePowerManager Instance = new CreativePowerManager();

		// Token: 0x04005B6C RID: 23404
		private Dictionary<ushort, ICreativePower> _powersById = new Dictionary<ushort, ICreativePower>();

		// Token: 0x04005B6D RID: 23405
		private Dictionary<string, ICreativePower> _powersByName = new Dictionary<string, ICreativePower>();

		// Token: 0x04005B6E RID: 23406
		private ushort _powersCount;

		// Token: 0x04005B6F RID: 23407
		private static bool _initialized = false;

		// Token: 0x04005B70 RID: 23408
		private const string _powerPermissionsLineHeader = "journeypermission_";

		// Token: 0x02000CDB RID: 3291
		private class PowerTypeStorage<T> where T : ICreativePower
		{
			// Token: 0x04007A45 RID: 31301
			public static ushort Id;

			// Token: 0x04007A46 RID: 31302
			public static string Name;

			// Token: 0x04007A47 RID: 31303
			public static T Power;
		}
	}
}
