using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x0200043F RID: 1087
	public class TileBatch : IDisposable
	{
		// Token: 0x060035CB RID: 13771 RVA: 0x00578C9E File Offset: 0x00576E9E
		public void Begin(RasterizerState rasterizer, Matrix transformation)
		{
			this.Begin(0, null, null, null, rasterizer, null, transformation);
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x00578CAD File Offset: 0x00576EAD
		public void Begin(Matrix transformation)
		{
			this.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, transformation);
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x00578CCC File Offset: 0x00576ECC
		public void Begin()
		{
			this.Begin(0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.Identity);
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x00578CEF File Offset: 0x00576EEF
		public void Begin(SpriteSortMode sortMode, BlendState blendState)
		{
			this.Begin(sortMode, blendState, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.Identity);
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x00578D0E File Offset: 0x00576F0E
		public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState)
		{
			this.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, null, Matrix.Identity);
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x00578D23 File Offset: 0x00576F23
		public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect)
		{
			this.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, Matrix.Identity);
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x00578D39 File Offset: 0x00576F39
		public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformationMatrix)
		{
			if (sortMode != null && sortMode != 2)
			{
				throw new NotImplementedException("TileBatch only supports SpriteSortMode.Deferred and SpriteSortMode.Texture.");
			}
			this._sortMode = sortMode;
			this._spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformationMatrix);
			this._spriteBatch.End();
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x00578D75 File Offset: 0x00576F75
		public void End()
		{
			if (this._sortMode == null)
			{
				this.DrawBatch();
				return;
			}
			if (this._sortMode == 2)
			{
				this.SortedDrawBatch();
			}
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x00578D98 File Offset: 0x00576F98
		public void Draw(Texture2D texture, Vector2 position, VertexColors colors)
		{
			this.Draw(texture, position, null, colors, Vector2.Zero, 1f, 0);
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x00578DC4 File Offset: 0x00576FC4
		public void Draw(Texture2D texture, Vector4 destination, VertexColors colors)
		{
			this.InternalDraw(texture, destination, null, colors, 0f, Vector2.Zero, 0, 0f);
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x00578DF4 File Offset: 0x00576FF4
		public void Draw(Texture2D texture, Vector4 destination, Rectangle? sourceRectangle, VertexColors colors)
		{
			this.InternalDraw(texture, destination, sourceRectangle, colors, 0f, Vector2.Zero, 0, 0f);
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x00578E1C File Offset: 0x0057701C
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, VertexColors colors, Vector2 origin, float scale, SpriteEffects effects)
		{
			float z;
			float w;
			if (sourceRectangle != null)
			{
				z = (float)sourceRectangle.Value.Width * scale;
				w = (float)sourceRectangle.Value.Height * scale;
			}
			else
			{
				z = (float)texture.Width * scale;
				w = (float)texture.Height * scale;
			}
			this.InternalDraw(texture, new Vector4(position.X, position.Y, z, w), sourceRectangle, colors, 0f, origin * scale, effects, 0f);
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x00578EA0 File Offset: 0x005770A0
		public void Draw(Texture2D texture, Vector4 destination, Rectangle? sourceRectangle, VertexColors colors, Vector2 origin, SpriteEffects effects, float rotation)
		{
			this.InternalDraw(texture, destination, sourceRectangle, colors, rotation, origin, effects, 0f);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x00578EC4 File Offset: 0x005770C4
		internal void InternalDraw(Texture2D texture, Vector4 destinationRectangle, Rectangle? sourceRectangle, VertexColors colors, float rotation, Vector2 origin, SpriteEffects effect, float depth)
		{
			Vector4 sourceRectangle2 = default(Vector4);
			if (sourceRectangle != null)
			{
				sourceRectangle2.X = (float)sourceRectangle.Value.X;
				sourceRectangle2.Y = (float)sourceRectangle.Value.Y;
				sourceRectangle2.Z = (float)sourceRectangle.Value.Width;
				sourceRectangle2.W = (float)sourceRectangle.Value.Height;
			}
			else
			{
				sourceRectangle2.X = 0f;
				sourceRectangle2.Y = 0f;
				sourceRectangle2.Z = (float)texture.Width;
				sourceRectangle2.W = (float)texture.Height;
			}
			Vector2 texCoordTL = default(Vector2);
			texCoordTL.X = sourceRectangle2.X / (float)texture.Width;
			texCoordTL.Y = sourceRectangle2.Y / (float)texture.Height;
			Vector2 texCoordBR = default(Vector2);
			texCoordBR.X = (sourceRectangle2.X + sourceRectangle2.Z) / (float)texture.Width;
			texCoordBR.Y = (sourceRectangle2.Y + sourceRectangle2.W) / (float)texture.Height;
			if ((effect & 2) != null)
			{
				float y = texCoordBR.Y;
				texCoordBR.Y = texCoordTL.Y;
				texCoordTL.Y = y;
			}
			if ((effect & 1) != null)
			{
				float x = texCoordBR.X;
				texCoordBR.X = texCoordTL.X;
				texCoordTL.X = x;
			}
			this.QueueSprite(destinationRectangle, -origin, colors, sourceRectangle2, texCoordTL, texCoordBR, texture, depth, rotation);
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x00579038 File Offset: 0x00577238
		public TileBatch(GraphicsDevice graphicsDevice)
		{
			this._spriteBatch = new SpriteBatch(graphicsDevice);
			this._graphicsDevice = graphicsDevice;
			for (int i = 0; i < 32; i++)
			{
				this._batchDrawGroups.Add(new TileBatch.BatchDrawGroup());
			}
			this._sortedIndexBuffer = new DynamicIndexBuffer(graphicsDevice, 0, 12288, 1);
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x005790AC File Offset: 0x005772AC
		~TileBatch()
		{
			this.Dispose();
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x005790D8 File Offset: 0x005772D8
		public void Dispose()
		{
			this._sortedIndexBuffer.Dispose();
			this._spriteBatch.Dispose();
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x005790F0 File Offset: 0x005772F0
		private void SortAndApplyIndexData(TileBatch.BatchDrawGroup batchDrawGroup)
		{
			Array.Sort<TileBatch.BatchDrawInfo>(batchDrawGroup.BatchDraws, 0, batchDrawGroup.BatchDrawCount, TileBatch.TextureComparer.Instance);
			int num = 0;
			for (int i = 0; i < batchDrawGroup.BatchDrawCount; i++)
			{
				TileBatch.BatchDrawInfo batchDrawInfo = batchDrawGroup.BatchDraws[i];
				for (int j = 0; j < batchDrawInfo.Count; j++)
				{
					int num2 = batchDrawInfo.Index * 4 + j * 4;
					this._sortedIndexData[num] = (short)num2;
					this._sortedIndexData[num + 1] = (short)(num2 + 1);
					this._sortedIndexData[num + 2] = (short)(num2 + 2);
					this._sortedIndexData[num + 3] = (short)(num2 + 3);
					this._sortedIndexData[num + 4] = (short)(num2 + 2);
					this._sortedIndexData[num + 5] = (short)(num2 + 1);
					num += 6;
				}
			}
			this._sortedIndexBuffer.SetData<short>(this._sortedIndexData, 0, num);
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x005791C8 File Offset: 0x005773C8
		private void SortedDrawBatch()
		{
			if (this._lastBatchDrawGroupIndex == 0 && this._batchDrawGroups[this._lastBatchDrawGroupIndex].SpriteCount == 0)
			{
				return;
			}
			this.FlushRemainingBatch();
			VertexBuffer vertexBuffer = this._graphicsDevice.GetVertexBuffers()[0].VertexBuffer;
			this._graphicsDevice.Indices = this._sortedIndexBuffer;
			for (int i = 0; i <= this._lastBatchDrawGroupIndex; i++)
			{
				TileBatch.BatchDrawGroup batchDrawGroup = this._batchDrawGroups[i];
				int vertexCount = batchDrawGroup.VertexCount;
				vertexBuffer.SetData<VertexPositionColorTexture>(this._batchDrawGroups[i].VertexArray, 0, vertexCount, 0);
				this.SortAndApplyIndexData(batchDrawGroup);
				int num = 0;
				for (int j = 0; j < batchDrawGroup.BatchDrawCount; j++)
				{
					TileBatch.BatchDrawInfo batchDrawInfo = batchDrawGroup.BatchDraws[j];
					this._graphicsDevice.Textures[0] = batchDrawInfo.Texture;
					int num2 = batchDrawInfo.Count;
					while (j + 1 < batchDrawGroup.BatchDrawCount && batchDrawInfo.Texture == batchDrawGroup.BatchDraws[j + 1].Texture)
					{
						num2 += batchDrawGroup.BatchDraws[j + 1].Count;
						j++;
					}
					this._graphicsDevice.DrawIndexedPrimitives(0, 0, 0, num2 * 4, num, num2 * 2);
					num += num2 * 6;
				}
				batchDrawGroup.Reset();
			}
			this._currentBatchDrawInfo = TileBatch.BatchDrawInfo.Empty;
			this._lastBatchDrawGroupIndex = 0;
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x00579344 File Offset: 0x00577544
		private void DrawBatch()
		{
			if (this._lastBatchDrawGroupIndex == 0 && this._batchDrawGroups[this._lastBatchDrawGroupIndex].SpriteCount == 0)
			{
				return;
			}
			this.FlushRemainingBatch();
			VertexBuffer vertexBuffer = this._graphicsDevice.GetVertexBuffers()[0].VertexBuffer;
			for (int i = 0; i <= this._lastBatchDrawGroupIndex; i++)
			{
				TileBatch.BatchDrawGroup batchDrawGroup = this._batchDrawGroups[i];
				int vertexCount = batchDrawGroup.VertexCount;
				vertexBuffer.SetData<VertexPositionColorTexture>(this._batchDrawGroups[i].VertexArray, 0, vertexCount, 1);
				for (int j = 0; j < batchDrawGroup.BatchDrawCount; j++)
				{
					TileBatch.BatchDrawInfo batchDrawInfo = batchDrawGroup.BatchDraws[j];
					this._graphicsDevice.Textures[0] = batchDrawInfo.Texture;
					this._graphicsDevice.DrawIndexedPrimitives(0, 0, 0, batchDrawInfo.Count * 4, batchDrawInfo.Index * 6, batchDrawInfo.Count * 2);
				}
				batchDrawGroup.Reset();
			}
			this._currentBatchDrawInfo = TileBatch.BatchDrawInfo.Empty;
			this._lastBatchDrawGroupIndex = 0;
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x00579454 File Offset: 0x00577654
		private void QueueSprite(Vector4 destinationRect, Vector2 origin, VertexColors colors, Vector4 sourceRectangle, Vector2 texCoordTL, Vector2 texCoordBR, Texture2D texture, float depth, float rotation)
		{
			this.UpdateCurrentBatchDraw(texture);
			float num = origin.X / sourceRectangle.Z;
			float num6 = origin.Y / sourceRectangle.W;
			float x = destinationRect.X;
			float y = destinationRect.Y;
			float z = destinationRect.Z;
			float w = destinationRect.W;
			float num2 = num * z;
			float num3 = num6 * w;
			TileBatch.BatchDrawGroup batchDrawGroup = this._batchDrawGroups[this._lastBatchDrawGroupIndex];
			float num4;
			float num5;
			if (rotation != 0f)
			{
				num4 = (float)Math.Cos((double)rotation);
				num5 = (float)Math.Sin((double)rotation);
			}
			else
			{
				num4 = 1f;
				num5 = 0f;
			}
			int vertexCount = batchDrawGroup.VertexCount;
			batchDrawGroup.VertexArray[vertexCount].Position.X = x + num2 * num4 - num3 * num5;
			batchDrawGroup.VertexArray[vertexCount].Position.Y = y + num2 * num5 + num3 * num4;
			batchDrawGroup.VertexArray[vertexCount].Position.Z = depth;
			batchDrawGroup.VertexArray[vertexCount].Color = colors.TopLeftColor;
			batchDrawGroup.VertexArray[vertexCount].TextureCoordinate.X = texCoordTL.X;
			batchDrawGroup.VertexArray[vertexCount].TextureCoordinate.Y = texCoordTL.Y;
			vertexCount++;
			batchDrawGroup.VertexArray[vertexCount].Position.X = x + (num2 + z) * num4 - num3 * num5;
			batchDrawGroup.VertexArray[vertexCount].Position.Y = y + (num2 + z) * num5 + num3 * num4;
			batchDrawGroup.VertexArray[vertexCount].Position.Z = depth;
			batchDrawGroup.VertexArray[vertexCount].Color = colors.TopRightColor;
			batchDrawGroup.VertexArray[vertexCount].TextureCoordinate.X = texCoordBR.X;
			batchDrawGroup.VertexArray[vertexCount].TextureCoordinate.Y = texCoordTL.Y;
			vertexCount++;
			batchDrawGroup.VertexArray[vertexCount].Position.X = x + num2 * num4 - (num3 + w) * num5;
			batchDrawGroup.VertexArray[vertexCount].Position.Y = y + num2 * num5 + (num3 + w) * num4;
			batchDrawGroup.VertexArray[vertexCount].Position.Z = depth;
			batchDrawGroup.VertexArray[vertexCount].Color = colors.BottomLeftColor;
			batchDrawGroup.VertexArray[vertexCount].TextureCoordinate.X = texCoordTL.X;
			batchDrawGroup.VertexArray[vertexCount].TextureCoordinate.Y = texCoordBR.Y;
			vertexCount++;
			batchDrawGroup.VertexArray[vertexCount].Position.X = x + (num2 + z) * num4 - (num3 + w) * num5;
			batchDrawGroup.VertexArray[vertexCount].Position.Y = y + (num2 + z) * num5 + (num3 + w) * num4;
			batchDrawGroup.VertexArray[vertexCount].Position.Z = depth;
			batchDrawGroup.VertexArray[vertexCount].Color = colors.BottomRightColor;
			batchDrawGroup.VertexArray[vertexCount].TextureCoordinate.X = texCoordBR.X;
			batchDrawGroup.VertexArray[vertexCount].TextureCoordinate.Y = texCoordBR.Y;
			batchDrawGroup.SpriteCount++;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x005797FC File Offset: 0x005779FC
		private void UpdateCurrentBatchDraw(Texture2D texture)
		{
			TileBatch.BatchDrawGroup batchDrawGroup = this._batchDrawGroups[this._lastBatchDrawGroupIndex];
			if (batchDrawGroup.SpriteCount >= 2048)
			{
				this._currentBatchDrawInfo.Count = 2048 - this._currentBatchDrawInfo.Index;
				this._batchDrawGroups[this._lastBatchDrawGroupIndex].AddBatch(this._currentBatchDrawInfo);
				this._currentBatchDrawInfo = new TileBatch.BatchDrawInfo(texture);
				this._lastBatchDrawGroupIndex++;
				if (this._lastBatchDrawGroupIndex >= this._batchDrawGroups.Count)
				{
					this._batchDrawGroups.Add(new TileBatch.BatchDrawGroup());
					return;
				}
			}
			else if (texture != this._currentBatchDrawInfo.Texture)
			{
				if (batchDrawGroup.SpriteCount != 0 || this._lastBatchDrawGroupIndex != 0)
				{
					this._currentBatchDrawInfo.Count = batchDrawGroup.SpriteCount - this._currentBatchDrawInfo.Index;
					batchDrawGroup.AddBatch(this._currentBatchDrawInfo);
				}
				this._currentBatchDrawInfo = new TileBatch.BatchDrawInfo(texture, batchDrawGroup.SpriteCount, 0);
			}
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x005798F8 File Offset: 0x00577AF8
		private void FlushRemainingBatch()
		{
			TileBatch.BatchDrawGroup batchDrawGroup = this._batchDrawGroups[this._lastBatchDrawGroupIndex];
			if (this._currentBatchDrawInfo.Index != batchDrawGroup.SpriteCount)
			{
				this._currentBatchDrawInfo.Count = batchDrawGroup.SpriteCount - this._currentBatchDrawInfo.Index;
				batchDrawGroup.AddBatch(this._currentBatchDrawInfo);
			}
		}

		// Token: 0x04004FE2 RID: 20450
		private SpriteSortMode _sortMode;

		// Token: 0x04004FE3 RID: 20451
		private const int MAX_SPRITES = 2048;

		// Token: 0x04004FE4 RID: 20452
		private const int MAX_VERTICES = 8192;

		// Token: 0x04004FE5 RID: 20453
		private const int MAX_INDICES = 12288;

		// Token: 0x04004FE6 RID: 20454
		private const int INITIAL_BATCH_DRAW_GROUP_COUNT = 32;

		// Token: 0x04004FE7 RID: 20455
		private short[] _sortedIndexData = new short[12288];

		// Token: 0x04004FE8 RID: 20456
		private DynamicIndexBuffer _sortedIndexBuffer;

		// Token: 0x04004FE9 RID: 20457
		private int _lastBatchDrawGroupIndex;

		// Token: 0x04004FEA RID: 20458
		private TileBatch.BatchDrawInfo _currentBatchDrawInfo;

		// Token: 0x04004FEB RID: 20459
		private List<TileBatch.BatchDrawGroup> _batchDrawGroups = new List<TileBatch.BatchDrawGroup>();

		// Token: 0x04004FEC RID: 20460
		private readonly GraphicsDevice _graphicsDevice;

		// Token: 0x04004FED RID: 20461
		private SpriteBatch _spriteBatch;

		// Token: 0x02000B73 RID: 2931
		private class TextureComparer : IComparer<TileBatch.BatchDrawInfo>
		{
			// Token: 0x06005CD9 RID: 23769 RVA: 0x006C53D0 File Offset: 0x006C35D0
			public int Compare(TileBatch.BatchDrawInfo info1, TileBatch.BatchDrawInfo info2)
			{
				return info1.Texture.GetHashCode().CompareTo(info2.Texture.GetHashCode());
			}

			// Token: 0x040075F2 RID: 30194
			public static TileBatch.TextureComparer Instance = new TileBatch.TextureComparer();
		}

		// Token: 0x02000B74 RID: 2932
		private struct BatchDrawInfo
		{
			// Token: 0x06005CDC RID: 23772 RVA: 0x006C540F File Offset: 0x006C360F
			public BatchDrawInfo(Texture2D texture, int index, int count)
			{
				this.Texture = texture;
				this.Index = index;
				this.Count = count;
			}

			// Token: 0x06005CDD RID: 23773 RVA: 0x006C5426 File Offset: 0x006C3626
			public BatchDrawInfo(Texture2D texture)
			{
				this.Texture = texture;
				this.Index = 0;
				this.Count = 0;
			}

			// Token: 0x040075F3 RID: 30195
			public static readonly TileBatch.BatchDrawInfo Empty = new TileBatch.BatchDrawInfo(null, 0, 0);

			// Token: 0x040075F4 RID: 30196
			public readonly Texture2D Texture;

			// Token: 0x040075F5 RID: 30197
			public readonly int Index;

			// Token: 0x040075F6 RID: 30198
			public int Count;
		}

		// Token: 0x02000B75 RID: 2933
		private class BatchDrawGroup
		{
			// Token: 0x17000945 RID: 2373
			// (get) Token: 0x06005CDF RID: 23775 RVA: 0x006C544C File Offset: 0x006C364C
			public int VertexCount
			{
				get
				{
					return this.SpriteCount * 4;
				}
			}

			// Token: 0x06005CE0 RID: 23776 RVA: 0x006C5456 File Offset: 0x006C3656
			public BatchDrawGroup()
			{
				this.VertexArray = new VertexPositionColorTexture[8192];
				this.BatchDraws = new TileBatch.BatchDrawInfo[2048];
				this.BatchDrawCount = 0;
				this.SpriteCount = 0;
			}

			// Token: 0x06005CE1 RID: 23777 RVA: 0x006C548C File Offset: 0x006C368C
			public void Reset()
			{
				this.BatchDrawCount = 0;
				this.SpriteCount = 0;
			}

			// Token: 0x06005CE2 RID: 23778 RVA: 0x006C549C File Offset: 0x006C369C
			public void AddBatch(TileBatch.BatchDrawInfo batch)
			{
				TileBatch.BatchDrawInfo[] batchDraws = this.BatchDraws;
				int batchDrawCount = this.BatchDrawCount;
				this.BatchDrawCount = batchDrawCount + 1;
				batchDraws[batchDrawCount] = batch;
			}

			// Token: 0x040075F7 RID: 30199
			public VertexPositionColorTexture[] VertexArray;

			// Token: 0x040075F8 RID: 30200
			public TileBatch.BatchDrawInfo[] BatchDraws;

			// Token: 0x040075F9 RID: 30201
			public int BatchDrawCount;

			// Token: 0x040075FA RID: 30202
			public int SpriteCount;
		}
	}
}
