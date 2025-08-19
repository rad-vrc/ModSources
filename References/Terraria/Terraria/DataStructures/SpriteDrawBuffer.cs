using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000442 RID: 1090
	public class SpriteDrawBuffer
	{
		// Token: 0x06002BB5 RID: 11189 RVA: 0x0059ECB4 File Offset: 0x0059CEB4
		public SpriteDrawBuffer(GraphicsDevice graphicsDevice, int defaultSize)
		{
			this.graphicsDevice = graphicsDevice;
			this.maxSprites = defaultSize;
			this.CreateBuffers();
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x0059ECE8 File Offset: 0x0059CEE8
		public void CheckGraphicsDevice(GraphicsDevice graphicsDevice)
		{
			if (this.graphicsDevice != graphicsDevice)
			{
				this.graphicsDevice = graphicsDevice;
				this.CreateBuffers();
			}
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x0059ED00 File Offset: 0x0059CF00
		private void CreateBuffers()
		{
			if (this.vertexBuffer != null)
			{
				this.vertexBuffer.Dispose();
			}
			this.vertexBuffer = new DynamicVertexBuffer(this.graphicsDevice, typeof(VertexPositionColorTexture), this.maxSprites * 4, BufferUsage.WriteOnly);
			if (this.indexBuffer != null)
			{
				this.indexBuffer.Dispose();
			}
			this.indexBuffer = new IndexBuffer(this.graphicsDevice, typeof(ushort), this.maxSprites * 6, BufferUsage.WriteOnly);
			this.indexBuffer.SetData<ushort>(SpriteDrawBuffer.GenIndexBuffer(this.maxSprites));
			Array.Resize<VertexPositionColorTexture>(ref this.vertices, this.maxSprites * 6);
			Array.Resize<Texture>(ref this.textures, this.maxSprites);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x0059EDB8 File Offset: 0x0059CFB8
		private static ushort[] GenIndexBuffer(int maxSprites)
		{
			ushort[] array = new ushort[maxSprites * 6];
			int i = 0;
			ushort num = 0;
			while (i < maxSprites)
			{
				array[i++] = num;
				array[i++] = num + 1;
				array[i++] = num + 2;
				array[i++] = num + 3;
				array[i++] = num + 2;
				array[i++] = num + 1;
				num += 4;
			}
			return array;
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x0059EE1D File Offset: 0x0059D01D
		public void UploadAndBind()
		{
			if (this.vertexCount > 0)
			{
				this.vertexBuffer.SetData<VertexPositionColorTexture>(this.vertices, 0, this.vertexCount, SetDataOptions.Discard);
			}
			this.vertexCount = 0;
			this.Bind();
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x0059EE50 File Offset: 0x0059D050
		public void Bind()
		{
			this.preBindVertexBuffers = this.graphicsDevice.GetVertexBuffers();
			this.preBindIndexBuffer = this.graphicsDevice.Indices;
			this.graphicsDevice.SetVertexBuffer(this.vertexBuffer);
			this.graphicsDevice.Indices = this.indexBuffer;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x0059EEA1 File Offset: 0x0059D0A1
		public void Unbind()
		{
			this.graphicsDevice.SetVertexBuffers(this.preBindVertexBuffers);
			this.graphicsDevice.Indices = this.preBindIndexBuffer;
			this.preBindVertexBuffers = null;
			this.preBindIndexBuffer = null;
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x0059EED3 File Offset: 0x0059D0D3
		public void DrawRange(int index, int count)
		{
			this.graphicsDevice.Textures[0] = this.textures[index];
			this.graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, index * 4, 0, count * 4, 0, count * 2);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x0059EF05 File Offset: 0x0059D105
		public void DrawSingle(int index)
		{
			this.DrawRange(index, 1);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x0059EF10 File Offset: 0x0059D110
		public void Draw(Texture2D texture, Vector2 position, VertexColors colors)
		{
			this.Draw(texture, position, null, colors, 0f, Vector2.Zero, 1f, SpriteEffects.None);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x0059EF40 File Offset: 0x0059D140
		public void Draw(Texture2D texture, Rectangle destination, VertexColors colors)
		{
			this.Draw(texture, destination, null, colors);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x0059EF5F File Offset: 0x0059D15F
		public void Draw(Texture2D texture, Rectangle destination, Rectangle? sourceRectangle, VertexColors colors)
		{
			this.Draw(texture, destination, sourceRectangle, colors, 0f, Vector2.Zero, SpriteEffects.None);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x0059EF78 File Offset: 0x0059D178
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, VertexColors color, float rotation, Vector2 origin, float scale, SpriteEffects effects)
		{
			this.Draw(texture, position, sourceRectangle, color, rotation, origin, new Vector2(scale, scale), effects);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x0059EFA0 File Offset: 0x0059D1A0
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, VertexColors colors, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects)
		{
			float z;
			float w;
			if (sourceRectangle != null)
			{
				z = (float)sourceRectangle.Value.Width * scale.X;
				w = (float)sourceRectangle.Value.Height * scale.Y;
			}
			else
			{
				z = (float)texture.Width * scale.X;
				w = (float)texture.Height * scale.Y;
			}
			this.Draw(texture, new Vector4(position.X, position.Y, z, w), sourceRectangle, colors, rotation, origin, effects, 0f);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x0059F02C File Offset: 0x0059D22C
		public void Draw(Texture2D texture, Rectangle destination, Rectangle? sourceRectangle, VertexColors colors, float rotation, Vector2 origin, SpriteEffects effects)
		{
			this.Draw(texture, new Vector4((float)destination.X, (float)destination.Y, (float)destination.Width, (float)destination.Height), sourceRectangle, colors, rotation, origin, effects, 0f);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x0059F070 File Offset: 0x0059D270
		public void Draw(Texture2D texture, Vector4 destinationRectangle, Rectangle? sourceRectangle, VertexColors colors, float rotation, Vector2 origin, SpriteEffects effect, float depth)
		{
			Vector4 vector;
			if (sourceRectangle != null)
			{
				vector.X = (float)sourceRectangle.Value.X;
				vector.Y = (float)sourceRectangle.Value.Y;
				vector.Z = (float)sourceRectangle.Value.Width;
				vector.W = (float)sourceRectangle.Value.Height;
			}
			else
			{
				vector.X = 0f;
				vector.Y = 0f;
				vector.Z = (float)texture.Width;
				vector.W = (float)texture.Height;
			}
			Vector2 vector2;
			vector2.X = vector.X / (float)texture.Width;
			vector2.Y = vector.Y / (float)texture.Height;
			Vector2 vector3;
			vector3.X = (vector.X + vector.Z) / (float)texture.Width;
			vector3.Y = (vector.Y + vector.W) / (float)texture.Height;
			if ((effect & SpriteEffects.FlipVertically) != SpriteEffects.None)
			{
				float y = vector3.Y;
				vector3.Y = vector2.Y;
				vector2.Y = y;
			}
			if ((effect & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
			{
				float x = vector3.X;
				vector3.X = vector2.X;
				vector2.X = x;
			}
			this.QueueSprite(destinationRectangle, -origin, colors, vector, vector2, vector3, texture, depth, rotation);
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x0059F1CC File Offset: 0x0059D3CC
		private void QueueSprite(Vector4 destinationRect, Vector2 origin, VertexColors colors, Vector4 sourceRectangle, Vector2 texCoordTL, Vector2 texCoordBR, Texture2D texture, float depth, float rotation)
		{
			float num = origin.X / sourceRectangle.Z;
			float num2 = origin.Y / sourceRectangle.W;
			float x = destinationRect.X;
			float y = destinationRect.Y;
			float z = destinationRect.Z;
			float w = destinationRect.W;
			float num3 = num * z;
			float num4 = num2 * w;
			float num5;
			float num6;
			if (rotation != 0f)
			{
				num5 = (float)Math.Cos((double)rotation);
				num6 = (float)Math.Sin((double)rotation);
			}
			else
			{
				num5 = 1f;
				num6 = 0f;
			}
			if (this.vertexCount + 4 >= this.maxSprites * 4)
			{
				this.maxSprites *= 2;
				this.CreateBuffers();
			}
			this.textures[this.vertexCount / 4] = texture;
			this.PushVertex(new Vector3(x + num3 * num5 - num4 * num6, y + num3 * num6 + num4 * num5, depth), colors.TopLeftColor, texCoordTL);
			this.PushVertex(new Vector3(x + (num3 + z) * num5 - num4 * num6, y + (num3 + z) * num6 + num4 * num5, depth), colors.TopRightColor, new Vector2(texCoordBR.X, texCoordTL.Y));
			this.PushVertex(new Vector3(x + num3 * num5 - (num4 + w) * num6, y + num3 * num6 + (num4 + w) * num5, depth), colors.BottomLeftColor, new Vector2(texCoordTL.X, texCoordBR.Y));
			this.PushVertex(new Vector3(x + (num3 + z) * num5 - (num4 + w) * num6, y + (num3 + z) * num6 + (num4 + w) * num5, depth), colors.BottomRightColor, texCoordBR);
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x0059F378 File Offset: 0x0059D578
		private void PushVertex(Vector3 pos, Color color, Vector2 texCoord)
		{
			VertexPositionColorTexture[] array = this.vertices;
			int num = this.vertexCount;
			this.vertexCount = num + 1;
			SpriteDrawBuffer.SetVertex(ref array[num], pos, color, texCoord);
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x0059F3A9 File Offset: 0x0059D5A9
		private static void SetVertex(ref VertexPositionColorTexture vertex, Vector3 pos, Color color, Vector2 texCoord)
		{
			vertex.Position = pos;
			vertex.Color = color;
			vertex.TextureCoordinate = texCoord;
		}

		// Token: 0x04004FDF RID: 20447
		private GraphicsDevice graphicsDevice;

		// Token: 0x04004FE0 RID: 20448
		private DynamicVertexBuffer vertexBuffer;

		// Token: 0x04004FE1 RID: 20449
		private IndexBuffer indexBuffer;

		// Token: 0x04004FE2 RID: 20450
		private VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[0];

		// Token: 0x04004FE3 RID: 20451
		private Texture[] textures = new Texture[0];

		// Token: 0x04004FE4 RID: 20452
		private int maxSprites;

		// Token: 0x04004FE5 RID: 20453
		private int vertexCount;

		// Token: 0x04004FE6 RID: 20454
		private VertexBufferBinding[] preBindVertexBuffers;

		// Token: 0x04004FE7 RID: 20455
		private IndexBuffer preBindIndexBuffer;
	}
}
