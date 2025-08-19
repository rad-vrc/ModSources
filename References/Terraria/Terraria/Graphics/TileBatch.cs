using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x020000F7 RID: 247
	public class TileBatch
	{
		// Token: 0x060015F6 RID: 5622 RVA: 0x004C4C7C File Offset: 0x004C2E7C
		public TileBatch(GraphicsDevice graphicsDevice)
		{
			this._graphicsDevice = graphicsDevice;
			this._spriteBatch = new SpriteBatch(graphicsDevice);
			this.Allocate();
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x004C4CC8 File Offset: 0x004C2EC8
		private void Allocate()
		{
			if (this._vertexBuffer == null || this._vertexBuffer.IsDisposed)
			{
				this._vertexBuffer = new DynamicVertexBuffer(this._graphicsDevice, typeof(VertexPositionColorTexture), 8192, BufferUsage.WriteOnly);
				this._vertexBufferPosition = 0;
				this._vertexBuffer.ContentLost += delegate(object <p0>, EventArgs <p1>)
				{
					this._vertexBufferPosition = 0;
				};
			}
			if (this._indexBuffer == null || this._indexBuffer.IsDisposed)
			{
				if (this._fallbackIndexData == null)
				{
					this._fallbackIndexData = new short[12288];
					for (int i = 0; i < 2048; i++)
					{
						this._fallbackIndexData[i * 6] = (short)(i * 4);
						this._fallbackIndexData[i * 6 + 1] = (short)(i * 4 + 1);
						this._fallbackIndexData[i * 6 + 2] = (short)(i * 4 + 2);
						this._fallbackIndexData[i * 6 + 3] = (short)(i * 4);
						this._fallbackIndexData[i * 6 + 4] = (short)(i * 4 + 2);
						this._fallbackIndexData[i * 6 + 5] = (short)(i * 4 + 3);
					}
				}
				this._indexBuffer = new DynamicIndexBuffer(this._graphicsDevice, typeof(short), 12288, BufferUsage.WriteOnly);
				this._indexBuffer.SetData<short>(this._fallbackIndexData);
				this._indexBuffer.ContentLost += delegate(object <p0>, EventArgs <p1>)
				{
					this._indexBuffer.SetData<short>(this._fallbackIndexData);
				};
			}
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x004C4E1B File Offset: 0x004C301B
		private void FlushRenderState()
		{
			this.Allocate();
			this._graphicsDevice.SetVertexBuffer(this._vertexBuffer);
			this._graphicsDevice.Indices = this._indexBuffer;
			this._graphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x004C4E5B File Offset: 0x004C305B
		public void Dispose()
		{
			if (this._vertexBuffer != null)
			{
				this._vertexBuffer.Dispose();
			}
			if (this._indexBuffer != null)
			{
				this._indexBuffer.Dispose();
			}
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x004C4E83 File Offset: 0x004C3083
		public void Begin(RasterizerState rasterizer, Matrix transformation)
		{
			this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, rasterizer, null, transformation);
			this._spriteBatch.End();
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x004C4EA2 File Offset: 0x004C30A2
		public void Begin()
		{
			this._spriteBatch.Begin();
			this._spriteBatch.End();
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x004C4EBC File Offset: 0x004C30BC
		public void Draw(Texture2D texture, Vector2 position, VertexColors colors)
		{
			Vector4 vector = default(Vector4);
			vector.X = position.X;
			vector.Y = position.Y;
			vector.Z = 1f;
			vector.W = 1f;
			this.InternalDraw(texture, ref vector, true, ref TileBatch._nullRectangle, ref colors, ref TileBatch._vector2Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x004C4F20 File Offset: 0x004C3120
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, VertexColors colors, Vector2 origin, float scale, SpriteEffects effects)
		{
			Vector4 vector = default(Vector4);
			vector.X = position.X;
			vector.Y = position.Y;
			vector.Z = scale;
			vector.W = scale;
			this.InternalDraw(texture, ref vector, true, ref sourceRectangle, ref colors, ref origin, effects, 0f);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x004C4F78 File Offset: 0x004C3178
		public void Draw(Texture2D texture, Vector4 destination, VertexColors colors)
		{
			this.InternalDraw(texture, ref destination, false, ref TileBatch._nullRectangle, ref colors, ref TileBatch._vector2Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x004C4FA4 File Offset: 0x004C31A4
		public void Draw(Texture2D texture, Vector2 position, VertexColors colors, Vector2 scale)
		{
			Vector4 vector = default(Vector4);
			vector.X = position.X;
			vector.Y = position.Y;
			vector.Z = scale.X;
			vector.W = scale.Y;
			this.InternalDraw(texture, ref vector, true, ref TileBatch._nullRectangle, ref colors, ref TileBatch._vector2Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x004C500C File Offset: 0x004C320C
		public void Draw(Texture2D texture, Vector4 destination, Rectangle? sourceRectangle, VertexColors colors)
		{
			this.InternalDraw(texture, ref destination, false, ref sourceRectangle, ref colors, ref TileBatch._vector2Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x004C5034 File Offset: 0x004C3234
		public void Draw(Texture2D texture, Vector4 destination, Rectangle? sourceRectangle, VertexColors colors, Vector2 origin, SpriteEffects effects, float rotation)
		{
			this.InternalDraw(texture, ref destination, false, ref sourceRectangle, ref colors, ref origin, effects, rotation);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x004C5058 File Offset: 0x004C3258
		public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, VertexColors colors)
		{
			Vector4 vector = default(Vector4);
			vector.X = (float)destinationRectangle.X;
			vector.Y = (float)destinationRectangle.Y;
			vector.Z = (float)destinationRectangle.Width;
			vector.W = (float)destinationRectangle.Height;
			this.InternalDraw(texture, ref vector, false, ref sourceRectangle, ref colors, ref TileBatch._vector2Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x004C50C0 File Offset: 0x004C32C0
		private static short[] CreateIndexData()
		{
			short[] array = new short[12288];
			for (int i = 0; i < 2048; i++)
			{
				array[i * 6] = (short)(i * 4);
				array[i * 6 + 1] = (short)(i * 4 + 1);
				array[i * 6 + 2] = (short)(i * 4 + 2);
				array[i * 6 + 3] = (short)(i * 4);
				array[i * 6 + 4] = (short)(i * 4 + 2);
				array[i * 6 + 5] = (short)(i * 4 + 3);
			}
			return array;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x004C5134 File Offset: 0x004C3334
		private unsafe void InternalDraw(Texture2D texture, ref Vector4 destination, bool scaleDestination, ref Rectangle? sourceRectangle, ref VertexColors colors, ref Vector2 origin, SpriteEffects effects, float rotation)
		{
			if (this._queuedSpriteCount >= this._spriteDataQueue.Length)
			{
				Array.Resize<TileBatch.SpriteData>(ref this._spriteDataQueue, this._spriteDataQueue.Length << 1);
			}
			fixed (TileBatch.SpriteData* ptr = &this._spriteDataQueue[this._queuedSpriteCount])
			{
				TileBatch.SpriteData* ptr2 = ptr;
				float num = destination.Z;
				float num2 = destination.W;
				if (sourceRectangle != null)
				{
					Rectangle value = sourceRectangle.Value;
					ptr2->Source.X = (float)value.X;
					ptr2->Source.Y = (float)value.Y;
					ptr2->Source.Z = (float)value.Width;
					ptr2->Source.W = (float)value.Height;
					if (scaleDestination)
					{
						num *= (float)value.Width;
						num2 *= (float)value.Height;
					}
				}
				else
				{
					float num3 = (float)texture.Width;
					float num4 = (float)texture.Height;
					ptr2->Source.X = 0f;
					ptr2->Source.Y = 0f;
					ptr2->Source.Z = num3;
					ptr2->Source.W = num4;
					if (scaleDestination)
					{
						num *= num3;
						num2 *= num4;
					}
				}
				ptr2->Destination.X = destination.X;
				ptr2->Destination.Y = destination.Y;
				ptr2->Destination.Z = num;
				ptr2->Destination.W = num2;
				ptr2->Origin.X = origin.X;
				ptr2->Origin.Y = origin.Y;
				ptr2->Effects = effects;
				ptr2->Colors = colors;
				ptr2->Rotation = rotation;
			}
			if (this._spriteTextures == null || this._spriteTextures.Length != this._spriteDataQueue.Length)
			{
				Array.Resize<Texture2D>(ref this._spriteTextures, this._spriteDataQueue.Length);
			}
			Texture2D[] spriteTextures = this._spriteTextures;
			int queuedSpriteCount = this._queuedSpriteCount;
			this._queuedSpriteCount = queuedSpriteCount + 1;
			spriteTextures[queuedSpriteCount] = texture;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x004C5322 File Offset: 0x004C3522
		public void End()
		{
			if (this._queuedSpriteCount == 0)
			{
				return;
			}
			this.FlushRenderState();
			this.Flush();
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x004C533C File Offset: 0x004C353C
		private void Flush()
		{
			Texture2D texture2D = null;
			int num = 0;
			for (int i = 0; i < this._queuedSpriteCount; i++)
			{
				if (this._spriteTextures[i] != texture2D)
				{
					if (i > num)
					{
						this.RenderBatch(texture2D, this._spriteDataQueue, num, i - num);
					}
					num = i;
					texture2D = this._spriteTextures[i];
				}
			}
			this.RenderBatch(texture2D, this._spriteDataQueue, num, this._queuedSpriteCount - num);
			Array.Clear(this._spriteTextures, 0, this._queuedSpriteCount);
			this._queuedSpriteCount = 0;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x004C53B8 File Offset: 0x004C35B8
		private unsafe void RenderBatch(Texture2D texture, TileBatch.SpriteData[] sprites, int offset, int count)
		{
			this._graphicsDevice.Textures[0] = texture;
			float num = 1f / (float)texture.Width;
			float num2 = 1f / (float)texture.Height;
			while (count > 0)
			{
				SetDataOptions options = SetDataOptions.NoOverwrite;
				int num3 = count;
				if (num3 > 2048 - this._vertexBufferPosition)
				{
					num3 = 2048 - this._vertexBufferPosition;
					if (num3 < 256)
					{
						this._vertexBufferPosition = 0;
						options = SetDataOptions.Discard;
						num3 = count;
						if (num3 > 2048)
						{
							num3 = 2048;
						}
					}
				}
				fixed (TileBatch.SpriteData* ptr = &sprites[offset])
				{
					TileBatch.SpriteData* ptr2 = ptr;
					fixed (VertexPositionColorTexture* ptr3 = &this._vertices[0])
					{
						VertexPositionColorTexture* ptr4 = ptr3;
						TileBatch.SpriteData* ptr5 = ptr2;
						VertexPositionColorTexture* ptr6 = ptr4;
						for (int i = 0; i < num3; i++)
						{
							float num4;
							float num5;
							if (ptr5->Rotation != 0f)
							{
								num4 = (float)Math.Cos((double)ptr5->Rotation);
								num5 = (float)Math.Sin((double)ptr5->Rotation);
							}
							else
							{
								num4 = 1f;
								num5 = 0f;
							}
							float num6 = ptr5->Origin.X / ptr5->Source.Z;
							float num7 = ptr5->Origin.Y / ptr5->Source.W;
							ptr6->Color = ptr5->Colors.TopLeftColor;
							ptr6[1].Color = ptr5->Colors.TopRightColor;
							ptr6[2].Color = ptr5->Colors.BottomRightColor;
							ptr6[3].Color = ptr5->Colors.BottomLeftColor;
							for (int j = 0; j < 4; j++)
							{
								float num8 = TileBatch.CORNER_OFFSET_X[j];
								float num9 = TileBatch.CORNER_OFFSET_Y[j];
								float num10 = (num8 - num6) * ptr5->Destination.Z;
								float num11 = (num9 - num7) * ptr5->Destination.W;
								float x = ptr5->Destination.X + num10 * num4 - num11 * num5;
								float y = ptr5->Destination.Y + num10 * num5 + num11 * num4;
								if ((ptr5->Effects & SpriteEffects.FlipVertically) != SpriteEffects.None)
								{
									num9 = 1f - num9;
								}
								if ((ptr5->Effects & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
								{
									num8 = 1f - num8;
								}
								ptr6->Position.X = x;
								ptr6->Position.Y = y;
								ptr6->Position.Z = 0f;
								ptr6->TextureCoordinate.X = (ptr5->Source.X + num8 * ptr5->Source.Z) * num;
								ptr6->TextureCoordinate.Y = (ptr5->Source.Y + num9 * ptr5->Source.W) * num2;
								ptr6++;
							}
							ptr5++;
						}
					}
				}
				int offsetInBytes = this._vertexBufferPosition * sizeof(VertexPositionColorTexture) * 4;
				this._vertexBuffer.SetData<VertexPositionColorTexture>(offsetInBytes, this._vertices, 0, num3 * 4, sizeof(VertexPositionColorTexture), options);
				int minVertexIndex = this._vertexBufferPosition * 4;
				int numVertices = num3 * 4;
				int startIndex = this._vertexBufferPosition * 6;
				int primitiveCount = num3 * 2;
				this._graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, minVertexIndex, numVertices, startIndex, primitiveCount);
				this._vertexBufferPosition += num3;
				offset += num3;
				count -= num3;
			}
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x004C5723 File Offset: 0x004C3923
		// Note: this type is marked as 'beforefieldinit'.
		static TileBatch()
		{
			float[] array = new float[4];
			array[1] = 1f;
			array[2] = 1f;
			TileBatch.CORNER_OFFSET_X = array;
			TileBatch.CORNER_OFFSET_Y = new float[]
			{
				0f,
				0f,
				1f,
				1f
			};
		}

		// Token: 0x040012F7 RID: 4855
		private static readonly float[] CORNER_OFFSET_X;

		// Token: 0x040012F8 RID: 4856
		private static readonly float[] CORNER_OFFSET_Y;

		// Token: 0x040012F9 RID: 4857
		private GraphicsDevice _graphicsDevice;

		// Token: 0x040012FA RID: 4858
		private TileBatch.SpriteData[] _spriteDataQueue = new TileBatch.SpriteData[2048];

		// Token: 0x040012FB RID: 4859
		private Texture2D[] _spriteTextures;

		// Token: 0x040012FC RID: 4860
		private int _queuedSpriteCount;

		// Token: 0x040012FD RID: 4861
		private SpriteBatch _spriteBatch;

		// Token: 0x040012FE RID: 4862
		private static Vector2 _vector2Zero;

		// Token: 0x040012FF RID: 4863
		private static Rectangle? _nullRectangle;

		// Token: 0x04001300 RID: 4864
		private DynamicVertexBuffer _vertexBuffer;

		// Token: 0x04001301 RID: 4865
		private DynamicIndexBuffer _indexBuffer;

		// Token: 0x04001302 RID: 4866
		private short[] _fallbackIndexData;

		// Token: 0x04001303 RID: 4867
		private VertexPositionColorTexture[] _vertices = new VertexPositionColorTexture[8192];

		// Token: 0x04001304 RID: 4868
		private int _vertexBufferPosition;

		// Token: 0x0200058D RID: 1421
		private struct SpriteData
		{
			// Token: 0x040059D7 RID: 22999
			public Vector4 Source;

			// Token: 0x040059D8 RID: 23000
			public Vector4 Destination;

			// Token: 0x040059D9 RID: 23001
			public Vector2 Origin;

			// Token: 0x040059DA RID: 23002
			public SpriteEffects Effects;

			// Token: 0x040059DB RID: 23003
			public VertexColors Colors;

			// Token: 0x040059DC RID: 23004
			public float Rotation;
		}
	}
}
