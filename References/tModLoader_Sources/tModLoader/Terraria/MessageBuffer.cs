using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.GameContent.Golf;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;
using Terraria.Net;
using Terraria.Net.Sockets;
using Terraria.Testing;
using Terraria.UI;

namespace Terraria
{
	// Token: 0x0200003A RID: 58
	public class MessageBuffer
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600066F RID: 1647 RVA: 0x0013FBBC File Offset: 0x0013DDBC
		// (remove) Token: 0x06000670 RID: 1648 RVA: 0x0013FBF0 File Offset: 0x0013DDF0
		public static event TileChangeReceivedEvent OnTileChangeReceived;

		// Token: 0x06000671 RID: 1649 RVA: 0x0013FC24 File Offset: 0x0013DE24
		public void Reset()
		{
			Array.Clear(this.readBuffer, 0, this.readBuffer.Length);
			Array.Clear(this.writeBuffer, 0, this.writeBuffer.Length);
			this.writeLocked = false;
			this.messageLength = 0;
			this.totalData = 0;
			this.spamCount = 0;
			this.broadcast = false;
			this.checkBytes = false;
			this.ResetReader();
			this.ResetWriter();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0013FC8F File Offset: 0x0013DE8F
		public void ResetReader()
		{
			if (this.readerStream != null)
			{
				this.readerStream.Close();
			}
			this.readerStream = new MemoryStream(this.readBuffer);
			this.reader = new BinaryReader(this.readerStream);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0013FCC6 File Offset: 0x0013DEC6
		public void ResetWriter()
		{
			if (this.writerStream != null)
			{
				this.writerStream.Close();
			}
			this.writerStream = new MemoryStream(this.writeBuffer);
			this.writer = new BinaryWriter(this.writerStream);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0013FD00 File Offset: 0x0013DF00
		private float[] ReUseTemporaryProjectileAI()
		{
			for (int i = 0; i < this._temporaryProjectileAI.Length; i++)
			{
				this._temporaryProjectileAI[i] = 0f;
			}
			return this._temporaryProjectileAI;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0013FD34 File Offset: 0x0013DF34
		private float[] ReUseTemporaryNPCAI()
		{
			for (int i = 0; i < this._temporaryNPCAI.Length; i++)
			{
				this._temporaryNPCAI[i] = 0f;
			}
			return this._temporaryNPCAI;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0013FD68 File Offset: 0x0013DF68
		public unsafe void GetData(int start, int length, out int messageType)
		{
			if (this.whoAmI < 256)
			{
				Netplay.Clients[this.whoAmI].TimeOutTimer = 0;
			}
			else
			{
				Netplay.Connection.TimeOutTimer = 0;
			}
			byte b = 0;
			int num = start + 1;
			int num252 = messageType = (int)this.readBuffer[start];
			b = (byte)num252;
			if (ModNet.DetailedLogging)
			{
				int num253 = this.whoAmI;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 3);
				defaultInterpolatedStringHandler.AppendLiteral("GetData ");
				defaultInterpolatedStringHandler.AppendFormatted(MessageID.GetName((int)b));
				defaultInterpolatedStringHandler.AppendLiteral("(");
				defaultInterpolatedStringHandler.AppendFormatted<byte>(b);
				defaultInterpolatedStringHandler.AppendLiteral("), ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(length + 2);
				ModNet.Debug(num253, defaultInterpolatedStringHandler.ToStringAndClear());
			}
			Main.ActiveNetDiagnosticsUI.CountReadMessage((int)b, length);
			if (Main.netMode == 1 && Netplay.Connection.StatusMax > 0)
			{
				Netplay.Connection.StatusCount++;
			}
			if (Main.verboseNetplay)
			{
				for (int i = start; i < start + length; i++)
				{
				}
				for (int j = start; j < start + length; j++)
				{
					byte b16 = this.readBuffer[j];
				}
			}
			if (Main.netMode == 2 && b != 38 && Netplay.Clients[this.whoAmI].State == -1)
			{
				NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[1].ToNetworkText(), 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			if (Main.netMode == 2)
			{
				if (Netplay.Clients[this.whoAmI].State < 10 && b > 12 && b != 93 && b != 16 && b != 42 && b != 50 && b != 38 && b != 68 && b != 147 && b < 250)
				{
					NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
				}
				if (Netplay.Clients[this.whoAmI].State == 0 && b != 1)
				{
					NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
				}
			}
			if (this.reader == null)
			{
				this.ResetReader();
			}
			this.reader.BaseStream.Position = (long)num;
			if (ModNet.HijackGetData(ref b, ref this.reader, this.whoAmI))
			{
				return;
			}
			switch (b)
			{
			case 1:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				if (Main.dedServ && Netplay.IsBanned(Netplay.Clients[this.whoAmI].Socket.GetRemoteAddress()))
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[3].ToNetworkText(), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (Netplay.Clients[this.whoAmI].State != 0)
				{
					return;
				}
				string kickMsg;
				if (!ModNet.IsClientCompatible(this.reader.ReadString(), out ModNet.isModdedClient[this.whoAmI], out kickMsg))
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromLiteral(Lang.mp[4].Value + "\n(" + kickMsg + ")"), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (!string.IsNullOrEmpty(Netplay.ServerPassword))
				{
					Netplay.Clients[this.whoAmI].State = -1;
					NetMessage.TrySendData(37, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				Netplay.Clients[this.whoAmI].State = 1;
				if (ModNet.isModdedClient[this.whoAmI])
				{
					ModNet.SyncMods(this.whoAmI);
					return;
				}
				NetMessage.TrySendData(3, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			case 2:
				if (Main.netMode == 1)
				{
					Netplay.Disconnect = true;
					Main.statusText = NetworkText.Deserialize(this.reader).ToString();
					Main.menuMode = 14;
					return;
				}
				return;
			case 3:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				if (Netplay.Connection.State == 1)
				{
					Netplay.Connection.State = 2;
				}
				int num2 = (int)this.reader.ReadByte();
				bool value5 = this.reader.ReadBoolean();
				Netplay.Connection.ServerSpecialFlags[2] = value5;
				if (num2 != Main.myPlayer)
				{
					Main.player[num2] = Main.ActivePlayerFileData.Player;
					Main.player[Main.myPlayer] = new Player();
				}
				Main.player[num2].whoAmI = num2;
				Main.myPlayer = num2;
				Player player12 = Main.player[num2];
				NetMessage.TrySendData(4, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(68, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(16, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(42, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(50, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(147, -1, -1, null, num2, (float)player12.CurrentLoadoutIndex, 0f, 0f, 0, 0, 0);
				for (int num3 = 0; num3 < 59; num3++)
				{
					NetMessage.TrySendData(5, -1, -1, null, num2, (float)(PlayerItemSlotID.Inventory0 + num3), (float)player12.inventory[num3].prefix, 0f, 0, 0, 0);
				}
				MessageBuffer.TrySendingItemArray(num2, player12.armor, PlayerItemSlotID.Armor0);
				MessageBuffer.TrySendingItemArray(num2, player12.dye, PlayerItemSlotID.Dye0);
				MessageBuffer.TrySendingItemArray(num2, player12.miscEquips, PlayerItemSlotID.Misc0);
				MessageBuffer.TrySendingItemArray(num2, player12.miscDyes, PlayerItemSlotID.MiscDye0);
				MessageBuffer.TrySendingItemArray(num2, player12.bank.item, PlayerItemSlotID.Bank1_0);
				MessageBuffer.TrySendingItemArray(num2, player12.bank2.item, PlayerItemSlotID.Bank2_0);
				NetMessage.TrySendData(5, -1, -1, null, num2, (float)PlayerItemSlotID.TrashItem, (float)player12.trashItem.prefix, 0f, 0, 0, 0);
				MessageBuffer.TrySendingItemArray(num2, player12.bank3.item, PlayerItemSlotID.Bank3_0);
				MessageBuffer.TrySendingItemArray(num2, player12.bank4.item, PlayerItemSlotID.Bank4_0);
				MessageBuffer.TrySendingItemArray(num2, player12.Loadouts[0].Armor, PlayerItemSlotID.Loadout1_Armor_0);
				MessageBuffer.TrySendingItemArray(num2, player12.Loadouts[0].Dye, PlayerItemSlotID.Loadout1_Dye_0);
				MessageBuffer.TrySendingItemArray(num2, player12.Loadouts[1].Armor, PlayerItemSlotID.Loadout2_Armor_0);
				MessageBuffer.TrySendingItemArray(num2, player12.Loadouts[1].Dye, PlayerItemSlotID.Loadout2_Dye_0);
				MessageBuffer.TrySendingItemArray(num2, player12.Loadouts[2].Armor, PlayerItemSlotID.Loadout3_Armor_0);
				MessageBuffer.TrySendingItemArray(num2, player12.Loadouts[2].Dye, PlayerItemSlotID.Loadout3_Dye_0);
				PlayerLoader.SyncPlayer(player12, -1, -1, true);
				NetMessage.TrySendData(6, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				if (Netplay.Connection.State == 2)
				{
					Netplay.Connection.State = 3;
					return;
				}
				return;
			}
			case 4:
			{
				int num4 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num4 = this.whoAmI;
				}
				if (num4 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				Player player13 = Main.player[num4];
				player13.whoAmI = num4;
				player13.skinVariant = (int)this.reader.ReadByte();
				player13.skinVariant = (int)MathHelper.Clamp((float)player13.skinVariant, 0f, (float)(PlayerVariantID.Count - 1));
				player13.hair = (int)this.reader.ReadByte();
				if (player13.hair >= HairLoader.Count)
				{
					player13.hair = 0;
				}
				player13.name = this.reader.ReadString().Trim().Trim();
				player13.hairDye = this.reader.Read7BitEncodedInt();
				MessageBuffer.ReadAccessoryVisibility(this.reader, player13.hideVisibleAccessory);
				player13.hideMisc = this.reader.ReadByte();
				player13.hairColor = this.reader.ReadRGB();
				player13.skinColor = this.reader.ReadRGB();
				player13.eyeColor = this.reader.ReadRGB();
				player13.shirtColor = this.reader.ReadRGB();
				player13.underShirtColor = this.reader.ReadRGB();
				player13.pantsColor = this.reader.ReadRGB();
				player13.shoeColor = this.reader.ReadRGB();
				BitsByte bitsByte24 = this.reader.ReadByte();
				player13.difficulty = 0;
				if (bitsByte24[0])
				{
					player13.difficulty = 1;
				}
				if (bitsByte24[1])
				{
					player13.difficulty = 2;
				}
				if (bitsByte24[3])
				{
					player13.difficulty = 3;
				}
				if (player13.difficulty > 3)
				{
					player13.difficulty = 3;
				}
				player13.extraAccessory = bitsByte24[2];
				BitsByte bitsByte25 = this.reader.ReadByte();
				player13.UsingBiomeTorches = bitsByte25[0];
				player13.happyFunTorchTime = bitsByte25[1];
				player13.unlockedBiomeTorches = bitsByte25[2];
				player13.unlockedSuperCart = bitsByte25[3];
				player13.enabledSuperCart = bitsByte25[4];
				BitsByte bitsByte26 = this.reader.ReadByte();
				player13.usedAegisCrystal = bitsByte26[0];
				player13.usedAegisFruit = bitsByte26[1];
				player13.usedArcaneCrystal = bitsByte26[2];
				player13.usedGalaxyPearl = bitsByte26[3];
				player13.usedGummyWorm = bitsByte26[4];
				player13.usedAmbrosia = bitsByte26[5];
				player13.ateArtisanBread = bitsByte26[6];
				if (Main.netMode != 2)
				{
					return;
				}
				bool flag12 = false;
				if (Netplay.Clients[this.whoAmI].State < 10)
				{
					for (int num5 = 0; num5 < 255; num5++)
					{
						if (num5 != num4 && player13.name == Main.player[num5].name && Netplay.Clients[num5].IsActive)
						{
							flag12 = true;
						}
					}
				}
				if (flag12)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey(Lang.mp[5].Key, new object[]
					{
						player13.name
					}), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player13.name.Length > Player.nameLen)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.NameTooLong", Array.Empty<object>()), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player13.name == "")
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.EmptyName", Array.Empty<object>()), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player13.difficulty == 3 && !Main.GameModeInfo.IsJourneyMode)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.PlayerIsCreativeAndWorldIsNotCreative", Array.Empty<object>()), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player13.difficulty != 3 && Main.GameModeInfo.IsJourneyMode)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.PlayerIsNotCreativeAndWorldIsCreative", Array.Empty<object>()), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				Netplay.Clients[this.whoAmI].Name = player13.name;
				Netplay.Clients[this.whoAmI].Name = player13.name;
				NetMessage.TrySendData(4, -1, this.whoAmI, null, num4, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			case 5:
			{
				int num6 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num6 = this.whoAmI;
				}
				if (num6 == Main.myPlayer && !Main.ServerSideCharacter && !Main.player[num6].HasLockedInventory())
				{
					return;
				}
				Player player14 = Main.player[num6];
				Player obj = player14;
				lock (obj)
				{
					int num7 = (int)this.reader.ReadInt16();
					int num8 = -1;
					Item[] array3 = null;
					Item[] array4 = null;
					int num9 = 0;
					bool flag13 = false;
					Player clientPlayer = Main.clientPlayer;
					if (num7 >= PlayerItemSlotID.Loadout3_Dye_0)
					{
						num9 = num7 - PlayerItemSlotID.Loadout3_Dye_0;
						array3 = player14.Loadouts[2].Dye;
						array4 = clientPlayer.Loadouts[2].Dye;
					}
					else if (num7 >= PlayerItemSlotID.Loadout3_Armor_0)
					{
						num9 = num7 - PlayerItemSlotID.Loadout3_Armor_0;
						array3 = player14.Loadouts[2].Armor;
						array4 = clientPlayer.Loadouts[2].Armor;
					}
					else if (num7 >= PlayerItemSlotID.Loadout2_Dye_0)
					{
						num9 = num7 - PlayerItemSlotID.Loadout2_Dye_0;
						array3 = player14.Loadouts[1].Dye;
						array4 = clientPlayer.Loadouts[1].Dye;
					}
					else if (num7 >= PlayerItemSlotID.Loadout2_Armor_0)
					{
						num9 = num7 - PlayerItemSlotID.Loadout2_Armor_0;
						array3 = player14.Loadouts[1].Armor;
						array4 = clientPlayer.Loadouts[1].Armor;
					}
					else if (num7 >= PlayerItemSlotID.Loadout1_Dye_0)
					{
						num9 = num7 - PlayerItemSlotID.Loadout1_Dye_0;
						array3 = player14.Loadouts[0].Dye;
						array4 = clientPlayer.Loadouts[0].Dye;
					}
					else if (num7 >= PlayerItemSlotID.Loadout1_Armor_0)
					{
						num9 = num7 - PlayerItemSlotID.Loadout1_Armor_0;
						array3 = player14.Loadouts[0].Armor;
						array4 = clientPlayer.Loadouts[0].Armor;
					}
					else if (num7 >= PlayerItemSlotID.Bank4_0)
					{
						num9 = num7 - PlayerItemSlotID.Bank4_0;
						array3 = player14.bank4.item;
						array4 = clientPlayer.bank4.item;
						if (Main.netMode == 1 && player14.disableVoidBag == num9)
						{
							player14.disableVoidBag = -1;
							Recipe.FindRecipes(true);
						}
					}
					else if (num7 >= PlayerItemSlotID.Bank3_0)
					{
						num9 = num7 - PlayerItemSlotID.Bank3_0;
						array3 = player14.bank3.item;
						array4 = clientPlayer.bank3.item;
					}
					else if (num7 >= PlayerItemSlotID.TrashItem)
					{
						flag13 = true;
					}
					else if (num7 >= PlayerItemSlotID.Bank2_0)
					{
						num9 = num7 - PlayerItemSlotID.Bank2_0;
						array3 = player14.bank2.item;
						array4 = clientPlayer.bank2.item;
					}
					else if (num7 >= PlayerItemSlotID.Bank1_0)
					{
						num9 = num7 - PlayerItemSlotID.Bank1_0;
						array3 = player14.bank.item;
						array4 = clientPlayer.bank.item;
					}
					else if (num7 >= PlayerItemSlotID.MiscDye0)
					{
						num9 = num7 - PlayerItemSlotID.MiscDye0;
						array3 = player14.miscDyes;
						array4 = clientPlayer.miscDyes;
					}
					else if (num7 >= PlayerItemSlotID.Misc0)
					{
						num9 = num7 - PlayerItemSlotID.Misc0;
						array3 = player14.miscEquips;
						array4 = clientPlayer.miscEquips;
					}
					else if (num7 >= PlayerItemSlotID.Dye0)
					{
						num9 = num7 - PlayerItemSlotID.Dye0;
						array3 = player14.dye;
						array4 = clientPlayer.dye;
					}
					else if (num7 >= PlayerItemSlotID.Armor0)
					{
						num9 = num7 - PlayerItemSlotID.Armor0;
						array3 = player14.armor;
						array4 = clientPlayer.armor;
					}
					else
					{
						num9 = num7 - PlayerItemSlotID.Inventory0;
						array3 = player14.inventory;
						array4 = clientPlayer.inventory;
					}
					if (flag13)
					{
						player14.trashItem = ItemIO.Receive(this.reader, true, false);
						if (num6 == Main.myPlayer && !Main.ServerSideCharacter)
						{
							clientPlayer.trashItem = player14.trashItem.Clone();
						}
					}
					else if (num7 <= 58)
					{
						int type17 = array3[num9].type;
						int stack8 = array3[num9].stack;
						array3[num9] = ItemIO.Receive(this.reader, true, false);
						if (num6 == Main.myPlayer && !Main.ServerSideCharacter)
						{
							array4[num9] = array3[num9].Clone();
						}
						if (num6 == Main.myPlayer && num9 == 58)
						{
							Main.mouseItem = array3[num9].Clone();
						}
						if (num6 == Main.myPlayer && Main.netMode == 1)
						{
							Main.player[num6].inventoryChestStack[num7] = false;
							if (array3[num9].stack != stack8 || array3[num9].type != type17)
							{
								Recipe.FindRecipes(true);
							}
						}
					}
					else
					{
						array3[num9] = ItemIO.Receive(this.reader, true, false);
						if (num6 == Main.myPlayer && !Main.ServerSideCharacter)
						{
							array4[num9] = array3[num9].Clone();
						}
					}
					bool[] canRelay = PlayerItemSlotID.CanRelay;
					if (Main.netMode == 2 && num6 == this.whoAmI && canRelay.IndexInRange(num7) && canRelay[num7])
					{
						NetMessage.TrySendData(5, -1, this.whoAmI, null, num6, (float)num7, (float)num8, 0f, 0, 0, 0);
					}
					return;
				}
				break;
			}
			case 6:
				break;
			case 7:
				if (Main.netMode == 1)
				{
					Main.time = (double)this.reader.ReadInt32();
					BitsByte bitsByte27 = this.reader.ReadByte();
					Main.dayTime = bitsByte27[0];
					Main.bloodMoon = bitsByte27[1];
					Main.eclipse = bitsByte27[2];
					Main.moonPhase = (int)this.reader.ReadByte();
					Main.maxTilesX = (int)this.reader.ReadInt16();
					Main.maxTilesY = (int)this.reader.ReadInt16();
					Main.spawnTileX = (int)this.reader.ReadInt16();
					Main.spawnTileY = (int)this.reader.ReadInt16();
					Main.worldSurface = (double)this.reader.ReadInt16();
					Main.rockLayer = (double)this.reader.ReadInt16();
					Main.worldID = this.reader.ReadInt32();
					Main.worldName = this.reader.ReadString();
					Main.GameMode = (int)this.reader.ReadByte();
					Main.ActiveWorldFileData.UniqueId = new Guid(this.reader.ReadBytes(16));
					Main.ActiveWorldFileData.WorldGeneratorVersion = this.reader.ReadUInt64();
					Main.moonType = (int)this.reader.ReadByte();
					WorldGen.setBG(0, (int)this.reader.ReadByte());
					WorldGen.setBG(10, (int)this.reader.ReadByte());
					WorldGen.setBG(11, (int)this.reader.ReadByte());
					WorldGen.setBG(12, (int)this.reader.ReadByte());
					WorldGen.setBG(1, (int)this.reader.ReadByte());
					WorldGen.setBG(2, (int)this.reader.ReadByte());
					WorldGen.setBG(3, (int)this.reader.ReadByte());
					WorldGen.setBG(4, (int)this.reader.ReadByte());
					WorldGen.setBG(5, (int)this.reader.ReadByte());
					WorldGen.setBG(6, (int)this.reader.ReadByte());
					WorldGen.setBG(7, (int)this.reader.ReadByte());
					WorldGen.setBG(8, (int)this.reader.ReadByte());
					WorldGen.setBG(9, (int)this.reader.ReadByte());
					Main.iceBackStyle = (int)this.reader.ReadByte();
					Main.jungleBackStyle = (int)this.reader.ReadByte();
					Main.hellBackStyle = (int)this.reader.ReadByte();
					Main.windSpeedTarget = this.reader.ReadSingle();
					Main.numClouds = (int)this.reader.ReadByte();
					for (int num10 = 0; num10 < 3; num10++)
					{
						Main.treeX[num10] = this.reader.ReadInt32();
					}
					for (int num11 = 0; num11 < 4; num11++)
					{
						Main.treeStyle[num11] = (int)this.reader.ReadByte();
					}
					for (int num12 = 0; num12 < 3; num12++)
					{
						Main.caveBackX[num12] = this.reader.ReadInt32();
					}
					for (int num13 = 0; num13 < 4; num13++)
					{
						Main.caveBackStyle[num13] = (int)this.reader.ReadByte();
					}
					WorldGen.TreeTops.SyncReceive(this.reader);
					WorldGen.BackgroundsCache.UpdateCache();
					Main.maxRaining = this.reader.ReadSingle();
					Main.raining = (Main.maxRaining > 0f);
					BitsByte bitsByte28 = this.reader.ReadByte();
					WorldGen.shadowOrbSmashed = bitsByte28[0];
					NPC.downedBoss1 = bitsByte28[1];
					NPC.downedBoss2 = bitsByte28[2];
					NPC.downedBoss3 = bitsByte28[3];
					Main.hardMode = bitsByte28[4];
					NPC.downedClown = bitsByte28[5];
					Main.ServerSideCharacter = bitsByte28[6];
					NPC.downedPlantBoss = bitsByte28[7];
					if (Main.ServerSideCharacter)
					{
						Main.ActivePlayerFileData.MarkAsServerSide();
					}
					BitsByte bitsByte29 = this.reader.ReadByte();
					NPC.downedMechBoss1 = bitsByte29[0];
					NPC.downedMechBoss2 = bitsByte29[1];
					NPC.downedMechBoss3 = bitsByte29[2];
					NPC.downedMechBossAny = bitsByte29[3];
					Main.cloudBGActive = (bitsByte29[4] > false);
					WorldGen.crimson = bitsByte29[5];
					Main.pumpkinMoon = bitsByte29[6];
					Main.snowMoon = bitsByte29[7];
					BitsByte bitsByte30 = this.reader.ReadByte();
					Main.fastForwardTimeToDawn = bitsByte30[1];
					Main.UpdateTimeRate();
					bool flag29 = bitsByte30[2];
					NPC.downedSlimeKing = bitsByte30[3];
					NPC.downedQueenBee = bitsByte30[4];
					NPC.downedFishron = bitsByte30[5];
					NPC.downedMartians = bitsByte30[6];
					NPC.downedAncientCultist = bitsByte30[7];
					BitsByte bitsByte31 = this.reader.ReadByte();
					NPC.downedMoonlord = bitsByte31[0];
					NPC.downedHalloweenKing = bitsByte31[1];
					NPC.downedHalloweenTree = bitsByte31[2];
					NPC.downedChristmasIceQueen = bitsByte31[3];
					NPC.downedChristmasSantank = bitsByte31[4];
					NPC.downedChristmasTree = bitsByte31[5];
					NPC.downedGolemBoss = bitsByte31[6];
					BirthdayParty.ManualParty = bitsByte31[7];
					BitsByte bitsByte32 = this.reader.ReadByte();
					NPC.downedPirates = bitsByte32[0];
					NPC.downedFrost = bitsByte32[1];
					NPC.downedGoblins = bitsByte32[2];
					Sandstorm.Happening = bitsByte32[3];
					DD2Event.Ongoing = bitsByte32[4];
					DD2Event.DownedInvasionT1 = bitsByte32[5];
					DD2Event.DownedInvasionT2 = bitsByte32[6];
					DD2Event.DownedInvasionT3 = bitsByte32[7];
					BitsByte bitsByte33 = this.reader.ReadByte();
					NPC.combatBookWasUsed = bitsByte33[0];
					LanternNight.ManualLanterns = bitsByte33[1];
					NPC.downedTowerSolar = bitsByte33[2];
					NPC.downedTowerVortex = bitsByte33[3];
					NPC.downedTowerNebula = bitsByte33[4];
					NPC.downedTowerStardust = bitsByte33[5];
					Main.forceHalloweenForToday = bitsByte33[6];
					Main.forceXMasForToday = bitsByte33[7];
					BitsByte bitsByte34 = this.reader.ReadByte();
					NPC.boughtCat = bitsByte34[0];
					NPC.boughtDog = bitsByte34[1];
					NPC.boughtBunny = bitsByte34[2];
					NPC.freeCake = bitsByte34[3];
					Main.drunkWorld = bitsByte34[4];
					NPC.downedEmpressOfLight = bitsByte34[5];
					NPC.downedQueenSlime = bitsByte34[6];
					Main.getGoodWorld = bitsByte34[7];
					BitsByte bitsByte35 = this.reader.ReadByte();
					Main.tenthAnniversaryWorld = bitsByte35[0];
					Main.dontStarveWorld = bitsByte35[1];
					NPC.downedDeerclops = bitsByte35[2];
					Main.notTheBeesWorld = bitsByte35[3];
					Main.remixWorld = bitsByte35[4];
					NPC.unlockedSlimeBlueSpawn = bitsByte35[5];
					NPC.combatBookVolumeTwoWasUsed = bitsByte35[6];
					NPC.peddlersSatchelWasUsed = bitsByte35[7];
					BitsByte bitsByte36 = this.reader.ReadByte();
					NPC.unlockedSlimeGreenSpawn = bitsByte36[0];
					NPC.unlockedSlimeOldSpawn = bitsByte36[1];
					NPC.unlockedSlimePurpleSpawn = bitsByte36[2];
					NPC.unlockedSlimeRainbowSpawn = bitsByte36[3];
					NPC.unlockedSlimeRedSpawn = bitsByte36[4];
					NPC.unlockedSlimeYellowSpawn = bitsByte36[5];
					NPC.unlockedSlimeCopperSpawn = bitsByte36[6];
					Main.fastForwardTimeToDusk = bitsByte36[7];
					BitsByte bitsByte37 = this.reader.ReadByte();
					Main.noTrapsWorld = bitsByte37[0];
					Main.zenithWorld = bitsByte37[1];
					NPC.unlockedTruffleSpawn = bitsByte37[2];
					Main.sundialCooldown = (int)this.reader.ReadByte();
					Main.moondialCooldown = (int)this.reader.ReadByte();
					WorldGen.SavedOreTiers.Copper = (int)this.reader.ReadInt16();
					WorldGen.SavedOreTiers.Iron = (int)this.reader.ReadInt16();
					WorldGen.SavedOreTiers.Silver = (int)this.reader.ReadInt16();
					WorldGen.SavedOreTiers.Gold = (int)this.reader.ReadInt16();
					WorldGen.SavedOreTiers.Cobalt = (int)this.reader.ReadInt16();
					WorldGen.SavedOreTiers.Mythril = (int)this.reader.ReadInt16();
					WorldGen.SavedOreTiers.Adamantite = (int)this.reader.ReadInt16();
					if (flag29)
					{
						Main.StartSlimeRain(true);
					}
					else
					{
						Main.StopSlimeRain(true);
					}
					Main.invasionType = (int)this.reader.ReadSByte();
					Main.LobbyId = this.reader.ReadUInt64();
					Sandstorm.IntendedSeverity = this.reader.ReadSingle();
					if (Netplay.Connection.State > 4)
					{
						WorldIO.ReceiveModData(this.reader);
					}
					if (Netplay.Connection.State == 3)
					{
						Main.windSpeedCurrent = Main.windSpeedTarget;
						Netplay.Connection.State = 4;
					}
					Main.checkHalloween();
					Main.checkXMas();
					return;
				}
				return;
			case 8:
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(7, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					int num14 = this.reader.ReadInt32();
					int num15 = this.reader.ReadInt32();
					bool flag14 = true;
					if (num14 == -1 || num15 == -1)
					{
						flag14 = false;
					}
					else if (num14 < 10 || num14 > Main.maxTilesX - 10)
					{
						flag14 = false;
					}
					else if (num15 < 10 || num15 > Main.maxTilesY - 10)
					{
						flag14 = false;
					}
					int num16 = Netplay.GetSectionX(Main.spawnTileX) - 2;
					int num17 = Netplay.GetSectionY(Main.spawnTileY) - 1;
					int num18 = num16 + 5;
					int num19 = num17 + 3;
					if (num16 < 0)
					{
						num16 = 0;
					}
					if (num18 >= Main.maxSectionsX)
					{
						num18 = Main.maxSectionsX;
					}
					if (num17 < 0)
					{
						num17 = 0;
					}
					if (num19 >= Main.maxSectionsY)
					{
						num19 = Main.maxSectionsY;
					}
					int num20 = (num18 - num16) * (num19 - num17);
					List<Point> list = new List<Point>();
					for (int num21 = num16; num21 < num18; num21++)
					{
						for (int num22 = num17; num22 < num19; num22++)
						{
							list.Add(new Point(num21, num22));
						}
					}
					int num23 = -1;
					int num24 = -1;
					if (flag14)
					{
						num14 = Netplay.GetSectionX(num14) - 2;
						num15 = Netplay.GetSectionY(num15) - 1;
						num23 = num14 + 5;
						num24 = num15 + 3;
						if (num14 < 0)
						{
							num14 = 0;
						}
						if (num23 >= Main.maxSectionsX)
						{
							num23 = Main.maxSectionsX - 1;
						}
						if (num15 < 0)
						{
							num15 = 0;
						}
						if (num24 >= Main.maxSectionsY)
						{
							num24 = Main.maxSectionsY - 1;
						}
						for (int num25 = num14; num25 <= num23; num25++)
						{
							for (int num26 = num15; num26 <= num24; num26++)
							{
								if (num25 < num16 || num25 >= num18 || num26 < num17 || num26 >= num19)
								{
									list.Add(new Point(num25, num26));
									num20++;
								}
							}
						}
					}
					List<Point> portalSections;
					PortalHelper.SyncPortalsOnPlayerJoin(this.whoAmI, 1, list, out portalSections);
					num20 += portalSections.Count;
					if (Netplay.Clients[this.whoAmI].State == 2)
					{
						Netplay.Clients[this.whoAmI].State = 3;
					}
					NetMessage.TrySendData(9, this.whoAmI, -1, Lang.inter[44].ToNetworkText(), num20, 0f, 0f, 0f, 0, 0, 0);
					Netplay.Clients[this.whoAmI].StatusText2 = Language.GetTextValue("Net.IsReceivingTileData");
					Netplay.Clients[this.whoAmI].StatusMax += num20;
					for (int num27 = num16; num27 < num18; num27++)
					{
						for (int num28 = num17; num28 < num19; num28++)
						{
							NetMessage.SendSection(this.whoAmI, num27, num28);
						}
					}
					if (flag14)
					{
						for (int num29 = num14; num29 <= num23; num29++)
						{
							for (int num30 = num15; num30 <= num24; num30++)
							{
								NetMessage.SendSection(this.whoAmI, num29, num30);
							}
						}
					}
					for (int num31 = 0; num31 < portalSections.Count; num31++)
					{
						NetMessage.SendSection(this.whoAmI, portalSections[num31].X, portalSections[num31].Y);
					}
					for (int num32 = 0; num32 < 400; num32++)
					{
						if (Main.item[num32].active)
						{
							NetMessage.TrySendData(21, this.whoAmI, -1, null, num32, 0f, 0f, 0f, 0, 0, 0);
							NetMessage.TrySendData(22, this.whoAmI, -1, null, num32, 0f, 0f, 0f, 0, 0, 0);
						}
					}
					for (int num33 = 0; num33 < 200; num33++)
					{
						if (Main.npc[num33].active)
						{
							NetMessage.TrySendData(23, this.whoAmI, -1, null, num33, 0f, 0f, 0f, 0, 0, 0);
						}
					}
					for (int num34 = 0; num34 < 1000; num34++)
					{
						if (Main.projectile[num34].active && (Main.projPet[Main.projectile[num34].type] || Main.projectile[num34].netImportant))
						{
							NetMessage.TrySendData(27, this.whoAmI, -1, null, num34, 0f, 0f, 0f, 0, 0, 0);
						}
					}
					for (int num35 = 0; num35 < NPCLoader.NPCCount; num35++)
					{
						NetMessage.TrySendData(83, this.whoAmI, -1, null, num35, 0f, 0f, 0f, 0, 0, 0);
					}
					NetMessage.TrySendData(57, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					NetMessage.TrySendData(103, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					NetMessage.TrySendData(101, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					NetMessage.TrySendData(136, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					NetMessage.TrySendData(49, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					Main.BestiaryTracker.OnPlayerJoining(this.whoAmI);
					CreativePowerManager.Instance.SyncThingsToJoiningPlayer(this.whoAmI);
					Main.PylonSystem.OnPlayerJoining(this.whoAmI);
					return;
				}
				return;
			case 9:
				if (Main.netMode == 1)
				{
					Netplay.Connection.StatusMax += this.reader.ReadInt32();
					Netplay.Connection.StatusText = NetworkText.Deserialize(this.reader).ToString();
					BitsByte bitsByte38 = this.reader.ReadByte();
					BitsByte serverSpecialFlags = Netplay.Connection.ServerSpecialFlags;
					serverSpecialFlags[0] = bitsByte38[0];
					serverSpecialFlags[1] = bitsByte38[1];
					Netplay.Connection.ServerSpecialFlags = serverSpecialFlags;
					return;
				}
				return;
			case 10:
				if (Main.netMode == 1)
				{
					NetMessage.DecompressTileBlock(this.reader.BaseStream);
					return;
				}
				return;
			case 11:
				if (Main.netMode == 1)
				{
					WorldGen.SectionTileFrame((int)this.reader.ReadInt16(), (int)this.reader.ReadInt16(), (int)this.reader.ReadInt16(), (int)this.reader.ReadInt16());
					return;
				}
				return;
			case 12:
			{
				int num36 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num36 = this.whoAmI;
				}
				Player player15 = Main.player[num36];
				player15.SpawnX = (int)this.reader.ReadInt16();
				player15.SpawnY = (int)this.reader.ReadInt16();
				player15.respawnTimer = this.reader.ReadInt32();
				player15.numberOfDeathsPVE = (int)this.reader.ReadInt16();
				player15.numberOfDeathsPVP = (int)this.reader.ReadInt16();
				if (player15.respawnTimer > 0)
				{
					player15.dead = true;
				}
				PlayerSpawnContext playerSpawnContext = (PlayerSpawnContext)this.reader.ReadByte();
				player15.Spawn(playerSpawnContext);
				if (Main.netMode != 2 || Netplay.Clients[this.whoAmI].State < 3)
				{
					return;
				}
				if (Netplay.Clients[this.whoAmI].State != 3)
				{
					NetMessage.TrySendData(12, -1, this.whoAmI, null, this.whoAmI, (float)((byte)playerSpawnContext), 0f, 0f, 0, 0, 0);
					return;
				}
				Netplay.Clients[this.whoAmI].State = 10;
				NetMessage.buffer[this.whoAmI].broadcast = true;
				NetMessage.SyncConnectedPlayer(this.whoAmI);
				bool flag15 = NetMessage.DoesPlayerSlotCountAsAHost(this.whoAmI);
				Main.countsAsHostForGameplay[this.whoAmI] = flag15;
				if (NetMessage.DoesPlayerSlotCountAsAHost(this.whoAmI))
				{
					NetMessage.TrySendData(139, this.whoAmI, -1, null, this.whoAmI, (float)flag15.ToInt(), 0f, 0f, 0, 0, 0);
				}
				NetMessage.TrySendData(12, -1, this.whoAmI, null, this.whoAmI, (float)((byte)playerSpawnContext), 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(74, this.whoAmI, -1, NetworkText.FromLiteral(Main.player[this.whoAmI].name), Main.anglerQuest, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(129, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.greetPlayer(this.whoAmI);
				if (Main.ActiveWorldFileData.ModSaveErrors.Any<KeyValuePair<string, string>>())
				{
					ChatHelper.SendChatMessageToClient(Utils.CreateSaveErrorMessage("tModLoader.WorldCustomDataSaveFail", Main.ActiveWorldFileData.ModSaveErrors, false), Color.OrangeRed, this.whoAmI);
				}
				if (Main.player[num36].unlockedBiomeTorches)
				{
					NPC nPC2 = new NPC();
					nPC2.SetDefaults(664, default(NPCSpawnParams));
					Main.BestiaryTracker.Kills.RegisterKill(nPC2);
					return;
				}
				return;
			}
			case 13:
			{
				int num37 = (int)this.reader.ReadByte();
				if (num37 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num37 = this.whoAmI;
				}
				Player player16 = Main.player[num37];
				BitsByte bitsByte39 = this.reader.ReadByte();
				BitsByte bitsByte40 = this.reader.ReadByte();
				BitsByte bitsByte41 = this.reader.ReadByte();
				BitsByte bitsByte42 = this.reader.ReadByte();
				player16.controlUp = bitsByte39[0];
				player16.controlDown = bitsByte39[1];
				player16.controlLeft = bitsByte39[2];
				player16.controlRight = bitsByte39[3];
				player16.controlJump = bitsByte39[4];
				player16.controlUseItem = bitsByte39[5];
				player16.direction = (bitsByte39[6] ? 1 : -1);
				if (bitsByte40[0])
				{
					player16.pulley = true;
					player16.pulleyDir = ((!bitsByte40[1]) ? 1 : 2);
				}
				else
				{
					player16.pulley = false;
				}
				player16.vortexStealthActive = bitsByte40[3];
				player16.gravDir = (float)(bitsByte40[4] ? 1 : -1);
				player16.TryTogglingShield(bitsByte40[5]);
				player16.ghost = bitsByte40[6];
				player16.selectedItem = (int)this.reader.ReadByte();
				player16.position = this.reader.ReadVector2();
				if (bitsByte40[2])
				{
					player16.velocity = this.reader.ReadVector2();
				}
				else
				{
					player16.velocity = Vector2.Zero;
				}
				if (bitsByte41[6])
				{
					player16.PotionOfReturnOriginalUsePosition = new Vector2?(this.reader.ReadVector2());
					player16.PotionOfReturnHomePosition = new Vector2?(this.reader.ReadVector2());
				}
				else
				{
					player16.PotionOfReturnOriginalUsePosition = null;
					player16.PotionOfReturnHomePosition = null;
				}
				player16.tryKeepingHoveringUp = bitsByte41[0];
				player16.IsVoidVaultEnabled = bitsByte41[1];
				player16.sitting.isSitting = bitsByte41[2];
				player16.downedDD2EventAnyDifficulty = bitsByte41[3];
				player16.isPettingAnimal = bitsByte41[4];
				player16.isTheAnimalBeingPetSmall = bitsByte41[5];
				player16.tryKeepingHoveringDown = bitsByte41[7];
				player16.sleeping.SetIsSleepingAndAdjustPlayerRotation(player16, bitsByte42[0]);
				player16.autoReuseAllWeapons = bitsByte42[1];
				player16.controlDownHold = bitsByte42[2];
				player16.isOperatingAnotherEntity = bitsByte42[3];
				player16.controlUseTile = bitsByte42[4];
				if (Main.netMode == 2 && Netplay.Clients[this.whoAmI].State == 10)
				{
					NetMessage.TrySendData(13, -1, this.whoAmI, null, num37, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 14:
			{
				int num38 = (int)this.reader.ReadByte();
				int num39 = (int)this.reader.ReadByte();
				if (Main.netMode != 1)
				{
					return;
				}
				bool active = Main.player[num38].active;
				if (num39 == 1)
				{
					if (!Main.player[num38].active)
					{
						Main.player[num38] = new Player();
					}
					Main.player[num38].active = true;
				}
				else
				{
					Main.player[num38].active = false;
				}
				if (active == Main.player[num38].active)
				{
					return;
				}
				if (Main.player[num38].active)
				{
					Player.Hooks.PlayerConnect(num38);
					return;
				}
				Player.Hooks.PlayerDisconnect(num38);
				return;
			}
			case 15:
			case 25:
			case 26:
			case 44:
			case 67:
			case 93:
				return;
			case 16:
			{
				int num40 = (int)this.reader.ReadByte();
				if (num40 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num40 = this.whoAmI;
				}
				Player player27 = Main.player[num40];
				player27.statLife = (int)this.reader.ReadInt16();
				player27.statLifeMax = (int)this.reader.ReadInt16();
				player27.dead = (player27.statLife <= 0);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(16, -1, this.whoAmI, null, num40, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 17:
			{
				byte b2 = this.reader.ReadByte();
				int num41 = (int)this.reader.ReadInt16();
				int num42 = (int)this.reader.ReadInt16();
				short num43 = this.reader.ReadInt16();
				int num44 = (int)this.reader.ReadByte();
				bool flag16 = num43 == 1;
				if (!WorldGen.InWorld(num41, num42, 3))
				{
					return;
				}
				if (Main.tile[num41, num42] == null)
				{
					Main.tile[num41, num42] = default(Tile);
				}
				if (Main.netMode == 2)
				{
					if (!flag16)
					{
						if (b2 == 0 || b2 == 2 || b2 == 4)
						{
							Netplay.Clients[this.whoAmI].SpamDeleteBlock += 1f;
						}
						if (b2 == 1 || b2 == 3)
						{
							Netplay.Clients[this.whoAmI].SpamAddBlock += 1f;
						}
					}
					if (!Netplay.Clients[this.whoAmI].TileSections[Netplay.GetSectionX(num41), Netplay.GetSectionY(num42)])
					{
						flag16 = true;
					}
				}
				if (b2 == 0)
				{
					WorldGen.KillTile(num41, num42, flag16, false, false);
					if (Main.netMode == 1 && !flag16)
					{
						HitTile.ClearAllTilesAtThisLocation(num41, num42);
					}
				}
				bool flag17 = false;
				if (b2 == 1)
				{
					bool forced = true;
					if (WorldGen.CheckTileBreakability2_ShouldTileSurvive(num41, num42))
					{
						flag17 = true;
						forced = false;
					}
					WorldGen.PlaceTile(num41, num42, (int)num43, false, forced, -1, num44);
				}
				if (b2 == 2)
				{
					WorldGen.KillWall(num41, num42, flag16);
				}
				if (b2 == 3)
				{
					WorldGen.PlaceWall(num41, num42, (int)num43, false);
				}
				if (b2 == 4)
				{
					WorldGen.KillTile(num41, num42, flag16, false, true);
				}
				if (b2 == 5)
				{
					WorldGen.PlaceWire(num41, num42);
				}
				if (b2 == 6)
				{
					WorldGen.KillWire(num41, num42);
				}
				if (b2 == 7)
				{
					WorldGen.PoundTile(num41, num42);
				}
				if (b2 == 8)
				{
					WorldGen.PlaceActuator(num41, num42);
				}
				if (b2 == 9)
				{
					WorldGen.KillActuator(num41, num42);
				}
				if (b2 == 10)
				{
					WorldGen.PlaceWire2(num41, num42);
				}
				if (b2 == 11)
				{
					WorldGen.KillWire2(num41, num42);
				}
				if (b2 == 12)
				{
					WorldGen.PlaceWire3(num41, num42);
				}
				if (b2 == 13)
				{
					WorldGen.KillWire3(num41, num42);
				}
				if (b2 == 14)
				{
					WorldGen.SlopeTile(num41, num42, (int)num43, false);
				}
				if (b2 == 15)
				{
					Minecart.FrameTrack(num41, num42, true, false);
				}
				if (b2 == 16)
				{
					WorldGen.PlaceWire4(num41, num42);
				}
				if (b2 == 17)
				{
					WorldGen.KillWire4(num41, num42);
				}
				switch (b2)
				{
				case 18:
					Wiring.SetCurrentUser(this.whoAmI);
					Wiring.PokeLogicGate(num41, num42);
					Wiring.SetCurrentUser(-1);
					return;
				case 19:
					Wiring.SetCurrentUser(this.whoAmI);
					Wiring.Actuate(num41, num42);
					Wiring.SetCurrentUser(-1);
					return;
				case 20:
					if (WorldGen.InWorld(num41, num42, 2))
					{
						int type18 = (int)(*Main.tile[num41, num42].type);
						WorldGen.KillTile(num41, num42, flag16, false, false);
						num43 = ((Main.tile[num41, num42].active() && (int)(*Main.tile[num41, num42].type) == type18) ? 1 : 0);
						if (Main.netMode == 2)
						{
							NetMessage.TrySendData(17, -1, -1, null, (int)b2, (float)num41, (float)num42, (float)num43, num44, 0, 0);
						}
					}
					return;
				case 21:
					WorldGen.ReplaceTile(num41, num42, (ushort)num43, num44);
					break;
				}
				if (b2 == 22)
				{
					WorldGen.ReplaceWall(num41, num42, (ushort)num43);
				}
				if (b2 == 23)
				{
					WorldGen.SlopeTile(num41, num42, (int)num43, false);
					WorldGen.PoundTile(num41, num42);
				}
				if (Main.netMode != 2)
				{
					return;
				}
				if (flag17)
				{
					NetMessage.SendTileSquare(-1, num41, num42, 5, TileChangeType.None);
					return;
				}
				if ((b2 != 1 && b2 != 21) || !TileID.Sets.Falling[(int)num43] || Main.tile[num41, num42].active())
				{
					NetMessage.TrySendData(17, -1, this.whoAmI, null, (int)b2, (float)num41, (float)num42, (float)num43, num44, 0, 0);
					return;
				}
				return;
			}
			case 18:
				if (Main.netMode == 1)
				{
					Main.dayTime = (this.reader.ReadByte() == 1);
					Main.time = (double)this.reader.ReadInt32();
					Main.sunModY = this.reader.ReadInt16();
					Main.moonModY = this.reader.ReadInt16();
					return;
				}
				return;
			case 19:
			{
				byte b3 = this.reader.ReadByte();
				int num45 = (int)this.reader.ReadInt16();
				int num46 = (int)this.reader.ReadInt16();
				if (!WorldGen.InWorld(num45, num46, 3))
				{
					return;
				}
				int num47 = (this.reader.ReadByte() != 0) ? 1 : -1;
				switch (b3)
				{
				case 0:
					WorldGen.OpenDoor(num45, num46, num47);
					break;
				case 1:
					WorldGen.CloseDoor(num45, num46, true);
					break;
				case 2:
					WorldGen.ShiftTrapdoor(num45, num46, num47 == 1, 1);
					break;
				case 3:
					WorldGen.ShiftTrapdoor(num45, num46, num47 == 1, 0);
					break;
				case 4:
					WorldGen.ShiftTallGate(num45, num46, false, true);
					break;
				case 5:
					WorldGen.ShiftTallGate(num45, num46, true, true);
					break;
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(19, -1, this.whoAmI, null, (int)b3, (float)num45, (float)num46, num47 == 1, 0, 0, 0);
					return;
				}
				return;
			}
			case 20:
			{
				int num48 = (int)this.reader.ReadInt16();
				int num49 = (int)this.reader.ReadInt16();
				ushort num50 = (ushort)this.reader.ReadByte();
				ushort num51 = (ushort)this.reader.ReadByte();
				byte b4 = this.reader.ReadByte();
				if (!WorldGen.InWorld(num48, num49, 3))
				{
					return;
				}
				TileChangeType type19 = TileChangeType.None;
				if (Enum.IsDefined(typeof(TileChangeType), b4))
				{
					type19 = (TileChangeType)b4;
				}
				if (MessageBuffer.OnTileChangeReceived != null)
				{
					MessageBuffer.OnTileChangeReceived(num48, num49, (int)Math.Max(num50, num51), type19);
				}
				BitsByte bitsByte43 = 0;
				BitsByte bitsByte44 = 0;
				BitsByte bitsByte45 = 0;
				Tile tile4 = default(Tile);
				for (int num52 = num48; num52 < num48 + (int)num50; num52++)
				{
					for (int num53 = num49; num53 < num49 + (int)num51; num53++)
					{
						if (Main.tile[num52, num53] == null)
						{
							Main.tile[num52, num53] = default(Tile);
						}
						tile4 = Main.tile[num52, num53];
						bool flag18 = tile4.active();
						bitsByte43 = this.reader.ReadByte();
						bitsByte44 = this.reader.ReadByte();
						bitsByte45 = this.reader.ReadByte();
						tile4.active(bitsByte43[0]);
						*tile4.wall = (ushort)((bitsByte43[2] > false) ? 1 : 0);
						bool flag19 = bitsByte43[3];
						if (Main.netMode != 2)
						{
							*tile4.liquid = ((flag19 > false) ? 1 : 0);
						}
						tile4.wire(bitsByte43[4]);
						tile4.halfBrick(bitsByte43[5]);
						tile4.actuator(bitsByte43[6]);
						tile4.inActive(bitsByte43[7]);
						tile4.wire2(bitsByte44[0]);
						tile4.wire3(bitsByte44[1]);
						if (bitsByte44[2])
						{
							tile4.color(this.reader.ReadByte());
						}
						if (bitsByte44[3])
						{
							tile4.wallColor(this.reader.ReadByte());
						}
						if (tile4.active())
						{
							int type20 = (int)(*tile4.type);
							*tile4.type = this.reader.ReadUInt16();
							if (Main.tileFrameImportant[(int)(*tile4.type)])
							{
								*tile4.frameX = this.reader.ReadInt16();
								*tile4.frameY = this.reader.ReadInt16();
							}
							else if (!flag18 || (int)(*tile4.type) != type20)
							{
								*tile4.frameX = -1;
								*tile4.frameY = -1;
							}
							byte b5 = 0;
							if (bitsByte44[4])
							{
								b5 += 1;
							}
							if (bitsByte44[5])
							{
								b5 += 2;
							}
							if (bitsByte44[6])
							{
								b5 += 4;
							}
							tile4.slope(b5);
						}
						tile4.wire4(bitsByte44[7]);
						tile4.fullbrightBlock(bitsByte45[0]);
						tile4.fullbrightWall(bitsByte45[1]);
						tile4.invisibleBlock(bitsByte45[2]);
						tile4.invisibleWall(bitsByte45[3]);
						if (*tile4.wall > 0)
						{
							*tile4.wall = this.reader.ReadUInt16();
						}
						if (flag19)
						{
							*tile4.liquid = this.reader.ReadByte();
							tile4.liquidType((int)this.reader.ReadByte());
						}
					}
				}
				WorldGen.RangeFrame(num48, num49, num48 + (int)num50, num49 + (int)num51);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, this.whoAmI, null, num48, (float)num49, (float)num50, (float)num51, (int)b4, 0, 0);
					return;
				}
				return;
			}
			case 21:
			case 90:
			case 145:
			case 148:
			{
				int num54 = (int)this.reader.ReadInt16();
				Vector2 position3 = this.reader.ReadVector2();
				Vector2 velocity3 = this.reader.ReadVector2();
				int stack9 = this.reader.Read7BitEncodedInt();
				int prefixWeWant2 = this.reader.Read7BitEncodedInt();
				int num55 = (int)this.reader.ReadByte();
				int num56 = (int)this.reader.ReadInt16();
				bool shimmered = false;
				float shimmerTime = 0f;
				int timeLeftInWhichTheItemCannotBeTakenByEnemies = 0;
				if (b == 145)
				{
					shimmered = this.reader.ReadBoolean();
					shimmerTime = this.reader.ReadSingle();
				}
				if (b == 148)
				{
					timeLeftInWhichTheItemCannotBeTakenByEnemies = (int)this.reader.ReadByte();
				}
				if (Main.netMode == 1)
				{
					if (num56 == 0)
					{
						Main.item[num54].active = false;
						return;
					}
					int num57 = num54;
					Item item2 = Main.item[num57];
					ItemSyncPersistentStats itemSyncPersistentStats = default(ItemSyncPersistentStats);
					itemSyncPersistentStats.CopyFrom(item2);
					bool newAndShiny = (item2.newAndShiny || item2.netID != num56) && ItemSlot.Options.HighlightNewItems && (num56 < 0 || !ItemID.Sets.NeverAppearsAsNewInInventory[num56]);
					item2.netDefaults(num56);
					item2.newAndShiny = newAndShiny;
					item2.Prefix(prefixWeWant2);
					item2.stack = stack9;
					ItemIO.ReceiveModData(item2, this.reader);
					item2.position = position3;
					item2.velocity = velocity3;
					item2.active = true;
					item2.shimmered = shimmered;
					item2.shimmerTime = shimmerTime;
					if (b == 90)
					{
						item2.instanced = true;
						item2.playerIndexTheItemIsReservedFor = Main.myPlayer;
						item2.keepTime = 600;
					}
					item2.timeLeftInWhichTheItemCannotBeTakenByEnemies = timeLeftInWhichTheItemCannotBeTakenByEnemies;
					item2.wet = Collision.WetCollision(item2.position, item2.width, item2.height);
					itemSyncPersistentStats.PasteInto(item2);
					return;
				}
				else
				{
					if (num54 == 400)
					{
						Main.timeItemSlotCannotBeReusedFor[num54] = 0;
					}
					if (Main.timeItemSlotCannotBeReusedFor[num54] > 0)
					{
						return;
					}
					if (num56 == 0)
					{
						if (num54 < 400)
						{
							Main.item[num54].active = false;
							NetMessage.TrySendData(21, -1, -1, null, num54, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						return;
					}
					else
					{
						bool flag20 = false;
						if (num54 == 400)
						{
							flag20 = true;
						}
						if (flag20)
						{
							Item item3 = new Item();
							item3.netDefaults(num56);
							num54 = Item.NewItem(new EntitySource_Sync(null), (int)position3.X, (int)position3.Y, item3.width, item3.height, item3.type, stack9, true, 0, false, false);
						}
						Item item4 = Main.item[num54];
						item4.netDefaults(num56);
						item4.Prefix(prefixWeWant2);
						item4.stack = stack9;
						ItemIO.ReceiveModData(item4, this.reader);
						item4.position = position3;
						item4.velocity = velocity3;
						item4.active = true;
						item4.playerIndexTheItemIsReservedFor = Main.myPlayer;
						item4.timeLeftInWhichTheItemCannotBeTakenByEnemies = timeLeftInWhichTheItemCannotBeTakenByEnemies;
						if (b == 145)
						{
							item4.shimmered = shimmered;
							item4.shimmerTime = shimmerTime;
						}
						if (flag20)
						{
							NetMessage.TrySendData((int)b, -1, -1, null, num54, 0f, 0f, 0f, 0, 0, 0);
							if (num55 == 0)
							{
								Main.item[num54].ownIgnore = this.whoAmI;
								Main.item[num54].ownTime = 100;
							}
							Main.item[num54].FindOwner(num54);
							return;
						}
						NetMessage.TrySendData((int)b, -1, this.whoAmI, null, num54, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
				}
				break;
			}
			case 22:
			{
				int num58 = (int)this.reader.ReadInt16();
				int num59 = (int)this.reader.ReadByte();
				if (Main.netMode == 2 && Main.item[num58].playerIndexTheItemIsReservedFor != this.whoAmI)
				{
					return;
				}
				Main.item[num58].playerIndexTheItemIsReservedFor = num59;
				if (num59 == Main.myPlayer)
				{
					Main.item[num58].keepTime = 15;
				}
				else
				{
					Main.item[num58].keepTime = 0;
				}
				if (Main.netMode == 2)
				{
					Main.item[num58].playerIndexTheItemIsReservedFor = 255;
					Main.item[num58].keepTime = 15;
					NetMessage.TrySendData(22, -1, -1, null, num58, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 23:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num60 = (int)this.reader.ReadInt16();
				Vector2 vector5 = this.reader.ReadVector2();
				Vector2 velocity4 = this.reader.ReadVector2();
				int num61 = (int)this.reader.ReadUInt16();
				if (num61 == 65535)
				{
					num61 = 0;
				}
				BitsByte bitsByte46 = this.reader.ReadByte();
				BitsByte bitsByte47 = this.reader.ReadByte();
				float[] array5 = this.ReUseTemporaryNPCAI();
				for (int num62 = 0; num62 < NPC.maxAI; num62++)
				{
					if (bitsByte46[num62 + 2])
					{
						array5[num62] = this.reader.ReadSingle();
					}
					else
					{
						array5[num62] = 0f;
					}
				}
				int num63 = (int)this.reader.ReadInt16();
				int? playerCountForMultiplayerDifficultyOverride = new int?(1);
				if (bitsByte47[0])
				{
					playerCountForMultiplayerDifficultyOverride = new int?((int)this.reader.ReadByte());
				}
				float value6 = 1f;
				if (bitsByte47[2])
				{
					value6 = this.reader.ReadSingle();
				}
				int num64 = 0;
				if (!bitsByte46[7])
				{
					byte b17 = this.reader.ReadByte();
					if (b17 != 2)
					{
						if (b17 != 4)
						{
							num64 = (int)this.reader.ReadSByte();
						}
						else
						{
							num64 = this.reader.ReadInt32();
						}
					}
					else
					{
						num64 = (int)this.reader.ReadInt16();
					}
				}
				int num65 = -1;
				NPC nPC3 = Main.npc[num60];
				if (nPC3.active && Main.multiplayerNPCSmoothingRange > 0 && Vector2.DistanceSquared(nPC3.position, vector5) < 640000f)
				{
					nPC3.netOffset += nPC3.position - vector5;
				}
				if (!nPC3.active || nPC3.netID != num63)
				{
					nPC3.netOffset *= 0f;
					if (nPC3.active)
					{
						num65 = nPC3.type;
					}
					nPC3.active = true;
					nPC3.SetDefaults(num63, new NPCSpawnParams
					{
						playerCountForMultiplayerDifficultyOverride = playerCountForMultiplayerDifficultyOverride,
						strengthMultiplierOverride = new float?(value6)
					});
				}
				nPC3.position = vector5;
				nPC3.velocity = velocity4;
				nPC3.target = num61;
				nPC3.direction = (bitsByte46[0] ? 1 : -1);
				nPC3.directionY = (bitsByte46[1] ? 1 : -1);
				nPC3.spriteDirection = (bitsByte46[6] ? 1 : -1);
				if (bitsByte46[7])
				{
					num252 = (nPC3.life = nPC3.lifeMax);
					num64 = num252;
				}
				else
				{
					nPC3.life = num64;
				}
				if (num64 <= 0)
				{
					nPC3.active = false;
				}
				nPC3.SpawnedFromStatue = bitsByte47[1];
				if (nPC3.SpawnedFromStatue)
				{
					nPC3.value = 0f;
				}
				for (int num66 = 0; num66 < NPC.maxAI; num66++)
				{
					nPC3.ai[num66] = array5[num66];
				}
				if (num65 > -1 && num65 != nPC3.type)
				{
					nPC3.TransformVisuals(num65, nPC3.type);
				}
				if (num63 == 262)
				{
					NPC.plantBoss = num60;
				}
				if (num63 == 245)
				{
					NPC.golemBoss = num60;
				}
				if (num63 == 668)
				{
					NPC.deerclopsBoss = num60;
				}
				if (nPC3.type >= 0 && Main.npcCatchable[nPC3.type])
				{
					nPC3.releaseOwner = (short)this.reader.ReadByte();
				}
				if (bitsByte47[3])
				{
					NPCLoader.ReceiveExtraAI(nPC3, NPCLoader.ReadExtraAI(this.reader));
					return;
				}
				return;
			}
			case 24:
			case 94:
			case 138:
			case 149:
			case 150:
			case 151:
			case 152:
			case 153:
			case 154:
			case 155:
			case 156:
			case 157:
			case 158:
			case 159:
			case 160:
			case 161:
			case 162:
			case 163:
			case 164:
			case 165:
			case 166:
			case 167:
			case 168:
			case 169:
			case 170:
			case 171:
			case 172:
			case 173:
			case 174:
			case 175:
			case 176:
			case 177:
			case 178:
			case 179:
			case 180:
			case 181:
			case 182:
			case 183:
			case 184:
			case 185:
			case 186:
			case 187:
			case 188:
			case 189:
			case 190:
			case 191:
			case 192:
			case 193:
			case 194:
			case 195:
			case 196:
			case 197:
			case 198:
			case 199:
			case 200:
			case 201:
			case 202:
			case 203:
			case 204:
			case 205:
			case 206:
			case 207:
			case 208:
			case 209:
			case 210:
			case 211:
			case 212:
			case 213:
			case 214:
			case 215:
			case 216:
			case 217:
			case 218:
			case 219:
			case 220:
			case 221:
			case 222:
			case 223:
			case 224:
			case 225:
			case 226:
			case 227:
			case 228:
			case 229:
			case 230:
			case 231:
			case 232:
			case 233:
			case 234:
			case 235:
			case 236:
			case 237:
			case 238:
			case 239:
			case 240:
			case 241:
			case 242:
			case 243:
			case 244:
			case 245:
			case 246:
			case 247:
			case 248:
				goto IL_9E21;
			case 27:
			{
				int num67 = (int)this.reader.ReadInt16();
				Vector2 position4 = this.reader.ReadVector2();
				Vector2 velocity5 = this.reader.ReadVector2();
				int num68 = (int)this.reader.ReadByte();
				int num69 = (int)this.reader.ReadInt16();
				BitsByte bitsByte48 = this.reader.ReadByte();
				BitsByte bitsByte49 = bitsByte48[2] ? this.reader.ReadByte() : 0;
				float[] array6 = this.ReUseTemporaryProjectileAI();
				array6[0] = (bitsByte48[0] ? this.reader.ReadSingle() : 0f);
				array6[1] = (bitsByte48[1] ? this.reader.ReadSingle() : 0f);
				int bannerIdToRespondTo = (int)(bitsByte48[3] ? this.reader.ReadUInt16() : 0);
				int damage2 = (int)(bitsByte48[4] ? this.reader.ReadInt16() : 0);
				float knockBack2 = bitsByte48[5] ? this.reader.ReadSingle() : 0f;
				int originalDamage = (int)(bitsByte48[6] ? this.reader.ReadInt16() : 0);
				int num70 = (int)(bitsByte48[7] ? this.reader.ReadInt16() : -1);
				if (num70 >= 1000)
				{
					num70 = -1;
				}
				array6[2] = (bitsByte49[0] ? this.reader.ReadSingle() : 0f);
				byte[] extraAI = bitsByte49[1] ? ProjectileLoader.ReadExtraAI(this.reader) : null;
				if (Main.netMode == 2)
				{
					if (num69 == 949)
					{
						num68 = 255;
					}
					else
					{
						num68 = this.whoAmI;
						if (Main.projHostile[num69])
						{
							return;
						}
					}
				}
				int num71 = 1000;
				for (int k = 0; k < 1000; k++)
				{
					if (Main.projectile[k].owner == num68 && Main.projectile[k].identity == num67 && Main.projectile[k].active)
					{
						num71 = k;
						break;
					}
				}
				if (num71 == 1000)
				{
					for (int num72 = 0; num72 < 1000; num72++)
					{
						if (!Main.projectile[num72].active)
						{
							num71 = num72;
							break;
						}
					}
				}
				if (num71 == 1000)
				{
					num71 = Projectile.FindOldestProjectile();
				}
				Projectile projectile = Main.projectile[num71];
				if (!projectile.active || projectile.type != num69)
				{
					projectile.SetDefaults(num69);
					if (Main.netMode == 2)
					{
						Netplay.Clients[this.whoAmI].SpamProjectile += 1f;
					}
				}
				projectile.identity = num67;
				projectile.position = position4;
				projectile.velocity = velocity5;
				projectile.type = num69;
				projectile.damage = damage2;
				projectile.bannerIdToRespondTo = bannerIdToRespondTo;
				projectile.originalDamage = originalDamage;
				projectile.knockBack = knockBack2;
				projectile.owner = num68;
				for (int num73 = 0; num73 < Projectile.maxAI; num73++)
				{
					projectile.ai[num73] = array6[num73];
				}
				if (num70 >= 0)
				{
					projectile.projUUID = num70;
					Main.projectileIdentity[num68, num70] = num71;
				}
				if (extraAI != null)
				{
					ProjectileLoader.ReceiveExtraAI(projectile, extraAI);
				}
				projectile.ProjectileFixDesperation();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(27, -1, this.whoAmI, null, num71, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 28:
			{
				int num74 = (int)this.reader.ReadInt16();
				int num75 = this.reader.Read7BitEncodedInt();
				NPC.HitInfo hit = default(NPC.HitInfo);
				if (num75 >= 0)
				{
					hit = new NPC.HitInfo
					{
						Damage = num75,
						SourceDamage = this.reader.Read7BitEncodedInt(),
						DamageType = DamageClassLoader.DamageClasses[this.reader.Read7BitEncodedInt()],
						HitDirection = (int)this.reader.ReadSByte(),
						Knockback = this.reader.ReadSingle()
					};
					BitsByte flags = this.reader.ReadByte();
					hit.Crit = flags[0];
					hit.InstantKill = flags[1];
					hit.HideCombatText = flags[2];
				}
				if (Main.netMode == 2)
				{
					if (num75 < 0)
					{
						num75 = 0;
					}
					Main.npc[num74].PlayerInteraction(this.whoAmI);
				}
				if (num75 >= 0)
				{
					Main.npc[num74].StrikeNPC(hit, true, false);
				}
				else
				{
					Main.npc[num74].life = 0;
					Main.npc[num74].HitEffect(0, 10.0, null);
					Main.npc[num74].active = false;
				}
				if (Main.netMode != 2)
				{
					return;
				}
				NetMessage.SendStrikeNPC(Main.npc[num74], hit, this.whoAmI);
				if (Main.npc[num74].life <= 0)
				{
					NetMessage.TrySendData(23, -1, -1, null, num74, 0f, 0f, 0f, 0, 0, 0);
				}
				else
				{
					Main.npc[num74].netUpdate = true;
				}
				if (Main.npc[num74].realLife < 0)
				{
					return;
				}
				if (Main.npc[Main.npc[num74].realLife].life <= 0)
				{
					NetMessage.TrySendData(23, -1, -1, null, Main.npc[num74].realLife, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				Main.npc[Main.npc[num74].realLife].netUpdate = true;
				return;
			}
			case 29:
			{
				int num76 = (int)this.reader.ReadInt16();
				int num77 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num77 = this.whoAmI;
				}
				for (int num78 = 0; num78 < 1000; num78++)
				{
					if (Main.projectile[num78].owner == num77 && Main.projectile[num78].identity == num76 && Main.projectile[num78].active)
					{
						Main.projectile[num78].Kill();
						break;
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(29, -1, this.whoAmI, null, num76, (float)num77, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 30:
			{
				int num79 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num79 = this.whoAmI;
				}
				bool flag21 = this.reader.ReadBoolean();
				Main.player[num79].hostile = flag21;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(30, -1, this.whoAmI, null, num79, 0f, 0f, 0f, 0, 0, 0);
					LocalizedText localizedText2 = flag21 ? Lang.mp[11] : Lang.mp[12];
					Color color2 = Main.teamColor[Main.player[num79].team];
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(localizedText2.Key, new object[]
					{
						Main.player[num79].name
					}), color2, -1);
					return;
				}
				return;
			}
			case 31:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num80 = (int)this.reader.ReadInt16();
				int num81 = (int)this.reader.ReadInt16();
				int num82 = Chest.FindChest(num80, num81);
				if (num82 <= -1 || Chest.UsingChest(num82) != -1)
				{
					return;
				}
				for (int num83 = 0; num83 < 40; num83++)
				{
					NetMessage.TrySendData(32, this.whoAmI, -1, null, num82, (float)num83, 0f, 0f, 0, 0, 0);
				}
				NetMessage.TrySendData(33, this.whoAmI, -1, null, num82, 0f, 0f, 0f, 0, 0, 0);
				Main.player[this.whoAmI].chest = num82;
				if (Main.myPlayer == this.whoAmI)
				{
					Main.recBigList = false;
				}
				NetMessage.TrySendData(80, -1, this.whoAmI, null, this.whoAmI, (float)num82, 0f, 0f, 0, 0, 0);
				if (Main.netMode == 2 && WorldGen.IsChestRigged(num80, num81))
				{
					Wiring.SetCurrentUser(this.whoAmI);
					Wiring.HitSwitch(num80, num81);
					Wiring.SetCurrentUser(-1);
					NetMessage.TrySendData(59, -1, this.whoAmI, null, num80, (float)num81, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 32:
			{
				int num84 = (int)this.reader.ReadInt16();
				int num85 = (int)this.reader.ReadByte();
				if (num84 >= 0 && num84 < 8000)
				{
					if (Main.chest[num84] == null)
					{
						Main.chest[num84] = new Chest(false);
					}
					if (Main.chest[num84].item[num85] == null)
					{
						Main.chest[num84].item[num85] = new Item();
					}
					ItemIO.Receive(Main.chest[num84].item[num85], this.reader, true, false);
					Recipe.FindRecipes(true);
					return;
				}
				return;
			}
			case 33:
			{
				int num86 = (int)this.reader.ReadInt16();
				int num87 = (int)this.reader.ReadInt16();
				int num88 = (int)this.reader.ReadInt16();
				int num89 = (int)this.reader.ReadByte();
				string name = string.Empty;
				if (num89 != 0)
				{
					if (num89 <= 63)
					{
						name = this.reader.ReadString();
					}
					else if (num89 != 255)
					{
						num89 = 0;
					}
				}
				if (Main.netMode != 1)
				{
					if (num89 != 0)
					{
						int chest = Main.player[this.whoAmI].chest;
						Chest chest2 = Main.chest[chest];
						chest2.name = name;
						NetMessage.TrySendData(69, -1, this.whoAmI, null, chest, (float)chest2.x, (float)chest2.y, 0f, 0, 0, 0);
					}
					Main.player[this.whoAmI].chest = num86;
					Recipe.FindRecipes(true);
					NetMessage.TrySendData(80, -1, this.whoAmI, null, this.whoAmI, (float)num86, 0f, 0f, 0, 0, 0);
					return;
				}
				Player player17 = Main.player[Main.myPlayer];
				if (player17.chest == -1)
				{
					Main.playerInventory = true;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
				else if (player17.chest != num86 && num86 != -1)
				{
					Main.playerInventory = true;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					Main.recBigList = false;
				}
				else if (player17.chest != -1 && num86 == -1)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					Main.recBigList = false;
				}
				player17.chest = num86;
				player17.chestX = num87;
				player17.chestY = num88;
				Recipe.FindRecipes(true);
				if (*Main.tile[num87, num88].frameX >= 36 && *Main.tile[num87, num88].frameX < 72)
				{
					AchievementsHelper.HandleSpecialEvent(Main.player[Main.myPlayer], 16);
					return;
				}
				return;
			}
			case 34:
			{
				byte b6 = this.reader.ReadByte();
				int num90 = (int)this.reader.ReadInt16();
				int num91 = (int)this.reader.ReadInt16();
				int num92 = (int)this.reader.ReadInt16();
				int num93 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					num93 = 0;
				}
				ushort modType = 0;
				if (b6 >= 100)
				{
					modType = this.reader.ReadUInt16();
				}
				if (Main.netMode == 2)
				{
					if (b6 % 100 == 0)
					{
						if (modType == 0)
						{
							modType = 21;
						}
						int num94 = WorldGen.PlaceChest(num90, num91, modType, false, num92);
						if (num94 != -1)
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b6, (float)num90, (float)num91, (float)num92, num94, (int)modType, 0);
							return;
						}
						NetMessage.TrySendData(34, this.whoAmI, -1, null, (int)b6, (float)num90, (float)num91, (float)num92, num94, (int)modType, 0);
						int itemSpawn = (b6 < 100) ? Chest.chestItemSpawn[num92] : TileLoader.GetItemDropFromTypeAndStyle((int)modType, num92);
						if (itemSpawn > 0)
						{
							Item.NewItem(new EntitySource_TileBreak(num90, num91, null), num90 * 16, num91 * 16, 32, 32, itemSpawn, 1, true, 0, false, false);
							return;
						}
						return;
					}
					else if (b6 % 100 == 1 && (*Main.tile[num90, num91].type == 21 || (b6 == 101 && TileID.Sets.BasicChest[(int)(*Main.tile[num90, num91].type)])))
					{
						Tile tile5 = Main.tile[num90, num91];
						if (*tile5.frameX % 36 != 0)
						{
							num90--;
						}
						if (*tile5.frameY % 36 != 0)
						{
							num91--;
						}
						int number = Chest.FindChest(num90, num91);
						WorldGen.KillTile(num90, num91, false, false, false);
						if (!tile5.active())
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b6, (float)num90, (float)num91, 0f, number, 0, 0);
							return;
						}
						return;
					}
					else if (b6 % 100 == 2)
					{
						if (modType == 0)
						{
							modType = 88;
						}
						int num95 = WorldGen.PlaceChest(num90, num91, modType, false, num92);
						if (num95 != -1)
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b6, (float)num90, (float)num91, (float)num92, num95, (int)modType, 0);
							return;
						}
						NetMessage.TrySendData(34, this.whoAmI, -1, null, (int)b6, (float)num90, (float)num91, (float)num92, num95, (int)modType, 0);
						int itemSpawn2 = (b6 < 100) ? Chest.dresserItemSpawn[num92] : TileLoader.GetItemDropFromTypeAndStyle((int)modType, num92);
						if (itemSpawn2 > 0)
						{
							Item.NewItem(new EntitySource_TileBreak(num90, num91, null), num90 * 16, num91 * 16, 32, 32, itemSpawn2, 1, true, 0, false, false);
							return;
						}
						return;
					}
					else if (b6 % 100 == 3 && (*Main.tile[num90, num91].type == 88 || (b6 == 103 && TileID.Sets.BasicDresser[(int)(*Main.tile[num90, num91].type)])))
					{
						Tile tile6 = Main.tile[num90, num91];
						num90 -= (int)(*tile6.frameX % 54 / 18);
						if (*tile6.frameY % 36 != 0)
						{
							num91--;
						}
						int number2 = Chest.FindChest(num90, num91);
						WorldGen.KillTile(num90, num91, false, false, false);
						if (!tile6.active())
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b6, (float)num90, (float)num91, 0f, number2, 0, 0);
							return;
						}
						return;
					}
					else if (b6 != 4)
					{
						if (b6 != 5)
						{
							return;
						}
						if (*Main.tile[num90, num91].type != 467)
						{
							return;
						}
						Tile tile7 = Main.tile[num90, num91];
						if (*tile7.frameX % 36 != 0)
						{
							num90--;
						}
						if (*tile7.frameY % 36 != 0)
						{
							num91--;
						}
						int number3 = Chest.FindChest(num90, num91);
						WorldGen.KillTile(num90, num91, false, false, false);
						if (!tile7.active())
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b6, (float)num90, (float)num91, 0f, number3, 0, 0);
							return;
						}
						return;
					}
					else
					{
						int num96 = WorldGen.PlaceChest(num90, num91, 467, false, num92);
						if (num96 == -1)
						{
							NetMessage.TrySendData(34, this.whoAmI, -1, null, (int)b6, (float)num90, (float)num91, (float)num92, num96, 0, 0);
							Item.NewItem(new EntitySource_TileBreak(num90, num91, null), num90 * 16, num91 * 16, 32, 32, Chest.chestItemSpawn2[num92], 1, true, 0, false, false);
							return;
						}
						NetMessage.TrySendData(34, -1, -1, null, (int)b6, (float)num90, (float)num91, (float)num92, num96, 0, 0);
						return;
					}
				}
				else
				{
					byte b17 = b6;
					if (b6 % 100 == 0)
					{
						if (num93 == -1)
						{
							WorldGen.KillTile(num90, num91, false, false, false);
							return;
						}
						SoundEngine.PlaySound(0, num90 * 16, num91 * 16, 1, 1f, 0f);
						if (modType == 0)
						{
							modType = 21;
						}
						WorldGen.PlaceChestDirect(num90, num91, modType, num92, num93);
						return;
					}
					else if (b6 % 100 == 2)
					{
						if (num93 == -1)
						{
							WorldGen.KillTile(num90, num91, false, false, false);
							return;
						}
						SoundEngine.PlaySound(0, num90 * 16, num91 * 16, 1, 1f, 0f);
						if (modType == 0)
						{
							modType = 88;
						}
						WorldGen.PlaceDresserDirect(num90, num91, modType, num92, num93);
						return;
					}
					else
					{
						if (b17 != 4)
						{
							Chest.DestroyChestDirect(num90, num91, num93);
							WorldGen.KillTile(num90, num91, false, false, false);
							return;
						}
						if (num93 == -1)
						{
							WorldGen.KillTile(num90, num91, false, false, false);
							return;
						}
						SoundEngine.PlaySound(0, num90 * 16, num91 * 16, 1, 1f, 0f);
						WorldGen.PlaceChestDirect(num90, num91, 467, num92, num93);
						return;
					}
				}
				break;
			}
			case 35:
			{
				int num97 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num97 = this.whoAmI;
				}
				int num98 = (int)this.reader.ReadInt16();
				if (num97 != Main.myPlayer || Main.ServerSideCharacter)
				{
					Main.player[num97].HealEffect(num98, true);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(35, -1, this.whoAmI, null, num97, (float)num98, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 36:
			{
				int num99 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num99 = this.whoAmI;
				}
				Player player18 = Main.player[num99];
				bool flag22 = player18.zone5[0];
				player18.zone1 = this.reader.ReadByte();
				player18.zone2 = this.reader.ReadByte();
				player18.zone3 = this.reader.ReadByte();
				player18.zone4 = this.reader.ReadByte();
				player18.zone5 = this.reader.ReadByte();
				BiomeLoader.ReceiveCustomBiomes(player18, this.reader);
				player18.ZonePurity = player18.InZonePurity();
				if (Main.netMode == 2)
				{
					if (!flag22 && player18.zone5[0])
					{
						NPC.SpawnFaelings(num99);
					}
					NetMessage.TrySendData(36, -1, this.whoAmI, null, num99, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 37:
				if (Main.netMode != 1)
				{
					return;
				}
				if (Main.autoPass)
				{
					NetMessage.TrySendData(38, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					Main.autoPass = false;
					return;
				}
				Netplay.ServerPassword = "";
				Main.menuMode = 31;
				return;
			case 38:
				if (Main.netMode != 2)
				{
					return;
				}
				if (!(this.reader.ReadString() == Netplay.ServerPassword))
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[1].ToNetworkText(), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				Netplay.Clients[this.whoAmI].State = 1;
				if (ModNet.isModdedClient[this.whoAmI])
				{
					ModNet.SyncMods(this.whoAmI);
					return;
				}
				NetMessage.TrySendData(3, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			case 39:
				if (Main.netMode == 1)
				{
					int num100 = (int)this.reader.ReadInt16();
					Main.item[num100].playerIndexTheItemIsReservedFor = 255;
					NetMessage.TrySendData(22, -1, -1, null, num100, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			case 40:
			{
				int num101 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num101 = this.whoAmI;
				}
				int npcIndex = (int)this.reader.ReadInt16();
				Main.player[num101].SetTalkNPC(npcIndex, true);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(40, -1, this.whoAmI, null, num101, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 41:
			{
				int num102 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num102 = this.whoAmI;
				}
				Player player19 = Main.player[num102];
				float itemRotation = this.reader.ReadSingle();
				this.reader.ReadInt16();
				player19.itemRotation = itemRotation;
				player19.channel = player19.inventory[player19.selectedItem].channel;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(41, -1, this.whoAmI, null, num102, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 42:
			{
				int num103 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num103 = this.whoAmI;
				}
				else if (Main.myPlayer == num103 && !Main.ServerSideCharacter)
				{
					return;
				}
				int statMana = (int)this.reader.ReadInt16();
				int statManaMax = (int)this.reader.ReadInt16();
				Main.player[num103].statMana = statMana;
				Main.player[num103].statManaMax = statManaMax;
				return;
			}
			case 43:
			{
				int num104 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num104 = this.whoAmI;
				}
				int num105 = (int)this.reader.ReadInt16();
				if (num104 != Main.myPlayer)
				{
					Main.player[num104].ManaEffect(num105);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(43, -1, this.whoAmI, null, num104, (float)num105, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 45:
			{
				int num106 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num106 = this.whoAmI;
				}
				int num107 = (int)this.reader.ReadByte();
				Player player20 = Main.player[num106];
				int team = player20.team;
				player20.team = num107;
				Color color = Main.teamColor[num107];
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(45, -1, this.whoAmI, null, num106, 0f, 0f, 0f, 0, 0, 0);
					LocalizedText localizedText = Lang.mp[13 + num107];
					if (num107 == 5)
					{
						localizedText = Lang.mp[22];
					}
					for (int num108 = 0; num108 < 255; num108++)
					{
						if (num108 == this.whoAmI || (team > 0 && Main.player[num108].team == team) || (num107 > 0 && Main.player[num108].team == num107))
						{
							ChatHelper.SendChatMessageToClient(NetworkText.FromKey(localizedText.Key, new object[]
							{
								player20.name
							}), color, num108);
						}
					}
					return;
				}
				return;
			}
			case 46:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int i2 = (int)this.reader.ReadInt16();
				int j2 = (int)this.reader.ReadInt16();
				int num109 = Sign.ReadSign(i2, j2, true);
				if (num109 >= 0)
				{
					NetMessage.TrySendData(47, this.whoAmI, -1, null, num109, (float)this.whoAmI, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 47:
			{
				int num110 = (int)this.reader.ReadInt16();
				int x = (int)this.reader.ReadInt16();
				int y = (int)this.reader.ReadInt16();
				string text = this.reader.ReadString();
				int num111 = (int)this.reader.ReadByte();
				BitsByte bitsByte50 = this.reader.ReadByte();
				if (num110 < 0 || num110 >= 1000)
				{
					return;
				}
				string text2 = null;
				if (Main.sign[num110] != null)
				{
					text2 = Main.sign[num110].text;
				}
				Main.sign[num110] = new Sign();
				Main.sign[num110].x = x;
				Main.sign[num110].y = y;
				Sign.TextSign(num110, text);
				if (Main.netMode == 2 && text2 != text)
				{
					num111 = this.whoAmI;
					NetMessage.TrySendData(47, -1, this.whoAmI, null, num110, (float)num111, 0f, 0f, 0, 0, 0);
				}
				if (Main.netMode == 1 && num111 == Main.myPlayer && Main.sign[num110] != null && !bitsByte50[0])
				{
					Main.playerInventory = false;
					Main.player[Main.myPlayer].SetTalkNPC(-1, true);
					Main.npcChatCornerItem = 0;
					Main.editSign = false;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
					Main.player[Main.myPlayer].sign = num110;
					Main.npcChatText = Main.sign[num110].text;
					return;
				}
				return;
			}
			case 48:
			{
				int num112 = (int)this.reader.ReadInt16();
				int num113 = (int)this.reader.ReadInt16();
				byte b7 = this.reader.ReadByte();
				byte liquidType = this.reader.ReadByte();
				if (Main.netMode == 2 && Netplay.SpamCheck)
				{
					int num114 = this.whoAmI;
					int num115 = (int)(Main.player[num114].position.X + (float)(Main.player[num114].width / 2));
					int num254 = (int)(Main.player[num114].position.Y + (float)(Main.player[num114].height / 2));
					int num116 = 10;
					int num117 = num115 - num116;
					int num118 = num115 + num116;
					int num119 = num254 - num116;
					int num120 = num254 + num116;
					if (num112 < num117 || num112 > num118 || num113 < num119 || num113 > num120)
					{
						Netplay.Clients[this.whoAmI].SpamWater += 1f;
					}
				}
				if (Main.tile[num112, num113] == null)
				{
					Main.tile[num112, num113] = default(Tile);
				}
				*Main.tile[num112, num113].liquid = b7;
				Main.tile[num112, num113].liquidType((int)liquidType);
				if (Main.netMode != 2)
				{
					return;
				}
				WorldGen.SquareTileFrame(num112, num113, true);
				if (b7 == 0)
				{
					NetMessage.SendData(48, -1, this.whoAmI, null, num112, (float)num113, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 49:
				if (Netplay.Connection.State == 6)
				{
					Netplay.Connection.State = 10;
					Main.player[Main.myPlayer].Spawn(PlayerSpawnContext.SpawningIntoWorld);
					return;
				}
				return;
			case 50:
			{
				int num121 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num121 = this.whoAmI;
				}
				else if (num121 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				Player player21 = Main.player[num121];
				for (int num122 = 0; num122 < Player.maxBuffs; num122++)
				{
					player21.buffType[num122] = (int)this.reader.ReadUInt16();
					if (player21.buffType[num122] > 0)
					{
						player21.buffTime[num122] = 60;
					}
					else
					{
						player21.buffTime[num122] = 0;
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(50, -1, this.whoAmI, null, num121, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 51:
			{
				byte b8 = this.reader.ReadByte();
				byte b9 = this.reader.ReadByte();
				switch (b9)
				{
				case 1:
					NPC.SpawnSkeletron((int)b8);
					return;
				case 2:
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(51, -1, this.whoAmI, null, (int)b8, (float)b9, 0f, 0f, 0, 0, 0);
						return;
					}
					SoundEngine.PlaySound(new SoundStyle?(SoundID.Item1), (int)Main.player[(int)b8].position.X, (int)Main.player[(int)b8].position.Y);
					return;
				case 3:
					if (Main.netMode == 2)
					{
						Main.Sundialing();
						return;
					}
					return;
				case 4:
					Main.npc[(int)b8].BigMimicSpawnSmoke();
					return;
				case 5:
					if (Main.netMode == 2)
					{
						NPC nPC4 = new NPC();
						nPC4.SetDefaults(664, default(NPCSpawnParams));
						Main.BestiaryTracker.Kills.RegisterKill(nPC4);
						return;
					}
					return;
				case 6:
					if (Main.netMode == 2)
					{
						Main.Moondialing();
						return;
					}
					return;
				default:
					return;
				}
				break;
			}
			case 52:
			{
				int num123 = (int)this.reader.ReadByte();
				int num124 = (int)this.reader.ReadInt16();
				int num125 = (int)this.reader.ReadInt16();
				if (num123 == 1)
				{
					Chest.Unlock(num124, num125);
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(52, -1, this.whoAmI, null, 0, (float)num123, (float)num124, (float)num125, 0, 0, 0);
						NetMessage.SendTileSquare(-1, num124, num125, 2, TileChangeType.None);
					}
				}
				if (num123 == 2)
				{
					WorldGen.UnlockDoor(num124, num125);
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(52, -1, this.whoAmI, null, 0, (float)num123, (float)num124, (float)num125, 0, 0, 0);
						NetMessage.SendTileSquare(-1, num124, num125, 2, TileChangeType.None);
					}
				}
				if (num123 != 3)
				{
					return;
				}
				Chest.Lock(num124, num125);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(52, -1, this.whoAmI, null, 0, (float)num123, (float)num124, (float)num125, 0, 0, 0);
					NetMessage.SendTileSquare(-1, num124, num125, 2, TileChangeType.None);
					return;
				}
				return;
			}
			case 53:
			{
				int num126 = (int)this.reader.ReadInt16();
				int type21 = (int)this.reader.ReadUInt16();
				int time2 = (int)this.reader.ReadInt16();
				Main.npc[num126].AddBuff(type21, time2, true);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(54, -1, -1, null, num126, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 54:
				if (Main.netMode == 1)
				{
					int num127 = (int)this.reader.ReadInt16();
					NPC nPC5 = Main.npc[num127];
					for (int num128 = 0; num128 < NPC.maxBuffs; num128++)
					{
						nPC5.buffType[num128] = (int)this.reader.ReadUInt16();
						nPC5.buffTime[num128] = (int)this.reader.ReadInt16();
					}
					return;
				}
				return;
			case 55:
			{
				int num129 = (int)this.reader.ReadByte();
				int num130 = (int)this.reader.ReadUInt16();
				int num131 = this.reader.ReadInt32();
				if (Main.netMode == 2 && num129 != this.whoAmI && !Main.pvpBuff[num130])
				{
					return;
				}
				if (Main.netMode == 1 && num129 == Main.myPlayer)
				{
					Main.player[num129].AddBuff(num130, num131, true, false);
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(55, -1, -1, null, num129, (float)num130, (float)num131, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 56:
			{
				int num132 = (int)this.reader.ReadInt16();
				if (num132 < 0 || num132 >= 200)
				{
					return;
				}
				if (Main.netMode == 1)
				{
					string givenName = this.reader.ReadString();
					Main.npc[num132].GivenName = givenName;
					int townNpcVariationIndex = this.reader.ReadInt32();
					Main.npc[num132].townNpcVariationIndex = townNpcVariationIndex;
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(56, this.whoAmI, -1, null, num132, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 57:
				if (Main.netMode == 1)
				{
					WorldGen.tGood = this.reader.ReadByte();
					WorldGen.tEvil = this.reader.ReadByte();
					WorldGen.tBlood = this.reader.ReadByte();
					return;
				}
				return;
			case 58:
			{
				int num133 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num133 = this.whoAmI;
				}
				float num134 = this.reader.ReadSingle();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(58, -1, this.whoAmI, null, this.whoAmI, num134, 0f, 0f, 0, 0, 0);
					return;
				}
				Player player22 = Main.player[num133];
				int type22 = player22.inventory[player22.selectedItem].type;
				if (type22 <= 4372)
				{
					if (type22 != 4057 && type22 != 4372)
					{
						goto IL_5D90;
					}
				}
				else
				{
					if (type22 == 4673)
					{
						player22.PlayDrums(num134);
						return;
					}
					if (type22 != 4715)
					{
						goto IL_5D90;
					}
				}
				player22.PlayGuitarChord(num134);
				return;
				IL_5D90:
				Main.musicPitch = num134;
				SoundStyle type23 = SoundID.Item26;
				if (type22 == 507)
				{
					type23 = SoundID.Item35;
				}
				if (type22 == 1305)
				{
					type23 = SoundID.Item47;
				}
				SoundEngine.PlaySound(type23, new Vector2?(player22.position), null);
				return;
			}
			case 59:
			{
				int num135 = (int)this.reader.ReadInt16();
				int num136 = (int)this.reader.ReadInt16();
				Wiring.SetCurrentUser(this.whoAmI);
				Wiring.HitSwitch(num135, num136);
				Wiring.SetCurrentUser(-1);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(59, -1, this.whoAmI, null, num135, (float)num136, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 60:
			{
				int num137 = (int)this.reader.ReadInt16();
				int num138 = (int)this.reader.ReadInt16();
				int num139 = (int)this.reader.ReadInt16();
				byte b10 = this.reader.ReadByte();
				if (num137 >= 200)
				{
					NetMessage.BootPlayer(this.whoAmI, NetworkText.FromKey("Net.CheatingInvalid", Array.Empty<object>()));
					return;
				}
				NPC nPC6 = Main.npc[num137];
				bool isLikeATownNPC = nPC6.isLikeATownNPC;
				if (Main.netMode == 1)
				{
					nPC6.homeless = (b10 == 1);
					nPC6.homeTileX = num138;
					nPC6.homeTileY = num139;
				}
				if (!isLikeATownNPC)
				{
					return;
				}
				if (Main.netMode == 1)
				{
					if (b10 == 1)
					{
						WorldGen.TownManager.KickOut(nPC6.type);
						return;
					}
					if (b10 != 2)
					{
						return;
					}
					WorldGen.TownManager.SetRoom(nPC6.type, num138, num139);
					return;
				}
				else
				{
					if (b10 == 1)
					{
						WorldGen.kickOut(num137);
						return;
					}
					WorldGen.moveRoom(num138, num139, num137);
					return;
				}
				break;
			}
			case 61:
			{
				int num140 = (int)this.reader.ReadInt16();
				int num141 = (int)this.reader.ReadInt16();
				if (Main.netMode != 2)
				{
					return;
				}
				if (num141 >= 0 && NPCID.Sets.MPAllowedEnemies[num141])
				{
					if (!NPC.AnyNPCs(num141))
					{
						NPC.SpawnOnPlayer(num140, num141);
						return;
					}
					return;
				}
				else if (num141 == -4)
				{
					if (!Main.dayTime && !DD2Event.Ongoing)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[31].Key, Array.Empty<object>()), new Color(50, 255, 130), -1);
						Main.startPumpkinMoon();
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(78, -1, -1, null, 0, 1f, 2f, 1f, 0, 0, 0);
						return;
					}
					return;
				}
				else if (num141 == -5)
				{
					if (!Main.dayTime && !DD2Event.Ongoing)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[34].Key, Array.Empty<object>()), new Color(50, 255, 130), -1);
						Main.startSnowMoon();
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(78, -1, -1, null, 0, 1f, 1f, 1f, 0, 0, 0);
						return;
					}
					return;
				}
				else if (num141 == -6)
				{
					if (Main.dayTime && !Main.eclipse)
					{
						if (Main.remixWorld)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[106].Key, Array.Empty<object>()), new Color(50, 255, 130), -1);
						}
						else
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[20].Key, Array.Empty<object>()), new Color(50, 255, 130), -1);
						}
						Main.eclipse = true;
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
					return;
				}
				else
				{
					if (num141 == -7)
					{
						Main.invasionDelay = 0;
						Main.StartInvasion(4);
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(78, -1, -1, null, 0, 1f, (float)(Main.invasionType + 3), 0f, 0, 0, 0);
						return;
					}
					if (num141 == -8)
					{
						if (NPC.downedGolemBoss && Main.hardMode && !NPC.AnyDanger(false, false) && !NPC.AnyoneNearCultists())
						{
							WorldGen.StartImpendingDoom(720);
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						return;
					}
					else if (num141 == -10)
					{
						if (!Main.dayTime && !Main.bloodMoon)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[8].Key, Array.Empty<object>()), new Color(50, 255, 130), -1);
							Main.bloodMoon = true;
							if (Main.GetMoonPhase() == MoonPhase.Empty)
							{
								Main.moonPhase = 5;
							}
							AchievementsHelper.NotifyProgressionEvent(4);
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						return;
					}
					else
					{
						if (num141 == -11)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.CombatBookUsed", Array.Empty<object>()), new Color(50, 255, 130), -1);
							NPC.combatBookWasUsed = true;
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						if (num141 == -12)
						{
							NPC.UnlockOrExchangePet(ref NPC.boughtCat, 637, "Misc.LicenseCatUsed", num141);
							return;
						}
						if (num141 == -13)
						{
							NPC.UnlockOrExchangePet(ref NPC.boughtDog, 638, "Misc.LicenseDogUsed", num141);
							return;
						}
						if (num141 == -14)
						{
							NPC.UnlockOrExchangePet(ref NPC.boughtBunny, 656, "Misc.LicenseBunnyUsed", num141);
							return;
						}
						if (num141 == -15)
						{
							NPC.UnlockOrExchangePet(ref NPC.unlockedSlimeBlueSpawn, 670, "Misc.LicenseSlimeUsed", num141);
							return;
						}
						if (num141 == -16)
						{
							NPC.SpawnMechQueen(num140);
							return;
						}
						if (num141 == -17)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.CombatBookVolumeTwoUsed", Array.Empty<object>()), new Color(50, 255, 130), -1);
							NPC.combatBookVolumeTwoWasUsed = true;
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						if (num141 == -18)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.PeddlersSatchelUsed", Array.Empty<object>()), new Color(50, 255, 130), -1);
							NPC.peddlersSatchelWasUsed = true;
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						if (num141 < 0)
						{
							int num142 = 1;
							if (num141 > (int)(-(int)InvasionID.Count))
							{
								num142 = -num141;
							}
							if (num142 > 0 && Main.invasionType == 0)
							{
								Main.invasionDelay = 0;
								Main.StartInvasion(num142);
							}
							NetMessage.TrySendData(78, -1, -1, null, 0, 1f, (float)(Main.invasionType + 3), 0f, 0, 0, 0);
							return;
						}
						return;
					}
				}
				break;
			}
			case 62:
			{
				int num143 = (int)this.reader.ReadByte();
				int num144 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num143 = this.whoAmI;
				}
				if (num144 == 1)
				{
					Main.player[num143].NinjaDodge();
				}
				if (num144 == 2)
				{
					Main.player[num143].ShadowDodge();
				}
				if (num144 == 4)
				{
					Main.player[num143].BrainOfConfusionDodge();
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(62, -1, this.whoAmI, null, num143, (float)num144, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 63:
			{
				int num145 = (int)this.reader.ReadInt16();
				int num146 = (int)this.reader.ReadInt16();
				byte b11 = this.reader.ReadByte();
				byte b12 = this.reader.ReadByte();
				if (b12 == 0)
				{
					WorldGen.paintTile(num145, num146, b11, false);
				}
				else
				{
					WorldGen.paintCoatTile(num145, num146, b11, false);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(63, -1, this.whoAmI, null, num145, (float)num146, (float)b11, (float)b12, 0, 0, 0);
					return;
				}
				return;
			}
			case 64:
			{
				int num147 = (int)this.reader.ReadInt16();
				int num148 = (int)this.reader.ReadInt16();
				byte b13 = this.reader.ReadByte();
				byte b14 = this.reader.ReadByte();
				if (b14 == 0)
				{
					WorldGen.paintWall(num147, num148, b13, false);
				}
				else
				{
					WorldGen.paintCoatWall(num147, num148, b13, false);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(64, -1, this.whoAmI, null, num147, (float)num148, (float)b13, (float)b14, 0, 0, 0);
					return;
				}
				return;
			}
			case 65:
			{
				BitsByte bitsByte51 = this.reader.ReadByte();
				int num149 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					num149 = this.whoAmI;
				}
				Vector2 vector6 = this.reader.ReadVector2();
				int num150 = (int)this.reader.ReadByte();
				int num151 = 0;
				if (bitsByte51[0])
				{
					num151++;
				}
				if (bitsByte51[1])
				{
					num151 += 2;
				}
				bool flag23 = false;
				if (bitsByte51[2])
				{
					flag23 = true;
				}
				int num152 = 0;
				if (bitsByte51[3])
				{
					num152 = this.reader.ReadInt32();
				}
				if (flag23)
				{
					vector6 = Main.player[num149].position;
				}
				switch (num151)
				{
				case 0:
					Main.player[num149].Teleport(vector6, num150, num152);
					break;
				case 1:
					Main.npc[num149].Teleport(vector6, num150, num152);
					break;
				case 2:
					Main.player[num149].Teleport(vector6, num150, num152);
					if (Main.netMode == 2)
					{
						RemoteClient.CheckSection(this.whoAmI, vector6, 1);
						NetMessage.TrySendData(65, -1, -1, null, 0, (float)num149, vector6.X, vector6.Y, num150, flag23.ToInt(), num152);
						int num153 = -1;
						float num154 = 9999f;
						for (int num155 = 0; num155 < 255; num155++)
						{
							if (Main.player[num155].active && num155 != this.whoAmI)
							{
								Vector2 vector7 = Main.player[num155].position - Main.player[this.whoAmI].position;
								if (vector7.Length() < num154)
								{
									num154 = vector7.Length();
									num153 = num155;
								}
							}
						}
						if (num153 >= 0)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Game.HasTeleportedTo", new object[]
							{
								Main.player[this.whoAmI].name,
								Main.player[num153].name
							}), new Color(250, 250, 0), -1);
						}
					}
					break;
				}
				if (Main.netMode == 2 && num151 == 0)
				{
					NetMessage.TrySendData(65, -1, this.whoAmI, null, num151, (float)num149, vector6.X, vector6.Y, num150, flag23.ToInt(), num152);
					return;
				}
				return;
			}
			case 66:
			{
				int num156 = (int)this.reader.ReadByte();
				int num157 = (int)this.reader.ReadInt16();
				if (num157 <= 0)
				{
					return;
				}
				Player player23 = Main.player[num156];
				player23.statLife += num157;
				if (player23.statLife > player23.statLifeMax2)
				{
					player23.statLife = player23.statLifeMax2;
				}
				player23.HealEffect(num157, false);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(66, -1, this.whoAmI, null, num156, (float)num157, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 68:
				this.reader.ReadString();
				return;
			case 69:
			{
				int num158 = (int)this.reader.ReadInt16();
				int num159 = (int)this.reader.ReadInt16();
				int num160 = (int)this.reader.ReadInt16();
				if (Main.netMode == 1)
				{
					if (num158 >= 0 && num158 < 8000)
					{
						Chest chest3 = Main.chest[num158];
						if (chest3 == null)
						{
							chest3 = new Chest(false);
							chest3.x = num159;
							chest3.y = num160;
							Main.chest[num158] = chest3;
						}
						else if (chest3.x != num159 || chest3.y != num160)
						{
							return;
						}
						chest3.name = this.reader.ReadString();
						return;
					}
					return;
				}
				else
				{
					if (num158 < -1 || num158 >= 8000)
					{
						return;
					}
					if (num158 == -1)
					{
						num158 = Chest.FindChest(num159, num160);
						if (num158 == -1)
						{
							return;
						}
					}
					Chest chest4 = Main.chest[num158];
					if (chest4.x == num159 && chest4.y == num160)
					{
						NetMessage.TrySendData(69, this.whoAmI, -1, null, num158, (float)num159, (float)num160, 0f, 0, 0, 0);
						return;
					}
					return;
				}
				break;
			}
			case 70:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num161 = (int)this.reader.ReadInt16();
				int who = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					who = this.whoAmI;
				}
				if (num161 < 200 && num161 >= 0)
				{
					NPC.CatchNPC(num161, who);
					return;
				}
				return;
			}
			case 71:
				if (Main.netMode == 2)
				{
					int x8 = this.reader.ReadInt32();
					int y2 = this.reader.ReadInt32();
					int type24 = (int)this.reader.ReadInt16();
					byte style3 = this.reader.ReadByte();
					NPC.ReleaseNPC(x8, y2, type24, (int)style3, this.whoAmI);
					return;
				}
				return;
			case 72:
				if (Main.netMode == 1)
				{
					for (int num162 = 0; num162 < 40; num162++)
					{
						Main.travelShop[num162] = (int)this.reader.ReadInt16();
					}
					return;
				}
				return;
			case 73:
				switch (this.reader.ReadByte())
				{
				case 0:
					Main.player[this.whoAmI].TeleportationPotion();
					return;
				case 1:
					Main.player[this.whoAmI].MagicConch();
					return;
				case 2:
					Main.player[this.whoAmI].DemonConch();
					return;
				case 3:
					Main.player[this.whoAmI].Shellphone_Spawn();
					return;
				default:
					return;
				}
				break;
			case 74:
				if (Main.netMode == 1)
				{
					Main.anglerQuest = (int)this.reader.ReadByte();
					Main.anglerQuestFinished = this.reader.ReadBoolean();
					return;
				}
				return;
			case 75:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				string name2 = Main.player[this.whoAmI].name;
				if (!Main.anglerWhoFinishedToday.Contains(name2))
				{
					Main.anglerWhoFinishedToday.Add(name2);
					return;
				}
				return;
			}
			case 76:
			{
				int num163 = (int)this.reader.ReadByte();
				if (num163 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num163 = this.whoAmI;
				}
				Player player28 = Main.player[num163];
				player28.anglerQuestsFinished = this.reader.ReadInt32();
				player28.golferScoreAccumulated = this.reader.ReadInt32();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(76, -1, this.whoAmI, null, num163, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 77:
			{
				int type31 = (int)this.reader.ReadInt16();
				ushort tileType = this.reader.ReadUInt16();
				short x2 = this.reader.ReadInt16();
				short y3 = this.reader.ReadInt16();
				Animation.NewTemporaryAnimation(type31, tileType, (int)x2, (int)y3);
				return;
			}
			case 78:
				if (Main.netMode == 1)
				{
					Main.ReportInvasionProgress(this.reader.ReadInt32(), this.reader.ReadInt32(), (int)this.reader.ReadSByte(), (int)this.reader.ReadSByte());
					return;
				}
				return;
			case 79:
			{
				int x3 = (int)this.reader.ReadInt16();
				int y4 = (int)this.reader.ReadInt16();
				short type25 = this.reader.ReadInt16();
				int style4 = (int)this.reader.ReadInt16();
				int num164 = (int)this.reader.ReadByte();
				int random = (int)this.reader.ReadSByte();
				int direction = this.reader.ReadBoolean() ? 1 : -1;
				if (Main.netMode == 2)
				{
					Netplay.Clients[this.whoAmI].SpamAddBlock += 1f;
					if (!WorldGen.InWorld(x3, y4, 10) || !Netplay.Clients[this.whoAmI].TileSections[Netplay.GetSectionX(x3), Netplay.GetSectionY(y4)])
					{
						return;
					}
				}
				WorldGen.PlaceObject(x3, y4, (int)type25, false, style4, num164, random, direction);
				if (Main.netMode == 2)
				{
					NetMessage.SendObjectPlacement(this.whoAmI, x3, y4, (int)type25, style4, num164, random, direction);
					return;
				}
				return;
			}
			case 80:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num165 = (int)this.reader.ReadByte();
				int num166 = (int)this.reader.ReadInt16();
				if (num166 >= -3 && num166 < 8000)
				{
					Main.player[num165].chest = num166;
					Recipe.FindRecipes(true);
					return;
				}
				return;
			}
			case 81:
				if (Main.netMode == 1)
				{
					int num255 = (int)this.reader.ReadSingle();
					int y5 = (int)this.reader.ReadSingle();
					Color color2 = this.reader.ReadRGB();
					num252 = this.reader.ReadInt32();
					CombatText.NewText(new Rectangle(num255, y5, 0, 0), color2, num252, false, false);
					return;
				}
				return;
			case 82:
				NetManager.Instance.Read(this.reader, this.whoAmI, length);
				return;
			case 83:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num167 = (int)this.reader.ReadInt16();
				int num168 = this.reader.ReadInt32();
				if (num167 >= 0)
				{
					NPC.killCount[num167] = num168;
					return;
				}
				return;
			}
			case 84:
			{
				int num169 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num169 = this.whoAmI;
				}
				float stealth = this.reader.ReadSingle();
				Main.player[num169].stealth = stealth;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(84, -1, this.whoAmI, null, num169, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 85:
			{
				int num170 = this.whoAmI;
				int slot = (int)this.reader.ReadInt16();
				if (Main.netMode == 2 && num170 < 255)
				{
					Chest.ServerPlaceItem(this.whoAmI, slot);
					return;
				}
				return;
			}
			case 86:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num171 = this.reader.ReadInt32();
				if (this.reader.ReadBoolean())
				{
					TileEntity tileEntity = TileEntity.Read(this.reader, true, true);
					tileEntity.ID = num171;
					TileEntity.ByID[tileEntity.ID] = tileEntity;
					TileEntity.ByPosition[tileEntity.Position] = tileEntity;
					return;
				}
				TileEntity value7;
				if (TileEntity.ByID.TryGetValue(num171, out value7))
				{
					TileEntity.ByID.Remove(num171);
					TileEntity.ByPosition.Remove(value7.Position);
					return;
				}
				return;
			}
			case 87:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int x4 = (int)this.reader.ReadInt16();
				int y6 = (int)this.reader.ReadInt16();
				int type26 = (int)this.reader.ReadByte();
				if (WorldGen.InWorld(x4, y6, 0) && !TileEntity.ByPosition.ContainsKey(new Point16(x4, y6)))
				{
					TileEntity.PlaceEntityNet(x4, y6, type26);
					return;
				}
				return;
			}
			case 88:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num172 = (int)this.reader.ReadInt16();
				if (num172 < 0 || num172 > 400)
				{
					return;
				}
				Item item5 = Main.item[num172];
				BitsByte bitsByte52 = this.reader.ReadByte();
				if (bitsByte52[0])
				{
					item5.color.PackedValue = this.reader.ReadUInt32();
				}
				if (bitsByte52[1])
				{
					item5.damage = (int)this.reader.ReadUInt16();
				}
				if (bitsByte52[2])
				{
					item5.knockBack = this.reader.ReadSingle();
				}
				if (bitsByte52[3])
				{
					item5.useAnimation = (int)this.reader.ReadUInt16();
				}
				if (bitsByte52[4])
				{
					item5.useTime = (int)this.reader.ReadUInt16();
				}
				if (bitsByte52[5])
				{
					item5.shoot = (int)this.reader.ReadInt16();
				}
				if (bitsByte52[6])
				{
					item5.shootSpeed = this.reader.ReadSingle();
				}
				if (!bitsByte52[7])
				{
					return;
				}
				bitsByte52 = this.reader.ReadByte();
				if (bitsByte52[0])
				{
					item5.width = (int)this.reader.ReadInt16();
				}
				if (bitsByte52[1])
				{
					item5.height = (int)this.reader.ReadInt16();
				}
				if (bitsByte52[2])
				{
					item5.scale = this.reader.ReadSingle();
				}
				if (bitsByte52[3])
				{
					item5.ammo = (int)this.reader.ReadInt16();
				}
				if (bitsByte52[4])
				{
					item5.useAmmo = (int)this.reader.ReadInt16();
				}
				if (bitsByte52[5])
				{
					item5.notAmmo = this.reader.ReadBoolean();
					return;
				}
				return;
			}
			case 89:
				if (Main.netMode == 2)
				{
					int x9 = (int)this.reader.ReadInt16();
					int y7 = (int)this.reader.ReadInt16();
					Item item6 = ItemIO.Receive(this.reader, false, false);
					item6.stack = this.reader.Read7BitEncodedInt();
					TEItemFrame.TryPlacing(x9, y7, item6, 1);
					return;
				}
				return;
			case 91:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num173 = this.reader.ReadInt32();
				int num174 = (int)this.reader.ReadByte();
				if (num174 == 2)
				{
					int owner = (int)this.reader.ReadByte();
					num174 |= owner << 8;
				}
				if (num174 != 255)
				{
					int num175 = (int)this.reader.ReadUInt16();
					int num176 = (int)this.reader.ReadUInt16();
					int num177 = (int)this.reader.ReadByte();
					int metadata = 0;
					if (num177 < 0)
					{
						metadata = (int)this.reader.ReadInt16();
					}
					WorldUIAnchor worldUIAnchor = EmoteBubble.DeserializeNetAnchor(num174, num175);
					if (num174 == 1)
					{
						Main.player[num175].emoteTime = 360;
					}
					Dictionary<int, EmoteBubble> byID = EmoteBubble.byID;
					lock (byID)
					{
						if (!EmoteBubble.byID.ContainsKey(num173))
						{
							EmoteBubble.byID[num173] = new EmoteBubble(num177, worldUIAnchor, num176);
						}
						else
						{
							EmoteBubble.byID[num173].lifeTime = num176;
							EmoteBubble.byID[num173].lifeTimeStart = num176;
							EmoteBubble.byID[num173].emote = num177;
							EmoteBubble.byID[num173].anchor = worldUIAnchor;
						}
						EmoteBubble.byID[num173].ID = num173;
						EmoteBubble.byID[num173].metadata = metadata;
						EmoteBubble.OnBubbleChange(num173);
						EmoteBubbleLoader.OnSpawn(EmoteBubble.byID[num173]);
						return;
					}
					goto IL_7A94;
				}
				if (EmoteBubble.byID.ContainsKey(num173))
				{
					EmoteBubble.byID.Remove(num173);
					return;
				}
				return;
			}
			case 92:
				goto IL_7A94;
			case 95:
			{
				ushort num178 = this.reader.ReadUInt16();
				int num179 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					for (int num180 = 0; num180 < 1000; num180++)
					{
						if (Main.projectile[num180].owner == (int)num178 && Main.projectile[num180].active && Main.projectile[num180].type == 602 && Main.projectile[num180].ai[1] == (float)num179)
						{
							Main.projectile[num180].Kill();
							NetMessage.TrySendData(29, -1, -1, null, Main.projectile[num180].identity, (float)num178, 0f, 0f, 0, 0, 0);
							return;
						}
					}
					return;
				}
				return;
			}
			case 96:
			{
				int num181 = (int)this.reader.ReadByte();
				Player player29 = Main.player[num181];
				int num182 = (int)this.reader.ReadInt16();
				Vector2 newPos2 = this.reader.ReadVector2();
				Vector2 velocity6 = this.reader.ReadVector2();
				int lastPortalColorIndex2 = num182 + ((num182 % 2 == 0) ? 1 : -1);
				player29.lastPortalColorIndex = lastPortalColorIndex2;
				player29.Teleport(newPos2, 4, num182);
				player29.velocity = velocity6;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(96, -1, -1, null, num181, newPos2.X, newPos2.Y, (float)num182, 0, 0, 0);
					return;
				}
				return;
			}
			case 97:
				if (Main.netMode == 1)
				{
					AchievementsHelper.NotifyNPCKilledDirect(Main.player[Main.myPlayer], (int)this.reader.ReadInt16());
					return;
				}
				return;
			case 98:
				if (Main.netMode == 1)
				{
					AchievementsHelper.NotifyProgressionEvent((int)this.reader.ReadInt16());
					return;
				}
				return;
			case 99:
			{
				int num183 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num183 = this.whoAmI;
				}
				Main.player[num183].MinionRestTargetPoint = this.reader.ReadVector2();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(99, -1, this.whoAmI, null, num183, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 100:
			{
				int num184 = (int)this.reader.ReadUInt16();
				NPC npc = Main.npc[num184];
				int num185 = (int)this.reader.ReadInt16();
				Vector2 newPos3 = this.reader.ReadVector2();
				Vector2 velocity7 = this.reader.ReadVector2();
				int lastPortalColorIndex3 = num185 + ((num185 % 2 == 0) ? 1 : -1);
				npc.lastPortalColorIndex = lastPortalColorIndex3;
				npc.Teleport(newPos3, 4, num185);
				npc.velocity = velocity7;
				npc.netOffset *= 0f;
				return;
			}
			case 101:
				if (Main.netMode == 2)
				{
					return;
				}
				NPC.ShieldStrengthTowerSolar = (int)this.reader.ReadUInt16();
				NPC.ShieldStrengthTowerVortex = (int)this.reader.ReadUInt16();
				NPC.ShieldStrengthTowerNebula = (int)this.reader.ReadUInt16();
				NPC.ShieldStrengthTowerStardust = (int)this.reader.ReadUInt16();
				if (NPC.ShieldStrengthTowerSolar < 0)
				{
					NPC.ShieldStrengthTowerSolar = 0;
				}
				if (NPC.ShieldStrengthTowerVortex < 0)
				{
					NPC.ShieldStrengthTowerVortex = 0;
				}
				if (NPC.ShieldStrengthTowerNebula < 0)
				{
					NPC.ShieldStrengthTowerNebula = 0;
				}
				if (NPC.ShieldStrengthTowerStardust < 0)
				{
					NPC.ShieldStrengthTowerStardust = 0;
				}
				if (NPC.ShieldStrengthTowerSolar > NPC.LunarShieldPowerMax)
				{
					NPC.ShieldStrengthTowerSolar = NPC.LunarShieldPowerMax;
				}
				if (NPC.ShieldStrengthTowerVortex > NPC.LunarShieldPowerMax)
				{
					NPC.ShieldStrengthTowerVortex = NPC.LunarShieldPowerMax;
				}
				if (NPC.ShieldStrengthTowerNebula > NPC.LunarShieldPowerMax)
				{
					NPC.ShieldStrengthTowerNebula = NPC.LunarShieldPowerMax;
				}
				if (NPC.ShieldStrengthTowerStardust > NPC.LunarShieldPowerMax)
				{
					NPC.ShieldStrengthTowerStardust = NPC.LunarShieldPowerMax;
					return;
				}
				return;
			case 102:
			{
				int num186 = (int)this.reader.ReadByte();
				ushort num187 = this.reader.ReadUInt16();
				Vector2 other = this.reader.ReadVector2();
				if (Main.netMode == 2)
				{
					num186 = this.whoAmI;
					NetMessage.TrySendData(102, -1, -1, null, num186, (float)num187, other.X, other.Y, 0, 0, 0);
					return;
				}
				Player player24 = Main.player[num186];
				for (int num188 = 0; num188 < 255; num188++)
				{
					Player player25 = Main.player[num188];
					if (player25.active && !player25.dead && (player24.team == 0 || player24.team == player25.team) && player25.Distance(other) < 700f)
					{
						Vector2 value8 = player24.Center - player25.Center;
						Vector2 vector8 = Vector2.Normalize(value8);
						if (!vector8.HasNaNs())
						{
							int type27 = 90;
							float num189 = 0f;
							float num190 = 0.20943952f;
							Vector2 spinningpoint;
							spinningpoint..ctor(0f, -8f);
							Vector2 vector9;
							vector9..ctor(-3f);
							float num191 = 0f;
							float num192 = 0.005f;
							if (num187 != 173)
							{
								if (num187 != 176)
								{
									if (num187 == 179)
									{
										type27 = 86;
									}
								}
								else
								{
									type27 = 88;
								}
							}
							else
							{
								type27 = 90;
							}
							int num193 = 0;
							while ((float)num193 < value8.Length() / 6f)
							{
								Vector2 position5 = player25.Center + 6f * (float)num193 * vector8 + spinningpoint.RotatedBy((double)num189, default(Vector2)) + vector9;
								num189 += num190;
								int num194 = Dust.NewDust(position5, 6, 6, type27, 0f, 0f, 100, default(Color), 1.5f);
								Main.dust[num194].noGravity = true;
								Main.dust[num194].velocity = Vector2.Zero;
								num191 = (Main.dust[num194].fadeIn = num191 + num192);
								Main.dust[num194].velocity += vector8 * 1.5f;
								num193++;
							}
						}
						player25.NebulaLevelup((int)num187);
					}
				}
				return;
			}
			case 103:
				if (Main.netMode == 1)
				{
					NPC.MaxMoonLordCountdown = this.reader.ReadInt32();
					NPC.MoonLordCountdown = this.reader.ReadInt32();
					return;
				}
				return;
			case 104:
			{
				if (Main.netMode != 1 || Main.npcShop <= 0)
				{
					return;
				}
				Item[] item7 = Main.instance.shop[Main.npcShop].item;
				int num195 = (int)this.reader.ReadByte();
				int type28 = (int)this.reader.ReadInt16();
				int stack10 = (int)this.reader.ReadInt16();
				int prefixWeWant3 = (int)this.reader.ReadByte();
				int value9 = this.reader.ReadInt32();
				BitsByte bitsByte53 = this.reader.ReadByte();
				if (num195 < item7.Length)
				{
					item7[num195] = new Item();
					item7[num195].netDefaults(type28);
					item7[num195].stack = stack10;
					item7[num195].Prefix(prefixWeWant3);
					item7[num195].value = value9;
					item7[num195].buyOnce = bitsByte53[0];
					return;
				}
				return;
			}
			case 105:
				if (Main.netMode != 1)
				{
					int i3 = (int)this.reader.ReadInt16();
					int j3 = (int)this.reader.ReadInt16();
					bool on = this.reader.ReadBoolean();
					WorldGen.ToggleGemLock(i3, j3, on);
					return;
				}
				return;
			case 106:
				if (Main.netMode == 1)
				{
					HalfVector2 halfVector = default(HalfVector2);
					halfVector.PackedValue = this.reader.ReadUInt32();
					Utils.PoofOfSmoke(halfVector.ToVector2());
					return;
				}
				return;
			case 107:
				if (Main.netMode == 1)
				{
					Color c = this.reader.ReadRGB();
					string text3 = NetworkText.Deserialize(this.reader).ToString();
					int widthLimit = (int)this.reader.ReadInt16();
					Main.NewTextMultiline(text3, false, c, widthLimit);
					return;
				}
				return;
			case 108:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int damage3 = (int)this.reader.ReadInt16();
				float knockBack3 = this.reader.ReadSingle();
				int x5 = (int)this.reader.ReadInt16();
				int y8 = (int)this.reader.ReadInt16();
				int angle = (int)this.reader.ReadInt16();
				int ammo = (int)this.reader.ReadInt16();
				int num196 = (int)this.reader.ReadByte();
				if (num196 == Main.myPlayer)
				{
					WorldGen.ShootFromCannon(x5, y8, angle, ammo, damage3, knockBack3, num196, true);
					return;
				}
				return;
			}
			case 109:
				if (Main.netMode == 2)
				{
					int num256 = (int)this.reader.ReadInt16();
					int y9 = (int)this.reader.ReadInt16();
					int x6 = (int)this.reader.ReadInt16();
					int y10 = (int)this.reader.ReadInt16();
					WiresUI.Settings.MultiToolMode toolMode3 = (WiresUI.Settings.MultiToolMode)this.reader.ReadByte();
					int num197 = this.whoAmI;
					WiresUI.Settings.MultiToolMode toolMode2 = WiresUI.Settings.ToolMode;
					WiresUI.Settings.ToolMode = toolMode3;
					Wiring.MassWireOperation(new Point(num256, y9), new Point(x6, y10), Main.player[num197]);
					WiresUI.Settings.ToolMode = toolMode2;
					return;
				}
				return;
			case 110:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int type29 = (int)this.reader.ReadInt16();
				int num198 = (int)this.reader.ReadInt16();
				int num199 = (int)this.reader.ReadByte();
				if (num199 == Main.myPlayer)
				{
					Player player26 = Main.player[num199];
					for (int l = 0; l < num198; l++)
					{
						player26.ConsumeItem(type29, false, false);
					}
					player26.wireOperationsCooldown = 0;
					return;
				}
				return;
			}
			case 111:
				if (Main.netMode == 2)
				{
					BirthdayParty.ToggleManualParty();
					return;
				}
				return;
			case 112:
			{
				int num200 = (int)this.reader.ReadByte();
				int num201 = this.reader.ReadInt32();
				int num202 = this.reader.ReadInt32();
				int num203 = (int)this.reader.ReadByte();
				int num204 = (int)this.reader.ReadInt16();
				if (num200 != 1)
				{
					if (num200 != 2)
					{
						return;
					}
					NPC.FairyEffects(new Vector2((float)num201, (float)num202), num204);
					return;
				}
				else
				{
					if (Main.netMode == 1)
					{
						WorldGen.TreeGrowFX(num201, num202, num203, num204, false);
					}
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData((int)b, -1, -1, null, num200, (float)num201, (float)num202, (float)num203, num204, 0, 0);
						return;
					}
					return;
				}
				break;
			}
			case 113:
			{
				int x7 = (int)this.reader.ReadInt16();
				int y11 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2 && !Main.snowMoon && !Main.pumpkinMoon)
				{
					if (DD2Event.WouldFailSpawningHere(x7, y11))
					{
						DD2Event.FailureMessage(this.whoAmI);
					}
					DD2Event.SummonCrystal(x7, y11, this.whoAmI);
					return;
				}
				return;
			}
			case 114:
				if (Main.netMode == 1)
				{
					DD2Event.WipeEntities();
					return;
				}
				return;
			case 115:
			{
				int num205 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num205 = this.whoAmI;
				}
				Main.player[num205].MinionAttackTargetNPC = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(115, -1, this.whoAmI, null, num205, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 116:
				if (Main.netMode == 1)
				{
					DD2Event.TimeLeftBetweenWaves = this.reader.ReadInt32();
					return;
				}
				return;
			case 117:
			{
				int num206 = (int)this.reader.ReadByte();
				bool directHurt = false;
				if (num206 == 255)
				{
					num206 = (int)this.reader.ReadByte();
					directHurt = true;
				}
				if (Main.netMode == 2 && this.whoAmI != num206 && (!Main.player[num206].hostile || !Main.player[this.whoAmI].hostile))
				{
					return;
				}
				if (!directHurt)
				{
					PlayerDeathReason playerDeathReason2 = PlayerDeathReason.FromReader(this.reader);
					int damage4 = (int)this.reader.ReadInt16();
					int num207 = (int)(this.reader.ReadByte() - 1);
					BitsByte bitsByte54 = this.reader.ReadByte();
					bool flag24 = bitsByte54[0];
					bool pvp2 = bitsByte54[1];
					int num208 = (int)this.reader.ReadSByte();
					Main.player[num206].Hurt(playerDeathReason2, damage4, num207, pvp2, true, flag24, num208, true, 0f);
					if (Main.netMode == 2)
					{
						NetMessage.SendPlayerHurt(num206, playerDeathReason2, damage4, num207, flag24, pvp2, num208, -1, this.whoAmI);
						return;
					}
					return;
				}
				else
				{
					BitsByte pack = this.reader.ReadByte();
					Player.HurtInfo args = new Player.HurtInfo
					{
						DamageSource = PlayerDeathReason.FromReader(this.reader),
						PvP = pack[0],
						CooldownCounter = (int)this.reader.ReadSByte(),
						Dodgeable = pack[1],
						SourceDamage = this.reader.Read7BitEncodedInt(),
						Damage = this.reader.Read7BitEncodedInt(),
						HitDirection = (int)this.reader.ReadSByte(),
						Knockback = this.reader.ReadSingle(),
						DustDisabled = pack[2],
						SoundDisabled = pack[3]
					};
					Main.player[num206].Hurt(args, true);
					if (Main.netMode == 2)
					{
						NetMessage.SendPlayerHurt(num206, args, this.whoAmI);
						return;
					}
					return;
				}
				break;
			}
			case 118:
			{
				int num209 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num209 = this.whoAmI;
				}
				PlayerDeathReason playerDeathReason3 = PlayerDeathReason.FromReader(this.reader);
				int num210 = (int)this.reader.ReadInt16();
				int num211 = (int)(this.reader.ReadByte() - 1);
				bool pvp3 = this.reader.ReadByte()[0];
				Main.player[num209].KillMe(playerDeathReason3, (double)num210, num211, pvp3);
				if (Main.netMode == 2)
				{
					NetMessage.SendPlayerDeath(num209, playerDeathReason3, num210, num211, pvp3, -1, this.whoAmI);
					return;
				}
				return;
			}
			case 119:
				if (Main.netMode == 1)
				{
					int num257 = (int)this.reader.ReadSingle();
					int y12 = (int)this.reader.ReadSingle();
					Color color2 = this.reader.ReadRGB();
					string text4 = NetworkText.Deserialize(this.reader).ToString();
					CombatText.NewText(new Rectangle(num257, y12, 0, 0), color2, text4, false, false);
					return;
				}
				return;
			case 120:
			{
				int num212 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num212 = this.whoAmI;
				}
				int num213 = (int)this.reader.ReadByte();
				if (num213 >= 0 && num213 < EmoteBubbleLoader.EmoteBubbleCount && Main.netMode == 2)
				{
					EmoteBubble.NewBubble(num213, new WorldUIAnchor(Main.player[num212]), 360);
					EmoteBubble.CheckForNPCsToReactToEmoteBubble(num213, Main.player[num212]);
					return;
				}
				return;
			}
			case 121:
			{
				int num214 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num214 = this.whoAmI;
				}
				int num215 = this.reader.ReadInt32();
				int num216 = (int)this.reader.ReadByte();
				bool flag25 = false;
				if (num216 >= 8)
				{
					flag25 = true;
					num216 -= 8;
				}
				TileEntity value10;
				if (!TileEntity.ByID.TryGetValue(num215, out value10))
				{
					this.reader.ReadInt32();
					this.reader.ReadByte();
					return;
				}
				if (num216 >= 8)
				{
					value10 = null;
				}
				TEDisplayDoll tEDisplayDoll = value10 as TEDisplayDoll;
				if (tEDisplayDoll != null)
				{
					tEDisplayDoll.ReadItem(num216, this.reader, flag25);
				}
				else
				{
					this.reader.ReadInt32();
					this.reader.ReadByte();
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, num214, null, num214, (float)num215, (float)num216, (float)flag25.ToInt(), 0, 0, 0);
					return;
				}
				return;
			}
			case 122:
			{
				int num217 = this.reader.ReadInt32();
				int num218 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num218 = this.whoAmI;
				}
				if (Main.netMode == 2)
				{
					if (num217 == -1)
					{
						Main.player[num218].tileEntityAnchor.Clear();
						NetMessage.TrySendData((int)b, -1, -1, null, num217, (float)num218, 0f, 0f, 0, 0, 0);
						return;
					}
					TileEntity value11;
					if (!TileEntity.IsOccupied(num217, out num252) && TileEntity.ByID.TryGetValue(num217, out value11))
					{
						Main.player[num218].tileEntityAnchor.Set(num217, (int)value11.Position.X, (int)value11.Position.Y);
						NetMessage.TrySendData((int)b, -1, -1, null, num217, (float)num218, 0f, 0f, 0, 0, 0);
					}
				}
				if (Main.netMode != 1)
				{
					return;
				}
				if (num217 == -1)
				{
					Main.player[num218].tileEntityAnchor.Clear();
					return;
				}
				TileEntity value12;
				if (TileEntity.ByID.TryGetValue(num217, out value12))
				{
					TileEntity.SetInteractionAnchor(Main.player[num218], (int)value12.Position.X, (int)value12.Position.Y, num217);
					return;
				}
				return;
			}
			case 123:
				if (Main.netMode == 2)
				{
					int x10 = (int)this.reader.ReadInt16();
					int y13 = (int)this.reader.ReadInt16();
					Item item8 = ItemIO.Receive(this.reader, true, false);
					TEWeaponsRack.TryPlacing(x10, y13, item8, item8.stack);
					return;
				}
				return;
			case 124:
			{
				int num219 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num219 = this.whoAmI;
				}
				int num220 = this.reader.ReadInt32();
				int num221 = (int)this.reader.ReadByte();
				bool flag26 = false;
				if (num221 >= 2)
				{
					flag26 = true;
					num221 -= 2;
				}
				TileEntity value13;
				if (!TileEntity.ByID.TryGetValue(num220, out value13))
				{
					this.reader.ReadInt32();
					this.reader.ReadByte();
					return;
				}
				if (num221 >= 2)
				{
					value13 = null;
				}
				TEHatRack tEHatRack = value13 as TEHatRack;
				if (tEHatRack != null)
				{
					tEHatRack.ReadItem(num221, this.reader, flag26);
				}
				else
				{
					this.reader.ReadInt32();
					this.reader.ReadByte();
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, num219, null, num219, (float)num220, (float)num221, (float)flag26.ToInt(), 0, 0, 0);
					return;
				}
				return;
			}
			case 125:
			{
				int num222 = (int)this.reader.ReadByte();
				int num223 = (int)this.reader.ReadInt16();
				int num224 = (int)this.reader.ReadInt16();
				int num225 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num222 = this.whoAmI;
				}
				if (Main.netMode == 1)
				{
					Main.player[Main.myPlayer].GetOtherPlayersPickTile(num223, num224, num225);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(125, -1, num222, null, num222, (float)num223, (float)num224, (float)num225, 0, 0, 0);
					return;
				}
				return;
			}
			case 126:
				if (Main.netMode == 1)
				{
					NPC.RevengeManager.AddMarkerFromReader(this.reader);
					return;
				}
				return;
			case 127:
			{
				int markerUniqueID = this.reader.ReadInt32();
				if (Main.netMode == 1)
				{
					NPC.RevengeManager.DestroyMarker(markerUniqueID);
					return;
				}
				return;
			}
			case 128:
			{
				int num226 = (int)this.reader.ReadByte();
				int num227 = (int)this.reader.ReadUInt16();
				int num228 = (int)this.reader.ReadUInt16();
				int num229 = (int)this.reader.ReadUInt16();
				int num230 = (int)this.reader.ReadUInt16();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(128, -1, num226, null, num226, (float)num229, (float)num230, 0f, num227, num228, 0);
					return;
				}
				GolfHelper.ContactListener.PutBallInCup_TextAndEffects(new Point(num227, num228), num226, num229, num230);
				return;
			}
			case 129:
				if (Main.netMode == 1)
				{
					Main.FixUIScale();
					Main.TrySetPreparationState(Main.WorldPreparationState.ProcessingData);
					return;
				}
				return;
			case 130:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num231 = (int)this.reader.ReadUInt16();
				int num232 = (int)this.reader.ReadUInt16();
				int num233 = (int)this.reader.ReadInt16();
				if (num233 == 682)
				{
					if (NPC.unlockedSlimeRedSpawn)
					{
						return;
					}
					NPC.unlockedSlimeRedSpawn = true;
					NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				num231 *= 16;
				num232 *= 16;
				NPC npc2 = new NPC();
				npc2.SetDefaults(num233, default(NPCSpawnParams));
				int type30 = npc2.type;
				int netID = npc2.netID;
				int num234 = NPC.NewNPC(new EntitySource_FishedOut(Main.player[this.whoAmI], null), num231, num232, num233, 0, 0f, 0f, 0f, 0f, 255);
				if (netID != type30)
				{
					Main.npc[num234].SetDefaults(netID, default(NPCSpawnParams));
					NetMessage.TrySendData(23, -1, -1, null, num234, 0f, 0f, 0f, 0, 0, 0);
				}
				if (num233 == 682)
				{
					WorldGen.CheckAchievement_RealEstateAndTownSlimes();
					return;
				}
				return;
			}
			case 131:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num235 = (int)this.reader.ReadUInt16();
				NPC nPC7 = (num235 >= 200) ? new NPC() : Main.npc[num235];
				if (this.reader.ReadByte() == 1)
				{
					int time3 = this.reader.ReadInt32();
					int fromWho = (int)this.reader.ReadInt16();
					nPC7.GetImmuneTime(fromWho, time3);
					return;
				}
				return;
			}
			case 132:
				if (Main.netMode == 1)
				{
					Point point = this.reader.ReadVector2().ToPoint();
					ushort key = this.reader.ReadUInt16();
					SoundStyle legacySoundStyle = SoundID.SoundByIndex[key];
					BitsByte bitsByte55 = this.reader.ReadByte();
					if (bitsByte55[0])
					{
						legacySoundStyle.Variants = new int[]
						{
							this.reader.ReadInt32()
						};
					}
					if (bitsByte55[1])
					{
						legacySoundStyle.Volume = MathHelper.Clamp(this.reader.ReadSingle(), 0f, 1f);
					}
					if (bitsByte55[2])
					{
						legacySoundStyle.Pitch = MathHelper.Clamp(this.reader.ReadSingle(), 0f, 1f);
					}
					SoundEngine.PlaySound(legacySoundStyle, new Vector2?(point.ToVector2()), null);
					return;
				}
				return;
			case 133:
				if (Main.netMode == 2)
				{
					int x11 = (int)this.reader.ReadInt16();
					int y14 = (int)this.reader.ReadInt16();
					Item item9 = ItemIO.Receive(this.reader, true, false);
					TEFoodPlatter.TryPlacing(x11, y14, item9, item9.stack);
					return;
				}
				return;
			case 134:
			{
				int num236 = (int)this.reader.ReadByte();
				double ladyBugLuckTimeLeft = this.reader.ReadDouble();
				float torchLuck = this.reader.ReadSingle();
				byte luckPotion = this.reader.ReadByte();
				bool hasGardenGnomeNearby = this.reader.ReadBoolean();
				float equipmentBasedLuckBonus = this.reader.ReadSingle();
				float coinLuck = this.reader.ReadSingle();
				if (Main.netMode == 2)
				{
					num236 = this.whoAmI;
				}
				Player player30 = Main.player[num236];
				player30.ladyBugLuckTimeLeft = ladyBugLuckTimeLeft;
				player30.torchLuck = torchLuck;
				player30.luckPotion = luckPotion;
				player30.HasGardenGnomeNearby = hasGardenGnomeNearby;
				player30.equipmentBasedLuckBonus = equipmentBasedLuckBonus;
				player30.coinLuck = coinLuck;
				player30.RecalculateLuck();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(134, -1, num236, null, num236, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 135:
			{
				int num237 = (int)this.reader.ReadByte();
				if (Main.netMode == 1)
				{
					Main.player[num237].immuneAlpha = 255;
					return;
				}
				return;
			}
			case 136:
				for (int m = 0; m < 2; m++)
				{
					for (int n = 0; n < 3; n++)
					{
						NPC.cavernMonsterType[m, n] = (int)this.reader.ReadUInt16();
					}
				}
				return;
			case 137:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num238 = (int)this.reader.ReadInt16();
				int buffTypeToRemove = (int)this.reader.ReadUInt16();
				if (num238 >= 0 && num238 < 200)
				{
					Main.npc[num238].RequestBuffRemoval(buffTypeToRemove);
					return;
				}
				return;
			}
			case 139:
				if (Main.netMode != 2)
				{
					int num239 = (int)this.reader.ReadByte();
					bool flag27 = this.reader.ReadBoolean();
					Main.countsAsHostForGameplay[num239] = flag27;
					return;
				}
				return;
			case 140:
			{
				int num240 = (int)this.reader.ReadByte();
				int num241 = this.reader.ReadInt32();
				switch (num240)
				{
				case 0:
					if (Main.netMode == 1)
					{
						CreditsRollEvent.SetRemainingTimeDirect(num241);
						return;
					}
					return;
				case 1:
					if (Main.netMode == 2)
					{
						NPC.TransformCopperSlime(num241);
						return;
					}
					return;
				case 2:
					if (Main.netMode == 2)
					{
						NPC.TransformElderSlime(num241);
						return;
					}
					return;
				default:
					return;
				}
				break;
			}
			case 141:
			{
				LucyAxeMessage.MessageSource messageSource = (LucyAxeMessage.MessageSource)this.reader.ReadByte();
				byte b15 = this.reader.ReadByte();
				Vector2 velocity8 = this.reader.ReadVector2();
				int num242 = this.reader.ReadInt32();
				int num243 = this.reader.ReadInt32();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(141, -1, this.whoAmI, null, (int)messageSource, (float)b15, velocity8.X, velocity8.Y, num242, num243, 0);
					return;
				}
				LucyAxeMessage.CreateFromNet(messageSource, b15, new Vector2((float)num242, (float)num243), velocity8);
				return;
			}
			case 142:
			{
				int num244 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num244 = this.whoAmI;
				}
				Player player31 = Main.player[num244];
				player31.piggyBankProjTracker.TryReading(this.reader);
				player31.voidLensChest.TryReading(this.reader);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(142, -1, this.whoAmI, null, num244, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 143:
				if (Main.netMode == 2)
				{
					DD2Event.AttemptToSkipWaitTime();
					return;
				}
				return;
			case 144:
				if (Main.netMode == 2)
				{
					NPC.HaveDryadDoStardewAnimation();
					return;
				}
				return;
			case 146:
				switch (this.reader.ReadByte())
				{
				case 0:
					Item.ShimmerEffect(this.reader.ReadVector2());
					return;
				case 1:
				{
					Vector2 coinPosition = this.reader.ReadVector2();
					int coinAmount = this.reader.ReadInt32();
					Main.player[Main.myPlayer].AddCoinLuck(coinPosition, coinAmount);
					return;
				}
				case 2:
				{
					int num245 = this.reader.ReadInt32();
					Main.npc[num245].SetNetShimmerEffect();
					return;
				}
				default:
					return;
				}
				break;
			case 147:
			{
				int num246 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num246 = this.whoAmI;
				}
				int num247 = (int)this.reader.ReadByte();
				Main.player[num246].TrySwitchingLoadout(num247);
				MessageBuffer.ReadAccessoryVisibility(this.reader, Main.player[num246].hideVisibleAccessory);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, num246, null, num246, (float)num247, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 249:
				ConfigManager.HandleInGameChangeConfigPacket(this.reader, this.whoAmI);
				return;
			case 250:
				ModNet.HandleModPacket(this.reader, this.whoAmI, length);
				return;
			case 251:
				if (Main.netMode == 1)
				{
					ModNet.SyncClientMods(this.reader);
					return;
				}
				ModNet.SendNetIDs(this.whoAmI);
				NetMessage.SendData(3, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			case 252:
				if (Main.netMode == 1)
				{
					ModNet.ReceiveMod(this.reader);
					return;
				}
				ModNet.SendMod(this.reader.ReadString(), this.whoAmI);
				return;
			case 253:
			{
				string keepAlive = "Keep Alive During Mod Reload";
				ModNet.Log(this.whoAmI, keepAlive);
				RemoteClient client = Netplay.Clients[this.whoAmI];
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 4);
				defaultInterpolatedStringHandler.AppendLiteral("[");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.whoAmI);
				defaultInterpolatedStringHandler.AppendLiteral("][");
				ISocket socket = client.Socket;
				string value14;
				if (socket == null)
				{
					value14 = null;
				}
				else
				{
					RemoteAddress remoteAddress = socket.GetRemoteAddress();
					value14 = ((remoteAddress != null) ? remoteAddress.GetFriendlyName() : null);
				}
				defaultInterpolatedStringHandler.AppendFormatted(value14);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted(client.Name);
				defaultInterpolatedStringHandler.AppendLiteral(")]: ");
				defaultInterpolatedStringHandler.AppendFormatted(keepAlive);
				Console.WriteLine(defaultInterpolatedStringHandler.ToStringAndClear());
				return;
			}
			default:
				goto IL_9E21;
			}
			if (Main.netMode == 2)
			{
				if (Netplay.Clients[this.whoAmI].State == 1)
				{
					Netplay.Clients[this.whoAmI].State = 2;
				}
				NetMessage.TrySendData(7, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				Main.SyncAnInvasion(this.whoAmI);
				return;
			}
			return;
			IL_7A94:
			int num248 = (int)this.reader.ReadInt16();
			int num249 = this.reader.ReadInt32();
			float num250 = this.reader.ReadSingle();
			float num251 = this.reader.ReadSingle();
			if (num248 < 0 || num248 > 200)
			{
				return;
			}
			if (Main.netMode == 1)
			{
				Main.npc[num248].moneyPing(new Vector2(num250, num251));
				Main.npc[num248].extraValue = num249;
				return;
			}
			Main.npc[num248].extraValue += num249;
			NetMessage.TrySendData(92, -1, -1, null, num248, (float)Main.npc[num248].extraValue, num250, num251, 0, 0, 0);
			return;
			IL_9E21:
			if (Netplay.Clients[this.whoAmI].State == 0)
			{
				NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00149BF4 File Offset: 0x00147DF4
		private static void ReadAccessoryVisibility(BinaryReader reader, bool[] hideVisibleAccessory)
		{
			ushort num = reader.ReadUInt16();
			for (int i = 0; i < hideVisibleAccessory.Length; i++)
			{
				hideVisibleAccessory[i] = (((int)num & 1 << i) != 0);
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00149C24 File Offset: 0x00147E24
		private static void TrySendingItemArray(int plr, Item[] array, int slotStartIndex)
		{
			for (int i = 0; i < array.Length; i++)
			{
				NetMessage.TrySendData(5, -1, -1, null, plr, (float)(slotStartIndex + i), (float)array[i].prefix, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0400077D RID: 1917
		public const int readBufferMax = 131070;

		// Token: 0x0400077E RID: 1918
		public const int writeBufferMax = 131070;

		// Token: 0x0400077F RID: 1919
		public bool broadcast;

		// Token: 0x04000780 RID: 1920
		public byte[] readBuffer = new byte[131070];

		// Token: 0x04000781 RID: 1921
		public byte[] writeBuffer = new byte[131070];

		// Token: 0x04000782 RID: 1922
		public bool writeLocked;

		// Token: 0x04000783 RID: 1923
		public int messageLength;

		// Token: 0x04000784 RID: 1924
		public int totalData;

		// Token: 0x04000785 RID: 1925
		public int whoAmI;

		// Token: 0x04000786 RID: 1926
		public int spamCount;

		// Token: 0x04000787 RID: 1927
		public int maxSpam;

		// Token: 0x04000788 RID: 1928
		public bool checkBytes;

		// Token: 0x04000789 RID: 1929
		public MemoryStream readerStream;

		// Token: 0x0400078A RID: 1930
		public MemoryStream writerStream;

		// Token: 0x0400078B RID: 1931
		public BinaryReader reader;

		// Token: 0x0400078C RID: 1932
		public BinaryWriter writer;

		// Token: 0x0400078D RID: 1933
		public PacketHistory History = new PacketHistory(100, 65535);

		// Token: 0x0400078E RID: 1934
		private float[] _temporaryProjectileAI = new float[Projectile.maxAI];

		// Token: 0x0400078F RID: 1935
		private float[] _temporaryNPCAI = new float[NPC.maxAI];
	}
}
