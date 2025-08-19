using System;
using System.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002BD RID: 701
	internal class ConsumedStatIncreasesPlayer : ModPlayer
	{
		// Token: 0x06002D61 RID: 11617 RVA: 0x0052E06C File Offset: 0x0052C26C
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ConsumedStatIncreasesPlayer.NetHandler.SendConsumedState(toWho, base.Player);
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x0052E07C File Offset: 0x0052C27C
		public override void CopyClientState(ModPlayer targetCopy)
		{
			Player source = base.Player;
			Player player = targetCopy.Player;
			player.ConsumedLifeCrystals = source.ConsumedLifeCrystals;
			player.ConsumedLifeFruit = source.ConsumedLifeFruit;
			player.ConsumedManaCrystals = source.ConsumedManaCrystals;
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x0052E0BC File Offset: 0x0052C2BC
		public override void SendClientChanges(ModPlayer clientPlayer)
		{
			Player player = base.Player;
			Player client = clientPlayer.Player;
			if (player.ConsumedLifeCrystals != client.ConsumedLifeCrystals || player.ConsumedLifeFruit != client.ConsumedLifeFruit || player.ConsumedManaCrystals != client.ConsumedManaCrystals)
			{
				ConsumedStatIncreasesPlayer.NetHandler.SendConsumedState(-1, player);
			}
		}

		// Token: 0x02000A74 RID: 2676
		internal static class NetHandler
		{
			// Token: 0x060058FC RID: 22780 RVA: 0x006A0768 File Offset: 0x0069E968
			public static void SendConsumedState(int toClient, Player player)
			{
				ModPacket packet = ModLoaderMod.GetPacket(1);
				packet.Write(1);
				if (Main.netMode == 2)
				{
					packet.Write((byte)player.whoAmI);
				}
				packet.Write((byte)player.ConsumedLifeCrystals);
				packet.Write((byte)player.ConsumedLifeFruit);
				packet.Write((byte)player.ConsumedManaCrystals);
				packet.Send(toClient, player.whoAmI);
			}

			// Token: 0x060058FD RID: 22781 RVA: 0x006A07CC File Offset: 0x0069E9CC
			private static void HandleConsumedState(BinaryReader reader, int sender)
			{
				if (Main.netMode == 1)
				{
					sender = (int)reader.ReadByte();
				}
				Player player = Main.player[sender];
				player.ConsumedLifeCrystals = (int)reader.ReadByte();
				player.ConsumedLifeFruit = (int)reader.ReadByte();
				player.ConsumedManaCrystals = (int)reader.ReadByte();
				if (Main.netMode == 2)
				{
					ConsumedStatIncreasesPlayer.NetHandler.SendConsumedState(-1, player);
				}
			}

			// Token: 0x060058FE RID: 22782 RVA: 0x006A0824 File Offset: 0x0069EA24
			public static void HandlePacket(BinaryReader reader, int sender)
			{
				if (reader.ReadByte() == 1)
				{
					ConsumedStatIncreasesPlayer.NetHandler.HandleConsumedState(reader, sender);
				}
			}

			// Token: 0x04006D1E RID: 27934
			public const byte SyncConsumedProperties = 1;
		}
	}
}
