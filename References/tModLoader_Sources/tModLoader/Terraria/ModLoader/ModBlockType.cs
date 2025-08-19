using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This is the superclass for ModTile and ModWall, combining common code
	/// </summary>
	// Token: 0x020001A1 RID: 417
	public abstract class ModBlockType : ModTexturedType, ILocalizedModType, IModType
	{
		/// <summary> The internal ID of this type of tile/wall. </summary>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x004E2A9D File Offset: 0x004E0C9D
		// (set) Token: 0x06002008 RID: 8200 RVA: 0x004E2AA5 File Offset: 0x004E0CA5
		public ushort Type { get; internal set; }

		/// <summary>
		/// The default style of sound made when this tile/wall is hit.<br />
		/// Defaults to SoundID.Dig, which is the sound used for tiles such as dirt and sand.
		/// </summary>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x004E2AAE File Offset: 0x004E0CAE
		// (set) Token: 0x0600200A RID: 8202 RVA: 0x004E2AB6 File Offset: 0x004E0CB6
		public SoundStyle? HitSound { get; set; } = new SoundStyle?(SoundID.Dig);

		/// <summary> The default type of dust made when this tile/wall is hit.
		/// <para /> Defaults to 0, which is <see cref="F:Terraria.ID.DustID.Dirt" />. To prevent spawning any hit dust, set this to -1 instead. </summary>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x004E2ABF File Offset: 0x004E0CBF
		// (set) Token: 0x0600200C RID: 8204 RVA: 0x004E2AC7 File Offset: 0x004E0CC7
		public int DustType { get; set; }

		/// <summary>
		/// The vanilla ID of what should replace the instance when a user unloads and subsequently deletes data from your mod in their save file.
		/// <br /><br /> <see cref="F:Terraria.Main.tileFrameImportant" /> tiles attempting to fallback to a vanilla <see cref="F:Terraria.Main.tileFrameImportant" /> tile need to match the layout (FrameX and FrameY values) of the fallback tile so that the resulting tiles aren't broken.
		/// <br /><br /> Also note that tiles with ModTileEntity won't be able to fallback to a working vanilla Tile+TileEntity. The user will have to mine and replace the tile to spawn the correct TileEntity.
		/// <br /><br /> Defaults to <see cref="F:Terraria.ID.TileID.Dirt" /> (0).
		/// </summary>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x004E2AD0 File Offset: 0x004E0CD0
		// (set) Token: 0x0600200E RID: 8206 RVA: 0x004E2AD8 File Offset: 0x004E0CD8
		public ushort VanillaFallbackOnModDeletion { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600200F RID: 8207
		public abstract string LocalizationCategory { get; }

		/// <summary>
		/// Legacy helper method for creating a localization sub-key MapEntry
		/// </summary>
		/// <returns></returns>
		// Token: 0x06002010 RID: 8208 RVA: 0x004E2AE1 File Offset: 0x004E0CE1
		public LocalizedText CreateMapEntryName()
		{
			return this.GetLocalization("MapEntry", new Func<string>(base.PrettyPrintName));
		}

		/// <summary>
		/// Allows you to modify the properties after initial loading has completed.
		/// <br /> This is where you would set the properties of this tile/wall. Many properties are stored as arrays throughout Terraria's code.
		/// <br /> For example:
		/// <list type="bullet">
		/// <item> Main.tileSolid[Type] = true; </item>
		/// <item> Main.tileSolidTop[Type] = true; </item>
		/// <item> Main.tileBrick[Type] = true; </item>
		/// <item> Main.tileBlockLight[Type] = true; </item>
		/// </list>
		/// </summary>
		// Token: 0x06002011 RID: 8209 RVA: 0x004E2AFA File Offset: 0x004E0CFA
		public override void SetStaticDefaults()
		{
		}

		/// <summary>
		/// Allows you to choose which minimap entry the tile/wall at the given coordinates will use. 0 is the first entry added by AddMapEntry, 1 is the second entry, etc. Returns 0 by default.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		// Token: 0x06002012 RID: 8210 RVA: 0x004E2AFC File Offset: 0x004E0CFC
		public virtual ushort GetMapOption(int i, int j)
		{
			return 0;
		}

		/// <summary>
		/// Allows you to customize which sound you want to play when the tile/wall at the given coordinates is hit. Return false to stop the game from playing its default sound for the tile/wall. Returns true by default.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="fail">If true, the tile/wall is only partially damaged. If false, the tile/wall is fully destroyed.</param>
		// Token: 0x06002013 RID: 8211 RVA: 0x004E2AFF File Offset: 0x004E0CFF
		public virtual bool KillSound(int i, int j, bool fail)
		{
			return true;
		}

		/// <summary>
		/// Allows you to change how many dust particles are created when the tile/wall at the given coordinates is hit.
		/// <para /> Use <see cref="M:Terraria.ModLoader.ModBlockType.CreateDust(System.Int32,System.Int32,System.Int32@)" /> to customize the dust spawned.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="fail">If true, the tile is spawning dust for reasons other than the tile actually being destroyed. Worms, projectiles, and other effects cause dust to spawn aside from the usual case of the tile breaking.</param>
		/// <param name="num">The number of dust that will be spawned by the calling code</param>
		// Token: 0x06002014 RID: 8212 RVA: 0x004E2B02 File Offset: 0x004E0D02
		public virtual void NumDust(int i, int j, bool fail, ref int num)
		{
		}

		/// <summary>
		/// Allows you to modify the default type of dust created when the tile/wall at the given coordinates is hit. Return false to stop the default dust (the type parameter) from being created. Returns true by default.
		/// <para /> The <paramref name="type" /> parameter defaults to <see cref="P:Terraria.ModLoader.ModBlockType.DustType" />.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">The dust type that will be spawned by the calling code</param>
		// Token: 0x06002015 RID: 8213 RVA: 0x004E2B04 File Offset: 0x004E0D04
		public virtual bool CreateDust(int i, int j, ref int type)
		{
			return true;
		}

		/// <summary>
		/// Allows you to stop this tile/wall from being placed at the given coordinates. This method is called on the local client.
		/// <para /> For tiles this is also checked during block replacement, but <see cref="M:Terraria.ModLoader.ModTile.CanReplace(System.Int32,System.Int32,System.Int32)" /> should be used for replace-specific logic.
		/// <para /> Return false to stop the tile/wall from being placed. Returns true by default.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		// Token: 0x06002016 RID: 8214 RVA: 0x004E2B07 File Offset: 0x004E0D07
		public virtual bool CanPlace(int i, int j)
		{
			return true;
		}

		/// <summary>
		/// Whether or not the tile/wall at the given coordinates can be killed by an explosion (ie. bombs). Returns true by default; return false to stop an explosion from destroying it.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		// Token: 0x06002017 RID: 8215 RVA: 0x004E2B0A File Offset: 0x004E0D0A
		public virtual bool CanExplode(int i, int j)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things behind the tile/wall at the given coordinates. Return false to stop the game from drawing the tile normally. Returns true by default.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="spriteBatch"></param>
		// Token: 0x06002018 RID: 8216 RVA: 0x004E2B0D File Offset: 0x004E0D0D
		public virtual bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of the tile/wall at the given coordinates. This can also be used to do things such as creating dust.<para />
		/// Note that this method will be called for tiles even when the tile is <see cref="P:Terraria.Tile.IsTileInvisible" /> due to Echo Coating. Use the <see cref="M:Terraria.GameContent.Drawing.TileDrawing.IsVisible(Terraria.Tile)" /> method to skip effects that shouldn't show when the tile is invisible. This method won't be called for invisible walls.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="spriteBatch"></param>
		// Token: 0x06002019 RID: 8217 RVA: 0x004E2B10 File Offset: 0x004E0D10
		public virtual void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
		}

		/// <summary>
		/// Called whenever the world randomly decides to update this tile/wall in a given tick. Useful for things such as growing or spreading.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		// Token: 0x0600201A RID: 8218 RVA: 0x004E2B12 File Offset: 0x004E0D12
		public virtual void RandomUpdate(int i, int j)
		{
		}

		/// <summary>
		/// Allows you to do something when this tile/wall is placed. Called on the local Client and Single Player.
		/// <para /> Note that the coordinates in this method account for the placement origin and are not necessarily the coordinates of the top left tile of a multi-tile.
		/// </summary>
		/// <param name="i">The x position in tile coordinates. Equal to Player.tileTargetX</param>
		/// <param name="j">The y position in tile coordinates. Equal to Player.tileTargetY</param>
		/// <param name="item">The item used to place this tile/wall.</param>
		// Token: 0x0600201B RID: 8219 RVA: 0x004E2B14 File Offset: 0x004E0D14
		public virtual void PlaceInWorld(int i, int j, Item item)
		{
		}

		/// <summary>
		/// Allows you to determine how much light this tile/wall emits.<br />
		/// If it is a tile, make sure you set Main.tileLighted[Type] to true in SetDefaults for this to work.<br />
		/// If it is a wall, it can also let you light up the block in front of this wall.<br />
		/// See <see cref="M:Terraria.Graphics.Light.TileLightScanner.ApplyTileLight(Terraria.Tile,System.Int32,System.Int32,Terraria.Utilities.FastRandom@,Microsoft.Xna.Framework.Vector3@)" /> for vanilla tile light values to use as a reference.<br />
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="r">The red component of light, usually a value between 0 and 1</param>
		/// <param name="g">The green component of light, usually a value between 0 and 1</param>
		/// <param name="b">The blue component of light, usually a value between 0 and 1</param>
		// Token: 0x0600201C RID: 8220 RVA: 0x004E2B16 File Offset: 0x004E0D16
		public virtual void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
		}

		/// <summary>
		/// Allows you to change what happens when this tile/wall is converted into another biome. If you need to override or add a new conversion to a vailla tile, use <see cref="M:Terraria.ModLoader.TileLoader.RegisterConversion(System.Int32,System.Int32,Terraria.ModLoader.TileLoader.ConvertTile)" /> and <see cref="M:Terraria.ModLoader.WallLoader.RegisterConversion(System.Int32,System.Int32,Terraria.ModLoader.WallLoader.ConvertWall)" />.
		/// <para /> Purification powder uses a separate conversionType, as it doesn't convert hallowed tiles back to purity tiles. Be sure to check for <see cref="F:Terraria.ID.BiomeConversionID.PurificationPowder" /> as well as <see cref="F:Terraria.ID.BiomeConversionID.Purity" /> when handling corruption/crimson tiles.
		/// <para /> You can use <see cref="M:Terraria.WorldGen.ConvertTile(System.Int32,System.Int32,System.Int32,System.Boolean)" /> or <see cref="M:Terraria.WorldGen.ConvertWall(System.Int32,System.Int32,System.Int32)" /> to automatically handle tile framing and multiplayer syncing.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="conversionType">The <see cref="T:Terraria.ID.BiomeConversionID" /> of the conversion</param>
		// Token: 0x0600201D RID: 8221 RVA: 0x004E2B18 File Offset: 0x004E0D18
		public virtual void Convert(int i, int j, int conversionType)
		{
		}
	}
}
