using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class allows you to modify the behavior of any wall in the game (although admittedly walls don't have much behavior).
	/// <br /> To use it, simply create a new class deriving from this one. Implementations will be registered automatically.
	/// </summary>
	// Token: 0x02000175 RID: 373
	public abstract class GlobalWall : GlobalBlockType
	{
		// Token: 0x06001DD9 RID: 7641 RVA: 0x004D4A6F File Offset: 0x004D2C6F
		protected sealed override void Register()
		{
			ModTypeLookup<GlobalWall>.Register(this);
			WallLoader.globalWalls.Add(this);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x004D4A82 File Offset: 0x004D2C82
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to customize which items the wall at the given coordinates drops. Return false to stop the game from dropping the wall's default item (the dropType parameter). Returns true by default.
		/// </summary>
		// Token: 0x06001DDB RID: 7643 RVA: 0x004D4A8A File Offset: 0x004D2C8A
		public virtual bool Drop(int i, int j, int type, ref int dropType)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine what happens when the wall at the given coordinates is killed or hit with a hammer. Fail determines whether the wall is mined (whether it is killed).
		/// </summary>
		// Token: 0x06001DDC RID: 7644 RVA: 0x004D4A8D File Offset: 0x004D2C8D
		public virtual void KillWall(int i, int j, int type, ref bool fail)
		{
		}

		/// <summary>
		/// Called for every wall that updates due to being placed or being next to a wall that is changed. Return false to stop the game from carrying out its default WallFrame operations. If you return false, make sure to set <see cref="P:Terraria.Tile.WallFrameNumber" />, <see cref="P:Terraria.Tile.WallFrameX" />, and <see cref="P:Terraria.Tile.WallFrameY" /> according to the your desired custom framing design. Returns true by default.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="type">Type of the wall being framed</param>
		/// <param name="randomizeFrame">True if the calling code intends that the frameNumber be randomly changed, such as when placing the wall initially or loading the world, but not when updating due to nearby tile or wall placements</param>
		/// <param name="style">The style or orientation that will be applied</param>
		/// <param name="frameNumber">The random style that will be applied</param>
		/// <returns></returns>
		// Token: 0x06001DDD RID: 7645 RVA: 0x004D4A8F File Offset: 0x004D2C8F
		public virtual bool WallFrame(int i, int j, int type, bool randomizeFrame, ref int style, ref int frameNumber)
		{
			return true;
		}

		/// <inheritdoc cref="!:ModPlayer.CanBeTeleportedTo(int, int, string)" />
		// Token: 0x06001DDE RID: 7646 RVA: 0x004D4A92 File Offset: 0x004D2C92
		public virtual bool CanBeTeleportedTo(int i, int j, int type, Player player, string context)
		{
			return true;
		}
	}
}
