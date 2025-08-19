using System;
using System.Collections;
using System.IO;

namespace Terraria.ModLoader
{
	// Token: 0x02000148 RID: 328
	public class BiomeLoader : Loader<ModBiome>
	{
		// Token: 0x06001ADF RID: 6879 RVA: 0x004CDDD2 File Offset: 0x004CBFD2
		internal void SetupPlayer(Player player)
		{
			player.modBiomeFlags = new BitArray(this.list.Count);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x004CDDEC File Offset: 0x004CBFEC
		public void UpdateBiomes(Player player)
		{
			for (int i = 0; i < player.modBiomeFlags.Length; i++)
			{
				bool prev = player.modBiomeFlags[i];
				bool value = player.modBiomeFlags[i] = this.list[i].IsBiomeActive(player);
				if (!prev && value)
				{
					this.list[i].OnEnter(player);
				}
				else if (!value && prev)
				{
					this.list[i].OnLeave(player);
				}
				if (value)
				{
					this.list[i].OnInBiome(player);
				}
			}
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x004CDE8C File Offset: 0x004CC08C
		public static bool CustomBiomesMatch(Player player, Player other)
		{
			for (int i = 0; i < player.modBiomeFlags.Length; i++)
			{
				if (player.modBiomeFlags[i] != other.modBiomeFlags[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x004CDECC File Offset: 0x004CC0CC
		public static void CopyCustomBiomesTo(Player player, Player other)
		{
			other.modBiomeFlags = (BitArray)player.modBiomeFlags.Clone();
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x004CDEE4 File Offset: 0x004CC0E4
		public static void SendCustomBiomes(Player player, BinaryWriter writer)
		{
			Utils.SendBitArray(player.modBiomeFlags, writer);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x004CDEF2 File Offset: 0x004CC0F2
		public static void ReceiveCustomBiomes(Player player, BinaryReader reader)
		{
			player.modBiomeFlags = Utils.ReceiveBitArray(player.modBiomeFlags.Length, reader);
		}
	}
}
