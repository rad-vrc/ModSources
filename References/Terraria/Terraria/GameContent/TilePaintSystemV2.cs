using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent
{
	// Token: 0x020001DD RID: 477
	public class TilePaintSystemV2
	{
		// Token: 0x06001C2F RID: 7215 RVA: 0x004F25F0 File Offset: 0x004F07F0
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

		// Token: 0x06001C30 RID: 7216 RVA: 0x004F2788 File Offset: 0x004F0988
		public void RequestTile(ref TilePaintSystemV2.TileVariationkey lookupKey)
		{
			TilePaintSystemV2.TileRenderTargetHolder tileRenderTargetHolder;
			if (!this._tilesRenders.TryGetValue(lookupKey, out tileRenderTargetHolder))
			{
				tileRenderTargetHolder = new TilePaintSystemV2.TileRenderTargetHolder
				{
					Key = lookupKey
				};
				this._tilesRenders.Add(lookupKey, tileRenderTargetHolder);
			}
			if (tileRenderTargetHolder.IsReady)
			{
				return;
			}
			this._requests.Add(tileRenderTargetHolder);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x004F27E4 File Offset: 0x004F09E4
		private void RequestTile_CheckForRelatedTileRequests(ref TilePaintSystemV2.TileVariationkey lookupKey)
		{
			if (lookupKey.TileType == 83)
			{
				TilePaintSystemV2.TileVariationkey tileVariationkey = new TilePaintSystemV2.TileVariationkey
				{
					TileType = 84,
					TileStyle = lookupKey.TileStyle,
					PaintColor = lookupKey.PaintColor
				};
				this.RequestTile(ref tileVariationkey);
			}
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x004F2830 File Offset: 0x004F0A30
		public void RequestWall(ref TilePaintSystemV2.WallVariationKey lookupKey)
		{
			TilePaintSystemV2.WallRenderTargetHolder wallRenderTargetHolder;
			if (!this._wallsRenders.TryGetValue(lookupKey, out wallRenderTargetHolder))
			{
				wallRenderTargetHolder = new TilePaintSystemV2.WallRenderTargetHolder
				{
					Key = lookupKey
				};
				this._wallsRenders.Add(lookupKey, wallRenderTargetHolder);
			}
			if (wallRenderTargetHolder.IsReady)
			{
				return;
			}
			this._requests.Add(wallRenderTargetHolder);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x004F288C File Offset: 0x004F0A8C
		public void RequestTreeTop(ref TilePaintSystemV2.TreeFoliageVariantKey lookupKey)
		{
			TilePaintSystemV2.TreeTopRenderTargetHolder treeTopRenderTargetHolder;
			if (!this._treeTopRenders.TryGetValue(lookupKey, out treeTopRenderTargetHolder))
			{
				treeTopRenderTargetHolder = new TilePaintSystemV2.TreeTopRenderTargetHolder
				{
					Key = lookupKey
				};
				this._treeTopRenders.Add(lookupKey, treeTopRenderTargetHolder);
			}
			if (treeTopRenderTargetHolder.IsReady)
			{
				return;
			}
			this._requests.Add(treeTopRenderTargetHolder);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x004F28E8 File Offset: 0x004F0AE8
		public void RequestTreeBranch(ref TilePaintSystemV2.TreeFoliageVariantKey lookupKey)
		{
			TilePaintSystemV2.TreeBranchTargetHolder treeBranchTargetHolder;
			if (!this._treeBranchRenders.TryGetValue(lookupKey, out treeBranchTargetHolder))
			{
				treeBranchTargetHolder = new TilePaintSystemV2.TreeBranchTargetHolder
				{
					Key = lookupKey
				};
				this._treeBranchRenders.Add(lookupKey, treeBranchTargetHolder);
			}
			if (treeBranchTargetHolder.IsReady)
			{
				return;
			}
			this._requests.Add(treeBranchTargetHolder);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x004F2944 File Offset: 0x004F0B44
		public Texture2D TryGetTileAndRequestIfNotReady(int tileType, int tileStyle, int paintColor)
		{
			TilePaintSystemV2.TileVariationkey key = new TilePaintSystemV2.TileVariationkey
			{
				TileType = tileType,
				TileStyle = tileStyle,
				PaintColor = paintColor
			};
			TilePaintSystemV2.TileRenderTargetHolder tileRenderTargetHolder;
			if (this._tilesRenders.TryGetValue(key, out tileRenderTargetHolder) && tileRenderTargetHolder.IsReady)
			{
				return tileRenderTargetHolder.Target;
			}
			this.RequestTile(ref key);
			return null;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x004F299C File Offset: 0x004F0B9C
		public Texture2D TryGetWallAndRequestIfNotReady(int wallType, int paintColor)
		{
			TilePaintSystemV2.WallVariationKey key = new TilePaintSystemV2.WallVariationKey
			{
				WallType = wallType,
				PaintColor = paintColor
			};
			TilePaintSystemV2.WallRenderTargetHolder wallRenderTargetHolder;
			if (this._wallsRenders.TryGetValue(key, out wallRenderTargetHolder) && wallRenderTargetHolder.IsReady)
			{
				return wallRenderTargetHolder.Target;
			}
			this.RequestWall(ref key);
			return null;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x004F29EC File Offset: 0x004F0BEC
		public Texture2D TryGetTreeTopAndRequestIfNotReady(int treeTopIndex, int treeTopStyle, int paintColor)
		{
			TilePaintSystemV2.TreeFoliageVariantKey key = new TilePaintSystemV2.TreeFoliageVariantKey
			{
				TextureIndex = treeTopIndex,
				TextureStyle = treeTopStyle,
				PaintColor = paintColor
			};
			TilePaintSystemV2.TreeTopRenderTargetHolder treeTopRenderTargetHolder;
			if (this._treeTopRenders.TryGetValue(key, out treeTopRenderTargetHolder) && treeTopRenderTargetHolder.IsReady)
			{
				return treeTopRenderTargetHolder.Target;
			}
			this.RequestTreeTop(ref key);
			return null;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x004F2A44 File Offset: 0x004F0C44
		public Texture2D TryGetTreeBranchAndRequestIfNotReady(int treeTopIndex, int treeTopStyle, int paintColor)
		{
			TilePaintSystemV2.TreeFoliageVariantKey key = new TilePaintSystemV2.TreeFoliageVariantKey
			{
				TextureIndex = treeTopIndex,
				TextureStyle = treeTopStyle,
				PaintColor = paintColor
			};
			TilePaintSystemV2.TreeBranchTargetHolder treeBranchTargetHolder;
			if (this._treeBranchRenders.TryGetValue(key, out treeBranchTargetHolder) && treeBranchTargetHolder.IsReady)
			{
				return treeBranchTargetHolder.Target;
			}
			this.RequestTreeBranch(ref key);
			return null;
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x004F2A9C File Offset: 0x004F0C9C
		public void PrepareAllRequests()
		{
			if (this._requests.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this._requests.Count; i++)
			{
				this._requests[i].Prepare();
			}
			this._requests.Clear();
		}

		// Token: 0x04004394 RID: 17300
		private Dictionary<TilePaintSystemV2.TileVariationkey, TilePaintSystemV2.TileRenderTargetHolder> _tilesRenders = new Dictionary<TilePaintSystemV2.TileVariationkey, TilePaintSystemV2.TileRenderTargetHolder>();

		// Token: 0x04004395 RID: 17301
		private Dictionary<TilePaintSystemV2.WallVariationKey, TilePaintSystemV2.WallRenderTargetHolder> _wallsRenders = new Dictionary<TilePaintSystemV2.WallVariationKey, TilePaintSystemV2.WallRenderTargetHolder>();

		// Token: 0x04004396 RID: 17302
		private Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeTopRenderTargetHolder> _treeTopRenders = new Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeTopRenderTargetHolder>();

		// Token: 0x04004397 RID: 17303
		private Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeBranchTargetHolder> _treeBranchRenders = new Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeBranchTargetHolder>();

		// Token: 0x04004398 RID: 17304
		private List<TilePaintSystemV2.ARenderTargetHolder> _requests = new List<TilePaintSystemV2.ARenderTargetHolder>();

		// Token: 0x020005FC RID: 1532
		public abstract class ARenderTargetHolder
		{
			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x06003319 RID: 13081 RVA: 0x00605284 File Offset: 0x00603484
			public bool IsReady
			{
				get
				{
					return this._wasPrepared;
				}
			}

			// Token: 0x0600331A RID: 13082
			public abstract void Prepare();

			// Token: 0x0600331B RID: 13083
			public abstract void PrepareShader();

			// Token: 0x0600331C RID: 13084 RVA: 0x0060528C File Offset: 0x0060348C
			public void Clear()
			{
				if (this.Target != null && !this.Target.IsDisposed)
				{
					this.Target.Dispose();
				}
			}

			// Token: 0x0600331D RID: 13085 RVA: 0x006052B0 File Offset: 0x006034B0
			protected void PrepareTextureIfNecessary(Texture2D originalTexture, Rectangle? sourceRect = null)
			{
				if (this.Target != null && !this.Target.IsContentLost)
				{
					return;
				}
				Main instance = Main.instance;
				if (sourceRect == null)
				{
					sourceRect = new Rectangle?(originalTexture.Frame(1, 1, 0, 0, 0, 0));
				}
				this.Target = new RenderTarget2D(instance.GraphicsDevice, sourceRect.Value.Width, sourceRect.Value.Height, false, instance.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
				this.Target.ContentLost += this.Target_ContentLost;
				this.Target.Disposing += this.Target_Disposing;
				instance.GraphicsDevice.SetRenderTarget(this.Target);
				instance.GraphicsDevice.Clear(Color.Transparent);
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
				this.PrepareShader();
				Rectangle value = sourceRect.Value;
				value.X = 0;
				value.Y = 0;
				Main.spriteBatch.Draw(originalTexture, value, Color.White);
				Main.spriteBatch.End();
				instance.GraphicsDevice.SetRenderTarget(null);
				this._wasPrepared = true;
			}

			// Token: 0x0600331E RID: 13086 RVA: 0x006053DD File Offset: 0x006035DD
			private void Target_Disposing(object sender, EventArgs e)
			{
				this._wasPrepared = false;
				this.Target = null;
			}

			// Token: 0x0600331F RID: 13087 RVA: 0x006053ED File Offset: 0x006035ED
			private void Target_ContentLost(object sender, EventArgs e)
			{
				this._wasPrepared = false;
			}

			// Token: 0x06003320 RID: 13088 RVA: 0x006053F8 File Offset: 0x006035F8
			protected void PrepareShader(int paintColor, TreePaintingSettings settings)
			{
				Effect tileShader = Main.tileShader;
				tileShader.Parameters["leafHueTestOffset"].SetValue(settings.HueTestOffset);
				tileShader.Parameters["leafMinHue"].SetValue(settings.SpecialGroupMinimalHueValue);
				tileShader.Parameters["leafMaxHue"].SetValue(settings.SpecialGroupMaximumHueValue);
				tileShader.Parameters["leafMinSat"].SetValue(settings.SpecialGroupMinimumSaturationValue);
				tileShader.Parameters["leafMaxSat"].SetValue(settings.SpecialGroupMaximumSaturationValue);
				tileShader.Parameters["invertSpecialGroupResult"].SetValue(settings.InvertSpecialGroupResult);
				int index = Main.ConvertPaintIdToTileShaderIndex(paintColor, settings.UseSpecialGroups, settings.UseWallShaderHacks);
				tileShader.CurrentTechnique.Passes[index].Apply();
			}

			// Token: 0x04006019 RID: 24601
			public RenderTarget2D Target;

			// Token: 0x0400601A RID: 24602
			protected bool _wasPrepared;
		}

		// Token: 0x020005FD RID: 1533
		public class TreeTopRenderTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x06003322 RID: 13090 RVA: 0x006054D4 File Offset: 0x006036D4
			public override void Prepare()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(TextureAssets.TreeTop[this.Key.TextureIndex].Name, 1);
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x06003323 RID: 13091 RVA: 0x00605518 File Offset: 0x00603718
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetTreeFoliageSettings(this.Key.TextureIndex, this.Key.TextureStyle));
			}

			// Token: 0x0400601B RID: 24603
			public TilePaintSystemV2.TreeFoliageVariantKey Key;
		}

		// Token: 0x020005FE RID: 1534
		public class TreeBranchTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x06003325 RID: 13093 RVA: 0x00605550 File Offset: 0x00603750
			public override void Prepare()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(TextureAssets.TreeBranch[this.Key.TextureIndex].Name, 1);
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x06003326 RID: 13094 RVA: 0x00605594 File Offset: 0x00603794
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetTreeFoliageSettings(this.Key.TextureIndex, this.Key.TextureStyle));
			}

			// Token: 0x0400601C RID: 24604
			public TilePaintSystemV2.TreeFoliageVariantKey Key;
		}

		// Token: 0x020005FF RID: 1535
		public class TileRenderTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x06003328 RID: 13096 RVA: 0x006055C4 File Offset: 0x006037C4
			public override void Prepare()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(TextureAssets.Tile[this.Key.TileType].Name, 1);
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x06003329 RID: 13097 RVA: 0x00605608 File Offset: 0x00603808
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetTileSettings(this.Key.TileType, this.Key.TileStyle));
			}

			// Token: 0x0400601D RID: 24605
			public TilePaintSystemV2.TileVariationkey Key;
		}

		// Token: 0x02000600 RID: 1536
		public class WallRenderTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x0600332B RID: 13099 RVA: 0x00605638 File Offset: 0x00603838
			public override void Prepare()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(TextureAssets.Wall[this.Key.WallType].Name, 1);
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x0600332C RID: 13100 RVA: 0x0060567C File Offset: 0x0060387C
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetWallSettings(this.Key.WallType));
			}

			// Token: 0x0400601E RID: 24606
			public TilePaintSystemV2.WallVariationKey Key;
		}

		// Token: 0x02000601 RID: 1537
		public struct TileVariationkey
		{
			// Token: 0x0600332E RID: 13102 RVA: 0x0060569F File Offset: 0x0060389F
			public bool Equals(TilePaintSystemV2.TileVariationkey other)
			{
				return this.TileType == other.TileType && this.TileStyle == other.TileStyle && this.PaintColor == other.PaintColor;
			}

			// Token: 0x0600332F RID: 13103 RVA: 0x006056CD File Offset: 0x006038CD
			public override bool Equals(object obj)
			{
				return obj is TilePaintSystemV2.TileVariationkey && this.Equals((TilePaintSystemV2.TileVariationkey)obj);
			}

			// Token: 0x06003330 RID: 13104 RVA: 0x006056E5 File Offset: 0x006038E5
			public override int GetHashCode()
			{
				return (this.TileType * 397 ^ this.TileStyle) * 397 ^ this.PaintColor;
			}

			// Token: 0x06003331 RID: 13105 RVA: 0x00605707 File Offset: 0x00603907
			public static bool operator ==(TilePaintSystemV2.TileVariationkey left, TilePaintSystemV2.TileVariationkey right)
			{
				return left.Equals(right);
			}

			// Token: 0x06003332 RID: 13106 RVA: 0x00605711 File Offset: 0x00603911
			public static bool operator !=(TilePaintSystemV2.TileVariationkey left, TilePaintSystemV2.TileVariationkey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x0400601F RID: 24607
			public int TileType;

			// Token: 0x04006020 RID: 24608
			public int TileStyle;

			// Token: 0x04006021 RID: 24609
			public int PaintColor;
		}

		// Token: 0x02000602 RID: 1538
		public struct WallVariationKey
		{
			// Token: 0x06003333 RID: 13107 RVA: 0x0060571E File Offset: 0x0060391E
			public bool Equals(TilePaintSystemV2.WallVariationKey other)
			{
				return this.WallType == other.WallType && this.PaintColor == other.PaintColor;
			}

			// Token: 0x06003334 RID: 13108 RVA: 0x0060573E File Offset: 0x0060393E
			public override bool Equals(object obj)
			{
				return obj is TilePaintSystemV2.WallVariationKey && this.Equals((TilePaintSystemV2.WallVariationKey)obj);
			}

			// Token: 0x06003335 RID: 13109 RVA: 0x00605756 File Offset: 0x00603956
			public override int GetHashCode()
			{
				return this.WallType * 397 ^ this.PaintColor;
			}

			// Token: 0x06003336 RID: 13110 RVA: 0x0060576B File Offset: 0x0060396B
			public static bool operator ==(TilePaintSystemV2.WallVariationKey left, TilePaintSystemV2.WallVariationKey right)
			{
				return left.Equals(right);
			}

			// Token: 0x06003337 RID: 13111 RVA: 0x00605775 File Offset: 0x00603975
			public static bool operator !=(TilePaintSystemV2.WallVariationKey left, TilePaintSystemV2.WallVariationKey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04006022 RID: 24610
			public int WallType;

			// Token: 0x04006023 RID: 24611
			public int PaintColor;
		}

		// Token: 0x02000603 RID: 1539
		public struct TreeFoliageVariantKey
		{
			// Token: 0x06003338 RID: 13112 RVA: 0x00605782 File Offset: 0x00603982
			public bool Equals(TilePaintSystemV2.TreeFoliageVariantKey other)
			{
				return this.TextureIndex == other.TextureIndex && this.TextureStyle == other.TextureStyle && this.PaintColor == other.PaintColor;
			}

			// Token: 0x06003339 RID: 13113 RVA: 0x006057B0 File Offset: 0x006039B0
			public override bool Equals(object obj)
			{
				return obj is TilePaintSystemV2.TreeFoliageVariantKey && this.Equals((TilePaintSystemV2.TreeFoliageVariantKey)obj);
			}

			// Token: 0x0600333A RID: 13114 RVA: 0x006057C8 File Offset: 0x006039C8
			public override int GetHashCode()
			{
				return (this.TextureIndex * 397 ^ this.TextureStyle) * 397 ^ this.PaintColor;
			}

			// Token: 0x0600333B RID: 13115 RVA: 0x006057EA File Offset: 0x006039EA
			public static bool operator ==(TilePaintSystemV2.TreeFoliageVariantKey left, TilePaintSystemV2.TreeFoliageVariantKey right)
			{
				return left.Equals(right);
			}

			// Token: 0x0600333C RID: 13116 RVA: 0x006057F4 File Offset: 0x006039F4
			public static bool operator !=(TilePaintSystemV2.TreeFoliageVariantKey left, TilePaintSystemV2.TreeFoliageVariantKey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04006024 RID: 24612
			public int TextureIndex;

			// Token: 0x04006025 RID: 24613
			public int TextureStyle;

			// Token: 0x04006026 RID: 24614
			public int PaintColor;
		}
	}
}
