using System;
using System.IO;
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
using Terraria.Net.Sockets;
using Terraria.Social;

namespace Terraria
{
	// Token: 0x0200002C RID: 44
	public class NetMessage
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00036000 File Offset: 0x00034200
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

		// Token: 0x06000252 RID: 594 RVA: 0x00036040 File Offset: 0x00034240
		public static void SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, NetworkText text = null, int number = 0, float number2 = 0f, float number3 = 0f, float number4 = 0f, int number5 = 0, int number6 = 0, int number7 = 0)
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
					writer.Write("Terraria" + 279);
					break;
				case 2:
					text.Serialize(writer);
					if (Main.dedServ)
					{
						Console.WriteLine(Language.GetTextValue("CLI.ClientWasBooted", Netplay.Clients[num].Socket.GetRemoteAddress().ToString(), text));
					}
					break;
				case 3:
					writer.Write((byte)remoteClient);
					writer.Write(false);
					break;
				case 4:
				{
					Player player = Main.player[number];
					writer.Write((byte)number);
					writer.Write((byte)player.skinVariant);
					writer.Write((byte)player.hair);
					writer.Write(player.name);
					writer.Write(player.hairDye);
					NetMessage.WriteAccessoryVisibility(writer, player.hideVisibleAccessory);
					writer.Write(player.hideMisc);
					writer.WriteRGB(player.hairColor);
					writer.WriteRGB(player.skinColor);
					writer.WriteRGB(player.eyeColor);
					writer.WriteRGB(player.shirtColor);
					writer.WriteRGB(player.underShirtColor);
					writer.WriteRGB(player.pantsColor);
					writer.WriteRGB(player.shoeColor);
					BitsByte bb = 0;
					if (player.difficulty == 1)
					{
						bb[0] = true;
					}
					else if (player.difficulty == 2)
					{
						bb[1] = true;
					}
					else if (player.difficulty == 3)
					{
						bb[3] = true;
					}
					bb[2] = player.extraAccessory;
					writer.Write(bb);
					BitsByte bb2 = 0;
					bb2[0] = player.UsingBiomeTorches;
					bb2[1] = player.happyFunTorchTime;
					bb2[2] = player.unlockedBiomeTorches;
					bb2[3] = player.unlockedSuperCart;
					bb2[4] = player.enabledSuperCart;
					writer.Write(bb2);
					BitsByte bb3 = 0;
					bb3[0] = player.usedAegisCrystal;
					bb3[1] = player.usedAegisFruit;
					bb3[2] = player.usedArcaneCrystal;
					bb3[3] = player.usedGalaxyPearl;
					bb3[4] = player.usedGummyWorm;
					bb3[5] = player.usedAmbrosia;
					bb3[6] = player.ateArtisanBread;
					writer.Write(bb3);
					break;
				}
				case 5:
				{
					writer.Write((byte)number);
					writer.Write((short)number2);
					Player player2 = Main.player[number];
					Item item;
					if (number2 >= (float)PlayerItemSlotID.Loadout3_Dye_0)
					{
						item = player2.Loadouts[2].Dye[(int)number2 - PlayerItemSlotID.Loadout3_Dye_0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Loadout3_Armor_0)
					{
						item = player2.Loadouts[2].Armor[(int)number2 - PlayerItemSlotID.Loadout3_Armor_0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Loadout2_Dye_0)
					{
						item = player2.Loadouts[1].Dye[(int)number2 - PlayerItemSlotID.Loadout2_Dye_0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Loadout2_Armor_0)
					{
						item = player2.Loadouts[1].Armor[(int)number2 - PlayerItemSlotID.Loadout2_Armor_0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Loadout1_Dye_0)
					{
						item = player2.Loadouts[0].Dye[(int)number2 - PlayerItemSlotID.Loadout1_Dye_0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Loadout1_Armor_0)
					{
						item = player2.Loadouts[0].Armor[(int)number2 - PlayerItemSlotID.Loadout1_Armor_0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Bank4_0)
					{
						item = player2.bank4.item[(int)number2 - PlayerItemSlotID.Bank4_0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Bank3_0)
					{
						item = player2.bank3.item[(int)number2 - PlayerItemSlotID.Bank3_0];
					}
					else if (number2 >= (float)PlayerItemSlotID.TrashItem)
					{
						item = player2.trashItem;
					}
					else if (number2 >= (float)PlayerItemSlotID.Bank2_0)
					{
						item = player2.bank2.item[(int)number2 - PlayerItemSlotID.Bank2_0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Bank1_0)
					{
						item = player2.bank.item[(int)number2 - PlayerItemSlotID.Bank1_0];
					}
					else if (number2 >= (float)PlayerItemSlotID.MiscDye0)
					{
						item = player2.miscDyes[(int)number2 - PlayerItemSlotID.MiscDye0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Misc0)
					{
						item = player2.miscEquips[(int)number2 - PlayerItemSlotID.Misc0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Dye0)
					{
						item = player2.dye[(int)number2 - PlayerItemSlotID.Dye0];
					}
					else if (number2 >= (float)PlayerItemSlotID.Armor0)
					{
						item = player2.armor[(int)number2 - PlayerItemSlotID.Armor0];
					}
					else
					{
						item = player2.inventory[(int)number2 - PlayerItemSlotID.Inventory0];
					}
					if (item.Name == "" || item.stack == 0 || item.type == 0)
					{
						item.SetDefaults(0, true, null);
					}
					int num2 = item.stack;
					int netID = item.netID;
					if (num2 < 0)
					{
						num2 = 0;
					}
					writer.Write((short)num2);
					writer.Write((byte)number3);
					writer.Write((short)netID);
					break;
				}
				case 7:
				{
					writer.Write((int)Main.time);
					BitsByte bb4 = 0;
					bb4[0] = Main.dayTime;
					bb4[1] = Main.bloodMoon;
					bb4[2] = Main.eclipse;
					writer.Write(bb4);
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
					for (int j = 0; j < 4; j++)
					{
						writer.Write((byte)Main.treeStyle[j]);
					}
					for (int k = 0; k < 3; k++)
					{
						writer.Write(Main.caveBackX[k]);
					}
					for (int l = 0; l < 4; l++)
					{
						writer.Write((byte)Main.caveBackStyle[l]);
					}
					WorldGen.TreeTops.SyncSend(writer);
					if (!Main.raining)
					{
						Main.maxRaining = 0f;
					}
					writer.Write(Main.maxRaining);
					BitsByte bb5 = 0;
					bb5[0] = WorldGen.shadowOrbSmashed;
					bb5[1] = NPC.downedBoss1;
					bb5[2] = NPC.downedBoss2;
					bb5[3] = NPC.downedBoss3;
					bb5[4] = Main.hardMode;
					bb5[5] = NPC.downedClown;
					bb5[7] = NPC.downedPlantBoss;
					writer.Write(bb5);
					BitsByte bb6 = 0;
					bb6[0] = NPC.downedMechBoss1;
					bb6[1] = NPC.downedMechBoss2;
					bb6[2] = NPC.downedMechBoss3;
					bb6[3] = NPC.downedMechBossAny;
					bb6[4] = (Main.cloudBGActive >= 1f);
					bb6[5] = WorldGen.crimson;
					bb6[6] = Main.pumpkinMoon;
					bb6[7] = Main.snowMoon;
					writer.Write(bb6);
					BitsByte bb7 = 0;
					bb7[1] = Main.fastForwardTimeToDawn;
					bb7[2] = Main.slimeRain;
					bb7[3] = NPC.downedSlimeKing;
					bb7[4] = NPC.downedQueenBee;
					bb7[5] = NPC.downedFishron;
					bb7[6] = NPC.downedMartians;
					bb7[7] = NPC.downedAncientCultist;
					writer.Write(bb7);
					BitsByte bb8 = 0;
					bb8[0] = NPC.downedMoonlord;
					bb8[1] = NPC.downedHalloweenKing;
					bb8[2] = NPC.downedHalloweenTree;
					bb8[3] = NPC.downedChristmasIceQueen;
					bb8[4] = NPC.downedChristmasSantank;
					bb8[5] = NPC.downedChristmasTree;
					bb8[6] = NPC.downedGolemBoss;
					bb8[7] = BirthdayParty.PartyIsUp;
					writer.Write(bb8);
					BitsByte bb9 = 0;
					bb9[0] = NPC.downedPirates;
					bb9[1] = NPC.downedFrost;
					bb9[2] = NPC.downedGoblins;
					bb9[3] = Sandstorm.Happening;
					bb9[4] = DD2Event.Ongoing;
					bb9[5] = DD2Event.DownedInvasionT1;
					bb9[6] = DD2Event.DownedInvasionT2;
					bb9[7] = DD2Event.DownedInvasionT3;
					writer.Write(bb9);
					BitsByte bb10 = 0;
					bb10[0] = NPC.combatBookWasUsed;
					bb10[1] = LanternNight.LanternsUp;
					bb10[2] = NPC.downedTowerSolar;
					bb10[3] = NPC.downedTowerVortex;
					bb10[4] = NPC.downedTowerNebula;
					bb10[5] = NPC.downedTowerStardust;
					bb10[6] = Main.forceHalloweenForToday;
					bb10[7] = Main.forceXMasForToday;
					writer.Write(bb10);
					BitsByte bb11 = 0;
					bb11[0] = NPC.boughtCat;
					bb11[1] = NPC.boughtDog;
					bb11[2] = NPC.boughtBunny;
					bb11[3] = NPC.freeCake;
					bb11[4] = Main.drunkWorld;
					bb11[5] = NPC.downedEmpressOfLight;
					bb11[6] = NPC.downedQueenSlime;
					bb11[7] = Main.getGoodWorld;
					writer.Write(bb11);
					BitsByte bb12 = 0;
					bb12[0] = Main.tenthAnniversaryWorld;
					bb12[1] = Main.dontStarveWorld;
					bb12[2] = NPC.downedDeerclops;
					bb12[3] = Main.notTheBeesWorld;
					bb12[4] = Main.remixWorld;
					bb12[5] = NPC.unlockedSlimeBlueSpawn;
					bb12[6] = NPC.combatBookVolumeTwoWasUsed;
					bb12[7] = NPC.peddlersSatchelWasUsed;
					writer.Write(bb12);
					BitsByte bb13 = 0;
					bb13[0] = NPC.unlockedSlimeGreenSpawn;
					bb13[1] = NPC.unlockedSlimeOldSpawn;
					bb13[2] = NPC.unlockedSlimePurpleSpawn;
					bb13[3] = NPC.unlockedSlimeRainbowSpawn;
					bb13[4] = NPC.unlockedSlimeRedSpawn;
					bb13[5] = NPC.unlockedSlimeYellowSpawn;
					bb13[6] = NPC.unlockedSlimeCopperSpawn;
					bb13[7] = Main.fastForwardTimeToDusk;
					writer.Write(bb13);
					BitsByte bb14 = 0;
					bb14[0] = Main.noTrapsWorld;
					bb14[1] = Main.zenithWorld;
					bb14[2] = NPC.unlockedTruffleSpawn;
					writer.Write(bb14);
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
					BitsByte bb15 = (byte)number2;
					writer.Write(bb15);
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
					Player player3 = Main.player[number];
					writer.Write((byte)number);
					writer.Write((short)player3.SpawnX);
					writer.Write((short)player3.SpawnY);
					writer.Write(player3.respawnTimer);
					writer.Write((short)player3.numberOfDeathsPVE);
					writer.Write((short)player3.numberOfDeathsPVP);
					writer.Write((byte)number2);
					break;
				}
				case 13:
				{
					Player player4 = Main.player[number];
					writer.Write((byte)number);
					BitsByte bb16 = 0;
					bb16[0] = player4.controlUp;
					bb16[1] = player4.controlDown;
					bb16[2] = player4.controlLeft;
					bb16[3] = player4.controlRight;
					bb16[4] = player4.controlJump;
					bb16[5] = player4.controlUseItem;
					bb16[6] = (player4.direction == 1);
					writer.Write(bb16);
					BitsByte bb17 = 0;
					bb17[0] = player4.pulley;
					bb17[1] = (player4.pulley && player4.pulleyDir == 2);
					bb17[2] = (player4.velocity != Vector2.Zero);
					bb17[3] = player4.vortexStealthActive;
					bb17[4] = (player4.gravDir == 1f);
					bb17[5] = player4.shieldRaised;
					bb17[6] = player4.ghost;
					writer.Write(bb17);
					BitsByte bb18 = 0;
					bb18[0] = player4.tryKeepingHoveringUp;
					bb18[1] = player4.IsVoidVaultEnabled;
					bb18[2] = player4.sitting.isSitting;
					bb18[3] = player4.downedDD2EventAnyDifficulty;
					bb18[4] = player4.isPettingAnimal;
					bb18[5] = player4.isTheAnimalBeingPetSmall;
					bb18[6] = (player4.PotionOfReturnOriginalUsePosition != null);
					bb18[7] = player4.tryKeepingHoveringDown;
					writer.Write(bb18);
					BitsByte bb19 = 0;
					bb19[0] = player4.sleeping.isSleeping;
					bb19[1] = player4.autoReuseAllWeapons;
					bb19[2] = player4.controlDownHold;
					bb19[3] = player4.isOperatingAnotherEntity;
					bb19[4] = player4.controlUseTile;
					writer.Write(bb19);
					writer.Write((byte)player4.selectedItem);
					writer.WriteVector2(player4.position);
					if (bb17[2])
					{
						writer.WriteVector2(player4.velocity);
					}
					if (bb18[6])
					{
						writer.WriteVector2(player4.PotionOfReturnOriginalUsePosition.Value);
						writer.WriteVector2(player4.PotionOfReturnHomePosition.Value);
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
					writer.Write(Main.dayTime ? 1 : 0);
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
					int num3 = number;
					int num4 = (int)number2;
					int num5 = (int)number3;
					if (num5 < 0)
					{
						num5 = 0;
					}
					int num6 = (int)number4;
					if (num6 < 0)
					{
						num6 = 0;
					}
					if (num3 < num5)
					{
						num3 = num5;
					}
					if (num3 >= Main.maxTilesX + num5)
					{
						num3 = Main.maxTilesX - num5 - 1;
					}
					if (num4 < num6)
					{
						num4 = num6;
					}
					if (num4 >= Main.maxTilesY + num6)
					{
						num4 = Main.maxTilesY - num6 - 1;
					}
					writer.Write((short)num3);
					writer.Write((short)num4);
					writer.Write((byte)num5);
					writer.Write((byte)num6);
					writer.Write((byte)number5);
					for (int m = num3; m < num3 + num5; m++)
					{
						for (int n = num4; n < num4 + num6; n++)
						{
							BitsByte bb20 = 0;
							BitsByte bb21 = 0;
							BitsByte bb22 = 0;
							byte b = 0;
							byte b2 = 0;
							Tile tile = Main.tile[m, n];
							bb20[0] = tile.active();
							bb20[2] = (tile.wall > 0);
							bb20[3] = (tile.liquid > 0 && Main.netMode == 2);
							bb20[4] = tile.wire();
							bb20[5] = tile.halfBrick();
							bb20[6] = tile.actuator();
							bb20[7] = tile.inActive();
							bb21[0] = tile.wire2();
							bb21[1] = tile.wire3();
							if (tile.active() && tile.color() > 0)
							{
								bb21[2] = true;
								b = tile.color();
							}
							if (tile.wall > 0 && tile.wallColor() > 0)
							{
								bb21[3] = true;
								b2 = tile.wallColor();
							}
							bb21 += (byte)(tile.slope() << 4);
							bb21[7] = tile.wire4();
							bb22[0] = tile.fullbrightBlock();
							bb22[1] = tile.fullbrightWall();
							bb22[2] = tile.invisibleBlock();
							bb22[3] = tile.invisibleWall();
							writer.Write(bb20);
							writer.Write(bb21);
							writer.Write(bb22);
							if (b > 0)
							{
								writer.Write(b);
							}
							if (b2 > 0)
							{
								writer.Write(b2);
							}
							if (tile.active())
							{
								writer.Write(tile.type);
								if (Main.tileFrameImportant[(int)tile.type])
								{
									writer.Write(tile.frameX);
									writer.Write(tile.frameY);
								}
							}
							if (tile.wall > 0)
							{
								writer.Write(tile.wall);
							}
							if (tile.liquid > 0 && Main.netMode == 2)
							{
								writer.Write(tile.liquid);
								writer.Write(tile.liquidType());
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
					Item item2 = Main.item[number];
					writer.Write((short)number);
					writer.WriteVector2(item2.position);
					writer.WriteVector2(item2.velocity);
					writer.Write((short)item2.stack);
					writer.Write(item2.prefix);
					writer.Write((byte)number2);
					short value = 0;
					if (item2.active && item2.stack > 0)
					{
						value = (short)item2.netID;
					}
					writer.Write(value);
					if (msgType == 145)
					{
						writer.Write(item2.shimmered);
						writer.Write(item2.shimmerTime);
					}
					if (msgType == 148)
					{
						writer.Write((byte)MathHelper.Clamp((float)item2.timeLeftInWhichTheItemCannotBeTakenByEnemies, 0f, 255f));
					}
					break;
				}
				case 22:
					writer.Write((short)number);
					writer.Write((byte)Main.item[number].playerIndexTheItemIsReservedFor);
					break;
				case 23:
				{
					NPC npc = Main.npc[number];
					writer.Write((short)number);
					writer.WriteVector2(npc.position);
					writer.WriteVector2(npc.velocity);
					writer.Write((ushort)npc.target);
					int num7 = npc.life;
					if (!npc.active)
					{
						num7 = 0;
					}
					if (!npc.active || npc.life <= 0)
					{
						npc.netSkip = 0;
					}
					short value2 = (short)npc.netID;
					bool[] array = new bool[4];
					BitsByte bb23 = 0;
					bb23[0] = (npc.direction > 0);
					bb23[1] = (npc.directionY > 0);
					bb23[2] = (array[0] = (npc.ai[0] != 0f));
					bb23[3] = (array[1] = (npc.ai[1] != 0f));
					bb23[4] = (array[2] = (npc.ai[2] != 0f));
					bb23[5] = (array[3] = (npc.ai[3] != 0f));
					bb23[6] = (npc.spriteDirection > 0);
					bb23[7] = (num7 == npc.lifeMax);
					writer.Write(bb23);
					BitsByte bb24 = 0;
					bb24[0] = (npc.statsAreScaledForThisManyPlayers > 1);
					bb24[1] = npc.SpawnedFromStatue;
					bb24[2] = (npc.strengthMultiplier != 1f);
					writer.Write(bb24);
					for (int num8 = 0; num8 < NPC.maxAI; num8++)
					{
						if (array[num8])
						{
							writer.Write(npc.ai[num8]);
						}
					}
					writer.Write(value2);
					if (bb24[0])
					{
						writer.Write((byte)npc.statsAreScaledForThisManyPlayers);
					}
					if (bb24[2])
					{
						writer.Write(npc.strengthMultiplier);
					}
					if (!bb23[7])
					{
						byte b3 = 1;
						if (npc.lifeMax > 32767)
						{
							b3 = 4;
						}
						else if (npc.lifeMax > 127)
						{
							b3 = 2;
						}
						writer.Write(b3);
						if (b3 == 2)
						{
							writer.Write((short)num7);
						}
						else if (b3 == 4)
						{
							writer.Write(num7);
						}
						else
						{
							writer.Write((sbyte)num7);
						}
					}
					if (npc.type >= 0 && npc.type < (int)NPCID.Count && Main.npcCatchable[npc.type])
					{
						writer.Write((byte)npc.releaseOwner);
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
					BitsByte bb25 = 0;
					BitsByte bb26 = 0;
					bb25[0] = (projectile.ai[0] != 0f);
					bb25[1] = (projectile.ai[1] != 0f);
					bb26[0] = (projectile.ai[2] != 0f);
					if (projectile.bannerIdToRespondTo != 0)
					{
						bb25[3] = true;
					}
					if (projectile.damage != 0)
					{
						bb25[4] = true;
					}
					if (projectile.knockBack != 0f)
					{
						bb25[5] = true;
					}
					if (projectile.type > 0 && projectile.type < (int)ProjectileID.Count && ProjectileID.Sets.NeedsUUID[projectile.type])
					{
						bb25[7] = true;
					}
					if (projectile.originalDamage != 0)
					{
						bb25[6] = true;
					}
					if (bb26 != 0)
					{
						bb25[2] = true;
					}
					writer.Write(bb25);
					if (bb25[2])
					{
						writer.Write(bb26);
					}
					if (bb25[0])
					{
						writer.Write(projectile.ai[0]);
					}
					if (bb25[1])
					{
						writer.Write(projectile.ai[1]);
					}
					if (bb25[3])
					{
						writer.Write((ushort)projectile.bannerIdToRespondTo);
					}
					if (bb25[4])
					{
						writer.Write((short)projectile.damage);
					}
					if (bb25[5])
					{
						writer.Write(projectile.knockBack);
					}
					if (bb25[6])
					{
						writer.Write((short)projectile.originalDamage);
					}
					if (bb25[7])
					{
						writer.Write((short)projectile.projUUID);
					}
					if (bb26[0])
					{
						writer.Write(projectile.ai[2]);
					}
					break;
				}
				case 28:
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write(number3);
					writer.Write((byte)(number4 + 1f));
					writer.Write((byte)number5);
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
					Item item3 = Main.chest[number].item[(int)((byte)number2)];
					writer.Write((short)number);
					writer.Write((byte)number2);
					short value3 = (short)item3.netID;
					if (item3.Name == null)
					{
						value3 = 0;
					}
					writer.Write((short)item3.stack);
					writer.Write(item3.prefix);
					writer.Write(value3);
					break;
				}
				case 33:
				{
					int num9 = 0;
					int num10 = 0;
					int num11 = 0;
					string text2 = null;
					if (number > -1)
					{
						num9 = Main.chest[number].x;
						num10 = Main.chest[number].y;
					}
					if (number2 == 1f)
					{
						string text3 = text.ToString();
						num11 = (int)((byte)text3.Length);
						if (num11 == 0 || num11 > 20)
						{
							num11 = 255;
						}
						else
						{
							text2 = text3;
						}
					}
					writer.Write((short)number);
					writer.Write((short)num9);
					writer.Write((short)num10);
					writer.Write((byte)num11);
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
					break;
				case 35:
					writer.Write((byte)number);
					writer.Write((short)number2);
					break;
				case 36:
				{
					Player player5 = Main.player[number];
					writer.Write((byte)number);
					writer.Write(player5.zone1);
					writer.Write(player5.zone2);
					writer.Write(player5.zone3);
					writer.Write(player5.zone4);
					writer.Write(player5.zone5);
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
					Tile tile2 = Main.tile[number, (int)number2];
					writer.Write((short)number);
					writer.Write((short)number2);
					writer.Write(tile2.liquid);
					writer.Write(tile2.liquidType());
					break;
				}
				case 50:
					writer.Write((byte)number);
					for (int num12 = 0; num12 < Player.maxBuffs; num12++)
					{
						writer.Write((ushort)Main.player[number].buffType[num12]);
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
					for (int num13 = 0; num13 < NPC.maxBuffs; num13++)
					{
						writer.Write((ushort)Main.npc[number].buffType[num13]);
						writer.Write((short)Main.npc[number].buffTime[num13]);
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
					BitsByte bb27 = 0;
					bb27[0] = ((number & 1) == 1);
					bb27[1] = ((number & 2) == 2);
					bb27[2] = (number6 == 1);
					bb27[3] = (number7 != 0);
					writer.Write(bb27);
					writer.Write((short)number2);
					writer.Write(number3);
					writer.Write(number4);
					writer.Write((byte)number5);
					if (bb27[3])
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
					for (int num14 = 0; num14 < 40; num14++)
					{
						writer.Write((short)Main.travelShop[num14]);
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
					writer.Write(number2);
					writer.Write(number3);
					writer.WriteRGB(new Color
					{
						PackedValue = (uint)number
					});
					writer.Write((int)number4);
					break;
				case 83:
				{
					int num15 = number;
					if (num15 < 0 && num15 >= 290)
					{
						num15 = 1;
					}
					int value5 = NPC.killCount[num15];
					writer.Write((short)num15);
					writer.Write(value5);
					break;
				}
				case 84:
				{
					byte b4 = (byte)number;
					float stealth = Main.player[(int)b4].stealth;
					writer.Write(b4);
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
					bool flag2 = TileEntity.ByID.ContainsKey(number);
					writer.Write(flag2);
					if (flag2)
					{
						TileEntity.Write(writer, TileEntity.ByID[number], true);
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
					BitsByte bb28 = (byte)number2;
					BitsByte bb29 = (byte)number3;
					writer.Write((short)number);
					writer.Write(bb28);
					Item item4 = Main.item[number];
					if (bb28[0])
					{
						writer.Write(item4.color.PackedValue);
					}
					if (bb28[1])
					{
						writer.Write((ushort)item4.damage);
					}
					if (bb28[2])
					{
						writer.Write(item4.knockBack);
					}
					if (bb28[3])
					{
						writer.Write((ushort)item4.useAnimation);
					}
					if (bb28[4])
					{
						writer.Write((ushort)item4.useTime);
					}
					if (bb28[5])
					{
						writer.Write((short)item4.shoot);
					}
					if (bb28[6])
					{
						writer.Write(item4.shootSpeed);
					}
					if (bb28[7])
					{
						writer.Write(bb29);
						if (bb29[0])
						{
							writer.Write((ushort)item4.width);
						}
						if (bb29[1])
						{
							writer.Write((ushort)item4.height);
						}
						if (bb29[2])
						{
							writer.Write(item4.scale);
						}
						if (bb29[3])
						{
							writer.Write((short)item4.ammo);
						}
						if (bb29[4])
						{
							writer.Write((short)item4.useAmmo);
						}
						if (bb29[5])
						{
							writer.Write(item4.notAmmo);
						}
					}
					break;
				}
				case 89:
				{
					writer.Write((short)number);
					writer.Write((short)number2);
					Item item5 = Main.player[(int)number4].inventory[(int)number3];
					writer.Write((short)item5.netID);
					writer.Write(item5.prefix);
					writer.Write((short)number5);
					break;
				}
				case 91:
					writer.Write(number);
					writer.Write((byte)number2);
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
					Player player6 = Main.player[number];
					writer.Write((short)number4);
					writer.Write(number2);
					writer.Write(number3);
					writer.WriteVector2(player6.velocity);
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
					NPC npc2 = Main.npc[number];
					writer.Write((short)number4);
					writer.Write(number2);
					writer.Write(number3);
					writer.WriteVector2(npc2.velocity);
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
				{
					HalfVector2 halfVector = new HalfVector2((float)number, number2);
					writer.Write(halfVector.PackedValue);
					break;
				}
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
					writer.Write((byte)number);
					NetMessage._currentPlayerDeathReason.WriteSelfTo(writer);
					writer.Write((short)number2);
					writer.Write((byte)(number3 + 1f));
					writer.Write((byte)number4);
					writer.Write((sbyte)number5);
					break;
				case 118:
					writer.Write((byte)number);
					NetMessage._currentPlayerDeathReason.WriteSelfTo(writer);
					writer.Write((short)number2);
					writer.Write((byte)(number3 + 1f));
					writer.Write((byte)number4);
					break;
				case 119:
					writer.Write(number2);
					writer.Write(number3);
					writer.WriteRGB(new Color
					{
						PackedValue = (uint)number
					});
					text.Serialize(writer);
					break;
				case 120:
					writer.Write((byte)number);
					writer.Write((byte)number2);
					break;
				case 121:
				{
					int num16 = (int)number3;
					bool flag3 = number4 == 1f;
					if (flag3)
					{
						num16 += 8;
					}
					writer.Write((byte)number);
					writer.Write((int)number2);
					writer.Write((byte)num16);
					TEDisplayDoll tedisplayDoll = TileEntity.ByID[(int)number2] as TEDisplayDoll;
					if (tedisplayDoll != null)
					{
						tedisplayDoll.WriteItem((int)number3, writer, flag3);
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
				{
					writer.Write((short)number);
					writer.Write((short)number2);
					Item item6 = Main.player[(int)number4].inventory[(int)number3];
					writer.Write((short)item6.netID);
					writer.Write(item6.prefix);
					writer.Write((short)number5);
					break;
				}
				case 124:
				{
					int num17 = (int)number3;
					bool flag4 = number4 == 1f;
					if (flag4)
					{
						num17 += 2;
					}
					writer.Write((byte)number);
					writer.Write((int)number2);
					writer.Write((byte)num17);
					TEHatRack tehatRack = TileEntity.ByID[(int)number2] as TEHatRack;
					if (tehatRack != null)
					{
						tehatRack.WriteItem((int)number3, writer, flag4);
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
				{
					writer.Write((ushort)number);
					writer.Write((byte)number2);
					byte b5 = (byte)number2;
					if (b5 == 1)
					{
						writer.Write((int)number3);
						writer.Write((short)number4);
					}
					break;
				}
				case 132:
					NetMessage._currentNetSoundInfo.WriteSelfTo(writer);
					break;
				case 133:
				{
					writer.Write((short)number);
					writer.Write((short)number2);
					Item item7 = Main.player[(int)number4].inventory[(int)number3];
					writer.Write((short)item7.netID);
					writer.Write(item7.prefix);
					writer.Write((short)number5);
					break;
				}
				case 134:
				{
					writer.Write((byte)number);
					Player player7 = Main.player[number];
					writer.Write(player7.ladyBugLuckTimeLeft);
					writer.Write(player7.torchLuck);
					writer.Write(player7.luckPotion);
					writer.Write(player7.HasGardenGnomeNearby);
					writer.Write(player7.equipmentBasedLuckBonus);
					writer.Write(player7.coinLuck);
					break;
				}
				case 135:
					writer.Write((byte)number);
					break;
				case 136:
					for (int num18 = 0; num18 < 2; num18++)
					{
						for (int num19 = 0; num19 < 3; num19++)
						{
							writer.Write((ushort)NPC.cavernMonsterType[num18, num19]);
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
					Player player8 = Main.player[number];
					player8.piggyBankProjTracker.Write(writer);
					player8.voidLensChest.Write(writer);
					break;
				}
				case 146:
					writer.Write((byte)number);
					if (number == 0)
					{
						writer.WriteVector2(new Vector2((float)((int)number2), (float)((int)number3)));
					}
					else if (number == 1)
					{
						writer.WriteVector2(new Vector2((float)((int)number2), (float)((int)number3)));
						writer.Write((int)number4);
					}
					else if (number == 2)
					{
						writer.Write((int)number2);
					}
					break;
				case 147:
					writer.Write((byte)number);
					writer.Write((byte)number2);
					NetMessage.WriteAccessoryVisibility(writer, Main.player[number].hideVisibleAccessory);
					break;
				}
				int num20 = (int)writer.BaseStream.Position;
				if (num20 > 65535)
				{
					throw new Exception(string.Concat(new object[]
					{
						"Maximum packet length exceeded. id: ",
						msgType,
						" length: ",
						num20
					}));
				}
				writer.BaseStream.Position = position;
				writer.Write((ushort)num20);
				writer.BaseStream.Position = (long)num20;
				if (Main.netMode == 1)
				{
					if (!Netplay.Connection.Socket.IsConnected())
					{
						goto IL_39CA;
					}
					try
					{
						NetMessage.buffer[num].spamCount++;
						Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num20);
						Netplay.Connection.Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num20, new SocketSendCallback(Netplay.Connection.ClientWriteCallBack), null);
						goto IL_39CA;
					}
					catch
					{
						goto IL_39CA;
					}
				}
				if (remoteClient == -1)
				{
					if (msgType == 34 || msgType == 69)
					{
						for (int num21 = 0; num21 < 256; num21++)
						{
							if (num21 != ignoreClient && NetMessage.buffer[num21].broadcast && Netplay.Clients[num21].IsConnected())
							{
								try
								{
									NetMessage.buffer[num21].spamCount++;
									Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num20);
									Netplay.Clients[num21].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num20, new SocketSendCallback(Netplay.Clients[num21].ServerWriteCallBack), null);
								}
								catch
								{
								}
							}
						}
					}
					else if (msgType == 20)
					{
						for (int num22 = 0; num22 < 256; num22++)
						{
							if (num22 != ignoreClient && NetMessage.buffer[num22].broadcast && Netplay.Clients[num22].IsConnected() && Netplay.Clients[num22].SectionRange((int)Math.Max(number3, number4), number, (int)number2))
							{
								try
								{
									NetMessage.buffer[num22].spamCount++;
									Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num20);
									Netplay.Clients[num22].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num20, new SocketSendCallback(Netplay.Clients[num22].ServerWriteCallBack), null);
								}
								catch
								{
								}
							}
						}
					}
					else if (msgType == 23)
					{
						NPC npc3 = Main.npc[number];
						for (int num23 = 0; num23 < 256; num23++)
						{
							if (num23 != ignoreClient && NetMessage.buffer[num23].broadcast && Netplay.Clients[num23].IsConnected())
							{
								bool flag5 = false;
								if (npc3.boss || npc3.netAlways || npc3.townNPC || !npc3.active)
								{
									flag5 = true;
								}
								else if (npc3.netSkip <= 0)
								{
									Rectangle rect = Main.player[num23].getRect();
									Rectangle rect2 = npc3.getRect();
									rect2.X -= 2500;
									rect2.Y -= 2500;
									rect2.Width += 5000;
									rect2.Height += 5000;
									if (rect.Intersects(rect2))
									{
										flag5 = true;
									}
								}
								else
								{
									flag5 = true;
								}
								if (flag5)
								{
									try
									{
										NetMessage.buffer[num23].spamCount++;
										Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num20);
										Netplay.Clients[num23].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num20, new SocketSendCallback(Netplay.Clients[num23].ServerWriteCallBack), null);
									}
									catch
									{
									}
								}
							}
						}
						npc3.netSkip++;
						if (npc3.netSkip > 4)
						{
							npc3.netSkip = 0;
						}
					}
					else if (msgType == 28)
					{
						NPC npc4 = Main.npc[number];
						for (int num24 = 0; num24 < 256; num24++)
						{
							if (num24 != ignoreClient && NetMessage.buffer[num24].broadcast && Netplay.Clients[num24].IsConnected())
							{
								bool flag6 = false;
								if (npc4.life <= 0)
								{
									flag6 = true;
								}
								else
								{
									Rectangle rect3 = Main.player[num24].getRect();
									Rectangle rect4 = npc4.getRect();
									rect4.X -= 3000;
									rect4.Y -= 3000;
									rect4.Width += 6000;
									rect4.Height += 6000;
									if (rect3.Intersects(rect4))
									{
										flag6 = true;
									}
								}
								if (flag6)
								{
									try
									{
										NetMessage.buffer[num24].spamCount++;
										Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num20);
										Netplay.Clients[num24].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num20, new SocketSendCallback(Netplay.Clients[num24].ServerWriteCallBack), null);
									}
									catch
									{
									}
								}
							}
						}
					}
					else if (msgType == 13)
					{
						for (int num25 = 0; num25 < 256; num25++)
						{
							if (num25 != ignoreClient && NetMessage.buffer[num25].broadcast && Netplay.Clients[num25].IsConnected())
							{
								try
								{
									NetMessage.buffer[num25].spamCount++;
									Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num20);
									Netplay.Clients[num25].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num20, new SocketSendCallback(Netplay.Clients[num25].ServerWriteCallBack), null);
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
						}
					}
					else if (msgType == 27)
					{
						Projectile projectile2 = Main.projectile[number];
						for (int num26 = 0; num26 < 256; num26++)
						{
							if (num26 != ignoreClient && NetMessage.buffer[num26].broadcast && Netplay.Clients[num26].IsConnected())
							{
								bool flag7 = false;
								if (projectile2.type == 12 || Main.projPet[projectile2.type] || projectile2.aiStyle == 11 || projectile2.netImportant)
								{
									flag7 = true;
								}
								else
								{
									Rectangle rect5 = Main.player[num26].getRect();
									Rectangle rect6 = projectile2.getRect();
									rect6.X -= 5000;
									rect6.Y -= 5000;
									rect6.Width += 10000;
									rect6.Height += 10000;
									if (rect5.Intersects(rect6))
									{
										flag7 = true;
									}
								}
								if (flag7)
								{
									try
									{
										NetMessage.buffer[num26].spamCount++;
										Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num20);
										Netplay.Clients[num26].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num20, new SocketSendCallback(Netplay.Clients[num26].ServerWriteCallBack), null);
									}
									catch
									{
									}
								}
							}
						}
					}
					else
					{
						for (int num27 = 0; num27 < 256; num27++)
						{
							if (num27 != ignoreClient && (NetMessage.buffer[num27].broadcast || (Netplay.Clients[num27].State >= 3 && msgType == 10)) && Netplay.Clients[num27].IsConnected())
							{
								try
								{
									NetMessage.buffer[num27].spamCount++;
									Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num20);
									Netplay.Clients[num27].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num20, new SocketSendCallback(Netplay.Clients[num27].ServerWriteCallBack), null);
								}
								catch
								{
								}
							}
						}
					}
				}
				else if (Netplay.Clients[remoteClient].IsConnected())
				{
					try
					{
						NetMessage.buffer[remoteClient].spamCount++;
						Main.ActiveNetDiagnosticsUI.CountSentMessage(msgType, num20);
						Netplay.Clients[remoteClient].Socket.AsyncSend(NetMessage.buffer[num].writeBuffer, 0, num20, new SocketSendCallback(Netplay.Clients[remoteClient].ServerWriteCallBack), null);
					}
					catch
					{
					}
				}
				IL_39CA:
				if (Main.verboseNetplay)
				{
					for (int num28 = 0; num28 < num20; num28++)
					{
					}
					for (int num29 = 0; num29 < num20; num29++)
					{
						byte b6 = NetMessage.buffer[num].writeBuffer[num29];
					}
				}
				NetMessage.buffer[num].writeLocked = false;
				if (msgType == 2 && Main.netMode == 2)
				{
					Netplay.Clients[num].PendingTermination = true;
					Netplay.Clients[num].PendingTerminationApproved = true;
				}
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00039B84 File Offset: 0x00037D84
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

		// Token: 0x06000254 RID: 596 RVA: 0x00039BB8 File Offset: 0x00037DB8
		public static void CompressTileBlock(int xStart, int yStart, short width, short height, Stream stream)
		{
			using (DeflateStream deflateStream = new DeflateStream(stream, 0, true))
			{
				BinaryWriter binaryWriter = new BinaryWriter(deflateStream);
				binaryWriter.Write(xStart);
				binaryWriter.Write(yStart);
				binaryWriter.Write(width);
				binaryWriter.Write(height);
				NetMessage.CompressTileBlock_Inner(binaryWriter, xStart, yStart, (int)width, (int)height);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00039C18 File Offset: 0x00037E18
		public static void CompressTileBlock_Inner(BinaryWriter writer, int xStart, int yStart, int width, int height)
		{
			short num = 0;
			short num2 = 0;
			short num3 = 0;
			short num4 = 0;
			int num5 = 0;
			int num6 = 0;
			byte b = 0;
			byte[] array = new byte[16];
			Tile tile = null;
			for (int i = yStart; i < yStart + height; i++)
			{
				for (int j = xStart; j < xStart + width; j++)
				{
					Tile tile2 = Main.tile[j, i];
					if (tile2.isTheSameAs(tile) && TileID.Sets.AllowsSaveCompressionBatching[(int)tile2.type])
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
							array[num5] = (byte)tile2.type;
							num5++;
							if (tile2.type > 255)
							{
								array[num5] = (byte)(tile2.type >> 8);
								num5++;
								b |= 32;
							}
							if (TileID.Sets.BasicChest[(int)tile2.type] && tile2.frameX % 36 == 0 && tile2.frameY % 36 == 0)
							{
								short num7 = (short)Chest.FindChest(j, i);
								if (num7 != -1)
								{
									NetMessage._compressChestList[(int)num] = num7;
									num += 1;
								}
							}
							if (tile2.type == 88 && tile2.frameX % 54 == 0 && tile2.frameY % 36 == 0)
							{
								short num8 = (short)Chest.FindChest(j, i);
								if (num8 != -1)
								{
									NetMessage._compressChestList[(int)num] = num8;
									num += 1;
								}
							}
							if (tile2.type == 85 && tile2.frameX % 36 == 0 && tile2.frameY % 36 == 0)
							{
								short num9 = (short)Sign.ReadSign(j, i, true);
								if (num9 != -1)
								{
									short[] compressSignList = NetMessage._compressSignList;
									short num10 = num2;
									num2 = num10 + 1;
									compressSignList[(int)num10] = num9;
								}
							}
							if (tile2.type == 55 && tile2.frameX % 36 == 0 && tile2.frameY % 36 == 0)
							{
								short num11 = (short)Sign.ReadSign(j, i, true);
								if (num11 != -1)
								{
									short[] compressSignList2 = NetMessage._compressSignList;
									short num12 = num2;
									num2 = num12 + 1;
									compressSignList2[(int)num12] = num11;
								}
							}
							if (tile2.type == 425 && tile2.frameX % 36 == 0 && tile2.frameY % 36 == 0)
							{
								short num13 = (short)Sign.ReadSign(j, i, true);
								if (num13 != -1)
								{
									short[] compressSignList3 = NetMessage._compressSignList;
									short num14 = num2;
									num2 = num14 + 1;
									compressSignList3[(int)num14] = num13;
								}
							}
							if (tile2.type == 573 && tile2.frameX % 36 == 0 && tile2.frameY % 36 == 0)
							{
								short num15 = (short)Sign.ReadSign(j, i, true);
								if (num15 != -1)
								{
									short[] compressSignList4 = NetMessage._compressSignList;
									short num16 = num2;
									num2 = num16 + 1;
									compressSignList4[(int)num16] = num15;
								}
							}
							if (tile2.type == 378 && tile2.frameX % 36 == 0 && tile2.frameY == 0)
							{
								int num17 = TETrainingDummy.Find(j, i);
								if (num17 != -1)
								{
									short[] compressEntities = NetMessage._compressEntities;
									short num18 = num3;
									num3 = num18 + 1;
									compressEntities[(int)num18] = (short)num17;
								}
							}
							if (tile2.type == 395 && tile2.frameX % 36 == 0 && tile2.frameY == 0)
							{
								int num19 = TEItemFrame.Find(j, i);
								if (num19 != -1)
								{
									short[] compressEntities2 = NetMessage._compressEntities;
									short num20 = num3;
									num3 = num20 + 1;
									compressEntities2[(int)num20] = (short)num19;
								}
							}
							if (tile2.type == 520 && tile2.frameX % 18 == 0 && tile2.frameY == 0)
							{
								int num21 = TEFoodPlatter.Find(j, i);
								if (num21 != -1)
								{
									short[] compressEntities3 = NetMessage._compressEntities;
									short num22 = num3;
									num3 = num22 + 1;
									compressEntities3[(int)num22] = (short)num21;
								}
							}
							if (tile2.type == 471 && tile2.frameX % 54 == 0 && tile2.frameY == 0)
							{
								int num23 = TEWeaponsRack.Find(j, i);
								if (num23 != -1)
								{
									short[] compressEntities4 = NetMessage._compressEntities;
									short num24 = num3;
									num3 = num24 + 1;
									compressEntities4[(int)num24] = (short)num23;
								}
							}
							if (tile2.type == 470 && tile2.frameX % 36 == 0 && tile2.frameY == 0)
							{
								int num25 = TEDisplayDoll.Find(j, i);
								if (num25 != -1)
								{
									short[] compressEntities5 = NetMessage._compressEntities;
									short num26 = num3;
									num3 = num26 + 1;
									compressEntities5[(int)num26] = (short)num25;
								}
							}
							if (tile2.type == 475 && tile2.frameX % 54 == 0 && tile2.frameY == 0)
							{
								int num27 = TEHatRack.Find(j, i);
								if (num27 != -1)
								{
									short[] compressEntities6 = NetMessage._compressEntities;
									short num28 = num3;
									num3 = num28 + 1;
									compressEntities6[(int)num28] = (short)num27;
								}
							}
							if (tile2.type == 597 && tile2.frameX % 54 == 0 && tile2.frameY % 72 == 0)
							{
								int num29 = TETeleportationPylon.Find(j, i);
								if (num29 != -1)
								{
									short[] compressEntities7 = NetMessage._compressEntities;
									short num30 = num3;
									num3 = num30 + 1;
									compressEntities7[(int)num30] = (short)num29;
								}
							}
							if (Main.tileFrameImportant[(int)tile2.type])
							{
								array[num5] = (byte)(tile2.frameX & 255);
								num5++;
								array[num5] = (byte)(((int)tile2.frameX & 65280) >> 8);
								num5++;
								array[num5] = (byte)(tile2.frameY & 255);
								num5++;
								array[num5] = (byte)(((int)tile2.frameY & 65280) >> 8);
								num5++;
							}
							if (tile2.color() != 0)
							{
								b3 |= 8;
								array[num5] = tile2.color();
								num5++;
							}
						}
						if (tile2.wall != 0)
						{
							b |= 4;
							array[num5] = (byte)tile2.wall;
							num5++;
							if (tile2.wallColor() != 0)
							{
								b3 |= 16;
								array[num5] = tile2.wallColor();
								num5++;
							}
						}
						if (tile2.liquid != 0)
						{
							if (tile2.shimmer())
							{
								b3 |= 128;
								b |= 8;
							}
							else if (tile2.lava())
							{
								b |= 16;
							}
							else if (tile2.honey())
							{
								b |= 24;
							}
							else
							{
								b |= 8;
							}
							array[num5] = tile2.liquid;
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
						int num31;
						if (tile2.halfBrick())
						{
							num31 = 16;
						}
						else if (tile2.slope() != 0)
						{
							num31 = (int)(tile2.slope() + 1) << 4;
						}
						else
						{
							num31 = 0;
						}
						b2 |= (byte)num31;
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
						if (tile2.wall > 255)
						{
							array[num5] = (byte)(tile2.wall >> 8);
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
						tile = tile2;
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
			writer.Write(num3);
			for (int m = 0; m < (int)num3; m++)
			{
				TileEntity.Write(writer, TileEntity.ByID[(int)NetMessage._compressEntities[m]], false);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0003A474 File Offset: 0x00038674
		public static void DecompressTileBlock(Stream stream)
		{
			using (DeflateStream deflateStream = new DeflateStream(stream, 1, true))
			{
				BinaryReader binaryReader = new BinaryReader(deflateStream);
				NetMessage.DecompressTileBlock_Inner(binaryReader, binaryReader.ReadInt32(), binaryReader.ReadInt32(), (int)binaryReader.ReadInt16(), (int)binaryReader.ReadInt16());
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0003A4CC File Offset: 0x000386CC
		public static void DecompressTileBlock_Inner(BinaryReader reader, int xStart, int yStart, int width, int height)
		{
			Tile tile = null;
			int num = 0;
			for (int i = yStart; i < yStart + height; i++)
			{
				for (int j = xStart; j < xStart + width; j++)
				{
					if (num != 0)
					{
						num--;
						if (Main.tile[j, i] == null)
						{
							Main.tile[j, i] = new Tile(tile);
						}
						else
						{
							Main.tile[j, i].CopyFrom(tile);
						}
					}
					else
					{
						byte b3;
						byte b2;
						byte b = b2 = (b3 = 0);
						tile = Main.tile[j, i];
						if (tile == null)
						{
							tile = new Tile();
							Main.tile[j, i] = tile;
						}
						else
						{
							tile.ClearEverything();
						}
						byte b4 = reader.ReadByte();
						bool flag = false;
						if ((b4 & 1) == 1)
						{
							flag = true;
							b2 = reader.ReadByte();
						}
						bool flag2 = false;
						if (flag && (b2 & 1) == 1)
						{
							flag2 = true;
							b = reader.ReadByte();
						}
						if (flag2 && (b & 1) == 1)
						{
							b3 = reader.ReadByte();
						}
						bool flag3 = tile.active();
						byte b5;
						if ((b4 & 2) == 2)
						{
							tile.active(true);
							ushort type = tile.type;
							int num2;
							if ((b4 & 32) == 32)
							{
								b5 = reader.ReadByte();
								num2 = (int)reader.ReadByte();
								num2 = (num2 << 8 | (int)b5);
							}
							else
							{
								num2 = (int)reader.ReadByte();
							}
							tile.type = (ushort)num2;
							if (Main.tileFrameImportant[num2])
							{
								tile.frameX = reader.ReadInt16();
								tile.frameY = reader.ReadInt16();
							}
							else if (!flag3 || tile.type != type)
							{
								tile.frameX = -1;
								tile.frameY = -1;
							}
							if ((b & 8) == 8)
							{
								tile.color(reader.ReadByte());
							}
						}
						if ((b4 & 4) == 4)
						{
							tile.wall = (ushort)reader.ReadByte();
							if ((b & 16) == 16)
							{
								tile.wallColor(reader.ReadByte());
							}
						}
						b5 = (byte)((b4 & 24) >> 3);
						if (b5 != 0)
						{
							tile.liquid = reader.ReadByte();
							if ((b & 128) == 128)
							{
								tile.shimmer(true);
							}
							else if (b5 > 1)
							{
								if (b5 == 2)
								{
									tile.lava(true);
								}
								else
								{
									tile.honey(true);
								}
							}
						}
						if (b2 > 1)
						{
							if ((b2 & 2) == 2)
							{
								tile.wire(true);
							}
							if ((b2 & 4) == 4)
							{
								tile.wire2(true);
							}
							if ((b2 & 8) == 8)
							{
								tile.wire3(true);
							}
							b5 = (byte)((b2 & 112) >> 4);
							if (b5 != 0 && Main.tileSolid[(int)tile.type])
							{
								if (b5 == 1)
								{
									tile.halfBrick(true);
								}
								else
								{
									tile.slope(b5 - 1);
								}
							}
						}
						if (b > 1)
						{
							if ((b & 2) == 2)
							{
								tile.actuator(true);
							}
							if ((b & 4) == 4)
							{
								tile.inActive(true);
							}
							if ((b & 32) == 32)
							{
								tile.wire4(true);
							}
							if ((b & 64) == 64)
							{
								b5 = reader.ReadByte();
								tile.wall = (ushort)((int)b5 << 8 | (int)tile.wall);
							}
						}
						if (b3 > 1)
						{
							if ((b3 & 2) == 2)
							{
								tile.invisibleBlock(true);
							}
							if ((b3 & 4) == 4)
							{
								tile.invisibleWall(true);
							}
							if ((b3 & 8) == 8)
							{
								tile.fullbrightBlock(true);
							}
							if ((b3 & 16) == 16)
							{
								tile.fullbrightWall(true);
							}
						}
						b5 = (byte)((b4 & 192) >> 6);
						if (b5 == 0)
						{
							num = 0;
						}
						else if (b5 == 1)
						{
							num = (int)reader.ReadByte();
						}
						else
						{
							num = (int)reader.ReadInt16();
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
				TileEntity tileEntity = TileEntity.Read(reader, false);
				TileEntity.ByID[tileEntity.ID] = tileEntity;
				TileEntity.ByPosition[tileEntity.Position] = tileEntity;
			}
			Main.sectionManager.SetTilesLoaded(xStart, yStart, xStart + width - 1, yStart + height - 1);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0003A97C File Offset: 0x00038B7C
		public static void ReceiveBytes(byte[] bytes, int streamLength, int i = 256)
		{
			MessageBuffer obj = NetMessage.buffer[i];
			lock (obj)
			{
				try
				{
					Buffer.BlockCopy(bytes, 0, NetMessage.buffer[i].readBuffer, NetMessage.buffer[i].totalData, streamLength);
					NetMessage.buffer[i].totalData += streamLength;
					NetMessage.buffer[i].checkBytes = true;
				}
				catch
				{
					if (Main.netMode == 1)
					{
						Main.menuMode = 15;
						Main.statusText = Language.GetTextValue("Error.BadHeaderBufferOverflow");
						Netplay.Disconnect = true;
					}
					else
					{
						Netplay.Clients[i].PendingTermination = true;
					}
				}
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0003AA3C File Offset: 0x00038C3C
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
					int i = NetMessage.buffer[bufferIndex].totalData;
					try
					{
						while (i >= 2)
						{
							int num2 = (int)BitConverter.ToUInt16(NetMessage.buffer[bufferIndex].readBuffer, num);
							if (i < num2)
							{
								break;
							}
							long position = NetMessage.buffer[bufferIndex].reader.BaseStream.Position;
							int num3;
							NetMessage.buffer[bufferIndex].GetData(num + 2, num2 - 2, out num3);
							if (Main.dedServ && Netplay.Clients[bufferIndex].PendingTermination)
							{
								Netplay.Clients[bufferIndex].PendingTerminationApproved = true;
								NetMessage.buffer[bufferIndex].checkBytes = false;
								break;
							}
							NetMessage.buffer[bufferIndex].reader.BaseStream.Position = position + (long)num2;
							i -= num2;
							num += num2;
						}
					}
					catch (Exception)
					{
						if (Main.dedServ && num < NetMessage.buffer.Length - 100)
						{
							Console.WriteLine(Language.GetTextValue("Error.NetMessageError", NetMessage.buffer[num + 2]));
						}
						i = 0;
						num = 0;
					}
					if (i != NetMessage.buffer[bufferIndex].totalData)
					{
						for (int j = 0; j < i; j++)
						{
							NetMessage.buffer[bufferIndex].readBuffer[j] = NetMessage.buffer[bufferIndex].readBuffer[j + num];
						}
						NetMessage.buffer[bufferIndex].totalData = i;
					}
					NetMessage.buffer[bufferIndex].checkBytes = false;
				}
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0003AC18 File Offset: 0x00038E18
		public static void BootPlayer(int plr, NetworkText msg)
		{
			NetMessage.SendData(2, plr, -1, msg, 0, 0f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0003AC44 File Offset: 0x00038E44
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

		// Token: 0x0600025C RID: 604 RVA: 0x0003AC7C File Offset: 0x00038E7C
		public static void SendTemporaryAnimation(int whoAmi, int animationType, int tileType, int xCoord, int yCoord)
		{
			if (Main.netMode == 2)
			{
				NetMessage.SendData(77, whoAmi, -1, null, animationType, (float)tileType, (float)xCoord, (float)yCoord, 0, 0, 0);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0003ACA8 File Offset: 0x00038EA8
		public static void SendPlayerHurt(int playerTargetIndex, PlayerDeathReason reason, int damage, int direction, bool critical, bool pvp, int hitContext, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentPlayerDeathReason = reason;
			BitsByte bb = 0;
			bb[0] = critical;
			bb[1] = pvp;
			NetMessage.SendData(117, remoteClient, ignoreClient, null, playerTargetIndex, (float)damage, (float)direction, (float)bb, hitContext, 0, 0);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0003ACF4 File Offset: 0x00038EF4
		public static void SendPlayerDeath(int playerTargetIndex, PlayerDeathReason reason, int damage, int direction, bool pvp, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentPlayerDeathReason = reason;
			BitsByte bb = 0;
			bb[0] = pvp;
			NetMessage.SendData(118, remoteClient, ignoreClient, null, playerTargetIndex, (float)damage, (float)direction, (float)bb, 0, 0, 0);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0003AD34 File Offset: 0x00038F34
		public static void PlayNetSound(NetMessage.NetSoundInfo info, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentNetSoundInfo = info;
			NetMessage.SendData(132, remoteClient, ignoreClient, null, 0, 0f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0003AD68 File Offset: 0x00038F68
		public static void SendCoinLossRevengeMarker(CoinLossRevengeSystem.RevengeMarker marker, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentRevengeMarker = marker;
			NetMessage.SendData(126, remoteClient, ignoreClient, null, 0, 0f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0003AD98 File Offset: 0x00038F98
		public static void SendTileSquare(int whoAmi, int tileX, int tileY, int xSize, int ySize, TileChangeType changeType = TileChangeType.None)
		{
			NetMessage.SendData(20, whoAmi, -1, null, tileX, (float)tileY, (float)xSize, (float)ySize, (int)changeType, 0, 0);
			WorldGen.RangeFrame(tileX, tileY, xSize, ySize);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0003ADC8 File Offset: 0x00038FC8
		public static void SendTileSquare(int whoAmi, int tileX, int tileY, int centeredSquareSize, TileChangeType changeType = TileChangeType.None)
		{
			int num = (centeredSquareSize - 1) / 2;
			NetMessage.SendTileSquare(whoAmi, tileX - num, tileY - num, centeredSquareSize, centeredSquareSize, changeType);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0003ADEC File Offset: 0x00038FEC
		public static void SendTileSquare(int whoAmi, int tileX, int tileY, TileChangeType changeType = TileChangeType.None)
		{
			int num = 1;
			int num2 = (num - 1) / 2;
			NetMessage.SendTileSquare(whoAmi, tileX - num2, tileY - num2, num, num, changeType);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0003AE10 File Offset: 0x00039010
		public static void SendTravelShop(int remoteClient)
		{
			if (Main.netMode == 2)
			{
				NetMessage.SendData(72, remoteClient, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0003AE44 File Offset: 0x00039044
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

		// Token: 0x06000266 RID: 614 RVA: 0x0003AEF4 File Offset: 0x000390F4
		public static void SendSection(int whoAmi, int sectionX, int sectionY)
		{
			if (Main.netMode != 2)
			{
				return;
			}
			try
			{
				if (sectionX >= 0 && sectionY >= 0 && sectionX < Main.maxSectionsX && sectionY < Main.maxSectionsY)
				{
					if (!Netplay.Clients[whoAmi].TileSections[sectionX, sectionY])
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
			}
			catch
			{
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0003B054 File Offset: 0x00039254
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
					if (text == "")
					{
						text += Main.player[i].name;
					}
					else
					{
						text = text + ", " + Main.player[i].name;
					}
				}
			}
			ChatHelper.SendChatMessageToClient(NetworkText.FromKey("Game.JoinGreeting", new object[]
			{
				text
			}), new Color(255, 240, 20), plr);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0003B160 File Offset: 0x00039360
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

		// Token: 0x06000269 RID: 617 RVA: 0x0003B20A File Offset: 0x0003940A
		public static void SyncDisconnectedPlayer(int plr)
		{
			NetMessage.SyncOnePlayer(plr, -1, plr);
			NetMessage.EnsureLocalPlayerIsPresent();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0003B21C File Offset: 0x0003941C
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

		// Token: 0x0600026B RID: 619 RVA: 0x0003B280 File Offset: 0x00039480
		private static void SendNPCHousesAndTravelShop(int plr)
		{
			bool flag = false;
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active)
				{
					bool flag2 = npc.townNPC && NPC.TypeToDefaultHeadIndex(npc.type) > 0;
					if (npc.aiStyle == 7)
					{
						flag2 = true;
					}
					if (flag2)
					{
						if (!flag && npc.type == 368)
						{
							flag = true;
						}
						byte householdStatus = WorldGen.TownManager.GetHouseholdStatus(npc);
						NetMessage.SendData(60, plr, -1, null, i, (float)npc.homeTileX, (float)npc.homeTileY, (float)householdStatus, 0, 0, 0);
					}
				}
			}
			if (flag)
			{
				NetMessage.SendTravelShop(plr);
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0003B324 File Offset: 0x00039524
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
				Console.WriteLine(Language.GetTextValue("Net.ServerAutoShutdown"));
				Netplay.Disconnect = true;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0003B36F File Offset: 0x0003956F
		public static bool DoesPlayerSlotCountAsAHost(int plr)
		{
			return Netplay.Clients[plr].State == 10 && Netplay.Clients[plr].Socket.GetRemoteAddress().IsLocalHost();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0003B39C File Offset: 0x0003959C
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
				if (!Netplay.Clients[plr].IsAnnouncementCompleted)
				{
					Netplay.Clients[plr].IsAnnouncementCompleted = true;
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.mp[19].Key, new object[]
					{
						Main.player[plr].name
					}), new Color(255, 240, 20), plr);
					if (Main.dedServ)
					{
						Console.WriteLine(Lang.mp[19].Format(Main.player[plr].name));
						return;
					}
				}
			}
			else
			{
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
						Console.WriteLine(Lang.mp[20].Format(Netplay.Clients[plr].Name));
					}
					Netplay.Clients[plr].Name = "Anonymous";
				}
				Player.Hooks.PlayerDisconnect(plr);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0003B844 File Offset: 0x00039A44
		private static void SyncOnePlayer_ItemArray(int plr, int toWho, int fromWho, Item[] arr, int slot)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				NetMessage.SendData(5, toWho, fromWho, null, plr, (float)(slot + i), (float)arr[i].prefix, 0f, 0, 0, 0);
			}
		}

		// Token: 0x040001C4 RID: 452
		public static MessageBuffer[] buffer = new MessageBuffer[257];

		// Token: 0x040001C5 RID: 453
		private static short[] _compressChestList = new short[8000];

		// Token: 0x040001C6 RID: 454
		private static short[] _compressSignList = new short[1000];

		// Token: 0x040001C7 RID: 455
		private static short[] _compressEntities = new short[1000];

		// Token: 0x040001C8 RID: 456
		private static PlayerDeathReason _currentPlayerDeathReason;

		// Token: 0x040001C9 RID: 457
		private static NetMessage.NetSoundInfo _currentNetSoundInfo;

		// Token: 0x040001CA RID: 458
		private static CoinLossRevengeSystem.RevengeMarker _currentRevengeMarker;

		// Token: 0x020004AE RID: 1198
		public struct NetSoundInfo
		{
			// Token: 0x06002ECB RID: 11979 RVA: 0x005C4FDB File Offset: 0x005C31DB
			public NetSoundInfo(Vector2 position, ushort soundIndex, int style = -1, float volume = -1f, float pitchOffset = -1f)
			{
				this.position = position;
				this.soundIndex = soundIndex;
				this.style = style;
				this.volume = volume;
				this.pitchOffset = pitchOffset;
			}

			// Token: 0x06002ECC RID: 11980 RVA: 0x005C5004 File Offset: 0x005C3204
			public void WriteSelfTo(BinaryWriter writer)
			{
				writer.WriteVector2(this.position);
				writer.Write(this.soundIndex);
				BitsByte bb = new BitsByte(this.style != -1, this.volume != -1f, this.pitchOffset != -1f, false, false, false, false, false);
				writer.Write(bb);
				if (bb[0])
				{
					writer.Write(this.style);
				}
				if (bb[1])
				{
					writer.Write(this.volume);
				}
				if (bb[2])
				{
					writer.Write(this.pitchOffset);
				}
			}

			// Token: 0x04005639 RID: 22073
			public Vector2 position;

			// Token: 0x0400563A RID: 22074
			public ushort soundIndex;

			// Token: 0x0400563B RID: 22075
			public int style;

			// Token: 0x0400563C RID: 22076
			public float volume;

			// Token: 0x0400563D RID: 22077
			public float pitchOffset;
		}
	}
}
