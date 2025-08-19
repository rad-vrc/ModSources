using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x02000441 RID: 1089
	public class VertexStrip
	{
		// Token: 0x060035E5 RID: 13797 RVA: 0x00579998 File Offset: 0x00577B98
		public void PrepareStrip(Vector2[] positions, float[] rotations, VertexStrip.StripColorFunction colorFunction, VertexStrip.StripHalfWidthFunction widthFunction, Vector2 offsetForAllPositions = default(Vector2), int? expectedVertexPairsAmount = null, bool includeBacksides = false)
		{
			int num = positions.Length;
			int num2 = this._vertexAmountCurrentlyMaintained = num * 2;
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

		// Token: 0x060035E6 RID: 13798 RVA: 0x00579A5C File Offset: 0x00577C5C
		public void PrepareStripWithProceduralPadding(Vector2[] positions, float[] rotations, VertexStrip.StripColorFunction colorFunction, VertexStrip.StripHalfWidthFunction widthFunction, Vector2 offsetForAllPositions = default(Vector2), bool includeBacksides = false, bool tryStoppingOddBug = true)
		{
			int num = positions.Length;
			this._temporaryPositionsCache.Clear();
			this._temporaryRotationsCache.Clear();
			int i = 0;
			while (i < num && !(positions[i] == Vector2.Zero))
			{
				Vector2 vector = positions[i];
				float num2 = MathHelper.WrapAngle(rotations[i]);
				this._temporaryPositionsCache.Add(vector);
				this._temporaryRotationsCache.Add(num2);
				if (i + 1 < num && positions[i + 1] != Vector2.Zero)
				{
					Vector2 vector2 = positions[i + 1];
					float num3 = MathHelper.WrapAngle(rotations[i + 1]);
					int num4 = (int)(Math.Abs(MathHelper.WrapAngle(num3 - num2)) / 0.2617994f);
					if (num4 != 0)
					{
						float num5 = vector.Distance(vector2);
						Vector2 value = vector + num2.ToRotationVector2() * num5;
						Vector2 value2 = vector2 + num3.ToRotationVector2() * (0f - num5);
						int num6 = num4 + 2;
						float num7 = 1f / (float)num6;
						Vector2 target = vector;
						for (float num8 = num7; num8 < 1f; num8 += num7)
						{
							Vector2 vector3 = Vector2.CatmullRom(value, vector, vector2, value2, num8);
							float item = MathHelper.WrapAngle(vector3.DirectionTo(target).ToRotation());
							this._temporaryPositionsCache.Add(vector3);
							this._temporaryRotationsCache.Add(item);
							target = vector3;
						}
					}
				}
				i++;
			}
			int count = this._temporaryPositionsCache.Count;
			Vector2 zero = Vector2.Zero;
			int j = 0;
			while (j < count && (!tryStoppingOddBug || !(this._temporaryPositionsCache[j] == zero)))
			{
				Vector2 pos = this._temporaryPositionsCache[j] + offsetForAllPositions;
				float rot = this._temporaryRotationsCache[j];
				int indexOnVertexArray = j * 2;
				float progressOnStrip = (float)j / (float)(count - 1);
				this.AddVertex(colorFunction, widthFunction, pos, rot, indexOnVertexArray, progressOnStrip);
				j++;
			}
			this._vertexAmountCurrentlyMaintained = count * 2;
			this.PrepareIndices(count, includeBacksides);
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x00579C70 File Offset: 0x00577E70
		private void PrepareIndices(int vertexPaidsAdded, bool includeBacksides)
		{
			int num = vertexPaidsAdded - 1;
			int num2 = 6 + includeBacksides.ToInt() * 6;
			int num3 = this._indicesAmountCurrentlyMaintained = num * num2;
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

		// Token: 0x060035E8 RID: 13800 RVA: 0x00579D94 File Offset: 0x00577F94
		private void AddVertex(VertexStrip.StripColorFunction colorFunction, VertexStrip.StripHalfWidthFunction widthFunction, Vector2 pos, float rot, int indexOnVertexArray, float progressOnStrip)
		{
			while (indexOnVertexArray + 1 >= this._vertices.Length)
			{
				Array.Resize<VertexStrip.CustomVertexInfo>(ref this._vertices, this._vertices.Length * 2);
			}
			Color color = colorFunction(progressOnStrip);
			float num = widthFunction(progressOnStrip);
			Vector2 vector = MathHelper.WrapAngle(rot - 1.5707964f).ToRotationVector2() * num;
			this._vertices[indexOnVertexArray].Position = pos + vector;
			this._vertices[indexOnVertexArray + 1].Position = pos - vector;
			this._vertices[indexOnVertexArray].TexCoord = new Vector2(progressOnStrip, 1f);
			this._vertices[indexOnVertexArray + 1].TexCoord = new Vector2(progressOnStrip, 0f);
			this._vertices[indexOnVertexArray].Color = color;
			this._vertices[indexOnVertexArray + 1].Color = color;
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x00579E8C File Offset: 0x0057808C
		public void DrawTrail()
		{
			if (this._vertexAmountCurrentlyMaintained >= 3)
			{
				Main.instance.GraphicsDevice.DrawUserIndexedPrimitives<VertexStrip.CustomVertexInfo>(0, this._vertices, 0, this._vertexAmountCurrentlyMaintained, this._indices, 0, this._indicesAmountCurrentlyMaintained / 3);
			}
		}

		// Token: 0x04004FF2 RID: 20466
		private VertexStrip.CustomVertexInfo[] _vertices = new VertexStrip.CustomVertexInfo[1];

		// Token: 0x04004FF3 RID: 20467
		private int _vertexAmountCurrentlyMaintained;

		// Token: 0x04004FF4 RID: 20468
		private short[] _indices = new short[1];

		// Token: 0x04004FF5 RID: 20469
		private int _indicesAmountCurrentlyMaintained;

		// Token: 0x04004FF6 RID: 20470
		private List<Vector2> _temporaryPositionsCache = new List<Vector2>();

		// Token: 0x04004FF7 RID: 20471
		private List<float> _temporaryRotationsCache = new List<float>();

		// Token: 0x02000B76 RID: 2934
		// (Invoke) Token: 0x06005CE4 RID: 23780
		public delegate Color StripColorFunction(float progressOnStrip);

		// Token: 0x02000B77 RID: 2935
		// (Invoke) Token: 0x06005CE8 RID: 23784
		public delegate float StripHalfWidthFunction(float progressOnStrip);

		// Token: 0x02000B78 RID: 2936
		private struct CustomVertexInfo : IVertexType
		{
			// Token: 0x17000946 RID: 2374
			// (get) Token: 0x06005CEB RID: 23787 RVA: 0x006C54C6 File Offset: 0x006C36C6
			public VertexDeclaration VertexDeclaration
			{
				get
				{
					return VertexStrip.CustomVertexInfo._vertexDeclaration;
				}
			}

			// Token: 0x06005CEC RID: 23788 RVA: 0x006C54CD File Offset: 0x006C36CD
			public CustomVertexInfo(Vector2 position, Color color, Vector2 texCoord)
			{
				this.Position = position;
				this.Color = color;
				this.TexCoord = texCoord;
			}

			// Token: 0x040075FB RID: 30203
			public Vector2 Position;

			// Token: 0x040075FC RID: 30204
			public Color Color;

			// Token: 0x040075FD RID: 30205
			public Vector2 TexCoord;

			// Token: 0x040075FE RID: 30206
			private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, 1, 0, 0),
				new VertexElement(8, 4, 1, 0),
				new VertexElement(12, 1, 2, 0)
			});
		}
	}
}
