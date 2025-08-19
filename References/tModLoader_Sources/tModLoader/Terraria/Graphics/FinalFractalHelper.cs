using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x02000438 RID: 1080
	public struct FinalFractalHelper
	{
		// Token: 0x060035A3 RID: 13731 RVA: 0x00577C04 File Offset: 0x00575E04
		public static int GetRandomProfileIndex()
		{
			List<int> list = FinalFractalHelper._fractalProfiles.Keys.ToList<int>();
			int index = Main.rand.Next(list.Count);
			if (list[index] == 4956)
			{
				list.RemoveAt(index);
				index = Main.rand.Next(list.Count);
			}
			return list[index];
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x00577C60 File Offset: 0x00575E60
		public void Draw(Projectile proj)
		{
			FinalFractalHelper.FinalFractalProfile finalFractalProfile = FinalFractalHelper.GetFinalFractalProfile((int)proj.ai[1]);
			MiscShaderData miscShaderData = GameShaders.Misc["FinalFractal"];
			int num = 4;
			int num2 = 0;
			int num3 = 0;
			int num4 = 4;
			miscShaderData.UseShaderSpecificData(new Vector4((float)num, (float)num2, (float)num3, (float)num4));
			miscShaderData.UseImage0("Images/Extra_" + 201.ToString());
			miscShaderData.UseImage1("Images/Extra_" + 193.ToString());
			miscShaderData.Apply(null);
			FinalFractalHelper._vertexStrip.PrepareStrip(proj.oldPos, proj.oldRot, finalFractalProfile.colorMethod, finalFractalProfile.widthMethod, -Main.screenPosition + proj.Size / 2f, new int?(proj.oldPos.Length), true);
			FinalFractalHelper._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x00577D6C File Offset: 0x00575F6C
		public static FinalFractalHelper.FinalFractalProfile GetFinalFractalProfile(int usedSwordId)
		{
			FinalFractalHelper.FinalFractalProfile value;
			if (!FinalFractalHelper._fractalProfiles.TryGetValue(usedSwordId, out value))
			{
				return FinalFractalHelper._defaultProfile;
			}
			return value;
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x00577D90 File Offset: 0x00575F90
		private Color StripColors(float progressOnStrip)
		{
			Color result = Color.Lerp(Color.White, Color.Violet, Utils.GetLerpValue(0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			result.A /= 2;
			return result;
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x00577DEB File Offset: 0x00575FEB
		private float StripWidth(float progressOnStrip)
		{
			return 50f;
		}

		// Token: 0x04004FCD RID: 20429
		public const int TotalIllusions = 4;

		// Token: 0x04004FCE RID: 20430
		public const int FramesPerImportantTrail = 15;

		// Token: 0x04004FCF RID: 20431
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x04004FD0 RID: 20432
		private static Dictionary<int, FinalFractalHelper.FinalFractalProfile> _fractalProfiles = new Dictionary<int, FinalFractalHelper.FinalFractalProfile>
		{
			{
				65,
				new FinalFractalHelper.FinalFractalProfile(48f, new Color(236, 62, 192))
			},
			{
				1123,
				new FinalFractalHelper.FinalFractalProfile(48f, Main.OurFavoriteColor)
			},
			{
				46,
				new FinalFractalHelper.FinalFractalProfile(48f, new Color(122, 66, 191))
			},
			{
				121,
				new FinalFractalHelper.FinalFractalProfile(76f, new Color(254, 158, 35))
			},
			{
				190,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(107, 203, 0))
			},
			{
				368,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(236, 200, 19))
			},
			{
				674,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(236, 200, 19))
			},
			{
				273,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(179, 54, 201))
			},
			{
				675,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(179, 54, 201))
			},
			{
				2880,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(84, 234, 245))
			},
			{
				989,
				new FinalFractalHelper.FinalFractalProfile(48f, new Color(91, 158, 232))
			},
			{
				1826,
				new FinalFractalHelper.FinalFractalProfile(76f, new Color(252, 95, 4))
			},
			{
				3063,
				new FinalFractalHelper.FinalFractalProfile(76f, new Color(254, 194, 250))
			},
			{
				3065,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(237, 63, 133))
			},
			{
				757,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(80, 222, 122))
			},
			{
				155,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(56, 78, 210))
			},
			{
				795,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(237, 28, 36))
			},
			{
				3018,
				new FinalFractalHelper.FinalFractalProfile(80f, new Color(143, 215, 29))
			},
			{
				4144,
				new FinalFractalHelper.FinalFractalProfile(45f, new Color(178, 255, 180))
			},
			{
				3507,
				new FinalFractalHelper.FinalFractalProfile(45f, new Color(235, 166, 135))
			},
			{
				4956,
				new FinalFractalHelper.FinalFractalProfile(86f, new Color(178, 255, 180))
			}
		};

		// Token: 0x04004FD1 RID: 20433
		private static FinalFractalHelper.FinalFractalProfile _defaultProfile = new FinalFractalHelper.FinalFractalProfile(50f, Color.White);

		// Token: 0x02000B71 RID: 2929
		// (Invoke) Token: 0x06005CD2 RID: 23762
		public delegate void SpawnDustMethod(Vector2 centerPosition, float rotation, Vector2 velocity);

		// Token: 0x02000B72 RID: 2930
		public struct FinalFractalProfile
		{
			// Token: 0x06005CD5 RID: 23765 RVA: 0x006C5220 File Offset: 0x006C3420
			public FinalFractalProfile(float fullBladeLength, Color color)
			{
				this.trailWidth = fullBladeLength / 2f;
				this.trailColor = color;
				this.widthMethod = null;
				this.colorMethod = null;
				this.dustMethod = null;
				this.widthMethod = new VertexStrip.StripHalfWidthFunction(this.StripWidth);
				this.colorMethod = new VertexStrip.StripColorFunction(this.StripColors);
				this.dustMethod = new FinalFractalHelper.SpawnDustMethod(this.StripDust);
			}

			// Token: 0x06005CD6 RID: 23766 RVA: 0x006C52AC File Offset: 0x006C34AC
			private void StripDust(Vector2 centerPosition, float rotation, Vector2 velocity)
			{
				if (Main.rand.Next(9) == 0)
				{
					int num = Main.rand.Next(1, 4);
					for (int i = 0; i < num; i++)
					{
						Dust dust = Dust.NewDustPerfect(centerPosition, 278, null, 100, Color.Lerp(this.trailColor, Color.White, Main.rand.NextFloat() * 0.3f), 1f);
						dust.scale = 0.4f;
						dust.fadeIn = 0.4f + Main.rand.NextFloat() * 0.3f;
						dust.noGravity = true;
						dust.velocity += rotation.ToRotationVector2() * (3f + Main.rand.NextFloat() * 4f);
					}
				}
			}

			// Token: 0x06005CD7 RID: 23767 RVA: 0x006C5384 File Offset: 0x006C3584
			private Color StripColors(float progressOnStrip)
			{
				Color result = this.trailColor * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
				result.A /= 2;
				return result;
			}

			// Token: 0x06005CD8 RID: 23768 RVA: 0x006C53C5 File Offset: 0x006C35C5
			private float StripWidth(float progressOnStrip)
			{
				return this.trailWidth;
			}

			// Token: 0x040075ED RID: 30189
			public float trailWidth;

			// Token: 0x040075EE RID: 30190
			public Color trailColor;

			// Token: 0x040075EF RID: 30191
			public FinalFractalHelper.SpawnDustMethod dustMethod;

			// Token: 0x040075F0 RID: 30192
			public VertexStrip.StripColorFunction colorMethod;

			// Token: 0x040075F1 RID: 30193
			public VertexStrip.StripHalfWidthFunction widthMethod;
		}
	}
}
