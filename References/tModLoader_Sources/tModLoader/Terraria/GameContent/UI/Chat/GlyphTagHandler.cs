using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x0200053F RID: 1343
	public class GlyphTagHandler : ITagHandler
	{
		// Token: 0x06003FEA RID: 16362 RVA: 0x005DD1AC File Offset: 0x005DB3AC
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			int result;
			if (!int.TryParse(text, out result) || result >= 26)
			{
				return new TextSnippet(text);
			}
			return new GlyphTagHandler.GlyphSnippet(result)
			{
				DeleteWhole = true,
				Text = "[g:" + result.ToString() + "]"
			};
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x005DD1F8 File Offset: 0x005DB3F8
		public static string GenerateTag(int index)
		{
			return "[g" + ":" + index.ToString() + "]";
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x005DD218 File Offset: 0x005DB418
		public static string GenerateTag(string keyname)
		{
			int value;
			if (GlyphTagHandler.GlyphIndexes.TryGetValue(keyname, out value))
			{
				return GlyphTagHandler.GenerateTag(value);
			}
			return keyname;
		}

		// Token: 0x04005831 RID: 22577
		private const int GlyphsPerLine = 25;

		// Token: 0x04005832 RID: 22578
		private const int MaxGlyphs = 26;

		// Token: 0x04005833 RID: 22579
		public static float GlyphsScale = 1f;

		// Token: 0x04005834 RID: 22580
		private static Dictionary<string, int> GlyphIndexes = new Dictionary<string, int>
		{
			{
				4096.ToString(),
				0
			},
			{
				8192.ToString(),
				1
			},
			{
				32.ToString(),
				4
			},
			{
				2.ToString(),
				15
			},
			{
				4.ToString(),
				14
			},
			{
				8.ToString(),
				13
			},
			{
				1.ToString(),
				16
			},
			{
				256.ToString(),
				6
			},
			{
				64.ToString(),
				10
			},
			{
				536870912.ToString(),
				20
			},
			{
				2097152.ToString(),
				17
			},
			{
				1073741824.ToString(),
				18
			},
			{
				268435456.ToString(),
				19
			},
			{
				8388608.ToString(),
				8
			},
			{
				512.ToString(),
				7
			},
			{
				128.ToString(),
				11
			},
			{
				33554432.ToString(),
				24
			},
			{
				134217728.ToString(),
				21
			},
			{
				67108864.ToString(),
				22
			},
			{
				16777216.ToString(),
				23
			},
			{
				4194304.ToString(),
				9
			},
			{
				16.ToString(),
				5
			},
			{
				16384.ToString(),
				2
			},
			{
				32768.ToString(),
				3
			},
			{
				"LR",
				25
			}
		};

		// Token: 0x02000C27 RID: 3111
		private class GlyphSnippet : TextSnippet
		{
			// Token: 0x06005F2C RID: 24364 RVA: 0x006CD41F File Offset: 0x006CB61F
			public GlyphSnippet(int index) : base("")
			{
				this._glyphIndex = index;
				this.Color = Color.White;
			}

			// Token: 0x06005F2D RID: 24365 RVA: 0x006CD440 File Offset: 0x006CB640
			public override bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default(Vector2), Color color = default(Color), float scale = 1f)
			{
				if (!justCheckingString && color != Color.Black)
				{
					int num = this._glyphIndex;
					if (this._glyphIndex == 25)
					{
						num = ((Main.GlobalTimeWrappedHourly % 0.6f < 0.3f) ? 17 : 18);
					}
					Texture2D value = TextureAssets.TextGlyph[0].Value;
					spriteBatch.Draw(value, position, new Rectangle?(value.Frame(25, 1, num, num / 25, 0, 0)), color, 0f, Vector2.Zero, GlyphTagHandler.GlyphsScale, 0, 0f);
				}
				size = new Vector2(26f) * GlyphTagHandler.GlyphsScale;
				return true;
			}

			// Token: 0x06005F2E RID: 24366 RVA: 0x006CD4E5 File Offset: 0x006CB6E5
			public override float GetStringLength(DynamicSpriteFont font)
			{
				return 26f * GlyphTagHandler.GlyphsScale;
			}

			// Token: 0x04007891 RID: 30865
			private int _glyphIndex;
		}
	}
}
