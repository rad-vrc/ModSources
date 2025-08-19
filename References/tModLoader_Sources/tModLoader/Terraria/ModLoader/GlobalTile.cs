using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Enums;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class allows you to modify the behavior of any tile in the game, both vanilla and modded.
	/// <br /> To use it, simply create a new class deriving from this one. Implementations will be registered automatically.
	/// </summary>
	// Token: 0x02000172 RID: 370
	public abstract class GlobalTile : GlobalBlockType
	{
		/// <summary>
		/// A convenient method for adding an integer to the end of an array. This can be used with the arrays in TileID.Sets.RoomNeeds.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="type"></param>
		// Token: 0x06001D9F RID: 7583 RVA: 0x004D4501 File Offset: 0x004D2701
		public void AddToArray(ref int[] array, int type)
		{
			Array.Resize<int>(ref array, array.Length + 1);
			array[array.Length - 1] = type;
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x004D4519 File Offset: 0x004D2719
		protected sealed override void Register()
		{
			ModTypeLookup<GlobalTile>.Register(this);
			TileLoader.globalTiles.Add(this);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x004D452C File Offset: 0x004D272C
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to modify the chance the tile at the given coordinates has of spawning a certain critter when the tile is killed.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The tile type</param>
		/// <param name="wormChance">Chance for a worm to spawn. Value corresponds to a chance of 1 in X. Vanilla values include: Grass-400, Plants-200, Various Piles-6</param>
		/// <param name="grassHopperChance">Chance for a grass hopper to spawn. Value corresponds to a chance of 1 in X. Vanilla values include: Grass-100, Plants-50</param>
		/// <param name="jungleGrubChance">Chance for a jungle grub to spawn. Value corresponds to a chance of 1 in X. Vanilla values include: JungleVines-250, JunglePlants2-40, PlantDetritus-10</param>
		// Token: 0x06001DA2 RID: 7586 RVA: 0x004D4534 File Offset: 0x004D2734
		public virtual void DropCritterChance(int i, int j, int type, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance)
		{
		}

		/// <summary>
		/// Allows prevention of item drops from the tile dropping at the given coordinates. Return false to stop the game from dropping the tile's item(s). Returns true by default. Use <see cref="M:Terraria.ModLoader.GlobalTile.Drop(System.Int32,System.Int32,System.Int32)" /> to spawn additional items.
		/// </summary>
		// Token: 0x06001DA3 RID: 7587 RVA: 0x004D4536 File Offset: 0x004D2736
		public virtual bool CanDrop(int i, int j, int type)
		{
			return true;
		}

		/// <summary>
		/// Allows you to spawn additional items when the tile at the given coordinates drops.
		/// <br /> This hook is called once for multi-tiles. Trees or Cactus call this method for every individual tile.
		/// <br /> For multi-tiles, the coordinates correspond to the tile that triggered this multi-tile to drop, so if checking <see cref="P:Terraria.Tile.TileFrameX" /> and <see cref="P:Terraria.Tile.TileFrameY" />, be aware that the coordinates won't necessarily be the top left corner or origin of the multi-tile. Also be aware that some parts of the multi-tile might already be mined out when this method is called, so any math to determine tile style should be done on the tile at the coordinates passed in.
		/// </summary>
		// Token: 0x06001DA4 RID: 7588 RVA: 0x004D4539 File Offset: 0x004D2739
		public virtual void Drop(int i, int j, int type)
		{
		}

		/// <summary>
		/// Allows you to determine whether or not the tile at the given coordinates can be hit by anything. Returns true by default. blockDamaged currently has no use.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The tile type</param>
		/// <param name="blockDamaged"></param>
		/// <returns></returns>
		// Token: 0x06001DA5 RID: 7589 RVA: 0x004D453B File Offset: 0x004D273B
		public virtual bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine what happens when the tile at the given coordinates is killed or hit with a pickaxe. If <paramref name="fail" /> is true, the tile will not be mined; <paramref name="effectOnly" /> makes it so that only dust is created; <paramref name="noItem" /> stops items from dropping.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The tile type</param>
		/// <param name="fail">If true, the tile won't be mined</param>
		/// <param name="effectOnly">If true, only the dust visuals will happen</param>
		/// <param name="noItem">If true, the corresponding item won't drop</param>
		// Token: 0x06001DA6 RID: 7590 RVA: 0x004D453E File Offset: 0x004D273E
		public virtual void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModTile.NearbyEffects(System.Int32,System.Int32,System.Boolean)" />
		// Token: 0x06001DA7 RID: 7591 RVA: 0x004D4540 File Offset: 0x004D2740
		public virtual void NearbyEffects(int i, int j, int type, bool closer)
		{
		}

		/// <summary>
		/// Allows you to determine whether this tile glows red when the given player has the Dangersense buff.
		/// <br />Return true to force this behavior, or false to prevent it, overriding vanilla conditions. Returns null by default.
		/// <br />This is only called on the local client.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The tile type</param>
		/// <param name="player">Main.LocalPlayer</param>
		// Token: 0x06001DA8 RID: 7592 RVA: 0x004D4544 File Offset: 0x004D2744
		public virtual bool? IsTileDangerous(int i, int j, int type, Player player)
		{
			return null;
		}

		/// <summary>
		/// Allows you to customize whether this tile glows <paramref name="sightColor" /> while the local player has the <see href="https://terraria.wiki.gg/wiki/Biome_Sight_Potion">Biome Sight buff</see>.
		/// <br />Return true to force this behavior, or false to prevent it, overriding vanilla conditions and colors. Returns null by default. 
		/// <br />This is only called on the local client.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The tile type</param>
		/// <param name="sightColor">The color this tile should glow with, which defaults to <see cref="P:Microsoft.Xna.Framework.Color.White" />.</param>
		// Token: 0x06001DA9 RID: 7593 RVA: 0x004D455C File Offset: 0x004D275C
		public virtual bool? IsTileBiomeSightable(int i, int j, int type, ref Color sightColor)
		{
			return null;
		}

		/// <summary>
		/// Allows you to customize whether this tile can glow yellow while having the Spelunker buff, and is also detected by various pets.
		/// <br />Return true to force this behavior, or false to prevent it, overriding vanilla conditions. Returns null by default.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The tile type</param>
		// Token: 0x06001DAA RID: 7594 RVA: 0x004D4574 File Offset: 0x004D2774
		public virtual bool? IsTileSpelunkable(int i, int j, int type)
		{
			return null;
		}

		/// <summary>
		/// Allows you to determine whether or not a tile will draw itself flipped in the world.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The tile type</param>
		/// <param name="spriteEffects"></param>
		// Token: 0x06001DAB RID: 7595 RVA: 0x004D458A File Offset: 0x004D278A
		public virtual void SetSpriteEffects(int i, int j, int type, ref SpriteEffects spriteEffects)
		{
		}

		/// <summary>
		/// Allows animating tiles that were previously static. Loading a new texture for the tile is required first. Use Main.tileFrameCounter to count game frames and Main.tileFrame to change animation frames.
		/// </summary>
		// Token: 0x06001DAC RID: 7596 RVA: 0x004D458C File Offset: 0x004D278C
		public virtual void AnimateTile()
		{
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModTile.DrawEffects(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.DataStructures.TileDrawInfo@)" />
		// Token: 0x06001DAD RID: 7597 RVA: 0x004D458E File Offset: 0x004D278E
		public virtual void DrawEffects(int i, int j, int type, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
		{
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModTile.EmitParticles(System.Int32,System.Int32,Terraria.Tile,System.Int16,System.Int16,Microsoft.Xna.Framework.Color,System.Boolean)" />
		// Token: 0x06001DAE RID: 7598 RVA: 0x004D4590 File Offset: 0x004D2790
		public virtual void EmitParticles(int i, int j, Tile tileCache, ushort typeCache, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
		{
		}

		/// <summary>
		/// Special Draw. Only called if coordinates are added using Main.instance.TilesRenderer.AddSpecialLegacyPoint during DrawEffects. Useful for drawing things that would otherwise be impossible to draw due to draw order, such as items in item frames.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The Tile type of the tile being drawn</param>
		/// <param name="spriteBatch">The SpriteBatch that should be used for all draw calls</param>
		// Token: 0x06001DAF RID: 7599 RVA: 0x004D4592 File Offset: 0x004D2792
		public virtual void SpecialDraw(int i, int j, int type, SpriteBatch spriteBatch)
		{
		}

		/// <summary>
		/// Allows you to draw behind this multi-tile's regular placement preview rendering, or change relevant drawing parameters. This is ran for each rendered section of the multi-tile.
		/// <br /><br /> Make sure to use <paramref name="frame" /> for logic rather than the TileFrameX/Y values of the tile at the provided coordinates, this tile isn't placed yet.
		/// <br /><br /> Return false to stop this section from drawing normally. Returns true by default.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The Tile type of the preview that will be drawn.</param>
		/// <param name="spriteBatch"></param>
		/// <param name="frame">The source rectangle that this section will use for rendering.</param>
		/// <param name="position">The position at which this section will be drawn.</param>
		/// <param name="color">The color with which this section will be drawn. This is red when overlapping with another tile.</param>
		/// <param name="validPlacement">Indicates if the tile can occupy this location.</param>
		/// <param name="spriteEffects">The <see cref="T:Microsoft.Xna.Framework.Graphics.SpriteEffects" /> that will be used to draw this section.</param>
		// Token: 0x06001DB0 RID: 7600 RVA: 0x004D4594 File Offset: 0x004D2794
		public virtual bool PreDrawPlacementPreview(int i, int j, int type, SpriteBatch spriteBatch, ref Rectangle frame, ref Vector2 position, ref Color color, bool validPlacement, ref SpriteEffects spriteEffects)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw in front of this multi-tile's placement preview rendering. This is ran for each rendered section of the multi-tile.
		/// <br /><br /> Make sure to use <paramref name="frame" /> for logic rather than the TileFrameX/Y values of the tile at the provided coordinates, this tile isn't placed yet.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The Tile type of the preview that was drawn.</param>
		/// <param name="spriteBatch"></param>
		/// <param name="frame">The source rectangle that was used for rendering this section.</param>
		/// <param name="position">The position at which this section was drawn.</param>
		/// <param name="color">The color with which this section was drawn.</param>
		/// <param name="validPlacement">Indicates if the tile can occupy this location.</param>
		/// <param name="spriteEffects">The <see cref="T:Microsoft.Xna.Framework.Graphics.SpriteEffects" /> that were used to draw this section.</param>
		// Token: 0x06001DB1 RID: 7601 RVA: 0x004D4597 File Offset: 0x004D2797
		public virtual void PostDrawPlacementPreview(int i, int j, int type, SpriteBatch spriteBatch, Rectangle frame, Vector2 position, Color color, bool validPlacement, SpriteEffects spriteEffects)
		{
		}

		/// <summary>
		/// Called for every tile that updates due to being placed or being next to a tile that is changed. Return false to stop the game from carrying out its default TileFrame operations. Returns true by default.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type"></param>
		/// <param name="resetFrame"></param>
		/// <param name="noBreak"></param>
		/// <returns></returns>
		// Token: 0x06001DB2 RID: 7602 RVA: 0x004D4599 File Offset: 0x004D2799
		public virtual bool TileFrame(int i, int j, int type, ref bool resetFrame, ref bool noBreak)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine which tiles the given tile type can be considered as when looking for crafting stations.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		// Token: 0x06001DB3 RID: 7603 RVA: 0x004D459C File Offset: 0x004D279C
		public virtual int[] AdjTiles(int type)
		{
			return new int[0];
		}

		/// <summary>
		/// Allows you to make something happen when any tile is right-clicked by the player.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type"></param>
		// Token: 0x06001DB4 RID: 7604 RVA: 0x004D45A4 File Offset: 0x004D27A4
		public virtual void RightClick(int i, int j, int type)
		{
		}

		/// <summary>
		/// Allows you to make something happen when the mouse hovers over any tile. Useful for showing item icons or text on the mouse.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type"></param>
		// Token: 0x06001DB5 RID: 7605 RVA: 0x004D45A6 File Offset: 0x004D27A6
		public virtual void MouseOver(int i, int j, int type)
		{
		}

		/// <summary>
		/// Allows you to make something happen when the mouse hovers over any tile, even when the player is far away. Useful for showing what's written on signs, etc.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type"></param>
		// Token: 0x06001DB6 RID: 7606 RVA: 0x004D45A8 File Offset: 0x004D27A8
		public virtual void MouseOverFar(int i, int j, int type)
		{
		}

		/// <summary>
		/// Allows you to determine whether the given item can become selected when the cursor is hovering over a tile and the auto selection keybind is pressed.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		// Token: 0x06001DB7 RID: 7607 RVA: 0x004D45AA File Offset: 0x004D27AA
		public virtual bool AutoSelect(int i, int j, int type, Item item)
		{
			return false;
		}

		/// <summary>
		/// Whether or not the vanilla HitWire code and the HitWire hook is allowed to run. Useful for overriding vanilla behavior by returning false. Returns true by default.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type"></param>
		/// <returns></returns>
		// Token: 0x06001DB8 RID: 7608 RVA: 0x004D45AD File Offset: 0x004D27AD
		public virtual bool PreHitWire(int i, int j, int type)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make something happen when a wire current passes through any tile. Both <see cref="M:Terraria.Wiring.SkipWire(System.Int32,System.Int32)" /> and <see cref="M:Terraria.NetMessage.SendTileSquare(System.Int32,System.Int32,System.Int32,System.Int32,Terraria.ID.TileChangeType)" /> are usually required in the logic used in this method to correctly work.
		/// <br />Only called on the server and single player. All wiring happens on the world, not multiplayer clients. 
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type"></param>
		// Token: 0x06001DB9 RID: 7609 RVA: 0x004D45B0 File Offset: 0x004D27B0
		public virtual void HitWire(int i, int j, int type)
		{
		}

		/// <summary>
		/// Allows you to control how hammers slope any tile. Return true to allow the tile to slope normally. Returns true by default. Called on the local Client and Single Player.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type"></param>
		/// <returns></returns>
		// Token: 0x06001DBA RID: 7610 RVA: 0x004D45B2 File Offset: 0x004D27B2
		public virtual bool Slope(int i, int j, int type)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make something happen when a player stands on the given type of tile. For example, you can make the player slide as if on ice.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="player"></param>
		// Token: 0x06001DBB RID: 7611 RVA: 0x004D45B5 File Offset: 0x004D27B5
		public virtual void FloorVisuals(int type, Player player)
		{
		}

		/// <summary>
		/// Allows you to change the style of waterfall that passes through or over any tile.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="style"></param>
		// Token: 0x06001DBC RID: 7612 RVA: 0x004D45B7 File Offset: 0x004D27B7
		public virtual void ChangeWaterfallStyle(int type, ref int style)
		{
		}

		/// <summary>
		/// Allows you to stop a tile at the given coordinates from being replaced via the block swap feature. The tileTypeBeingPlaced parameter is the tile type that will replace the current tile. The type parameter is the tile type currently at the coordinates.
		/// <br /> This method is called on the local client. This method is only called if the local player has sufficient pickaxe power to mine the existing tile.
		/// <br /> Return false to block the tile from being replaced. Returns true by default.
		/// <br /> Use this for dynamic logic. <see cref="F:Terraria.ID.TileID.Sets.DoesntGetReplacedWithTileReplacement" />, <see cref="F:Terraria.ID.TileID.Sets.DoesntPlaceWithTileReplacement" />, and <see cref="F:Terraria.ID.TileID.Sets.PreventsTileReplaceIfOnTopOfIt" /> cover the most common use cases and should be used instead if possible.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="type"></param>
		/// <param name="tileTypeBeingPlaced"></param>
		/// <returns></returns>
		// Token: 0x06001DBD RID: 7613 RVA: 0x004D45B9 File Offset: 0x004D27B9
		public virtual bool CanReplace(int i, int j, int type, int tileTypeBeingPlaced)
		{
			return true;
		}

		/// <summary>
		/// Can be used to adjust tile merge related things that are not possible to do in <see cref="M:Terraria.ModLoader.ModBlockType.SetStaticDefaults" /> due to timing.
		/// </summary>
		// Token: 0x06001DBE RID: 7614 RVA: 0x004D45BC File Offset: 0x004D27BC
		public virtual void PostSetupTileMerge()
		{
		}

		/// <summary>
		/// This hook runs before <see cref="M:Terraria.ModLoader.GlobalTile.ShakeTree(System.Int32,System.Int32,Terraria.Enums.TreeTypes)" /> and is intended to be used to spawn bonus tree shaking drops and prevent existing drops using <see cref="F:Terraria.ModLoader.NPCLoader.blockLoot" />.
		/// <para /> The tile coordinates provided indicates the leafy top of the tree where entities should be spawned.
		/// <para /> Runs on the server or singleplayer.
		/// </summary>
		/// <param name="x">The x tile coordinate of the tree.</param>
		/// <param name="y">The y tile coordinate of the top of the tree.</param>
		/// <param name="treeType">The type of tree that is being shaken. Modded trees will be <see cref="F:Terraria.Enums.TreeTypes.Custom" /> by default.</param>
		// Token: 0x06001DBF RID: 7615 RVA: 0x004D45BE File Offset: 0x004D27BE
		public virtual void PreShakeTree(int x, int y, TreeTypes treeType)
		{
		}

		/// <summary>
		/// This hook runs when any tree is shaken (See the <see href="https://terraria.wiki.gg/wiki/Trees#Shaking">Tree Shaking wiki page</see>). It is intended to be used to drop the primary item (or NPC). Use <see cref="M:Terraria.ModLoader.GlobalTile.PreShakeTree(System.Int32,System.Int32,Terraria.Enums.TreeTypes)" /> to implement bonus drops instead, since this method isn't guaranteed to be called if another mod rolls an item drop.
		/// <para /> If a drop happens, return true to signify that a primary drop has been spawned and to prevent other mods and vanilla code from also attempting to drop the primary item. When spawning the drop be sure to use <see cref="T:Terraria.DataStructures.EntitySource_ShakeTree" /> as the source.
		/// <para /> The tile coordinates provided indicates the leafy top of the tree where entities should be spawned.
		/// <para /> Returns false by default. Runs on the server or singleplayer.
		/// </summary>
		/// <param name="x">The x tile coordinate of the tree.</param>
		/// <param name="y">The y tile coordinate of the top of the tree.</param>
		/// <param name="treeType">The type of tree that is being shaken. Modded trees will be <see cref="F:Terraria.Enums.TreeTypes.Custom" /> by default.</param>
		// Token: 0x06001DC0 RID: 7616 RVA: 0x004D45C0 File Offset: 0x004D27C0
		public virtual bool ShakeTree(int x, int y, TreeTypes treeType)
		{
			return false;
		}
	}
}
