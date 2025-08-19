using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004BD RID: 1213
	public class TilePaintSystemV2
	{
		// Token: 0x06003A25 RID: 14885 RVA: 0x005A5D98 File Offset: 0x005A3F98
		public void Reset()
		{
			foreach (TilePaintSystemV2.TileRenderTargetHolder tileRenderTargetHolder in this._tilesRenders.Values)
			{
				tileRenderTargetHolder.Clear();
			}
			this._tilesRenders.Clear();
			foreach (TilePaintSystemV2.WallRenderTargetHolder wallRenderTargetHolder in this._wallsRenders.Values)
			{
				wallRenderTargetHolder.Clear();
			}
			this._wallsRenders.Clear();
			foreach (TilePaintSystemV2.TreeTopRenderTargetHolder treeTopRenderTargetHolder in this._treeTopRenders.Values)
			{
				treeTopRenderTargetHolder.Clear();
			}
			this._treeTopRenders.Clear();
			foreach (TilePaintSystemV2.TreeBranchTargetHolder treeBranchTargetHolder in this._treeBranchRenders.Values)
			{
				treeBranchTargetHolder.Clear();
			}
			this._treeBranchRenders.Clear();
			foreach (TilePaintSystemV2.ARenderTargetHolder arenderTargetHolder in this._requests)
			{
				arenderTargetHolder.Clear();
			}
			this._requests.Clear();
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x005A5F30 File Offset: 0x005A4130
		public void RequestTile(ref TilePaintSystemV2.TileVariationkey lookupKey)
		{
			TilePaintSystemV2.TileRenderTargetHolder value;
			if (!this._tilesRenders.TryGetValue(lookupKey, out value))
			{
				value = new TilePaintSystemV2.TileRenderTargetHolder
				{
					Key = lookupKey
				};
				this._tilesRenders.Add(lookupKey, value);
			}
			if (!value.IsReady)
			{
				this._requests.Add(value);
			}
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x005A5F8C File Offset: 0x005A418C
		private void RequestTile_CheckForRelatedTileRequests(ref TilePaintSystemV2.TileVariationkey lookupKey)
		{
			if (lookupKey.TileType == 83)
			{
				TilePaintSystemV2.TileVariationkey lookupKey2 = new TilePaintSystemV2.TileVariationkey
				{
					TileType = 84,
					TileStyle = lookupKey.TileStyle,
					PaintColor = lookupKey.PaintColor
				};
				this.RequestTile(ref lookupKey2);
			}
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x005A5FD8 File Offset: 0x005A41D8
		public void RequestWall(ref TilePaintSystemV2.WallVariationKey lookupKey)
		{
			TilePaintSystemV2.WallRenderTargetHolder value;
			if (!this._wallsRenders.TryGetValue(lookupKey, out value))
			{
				value = new TilePaintSystemV2.WallRenderTargetHolder
				{
					Key = lookupKey
				};
				this._wallsRenders.Add(lookupKey, value);
			}
			if (!value.IsReady)
			{
				this._requests.Add(value);
			}
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x005A6034 File Offset: 0x005A4234
		public void RequestTreeTop(ref TilePaintSystemV2.TreeFoliageVariantKey lookupKey)
		{
			TilePaintSystemV2.TreeTopRenderTargetHolder value;
			if (!this._treeTopRenders.TryGetValue(lookupKey, out value))
			{
				value = new TilePaintSystemV2.TreeTopRenderTargetHolder
				{
					Key = lookupKey
				};
				this._treeTopRenders.Add(lookupKey, value);
			}
			if (!value.IsReady)
			{
				this._requests.Add(value);
			}
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x005A6090 File Offset: 0x005A4290
		public void RequestTreeBranch(ref TilePaintSystemV2.TreeFoliageVariantKey lookupKey)
		{
			TilePaintSystemV2.TreeBranchTargetHolder value;
			if (!this._treeBranchRenders.TryGetValue(lookupKey, out value))
			{
				value = new TilePaintSystemV2.TreeBranchTargetHolder
				{
					Key = lookupKey
				};
				this._treeBranchRenders.Add(lookupKey, value);
			}
			if (!value.IsReady)
			{
				this._requests.Add(value);
			}
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x005A60EC File Offset: 0x005A42EC
		public Texture2D TryGetTileAndRequestIfNotReady(int tileType, int tileStyle, int paintColor)
		{
			TilePaintSystemV2.TileVariationkey lookupKey = new TilePaintSystemV2.TileVariationkey
			{
				TileType = tileType,
				TileStyle = tileStyle,
				PaintColor = paintColor
			};
			TilePaintSystemV2.TileRenderTargetHolder value;
			if (this._tilesRenders.TryGetValue(lookupKey, out value) && value.IsReady)
			{
				return value.Target;
			}
			this.RequestTile(ref lookupKey);
			return null;
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x005A6144 File Offset: 0x005A4344
		public Texture2D TryGetWallAndRequestIfNotReady(int wallType, int paintColor)
		{
			TilePaintSystemV2.WallVariationKey lookupKey = new TilePaintSystemV2.WallVariationKey
			{
				WallType = wallType,
				PaintColor = paintColor
			};
			TilePaintSystemV2.WallRenderTargetHolder value;
			if (this._wallsRenders.TryGetValue(lookupKey, out value) && value.IsReady)
			{
				return value.Target;
			}
			this.RequestWall(ref lookupKey);
			return null;
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x005A6194 File Offset: 0x005A4394
		public Texture2D TryGetTreeTopAndRequestIfNotReady(int treeTopIndex, int treeTopStyle, int paintColor)
		{
			TilePaintSystemV2.TreeFoliageVariantKey lookupKey = new TilePaintSystemV2.TreeFoliageVariantKey
			{
				TextureIndex = treeTopIndex,
				TextureStyle = treeTopStyle,
				PaintColor = paintColor
			};
			TilePaintSystemV2.TreeTopRenderTargetHolder value;
			if (this._treeTopRenders.TryGetValue(lookupKey, out value) && value.IsReady)
			{
				return value.Target;
			}
			this.RequestTreeTop(ref lookupKey);
			return null;
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x005A61EC File Offset: 0x005A43EC
		public Texture2D TryGetTreeBranchAndRequestIfNotReady(int treeTopIndex, int treeTopStyle, int paintColor)
		{
			TilePaintSystemV2.TreeFoliageVariantKey lookupKey = new TilePaintSystemV2.TreeFoliageVariantKey
			{
				TextureIndex = treeTopIndex,
				TextureStyle = treeTopStyle,
				PaintColor = paintColor
			};
			TilePaintSystemV2.TreeBranchTargetHolder value;
			if (this._treeBranchRenders.TryGetValue(lookupKey, out value) && value.IsReady)
			{
				return value.Target;
			}
			this.RequestTreeBranch(ref lookupKey);
			return null;
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x005A6244 File Offset: 0x005A4444
		public void PrepareAllRequests()
		{
			if (this._requests.Count != 0)
			{
				for (int i = 0; i < this._requests.Count; i++)
				{
					this._requests[i].Prepare();
				}
				this._requests.Clear();
			}
		}

		// Token: 0x040053C7 RID: 21447
		private Dictionary<TilePaintSystemV2.TileVariationkey, TilePaintSystemV2.TileRenderTargetHolder> _tilesRenders = new Dictionary<TilePaintSystemV2.TileVariationkey, TilePaintSystemV2.TileRenderTargetHolder>();

		// Token: 0x040053C8 RID: 21448
		private Dictionary<TilePaintSystemV2.WallVariationKey, TilePaintSystemV2.WallRenderTargetHolder> _wallsRenders = new Dictionary<TilePaintSystemV2.WallVariationKey, TilePaintSystemV2.WallRenderTargetHolder>();

		// Token: 0x040053C9 RID: 21449
		private Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeTopRenderTargetHolder> _treeTopRenders = new Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeTopRenderTargetHolder>();

		// Token: 0x040053CA RID: 21450
		private Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeBranchTargetHolder> _treeBranchRenders = new Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeBranchTargetHolder>();

		// Token: 0x040053CB RID: 21451
		private List<TilePaintSystemV2.ARenderTargetHolder> _requests = new List<TilePaintSystemV2.ARenderTargetHolder>();

		// Token: 0x02000BC1 RID: 3009
		public abstract class ARenderTargetHolder
		{
			// Token: 0x17000952 RID: 2386
			// (get) Token: 0x06005DB0 RID: 23984 RVA: 0x006C921A File Offset: 0x006C741A
			public bool IsReady
			{
				get
				{
					return this._wasPrepared;
				}
			}

			// Token: 0x06005DB1 RID: 23985
			public abstract void Prepare();

			// Token: 0x06005DB2 RID: 23986
			public abstract void PrepareShader();

			// Token: 0x06005DB3 RID: 23987 RVA: 0x006C9222 File Offset: 0x006C7422
			public void Clear()
			{
				if (this.Target != null && !this.Target.IsDisposed)
				{
					this.Target.Dispose();
				}
			}

			// Token: 0x06005DB4 RID: 23988 RVA: 0x006C9244 File Offset: 0x006C7444
			protected void PrepareTextureIfNecessary(Texture2D originalTexture, Rectangle? sourceRect = null)
			{
				if (this.Target == null || this.Target.IsContentLost)
				{
					Main instance = Main.instance;
					if (sourceRect == null)
					{
						sourceRect = new Rectangle?(originalTexture.Frame(1, 1, 0, 0, 0, 0));
					}
					this.Target = new RenderTarget2D(instance.GraphicsDevice, sourceRect.Value.Width, sourceRect.Value.Height, false, instance.GraphicsDevice.PresentationParameters.BackBufferFormat, 0, 0, 1);
					this.Target.ContentLost += this.Target_ContentLost;
					this.Target.Disposing += this.Target_Disposing;
					instance.GraphicsDevice.SetRenderTarget(this.Target);
					instance.GraphicsDevice.Clear(Color.Transparent);
					Main.spriteBatch.Begin(1, BlendState.AlphaBlend);
					this.PrepareShader();
					Rectangle value = sourceRect.Value;
					value.X = 0;
					value.Y = 0;
					Main.spriteBatch.Draw(originalTexture, value, Color.White);
					Main.spriteBatch.End();
					instance.GraphicsDevice.SetRenderTarget(null);
					this._wasPrepared = true;
				}
			}

			// Token: 0x06005DB5 RID: 23989 RVA: 0x006C9373 File Offset: 0x006C7573
			private void Target_Disposing(object sender, EventArgs e)
			{
				this._wasPrepared = false;
				this.Target = null;
			}

			// Token: 0x06005DB6 RID: 23990 RVA: 0x006C9383 File Offset: 0x006C7583
			private void Target_ContentLost(object sender, EventArgs e)
			{
				this._wasPrepared = false;
			}

			// Token: 0x06005DB7 RID: 23991 RVA: 0x006C938C File Offset: 0x006C758C
			protected void PrepareShader(int paintColor, TreePaintingSettings settings)
			{
				Effect tileShader = Main.tileShader;
				EffectParameter effectParameter = tileShader.Parameters["leafHueTestOffset"];
				if (effectParameter != null)
				{
					effectParameter.SetValue(settings.HueTestOffset);
				}
				EffectParameter effectParameter2 = tileShader.Parameters["leafMinHue"];
				if (effectParameter2 != null)
				{
					effectParameter2.SetValue(settings.SpecialGroupMinimalHueValue);
				}
				EffectParameter effectParameter3 = tileShader.Parameters["leafMaxHue"];
				if (effectParameter3 != null)
				{
					effectParameter3.SetValue(settings.SpecialGroupMaximumHueValue);
				}
				EffectParameter effectParameter4 = tileShader.Parameters["leafMinSat"];
				if (effectParameter4 != null)
				{
					effectParameter4.SetValue(settings.SpecialGroupMinimumSaturationValue);
				}
				EffectParameter effectParameter5 = tileShader.Parameters["leafMaxSat"];
				if (effectParameter5 != null)
				{
					effectParameter5.SetValue(settings.SpecialGroupMaximumSaturationValue);
				}
				EffectParameter effectParameter6 = tileShader.Parameters["invertSpecialGroupResult"];
				if (effectParameter6 != null)
				{
					effectParameter6.SetValue(settings.InvertSpecialGroupResult);
				}
				int index = Main.ConvertPaintIdToTileShaderIndex(paintColor, settings.UseSpecialGroups, settings.UseWallShaderHacks);
				tileShader.CurrentTechnique.Passes[index].Apply();
			}

			// Token: 0x0400770B RID: 30475
			public RenderTarget2D Target;

			// Token: 0x0400770C RID: 30476
			protected bool _wasPrepared;
		}

		// Token: 0x02000BC2 RID: 3010
		public class TreeTopRenderTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x06005DB9 RID: 23993 RVA: 0x006C9494 File Offset: 0x006C7694
			public override void Prepare()
			{
				Asset<Texture2D> asset;
				if (this.Key.TextureIndex >= 100)
				{
					int lookup = this.Key.TextureIndex - 100;
					asset = PlantLoader.Get<ModTree>(5, lookup).GetTopTextures();
				}
				else if (this.Key.TextureIndex < 0)
				{
					int lookup2 = -1 * this.Key.TextureIndex;
					if (lookup2 % 2 == 0)
					{
						lookup2 /= 2;
						asset = PlantLoader.Get<ModPalmTree>(323, lookup2).GetTopTextures();
					}
					else
					{
						lookup2 = (lookup2 - 1) / 2;
						asset = PlantLoader.Get<ModPalmTree>(323, lookup2).GetOasisTopTextures();
					}
				}
				else
				{
					asset = TextureAssets.TreeTop[this.Key.TextureIndex];
				}
				if (asset == null)
				{
					asset = TextureAssets.TreeTop[0];
				}
				Action wait = asset.Wait;
				if (wait != null)
				{
					wait();
				}
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x06005DBA RID: 23994 RVA: 0x006C9565 File Offset: 0x006C7765
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetTreeFoliageSettings(this.Key.TextureIndex, this.Key.TextureStyle));
			}

			// Token: 0x0400770D RID: 30477
			public TilePaintSystemV2.TreeFoliageVariantKey Key;
		}

		// Token: 0x02000BC3 RID: 3011
		public class TreeBranchTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x06005DBC RID: 23996 RVA: 0x006C959C File Offset: 0x006C779C
			public override void Prepare()
			{
				Asset<Texture2D> asset;
				if (this.Key.TextureIndex >= 100)
				{
					int lookup = this.Key.TextureIndex - 100;
					asset = PlantLoader.Get<ModTree>(5, lookup).GetBranchTextures();
				}
				else
				{
					asset = TextureAssets.TreeBranch[this.Key.TextureIndex];
				}
				if (asset == null)
				{
					asset = TextureAssets.TreeBranch[0];
				}
				Action wait = asset.Wait;
				if (wait != null)
				{
					wait();
				}
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x06005DBD RID: 23997 RVA: 0x006C9619 File Offset: 0x006C7819
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetTreeFoliageSettings(this.Key.TextureIndex, this.Key.TextureStyle));
			}

			// Token: 0x0400770E RID: 30478
			public TilePaintSystemV2.TreeFoliageVariantKey Key;
		}

		// Token: 0x02000BC4 RID: 3012
		public class TileRenderTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x06005DBF RID: 23999 RVA: 0x006C9650 File Offset: 0x006C7850
			public override void Prepare()
			{
				int val;
				Asset<Texture2D> asset;
				if (PlantLoader.plantIdToStyleLimit.TryGetValue(this.Key.TileType, out val) && val <= Math.Abs(this.Key.TileStyle))
				{
					int lookup = Math.Abs(this.Key.TileStyle) - val;
					asset = PlantLoader.GetTexture(this.Key.TileType, lookup);
				}
				else
				{
					Main.instance.LoadTiles(this.Key.TileType);
					asset = TextureAssets.Tile[this.Key.TileType];
				}
				Action wait = asset.Wait;
				if (wait != null)
				{
					wait();
				}
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x06005DC0 RID: 24000 RVA: 0x006C96FD File Offset: 0x006C78FD
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetTileSettings(this.Key.TileType, this.Key.TileStyle));
			}

			// Token: 0x0400770F RID: 30479
			public TilePaintSystemV2.TileVariationkey Key;
		}

		// Token: 0x02000BC5 RID: 3013
		public class WallRenderTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x06005DC2 RID: 24002 RVA: 0x006C9734 File Offset: 0x006C7934
			public override void Prepare()
			{
				Asset<Texture2D> asset = TextureAssets.Wall[this.Key.WallType];
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x06005DC3 RID: 24003 RVA: 0x006C9768 File Offset: 0x006C7968
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetWallSettings(this.Key.WallType));
			}

			// Token: 0x04007710 RID: 30480
			public TilePaintSystemV2.WallVariationKey Key;
		}

		// Token: 0x02000BC6 RID: 3014
		public struct TileVariationkey
		{
			// Token: 0x06005DC5 RID: 24005 RVA: 0x006C9793 File Offset: 0x006C7993
			public bool Equals(TilePaintSystemV2.TileVariationkey other)
			{
				return this.TileType == other.TileType && this.TileStyle == other.TileStyle && this.PaintColor == other.PaintColor;
			}

			// Token: 0x06005DC6 RID: 24006 RVA: 0x006C97C1 File Offset: 0x006C79C1
			public override bool Equals(object obj)
			{
				return obj is TilePaintSystemV2.TileVariationkey && this.Equals((TilePaintSystemV2.TileVariationkey)obj);
			}

			// Token: 0x06005DC7 RID: 24007 RVA: 0x006C97D9 File Offset: 0x006C79D9
			public override int GetHashCode()
			{
				return (this.TileType * 397 ^ this.TileStyle) * 397 ^ this.PaintColor;
			}

			// Token: 0x06005DC8 RID: 24008 RVA: 0x006C97FB File Offset: 0x006C79FB
			public static bool operator ==(TilePaintSystemV2.TileVariationkey left, TilePaintSystemV2.TileVariationkey right)
			{
				return left.Equals(right);
			}

			// Token: 0x06005DC9 RID: 24009 RVA: 0x006C9805 File Offset: 0x006C7A05
			public static bool operator !=(TilePaintSystemV2.TileVariationkey left, TilePaintSystemV2.TileVariationkey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04007711 RID: 30481
			public int TileType;

			// Token: 0x04007712 RID: 30482
			public int TileStyle;

			// Token: 0x04007713 RID: 30483
			public int PaintColor;
		}

		// Token: 0x02000BC7 RID: 3015
		public struct WallVariationKey
		{
			// Token: 0x06005DCA RID: 24010 RVA: 0x006C9812 File Offset: 0x006C7A12
			public bool Equals(TilePaintSystemV2.WallVariationKey other)
			{
				return this.WallType == other.WallType && this.PaintColor == other.PaintColor;
			}

			// Token: 0x06005DCB RID: 24011 RVA: 0x006C9832 File Offset: 0x006C7A32
			public override bool Equals(object obj)
			{
				return obj is TilePaintSystemV2.WallVariationKey && this.Equals((TilePaintSystemV2.WallVariationKey)obj);
			}

			// Token: 0x06005DCC RID: 24012 RVA: 0x006C984A File Offset: 0x006C7A4A
			public override int GetHashCode()
			{
				return this.WallType * 397 ^ this.PaintColor;
			}

			// Token: 0x06005DCD RID: 24013 RVA: 0x006C985F File Offset: 0x006C7A5F
			public static bool operator ==(TilePaintSystemV2.WallVariationKey left, TilePaintSystemV2.WallVariationKey right)
			{
				return left.Equals(right);
			}

			// Token: 0x06005DCE RID: 24014 RVA: 0x006C9869 File Offset: 0x006C7A69
			public static bool operator !=(TilePaintSystemV2.WallVariationKey left, TilePaintSystemV2.WallVariationKey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04007714 RID: 30484
			public int WallType;

			// Token: 0x04007715 RID: 30485
			public int PaintColor;
		}

		// Token: 0x02000BC8 RID: 3016
		public struct TreeFoliageVariantKey
		{
			// Token: 0x06005DCF RID: 24015 RVA: 0x006C9876 File Offset: 0x006C7A76
			public bool Equals(TilePaintSystemV2.TreeFoliageVariantKey other)
			{
				return this.TextureIndex == other.TextureIndex && this.TextureStyle == other.TextureStyle && this.PaintColor == other.PaintColor;
			}

			// Token: 0x06005DD0 RID: 24016 RVA: 0x006C98A4 File Offset: 0x006C7AA4
			public override bool Equals(object obj)
			{
				return obj is TilePaintSystemV2.TreeFoliageVariantKey && this.Equals((TilePaintSystemV2.TreeFoliageVariantKey)obj);
			}

			// Token: 0x06005DD1 RID: 24017 RVA: 0x006C98BC File Offset: 0x006C7ABC
			public override int GetHashCode()
			{
				return (this.TextureIndex * 397 ^ this.TextureStyle) * 397 ^ this.PaintColor;
			}

			// Token: 0x06005DD2 RID: 24018 RVA: 0x006C98DE File Offset: 0x006C7ADE
			public static bool operator ==(TilePaintSystemV2.TreeFoliageVariantKey left, TilePaintSystemV2.TreeFoliageVariantKey right)
			{
				return left.Equals(right);
			}

			// Token: 0x06005DD3 RID: 24019 RVA: 0x006C98E8 File Offset: 0x006C7AE8
			public static bool operator !=(TilePaintSystemV2.TreeFoliageVariantKey left, TilePaintSystemV2.TreeFoliageVariantKey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04007716 RID: 30486
			public int TextureIndex;

			// Token: 0x04007717 RID: 30487
			public int TextureStyle;

			// Token: 0x04007718 RID: 30488
			public int PaintColor;
		}
	}
}
