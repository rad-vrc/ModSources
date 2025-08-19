using System;
using System.Collections.Generic;
using System.IO;
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
using Terraria.Net;
using Terraria.Testing;
using Terraria.UI;

namespace Terraria
{
	// Token: 0x02000029 RID: 41
	public class MessageBuffer
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001D5 RID: 469 RVA: 0x0001E920 File Offset: 0x0001CB20
		// (remove) Token: 0x060001D6 RID: 470 RVA: 0x0001E954 File Offset: 0x0001CB54
		public static event TileChangeReceivedEvent OnTileChangeReceived;

		// Token: 0x060001D7 RID: 471 RVA: 0x0001E988 File Offset: 0x0001CB88
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

		// Token: 0x060001D8 RID: 472 RVA: 0x0001E9F3 File Offset: 0x0001CBF3
		public void ResetReader()
		{
			if (this.readerStream != null)
			{
				this.readerStream.Close();
			}
			this.readerStream = new MemoryStream(this.readBuffer);
			this.reader = new BinaryReader(this.readerStream);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0001EA2A File Offset: 0x0001CC2A
		public void ResetWriter()
		{
			if (this.writerStream != null)
			{
				this.writerStream.Close();
			}
			this.writerStream = new MemoryStream(this.writeBuffer);
			this.writer = new BinaryWriter(this.writerStream);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0001EA64 File Offset: 0x0001CC64
		private float[] ReUseTemporaryProjectileAI()
		{
			for (int i = 0; i < this._temporaryProjectileAI.Length; i++)
			{
				this._temporaryProjectileAI[i] = 0f;
			}
			return this._temporaryProjectileAI;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0001EA98 File Offset: 0x0001CC98
		private float[] ReUseTemporaryNPCAI()
		{
			for (int i = 0; i < this._temporaryNPCAI.Length; i++)
			{
				this._temporaryNPCAI[i] = 0f;
			}
			return this._temporaryNPCAI;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0001EACC File Offset: 0x0001CCCC
		public void GetData(int start, int length, out int messageType)
		{
			if (this.whoAmI < 256)
			{
				Netplay.Clients[this.whoAmI].TimeOutTimer = 0;
			}
			else
			{
				Netplay.Connection.TimeOutTimer = 0;
			}
			int num = start + 1;
			byte b = this.readBuffer[start];
			messageType = (int)b;
			if (b >= MessageID.Count)
			{
				return;
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
					byte b2 = this.readBuffer[j];
				}
			}
			if (Main.netMode == 2 && b != 38 && Netplay.Clients[this.whoAmI].State == -1)
			{
				NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[1].ToNetworkText(), 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			if (Main.netMode == 2)
			{
				if (Netplay.Clients[this.whoAmI].State < 10 && b > 12 && b != 93 && b != 16 && b != 42 && b != 50 && b != 38 && b != 68 && b != 147)
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
			switch (b)
			{
			case 1:
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
				if (!(this.reader.ReadString() == "Terraria" + 279))
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[4].ToNetworkText(), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (string.IsNullOrEmpty(Netplay.ServerPassword))
				{
					Netplay.Clients[this.whoAmI].State = 1;
					NetMessage.TrySendData(3, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				Netplay.Clients[this.whoAmI].State = -1;
				NetMessage.TrySendData(37, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			case 2:
				if (Main.netMode != 1)
				{
					return;
				}
				Netplay.Disconnect = true;
				Main.statusText = NetworkText.Deserialize(this.reader).ToString();
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
				bool value = this.reader.ReadBoolean();
				Netplay.Connection.ServerSpecialFlags[2] = value;
				if (num2 != Main.myPlayer)
				{
					Main.player[num2] = Main.ActivePlayerFileData.Player;
					Main.player[Main.myPlayer] = new Player();
				}
				Main.player[num2].whoAmI = num2;
				Main.myPlayer = num2;
				Player player = Main.player[num2];
				NetMessage.TrySendData(4, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(68, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(16, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(42, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(50, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(147, -1, -1, null, num2, (float)player.CurrentLoadoutIndex, 0f, 0f, 0, 0, 0);
				for (int k = 0; k < 59; k++)
				{
					NetMessage.TrySendData(5, -1, -1, null, num2, (float)(PlayerItemSlotID.Inventory0 + k), (float)player.inventory[k].prefix, 0f, 0, 0, 0);
				}
				MessageBuffer.TrySendingItemArray(num2, player.armor, PlayerItemSlotID.Armor0);
				MessageBuffer.TrySendingItemArray(num2, player.dye, PlayerItemSlotID.Dye0);
				MessageBuffer.TrySendingItemArray(num2, player.miscEquips, PlayerItemSlotID.Misc0);
				MessageBuffer.TrySendingItemArray(num2, player.miscDyes, PlayerItemSlotID.MiscDye0);
				MessageBuffer.TrySendingItemArray(num2, player.bank.item, PlayerItemSlotID.Bank1_0);
				MessageBuffer.TrySendingItemArray(num2, player.bank2.item, PlayerItemSlotID.Bank2_0);
				NetMessage.TrySendData(5, -1, -1, null, num2, (float)PlayerItemSlotID.TrashItem, (float)player.trashItem.prefix, 0f, 0, 0, 0);
				MessageBuffer.TrySendingItemArray(num2, player.bank3.item, PlayerItemSlotID.Bank3_0);
				MessageBuffer.TrySendingItemArray(num2, player.bank4.item, PlayerItemSlotID.Bank4_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[0].Armor, PlayerItemSlotID.Loadout1_Armor_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[0].Dye, PlayerItemSlotID.Loadout1_Dye_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[1].Armor, PlayerItemSlotID.Loadout2_Armor_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[1].Dye, PlayerItemSlotID.Loadout2_Dye_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[2].Armor, PlayerItemSlotID.Loadout3_Armor_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[2].Dye, PlayerItemSlotID.Loadout3_Dye_0);
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
				int num3 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num3 = this.whoAmI;
				}
				if (num3 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				Player player2 = Main.player[num3];
				player2.whoAmI = num3;
				player2.skinVariant = (int)this.reader.ReadByte();
				player2.skinVariant = (int)MathHelper.Clamp((float)player2.skinVariant, 0f, (float)(PlayerVariantID.Count - 1));
				player2.hair = (int)this.reader.ReadByte();
				if (player2.hair >= 165)
				{
					player2.hair = 0;
				}
				player2.name = this.reader.ReadString().Trim().Trim();
				player2.hairDye = this.reader.ReadByte();
				MessageBuffer.ReadAccessoryVisibility(this.reader, player2.hideVisibleAccessory);
				player2.hideMisc = this.reader.ReadByte();
				player2.hairColor = this.reader.ReadRGB();
				player2.skinColor = this.reader.ReadRGB();
				player2.eyeColor = this.reader.ReadRGB();
				player2.shirtColor = this.reader.ReadRGB();
				player2.underShirtColor = this.reader.ReadRGB();
				player2.pantsColor = this.reader.ReadRGB();
				player2.shoeColor = this.reader.ReadRGB();
				BitsByte bitsByte = this.reader.ReadByte();
				player2.difficulty = 0;
				if (bitsByte[0])
				{
					player2.difficulty = 1;
				}
				if (bitsByte[1])
				{
					player2.difficulty = 2;
				}
				if (bitsByte[3])
				{
					player2.difficulty = 3;
				}
				if (player2.difficulty > 3)
				{
					player2.difficulty = 3;
				}
				player2.extraAccessory = bitsByte[2];
				BitsByte bitsByte2 = this.reader.ReadByte();
				player2.UsingBiomeTorches = bitsByte2[0];
				player2.happyFunTorchTime = bitsByte2[1];
				player2.unlockedBiomeTorches = bitsByte2[2];
				player2.unlockedSuperCart = bitsByte2[3];
				player2.enabledSuperCart = bitsByte2[4];
				BitsByte bitsByte3 = this.reader.ReadByte();
				player2.usedAegisCrystal = bitsByte3[0];
				player2.usedAegisFruit = bitsByte3[1];
				player2.usedArcaneCrystal = bitsByte3[2];
				player2.usedGalaxyPearl = bitsByte3[3];
				player2.usedGummyWorm = bitsByte3[4];
				player2.usedAmbrosia = bitsByte3[5];
				player2.ateArtisanBread = bitsByte3[6];
				if (Main.netMode != 2)
				{
					return;
				}
				bool flag = false;
				if (Netplay.Clients[this.whoAmI].State < 10)
				{
					for (int l = 0; l < 255; l++)
					{
						if (l != num3 && player2.name == Main.player[l].name && Netplay.Clients[l].IsActive)
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey(Lang.mp[5].Key, new object[]
					{
						player2.name
					}), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player2.name.Length > Player.nameLen)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.NameTooLong", new object[0]), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player2.name == "")
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.EmptyName", new object[0]), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player2.difficulty == 3 && !Main.GameModeInfo.IsJourneyMode)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.PlayerIsCreativeAndWorldIsNotCreative", new object[0]), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player2.difficulty != 3 && Main.GameModeInfo.IsJourneyMode)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.PlayerIsNotCreativeAndWorldIsCreative", new object[0]), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				Netplay.Clients[this.whoAmI].Name = player2.name;
				Netplay.Clients[this.whoAmI].Name = player2.name;
				NetMessage.TrySendData(4, -1, this.whoAmI, null, num3, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			case 5:
			{
				int num4 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num4 = this.whoAmI;
				}
				if (num4 == Main.myPlayer && !Main.ServerSideCharacter && !Main.player[num4].HasLockedInventory())
				{
					return;
				}
				Player player3 = Main.player[num4];
				Player obj = player3;
				lock (obj)
				{
					int num5 = (int)this.reader.ReadInt16();
					int stack = (int)this.reader.ReadInt16();
					int num6 = (int)this.reader.ReadByte();
					int type = (int)this.reader.ReadInt16();
					Item[] array = null;
					Item[] array2 = null;
					int num7 = 0;
					bool flag3 = false;
					Player clientPlayer = Main.clientPlayer;
					if (num5 >= PlayerItemSlotID.Loadout3_Dye_0)
					{
						num7 = num5 - PlayerItemSlotID.Loadout3_Dye_0;
						array = player3.Loadouts[2].Dye;
						array2 = clientPlayer.Loadouts[2].Dye;
					}
					else if (num5 >= PlayerItemSlotID.Loadout3_Armor_0)
					{
						num7 = num5 - PlayerItemSlotID.Loadout3_Armor_0;
						array = player3.Loadouts[2].Armor;
						array2 = clientPlayer.Loadouts[2].Armor;
					}
					else if (num5 >= PlayerItemSlotID.Loadout2_Dye_0)
					{
						num7 = num5 - PlayerItemSlotID.Loadout2_Dye_0;
						array = player3.Loadouts[1].Dye;
						array2 = clientPlayer.Loadouts[1].Dye;
					}
					else if (num5 >= PlayerItemSlotID.Loadout2_Armor_0)
					{
						num7 = num5 - PlayerItemSlotID.Loadout2_Armor_0;
						array = player3.Loadouts[1].Armor;
						array2 = clientPlayer.Loadouts[1].Armor;
					}
					else if (num5 >= PlayerItemSlotID.Loadout1_Dye_0)
					{
						num7 = num5 - PlayerItemSlotID.Loadout1_Dye_0;
						array = player3.Loadouts[0].Dye;
						array2 = clientPlayer.Loadouts[0].Dye;
					}
					else if (num5 >= PlayerItemSlotID.Loadout1_Armor_0)
					{
						num7 = num5 - PlayerItemSlotID.Loadout1_Armor_0;
						array = player3.Loadouts[0].Armor;
						array2 = clientPlayer.Loadouts[0].Armor;
					}
					else if (num5 >= PlayerItemSlotID.Bank4_0)
					{
						num7 = num5 - PlayerItemSlotID.Bank4_0;
						array = player3.bank4.item;
						array2 = clientPlayer.bank4.item;
						if (Main.netMode == 1 && player3.disableVoidBag == num7)
						{
							player3.disableVoidBag = -1;
							Recipe.FindRecipes(true);
						}
					}
					else if (num5 >= PlayerItemSlotID.Bank3_0)
					{
						num7 = num5 - PlayerItemSlotID.Bank3_0;
						array = player3.bank3.item;
						array2 = clientPlayer.bank3.item;
					}
					else if (num5 >= PlayerItemSlotID.TrashItem)
					{
						flag3 = true;
					}
					else if (num5 >= PlayerItemSlotID.Bank2_0)
					{
						num7 = num5 - PlayerItemSlotID.Bank2_0;
						array = player3.bank2.item;
						array2 = clientPlayer.bank2.item;
					}
					else if (num5 >= PlayerItemSlotID.Bank1_0)
					{
						num7 = num5 - PlayerItemSlotID.Bank1_0;
						array = player3.bank.item;
						array2 = clientPlayer.bank.item;
					}
					else if (num5 >= PlayerItemSlotID.MiscDye0)
					{
						num7 = num5 - PlayerItemSlotID.MiscDye0;
						array = player3.miscDyes;
						array2 = clientPlayer.miscDyes;
					}
					else if (num5 >= PlayerItemSlotID.Misc0)
					{
						num7 = num5 - PlayerItemSlotID.Misc0;
						array = player3.miscEquips;
						array2 = clientPlayer.miscEquips;
					}
					else if (num5 >= PlayerItemSlotID.Dye0)
					{
						num7 = num5 - PlayerItemSlotID.Dye0;
						array = player3.dye;
						array2 = clientPlayer.dye;
					}
					else if (num5 >= PlayerItemSlotID.Armor0)
					{
						num7 = num5 - PlayerItemSlotID.Armor0;
						array = player3.armor;
						array2 = clientPlayer.armor;
					}
					else
					{
						num7 = num5 - PlayerItemSlotID.Inventory0;
						array = player3.inventory;
						array2 = clientPlayer.inventory;
					}
					if (flag3)
					{
						player3.trashItem = new Item();
						player3.trashItem.netDefaults(type);
						player3.trashItem.stack = stack;
						player3.trashItem.Prefix(num6);
						if (num4 == Main.myPlayer && !Main.ServerSideCharacter)
						{
							clientPlayer.trashItem = player3.trashItem.Clone();
						}
					}
					else if (num5 <= 58)
					{
						int type2 = array[num7].type;
						int stack2 = array[num7].stack;
						array[num7] = new Item();
						array[num7].netDefaults(type);
						array[num7].stack = stack;
						array[num7].Prefix(num6);
						if (num4 == Main.myPlayer && !Main.ServerSideCharacter)
						{
							array2[num7] = array[num7].Clone();
						}
						if (num4 == Main.myPlayer && num7 == 58)
						{
							Main.mouseItem = array[num7].Clone();
						}
						if (num4 == Main.myPlayer && Main.netMode == 1)
						{
							Main.player[num4].inventoryChestStack[num5] = false;
							if (array[num7].stack != stack2 || array[num7].type != type2)
							{
								Recipe.FindRecipes(true);
							}
						}
					}
					else
					{
						array[num7] = new Item();
						array[num7].netDefaults(type);
						array[num7].stack = stack;
						array[num7].Prefix(num6);
						if (num4 == Main.myPlayer && !Main.ServerSideCharacter)
						{
							array2[num7] = array[num7].Clone();
						}
					}
					bool[] canRelay = PlayerItemSlotID.CanRelay;
					if (Main.netMode == 2 && num4 == this.whoAmI && canRelay.IndexInRange(num5) && canRelay[num5])
					{
						NetMessage.TrySendData(5, -1, this.whoAmI, null, num4, (float)num5, (float)num6, 0f, 0, 0, 0);
					}
					return;
				}
				break;
			}
			case 6:
				break;
			case 7:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				Main.time = (double)this.reader.ReadInt32();
				BitsByte bitsByte4 = this.reader.ReadByte();
				Main.dayTime = bitsByte4[0];
				Main.bloodMoon = bitsByte4[1];
				Main.eclipse = bitsByte4[2];
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
				for (int m = 0; m < 3; m++)
				{
					Main.treeX[m] = this.reader.ReadInt32();
				}
				for (int n = 0; n < 4; n++)
				{
					Main.treeStyle[n] = (int)this.reader.ReadByte();
				}
				for (int num8 = 0; num8 < 3; num8++)
				{
					Main.caveBackX[num8] = this.reader.ReadInt32();
				}
				for (int num9 = 0; num9 < 4; num9++)
				{
					Main.caveBackStyle[num9] = (int)this.reader.ReadByte();
				}
				WorldGen.TreeTops.SyncReceive(this.reader);
				WorldGen.BackgroundsCache.UpdateCache();
				Main.maxRaining = this.reader.ReadSingle();
				Main.raining = (Main.maxRaining > 0f);
				BitsByte bitsByte5 = this.reader.ReadByte();
				WorldGen.shadowOrbSmashed = bitsByte5[0];
				NPC.downedBoss1 = bitsByte5[1];
				NPC.downedBoss2 = bitsByte5[2];
				NPC.downedBoss3 = bitsByte5[3];
				Main.hardMode = bitsByte5[4];
				NPC.downedClown = bitsByte5[5];
				Main.ServerSideCharacter = bitsByte5[6];
				NPC.downedPlantBoss = bitsByte5[7];
				if (Main.ServerSideCharacter)
				{
					Main.ActivePlayerFileData.MarkAsServerSide();
				}
				BitsByte bitsByte6 = this.reader.ReadByte();
				NPC.downedMechBoss1 = bitsByte6[0];
				NPC.downedMechBoss2 = bitsByte6[1];
				NPC.downedMechBoss3 = bitsByte6[2];
				NPC.downedMechBossAny = bitsByte6[3];
				Main.cloudBGActive = (float)(bitsByte6[4] ? 1 : 0);
				WorldGen.crimson = bitsByte6[5];
				Main.pumpkinMoon = bitsByte6[6];
				Main.snowMoon = bitsByte6[7];
				BitsByte bitsByte7 = this.reader.ReadByte();
				Main.fastForwardTimeToDawn = bitsByte7[1];
				Main.UpdateTimeRate();
				bool flag4 = bitsByte7[2];
				NPC.downedSlimeKing = bitsByte7[3];
				NPC.downedQueenBee = bitsByte7[4];
				NPC.downedFishron = bitsByte7[5];
				NPC.downedMartians = bitsByte7[6];
				NPC.downedAncientCultist = bitsByte7[7];
				BitsByte bitsByte8 = this.reader.ReadByte();
				NPC.downedMoonlord = bitsByte8[0];
				NPC.downedHalloweenKing = bitsByte8[1];
				NPC.downedHalloweenTree = bitsByte8[2];
				NPC.downedChristmasIceQueen = bitsByte8[3];
				NPC.downedChristmasSantank = bitsByte8[4];
				NPC.downedChristmasTree = bitsByte8[5];
				NPC.downedGolemBoss = bitsByte8[6];
				BirthdayParty.ManualParty = bitsByte8[7];
				BitsByte bitsByte9 = this.reader.ReadByte();
				NPC.downedPirates = bitsByte9[0];
				NPC.downedFrost = bitsByte9[1];
				NPC.downedGoblins = bitsByte9[2];
				Sandstorm.Happening = bitsByte9[3];
				DD2Event.Ongoing = bitsByte9[4];
				DD2Event.DownedInvasionT1 = bitsByte9[5];
				DD2Event.DownedInvasionT2 = bitsByte9[6];
				DD2Event.DownedInvasionT3 = bitsByte9[7];
				BitsByte bitsByte10 = this.reader.ReadByte();
				NPC.combatBookWasUsed = bitsByte10[0];
				LanternNight.ManualLanterns = bitsByte10[1];
				NPC.downedTowerSolar = bitsByte10[2];
				NPC.downedTowerVortex = bitsByte10[3];
				NPC.downedTowerNebula = bitsByte10[4];
				NPC.downedTowerStardust = bitsByte10[5];
				Main.forceHalloweenForToday = bitsByte10[6];
				Main.forceXMasForToday = bitsByte10[7];
				BitsByte bitsByte11 = this.reader.ReadByte();
				NPC.boughtCat = bitsByte11[0];
				NPC.boughtDog = bitsByte11[1];
				NPC.boughtBunny = bitsByte11[2];
				NPC.freeCake = bitsByte11[3];
				Main.drunkWorld = bitsByte11[4];
				NPC.downedEmpressOfLight = bitsByte11[5];
				NPC.downedQueenSlime = bitsByte11[6];
				Main.getGoodWorld = bitsByte11[7];
				BitsByte bitsByte12 = this.reader.ReadByte();
				Main.tenthAnniversaryWorld = bitsByte12[0];
				Main.dontStarveWorld = bitsByte12[1];
				NPC.downedDeerclops = bitsByte12[2];
				Main.notTheBeesWorld = bitsByte12[3];
				Main.remixWorld = bitsByte12[4];
				NPC.unlockedSlimeBlueSpawn = bitsByte12[5];
				NPC.combatBookVolumeTwoWasUsed = bitsByte12[6];
				NPC.peddlersSatchelWasUsed = bitsByte12[7];
				BitsByte bitsByte13 = this.reader.ReadByte();
				NPC.unlockedSlimeGreenSpawn = bitsByte13[0];
				NPC.unlockedSlimeOldSpawn = bitsByte13[1];
				NPC.unlockedSlimePurpleSpawn = bitsByte13[2];
				NPC.unlockedSlimeRainbowSpawn = bitsByte13[3];
				NPC.unlockedSlimeRedSpawn = bitsByte13[4];
				NPC.unlockedSlimeYellowSpawn = bitsByte13[5];
				NPC.unlockedSlimeCopperSpawn = bitsByte13[6];
				Main.fastForwardTimeToDusk = bitsByte13[7];
				BitsByte bitsByte14 = this.reader.ReadByte();
				Main.noTrapsWorld = bitsByte14[0];
				Main.zenithWorld = bitsByte14[1];
				NPC.unlockedTruffleSpawn = bitsByte14[2];
				Main.sundialCooldown = (int)this.reader.ReadByte();
				Main.moondialCooldown = (int)this.reader.ReadByte();
				WorldGen.SavedOreTiers.Copper = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Iron = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Silver = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Gold = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Cobalt = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Mythril = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Adamantite = (int)this.reader.ReadInt16();
				if (flag4)
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
				if (Netplay.Connection.State == 3)
				{
					Main.windSpeedCurrent = Main.windSpeedTarget;
					Netplay.Connection.State = 4;
				}
				Main.checkHalloween();
				Main.checkXMas();
				return;
			}
			case 8:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				NetMessage.TrySendData(7, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				int num10 = this.reader.ReadInt32();
				int num11 = this.reader.ReadInt32();
				bool flag5 = true;
				if (num10 == -1 || num11 == -1)
				{
					flag5 = false;
				}
				else if (num10 < 10 || num10 > Main.maxTilesX - 10)
				{
					flag5 = false;
				}
				else if (num11 < 10 || num11 > Main.maxTilesY - 10)
				{
					flag5 = false;
				}
				int num12 = Netplay.GetSectionX(Main.spawnTileX) - 2;
				int num13 = Netplay.GetSectionY(Main.spawnTileY) - 1;
				int num14 = num12 + 5;
				int num15 = num13 + 3;
				if (num12 < 0)
				{
					num12 = 0;
				}
				if (num14 >= Main.maxSectionsX)
				{
					num14 = Main.maxSectionsX;
				}
				if (num13 < 0)
				{
					num13 = 0;
				}
				if (num15 >= Main.maxSectionsY)
				{
					num15 = Main.maxSectionsY;
				}
				int num16 = (num14 - num12) * (num15 - num13);
				List<Point> list = new List<Point>();
				for (int num17 = num12; num17 < num14; num17++)
				{
					for (int num18 = num13; num18 < num15; num18++)
					{
						list.Add(new Point(num17, num18));
					}
				}
				int num19 = -1;
				int num20 = -1;
				if (flag5)
				{
					num10 = Netplay.GetSectionX(num10) - 2;
					num11 = Netplay.GetSectionY(num11) - 1;
					num19 = num10 + 5;
					num20 = num11 + 3;
					if (num10 < 0)
					{
						num10 = 0;
					}
					if (num19 >= Main.maxSectionsX)
					{
						num19 = Main.maxSectionsX - 1;
					}
					if (num11 < 0)
					{
						num11 = 0;
					}
					if (num20 >= Main.maxSectionsY)
					{
						num20 = Main.maxSectionsY - 1;
					}
					for (int num21 = num10; num21 <= num19; num21++)
					{
						for (int num22 = num11; num22 <= num20; num22++)
						{
							if (num21 < num12 || num21 >= num14 || num22 < num13 || num22 >= num15)
							{
								list.Add(new Point(num21, num22));
								num16++;
							}
						}
					}
				}
				List<Point> list2;
				PortalHelper.SyncPortalsOnPlayerJoin(this.whoAmI, 1, list, out list2);
				num16 += list2.Count;
				if (Netplay.Clients[this.whoAmI].State == 2)
				{
					Netplay.Clients[this.whoAmI].State = 3;
				}
				NetMessage.TrySendData(9, this.whoAmI, -1, Lang.inter[44].ToNetworkText(), num16, 0f, 0f, 0f, 0, 0, 0);
				Netplay.Clients[this.whoAmI].StatusText2 = Language.GetTextValue("Net.IsReceivingTileData");
				Netplay.Clients[this.whoAmI].StatusMax += num16;
				for (int num23 = num12; num23 < num14; num23++)
				{
					for (int num24 = num13; num24 < num15; num24++)
					{
						NetMessage.SendSection(this.whoAmI, num23, num24);
					}
				}
				if (flag5)
				{
					for (int num25 = num10; num25 <= num19; num25++)
					{
						for (int num26 = num11; num26 <= num20; num26++)
						{
							NetMessage.SendSection(this.whoAmI, num25, num26);
						}
					}
				}
				for (int num27 = 0; num27 < list2.Count; num27++)
				{
					NetMessage.SendSection(this.whoAmI, list2[num27].X, list2[num27].Y);
				}
				for (int num28 = 0; num28 < 400; num28++)
				{
					if (Main.item[num28].active)
					{
						NetMessage.TrySendData(21, this.whoAmI, -1, null, num28, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(22, this.whoAmI, -1, null, num28, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				for (int num29 = 0; num29 < 200; num29++)
				{
					if (Main.npc[num29].active)
					{
						NetMessage.TrySendData(23, this.whoAmI, -1, null, num29, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				for (int num30 = 0; num30 < 1000; num30++)
				{
					if (Main.projectile[num30].active && (Main.projPet[Main.projectile[num30].type] || Main.projectile[num30].netImportant))
					{
						NetMessage.TrySendData(27, this.whoAmI, -1, null, num30, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				for (int num31 = 0; num31 < 290; num31++)
				{
					NetMessage.TrySendData(83, this.whoAmI, -1, null, num31, 0f, 0f, 0f, 0, 0, 0);
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
			case 9:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				Netplay.Connection.StatusMax += this.reader.ReadInt32();
				Netplay.Connection.StatusText = NetworkText.Deserialize(this.reader).ToString();
				BitsByte bitsByte15 = this.reader.ReadByte();
				BitsByte serverSpecialFlags = Netplay.Connection.ServerSpecialFlags;
				serverSpecialFlags[0] = bitsByte15[0];
				serverSpecialFlags[1] = bitsByte15[1];
				Netplay.Connection.ServerSpecialFlags = serverSpecialFlags;
				return;
			}
			case 10:
				if (Main.netMode != 1)
				{
					return;
				}
				NetMessage.DecompressTileBlock(this.reader.BaseStream);
				return;
			case 11:
				if (Main.netMode != 1)
				{
					return;
				}
				WorldGen.SectionTileFrame((int)this.reader.ReadInt16(), (int)this.reader.ReadInt16(), (int)this.reader.ReadInt16(), (int)this.reader.ReadInt16());
				return;
			case 12:
			{
				int num32 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num32 = this.whoAmI;
				}
				Player player4 = Main.player[num32];
				player4.SpawnX = (int)this.reader.ReadInt16();
				player4.SpawnY = (int)this.reader.ReadInt16();
				player4.respawnTimer = this.reader.ReadInt32();
				player4.numberOfDeathsPVE = (int)this.reader.ReadInt16();
				player4.numberOfDeathsPVP = (int)this.reader.ReadInt16();
				if (player4.respawnTimer > 0)
				{
					player4.dead = true;
				}
				PlayerSpawnContext playerSpawnContext = (PlayerSpawnContext)this.reader.ReadByte();
				player4.Spawn(playerSpawnContext);
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
				bool flag6 = NetMessage.DoesPlayerSlotCountAsAHost(this.whoAmI);
				Main.countsAsHostForGameplay[this.whoAmI] = flag6;
				if (NetMessage.DoesPlayerSlotCountAsAHost(this.whoAmI))
				{
					NetMessage.TrySendData(139, this.whoAmI, -1, null, this.whoAmI, (float)flag6.ToInt(), 0f, 0f, 0, 0, 0);
				}
				NetMessage.TrySendData(12, -1, this.whoAmI, null, this.whoAmI, (float)((byte)playerSpawnContext), 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(74, this.whoAmI, -1, NetworkText.FromLiteral(Main.player[this.whoAmI].name), Main.anglerQuest, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(129, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.greetPlayer(this.whoAmI);
				if (Main.player[num32].unlockedBiomeTorches)
				{
					NPC npc = new NPC();
					npc.SetDefaults(664, default(NPCSpawnParams));
					Main.BestiaryTracker.Kills.RegisterKill(npc);
					return;
				}
				return;
			}
			case 13:
			{
				int num33 = (int)this.reader.ReadByte();
				if (num33 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num33 = this.whoAmI;
				}
				Player player5 = Main.player[num33];
				BitsByte bitsByte16 = this.reader.ReadByte();
				BitsByte bitsByte17 = this.reader.ReadByte();
				BitsByte bitsByte18 = this.reader.ReadByte();
				BitsByte bitsByte19 = this.reader.ReadByte();
				player5.controlUp = bitsByte16[0];
				player5.controlDown = bitsByte16[1];
				player5.controlLeft = bitsByte16[2];
				player5.controlRight = bitsByte16[3];
				player5.controlJump = bitsByte16[4];
				player5.controlUseItem = bitsByte16[5];
				player5.direction = (bitsByte16[6] ? 1 : -1);
				if (bitsByte17[0])
				{
					player5.pulley = true;
					player5.pulleyDir = (bitsByte17[1] ? 2 : 1);
				}
				else
				{
					player5.pulley = false;
				}
				player5.vortexStealthActive = bitsByte17[3];
				player5.gravDir = (float)(bitsByte17[4] ? 1 : -1);
				player5.TryTogglingShield(bitsByte17[5]);
				player5.ghost = bitsByte17[6];
				player5.selectedItem = (int)this.reader.ReadByte();
				player5.position = this.reader.ReadVector2();
				if (bitsByte17[2])
				{
					player5.velocity = this.reader.ReadVector2();
				}
				else
				{
					player5.velocity = Vector2.Zero;
				}
				if (bitsByte18[6])
				{
					player5.PotionOfReturnOriginalUsePosition = new Vector2?(this.reader.ReadVector2());
					player5.PotionOfReturnHomePosition = new Vector2?(this.reader.ReadVector2());
				}
				else
				{
					player5.PotionOfReturnOriginalUsePosition = null;
					player5.PotionOfReturnHomePosition = null;
				}
				player5.tryKeepingHoveringUp = bitsByte18[0];
				player5.IsVoidVaultEnabled = bitsByte18[1];
				player5.sitting.isSitting = bitsByte18[2];
				player5.downedDD2EventAnyDifficulty = bitsByte18[3];
				player5.isPettingAnimal = bitsByte18[4];
				player5.isTheAnimalBeingPetSmall = bitsByte18[5];
				player5.tryKeepingHoveringDown = bitsByte18[7];
				player5.sleeping.SetIsSleepingAndAdjustPlayerRotation(player5, bitsByte19[0]);
				player5.autoReuseAllWeapons = bitsByte19[1];
				player5.controlDownHold = bitsByte19[2];
				player5.isOperatingAnotherEntity = bitsByte19[3];
				player5.controlUseTile = bitsByte19[4];
				if (Main.netMode == 2 && Netplay.Clients[this.whoAmI].State == 10)
				{
					NetMessage.TrySendData(13, -1, this.whoAmI, null, num33, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 14:
			{
				int num34 = (int)this.reader.ReadByte();
				int num35 = (int)this.reader.ReadByte();
				if (Main.netMode != 1)
				{
					return;
				}
				bool active = Main.player[num34].active;
				if (num35 == 1)
				{
					if (!Main.player[num34].active)
					{
						Main.player[num34] = new Player();
					}
					Main.player[num34].active = true;
				}
				else
				{
					Main.player[num34].active = false;
				}
				if (active == Main.player[num34].active)
				{
					return;
				}
				if (Main.player[num34].active)
				{
					Player.Hooks.PlayerConnect(num34);
					return;
				}
				Player.Hooks.PlayerDisconnect(num34);
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
				int num36 = (int)this.reader.ReadByte();
				if (num36 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num36 = this.whoAmI;
				}
				Player player6 = Main.player[num36];
				player6.statLife = (int)this.reader.ReadInt16();
				player6.statLifeMax = (int)this.reader.ReadInt16();
				if (player6.statLifeMax < 100)
				{
					player6.statLifeMax = 100;
				}
				player6.dead = (player6.statLife <= 0);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(16, -1, this.whoAmI, null, num36, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 17:
			{
				byte b3 = this.reader.ReadByte();
				int num37 = (int)this.reader.ReadInt16();
				int num38 = (int)this.reader.ReadInt16();
				short num39 = this.reader.ReadInt16();
				int num40 = (int)this.reader.ReadByte();
				bool flag7 = num39 == 1;
				if (!WorldGen.InWorld(num37, num38, 3))
				{
					return;
				}
				if (Main.tile[num37, num38] == null)
				{
					Main.tile[num37, num38] = new Tile();
				}
				if (Main.netMode == 2)
				{
					if (!flag7)
					{
						if (b3 == 0 || b3 == 2 || b3 == 4)
						{
							Netplay.Clients[this.whoAmI].SpamDeleteBlock += 1f;
						}
						if (b3 == 1 || b3 == 3)
						{
							Netplay.Clients[this.whoAmI].SpamAddBlock += 1f;
						}
					}
					if (!Netplay.Clients[this.whoAmI].TileSections[Netplay.GetSectionX(num37), Netplay.GetSectionY(num38)])
					{
						flag7 = true;
					}
				}
				if (b3 == 0)
				{
					WorldGen.KillTile(num37, num38, flag7, false, false);
					if (Main.netMode == 1 && !flag7)
					{
						HitTile.ClearAllTilesAtThisLocation(num37, num38);
					}
				}
				bool flag8 = false;
				if (b3 == 1)
				{
					bool forced = true;
					if (WorldGen.CheckTileBreakability2_ShouldTileSurvive(num37, num38))
					{
						flag8 = true;
						forced = false;
					}
					WorldGen.PlaceTile(num37, num38, (int)num39, false, forced, -1, num40);
				}
				if (b3 == 2)
				{
					WorldGen.KillWall(num37, num38, flag7);
				}
				if (b3 == 3)
				{
					WorldGen.PlaceWall(num37, num38, (int)num39, false);
				}
				if (b3 == 4)
				{
					WorldGen.KillTile(num37, num38, flag7, false, true);
				}
				if (b3 == 5)
				{
					WorldGen.PlaceWire(num37, num38);
				}
				if (b3 == 6)
				{
					WorldGen.KillWire(num37, num38);
				}
				if (b3 == 7)
				{
					WorldGen.PoundTile(num37, num38);
				}
				if (b3 == 8)
				{
					WorldGen.PlaceActuator(num37, num38);
				}
				if (b3 == 9)
				{
					WorldGen.KillActuator(num37, num38);
				}
				if (b3 == 10)
				{
					WorldGen.PlaceWire2(num37, num38);
				}
				if (b3 == 11)
				{
					WorldGen.KillWire2(num37, num38);
				}
				if (b3 == 12)
				{
					WorldGen.PlaceWire3(num37, num38);
				}
				if (b3 == 13)
				{
					WorldGen.KillWire3(num37, num38);
				}
				if (b3 == 14)
				{
					WorldGen.SlopeTile(num37, num38, (int)num39, false);
				}
				if (b3 == 15)
				{
					Minecart.FrameTrack(num37, num38, true, false);
				}
				if (b3 == 16)
				{
					WorldGen.PlaceWire4(num37, num38);
				}
				if (b3 == 17)
				{
					WorldGen.KillWire4(num37, num38);
				}
				if (b3 == 18)
				{
					Wiring.SetCurrentUser(this.whoAmI);
					Wiring.PokeLogicGate(num37, num38);
					Wiring.SetCurrentUser(-1);
					return;
				}
				if (b3 == 19)
				{
					Wiring.SetCurrentUser(this.whoAmI);
					Wiring.Actuate(num37, num38);
					Wiring.SetCurrentUser(-1);
					return;
				}
				if (b3 == 20)
				{
					if (!WorldGen.InWorld(num37, num38, 2))
					{
						return;
					}
					int type3 = (int)Main.tile[num37, num38].type;
					WorldGen.KillTile(num37, num38, flag7, false, false);
					num39 = ((Main.tile[num37, num38].active() && (int)Main.tile[num37, num38].type == type3) ? 1 : 0);
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(17, -1, -1, null, (int)b3, (float)num37, (float)num38, (float)num39, num40, 0, 0);
						return;
					}
					return;
				}
				else
				{
					if (b3 == 21)
					{
						WorldGen.ReplaceTile(num37, num38, (ushort)num39, num40);
					}
					if (b3 == 22)
					{
						WorldGen.ReplaceWall(num37, num38, (ushort)num39);
					}
					if (b3 == 23)
					{
						WorldGen.SlopeTile(num37, num38, (int)num39, false);
						WorldGen.PoundTile(num37, num38);
					}
					if (Main.netMode != 2)
					{
						return;
					}
					if (flag8)
					{
						NetMessage.SendTileSquare(-1, num37, num38, 5, TileChangeType.None);
						return;
					}
					if ((b3 != 1 && b3 != 21) || !TileID.Sets.Falling[(int)num39] || Main.tile[num37, num38].active())
					{
						NetMessage.TrySendData(17, -1, this.whoAmI, null, (int)b3, (float)num37, (float)num38, (float)num39, num40, 0, 0);
						return;
					}
					return;
				}
				break;
			}
			case 18:
				if (Main.netMode != 1)
				{
					return;
				}
				Main.dayTime = (this.reader.ReadByte() == 1);
				Main.time = (double)this.reader.ReadInt32();
				Main.sunModY = this.reader.ReadInt16();
				Main.moonModY = this.reader.ReadInt16();
				return;
			case 19:
			{
				byte b4 = this.reader.ReadByte();
				int num41 = (int)this.reader.ReadInt16();
				int num42 = (int)this.reader.ReadInt16();
				if (!WorldGen.InWorld(num41, num42, 3))
				{
					return;
				}
				int num43 = (this.reader.ReadByte() == 0) ? -1 : 1;
				if (b4 == 0)
				{
					WorldGen.OpenDoor(num41, num42, num43);
				}
				else if (b4 == 1)
				{
					WorldGen.CloseDoor(num41, num42, true);
				}
				else if (b4 == 2)
				{
					WorldGen.ShiftTrapdoor(num41, num42, num43 == 1, 1);
				}
				else if (b4 == 3)
				{
					WorldGen.ShiftTrapdoor(num41, num42, num43 == 1, 0);
				}
				else if (b4 == 4)
				{
					WorldGen.ShiftTallGate(num41, num42, false, true);
				}
				else if (b4 == 5)
				{
					WorldGen.ShiftTallGate(num41, num42, true, true);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(19, -1, this.whoAmI, null, (int)b4, (float)num41, (float)num42, (float)((num43 == 1) ? 1 : 0), 0, 0, 0);
					return;
				}
				return;
			}
			case 20:
			{
				int num44 = (int)this.reader.ReadInt16();
				int num45 = (int)this.reader.ReadInt16();
				ushort num46 = (ushort)this.reader.ReadByte();
				ushort num47 = (ushort)this.reader.ReadByte();
				byte b5 = this.reader.ReadByte();
				if (!WorldGen.InWorld(num44, num45, 3))
				{
					return;
				}
				TileChangeType type4 = TileChangeType.None;
				if (Enum.IsDefined(typeof(TileChangeType), b5))
				{
					type4 = (TileChangeType)b5;
				}
				if (MessageBuffer.OnTileChangeReceived != null)
				{
					MessageBuffer.OnTileChangeReceived(num44, num45, (int)Math.Max(num46, num47), type4);
				}
				BitsByte bitsByte20 = 0;
				BitsByte bitsByte21 = 0;
				BitsByte bitsByte22 = 0;
				for (int num48 = num44; num48 < num44 + (int)num46; num48++)
				{
					for (int num49 = num45; num49 < num45 + (int)num47; num49++)
					{
						if (Main.tile[num48, num49] == null)
						{
							Main.tile[num48, num49] = new Tile();
						}
						Tile tile = Main.tile[num48, num49];
						bool flag9 = tile.active();
						bitsByte20 = this.reader.ReadByte();
						bitsByte21 = this.reader.ReadByte();
						bitsByte22 = this.reader.ReadByte();
						tile.active(bitsByte20[0]);
						tile.wall = (ushort)(bitsByte20[2] ? 1 : 0);
						bool flag10 = bitsByte20[3];
						if (Main.netMode != 2)
						{
							tile.liquid = (flag10 ? 1 : 0);
						}
						tile.wire(bitsByte20[4]);
						tile.halfBrick(bitsByte20[5]);
						tile.actuator(bitsByte20[6]);
						tile.inActive(bitsByte20[7]);
						tile.wire2(bitsByte21[0]);
						tile.wire3(bitsByte21[1]);
						if (bitsByte21[2])
						{
							tile.color(this.reader.ReadByte());
						}
						if (bitsByte21[3])
						{
							tile.wallColor(this.reader.ReadByte());
						}
						if (tile.active())
						{
							int type5 = (int)tile.type;
							tile.type = this.reader.ReadUInt16();
							if (Main.tileFrameImportant[(int)tile.type])
							{
								tile.frameX = this.reader.ReadInt16();
								tile.frameY = this.reader.ReadInt16();
							}
							else if (!flag9 || (int)tile.type != type5)
							{
								tile.frameX = -1;
								tile.frameY = -1;
							}
							byte b6 = 0;
							if (bitsByte21[4])
							{
								b6 += 1;
							}
							if (bitsByte21[5])
							{
								b6 += 2;
							}
							if (bitsByte21[6])
							{
								b6 += 4;
							}
							tile.slope(b6);
						}
						tile.wire4(bitsByte21[7]);
						tile.fullbrightBlock(bitsByte22[0]);
						tile.fullbrightWall(bitsByte22[1]);
						tile.invisibleBlock(bitsByte22[2]);
						tile.invisibleWall(bitsByte22[3]);
						if (tile.wall > 0)
						{
							tile.wall = this.reader.ReadUInt16();
						}
						if (flag10)
						{
							tile.liquid = this.reader.ReadByte();
							tile.liquidType((int)this.reader.ReadByte());
						}
					}
				}
				WorldGen.RangeFrame(num44, num45, num44 + (int)num46, num45 + (int)num47);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, this.whoAmI, null, num44, (float)num45, (float)num46, (float)num47, (int)b5, 0, 0);
					return;
				}
				return;
			}
			case 21:
			case 90:
			case 145:
			case 148:
			{
				int num50 = (int)this.reader.ReadInt16();
				Vector2 vector = this.reader.ReadVector2();
				Vector2 velocity = this.reader.ReadVector2();
				int stack3 = (int)this.reader.ReadInt16();
				int prefixWeWant = (int)this.reader.ReadByte();
				int num51 = (int)this.reader.ReadByte();
				int num52 = (int)this.reader.ReadInt16();
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
					if (num52 == 0)
					{
						Main.item[num50].active = false;
						return;
					}
					int num53 = num50;
					Item item = Main.item[num53];
					ItemSyncPersistentStats itemSyncPersistentStats = default(ItemSyncPersistentStats);
					itemSyncPersistentStats.CopyFrom(item);
					bool newAndShiny = (item.newAndShiny || item.netID != num52) && ItemSlot.Options.HighlightNewItems && (num52 < 0 || num52 >= (int)ItemID.Count || !ItemID.Sets.NeverAppearsAsNewInInventory[num52]);
					item.netDefaults(num52);
					item.newAndShiny = newAndShiny;
					item.Prefix(prefixWeWant);
					item.stack = stack3;
					item.position = vector;
					item.velocity = velocity;
					item.active = true;
					item.shimmered = shimmered;
					item.shimmerTime = shimmerTime;
					if (b == 90)
					{
						item.instanced = true;
						item.playerIndexTheItemIsReservedFor = Main.myPlayer;
						item.keepTime = 600;
					}
					item.timeLeftInWhichTheItemCannotBeTakenByEnemies = timeLeftInWhichTheItemCannotBeTakenByEnemies;
					item.wet = Collision.WetCollision(item.position, item.width, item.height);
					itemSyncPersistentStats.PasteInto(item);
					return;
				}
				else
				{
					if (Main.timeItemSlotCannotBeReusedFor[num50] > 0)
					{
						return;
					}
					if (num52 == 0)
					{
						if (num50 < 400)
						{
							Main.item[num50].active = false;
							NetMessage.TrySendData(21, -1, -1, null, num50, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						return;
					}
					else
					{
						bool flag11 = false;
						if (num50 == 400)
						{
							flag11 = true;
						}
						if (flag11)
						{
							Item item2 = new Item();
							item2.netDefaults(num52);
							num50 = Item.NewItem(new EntitySource_Sync(), (int)vector.X, (int)vector.Y, item2.width, item2.height, item2.type, stack3, true, 0, false, false);
						}
						Item item3 = Main.item[num50];
						item3.netDefaults(num52);
						item3.Prefix(prefixWeWant);
						item3.stack = stack3;
						item3.position = vector;
						item3.velocity = velocity;
						item3.active = true;
						item3.playerIndexTheItemIsReservedFor = Main.myPlayer;
						item3.timeLeftInWhichTheItemCannotBeTakenByEnemies = timeLeftInWhichTheItemCannotBeTakenByEnemies;
						if (b == 145)
						{
							item3.shimmered = shimmered;
							item3.shimmerTime = shimmerTime;
						}
						if (flag11)
						{
							NetMessage.TrySendData((int)b, -1, -1, null, num50, 0f, 0f, 0f, 0, 0, 0);
							if (num51 == 0)
							{
								Main.item[num50].ownIgnore = this.whoAmI;
								Main.item[num50].ownTime = 100;
							}
							Main.item[num50].FindOwner(num50);
							return;
						}
						NetMessage.TrySendData((int)b, -1, this.whoAmI, null, num50, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
				}
				break;
			}
			case 22:
			{
				int num54 = (int)this.reader.ReadInt16();
				int num55 = (int)this.reader.ReadByte();
				if (Main.netMode == 2 && Main.item[num54].playerIndexTheItemIsReservedFor != this.whoAmI)
				{
					return;
				}
				Main.item[num54].playerIndexTheItemIsReservedFor = num55;
				if (num55 == Main.myPlayer)
				{
					Main.item[num54].keepTime = 15;
				}
				else
				{
					Main.item[num54].keepTime = 0;
				}
				if (Main.netMode == 2)
				{
					Main.item[num54].playerIndexTheItemIsReservedFor = 255;
					Main.item[num54].keepTime = 15;
					NetMessage.TrySendData(22, -1, -1, null, num54, 0f, 0f, 0f, 0, 0, 0);
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
				int num56 = (int)this.reader.ReadInt16();
				Vector2 vector2 = this.reader.ReadVector2();
				Vector2 velocity2 = this.reader.ReadVector2();
				int num57 = (int)this.reader.ReadUInt16();
				if (num57 == 65535)
				{
					num57 = 0;
				}
				BitsByte bitsByte23 = this.reader.ReadByte();
				BitsByte bitsByte24 = this.reader.ReadByte();
				float[] array3 = this.ReUseTemporaryNPCAI();
				for (int num58 = 0; num58 < NPC.maxAI; num58++)
				{
					if (bitsByte23[num58 + 2])
					{
						array3[num58] = this.reader.ReadSingle();
					}
					else
					{
						array3[num58] = 0f;
					}
				}
				int num59 = (int)this.reader.ReadInt16();
				int? playerCountForMultiplayerDifficultyOverride = new int?(1);
				if (bitsByte24[0])
				{
					playerCountForMultiplayerDifficultyOverride = new int?((int)this.reader.ReadByte());
				}
				float value2 = 1f;
				if (bitsByte24[2])
				{
					value2 = this.reader.ReadSingle();
				}
				int num60 = 0;
				if (!bitsByte23[7])
				{
					byte b7 = this.reader.ReadByte();
					if (b7 == 2)
					{
						num60 = (int)this.reader.ReadInt16();
					}
					else if (b7 == 4)
					{
						num60 = this.reader.ReadInt32();
					}
					else
					{
						num60 = (int)this.reader.ReadSByte();
					}
				}
				int num61 = -1;
				NPC npc2 = Main.npc[num56];
				if (npc2.active && Main.multiplayerNPCSmoothingRange > 0 && Vector2.DistanceSquared(npc2.position, vector2) < 640000f)
				{
					npc2.netOffset += npc2.position - vector2;
				}
				if (!npc2.active || npc2.netID != num59)
				{
					npc2.netOffset *= 0f;
					if (npc2.active)
					{
						num61 = npc2.type;
					}
					npc2.active = true;
					npc2.SetDefaults(num59, new NPCSpawnParams
					{
						playerCountForMultiplayerDifficultyOverride = playerCountForMultiplayerDifficultyOverride,
						strengthMultiplierOverride = new float?(value2)
					});
				}
				npc2.position = vector2;
				npc2.velocity = velocity2;
				npc2.target = num57;
				npc2.direction = (bitsByte23[0] ? 1 : -1);
				npc2.directionY = (bitsByte23[1] ? 1 : -1);
				npc2.spriteDirection = (bitsByte23[6] ? 1 : -1);
				if (bitsByte23[7])
				{
					num60 = (npc2.life = npc2.lifeMax);
				}
				else
				{
					npc2.life = num60;
				}
				if (num60 <= 0)
				{
					npc2.active = false;
				}
				npc2.SpawnedFromStatue = bitsByte24[1];
				if (npc2.SpawnedFromStatue)
				{
					npc2.value = 0f;
				}
				for (int num62 = 0; num62 < NPC.maxAI; num62++)
				{
					npc2.ai[num62] = array3[num62];
				}
				if (num61 > -1 && num61 != npc2.type)
				{
					npc2.TransformVisuals(num61, npc2.type);
				}
				if (num59 == 262)
				{
					NPC.plantBoss = num56;
				}
				if (num59 == 245)
				{
					NPC.golemBoss = num56;
				}
				if (num59 == 668)
				{
					NPC.deerclopsBoss = num56;
				}
				if (npc2.type >= 0 && npc2.type < (int)NPCID.Count && Main.npcCatchable[npc2.type])
				{
					npc2.releaseOwner = (short)this.reader.ReadByte();
					return;
				}
				return;
			}
			case 24:
			{
				int num63 = (int)this.reader.ReadInt16();
				int num64 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num64 = this.whoAmI;
				}
				Player player7 = Main.player[num64];
				Main.npc[num63].StrikeNPC(player7.inventory[player7.selectedItem].damage, player7.inventory[player7.selectedItem].knockBack, player7.direction, false, false, false);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(24, -1, this.whoAmI, null, num63, (float)num64, 0f, 0f, 0, 0, 0);
					NetMessage.TrySendData(23, -1, -1, null, num63, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 27:
			{
				int num65 = (int)this.reader.ReadInt16();
				Vector2 position = this.reader.ReadVector2();
				Vector2 velocity3 = this.reader.ReadVector2();
				int num66 = (int)this.reader.ReadByte();
				int num67 = (int)this.reader.ReadInt16();
				BitsByte bitsByte25 = this.reader.ReadByte();
				BitsByte bitsByte26 = bitsByte25[2] ? this.reader.ReadByte() : 0;
				float[] array4 = this.ReUseTemporaryProjectileAI();
				array4[0] = (bitsByte25[0] ? this.reader.ReadSingle() : 0f);
				array4[1] = (bitsByte25[1] ? this.reader.ReadSingle() : 0f);
				int bannerIdToRespondTo = (int)(bitsByte25[3] ? this.reader.ReadUInt16() : 0);
				int damage = (int)(bitsByte25[4] ? this.reader.ReadInt16() : 0);
				float knockBack = bitsByte25[5] ? this.reader.ReadSingle() : 0f;
				int originalDamage = (int)(bitsByte25[6] ? this.reader.ReadInt16() : 0);
				int num68 = (int)(bitsByte25[7] ? this.reader.ReadInt16() : -1);
				if (num68 >= 1000)
				{
					num68 = -1;
				}
				array4[2] = (bitsByte26[0] ? this.reader.ReadSingle() : 0f);
				if (Main.netMode == 2)
				{
					if (num67 == 949)
					{
						num66 = 255;
					}
					else
					{
						num66 = this.whoAmI;
						if (Main.projHostile[num67])
						{
							return;
						}
					}
				}
				int num69 = 1000;
				for (int num70 = 0; num70 < 1000; num70++)
				{
					if (Main.projectile[num70].owner == num66 && Main.projectile[num70].identity == num65 && Main.projectile[num70].active)
					{
						num69 = num70;
						break;
					}
				}
				if (num69 == 1000)
				{
					for (int num71 = 0; num71 < 1000; num71++)
					{
						if (!Main.projectile[num71].active)
						{
							num69 = num71;
							break;
						}
					}
				}
				if (num69 == 1000)
				{
					num69 = Projectile.FindOldestProjectile();
				}
				Projectile projectile = Main.projectile[num69];
				if (!projectile.active || projectile.type != num67)
				{
					projectile.SetDefaults(num67);
					if (Main.netMode == 2)
					{
						Netplay.Clients[this.whoAmI].SpamProjectile += 1f;
					}
				}
				projectile.identity = num65;
				projectile.position = position;
				projectile.velocity = velocity3;
				projectile.type = num67;
				projectile.damage = damage;
				projectile.bannerIdToRespondTo = bannerIdToRespondTo;
				projectile.originalDamage = originalDamage;
				projectile.knockBack = knockBack;
				projectile.owner = num66;
				for (int num72 = 0; num72 < Projectile.maxAI; num72++)
				{
					projectile.ai[num72] = array4[num72];
				}
				if (num68 >= 0)
				{
					projectile.projUUID = num68;
					Main.projectileIdentity[num66, num68] = num69;
				}
				projectile.ProjectileFixDesperation();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(27, -1, this.whoAmI, null, num69, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 28:
			{
				int num73 = (int)this.reader.ReadInt16();
				int num74 = (int)this.reader.ReadInt16();
				float num75 = this.reader.ReadSingle();
				int num76 = (int)(this.reader.ReadByte() - 1);
				byte b8 = this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					if (num74 < 0)
					{
						num74 = 0;
					}
					Main.npc[num73].PlayerInteraction(this.whoAmI);
				}
				if (num74 >= 0)
				{
					Main.npc[num73].StrikeNPC(num74, num75, num76, b8 == 1, false, true);
				}
				else
				{
					Main.npc[num73].life = 0;
					Main.npc[num73].HitEffect(0, 10.0);
					Main.npc[num73].active = false;
				}
				if (Main.netMode != 2)
				{
					return;
				}
				NetMessage.TrySendData(28, -1, this.whoAmI, null, num73, (float)num74, num75, (float)num76, (int)b8, 0, 0);
				if (Main.npc[num73].life <= 0)
				{
					NetMessage.TrySendData(23, -1, -1, null, num73, 0f, 0f, 0f, 0, 0, 0);
				}
				else
				{
					Main.npc[num73].netUpdate = true;
				}
				if (Main.npc[num73].realLife < 0)
				{
					return;
				}
				if (Main.npc[Main.npc[num73].realLife].life <= 0)
				{
					NetMessage.TrySendData(23, -1, -1, null, Main.npc[num73].realLife, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				Main.npc[Main.npc[num73].realLife].netUpdate = true;
				return;
			}
			case 29:
			{
				int num77 = (int)this.reader.ReadInt16();
				int num78 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num78 = this.whoAmI;
				}
				for (int num79 = 0; num79 < 1000; num79++)
				{
					if (Main.projectile[num79].owner == num78 && Main.projectile[num79].identity == num77 && Main.projectile[num79].active)
					{
						Main.projectile[num79].Kill();
						break;
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(29, -1, this.whoAmI, null, num77, (float)num78, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 30:
			{
				int num80 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num80 = this.whoAmI;
				}
				bool flag12 = this.reader.ReadBoolean();
				Main.player[num80].hostile = flag12;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(30, -1, this.whoAmI, null, num80, 0f, 0f, 0f, 0, 0, 0);
					LocalizedText localizedText = flag12 ? Lang.mp[11] : Lang.mp[12];
					Color color = Main.teamColor[Main.player[num80].team];
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(localizedText.Key, new object[]
					{
						Main.player[num80].name
					}), color, -1);
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
				int num81 = (int)this.reader.ReadInt16();
				int num82 = (int)this.reader.ReadInt16();
				int num83 = Chest.FindChest(num81, num82);
				if (num83 <= -1 || Chest.UsingChest(num83) != -1)
				{
					return;
				}
				for (int num84 = 0; num84 < 40; num84++)
				{
					NetMessage.TrySendData(32, this.whoAmI, -1, null, num83, (float)num84, 0f, 0f, 0, 0, 0);
				}
				NetMessage.TrySendData(33, this.whoAmI, -1, null, num83, 0f, 0f, 0f, 0, 0, 0);
				Main.player[this.whoAmI].chest = num83;
				if (Main.myPlayer == this.whoAmI)
				{
					Main.recBigList = false;
				}
				NetMessage.TrySendData(80, -1, this.whoAmI, null, this.whoAmI, (float)num83, 0f, 0f, 0, 0, 0);
				if (Main.netMode == 2 && WorldGen.IsChestRigged(num81, num82))
				{
					Wiring.SetCurrentUser(this.whoAmI);
					Wiring.HitSwitch(num81, num82);
					Wiring.SetCurrentUser(-1);
					NetMessage.TrySendData(59, -1, this.whoAmI, null, num81, (float)num82, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 32:
			{
				int num85 = (int)this.reader.ReadInt16();
				int num86 = (int)this.reader.ReadByte();
				int stack4 = (int)this.reader.ReadInt16();
				int prefixWeWant2 = (int)this.reader.ReadByte();
				int type6 = (int)this.reader.ReadInt16();
				if (num85 < 0 || num85 >= 8000)
				{
					return;
				}
				if (Main.chest[num85] == null)
				{
					Main.chest[num85] = new Chest(false);
				}
				if (Main.chest[num85].item[num86] == null)
				{
					Main.chest[num85].item[num86] = new Item();
				}
				Main.chest[num85].item[num86].netDefaults(type6);
				Main.chest[num85].item[num86].Prefix(prefixWeWant2);
				Main.chest[num85].item[num86].stack = stack4;
				Recipe.FindRecipes(true);
				return;
			}
			case 33:
			{
				int num87 = (int)this.reader.ReadInt16();
				int num88 = (int)this.reader.ReadInt16();
				int num89 = (int)this.reader.ReadInt16();
				int num90 = (int)this.reader.ReadByte();
				string name = string.Empty;
				if (num90 != 0)
				{
					if (num90 <= 20)
					{
						name = this.reader.ReadString();
					}
					else if (num90 != 255)
					{
						num90 = 0;
					}
				}
				if (Main.netMode != 1)
				{
					if (num90 != 0)
					{
						int chest = Main.player[this.whoAmI].chest;
						Chest chest2 = Main.chest[chest];
						chest2.name = name;
						NetMessage.TrySendData(69, -1, this.whoAmI, null, chest, (float)chest2.x, (float)chest2.y, 0f, 0, 0, 0);
					}
					Main.player[this.whoAmI].chest = num87;
					Recipe.FindRecipes(true);
					NetMessage.TrySendData(80, -1, this.whoAmI, null, this.whoAmI, (float)num87, 0f, 0f, 0, 0, 0);
					return;
				}
				Player player8 = Main.player[Main.myPlayer];
				if (player8.chest == -1)
				{
					Main.playerInventory = true;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				}
				else if (player8.chest != num87 && num87 != -1)
				{
					Main.playerInventory = true;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					Main.recBigList = false;
				}
				else if (player8.chest != -1 && num87 == -1)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					Main.recBigList = false;
				}
				player8.chest = num87;
				player8.chestX = num88;
				player8.chestY = num89;
				Recipe.FindRecipes(true);
				if (Main.tile[num88, num89].frameX >= 36 && Main.tile[num88, num89].frameX < 72)
				{
					AchievementsHelper.HandleSpecialEvent(Main.player[Main.myPlayer], 16);
					return;
				}
				return;
			}
			case 34:
			{
				byte b9 = this.reader.ReadByte();
				int num91 = (int)this.reader.ReadInt16();
				int num92 = (int)this.reader.ReadInt16();
				int num93 = (int)this.reader.ReadInt16();
				int num94 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					num94 = 0;
				}
				if (Main.netMode == 2)
				{
					if (b9 == 0)
					{
						int num95 = WorldGen.PlaceChest(num91, num92, 21, false, num93);
						if (num95 == -1)
						{
							NetMessage.TrySendData(34, this.whoAmI, -1, null, (int)b9, (float)num91, (float)num92, (float)num93, num95, 0, 0);
							Item.NewItem(new EntitySource_TileBreak(num91, num92), num91 * 16, num92 * 16, 32, 32, Chest.chestItemSpawn[num93], 1, true, 0, false, false);
							return;
						}
						NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num91, (float)num92, (float)num93, num95, 0, 0);
						return;
					}
					else if (b9 == 1 && Main.tile[num91, num92].type == 21)
					{
						Tile tile2 = Main.tile[num91, num92];
						if (tile2.frameX % 36 != 0)
						{
							num91--;
						}
						if (tile2.frameY % 36 != 0)
						{
							num92--;
						}
						int number = Chest.FindChest(num91, num92);
						WorldGen.KillTile(num91, num92, false, false, false);
						if (!tile2.active())
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num91, (float)num92, 0f, number, 0, 0);
							return;
						}
						return;
					}
					else if (b9 == 2)
					{
						int num96 = WorldGen.PlaceChest(num91, num92, 88, false, num93);
						if (num96 == -1)
						{
							NetMessage.TrySendData(34, this.whoAmI, -1, null, (int)b9, (float)num91, (float)num92, (float)num93, num96, 0, 0);
							Item.NewItem(new EntitySource_TileBreak(num91, num92), num91 * 16, num92 * 16, 32, 32, Chest.dresserItemSpawn[num93], 1, true, 0, false, false);
							return;
						}
						NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num91, (float)num92, (float)num93, num96, 0, 0);
						return;
					}
					else if (b9 == 3 && Main.tile[num91, num92].type == 88)
					{
						Tile tile3 = Main.tile[num91, num92];
						num91 -= (int)(tile3.frameX % 54 / 18);
						if (tile3.frameY % 36 != 0)
						{
							num92--;
						}
						int number2 = Chest.FindChest(num91, num92);
						WorldGen.KillTile(num91, num92, false, false, false);
						if (!tile3.active())
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num91, (float)num92, 0f, number2, 0, 0);
							return;
						}
						return;
					}
					else if (b9 == 4)
					{
						int num97 = WorldGen.PlaceChest(num91, num92, 467, false, num93);
						if (num97 == -1)
						{
							NetMessage.TrySendData(34, this.whoAmI, -1, null, (int)b9, (float)num91, (float)num92, (float)num93, num97, 0, 0);
							Item.NewItem(new EntitySource_TileBreak(num91, num92), num91 * 16, num92 * 16, 32, 32, Chest.chestItemSpawn2[num93], 1, true, 0, false, false);
							return;
						}
						NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num91, (float)num92, (float)num93, num97, 0, 0);
						return;
					}
					else
					{
						if (b9 != 5 || Main.tile[num91, num92].type != 467)
						{
							return;
						}
						Tile tile4 = Main.tile[num91, num92];
						if (tile4.frameX % 36 != 0)
						{
							num91--;
						}
						if (tile4.frameY % 36 != 0)
						{
							num92--;
						}
						int number3 = Chest.FindChest(num91, num92);
						WorldGen.KillTile(num91, num92, false, false, false);
						if (!tile4.active())
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num91, (float)num92, 0f, number3, 0, 0);
							return;
						}
						return;
					}
				}
				else if (b9 == 0)
				{
					if (num94 == -1)
					{
						WorldGen.KillTile(num91, num92, false, false, false);
						return;
					}
					SoundEngine.PlaySound(0, num91 * 16, num92 * 16, 1, 1f, 0f);
					WorldGen.PlaceChestDirect(num91, num92, 21, num93, num94);
					return;
				}
				else if (b9 == 2)
				{
					if (num94 == -1)
					{
						WorldGen.KillTile(num91, num92, false, false, false);
						return;
					}
					SoundEngine.PlaySound(0, num91 * 16, num92 * 16, 1, 1f, 0f);
					WorldGen.PlaceDresserDirect(num91, num92, 88, num93, num94);
					return;
				}
				else
				{
					if (b9 != 4)
					{
						Chest.DestroyChestDirect(num91, num92, num94);
						WorldGen.KillTile(num91, num92, false, false, false);
						return;
					}
					if (num94 == -1)
					{
						WorldGen.KillTile(num91, num92, false, false, false);
						return;
					}
					SoundEngine.PlaySound(0, num91 * 16, num92 * 16, 1, 1f, 0f);
					WorldGen.PlaceChestDirect(num91, num92, 467, num93, num94);
					return;
				}
				break;
			}
			case 35:
			{
				int num98 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num98 = this.whoAmI;
				}
				int num99 = (int)this.reader.ReadInt16();
				if (num98 != Main.myPlayer || Main.ServerSideCharacter)
				{
					Main.player[num98].HealEffect(num99, true);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(35, -1, this.whoAmI, null, num98, (float)num99, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 36:
			{
				int num100 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num100 = this.whoAmI;
				}
				Player player9 = Main.player[num100];
				bool flag13 = player9.zone5[0];
				player9.zone1 = this.reader.ReadByte();
				player9.zone2 = this.reader.ReadByte();
				player9.zone3 = this.reader.ReadByte();
				player9.zone4 = this.reader.ReadByte();
				player9.zone5 = this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					if (!flag13 && player9.zone5[0])
					{
						NPC.SpawnFaelings(num100);
					}
					NetMessage.TrySendData(36, -1, this.whoAmI, null, num100, 0f, 0f, 0f, 0, 0, 0);
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
				if (this.reader.ReadString() == Netplay.ServerPassword)
				{
					Netplay.Clients[this.whoAmI].State = 1;
					NetMessage.TrySendData(3, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[1].ToNetworkText(), 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			case 39:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num101 = (int)this.reader.ReadInt16();
				Main.item[num101].playerIndexTheItemIsReservedFor = 255;
				NetMessage.TrySendData(22, -1, -1, null, num101, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			case 40:
			{
				int num102 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num102 = this.whoAmI;
				}
				int npcIndex = (int)this.reader.ReadInt16();
				Main.player[num102].SetTalkNPC(npcIndex, true);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(40, -1, this.whoAmI, null, num102, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 41:
			{
				int num103 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num103 = this.whoAmI;
				}
				Player player10 = Main.player[num103];
				float itemRotation = this.reader.ReadSingle();
				int itemAnimation = (int)this.reader.ReadInt16();
				player10.itemRotation = itemRotation;
				player10.itemAnimation = itemAnimation;
				player10.channel = player10.inventory[player10.selectedItem].channel;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(41, -1, this.whoAmI, null, num103, 0f, 0f, 0f, 0, 0, 0);
				}
				if (Main.netMode != 1)
				{
					return;
				}
				Item item4 = player10.inventory[player10.selectedItem];
				if (item4.UseSound != null)
				{
					SoundEngine.PlaySound(item4.UseSound, player10.Center);
					return;
				}
				return;
			}
			case 42:
			{
				int num104 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num104 = this.whoAmI;
				}
				else if (Main.myPlayer == num104 && !Main.ServerSideCharacter)
				{
					return;
				}
				int statMana = (int)this.reader.ReadInt16();
				int statManaMax = (int)this.reader.ReadInt16();
				Main.player[num104].statMana = statMana;
				Main.player[num104].statManaMax = statManaMax;
				return;
			}
			case 43:
			{
				int num105 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num105 = this.whoAmI;
				}
				int num106 = (int)this.reader.ReadInt16();
				if (num105 != Main.myPlayer)
				{
					Main.player[num105].ManaEffect(num106);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(43, -1, this.whoAmI, null, num105, (float)num106, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 45:
			{
				int num107 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num107 = this.whoAmI;
				}
				int num108 = (int)this.reader.ReadByte();
				Player player11 = Main.player[num107];
				int team = player11.team;
				player11.team = num108;
				Color color2 = Main.teamColor[num108];
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(45, -1, this.whoAmI, null, num107, 0f, 0f, 0f, 0, 0, 0);
					LocalizedText localizedText2 = Lang.mp[13 + num108];
					if (num108 == 5)
					{
						localizedText2 = Lang.mp[22];
					}
					for (int num109 = 0; num109 < 255; num109++)
					{
						if (num109 == this.whoAmI || (team > 0 && Main.player[num109].team == team) || (num108 > 0 && Main.player[num109].team == num108))
						{
							ChatHelper.SendChatMessageToClient(NetworkText.FromKey(localizedText2.Key, new object[]
							{
								player11.name
							}), color2, num109);
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
				int num110 = Sign.ReadSign(i2, j2, true);
				if (num110 >= 0)
				{
					NetMessage.TrySendData(47, this.whoAmI, -1, null, num110, (float)this.whoAmI, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 47:
			{
				int num111 = (int)this.reader.ReadInt16();
				int x = (int)this.reader.ReadInt16();
				int y = (int)this.reader.ReadInt16();
				string text = this.reader.ReadString();
				int num112 = (int)this.reader.ReadByte();
				BitsByte bitsByte27 = this.reader.ReadByte();
				if (num111 < 0 || num111 >= 1000)
				{
					return;
				}
				string a = null;
				if (Main.sign[num111] != null)
				{
					a = Main.sign[num111].text;
				}
				Main.sign[num111] = new Sign();
				Main.sign[num111].x = x;
				Main.sign[num111].y = y;
				Sign.TextSign(num111, text);
				if (Main.netMode == 2 && a != text)
				{
					num112 = this.whoAmI;
					NetMessage.TrySendData(47, -1, this.whoAmI, null, num111, (float)num112, 0f, 0f, 0, 0, 0);
				}
				if (Main.netMode == 1 && num112 == Main.myPlayer && Main.sign[num111] != null && !bitsByte27[0])
				{
					Main.playerInventory = false;
					Main.player[Main.myPlayer].SetTalkNPC(-1, true);
					Main.npcChatCornerItem = 0;
					Main.editSign = false;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
					Main.player[Main.myPlayer].sign = num111;
					Main.npcChatText = Main.sign[num111].text;
					return;
				}
				return;
			}
			case 48:
			{
				int num113 = (int)this.reader.ReadInt16();
				int num114 = (int)this.reader.ReadInt16();
				byte b10 = this.reader.ReadByte();
				byte liquidType = this.reader.ReadByte();
				if (Main.netMode == 2 && Netplay.SpamCheck)
				{
					int num115 = this.whoAmI;
					int num116 = (int)(Main.player[num115].position.X + (float)(Main.player[num115].width / 2));
					int num117 = (int)(Main.player[num115].position.Y + (float)(Main.player[num115].height / 2));
					int num118 = 10;
					int num119 = num116 - num118;
					int num120 = num116 + num118;
					int num121 = num117 - num118;
					int num122 = num117 + num118;
					if (num113 < num119 || num113 > num120 || num114 < num121 || num114 > num122)
					{
						Netplay.Clients[this.whoAmI].SpamWater += 1f;
					}
				}
				if (Main.tile[num113, num114] == null)
				{
					Main.tile[num113, num114] = new Tile();
				}
				Tile obj2 = Main.tile[num113, num114];
				lock (obj2)
				{
					Main.tile[num113, num114].liquid = b10;
					Main.tile[num113, num114].liquidType((int)liquidType);
					if (Main.netMode == 2)
					{
						WorldGen.SquareTileFrame(num113, num114, true);
						if (b10 == 0)
						{
							NetMessage.SendData(48, -1, this.whoAmI, null, num113, (float)num114, 0f, 0f, 0, 0, 0);
						}
					}
					return;
				}
				goto IL_525A;
			}
			case 49:
				goto IL_525A;
			case 50:
			{
				int num123 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num123 = this.whoAmI;
				}
				else if (num123 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				Player player12 = Main.player[num123];
				for (int num124 = 0; num124 < Player.maxBuffs; num124++)
				{
					player12.buffType[num124] = (int)this.reader.ReadUInt16();
					if (player12.buffType[num124] > 0)
					{
						player12.buffTime[num124] = 60;
					}
					else
					{
						player12.buffTime[num124] = 0;
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(50, -1, this.whoAmI, null, num123, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 51:
			{
				byte b11 = this.reader.ReadByte();
				byte b12 = this.reader.ReadByte();
				if (b12 == 1)
				{
					NPC.SpawnSkeletron((int)b11);
					return;
				}
				if (b12 == 2)
				{
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(51, -1, this.whoAmI, null, (int)b11, (float)b12, 0f, 0f, 0, 0, 0);
						return;
					}
					SoundEngine.PlaySound(SoundID.Item1, (int)Main.player[(int)b11].position.X, (int)Main.player[(int)b11].position.Y);
					return;
				}
				else if (b12 == 3)
				{
					if (Main.netMode == 2)
					{
						Main.Sundialing();
						return;
					}
					return;
				}
				else
				{
					if (b12 == 4)
					{
						Main.npc[(int)b11].BigMimicSpawnSmoke();
						return;
					}
					if (b12 == 5)
					{
						if (Main.netMode == 2)
						{
							NPC npc3 = new NPC();
							npc3.SetDefaults(664, default(NPCSpawnParams));
							Main.BestiaryTracker.Kills.RegisterKill(npc3);
							return;
						}
						return;
					}
					else
					{
						if (b12 == 6 && Main.netMode == 2)
						{
							Main.Moondialing();
							return;
						}
						return;
					}
				}
				break;
			}
			case 52:
			{
				int num125 = (int)this.reader.ReadByte();
				int num126 = (int)this.reader.ReadInt16();
				int num127 = (int)this.reader.ReadInt16();
				if (num125 == 1)
				{
					Chest.Unlock(num126, num127);
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(52, -1, this.whoAmI, null, 0, (float)num125, (float)num126, (float)num127, 0, 0, 0);
						NetMessage.SendTileSquare(-1, num126, num127, 2, TileChangeType.None);
					}
				}
				if (num125 == 2)
				{
					WorldGen.UnlockDoor(num126, num127);
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(52, -1, this.whoAmI, null, 0, (float)num125, (float)num126, (float)num127, 0, 0, 0);
						NetMessage.SendTileSquare(-1, num126, num127, 2, TileChangeType.None);
					}
				}
				if (num125 != 3)
				{
					return;
				}
				Chest.Lock(num126, num127);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(52, -1, this.whoAmI, null, 0, (float)num125, (float)num126, (float)num127, 0, 0, 0);
					NetMessage.SendTileSquare(-1, num126, num127, 2, TileChangeType.None);
					return;
				}
				return;
			}
			case 53:
			{
				int num128 = (int)this.reader.ReadInt16();
				int type7 = (int)this.reader.ReadUInt16();
				int time = (int)this.reader.ReadInt16();
				Main.npc[num128].AddBuff(type7, time, true);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(54, -1, -1, null, num128, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 54:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num129 = (int)this.reader.ReadInt16();
				NPC npc4 = Main.npc[num129];
				for (int num130 = 0; num130 < NPC.maxBuffs; num130++)
				{
					npc4.buffType[num130] = (int)this.reader.ReadUInt16();
					npc4.buffTime[num130] = (int)this.reader.ReadInt16();
				}
				return;
			}
			case 55:
			{
				int num131 = (int)this.reader.ReadByte();
				int num132 = (int)this.reader.ReadUInt16();
				int num133 = this.reader.ReadInt32();
				if (Main.netMode == 2 && num131 != this.whoAmI && !Main.pvpBuff[num132])
				{
					return;
				}
				if (Main.netMode == 1 && num131 == Main.myPlayer)
				{
					Main.player[num131].AddBuff(num132, num133, true, false);
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(55, -1, -1, null, num131, (float)num132, (float)num133, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 56:
			{
				int num134 = (int)this.reader.ReadInt16();
				if (num134 < 0 || num134 >= 200)
				{
					return;
				}
				if (Main.netMode == 1)
				{
					string givenName = this.reader.ReadString();
					Main.npc[num134].GivenName = givenName;
					int townNpcVariationIndex = this.reader.ReadInt32();
					Main.npc[num134].townNpcVariationIndex = townNpcVariationIndex;
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(56, this.whoAmI, -1, null, num134, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 57:
				if (Main.netMode != 1)
				{
					return;
				}
				WorldGen.tGood = this.reader.ReadByte();
				WorldGen.tEvil = this.reader.ReadByte();
				WorldGen.tBlood = this.reader.ReadByte();
				return;
			case 58:
			{
				int num135 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num135 = this.whoAmI;
				}
				float num136 = this.reader.ReadSingle();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(58, -1, this.whoAmI, null, this.whoAmI, num136, 0f, 0f, 0, 0, 0);
					return;
				}
				Player player13 = Main.player[num135];
				int type8 = player13.inventory[player13.selectedItem].type;
				if (type8 == 4372 || type8 == 4057 || type8 == 4715)
				{
					player13.PlayGuitarChord(num136);
					return;
				}
				if (type8 == 4673)
				{
					player13.PlayDrums(num136);
					return;
				}
				Main.musicPitch = num136;
				LegacySoundStyle type9 = SoundID.Item26;
				if (type8 == 507)
				{
					type9 = SoundID.Item35;
				}
				if (type8 == 1305)
				{
					type9 = SoundID.Item47;
				}
				SoundEngine.PlaySound(type9, player13.position);
				return;
			}
			case 59:
			{
				int num137 = (int)this.reader.ReadInt16();
				int num138 = (int)this.reader.ReadInt16();
				Wiring.SetCurrentUser(this.whoAmI);
				Wiring.HitSwitch(num137, num138);
				Wiring.SetCurrentUser(-1);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(59, -1, this.whoAmI, null, num137, (float)num138, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 60:
			{
				int num139 = (int)this.reader.ReadInt16();
				int num140 = (int)this.reader.ReadInt16();
				int num141 = (int)this.reader.ReadInt16();
				byte b13 = this.reader.ReadByte();
				if (num139 >= 200)
				{
					NetMessage.BootPlayer(this.whoAmI, NetworkText.FromKey("Net.CheatingInvalid", new object[0]));
					return;
				}
				NPC npc5 = Main.npc[num139];
				bool isLikeATownNPC = npc5.isLikeATownNPC;
				if (Main.netMode == 1)
				{
					npc5.homeless = (b13 == 1);
					npc5.homeTileX = num140;
					npc5.homeTileY = num141;
				}
				if (!isLikeATownNPC)
				{
					return;
				}
				if (Main.netMode == 1)
				{
					if (b13 == 1)
					{
						WorldGen.TownManager.KickOut(npc5.type);
						return;
					}
					if (b13 == 2)
					{
						WorldGen.TownManager.SetRoom(npc5.type, num140, num141);
						return;
					}
					return;
				}
				else
				{
					if (b13 == 1)
					{
						WorldGen.kickOut(num139);
						return;
					}
					WorldGen.moveRoom(num140, num141, num139);
					return;
				}
				break;
			}
			case 61:
			{
				int num142 = (int)this.reader.ReadInt16();
				int num143 = (int)this.reader.ReadInt16();
				if (Main.netMode != 2)
				{
					return;
				}
				if (num143 >= 0 && num143 < (int)NPCID.Count && NPCID.Sets.MPAllowedEnemies[num143])
				{
					if (!NPC.AnyNPCs(num143))
					{
						NPC.SpawnOnPlayer(num142, num143);
						return;
					}
					return;
				}
				else if (num143 == -4)
				{
					if (!Main.dayTime && !DD2Event.Ongoing)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[31].Key, new object[0]), new Color(50, 255, 130), -1);
						Main.startPumpkinMoon();
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(78, -1, -1, null, 0, 1f, 2f, 1f, 0, 0, 0);
						return;
					}
					return;
				}
				else if (num143 == -5)
				{
					if (!Main.dayTime && !DD2Event.Ongoing)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[34].Key, new object[0]), new Color(50, 255, 130), -1);
						Main.startSnowMoon();
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(78, -1, -1, null, 0, 1f, 1f, 1f, 0, 0, 0);
						return;
					}
					return;
				}
				else if (num143 == -6)
				{
					if (Main.dayTime && !Main.eclipse)
					{
						if (Main.remixWorld)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[106].Key, new object[0]), new Color(50, 255, 130), -1);
						}
						else
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[20].Key, new object[0]), new Color(50, 255, 130), -1);
						}
						Main.eclipse = true;
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
					return;
				}
				else
				{
					if (num143 == -7)
					{
						Main.invasionDelay = 0;
						Main.StartInvasion(4);
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(78, -1, -1, null, 0, 1f, (float)(Main.invasionType + 3), 0f, 0, 0, 0);
						return;
					}
					if (num143 == -8)
					{
						if (NPC.downedGolemBoss && Main.hardMode && !NPC.AnyDanger(false, false) && !NPC.AnyoneNearCultists())
						{
							WorldGen.StartImpendingDoom(720);
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						return;
					}
					else if (num143 == -10)
					{
						if (!Main.dayTime && !Main.bloodMoon)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[8].Key, new object[0]), new Color(50, 255, 130), -1);
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
						if (num143 == -11)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.CombatBookUsed", new object[0]), new Color(50, 255, 130), -1);
							NPC.combatBookWasUsed = true;
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						if (num143 == -12)
						{
							NPC.UnlockOrExchangePet(ref NPC.boughtCat, 637, "Misc.LicenseCatUsed", num143);
							return;
						}
						if (num143 == -13)
						{
							NPC.UnlockOrExchangePet(ref NPC.boughtDog, 638, "Misc.LicenseDogUsed", num143);
							return;
						}
						if (num143 == -14)
						{
							NPC.UnlockOrExchangePet(ref NPC.boughtBunny, 656, "Misc.LicenseBunnyUsed", num143);
							return;
						}
						if (num143 == -15)
						{
							NPC.UnlockOrExchangePet(ref NPC.unlockedSlimeBlueSpawn, 670, "Misc.LicenseSlimeUsed", num143);
							return;
						}
						if (num143 == -16)
						{
							NPC.SpawnMechQueen(num142);
							return;
						}
						if (num143 == -17)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.CombatBookVolumeTwoUsed", new object[0]), new Color(50, 255, 130), -1);
							NPC.combatBookVolumeTwoWasUsed = true;
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						if (num143 == -18)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.PeddlersSatchelUsed", new object[0]), new Color(50, 255, 130), -1);
							NPC.peddlersSatchelWasUsed = true;
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						if (num143 < 0)
						{
							int num144 = 1;
							if (num143 > (int)(-(int)InvasionID.Count))
							{
								num144 = -num143;
							}
							if (num144 > 0 && Main.invasionType == 0)
							{
								Main.invasionDelay = 0;
								Main.StartInvasion(num144);
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
				int num145 = (int)this.reader.ReadByte();
				int num146 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num145 = this.whoAmI;
				}
				if (num146 == 1)
				{
					Main.player[num145].NinjaDodge();
				}
				if (num146 == 2)
				{
					Main.player[num145].ShadowDodge();
				}
				if (num146 == 4)
				{
					Main.player[num145].BrainOfConfusionDodge();
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(62, -1, this.whoAmI, null, num145, (float)num146, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 63:
			{
				int num147 = (int)this.reader.ReadInt16();
				int num148 = (int)this.reader.ReadInt16();
				byte b14 = this.reader.ReadByte();
				byte b15 = this.reader.ReadByte();
				if (b15 == 0)
				{
					WorldGen.paintTile(num147, num148, b14, false);
				}
				else
				{
					WorldGen.paintCoatTile(num147, num148, b14, false);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(63, -1, this.whoAmI, null, num147, (float)num148, (float)b14, (float)b15, 0, 0, 0);
					return;
				}
				return;
			}
			case 64:
			{
				int num149 = (int)this.reader.ReadInt16();
				int num150 = (int)this.reader.ReadInt16();
				byte b16 = this.reader.ReadByte();
				byte b17 = this.reader.ReadByte();
				if (b17 == 0)
				{
					WorldGen.paintWall(num149, num150, b16, false);
				}
				else
				{
					WorldGen.paintCoatWall(num149, num150, b16, false);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(64, -1, this.whoAmI, null, num149, (float)num150, (float)b16, (float)b17, 0, 0, 0);
					return;
				}
				return;
			}
			case 65:
			{
				BitsByte bitsByte28 = this.reader.ReadByte();
				int num151 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					num151 = this.whoAmI;
				}
				Vector2 vector3 = this.reader.ReadVector2();
				int num152 = (int)this.reader.ReadByte();
				int num153 = 0;
				if (bitsByte28[0])
				{
					num153++;
				}
				if (bitsByte28[1])
				{
					num153 += 2;
				}
				bool flag14 = false;
				if (bitsByte28[2])
				{
					flag14 = true;
				}
				int num154 = 0;
				if (bitsByte28[3])
				{
					num154 = this.reader.ReadInt32();
				}
				if (flag14)
				{
					vector3 = Main.player[num151].position;
				}
				if (num153 == 0)
				{
					Main.player[num151].Teleport(vector3, num152, num154);
				}
				else if (num153 == 1)
				{
					Main.npc[num151].Teleport(vector3, num152, num154);
				}
				else if (num153 == 2)
				{
					Main.player[num151].Teleport(vector3, num152, num154);
					if (Main.netMode == 2)
					{
						RemoteClient.CheckSection(this.whoAmI, vector3, 1);
						NetMessage.TrySendData(65, -1, -1, null, 0, (float)num151, vector3.X, vector3.Y, num152, flag14.ToInt(), num154);
						int num155 = -1;
						float num156 = 9999f;
						for (int num157 = 0; num157 < 255; num157++)
						{
							if (Main.player[num157].active && num157 != this.whoAmI)
							{
								Vector2 vector4 = Main.player[num157].position - Main.player[this.whoAmI].position;
								if (vector4.Length() < num156)
								{
									num156 = vector4.Length();
									num155 = num157;
								}
							}
						}
						if (num155 >= 0)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Game.HasTeleportedTo", new object[]
							{
								Main.player[this.whoAmI].name,
								Main.player[num155].name
							}), new Color(250, 250, 0), -1);
						}
					}
				}
				if (Main.netMode == 2 && num153 == 0)
				{
					NetMessage.TrySendData(65, -1, this.whoAmI, null, num153, (float)num151, vector3.X, vector3.Y, num152, flag14.ToInt(), num154);
					return;
				}
				return;
			}
			case 66:
			{
				int num158 = (int)this.reader.ReadByte();
				int num159 = (int)this.reader.ReadInt16();
				if (num159 <= 0)
				{
					return;
				}
				Player player14 = Main.player[num158];
				player14.statLife += num159;
				if (player14.statLife > player14.statLifeMax2)
				{
					player14.statLife = player14.statLifeMax2;
				}
				player14.HealEffect(num159, false);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(66, -1, this.whoAmI, null, num158, (float)num159, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 68:
				this.reader.ReadString();
				return;
			case 69:
			{
				int num160 = (int)this.reader.ReadInt16();
				int num161 = (int)this.reader.ReadInt16();
				int num162 = (int)this.reader.ReadInt16();
				if (Main.netMode == 1)
				{
					if (num160 < 0 || num160 >= 8000)
					{
						return;
					}
					Chest chest3 = Main.chest[num160];
					if (chest3 == null)
					{
						chest3 = new Chest(false);
						chest3.x = num161;
						chest3.y = num162;
						Main.chest[num160] = chest3;
					}
					else if (chest3.x != num161 || chest3.y != num162)
					{
						return;
					}
					chest3.name = this.reader.ReadString();
					return;
				}
				else
				{
					if (num160 < -1 || num160 >= 8000)
					{
						return;
					}
					if (num160 == -1)
					{
						num160 = Chest.FindChest(num161, num162);
						if (num160 == -1)
						{
							return;
						}
					}
					Chest chest4 = Main.chest[num160];
					if (chest4.x != num161 || chest4.y != num162)
					{
						return;
					}
					NetMessage.TrySendData(69, this.whoAmI, -1, null, num160, (float)num161, (float)num162, 0f, 0, 0, 0);
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
				int num163 = (int)this.reader.ReadInt16();
				int who = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					who = this.whoAmI;
				}
				if (num163 < 200 && num163 >= 0)
				{
					NPC.CatchNPC(num163, who);
					return;
				}
				return;
			}
			case 71:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int x2 = this.reader.ReadInt32();
				int y2 = this.reader.ReadInt32();
				int type10 = (int)this.reader.ReadInt16();
				byte style = this.reader.ReadByte();
				NPC.ReleaseNPC(x2, y2, type10, (int)style, this.whoAmI);
				return;
			}
			case 72:
				if (Main.netMode != 1)
				{
					return;
				}
				for (int num164 = 0; num164 < 40; num164++)
				{
					Main.travelShop[num164] = (int)this.reader.ReadInt16();
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
				if (Main.netMode != 1)
				{
					return;
				}
				Main.anglerQuest = (int)this.reader.ReadByte();
				Main.anglerQuestFinished = this.reader.ReadBoolean();
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
				int num165 = (int)this.reader.ReadByte();
				if (num165 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num165 = this.whoAmI;
				}
				Player player15 = Main.player[num165];
				player15.anglerQuestsFinished = this.reader.ReadInt32();
				player15.golferScoreAccumulated = this.reader.ReadInt32();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(76, -1, this.whoAmI, null, num165, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 77:
			{
				int type11 = (int)this.reader.ReadInt16();
				ushort tileType = this.reader.ReadUInt16();
				short x3 = this.reader.ReadInt16();
				short y3 = this.reader.ReadInt16();
				Animation.NewTemporaryAnimation(type11, tileType, (int)x3, (int)y3);
				return;
			}
			case 78:
				if (Main.netMode != 1)
				{
					return;
				}
				Main.ReportInvasionProgress(this.reader.ReadInt32(), this.reader.ReadInt32(), (int)this.reader.ReadSByte(), (int)this.reader.ReadSByte());
				return;
			case 79:
			{
				int x4 = (int)this.reader.ReadInt16();
				int y4 = (int)this.reader.ReadInt16();
				short type12 = this.reader.ReadInt16();
				int style2 = (int)this.reader.ReadInt16();
				int num166 = (int)this.reader.ReadByte();
				int random = (int)this.reader.ReadSByte();
				int direction;
				if (this.reader.ReadBoolean())
				{
					direction = 1;
				}
				else
				{
					direction = -1;
				}
				if (Main.netMode == 2)
				{
					Netplay.Clients[this.whoAmI].SpamAddBlock += 1f;
					if (!WorldGen.InWorld(x4, y4, 10) || !Netplay.Clients[this.whoAmI].TileSections[Netplay.GetSectionX(x4), Netplay.GetSectionY(y4)])
					{
						return;
					}
				}
				WorldGen.PlaceObject(x4, y4, (int)type12, false, style2, num166, random, direction);
				if (Main.netMode == 2)
				{
					NetMessage.SendObjectPlacement(this.whoAmI, x4, y4, (int)type12, style2, num166, random, direction);
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
				int num167 = (int)this.reader.ReadByte();
				int num168 = (int)this.reader.ReadInt16();
				if (num168 >= -3 && num168 < 8000)
				{
					Main.player[num167].chest = num168;
					Recipe.FindRecipes(true);
					return;
				}
				return;
			}
			case 81:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int x5 = (int)this.reader.ReadSingle();
				int y5 = (int)this.reader.ReadSingle();
				Color color3 = this.reader.ReadRGB();
				int amount = this.reader.ReadInt32();
				CombatText.NewText(new Rectangle(x5, y5, 0, 0), color3, amount, false, false);
				return;
			}
			case 82:
				NetManager.Instance.Read(this.reader, this.whoAmI, length);
				return;
			case 83:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num169 = (int)this.reader.ReadInt16();
				int num170 = this.reader.ReadInt32();
				if (num169 >= 0 && num169 < 290)
				{
					NPC.killCount[num169] = num170;
					return;
				}
				return;
			}
			case 84:
			{
				int num171 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num171 = this.whoAmI;
				}
				float stealth = this.reader.ReadSingle();
				Main.player[num171].stealth = stealth;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(84, -1, this.whoAmI, null, num171, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 85:
			{
				int num172 = this.whoAmI;
				int slot = (int)this.reader.ReadInt16();
				if (Main.netMode == 2 && num172 < 255)
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
				int num173 = this.reader.ReadInt32();
				if (this.reader.ReadBoolean())
				{
					TileEntity tileEntity = TileEntity.Read(this.reader, true);
					tileEntity.ID = num173;
					TileEntity.ByID[tileEntity.ID] = tileEntity;
					TileEntity.ByPosition[tileEntity.Position] = tileEntity;
					return;
				}
				TileEntity tileEntity2;
				if (TileEntity.ByID.TryGetValue(num173, out tileEntity2))
				{
					TileEntity.ByID.Remove(num173);
					TileEntity.ByPosition.Remove(tileEntity2.Position);
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
				int x6 = (int)this.reader.ReadInt16();
				int y6 = (int)this.reader.ReadInt16();
				int type13 = (int)this.reader.ReadByte();
				if (!WorldGen.InWorld(x6, y6, 0))
				{
					return;
				}
				if (TileEntity.ByPosition.ContainsKey(new Point16(x6, y6)))
				{
					return;
				}
				TileEntity.PlaceEntityNet(x6, y6, type13);
				return;
			}
			case 88:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num174 = (int)this.reader.ReadInt16();
				if (num174 < 0 || num174 > 400)
				{
					return;
				}
				Item item5 = Main.item[num174];
				BitsByte bitsByte29 = this.reader.ReadByte();
				if (bitsByte29[0])
				{
					item5.color.PackedValue = this.reader.ReadUInt32();
				}
				if (bitsByte29[1])
				{
					item5.damage = (int)this.reader.ReadUInt16();
				}
				if (bitsByte29[2])
				{
					item5.knockBack = this.reader.ReadSingle();
				}
				if (bitsByte29[3])
				{
					item5.useAnimation = (int)this.reader.ReadUInt16();
				}
				if (bitsByte29[4])
				{
					item5.useTime = (int)this.reader.ReadUInt16();
				}
				if (bitsByte29[5])
				{
					item5.shoot = (int)this.reader.ReadInt16();
				}
				if (bitsByte29[6])
				{
					item5.shootSpeed = this.reader.ReadSingle();
				}
				if (!bitsByte29[7])
				{
					return;
				}
				bitsByte29 = this.reader.ReadByte();
				if (bitsByte29[0])
				{
					item5.width = (int)this.reader.ReadInt16();
				}
				if (bitsByte29[1])
				{
					item5.height = (int)this.reader.ReadInt16();
				}
				if (bitsByte29[2])
				{
					item5.scale = this.reader.ReadSingle();
				}
				if (bitsByte29[3])
				{
					item5.ammo = (int)this.reader.ReadInt16();
				}
				if (bitsByte29[4])
				{
					item5.useAmmo = (int)this.reader.ReadInt16();
				}
				if (bitsByte29[5])
				{
					item5.notAmmo = this.reader.ReadBoolean();
					return;
				}
				return;
			}
			case 89:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int x7 = (int)this.reader.ReadInt16();
				int y7 = (int)this.reader.ReadInt16();
				int netid = (int)this.reader.ReadInt16();
				int prefix = (int)this.reader.ReadByte();
				int stack5 = (int)this.reader.ReadInt16();
				TEItemFrame.TryPlacing(x7, y7, netid, prefix, stack5);
				return;
			}
			case 91:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num175 = this.reader.ReadInt32();
				int num176 = (int)this.reader.ReadByte();
				if (num176 != 255)
				{
					int num177 = (int)this.reader.ReadUInt16();
					int num178 = (int)this.reader.ReadUInt16();
					int num179 = (int)this.reader.ReadByte();
					int metadata = 0;
					if (num179 < 0)
					{
						metadata = (int)this.reader.ReadInt16();
					}
					WorldUIAnchor worldUIAnchor = EmoteBubble.DeserializeNetAnchor(num176, num177);
					if (num176 == 1)
					{
						Main.player[num177].emoteTime = 360;
					}
					Dictionary<int, EmoteBubble> byID = EmoteBubble.byID;
					lock (byID)
					{
						if (!EmoteBubble.byID.ContainsKey(num175))
						{
							EmoteBubble.byID[num175] = new EmoteBubble(num179, worldUIAnchor, num178);
						}
						else
						{
							EmoteBubble.byID[num175].lifeTime = num178;
							EmoteBubble.byID[num175].lifeTimeStart = num178;
							EmoteBubble.byID[num175].emote = num179;
							EmoteBubble.byID[num175].anchor = worldUIAnchor;
						}
						EmoteBubble.byID[num175].ID = num175;
						EmoteBubble.byID[num175].metadata = metadata;
						EmoteBubble.OnBubbleChange(num175);
						return;
					}
					goto IL_76C2;
				}
				if (EmoteBubble.byID.ContainsKey(num175))
				{
					EmoteBubble.byID.Remove(num175);
					return;
				}
				return;
			}
			case 92:
				goto IL_76C2;
			case 94:
			case 138:
				goto IL_97FA;
			case 95:
			{
				ushort num180 = this.reader.ReadUInt16();
				int num181 = (int)this.reader.ReadByte();
				if (Main.netMode != 2)
				{
					return;
				}
				for (int num182 = 0; num182 < 1000; num182++)
				{
					if (Main.projectile[num182].owner == (int)num180 && Main.projectile[num182].active && Main.projectile[num182].type == 602 && Main.projectile[num182].ai[1] == (float)num181)
					{
						Main.projectile[num182].Kill();
						NetMessage.TrySendData(29, -1, -1, null, Main.projectile[num182].identity, (float)num180, 0f, 0f, 0, 0, 0);
						return;
					}
				}
				return;
			}
			case 96:
			{
				int num183 = (int)this.reader.ReadByte();
				Player player16 = Main.player[num183];
				int num184 = (int)this.reader.ReadInt16();
				Vector2 vector5 = this.reader.ReadVector2();
				Vector2 velocity4 = this.reader.ReadVector2();
				int lastPortalColorIndex = num184 + ((num184 % 2 == 0) ? 1 : -1);
				player16.lastPortalColorIndex = lastPortalColorIndex;
				player16.Teleport(vector5, 4, num184);
				player16.velocity = velocity4;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(96, -1, -1, null, num183, vector5.X, vector5.Y, (float)num184, 0, 0, 0);
					return;
				}
				return;
			}
			case 97:
				if (Main.netMode != 1)
				{
					return;
				}
				AchievementsHelper.NotifyNPCKilledDirect(Main.player[Main.myPlayer], (int)this.reader.ReadInt16());
				return;
			case 98:
				if (Main.netMode != 1)
				{
					return;
				}
				AchievementsHelper.NotifyProgressionEvent((int)this.reader.ReadInt16());
				return;
			case 99:
			{
				int num185 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num185 = this.whoAmI;
				}
				Main.player[num185].MinionRestTargetPoint = this.reader.ReadVector2();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(99, -1, this.whoAmI, null, num185, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 100:
			{
				int num186 = (int)this.reader.ReadUInt16();
				NPC npc6 = Main.npc[num186];
				int num187 = (int)this.reader.ReadInt16();
				Vector2 newPos = this.reader.ReadVector2();
				Vector2 velocity5 = this.reader.ReadVector2();
				int lastPortalColorIndex2 = num187 + ((num187 % 2 == 0) ? 1 : -1);
				npc6.lastPortalColorIndex = lastPortalColorIndex2;
				npc6.Teleport(newPos, 4, num187);
				npc6.velocity = velocity5;
				npc6.netOffset *= 0f;
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
				int num188 = (int)this.reader.ReadByte();
				ushort num189 = this.reader.ReadUInt16();
				Vector2 vector6 = this.reader.ReadVector2();
				if (Main.netMode == 2)
				{
					num188 = this.whoAmI;
					NetMessage.TrySendData(102, -1, -1, null, num188, (float)num189, vector6.X, vector6.Y, 0, 0, 0);
					return;
				}
				Player player17 = Main.player[num188];
				for (int num190 = 0; num190 < 255; num190++)
				{
					Player player18 = Main.player[num190];
					if (player18.active && !player18.dead && (player17.team == 0 || player17.team == player18.team) && player18.Distance(vector6) < 700f)
					{
						Vector2 value3 = player17.Center - player18.Center;
						Vector2 vector7 = Vector2.Normalize(value3);
						if (!vector7.HasNaNs())
						{
							int type14 = 90;
							float num191 = 0f;
							float num192 = 0.20943952f;
							Vector2 spinningpoint = new Vector2(0f, -8f);
							Vector2 value4 = new Vector2(-3f);
							float num193 = 0f;
							float num194 = 0.005f;
							if (num189 != 173)
							{
								if (num189 != 176)
								{
									if (num189 == 179)
									{
										type14 = 86;
									}
								}
								else
								{
									type14 = 88;
								}
							}
							else
							{
								type14 = 90;
							}
							int num195 = 0;
							while ((float)num195 < value3.Length() / 6f)
							{
								Vector2 position2 = player18.Center + 6f * (float)num195 * vector7 + spinningpoint.RotatedBy((double)num191, default(Vector2)) + value4;
								num191 += num192;
								int num196 = Dust.NewDust(position2, 6, 6, type14, 0f, 0f, 100, default(Color), 1.5f);
								Main.dust[num196].noGravity = true;
								Main.dust[num196].velocity = Vector2.Zero;
								num193 = (Main.dust[num196].fadeIn = num193 + num194);
								Main.dust[num196].velocity += vector7 * 1.5f;
								num195++;
							}
						}
						player18.NebulaLevelup((int)num189);
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
				Item[] item6 = Main.instance.shop[Main.npcShop].item;
				int num197 = (int)this.reader.ReadByte();
				int type15 = (int)this.reader.ReadInt16();
				int stack6 = (int)this.reader.ReadInt16();
				int prefixWeWant3 = (int)this.reader.ReadByte();
				int value5 = this.reader.ReadInt32();
				BitsByte bitsByte30 = this.reader.ReadByte();
				if (num197 < item6.Length)
				{
					item6[num197] = new Item();
					item6[num197].netDefaults(type15);
					item6[num197].stack = stack6;
					item6[num197].Prefix(prefixWeWant3);
					item6[num197].value = value5;
					item6[num197].buyOnce = bitsByte30[0];
					return;
				}
				return;
			}
			case 105:
			{
				if (Main.netMode == 1)
				{
					return;
				}
				int i3 = (int)this.reader.ReadInt16();
				int j3 = (int)this.reader.ReadInt16();
				bool on = this.reader.ReadBoolean();
				WorldGen.ToggleGemLock(i3, j3, on);
				return;
			}
			case 106:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				HalfVector2 halfVector = default(HalfVector2);
				halfVector.PackedValue = this.reader.ReadUInt32();
				Utils.PoofOfSmoke(halfVector.ToVector2());
				return;
			}
			case 107:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				Color c = this.reader.ReadRGB();
				string text2 = NetworkText.Deserialize(this.reader).ToString();
				int widthLimit = (int)this.reader.ReadInt16();
				Main.NewTextMultiline(text2, false, c, widthLimit);
				return;
			}
			case 108:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int damage2 = (int)this.reader.ReadInt16();
				float knockBack2 = this.reader.ReadSingle();
				int x8 = (int)this.reader.ReadInt16();
				int y8 = (int)this.reader.ReadInt16();
				int angle = (int)this.reader.ReadInt16();
				int ammo = (int)this.reader.ReadInt16();
				int num198 = (int)this.reader.ReadByte();
				if (num198 != Main.myPlayer)
				{
					return;
				}
				WorldGen.ShootFromCannon(x8, y8, angle, ammo, damage2, knockBack2, num198, true);
				return;
			}
			case 109:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int x9 = (int)this.reader.ReadInt16();
				int y9 = (int)this.reader.ReadInt16();
				int x10 = (int)this.reader.ReadInt16();
				int y10 = (int)this.reader.ReadInt16();
				WiresUI.Settings.MultiToolMode toolMode = (WiresUI.Settings.MultiToolMode)this.reader.ReadByte();
				int num199 = this.whoAmI;
				WiresUI.Settings.MultiToolMode toolMode2 = WiresUI.Settings.ToolMode;
				WiresUI.Settings.ToolMode = toolMode;
				Wiring.MassWireOperation(new Point(x9, y9), new Point(x10, y10), Main.player[num199]);
				WiresUI.Settings.ToolMode = toolMode2;
				return;
			}
			case 110:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int type16 = (int)this.reader.ReadInt16();
				int num200 = (int)this.reader.ReadInt16();
				int num201 = (int)this.reader.ReadByte();
				if (num201 != Main.myPlayer)
				{
					return;
				}
				Player player19 = Main.player[num201];
				for (int num202 = 0; num202 < num200; num202++)
				{
					player19.ConsumeItem(type16, false, false);
				}
				player19.wireOperationsCooldown = 0;
				return;
			}
			case 111:
				if (Main.netMode != 2)
				{
					return;
				}
				BirthdayParty.ToggleManualParty();
				return;
			case 112:
			{
				int num203 = (int)this.reader.ReadByte();
				int num204 = this.reader.ReadInt32();
				int num205 = this.reader.ReadInt32();
				int num206 = (int)this.reader.ReadByte();
				int num207 = (int)this.reader.ReadInt16();
				if (num203 == 1)
				{
					if (Main.netMode == 1)
					{
						WorldGen.TreeGrowFX(num204, num205, num206, num207, false);
					}
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData((int)b, -1, -1, null, num203, (float)num204, (float)num205, (float)num206, num207, 0, 0);
						return;
					}
					return;
				}
				else
				{
					if (num203 == 2)
					{
						NPC.FairyEffects(new Vector2((float)num204, (float)num205), num207);
						return;
					}
					return;
				}
				break;
			}
			case 113:
			{
				int x11 = (int)this.reader.ReadInt16();
				int y11 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2 && !Main.snowMoon && !Main.pumpkinMoon)
				{
					if (DD2Event.WouldFailSpawningHere(x11, y11))
					{
						DD2Event.FailureMessage(this.whoAmI);
					}
					DD2Event.SummonCrystal(x11, y11, this.whoAmI);
					return;
				}
				return;
			}
			case 114:
				if (Main.netMode != 1)
				{
					return;
				}
				DD2Event.WipeEntities();
				return;
			case 115:
			{
				int num208 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num208 = this.whoAmI;
				}
				Main.player[num208].MinionAttackTargetNPC = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(115, -1, this.whoAmI, null, num208, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 116:
				if (Main.netMode != 1)
				{
					return;
				}
				DD2Event.TimeLeftBetweenWaves = this.reader.ReadInt32();
				return;
			case 117:
			{
				int num209 = (int)this.reader.ReadByte();
				if (Main.netMode == 2 && this.whoAmI != num209 && (!Main.player[num209].hostile || !Main.player[this.whoAmI].hostile))
				{
					return;
				}
				PlayerDeathReason playerDeathReason = PlayerDeathReason.FromReader(this.reader);
				int damage3 = (int)this.reader.ReadInt16();
				int num210 = (int)(this.reader.ReadByte() - 1);
				BitsByte bitsByte31 = this.reader.ReadByte();
				bool flag15 = bitsByte31[0];
				bool pvp = bitsByte31[1];
				int num211 = (int)this.reader.ReadSByte();
				Main.player[num209].Hurt(playerDeathReason, damage3, num210, pvp, true, flag15, num211, true);
				if (Main.netMode == 2)
				{
					NetMessage.SendPlayerHurt(num209, playerDeathReason, damage3, num210, flag15, pvp, num211, -1, this.whoAmI);
					return;
				}
				return;
			}
			case 118:
			{
				int num212 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num212 = this.whoAmI;
				}
				PlayerDeathReason playerDeathReason2 = PlayerDeathReason.FromReader(this.reader);
				int num213 = (int)this.reader.ReadInt16();
				int num214 = (int)(this.reader.ReadByte() - 1);
				bool pvp2 = this.reader.ReadByte()[0];
				Main.player[num212].KillMe(playerDeathReason2, (double)num213, num214, pvp2);
				if (Main.netMode == 2)
				{
					NetMessage.SendPlayerDeath(num212, playerDeathReason2, num213, num214, pvp2, -1, this.whoAmI);
					return;
				}
				return;
			}
			case 119:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int x12 = (int)this.reader.ReadSingle();
				int y12 = (int)this.reader.ReadSingle();
				Color color4 = this.reader.ReadRGB();
				NetworkText networkText = NetworkText.Deserialize(this.reader);
				CombatText.NewText(new Rectangle(x12, y12, 0, 0), color4, networkText.ToString(), false, false);
				return;
			}
			case 120:
			{
				int num215 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num215 = this.whoAmI;
				}
				int num216 = (int)this.reader.ReadByte();
				if (num216 >= 0 && num216 < EmoteID.Count && Main.netMode == 2)
				{
					EmoteBubble.NewBubble(num216, new WorldUIAnchor(Main.player[num215]), 360);
					EmoteBubble.CheckForNPCsToReactToEmoteBubble(num216, Main.player[num215]);
					return;
				}
				return;
			}
			case 121:
			{
				int num217 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num217 = this.whoAmI;
				}
				int num218 = this.reader.ReadInt32();
				int num219 = (int)this.reader.ReadByte();
				bool flag16 = false;
				if (num219 >= 8)
				{
					flag16 = true;
					num219 -= 8;
				}
				TileEntity tileEntity3;
				if (!TileEntity.ByID.TryGetValue(num218, out tileEntity3))
				{
					this.reader.ReadInt32();
					this.reader.ReadByte();
					return;
				}
				if (num219 >= 8)
				{
					tileEntity3 = null;
				}
				TEDisplayDoll tedisplayDoll = tileEntity3 as TEDisplayDoll;
				if (tedisplayDoll != null)
				{
					tedisplayDoll.ReadItem(num219, this.reader, flag16);
				}
				else
				{
					this.reader.ReadInt32();
					this.reader.ReadByte();
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, num217, null, num217, (float)num218, (float)num219, (float)flag16.ToInt(), 0, 0, 0);
					return;
				}
				return;
			}
			case 122:
			{
				int num220 = this.reader.ReadInt32();
				int num221 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num221 = this.whoAmI;
				}
				if (Main.netMode == 2)
				{
					if (num220 == -1)
					{
						Main.player[num221].tileEntityAnchor.Clear();
						NetMessage.TrySendData((int)b, -1, -1, null, num220, (float)num221, 0f, 0f, 0, 0, 0);
						return;
					}
					int num222;
					TileEntity tileEntity4;
					if (!TileEntity.IsOccupied(num220, out num222) && TileEntity.ByID.TryGetValue(num220, out tileEntity4))
					{
						Main.player[num221].tileEntityAnchor.Set(num220, (int)tileEntity4.Position.X, (int)tileEntity4.Position.Y);
						NetMessage.TrySendData((int)b, -1, -1, null, num220, (float)num221, 0f, 0f, 0, 0, 0);
					}
				}
				if (Main.netMode != 1)
				{
					return;
				}
				if (num220 == -1)
				{
					Main.player[num221].tileEntityAnchor.Clear();
					return;
				}
				TileEntity tileEntity5;
				if (TileEntity.ByID.TryGetValue(num220, out tileEntity5))
				{
					TileEntity.SetInteractionAnchor(Main.player[num221], (int)tileEntity5.Position.X, (int)tileEntity5.Position.Y, num220);
					return;
				}
				return;
			}
			case 123:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int x13 = (int)this.reader.ReadInt16();
				int y13 = (int)this.reader.ReadInt16();
				int netid2 = (int)this.reader.ReadInt16();
				int prefix2 = (int)this.reader.ReadByte();
				int stack7 = (int)this.reader.ReadInt16();
				TEWeaponsRack.TryPlacing(x13, y13, netid2, prefix2, stack7);
				return;
			}
			case 124:
			{
				int num223 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num223 = this.whoAmI;
				}
				int num224 = this.reader.ReadInt32();
				int num225 = (int)this.reader.ReadByte();
				bool flag17 = false;
				if (num225 >= 2)
				{
					flag17 = true;
					num225 -= 2;
				}
				TileEntity tileEntity6;
				if (!TileEntity.ByID.TryGetValue(num224, out tileEntity6))
				{
					this.reader.ReadInt32();
					this.reader.ReadByte();
					return;
				}
				if (num225 >= 2)
				{
					tileEntity6 = null;
				}
				TEHatRack tehatRack = tileEntity6 as TEHatRack;
				if (tehatRack != null)
				{
					tehatRack.ReadItem(num225, this.reader, flag17);
				}
				else
				{
					this.reader.ReadInt32();
					this.reader.ReadByte();
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, num223, null, num223, (float)num224, (float)num225, (float)flag17.ToInt(), 0, 0, 0);
					return;
				}
				return;
			}
			case 125:
			{
				int num226 = (int)this.reader.ReadByte();
				int num227 = (int)this.reader.ReadInt16();
				int num228 = (int)this.reader.ReadInt16();
				int num229 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num226 = this.whoAmI;
				}
				if (Main.netMode == 1)
				{
					Main.player[Main.myPlayer].GetOtherPlayersPickTile(num227, num228, num229);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(125, -1, num226, null, num226, (float)num227, (float)num228, (float)num229, 0, 0, 0);
					return;
				}
				return;
			}
			case 126:
				if (Main.netMode != 1)
				{
					return;
				}
				NPC.RevengeManager.AddMarkerFromReader(this.reader);
				return;
			case 127:
			{
				int markerUniqueID = this.reader.ReadInt32();
				if (Main.netMode != 1)
				{
					return;
				}
				NPC.RevengeManager.DestroyMarker(markerUniqueID);
				return;
			}
			case 128:
			{
				int num230 = (int)this.reader.ReadByte();
				int num231 = (int)this.reader.ReadUInt16();
				int num232 = (int)this.reader.ReadUInt16();
				int num233 = (int)this.reader.ReadUInt16();
				int num234 = (int)this.reader.ReadUInt16();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(128, -1, num230, null, num230, (float)num233, (float)num234, 0f, num231, num232, 0);
					return;
				}
				GolfHelper.ContactListener.PutBallInCup_TextAndEffects(new Point(num231, num232), num230, num233, num234);
				return;
			}
			case 129:
				if (Main.netMode != 1)
				{
					return;
				}
				Main.FixUIScale();
				Main.TrySetPreparationState(Main.WorldPreparationState.ProcessingData);
				return;
			case 130:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num235 = (int)this.reader.ReadUInt16();
				int num236 = (int)this.reader.ReadUInt16();
				int num237 = (int)this.reader.ReadInt16();
				if (num237 == 682)
				{
					if (NPC.unlockedSlimeRedSpawn)
					{
						return;
					}
					NPC.unlockedSlimeRedSpawn = true;
					NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				num235 *= 16;
				num236 *= 16;
				NPC npc7 = new NPC();
				npc7.SetDefaults(num237, default(NPCSpawnParams));
				int type17 = npc7.type;
				int netID = npc7.netID;
				int num238 = NPC.NewNPC(new EntitySource_FishedOut(Main.player[this.whoAmI]), num235, num236, num237, 0, 0f, 0f, 0f, 0f, 255);
				if (netID != type17)
				{
					Main.npc[num238].SetDefaults(netID, default(NPCSpawnParams));
					NetMessage.TrySendData(23, -1, -1, null, num238, 0f, 0f, 0f, 0, 0, 0);
				}
				if (num237 == 682)
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
				int num239 = (int)this.reader.ReadUInt16();
				NPC npc8;
				if (num239 < 200)
				{
					npc8 = Main.npc[num239];
				}
				else
				{
					npc8 = new NPC();
				}
				int num240 = (int)this.reader.ReadByte();
				if (num240 == 1)
				{
					int time2 = this.reader.ReadInt32();
					int fromWho = (int)this.reader.ReadInt16();
					npc8.GetImmuneTime(fromWho, time2);
					return;
				}
				return;
			}
			case 132:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				Point point = this.reader.ReadVector2().ToPoint();
				ushort key = this.reader.ReadUInt16();
				LegacySoundStyle legacySoundStyle = SoundID.SoundByIndex[key];
				BitsByte bitsByte32 = this.reader.ReadByte();
				int style3;
				if (bitsByte32[0])
				{
					style3 = this.reader.ReadInt32();
				}
				else
				{
					style3 = legacySoundStyle.Style;
				}
				float volumeScale;
				if (bitsByte32[1])
				{
					volumeScale = MathHelper.Clamp(this.reader.ReadSingle(), 0f, 1f);
				}
				else
				{
					volumeScale = legacySoundStyle.Volume;
				}
				float pitchOffset;
				if (bitsByte32[2])
				{
					pitchOffset = MathHelper.Clamp(this.reader.ReadSingle(), -1f, 1f);
				}
				else
				{
					pitchOffset = legacySoundStyle.GetRandomPitch();
				}
				SoundEngine.PlaySound(legacySoundStyle.SoundId, point.X, point.Y, style3, volumeScale, pitchOffset);
				return;
			}
			case 133:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int x14 = (int)this.reader.ReadInt16();
				int y14 = (int)this.reader.ReadInt16();
				int netid3 = (int)this.reader.ReadInt16();
				int prefix3 = (int)this.reader.ReadByte();
				int stack8 = (int)this.reader.ReadInt16();
				TEFoodPlatter.TryPlacing(x14, y14, netid3, prefix3, stack8);
				return;
			}
			case 134:
			{
				int num241 = (int)this.reader.ReadByte();
				int ladyBugLuckTimeLeft = this.reader.ReadInt32();
				float torchLuck = this.reader.ReadSingle();
				byte luckPotion = this.reader.ReadByte();
				bool hasGardenGnomeNearby = this.reader.ReadBoolean();
				float equipmentBasedLuckBonus = this.reader.ReadSingle();
				float coinLuck = this.reader.ReadSingle();
				if (Main.netMode == 2)
				{
					num241 = this.whoAmI;
				}
				Player player20 = Main.player[num241];
				player20.ladyBugLuckTimeLeft = ladyBugLuckTimeLeft;
				player20.torchLuck = torchLuck;
				player20.luckPotion = luckPotion;
				player20.HasGardenGnomeNearby = hasGardenGnomeNearby;
				player20.equipmentBasedLuckBonus = equipmentBasedLuckBonus;
				player20.coinLuck = coinLuck;
				player20.RecalculateLuck();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(134, -1, num241, null, num241, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 135:
			{
				int num242 = (int)this.reader.ReadByte();
				if (Main.netMode == 1)
				{
					Main.player[num242].immuneAlpha = 255;
					return;
				}
				return;
			}
			case 136:
				for (int num243 = 0; num243 < 2; num243++)
				{
					for (int num244 = 0; num244 < 3; num244++)
					{
						NPC.cavernMonsterType[num243, num244] = (int)this.reader.ReadUInt16();
					}
				}
				return;
			case 137:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num245 = (int)this.reader.ReadInt16();
				int buffTypeToRemove = (int)this.reader.ReadUInt16();
				if (num245 >= 0 && num245 < 200)
				{
					Main.npc[num245].RequestBuffRemoval(buffTypeToRemove);
					return;
				}
				return;
			}
			case 139:
				if (Main.netMode != 2)
				{
					int num246 = (int)this.reader.ReadByte();
					bool flag18 = this.reader.ReadBoolean();
					Main.countsAsHostForGameplay[num246] = flag18;
					return;
				}
				return;
			case 140:
			{
				int num247 = (int)this.reader.ReadByte();
				int num248 = this.reader.ReadInt32();
				switch (num247)
				{
				case 0:
					if (Main.netMode != 1)
					{
						return;
					}
					CreditsRollEvent.SetRemainingTimeDirect(num248);
					return;
				case 1:
					if (Main.netMode != 2)
					{
						return;
					}
					NPC.TransformCopperSlime(num248);
					return;
				case 2:
					if (Main.netMode != 2)
					{
						return;
					}
					NPC.TransformElderSlime(num248);
					return;
				default:
					return;
				}
				break;
			}
			case 141:
			{
				LucyAxeMessage.MessageSource messageSource = (LucyAxeMessage.MessageSource)this.reader.ReadByte();
				byte b18 = this.reader.ReadByte();
				Vector2 vector8 = this.reader.ReadVector2();
				int num249 = this.reader.ReadInt32();
				int num250 = this.reader.ReadInt32();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(141, -1, this.whoAmI, null, (int)messageSource, (float)b18, vector8.X, vector8.Y, num249, num250, 0);
					return;
				}
				LucyAxeMessage.CreateFromNet(messageSource, b18, new Vector2((float)num249, (float)num250), vector8);
				return;
			}
			case 142:
			{
				int num251 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num251 = this.whoAmI;
				}
				Player player21 = Main.player[num251];
				player21.piggyBankProjTracker.TryReading(this.reader);
				player21.voidLensChest.TryReading(this.reader);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(142, -1, this.whoAmI, null, num251, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 143:
				if (Main.netMode != 2)
				{
					return;
				}
				DD2Event.AttemptToSkipWaitTime();
				return;
			case 144:
				if (Main.netMode != 2)
				{
					return;
				}
				NPC.HaveDryadDoStardewAnimation();
				return;
			case 146:
			{
				int num252 = (int)this.reader.ReadByte();
				if (num252 == 0)
				{
					Item.ShimmerEffect(this.reader.ReadVector2());
					return;
				}
				if (num252 == 1)
				{
					Vector2 coinPosition = this.reader.ReadVector2();
					int coinAmount = this.reader.ReadInt32();
					Main.player[Main.myPlayer].AddCoinLuck(coinPosition, coinAmount);
					return;
				}
				if (num252 == 2)
				{
					int num253 = this.reader.ReadInt32();
					Main.npc[num253].SetNetShimmerEffect();
					return;
				}
				return;
			}
			case 147:
			{
				int num254 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num254 = this.whoAmI;
				}
				int num255 = (int)this.reader.ReadByte();
				Main.player[num254].TrySwitchingLoadout(num255);
				MessageBuffer.ReadAccessoryVisibility(this.reader, Main.player[num254].hideVisibleAccessory);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, num254, null, num254, (float)num255, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			default:
				goto IL_97FA;
			}
			if (Main.netMode != 2)
			{
				return;
			}
			if (Netplay.Clients[this.whoAmI].State == 1)
			{
				Netplay.Clients[this.whoAmI].State = 2;
			}
			NetMessage.TrySendData(7, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			Main.SyncAnInvasion(this.whoAmI);
			return;
			IL_525A:
			if (Netplay.Connection.State == 6)
			{
				Netplay.Connection.State = 10;
				Main.player[Main.myPlayer].Spawn(PlayerSpawnContext.SpawningIntoWorld);
				return;
			}
			return;
			IL_76C2:
			int num256 = (int)this.reader.ReadInt16();
			int num257 = this.reader.ReadInt32();
			float num258 = this.reader.ReadSingle();
			float num259 = this.reader.ReadSingle();
			if (num256 < 0 || num256 > 200)
			{
				return;
			}
			if (Main.netMode == 1)
			{
				Main.npc[num256].moneyPing(new Vector2(num258, num259));
				Main.npc[num256].extraValue = num257;
				return;
			}
			Main.npc[num256].extraValue += num257;
			NetMessage.TrySendData(92, -1, -1, null, num256, (float)Main.npc[num256].extraValue, num258, num259, 0, 0, 0);
			return;
			IL_97FA:
			if (Netplay.Clients[this.whoAmI].State == 0)
			{
				NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0002834C File Offset: 0x0002654C
		private static void ReadAccessoryVisibility(BinaryReader reader, bool[] hideVisibleAccessory)
		{
			ushort num = reader.ReadUInt16();
			for (int i = 0; i < hideVisibleAccessory.Length; i++)
			{
				hideVisibleAccessory[i] = (((int)num & 1 << i) != 0);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0002837C File Offset: 0x0002657C
		private static void TrySendingItemArray(int plr, Item[] array, int slotStartIndex)
		{
			for (int i = 0; i < array.Length; i++)
			{
				NetMessage.TrySendData(5, -1, -1, null, plr, (float)(slotStartIndex + i), (float)array[i].prefix, 0f, 0, 0, 0);
			}
		}

		// Token: 0x04000157 RID: 343
		public const int readBufferMax = 131070;

		// Token: 0x04000158 RID: 344
		public const int writeBufferMax = 131070;

		// Token: 0x04000159 RID: 345
		public bool broadcast;

		// Token: 0x0400015A RID: 346
		public byte[] readBuffer = new byte[131070];

		// Token: 0x0400015B RID: 347
		public byte[] writeBuffer = new byte[131070];

		// Token: 0x0400015C RID: 348
		public bool writeLocked;

		// Token: 0x0400015D RID: 349
		public int messageLength;

		// Token: 0x0400015E RID: 350
		public int totalData;

		// Token: 0x0400015F RID: 351
		public int whoAmI;

		// Token: 0x04000160 RID: 352
		public int spamCount;

		// Token: 0x04000161 RID: 353
		public int maxSpam;

		// Token: 0x04000162 RID: 354
		public bool checkBytes;

		// Token: 0x04000163 RID: 355
		public MemoryStream readerStream;

		// Token: 0x04000164 RID: 356
		public MemoryStream writerStream;

		// Token: 0x04000165 RID: 357
		public BinaryReader reader;

		// Token: 0x04000166 RID: 358
		public BinaryWriter writer;

		// Token: 0x04000167 RID: 359
		public PacketHistory History = new PacketHistory(100, 65535);

		// Token: 0x04000169 RID: 361
		private float[] _temporaryProjectileAI = new float[Projectile.maxAI];

		// Token: 0x0400016A RID: 362
		private float[] _temporaryNPCAI = new float[NPC.maxAI];
	}
}
