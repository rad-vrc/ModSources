using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.GameContent.Metadata;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central class from which tile-related functions are supported and carried out.
	/// </summary>
	// Token: 0x020001FE RID: 510
	public static class TileLoader
	{
		// Token: 0x0600276F RID: 10095 RVA: 0x0050551F File Offset: 0x0050371F
		internal static int ReserveTileID()
		{
			int result = TileLoader.nextTile;
			TileLoader.nextTile++;
			return result;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x00505532 File Offset: 0x00503732
		public static int TileCount
		{
			get
			{
				return TileLoader.nextTile;
			}
		}

		/// <summary>
		/// Gets the ModTile instance with the given type. If no ModTile with the given type exists, returns null.
		/// </summary>
		/// <param name="type">The type of the ModTile</param>
		/// <returns>The ModTile instance in the tiles array, null if not found.</returns>
		// Token: 0x06002771 RID: 10097 RVA: 0x00505539 File Offset: 0x00503739
		public static ModTile GetTile(int type)
		{
			if (type < (int)TileID.Count || type >= TileLoader.TileCount)
			{
				return null;
			}
			return TileLoader.tiles[type - (int)TileID.Count];
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x00505560 File Offset: 0x00503760
		private static void Resize2DArray<T>(ref T[,] array, int newSize)
		{
			int dim = array.GetLength(0);
			int dim2 = array.GetLength(1);
			T[,] newArray = new T[newSize, dim2];
			int i = 0;
			while (i < newSize && i < dim)
			{
				for (int j = 0; j < dim2; j++)
				{
					newArray[i, j] = array[i, j];
				}
				i++;
			}
			array = newArray;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x005055C0 File Offset: 0x005037C0
		internal unsafe static void ResizeArrays(bool unloading = false)
		{
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Tile, TileLoader.nextTile);
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.HighlightMask, TileLoader.nextTile);
			LoaderUtils.ResetStaticMembers(typeof(TileID), true);
			Array.Resize<int>(ref Main.SceneMetrics._tileCounts, TileLoader.nextTile);
			Array.Resize<int>(ref Main.PylonSystem._sceneMetrics._tileCounts, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileLighted, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileMergeDirt, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileCut, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileAlch, TileLoader.nextTile);
			Array.Resize<int>(ref Main.tileShine, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileShine2, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileStone, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileAxe, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileHammer, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileWaterDeath, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileLavaDeath, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileTable, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileBlockLight, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileNoSunLight, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileDungeon, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileSpelunker, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileSolidTop, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileSolid, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileBouncy, TileLoader.nextTile);
			Array.Resize<byte>(ref Main.tileLargeFrames, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileRope, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileBrick, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileMoss, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileNoAttach, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileNoFail, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileObsidianKill, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileFrameImportant, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tilePile, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileBlendAll, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileContainer, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileSign, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileSand, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileFlame, TileLoader.nextTile);
			Array.Resize<int>(ref Main.tileFrame, TileLoader.nextTile);
			Array.Resize<int>(ref Main.tileFrameCounter, TileLoader.nextTile);
			Array.Resize<bool[]>(ref Main.tileMerge, TileLoader.nextTile);
			Array.Resize<short>(ref Main.tileOreFinderPriority, TileLoader.nextTile);
			Array.Resize<short>(ref Main.tileGlowMask, TileLoader.nextTile);
			Array.Resize<bool>(ref Main.tileCracked, TileLoader.nextTile);
			Array.Resize<int>(ref WorldGen.tileCounts, TileLoader.nextTile);
			Array.Resize<bool>(ref WorldGen.houseTile, TileLoader.nextTile);
			Array.Resize<bool>(ref CorruptionPitBiome.ValidTiles, TileLoader.nextTile);
			Array.Resize<TileMaterial>(ref TileMaterials.MaterialsByTileId, TileLoader.nextTile);
			Array.Resize<bool>(ref HouseUtils.BlacklistedTiles, TileLoader.nextTile);
			Array.Resize<bool>(ref HouseUtils.BeelistedTiles, TileLoader.nextTile);
			for (int i = 0; i < TileLoader.nextTile; i++)
			{
				Array.Resize<bool>(ref Main.tileMerge[i], TileLoader.nextTile);
			}
			for (int j = (int)TileID.Count; j < TileLoader.nextTile; j++)
			{
				Main.tileGlowMask[j] = -1;
				TileMaterials.MaterialsByTileId[j] = TileMaterials._materialsByName["Default"];
			}
			while (TileObjectData._data.Count < TileLoader.nextTile)
			{
				TileObjectData._data.Add(null);
			}
			TileLoader.tileConversionDelegates = new List<TileLoader.ConvertTile>[TileLoader.nextTile][];
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, bool, bool>>(ref TileLoader.HookKillSound, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, bool, bool>)methodof(GlobalBlockType.KillSound(int, int, int, bool)).CreateDelegate(typeof(Func<int, int, int, bool, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateNumDust>(ref TileLoader.HookNumDust, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateNumDust)methodof(GlobalBlockType.NumDust(int, int, int, bool, int*)).CreateDelegate(typeof(TileLoader.DelegateNumDust), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateCreateDust>(ref TileLoader.HookCreateDust, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateCreateDust)methodof(GlobalBlockType.CreateDust(int, int, int, int*)).CreateDelegate(typeof(TileLoader.DelegateCreateDust), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateDropCritterChance>(ref TileLoader.HookDropCritterChance, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateDropCritterChance)methodof(GlobalTile.DropCritterChance(int, int, int, int*, int*, int*)).CreateDelegate(typeof(TileLoader.DelegateDropCritterChance), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, bool>>(ref TileLoader.HookCanDrop, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, bool>)methodof(GlobalTile.CanDrop(int, int, int)).CreateDelegate(typeof(Func<int, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int>>(ref TileLoader.HookDrop, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int>)methodof(GlobalTile.Drop(int, int, int)).CreateDelegate(typeof(Action<int, int, int>), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateCanKillTile>(ref TileLoader.HookCanKillTile, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateCanKillTile)methodof(GlobalTile.CanKillTile(int, int, int, bool*)).CreateDelegate(typeof(TileLoader.DelegateCanKillTile), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateKillTile>(ref TileLoader.HookKillTile, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateKillTile)methodof(GlobalTile.KillTile(int, int, int, bool*, bool*, bool*)).CreateDelegate(typeof(TileLoader.DelegateKillTile), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, bool>>(ref TileLoader.HookCanExplode, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, bool>)methodof(GlobalBlockType.CanExplode(int, int, int)).CreateDelegate(typeof(Func<int, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int, bool>>(ref TileLoader.HookNearbyEffects, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int, bool>)methodof(GlobalTile.NearbyEffects(int, int, int, bool)).CreateDelegate(typeof(Action<int, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateModifyLight>(ref TileLoader.HookModifyLight, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateModifyLight)methodof(GlobalBlockType.ModifyLight(int, int, int, float*, float*, float*)).CreateDelegate(typeof(TileLoader.DelegateModifyLight), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, Player, bool?>>(ref TileLoader.HookIsTileDangerous, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, Player, bool?>)methodof(GlobalTile.IsTileDangerous(int, int, int, Player)).CreateDelegate(typeof(Func<int, int, int, Player, bool?>), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateIsTileBiomeSightable>(ref TileLoader.HookIsTileBiomeSightable, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateIsTileBiomeSightable)methodof(GlobalTile.IsTileBiomeSightable(int, int, int, Color*)).CreateDelegate(typeof(TileLoader.DelegateIsTileBiomeSightable), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, bool?>>(ref TileLoader.HookIsTileSpelunkable, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, bool?>)methodof(GlobalTile.IsTileSpelunkable(int, int, int)).CreateDelegate(typeof(Func<int, int, int, bool?>), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateSetSpriteEffects>(ref TileLoader.HookSetSpriteEffects, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateSetSpriteEffects)methodof(GlobalTile.SetSpriteEffects(int, int, int, SpriteEffects*)).CreateDelegate(typeof(TileLoader.DelegateSetSpriteEffects), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action>(ref TileLoader.HookAnimateTile, TileLoader.globalTiles, (GlobalTile g) => (Action)methodof(GlobalTile.AnimateTile()).CreateDelegate(typeof(Action), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, SpriteBatch, bool>>(ref TileLoader.HookPreDraw, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, SpriteBatch, bool>)methodof(GlobalBlockType.PreDraw(int, int, int, SpriteBatch)).CreateDelegate(typeof(Func<int, int, int, SpriteBatch, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateDrawEffects>(ref TileLoader.HookDrawEffects, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateDrawEffects)methodof(GlobalTile.DrawEffects(int, int, int, SpriteBatch, TileDrawInfo*)).CreateDelegate(typeof(TileLoader.DelegateDrawEffects), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, Tile, ushort, short, short, Color, bool>>(ref TileLoader.HookEmitParticles, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, Tile, ushort, short, short, Color, bool>)methodof(GlobalTile.EmitParticles(int, int, Tile, ushort, short, short, Color, bool)).CreateDelegate(typeof(Action<int, int, Tile, ushort, short, short, Color, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int, SpriteBatch>>(ref TileLoader.HookPostDraw, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int, SpriteBatch>)methodof(GlobalBlockType.PostDraw(int, int, int, SpriteBatch)).CreateDelegate(typeof(Action<int, int, int, SpriteBatch>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int, SpriteBatch>>(ref TileLoader.HookSpecialDraw, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int, SpriteBatch>)methodof(GlobalTile.SpecialDraw(int, int, int, SpriteBatch)).CreateDelegate(typeof(Action<int, int, int, SpriteBatch>), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegatePreDrawPlacementPreview>(ref TileLoader.HookPreDrawPlacementPreview, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegatePreDrawPlacementPreview)methodof(GlobalTile.PreDrawPlacementPreview(int, int, int, SpriteBatch, Rectangle*, Vector2*, Color*, bool, SpriteEffects*)).CreateDelegate(typeof(TileLoader.DelegatePreDrawPlacementPreview), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int, SpriteBatch, Rectangle, Vector2, Color, bool, SpriteEffects>>(ref TileLoader.HookPostDrawPlacementPreview, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int, SpriteBatch, Rectangle, Vector2, Color, bool, SpriteEffects>)methodof(GlobalTile.PostDrawPlacementPreview(int, int, int, SpriteBatch, Rectangle, Vector2, Color, bool, SpriteEffects)).CreateDelegate(typeof(Action<int, int, int, SpriteBatch, Rectangle, Vector2, Color, bool, SpriteEffects>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int>>(ref TileLoader.HookRandomUpdate, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int>)methodof(GlobalBlockType.RandomUpdate(int, int, int)).CreateDelegate(typeof(Action<int, int, int>), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateTileFrame>(ref TileLoader.HookTileFrame, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateTileFrame)methodof(GlobalTile.TileFrame(int, int, int, bool*, bool*)).CreateDelegate(typeof(TileLoader.DelegateTileFrame), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, bool>>(ref TileLoader.HookCanPlace, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, bool>)methodof(GlobalBlockType.CanPlace(int, int, int)).CreateDelegate(typeof(Func<int, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, int, bool>>(ref TileLoader.HookCanReplace, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, int, bool>)methodof(GlobalTile.CanReplace(int, int, int, int)).CreateDelegate(typeof(Func<int, int, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int[]>>(ref TileLoader.HookAdjTiles, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int[]>)methodof(GlobalTile.AdjTiles(int)).CreateDelegate(typeof(Func<int, int[]>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int>>(ref TileLoader.HookRightClick, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int>)methodof(GlobalTile.RightClick(int, int, int)).CreateDelegate(typeof(Action<int, int, int>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int>>(ref TileLoader.HookMouseOver, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int>)methodof(GlobalTile.MouseOver(int, int, int)).CreateDelegate(typeof(Action<int, int, int>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int>>(ref TileLoader.HookMouseOverFar, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int>)methodof(GlobalTile.MouseOverFar(int, int, int)).CreateDelegate(typeof(Action<int, int, int>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, Item, bool>>(ref TileLoader.HookAutoSelect, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, Item, bool>)methodof(GlobalTile.AutoSelect(int, int, int, Item)).CreateDelegate(typeof(Func<int, int, int, Item, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, bool>>(ref TileLoader.HookPreHitWire, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, bool>)methodof(GlobalTile.PreHitWire(int, int, int)).CreateDelegate(typeof(Func<int, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int>>(ref TileLoader.HookHitWire, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int>)methodof(GlobalTile.HitWire(int, int, int)).CreateDelegate(typeof(Action<int, int, int>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, int, bool>>(ref TileLoader.HookSlope, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, int, bool>)methodof(GlobalTile.Slope(int, int, int)).CreateDelegate(typeof(Func<int, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, Player>>(ref TileLoader.HookFloorVisuals, TileLoader.globalTiles, (GlobalTile g) => (Action<int, Player>)methodof(GlobalTile.FloorVisuals(int, Player)).CreateDelegate(typeof(Action<int, Player>), g));
			ModLoader.BuildGlobalHook<GlobalTile, TileLoader.DelegateChangeWaterfallStyle>(ref TileLoader.HookChangeWaterfallStyle, TileLoader.globalTiles, (GlobalTile g) => (TileLoader.DelegateChangeWaterfallStyle)methodof(GlobalTile.ChangeWaterfallStyle(int, int*)).CreateDelegate(typeof(TileLoader.DelegateChangeWaterfallStyle), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, int, Item>>(ref TileLoader.HookPlaceInWorld, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, int, Item>)methodof(GlobalBlockType.PlaceInWorld(int, int, int, Item)).CreateDelegate(typeof(Action<int, int, int, Item>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action>(ref TileLoader.HookPostSetupTileMerge, TileLoader.globalTiles, (GlobalTile g) => (Action)methodof(GlobalTile.PostSetupTileMerge()).CreateDelegate(typeof(Action), g));
			ModLoader.BuildGlobalHook<GlobalTile, Action<int, int, TreeTypes>>(ref TileLoader.HookPreShakeTree, TileLoader.globalTiles, (GlobalTile g) => (Action<int, int, TreeTypes>)methodof(GlobalTile.PreShakeTree(int, int, TreeTypes)).CreateDelegate(typeof(Action<int, int, TreeTypes>), g));
			ModLoader.BuildGlobalHook<GlobalTile, Func<int, int, TreeTypes, bool>>(ref TileLoader.HookShakeTree, TileLoader.globalTiles, (GlobalTile g) => (Func<int, int, TreeTypes, bool>)methodof(GlobalTile.ShakeTree(int, int, TreeTypes)).CreateDelegate(typeof(Func<int, int, TreeTypes, bool>), g));
			if (!unloading)
			{
				TileLoader.loaded = true;
			}
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x00507202 File Offset: 0x00505402
		internal static void PostSetupContent()
		{
			Main.SetupAllBlockMerge();
			TileLoader.PostSetupTileMerge();
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x00507210 File Offset: 0x00505410
		internal static void Unload()
		{
			TileLoader.loaded = false;
			TileLoader.nextTile = (int)TileID.Count;
			TileLoader.tiles.Clear();
			TileLoader.globalTiles.Clear();
			TileLoader.tileTypeAndTileStyleToItemType.Clear();
			Animation.Unload();
			TileLoader.tileConversionDelegates = null;
			Main.QueueMainThreadAction(delegate
			{
				Main.instance.TilePaintSystem.Reset();
			});
			Array.Resize<int>(ref TileID.Sets.RoomNeeds.CountsAsChair, TileLoader.vanillaChairCount);
			Array.Resize<int>(ref TileID.Sets.RoomNeeds.CountsAsTable, TileLoader.vanillaTableCount);
			Array.Resize<int>(ref TileID.Sets.RoomNeeds.CountsAsTorch, TileLoader.vanillaTorchCount);
			Array.Resize<int>(ref TileID.Sets.RoomNeeds.CountsAsDoor, TileLoader.vanillaDoorCount);
			while (TileObjectData._data.Count > (int)TileID.Count)
			{
				TileObjectData._data.RemoveAt(TileObjectData._data.Count - 1);
			}
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x005072E0 File Offset: 0x005054E0
		public unsafe static void CheckModTile(int i, int j, int type)
		{
			if (type <= (int)TileID.Count)
			{
				return;
			}
			if (WorldGen.destroyObject)
			{
				return;
			}
			TileObjectData tileData = TileObjectData.GetTileData(type, 0, 0);
			if (tileData == null)
			{
				return;
			}
			int frameX = (int)(*Main.tile[i, j].frameX);
			int frameY = (int)(*Main.tile[i, j].frameY);
			int subX = frameX / tileData.CoordinateFullWidth;
			int subY = frameY / tileData.CoordinateFullHeight;
			int wrap = tileData.StyleWrapLimit;
			if (wrap == 0)
			{
				wrap = 1;
			}
			int styleLineSkip = tileData.StyleLineSkip;
			int style = (tileData.StyleHorizontal ? (subY / styleLineSkip * wrap + subX) : (subX / styleLineSkip * wrap + subY)) / tileData.StyleMultiplier;
			tileData = TileObjectData.GetTileData(Main.tile[i, j]);
			int partFrameX = frameX % tileData.CoordinateFullWidth;
			int partFrameY = frameY % tileData.CoordinateFullHeight;
			int partX = partFrameX / (tileData.CoordinateWidth + tileData.CoordinatePadding);
			int partY = 0;
			int remainingFrameY = partFrameY;
			while (partY + 1 < tileData.Height && remainingFrameY - tileData.CoordinateHeights[partY] - tileData.CoordinatePadding >= 0)
			{
				remainingFrameY -= tileData.CoordinateHeights[partY] + tileData.CoordinatePadding;
				partY++;
			}
			int originalI = i;
			int originalJ = j;
			i -= partX;
			j -= partY;
			int originX = i + (int)tileData.Origin.X;
			int originY = j + (int)tileData.Origin.Y;
			bool partiallyDestroyed = false;
			for (int x = i; x < i + tileData.Width; x++)
			{
				for (int y = j; y < j + tileData.Height; y++)
				{
					if (!Main.tile[x, y].active() || (int)(*Main.tile[x, y].type) != type)
					{
						partiallyDestroyed = true;
						break;
					}
				}
				if (partiallyDestroyed)
				{
					break;
				}
			}
			TileObject objectData;
			if (partiallyDestroyed || !TileObject.CanPlace(originX, originY, type, style, 0, out objectData, true, null, true))
			{
				WorldGen.destroyObject = true;
				if (tileData.Width != 1 || tileData.Height != 1)
				{
					WorldGen.KillTile_DropItems(originalI, originalJ, Main.tile[originalI, originalJ], true, true);
				}
				for (int x2 = i; x2 < i + tileData.Width; x2++)
				{
					for (int y2 = j; y2 < j + tileData.Height; y2++)
					{
						if ((int)(*Main.tile[x2, y2].type) == type && Main.tile[x2, y2].active())
						{
							WorldGen.KillTile(x2, y2, false, false, false);
						}
					}
				}
				TileLoader.KillMultiTile(i, j, frameX - partFrameX, frameY - partFrameY, type);
				WorldGen.destroyObject = false;
				for (int x3 = i - 1; x3 < i + tileData.Width + 2; x3++)
				{
					for (int y3 = j - 1; y3 < j + tileData.Height + 2; y3++)
					{
						WorldGen.TileFrame(x3, y3, false, false);
					}
				}
			}
			TileObject.objectPreview.Active = false;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x005075CC File Offset: 0x005057CC
		public unsafe static int OpenDoorID(Tile tile)
		{
			ModTile modTile = TileLoader.GetTile((int)(*tile.type));
			if (modTile != null)
			{
				return TileID.Sets.OpenDoorID[(int)modTile.Type];
			}
			if (*tile.type == 10 && (*tile.frameY < 594 || *tile.frameY > 646 || *tile.frameX >= 54))
			{
				return 11;
			}
			return -1;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x00507634 File Offset: 0x00505834
		public unsafe static int CloseDoorID(Tile tile)
		{
			ModTile modTile = TileLoader.GetTile((int)(*tile.type));
			if (modTile != null)
			{
				return TileID.Sets.CloseDoorID[(int)modTile.Type];
			}
			if (*tile.type == 11)
			{
				return 10;
			}
			return -1;
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.TileLoader.IsClosedDoor(System.Int32)" />
		// Token: 0x06002779 RID: 10105 RVA: 0x0050766F File Offset: 0x0050586F
		public unsafe static bool IsClosedDoor(Tile tile)
		{
			return TileLoader.IsClosedDoor((int)(*tile.type));
		}

		/// <summary>
		/// Returns true if the tile is a vanilla or modded closed door.
		/// </summary>
		// Token: 0x0600277A RID: 10106 RVA: 0x0050767E File Offset: 0x0050587E
		public static bool IsClosedDoor(int type)
		{
			if (TileLoader.GetTile(type) != null)
			{
				return TileID.Sets.OpenDoorID[type] > -1;
			}
			return type == 10;
		}

		/// <summary> Returns the default name for a modded chest or dresser with the provided FrameX and FrameY values. </summary>
		// Token: 0x0600277B RID: 10107 RVA: 0x00507698 File Offset: 0x00505898
		public static string DefaultContainerName(int type, int frameX, int frameY)
		{
			ModTile tile = TileLoader.GetTile(type);
			string text;
			if (tile == null)
			{
				text = null;
			}
			else
			{
				LocalizedText localizedText = tile.DefaultContainerName(frameX, frameY);
				text = ((localizedText != null) ? localizedText.Value : null);
			}
			return text ?? string.Empty;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x005076C3 File Offset: 0x005058C3
		public unsafe static bool IsModMusicBox(Tile tile)
		{
			return MusicLoader.tileToMusic.ContainsKey((int)(*tile.type)) && MusicLoader.tileToMusic[(int)(*tile.type)].ContainsKey((int)(*tile.frameY / 36 * 36));
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00507700 File Offset: 0x00505900
		public static bool HasSmartInteract(int i, int j, int type, SmartInteractScanSettings settings)
		{
			ModTile tile = TileLoader.GetTile(type);
			return tile != null && tile.HasSmartInteract(i, j, settings);
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x00507718 File Offset: 0x00505918
		public static void ModifySmartInteractCoords(int type, ref int width, ref int height, ref int frameWidth, ref int frameHeight, ref int extraY)
		{
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile == null)
			{
				return;
			}
			TileObjectData data = TileObjectData.GetTileData(type, 0, 0);
			if (data == null)
			{
				return;
			}
			width = data.Width;
			height = data.Height;
			frameWidth = data.CoordinateWidth + data.CoordinatePadding;
			frameHeight = data.CoordinateHeights[0] + data.CoordinatePadding;
			extraY = data.CoordinateFullHeight % frameHeight;
			modTile.ModifySmartInteractCoords(ref width, ref height, ref frameWidth, ref frameHeight, ref extraY);
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x00507788 File Offset: 0x00505988
		public static void ModifySittingTargetInfo(int i, int j, int type, ref TileRestingInfo info)
		{
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile != null)
			{
				modTile.ModifySittingTargetInfo(i, j, ref info);
				return;
			}
			info.AnchorTilePosition.Y = info.AnchorTilePosition.Y + 1;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x005077BC File Offset: 0x005059BC
		public static void ModifySleepingTargetInfo(int i, int j, int type, ref TileRestingInfo info)
		{
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile != null)
			{
				info.VisualOffset = new Vector2(-9f, 1f);
				modTile.ModifySleepingTargetInfo(i, j, ref info);
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x005077F4 File Offset: 0x005059F4
		public static bool KillSound(int i, int j, int type, bool fail)
		{
			Func<int, int, int, bool, bool>[] hookKillSound = TileLoader.HookKillSound;
			for (int k = 0; k < hookKillSound.Length; k++)
			{
				if (!hookKillSound[k](i, j, type, fail))
				{
					return false;
				}
			}
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile == null)
			{
				return true;
			}
			if (!modTile.KillSound(i, j, fail))
			{
				return false;
			}
			SoundEngine.PlaySound(modTile.HitSound, new Vector2?(new Vector2((float)(i * 16), (float)(j * 16))));
			return false;
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00507860 File Offset: 0x00505A60
		public static void NumDust(int i, int j, int type, bool fail, ref int numDust)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.NumDust(i, j, fail, ref numDust);
			}
			TileLoader.DelegateNumDust[] hookNumDust = TileLoader.HookNumDust;
			for (int k = 0; k < hookNumDust.Length; k++)
			{
				hookNumDust[k](i, j, type, fail, ref numDust);
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x005078A8 File Offset: 0x00505AA8
		public static bool CreateDust(int i, int j, int type, ref int dustType)
		{
			TileLoader.DelegateCreateDust[] hookCreateDust = TileLoader.HookCreateDust;
			for (int k = 0; k < hookCreateDust.Length; k++)
			{
				if (!hookCreateDust[k](i, j, type, ref dustType))
				{
					return false;
				}
			}
			ModTile tile = TileLoader.GetTile(type);
			return tile == null || tile.CreateDust(i, j, ref dustType);
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x005078F0 File Offset: 0x00505AF0
		public static void DropCritterChance(int i, int j, int type, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.DropCritterChance(i, j, ref wormChance, ref grassHopperChance, ref jungleGrubChance);
			}
			TileLoader.DelegateDropCritterChance[] hookDropCritterChance = TileLoader.HookDropCritterChance;
			for (int k = 0; k < hookDropCritterChance.Length; k++)
			{
				hookDropCritterChance[k](i, j, type, ref wormChance, ref grassHopperChance, ref jungleGrubChance);
			}
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x0050793C File Offset: 0x00505B3C
		public static bool Drop(int i, int j, int type, bool includeLargeObjectDrops = true)
		{
			bool isLarge = false;
			if (Main.tileFrameImportant[type])
			{
				TileObjectData tileData = TileObjectData.GetTileData(type, 0, 0);
				if (tileData != null)
				{
					if (tileData.Width != 1 || tileData.Height != 1)
					{
						isLarge = true;
					}
				}
				else if (TileID.Sets.IsMultitile[type])
				{
					isLarge = true;
				}
			}
			if (!includeLargeObjectDrops && isLarge)
			{
				return true;
			}
			Tile tile = Main.tile[i, j];
			ModTile tile2 = TileLoader.GetTile(type);
			bool dropItem = tile2 == null || tile2.CanDrop(i, j);
			foreach (Func<int, int, int, bool> hook in TileLoader.HookCanDrop)
			{
				dropItem &= hook(i, j, type);
			}
			if (!dropItem)
			{
				return false;
			}
			Action<int, int, int>[] hookDrop = TileLoader.HookDrop;
			for (int k = 0; k < hookDrop.Length; k++)
			{
				hookDrop[k](i, j, type);
			}
			return true;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x00507A08 File Offset: 0x00505C08
		public unsafe static void GetItemDrops(int x, int y, Tile tileCache, bool includeLargeObjectDrops = false, bool includeAllModdedLargeObjectDrops = false)
		{
			ModTile modTile = TileLoader.GetTile((int)(*tileCache.TileType));
			if (modTile == null)
			{
				return;
			}
			bool needDrops = false;
			TileObjectData tileData = TileObjectData.GetTileData((int)(*tileCache.TileType), 0, 0);
			if (tileData == null)
			{
				needDrops = true;
			}
			else if (tileData.Width == 1 && tileData.Height == 1)
			{
				needDrops = !includeAllModdedLargeObjectDrops;
			}
			else if (includeAllModdedLargeObjectDrops)
			{
				needDrops = true;
			}
			else if (includeLargeObjectDrops && (TileID.Sets.BasicChest[(int)(*tileCache.type)] || TileID.Sets.BasicDresser[(int)(*tileCache.type)] || TileID.Sets.Campfire[(int)(*tileCache.type)]))
			{
				needDrops = true;
			}
			if (!needDrops)
			{
				return;
			}
			IEnumerable<Item> itemDrops = modTile.GetItemDrops(x, y);
			if (itemDrops != null)
			{
				foreach (Item item in itemDrops)
				{
					item.Prefix(-1);
					int num = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(x, y), x * 16, y * 16, 16, 16, item, false, false, false);
					Main.item[num].TryCombiningIntoNearbyItems(num);
				}
			}
		}

		/// <summary>
		/// Retrieves the item type that would drop from a tile of the specified type and style. This method is only reliable for modded tile types. This method can be used in <see cref="M:Terraria.ModLoader.ModTile.GetItemDrops(System.Int32,System.Int32)" /> for tiles that have custom tile style logic. If the specified style is not found, a fallback item will be returned if one has been registered through <see cref="M:Terraria.ModLoader.ModTile.RegisterItemDrop(System.Int32,System.Int32[])" /> usage.<br />
		/// Modders querying modded tile drops should use <see cref="M:Terraria.ModLoader.ModTile.GetItemDrops(System.Int32,System.Int32)" /> directly rather that use this method so that custom drop logic is accounted for.
		/// <br /> A return of 0 indicates that no item would drop from the tile.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="style"></param>
		/// <returns></returns>
		// Token: 0x06002787 RID: 10119 RVA: 0x00507B18 File Offset: 0x00505D18
		public static int GetItemDropFromTypeAndStyle(int type, int style = 0)
		{
			int value;
			if (TileLoader.tileTypeAndTileStyleToItemType.TryGetValue(new ValueTuple<int, int>(type, style), out value) || TileLoader.tileTypeAndTileStyleToItemType.TryGetValue(new ValueTuple<int, int>(type, -1), out value))
			{
				return value;
			}
			return 0;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x00507B54 File Offset: 0x00505D54
		public static bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
		{
			TileLoader.DelegateCanKillTile[] hookCanKillTile = TileLoader.HookCanKillTile;
			for (int k = 0; k < hookCanKillTile.Length; k++)
			{
				if (!hookCanKillTile[k](i, j, type, ref blockDamaged))
				{
					return false;
				}
			}
			ModTile tile = TileLoader.GetTile(type);
			return tile == null || tile.CanKillTile(i, j, ref blockDamaged);
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00507B9C File Offset: 0x00505D9C
		public static void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
			}
			TileLoader.DelegateKillTile[] hookKillTile = TileLoader.HookKillTile;
			for (int k = 0; k < hookKillTile.Length; k++)
			{
				hookKillTile[k](i, j, type, ref fail, ref effectOnly, ref noItem);
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x00507BE5 File Offset: 0x00505DE5
		public static void KillMultiTile(int i, int j, int frameX, int frameY, int type)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile == null)
			{
				return;
			}
			tile.KillMultiTile(i, j, frameX, frameY);
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00507BFC File Offset: 0x00505DFC
		public unsafe static bool CanExplode(int i, int j)
		{
			int type = (int)(*Main.tile[i, j].type);
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile != null && !modTile.CanExplode(i, j))
			{
				return false;
			}
			Func<int, int, int, bool>[] hookCanExplode = TileLoader.HookCanExplode;
			for (int k = 0; k < hookCanExplode.Length; k++)
			{
				if (!hookCanExplode[k](i, j, type))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00507C60 File Offset: 0x00505E60
		public static void NearbyEffects(int i, int j, int type, bool closer)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.NearbyEffects(i, j, closer);
			}
			Action<int, int, int, bool>[] hookNearbyEffects = TileLoader.HookNearbyEffects;
			for (int k = 0; k < hookNearbyEffects.Length; k++)
			{
				hookNearbyEffects[k](i, j, type, closer);
			}
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00507CA4 File Offset: 0x00505EA4
		public static void ModifyTorchLuck(Player player, ref float positiveLuck, ref float negativeLuck)
		{
			foreach (int type in player.NearbyModTorch)
			{
				float f = TileLoader.GetTile(type).GetTorchLuck(player);
				if (f > 0f)
				{
					positiveLuck += f;
				}
				else
				{
					negativeLuck += -f;
				}
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00507D14 File Offset: 0x00505F14
		public static void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
		{
			if (!Main.tileLighted[type])
			{
				return;
			}
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.ModifyLight(i, j, ref r, ref g, ref b);
			}
			TileLoader.DelegateModifyLight[] hookModifyLight = TileLoader.HookModifyLight;
			for (int k = 0; k < hookModifyLight.Length; k++)
			{
				hookModifyLight[k](i, j, type, ref r, ref g, ref b);
			}
		}

		/// <summary>
		/// Registers a tile type as having custom biome conversion code for this specific <see cref="T:Terraria.ID.BiomeConversionID" />. For modded tiles, you can directly use <see cref="M:Terraria.ModLoader.TileLoader.Convert(System.Int32,System.Int32,System.Int32)" /> <br />
		/// If you need to register conversions that rely on <see cref="T:Terraria.ID.TileID.Sets.Conversion" /> being fully populated, consider doing it in <see cref="M:Terraria.ModLoader.ModBiomeConversion.PostSetupContent" />
		/// </summary>
		/// <param name="tileType">The tile type that has is affected by this custom conversion.</param>
		/// <param name="conversionType">The conversion type for which the tile should use custom conversion code.</param>
		/// <param name="conversionDelegate">Code to run when the tile attempts to get converted. Return false to signal that your custom conversion took place and that vanilla code shouldn't be ran.</param>
		// Token: 0x0600278F RID: 10127 RVA: 0x00507D68 File Offset: 0x00505F68
		public static void RegisterConversion(int tileType, int conversionType, TileLoader.ConvertTile conversionDelegate)
		{
			if (TileLoader.tileConversionDelegates == null)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorCallDuringLoad", "TileLoader.RegisterConversion"));
			}
			List<TileLoader.ConvertTile>[][] array = TileLoader.tileConversionDelegates;
			List<TileLoader.ConvertTile>[] array2;
			if ((array2 = array[tileType]) == null)
			{
				array2 = (array[tileType] = new List<TileLoader.ConvertTile>[BiomeConversionLoader.BiomeConversionCount]);
			}
			List<TileLoader.ConvertTile>[] array3 = array2;
			List<TileLoader.ConvertTile> list;
			if ((list = array3[conversionType]) == null)
			{
				list = (array3[conversionType] = new List<TileLoader.ConvertTile>());
			}
			list.Add(conversionDelegate);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00507DCC File Offset: 0x00505FCC
		public unsafe static bool Convert(int i, int j, int conversionType)
		{
			int type = (int)(*Main.tile[i, j].type);
			List<TileLoader.ConvertTile>[] array = TileLoader.tileConversionDelegates[type];
			List<TileLoader.ConvertTile> list = (array != null) ? array[conversionType] : null;
			if (list != null)
			{
				Span<TileLoader.ConvertTile> span = CollectionsMarshal.AsSpan<TileLoader.ConvertTile>(list);
				for (int k = 0; k < span.Length; k++)
				{
					if (!(*span[k])(i, j, type, conversionType))
					{
						return false;
					}
				}
			}
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.Convert(i, j, conversionType);
			}
			return true;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x00507E4C File Offset: 0x0050604C
		public static bool? IsTileDangerous(int i, int j, int type, Player player)
		{
			bool? retVal = null;
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile != null && modTile.IsTileDangerous(i, j, player))
			{
				retVal = new bool?(true);
			}
			Func<int, int, int, Player, bool?>[] hookIsTileDangerous = TileLoader.HookIsTileDangerous;
			for (int k = 0; k < hookIsTileDangerous.Length; k++)
			{
				bool? globalRetVal = hookIsTileDangerous[k](i, j, type, player);
				if (globalRetVal != null)
				{
					if (!globalRetVal.Value)
					{
						return new bool?(false);
					}
					retVal = new bool?(true);
				}
			}
			return retVal;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x00507EC4 File Offset: 0x005060C4
		public static bool? IsTileBiomeSightable(int i, int j, int type, ref Color sightColor)
		{
			bool? retVal = null;
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile != null && modTile.IsTileBiomeSightable(i, j, ref sightColor))
			{
				retVal = new bool?(true);
			}
			TileLoader.DelegateIsTileBiomeSightable[] hookIsTileBiomeSightable = TileLoader.HookIsTileBiomeSightable;
			for (int k = 0; k < hookIsTileBiomeSightable.Length; k++)
			{
				bool? globalRetVal = hookIsTileBiomeSightable[k](i, j, type, ref sightColor);
				if (globalRetVal != null)
				{
					if (!globalRetVal.Value)
					{
						return new bool?(false);
					}
					retVal = new bool?(true);
				}
			}
			return retVal;
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x00507F3C File Offset: 0x0050613C
		public static bool? IsTileSpelunkable(int i, int j, int type)
		{
			bool? retVal = null;
			ModTile modTile = TileLoader.GetTile(type);
			if (!Main.tileSpelunker[type] && modTile != null && modTile.IsTileSpelunkable(i, j))
			{
				retVal = new bool?(true);
			}
			Func<int, int, int, bool?>[] hookIsTileSpelunkable = TileLoader.HookIsTileSpelunkable;
			for (int k = 0; k < hookIsTileSpelunkable.Length; k++)
			{
				bool? globalRetVal = hookIsTileSpelunkable[k](i, j, type);
				if (globalRetVal != null)
				{
					if (!globalRetVal.Value)
					{
						return new bool?(false);
					}
					retVal = new bool?(true);
				}
			}
			return retVal;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00507FBC File Offset: 0x005061BC
		public static void SetSpriteEffects(int i, int j, int type, ref SpriteEffects spriteEffects)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.SetSpriteEffects(i, j, ref spriteEffects);
			}
			TileLoader.DelegateSetSpriteEffects[] hookSetSpriteEffects = TileLoader.HookSetSpriteEffects;
			for (int k = 0; k < hookSetSpriteEffects.Length; k++)
			{
				hookSetSpriteEffects[k](i, j, type, ref spriteEffects);
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x00508000 File Offset: 0x00506200
		public unsafe static void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			Tile tile = Main.tile[i, j];
			if (*tile.type >= TileID.Count)
			{
				TileObjectData tileData = TileObjectData.GetTileData((int)(*tile.type), 0, 0);
				if (tileData != null)
				{
					int partY = 0;
					int remainingFrameY = (int)(*tile.frameY) % tileData.CoordinateFullHeight;
					while (partY + 1 < tileData.Height && remainingFrameY - tileData.CoordinateHeights[partY] - tileData.CoordinatePadding >= 0)
					{
						remainingFrameY -= tileData.CoordinateHeights[partY] + tileData.CoordinatePadding;
						partY++;
					}
					width = tileData.CoordinateWidth;
					offsetY = tileData.DrawYOffset;
					height = tileData.CoordinateHeights[partY];
				}
				TileLoader.GetTile((int)(*tile.type)).SetDrawPositions(i, j, ref width, ref offsetY, ref height, ref tileFrameX, ref tileFrameY);
			}
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x005080C0 File Offset: 0x005062C0
		public static void AnimateTiles()
		{
			if (TileLoader.loaded)
			{
				for (int i = 0; i < TileLoader.tiles.Count; i++)
				{
					ModTile modTile = TileLoader.tiles[i];
					modTile.AnimateTile(ref Main.tileFrame[(int)modTile.Type], ref Main.tileFrameCounter[(int)modTile.Type]);
				}
				Action[] hookAnimateTile = TileLoader.HookAnimateTile;
				for (int j = 0; j < hookAnimateTile.Length; j++)
				{
					hookAnimateTile[j]();
				}
			}
		}

		/// <summary>
		/// Sets the animation frame. Sets frameYOffset = modTile.animationFrameHeight * Main.tileFrame[type]; and then calls ModTile.AnimateIndividualTile
		/// </summary>
		/// <param name="type">The tile type.</param>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="frameXOffset">The offset to frameX.</param>
		/// <param name="frameYOffset">The offset to frameY.</param>
		// Token: 0x06002797 RID: 10135 RVA: 0x00508138 File Offset: 0x00506338
		public static void SetAnimationFrame(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile != null)
			{
				frameYOffset = modTile.AnimationFrameHeight * Main.tileFrame[type];
				modTile.AnimateIndividualTile(type, i, j, ref frameXOffset, ref frameYOffset);
			}
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x0050816C File Offset: 0x0050636C
		public static bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
		{
			Func<int, int, int, SpriteBatch, bool>[] hookPreDraw = TileLoader.HookPreDraw;
			for (int k = 0; k < hookPreDraw.Length; k++)
			{
				if (!hookPreDraw[k](i, j, type, spriteBatch))
				{
					return false;
				}
			}
			ModTile tile = TileLoader.GetTile(type);
			return tile == null || tile.PreDraw(i, j, spriteBatch);
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x005081B4 File Offset: 0x005063B4
		public static void DrawEffects(int i, int j, int type, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.DrawEffects(i, j, spriteBatch, ref drawData);
			}
			TileLoader.DelegateDrawEffects[] hookDrawEffects = TileLoader.HookDrawEffects;
			for (int k = 0; k < hookDrawEffects.Length; k++)
			{
				hookDrawEffects[k](i, j, type, spriteBatch, ref drawData);
			}
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x005081FC File Offset: 0x005063FC
		public static void EmitParticles(int i, int j, Tile tileCache, ushort typeCache, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
		{
			Action<int, int, Tile, ushort, short, short, Color, bool>[] hookEmitParticles = TileLoader.HookEmitParticles;
			for (int k = 0; k < hookEmitParticles.Length; k++)
			{
				hookEmitParticles[k](i, j, tileCache, typeCache, tileFrameX, tileFrameY, tileLight, visible);
			}
			ModTile tile = TileLoader.GetTile((int)typeCache);
			if (tile == null)
			{
				return;
			}
			tile.EmitParticles(i, j, tileCache, tileFrameX, tileFrameY, tileLight, visible);
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x0050824C File Offset: 0x0050644C
		public static void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.PostDraw(i, j, spriteBatch);
			}
			Action<int, int, int, SpriteBatch>[] hookPostDraw = TileLoader.HookPostDraw;
			for (int k = 0; k < hookPostDraw.Length; k++)
			{
				hookPostDraw[k](i, j, type, spriteBatch);
			}
		}

		/// <summary>
		/// Special Draw calls ModTile and GlobalTile SpecialDraw methods. Special Draw is called at the end of the DrawSpecialTilesLegacy loop, allowing for basically another layer above tiles. Use DrawEffects hook to queue for SpecialDraw.
		/// </summary>
		// Token: 0x0600279C RID: 10140 RVA: 0x00508290 File Offset: 0x00506490
		public static void SpecialDraw(int type, int specialTileX, int specialTileY, SpriteBatch spriteBatch)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.SpecialDraw(specialTileX, specialTileY, spriteBatch);
			}
			Action<int, int, int, SpriteBatch>[] hookSpecialDraw = TileLoader.HookSpecialDraw;
			for (int i = 0; i < hookSpecialDraw.Length; i++)
			{
				hookSpecialDraw[i](specialTileX, specialTileY, type, spriteBatch);
			}
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x005082D4 File Offset: 0x005064D4
		public static bool PreDrawPlacementPreview(int i, int j, int type, SpriteBatch spriteBatch, ref Rectangle frame, ref Vector2 position, ref Color color, bool validPlacement, ref SpriteEffects spriteEffects)
		{
			TileLoader.DelegatePreDrawPlacementPreview[] hookPreDrawPlacementPreview = TileLoader.HookPreDrawPlacementPreview;
			for (int k = 0; k < hookPreDrawPlacementPreview.Length; k++)
			{
				if (!hookPreDrawPlacementPreview[k](i, j, type, spriteBatch, ref frame, ref position, ref color, validPlacement, ref spriteEffects))
				{
					return false;
				}
			}
			ModTile tile = TileLoader.GetTile(type);
			return tile == null || tile.PreDrawPlacementPreview(i, j, spriteBatch, ref frame, ref position, ref color, validPlacement, ref spriteEffects);
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x00508330 File Offset: 0x00506530
		public static void PostDrawPlacementPreview(int i, int j, int type, SpriteBatch spriteBatch, Rectangle frame, Vector2 position, Color color, bool validPlacement, SpriteEffects spriteEffects)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.PostDrawPlacementPreview(i, j, spriteBatch, frame, position, color, validPlacement, spriteEffects);
			}
			Action<int, int, int, SpriteBatch, Rectangle, Vector2, Color, bool, SpriteEffects>[] hookPostDrawPlacementPreview = TileLoader.HookPostDrawPlacementPreview;
			for (int k = 0; k < hookPostDrawPlacementPreview.Length; k++)
			{
				hookPostDrawPlacementPreview[k](i, j, type, spriteBatch, frame, position, color, validPlacement, spriteEffects);
			}
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x00508388 File Offset: 0x00506588
		public static void RandomUpdate(int i, int j, int type)
		{
			if (!Main.tile[i, j].active())
			{
				return;
			}
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.RandomUpdate(i, j);
			}
			Action<int, int, int>[] hookRandomUpdate = TileLoader.HookRandomUpdate;
			for (int k = 0; k < hookRandomUpdate.Length; k++)
			{
				hookRandomUpdate[k](i, j, type);
			}
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x005083E0 File Offset: 0x005065E0
		public static bool TileFrame(int i, int j, int type, ref bool resetFrame, ref bool noBreak)
		{
			ModTile modTile = TileLoader.GetTile(type);
			bool flag = true;
			if (modTile != null)
			{
				flag = modTile.TileFrame(i, j, ref resetFrame, ref noBreak);
			}
			foreach (TileLoader.DelegateTileFrame hook in TileLoader.HookTileFrame)
			{
				flag &= hook(i, j, type, ref resetFrame, ref noBreak);
			}
			return flag;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00508430 File Offset: 0x00506630
		public static void PostTileFrame(int type, int i, int j, int up, int down, int left, int right, int upLeft, int upRight, int downLeft, int downRight)
		{
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile != null)
			{
				modTile.PostTileFrame(i, j, up, down, left, right, upLeft, upRight, downLeft, downRight);
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x00508460 File Offset: 0x00506660
		public static void ModifyFrameMerge(int type, int i, int j, ref int up, ref int down, ref int left, ref int right, ref int upLeft, ref int upRight, ref int downLeft, ref int downRight)
		{
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile != null)
			{
				modTile.ModifyFrameMerge(i, j, ref up, ref down, ref left, ref right, ref upLeft, ref upRight, ref downLeft, ref downRight);
			}
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x00508490 File Offset: 0x00506690
		public unsafe static void PickPowerCheck(Tile target, int pickPower, ref int damage)
		{
			ModTile modTile = TileLoader.GetTile((int)(*target.type));
			if (modTile != null && pickPower < modTile.MinPick)
			{
				damage = 0;
			}
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x005084BC File Offset: 0x005066BC
		public static bool CanPlace(int i, int j, int type)
		{
			Func<int, int, int, bool>[] hookCanPlace = TileLoader.HookCanPlace;
			for (int k = 0; k < hookCanPlace.Length; k++)
			{
				if (!hookCanPlace[k](i, j, type))
				{
					return false;
				}
			}
			ModTile tile = TileLoader.GetTile(type);
			return tile == null || tile.CanPlace(i, j);
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x00508500 File Offset: 0x00506700
		public static bool CanReplace(int i, int j, int type, int tileTypeBeingPlaced)
		{
			Func<int, int, int, int, bool>[] hookCanReplace = TileLoader.HookCanReplace;
			for (int k = 0; k < hookCanReplace.Length; k++)
			{
				if (!hookCanReplace[k](i, j, type, tileTypeBeingPlaced))
				{
					return false;
				}
			}
			ModTile tile = TileLoader.GetTile(type);
			return tile == null || tile.CanReplace(i, j, tileTypeBeingPlaced);
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x00508548 File Offset: 0x00506748
		public static void AdjTiles(Player player, int type)
		{
			ModTile modTile = TileLoader.GetTile(type);
			if (modTile != null)
			{
				foreach (int i in modTile.AdjTiles)
				{
					player.adjTile[i] = true;
				}
			}
			Func<int, int[]>[] hookAdjTiles = TileLoader.HookAdjTiles;
			for (int k = 0; k < hookAdjTiles.Length; k++)
			{
				foreach (int j in hookAdjTiles[k](type))
				{
					player.adjTile[j] = true;
				}
			}
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x005085C4 File Offset: 0x005067C4
		public unsafe static bool RightClick(int i, int j)
		{
			bool returnValue = false;
			int type = (int)(*Main.tile[i, j].type);
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null && tile.RightClick(i, j))
			{
				returnValue = true;
			}
			Action<int, int, int>[] hookRightClick = TileLoader.HookRightClick;
			for (int k = 0; k < hookRightClick.Length; k++)
			{
				hookRightClick[k](i, j, type);
			}
			return returnValue;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x00508628 File Offset: 0x00506828
		public unsafe static void MouseOver(int i, int j)
		{
			int type = (int)(*Main.tile[i, j].type);
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.MouseOver(i, j);
			}
			Action<int, int, int>[] hookMouseOver = TileLoader.HookMouseOver;
			for (int k = 0; k < hookMouseOver.Length; k++)
			{
				hookMouseOver[k](i, j, type);
			}
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x00508680 File Offset: 0x00506880
		public unsafe static void MouseOverFar(int i, int j)
		{
			int type = (int)(*Main.tile[i, j].type);
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.MouseOverFar(i, j);
			}
			Action<int, int, int>[] hookMouseOverFar = TileLoader.HookMouseOverFar;
			for (int k = 0; k < hookMouseOverFar.Length; k++)
			{
				hookMouseOverFar[k](i, j, type);
			}
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x005086D8 File Offset: 0x005068D8
		public unsafe static int AutoSelect(int i, int j, Player player)
		{
			if (!Main.tile[i, j].active())
			{
				return -1;
			}
			int type = (int)(*Main.tile[i, j].type);
			ModTile modTile = TileLoader.GetTile(type);
			for (int k = 0; k < 50; k++)
			{
				Item item = player.inventory[k];
				if (item.type != 0 && item.stack != 0)
				{
					if (modTile != null && modTile.AutoSelect(i, j, item))
					{
						return k;
					}
					Func<int, int, int, Item, bool>[] hookAutoSelect = TileLoader.HookAutoSelect;
					for (int l = 0; l < hookAutoSelect.Length; l++)
					{
						if (hookAutoSelect[l](i, j, type, item))
						{
							return k;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x00508784 File Offset: 0x00506984
		public static bool PreHitWire(int i, int j, int type)
		{
			Func<int, int, int, bool>[] hookPreHitWire = TileLoader.HookPreHitWire;
			for (int k = 0; k < hookPreHitWire.Length; k++)
			{
				if (!hookPreHitWire[k](i, j, type))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x005087B8 File Offset: 0x005069B8
		public static void HitWire(int i, int j, int type)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.HitWire(i, j);
			}
			Action<int, int, int>[] hookHitWire = TileLoader.HookHitWire;
			for (int k = 0; k < hookHitWire.Length; k++)
			{
				hookHitWire[k](i, j, type);
			}
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x005087F8 File Offset: 0x005069F8
		public static void FloorVisuals(int type, Player player)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.FloorVisuals(player);
			}
			Action<int, Player>[] hookFloorVisuals = TileLoader.HookFloorVisuals;
			for (int i = 0; i < hookFloorVisuals.Length; i++)
			{
				hookFloorVisuals[i](type, player);
			}
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x00508838 File Offset: 0x00506A38
		public static bool Slope(int i, int j, int type)
		{
			Func<int, int, int, bool>[] hookSlope = TileLoader.HookSlope;
			for (int k = 0; k < hookSlope.Length; k++)
			{
				if (!hookSlope[k](i, j, type))
				{
					return true;
				}
			}
			ModTile tile = TileLoader.GetTile(type);
			return tile != null && !tile.Slope(i, j);
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x0050887E File Offset: 0x00506A7E
		public static bool HasWalkDust(int type)
		{
			ModTile tile = TileLoader.GetTile(type);
			return tile != null && tile.HasWalkDust();
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x00508891 File Offset: 0x00506A91
		public static void WalkDust(int type, ref int dustType, ref bool makeDust, ref Color color)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile == null)
			{
				return;
			}
			tile.WalkDust(ref dustType, ref makeDust, ref color);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x005088A8 File Offset: 0x00506AA8
		public static void ChangeWaterfallStyle(int type, ref int style)
		{
			ModTile tile = TileLoader.GetTile(type);
			if (tile != null)
			{
				tile.ChangeWaterfallStyle(ref style);
			}
			TileLoader.DelegateChangeWaterfallStyle[] hookChangeWaterfallStyle = TileLoader.HookChangeWaterfallStyle;
			for (int i = 0; i < hookChangeWaterfallStyle.Length; i++)
			{
				hookChangeWaterfallStyle[i](type, ref style);
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x005088E8 File Offset: 0x00506AE8
		public static bool SaplingGrowthType(int soilType, ref int saplingType, ref int style)
		{
			int originalType = saplingType;
			int originalStyle = style;
			ModTree treeGrown = PlantLoader.Get<ModTree>(5, soilType);
			if (treeGrown == null)
			{
				ModPalmTree palmGrown = PlantLoader.Get<ModPalmTree>(323, soilType);
				if (palmGrown == null)
				{
					return false;
				}
				saplingType = palmGrown.SaplingGrowthType(ref style);
			}
			else
			{
				saplingType = treeGrown.SaplingGrowthType(ref style);
			}
			if (TileID.Sets.TreeSapling[saplingType])
			{
				return true;
			}
			saplingType = originalType;
			style = originalStyle;
			return false;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x0050893E File Offset: 0x00506B3E
		public static bool CanGrowModTree(int type)
		{
			return PlantLoader.Exists(5, type);
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x00508948 File Offset: 0x00506B48
		public unsafe static void TreeDust(Tile tile, ref int dust)
		{
			if (!tile.active())
			{
				return;
			}
			ModTree tree = PlantLoader.Get<ModTree>(5, (int)(*tile.type));
			if (tree != null)
			{
				dust = tree.CreateDust();
			}
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x0050897C File Offset: 0x00506B7C
		public static bool CanDropAcorn(int type)
		{
			ModTree tree = PlantLoader.Get<ModTree>(5, type);
			return tree != null && tree.CanDropAcorn();
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x0050899C File Offset: 0x00506B9C
		public static void DropTreeWood(int type, ref int wood)
		{
			ModTree tree = PlantLoader.Get<ModTree>(5, type);
			if (tree != null)
			{
				wood = tree.DropWood();
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x005089BC File Offset: 0x00506BBC
		public static bool CanGrowModPalmTree(int type)
		{
			return PlantLoader.Exists(323, type);
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x005089CC File Offset: 0x00506BCC
		public unsafe static void PalmTreeDust(Tile tile, ref int dust)
		{
			if (!tile.active())
			{
				return;
			}
			ModPalmTree tree = PlantLoader.Get<ModPalmTree>(323, (int)(*tile.type));
			if (tree != null)
			{
				dust = tree.CreateDust();
			}
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00508A04 File Offset: 0x00506C04
		public static void DropPalmTreeWood(int type, ref int wood)
		{
			ModPalmTree tree = PlantLoader.Get<ModPalmTree>(323, type);
			if (tree != null)
			{
				wood = tree.DropWood();
			}
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00508A28 File Offset: 0x00506C28
		public static bool CanGrowModCactus(int type)
		{
			return PlantLoader.Exists(80, type) || TileIO.Tiles.unloadedTypes.Contains((ushort)type);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x00508A48 File Offset: 0x00506C48
		public static Texture2D GetCactusTexture(int type)
		{
			ModCactus tree = PlantLoader.Get<ModCactus>(80, type);
			if (tree == null)
			{
				return null;
			}
			return tree.GetTexture().Value;
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x00508A70 File Offset: 0x00506C70
		public static void PlaceInWorld(int i, int j, Item item)
		{
			int type = item.createTile;
			if (type < 0)
			{
				return;
			}
			Action<int, int, int, Item>[] hookPlaceInWorld = TileLoader.HookPlaceInWorld;
			for (int k = 0; k < hookPlaceInWorld.Length; k++)
			{
				hookPlaceInWorld[k](i, j, type, item);
			}
			ModTile tile = TileLoader.GetTile(type);
			if (tile == null)
			{
				return;
			}
			tile.PlaceInWorld(i, j, item);
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00508ABC File Offset: 0x00506CBC
		public static void PostSetupTileMerge()
		{
			Action[] hookPostSetupTileMerge = TileLoader.HookPostSetupTileMerge;
			for (int i = 0; i < hookPostSetupTileMerge.Length; i++)
			{
				hookPostSetupTileMerge[i]();
			}
			foreach (ModTile modTile in TileLoader.tiles)
			{
				modTile.PostSetupTileMerge();
			}
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x00508B24 File Offset: 0x00506D24
		public static bool IsLockedChest(int i, int j, int type)
		{
			ModTile tile = TileLoader.GetTile(type);
			return tile != null && tile.IsLockedChest(i, j);
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x00508B39 File Offset: 0x00506D39
		public static bool UnlockChest(int i, int j, int type, ref short frameXAdjustment, ref int dustType, ref bool manual)
		{
			ModTile tile = TileLoader.GetTile(type);
			return tile != null && tile.UnlockChest(i, j, ref frameXAdjustment, ref dustType, ref manual);
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x00508B53 File Offset: 0x00506D53
		public static bool LockChest(int i, int j, int type, ref short frameXAdjustment, ref bool manual)
		{
			ModTile tile = TileLoader.GetTile(type);
			return tile != null && tile.LockChest(i, j, ref frameXAdjustment, ref manual);
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x00508B6C File Offset: 0x00506D6C
		public static void RecountTiles(SceneMetrics metrics)
		{
			metrics.HolyTileCount = (metrics.EvilTileCount = (metrics.BloodTileCount = (metrics.SnowTileCount = (metrics.JungleTileCount = (metrics.MushroomTileCount = (metrics.SandTileCount = (metrics.DungeonTileCount = 0)))))));
			for (int i = 0; i < TileLoader.TileCount; i++)
			{
				int tileCount = metrics._tileCounts[i];
				if (tileCount != 0)
				{
					metrics.HolyTileCount += tileCount * TileID.Sets.HallowBiome[i];
					metrics.SnowTileCount += tileCount * TileID.Sets.SnowBiome[i];
					metrics.MushroomTileCount += tileCount * TileID.Sets.MushroomBiome[i];
					metrics.SandTileCount += tileCount * TileID.Sets.SandBiome[i];
					metrics.DungeonTileCount += tileCount * TileID.Sets.DungeonBiome[i];
					int corrupt;
					int crimson;
					int jungle;
					if (!Main.remixWorld)
					{
						corrupt = TileID.Sets.CorruptBiome[i];
						crimson = TileID.Sets.CrimsonBiome[i];
						jungle = TileID.Sets.JungleBiome[i];
					}
					else
					{
						corrupt = TileID.Sets.RemixCorruptBiome[i];
						crimson = TileID.Sets.RemixCrimsonBiome[i];
						jungle = TileID.Sets.RemixJungleBiome[i];
					}
					metrics.EvilTileCount += tileCount * corrupt;
					metrics.BloodTileCount += tileCount * crimson;
					metrics.JungleTileCount += tileCount * jungle;
				}
			}
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x00508CE8 File Offset: 0x00506EE8
		internal static void FinishSetup()
		{
			for (int i = 0; i < ItemLoader.ItemCount; i++)
			{
				Item item = ContentSamples.ItemsByType[i];
				if (!ItemID.Sets.DisableAutomaticPlaceableDrop[i] && item.createTile > -1)
				{
					TileLoader.tileTypeAndTileStyleToItemType.TryAdd(new ValueTuple<int, int>(item.createTile, item.placeStyle), item.type);
				}
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x00508D48 File Offset: 0x00506F48
		public static bool GlobalShakeTree(int x, int y, TreeTypes treeType)
		{
			Action<int, int, TreeTypes>[] hookPreShakeTree = TileLoader.HookPreShakeTree;
			for (int i = 0; i < hookPreShakeTree.Length; i++)
			{
				hookPreShakeTree[i](x, y, treeType);
			}
			Func<int, int, TreeTypes, bool>[] hookShakeTree = TileLoader.HookShakeTree;
			for (int i = 0; i < hookShakeTree.Length; i++)
			{
				if (hookShakeTree[i](x, y, treeType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040018F8 RID: 6392
		private static int nextTile = (int)TileID.Count;

		// Token: 0x040018F9 RID: 6393
		internal static readonly IList<ModTile> tiles = new List<ModTile>();

		// Token: 0x040018FA RID: 6394
		internal static readonly IList<GlobalTile> globalTiles = new List<GlobalTile>();

		/// <summary> Maps Tile type and Tile style to the Item type that places the tile with the style. </summary>
		// Token: 0x040018FB RID: 6395
		internal static readonly Dictionary<ValueTuple<int, int>, int> tileTypeAndTileStyleToItemType = new Dictionary<ValueTuple<int, int>, int>();

		// Token: 0x040018FC RID: 6396
		internal static List<TileLoader.ConvertTile>[][] tileConversionDelegates = null;

		// Token: 0x040018FD RID: 6397
		private static bool loaded = false;

		// Token: 0x040018FE RID: 6398
		private static readonly int vanillaChairCount = TileID.Sets.RoomNeeds.CountsAsChair.Length;

		// Token: 0x040018FF RID: 6399
		private static readonly int vanillaTableCount = TileID.Sets.RoomNeeds.CountsAsTable.Length;

		// Token: 0x04001900 RID: 6400
		private static readonly int vanillaTorchCount = TileID.Sets.RoomNeeds.CountsAsTorch.Length;

		// Token: 0x04001901 RID: 6401
		private static readonly int vanillaDoorCount = TileID.Sets.RoomNeeds.CountsAsDoor.Length;

		// Token: 0x04001902 RID: 6402
		private static Func<int, int, int, bool, bool>[] HookKillSound;

		// Token: 0x04001903 RID: 6403
		private static TileLoader.DelegateNumDust[] HookNumDust;

		// Token: 0x04001904 RID: 6404
		private static TileLoader.DelegateCreateDust[] HookCreateDust;

		// Token: 0x04001905 RID: 6405
		private static TileLoader.DelegateDropCritterChance[] HookDropCritterChance;

		// Token: 0x04001906 RID: 6406
		private static Func<int, int, int, bool>[] HookCanDrop;

		// Token: 0x04001907 RID: 6407
		private static Action<int, int, int>[] HookDrop;

		// Token: 0x04001908 RID: 6408
		private static TileLoader.DelegateCanKillTile[] HookCanKillTile;

		// Token: 0x04001909 RID: 6409
		private static TileLoader.DelegateKillTile[] HookKillTile;

		// Token: 0x0400190A RID: 6410
		private static Func<int, int, int, bool>[] HookCanExplode;

		// Token: 0x0400190B RID: 6411
		private static Action<int, int, int, bool>[] HookNearbyEffects;

		// Token: 0x0400190C RID: 6412
		private static TileLoader.DelegateModifyLight[] HookModifyLight;

		// Token: 0x0400190D RID: 6413
		private static Func<int, int, int, Player, bool?>[] HookIsTileDangerous;

		// Token: 0x0400190E RID: 6414
		private static TileLoader.DelegateIsTileBiomeSightable[] HookIsTileBiomeSightable;

		// Token: 0x0400190F RID: 6415
		private static Func<int, int, int, bool?>[] HookIsTileSpelunkable;

		// Token: 0x04001910 RID: 6416
		private static TileLoader.DelegateSetSpriteEffects[] HookSetSpriteEffects;

		// Token: 0x04001911 RID: 6417
		private static Action[] HookAnimateTile;

		// Token: 0x04001912 RID: 6418
		private static Func<int, int, int, SpriteBatch, bool>[] HookPreDraw;

		// Token: 0x04001913 RID: 6419
		private static TileLoader.DelegateDrawEffects[] HookDrawEffects;

		// Token: 0x04001914 RID: 6420
		private static Action<int, int, Tile, ushort, short, short, Color, bool>[] HookEmitParticles;

		// Token: 0x04001915 RID: 6421
		private static Action<int, int, int, SpriteBatch>[] HookPostDraw;

		// Token: 0x04001916 RID: 6422
		private static Action<int, int, int, SpriteBatch>[] HookSpecialDraw;

		// Token: 0x04001917 RID: 6423
		private static TileLoader.DelegatePreDrawPlacementPreview[] HookPreDrawPlacementPreview;

		// Token: 0x04001918 RID: 6424
		private static Action<int, int, int, SpriteBatch, Rectangle, Vector2, Color, bool, SpriteEffects>[] HookPostDrawPlacementPreview;

		// Token: 0x04001919 RID: 6425
		private static Action<int, int, int>[] HookRandomUpdate;

		// Token: 0x0400191A RID: 6426
		private static TileLoader.DelegateTileFrame[] HookTileFrame;

		// Token: 0x0400191B RID: 6427
		private static Func<int, int, int, bool>[] HookCanPlace;

		// Token: 0x0400191C RID: 6428
		private static Func<int, int, int, int, bool>[] HookCanReplace;

		// Token: 0x0400191D RID: 6429
		private static Func<int, int[]>[] HookAdjTiles;

		// Token: 0x0400191E RID: 6430
		private static Action<int, int, int>[] HookRightClick;

		// Token: 0x0400191F RID: 6431
		private static Action<int, int, int>[] HookMouseOver;

		// Token: 0x04001920 RID: 6432
		private static Action<int, int, int>[] HookMouseOverFar;

		// Token: 0x04001921 RID: 6433
		private static Func<int, int, int, Item, bool>[] HookAutoSelect;

		// Token: 0x04001922 RID: 6434
		private static Func<int, int, int, bool>[] HookPreHitWire;

		// Token: 0x04001923 RID: 6435
		private static Action<int, int, int>[] HookHitWire;

		// Token: 0x04001924 RID: 6436
		private static Func<int, int, int, bool>[] HookSlope;

		// Token: 0x04001925 RID: 6437
		private static Action<int, Player>[] HookFloorVisuals;

		// Token: 0x04001926 RID: 6438
		private static TileLoader.DelegateChangeWaterfallStyle[] HookChangeWaterfallStyle;

		// Token: 0x04001927 RID: 6439
		private static Action<int, int, int, Item>[] HookPlaceInWorld;

		// Token: 0x04001928 RID: 6440
		private static Action[] HookPostSetupTileMerge;

		// Token: 0x04001929 RID: 6441
		private static Action<int, int, TreeTypes>[] HookPreShakeTree;

		// Token: 0x0400192A RID: 6442
		private static Func<int, int, TreeTypes, bool>[] HookShakeTree;

		// Token: 0x020009CF RID: 2511
		// (Invoke) Token: 0x06005651 RID: 22097
		public delegate bool ConvertTile(int i, int j, int type, int conversionType);

		// Token: 0x020009D0 RID: 2512
		// (Invoke) Token: 0x06005655 RID: 22101
		private delegate void DelegateNumDust(int i, int j, int type, bool fail, ref int num);

		// Token: 0x020009D1 RID: 2513
		// (Invoke) Token: 0x06005659 RID: 22105
		private delegate bool DelegateCreateDust(int i, int j, int type, ref int dustType);

		// Token: 0x020009D2 RID: 2514
		// (Invoke) Token: 0x0600565D RID: 22109
		private delegate void DelegateDropCritterChance(int i, int j, int type, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance);

		// Token: 0x020009D3 RID: 2515
		// (Invoke) Token: 0x06005661 RID: 22113
		private delegate bool DelegateCanKillTile(int i, int j, int type, ref bool blockDamaged);

		// Token: 0x020009D4 RID: 2516
		// (Invoke) Token: 0x06005665 RID: 22117
		private delegate void DelegateKillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem);

		// Token: 0x020009D5 RID: 2517
		// (Invoke) Token: 0x06005669 RID: 22121
		private delegate void DelegateModifyLight(int i, int j, int type, ref float r, ref float g, ref float b);

		// Token: 0x020009D6 RID: 2518
		// (Invoke) Token: 0x0600566D RID: 22125
		private delegate bool? DelegateIsTileBiomeSightable(int i, int j, int type, ref Color sightColor);

		// Token: 0x020009D7 RID: 2519
		// (Invoke) Token: 0x06005671 RID: 22129
		private delegate void DelegateSetSpriteEffects(int i, int j, int type, ref SpriteEffects spriteEffects);

		// Token: 0x020009D8 RID: 2520
		// (Invoke) Token: 0x06005675 RID: 22133
		private delegate void DelegateDrawEffects(int i, int j, int type, SpriteBatch spriteBatch, ref TileDrawInfo drawData);

		// Token: 0x020009D9 RID: 2521
		// (Invoke) Token: 0x06005679 RID: 22137
		private delegate bool DelegatePreDrawPlacementPreview(int i, int j, int type, SpriteBatch spriteBatch, ref Rectangle frame, ref Vector2 position, ref Color color, bool validPlacement, ref SpriteEffects spriteEffects);

		// Token: 0x020009DA RID: 2522
		// (Invoke) Token: 0x0600567D RID: 22141
		private delegate bool DelegateTileFrame(int i, int j, int type, ref bool resetFrame, ref bool noBreak);

		// Token: 0x020009DB RID: 2523
		// (Invoke) Token: 0x06005681 RID: 22145
		private delegate void DelegateChangeWaterfallStyle(int type, ref int style);
	}
}
