using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000732 RID: 1842
	public class SpriteDrawBuffer
	{
		// Token: 0x06004ACB RID: 19147 RVA: 0x00668491 File Offset: 0x00666691
		public SpriteDrawBuffer(GraphicsDevice graphicsDevice, int defaultSize)
		{
			this.graphicsDevice = graphicsDevice;
			this.maxSprites = defaultSize;
			this.CreateBuffers();
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x006684C5 File Offset: 0x006666C5
		public void CheckGraphicsDevice(GraphicsDevice graphicsDevice)
		{
			if (this.graphicsDevice != graphicsDevice)
			{
				this.graphicsDevice = graphicsDevice;
				this.CreateBuffers();
			}
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x006684E0 File Offset: 0x006666E0
		private void CreateBuffers()
		{
			if (this.vertexBuffer != null)
			{
				this.vertexBuffer.Dispose();
			}
			this.vertexBuffer = new DynamicVertexBuffer(this.graphicsDevice, typeof(VertexPositionColorTexture), this.maxSprites * 4, 1);
			if (this.indexBuffer != null)
			{
				this.indexBuffer.Dispose();
			}
			this.indexBuffer = new IndexBuffer(this.graphicsDevice, typeof(ushort), this.maxSprites * 6, 1);
			this.indexBuffer.SetData<ushort>(SpriteDrawBuffer.GenIndexBuffer(this.maxSprites));
			Array.Resize<VertexPositionColorTexture>(ref this.vertices, this.maxSprites * 6);
			Array.Resize<Texture>(ref this.textures, this.maxSprites);
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x00668598 File Offset: 0x00666798
		private static ushort[] GenIndexBuffer(int maxSprites)
		{
			ushort[] array = new ushort[maxSprites * 6];
			int num = 0;
			ushort num2 = 0;
			while (num < maxSprites)
			{
				array[num++] = num2;
				array[num++] = num2 + 1;
				array[num++] = num2 + 2;
				array[num++] = num2 + 3;
				array[num++] = num2 + 2;
				array[num++] = num2 + 1;
				num2 += 4;
			}
			return array;
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x006685FD File Offset: 0x006667FD
		public void UploadAndBind()
		{
			if (this.vertexCount > 0)
			{
				this.vertexBuffer.SetData<VertexPositionColorTexture>(this.vertices, 0, this.vertexCount, 1);
			}
			this.vertexCount = 0;
			this.Bind();
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x00668630 File Offset: 0x00666830
		public void Bind()
		{
			this.preBindVertexBuffers = this.graphicsDevice.GetVertexBuffers();
			this.preBindIndexBuffer = this.graphicsDevice.Indices;
			this.graphicsDevice.SetVertexBuffer(this.vertexBuffer);
			this.graphicsDevice.Indices = this.indexBuffer;
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x00668681 File Offset: 0x00666881
		public void Unbind()
		{
			this.graphicsDevice.SetVertexBuffers(this.preBindVertexBuffers);
			this.graphicsDevice.Indices = this.preBindIndexBuffer;
			this.preBindVertexBuffers = null;
			this.preBindIndexBuffer = null;
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x006686B3 File Offset: 0x006668B3
		public void DrawRange(int index, int count)
		{
			this.graphicsDevice.Textures[0] = this.textures[index];
			this.graphicsDevice.DrawIndexedPrimitives(0, index * 4, 0, count * 4, 0, count * 2);
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x006686E5 File Offset: 0x006668E5
		public void DrawSingle(int index)
		{
			this.DrawRange(index, 1);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x006686F0 File Offset: 0x006668F0
		public void Draw(Texture2D texture, Vector2 position, VertexColors colors)
		{
			this.Draw(texture, position, null, colors, 0f, Vector2.Zero, 1f, 0);
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x00668720 File Offset: 0x00666920
		public void Draw(Texture2D texture, Rectangle destination, VertexColors colors)
		{
			this.Draw(texture, destination, null, colors);
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x0066873F File Offset: 0x0066693F
		public void Draw(Texture2D texture, Rectangle destination, Rectangle? sourceRectangle, VertexColors colors)
		{
			this.Draw(texture, destination, sourceRectangle, colors, 0f, Vector2.Zero, 0);
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x00668758 File Offset: 0x00666958
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, VertexColors color, float rotation, Vector2 origin, float scale, SpriteEffects effects)
		{
			this.Draw(texture, position, sourceRectangle, color, rotation, origin, new Vector2(scale, scale), effects);
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x00668780 File Offset: 0x00666980
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

		// Token: 0x06004AD9 RID: 19161 RVA: 0x0066880C File Offset: 0x00666A0C
		public void Draw(Texture2D texture, Rectangle destination, Rectangle? sourceRectangle, VertexColors colors, float rotation, Vector2 origin, SpriteEffects effects)
		{
			this.Draw(texture, new Vector4((float)destination.X, (float)destination.Y, (float)destination.Width, (float)destination.Height), sourceRectangle, colors, rotation, origin, effects, 0f);
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00668850 File Offset: 0x00666A50
		public void Draw(Texture2D texture, Vector4 destinationRectangle, Rectangle? sourceRectangle, VertexColors colors, float rotation, Vector2 origin, SpriteEffects effect, float depth)
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

		// Token: 0x06004ADB RID: 19163 RVA: 0x006689C4 File Offset: 0x00666BC4
		private void QueueSprite(Vector4 destinationRect, Vector2 origin, VertexColors colors, Vector4 sourceRectangle, Vector2 texCoordTL, Vector2 texCoordBR, Texture2D texture, float depth, float rotation)
		{
			float num = origin.X / sourceRectangle.Z;
			float num6 = origin.Y / sourceRectangle.W;
			float x = destinationRect.X;
			float y = destinationRect.Y;
			float z = destinationRect.Z;
			float w = destinationRect.W;
			float num2 = num * z;
			float num3 = num6 * w;
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
			if (this.vertexCount + 4 >= this.maxSprites * 4)
			{
				this.maxSprites *= 2;
				this.CreateBuffers();
			}
			this.textures[this.vertexCount / 4] = texture;
			this.PushVertex(new Vector3(x + num2 * num4 - num3 * num5, y + num2 * num5 + num3 * num4, depth), colors.TopLeftColor, texCoordTL);
			this.PushVertex(new Vector3(x + (num2 + z) * num4 - num3 * num5, y + (num2 + z) * num5 + num3 * num4, depth), colors.TopRightColor, new Vector2(texCoordBR.X, texCoordTL.Y));
			this.PushVertex(new Vector3(x + num2 * num4 - (num3 + w) * num5, y + num2 * num5 + (num3 + w) * num4, depth), colors.BottomLeftColor, new Vector2(texCoordTL.X, texCoordBR.Y));
			this.PushVertex(new Vector3(x + (num2 + z) * num4 - (num3 + w) * num5, y + (num2 + z) * num5 + (num3 + w) * num4, depth), colors.BottomRightColor, texCoordBR);
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x00668B70 File Offset: 0x00666D70
		private void PushVertex(Vector3 pos, Color color, Vector2 texCoord)
		{
			VertexPositionColorTexture[] array = this.vertices;
			int num = this.vertexCount;
			this.vertexCount = num + 1;
			SpriteDrawBuffer.SetVertex(ref array[num], pos, color, texCoord);
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x00668BA1 File Offset: 0x00666DA1
		private static void SetVertex(ref VertexPositionColorTexture vertex, Vector3 pos, Color color, Vector2 texCoord)
		{
			vertex.Position = pos;
			vertex.Color = color;
			vertex.TextureCoordinate = texCoord;
		}

		// Token: 0x04006007 RID: 24583
		private GraphicsDevice graphicsDevice;

		// Token: 0x04006008 RID: 24584
		private DynamicVertexBuffer vertexBuffer;

		// Token: 0x04006009 RID: 24585
		private IndexBuffer indexBuffer;

		// Token: 0x0400600A RID: 24586
		private VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[0];

		// Token: 0x0400600B RID: 24587
		private Texture[] textures = new Texture[0];

		// Token: 0x0400600C RID: 24588
		private int maxSprites;

		// Token: 0x0400600D RID: 24589
		private int vertexCount;

		// Token: 0x0400600E RID: 24590
		private VertexBufferBinding[] preBindVertexBuffers;

		// Token: 0x0400600F RID: 24591
		private IndexBuffer preBindIndexBuffer;
	}
}
