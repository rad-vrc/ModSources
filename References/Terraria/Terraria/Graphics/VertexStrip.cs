using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x020000F2 RID: 242
	public class VertexStrip
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x004C41B0 File Offset: 0x004C23B0
		public void PrepareStrip(Vector2[] positions, float[] rotations, VertexStrip.StripColorFunction colorFunction, VertexStrip.StripHalfWidthFunction widthFunction, Vector2 offsetForAllPositions = default(Vector2), int? expectedVertexPairsAmount = null, bool includeBacksides = false)
		{
			int num = positions.Length;
			int num2 = num * 2;
			this._vertexAmountCurrentlyMaintained = num2;
			if (this._vertices.Length < num2)
			{
				Array.Resize<VertexStrip.CustomVertexInfo>(ref this._vertices, num2);
			}
			int num3 = num;
			if (expectedVertexPairsAmount != null)
			{
				num3 = expectedVertexPairsAmount.Value;
			}
			for (int i = 0; i < num; i++)
			{
				if (positions[i] == Vector2.Zero)
				{
					num = i - 1;
					this._vertexAmountCurrentlyMaintained = num * 2;
					break;
				}
				Vector2 pos = positions[i] + offsetForAllPositions;
				float rot = MathHelper.WrapAngle(rotations[i]);
				int indexOnVertexArray = i * 2;
				float progressOnStrip = (float)i / (float)(num3 - 1);
				this.AddVertex(colorFunction, widthFunction, pos, rot, indexOnVertexArray, progressOnStrip);
			}
			this.PrepareIndices(num, includeBacksides);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x004C4268 File Offset: 0x004C2468
		public void PrepareStripWithProceduralPadding(Vector2[] positions, float[] rotations, VertexStrip.StripColorFunction colorFunction, VertexStrip.StripHalfWidthFunction widthFunction, Vector2 offsetForAllPositions = default(Vector2), bool includeBacksides = false, bool tryStoppingOddBug = true)
		{
			int num = positions.Length;
			this._temporaryPositionsCache.Clear();
			this._temporaryRotationsCache.Clear();
			int num2 = 0;
			while (num2 < num && !(positions[num2] == Vector2.Zero))
			{
				Vector2 vector = positions[num2];
				float num3 = MathHelper.WrapAngle(rotations[num2]);
				this._temporaryPositionsCache.Add(vector);
				this._temporaryRotationsCache.Add(num3);
				if (num2 + 1 < num && positions[num2 + 1] != Vector2.Zero)
				{
					Vector2 vector2 = positions[num2 + 1];
					float num4 = MathHelper.WrapAngle(rotations[num2 + 1]);
					int num5 = (int)(Math.Abs(MathHelper.WrapAngle(num4 - num3)) / 0.2617994f);
					if (num5 != 0)
					{
						float num6 = vector.Distance(vector2);
						Vector2 value = vector + num3.ToRotationVector2() * num6;
						Vector2 value2 = vector2 + num4.ToRotationVector2() * -num6;
						int num7 = num5 + 2;
						float num8 = 1f / (float)num7;
						Vector2 target = vector;
						for (float num9 = num8; num9 < 1f; num9 += num8)
						{
							Vector2 vector3 = Vector2.CatmullRom(value, vector, vector2, value2, num9);
							float item = MathHelper.WrapAngle(vector3.DirectionTo(target).ToRotation());
							this._temporaryPositionsCache.Add(vector3);
							this._temporaryRotationsCache.Add(item);
							target = vector3;
						}
					}
				}
				num2++;
			}
			int count = this._temporaryPositionsCache.Count;
			Vector2 zero = Vector2.Zero;
			int num10 = 0;
			while (num10 < count && (!tryStoppingOddBug || !(this._temporaryPositionsCache[num10] == zero)))
			{
				Vector2 pos = this._temporaryPositionsCache[num10] + offsetForAllPositions;
				float rot = this._temporaryRotationsCache[num10];
				int indexOnVertexArray = num10 * 2;
				float progressOnStrip = (float)num10 / (float)(count - 1);
				this.AddVertex(colorFunction, widthFunction, pos, rot, indexOnVertexArray, progressOnStrip);
				num10++;
			}
			this._vertexAmountCurrentlyMaintained = count * 2;
			this.PrepareIndices(count, includeBacksides);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x004C4478 File Offset: 0x004C2678
		private void PrepareIndices(int vertexPaidsAdded, bool includeBacksides)
		{
			int num = vertexPaidsAdded - 1;
			int num2 = 6 + includeBacksides.ToInt() * 6;
			int num3 = num * num2;
			this._indicesAmountCurrentlyMaintained = num3;
			if (this._indices.Length < num3)
			{
				Array.Resize<short>(ref this._indices, num3);
			}
			short num4 = 0;
			while ((int)num4 < num)
			{
				short num5 = (short)((int)num4 * num2);
				int num6 = (int)(num4 * 2);
				this._indices[(int)num5] = (short)num6;
				this._indices[(int)(num5 + 1)] = (short)(num6 + 1);
				this._indices[(int)(num5 + 2)] = (short)(num6 + 2);
				this._indices[(int)(num5 + 3)] = (short)(num6 + 2);
				this._indices[(int)(num5 + 4)] = (short)(num6 + 1);
				this._indices[(int)(num5 + 5)] = (short)(num6 + 3);
				if (includeBacksides)
				{
					this._indices[(int)(num5 + 6)] = (short)(num6 + 2);
					this._indices[(int)(num5 + 7)] = (short)(num6 + 1);
					this._indices[(int)(num5 + 8)] = (short)num6;
					this._indices[(int)(num5 + 9)] = (short)(num6 + 2);
					this._indices[(int)(num5 + 10)] = (short)(num6 + 3);
					this._indices[(int)(num5 + 11)] = (short)(num6 + 1);
				}
				num4 += 1;
			}
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x004C4594 File Offset: 0x004C2794
		private void AddVertex(VertexStrip.StripColorFunction colorFunction, VertexStrip.StripHalfWidthFunction widthFunction, Vector2 pos, float rot, int indexOnVertexArray, float progressOnStrip)
		{
			while (indexOnVertexArray + 1 >= this._vertices.Length)
			{
				Array.Resize<VertexStrip.CustomVertexInfo>(ref this._vertices, this._vertices.Length * 2);
			}
			Color color = colorFunction(progressOnStrip);
			float scaleFactor = widthFunction(progressOnStrip);
			Vector2 value = MathHelper.WrapAngle(rot - 1.5707964f).ToRotationVector2() * scaleFactor;
			this._vertices[indexOnVertexArray].Position = pos + value;
			this._vertices[indexOnVertexArray + 1].Position = pos - value;
			this._vertices[indexOnVertexArray].TexCoord = new Vector2(progressOnStrip, 1f);
			this._vertices[indexOnVertexArray + 1].TexCoord = new Vector2(progressOnStrip, 0f);
			this._vertices[indexOnVertexArray].Color = color;
			this._vertices[indexOnVertexArray + 1].Color = color;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x004C468C File Offset: 0x004C288C
		public void DrawTrail()
		{
			if (this._vertexAmountCurrentlyMaintained < 3)
			{
				return;
			}
			Main.instance.GraphicsDevice.DrawUserIndexedPrimitives<VertexStrip.CustomVertexInfo>(PrimitiveType.TriangleList, this._vertices, 0, this._vertexAmountCurrentlyMaintained, this._indices, 0, this._indicesAmountCurrentlyMaintained / 3);
		}

		// Token: 0x040012E5 RID: 4837
		private VertexStrip.CustomVertexInfo[] _vertices = new VertexStrip.CustomVertexInfo[1];

		// Token: 0x040012E6 RID: 4838
		private int _vertexAmountCurrentlyMaintained;

		// Token: 0x040012E7 RID: 4839
		private short[] _indices = new short[1];

		// Token: 0x040012E8 RID: 4840
		private int _indicesAmountCurrentlyMaintained;

		// Token: 0x040012E9 RID: 4841
		private List<Vector2> _temporaryPositionsCache = new List<Vector2>();

		// Token: 0x040012EA RID: 4842
		private List<float> _temporaryRotationsCache = new List<float>();

		// Token: 0x0200058A RID: 1418
		// (Invoke) Token: 0x06003210 RID: 12816
		public delegate Color StripColorFunction(float progressOnStrip);

		// Token: 0x0200058B RID: 1419
		// (Invoke) Token: 0x06003214 RID: 12820
		public delegate float StripHalfWidthFunction(float progressOnStrip);

		// Token: 0x0200058C RID: 1420
		private struct CustomVertexInfo : IVertexType
		{
			// Token: 0x06003217 RID: 12823 RVA: 0x005E9AB1 File Offset: 0x005E7CB1
			public CustomVertexInfo(Vector2 position, Color color, Vector2 texCoord)
			{
				this.Position = position;
				this.Color = color;
				this.TexCoord = texCoord;
			}

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x06003218 RID: 12824 RVA: 0x005E9AC8 File Offset: 0x005E7CC8
			public VertexDeclaration VertexDeclaration
			{
				get
				{
					return VertexStrip.CustomVertexInfo._vertexDeclaration;
				}
			}

			// Token: 0x040059D3 RID: 22995
			public Vector2 Position;

			// Token: 0x040059D4 RID: 22996
			public Color Color;

			// Token: 0x040059D5 RID: 22997
			public Vector2 TexCoord;

			// Token: 0x040059D6 RID: 22998
			private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
				new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
				new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
			});
		}
	}
}
