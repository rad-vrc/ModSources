using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.Modules;

namespace Terraria.ObjectData
{
	// Token: 0x02000053 RID: 83
	public class TileObjectData
	{
		// Token: 0x0600109D RID: 4253 RVA: 0x00481BC0 File Offset: 0x0047FDC0
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

		// Token: 0x0600109E RID: 4254 RVA: 0x00481C3C File Offset: 0x0047FE3C
		public void CopyFrom(TileObjectData copy)
		{
			if (copy == null)
			{
				return;
			}
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
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00481CD1 File Offset: 0x0047FED1
		public void FullCopyFrom(ushort tileType)
		{
			this.FullCopyFrom(TileObjectData.GetTileData((int)tileType, 0, 0));
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00481CE4 File Offset: 0x0047FEE4
		public void FullCopyFrom(TileObjectData copy)
		{
			if (copy == null)
			{
				return;
			}
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

		// Token: 0x060010A1 RID: 4257 RVA: 0x00481D94 File Offset: 0x0047FF94
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

		// Token: 0x060010A2 RID: 4258 RVA: 0x00481FC4 File Offset: 0x004801C4
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
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x004820B6 File Offset: 0x004802B6
		private void WriteCheck()
		{
			if (TileObjectData.readOnlyData)
			{
				throw new FieldAccessException("Tile data is locked and only accessible during startup.");
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x004820CA File Offset: 0x004802CA
		private void LockWrites()
		{
			TileObjectData.readOnlyData = true;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x004820D2 File Offset: 0x004802D2
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x004820DA File Offset: 0x004802DA
		private bool LinkedAlternates
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

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0048210C File Offset: 0x0048030C
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00482114 File Offset: 0x00480314
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

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00482123 File Offset: 0x00480323
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x00482143 File Offset: 0x00480343
		private List<TileObjectData> Alternates
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

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00482171 File Offset: 0x00480371
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x00482194 File Offset: 0x00480394
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00482221 File Offset: 0x00480421
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x00482244 File Offset: 0x00480444
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

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x004822D1 File Offset: 0x004804D1
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x004822F4 File Offset: 0x004804F4
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

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00482381 File Offset: 0x00480581
		// (set) Token: 0x060010B2 RID: 4274 RVA: 0x004823A4 File Offset: 0x004805A4
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

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00482431 File Offset: 0x00480631
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00482454 File Offset: 0x00480654
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x004824DC File Offset: 0x004806DC
		// (set) Token: 0x060010B6 RID: 4278 RVA: 0x004824FC File Offset: 0x004806FC
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
				if (this._linkedAlternates)
				{
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
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0048259A File Offset: 0x0048079A
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x004825BC File Offset: 0x004807BC
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
				if (this._linkedAlternates)
				{
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
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0048265A File Offset: 0x0048085A
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x0048267C File Offset: 0x0048087C
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
				if (this._linkedAlternates)
				{
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
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0048271A File Offset: 0x0048091A
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x0048273C File Offset: 0x0048093C
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
				if (this._linkedAlternates)
				{
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
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x004827C6 File Offset: 0x004809C6
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x004827E8 File Offset: 0x004809E8
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

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00482870 File Offset: 0x00480A70
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00482890 File Offset: 0x00480A90
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

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00482918 File Offset: 0x00480B18
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x00482938 File Offset: 0x00480B38
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x004829C0 File Offset: 0x00480BC0
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x004829E0 File Offset: 0x00480BE0
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

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00482A68 File Offset: 0x00480C68
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x00482A88 File Offset: 0x00480C88
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

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00482ABC File Offset: 0x00480CBC
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x00482ADC File Offset: 0x00480CDC
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

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00482B10 File Offset: 0x00480D10
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x00482B30 File Offset: 0x00480D30
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00482B64 File Offset: 0x00480D64
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x00482B84 File Offset: 0x00480D84
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

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00482BB8 File Offset: 0x00480DB8
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x00482BD8 File Offset: 0x00480DD8
		private List<TileObjectData> SubTiles
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

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00482C12 File Offset: 0x00480E12
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x00482C30 File Offset: 0x00480E30
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

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00482CB8 File Offset: 0x00480EB8
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x00482CD4 File Offset: 0x00480ED4
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

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00482D5C File Offset: 0x00480F5C
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x00482D78 File Offset: 0x00480F78
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00482E00 File Offset: 0x00481000
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x00482E1C File Offset: 0x0048101C
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00482EA4 File Offset: 0x004810A4
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x00482EC0 File Offset: 0x004810C0
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

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00482F48 File Offset: 0x00481148
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x00482F64 File Offset: 0x00481164
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

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00482FEC File Offset: 0x004811EC
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x0048300C File Offset: 0x0048120C
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

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00483094 File Offset: 0x00481294
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x004830B4 File Offset: 0x004812B4
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

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0048313C File Offset: 0x0048133C
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x0048315C File Offset: 0x0048135C
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

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00483207 File Offset: 0x00481407
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x00483228 File Offset: 0x00481428
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

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x004832D3 File Offset: 0x004814D3
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x004832F4 File Offset: 0x004814F4
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

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0048337C File Offset: 0x0048157C
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x0048339C File Offset: 0x0048159C
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

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00483424 File Offset: 0x00481624
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00483444 File Offset: 0x00481644
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

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x004834F9 File Offset: 0x004816F9
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x0048351C File Offset: 0x0048171C
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

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x004835D1 File Offset: 0x004817D1
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x004835F4 File Offset: 0x004817F4
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

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00483681 File Offset: 0x00481881
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x004836A4 File Offset: 0x004818A4
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0048372C File Offset: 0x0048192C
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x0048374C File Offset: 0x0048194C
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

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x004837D4 File Offset: 0x004819D4
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x004837F4 File Offset: 0x004819F4
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

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0048387C File Offset: 0x00481A7C
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x0048389C File Offset: 0x00481A9C
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

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00483924 File Offset: 0x00481B24
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x00483944 File Offset: 0x00481B44
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
				if (this._linkedAlternates)
				{
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
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x004839F1 File Offset: 0x00481BF1
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x00483A14 File Offset: 0x00481C14
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

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00483AA9 File Offset: 0x00481CA9
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x00483ACC File Offset: 0x00481CCC
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

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00483B61 File Offset: 0x00481D61
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x00483B84 File Offset: 0x00481D84
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00483C1E File Offset: 0x00481E1E
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

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00483C51 File Offset: 0x00481E51
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

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00483C84 File Offset: 0x00481E84
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x00483CA4 File Offset: 0x00481EA4
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

		// Token: 0x06001101 RID: 4353 RVA: 0x00483D3C File Offset: 0x00481F3C
		public bool LiquidPlace(Tile checkTile)
		{
			if (checkTile == null)
			{
				return false;
			}
			if (checkTile.liquid > 0)
			{
				switch (checkTile.liquidType())
				{
				case 0:
				case 2:
				case 3:
					if (this.WaterPlacement == LiquidPlacement.NotAllowed)
					{
						return false;
					}
					if (this.WaterPlacement == LiquidPlacement.OnlyInFullLiquid && checkTile.liquid != 255)
					{
						return false;
					}
					break;
				case 1:
					if (this.LavaPlacement == LiquidPlacement.NotAllowed)
					{
						return false;
					}
					if (this.LavaPlacement == LiquidPlacement.OnlyInFullLiquid && checkTile.liquid != 255)
					{
						return false;
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

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x00483E07 File Offset: 0x00482007
		public int AlternatesCount
		{
			get
			{
				return this.Alternates.Count;
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00483E14 File Offset: 0x00482014
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

		// Token: 0x06001104 RID: 4356 RVA: 0x00483E7C File Offset: 0x0048207C
		public bool isValidWallAnchor(int type)
		{
			int[] array;
			if (this._anchorTiles == null)
			{
				array = null;
			}
			else
			{
				array = this._anchorTiles.wallValid;
			}
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

		// Token: 0x06001105 RID: 4357 RVA: 0x00483EC4 File Offset: 0x004820C4
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

		// Token: 0x06001106 RID: 4358 RVA: 0x00483F04 File Offset: 0x00482104
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

		// Token: 0x06001107 RID: 4359 RVA: 0x00483F2C File Offset: 0x0048212C
		private static void addBaseTile(out TileObjectData baseTile)
		{
			TileObjectData.newTile.Calculate();
			baseTile = TileObjectData.newTile;
			baseTile._parent = TileObjectData._baseObject;
			TileObjectData.newTile = new TileObjectData(TileObjectData._baseObject);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00483F5A File Offset: 0x0048215A
		private static void addTile(int tileType)
		{
			TileObjectData.newTile.Calculate();
			TileObjectData._data[tileType] = TileObjectData.newTile;
			TileObjectData.newTile = new TileObjectData(TileObjectData._baseObject);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00483F88 File Offset: 0x00482188
		private static void addSubTile(params int[] styles)
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
					for (int j = list.Count; j <= num; j++)
					{
						list.Add(null);
					}
				}
				TileObjectData.newSubTile._parent = TileObjectData.newTile;
				list[num] = TileObjectData.newSubTile;
			}
			TileObjectData.newSubTile = new TileObjectData(TileObjectData._baseObject);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0048402C File Offset: 0x0048222C
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

		// Token: 0x0600110B RID: 4363 RVA: 0x004840BC File Offset: 0x004822BC
		private static void addAlternate(int baseStyle)
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

		// Token: 0x0600110C RID: 4364 RVA: 0x0048412C File Offset: 0x0048232C
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
			TileObjectData.newTile.HookPlaceOverride = new PlacementHook(new Func<int, int, int, int, int, int, int>(WorldGen.PlaceXmasTree_Direct), -1, 0, true);
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
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(TEFoodPlatter.Hook_AfterPlacement), -1, 0, true);
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
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(TETrainingDummy.Hook_AfterPlacement), -1, 0, false);
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
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(TEDisplayDoll.Hook_AfterPlacement), -1, 0, false);
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
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);
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
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(TEHatRack.Hook_AfterPlacement), -1, 0, false);
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
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(TETeleportationPylon.PlacementPreviewHook_CheckIfCanPlace), 1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(TETeleportationPylon.PlacementPreviewHook_AfterPlacement), -1, 0, false);
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
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);
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
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);
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
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(TEItemFrame.Hook_AfterPlacement), -1, 0, true);
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
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 1);
			TileObjectData.newAlternate.AnchorAlternateTiles = new int[]
			{
				419
			};
			TileObjectData.addTile(419);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(TELogicSensor.Hook_AfterPlacement), -1, 0, true);
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
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(WorldGen.CanPlaceProjectilePressurePad), -1, 0, true);
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
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(TEWeaponsRack.Hook_AfterPlacement), -1, 0, true);
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
			TileObjectData.readOnlyData = true;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0048BC30 File Offset: 0x00489E30
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

		// Token: 0x0600110E RID: 4366 RVA: 0x0048BC90 File Offset: 0x00489E90
		public static bool CheckLiquidPlacement(int type, int style, Tile checkTile)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, 0);
			if (tileData != null)
			{
				return tileData.LiquidPlace(checkTile);
			}
			return TileObjectData.LiquidPlace(type, checkTile);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0048BCB8 File Offset: 0x00489EB8
		public static bool LiquidPlace(int type, Tile checkTile)
		{
			if (checkTile == null)
			{
				return false;
			}
			if (checkTile.liquid > 0)
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

		// Token: 0x06001110 RID: 4368 RVA: 0x0048BD0C File Offset: 0x00489F0C
		public static bool CheckWaterDeath(int type, int style)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, 0);
			if (tileData == null)
			{
				return Main.tileWaterDeath[type];
			}
			return tileData.WaterDeath;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0048BD34 File Offset: 0x00489F34
		public static bool CheckWaterDeath(Tile checkTile)
		{
			if (!checkTile.active())
			{
				return false;
			}
			TileObjectData tileData = TileObjectData.GetTileData(checkTile);
			if (tileData == null)
			{
				return Main.tileWaterDeath[(int)checkTile.type];
			}
			return tileData.WaterDeath;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0048BD68 File Offset: 0x00489F68
		public static bool CheckLavaDeath(int type, int style)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, 0);
			if (tileData == null)
			{
				return Main.tileLavaDeath[type];
			}
			return tileData.LavaDeath;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0048BD90 File Offset: 0x00489F90
		public static bool CheckLavaDeath(Tile checkTile)
		{
			if (!checkTile.active())
			{
				return false;
			}
			TileObjectData tileData = TileObjectData.GetTileData(checkTile);
			if (tileData == null)
			{
				return Main.tileLavaDeath[(int)checkTile.type];
			}
			return tileData.LavaDeath;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0048BDC4 File Offset: 0x00489FC4
		public static int PlatformFrameWidth()
		{
			return TileObjectData._data[19].CoordinateFullWidth;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0048BDD8 File Offset: 0x00489FD8
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

		// Token: 0x06001116 RID: 4374 RVA: 0x0048BE70 File Offset: 0x0048A070
		public static TileObjectData GetTileData(Tile getTile)
		{
			if (getTile == null || !getTile.active())
			{
				return null;
			}
			int type = (int)getTile.type;
			if (type < 0 || type >= TileObjectData._data.Count)
			{
				throw new ArgumentOutOfRangeException("Function called with a bad tile type");
			}
			TileObjectData tileObjectData = TileObjectData._data[type];
			if (tileObjectData == null)
			{
				return null;
			}
			int num = (int)getTile.frameX / tileObjectData.CoordinateFullWidth;
			int num2 = (int)getTile.frameY / tileObjectData.CoordinateFullHeight;
			int num3 = tileObjectData.StyleWrapLimit;
			if (num3 == 0)
			{
				num3 = 1;
			}
			int num4;
			if (tileObjectData.StyleHorizontal)
			{
				num4 = num2 * num3 + num;
			}
			else
			{
				num4 = num * num3 + num2;
			}
			int num5 = num4 / tileObjectData.StyleMultiplier;
			int num6 = num4 % tileObjectData.StyleMultiplier;
			int styleLineSkip = tileObjectData.StyleLineSkip;
			if (styleLineSkip > 1)
			{
				if (tileObjectData.StyleHorizontal)
				{
					num5 = num2 / styleLineSkip * num3 + num;
					num6 = num2 % styleLineSkip;
				}
				else
				{
					num5 = num / styleLineSkip * num3 + num2;
					num6 = num % styleLineSkip;
				}
			}
			if (tileObjectData.SubTiles != null && num5 >= 0 && num5 < tileObjectData.SubTiles.Count)
			{
				TileObjectData tileObjectData2 = tileObjectData.SubTiles[num5];
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
					if (tileObjectData3 != null && num6 >= tileObjectData3.Style && num6 <= tileObjectData3.Style + tileObjectData3.RandomStyleRange)
					{
						return tileObjectData3;
					}
				}
			}
			return tileObjectData;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0048BFE0 File Offset: 0x0048A1E0
		public static void SyncObjectPlacement(int tileX, int tileY, int type, int style, int dir)
		{
			NetMessage.SendData(17, -1, -1, null, 1, (float)tileX, (float)tileY, (float)type, style, 0, 0);
			TileObjectData.GetTileData(type, style, 0);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0048C00C File Offset: 0x0048A20C
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

		// Token: 0x06001119 RID: 4377 RVA: 0x0048C09C File Offset: 0x0048A29C
		public static void OriginToTopLeft(int type, int style, ref Point16 baseCoords)
		{
			TileObjectData tileData = TileObjectData.GetTileData(type, style, 0);
			if (tileData == null)
			{
				return;
			}
			baseCoords = new Point16((int)(baseCoords.X - tileData.Origin.X), (int)(baseCoords.Y - tileData.Origin.Y));
		}

		// Token: 0x04000EA1 RID: 3745
		private TileObjectData _parent;

		// Token: 0x04000EA2 RID: 3746
		private bool _linkedAlternates;

		// Token: 0x04000EA3 RID: 3747
		private bool _usesCustomCanPlace;

		// Token: 0x04000EA4 RID: 3748
		private TileObjectAlternatesModule _alternates;

		// Token: 0x04000EA5 RID: 3749
		private AnchorDataModule _anchor;

		// Token: 0x04000EA6 RID: 3750
		private AnchorTypesModule _anchorTiles;

		// Token: 0x04000EA7 RID: 3751
		private LiquidDeathModule _liquidDeath;

		// Token: 0x04000EA8 RID: 3752
		private LiquidPlacementModule _liquidPlacement;

		// Token: 0x04000EA9 RID: 3753
		private TilePlacementHooksModule _placementHooks;

		// Token: 0x04000EAA RID: 3754
		private TileObjectSubTilesModule _subTiles;

		// Token: 0x04000EAB RID: 3755
		private TileObjectDrawModule _tileObjectDraw;

		// Token: 0x04000EAC RID: 3756
		private TileObjectStyleModule _tileObjectStyle;

		// Token: 0x04000EAD RID: 3757
		private TileObjectBaseModule _tileObjectBase;

		// Token: 0x04000EAE RID: 3758
		private TileObjectCoordinatesModule _tileObjectCoords;

		// Token: 0x04000EAF RID: 3759
		private bool _hasOwnAlternates;

		// Token: 0x04000EB0 RID: 3760
		private bool _hasOwnAnchor;

		// Token: 0x04000EB1 RID: 3761
		private bool _hasOwnAnchorTiles;

		// Token: 0x04000EB2 RID: 3762
		private bool _hasOwnLiquidDeath;

		// Token: 0x04000EB3 RID: 3763
		private bool _hasOwnLiquidPlacement;

		// Token: 0x04000EB4 RID: 3764
		private bool _hasOwnPlacementHooks;

		// Token: 0x04000EB5 RID: 3765
		private bool _hasOwnSubTiles;

		// Token: 0x04000EB6 RID: 3766
		private bool _hasOwnTileObjectBase;

		// Token: 0x04000EB7 RID: 3767
		private bool _hasOwnTileObjectDraw;

		// Token: 0x04000EB8 RID: 3768
		private bool _hasOwnTileObjectStyle;

		// Token: 0x04000EB9 RID: 3769
		private bool _hasOwnTileObjectCoords;

		// Token: 0x04000EBA RID: 3770
		private static List<TileObjectData> _data;

		// Token: 0x04000EBB RID: 3771
		private static TileObjectData _baseObject;

		// Token: 0x04000EBC RID: 3772
		private static bool readOnlyData;

		// Token: 0x04000EBD RID: 3773
		private static TileObjectData newTile;

		// Token: 0x04000EBE RID: 3774
		private static TileObjectData newSubTile;

		// Token: 0x04000EBF RID: 3775
		private static TileObjectData newAlternate;

		// Token: 0x04000EC0 RID: 3776
		private static TileObjectData StyleSwitch;

		// Token: 0x04000EC1 RID: 3777
		private static TileObjectData StyleTorch;

		// Token: 0x04000EC2 RID: 3778
		private static TileObjectData Style4x2;

		// Token: 0x04000EC3 RID: 3779
		private static TileObjectData Style2x2;

		// Token: 0x04000EC4 RID: 3780
		private static TileObjectData Style1x2;

		// Token: 0x04000EC5 RID: 3781
		private static TileObjectData Style1x1;

		// Token: 0x04000EC6 RID: 3782
		private static TileObjectData StyleAlch;

		// Token: 0x04000EC7 RID: 3783
		private static TileObjectData StyleDye;

		// Token: 0x04000EC8 RID: 3784
		private static TileObjectData Style2x1;

		// Token: 0x04000EC9 RID: 3785
		private static TileObjectData Style6x3;

		// Token: 0x04000ECA RID: 3786
		private static TileObjectData StyleSmallCage;

		// Token: 0x04000ECB RID: 3787
		private static TileObjectData StyleOnTable1x1;

		// Token: 0x04000ECC RID: 3788
		private static TileObjectData Style1x2Top;

		// Token: 0x04000ECD RID: 3789
		private static TileObjectData Style1xX;

		// Token: 0x04000ECE RID: 3790
		private static TileObjectData Style2xX;

		// Token: 0x04000ECF RID: 3791
		private static TileObjectData Style3x2;

		// Token: 0x04000ED0 RID: 3792
		private static TileObjectData Style3x3;

		// Token: 0x04000ED1 RID: 3793
		private static TileObjectData Style3x4;

		// Token: 0x04000ED2 RID: 3794
		private static TileObjectData Style5x4;

		// Token: 0x04000ED3 RID: 3795
		private static TileObjectData Style3x3Wall;
	}
}
