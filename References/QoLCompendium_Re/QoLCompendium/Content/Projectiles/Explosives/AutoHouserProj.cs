using System;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Explosives
{
	// Token: 0x0200002D RID: 45
	public class AutoHouserProj : ModProjectile
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000046D4 File Offset: 0x000028D4
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Projectiles/Invisible";
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005775 File Offset: 0x00003975
		public override void SetDefaults()
		{
			base.Projectile.width = 1;
			base.Projectile.height = 1;
			base.Projectile.timeLeft = 1;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000579C File Offset: 0x0000399C
		public unsafe static void PlaceHouse(int x, int y, Vector2 position, int side, Player player)
		{
			int xPosition = (int)((float)(side * -1 + x) + position.X / 16f);
			int yPosition = (int)((float)y + position.Y / 16f);
			Tile tile = Main.tile[xPosition, yPosition];
			if (!CheckDestruction.OkayToDestroyTileAt(xPosition, yPosition))
			{
				return;
			}
			int wallID = 4;
			int tileID = 30;
			int platformID = 0;
			AutoHouserProj.GetHouseStyle(player, ref wallID, ref tileID, ref platformID);
			int modWallID = 0;
			int modTileID = -1;
			int modPlatformID = -1;
			bool inModdedBiome = false;
			AutoHouserProj.GetModdedHouseStyle(player, ref modWallID, ref modTileID, ref modPlatformID, ref inModdedBiome);
			if (x == 10 * side || x == side)
			{
				if (y == -5 && (int)(*tile.TileType) == tileID)
				{
					return;
				}
				if ((y == -4 || y == 0) && (int)(*tile.TileType) == tileID)
				{
					return;
				}
				if ((y == -1 || y == -2 || y == -3) && (*tile.TileType == 10 || *tile.TileType == 11))
				{
					return;
				}
			}
			else
			{
				if (y == -5 && (*tile.TileType == 19 || (int)(*tile.TileType) == tileID || (int)(*tile.TileType) == modPlatformID))
				{
					return;
				}
				if (y == 0 && (*tile.TileType == 19 || (int)(*tile.TileType) == tileID || (int)(*tile.TileType) == modPlatformID))
				{
					return;
				}
			}
			if ((x != 9 * side && x != 2 * side) || (y != -1 && y != -2 && y != -3) || *tile.TileType != 11)
			{
				Destruction.ClearEverything(xPosition, yPosition, true);
			}
			if (y != -5 && y != 0 && x != 10 * side && x != side)
			{
				if (inModdedBiome)
				{
					WorldGen.PlaceWall(xPosition, yPosition, modWallID, false);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, xPosition, yPosition, 1, TileChangeType.None);
					}
				}
				else
				{
					WorldGen.PlaceWall(xPosition, yPosition, wallID, false);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, xPosition, yPosition, 1, TileChangeType.None);
					}
				}
			}
			if (y == -5 && Math.Abs(x) >= 3 && Math.Abs(x) <= 5)
			{
				if (inModdedBiome)
				{
					WorldGen.PlaceTile(xPosition, yPosition, modPlatformID, false, false, -1, 0);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(17, -1, -1, null, 1, (float)xPosition, (float)yPosition, (float)modPlatformID, 0, 0, 0);
						return;
					}
				}
				else
				{
					WorldGen.PlaceTile(xPosition, yPosition, 19, false, false, -1, platformID);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(17, -1, -1, null, 1, (float)xPosition, (float)yPosition, 19f, platformID, 0, 0);
						return;
					}
				}
			}
			else if (y == -5 || y == 0 || x == 10 * side || (x == side && y == -4))
			{
				if (inModdedBiome)
				{
					WorldGen.PlaceTile(xPosition, yPosition, modTileID, false, false, -1, 0);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, xPosition, yPosition, 1, TileChangeType.None);
						return;
					}
				}
				else
				{
					WorldGen.PlaceTile(xPosition, yPosition, tileID, false, false, -1, 0);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, xPosition, yPosition, 1, TileChangeType.None);
					}
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005A18 File Offset: 0x00003C18
		public static void PlaceFurniture(int x, int y, Vector2 position, int side, Player player)
		{
			int xPosition = (int)((float)(side * -1 + x) + position.X / 16f);
			int yPosition = (int)((float)y + position.Y / 16f);
			if (!CheckDestruction.OkayToDestroyTile(Main.tile[xPosition, yPosition], false))
			{
				return;
			}
			int tableTileType = 1;
			int tableStyle = 0;
			int chairStyle = 0;
			int doorStyle = 0;
			int torchStyle = 0;
			AutoHouserProj.GetFurnitureStyle(player, ref tableStyle, ref chairStyle, ref doorStyle, ref torchStyle, ref tableTileType);
			int modTableID = -1;
			int modChairID = -1;
			int modDoorID = -1;
			int modTorchID = -1;
			bool inModdedBiome = false;
			AutoHouserProj.GetModdedFurnitureStyle(player, ref modTableID, ref modChairID, ref modDoorID, ref modTorchID, ref inModdedBiome);
			if (y == -1)
			{
				if (Math.Abs(x) == 1)
				{
					if (inModdedBiome)
					{
						WorldGen.PlaceTile(xPosition, yPosition, modDoorID, false, false, -1, 0);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, xPosition, yPosition - 2, 1, 3, TileChangeType.None);
						}
					}
					else
					{
						WorldGen.PlaceTile(xPosition, yPosition, 10, false, false, -1, doorStyle);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, xPosition, yPosition - 2, 1, 3, TileChangeType.None);
						}
					}
				}
				if (x == 5 * side)
				{
					if (inModdedBiome)
					{
						if (side == -1 && modChairID == Common.GetModTile(ModConditions.calamityMod, "AcidwoodChairTile"))
						{
							xPosition++;
						}
						WorldGen.PlaceObject(xPosition, yPosition, modChairID, false, 0, 0, -1, side);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(17, -1, -1, null, 1, (float)xPosition, (float)yPosition, (float)modChairID, 0, 0, 0);
						}
					}
					else
					{
						WorldGen.PlaceObject(xPosition, yPosition, 15, false, chairStyle, 0, -1, side);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(17, -1, -1, null, 1, (float)xPosition, (float)yPosition, 15f, chairStyle, 0, 0);
						}
					}
				}
				if (x == 7 * side)
				{
					if (inModdedBiome)
					{
						WorldGen.PlaceTile(xPosition, yPosition, modTableID, false, false, -1, 0);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(17, -1, -1, null, 1, (float)xPosition, (float)yPosition, (float)modTableID, 0, 0, 0);
						}
					}
					else
					{
						if (tableTileType == 1)
						{
							WorldGen.PlaceTile(xPosition, yPosition, 14, false, false, -1, tableStyle);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(17, -1, -1, null, 1, (float)xPosition, (float)yPosition, 14f, tableStyle, 0, 0);
							}
						}
						if (tableTileType == 2)
						{
							WorldGen.PlaceTile(xPosition, yPosition, 469, false, false, -1, tableStyle);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(17, -1, -1, null, 1, (float)xPosition, (float)yPosition, 469f, tableStyle, 0, 0);
							}
						}
					}
				}
			}
			if (x == 7 * side && y == -4)
			{
				if (inModdedBiome)
				{
					WorldGen.PlaceTile(xPosition, yPosition, modTorchID, false, false, -1, 0);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(17, -1, -1, null, 1, (float)xPosition, (float)yPosition, (float)modTorchID, 0, 0, 0);
						return;
					}
				}
				else
				{
					WorldGen.PlaceTile(xPosition, yPosition, 4, false, false, -1, torchStyle);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(17, -1, -1, null, 1, (float)xPosition, (float)yPosition, 4f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005C8C File Offset: 0x00003E8C
		public static void UpdateWall(int x, int y, Vector2 position, int side, Player player)
		{
			int xPosition = (int)((float)(side * -1 + x) + position.X / 16f);
			int yPosition = (int)((float)y + position.Y / 16f);
			WorldGen.SquareWallFrame(xPosition, yPosition, true);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, xPosition, yPosition, 1, TileChangeType.None);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005CDC File Offset: 0x00003EDC
		public override void OnKill(int timeLeft)
		{
			Vector2 position = base.Projectile.Center;
			SoundEngine.PlaySound(SoundID.Item14, new Vector2?(position), null);
			Player player = Main.player[base.Projectile.owner];
			if (Main.netMode == 1)
			{
				return;
			}
			if (player.Center.X < position.X)
			{
				for (int i = 0; i < 3; i++)
				{
					for (int x = 11; x > -1; x--)
					{
						if (i == 2 || (x != 11 && x != 0))
						{
							for (int y = -6; y <= 1; y++)
							{
								if (i == 2 || (y != -6 && y != 1))
								{
									if (i == 0)
									{
										AutoHouserProj.PlaceHouse(x, y, position, 1, player);
									}
									else if (i == 1)
									{
										AutoHouserProj.PlaceFurniture(x, y, position, 1, player);
									}
									else
									{
										AutoHouserProj.UpdateWall(x, y, position, 1, player);
									}
								}
							}
						}
					}
				}
				return;
			}
			for (int j = 0; j < 3; j++)
			{
				for (int x2 = -11; x2 < 1; x2++)
				{
					if (j == 2 || (x2 != -11 && x2 != 0))
					{
						for (int y2 = -6; y2 <= 1; y2++)
						{
							if (j == 2 || (y2 != -6 && y2 != 1))
							{
								if (j == 0)
								{
									AutoHouserProj.PlaceHouse(x2, y2, position, -1, player);
								}
								else if (j == 1)
								{
									AutoHouserProj.PlaceFurniture(x2, y2, position, -1, player);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005E1C File Offset: 0x0000401C
		public static void GetHouseStyle(Player player, ref int wallID, ref int tileID, ref int platformID)
		{
			if (player.ZoneDesert && !player.ZoneBeach)
			{
				wallID = 235;
				tileID = 479;
				platformID = 42;
			}
			if (player.ZoneSnow)
			{
				wallID = 149;
				tileID = 321;
				platformID = 19;
			}
			if (player.ZoneJungle)
			{
				wallID = 42;
				tileID = 158;
				platformID = 2;
			}
			if (player.ZoneCorrupt)
			{
				wallID = 41;
				tileID = 157;
				platformID = 1;
			}
			if (player.ZoneCrimson)
			{
				wallID = 85;
				tileID = 208;
				platformID = 5;
			}
			if (player.ZoneBeach)
			{
				wallID = 151;
				tileID = 322;
				platformID = 17;
			}
			if (player.ZoneHallow)
			{
				wallID = 43;
				tileID = 159;
				platformID = 3;
			}
			if (player.ZoneGlowshroom)
			{
				wallID = 74;
				tileID = 190;
				platformID = 18;
			}
			if (player.ZoneSkyHeight)
			{
				wallID = 82;
				tileID = 202;
				platformID = 22;
			}
			if (player.ZoneUnderworldHeight)
			{
				wallID = 20;
				tileID = 75;
				platformID = 13;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005F1C File Offset: 0x0000411C
		public static void GetFurnitureStyle(Player player, ref int tableID, ref int chairID, ref int doorID, ref int torchID, ref int tableTile)
		{
			if (player.ZoneDesert && !player.ZoneBeach)
			{
				tableTile = 2;
				tableID = 7;
				chairID = 43;
				doorID = 43;
				torchID = 16;
			}
			if (player.ZoneSnow)
			{
				tableID = 28;
				chairID = 30;
				doorID = 30;
				torchID = 9;
			}
			if (player.ZoneJungle)
			{
				tableID = 2;
				chairID = 3;
				doorID = 2;
				torchID = 21;
			}
			if (player.ZoneCorrupt)
			{
				tableID = 1;
				chairID = 2;
				doorID = 1;
				torchID = 18;
			}
			if (player.ZoneCrimson)
			{
				tableID = 8;
				chairID = 11;
				doorID = 10;
				torchID = 19;
			}
			if (player.ZoneBeach)
			{
				tableID = 26;
				chairID = 29;
				doorID = 29;
				torchID = 17;
			}
			if (player.ZoneHallow)
			{
				tableID = 3;
				chairID = 4;
				doorID = 3;
				torchID = 20;
			}
			if (player.ZoneGlowshroom)
			{
				tableID = 27;
				chairID = 9;
				doorID = 6;
				torchID = 22;
			}
			if (player.ZoneSkyHeight)
			{
				tableID = 7;
				chairID = 10;
				doorID = 9;
				torchID = 6;
			}
			if (player.ZoneUnderworldHeight)
			{
				tableID = 13;
				chairID = 16;
				doorID = 19;
				torchID = 7;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006020 File Offset: 0x00004220
		public static void GetModdedHouseStyle(Player player, ref int wallID, ref int tileID, ref int platformID, ref bool inModdedBiome)
		{
			if (ModConditions.calamityLoaded)
			{
				ModBiome AstralInfectionBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("AstralInfectionBiome", out AstralInfectionBiome) && Main.LocalPlayer.InModBiome(AstralInfectionBiome))
				{
					wallID = Common.GetModWall(ModConditions.calamityMod, "AstralMonolithWall");
					tileID = Common.GetModTile(ModConditions.calamityMod, "AstralMonolith");
					platformID = Common.GetModTile(ModConditions.calamityMod, "MonolithPlatform");
					inModdedBiome = true;
				}
				ModBiome AbyssLayer1Biome;
				ModBiome AbyssLayer2Biome;
				ModBiome AbyssLayer3Biome;
				ModBiome AbyssLayer4Biome;
				if ((ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer1Biome", out AbyssLayer1Biome) && Main.LocalPlayer.InModBiome(AbyssLayer1Biome)) || (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer2Biome", out AbyssLayer2Biome) && Main.LocalPlayer.InModBiome(AbyssLayer2Biome)) || (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer3Biome", out AbyssLayer3Biome) && Main.LocalPlayer.InModBiome(AbyssLayer3Biome)) || (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer4Biome", out AbyssLayer4Biome) && Main.LocalPlayer.InModBiome(AbyssLayer4Biome)))
				{
					wallID = Common.GetModWall(ModConditions.calamityMod, "SmoothAbyssGravelWall");
					tileID = Common.GetModTile(ModConditions.calamityMod, "SmoothAbyssGravel");
					platformID = Common.GetModTile(ModConditions.calamityMod, "SmoothAbyssGravelPlatform");
					inModdedBiome = true;
				}
				ModBiome BrimstoneCragsBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("BrimstoneCragsBiome", out BrimstoneCragsBiome) && Main.LocalPlayer.InModBiome(BrimstoneCragsBiome))
				{
					wallID = Common.GetModWall(ModConditions.calamityMod, "SmoothBrimstoneSlagWall");
					tileID = Common.GetModTile(ModConditions.calamityMod, "SmoothBrimstoneSlag");
					platformID = Common.GetModTile(ModConditions.calamityMod, "AshenPlatform");
					inModdedBiome = true;
				}
				ModBiome SulphurousSeaBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("SulphurousSeaBiome", out SulphurousSeaBiome) && Main.LocalPlayer.InModBiome(SulphurousSeaBiome))
				{
					wallID = Common.GetModWall(ModConditions.calamityMod, "AcidwoodWall");
					tileID = Common.GetModTile(ModConditions.calamityMod, "AcidwoodTile");
					platformID = Common.GetModTile(ModConditions.calamityMod, "AcidwoodPlatformTile");
					inModdedBiome = true;
				}
				ModBiome SunkenSeaBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("SunkenSeaBiome", out SunkenSeaBiome) && Main.LocalPlayer.InModBiome(SunkenSeaBiome))
				{
					wallID = Common.GetModWall(ModConditions.calamityMod, "SmoothNavystoneWall");
					tileID = Common.GetModTile(ModConditions.calamityMod, "SmoothNavystone");
					platformID = Common.GetModTile(ModConditions.calamityMod, "EutrophicPlatform");
					inModdedBiome = true;
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006250 File Offset: 0x00004450
		public static void GetModdedFurnitureStyle(Player player, ref int tableID, ref int chairID, ref int doorID, ref int torchID, ref bool inModdedBiome)
		{
			if (ModConditions.calamityLoaded)
			{
				ModBiome AstralInfectionBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("AstralInfectionBiome", out AstralInfectionBiome) && Main.LocalPlayer.InModBiome(AstralInfectionBiome))
				{
					tableID = Common.GetModTile(ModConditions.calamityMod, "MonolithTable");
					chairID = Common.GetModTile(ModConditions.calamityMod, "MonolithChair");
					doorID = Common.GetModTile(ModConditions.calamityMod, "MonolithDoorClosed");
					torchID = Common.GetModTile(ModConditions.calamityMod, "AstralTorch");
					inModdedBiome = true;
				}
				ModBiome AbyssLayer1Biome;
				ModBiome AbyssLayer2Biome;
				ModBiome AbyssLayer3Biome;
				ModBiome AbyssLayer4Biome;
				if ((ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer1Biome", out AbyssLayer1Biome) && Main.LocalPlayer.InModBiome(AbyssLayer1Biome)) || (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer2Biome", out AbyssLayer2Biome) && Main.LocalPlayer.InModBiome(AbyssLayer2Biome)) || (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer3Biome", out AbyssLayer3Biome) && Main.LocalPlayer.InModBiome(AbyssLayer3Biome)) || (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer4Biome", out AbyssLayer4Biome) && Main.LocalPlayer.InModBiome(AbyssLayer4Biome)))
				{
					tableID = Common.GetModTile(ModConditions.calamityMod, "AbyssTable");
					chairID = Common.GetModTile(ModConditions.calamityMod, "AbyssChair");
					doorID = Common.GetModTile(ModConditions.calamityMod, "AbyssDoorClosed");
					torchID = Common.GetModTile(ModConditions.calamityMod, "AbyssTorch");
					inModdedBiome = true;
				}
				ModBiome BrimstoneCragsBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("BrimstoneCragsBiome", out BrimstoneCragsBiome) && Main.LocalPlayer.InModBiome(BrimstoneCragsBiome))
				{
					tableID = Common.GetModTile(ModConditions.calamityMod, "AshenTable");
					chairID = Common.GetModTile(ModConditions.calamityMod, "AshenChair");
					doorID = Common.GetModTile(ModConditions.calamityMod, "AshenDoorClosed");
					torchID = Common.GetModTile(ModConditions.calamityMod, "GloomTorch");
					inModdedBiome = true;
				}
				ModBiome SulphurousSeaBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("SulphurousSeaBiome", out SulphurousSeaBiome) && Main.LocalPlayer.InModBiome(SulphurousSeaBiome))
				{
					tableID = Common.GetModTile(ModConditions.calamityMod, "AcidwoodTableTile");
					chairID = Common.GetModTile(ModConditions.calamityMod, "AcidwoodChairTile");
					doorID = Common.GetModTile(ModConditions.calamityMod, "AcidwoodDoorClosed");
					torchID = Common.GetModTile(ModConditions.calamityMod, "SulphurousTorch");
					inModdedBiome = true;
				}
				ModBiome SunkenSeaBiome;
				if (ModConditions.calamityMod.TryFind<ModBiome>("SunkenSeaBiome", out SunkenSeaBiome) && Main.LocalPlayer.InModBiome(SunkenSeaBiome))
				{
					tableID = Common.GetModTile(ModConditions.calamityMod, "EutrophicTable");
					chairID = Common.GetModTile(ModConditions.calamityMod, "EutrophicChair");
					doorID = Common.GetModTile(ModConditions.calamityMod, "EutrophicDoorClosed");
					torchID = Common.GetModTile(ModConditions.calamityMod, "NavyPrismTorch");
					inModdedBiome = true;
				}
			}
		}
	}
}
