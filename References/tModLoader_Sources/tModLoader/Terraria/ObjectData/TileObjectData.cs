using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using log4net;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.Modules;

namespace Terraria.ObjectData
{
	/// <summary>
	/// Contains tile properties for multitiles dictating how they are placed and how they behave. Multitiles are non-terrain tiles.
	/// <para /> TileObjectData supports alternate placements as well as tile style-specific behaviors. 
	/// <para /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#tileobjectdata-or-frameimportantmultitiles">TileObjectData section of the Basic Tile wiki guide</see> has visuals and guides teaching how to use the TileObjectData system.
	/// </summary>
	// Token: 0x0200011B RID: 283
	public class TileObjectData
	{
		/// <summary>
		/// Call on <see cref="F:Terraria.ObjectData.TileObjectData.newSubTile" /> only. If set to true, this subtile will be populated with its own copy of the <see cref="P:Terraria.ObjectData.TileObjectData.Alternates" /> data. This will allow changes made to this subtile to apply to alternate placements of this tile style as well.
		/// <para /> This must be set to true after <c>TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);</c> and before adjusting any other properties. This should always be set for any subtile of a tile that has alternate placements.
		/// <para /> Defaults to false.
		/// </summary>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x004BFC7F File Offset: 0x004BDE7F
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x004BFC87 File Offset: 0x004BDE87
		public bool LinkedAlternates
		{
			get
			{
				return this._linkedAlternates;
			}
			set
			{
				this.WriteCheck();
				if (value && !this._hasOwnAlternates)
				{
					this._hasOwnAlternates = true;
					this._alternates = new TileObjectAlternatesModule(this._alternates);
				}
				this._linkedAlternates = value;
			}
		}

		/// <summary>
		/// If true, this tile will use anchors to decide if the tile can be placed, otherwise it will fallback to being placed like a terrain tile. Aside from platforms and torches, all multitiles set this to true. 
		/// <para /> Defaults to false, but most template styles set this to true. If not copying from a template style or existing tile <b>be sure to set this to true</b>.
		/// </summary>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x004BFCB9 File Offset: 0x004BDEB9
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x004BFCC1 File Offset: 0x004BDEC1
		public bool UsesCustomCanPlace
		{
			get
			{
				return this._usesCustomCanPlace;
			}
			set
			{
				this.WriteCheck();
				this._usesCustomCanPlace = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x004BFCD0 File Offset: 0x004BDED0
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x004BFCF0 File Offset: 0x004BDEF0
		internal List<TileObjectData> Alternates
		{
			get
			{
				if (this._alternates == null)
				{
					return TileObjectData._baseObject.Alternates;
				}
				return this._alternates.data;
			}
			set
			{
				if (!this._hasOwnAlternates)
				{
					this._hasOwnAlternates = true;
					this._alternates = new TileObjectAlternatesModule(this._alternates);
				}
				this._alternates.data = value;
			}
		}

		/// <summary>
		/// Defines the anchors needed above this tile. Defaults to null.
		/// <para /> Read the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#anchorbottomanchorleftanchorrightanchortop">Anchor section of the Basic Tile Guide on the wiki</see> for more information.
		/// </summary>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x004BFD1E File Offset: 0x004BDF1E
		// (set) Token: 0x0600198F RID: 6543 RVA: 0x004BFD40 File Offset: 0x004BDF40
		public AnchorData AnchorTop
		{
			get
			{
				if (this._anchor == null)
				{
					return TileObjectData._baseObject.AnchorTop;
				}
				return this._anchor.top;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnAnchor)
				{
					if (this._anchor.top == value)
					{
						return;
					}
					this._hasOwnAnchor = true;
					this._anchor = new AnchorDataModule(this._anchor);
				}
				this._anchor.top = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].AnchorTop = value;
					}
				}
			}
		}

		/// <summary>
		/// Defines the anchors needed below this tile. Defaults to null, but most template styles will have this set already if copied from.
		/// <para /> Most typical furniture will define an AnchorBottom spanning the width of the multitile requiring solid tiles: <c>TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);</c>
		/// <para /> Read the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#anchorbottomanchorleftanchorrightanchortop">Anchor section of the Basic Tile Guide on the wiki</see> for more information.
		/// </summary>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x004BFDCD File Offset: 0x004BDFCD
		// (set) Token: 0x06001991 RID: 6545 RVA: 0x004BFDF0 File Offset: 0x004BDFF0
		public AnchorData AnchorBottom
		{
			get
			{
				if (this._anchor == null)
				{
					return TileObjectData._baseObject.AnchorBottom;
				}
				return this._anchor.bottom;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnAnchor)
				{
					if (this._anchor.bottom == value)
					{
						return;
					}
					this._hasOwnAnchor = true;
					this._anchor = new AnchorDataModule(this._anchor);
				}
				this._anchor.bottom = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].AnchorBottom = value;
					}
				}
			}
		}

		/// <summary>
		/// Defines the anchors needed to the left of this tile. Defaults to null.
		/// <para /> Read the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#anchorbottomanchorleftanchorrightanchortop">Anchor section of the Basic Tile Guide on the wiki</see> for more information.
		/// </summary>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x004BFE7D File Offset: 0x004BE07D
		// (set) Token: 0x06001993 RID: 6547 RVA: 0x004BFEA0 File Offset: 0x004BE0A0
		public AnchorData AnchorLeft
		{
			get
			{
				if (this._anchor == null)
				{
					return TileObjectData._baseObject.AnchorLeft;
				}
				return this._anchor.left;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnAnchor)
				{
					if (this._anchor.left == value)
					{
						return;
					}
					this._hasOwnAnchor = true;
					this._anchor = new AnchorDataModule(this._anchor);
				}
				this._anchor.left = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].AnchorLeft = value;
					}
				}
			}
		}

		/// <summary>
		/// Defines the anchors needed to the right of this tile. Defaults to null.
		/// <para /> Read the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#anchorbottomanchorleftanchorrightanchortop">Anchor section of the Basic Tile Guide on the wiki</see> for more information.
		/// </summary>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x004BFF2D File Offset: 0x004BE12D
		// (set) Token: 0x06001995 RID: 6549 RVA: 0x004BFF50 File Offset: 0x004BE150
		public AnchorData AnchorRight
		{
			get
			{
				if (this._anchor == null)
				{
					return TileObjectData._baseObject.AnchorRight;
				}
				return this._anchor.right;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnAnchor)
				{
					if (this._anchor.right == value)
					{
						return;
					}
					this._hasOwnAnchor = true;
					this._anchor = new AnchorDataModule(this._anchor);
				}
				this._anchor.right = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].AnchorRight = value;
					}
				}
			}
		}

		/// <summary>
		/// If true, then this multitile requires walls behind each individual tile to place. Defaults to false.
		/// </summary>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x004BFFDD File Offset: 0x004BE1DD
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x004C0000 File Offset: 0x004BE200
		public bool AnchorWall
		{
			get
			{
				if (this._anchor == null)
				{
					return TileObjectData._baseObject.AnchorWall;
				}
				return this._anchor.wall;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnAnchor)
				{
					if (this._anchor.wall == value)
					{
						return;
					}
					this._hasOwnAnchor = true;
					this._anchor = new AnchorDataModule(this._anchor);
				}
				this._anchor.wall = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].AnchorWall = value;
					}
				}
			}
		}

		/// <summary>
		/// Limits which tile types are valid for anchoring to a specific set of types. 
		/// </summary>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x004C0088 File Offset: 0x004BE288
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x004C00A8 File Offset: 0x004BE2A8
		public int[] AnchorValidTiles
		{
			get
			{
				if (this._anchorTiles == null)
				{
					return TileObjectData._baseObject.AnchorValidTiles;
				}
				return this._anchorTiles.tileValid;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnAnchorTiles)
				{
					if (value.deepCompare(this._anchorTiles.tileValid))
					{
						return;
					}
					this._hasOwnAnchorTiles = true;
					this._anchorTiles = new AnchorTypesModule(this._anchorTiles);
				}
				this._anchorTiles.tileValid = value;
				if (!this._linkedAlternates)
				{
					return;
				}
				for (int i = 0; i < this._alternates.data.Count; i++)
				{
					int[] anchorValidTiles = value;
					if (value != null)
					{
						anchorValidTiles = (int[])value.Clone();
					}
					this._alternates.data[i].AnchorValidTiles = anchorValidTiles;
				}
			}
		}

		/// <summary>
		/// Dictates specific tile types that are not valid for anchoring to. 
		/// </summary>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x004C0147 File Offset: 0x004BE347
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x004C0168 File Offset: 0x004BE368
		public int[] AnchorInvalidTiles
		{
			get
			{
				if (this._anchorTiles == null)
				{
					return TileObjectData._baseObject.AnchorInvalidTiles;
				}
				return this._anchorTiles.tileInvalid;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnAnchorTiles)
				{
					if (value.deepCompare(this._anchorTiles.tileInvalid))
					{
						return;
					}
					this._hasOwnAnchorTiles = true;
					this._anchorTiles = new AnchorTypesModule(this._anchorTiles);
				}
				this._anchorTiles.tileInvalid = value;
				if (!this._linkedAlternates)
				{
					return;
				}
				for (int i = 0; i < this._alternates.data.Count; i++)
				{
					int[] anchorInvalidTiles = value;
					if (value != null)
					{
						anchorInvalidTiles = (int[])value.Clone();
					}
					this._alternates.data[i].AnchorInvalidTiles = anchorInvalidTiles;
				}
			}
		}

		/// <summary>
		/// Tiles contained in this array are tiles that will satisfy the <see cref="F:Terraria.Enums.AnchorType.AlternateTile" /> anchor type. This can be used to provide specific additional valid anchors for a specific alternate placement or tile style.
		/// </summary>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x004C0207 File Offset: 0x004BE407
		// (set) Token: 0x0600199D RID: 6557 RVA: 0x004C0228 File Offset: 0x004BE428
		public int[] AnchorAlternateTiles
		{
			get
			{
				if (this._anchorTiles == null)
				{
					return TileObjectData._baseObject.AnchorAlternateTiles;
				}
				return this._anchorTiles.tileAlternates;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnAnchorTiles)
				{
					if (value.deepCompare(this._anchorTiles.tileInvalid))
					{
						return;
					}
					this._hasOwnAnchorTiles = true;
					this._anchorTiles = new AnchorTypesModule(this._anchorTiles);
				}
				this._anchorTiles.tileAlternates = value;
				if (!this._linkedAlternates)
				{
					return;
				}
				for (int i = 0; i < this._alternates.data.Count; i++)
				{
					int[] anchorAlternateTiles = value;
					if (value != null)
					{
						anchorAlternateTiles = (int[])value.Clone();
					}
					this._alternates.data[i].AnchorAlternateTiles = anchorAlternateTiles;
				}
			}
		}

		/// <summary>
		/// Unimplemented, has no effect.
		/// </summary>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x004C02C7 File Offset: 0x004BE4C7
		// (set) Token: 0x0600199F RID: 6559 RVA: 0x004C02E8 File Offset: 0x004BE4E8
		public int[] AnchorValidWalls
		{
			get
			{
				if (this._anchorTiles == null)
				{
					return TileObjectData._baseObject.AnchorValidWalls;
				}
				return this._anchorTiles.wallValid;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnAnchorTiles)
				{
					this._hasOwnAnchorTiles = true;
					this._anchorTiles = new AnchorTypesModule(this._anchorTiles);
				}
				this._anchorTiles.wallValid = value;
				if (!this._linkedAlternates)
				{
					return;
				}
				for (int i = 0; i < this._alternates.data.Count; i++)
				{
					int[] anchorValidWalls = value;
					if (value != null)
					{
						anchorValidWalls = (int[])value.Clone();
					}
					this._alternates.data[i].AnchorValidWalls = anchorValidWalls;
				}
			}
		}

		/// <summary>
		/// If true, this tile will break when in contact with water. This supersedes the <see cref="F:Terraria.Main.tileWaterDeath" /> value.
		/// <para />By assigning to this on <see cref="F:Terraria.ObjectData.TileObjectData.newSubTile" /> this can be used to allow specific tile styles to be immune to water. Modders should try to keep this and <see cref="F:Terraria.Main.tileWaterDeath" /> in sync and only deviate from that common value for specific tile styles that should behave differently if any. <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/ExampleTorch.cs#L57">ExampleTorch</see> serves as an example of this and <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#conditional-behavior">the Conditional Behavior section of the Basic Tile wiki page</see> also has more information on this concept.
		/// <para /> Defaults to false.
		/// </summary>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x004C0373 File Offset: 0x004BE573
		// (set) Token: 0x060019A1 RID: 6561 RVA: 0x004C0394 File Offset: 0x004BE594
		public bool WaterDeath
		{
			get
			{
				if (this._liquidDeath == null)
				{
					return TileObjectData._baseObject.WaterDeath;
				}
				return this._liquidDeath.water;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnLiquidDeath)
				{
					if (this._liquidDeath.water == value)
					{
						return;
					}
					this._hasOwnLiquidDeath = true;
					this._liquidDeath = new LiquidDeathModule(this._liquidDeath);
				}
				this._liquidDeath.water = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].WaterDeath = value;
					}
				}
			}
		}

		/// <summary>
		/// If true, this tile will break when in contact with lava. This supersedes the <see cref="F:Terraria.Main.tileLavaDeath" /> value.
		/// <para />By assigning to this on <see cref="F:Terraria.ObjectData.TileObjectData.newSubTile" /> this can be used to allow specific tile styles to be immune to lava. Modders should try to keep this and <see cref="F:Terraria.Main.tileLavaDeath" /> in sync and only deviate from that common value for specific tile styles that should behave differently if any. <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/ExampleTorch.cs#L57">ExampleTorch</see> serves as an example of this and <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#conditional-behavior">the Conditional Behavior section of the Basic Tile wiki page</see> also has more information on this concept.
		/// <para /> Defaults to false, but many of the templates default this to true.
		/// </summary>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x004C041C File Offset: 0x004BE61C
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x004C043C File Offset: 0x004BE63C
		public bool LavaDeath
		{
			get
			{
				if (this._liquidDeath == null)
				{
					return TileObjectData._baseObject.LavaDeath;
				}
				return this._liquidDeath.lava;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnLiquidDeath)
				{
					if (this._liquidDeath.lava == value)
					{
						return;
					}
					this._hasOwnLiquidDeath = true;
					this._liquidDeath = new LiquidDeathModule(this._liquidDeath);
				}
				this._liquidDeath.lava = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].LavaDeath = value;
					}
				}
			}
		}

		/// <summary>
		/// Controls if a tile can be placed in water. Typically used in conjunction with <see cref="P:Terraria.ObjectData.TileObjectData.WaterDeath" /> for consistency. Many tiles with visual flames, for example, tend to not allow themselves to be placed in water.
		/// <para /><see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/ExampleTorch.cs#L57">ExampleTorch</see> has an underwater style that is specifically placeable underwater.
		/// <para /> Defaults to <see cref="F:Terraria.Enums.LiquidPlacement.Allowed" />.
		/// </summary>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x004C04C4 File Offset: 0x004BE6C4
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x004C04E4 File Offset: 0x004BE6E4
		public LiquidPlacement WaterPlacement
		{
			get
			{
				if (this._liquidPlacement == null)
				{
					return TileObjectData._baseObject.WaterPlacement;
				}
				return this._liquidPlacement.water;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnLiquidPlacement)
				{
					if (this._liquidPlacement.water == value)
					{
						return;
					}
					this._hasOwnLiquidPlacement = true;
					this._liquidPlacement = new LiquidPlacementModule(this._liquidPlacement);
				}
				this._liquidPlacement.water = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].WaterPlacement = value;
					}
				}
			}
		}

		/// <summary>
		/// Controls if a tile can be placed in lava. Typically used in conjunction with <see cref="P:Terraria.ObjectData.TileObjectData.LavaDeath" /> for consistency. Aside from obsidian furniture, most tiles in the game can not be placed in lava. Many tiles can't be placed in lava but won't break in lava, while others will break.
		/// <para /><see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/ExampleTorch.cs#L57">ExampleTorch</see> has a style that is specifically placeable in lava.
		/// <para /> Defaults to <see cref="F:Terraria.Enums.LiquidPlacement.NotAllowed" />.
		/// </summary>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x004C056C File Offset: 0x004BE76C
		// (set) Token: 0x060019A7 RID: 6567 RVA: 0x004C058C File Offset: 0x004BE78C
		public LiquidPlacement LavaPlacement
		{
			get
			{
				if (this._liquidPlacement == null)
				{
					return TileObjectData._baseObject.LavaPlacement;
				}
				return this._liquidPlacement.lava;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnLiquidPlacement)
				{
					if (this._liquidPlacement.lava == value)
					{
						return;
					}
					this._hasOwnLiquidPlacement = true;
					this._liquidPlacement = new LiquidPlacementModule(this._liquidPlacement);
				}
				this._liquidPlacement.lava = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].LavaPlacement = value;
					}
				}
			}
		}

		/// <summary>
		/// Designates a method to call to determine if the tile can't be placed by the player at the location. This method is only called on the client placing the tile. This is used by chest tiles to see if the chest limit has been reached and by pylons to check if the pylon has already been placed. 
		/// </summary>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x004C0614 File Offset: 0x004BE814
		// (set) Token: 0x060019A9 RID: 6569 RVA: 0x004C0634 File Offset: 0x004BE834
		public PlacementHook HookCheckIfCanPlace
		{
			get
			{
				if (this._placementHooks == null)
				{
					return TileObjectData._baseObject.HookCheckIfCanPlace;
				}
				return this._placementHooks.check;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnPlacementHooks)
				{
					this._hasOwnPlacementHooks = true;
					this._placementHooks = new TilePlacementHooksModule(this._placementHooks);
				}
				this._placementHooks.check = value;
			}
		}

		/// <summary> Unused. </summary>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x004C0668 File Offset: 0x004BE868
		// (set) Token: 0x060019AB RID: 6571 RVA: 0x004C0688 File Offset: 0x004BE888
		public PlacementHook HookPostPlaceEveryone
		{
			get
			{
				if (this._placementHooks == null)
				{
					return TileObjectData._baseObject.HookPostPlaceEveryone;
				}
				return this._placementHooks.postPlaceEveryone;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnPlacementHooks)
				{
					this._hasOwnPlacementHooks = true;
					this._placementHooks = new TilePlacementHooksModule(this._placementHooks);
				}
				this._placementHooks.postPlaceEveryone = value;
			}
		}

		/// <summary>
		/// Designates a method to call after this tile is placed by a player. This method facilitates binding extra data to a placed tile. This method is only called on the client placing the tile. This is used by chest tiles to place the actual <see cref="T:Terraria.Chest" /> and by other tiles to place a <see cref="T:Terraria.DataStructures.TileEntity" />.
		/// </summary>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x004C06BC File Offset: 0x004BE8BC
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x004C06DC File Offset: 0x004BE8DC
		public PlacementHook HookPostPlaceMyPlayer
		{
			get
			{
				if (this._placementHooks == null)
				{
					return TileObjectData._baseObject.HookPostPlaceMyPlayer;
				}
				return this._placementHooks.postPlaceMyPlayer;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnPlacementHooks)
				{
					this._hasOwnPlacementHooks = true;
					this._placementHooks = new TilePlacementHooksModule(this._placementHooks);
				}
				this._placementHooks.postPlaceMyPlayer = value;
			}
		}

		/// <summary>
		/// Designates a method that will run instead of the usual code that places each individual tile of a mutitile in the world. Used only be <see cref="F:Terraria.ID.TileID.ChristmasTree" /> due to its complicated logic.
		/// </summary>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x004C0710 File Offset: 0x004BE910
		// (set) Token: 0x060019AF RID: 6575 RVA: 0x004C0730 File Offset: 0x004BE930
		public PlacementHook HookPlaceOverride
		{
			get
			{
				if (this._placementHooks == null)
				{
					return TileObjectData._baseObject.HookPlaceOverride;
				}
				return this._placementHooks.placeOverride;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnPlacementHooks)
				{
					this._hasOwnPlacementHooks = true;
					this._placementHooks = new TilePlacementHooksModule(this._placementHooks);
				}
				this._placementHooks.placeOverride = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x004C0764 File Offset: 0x004BE964
		// (set) Token: 0x060019B1 RID: 6577 RVA: 0x004C0784 File Offset: 0x004BE984
		internal List<TileObjectData> SubTiles
		{
			get
			{
				if (this._subTiles == null)
				{
					return TileObjectData._baseObject.SubTiles;
				}
				return this._subTiles.data;
			}
			set
			{
				if (!this._hasOwnSubTiles)
				{
					this._hasOwnSubTiles = true;
					this._subTiles = new TileObjectSubTilesModule(null, null);
				}
				if (value == null)
				{
					this._subTiles.data = null;
					return;
				}
				this._subTiles.data = value;
			}
		}

		/// <summary>
		/// Offsets the drawing of this tile in the Y direction. Can be used to draw a tile at a location other than the default. One example is how <see cref="F:Terraria.ID.TileID.Switches" /> appear slightly embedded into the tile they are anchored to. Another example is how the alternate placement of banners for  platforms shifts the drawing up.
		/// <para /> For modded tiles this affects both the placement preview and placed tiles.
		/// <para /> Read <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#drawyoffset">this section on the wiki</see> for a visual explanation of this.
		/// <para /> Defaults to 0.
		/// </summary>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x004C07BE File Offset: 0x004BE9BE
		// (set) Token: 0x060019B3 RID: 6579 RVA: 0x004C07DC File Offset: 0x004BE9DC
		public int DrawYOffset
		{
			get
			{
				if (this._tileObjectDraw == null)
				{
					return this.DrawYOffset;
				}
				return this._tileObjectDraw.yOffset;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectDraw)
				{
					if (this._tileObjectDraw.yOffset == value)
					{
						return;
					}
					this._hasOwnTileObjectDraw = true;
					this._tileObjectDraw = new TileObjectDrawModule(this._tileObjectDraw);
				}
				this._tileObjectDraw.yOffset = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].DrawYOffset = value;
					}
				}
			}
		}

		/// <summary>
		/// Offsets the drawing of this tile in the X direction. Can be used to draw a tile at a location other than the default. One example is how <see cref="F:Terraria.ID.TileID.Switches" /> appear slightly embedded into the tile they are anchored to.
		/// <para /> Read <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#drawxoffset">this section on the wiki</see> for a visual explanation of this.
		/// <para /> Defaults to 0.
		/// </summary>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x004C0864 File Offset: 0x004BEA64
		// (set) Token: 0x060019B5 RID: 6581 RVA: 0x004C0880 File Offset: 0x004BEA80
		public int DrawXOffset
		{
			get
			{
				if (this._tileObjectDraw == null)
				{
					return this.DrawXOffset;
				}
				return this._tileObjectDraw.xOffset;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectDraw)
				{
					if (this._tileObjectDraw.xOffset == value)
					{
						return;
					}
					this._hasOwnTileObjectDraw = true;
					this._tileObjectDraw = new TileObjectDrawModule(this._tileObjectDraw);
				}
				this._tileObjectDraw.xOffset = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].DrawXOffset = value;
					}
				}
			}
		}

		/// <summary>
		/// If true, the tile placement preview will draw this tile flipped horizontally at even X tile coordinates. This effect must be replicated in <see cref="M:Terraria.ModLoader.ModTile.SetSpriteEffects(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteEffects@)" /> to work for the placed tile. The <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/ExampleLamp.cs#L66">ExampleLamp.SetSpriteEffects method</see> shows an example of this required code.
		/// <para /> Use this for visual variety and to break up visual repetition. Tiles with normal left and right placements, such as <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/Furniture/ExampleChair.cs">ExampleChair</see>, will use alternate placements influenced by the player direction rather than this.
		/// <para /> Note that this should only be used for multitiles that are 1 tile wide, the effect will not work correctly for wider tiles.
		/// <para /> Defaults to false.
		/// </summary>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x004C0908 File Offset: 0x004BEB08
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x004C0924 File Offset: 0x004BEB24
		public bool DrawFlipHorizontal
		{
			get
			{
				if (this._tileObjectDraw == null)
				{
					return this.DrawFlipHorizontal;
				}
				return this._tileObjectDraw.flipHorizontal;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectDraw)
				{
					if (this._tileObjectDraw.flipHorizontal == value)
					{
						return;
					}
					this._hasOwnTileObjectDraw = true;
					this._tileObjectDraw = new TileObjectDrawModule(this._tileObjectDraw);
				}
				this._tileObjectDraw.flipHorizontal = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].DrawFlipHorizontal = value;
					}
				}
			}
		}

		/// <summary>
		/// If true, the tile placement preview will draw this tile flipped vertically at even Y tile coordinates. This effect must be replicated in <see cref="M:Terraria.ModLoader.ModTile.SetSpriteEffects(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteEffects@)" /> to work for the placed tile. The <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/ExampleLamp.cs#L66">ExampleLamp.SetSpriteEffects method</see> shows an example of this required code, albeit the example is for DrawFlipHorizontal usage.
		/// <para /> Use this for visual variety and to break up visual repetition.
		/// <para /> Note that this should only be used for multitiles that are 1 tile high, the effect will not work correctly for taller tiles.
		/// <para /> Defaults to false.
		/// </summary>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x004C09AC File Offset: 0x004BEBAC
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x004C09C8 File Offset: 0x004BEBC8
		public bool DrawFlipVertical
		{
			get
			{
				if (this._tileObjectDraw == null)
				{
					return this.DrawFlipVertical;
				}
				return this._tileObjectDraw.flipVertical;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectDraw)
				{
					if (this._tileObjectDraw.flipVertical == value)
					{
						return;
					}
					this._hasOwnTileObjectDraw = true;
					this._tileObjectDraw = new TileObjectDrawModule(this._tileObjectDraw);
				}
				this._tileObjectDraw.flipVertical = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].DrawFlipVertical = value;
					}
				}
			}
		}

		/// <summary>
		/// Similar to <see cref="P:Terraria.ObjectData.TileObjectData.DrawYOffset" />, this also offsets the drawing of this tile in the Y direction, but this offset will only be applied if there is a solid tile above this tile. Torches use this to shift the torch down 2 more pixels so it doesn't look like the roof is being burnt.
		/// <para /> This is only applied to the tile placement preview and <see cref="M:Terraria.ModLoader.ModTile.SetDrawPositions(System.Int32,System.Int32,System.Int32@,System.Int32@,System.Int32@,System.Int16@,System.Int16@)" /> must be used to replicate this behavior in the placed tile. While torches use this, torches don't actually show a placement preview in the first place. The <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/ExampleTorch.cs#L57">ExampleTorch.SetDrawPositions method</see> shows an example of this.
		/// </summary>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x004C0A50 File Offset: 0x004BEC50
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x004C0A6C File Offset: 0x004BEC6C
		public int DrawStepDown
		{
			get
			{
				if (this._tileObjectDraw == null)
				{
					return this.DrawStepDown;
				}
				return this._tileObjectDraw.stepDown;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectDraw)
				{
					if (this._tileObjectDraw.stepDown == value)
					{
						return;
					}
					this._hasOwnTileObjectDraw = true;
					this._tileObjectDraw = new TileObjectDrawModule(this._tileObjectDraw);
				}
				this._tileObjectDraw.stepDown = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].DrawStepDown = value;
					}
				}
			}
		}

		/// <summary>
		/// If true, tile styles are arranged one after the other horizontally in the spritesheet, otherwise they are arranged vertically. New lines of styles will roll over to the next row once <see cref="P:Terraria.ObjectData.TileObjectData.StyleWrapLimit" /> is reached, if defined.
		/// <para /> Some tiles keep this false but use a small <see cref="P:Terraria.ObjectData.TileObjectData.StyleWrapLimit" /> and <see cref="P:Terraria.ObjectData.TileObjectData.StyleMultiplier" /> value to provide space for alternate placements, resulting in the tile styles themselves appearing to be arranged horizontally in the resulting spritesheet with the alternate placements below.  
		/// <para /> Defaults to <c>false</c>.
		/// </summary>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x004C0AF4 File Offset: 0x004BECF4
		// (set) Token: 0x060019BD RID: 6589 RVA: 0x004C0B10 File Offset: 0x004BED10
		public bool StyleHorizontal
		{
			get
			{
				if (this._tileObjectStyle == null)
				{
					return this.StyleHorizontal;
				}
				return this._tileObjectStyle.horizontal;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectStyle)
				{
					if (this._tileObjectStyle.horizontal == value)
					{
						return;
					}
					this._hasOwnTileObjectStyle = true;
					this._tileObjectStyle = new TileObjectStyleModule(this._tileObjectStyle);
				}
				this._tileObjectStyle.horizontal = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].StyleHorizontal = value;
					}
				}
			}
		}

		/// <summary>
		/// Used by alternate placements to offset the calculated placement style.
		/// <para /> Equal to the value passed into <see cref="M:Terraria.ObjectData.TileObjectData.addAlternate(System.Int32)" />.
		/// <para /> If using <see cref="M:Terraria.ObjectData.TileObjectData.GetTileData(Terraria.Tile)" /> note that this is <b>NOT the tile style or placement style</b> of that Tile. Use <see cref="M:Terraria.ObjectData.TileObjectData.GetTileInfo(Terraria.Tile,System.Int32@,System.Int32@)" /> or <see cref="M:Terraria.ObjectData.TileObjectData.GetTileStyle(Terraria.Tile)" /> instead to query those.
		/// <para /> Defaults to 0.
		/// </summary>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x004C0B98 File Offset: 0x004BED98
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x004C0BB8 File Offset: 0x004BEDB8
		public int Style
		{
			get
			{
				if (this._tileObjectStyle == null)
				{
					return TileObjectData._baseObject.Style;
				}
				return this._tileObjectStyle.style;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectStyle)
				{
					if (this._tileObjectStyle.style == value)
					{
						return;
					}
					this._hasOwnTileObjectStyle = true;
					this._tileObjectStyle = new TileObjectStyleModule(this._tileObjectStyle);
				}
				this._tileObjectStyle.style = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].Style = value;
					}
				}
			}
		}

		/// <summary>
		/// Defines how many tile placement styles (Number of styles times <see cref="P:Terraria.ObjectData.TileObjectData.StyleMultiplier" />) will fit into a line (row if <see cref="P:Terraria.ObjectData.TileObjectData.StyleHorizontal" /> is true, column if StyleHorizontal is false) of the spritesheet before wrapping to the next line. This is used for organizational purposes and to keep spritesheets easy to work with.
		/// <para /> Defaults to 0, which means no wrapping.
		/// </summary>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x004C0C40 File Offset: 0x004BEE40
		// (set) Token: 0x060019C1 RID: 6593 RVA: 0x004C0C60 File Offset: 0x004BEE60
		public int StyleWrapLimit
		{
			get
			{
				if (this._tileObjectStyle == null)
				{
					return TileObjectData._baseObject.StyleWrapLimit;
				}
				return this._tileObjectStyle.styleWrapLimit;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectStyle)
				{
					if (this._tileObjectStyle.styleWrapLimit == value)
					{
						return;
					}
					this._hasOwnTileObjectStyle = true;
					this._tileObjectStyle = new TileObjectStyleModule(this._tileObjectStyle);
				}
				this._tileObjectStyle.styleWrapLimit = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].StyleWrapLimit = value;
					}
				}
			}
		}

		/// <summary> A hack used by some vanilla tiles, do not use. </summary>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x004C0CE8 File Offset: 0x004BEEE8
		// (set) Token: 0x060019C3 RID: 6595 RVA: 0x004C0D08 File Offset: 0x004BEF08
		public int? StyleWrapLimitVisualOverride
		{
			get
			{
				if (this._tileObjectStyle == null)
				{
					return TileObjectData._baseObject.StyleWrapLimitVisualOverride;
				}
				return this._tileObjectStyle.styleWrapLimitVisualOverride;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectStyle)
				{
					int? styleWrapLimitVisualOverride = this._tileObjectStyle.styleWrapLimitVisualOverride;
					int? num = value;
					if (styleWrapLimitVisualOverride.GetValueOrDefault() == num.GetValueOrDefault() & styleWrapLimitVisualOverride != null == (num != null))
					{
						return;
					}
					this._hasOwnTileObjectStyle = true;
					this._tileObjectStyle = new TileObjectStyleModule(this._tileObjectStyle);
				}
				this._tileObjectStyle.styleWrapLimitVisualOverride = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].StyleWrapLimitVisualOverride = value;
					}
				}
			}
		}

		/// <summary> A hack used by some vanilla tiles, do not use. </summary>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x004C0DB3 File Offset: 0x004BEFB3
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x004C0DD4 File Offset: 0x004BEFD4
		public int? styleLineSkipVisualOverride
		{
			get
			{
				if (this._tileObjectStyle == null)
				{
					return TileObjectData._baseObject.styleLineSkipVisualOverride;
				}
				return this._tileObjectStyle.styleLineSkipVisualoverride;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectStyle)
				{
					int? styleLineSkipVisualoverride = this._tileObjectStyle.styleLineSkipVisualoverride;
					int? num = value;
					if (styleLineSkipVisualoverride.GetValueOrDefault() == num.GetValueOrDefault() & styleLineSkipVisualoverride != null == (num != null))
					{
						return;
					}
					this._hasOwnTileObjectStyle = true;
					this._tileObjectStyle = new TileObjectStyleModule(this._tileObjectStyle);
				}
				this._tileObjectStyle.styleLineSkipVisualoverride = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].styleLineSkipVisualOverride = value;
					}
				}
			}
		}

		/// <summary>
		/// How many lines (rows if StyleHorizontal is true, columns if StyleHorizontal is false) will be skipped between each line of styles when wrapping to the next line due to <see cref="P:Terraria.ObjectData.TileObjectData.StyleWrapLimit" />. Skipping lines allows tile spritesheets to have space for animations or toggled states (manual changes to TileFrameX and TileFrameY). For example, <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/ExampleCampfire.cs">ExampleCampfire</see> sets this to 9 because it has 8 animation frames and an off state taking up additional lines. Failure to set this for a tile that can change states will result in tile drops being incorrectly calculated.
		/// <para /> Defaults to 1, meaning no lines are skipped.
		/// </summary>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x004C0E7F File Offset: 0x004BF07F
		// (set) Token: 0x060019C7 RID: 6599 RVA: 0x004C0EA0 File Offset: 0x004BF0A0
		public int StyleLineSkip
		{
			get
			{
				if (this._tileObjectStyle == null)
				{
					return TileObjectData._baseObject.StyleLineSkip;
				}
				return this._tileObjectStyle.styleLineSkip;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectStyle)
				{
					if (this._tileObjectStyle.styleLineSkip == value)
					{
						return;
					}
					this._hasOwnTileObjectStyle = true;
					this._tileObjectStyle = new TileObjectStyleModule(this._tileObjectStyle);
				}
				this._tileObjectStyle.styleLineSkip = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].StyleLineSkip = value;
					}
				}
			}
		}

		/// <summary>
		/// Applies a multiplier to style calculations to account for alternate placements (<see cref="M:Terraria.ObjectData.TileObjectData.addAlternate(System.Int32)" />) and <see cref="P:Terraria.ObjectData.TileObjectData.RandomStyleRange" />. For example, chair tiles can be placed facing left or right. These are intended to share the same style (<see cref="F:Terraria.Item.placeStyle" />) value, but will have different placement style values. The multiplier accounts for these alternate placements allowing tile styles to be properly determined and consequently the correct item to drop when the tile is destroyed.
		/// <para /> If tiles are mistakenly dropping items from different styles, StyleMultiplier might have the incorrect value.
		/// <para /> Defaults to 1.
		/// </summary>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x004C0F28 File Offset: 0x004BF128
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x004C0F48 File Offset: 0x004BF148
		public int StyleMultiplier
		{
			get
			{
				if (this._tileObjectStyle == null)
				{
					return TileObjectData._baseObject.StyleMultiplier;
				}
				return this._tileObjectStyle.styleMultiplier;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectStyle)
				{
					if (this._tileObjectStyle.styleMultiplier == value)
					{
						return;
					}
					this._hasOwnTileObjectStyle = true;
					this._tileObjectStyle = new TileObjectStyleModule(this._tileObjectStyle);
				}
				this._tileObjectStyle.styleMultiplier = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].StyleMultiplier = value;
					}
				}
			}
		}

		/// <summary>
		/// The width of this multitile in tile coordinates. For example, chests are 2 tiles wide while furnaces are 3 tiles wide.
		/// <para /> Read the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#width">Width section of the Basic Tile Guide on the wiki</see> for more information and helpful visualizations.
		/// <para /> Defaults to 1.
		/// </summary>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x004C0FD0 File Offset: 0x004BF1D0
		// (set) Token: 0x060019CB RID: 6603 RVA: 0x004C0FF0 File Offset: 0x004BF1F0
		public int Width
		{
			get
			{
				if (this._tileObjectBase == null)
				{
					return TileObjectData._baseObject.Width;
				}
				return this._tileObjectBase.width;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectBase)
				{
					if (this._tileObjectBase.width == value)
					{
						return;
					}
					this._hasOwnTileObjectBase = true;
					this._tileObjectBase = new TileObjectBaseModule(this._tileObjectBase);
					if (!this._hasOwnTileObjectCoords)
					{
						this._hasOwnTileObjectCoords = true;
						this._tileObjectCoords = new TileObjectCoordinatesModule(this._tileObjectCoords, null);
						this._tileObjectCoords.calculated = false;
					}
				}
				this._tileObjectBase.width = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].Width = value;
					}
				}
			}
		}

		/// <summary>
		/// The height of this multitile in tile coordinates. For example, chests are 2 tiles high while grandfather clocks are 5 tiles high.
		/// <para /> Read the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#height">Height section of the Basic Tile Guide on the wiki</see> for more information and helpful visualizations.
		/// <para /> Defaults to 1. When changing this <see cref="P:Terraria.ObjectData.TileObjectData.CoordinateHeights" /> must be changed as well.
		/// </summary>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x004C10A5 File Offset: 0x004BF2A5
		// (set) Token: 0x060019CD RID: 6605 RVA: 0x004C10C8 File Offset: 0x004BF2C8
		public int Height
		{
			get
			{
				if (this._tileObjectBase == null)
				{
					return TileObjectData._baseObject.Height;
				}
				return this._tileObjectBase.height;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectBase)
				{
					if (this._tileObjectBase.height == value)
					{
						return;
					}
					this._hasOwnTileObjectBase = true;
					this._tileObjectBase = new TileObjectBaseModule(this._tileObjectBase);
					if (!this._hasOwnTileObjectCoords)
					{
						this._hasOwnTileObjectCoords = true;
						this._tileObjectCoords = new TileObjectCoordinatesModule(this._tileObjectCoords, null);
						this._tileObjectCoords.calculated = false;
					}
				}
				this._tileObjectBase.height = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].Height = value;
					}
				}
			}
		}

		/// <summary>
		/// The tile coordinate from which this multitile is placed from. This affects the position of the tile in relation to the users mouse and also the placement of the tile when placed through code such as <see cref="M:Terraria.WorldGen.PlaceTile(System.Int32,System.Int32,System.Int32,System.Boolean,System.Boolean,System.Int32,System.Int32)" />
		/// <para /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#origin">Origin section of the Basic Tile wiki guide</see> has more information and useful visualizations of this property.
		/// <para /> Defaults to Point16.Zero (0, 0), or the top left corner tile.
		/// </summary>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x004C117D File Offset: 0x004BF37D
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x004C11A0 File Offset: 0x004BF3A0
		public Point16 Origin
		{
			get
			{
				if (this._tileObjectBase == null)
				{
					return TileObjectData._baseObject.Origin;
				}
				return this._tileObjectBase.origin;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectBase)
				{
					if (this._tileObjectBase.origin == value)
					{
						return;
					}
					this._hasOwnTileObjectBase = true;
					this._tileObjectBase = new TileObjectBaseModule(this._tileObjectBase);
				}
				this._tileObjectBase.origin = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].Origin = value;
					}
				}
			}
		}

		/// <summary>
		/// The direction this TileObjectData will be eligible to be placed in.
		/// <para /> Many furniture tiles allow the player to place them facing right or left controlled by the players current direction. These tiles do this by having the base tile place in one direction while an alternate is added placing in the other direction.
		/// <para /> See <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/Furniture/ExampleChair.cs#L38">ExampleChair</see> for an example of this approach.
		/// <para /> Defaults to <see cref="F:Terraria.Enums.TileObjectDirection.None" />.
		/// </summary>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x004C122D File Offset: 0x004BF42D
		// (set) Token: 0x060019D1 RID: 6609 RVA: 0x004C1250 File Offset: 0x004BF450
		public TileObjectDirection Direction
		{
			get
			{
				if (this._tileObjectBase == null)
				{
					return TileObjectData._baseObject.Direction;
				}
				return this._tileObjectBase.direction;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectBase)
				{
					if (this._tileObjectBase.direction == value)
					{
						return;
					}
					this._hasOwnTileObjectBase = true;
					this._tileObjectBase = new TileObjectBaseModule(this._tileObjectBase);
				}
				this._tileObjectBase.direction = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].Direction = value;
					}
				}
			}
		}

		/// <summary>
		/// Allows this tile to place a randomly selected placement style when placing. The placement preview will update randomly as the player attempts to place the tile.
		/// <para /> For example, the <see cref="F:Terraria.ID.TileID.Coral" /> tile uses a RandomStyleRange value of 6, allowing 6 different tile placement styles to be selected when placing.
		/// <para /> This can be used in conjunction with <see cref="P:Terraria.ObjectData.TileObjectData.StyleMultiplier" /> to ensure that each of these random placement styles are considered the same tile style, but sometimes they are not.
		/// One interesting advanced usage of this feature is the <see cref="F:Terraria.ID.TileID.Painting2X3" /> tile. It uses RandomStyleRange only for tile style 15, which allows the <see cref="F:Terraria.ID.ItemID.StrangeGrowth" /> item to place one of four tile styles.
		/// <para /> If used with alternate placement styles, the random styles of a specific alternate should be located together on the spritesheet. The <see cref="M:Terraria.ObjectData.TileObjectData.addAlternate(System.Int32)" /> parameter should account for RandomStyleRange as well.
		/// <para /> Note that tiles placed with <see cref="M:Terraria.WorldGen.PlaceTile(System.Int32,System.Int32,System.Int32,System.Boolean,System.Boolean,System.Int32,System.Int32)" /> won't randomize if <see cref="P:Terraria.ObjectData.TileObjectData.StyleMultiplier" /> is also used, but a specific random placement style choice can be passed into <see cref="M:Terraria.WorldGen.PlaceObject(System.Int32,System.Int32,System.Int32,System.Boolean,System.Int32,System.Int32,System.Int32,System.Int32)" />.
		/// <para /> Defaults to 0.
		/// </summary>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x004C12D8 File Offset: 0x004BF4D8
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x004C12F8 File Offset: 0x004BF4F8
		public int RandomStyleRange
		{
			get
			{
				if (this._tileObjectBase == null)
				{
					return TileObjectData._baseObject.RandomStyleRange;
				}
				return this._tileObjectBase.randomRange;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectBase)
				{
					if (this._tileObjectBase.randomRange == value)
					{
						return;
					}
					this._hasOwnTileObjectBase = true;
					this._tileObjectBase = new TileObjectBaseModule(this._tileObjectBase);
				}
				this._tileObjectBase.randomRange = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].RandomStyleRange = value;
					}
				}
			}
		}

		/// <summary>
		/// Similar to <see cref="P:Terraria.ObjectData.TileObjectData.RandomStyleRange" />, except a random placement style is selected from a selection of random placement styles. This is unused by vanilla tiles, but should work as expected.
		/// </summary>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x004C1380 File Offset: 0x004BF580
		// (set) Token: 0x060019D5 RID: 6613 RVA: 0x004C13A0 File Offset: 0x004BF5A0
		public int[] SpecificRandomStyles
		{
			get
			{
				if (this._tileObjectBase == null)
				{
					return TileObjectData._baseObject.SpecificRandomStyles;
				}
				return this._tileObjectBase.specificRandomStyles;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectBase)
				{
					if (this._tileObjectBase.specificRandomStyles == value)
					{
						return;
					}
					this._hasOwnTileObjectBase = true;
					this._tileObjectBase = new TileObjectBaseModule(this._tileObjectBase);
				}
				this._tileObjectBase.specificRandomStyles = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].SpecificRandomStyles = value;
					}
				}
			}
		}

		/// <summary>
		/// If true, placing this tile will cause the anchored solid tiles to become unsloped. Otherwise, slopes on anchor tiles will prevent this tile from placing.
		/// <para /> <see cref="F:Terraria.ID.TileID.Switches" />, <see cref="F:Terraria.ID.TileID.Torches" />, and <see cref="F:Terraria.ID.TileID.ProjectilePressurePad" /> (partially) all use this, allowing these tiles to be placed more conveniently.
		/// <para /> Defaults to false.
		/// </summary>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x004C1428 File Offset: 0x004BF628
		// (set) Token: 0x060019D7 RID: 6615 RVA: 0x004C1448 File Offset: 0x004BF648
		public bool FlattenAnchors
		{
			get
			{
				if (this._tileObjectBase == null)
				{
					return TileObjectData._baseObject.FlattenAnchors;
				}
				return this._tileObjectBase.flattenAnchors;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectBase)
				{
					if (this._tileObjectBase.flattenAnchors == value)
					{
						return;
					}
					this._hasOwnTileObjectBase = true;
					this._tileObjectBase = new TileObjectBaseModule(this._tileObjectBase);
				}
				this._tileObjectBase.flattenAnchors = value;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].FlattenAnchors = value;
					}
				}
			}
		}

		/// <summary>
		/// An array defining how tall each individual tile within this multitile is, in pixels. This array must have exactly <see cref="P:Terraria.ObjectData.TileObjectData.Height" /> elements in it. In most cases, all values should be 16, but sometimes 18 is suitable for the last value.
		/// <para /> For example, a 3 tile high multitile might set CoordinateHeights as follows: <c>TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };</c>.
		/// <para /> Read the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#coordinateheights">CoordinateHeights section of the Basic Tile Guide on the wiki</see> for more information and helpful visualizations.
		/// <para /> Defaults to <c>new int[] { 16 }</c>. If changing <see cref="P:Terraria.ObjectData.TileObjectData.Height" /> this must be changed as well.
		/// </summary>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x004C14D0 File Offset: 0x004BF6D0
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x004C14F0 File Offset: 0x004BF6F0
		public int[] CoordinateHeights
		{
			get
			{
				if (this._tileObjectCoords == null)
				{
					return TileObjectData._baseObject.CoordinateHeights;
				}
				return this._tileObjectCoords.heights;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectCoords)
				{
					if (value.deepCompare(this._tileObjectCoords.heights))
					{
						return;
					}
					this._hasOwnTileObjectCoords = true;
					this._tileObjectCoords = new TileObjectCoordinatesModule(this._tileObjectCoords, value);
				}
				else
				{
					this._tileObjectCoords.heights = value;
				}
				this._tileObjectCoords.calculated = false;
				if (!this._linkedAlternates)
				{
					return;
				}
				for (int i = 0; i < this._alternates.data.Count; i++)
				{
					int[] coordinateHeights = value;
					if (value != null)
					{
						coordinateHeights = (int[])value.Clone();
					}
					this._alternates.data[i].CoordinateHeights = coordinateHeights;
				}
			}
		}

		/// <summary>
		/// How wide each individual tile within this multitile is, in pixels.
		/// <para /> This should almost always be 16 since each tile in the world is 16x16 pixels, but in rare situations a different value might be suitable, such as in this <see cref="F:Terraria.ID.TileID.Coral" /> example on the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#non-16-or-18-values">Basic Tile Guide on the wiki</see> where the extra-wide tile will spill out and draw outside the normal tile bounds.
		/// <para /> Defaults to 0, but most templates default this to 16. If not copying from a template or existing tile <b>be sure to set this to 16</b>.
		/// </summary>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x004C159E File Offset: 0x004BF79E
		// (set) Token: 0x060019DB RID: 6619 RVA: 0x004C15C0 File Offset: 0x004BF7C0
		public int CoordinateWidth
		{
			get
			{
				if (this._tileObjectCoords == null)
				{
					return TileObjectData._baseObject.CoordinateWidth;
				}
				return this._tileObjectCoords.width;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectCoords)
				{
					if (this._tileObjectCoords.width == value)
					{
						return;
					}
					this._hasOwnTileObjectCoords = true;
					this._tileObjectCoords = new TileObjectCoordinatesModule(this._tileObjectCoords, null);
				}
				this._tileObjectCoords.width = value;
				this._tileObjectCoords.calculated = false;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].CoordinateWidth = value;
					}
				}
			}
		}

		/// <summary>
		/// The number of empty padding pixels between each individual tile of a multitile in its spritesheet.
		/// <para /> All vanilla tiles set this to 2. While it may be possible to use a value of 0 to make it easier on artists, there are plenty of places in the Terraria code that still assume a padding of 2 so it is not recommended. The examples shown in <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#framed-vs-frameimportant-tiles">the Basic Tile wiki guide</see> show what the padding looks like in practice.
		/// <para /> The community has made tools such as <see href="https://forums.terraria.org/index.php?threads/tspritepadder-ready-to-use-sprites-for-terraria-tiles.96177/">tSpritePadder</see> to simplify adding padding pixels to tile sprites.
		/// <para /> Defaults to 0 but most templates default this to 2.
		/// </summary>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x004C1655 File Offset: 0x004BF855
		// (set) Token: 0x060019DD RID: 6621 RVA: 0x004C1678 File Offset: 0x004BF878
		public int CoordinatePadding
		{
			get
			{
				if (this._tileObjectCoords == null)
				{
					return TileObjectData._baseObject.CoordinatePadding;
				}
				return this._tileObjectCoords.padding;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectCoords)
				{
					if (this._tileObjectCoords.padding == value)
					{
						return;
					}
					this._hasOwnTileObjectCoords = true;
					this._tileObjectCoords = new TileObjectCoordinatesModule(this._tileObjectCoords, null);
				}
				this._tileObjectCoords.padding = value;
				this._tileObjectCoords.calculated = false;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].CoordinatePadding = value;
					}
				}
			}
		}

		/// <summary>
		/// The number of pixels between each tile placement style of a multitile in its spritesheet in addition to the normal <see cref="P:Terraria.ObjectData.TileObjectData.CoordinatePadding" /> between each individual tile. Can be used to give more or less room between tile styles for the artist's convenience.
		/// <para /> Defaults to (0, 0).
		/// </summary>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x004C170D File Offset: 0x004BF90D
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x004C1730 File Offset: 0x004BF930
		public Point16 CoordinatePaddingFix
		{
			get
			{
				if (this._tileObjectCoords == null)
				{
					return TileObjectData._baseObject.CoordinatePaddingFix;
				}
				return this._tileObjectCoords.paddingFix;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectCoords)
				{
					if (this._tileObjectCoords.paddingFix == value)
					{
						return;
					}
					this._hasOwnTileObjectCoords = true;
					this._tileObjectCoords = new TileObjectCoordinatesModule(this._tileObjectCoords, null);
				}
				this._tileObjectCoords.paddingFix = value;
				this._tileObjectCoords.calculated = false;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].CoordinatePaddingFix = value;
					}
				}
			}
		}

		/// <summary>
		/// The full width of a multitile in terms of the spritesheet texture coordinates (NOT world coordinates).
		/// <para /> Used to determine which style and alternate placement a specific Tile with <see cref="P:Terraria.Tile.TileFrameX" /> and <see cref="P:Terraria.Tile.TileFrameY" /> values belongs to.
		/// <para /> Equal to <c>(<see cref="P:Terraria.ObjectData.TileObjectData.CoordinateWidth" /> + <see cref="P:Terraria.ObjectData.TileObjectData.CoordinatePadding" />) * <see cref="P:Terraria.ObjectData.TileObjectData.Width" /> + <see cref="P:Terraria.ObjectData.TileObjectData.CoordinatePaddingFix" />.X</c>.
		/// </summary>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x004C17CA File Offset: 0x004BF9CA
		public int CoordinateFullWidth
		{
			get
			{
				if (this._tileObjectCoords == null)
				{
					return TileObjectData._baseObject.CoordinateFullWidth;
				}
				if (!this._tileObjectCoords.calculated)
				{
					this.Calculate();
				}
				return this._tileObjectCoords.styleWidth;
			}
		}

		/// <summary>
		/// The full height of a multitile in terms of the spritesheet texture coordinates (NOT world coordinates).
		/// <para /> Used to determine which style and alternate placement a specific Tile with <see cref="P:Terraria.Tile.TileFrameX" /> and <see cref="P:Terraria.Tile.TileFrameY" /> values belongs to.
		/// <para /> Equal to <c><see cref="P:Terraria.ObjectData.TileObjectData.CoordinateHeights" />.Sum() + (<see cref="P:Terraria.ObjectData.TileObjectData.CoordinatePadding" /> * <see cref="P:Terraria.ObjectData.TileObjectData.Height" />) + <see cref="P:Terraria.ObjectData.TileObjectData.CoordinatePaddingFix" />.Y</c>.
		/// </summary>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x004C17FD File Offset: 0x004BF9FD
		public int CoordinateFullHeight
		{
			get
			{
				if (this._tileObjectCoords == null)
				{
					return TileObjectData._baseObject.CoordinateFullHeight;
				}
				if (!this._tileObjectCoords.calculated)
				{
					this.Calculate();
				}
				return this._tileObjectCoords.styleHeight;
			}
		}

		/// <summary>
		/// Offsets the tile placement preview by a number of placement styles.
		/// <para /> This can be used for tiles that have custom draw code to show something resembling the final placed tile despite the actual placement sprite missing parts of the final visuals. <see cref="F:Terraria.ID.TileID.Mannequin" /> and <see cref="F:Terraria.ID.TileID.GolfCupFlag" /> use this.
		/// <para /> Defaults to 0.
		/// </summary>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x004C1830 File Offset: 0x004BFA30
		// (set) Token: 0x060019E3 RID: 6627 RVA: 0x004C1850 File Offset: 0x004BFA50
		public int DrawStyleOffset
		{
			get
			{
				if (this._tileObjectCoords == null)
				{
					return TileObjectData._baseObject.DrawStyleOffset;
				}
				return this._tileObjectCoords.drawStyleOffset;
			}
			set
			{
				this.WriteCheck();
				if (!this._hasOwnTileObjectCoords)
				{
					if (this._tileObjectCoords.drawStyleOffset == value)
					{
						return;
					}
					this._hasOwnTileObjectCoords = true;
					this._tileObjectCoords = new TileObjectCoordinatesModule(this._tileObjectCoords, null);
				}
				this._tileObjectCoords.drawStyleOffset = value;
				this._tileObjectCoords.calculated = false;
				if (this._linkedAlternates)
				{
					for (int i = 0; i < this._alternates.data.Count; i++)
					{
						this._alternates.data[i].DrawStyleOffset = value;
					}
				}
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x004C18E5 File Offset: 0x004BFAE5
		public int AlternatesCount
		{
			get
			{
				return this.Alternates.Count;
			}
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x004C18F4 File Offset: 0x004BFAF4
		public TileObjectData(TileObjectData copyFrom = null)
		{
			this._parent = null;
			this._linkedAlternates = false;
			if (copyFrom == null)
			{
				this._usesCustomCanPlace = false;
				this._alternates = null;
				this._anchor = null;
				this._anchorTiles = null;
				this._tileObjectBase = null;
				this._liquidDeath = null;
				this._liquidPlacement = null;
				this._placementHooks = null;
				this._tileObjectDraw = null;
				this._tileObjectStyle = null;
				this._tileObjectCoords = null;
				return;
			}
			this.CopyFrom(copyFrom);
		}

		/// <summary>
		/// Copies all tile object data from the <paramref name="copy" /> to this TileObjectData <b>except</b> the subtiles. <see cref="M:Terraria.ObjectData.TileObjectData.FullCopyFrom(Terraria.ObjectData.TileObjectData)" /> will additionally copy subtiles.
		/// <para /> Copying tile data will copy anchors, sizes, origin, and all other properties of the tile dictating their placement behavior.
		/// <para /> The most common usage is to copy the tile properties of one of the existing tile templates found in the TileObjectData class: <c>TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);</c>. After copying from a template, the properties would be further customized if desired. 
		/// <para /> Another common usage would be to copy a vanilla furniture tile to easily create tiles matching the behavior of existing furniture: <c>TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.OpenDoor, 0));</c>. CopyFrom is used instead of FullCopyFrom in this situation because copying subtiles would incorrectly copy over subtile specific behavior, such as lava immunity for some subtiles.
		/// <para /> One more usage is to copy over tile properties from <see cref="F:Terraria.ObjectData.TileObjectData.newTile" /> to either <see cref="F:Terraria.ObjectData.TileObjectData.newAlternate" /> or <see cref="F:Terraria.ObjectData.TileObjectData.newSubTile" />: <c>TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);</c>.
		/// <para /> Copying tile properties is a good approach to writing clean code and avoiding hard to troubleshoot bugs. Make sure to call this method before modifying any other properties as this method will overwrite any changes made earlier in the code.
		/// </summary>
		/// <param name="copy"></param>
		// Token: 0x060019E6 RID: 6630 RVA: 0x004C1970 File Offset: 0x004BFB70
		public void CopyFrom(TileObjectData copy)
		{
			if (copy != null)
			{
				this._usesCustomCanPlace = copy._usesCustomCanPlace;
				this._alternates = copy._alternates;
				this._anchor = copy._anchor;
				this._anchorTiles = copy._anchorTiles;
				this._tileObjectBase = copy._tileObjectBase;
				this._liquidDeath = copy._liquidDeath;
				this._liquidPlacement = copy._liquidPlacement;
				this._placementHooks = copy._placementHooks;
				this._tileObjectDraw = copy._tileObjectDraw;
				this._tileObjectStyle = copy._tileObjectStyle;
				this._tileObjectCoords = copy._tileObjectCoords;
				if (this._hasOwnAlternates || this._hasOwnAnchor || this._hasOwnAnchorTiles || this._hasOwnLiquidDeath || this._hasOwnLiquidPlacement || this._hasOwnPlacementHooks || this._hasOwnSubTiles || this._hasOwnTileObjectBase || this._hasOwnTileObjectDraw || this._hasOwnTileObjectStyle || this._hasOwnTileObjectCoords)
				{
					this._hasOwnAlternates = (this._hasOwnAnchor = (this._hasOwnAnchorTiles = (this._hasOwnLiquidDeath = (this._hasOwnLiquidPlacement = (this._hasOwnPlacementHooks = (this._hasOwnSubTiles = (this._hasOwnTileObjectBase = (this._hasOwnTileObjectDraw = (this._hasOwnTileObjectStyle = (this._hasOwnTileObjectCoords = false))))))))));
					if (ModCompile.activelyModding)
					{
						ILog tML = Logging.tML;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(433, 1);
						defaultInterpolatedStringHandler.AppendLiteral("Bad TileObjectData values detected.\nLook at this ModTile and the ModTile that would load immediately before this ModTile and ensure that all modifications to TileObjectData.newTile are done between CopyFrom and AddTile.\nThe previous ModTile would typically be alphabetically before the tile in this stack trace.\nSee https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#basic-tileobjectdatanewtile-structure for more information.\n");
						defaultInterpolatedStringHandler.AppendFormatted<StackTrace>(new StackTrace(true));
						tML.Warn(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					return;
				}
			}
		}

		/// <inheritdoc cref="M:Terraria.ObjectData.TileObjectData.FullCopyFrom(Terraria.ObjectData.TileObjectData)" />
		// Token: 0x060019E7 RID: 6631 RVA: 0x004C1B02 File Offset: 0x004BFD02
		public void FullCopyFrom(ushort tileType)
		{
			this.FullCopyFrom(TileObjectData.GetTileData((int)tileType, 0, 0));
		}

		/// <summary>
		/// Similar to <see cref="M:Terraria.ObjectData.TileObjectData.CopyFrom(Terraria.ObjectData.TileObjectData)" />, except subtile data will also be copied. This is useful for inheriting subtile specific data, such as how each herb tile type (ImmatureHerbs, MatureHerbs, BloomingHerbs) share the same water placement and lava death properties for their Fireblossom and Moonglow styles.
		/// </summary>
		/// <param name="copy"></param>
		// Token: 0x060019E8 RID: 6632 RVA: 0x004C1B14 File Offset: 0x004BFD14
		public void FullCopyFrom(TileObjectData copy)
		{
			if (copy != null)
			{
				this._usesCustomCanPlace = copy._usesCustomCanPlace;
				this._alternates = copy._alternates;
				this._anchor = copy._anchor;
				this._anchorTiles = copy._anchorTiles;
				this._tileObjectBase = copy._tileObjectBase;
				this._liquidDeath = copy._liquidDeath;
				this._liquidPlacement = copy._liquidPlacement;
				this._placementHooks = copy._placementHooks;
				this._tileObjectDraw = copy._tileObjectDraw;
				this._tileObjectStyle = copy._tileObjectStyle;
				this._tileObjectCoords = copy._tileObjectCoords;
				this._subTiles = new TileObjectSubTilesModule(copy._subTiles, null);
				this._hasOwnSubTiles = true;
			}
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x004C1BC4 File Offset: 0x004BFDC4
		private void SetupBaseObject()
		{
			this._alternates = new TileObjectAlternatesModule(null);
			this._hasOwnAlternates = true;
			this.Alternates = new List<TileObjectData>();
			this._anchor = new AnchorDataModule(null);
			this._hasOwnAnchor = true;
			this.AnchorTop = default(AnchorData);
			this.AnchorBottom = default(AnchorData);
			this.AnchorLeft = default(AnchorData);
			this.AnchorRight = default(AnchorData);
			this.AnchorWall = false;
			this._anchorTiles = new AnchorTypesModule(null);
			this._hasOwnAnchorTiles = true;
			this.AnchorValidTiles = null;
			this.AnchorInvalidTiles = null;
			this.AnchorAlternateTiles = null;
			this.AnchorValidWalls = null;
			this._liquidDeath = new LiquidDeathModule(null);
			this._hasOwnLiquidDeath = true;
			this.WaterDeath = false;
			this.LavaDeath = false;
			this._liquidPlacement = new LiquidPlacementModule(null);
			this._hasOwnLiquidPlacement = true;
			this.WaterPlacement = LiquidPlacement.Allowed;
			this.LavaPlacement = LiquidPlacement.NotAllowed;
			this._placementHooks = new TilePlacementHooksModule(null);
			this._hasOwnPlacementHooks = true;
			this.HookCheckIfCanPlace = default(PlacementHook);
			this.HookPostPlaceEveryone = default(PlacementHook);
			this.HookPostPlaceMyPlayer = default(PlacementHook);
			this.HookPlaceOverride = default(PlacementHook);
			this.SubTiles = new List<TileObjectData>((int)TileID.Count);
			this._tileObjectBase = new TileObjectBaseModule(null);
			this._hasOwnTileObjectBase = true;
			this.Width = 1;
			this.Height = 1;
			this.Origin = Point16.Zero;
			this.Direction = TileObjectDirection.None;
			this.RandomStyleRange = 0;
			this.FlattenAnchors = false;
			this._tileObjectCoords = new TileObjectCoordinatesModule(null, null);
			this._hasOwnTileObjectCoords = true;
			this.CoordinateHeights = new int[]
			{
				16
			};
			this.CoordinateWidth = 0;
			this.CoordinatePadding = 0;
			this.CoordinatePaddingFix = Point16.Zero;
			this._tileObjectDraw = new TileObjectDrawModule(null);
			this._hasOwnTileObjectDraw = true;
			this.DrawYOffset = 0;
			this.DrawFlipHorizontal = false;
			this.DrawFlipVertical = false;
			this.DrawStepDown = 0;
			this._tileObjectStyle = new TileObjectStyleModule(null);
			this._hasOwnTileObjectStyle = true;
			this.Style = 0;
			this.StyleHorizontal = false;
			this.StyleWrapLimit = 0;
			this.StyleMultiplier = 1;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x004C1DF4 File Offset: 0x004BFFF4
		private void Calculate()
		{
			if (this._tileObjectCoords.calculated)
			{
				return;
			}
			this._tileObjectCoords.calculated = true;
			this._tileObjectCoords.styleWidth = (this._tileObjectCoords.width + this._tileObjectCoords.padding) * this.Width + (int)this._tileObjectCoords.paddingFix.X;
			int num = 0;
			this._tileObjectCoords.styleHeight = 0;
			for (int i = 0; i < this._tileObjectCoords.heights.Length; i++)
			{
				num += this._tileObjectCoords.heights[i] + this._tileObjectCoords.padding;
			}
			num += (int)this._tileObjectCoords.paddingFix.Y;
			this._tileObjectCoords.styleHeight = num;
			if (this._hasOwnLiquidDeath)
			{
				if (this._liquidDeath.lava)
				{
					this.LavaPlacement = LiquidPlacement.NotAllowed;
				}
				if (this._liquidDeath.water)
				{
					this.WaterPlacement = LiquidPlacement.NotAllowed;
				}
			}
			if (this.Height == this._tileObjectCoords.heights.Length)
			{
				return;
			}
			if (this.Height < this._tileObjectCoords.heights.Length)
			{
				return;
			}
			TileObjectData.FixNewTile();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(145, 2);
			defaultInterpolatedStringHandler.AppendLiteral("TileObjectData Height and CoordinateHeights are incorrect. Height must equal the number of values in CoordinateHeights: [");
			defaultInterpolatedStringHandler.AppendFormatted(string.Join<int>(", ", this._tileObjectCoords.heights));
			defaultInterpolatedStringHandler.AppendLiteral("] is not ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.Height);
			defaultInterpolatedStringHandler.AppendLiteral(" elements long.");
			throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear())
			{
				HelpLink = "https://github.com/tModLoader/tModLoader/wiki/Basic-Tile"
			};
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x004C1F8B File Offset: 0x004C018B
		private void WriteCheck()
		{
			if (TileObjectData.readOnlyData)
			{
				throw new FieldAccessException("Tile data is locked and only accessible during startup.");
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x004C1F9F File Offset: 0x004C019F
		private void LockWrites()
		{
			TileObjectData.readOnlyData = true;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x004C1FA8 File Offset: 0x004C01A8
		public unsafe bool LiquidPlace(Tile checkTile, bool checkStay = false)
		{
			if (checkTile == null)
			{
				return false;
			}
			if (*checkTile.liquid > 0)
			{
				switch (checkTile.liquidType())
				{
				case 0:
				case 2:
				case 3:
					if (!checkStay || this.WaterDeath)
					{
						if (this.WaterPlacement == LiquidPlacement.NotAllowed)
						{
							return false;
						}
						if (this.WaterPlacement == LiquidPlacement.OnlyInFullLiquid && *checkTile.liquid != 255)
						{
							return false;
						}
					}
					break;
				case 1:
					if (!checkStay || this.LavaDeath)
					{
						if (this.LavaPlacement == LiquidPlacement.NotAllowed)
						{
							return false;
						}
						if (this.LavaPlacement == LiquidPlacement.OnlyInFullLiquid && *checkTile.liquid != 255)
						{
							return false;
						}
					}
					break;
				}
			}
			else
			{
				switch (checkTile.liquidType())
				{
				case 0:
				case 2:
				case 3:
					if (this.WaterPlacement == LiquidPlacement.OnlyInFullLiquid || this.WaterPlacement == LiquidPlacement.OnlyInLiquid)
					{
						return false;
					}
					break;
				case 1:
					if (this.LavaPlacement == LiquidPlacement.OnlyInFullLiquid || this.LavaPlacement == LiquidPlacement.OnlyInLiquid)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		/// <summary>
		/// If the tile type is in <see cref="P:Terraria.ObjectData.TileObjectData.AnchorInvalidTiles" />, returns false. If <see cref="P:Terraria.ObjectData.TileObjectData.AnchorValidTiles" /> exists and tile type isn't in it, returns false. Otherwise returns true.
		/// </summary>
		// Token: 0x060019EE RID: 6638 RVA: 0x004C20A0 File Offset: 0x004C02A0
		public bool isValidTileAnchor(int type)
		{
			int[] array;
			int[] array2;
			if (this._anchorTiles == null)
			{
				array = null;
				array2 = null;
			}
			else
			{
				array = this._anchorTiles.tileValid;
				array2 = this._anchorTiles.tileInvalid;
			}
			if (array2 != null)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (type == array2[i])
					{
						return false;
					}
				}
			}
			if (array == null)
			{
				return true;
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (type == array[j])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x004C2108 File Offset: 0x004C0308
		public bool isValidWallAnchor(int type)
		{
			int[] array = (this._anchorTiles != null) ? this._anchorTiles.wallValid : null;
			if (array == null)
			{
				return type != 0;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (type == array[i])
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// If the tile type is in <see cref="P:Terraria.ObjectData.TileObjectData.AnchorAlternateTiles" />, returns true.
		/// </summary>
		// Token: 0x060019F0 RID: 6640 RVA: 0x004C2150 File Offset: 0x004C0350
		public bool isValidAlternateAnchor(int type)
		{
			if (this._anchorTiles == null)
			{
				return false;
			}
			int[] tileAlternates = this._anchorTiles.tileAlternates;
			if (tileAlternates == null)
			{
				return false;
			}
			for (int i = 0; i < tileAlternates.Length; i++)
			{
				if (type == tileAlternates[i])
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Calculates a placement style from a provided tile style and random style offset. A placement style is the tile style multiplied by <see cref="P:Terraria.ObjectData.TileObjectData.StyleMultiplier" /> plus the alternate and random placement style offsets. 
		/// <para /> The <paramref name="alternate" /> parameter is unused, but the <see cref="P:Terraria.ObjectData.TileObjectData.Style" /> of this TileObjectData (equivalent to the value passed into <see cref="M:Terraria.ObjectData.TileObjectData.addAlternate(System.Int32)" />) is used to offset the style to account for alternate placements. As such, this method should be called on the alternate TileObjectData if applicable, not the root TileObjectData for this tile style. Do this by first retrieving it by calling the <see cref="M:Terraria.ObjectData.TileObjectData.GetTileData(Terraria.Tile)" /> method.
		/// </summary>
		// Token: 0x060019F1 RID: 6641 RVA: 0x004C2190 File Offset: 0x004C0390
		public int CalculatePlacementStyle(int style, int alternate, int random)
		{
			int num = style * this.StyleMultiplier;
			num += this.Style;
			if (random >= 0)
			{
				num += random;
			}
			return num;
		}

		/// <summary>
		/// Similar to <see cref="M:Terraria.ObjectData.TileObjectData.addTile(System.Int32)" /> except the TileObjectData is not registered to a specific tile type. Instead, the completed TileObjectData is assigned to <paramref name="baseTile" />. This can be used by modders to create their own tile templates.
		/// </summary>
		// Token: 0x060019F2 RID: 6642 RVA: 0x004C21B8 File Offset: 0x004C03B8
		public static void addBaseTile(out TileObjectData baseTile)
		{
			TileObjectData.newTile.Calculate();
			baseTile = TileObjectData.newTile;
			baseTile._parent = TileObjectData._baseObject;
			TileObjectData.newTile = new TileObjectData(TileObjectData._baseObject);
		}

		/// <summary>
		/// Registers the current <see cref="F:Terraria.ObjectData.TileObjectData.newTile" /> with the supplied tile type. Make sure to call this method last after all edits to <see cref="F:Terraria.ObjectData.TileObjectData.newTile" />, <see cref="F:Terraria.ObjectData.TileObjectData.newAlternate" />, and <see cref="F:Terraria.ObjectData.TileObjectData.newSubTile" />.
		/// </summary>
		// Token: 0x060019F3 RID: 6643 RVA: 0x004C21E6 File Offset: 0x004C03E6
		public static void addTile(int tileType)
		{
			TileObjectData.newTile.Calculate();
			TileObjectData._data[tileType] = TileObjectData.newTile;
			TileObjectData.newTile = new TileObjectData(TileObjectData._baseObject);
		}

		/// <inheritdoc cref="M:Terraria.ObjectData.TileObjectData.addSubTile(System.Int32)" />
		// Token: 0x060019F4 RID: 6644 RVA: 0x004C2214 File Offset: 0x004C0414
		public static void addSubTile(params int[] styles)
		{
			TileObjectData.newSubTile.Calculate();
			foreach (int num in styles)
			{
				List<TileObjectData> list;
				if (!TileObjectData.newTile._hasOwnSubTiles)
				{
					list = new List<TileObjectData>(num + 1);
					TileObjectData.newTile.SubTiles = list;
				}
				else
				{
					list = TileObjectData.newTile.SubTiles;
				}
				if (list.Count <= num)
				{
					for (int i = list.Count; i <= num; i++)
					{
						list.Add(null);
					}
				}
				TileObjectData.newSubTile._parent = TileObjectData.newTile;
				list[num] = TileObjectData.newSubTile;
			}
			TileObjectData.newSubTile = new TileObjectData(TileObjectData._baseObject);
		}

		/// <summary>
		/// Registers <see cref="F:Terraria.ObjectData.TileObjectData.newSubTile" /> as a subtile of <see cref="F:Terraria.ObjectData.TileObjectData.newTile" />. The style or styles parameter correspond to the tiles styles that should use these tile properties. This corresponds to the <see cref="F:Terraria.Item.placeStyle" /> of the item that will place this tile.
		/// <para /> Read the <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#multiple-styles">Multiple Styles section of the Basic Tile wiki guide</see> for more information and visualizations.
		/// </summary>
		// Token: 0x060019F5 RID: 6645 RVA: 0x004C22BC File Offset: 0x004C04BC
		private static void addSubTile(int style)
		{
			TileObjectData.newSubTile.Calculate();
			List<TileObjectData> list;
			if (!TileObjectData.newTile._hasOwnSubTiles)
			{
				list = new List<TileObjectData>(style + 1);
				TileObjectData.newTile.SubTiles = list;
			}
			else
			{
				list = TileObjectData.newTile.SubTiles;
			}
			if (list.Count <= style)
			{
				for (int i = list.Count; i <= style; i++)
				{
					list.Add(null);
				}
			}
			TileObjectData.newSubTile._parent = TileObjectData.newTile;
			list[style] = TileObjectData.newSubTile;
			TileObjectData.newSubTile = new TileObjectData(TileObjectData._baseObject);
		}

		/// <summary>
		/// Registers <see cref="F:Terraria.ObjectData.TileObjectData.newAlternate" /> as an alternate placement of <see cref="F:Terraria.ObjectData.TileObjectData.newTile" />. The <paramref name="baseStyle" /> parameter corresponds to which placement style sprite this alternate placement will use. This value will apply an offset to the tile style being placed. 
		/// <para /> <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/ExampleTorch.cs#L44">ExampleTorch</see>, for example, shows alternate placements using the 0, 1, and 2 placement style sprites, showing how different anchors can result in different placements sprites. <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/Furniture/ExampleDoorClosed.cs#L59">ExampleDoorClosed</see>, on the other hand, adds multiple alternates all using the same 0 placement sprite but different <see cref="P:Terraria.ObjectData.TileObjectData.Origin" /> to allow the user an easier time placing the door in a doorway.
		/// <para /> <see cref="P:Terraria.ObjectData.TileObjectData.StyleMultiplier" /> is usually used to account for alternate placements to allow them to be considered the same tile style. If <see cref="P:Terraria.ObjectData.TileObjectData.RandomStyleRange" /> is used, the random style will be added after the alternate placement style has been applied, so usually multiples of RandomStyleRange would be used for each addAlternate parameter. (For example, if RandomStyleRange is 5 for a tile with left and right placements, addAlternate(5) could be used for the right facing placement and StyleMultiplier should be set to 10.)
		/// </summary>
		// Token: 0x060019F6 RID: 6646 RVA: 0x004C234C File Offset: 0x004C054C
		public static void addAlternate(int baseStyle)
		{
			TileObjectData.newAlternate.Calculate();
			if (!TileObjectData.newTile._hasOwnAlternates)
			{
				TileObjectData.newTile.Alternates = new List<TileObjectData>();
			}
			TileObjectData.newAlternate.Style = baseStyle;
			TileObjectData.newAlternate._parent = TileObjectData.newTile;
			TileObjectData.newTile.Alternates.Add(TileObjectData.newAlternate);
			TileObjectData.newAlternate = new TileObjectData(TileObjectData._baseObject);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x004C23BC File Offset: 0x004C05BC
		public static void Initialize()
		{
			TileObjectData._baseObject = new TileObjectData(null);
			TileObjectData._baseObject.SetupBaseObject();
			TileObjectData._data = new List<TileObjectData>((int)TileID.Count);
			for (int i = 0; i < (int)TileID.Count; i++)
			{
				TileObjectData._data.Add(null);
			}
			TileObjectData.newTile = new TileObjectData(TileObjectData._baseObject);
			TileObjectData.newSubTile = new TileObjectData(TileObjectData._baseObject);
			TileObjectData.newAlternate = new TileObjectData(TileObjectData._baseObject);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 27;
			TileObjectData.newTile.StyleWrapLimit = 27;
			TileObjectData.newTile.UsesCustomCanPlace = false;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				13,
				47
			});
			TileObjectData.addSubTile(43);
			TileObjectData.addTile(19);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleMultiplier = 27;
			TileObjectData.newTile.StyleWrapLimit = 27;
			TileObjectData.newTile.UsesCustomCanPlace = false;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(427);
			for (int j = 435; j <= 439; j++)
			{
				TileObjectData.newTile.CoordinateHeights = new int[]
				{
					16
				};
				TileObjectData.newTile.CoordinateWidth = 16;
				TileObjectData.newTile.CoordinatePadding = 2;
				TileObjectData.newTile.StyleHorizontal = true;
				TileObjectData.newTile.StyleMultiplier = 27;
				TileObjectData.newTile.StyleWrapLimit = 27;
				TileObjectData.newTile.UsesCustomCanPlace = false;
				TileObjectData.newTile.LavaDeath = true;
				TileObjectData.addTile(j);
			}
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 8;
			TileObjectData.newTile.Origin = new Point16(1, 7);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData tileObjectData = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook;
			if ((hook = TileObjectData.<>O.<0>__PlaceXmasTree_Direct) == null)
			{
				hook = (TileObjectData.<>O.<0>__PlaceXmasTree_Direct = new Func<int, int, int, int, int, int, int>(WorldGen.PlaceXmasTree_Direct));
			}
			tileObjectData.HookPlaceOverride = new PlacementHook(hook, -1, 0, true);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.addTile(171);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.EmptyTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				38
			};
			TileObjectData.newTile.CoordinateWidth = 32;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.DrawYOffset = -20;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.addBaseTile(out TileObjectData.StyleDye);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleDye);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleDye);
			TileObjectData.newSubTile.AnchorValidWalls = new int[1];
			TileObjectData.addSubTile(3);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleDye);
			TileObjectData.newSubTile.AnchorValidWalls = new int[1];
			TileObjectData.addSubTile(4);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleDye);
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.OnlyInFullLiquid;
			TileObjectData.addSubTile(5);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleDye);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				80
			};
			TileObjectData.newSubTile.AnchorLeft = new AnchorData(AnchorType.EmptyTile, 1, 1);
			TileObjectData.newSubTile.AnchorRight = new AnchorData(AnchorType.EmptyTile, 1, 1);
			TileObjectData.addSubTile(6);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleDye);
			TileObjectData.newSubTile.DrawYOffset = -6;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.newSubTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newSubTile.Width, 0);
			TileObjectData.newSubTile.AnchorBottom = new AnchorData(AnchorType.EmptyTile, TileObjectData.newSubTile.Width, 0);
			TileObjectData.addSubTile(7);
			TileObjectData.addTile(227);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleDye);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.CoordinateWidth = 20;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newTile.AnchorTop = AnchorData.Empty;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.Table, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawFlipHorizontal = false;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(579);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = false;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.newTile.StyleLineSkip = 3;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 1);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 2);
			TileObjectData.addAlternate(0);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LinkedAlternates = true;
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				19,
				48
			});
			TileObjectData.addTile(10);
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = false;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.newTile.StyleLineSkip = 2;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 1);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 2);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 0);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 1);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 2);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LinkedAlternates = true;
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				19,
				48
			});
			TileObjectData.addTile(11);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				18,
				16,
				16,
				16,
				18
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleWrapLimit = 2;
			for (int k = 1; k < 5; k++)
			{
				TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
				TileObjectData.newAlternate.Origin = new Point16(0, k);
				TileObjectData.addAlternate(0);
			}
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			for (int l = 1; l < 5; l++)
			{
				TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
				TileObjectData.newAlternate.Origin = new Point16(0, l);
				TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
				TileObjectData.addAlternate(1);
			}
			TileObjectData.addTile(388);
			TileObjectData.newTile.FullCopyFrom(388);
			TileObjectData.addTile(389);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addBaseTile(out TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(13);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.DrawYOffset = -4;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				25,
				41
			});
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(39);
			TileObjectData.addTile(33);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.DrawYOffset = -4;
			TileObjectData.addTile(49);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData tileObjectData2 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook2;
			if ((hook2 = TileObjectData.<>O.<1>__Hook_AfterPlacement) == null)
			{
				hook2 = (TileObjectData.<>O.<1>__Hook_AfterPlacement = new Func<int, int, int, int, int, int, int>(TEFoodPlatter.Hook_AfterPlacement));
			}
			tileObjectData2.HookPostPlaceMyPlayer = new PlacementHook(hook2, -1, 0, true);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(520);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.DrawYOffset = -4;
			TileObjectData.addTile(372);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.DrawYOffset = -4;
			TileObjectData.addTile(646);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.RandomStyleRange = 5;
			TileObjectData.addTile(50);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(494);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(78);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.DrawYOffset = -4;
			TileObjectData.addTile(174);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addBaseTile(out TileObjectData.Style1xX);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
			TileObjectData.newTile.StyleWrapLimitVisualOverride = new int?(37);
			TileObjectData.newTile.StyleLineSkip = 2;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.WaterDeath = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				23,
				42
			});
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(40);
			TileObjectData.addTile(93);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
			TileObjectData.newTile.Height = 6;
			TileObjectData.newTile.Origin = new Point16(0, 5);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.addTile(92);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(453);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style1x2Top);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(270);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(271);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(581);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(660);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newTile.StyleWrapLimit = 6;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(572);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				32,
				48
			});
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(46);
			TileObjectData.addTile(42);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.StyleWrapLimit = 111;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(91);
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(487);
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addBaseTile(out TileObjectData.Style4x2);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LinkedAlternates = true;
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				25,
				42
			});
			TileObjectData.addTile(90);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, -2);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LinkedAlternates = true;
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				8,
				42
			});
			TileObjectData.addTile(79);
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 2, 1);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile(209);
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.DrawYOffset = 0;
			TileObjectData.addBaseTile(out TileObjectData.StyleSmallCage);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(285);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(286);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(582);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(619);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(298);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(299);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(310);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(532);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(533);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(339);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(538);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(555);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(556);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(629);
			TileObjectData.newTile.Width = 6;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(3, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.DrawYOffset = 0;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style6x3);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(275);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(276);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(413);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(414);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(277);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(278);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(279);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(280);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(281);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(632);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(640);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(643);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(644);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(645);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(296);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(297);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(309);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(550);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(551);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(553);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(554);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(558);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(559);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(599);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(600);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(601);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(602);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(603);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(604);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(605);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(606);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(607);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(608);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(609);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(610);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(611);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(612);
			TileObjectData.newTile.Width = 5;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Origin = new Point16(2, 3);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style5x4);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
			TileObjectData.addTile(464);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
			TileObjectData.addTile(466);
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style2x1);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(29);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(103);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(462);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				18
			};
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				14,
				43
			});
			TileObjectData.addTile(18);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				18
			};
			TileObjectData.addTile(16);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(134);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.AnchorLeft = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Height, 0);
			TileObjectData.newTile.AnchorRight = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Height, 0);
			TileObjectData.addTile(387);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleWrapLimit = 53;
			TileObjectData.addTile(649);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -2;
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -2;
			TileObjectData.addAlternate(3);
			TileObjectData.addTile(443);
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addBaseTile(out TileObjectData.Style2xX);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.Origin = new Point16(1, 4);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(547);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.Origin = new Point16(1, 4);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(623);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Origin = new Point16(1, 3);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(207);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				18
			};
			TileObjectData.addTile(410);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(480);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(509);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(657);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(658);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.addTile(489);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleWrapLimit = 7;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(7);
			TileObjectData.addTile(349);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.addTile(337);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(560);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorBottom = default(AnchorData);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.PlatformNonHammered, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(465);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorBottom = default(AnchorData);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(531);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.addTile(320);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.addTile(456);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData tileObjectData3 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook3;
			if ((hook3 = TileObjectData.<>O.<2>__Hook_AfterPlacement) == null)
			{
				hook3 = (TileObjectData.<>O.<2>__Hook_AfterPlacement = new Func<int, int, int, int, int, int, int>(TETrainingDummy.Hook_AfterPlacement));
			}
			tileObjectData3.HookPostPlaceMyPlayer = new PlacementHook(hook3, -1, 0, false);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(378);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleWrapLimit = 55;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(165);
			TileObjectData.addTile(105);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.newTile.RandomStyleRange = 2;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(2);
			TileObjectData.addTile(545);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.Origin = new Point16(0, 4);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				17,
				43
			});
			TileObjectData.addTile(104);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(128);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(506);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(269);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.DrawStyleOffset = 4;
			TileObjectData tileObjectData4 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook4;
			if ((hook4 = TileObjectData.<>O.<3>__Hook_AfterPlacement) == null)
			{
				hook4 = (TileObjectData.<>O.<3>__Hook_AfterPlacement = new Func<int, int, int, int, int, int, int>(TEDisplayDoll.Hook_AfterPlacement));
			}
			tileObjectData4.HookPostPlaceMyPlayer = new PlacementHook(hook4, -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[]
			{
				127,
				138,
				664,
				665,
				484
			};
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(470);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorBottom = default(AnchorData);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.PlatformNonHammered, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(591);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorBottom = default(AnchorData);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.PlatformNonHammered, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawYOffset = -10;
			TileObjectData.addAlternate(0);
			TileObjectData.addTile(592);
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addBaseTile(out TileObjectData.Style3x3);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Height = 6;
			TileObjectData.newTile.Origin = new Point16(1, 5);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(7);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(8);
			TileObjectData.addTile(548);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.Origin = new Point16(1, 4);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(613);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Height = 6;
			TileObjectData.newTile.Origin = new Point16(1, 5);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(614);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Origin = new Point16(1, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 1, 1);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.StyleWrapLimit = 37;
			TileObjectData.newTile.StyleHorizontal = false;
			TileObjectData.newTile.StyleLineSkip = 2;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				32,
				48
			});
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(46);
			TileObjectData.addTile(34);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Origin = new Point16(2, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 1, 1);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.addTile(454);
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style3x2);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newSubTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(13);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newSubTile.Height = 1;
			TileObjectData.newSubTile.Origin = new Point16(1, 0);
			TileObjectData.newSubTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.addSubTile(25);
			TileObjectData.addTile(14);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newSubTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(11);
			TileObjectData.addTile(469);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleWrapLimitVisualOverride = new int?(37);
			TileObjectData tileObjectData5 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook5;
			if ((hook5 = TileObjectData.<>O.<4>__FindEmptyChest) == null)
			{
				hook5 = (TileObjectData.<>O.<4>__FindEmptyChest = new Func<int, int, int, int, int, int, int>(Chest.FindEmptyChest));
			}
			tileObjectData5.HookCheckIfCanPlace = new PlacementHook(hook5, -1, 0, true);
			TileObjectData tileObjectData6 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook6;
			if ((hook6 = TileObjectData.<>O.<5>__AfterPlacement_Hook) == null)
			{
				hook6 = (TileObjectData.<>O.<5>__AfterPlacement_Hook = new Func<int, int, int, int, int, int, int>(Chest.AfterPlacement_Hook));
			}
			tileObjectData6.HookPostPlaceMyPlayer = new PlacementHook(hook6, -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[]
			{
				127,
				138,
				664,
				665,
				484
			};
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				9,
				42
			});
			TileObjectData.addTile(88);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addTile(237);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(244);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(647);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleWrapLimit = 35;
			TileObjectData.addTile(648);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(651);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.addTile(26);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.addTile(86);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(377);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleWrapLimitVisualOverride = new int?(37);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				15,
				42
			});
			TileObjectData.addTile(87);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.addTile(486);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				2,
				477,
				109,
				492
			};
			TileObjectData.addTile(488);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleWrapLimitVisualOverride = new int?(37);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				10,
				46
			});
			TileObjectData.addTile(89);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(114);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				59,
				70
			};
			TileObjectData.addSubTile(new int[]
			{
				32,
				33,
				34
			});
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				147,
				161,
				163,
				200,
				164,
				162,
				224
			};
			TileObjectData.addSubTile(new int[]
			{
				26,
				27,
				28,
				29,
				30,
				31
			});
			TileObjectData.addTile(186);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleWrapLimit = 35;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				59,
				60,
				226
			};
			TileObjectData.addSubTile(new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5
			});
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				57,
				58,
				75,
				76
			};
			TileObjectData.addSubTile(new int[]
			{
				6,
				7,
				8
			});
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				53,
				397,
				396,
				112,
				398,
				400,
				234,
				399,
				401,
				116,
				402,
				403
			};
			TileObjectData.addSubTile(new int[]
			{
				29,
				30,
				31,
				32,
				33,
				34
			});
			TileObjectData.addTile(187);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				53,
				112,
				234,
				116
			};
			TileObjectData.newTile.WaterDeath = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.RandomStyleRange = 4;
			TileObjectData.addTile(552);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleWrapLimit = 16;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.WaterDeath = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(1);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(4);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(9);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(1 + TileObjectData.newTile.StyleWrapLimit);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(4 + TileObjectData.newTile.StyleWrapLimit);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(9 + TileObjectData.newTile.StyleWrapLimit);
			TileObjectData.addTile(215);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(217);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(218);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.addTile(17);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(77);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(133);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.addTile(405);
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Origin = new Point16(1, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile(235);
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Origin = new Point16(1, 3);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style3x4);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.StyleWrapLimitVisualOverride = new int?(37);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				4,
				43
			});
			TileObjectData.addTile(101);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(102);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(463);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData tileObjectData7 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook7;
			if ((hook7 = TileObjectData.<>O.<6>__Hook_AfterPlacement) == null)
			{
				hook7 = (TileObjectData.<>O.<6>__Hook_AfterPlacement = new Func<int, int, int, int, int, int, int>(TEHatRack.Hook_AfterPlacement));
			}
			tileObjectData7.HookPostPlaceMyPlayer = new PlacementHook(hook7, -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[]
			{
				127,
				138,
				664,
				665,
				484
			};
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(475);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData tileObjectData8 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook8;
			if ((hook8 = TileObjectData.<>O.<7>__PlacementPreviewHook_CheckIfCanPlace) == null)
			{
				hook8 = (TileObjectData.<>O.<7>__PlacementPreviewHook_CheckIfCanPlace = new Func<int, int, int, int, int, int, int>(TETeleportationPylon.PlacementPreviewHook_CheckIfCanPlace));
			}
			tileObjectData8.HookCheckIfCanPlace = new PlacementHook(hook8, 1, 0, true);
			TileObjectData tileObjectData9 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook9;
			if ((hook9 = TileObjectData.<>O.<8>__PlacementPreviewHook_AfterPlacement) == null)
			{
				hook9 = (TileObjectData.<>O.<8>__PlacementPreviewHook_AfterPlacement = new Func<int, int, int, int, int, int, int>(TETeleportationPylon.PlacementPreviewHook_AfterPlacement));
			}
			tileObjectData9.HookPostPlaceMyPlayer = new PlacementHook(hook9, -1, 0, false);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(597);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleHorizontal = false;
			TileObjectData.newTile.StyleWrapLimitVisualOverride = new int?(2);
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.styleLineSkipVisualOverride = new int?(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(617);
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style2x2);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData tileObjectData10 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook10;
			if ((hook10 = TileObjectData.<>O.<4>__FindEmptyChest) == null)
			{
				hook10 = (TileObjectData.<>O.<4>__FindEmptyChest = new Func<int, int, int, int, int, int, int>(Chest.FindEmptyChest));
			}
			tileObjectData10.HookCheckIfCanPlace = new PlacementHook(hook10, -1, 0, true);
			TileObjectData tileObjectData11 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook11;
			if ((hook11 = TileObjectData.<>O.<5>__AfterPlacement_Hook) == null)
			{
				hook11 = (TileObjectData.<>O.<5>__AfterPlacement_Hook = new Func<int, int, int, int, int, int, int>(Chest.AfterPlacement_Hook));
			}
			tileObjectData11.HookPostPlaceMyPlayer = new PlacementHook(hook11, -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[]
			{
				127,
				138,
				664,
				665,
				484
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(21);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData tileObjectData12 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook12;
			if ((hook12 = TileObjectData.<>O.<4>__FindEmptyChest) == null)
			{
				hook12 = (TileObjectData.<>O.<4>__FindEmptyChest = new Func<int, int, int, int, int, int, int>(Chest.FindEmptyChest));
			}
			tileObjectData12.HookCheckIfCanPlace = new PlacementHook(hook12, -1, 0, true);
			TileObjectData tileObjectData13 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook13;
			if ((hook13 = TileObjectData.<>O.<5>__AfterPlacement_Hook) == null)
			{
				hook13 = (TileObjectData.<>O.<5>__AfterPlacement_Hook = new Func<int, int, int, int, int, int, int>(Chest.AfterPlacement_Hook));
			}
			tileObjectData13.HookPostPlaceMyPlayer = new PlacementHook(hook13, -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[]
			{
				127,
				138,
				664,
				665,
				484
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(467);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.AnchorInvalidTiles = new int[]
			{
				127,
				138,
				664,
				665,
				484
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(441);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.AnchorInvalidTiles = new int[]
			{
				127,
				138,
				664,
				665,
				484
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(468);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.StyleWrapLimit = 6;
			TileObjectData.newTile.StyleMultiplier = 6;
			TileObjectData.newTile.RandomStyleRange = 6;
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				2,
				477,
				109,
				492
			};
			TileObjectData.addTile(254);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(96);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.StyleWrapLimit = 4;
			TileObjectData.newTile.StyleMultiplier = 1;
			TileObjectData.newTile.RandomStyleRange = 4;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(485);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.RandomStyleRange = 5;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(457);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(490);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleWrapLimitVisualOverride = new int?(56);
			TileObjectData.newTile.styleLineSkipVisualOverride = new int?(2);
			TileObjectData.addTile(139);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.RandomStyleRange = 9;
			TileObjectData.addTile(35);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(652);
			int styleWrapLimit = 3;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = styleWrapLimit;
			TileObjectData.addTile(653);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(1, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.addTile(95);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(1, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.addTile(126);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(1, 0);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.addTile(444);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.WaterDeath = true;
			TileObjectData.addTile(98);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				13,
				43
			});
			TileObjectData.addTile(172);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(94);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(411);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(97);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(99);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				25,
				42
			});
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(40);
			TileObjectData.addTile(100);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(125);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(621);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(622);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(173);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(287);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(319);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(287);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(376);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(138);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(664);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(654);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addTile(484);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(142);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(143);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(282);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(543);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(598);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(568);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(569);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(570);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(288);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(289);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(290);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(291);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(292);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(293);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(294);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(295);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(316);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(317);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(318);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(360);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(580);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(620);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(565);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(521);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(522);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(523);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(524);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(525);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(526);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(527);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(505);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(358);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(359);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile(542);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(361);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(362);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(363);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(364);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(544);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(391);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(392);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(393);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
			TileObjectData.addTile(394);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(287);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(335);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(564);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(594);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(354);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(355);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(491);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.addTile(356);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.addTile(663);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.AnchorLeft = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newTile.AnchorRight = new AnchorData(AnchorType.SolidTile, 1, 1);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.addTile(386);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.addAlternate(2);
			TileObjectData.addTile(132);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 0);
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 0);
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(3);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(4);
			TileObjectData.addTile(55);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 0);
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 0);
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(3);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(4);
			TileObjectData.addTile(573);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 0);
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 0);
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(3);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(4);
			TileObjectData.addTile(425);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 0);
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 0);
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(3);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(4);
			TileObjectData.addTile(510);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 0);
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 0);
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(3);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(4);
			TileObjectData.addTile(511);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(85);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData tileObjectData14 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook14;
			if ((hook14 = TileObjectData.<>O.<9>__Hook_AfterPlacement) == null)
			{
				hook14 = (TileObjectData.<>O.<9>__Hook_AfterPlacement = new Func<int, int, int, int, int, int, int>(TEItemFrame.Hook_AfterPlacement));
			}
			tileObjectData14.HookPostPlaceMyPlayer = new PlacementHook(hook14, -1, 0, true);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 0);
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(1, 0);
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 2, 0);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(3);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = Point16.Zero;
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.addAlternate(4);
			TileObjectData.addTile(395);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(12);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(665);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(639);
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style3x3);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.addTile(106);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(212);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(219);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(642);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(220);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(228);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(231);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(243);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(247);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(283);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(300);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(301);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(302);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(303);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(304);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(305);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(306);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(307);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(308);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.addTile(406);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.addTile(452);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(412);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(455);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(499);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style1x2);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LinkedAlternates = true;
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				16,
				47
			});
			TileObjectData.addTile(15);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LinkedAlternates = true;
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(new int[]
			{
				14,
				42
			});
			TileObjectData.addTile(497);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				20
			};
			TileObjectData.addTile(216);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(390);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.addTile(338);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 6;
			TileObjectData.newTile.DrawStyleOffset = 13 * TileObjectData.newTile.StyleWrapLimit;
			TileObjectData.addTile(493);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.RandomStyleRange = 5;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				18,
				18
			};
			TileObjectData.newTile.CoordinateWidth = 26;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.addTile(567);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style1x1);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(420);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.addTile(624);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.addTile(656);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				18
			};
			TileObjectData.newTile.CoordinateWidth = 20;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(476);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
				420,
				419
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(419);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData tileObjectData15 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook15;
			if ((hook15 = TileObjectData.<>O.<10>__Hook_AfterPlacement) == null)
			{
				hook15 = (TileObjectData.<>O.<10>__Hook_AfterPlacement = new Func<int, int, int, int, int, int, int>(TELogicSensor.Hook_AfterPlacement));
			}
			tileObjectData15.HookPostPlaceMyPlayer = new PlacementHook(hook15, -1, 0, true);
			TileObjectData.addTile(423);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(424);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(445);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(429);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.EmptyTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				26
			};
			TileObjectData.newTile.CoordinateWidth = 24;
			TileObjectData.newTile.DrawYOffset = -8;
			TileObjectData.newTile.RandomStyleRange = 6;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(81);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				18
			};
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(135);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				18
			};
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(428);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.RandomStyleRange = 2;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(141);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(144);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(210);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(239);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(650);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.RandomStyleRange = 7;
			TileObjectData.addTile(36);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.newTile.StyleMultiplier = 3;
			TileObjectData.newTile.StyleWrapLimit = 3;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.CoordinateWidth = 20;
			TileObjectData.newTile.DrawYOffset = -2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(324);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(593);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
				124,
				561,
				574,
				575,
				576,
				577,
				578
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.AlternateTile | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
			TileObjectData.addAlternate(3);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.addAlternate(4);
			TileObjectData.addTile(630);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
				124,
				561,
				574,
				575,
				576,
				577,
				578
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.AlternateTile | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
			TileObjectData.addAlternate(3);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.addAlternate(4);
			TileObjectData.addTile(631);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.FlattenAnchors = true;
			TileObjectData.addBaseTile(out TileObjectData.StyleSwitch);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleSwitch);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleSwitch);
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
			TileObjectData.newAlternate.AnchorAlternateTiles = new int[]
			{
				124,
				561,
				574,
				575,
				576,
				577,
				578
			};
			TileObjectData.newAlternate.DrawXOffset = -2;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleSwitch);
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
			TileObjectData.newAlternate.AnchorAlternateTiles = new int[]
			{
				124,
				561,
				574,
				575,
				576,
				577,
				578
			};
			TileObjectData.newAlternate.DrawXOffset = 2;
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleSwitch);
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.addAlternate(3);
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(136);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.FlattenAnchors = true;
			TileObjectData.newTile.UsesCustomCanPlace = false;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.DrawStepDown = 2;
			TileObjectData.newTile.CoordinateWidth = 20;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleMultiplier = 6;
			TileObjectData.newTile.StyleWrapLimit = 6;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.WaterDeath = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.StyleTorch);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleTorch);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
			TileObjectData.newAlternate.AnchorAlternateTiles = new int[]
			{
				124,
				561,
				574,
				575,
				576,
				577,
				578
			};
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
			TileObjectData.newAlternate.AnchorAlternateTiles = new int[]
			{
				124,
				561,
				574,
				575,
				576,
				577,
				578
			};
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
			TileObjectData.newAlternate.AnchorWall = true;
			TileObjectData.addAlternate(0);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LinkedAlternates = true;
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(8);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LinkedAlternates = true;
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(11);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.LinkedAlternates = true;
			TileObjectData.newSubTile.WaterDeath = false;
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(17);
			TileObjectData.addTile(4);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.FlattenAnchors = true;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.DrawStepDown = 2;
			TileObjectData.newTile.CoordinateWidth = 20;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.WaterDeath = false;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.StyleWrapLimit = 4;
			TileObjectData.newTile.StyleMultiplier = 4;
			TileObjectData tileObjectData16 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook16;
			if ((hook16 = TileObjectData.<>O.<11>__CanPlaceProjectilePressurePad) == null)
			{
				hook16 = (TileObjectData.<>O.<11>__CanPlaceProjectilePressurePad = new Func<int, int, int, int, int, int, int>(WorldGen.CanPlaceProjectilePressurePad));
			}
			tileObjectData16.HookCheckIfCanPlace = new PlacementHook(hook16, -1, 0, true);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile | AnchorType.EmptyTile | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.DrawStepDown = 0;
			TileObjectData.newAlternate.DrawYOffset = -4;
			TileObjectData.addAlternate(1);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile | AnchorType.EmptyTile | AnchorType.SolidBottom, TileObjectData.newTile.Height, 0);
			TileObjectData.newAlternate.AnchorAlternateTiles = new int[]
			{
				124,
				561,
				574,
				575,
				576,
				577,
				578
			};
			TileObjectData.newAlternate.DrawXOffset = -2;
			TileObjectData.newAlternate.DrawYOffset = -2;
			TileObjectData.addAlternate(2);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile | AnchorType.EmptyTile | AnchorType.SolidBottom, TileObjectData.newTile.Height, 0);
			TileObjectData.newAlternate.AnchorAlternateTiles = new int[]
			{
				124,
				561,
				574,
				575,
				576,
				577,
				578
			};
			TileObjectData.newAlternate.DrawXOffset = 2;
			TileObjectData.newAlternate.DrawYOffset = -2;
			TileObjectData.addAlternate(3);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile | AnchorType.EmptyTile | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(442);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Origin = Point16.Zero;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.DrawYOffset = -1;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.AlternateTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.addBaseTile(out TileObjectData.StyleAlch);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				2,
				477,
				109,
				492
			};
			TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
				78
			};
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				60
			};
			TileObjectData.newSubTile.AnchorAlternateTiles = new int[]
			{
				78
			};
			TileObjectData.addSubTile(1);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				0,
				59
			};
			TileObjectData.newSubTile.AnchorAlternateTiles = new int[]
			{
				78
			};
			TileObjectData.addSubTile(2);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				199,
				203,
				25,
				23
			};
			TileObjectData.newSubTile.AnchorAlternateTiles = new int[]
			{
				78
			};
			TileObjectData.addSubTile(3);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				53,
				116
			};
			TileObjectData.newSubTile.AnchorAlternateTiles = new int[]
			{
				78
			};
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(4);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				57,
				633
			};
			TileObjectData.newSubTile.AnchorAlternateTiles = new int[]
			{
				78
			};
			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
			TileObjectData.newSubTile.LavaDeath = false;
			TileObjectData.addSubTile(5);
			TileObjectData.newSubTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newSubTile.AnchorValidTiles = new int[]
			{
				147,
				161,
				163,
				164,
				200
			};
			TileObjectData.newSubTile.AnchorAlternateTiles = new int[]
			{
				78
			};
			TileObjectData.newSubTile.WaterPlacement = LiquidPlacement.Allowed;
			TileObjectData.addSubTile(6);
			TileObjectData.addTile(82);
			TileObjectData.newTile.FullCopyFrom(82);
			TileObjectData.addTile(83);
			TileObjectData.newTile.FullCopyFrom(83);
			TileObjectData.addTile(84);
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.AnchorWall = true;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.addBaseTile(out TileObjectData.Style3x3Wall);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(240);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(440);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(334);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData tileObjectData17 = TileObjectData.newTile;
			Func<int, int, int, int, int, int, int> hook17;
			if ((hook17 = TileObjectData.<>O.<12>__Hook_AfterPlacement) == null)
			{
				hook17 = (TileObjectData.<>O.<12>__Hook_AfterPlacement = new Func<int, int, int, int, int, int, int>(TEWeaponsRack.Hook_AfterPlacement));
			}
			tileObjectData17.HookPostPlaceMyPlayer = new PlacementHook(hook17, -1, 0, true);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(471);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newSubTile.RandomStyleRange = 4;
			TileObjectData.addSubTile(15);
			TileObjectData.addTile(245);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(1, 0);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.addTile(246);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.RandomStyleRange = 9;
			TileObjectData.addTile(241);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.Width = 6;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Origin = new Point16(2, 2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.StyleWrapLimit = 27;
			TileObjectData.addTile(242);
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Origin = new Point16(0, 3);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				18
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				2,
				477,
				109,
				60,
				492,
				633
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.addTile(27);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				2,
				477
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorValidTiles = new int[]
			{
				147
			};
			TileObjectData.addAlternate(3);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorValidTiles = new int[]
			{
				60
			};
			TileObjectData.addAlternate(6);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorValidTiles = new int[]
			{
				23,
				661
			};
			TileObjectData.addAlternate(9);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorValidTiles = new int[]
			{
				199,
				662
			};
			TileObjectData.addAlternate(12);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorValidTiles = new int[]
			{
				109,
				492
			};
			TileObjectData.addAlternate(15);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorValidTiles = new int[]
			{
				53
			};
			TileObjectData.addAlternate(18);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorValidTiles = new int[]
			{
				116
			};
			TileObjectData.addAlternate(21);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorValidTiles = new int[]
			{
				234
			};
			TileObjectData.addAlternate(24);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorValidTiles = new int[]
			{
				112
			};
			TileObjectData.addAlternate(27);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorValidTiles = new int[]
			{
				633
			};
			TileObjectData.addAlternate(30);
			TileObjectData.addTile(20);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				1,
				25,
				117,
				203,
				182,
				180,
				179,
				381,
				183,
				181,
				534,
				536,
				539,
				625,
				627
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.newTile.StyleMultiplier = 3;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(590);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				2,
				477,
				492,
				60,
				109,
				199,
				23
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.newTile.StyleMultiplier = 3;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(595);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				2,
				477,
				492,
				60,
				109,
				199,
				23
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.newTile.StyleMultiplier = 3;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(615);
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x004C9FB8 File Offset: 0x004C81B8
		public static bool CustomPlace(int type, int style)
		{
			if (type < 0 || type >= TileObjectData._data.Count || style < 0)
			{
				return false;
			}
			TileObjectData tileObjectData = TileObjectData._data[type];
			if (tileObjectData == null)
			{
				return false;
			}
			List<TileObjectData> subTiles = tileObjectData.SubTiles;
			if (subTiles != null && style < subTiles.Count)
			{
				TileObjectData tileObjectData2 = subTiles[style];
				if (tileObjectData2 != null)
				{
					return tileObjectData2._usesCustomCanPlace;
				}
			}
			return tileObjectData._usesCustomCanPlace;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x004CA018 File Offset: 0x004C8218
		public static bool CheckLiquidPlacement(int type, int style, Tile checkTile)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, 0);
			if (tileData == null)
			{
				return TileObjectData.LiquidPlace(type, checkTile);
			}
			return tileData.LiquidPlace(checkTile, false);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x004CA038 File Offset: 0x004C8238
		public unsafe static bool LiquidPlace(int type, Tile checkTile)
		{
			if (checkTile == null)
			{
				return false;
			}
			if (*checkTile.liquid > 0)
			{
				switch (checkTile.liquidType())
				{
				case 0:
				case 2:
				case 3:
					if (Main.tileWaterDeath[type])
					{
						return false;
					}
					break;
				case 1:
					if (Main.tileLavaDeath[type])
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x004CA092 File Offset: 0x004C8292
		public static bool CheckWaterDeath(int type, int style)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, 0);
			if (tileData == null)
			{
				return Main.tileWaterDeath[type];
			}
			return tileData.WaterDeath;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x004CA0AD File Offset: 0x004C82AD
		public unsafe static bool CheckWaterDeath(Tile checkTile)
		{
			if (!checkTile.active())
			{
				return false;
			}
			TileObjectData tileData = TileObjectData.GetTileData(checkTile);
			if (tileData == null)
			{
				return Main.tileWaterDeath[(int)(*checkTile.type)];
			}
			return tileData.WaterDeath;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x004CA0D8 File Offset: 0x004C82D8
		public static bool CheckLavaDeath(int type, int style)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, 0);
			if (tileData == null)
			{
				return Main.tileLavaDeath[type];
			}
			return tileData.LavaDeath;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x004CA0F3 File Offset: 0x004C82F3
		public unsafe static bool CheckLavaDeath(Tile checkTile)
		{
			if (!checkTile.active())
			{
				return false;
			}
			TileObjectData tileData = TileObjectData.GetTileData(checkTile);
			if (tileData == null)
			{
				return Main.tileLavaDeath[(int)(*checkTile.type)];
			}
			return tileData.LavaDeath;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x004CA11E File Offset: 0x004C831E
		public static int PlatformFrameWidth()
		{
			return TileObjectData._data[19].CoordinateFullWidth;
		}

		/// <summary>
		/// Retrieves the TileObjectData corresponding to the passed in tile <paramref name="type" />, tile <paramref name="style" />, and optional <paramref name="alternate" /> parameter. Terrain tiles will return null.
		/// <para /> Note that the alternate parameter in this method retrieves the alternate TileObjectData according to the order that they were registered, not according to a calculated alternate placement style. This parameter counts from 1, not 0, so the default parameter value of 0 will not attempt to retrieve an alternate TileObjectData. As such, it is not suitable for retrieving the TileObjectData of a placed Tile, use  <see cref="M:Terraria.ObjectData.TileObjectData.GetTileData(Terraria.Tile)" /> for that.
		/// <para /> Other related methods include <see cref="M:Terraria.ObjectData.TileObjectData.GetTileData(Terraria.Tile)" />, <see cref="M:Terraria.ObjectData.TileObjectData.GetTileInfo(Terraria.Tile,System.Int32@,System.Int32@)" />, and <see cref="M:Terraria.ObjectData.TileObjectData.GetTileStyle(Terraria.Tile)" />.
		/// </summary>
		// Token: 0x06001A00 RID: 6656 RVA: 0x004CA134 File Offset: 0x004C8334
		public static TileObjectData GetTileData(int type, int style, int alternate = 0)
		{
			if (type < 0 || type >= TileObjectData._data.Count)
			{
				throw new ArgumentOutOfRangeException("Function called with a bad type argument");
			}
			if (style < 0)
			{
				throw new ArgumentOutOfRangeException("Function called with a bad style argument");
			}
			TileObjectData tileObjectData = TileObjectData._data[type];
			if (tileObjectData == null)
			{
				return null;
			}
			List<TileObjectData> subTiles = tileObjectData.SubTiles;
			if (subTiles != null && style < subTiles.Count)
			{
				TileObjectData tileObjectData2 = subTiles[style];
				if (tileObjectData2 != null)
				{
					tileObjectData = tileObjectData2;
				}
			}
			alternate--;
			List<TileObjectData> alternates = tileObjectData.Alternates;
			if (alternates != null && alternate >= 0 && alternate < alternates.Count)
			{
				TileObjectData tileObjectData3 = alternates[alternate];
				if (tileObjectData3 != null)
				{
					tileObjectData = tileObjectData3;
				}
			}
			return tileObjectData;
		}

		/// <summary>
		/// Retrieves the TileObjectData corresponding to the passed in Tile. Empty tiles and terrain tiles will return null. Any individual Tile of the multitile works. Will correctly retrieve the style or alternate specific TileObjectData if it exists.
		/// <para /> Other related methods include <see cref="M:Terraria.ObjectData.TileObjectData.GetTileData(System.Int32,System.Int32,System.Int32)" />, <see cref="M:Terraria.ObjectData.TileObjectData.GetTileInfo(Terraria.Tile,System.Int32@,System.Int32@)" />, and <see cref="M:Terraria.ObjectData.TileObjectData.GetTileStyle(Terraria.Tile)" />.
		/// </summary>
		// Token: 0x06001A01 RID: 6657 RVA: 0x004CA1CC File Offset: 0x004C83CC
		public unsafe static TileObjectData GetTileData(Tile getTile)
		{
			if (getTile == null || !getTile.active())
			{
				return null;
			}
			int type = (int)(*getTile.type);
			if (type < 0 || type >= TileObjectData._data.Count)
			{
				throw new ArgumentOutOfRangeException("Function called with a bad tile type");
			}
			TileObjectData tileObjectData = TileObjectData._data[type];
			if (tileObjectData == null)
			{
				return null;
			}
			int num = (int)(*getTile.frameX) / tileObjectData.CoordinateFullWidth;
			int num2 = (int)(*getTile.frameY) / tileObjectData.CoordinateFullHeight;
			int num3 = tileObjectData.StyleWrapLimit;
			if (num3 == 0)
			{
				num3 = 1;
			}
			int styleLineSkip = tileObjectData.StyleLineSkip;
			int num6 = (!tileObjectData.StyleHorizontal) ? (num / styleLineSkip * num3 + num2) : (num2 / styleLineSkip * num3 + num);
			int num4 = num6 / tileObjectData.StyleMultiplier;
			int num5 = num6 % tileObjectData.StyleMultiplier;
			if (tileObjectData.SubTiles != null && num4 >= 0 && num4 < tileObjectData.SubTiles.Count)
			{
				TileObjectData tileObjectData2 = tileObjectData.SubTiles[num4];
				if (tileObjectData2 != null)
				{
					tileObjectData = tileObjectData2;
				}
			}
			if (tileObjectData._alternates != null)
			{
				for (int i = 0; i < tileObjectData.Alternates.Count; i++)
				{
					TileObjectData tileObjectData3 = tileObjectData.Alternates[i];
					if (tileObjectData3 != null && num5 >= tileObjectData3.Style && num5 <= tileObjectData3.Style + tileObjectData3.RandomStyleRange)
					{
						return tileObjectData3;
					}
				}
			}
			return tileObjectData;
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x004CA314 File Offset: 0x004C8514
		public static void SyncObjectPlacement(int tileX, int tileY, int type, int style, int dir)
		{
			NetMessage.SendData(17, -1, -1, null, 1, (float)tileX, (float)tileY, (float)type, style, 0, 0);
			TileObjectData.GetTileData(type, style, 0);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x004CA340 File Offset: 0x004C8540
		public static bool CallPostPlacementPlayerHook(int tileX, int tileY, int type, int style, int dir, int alternate, TileObject data)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, data.alternate);
			if (tileData == null || tileData._placementHooks == null || tileData._placementHooks.postPlaceMyPlayer.hook == null)
			{
				return false;
			}
			PlacementHook postPlaceMyPlayer = tileData._placementHooks.postPlaceMyPlayer;
			if (postPlaceMyPlayer.processedCoordinates)
			{
				tileX -= (int)tileData.Origin.X;
				tileY -= (int)tileData.Origin.Y;
			}
			return postPlaceMyPlayer.hook(tileX, tileY, type, style, dir, data.alternate) == postPlaceMyPlayer.badReturn;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x004CA3D0 File Offset: 0x004C85D0
		public static void OriginToTopLeft(int type, int style, ref Point16 baseCoords)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, 0);
			if (tileData != null)
			{
				baseCoords = new Point16((int)(baseCoords.X - tileData.Origin.X), (int)(baseCoords.Y - tileData.Origin.Y));
			}
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x004CA418 File Offset: 0x004C8618
		public static void FixNewTile()
		{
			TileObjectData.newTile = new TileObjectData(TileObjectData._baseObject);
		}

		/// <summary>
		/// Retrieves the tile style corresponding to the passed in Tile. Empty tiles and terrain tiles will return -1. Any Tile of the multitile works.
		/// <para /> This is most useful in replacing hard-coded math where the tile style is calculated from <see cref="P:Terraria.Tile.TileFrameX" /> and <see cref="P:Terraria.Tile.TileFrameY" /> directly, such as mouse over icons and other tile style specific behaviors.
		/// <para /> Other related methods include <see cref="M:Terraria.ObjectData.TileObjectData.GetTileData(Terraria.Tile)" />, <see cref="M:Terraria.ObjectData.TileObjectData.GetTileData(System.Int32,System.Int32,System.Int32)" />, and <see cref="M:Terraria.ObjectData.TileObjectData.GetTileInfo(Terraria.Tile,System.Int32@,System.Int32@)" />.
		/// </summary>
		// Token: 0x06001A06 RID: 6662 RVA: 0x004CA42C File Offset: 0x004C862C
		public unsafe static int GetTileStyle(Tile getTile)
		{
			if (getTile == null || !getTile.active())
			{
				return -1;
			}
			int type = (int)(*getTile.type);
			if (type < 0 || type >= TileObjectData._data.Count)
			{
				throw new ArgumentOutOfRangeException("getTile", "Function called with a bad tile type");
			}
			TileObjectData tileObjectData = TileObjectData._data[type];
			if (tileObjectData == null)
			{
				return -1;
			}
			int num = (int)(*getTile.frameX) / tileObjectData.CoordinateFullWidth;
			int num2 = (int)(*getTile.frameY) / tileObjectData.CoordinateFullHeight;
			int num3 = tileObjectData.StyleWrapLimit;
			if (num3 == 0)
			{
				num3 = 1;
			}
			int styleLineSkip = tileObjectData.StyleLineSkip;
			return ((!tileObjectData.StyleHorizontal) ? (num / styleLineSkip * num3 + num2) : (num2 / styleLineSkip * num3 + num)) / tileObjectData.StyleMultiplier;
		}

		/// <summary>
		/// Retrieves the tile <paramref name="style" /> and <paramref name="alternate" /> placement corresponding to the passed in Tile. Empty tiles and terrain tiles will return without setting the ref parameters. Any Tile of the multitile works.
		/// <para /> Other related methods include <see cref="M:Terraria.ObjectData.TileObjectData.GetTileData(Terraria.Tile)" />, <see cref="M:Terraria.ObjectData.TileObjectData.GetTileData(System.Int32,System.Int32,System.Int32)" />, and <see cref="M:Terraria.ObjectData.TileObjectData.GetTileStyle(Terraria.Tile)" />.
		/// </summary>
		// Token: 0x06001A07 RID: 6663 RVA: 0x004CA4E4 File Offset: 0x004C86E4
		public unsafe static void GetTileInfo(Tile getTile, ref int style, ref int alternate)
		{
			if (getTile == null || !getTile.active())
			{
				return;
			}
			int type = (int)(*getTile.type);
			if (type < 0 || type >= TileObjectData._data.Count)
			{
				throw new ArgumentOutOfRangeException("getTile", "Function called with a bad tile type");
			}
			TileObjectData tileObjectData = TileObjectData._data[type];
			if (tileObjectData == null)
			{
				return;
			}
			int num = (int)(*getTile.frameX) / tileObjectData.CoordinateFullWidth;
			int num2 = (int)(*getTile.frameY) / tileObjectData.CoordinateFullHeight;
			int num3 = tileObjectData.StyleWrapLimit;
			if (num3 == 0)
			{
				num3 = 1;
			}
			int styleLineSkip = tileObjectData.StyleLineSkip;
			int num6 = (!tileObjectData.StyleHorizontal) ? (num / styleLineSkip * num3 + num2) : (num2 / styleLineSkip * num3 + num);
			int num4 = num6 / tileObjectData.StyleMultiplier;
			int num5 = num6 % tileObjectData.StyleMultiplier;
			style = num4;
			alternate = num5;
		}

		/// <summary>
		/// Returns true if the <see cref="T:Terraria.Tile" /> at the location provided has a placed tile and is the top left tile of a multitile (supports alternate placements and states as well). Returns false otherwise.
		/// <para /> Can be used for logic that should only run once per multitile, such as custom tile rendering.
		/// </summary>
		// Token: 0x06001A08 RID: 6664 RVA: 0x004CA5AC File Offset: 0x004C87AC
		public static bool IsTopLeft(int i, int j)
		{
			return TileObjectData.IsTopLeft(Main.tile[i, j]);
		}

		/// <summary>
		/// Returns true if the <see cref="T:Terraria.Tile" /> provided has a placed tile and is the top left tile of a multitile (supports alternate placements and states as well). Returns false otherwise. 
		/// <para /> Can be used for logic that should only run once per multitile, such as custom tile rendering.
		/// </summary>
		// Token: 0x06001A09 RID: 6665 RVA: 0x004CA5C0 File Offset: 0x004C87C0
		public unsafe static bool IsTopLeft(Tile tile)
		{
			if (!tile.HasTile)
			{
				return false;
			}
			TileObjectData tileData = TileObjectData.GetTileData(tile);
			if (tileData == null)
			{
				return false;
			}
			bool flag = (int)(*tile.TileFrameX) % tileData.CoordinateFullWidth != 0;
			int partFrameY = (int)(*tile.TileFrameY) % tileData.CoordinateFullHeight;
			return !flag && partFrameY == 0;
		}

		/// <summary>
		/// Returns the coordinates of the top left tile of the multitile at the location provided. Returns <see cref="F:Terraria.DataStructures.Point16.NegativeOne" /> if no tile exists at the coordinates. If the tile does not have a TileObjectData, such as if it were a normal terrain tile, the provided coordinates will be returned.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <returns></returns>
		// Token: 0x06001A0A RID: 6666 RVA: 0x004CA60C File Offset: 0x004C880C
		public unsafe static Point16 TopLeft(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			if (!tile.HasTile)
			{
				return Point16.NegativeOne;
			}
			TileObjectData tileData = TileObjectData.GetTileData(tile);
			if (tileData == null)
			{
				return new Point16(i, j);
			}
			int num = (int)(*tile.TileFrameX) % tileData.CoordinateFullWidth;
			int partFrameY = (int)(*tile.TileFrameY) % tileData.CoordinateFullHeight;
			int partX = num / (tileData.CoordinateWidth + tileData.CoordinatePadding);
			int partY = 0;
			int remainingFrameY = partFrameY;
			while (partY + 1 < tileData.Height && remainingFrameY - tileData.CoordinateHeights[partY] - tileData.CoordinatePadding >= 0)
			{
				remainingFrameY -= tileData.CoordinateHeights[partY] + tileData.CoordinatePadding;
				partY++;
			}
			i -= partX;
			j -= partY;
			return new Point16(i, j);
		}

		/// <inheritdoc cref="M:Terraria.ObjectData.TileObjectData.TopLeft(System.Int32,System.Int32)" />
		// Token: 0x06001A0B RID: 6667 RVA: 0x004CA6CC File Offset: 0x004C88CC
		public static Point16 TopLeft(Point16 point)
		{
			return TileObjectData.TopLeft((int)point.X, (int)point.Y);
		}

		// Token: 0x040013D2 RID: 5074
		internal TileObjectData _parent;

		// Token: 0x040013D3 RID: 5075
		private bool _linkedAlternates;

		// Token: 0x040013D4 RID: 5076
		private bool _usesCustomCanPlace;

		// Token: 0x040013D5 RID: 5077
		private TileObjectAlternatesModule _alternates;

		// Token: 0x040013D6 RID: 5078
		private AnchorDataModule _anchor;

		// Token: 0x040013D7 RID: 5079
		private AnchorTypesModule _anchorTiles;

		// Token: 0x040013D8 RID: 5080
		private LiquidDeathModule _liquidDeath;

		// Token: 0x040013D9 RID: 5081
		private LiquidPlacementModule _liquidPlacement;

		// Token: 0x040013DA RID: 5082
		private TilePlacementHooksModule _placementHooks;

		// Token: 0x040013DB RID: 5083
		private TileObjectSubTilesModule _subTiles;

		// Token: 0x040013DC RID: 5084
		private TileObjectDrawModule _tileObjectDraw;

		// Token: 0x040013DD RID: 5085
		private TileObjectStyleModule _tileObjectStyle;

		// Token: 0x040013DE RID: 5086
		private TileObjectBaseModule _tileObjectBase;

		// Token: 0x040013DF RID: 5087
		private TileObjectCoordinatesModule _tileObjectCoords;

		// Token: 0x040013E0 RID: 5088
		private bool _hasOwnAlternates;

		// Token: 0x040013E1 RID: 5089
		private bool _hasOwnAnchor;

		// Token: 0x040013E2 RID: 5090
		private bool _hasOwnAnchorTiles;

		// Token: 0x040013E3 RID: 5091
		private bool _hasOwnLiquidDeath;

		// Token: 0x040013E4 RID: 5092
		private bool _hasOwnLiquidPlacement;

		// Token: 0x040013E5 RID: 5093
		private bool _hasOwnPlacementHooks;

		// Token: 0x040013E6 RID: 5094
		private bool _hasOwnSubTiles;

		// Token: 0x040013E7 RID: 5095
		private bool _hasOwnTileObjectBase;

		// Token: 0x040013E8 RID: 5096
		private bool _hasOwnTileObjectDraw;

		// Token: 0x040013E9 RID: 5097
		private bool _hasOwnTileObjectStyle;

		// Token: 0x040013EA RID: 5098
		private bool _hasOwnTileObjectCoords;

		// Token: 0x040013EB RID: 5099
		internal static List<TileObjectData> _data;

		// Token: 0x040013EC RID: 5100
		private static TileObjectData _baseObject;

		// Token: 0x040013ED RID: 5101
		private static bool readOnlyData;

		/// <summary>
		/// Contains the tile properties for the current tile. This is used to control the behavior, size, and other properties of a non-terrain tiles.
		/// <para /> Tiles using a TileObjectData are referred to as multitiles.
		/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#tileobjectdata-or-frameimportantmultitiles">Basic Tile Guide on the wiki</see> has more information about multitiles.
		/// <para /> After all properties are set, call <see cref="M:Terraria.ObjectData.TileObjectData.addTile(System.Int32)" /> to register the data to the tile type.
		/// <para /> <see cref="F:Terraria.ObjectData.TileObjectData.newSubTile" /> and <see cref="F:Terraria.ObjectData.TileObjectData.newAlternate" /> are used to provide subtile and alternate-specific properties. Alternates should be setup before subtiles.
		/// </summary>
		// Token: 0x040013EE RID: 5102
		public static TileObjectData newTile;

		/// <summary>
		/// Contains the tile properties specific to a subtile, also known as a tile style, of the current <see cref="F:Terraria.ObjectData.TileObjectData.newTile" />.
		/// <para /> Subtiles allow placed tile styles to have specific behaviors. This is most commonly used to declare certain tile styles as immune to lava or water. Another common usage is to declare specific tiles a tile style can anchor to.
		/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#newsubtile---tile-styles">newSubTile section of the Tile wiki page</see> has more information about subtiles.
		/// <para /> Calling <see cref="M:Terraria.ObjectData.TileObjectData.addSubTile(System.Int32)" /> registers the alternate placement.
		/// <para /> If there are alternate placements (<see cref="M:Terraria.ObjectData.TileObjectData.addAlternate(System.Int32)" />) for this tile, make sure to register those first and to set <see cref="P:Terraria.ObjectData.TileObjectData.LinkedAlternates" /> to true after copying the base tile.
		/// </summary>
		// Token: 0x040013EF RID: 5103
		public static TileObjectData newSubTile;

		/// <summary>
		/// Contains the tile properties specific to an alternate placement of the current <see cref="F:Terraria.ObjectData.TileObjectData.newTile" />.
		/// <para /> Alternate placements allow each style to be placed in multiple ways, using alternate sprites or alternate anchors.  Mostly useful for tiles that can anchor in multiple ways, such as how torches can anchor to tiles to the left, right, or bottom as well as to walls.
		/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Tile#newalternate---alternate-placement-styles">newAlternate section of the Tile wiki page</see> has more information about alternate placements.
		/// <para /> Calling <see cref="M:Terraria.ObjectData.TileObjectData.addAlternate(System.Int32)" /> registers the alternate placement.
		/// <para /> If there are subtiles (<see cref="M:Terraria.ObjectData.TileObjectData.addSubTile(System.Int32)" />) for this tile, make sure to do those after alternates are registered and to set <see cref="P:Terraria.ObjectData.TileObjectData.LinkedAlternates" /> to true on newSubTile after copying the base tile.
		/// </summary>
		// Token: 0x040013F0 RID: 5104
		public static TileObjectData newAlternate;

		// Token: 0x040013F1 RID: 5105
		public static TileObjectData StyleSwitch;

		// Token: 0x040013F2 RID: 5106
		public static TileObjectData StyleTorch;

		/// <summary> Has left and right alternate placements. </summary>
		// Token: 0x040013F3 RID: 5107
		public static TileObjectData Style4x2;

		// Token: 0x040013F4 RID: 5108
		public static TileObjectData Style2x2;

		// Token: 0x040013F5 RID: 5109
		public static TileObjectData Style1x2;

		/// <summary> No anchors. </summary>
		// Token: 0x040013F6 RID: 5110
		public static TileObjectData Style1x1;

		// Token: 0x040013F7 RID: 5111
		public static TileObjectData StyleAlch;

		// Token: 0x040013F8 RID: 5112
		public static TileObjectData StyleDye;

		// Token: 0x040013F9 RID: 5113
		public static TileObjectData Style2x1;

		// Token: 0x040013FA RID: 5114
		public static TileObjectData Style6x3;

		// Token: 0x040013FB RID: 5115
		public static TileObjectData StyleSmallCage;

		// Token: 0x040013FC RID: 5116
		public static TileObjectData StyleOnTable1x1;

		/// <summary> Anchors to tiles above. </summary>
		// Token: 0x040013FD RID: 5117
		public static TileObjectData Style1x2Top;

		/// <summary> Is 1x3. </summary>
		// Token: 0x040013FE RID: 5118
		public static TileObjectData Style1xX;

		/// <summary> Is 2x3. </summary>
		// Token: 0x040013FF RID: 5119
		public static TileObjectData Style2xX;

		// Token: 0x04001400 RID: 5120
		public static TileObjectData Style3x2;

		// Token: 0x04001401 RID: 5121
		public static TileObjectData Style3x3;

		// Token: 0x04001402 RID: 5122
		public static TileObjectData Style3x4;

		// Token: 0x04001403 RID: 5123
		public static TileObjectData Style5x4;

		/// <summary> Anchors to walls. </summary>
		// Token: 0x04001404 RID: 5124
		public static TileObjectData Style3x3Wall;

		// Token: 0x0200089D RID: 2205
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006A2A RID: 27178
			public static Func<int, int, int, int, int, int, int> <0>__PlaceXmasTree_Direct;

			// Token: 0x04006A2B RID: 27179
			public static Func<int, int, int, int, int, int, int> <1>__Hook_AfterPlacement;

			// Token: 0x04006A2C RID: 27180
			public static Func<int, int, int, int, int, int, int> <2>__Hook_AfterPlacement;

			// Token: 0x04006A2D RID: 27181
			public static Func<int, int, int, int, int, int, int> <3>__Hook_AfterPlacement;

			// Token: 0x04006A2E RID: 27182
			public static Func<int, int, int, int, int, int, int> <4>__FindEmptyChest;

			// Token: 0x04006A2F RID: 27183
			public static Func<int, int, int, int, int, int, int> <5>__AfterPlacement_Hook;

			// Token: 0x04006A30 RID: 27184
			public static Func<int, int, int, int, int, int, int> <6>__Hook_AfterPlacement;

			// Token: 0x04006A31 RID: 27185
			public static Func<int, int, int, int, int, int, int> <7>__PlacementPreviewHook_CheckIfCanPlace;

			// Token: 0x04006A32 RID: 27186
			public static Func<int, int, int, int, int, int, int> <8>__PlacementPreviewHook_AfterPlacement;

			// Token: 0x04006A33 RID: 27187
			public static Func<int, int, int, int, int, int, int> <9>__Hook_AfterPlacement;

			// Token: 0x04006A34 RID: 27188
			public static Func<int, int, int, int, int, int, int> <10>__Hook_AfterPlacement;

			// Token: 0x04006A35 RID: 27189
			public static Func<int, int, int, int, int, int, int> <11>__CanPlaceProjectilePressurePad;

			// Token: 0x04006A36 RID: 27190
			public static Func<int, int, int, int, int, int, int> <12>__Hook_AfterPlacement;
		}
	}
}
