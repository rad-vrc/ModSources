using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020000EC RID: 236
	public struct FinalFractalHelper
	{
		// Token: 0x060015B3 RID: 5555 RVA: 0x004C3504 File Offset: 0x004C1704
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

		// Token: 0x060015B4 RID: 5556 RVA: 0x004C3560 File Offset: 0x004C1760
		public void Draw(Projectile proj)
		{
			FinalFractalHelper.FinalFractalProfile finalFractalProfile = FinalFractalHelper.GetFinalFractalProfile((int)proj.ai[1]);
			MiscShaderData miscShaderData = GameShaders.Misc["FinalFractal"];
			int num = 4;
			int num2 = 0;
			int num3 = 0;
			int num4 = 4;
			miscShaderData.UseShaderSpecificData(new Vector4((float)num, (float)num2, (float)num3, (float)num4));
			miscShaderData.UseImage0("Images/Extra_" + 201);
			miscShaderData.UseImage1("Images/Extra_" + 193);
			miscShaderData.Apply(null);
			FinalFractalHelper._vertexStrip.PrepareStrip(proj.oldPos, proj.oldRot, finalFractalProfile.colorMethod, finalFractalProfile.widthMethod, -Main.screenPosition + proj.Size / 2f, new int?(proj.oldPos.Length), true);
			FinalFractalHelper._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x004C3664 File Offset: 0x004C1864
		public static FinalFractalHelper.FinalFractalProfile GetFinalFractalProfile(int usedSwordId)
		{
			FinalFractalHelper.FinalFractalProfile defaultProfile;
			if (!FinalFractalHelper._fractalProfiles.TryGetValue(usedSwordId, out defaultProfile))
			{
				defaultProfile = FinalFractalHelper._defaultProfile;
			}
			return defaultProfile;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x004C3688 File Offset: 0x004C1888
		private Color StripColors(float progressOnStrip)
		{
			Color result = Color.Lerp(Color.White, Color.Violet, Utils.GetLerpValue(0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			result.A /= 2;
			return result;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x004C36E3 File Offset: 0x004C18E3
		private float StripWidth(float progressOnStrip)
		{
			return 50f;
		}

		// Token: 0x040012D6 RID: 4822
		public const int TotalIllusions = 4;

		// Token: 0x040012D7 RID: 4823
		public const int FramesPerImportantTrail = 15;

		// Token: 0x040012D8 RID: 4824
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x040012D9 RID: 4825
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

		// Token: 0x040012DA RID: 4826
		private static FinalFractalHelper.FinalFractalProfile _defaultProfile = new FinalFractalHelper.FinalFractalProfile(50f, Color.White);

		// Token: 0x02000588 RID: 1416
		// (Invoke) Token: 0x06003208 RID: 12808
		public delegate void SpawnDustMethod(Vector2 centerPosition, float rotation, Vector2 velocity);

		// Token: 0x02000589 RID: 1417
		public struct FinalFractalProfile
		{
			// Token: 0x0600320B RID: 12811 RVA: 0x005E9904 File Offset: 0x005E7B04
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

			// Token: 0x0600320C RID: 12812 RVA: 0x005E9990 File Offset: 0x005E7B90
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

			// Token: 0x0600320D RID: 12813 RVA: 0x005E9A68 File Offset: 0x005E7C68
			private Color StripColors(float progressOnStrip)
			{
				Color result = this.trailColor * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
				result.A /= 2;
				return result;
			}

			// Token: 0x0600320E RID: 12814 RVA: 0x005E9AA9 File Offset: 0x005E7CA9
			private float StripWidth(float progressOnStrip)
			{
				return this.trailWidth;
			}

			// Token: 0x040059CE RID: 22990
			public float trailWidth;

			// Token: 0x040059CF RID: 22991
			public Color trailColor;

			// Token: 0x040059D0 RID: 22992
			public FinalFractalHelper.SpawnDustMethod dustMethod;

			// Token: 0x040059D1 RID: 22993
			public VertexStrip.StripColorFunction colorMethod;

			// Token: 0x040059D2 RID: 22994
			public VertexStrip.StripHalfWidthFunction widthMethod;
		}
	}
}
