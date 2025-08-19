using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.Chat;
using Terraria.GameContent.UI.Chat;

namespace Terraria.UI.Chat
{
	// Token: 0x0200009D RID: 157
	public static class ChatManager
	{
		// Token: 0x0600131B RID: 4891 RVA: 0x0049D5E4 File Offset: 0x0049B7E4
		public static Color WaveColor(Color color)
		{
			float num = (float)Main.mouseTextColor / 255f;
			color = Color.Lerp(color, Color.Black, 1f - num);
			color.A = Main.mouseTextColor;
			return color;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0049D620 File Offset: 0x0049B820
		public static void ConvertNormalSnippets(TextSnippet[] snippets)
		{
			for (int i = 0; i < snippets.Length; i++)
			{
				TextSnippet textSnippet = snippets[i];
				if (snippets[i].GetType() == typeof(TextSnippet))
				{
					PlainTagHandler.PlainSnippet plainSnippet = new PlainTagHandler.PlainSnippet(textSnippet.Text, textSnippet.Color, textSnippet.Scale);
					snippets[i] = plainSnippet;
				}
			}
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0049D674 File Offset: 0x0049B874
		public static void Register<T>(params string[] names) where T : ITagHandler, new()
		{
			T t = Activator.CreateInstance<T>();
			for (int i = 0; i < names.Length; i++)
			{
				ChatManager._handlers[names[i].ToLower()] = t;
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0049D6B0 File Offset: 0x0049B8B0
		private static ITagHandler GetHandler(string tagName)
		{
			string key = tagName.ToLower();
			if (ChatManager._handlers.ContainsKey(key))
			{
				return ChatManager._handlers[key];
			}
			return null;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0049D6E0 File Offset: 0x0049B8E0
		public static List<TextSnippet> ParseMessage(string text, Color baseColor)
		{
			text = text.Replace("\r", "");
			MatchCollection matchCollection = ChatManager.Regexes.Format.Matches(text);
			List<TextSnippet> list = new List<TextSnippet>();
			int num = 0;
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				if (match.Index > num)
				{
					list.Add(new TextSnippet(text.Substring(num, match.Index - num), baseColor, 1f));
				}
				num = match.Index + match.Length;
				string value = match.Groups["tag"].Value;
				string value2 = match.Groups["text"].Value;
				string value3 = match.Groups["options"].Value;
				ITagHandler handler = ChatManager.GetHandler(value);
				if (handler != null)
				{
					list.Add(handler.Parse(value2, baseColor, value3));
					list[list.Count - 1].TextOriginal = match.ToString();
				}
				else
				{
					list.Add(new TextSnippet(value2, baseColor, 1f));
				}
			}
			if (text.Length > num)
			{
				list.Add(new TextSnippet(text.Substring(num, text.Length - num), baseColor, 1f));
			}
			return list;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0049D84C File Offset: 0x0049BA4C
		public static bool AddChatText(DynamicSpriteFont font, string text, Vector2 baseScale)
		{
			int num = Main.screenWidth - 330;
			if (ChatManager.GetStringSize(font, Main.chatText + text, baseScale, -1f).X > (float)num)
			{
				return false;
			}
			Main.chatText += text;
			return true;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0049D8A0 File Offset: 0x0049BAA0
		public static Vector2 GetStringSize(DynamicSpriteFont font, string text, Vector2 baseScale, float maxWidth = -1f)
		{
			TextSnippet[] snippets = ChatManager.ParseMessage(text, Color.White).ToArray();
			return ChatManager.GetStringSize(font, snippets, baseScale, maxWidth);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0049D8C8 File Offset: 0x0049BAC8
		public static Vector2 GetStringSize(DynamicSpriteFont font, TextSnippet[] snippets, Vector2 baseScale, float maxWidth = -1f)
		{
			Vector2 vec = new Vector2((float)Main.mouseX, (float)Main.mouseY);
			Vector2 zero = Vector2.Zero;
			Vector2 vector = zero;
			Vector2 vector2 = vector;
			float x = font.MeasureString(" ").X;
			float num = 0f;
			foreach (TextSnippet textSnippet in snippets)
			{
				textSnippet.Update();
				float scale = textSnippet.Scale;
				Vector2 vector3;
				if (textSnippet.UniqueDraw(true, out vector3, null, default(Vector2), default(Color), 1f))
				{
					vector.X += vector3.X * baseScale.X * scale;
					vector2.X = Math.Max(vector2.X, vector.X);
					vector2.Y = Math.Max(vector2.Y, vector.Y + vector3.Y);
				}
				else
				{
					string[] array = textSnippet.Text.Split(new char[]
					{
						'\n'
					});
					string[] array2 = array;
					for (int j = 0; j < array2.Length; j++)
					{
						string[] array3 = array2[j].Split(new char[]
						{
							' '
						});
						for (int k = 0; k < array3.Length; k++)
						{
							if (k != 0)
							{
								vector.X += x * baseScale.X * scale;
							}
							if (maxWidth > 0f)
							{
								float num2 = font.MeasureString(array3[k]).X * baseScale.X * scale;
								if (vector.X - zero.X + num2 > maxWidth)
								{
									vector.X = zero.X;
									vector.Y += (float)font.LineSpacing * num * baseScale.Y;
									vector2.Y = Math.Max(vector2.Y, vector.Y);
									num = 0f;
								}
							}
							if (num < scale)
							{
								num = scale;
							}
							Vector2 vector4 = font.MeasureString(array3[k]);
							vec.Between(vector, vector + vector4);
							vector.X += vector4.X * baseScale.X * scale;
							vector2.X = Math.Max(vector2.X, vector.X);
							vector2.Y = Math.Max(vector2.Y, vector.Y + vector4.Y);
						}
						if (array.Length > 1)
						{
							vector.X = zero.X;
							vector.Y += (float)font.LineSpacing * num * baseScale.Y;
							vector2.Y = Math.Max(vector2.Y, vector.Y);
							num = 0f;
						}
					}
				}
			}
			return vector2;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0049DB8C File Offset: 0x0049BD8C
		public static void DrawColorCodedStringShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
		{
			for (int i = 0; i < ChatManager.ShadowDirections.Length; i++)
			{
				int num;
				ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position + ChatManager.ShadowDirections[i] * spread, baseColor, rotation, origin, baseScale, out num, maxWidth, true);
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0049DBDC File Offset: 0x0049BDDC
		public static Vector2 DrawColorCodedString(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth, bool ignoreColors = false)
		{
			int num = -1;
			Vector2 vec = new Vector2((float)Main.mouseX, (float)Main.mouseY);
			Vector2 vector = position;
			Vector2 vector2 = vector;
			float x = font.MeasureString(" ").X;
			Color color = baseColor;
			float num2 = 0f;
			for (int i = 0; i < snippets.Length; i++)
			{
				TextSnippet textSnippet = snippets[i];
				textSnippet.Update();
				if (!ignoreColors)
				{
					color = textSnippet.GetVisibleColor();
				}
				float scale = textSnippet.Scale;
				Vector2 vector3;
				if (textSnippet.UniqueDraw(false, out vector3, spriteBatch, vector, color, scale))
				{
					if (vec.Between(vector, vector + vector3))
					{
						num = i;
					}
					vector.X += vector3.X * baseScale.X * scale;
					vector2.X = Math.Max(vector2.X, vector.X);
				}
				else
				{
					string[] array = textSnippet.Text.Split(new char[]
					{
						'\n'
					});
					array = Regex.Split(textSnippet.Text, "(\n)");
					bool flag = true;
					foreach (string text in array)
					{
						string[] array2 = Regex.Split(text, "( )");
						array2 = text.Split(new char[]
						{
							' '
						});
						if (text == "\n")
						{
							vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
							vector.X = position.X;
							vector2.Y = Math.Max(vector2.Y, vector.Y);
							num2 = 0f;
							flag = false;
						}
						else
						{
							for (int k = 0; k < array2.Length; k++)
							{
								if (k != 0)
								{
									vector.X += x * baseScale.X * scale;
								}
								if (maxWidth > 0f)
								{
									float num3 = font.MeasureString(array2[k]).X * baseScale.X * scale;
									if (vector.X - position.X + num3 > maxWidth)
									{
										vector.X = position.X;
										vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
										vector2.Y = Math.Max(vector2.Y, vector.Y);
										num2 = 0f;
									}
								}
								if (num2 < scale)
								{
									num2 = scale;
								}
								DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, array2[k], vector, color, rotation, origin, baseScale * textSnippet.Scale * scale, SpriteEffects.None, 0f);
								Vector2 vector4 = font.MeasureString(array2[k]);
								if (vec.Between(vector, vector + vector4))
								{
									num = i;
								}
								vector.X += vector4.X * baseScale.X * scale;
								vector2.X = Math.Max(vector2.X, vector.X);
							}
							if (array.Length > 1 && flag)
							{
								vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
								vector.X = position.X;
								vector2.Y = Math.Max(vector2.Y, vector.Y);
								num2 = 0f;
							}
							flag = true;
						}
					}
				}
			}
			hoveredSnippet = num;
			return vector2;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0049DF34 File Offset: 0x0049C134
		public static Vector2 DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, float rotation, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth = -1f, float spread = 2f)
		{
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, Color.Black, rotation, origin, baseScale, maxWidth, spread);
			return ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, Color.White, rotation, origin, baseScale, out hoveredSnippet, maxWidth, false);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0049DF74 File Offset: 0x0049C174
		public static Vector2 DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, float rotation, Color color, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth = -1f, float spread = 2f)
		{
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, Color.Black, rotation, origin, baseScale, maxWidth, spread);
			return ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, color, rotation, origin, baseScale, out hoveredSnippet, maxWidth, true);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0049DFB0 File Offset: 0x0049C1B0
		public static void DrawColorCodedStringShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
		{
			for (int i = 0; i < ChatManager.ShadowDirections.Length; i++)
			{
				ChatManager.DrawColorCodedString(spriteBatch, font, text, position + ChatManager.ShadowDirections[i] * spread, baseColor, rotation, origin, baseScale, maxWidth, true);
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0049DFFC File Offset: 0x0049C1FC
		public static Vector2 DrawColorCodedString(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, bool ignoreColors = false)
		{
			Vector2 vector = position;
			Vector2 vector2 = vector;
			string[] array = text.Split(new char[]
			{
				'\n'
			});
			float x = font.MeasureString(" ").X;
			Color color = baseColor;
			float num = 1f;
			float num2 = 0f;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				foreach (string text2 in array2[i].Split(new char[]
				{
					':'
				}))
				{
					if (text2.StartsWith("sss"))
					{
						if (text2.StartsWith("sss1"))
						{
							if (!ignoreColors)
							{
								color = Color.Red;
							}
						}
						else if (text2.StartsWith("sss2"))
						{
							if (!ignoreColors)
							{
								color = Color.Blue;
							}
						}
						else if (text2.StartsWith("sssr") && !ignoreColors)
						{
							color = Color.White;
						}
					}
					else
					{
						string[] array4 = text2.Split(new char[]
						{
							' '
						});
						for (int k = 0; k < array4.Length; k++)
						{
							if (k != 0)
							{
								vector.X += x * baseScale.X * num;
							}
							if (maxWidth > 0f)
							{
								float num3 = font.MeasureString(array4[k]).X * baseScale.X * num;
								if (vector.X - position.X + num3 > maxWidth)
								{
									vector.X = position.X;
									vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
									vector2.Y = Math.Max(vector2.Y, vector.Y);
									num2 = 0f;
								}
							}
							if (num2 < num)
							{
								num2 = num;
							}
							DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, array4[k], vector, color, rotation, origin, baseScale * num, SpriteEffects.None, 0f);
							vector.X += font.MeasureString(array4[k]).X * baseScale.X * num;
							vector2.X = Math.Max(vector2.X, vector.X);
						}
					}
				}
				vector.X = position.X;
				vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
				vector2.Y = Math.Max(vector2.Y, vector.Y);
				num2 = 0f;
			}
			return vector2;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0049E284 File Offset: 0x0049C484
		public static Vector2 DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
		{
			TextSnippet[] snippets = ChatManager.ParseMessage(text, baseColor).ToArray();
			ChatManager.ConvertNormalSnippets(snippets);
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, new Color(0, 0, 0, (int)baseColor.A), rotation, origin, baseScale, maxWidth, spread);
			int num;
			return ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, Color.White, rotation, origin, baseScale, out num, maxWidth, false);
		}

		// Token: 0x04001074 RID: 4212
		public static readonly ChatCommandProcessor Commands = new ChatCommandProcessor();

		// Token: 0x04001075 RID: 4213
		private static ConcurrentDictionary<string, ITagHandler> _handlers = new ConcurrentDictionary<string, ITagHandler>();

		// Token: 0x04001076 RID: 4214
		public static readonly Vector2[] ShadowDirections = new Vector2[]
		{
			-Vector2.UnitX,
			Vector2.UnitX,
			-Vector2.UnitY,
			Vector2.UnitY
		};

		// Token: 0x0200054F RID: 1359
		public static class Regexes
		{
			// Token: 0x040058AB RID: 22699
			public static readonly Regex Format = new Regex("(?<!\\\\)\\[(?<tag>[a-zA-Z]{1,10})(\\/(?<options>[^:]+))?:(?<text>.+?)(?<!\\\\)\\]", RegexOptions.Compiled);
		}
	}
}
