using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class represents a type of wall that can be added by a mod. Only one instance of this class will ever exist for each type of wall that is added. Any hooks that are called will be called by the instance corresponding to the wall type.
	/// <br /><br /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Wall">Wall Guide</see> teaches the basics of making a modded wall.
	/// </summary>
	// Token: 0x020001D4 RID: 468
	public abstract class ModWall : ModBlockType
	{
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x004EA1A1 File Offset: 0x004E83A1
		public override string LocalizationCategory
		{
			get
			{
				return "Walls";
			}
		}

		/// <summary>
		/// Adds an entry to the minimap for this wall with the given color and display name. This should be called in SetDefaults.
		/// </summary>
		// Token: 0x060024AE RID: 9390 RVA: 0x004EA1A8 File Offset: 0x004E83A8
		public void AddMapEntry(Color color, LocalizedText name = null)
		{
			if (!MapLoader.initialized)
			{
				MapEntry entry = new MapEntry(color, name);
				if (!MapLoader.wallEntries.Keys.Contains(base.Type))
				{
					MapLoader.wallEntries[base.Type] = new List<MapEntry>();
				}
				MapLoader.wallEntries[base.Type].Add(entry);
			}
		}

		/// <summary>
		/// <inheritdoc cref="M:Terraria.ModLoader.ModWall.AddMapEntry(Microsoft.Xna.Framework.Color,Terraria.Localization.LocalizedText)" />
		/// <br /><br /> <b>Overload specific:</b> This overload has an additional <paramref name="nameFunc" /> parameter. This function will be used to dynamically adjust the hover text. The parameters for the function are the default display name, x-coordinate, and y-coordinate.
		/// </summary>
		// Token: 0x060024AF RID: 9391 RVA: 0x004EA208 File Offset: 0x004E8408
		public void AddMapEntry(Color color, LocalizedText name, Func<string, int, int, string> nameFunc)
		{
			if (!MapLoader.initialized)
			{
				MapEntry entry = new MapEntry(color, name, nameFunc);
				if (!MapLoader.wallEntries.Keys.Contains(base.Type))
				{
					MapLoader.wallEntries[base.Type] = new List<MapEntry>();
				}
				MapLoader.wallEntries[base.Type].Add(entry);
			}
		}

		/// <summary>
		/// Manually registers the item to drop for this wall.<br />
		/// Only necessary if there is no item which places this wall, such as with unsafe walls dropping safe variants. Otherwise, the item placing this wall will be dropped automatically.<br />
		/// Use <see cref="M:Terraria.ModLoader.ModWall.Drop(System.Int32,System.Int32,System.Int32@)" /> to conditionally prevent or change drops.
		/// </summary>
		/// <param name="itemType"></param>
		// Token: 0x060024B0 RID: 9392 RVA: 0x004EA268 File Offset: 0x004E8468
		public void RegisterItemDrop(int itemType)
		{
			WallLoader.wallTypeToItemType[(int)base.Type] = itemType;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x004EA27B File Offset: 0x004E847B
		protected sealed override void Register()
		{
			base.Type = (ushort)WallLoader.ReserveWallID();
			ModTypeLookup<ModWall>.Register(this);
			WallLoader.walls.Add(this);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x004EA29A File Offset: 0x004E849A
		public sealed override void SetupContent()
		{
			TextureAssets.Wall[(int)base.Type] = ModContent.Request<Texture2D>(this.Texture, 2);
			this.SetStaticDefaults();
			WallID.Search.Add(base.FullName, (int)base.Type);
		}

		/// <summary>
		/// Allows you to customize which items the wall at the given coordinates drops. Return false to stop the game from dropping the tile's default item (the type parameter). Returns true by default.
		/// <br /> The <paramref name="type" /> passed in is the item type of the loaded item with <see cref="F:Terraria.Item.createWall" /> matching the type of this Wall. If <see cref="M:Terraria.ModLoader.ModWall.RegisterItemDrop(System.Int32)" /> was used, that item will be passed in instead.
		/// </summary>
		// Token: 0x060024B3 RID: 9395 RVA: 0x004EA2D0 File Offset: 0x004E84D0
		public virtual bool Drop(int i, int j, ref int type)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine what happens when the tile at the given coordinates is killed or hit with a hammer. Fail determines whether the tile is mined (whether it is killed).
		/// </summary>
		// Token: 0x060024B4 RID: 9396 RVA: 0x004EA2D3 File Offset: 0x004E84D3
		public virtual void KillWall(int i, int j, ref bool fail)
		{
		}

		/// <summary>
		/// Allows you to animate your wall. Use frameCounter to keep track of how long the current frame has been active, and use frame to change the current frame. Walls are drawn every 4 frames.
		/// </summary>
		// Token: 0x060024B5 RID: 9397 RVA: 0x004EA2D5 File Offset: 0x004E84D5
		public virtual void AnimateWall(ref byte frame, ref byte frameCounter)
		{
		}

		/// <summary>
		/// Called whenever this wall updates due to being placed or being next to a wall that is changed. Return false to stop the game from carrying out its default WallFrame operations. If you return false, make sure to set <see cref="P:Terraria.Tile.WallFrameNumber" />, <see cref="P:Terraria.Tile.WallFrameX" />, and <see cref="P:Terraria.Tile.WallFrameY" /> according to the your desired custom framing design. Returns true by default.
		/// </summary>
		/// <param name="i">The x position in tile coordinates.</param>
		/// <param name="j">The y position in tile coordinates.</param>
		/// <param name="randomizeFrame">True if the calling code intends that the frameNumber be randomly changed, such as when placing the wall initially or loading the world, but not when updating due to nearby tile or wall placements</param>
		/// <param name="style">The style or orientation that will be applied</param>
		/// <param name="frameNumber">The random style that will be applied</param>
		// Token: 0x060024B6 RID: 9398 RVA: 0x004EA2D7 File Offset: 0x004E84D7
		public virtual bool WallFrame(int i, int j, bool randomizeFrame, ref int style, ref int frameNumber)
		{
			return true;
		}

		/// <inheritdoc cref="!:ModPlayer.CanBeTeleportedTo(int, int, string)" />
		// Token: 0x060024B7 RID: 9399 RVA: 0x004EA2DA File Offset: 0x004E84DA
		public virtual bool CanBeTeleportedTo(int i, int j, Player player, string context)
		{
			return true;
		}
	}
}
