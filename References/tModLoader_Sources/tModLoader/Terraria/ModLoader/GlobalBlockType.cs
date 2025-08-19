using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This is the superclass for GlobalTile and GlobalWall, combining common code
	/// </summary>
	// Token: 0x02000166 RID: 358
	public abstract class GlobalBlockType : ModType
	{
		// Token: 0x06001C5C RID: 7260 RVA: 0x004D369E File Offset: 0x004D189E
		internal GlobalBlockType()
		{
		}

		/// <summary>
		/// Allows you to customize which sound you want to play when the tile/wall at the given coordinates is hit. Return false to stop the game from playing its default sound for the tile/wall. Returns true by default.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="type"></param>
		/// <param name="fail">If true, the tile/wall is only partially damaged. If false, the tile/wall is fully destroyed.</param>
		/// <returns></returns>
		// Token: 0x06001C5D RID: 7261 RVA: 0x004D36A6 File Offset: 0x004D18A6
		public virtual bool KillSound(int i, int j, int type, bool fail)
		{
			return true;
		}

		/// <summary>
		/// Allows you to change how many dust particles are created when the tile/wall at the given coordinates is hit.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="type"></param>
		/// <param name="fail"></param>
		/// <param name="num"></param>
		// Token: 0x06001C5E RID: 7262 RVA: 0x004D36A9 File Offset: 0x004D18A9
		public virtual void NumDust(int i, int j, int type, bool fail, ref int num)
		{
		}

		/// <summary>
		/// Allows you to modify the default type of dust created when the tile/wall at the given coordinates is hit. Return false to stop the default dust (the dustType parameter) from being created. Returns true by default.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="type"></param>
		/// <param name="dustType"></param>
		/// <returns></returns>
		// Token: 0x06001C5F RID: 7263 RVA: 0x004D36AB File Offset: 0x004D18AB
		public virtual bool CreateDust(int i, int j, int type, ref int dustType)
		{
			return true;
		}

		/// <summary>
		/// Allows you to stop a tile/wall from being placed at the given coordinates. Return false to block the tile/wall from being placed. Returns true by default.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		// Token: 0x06001C60 RID: 7264 RVA: 0x004D36AE File Offset: 0x004D18AE
		public virtual bool CanPlace(int i, int j, int type)
		{
			return true;
		}

		/// <summary>
		/// Whether or not the tile/wall at the given coordinates can be killed by an explosion (ie. bombs). Returns true by default; return false to stop an explosion from destroying it.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		// Token: 0x06001C61 RID: 7265 RVA: 0x004D36B1 File Offset: 0x004D18B1
		public virtual bool CanExplode(int i, int j, int type)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things behind the tile/wall at the given coordinates. Return false to stop the game from drawing the tile/wall normally. Returns true by default.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="type"></param>
		/// <param name="spriteBatch"></param>
		/// <returns></returns>
		// Token: 0x06001C62 RID: 7266 RVA: 0x004D36B4 File Offset: 0x004D18B4
		public virtual bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of the tile/wall at the given coordinates. This can also be used to do things such as creating dust. Called on active tiles. See also ModSystem.PostDrawTiles.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="type"></param>
		/// <param name="spriteBatch"></param>
		// Token: 0x06001C63 RID: 7267 RVA: 0x004D36B7 File Offset: 0x004D18B7
		public virtual void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
		{
		}

		/// <summary>
		/// Called for every tile/wall the world randomly decides to update in a given tick. Useful for things such as growing or spreading.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="type"></param>
		// Token: 0x06001C64 RID: 7268 RVA: 0x004D36B9 File Offset: 0x004D18B9
		public virtual void RandomUpdate(int i, int j, int type)
		{
		}

		/// <summary>
		/// Allows you to do something when this tile/wall is placed. Called on the local Client and Single Player.
		/// </summary>
		/// <param name="i">The x position in tile coordinates. Equal to Player.tileTargetX</param>
		/// <param name="j">The y position in tile coordinates. Equal to Player.tileTargetY</param>
		/// <param name="type"></param>
		/// <param name="item">The item used to place this tile/wall.</param>
		// Token: 0x06001C65 RID: 7269 RVA: 0x004D36BB File Offset: 0x004D18BB
		public virtual void PlaceInWorld(int i, int j, int type, Item item)
		{
		}

		/// <summary>
		/// Allows you to determine how much light the block emits.
		/// If it is a tile, make sure you set Main.tileLighted[Type] to true in SetDefaults for this to work.
		/// If it is a wall, it can also let you light up the block in front of this wall.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="type"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		// Token: 0x06001C66 RID: 7270 RVA: 0x004D36BD File Offset: 0x004D18BD
		public virtual void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
		{
		}
	}
}
