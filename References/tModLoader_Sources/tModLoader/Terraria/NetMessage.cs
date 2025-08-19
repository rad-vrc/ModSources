using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Ionic.Zlib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Net.Sockets;
using Terraria.Social;

namespace Terraria
{
	// Token: 0x0200003E RID: 62
	public class NetMessage
	{
		/// <inheritdoc cref="M:Terraria.NetMessage.SendData(System.Int32,System.Int32,System.Int32,Terraria.Localization.NetworkText,System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32)" />
		// Token: 0x060006F1 RID: 1777 RVA: 0x00157F00 File Offset: 0x00156100
		public static bool TrySendData(int msgType, int remoteClient = -1, int ignoreClient = -1, NetworkText text = null, int number = 0, float number2 = 0f, float number3 = 0f, float number4 = 0f, int number5 = 0, int number6 = 0, int number7 = 0)
		{
			try
			{
				NetMessage.SendData(msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, number5, number6, number7);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Used to send a vanilla <see cref="T:Terraria.ID.MessageID" /> message to send data between the server and client. This is the vanilla equivalent to <see cref="T:Terraria.ModLoader.ModPacket" />.
		/// <br /><br /> Vanilla messages are usually automatically sent by the game, but there are cases where manually sending a vanilla message is useful. To get started with learning netcode, read <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Netcode">the Basic Netcode wiki guide</see>.
		/// <br /><br /> To use this method, pass a <see cref="T:Terraria.ID.MessageID" /> value into <paramref name="msgType" /> parameter. The remaining parameters (text and number0-7) need to be populated with appropriate values according to the documentation of that specific <see cref="T:Terraria.ID.MessageID" />. When in doubt, consult the decompiled source code.
		/// <br /><br /> <b><paramref name="ignoreClient" /> and <paramref name="remoteClient" />:</b> When called on a client, the data will be sent to the server and <paramref name="ignoreClient" /> and <paramref name="remoteClient" /> are ignored. When called on a server, the data will be sent to either all clients, all clients except a specific client, or just a specific client depending on the values of <paramref name="ignoreClient" /> and <paramref name="remoteClient" />.
		/// <br /><br /> The <see cref="M:Terraria.MessageBuffer.GetData(System.Int32,System.Int32,System.Int32@)" /> method is what handles these messages when received.
		/// </summary>
		// Token: 0x060006F2 RID: 1778 RVA: 0x00157F40 File Offset: 0x00156140
		public unsafe static void SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, NetworkText text = null, int number = 0, float number2 = 0f, float number3 = 0f, float number4 = 0f, int number5 = 0, int number6 = 0, int number7 = 0)
		{
			if (Main.netMode == 0)
			{
				return;
			}
			if (msgType == 21 && (Main.item[number].shimmerTime > 0f || Main.item[number].shimmered))
			{
				msgType = 145;
			}
			int num = 256;
			if (text == null)
			{
				text = NetworkText.Empty;
			}
			if (Main.netMode == 2 && remoteClient >= 0)
			{
				num = remoteClient;
			}
			if (ModNet.HijackSendData(num, msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, number5, number6, number7))
			{
				return;
			}
			MessageBuffer obj = NetMessage.buffer[num];
			lock (obj)
			{
				BinaryWriter writer = NetMessage.buffer[num].writer;
				if (writer == null)
				{
					NetMessage.buffer[num].ResetWriter();
					writer = NetMessage.buffer[num].writer;
				}
				writer.BaseStream.Position = 0L;
				long position = writer.BaseStream.Position;
				writer.BaseStream.Position += 2L;
				writer.Write((byte)msgType);
				switch (msgType)
				{
				case 1:
					writer.Write(ModNet.NetVersionString);
					break;
				case 2:
					text.Serialize(writer);
					if (Main.dedServ)
					{
						Logging.ServerConsoleLine(Language.GetTextValue("CLI.ClientWasBooted", Netplay.Clients[num].Socket.GetRemoteAddress().ToString(), text));
					}
					break;
				case 3:
					writer.Write((byte)remoteClient);
					writer.Write(false);
					break;
				case 4:
				{
					Player player4 = Main.player[number];
					writer.Write((byte)number);
					writer.Write((byte)player4.skinVariant);
					writer.Write((byte)player4.hair);
					writer.Write(player4.name);
					writer.Write7BitEncodedInt(player4.hairDye);
					NetMessage.WriteAccessoryVisibility(writer, player4.hideVisibleAccessory);
					writer.Write(player4.hideMisc);
					writer.WriteRGB(player4.hairColor);
					writer.WriteRGB(player4.skinColor);
					writer.WriteRGB(player4.eyeColor);
					writer.WriteRGB(player4.shirtColor);
					writer.WriteRGB(player4.underShirtColor);
					writer.WriteRGB(player4.pantsColor);
					writer.WriteRGB(player4.shoeColor);
					BitsByte bitsByte16 = 0;
					if (player4.difficulty == 1)
					{
						bitsByte16[0] = true;
					}
					else if (player4.difficulty == 2)
					{
						bitsByte16[1] = true;
					}
					else if (player4.difficulty == 3)
					{
						bitsByte16[3] = true;
					}
					bitsByte16[2] = player4.extraAccessory;
					writer.Write(bitsByte16);
					BitsByte bitsByte17 = 0;
					bitsByte17[0] = player4.UsingBiomeTorches;
					bitsByte17[1] = player4.happyFunTorchTime;
					bitsByte17[2] = player4.unlockedBiomeTorches;
					bitsByte17[3] = player4.unlockedSuperCart;
					bitsByte17[4] = player4.enabledSuperCart;
					writer.Write(bitsByte17);
					BitsByte bitsByte18 = 0;
					bitsByte18[0] = player4.usedAegisCrystal;
					bitsByte18[1] = player4.usedAegisFruit;
					bitsByte18[2] = player4.usedArcaneCrystal;
					bitsByte18[3] = player4.usedGalaxyPearl;
					bitsByte18[4] = player4.usedGummyWorm;
					bitsByte18[5] = player4.usedAmbrosia;
					bitsByte18[6] = player4.ateArtisanBread;
					writer.Write(bitsByte18);
					break;
				}
				case 5:
				{
					writer.Write((byte)number);
					writer.Write((short)number2);
					Player player5 = Main.player[number];
					Item item6 = (number2 >= (float)PlayerItemSlotID.Loadout3_Dye_0) ? player5.Loadouts[2].Dye[(int)number2 - PlayerItemSlotID.Loadout3_Dye_0] : ((number2 >= (float)PlayerItemSlotID.Loadout3_Armor_0) ? player5.Loadouts[2].Armor[(int)number2 - PlayerItemSlotID.Loadout3_Armor_0] : ((number2 >= (float)PlayerItemSlotID.Loadout2_Dye_0) ? player5.Loadouts[1].Dye[(int)number2 - PlayerItemSlotID.Loadout2_Dye_0] : ((number2 >= (float)PlayerItemSlotID.Loadout2_Armor_0) ? player5.Loadouts[1].Armor[(int)number2 - PlayerItemSlotID.Loadout2_Armor_0] : ((number2 >= (float)PlayerItemSlotID.Loadout1_Dye_0) ? player5.Loadouts[0].Dye[(int)number2 - PlayerItemSlotID.Loadout1_Dye_0] : ((number2 >= (float)PlayerItemSlotID.Loadout1_Armor_0) ? player5.Loadouts[0].Armor[(int)number2 - PlayerItemSlotID.Loadout1_Armor_0] : ((number2 >= (float)PlayerItemSlotID.Bank4_0) ? player5.bank4.item[(int)number2 - PlayerItemSlotID.Bank4_0] : ((number2 >= (float)PlayerItemSlotID.Bank3_0) ? player5.bank3.item[(int)number2 - PlayerItemSlotID.Bank3_0] : ((number2 >= (float)PlayerItemSlotID.TrashItem) ? player5.trashItem : ((number2 >= (float)PlayerItemSlotID.Bank2_0) ? player5.bank2.item[(int)number2 - PlayerItemSlotID.Bank2_0] : ((number2 >= (float)PlayerItemSlotID.Bank1_0) ? player5.bank.item[(int)number2 - PlayerItemSlotID.Bank1_0] : ((number2 >= (float)PlayerItemSlotID.MiscDye0) ? player5.miscDyes[(int)number2 - PlayerItemSlotID.MiscDye0] : ((number2 >= (float)PlayerItemSlotID.Misc0) ? player5.miscEquips[(int)number2 - PlayerItemSlotID.Misc0] : ((number2 >= (float)PlayerItemSlotID.Dye0) ? player5.dye[(int)number2 - PlayerItemSlotID.Dye0] : ((number2 < (float)PlayerItemSlotID.Armor0) ? player5.inventory[(int)number2 - PlayerItemSlotID.Inventory0] : player5.armor[(int)number2 - PlayerItemSlotID.Armor0]))))))))))))));
					if (item6.Name == "" || item6.stack == 0 || item6.type == 0)
					{
						item6.SetDefaults(0, true, null);
					}
					int stack = item6.stack;
					int netID = item6.netID;
					ItemIO.Send(item6, writer, true, false);
					break;
				}
				case 7:
				{
					writer.Write((int)Main.time);
					BitsByte bitsByte19 = 0;
					bitsByte19[0] = Main.dayTime;
					bitsByte19[1] = Main.bloodMoon;
					bitsByte19[2] = Main.eclipse;
					writer.Write(bitsByte19);
					writer.Write((byte)Main.moonPhase);
					writer.Write((short)Main.maxTilesX);
					writer.Write((short)Main.maxTilesY);
					writer.Write((short)Main.spawnTileX);
					writer.Write((short)Main.spawnTileY);
					writer.Write((short)Main.worldSurface);
					writer.Write((short)Main.rockLayer);
					writer.Write(Main.worldID);
					writer.Write(Main.worldName);
					writer.Write((byte)Main.GameMode);
					writer.Write(Main.ActiveWorldFileData.UniqueId.ToByteArray());
					writer.Write(Main.ActiveWorldFileData.WorldGeneratorVersion);
					writer.Write((byte)Main.moonType);
					writer.Write((byte)WorldGen.treeBG1);
					writer.Write((byte)WorldGen.treeBG2);
					writer.Write((byte)WorldGen.treeBG3);
					writer.Write((byte)WorldGen.treeBG4);
					writer.Write((byte)WorldGen.corruptBG);
					writer.Write((byte)WorldGen.jungleBG);
					writer.Write((byte)WorldGen.snowBG);
					writer.Write((byte)WorldGen.hallowBG);
					writer.Write((byte)WorldGen.crimsonBG);
					writer.Write((byte)WorldGen.desertBG);
					writer.Write((byte)WorldGen.oceanBG);
					writer.Write((byte)WorldGen.mushroomBG);
					writer.Write((byte)WorldGen.underworldBG);
					writer.Write((byte)Main.iceBackStyle);
					writer.Write((byte)Main.jungleBackStyle);
					writer.Write((byte)Main.hellBackStyle);
					writer.Write(Main.windSpeedTarget);
					writer.Write((byte)Main.numClouds);
					for (int i = 0; i < 3; i++)
					{
						writer.Write(Main.treeX[i]);
					}
					for (int num2 = 0; num2 < 4; num2++)
					{
						writer.Write((byte)Main.treeStyle[num2]);
					}
					for (int num3 = 0; num3 < 3; num3++)
					{
						writer.Write(Main.caveBackX[num3]);
					}
					for (int num4 = 0; num4 < 4; num4++)
					{
						writer.Write((byte)Main.caveBackStyle[num4]);
					}
					WorldGen.TreeTops.SyncSend(writer);
					if (!Main.raining)
					{
						Main.maxRaining = 0f;
					}
					writer.Write(Main.maxRaining);
					BitsByte bitsByte20 = 0;
					bitsByte20[0] = WorldGen.shadowOrbSmashed;
					bitsByte20[1] = NPC.downedBoss1;
					bitsByte20[2] = NPC.downedBoss2;
					bitsByte20[3] = NPC.downedBoss3;
					bitsByte20[4] = Main.hardMode;
					bitsByte20[5] = NPC.downedClown;
					bitsByte20[7] = NPC.downedPlantBoss;
					writer.Write(bitsByte20);
					BitsByte bitsByte21 = 0;
					bitsByte21[0] = NPC.downedMechBoss1;
					bitsByte21[1] = NPC.downedMechBoss2;
					bitsByte21[2] = NPC.downedMechBoss3;
					bitsByte21[3] = NPC.downedMechBossAny;
					bitsByte21[4] = (Main.cloudBGActive >= 1f);
					bitsByte21[5] = WorldGen.crimson;
					bitsByte21[6] = Main.pumpkinMoon;
					bitsByte21[7] = Main.snowMoon;
					writer.Write(bitsByte21);
					BitsByte bitsByte22 = 0;
					bitsByte22[1] = Main.fastForwardTimeToDawn;
					bitsByte22[2] = Main.slimeRain;
					bitsByte22[3] = NPC.downedSlimeKing;
					bitsByte22[4] = NPC.downedQueenBee;
					bitsByte22[5] = NPC.downedFishron;
					bitsByte22[6] = NPC.downedMartians;
					bitsByte22[7] = NPC.downedAncientCultist;
					writer.Write(bitsByte22);
					BitsByte bitsByte23 = 0;
					bitsByte23[0] = NPC.downedMoonlord;
					bitsByte23[1] = NPC.downedHalloweenKing;
					bitsByte23[2] = NPC.downedHalloweenTree;
					bitsByte23[3] = NPC.downedChristmasIceQueen;
					bitsByte23[4] = NPC.downedChristmasSantank;
					bitsByte23[5] = NPC.downedChristmasTree;
					bitsByte23[6] = NPC.downedGolemBoss;
					bitsByte23[7] = BirthdayParty.PartyIsUp;
					writer.Write(bitsByte23);
					BitsByte bitsByte24 = 0;
					bitsByte24[0] = NPC.downedPirates;
					bitsByte24[1] = NPC.downedFrost;
					bitsByte24[2] = NPC.downedGoblins;
					bitsByte24[3] = Sandstorm.Happening;
					bitsByte24[4] = DD2Event.Ongoing;
					bitsByte24[5] = DD2Event.DownedInvasionT1;
					bitsByte24[6] = DD2Event.DownedInvasionT2;
					bitsByte24[7] = DD2Event.DownedInvasionT3;
					writer.Write(bitsByte24);
					BitsByte bitsByte25 = 0;
					bitsByte25[0] = NPC.combatBookWasUsed;
					bitsByte25[1] = LanternNight.LanternsUp;
					bitsByte25[2] = NPC.downedTowerSolar;
					bitsByte25[3] = NPC.downedTowerVortex;
					bitsByte25[4] = NPC.downedTowerNebula;
					bitsByte25[5] = NPC.downedTowerStardust;
					bitsByte25[6] = Main.forceHalloweenForToday;
					bitsByte25[7] = Main.forceXMasForToday;
					writer.Write(bitsByte25);
					BitsByte bitsByte26 = 0;
					bitsByte26[0] = NPC.boughtCat;
					bitsByte26[1] = NPC.boughtDog;
					bitsByte26[2] = NPC.boughtBunny;
					bitsByte26[3] = NPC.freeCake;
					bitsByte26[4] = Main.drunkWorld;
					bitsByte26[5] = NPC.downedEmpressOfLight;
					bitsByte26[6] = NPC.downedQueenSlime;
					bitsByte26[7] = Main.getGoodWorld;
					writer.Write(bitsByte26);
					BitsByte bitsByte27 = 0;
					bitsByte27[0] = Main.tenthAnniversaryWorld;
					bitsByte27[1] = Main.dontStarveWorld;
					bitsByte27[2] = NPC.downedDeerclops;
					bitsByte27[3] = Main.notTheBeesWorld;
					bitsByte27[4] = Main.remixWorld;
					bitsByte27[5] = NPC.unlockedSlimeBlueSpawn;
					bitsByte27[6] = NPC.combatBookVolumeTwoWasUsed;
					bitsByte27[7] = NPC.peddlersSatchelWasUsed;
					writer.Write(bitsByte27);
					BitsByte bitsByte28 = 0;
					bitsByte28[0] = NPC.unlockedSlimeGreenSpawn;
					bitsByte28[1] = NPC.unlockedSlimeOldSpawn;
					bitsByte28[2] = NPC.unlockedSlimePurpleSpawn;
					bitsByte28[3] = NPC.unlockedSlimeRainbowSpawn;
					bitsByte28[4] = NPC.unlockedSlimeRedSpawn;
					bitsByte28[5] = NPC.unlockedSlimeYellowSpawn;
					bitsByte28[6] = NPC.unlockedSlimeCopperSpawn;
					bitsByte28[7] = Main.fastForwardTimeToDusk;
					writer.Write(bitsByte28);
					BitsByte bitsByte29 = 0;
					bitsByte29[0] = Main.noTrapsWorld;
					bitsByte29[1] = Main.zenithWorld;
					bitsByte29[2] = NPC.unlockedTruffleSpawn;
					writer.Write(bitsByte29);
					writer.Write((byte)Main.sundialCooldown);
					writer.Write((byte)Main.moondialCooldown);
					writer.Write((short)WorldGen.SavedOreTiers.Copper);
					writer.Write((short)WorldGen.SavedOreTiers.Iron);
					writer.Write((short)WorldGen.SavedOreTiers.Silver);
					writer.Write((short)WorldGen.SavedOreTiers.Gold);
					writer.Write((short)WorldGen.SavedOreTiers.Cobalt);
					writer.Write((short)WorldGen.SavedOreTiers.Mythril);
					writer.Write((short)WorldGen.SavedOreTiers.Adamantite);
					writer.Write((sbyte)Main.invasionType);
					if (SocialAPI.Network != null)
					{
						writer.Write(SocialAPI.Network.GetLobbyId());
					}
					else
					{
						writer.Write(0UL);
					}
					writer.Write(Sandstorm.IntendedSeverity);
					WorldIO.SendModData(writer);
					break;
				}
				case 8:
					writer.Write(number);
					writer.Write((int)number2);
					break;
				case 9:
				{
					writer.Write(number);
					text.Serialize(writer);
					BitsByte bitsByte30 = (byte)number2;
					writer.Write(bitsByte30);
					break;
				}
				case 10:
					NetMessage.CompressTileBlock(number, (int)number2, (short)number3, (short)number4, writer.BaseStream);
					break;
				case 11:
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write((short)number3);
					writer.Write((short)number4);
					break;
				case 12:
				{
					Player player6 = Main.player[number];
					writer.Write((byte)number);
					writer.Write((short)player6.SpawnX);
					writer.Write((short)player6.SpawnY);
					writer.Write(player6.respawnTimer);
					writer.Write((short)player6.numberOfDeathsPVE);
					writer.Write((short)player6.numberOfDeathsPVP);
					writer.Write((byte)number2);
					break;
				}
				case 13:
				{
					Player player7 = Main.player[number];
					writer.Write((byte)number);
					BitsByte bitsByte31 = 0;
					bitsByte31[0] = player7.controlUp;
					bitsByte31[1] = player7.controlDown;
					bitsByte31[2] = player7.controlLeft;
					bitsByte31[3] = player7.controlRight;
					bitsByte31[4] = player7.controlJump;
					bitsByte31[5] = player7.controlUseItem;
					bitsByte31[6] = (player7.direction == 1);
					writer.Write(bitsByte31);
					BitsByte bitsByte32 = 0;
					bitsByte32[0] = player7.pulley;
					bitsByte32[1] = (player7.pulley && player7.pulleyDir == 2);
					bitsByte32[2] = (player7.velocity != Vector2.Zero);
					bitsByte32[3] = player7.vortexStealthActive;
					bitsByte32[4] = (player7.gravDir == 1f);
					bitsByte32[5] = player7.shieldRaised;
					bitsByte32[6] = player7.ghost;
					writer.Write(bitsByte32);
					BitsByte bitsByte33 = 0;
					bitsByte33[0] = player7.tryKeepingHoveringUp;
					bitsByte33[1] = player7.IsVoidVaultEnabled;
					bitsByte33[2] = player7.sitting.isSitting;
					bitsByte33[3] = player7.downedDD2EventAnyDifficulty;
					bitsByte33[4] = player7.isPettingAnimal;
					bitsByte33[5] = player7.isTheAnimalBeingPetSmall;
					bitsByte33[6] = (player7.PotionOfReturnOriginalUsePosition != null);
					bitsByte33[7] = player7.tryKeepingHoveringDown;
					writer.Write(bitsByte33);
					BitsByte bitsByte34 = 0;
					bitsByte34[0] = player7.sleeping.isSleeping;
					bitsByte34[1] = player7.autoReuseAllWeapons;
					bitsByte34[2] = player7.controlDownHold;
					bitsByte34[3] = player7.isOperatingAnotherEntity;
					bitsByte34[4] = player7.controlUseTile;
					writer.Write(bitsByte34);
					writer.Write((byte)player7.selectedItem);
					writer.WriteVector2(player7.position);
					if (bitsByte32[2])
					{
						writer.WriteVector2(player7.velocity);
					}
					if (bitsByte33[6])
					{
						writer.WriteVector2(player7.PotionOfReturnOriginalUsePosition.Value);
						writer.WriteVector2(player7.PotionOfReturnHomePosition.Value);
					}
					break;
				}
				case 14:
					writer.Write((byte)number);
					writer.Write((byte)number2);
					break;
				case 16:
					writer.Write((byte)number);
					writer.Write((short)Main.player[number].statLife);
					writer.Write((short)Main.player[number].statLifeMax);
					break;
				case 17:
					writer.Write((byte)number);
					writer.Write((short)number2);
					writer.Write((short)number3);
					writer.Write((short)number4);
					writer.Write((byte)number5);
					break;
				case 18:
					writer.Write((Main.dayTime > false) ? 1 : 0);
					writer.Write((int)Main.time);
					writer.Write(Main.sunModY);
					writer.Write(Main.moonModY);
					break;
				case 19:
					writer.Write((byte)number);
					writer.Write((short)number2);
					writer.Write((short)number3);
					writer.Write((number4 == 1f) ? 1 : 0);
					break;
				case 20:
				{
					int num5 = number;
					int num6 = (int)number2;
					int num7 = (int)number3;
					if (num7 < 0)
					{
						num7 = 0;
					}
					int num8 = (int)number4;
					if (num8 < 0)
					{
						num8 = 0;
					}
					if (num5 < num7)
					{
						num5 = num7;
					}
					if (num5 >= Main.maxTilesX + num7)
					{
						num5 = Main.maxTilesX - num7 - 1;
					}
					if (num6 < num8)
					{
						num6 = num8;
					}
					if (num6 >= Main.maxTilesY + num8)
					{
						num6 = Main.maxTilesY - num8 - 1;
					}
					writer.Write((short)num5);
					writer.Write((short)num6);
					writer.Write((byte)num7);
					writer.Write((byte)num8);
					writer.Write((byte)number5);
					for (int num9 = num5; num9 < num5 + num7; num9++)
					{
						for (int num10 = num6; num10 < num6 + num8; num10++)
						{
							BitsByte bitsByte35 = 0;
							BitsByte bitsByte36 = 0;
							BitsByte bitsByte37 = 0;
							byte b3 = 0;
							byte b4 = 0;
							Tile tile2 = Main.tile[num9, num10];
							bitsByte35[0] = tile2.active();
							bitsByte35[2] = (*tile2.wall > 0);
							bitsByte35[3] = (*tile2.liquid > 0 && Main.netMode == 2);
							bitsByte35[4] = tile2.wire();
							bitsByte35[5] = tile2.halfBrick();
							bitsByte35[6] = tile2.actuator();
							bitsByte35[7] = tile2.inActive();
							bitsByte36[0] = tile2.wire2();
							bitsByte36[1] = tile2.wire3();
							if (tile2.active() && tile2.color() > 0)
							{
								bitsByte36[2] = true;
								b3 = tile2.color();
							}
							if (*tile2.wall > 0 && tile2.wallColor() > 0)
							{
								bitsByte36[3] = true;
								b4 = tile2.wallColor();
							}
							bitsByte36 += (byte)(tile2.slope() << 4);
							bitsByte36[7] = tile2.wire4();
							bitsByte37[0] = tile2.fullbrightBlock();
							bitsByte37[1] = tile2.fullbrightWall();
							bitsByte37[2] = tile2.invisibleBlock();
							bitsByte37[3] = tile2.invisibleWall();
							writer.Write(bitsByte35);
							writer.Write(bitsByte36);
							writer.Write(bitsByte37);
							if (b3 > 0)
							{
								writer.Write(b3);
							}
							if (b4 > 0)
							{
								writer.Write(b4);
							}
							if (tile2.active())
							{
								writer.Write(*tile2.type);
								if (Main.tileFrameImportant[(int)(*tile2.type)])
								{
									writer.Write(*tile2.frameX);
									writer.Write(*tile2.frameY);
								}
							}
							if (*tile2.wall > 0)
							{
								writer.Write(*tile2.wall);
							}
							if (*tile2.liquid > 0 && Main.netMode == 2)
							{
								writer.Write(*tile2.liquid);
								writer.Write(tile2.liquidType());
							}
						}
					}
					break;
				}
				case 21:
				case 90:
				case 145:
				case 148:
				{
					Item item7 = Main.item[number];
					writer.Write((short)number);
					writer.WriteVector2(item7.position);
					writer.WriteVector2(item7.velocity);
					writer.Write7BitEncodedInt(item7.stack);
					writer.Write7BitEncodedInt(item7.prefix);
					writer.Write((byte)number2);
					short value2 = 0;
					if (item7.active && item7.stack > 0)
					{
						value2 = (short)item7.netID;
					}
					writer.Write(value2);
					if (msgType == 145)
					{
						writer.Write(item7.shimmered);
						writer.Write(item7.shimmerTime);
					}
					if (msgType == 148)
					{
						writer.Write((byte)MathHelper.Clamp((float)item7.timeLeftInWhichTheItemCannotBeTakenByEnemies, 0f, 255f));
					}
					ItemIO.SendModData(item7, writer);
					break;
				}
				case 22:
					writer.Write((short)number);
					writer.Write((byte)Main.item[number].playerIndexTheItemIsReservedFor);
					break;
				case 23:
				{
					NPC nPC2 = Main.npc[number];
					writer.Write((short)number);
					writer.WriteVector2(nPC2.position);
					writer.WriteVector2(nPC2.velocity);
					writer.Write((ushort)nPC2.target);
					int num11 = nPC2.life;
					if (!nPC2.active)
					{
						num11 = 0;
					}
					if (!nPC2.active || nPC2.life <= 0)
					{
						nPC2.netSkip = 0;
					}
					short value3 = (short)nPC2.netID;
					bool[] array = new bool[4];
					BitsByte bitsByte38 = 0;
					bitsByte38[0] = (nPC2.direction > 0);
					bitsByte38[1] = (nPC2.directionY > 0);
					bitsByte38[2] = (array[0] = (nPC2.ai[0] != 0f));
					bitsByte38[3] = (array[1] = (nPC2.ai[1] != 0f));
					bitsByte38[4] = (array[2] = (nPC2.ai[2] != 0f));
					bitsByte38[5] = (array[3] = (nPC2.ai[3] != 0f));
					bitsByte38[6] = (nPC2.spriteDirection > 0);
					bitsByte38[7] = (num11 == nPC2.lifeMax);
					writer.Write(bitsByte38);
					BitsByte bitsByte39 = 0;
					bitsByte39[0] = (nPC2.statsAreScaledForThisManyPlayers > 1);
					bitsByte39[1] = nPC2.SpawnedFromStatue;
					bitsByte39[2] = (nPC2.strengthMultiplier != 1f);
					byte[] extraAI = NPCLoader.WriteExtraAI(nPC2);
					bool hasExtraAI = extraAI != null && extraAI.Length != 0;
					bitsByte39[3] = hasExtraAI;
					writer.Write(bitsByte39);
					for (int j = 0; j < NPC.maxAI; j++)
					{
						if (array[j])
						{
							writer.Write(nPC2.ai[j]);
						}
					}
					writer.Write(value3);
					if (bitsByte39[0])
					{
						writer.Write((byte)nPC2.statsAreScaledForThisManyPlayers);
					}
					if (bitsByte39[2])
					{
						writer.Write(nPC2.strengthMultiplier);
					}
					if (!bitsByte38[7])
					{
						byte b5 = 1;
						if (nPC2.lifeMax > 32767)
						{
							b5 = 4;
						}
						else if (nPC2.lifeMax > 127)
						{
							b5 = 2;
						}
						writer.Write(b5);
						if (b5 != 2)
						{
							if (b5 != 4)
							{
								writer.Write((sbyte)num11);
							}
							else
							{
								writer.Write(num11);
							}
						}
						else
						{
							writer.Write((short)num11);
						}
					}
					if (nPC2.type >= 0 && Main.npcCatchable[nPC2.type])
					{
						writer.Write((byte)nPC2.releaseOwner);
					}
					if (hasExtraAI)
					{
						NPCLoader.SendExtraAI(writer, extraAI);
					}
					break;
				}
				case 24:
					writer.Write((short)number);
					writer.Write((byte)number2);
					break;
				case 27:
				{
					Projectile projectile = Main.projectile[number];
					writer.Write((short)projectile.identity);
					writer.WriteVector2(projectile.position);
					writer.WriteVector2(projectile.velocity);
					writer.Write((byte)projectile.owner);
					writer.Write((short)projectile.type);
					BitsByte bitsByte40 = 0;
					BitsByte bitsByte41 = 0;
					bitsByte40[0] = (projectile.ai[0] != 0f);
					bitsByte40[1] = (projectile.ai[1] != 0f);
					bitsByte41[0] = (projectile.ai[2] != 0f);
					if (projectile.bannerIdToRespondTo != 0)
					{
						bitsByte40[3] = true;
					}
					if (projectile.damage != 0)
					{
						bitsByte40[4] = true;
					}
					if (projectile.knockBack != 0f)
					{
						bitsByte40[5] = true;
					}
					if (projectile.type > 0 && ProjectileID.Sets.NeedsUUID[projectile.type])
					{
						bitsByte40[7] = true;
					}
					if (projectile.originalDamage != 0)
					{
						bitsByte40[6] = true;
					}
					byte[] extraAI2 = ProjectileLoader.WriteExtraAI(projectile);
					bool hasExtraAI2 = extraAI2 != null && extraAI2.Length != 0;
					bitsByte41[1] = hasExtraAI2;
					if (bitsByte41 != 0)
					{
						bitsByte40[2] = true;
					}
					writer.Write(bitsByte40);
					if (bitsByte40[2])
					{
						writer.Write(bitsByte41);
					}
					if (bitsByte40[0])
					{
						writer.Write(projectile.ai[0]);
					}
					if (bitsByte40[1])
					{
						writer.Write(projectile.ai[1]);
					}
					if (bitsByte40[3])
					{
						writer.Write((ushort)projectile.bannerIdToRespondTo);
					}
					if (bitsByte40[4])
					{
						writer.Write((short)projectile.damage);
					}
					if (bitsByte40[5])
					{
						writer.Write(projectile.knockBack);
					}
					if (bitsByte40[6])
					{
						writer.Write((short)projectile.originalDamage);
					}
					if (bitsByte40[7])
					{
						writer.Write((short)projectile.projUUID);
					}
					if (bitsByte41[0])
					{
						writer.Write(projectile.ai[2]);
					}
					if (hasExtraAI2)
					{
						ProjectileLoader.SendExtraAI(writer, extraAI2);
					}
					break;
				}
				case 28:
					writer.Write((short)number);
					if (number2 < 0f)
					{
						if (Main.netMode != 2)
						{
							throw new ArgumentException("Packet 28 (StrikeNPC) can only be sent with negative damage (silent insta-kill) from the server.");
						}
						writer.Write7BitEncodedInt((int)number2);
					}
					else
					{
						NPC.HitInfo hit = (number7 == 1) ? NetMessage._currentStrike : NetMessage._lastLegacyStrike;
						writer.Write7BitEncodedInt(hit.Damage);
						writer.Write7BitEncodedInt(hit.SourceDamage);
						writer.Write7BitEncodedInt(hit.DamageType.Type);
						writer.Write((sbyte)hit.HitDirection);
						writer.Write(hit.Knockback);
						BitsByte flags = new BitsByte(hit.Crit, hit.InstantKill, hit.HideCombatText, false, false, false, false, false);
						writer.Write(flags);
					}
					break;
				case 29:
					writer.Write((short)number);
					writer.Write((byte)number2);
					break;
				case 30:
					writer.Write((byte)number);
					writer.Write(Main.player[number].hostile);
					break;
				case 31:
					writer.Write((short)number);
					writer.Write((short)number2);
					break;
				case 32:
				{
					Item item9 = Main.chest[number].item[(int)((byte)number2)];
					writer.Write((short)number);
					writer.Write((byte)number2);
					ItemIO.Send(item9, writer, true, false);
					break;
				}
				case 33:
				{
					int num12 = 0;
					int num13 = 0;
					int num14 = 0;
					string text2 = null;
					if (number > -1)
					{
						num12 = Main.chest[number].x;
						num13 = Main.chest[number].y;
					}
					if (number2 == 1f)
					{
						string text3 = text.ToString();
						num14 = (int)((byte)text3.Length);
						if (num14 == 0 || num14 > 63)
						{
							num14 = 255;
						}
						else
						{
							text2 = text3;
						}
					}
					writer.Write((short)number);
					writer.Write((short)num12);
					writer.Write((short)num13);
					writer.Write((byte)num14);
					if (text2 != null)
					{
						writer.Write(text2);
					}
					break;
				}
				case 34:
					writer.Write((byte)number);
					writer.Write((short)number2);
					writer.Write((short)number3);
					writer.Write((short)number4);
					if (Main.netMode == 2)
					{
						Netplay.GetSectionX((int)number2);
						Netplay.GetSectionY((int)number3);
						writer.Write((short)number5);
					}
					else
					{
						writer.Write(0);
					}
					if (number >= 100)
					{
						writer.Write((ushort)number6);
					}
					break;
				case 35:
					writer.Write((byte)number);
					writer.Write((short)number2);
					break;
				case 36:
				{
					Player player8 = Main.player[number];
					writer.Write((byte)number);
					writer.Write(player8.zone1);
					writer.Write(player8.zone2);
					writer.Write(player8.zone3);
					writer.Write(player8.zone4);
					writer.Write(player8.zone5);
					BiomeLoader.SendCustomBiomes(player8, writer);
					break;
				}
				case 38:
					writer.Write(Netplay.ServerPassword);
					break;
				case 39:
					writer.Write((short)number);
					break;
				case 40:
					writer.Write((byte)number);
					writer.Write((short)Main.player[number].talkNPC);
					break;
				case 41:
					writer.Write((byte)number);
					writer.Write(Main.player[number].itemRotation);
					writer.Write((short)Main.player[number].itemAnimation);
					break;
				case 42:
					writer.Write((byte)number);
					writer.Write((short)Main.player[number].statMana);
					writer.Write((short)Main.player[number].statManaMax);
					break;
				case 43:
					writer.Write((byte)number);
					writer.Write((short)number2);
					break;
				case 45:
					writer.Write((byte)number);
					writer.Write((byte)Main.player[number].team);
					break;
				case 46:
					writer.Write((short)number);
					writer.Write((short)number2);
					break;
				case 47:
					writer.Write((short)number);
					writer.Write((short)Main.sign[number].x);
					writer.Write((short)Main.sign[number].y);
					writer.Write(Main.sign[number].text);
					writer.Write((byte)number2);
					writer.Write((byte)number3);
					break;
				case 48:
				{
					Tile tile3 = Main.tile[number, (int)number2];
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write(*tile3.liquid);
					writer.Write(tile3.liquidType());
					break;
				}
				case 50:
					writer.Write((byte)number);
					for (int k = 0; k < Player.maxBuffs; k++)
					{
						writer.Write((ushort)Main.player[number].buffType[k]);
					}
					break;
				case 51:
					writer.Write((byte)number);
					writer.Write((byte)number2);
					break;
				case 52:
					writer.Write((byte)number2);
					writer.Write((short)number3);
					writer.Write((short)number4);
					break;
				case 53:
					writer.Write((short)number);
					writer.Write((ushort)number2);
					writer.Write((short)number3);
					break;
				case 54:
					writer.Write((short)number);
					for (int l = 0; l < NPC.maxBuffs; l++)
					{
						writer.Write((ushort)Main.npc[number].buffType[l]);
						writer.Write((short)Main.npc[number].buffTime[l]);
					}
					break;
				case 55:
					writer.Write((byte)number);
					writer.Write((ushort)number2);
					writer.Write((int)number3);
					break;
				case 56:
					writer.Write((short)number);
					if (Main.netMode == 2)
					{
						string givenName = Main.npc[number].GivenName;
						writer.Write(givenName);
						writer.Write(Main.npc[number].townNpcVariationIndex);
					}
					break;
				case 57:
					writer.Write(WorldGen.tGood);
					writer.Write(WorldGen.tEvil);
					writer.Write(WorldGen.tBlood);
					break;
				case 58:
					writer.Write((byte)number);
					writer.Write(number2);
					break;
				case 59:
					writer.Write((short)number);
					writer.Write((short)number2);
					break;
				case 60:
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write((short)number3);
					writer.Write((byte)number4);
					break;
				case 61:
					writer.Write((short)number);
					writer.Write((short)number2);
					break;
				case 62:
					writer.Write((byte)number);
					writer.Write((byte)number2);
					break;
				case 63:
				case 64:
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write((byte)number3);
					writer.Write((byte)number4);
					break;
				case 65:
				{
					BitsByte bitsByte42 = 0;
					bitsByte42[0] = ((number & 1) == 1);
					bitsByte42[1] = ((number & 2) == 2);
					bitsByte42[2] = (number6 == 1);
					bitsByte42[3] = (number7 != 0);
					writer.Write(bitsByte42);
					writer.Write((short)number2);
					writer.Write(number3);
					writer.Write(number4);
					writer.Write((byte)number5);
					if (bitsByte42[3])
					{
						writer.Write(number7);
					}
					break;
				}
				case 66:
					writer.Write((byte)number);
					writer.Write((short)number2);
					break;
				case 68:
					writer.Write(Main.clientUUID);
					break;
				case 69:
					Netplay.GetSectionX((int)number2);
					Netplay.GetSectionY((int)number3);
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write((short)number3);
					writer.Write(Main.chest[(int)((short)number)].name);
					break;
				case 70:
					writer.Write((short)number);
					writer.Write((byte)number2);
					break;
				case 71:
					writer.Write(number);
					writer.Write((int)number2);
					writer.Write((short)number3);
					writer.Write((byte)number4);
					break;
				case 72:
					for (int num15 = 0; num15 < 40; num15++)
					{
						writer.Write((short)Main.travelShop[num15]);
					}
					break;
				case 73:
					writer.Write((byte)number);
					break;
				case 74:
				{
					writer.Write((byte)Main.anglerQuest);
					bool value4 = Main.anglerWhoFinishedToday.Contains(text.ToString());
					writer.Write(value4);
					break;
				}
				case 76:
					writer.Write((byte)number);
					writer.Write(Main.player[number].anglerQuestsFinished);
					writer.Write(Main.player[number].golferScoreAccumulated);
					break;
				case 77:
					writer.Write((short)number);
					writer.Write((ushort)number2);
					writer.Write((short)number3);
					writer.Write((short)number4);
					break;
				case 78:
					writer.Write(number);
					writer.Write((int)number2);
					writer.Write((sbyte)number3);
					writer.Write((sbyte)number4);
					break;
				case 79:
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write((short)number3);
					writer.Write((short)number4);
					writer.Write((byte)number5);
					writer.Write((sbyte)number6);
					writer.Write(number7 == 1);
					break;
				case 80:
					writer.Write((byte)number);
					writer.Write((short)number2);
					break;
				case 81:
				{
					writer.Write(number2);
					writer.Write(number3);
					Color c2 = default(Color);
					c2.PackedValue = (uint)number;
					writer.WriteRGB(c2);
					writer.Write((int)number4);
					break;
				}
				case 83:
				{
					int num16 = number;
					if (num16 < 0 && num16 >= 290)
					{
						num16 = 1;
					}
					int value5 = NPC.killCount[num16];
					writer.Write((short)num16);
					writer.Write(value5);
					break;
				}
				case 84:
				{
					byte b6 = (byte)number;
					float stealth = Main.player[(int)b6].stealth;
					writer.Write(b6);
					writer.Write(stealth);
					break;
				}
				case 85:
				{
					short value6 = (short)number;
					writer.Write(value6);
					break;
				}
				case 86:
				{
					writer.Write(number);
					bool flag3 = TileEntity.ByID.ContainsKey(number);
					writer.Write(flag3);
					if (flag3)
					{
						TileEntity.Write(writer, TileEntity.ByID[number], true, true);
					}
					break;
				}
				case 87:
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write((byte)number3);
					break;
				case 88:
				{
					BitsByte bitsByte43 = (byte)number2;
					BitsByte bitsByte44 = (byte)number3;
					writer.Write((short)number);
					writer.Write(bitsByte43);
					Item item8 = Main.item[number];
					if (bitsByte43[0])
					{
						writer.Write(item8.color.PackedValue);
					}
					if (bitsByte43[1])
					{
						writer.Write((ushort)item8.damage);
					}
					if (bitsByte43[2])
					{
						writer.Write(item8.knockBack);
					}
					if (bitsByte43[3])
					{
						writer.Write((ushort)item8.useAnimation);
					}
					if (bitsByte43[4])
					{
						writer.Write((ushort)item8.useTime);
					}
					if (bitsByte43[5])
					{
						writer.Write((short)item8.shoot);
					}
					if (bitsByte43[6])
					{
						writer.Write(item8.shootSpeed);
					}
					if (bitsByte43[7])
					{
						writer.Write(bitsByte44);
						if (bitsByte44[0])
						{
							writer.Write((ushort)item8.width);
						}
						if (bitsByte44[1])
						{
							writer.Write((ushort)item8.height);
						}
						if (bitsByte44[2])
						{
							writer.Write(item8.scale);
						}
						if (bitsByte44[3])
						{
							writer.Write((short)item8.ammo);
						}
						if (bitsByte44[4])
						{
							writer.Write((short)item8.useAmmo);
						}
						if (bitsByte44[5])
						{
							writer.Write(item8.notAmmo);
						}
					}
					break;
				}
				case 89:
					writer.Write((short)number);
					writer.Write((short)number2);
					ItemIO.Send(Main.player[(int)number4].inventory[(int)number3], writer, false, false);
					writer.Write7BitEncodedInt(number5);
					break;
				case 91:
					writer.Write(number);
					writer.Write((byte)number2);
					if ((byte)number2 == 2)
					{
						writer.Write((byte)((int)number2 >> 8));
					}
					if (number2 != 255f)
					{
						writer.Write((ushort)number3);
						writer.Write((ushort)number4);
						writer.Write((byte)number5);
						if (number5 < 0)
						{
							writer.Write((short)number6);
						}
					}
					break;
				case 92:
					writer.Write((short)number);
					writer.Write((int)number2);
					writer.Write(number3);
					writer.Write(number4);
					break;
				case 95:
					writer.Write((ushort)number);
					writer.Write((byte)number2);
					break;
				case 96:
				{
					writer.Write((byte)number);
					Player player9 = Main.player[number];
					writer.Write((short)number4);
					writer.Write(number2);
					writer.Write(number3);
					writer.WriteVector2(player9.velocity);
					break;
				}
				case 97:
					writer.Write((short)number);
					break;
				case 98:
					writer.Write((short)number);
					break;
				case 99:
					writer.Write((byte)number);
					writer.WriteVector2(Main.player[number].MinionRestTargetPoint);
					break;
				case 100:
				{
					writer.Write((ushort)number);
					NPC nPC3 = Main.npc[number];
					writer.Write((short)number4);
					writer.Write(number2);
					writer.Write(number3);
					writer.WriteVector2(nPC3.velocity);
					break;
				}
				case 101:
					writer.Write((ushort)NPC.ShieldStrengthTowerSolar);
					writer.Write((ushort)NPC.ShieldStrengthTowerVortex);
					writer.Write((ushort)NPC.ShieldStrengthTowerNebula);
					writer.Write((ushort)NPC.ShieldStrengthTowerStardust);
					break;
				case 102:
					writer.Write((byte)number);
					writer.Write((ushort)number2);
					writer.Write(number3);
					writer.Write(number4);
					break;
				case 103:
					writer.Write(NPC.MaxMoonLordCountdown);
					writer.Write(NPC.MoonLordCountdown);
					break;
				case 104:
					writer.Write((byte)number);
					writer.Write((short)number2);
					writer.Write(((short)number3 < 0) ? 0f : number3);
					writer.Write((byte)number4);
					writer.Write(number5);
					writer.Write((byte)number6);
					break;
				case 105:
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write(number3 == 1f);
					break;
				case 106:
					writer.Write(new HalfVector2((float)number, number2).PackedValue);
					break;
				case 107:
					writer.Write((byte)number2);
					writer.Write((byte)number3);
					writer.Write((byte)number4);
					text.Serialize(writer);
					writer.Write((short)number5);
					break;
				case 108:
					writer.Write((short)number);
					writer.Write(number2);
					writer.Write((short)number3);
					writer.Write((short)number4);
					writer.Write((short)number5);
					writer.Write((short)number6);
					writer.Write((byte)number7);
					break;
				case 109:
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write((short)number3);
					writer.Write((short)number4);
					writer.Write((byte)number5);
					break;
				case 110:
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write((byte)number3);
					break;
				case 112:
					writer.Write((byte)number);
					writer.Write((int)number2);
					writer.Write((int)number3);
					writer.Write((byte)number4);
					writer.Write((short)number5);
					break;
				case 113:
					writer.Write((short)number);
					writer.Write((short)number2);
					break;
				case 115:
					writer.Write((byte)number);
					writer.Write((short)Main.player[number].MinionAttackTargetNPC);
					break;
				case 116:
					writer.Write(number);
					break;
				case 117:
					if (number7 == 1)
					{
						writer.Write(byte.MaxValue);
						writer.Write((byte)number);
						Player.HurtInfo args = NetMessage._currentPlayerHurtInfo;
						BitsByte pack = new BitsByte(args.PvP, args.Dodgeable, args.DustDisabled, args.SoundDisabled, false, false, false, false);
						writer.Write(pack);
						args.DamageSource.WriteSelfTo(writer);
						writer.Write((sbyte)args.CooldownCounter);
						writer.Write7BitEncodedInt(args.SourceDamage);
						writer.Write7BitEncodedInt(args.Damage);
						writer.Write((sbyte)args.HitDirection);
						writer.Write(args.Knockback);
					}
					else
					{
						writer.Write((byte)number);
						NetMessage._currentPlayerDeathReason.WriteSelfTo(writer);
						writer.Write((short)number2);
						writer.Write((byte)(number3 + 1f));
						writer.Write((byte)number4);
						writer.Write((sbyte)number5);
					}
					break;
				case 118:
					writer.Write((byte)number);
					NetMessage._currentPlayerDeathReason.WriteSelfTo(writer);
					writer.Write((short)number2);
					writer.Write((byte)(number3 + 1f));
					writer.Write((byte)number4);
					break;
				case 119:
				{
					writer.Write(number2);
					writer.Write(number3);
					Color c3 = default(Color);
					c3.PackedValue = (uint)number;
					writer.WriteRGB(c3);
					text.Serialize(writer);
					break;
				}
				case 120:
					writer.Write((byte)number);
					writer.Write((byte)number2);
					break;
				case 121:
				{
					int num17 = (int)number3;
					bool flag4 = number4 == 1f;
					if (flag4)
					{
						num17 += 8;
					}
					writer.Write((byte)number);
					writer.Write((int)number2);
					writer.Write((byte)num17);
					TEDisplayDoll tEDisplayDoll = TileEntity.ByID[(int)number2] as TEDisplayDoll;
					if (tEDisplayDoll != null)
					{
						tEDisplayDoll.WriteItem((int)number3, writer, flag4);
					}
					else
					{
						writer.Write(0);
						writer.Write(0);
					}
					break;
				}
				case 122:
					writer.Write(number);
					writer.Write((byte)number2);
					break;
				case 123:
					writer.Write((short)number);
					writer.Write((short)number2);
					ItemIO.Send(Main.player[(int)number4].inventory[(int)number3], writer, true, false);
					break;
				case 124:
				{
					int num18 = (int)number3;
					bool flag5 = number4 == 1f;
					if (flag5)
					{
						num18 += 2;
					}
					writer.Write((byte)number);
					writer.Write((int)number2);
					writer.Write((byte)num18);
					TEHatRack tEHatRack = TileEntity.ByID[(int)number2] as TEHatRack;
					if (tEHatRack != null)
					{
						tEHatRack.WriteItem((int)number3, writer, flag5);
					}
					else
					{
						writer.Write(0);
						writer.Write(0);
					}
					break;
				}
				case 125:
					writer.Write((byte)number);
					writer.Write((short)number2);
					writer.Write((short)number3);
					writer.Write((byte)number4);
					break;
				case 126:
					NetMessage._currentRevengeMarker.WriteSelfTo(writer);
					break;
				case 127:
					writer.Write(number);
					break;
				case 128:
					writer.Write((byte)number);
					writer.Write((ushort)number5);
					writer.Write((ushort)number6);
					writer.Write((ushort)number2);
					writer.Write((ushort)number3);
					break;
				case 130:
					writer.Write((ushort)number);
					writer.Write((ushort)number2);
					writer.Write((short)number3);
					break;
				case 131:
					writer.Write((ushort)number);
					writer.Write((byte)number2);
					if ((byte)number2 == 1)
					{
						writer.Write((int)number3);
						writer.Write((short)number4);
					}
					break;
				case 132:
					NetMessage._currentNetSoundInfo.WriteSelfTo(writer);
					break;
				case 133:
					writer.Write((short)number);
					writer.Write((short)number2);
					ItemIO.Send(Main.player[(int)number4].inventory[(int)number3], writer, true, false);
					break;
				case 134:
				{
					writer.Write((byte)number);
					Player player10 = Main.player[number];
					writer.Write(player10.ladyBugLuckTimeLeft);
					writer.Write(player10.torchLuck);
					writer.Write(player10.luckPotion);
					writer.Write(player10.HasGardenGnomeNearby);
					writer.Write(player10.equipmentBasedLuckBonus);
					writer.Write(player10.coinLuck);
					break;
				}
				case 135:
					writer.Write((byte)number);
					break;
				case 136:
					for (int m = 0; m < 2; m++)
					{
						for (int n = 0; n < 3; n++)
						{
							writer.Write((ushort)NPC.cavernMonsterType[m, n]);
						}
					}
					break;
				case 137:
					writer.Write((short)number);
					writer.Write((ushort)number2);
					break;
				case 139:
				{
					writer.Write((byte)number);
					bool value7 = number2 == 1f;
					writer.Write(value7);
					break;
				}
				case 140:
					writer.Write((byte)number);
					writer.Write((int)number2);
					break;
				case 141:
					writer.Write((byte)number);
					writer.Write((byte)number2);
					writer.Write(number3);
					writer.Write(number4);
					writer.Write(number5);
					writer.Write(number6);
					break;
				case 142:
				{
					writer.Write((byte)number);
					Player player11 = Main.player[number];
					player11.piggyBankProjTracker.Write(writer);
					player11.voidLensChest.Write(writer);
					break;
				}
				case 146:
					writer.Write((byte)number);
					switch (number)
					{
					case 0:
						writer.WriteVector2(new Vector2((float)((int)number2), (float)((int)number3)));
						break;
					case 1:
						writer.WriteVector2(new Vector2((float)((int)number2), (float)((int)number3)));
						writer.Write((int)number4);
						break;
					case 2:
						writer.Write((int)number2);
						break;
					}
					break;
				case 147:
					writer.Write((byte)number);
					writer.Write((byte)number2);
					NetMessage.WriteAccessoryVisibility(writer, Main.player[number].hideVisibleAccessory);
					break;
				}
				int num19 = (int)writer.BaseStream.Position;
				if (num19 > 65535)
				{
					throw new Exception("Maximum packet length exceeded. id: " + msgType.ToString() + " length: " + num19.ToString());
				}
				writer.BaseStream.Position = position;
				if (num19 > 65535)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Maximum packet length exceeded ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(num19);
					defaultInterpolatedStringHandler.AppendLiteral(" > ");
					defaultInterpolatedStringHandler.AppendFormatted<ushort>(ushort.MaxValue);
					throw new IndexOutOfRangeException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (ModNet.DetailedLogging)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
					defaultInterpolatedStringHandler.AppendLiteral("SendData ");
					defaultInterpolatedStringHandler.AppendFormatted(MessageID.GetName(msgType));
					defaultInterpolatedStringHandler.AppendLiteral("(");
					defaultInterpolatedStringHandler.AppendFormatted<int>(msgType);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					ModNet.LogSend(remoteClient, ignoreClient, defaultInterpolatedStringHandler.ToStringAndClear(), num19);
				}
				writer.Write((ushort)num19);
				writer.BaseStream.Position = (long)num19;
				if (Main.netMode == 1)
				{
					if (!Netplay.Connection.Socket.IsConnected())
					{
						goto IL_3B8E;
					}
					try
					{
						NetMessage.buffer[num].spamCount++;
						Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num19);
						Netplay.Connection.Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num19, new SocketSendCallback(Netplay.Connection.ClientWriteCallBack), null);
						goto IL_3B8E;
					}
					catch
					{
						goto IL_3B8E;
					}
				}
				if (remoteClient == -1)
				{
					if (msgType <= 23)
					{
						if (msgType != 13)
						{
							if (msgType == 20)
							{
								for (int num20 = 0; num20 < 256; num20++)
								{
									if (num20 != ignoreClient && NetMessage.buffer[num20].broadcast && Netplay.Clients[num20].IsConnected() && Netplay.Clients[num20].SectionRange((int)Math.Max(number3, number4), number, (int)number2))
									{
										try
										{
											NetMessage.buffer[num20].spamCount++;
											Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num19);
											Netplay.Clients[num20].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num19, new SocketSendCallback(Netplay.Clients[num20].ServerWriteCallBack), null);
										}
										catch
										{
										}
									}
								}
								goto IL_3B8E;
							}
							if (msgType == 23)
							{
								NPC nPC4 = Main.npc[number];
								for (int num21 = 0; num21 < 256; num21++)
								{
									if (num21 != ignoreClient && NetMessage.buffer[num21].broadcast && Netplay.Clients[num21].IsConnected())
									{
										bool flag6 = false;
										if (nPC4.boss || nPC4.netAlways || nPC4.townNPC || !nPC4.active)
										{
											flag6 = true;
										}
										else if (nPC4.netSkip <= 0)
										{
											Rectangle rect5 = Main.player[num21].getRect();
											Rectangle rect6 = nPC4.getRect();
											rect6.X -= 2500;
											rect6.Y -= 2500;
											rect6.Width += 5000;
											rect6.Height += 5000;
											if (rect5.Intersects(rect6))
											{
												flag6 = true;
											}
										}
										else
										{
											flag6 = true;
										}
										if (flag6)
										{
											try
											{
												NetMessage.buffer[num21].spamCount++;
												Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num19);
												Netplay.Clients[num21].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num19, new SocketSendCallback(Netplay.Clients[num21].ServerWriteCallBack), null);
											}
											catch
											{
											}
										}
									}
								}
								nPC4.netSkip++;
								if (nPC4.netSkip > 4)
								{
									nPC4.netSkip = 0;
									goto IL_3B8E;
								}
								goto IL_3B8E;
							}
						}
						else
						{
							for (int num22 = 0; num22 < 256; num22++)
							{
								if (num22 != ignoreClient && NetMessage.buffer[num22].broadcast && Netplay.Clients[num22].IsConnected())
								{
									try
									{
										NetMessage.buffer[num22].spamCount++;
										Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num19);
										Netplay.Clients[num22].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num19, new SocketSendCallback(Netplay.Clients[num22].ServerWriteCallBack), null);
									}
									catch
									{
									}
								}
							}
							Main.player[number].netSkip++;
							if (Main.player[number].netSkip > 2)
							{
								Main.player[number].netSkip = 0;
								goto IL_3B8E;
							}
							goto IL_3B8E;
						}
					}
					else if (msgType <= 28)
					{
						if (msgType == 27)
						{
							Projectile projectile2 = Main.projectile[number];
							for (int num23 = 0; num23 < 256; num23++)
							{
								if (num23 != ignoreClient && NetMessage.buffer[num23].broadcast && Netplay.Clients[num23].IsConnected())
								{
									bool flag7 = false;
									if (projectile2.type == 12 || Main.projPet[projectile2.type] || projectile2.aiStyle == 11 || projectile2.netImportant)
									{
										flag7 = true;
									}
									else
									{
										Rectangle rect7 = Main.player[num23].getRect();
										Rectangle rect8 = projectile2.getRect();
										rect8.X -= 5000;
										rect8.Y -= 5000;
										rect8.Width += 10000;
										rect8.Height += 10000;
										if (rect7.Intersects(rect8))
										{
											flag7 = true;
										}
									}
									if (flag7)
									{
										try
										{
											NetMessage.buffer[num23].spamCount++;
											Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num19);
											Netplay.Clients[num23].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num19, new SocketSendCallback(Netplay.Clients[num23].ServerWriteCallBack), null);
										}
										catch
										{
										}
									}
								}
							}
							goto IL_3B8E;
						}
						if (msgType == 28)
						{
							NPC nPC5 = Main.npc[number];
							for (int num24 = 0; num24 < 256; num24++)
							{
								if (num24 != ignoreClient && NetMessage.buffer[num24].broadcast && Netplay.Clients[num24].IsConnected())
								{
									bool flag8 = false;
									if (nPC5.life <= 0)
									{
										flag8 = true;
									}
									else
									{
										Rectangle rect9 = Main.player[num24].getRect();
										Rectangle rect10 = nPC5.getRect();
										rect10.X -= 3000;
										rect10.Y -= 3000;
										rect10.Width += 6000;
										rect10.Height += 6000;
										if (rect9.Intersects(rect10))
										{
											flag8 = true;
										}
									}
									if (flag8)
									{
										try
										{
											NetMessage.buffer[num24].spamCount++;
											Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num19);
											Netplay.Clients[num24].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num19, new SocketSendCallback(Netplay.Clients[num24].ServerWriteCallBack), null);
										}
										catch
										{
										}
									}
								}
							}
							goto IL_3B8E;
						}
					}
					else if (msgType == 34 || msgType == 69)
					{
						for (int num25 = 0; num25 < 256; num25++)
						{
							if (num25 != ignoreClient && NetMessage.buffer[num25].broadcast && Netplay.Clients[num25].IsConnected())
							{
								try
								{
									NetMessage.buffer[num25].spamCount++;
									Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num19);
									Netplay.Clients[num25].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num19, new SocketSendCallback(Netplay.Clients[num25].ServerWriteCallBack), null);
								}
								catch
								{
								}
							}
						}
						goto IL_3B8E;
					}
					for (int num26 = 0; num26 < 256; num26++)
					{
						if (num26 != ignoreClient && (NetMessage.buffer[num26].broadcast || (Netplay.Clients[num26].State >= 3 && msgType == 10)) && Netplay.Clients[num26].IsConnected())
						{
							try
							{
								NetMessage.buffer[num26].spamCount++;
								Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num19);
								Netplay.Clients[num26].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num19, new SocketSendCallback(Netplay.Clients[num26].ServerWriteCallBack), null);
							}
							catch
							{
							}
						}
					}
				}
				else if (Netplay.Clients[remoteClient].IsConnected())
				{
					try
					{
						NetMessage.buffer[remoteClient].spamCount++;
						Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num19);
						Netplay.Clients[remoteClient].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num19, new SocketSendCallback(Netplay.Clients[remoteClient].ServerWriteCallBack), null);
					}
					catch
					{
					}
				}
				IL_3B8E:
				if (Main.verboseNetplay)
				{
					for (int num27 = 0; num27 < num19; num27++)
					{
					}
					for (int num28 = 0; num28 < num19; num28++)
					{
						byte b7 = NetMessage.buffer[num].writeBuffer[num28];
					}
				}
				NetMessage.buffer[num].writeLocked = false;
				if (msgType == 2 && Main.netMode == 2)
				{
					Netplay.Clients[num].SetPendingTermination("Kicked");
					Netplay.Clients[num].PendingTerminationApproved = true;
				}
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0015BC4C File Offset: 0x00159E4C
		private static void WriteAccessoryVisibility(BinaryWriter writer, bool[] hideVisibleAccessory)
		{
			ushort num = 0;
			for (int i = 0; i < hideVisibleAccessory.Length; i++)
			{
				if (hideVisibleAccessory[i])
				{
					num |= (ushort)(1 << i);
				}
			}
			writer.Write(num);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0015BC80 File Offset: 0x00159E80
		public static void CompressTileBlock(int xStart, int yStart, short width, short height, Stream stream)
		{
			using (DeflateStream output = new DeflateStream(stream, 0, true))
			{
				BinaryWriter binaryWriter = new BinaryWriter(output);
				binaryWriter.Write(xStart);
				binaryWriter.Write(yStart);
				binaryWriter.Write(width);
				binaryWriter.Write(height);
				NetMessage.CompressTileBlock_Inner(binaryWriter, xStart, yStart, (int)width, (int)height);
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0015BCE0 File Offset: 0x00159EE0
		public unsafe static void CompressTileBlock_Inner(BinaryWriter writer, int xStart, int yStart, int width, int height)
		{
			short num = 0;
			short num2 = 0;
			short num3 = 0;
			short num4 = 0;
			int num5 = 0;
			int num6 = 0;
			byte b = 0;
			byte[] array = new byte[16];
			Tile? tile = null;
			for (int i = yStart; i < yStart + height; i++)
			{
				for (int j = xStart; j < xStart + width; j++)
				{
					Tile tile2 = Main.tile[j, i];
					if (tile != null && tile2.isTheSameAs(tile.Value) && TileID.Sets.AllowsSaveCompressionBatching[(int)(*tile2.type)])
					{
						num4 += 1;
					}
					else
					{
						if (tile != null)
						{
							if (num4 > 0)
							{
								array[num5] = (byte)(num4 & 255);
								num5++;
								if (num4 > 255)
								{
									b |= 128;
									array[num5] = (byte)(((int)num4 & 65280) >> 8);
									num5++;
								}
								else
								{
									b |= 64;
								}
							}
							array[num6] = b;
							writer.Write(array, num6, num5 - num6);
							num4 = 0;
						}
						num5 = 4;
						byte b4;
						byte b3;
						byte b2 = b = (b3 = (b4 = 0));
						if (tile2.active())
						{
							b |= 2;
							array[num5] = (byte)(*tile2.type);
							num5++;
							if (*tile2.type > 255)
							{
								array[num5] = (byte)(*tile2.type >> 8);
								num5++;
								b |= 32;
							}
							if (TileID.Sets.BasicChest[(int)(*tile2.type)] && *tile2.frameX % 36 == 0 && *tile2.frameY % 36 == 0)
							{
								short num7 = (short)Chest.FindChest(j, i);
								if (num7 != -1)
								{
									NetMessage._compressChestList[(int)num] = num7;
									num += 1;
								}
							}
							if (TileID.Sets.BasicDresser[(int)(*tile2.type)] && *tile2.frameX % 54 == 0 && *tile2.frameY % 36 == 0)
							{
								short num8 = (short)Chest.FindChest(j, i);
								if (num8 != -1)
								{
									NetMessage._compressChestList[(int)num] = num8;
									num += 1;
								}
							}
							if (*tile2.type == 85 && *tile2.frameX % 36 == 0 && *tile2.frameY % 36 == 0)
							{
								short num9 = (short)Sign.ReadSign(j, i, true);
								if (num9 != -1)
								{
									short[] compressSignList = NetMessage._compressSignList;
									short num21 = num2;
									num2 = num21 + 1;
									compressSignList[(int)num21] = num9;
								}
							}
							if (*tile2.type == 55 && *tile2.frameX % 36 == 0 && *tile2.frameY % 36 == 0)
							{
								short num10 = (short)Sign.ReadSign(j, i, true);
								if (num10 != -1)
								{
									short[] compressSignList2 = NetMessage._compressSignList;
									short num22 = num2;
									num2 = num22 + 1;
									compressSignList2[(int)num22] = num10;
								}
							}
							if ((*tile2.type == 425 || (*tile2.type >= TileID.Count && Main.tileSign[(int)(*tile2.type)])) && *tile2.frameX % 36 == 0 && *tile2.frameY % 36 == 0)
							{
								short num11 = (short)Sign.ReadSign(j, i, true);
								if (num11 != -1)
								{
									short[] compressSignList3 = NetMessage._compressSignList;
									short num23 = num2;
									num2 = num23 + 1;
									compressSignList3[(int)num23] = num11;
								}
							}
							if (*tile2.type == 573 && *tile2.frameX % 36 == 0 && *tile2.frameY % 36 == 0)
							{
								short num12 = (short)Sign.ReadSign(j, i, true);
								if (num12 != -1)
								{
									short[] compressSignList4 = NetMessage._compressSignList;
									short num24 = num2;
									num2 = num24 + 1;
									compressSignList4[(int)num24] = num12;
								}
							}
							if (*tile2.type == 378 && *tile2.frameX % 36 == 0 && *tile2.frameY == 0)
							{
								int num13 = TETrainingDummy.Find(j, i);
								if (num13 != -1)
								{
									short[] compressEntities = NetMessage._compressEntities;
									short num25 = num3;
									num3 = num25 + 1;
									compressEntities[(int)num25] = (short)num13;
								}
							}
							if (*tile2.type == 395 && *tile2.frameX % 36 == 0 && *tile2.frameY == 0)
							{
								int num14 = TEItemFrame.Find(j, i);
								if (num14 != -1)
								{
									short[] compressEntities2 = NetMessage._compressEntities;
									short num26 = num3;
									num3 = num26 + 1;
									compressEntities2[(int)num26] = (short)num14;
								}
							}
							if (*tile2.type == 520 && *tile2.frameX % 18 == 0 && *tile2.frameY == 0)
							{
								int num15 = TEFoodPlatter.Find(j, i);
								if (num15 != -1)
								{
									short[] compressEntities3 = NetMessage._compressEntities;
									short num27 = num3;
									num3 = num27 + 1;
									compressEntities3[(int)num27] = (short)num15;
								}
							}
							if (*tile2.type == 471 && *tile2.frameX % 54 == 0 && *tile2.frameY == 0)
							{
								int num16 = TEWeaponsRack.Find(j, i);
								if (num16 != -1)
								{
									short[] compressEntities4 = NetMessage._compressEntities;
									short num28 = num3;
									num3 = num28 + 1;
									compressEntities4[(int)num28] = (short)num16;
								}
							}
							if (*tile2.type == 470 && *tile2.frameX % 36 == 0 && *tile2.frameY == 0)
							{
								int num17 = TEDisplayDoll.Find(j, i);
								if (num17 != -1)
								{
									short[] compressEntities5 = NetMessage._compressEntities;
									short num29 = num3;
									num3 = num29 + 1;
									compressEntities5[(int)num29] = (short)num17;
								}
							}
							if (*tile2.type == 475 && *tile2.frameX % 54 == 0 && *tile2.frameY == 0)
							{
								int num18 = TEHatRack.Find(j, i);
								if (num18 != -1)
								{
									short[] compressEntities6 = NetMessage._compressEntities;
									short num30 = num3;
									num3 = num30 + 1;
									compressEntities6[(int)num30] = (short)num18;
								}
							}
							if (*tile2.type == 597 && *tile2.frameX % 54 == 0 && *tile2.frameY % 72 == 0)
							{
								int num19 = TETeleportationPylon.Find(j, i);
								if (num19 != -1)
								{
									short[] compressEntities7 = NetMessage._compressEntities;
									short num31 = num3;
									num3 = num31 + 1;
									compressEntities7[(int)num31] = (short)num19;
								}
							}
							if (Main.tileFrameImportant[(int)(*tile2.type)])
							{
								array[num5] = (byte)(*tile2.frameX & 255);
								num5++;
								array[num5] = (byte)(((int)(*tile2.frameX) & 65280) >> 8);
								num5++;
								array[num5] = (byte)(*tile2.frameY & 255);
								num5++;
								array[num5] = (byte)(((int)(*tile2.frameY) & 65280) >> 8);
								num5++;
							}
							if (tile2.color() != 0)
							{
								b3 |= 8;
								array[num5] = tile2.color();
								num5++;
							}
						}
						if (*tile2.wall != 0)
						{
							b |= 4;
							array[num5] = (byte)(*tile2.wall);
							num5++;
							if (tile2.wallColor() != 0)
							{
								b3 |= 16;
								array[num5] = tile2.wallColor();
								num5++;
							}
						}
						if (*tile2.liquid != 0)
						{
							if (!tile2.shimmer())
							{
								b = (tile2.lava() ? (b | 16) : ((!tile2.honey()) ? (b | 8) : (b | 24)));
							}
							else
							{
								b3 |= 128;
								b |= 8;
							}
							array[num5] = *tile2.liquid;
							num5++;
						}
						if (tile2.wire())
						{
							b2 |= 2;
						}
						if (tile2.wire2())
						{
							b2 |= 4;
						}
						if (tile2.wire3())
						{
							b2 |= 8;
						}
						int num20 = tile2.halfBrick() ? 16 : ((tile2.slope() != 0) ? ((int)(tile2.slope() + 1) << 4) : 0);
						b2 |= (byte)num20;
						if (tile2.actuator())
						{
							b3 |= 2;
						}
						if (tile2.inActive())
						{
							b3 |= 4;
						}
						if (tile2.wire4())
						{
							b3 |= 32;
						}
						if (*tile2.wall > 255)
						{
							array[num5] = (byte)(*tile2.wall >> 8);
							num5++;
							b3 |= 64;
						}
						if (tile2.invisibleBlock())
						{
							b4 |= 2;
						}
						if (tile2.invisibleWall())
						{
							b4 |= 4;
						}
						if (tile2.fullbrightBlock())
						{
							b4 |= 8;
						}
						if (tile2.fullbrightWall())
						{
							b4 |= 16;
						}
						num6 = 3;
						if (b4 != 0)
						{
							b3 |= 1;
							array[num6] = b4;
							num6--;
						}
						if (b3 != 0)
						{
							b2 |= 1;
							array[num6] = b3;
							num6--;
						}
						if (b2 != 0)
						{
							b |= 1;
							array[num6] = b2;
							num6--;
						}
						tile = new Tile?(tile2);
					}
				}
			}
			if (num4 > 0)
			{
				array[num5] = (byte)(num4 & 255);
				num5++;
				if (num4 > 255)
				{
					b |= 128;
					array[num5] = (byte)(((int)num4 & 65280) >> 8);
					num5++;
				}
				else
				{
					b |= 64;
				}
			}
			array[num6] = b;
			writer.Write(array, num6, num5 - num6);
			writer.Write(num);
			for (int k = 0; k < (int)num; k++)
			{
				Chest chest = Main.chest[(int)NetMessage._compressChestList[k]];
				writer.Write(NetMessage._compressChestList[k]);
				writer.Write((short)chest.x);
				writer.Write((short)chest.y);
				writer.Write(chest.name);
			}
			writer.Write(num2);
			for (int l = 0; l < (int)num2; l++)
			{
				Sign sign = Main.sign[(int)NetMessage._compressSignList[l]];
				writer.Write(NetMessage._compressSignList[l]);
				writer.Write((short)sign.x);
				writer.Write((short)sign.y);
				writer.Write(sign.text);
			}
			foreach (KeyValuePair<Point16, TileEntity> item in TileEntity.ByPosition)
			{
				Point16 pos = item.Key;
				if ((int)pos.X >= xStart && (int)pos.X < xStart + width && (int)pos.Y >= yStart && (int)pos.Y < yStart + height && (int)item.Value.type >= TileEntitiesManager.VanillaTypeCount)
				{
					short[] compressEntities8 = NetMessage._compressEntities;
					short num32 = num3;
					num3 = num32 + 1;
					compressEntities8[(int)num32] = (short)item.Value.ID;
				}
			}
			writer.Write(num3);
			for (int m = 0; m < (int)num3; m++)
			{
				TileEntity.Write(writer, TileEntity.ByID[(int)NetMessage._compressEntities[m]], true, false);
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0015C690 File Offset: 0x0015A890
		public static void DecompressTileBlock(Stream stream)
		{
			using (DeflateStream input = new DeflateStream(stream, 1, true))
			{
				BinaryReader binaryReader = new BinaryReader(input);
				NetMessage.DecompressTileBlock_Inner(binaryReader, binaryReader.ReadInt32(), binaryReader.ReadInt32(), (int)binaryReader.ReadInt16(), (int)binaryReader.ReadInt16());
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0015C6E8 File Offset: 0x0015A8E8
		public unsafe static void DecompressTileBlock_Inner(BinaryReader reader, int xStart, int yStart, int width, int height)
		{
			Tile tile = default(Tile);
			int num = 0;
			for (int i = yStart; i < yStart + height; i++)
			{
				for (int j = xStart; j < xStart + width; j++)
				{
					if (num != 0)
					{
						num--;
						Main.tile[j, i].CopyFrom(tile);
					}
					else
					{
						byte b4;
						byte b3;
						byte b2 = b3 = (b4 = 0);
						tile = Main.tile[j, i];
						if (tile == null)
						{
							tile = default(Tile);
							Main.tile[j, i] = tile;
						}
						else
						{
							tile.ClearEverything();
						}
						byte b5 = reader.ReadByte();
						bool flag = false;
						if ((b5 & 1) == 1)
						{
							flag = true;
							b3 = reader.ReadByte();
						}
						bool flag2 = false;
						if (flag && (b3 & 1) == 1)
						{
							flag2 = true;
							b2 = reader.ReadByte();
						}
						if (flag2 && (b2 & 1) == 1)
						{
							b4 = reader.ReadByte();
						}
						bool flag3 = tile.active();
						byte b6;
						if ((b5 & 2) == 2)
						{
							tile.active(true);
							ushort type = *tile.type;
							int num2;
							if ((b5 & 32) == 32)
							{
								b6 = reader.ReadByte();
								num2 = (int)reader.ReadByte();
								num2 = (num2 << 8 | (int)b6);
							}
							else
							{
								num2 = (int)reader.ReadByte();
							}
							*tile.type = (ushort)num2;
							if (Main.tileFrameImportant[num2])
							{
								*tile.frameX = reader.ReadInt16();
								*tile.frameY = reader.ReadInt16();
							}
							else if (!flag3 || *tile.type != type)
							{
								*tile.frameX = -1;
								*tile.frameY = -1;
							}
							if ((b2 & 8) == 8)
							{
								tile.color(reader.ReadByte());
							}
						}
						if ((b5 & 4) == 4)
						{
							*tile.wall = (ushort)reader.ReadByte();
							if ((b2 & 16) == 16)
							{
								tile.wallColor(reader.ReadByte());
							}
						}
						b6 = (byte)((b5 & 24) >> 3);
						if (b6 != 0)
						{
							*tile.liquid = reader.ReadByte();
							if ((b2 & 128) == 128)
							{
								tile.shimmer(true);
							}
							else if (b6 > 1)
							{
								if (b6 == 2)
								{
									tile.lava(true);
								}
								else
								{
									tile.honey(true);
								}
							}
						}
						if (b3 > 1)
						{
							if ((b3 & 2) == 2)
							{
								tile.wire(true);
							}
							if ((b3 & 4) == 4)
							{
								tile.wire2(true);
							}
							if ((b3 & 8) == 8)
							{
								tile.wire3(true);
							}
							b6 = (byte)((b3 & 112) >> 4);
							if (b6 != 0 && Main.tileSolid[(int)(*tile.type)])
							{
								if (b6 == 1)
								{
									tile.halfBrick(true);
								}
								else
								{
									tile.slope(b6 - 1);
								}
							}
						}
						if (b2 > 1)
						{
							if ((b2 & 2) == 2)
							{
								tile.actuator(true);
							}
							if ((b2 & 4) == 4)
							{
								tile.inActive(true);
							}
							if ((b2 & 32) == 32)
							{
								tile.wire4(true);
							}
							if ((b2 & 64) == 64)
							{
								b6 = reader.ReadByte();
								*tile.wall = (ushort)((int)b6 << 8 | (int)(*tile.wall));
							}
						}
						if (b4 > 1)
						{
							if ((b4 & 2) == 2)
							{
								tile.invisibleBlock(true);
							}
							if ((b4 & 4) == 4)
							{
								tile.invisibleWall(true);
							}
							if ((b4 & 8) == 8)
							{
								tile.fullbrightBlock(true);
							}
							if ((b4 & 16) == 16)
							{
								tile.fullbrightWall(true);
							}
						}
						byte b7 = (byte)((b5 & 192) >> 6);
						if (b7 != 0)
						{
							if (b7 != 1)
							{
								num = (int)reader.ReadInt16();
							}
							else
							{
								num = (int)reader.ReadByte();
							}
						}
						else
						{
							num = 0;
						}
					}
				}
			}
			short num3 = reader.ReadInt16();
			for (int k = 0; k < (int)num3; k++)
			{
				short num4 = reader.ReadInt16();
				short x = reader.ReadInt16();
				short y = reader.ReadInt16();
				string name = reader.ReadString();
				if (num4 >= 0 && num4 < 8000)
				{
					if (Main.chest[(int)num4] == null)
					{
						Main.chest[(int)num4] = new Chest(false);
					}
					Main.chest[(int)num4].name = name;
					Main.chest[(int)num4].x = (int)x;
					Main.chest[(int)num4].y = (int)y;
				}
			}
			num3 = reader.ReadInt16();
			for (int l = 0; l < (int)num3; l++)
			{
				short num5 = reader.ReadInt16();
				short x2 = reader.ReadInt16();
				short y2 = reader.ReadInt16();
				string text = reader.ReadString();
				if (num5 >= 0 && num5 < 1000)
				{
					if (Main.sign[(int)num5] == null)
					{
						Main.sign[(int)num5] = new Sign();
					}
					Main.sign[(int)num5].text = text;
					Main.sign[(int)num5].x = (int)x2;
					Main.sign[(int)num5].y = (int)y2;
				}
			}
			num3 = reader.ReadInt16();
			for (int m = 0; m < (int)num3; m++)
			{
				TileEntity tileEntity = TileEntity.Read(reader, true, false);
				TileEntity.ByID[tileEntity.ID] = tileEntity;
				TileEntity.ByPosition[tileEntity.Position] = tileEntity;
			}
			Main.sectionManager.SetTilesLoaded(xStart, yStart, xStart + width - 1, yStart + height - 1);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0015CBB4 File Offset: 0x0015ADB4
		public static void ReceiveBytes(byte[] bytes, int streamLength, int i = 256)
		{
			if (ModNet.DetailedLogging)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
				defaultInterpolatedStringHandler.AppendLiteral("recv ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(streamLength);
				ModNet.Debug(i, defaultInterpolatedStringHandler.ToStringAndClear());
			}
			DateTime lockStart = DateTime.Now;
			MessageBuffer obj = NetMessage.buffer[i];
			lock (obj)
			{
				bool firstWait = true;
				while (NetMessage.buffer[i].totalData + streamLength > NetMessage.buffer[i].readBuffer.Length)
				{
					if (firstWait)
					{
						if (ModNet.DetailedLogging)
						{
							ModNet.Debug(i, "waiting for space in readBuffer");
						}
						firstWait = false;
					}
					Monitor.Exit(NetMessage.buffer[i]);
					Thread.Yield();
					Monitor.Enter(NetMessage.buffer[i]);
				}
				double timeToAcquireLock = (DateTime.Now - lockStart).TotalMilliseconds;
				if (timeToAcquireLock > 1.0 && ModNet.DetailedLogging)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 1);
					defaultInterpolatedStringHandler.AppendLiteral("buffer lock contended for ");
					defaultInterpolatedStringHandler.AppendFormatted<double>(timeToAcquireLock, "0.0");
					defaultInterpolatedStringHandler.AppendLiteral("ms");
					ModNet.Debug(i, defaultInterpolatedStringHandler.ToStringAndClear());
				}
				try
				{
					Buffer.BlockCopy(bytes, 0, NetMessage.buffer[i].readBuffer, NetMessage.buffer[i].totalData, streamLength);
					NetMessage.buffer[i].totalData += streamLength;
					NetMessage.buffer[i].checkBytes = true;
				}
				catch (Exception e)
				{
					if (Main.netMode == 1)
					{
						Main.menuMode = 15;
						Main.statusText = Language.GetTextValue("Error.BadHeaderBufferOverflow");
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 4);
						defaultInterpolatedStringHandler.AppendLiteral("BlockCopy bounds error: srclen=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(bytes.Length);
						defaultInterpolatedStringHandler.AppendLiteral(", dstlen=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(NetMessage.buffer[i].readBuffer.Length);
						defaultInterpolatedStringHandler.AppendLiteral(", dstoffset=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(NetMessage.buffer[i].totalData);
						defaultInterpolatedStringHandler.AppendLiteral(", num=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(streamLength);
						ModNet.Error(defaultInterpolatedStringHandler.ToStringAndClear(), e);
						Netplay.Disconnect = true;
					}
					else
					{
						Netplay.Clients[i].SetPendingTermination("Exception in ReceiveBytes " + e.Message);
					}
				}
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0015CE1C File Offset: 0x0015B01C
		public static void CheckBytes(int bufferIndex = 256)
		{
			MessageBuffer obj = NetMessage.buffer[bufferIndex];
			lock (obj)
			{
				if (Main.dedServ && Netplay.Clients[bufferIndex].PendingTermination)
				{
					Netplay.Clients[bufferIndex].PendingTerminationApproved = true;
					NetMessage.buffer[bufferIndex].checkBytes = false;
				}
				else
				{
					int num = 0;
					int num2 = NetMessage.buffer[bufferIndex].totalData;
					int msgId = -1;
					try
					{
						if (num2 > 0 && ModNet.DetailedLogging)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
							defaultInterpolatedStringHandler.AppendLiteral("check ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
							ModNet.Debug(bufferIndex, defaultInterpolatedStringHandler.ToStringAndClear());
						}
						while (num2 >= 2)
						{
							int num3 = (int)BitConverter.ToUInt16(NetMessage.buffer[bufferIndex].readBuffer, num);
							if (num2 < num3)
							{
								break;
							}
							long position = NetMessage.buffer[bufferIndex].reader.BaseStream.Position;
							NetMessage.buffer[bufferIndex].GetData(num + 2, num3 - 2, out msgId);
							if (Main.dedServ && Netplay.Clients[bufferIndex].PendingTermination)
							{
								Netplay.Clients[bufferIndex].PendingTerminationApproved = true;
								NetMessage.buffer[bufferIndex].checkBytes = false;
								break;
							}
							NetMessage.buffer[bufferIndex].reader.BaseStream.Position = position + (long)num3;
							num2 -= num3;
							num += num3;
						}
					}
					catch (Exception e)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
						defaultInterpolatedStringHandler.AppendFormatted(MessageID.GetName(msgId));
						defaultInterpolatedStringHandler.AppendLiteral("(");
						defaultInterpolatedStringHandler.AppendFormatted<int>(msgId);
						defaultInterpolatedStringHandler.AppendLiteral(")");
						ModNet.Error(bufferIndex, defaultInterpolatedStringHandler.ToStringAndClear(), e);
						num2 = 0;
						num = 0;
					}
					if (num2 != NetMessage.buffer[bufferIndex].totalData)
					{
						for (int i = 0; i < num2; i++)
						{
							NetMessage.buffer[bufferIndex].readBuffer[i] = NetMessage.buffer[bufferIndex].readBuffer[i + num];
						}
						NetMessage.buffer[bufferIndex].totalData = num2;
						if (num2 > 0 && ModNet.DetailedLogging)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
							defaultInterpolatedStringHandler.AppendLiteral("partial ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
							ModNet.Debug(bufferIndex, defaultInterpolatedStringHandler.ToStringAndClear());
						}
					}
					NetMessage.buffer[bufferIndex].checkBytes = false;
				}
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0015D088 File Offset: 0x0015B288
		public static void BootPlayer(int plr, NetworkText msg)
		{
			NetMessage.SendData(2, plr, -1, msg, 0, 0f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0015D0B4 File Offset: 0x0015B2B4
		public static void SendObjectPlacement(int whoAmi, int x, int y, int type, int style, int alternative, int random, int direction)
		{
			int remoteClient;
			int ignoreClient;
			if (Main.netMode == 2)
			{
				remoteClient = -1;
				ignoreClient = whoAmi;
			}
			else
			{
				remoteClient = whoAmi;
				ignoreClient = -1;
			}
			NetMessage.SendData(79, remoteClient, ignoreClient, null, x, (float)y, (float)type, (float)style, alternative, random, direction);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0015D0EC File Offset: 0x0015B2EC
		public static void SendTemporaryAnimation(int whoAmi, int animationType, int tileType, int xCoord, int yCoord)
		{
			if (Main.netMode == 2)
			{
				NetMessage.SendData(77, whoAmi, -1, null, animationType, (float)tileType, (float)xCoord, (float)yCoord, 0, 0, 0);
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0015D118 File Offset: 0x0015B318
		internal static void SendPlayerHurt(int playerTargetIndex, PlayerDeathReason reason, int damage, int direction, bool critical, bool pvp, int hitContext, int remoteClient = -1, int ignoreClient = -1)
		{
			if (!pvp)
			{
				throw new ArgumentException("SendPlayerHurt is legacy, for pvp usage only. Call Player.Hurt with quiet: false, or use the Player.HurtInfo overload");
			}
			NetMessage._currentPlayerDeathReason = reason;
			BitsByte bitsByte = 0;
			bitsByte[0] = critical;
			bitsByte[1] = pvp;
			NetMessage.SendData(117, remoteClient, ignoreClient, null, playerTargetIndex, (float)damage, (float)direction, (float)bitsByte, hitContext, 0, 0);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0015D174 File Offset: 0x0015B374
		public static void SendPlayerHurt(int playerTargetIndex, Player.HurtInfo info, int ignoreClient = -1)
		{
			NetMessage._currentPlayerHurtInfo = info;
			NetMessage.SendData(117, -1, ignoreClient, null, playerTargetIndex, 0f, 0f, 0f, 0, 0, 1);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0015D1A4 File Offset: 0x0015B3A4
		public static void SendPlayerDeath(int playerTargetIndex, PlayerDeathReason reason, int damage, int direction, bool pvp, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentPlayerDeathReason = reason;
			BitsByte bitsByte = 0;
			bitsByte[0] = pvp;
			NetMessage.SendData(118, remoteClient, ignoreClient, null, playerTargetIndex, (float)damage, (float)direction, (float)bitsByte, 0, 0, 0);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0015D1E4 File Offset: 0x0015B3E4
		public static void PlayNetSound(NetMessage.NetSoundInfo info, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentNetSoundInfo = info;
			NetMessage.SendData(132, remoteClient, ignoreClient, null, 0, 0f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0015D218 File Offset: 0x0015B418
		public static void SendCoinLossRevengeMarker(CoinLossRevengeSystem.RevengeMarker marker, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentRevengeMarker = marker;
			NetMessage.SendData(126, remoteClient, ignoreClient, null, 0, 0f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0015D248 File Offset: 0x0015B448
		internal static void SetLegacyStrike(NPC.HitInfo hit)
		{
			NetMessage._lastLegacyStrike = hit;
		}

		/// <summary>
		/// Use this to synchronize damage strikes against NPCs!
		/// </summary>
		// Token: 0x06000703 RID: 1795 RVA: 0x0015D250 File Offset: 0x0015B450
		public static void SendStrikeNPC(NPC npc, in NPC.HitInfo hit, int ignoreClient = -1)
		{
			NetMessage._currentStrike = hit;
			NetMessage.SendData(28, -1, ignoreClient, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 1);
		}

		/// <SharedSummary>
		/// Sends all the tiles in a rectangle range to sync them. The range is defined by parameters explained below.
		/// <para /> <paramref name="whoAmi" /> should usually be -1. <paramref name="changeType" /> can optionally be set to one of the <see cref="T:Terraria.ID.TileChangeType" /> to automatically play the corresponding liquid sound on the receiving clients.
		/// <para /> If called on a client, the server will relay the changed tiles to other clients when it is received.
		/// <para /> Use this method when manually adjusting <see cref="F:Terraria.Main.tile" />. Uses such as wiring, right clicking on tiles, tiles randomly updating, and projectiles modifying tiles are all exhibited in <see href="https://github.com/search?q=repo%3AtModLoader%2FtModLoader+SendTileSquare+path%3AExampleMod&amp;type=Code">ExampleMod</see>. <para />
		/// </SharedSummary>
		/// <summary>
		/// <inheritdoc cref="M:Terraria.NetMessage.SendTileSquare(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,Terraria.ID.TileChangeType)" path="/SharedSummary/node()" />
		/// <b>Range: </b>In this particular overload, <paramref name="tileX" /> and <paramref name="tileY" /> denote the top left corner of tiles to sync and <paramref name="xSize" /> and <paramref name="ySize" /> denote the width and height of the rectangle.
		/// </summary>
		// Token: 0x06000704 RID: 1796 RVA: 0x0015D28C File Offset: 0x0015B48C
		public static void SendTileSquare(int whoAmi, int tileX, int tileY, int xSize, int ySize, TileChangeType changeType = TileChangeType.None)
		{
			NetMessage.SendData(20, whoAmi, -1, null, tileX, (float)tileY, (float)xSize, (float)ySize, (int)changeType, 0, 0);
			WorldGen.RangeFrame(tileX, tileY, xSize, ySize);
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.NetMessage.SendTileSquare(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,Terraria.ID.TileChangeType)" path="/SharedSummary/node()" />
		/// <b>Range: </b>In this particular overload, <paramref name="tileX" /> and <paramref name="tileY" /> denote the center of a square range of tiles to sync and <paramref name="centeredSquareSize" /> denotes the width/height of the square to send. Odd <paramref name="centeredSquareSize" /> values result in a square centered directly on <paramref name="tileX" /> and <paramref name="tileY" />, while even values will result in the square being 1 tile longer to the right of and below of the coordinates.
		/// <para /> The <see cref="M:Terraria.NetMessage.SendTileSquare(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,Terraria.ID.TileChangeType)" /> overload is usually preferred to this because it sends the exact range of tiles desired and visualizing how the square range fits over the desired range of tiles can be hard to visualize and program correctly. 
		/// </summary>
		// Token: 0x06000705 RID: 1797 RVA: 0x0015D2BC File Offset: 0x0015B4BC
		public static void SendTileSquare(int whoAmi, int tileX, int tileY, int centeredSquareSize, TileChangeType changeType = TileChangeType.None)
		{
			int num = (centeredSquareSize - 1) / 2;
			NetMessage.SendTileSquare(whoAmi, tileX - num, tileY - num, centeredSquareSize, centeredSquareSize, changeType);
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.NetMessage.SendTileSquare(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,Terraria.ID.TileChangeType)" path="/SharedSummary/node()" />
		/// <b>Range: </b>In this particular overload, <paramref name="tileX" /> and <paramref name="tileY" /> denote a single tile to sync. If sending multiple tiles use the other overloads instead.
		/// </summary>
		// Token: 0x06000706 RID: 1798 RVA: 0x0015D2E0 File Offset: 0x0015B4E0
		public static void SendTileSquare(int whoAmi, int tileX, int tileY, TileChangeType changeType = TileChangeType.None)
		{
			int num = 1;
			int num2 = (num - 1) / 2;
			NetMessage.SendTileSquare(whoAmi, tileX - num2, tileY - num2, num, num, changeType);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0015D304 File Offset: 0x0015B504
		public static void SendTravelShop(int remoteClient)
		{
			if (Main.netMode == 2)
			{
				NetMessage.SendData(72, remoteClient, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0015D338 File Offset: 0x0015B538
		public static void SendAnglerQuest(int remoteClient)
		{
			if (Main.netMode != 2)
			{
				return;
			}
			if (remoteClient == -1)
			{
				for (int i = 0; i < 255; i++)
				{
					if (Netplay.Clients[i].State == 10)
					{
						NetMessage.SendData(74, i, -1, NetworkText.FromLiteral(Main.player[i].name), Main.anglerQuest, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				return;
			}
			if (Netplay.Clients[remoteClient].State == 10)
			{
				NetMessage.SendData(74, remoteClient, -1, NetworkText.FromLiteral(Main.player[remoteClient].name), Main.anglerQuest, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0015D3E8 File Offset: 0x0015B5E8
		public static void SendSection(int whoAmi, int sectionX, int sectionY)
		{
			if (Main.netMode != 2)
			{
				return;
			}
			try
			{
				if (sectionX >= 0 && sectionY >= 0 && sectionX < Main.maxSectionsX && sectionY < Main.maxSectionsY && !Netplay.Clients[whoAmi].TileSections[sectionX, sectionY])
				{
					Netplay.Clients[whoAmi].TileSections[sectionX, sectionY] = true;
					int number = sectionX * 200;
					int num = sectionY * 150;
					int num2 = 150;
					for (int i = num; i < num + 150; i += num2)
					{
						NetMessage.SendData(10, whoAmi, -1, null, number, (float)i, 200f, (float)num2, 0, 0, 0);
					}
					for (int j = 0; j < 200; j++)
					{
						if (Main.npc[j].active && Main.npc[j].townNPC)
						{
							int sectionX2 = Netplay.GetSectionX((int)(Main.npc[j].position.X / 16f));
							int sectionY2 = Netplay.GetSectionY((int)(Main.npc[j].position.Y / 16f));
							if (sectionX2 == sectionX && sectionY2 == sectionY)
							{
								NetMessage.SendData(23, whoAmi, -1, null, j, 0f, 0f, 0f, 0, 0, 0);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0015D544 File Offset: 0x0015B744
		public static void greetPlayer(int plr)
		{
			if (Main.motd == "")
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromFormattable("{0} {1}!", new object[]
				{
					Lang.mp[18].ToNetworkText(),
					Main.worldName
				}), new Color(255, 240, 20), plr);
			}
			else
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(Main.motd), new Color(255, 240, 20), plr);
			}
			string text = "";
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					text = ((!(text == "")) ? (text + ", " + Main.player[i].name) : (text + Main.player[i].name));
				}
			}
			ChatHelper.SendChatMessageToClient(NetworkText.FromKey("Game.JoinGreeting", new object[]
			{
				text
			}), new Color(255, 240, 20), plr);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0015D64C File Offset: 0x0015B84C
		public static void sendWater(int x, int y)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendData(48, -1, -1, null, x, (float)y, 0f, 0f, 0, 0, 0);
				return;
			}
			for (int i = 0; i < 256; i++)
			{
				if ((NetMessage.buffer[i].broadcast || Netplay.Clients[i].State >= 3) && Netplay.Clients[i].IsConnected())
				{
					int num = x / 200;
					int num2 = y / 150;
					if (Netplay.Clients[i].TileSections[num, num2])
					{
						NetMessage.SendData(48, i, -1, null, x, (float)y, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0015D6F6 File Offset: 0x0015B8F6
		public static void SyncDisconnectedPlayer(int plr)
		{
			NetMessage.SyncOnePlayer(plr, -1, plr);
			NetMessage.EnsureLocalPlayerIsPresent();
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0015D708 File Offset: 0x0015B908
		public static void SyncConnectedPlayer(int plr)
		{
			NetMessage.SyncOnePlayer(plr, -1, plr);
			for (int i = 0; i < 255; i++)
			{
				if (plr != i && Main.player[i].active)
				{
					NetMessage.SyncOnePlayer(i, plr, -1);
				}
			}
			NetMessage.SendNPCHousesAndTravelShop(plr);
			NetMessage.SendAnglerQuest(plr);
			CreditsRollEvent.SendCreditsRollRemainingTimeToPlayer(plr);
			NPC.RevengeManager.SendAllMarkersToPlayer(plr);
			NetMessage.EnsureLocalPlayerIsPresent();
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0015D76C File Offset: 0x0015B96C
		private static void SendNPCHousesAndTravelShop(int plr)
		{
			bool flag = false;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.active)
				{
					bool flag2 = nPC.townNPC && NPC.TypeToDefaultHeadIndex(nPC.type) > 0;
					if (nPC.aiStyle == 7)
					{
						flag2 = true;
					}
					if (flag2)
					{
						if (!flag && nPC.type == 368)
						{
							flag = true;
						}
						byte householdStatus = WorldGen.TownManager.GetHouseholdStatus(nPC);
						NetMessage.SendData(60, plr, -1, null, i, (float)nPC.homeTileX, (float)nPC.homeTileY, (float)householdStatus, 0, 0, 0);
					}
				}
			}
			if (flag)
			{
				NetMessage.SendTravelShop(plr);
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0015D810 File Offset: 0x0015BA10
		private static void EnsureLocalPlayerIsPresent()
		{
			if (!Main.autoShutdown)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < 255; i++)
			{
				if (NetMessage.DoesPlayerSlotCountAsAHost(i))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Logging.ServerConsoleLine(Language.GetTextValue("Net.ServerAutoShutdown"));
				Netplay.Disconnect = true;
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0015D85B File Offset: 0x0015BA5B
		public static bool DoesPlayerSlotCountAsAHost(int plr)
		{
			return Netplay.Clients[plr].State == 10 && Netplay.Clients[plr].Socket.GetRemoteAddress().IsLocalHost();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0015D888 File Offset: 0x0015BA88
		private static void SyncOnePlayer(int plr, int toWho, int fromWho)
		{
			int num = 0;
			if (Main.player[plr].active)
			{
				num = 1;
			}
			if (Netplay.Clients[plr].State == 10)
			{
				NetMessage.SendData(14, toWho, fromWho, null, plr, (float)num, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(4, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(13, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				if (Main.player[plr].statLife <= 0)
				{
					NetMessage.SendData(135, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				}
				NetMessage.SendData(16, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(30, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(45, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(42, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(50, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(80, toWho, fromWho, null, plr, (float)Main.player[plr].chest, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(142, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(147, toWho, fromWho, null, plr, (float)Main.player[plr].CurrentLoadoutIndex, 0f, 0f, 0, 0, 0);
				for (int i = 0; i < 59; i++)
				{
					NetMessage.SendData(5, toWho, fromWho, null, plr, (float)(PlayerItemSlotID.Inventory0 + i), (float)Main.player[plr].inventory[i].prefix, 0f, 0, 0, 0);
				}
				for (int j = 0; j < Main.player[plr].armor.Length; j++)
				{
					NetMessage.SendData(5, toWho, fromWho, null, plr, (float)(PlayerItemSlotID.Armor0 + j), (float)Main.player[plr].armor[j].prefix, 0f, 0, 0, 0);
				}
				for (int k = 0; k < Main.player[plr].dye.Length; k++)
				{
					NetMessage.SendData(5, toWho, fromWho, null, plr, (float)(PlayerItemSlotID.Dye0 + k), (float)Main.player[plr].dye[k].prefix, 0f, 0, 0, 0);
				}
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].miscEquips, PlayerItemSlotID.Misc0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].miscDyes, PlayerItemSlotID.MiscDye0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[0].Armor, PlayerItemSlotID.Loadout1_Armor_0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[0].Dye, PlayerItemSlotID.Loadout1_Dye_0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[1].Armor, PlayerItemSlotID.Loadout2_Armor_0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[1].Dye, PlayerItemSlotID.Loadout2_Dye_0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[2].Armor, PlayerItemSlotID.Loadout3_Armor_0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[2].Dye, PlayerItemSlotID.Loadout3_Dye_0);
				PlayerLoader.SyncPlayer(Main.player[plr], toWho, fromWho, false);
				if (!Netplay.Clients[plr].IsAnnouncementCompleted)
				{
					Netplay.Clients[plr].IsAnnouncementCompleted = true;
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.mp[19].Key, new object[]
					{
						Main.player[plr].name
					}), new Color(255, 240, 20), plr);
					if (Main.dedServ)
					{
						Logging.ServerConsoleLine(Lang.mp[19].Format(new object[]
						{
							Main.player[plr].name
						}));
					}
				}
				return;
			}
			num = 0;
			NetMessage.SendData(14, -1, plr, null, plr, (float)num, 0f, 0f, 0, 0, 0);
			if (Netplay.Clients[plr].IsAnnouncementCompleted)
			{
				Netplay.Clients[plr].IsAnnouncementCompleted = false;
				ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.mp[20].Key, new object[]
				{
					Netplay.Clients[plr].Name
				}), new Color(255, 240, 20), plr);
				if (Main.dedServ)
				{
					Logging.ServerConsoleLine(Lang.mp[20].Format(new object[]
					{
						Netplay.Clients[plr].Name
					}));
				}
				Netplay.Clients[plr].Name = "Anonymous";
			}
			Player.Hooks.PlayerDisconnect(plr);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0015DD4C File Offset: 0x0015BF4C
		private static void SyncOnePlayer_ItemArray(int plr, int toWho, int fromWho, Item[] arr, int slot)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				NetMessage.SendData(5, toWho, fromWho, null, plr, (float)(slot + i), (float)arr[i].prefix, 0f, 0, 0, 0);
			}
		}

		// Token: 0x040007ED RID: 2029
		public static MessageBuffer[] buffer = new MessageBuffer[257];

		// Token: 0x040007EE RID: 2030
		private static short[] _compressChestList = new short[8000];

		// Token: 0x040007EF RID: 2031
		private static short[] _compressSignList = new short[1000];

		// Token: 0x040007F0 RID: 2032
		private static short[] _compressEntities = new short[1000];

		// Token: 0x040007F1 RID: 2033
		public static PlayerDeathReason _currentPlayerDeathReason;

		// Token: 0x040007F2 RID: 2034
		private static NetMessage.NetSoundInfo _currentNetSoundInfo;

		// Token: 0x040007F3 RID: 2035
		private static CoinLossRevengeSystem.RevengeMarker _currentRevengeMarker;

		// Token: 0x040007F4 RID: 2036
		private static Player.HurtInfo _currentPlayerHurtInfo;

		// Token: 0x040007F5 RID: 2037
		private static NPC.HitInfo _lastLegacyStrike;

		// Token: 0x040007F6 RID: 2038
		private static NPC.HitInfo _currentStrike;

		// Token: 0x020007C0 RID: 1984
		public struct NetSoundInfo
		{
			// Token: 0x06004F0C RID: 20236 RVA: 0x00675FBF File Offset: 0x006741BF
			public NetSoundInfo(Vector2 position, ushort soundIndex, int style = -1, float volume = -1f, float pitchOffset = -1f)
			{
				this.position = position;
				this.soundIndex = soundIndex;
				this.style = style;
				this.volume = volume;
				this.pitchOffset = pitchOffset;
			}

			// Token: 0x06004F0D RID: 20237 RVA: 0x00675FE8 File Offset: 0x006741E8
			public void WriteSelfTo(BinaryWriter writer)
			{
				writer.WriteVector2(this.position);
				writer.Write(this.soundIndex);
				BitsByte bitsByte = new BitsByte(this.style != -1, this.volume != -1f, this.pitchOffset != -1f, false, false, false, false, false);
				writer.Write(bitsByte);
				if (bitsByte[0])
				{
					writer.Write(this.style);
				}
				if (bitsByte[1])
				{
					writer.Write(this.volume);
				}
				if (bitsByte[2])
				{
					writer.Write(this.pitchOffset);
				}
			}

			// Token: 0x040066BD RID: 26301
			public Vector2 position;

			// Token: 0x040066BE RID: 26302
			public ushort soundIndex;

			// Token: 0x040066BF RID: 26303
			public int style;

			// Token: 0x040066C0 RID: 26304
			public float volume;

			// Token: 0x040066C1 RID: 26305
			public float pitchOffset;
		}
	}
}
