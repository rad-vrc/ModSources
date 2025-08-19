using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x0200039C RID: 924
	public class GlyphTagHandler : ITagHandler
	{
		// Token: 0x060029AC RID: 10668 RVA: 0x005946E0 File Offset: 0x005928E0
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			int num;
			if (!int.TryParse(text, out num) || num >= 26)
			{
				return new TextSnippet(text);
			}
			return new GlyphTagHandler.GlyphSnippet(num)
			{
				DeleteWhole = true,
				Text = "[g:" + num + "]"
			};
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x0059472C File Offset: 0x0059292C
		public static string GenerateTag(int index)
		{
			string text = "[g";
			return string.Concat(new object[]
			{
				text,
				":",
				index,
				"]"
			});
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x0059476C File Offset: 0x0059296C
		public static string GenerateTag(string keyname)
		{
			int index;
			if (GlyphTagHandler.GlyphIndexes.TryGetValue(keyname, out index))
			{
				return GlyphTagHandler.GenerateTag(index);
			}
			return keyname;
		}

		// Token: 0x04004CA4 RID: 19620
		private const int GlyphsPerLine = 25;

		// Token: 0x04004CA5 RID: 19621
		private const int MaxGlyphs = 26;

		// Token: 0x04004CA6 RID: 19622
		public static float GlyphsScale = 1f;

		// Token: 0x04004CA7 RID: 19623
		private static Dictionary<string, int> GlyphIndexes = new Dictionary<string, int>
		{
			{
				Buttons.A.ToString(),
				0
			},
			{
				Buttons.B.ToString(),
				1
			},
			{
				Buttons.Back.ToString(),
				4
			},
			{
				Buttons.DPadDown.ToString(),
				15
			},
			{
				Buttons.DPadLeft.ToString(),
				14
			},
			{
				Buttons.DPadRight.ToString(),
				13
			},
			{
				Buttons.DPadUp.ToString(),
				16
			},
			{
				Buttons.LeftShoulder.ToString(),
				6
			},
			{
				Buttons.LeftStick.ToString(),
				10
			},
			{
				Buttons.LeftThumbstickDown.ToString(),
				20
			},
			{
				Buttons.LeftThumbstickLeft.ToString(),
				17
			},
			{
				Buttons.LeftThumbstickRight.ToString(),
				18
			},
			{
				Buttons.LeftThumbstickUp.ToString(),
				19
			},
			{
				Buttons.LeftTrigger.ToString(),
				8
			},
			{
				Buttons.RightShoulder.ToString(),
				7
			},
			{
				Buttons.RightStick.ToString(),
				11
			},
			{
				Buttons.RightThumbstickDown.ToString(),
				24
			},
			{
				Buttons.RightThumbstickLeft.ToString(),
				21
			},
			{
				Buttons.RightThumbstickRight.ToString(),
				22
			},
			{
				Buttons.RightThumbstickUp.ToString(),
				23
			},
			{
				Buttons.RightTrigger.ToString(),
				9
			},
			{
				Buttons.Start.ToString(),
				5
			},
			{
				Buttons.X.ToString(),
				2
			},
			{
				Buttons.Y.ToString(),
				3
			},
			{
				"LR",
				25
			}
		};

		// Token: 0x02000757 RID: 1879
		private class GlyphSnippet : TextSnippet
		{
			// Token: 0x060038B9 RID: 14521 RVA: 0x0061416A File Offset: 0x0061236A
			public GlyphSnippet(int index) : base("")
			{
				this._glyphIndex = index;
				this.Color = Color.White;
			}

			// Token: 0x060038BA RID: 14522 RVA: 0x0061418C File Offset: 0x0061238C
			public override bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default(Vector2), Color color = default(Color), float scale = 1f)
			{
				if (!justCheckingString && color != Color.Black)
				{
					int num = this._glyphIndex;
					int glyphIndex = this._glyphIndex;
					if (glyphIndex == 25)
					{
						num = ((Main.GlobalTimeWrappedHourly % 0.6f < 0.3f) ? 17 : 18);
					}
					Texture2D value = TextureAssets.TextGlyph[0].Value;
					spriteBatch.Draw(value, position, new Rectangle?(value.Frame(25, 1, num, num / 25, 0, 0)), color, 0f, Vector2.Zero, GlyphTagHandler.GlyphsScale, SpriteEffects.None, 0f);
				}
				size = new Vector2(26f) * GlyphTagHandler.GlyphsScale;
				return true;
			}

			// Token: 0x060038BB RID: 14523 RVA: 0x00614233 File Offset: 0x00612433
			public override float GetStringLength(DynamicSpriteFont font)
			{
				return 26f * GlyphTagHandler.GlyphsScale;
			}

			// Token: 0x0400643E RID: 25662
			private int _glyphIndex;
		}
	}
}
