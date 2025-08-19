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
	// Token: 0x020000C1 RID: 193
	public static class ChatManager
	{
		// Token: 0x06001674 RID: 5748 RVA: 0x004B4130 File Offset: 0x004B2330
		public static Color WaveColor(Color color)
		{
			float num = (float)Main.mouseTextColor / 255f;
			color = Color.Lerp(color, Color.Black, 1f - num);
			color.A = Main.mouseTextColor;
			return color;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x004B416C File Offset: 0x004B236C
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

		// Token: 0x06001676 RID: 5750 RVA: 0x004B41C0 File Offset: 0x004B23C0
		public static void Register<T>(params string[] names) where T : ITagHandler, new()
		{
			T val = Activator.CreateInstance<T>();
			for (int i = 0; i < names.Length; i++)
			{
				ChatManager._handlers[names[i].ToLower()] = val;
			}
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x004B41FC File Offset: 0x004B23FC
		private static ITagHandler GetHandler(string tagName)
		{
			string key = tagName.ToLower();
			if (ChatManager._handlers.ContainsKey(key))
			{
				return ChatManager._handlers[key];
			}
			return null;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x004B422C File Offset: 0x004B242C
		public static List<TextSnippet> ParseMessage(string text, Color baseColor)
		{
			text = text.Replace("\r", "");
			MatchCollection matchCollection = ChatManager.Regexes.Format.Matches(text);
			List<TextSnippet> list = new List<TextSnippet>();
			int num = 0;
			foreach (object obj in matchCollection)
			{
				Match item = (Match)obj;
				if (item.Index > num)
				{
					list.Add(new TextSnippet(text.Substring(num, item.Index - num), baseColor, 1f));
				}
				num = item.Index + item.Length;
				string value4 = item.Groups["tag"].Value;
				string value2 = item.Groups["text"].Value;
				string value3 = item.Groups["options"].Value;
				ITagHandler handler = ChatManager.GetHandler(value4);
				if (handler != null)
				{
					list.Add(handler.Parse(value2, baseColor, value3));
					list[list.Count - 1].TextOriginal = item.ToString();
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

		// Token: 0x06001679 RID: 5753 RVA: 0x004B4398 File Offset: 0x004B2598
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

		// Token: 0x0600167A RID: 5754 RVA: 0x004B43EC File Offset: 0x004B25EC
		public static Vector2 GetStringSize(DynamicSpriteFont font, string text, Vector2 baseScale, float maxWidth = -1f)
		{
			TextSnippet[] snippets = ChatManager.ParseMessage(text, Color.White).ToArray();
			return ChatManager.GetStringSize(font, snippets, baseScale, maxWidth);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x004B4414 File Offset: 0x004B2614
		public static Vector2 GetStringSize(DynamicSpriteFont font, TextSnippet[] snippets, Vector2 baseScale, float maxWidth = -1f)
		{
			Vector2 vec;
			vec..ctor((float)Main.mouseX, (float)Main.mouseY);
			Vector2 zero = Vector2.Zero;
			Vector2 vector = zero;
			Vector2 result = vector;
			float x = font.MeasureString(" ").X;
			float num2 = 0f;
			foreach (TextSnippet textSnippet in snippets)
			{
				textSnippet.Update();
				float num3 = textSnippet.Scale;
				TextSnippet textSnippet2 = textSnippet;
				bool justCheckingString = true;
				SpriteBatch spriteBatch = null;
				float scale = baseScale.X * num3;
				Vector2 size;
				if (textSnippet2.UniqueDraw(justCheckingString, out size, spriteBatch, default(Vector2), default(Color), scale))
				{
					vector.X += size.X;
					result.X = Math.Max(result.X, vector.X);
					result.Y = Math.Max(result.Y, vector.Y + size.Y);
				}
				else
				{
					string[] array = textSnippet.Text.Split('\n', StringSplitOptions.None);
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string[] array3 = array2[i].Split(' ', StringSplitOptions.None);
						for (int j = 0; j < array3.Length; j++)
						{
							if (j != 0)
							{
								vector.X += x * baseScale.X * num3;
							}
							if (maxWidth > 0f)
							{
								float num4 = font.MeasureString(array3[j]).X * baseScale.X * num3;
								if (vector.X - zero.X + num4 > maxWidth)
								{
									vector.X = zero.X;
									vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
									result.Y = Math.Max(result.Y, vector.Y);
									num2 = 0f;
								}
							}
							if (num2 < num3)
							{
								num2 = num3;
							}
							Vector2 vector2 = font.MeasureString(array3[j]);
							vec.Between(vector, vector + vector2);
							vector.X += vector2.X * baseScale.X * num3;
							result.X = Math.Max(result.X, vector.X);
							result.Y = Math.Max(result.Y, vector.Y + vector2.Y);
						}
						if (array.Length > 1 && i < array2.Length - 1)
						{
							vector.X = zero.X;
							vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
							result.Y = Math.Max(result.Y, vector.Y);
							num2 = 0f;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x004B46D4 File Offset: 0x004B28D4
		public static void DrawColorCodedStringShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
		{
			for (int i = 0; i < ChatManager.ShadowDirections.Length; i++)
			{
				int num;
				ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position + ChatManager.ShadowDirections[i] * spread, baseColor, rotation, origin, baseScale, out num, maxWidth, true);
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x004B4724 File Offset: 0x004B2924
		public static Vector2 DrawColorCodedString(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth, bool ignoreColors = false)
		{
			int num = -1;
			Vector2 vec;
			vec..ctor((float)Main.mouseX, (float)Main.mouseY);
			Vector2 vector = position;
			Vector2 result = vector;
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
				float num3 = textSnippet.Scale;
				Vector2 size;
				if (textSnippet.UniqueDraw(false, out size, spriteBatch, vector, color, baseScale.X * num3))
				{
					if (vec.Between(vector, vector + size))
					{
						num = i;
					}
					vector.X += size.X;
					result.X = Math.Max(result.X, vector.X);
				}
				else
				{
					string[] array = textSnippet.Text.Split('\n', StringSplitOptions.None);
					array = Regex.Split(textSnippet.Text, "(\n)");
					bool flag = true;
					foreach (string text in array)
					{
						string[] array2 = Regex.Split(text, "( )");
						array2 = text.Split(' ', StringSplitOptions.None);
						if (text == "\n")
						{
							vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
							vector.X = position.X;
							result.Y = Math.Max(result.Y, vector.Y);
							num2 = 0f;
							flag = false;
						}
						else
						{
							for (int j = 0; j < array2.Length; j++)
							{
								if (j != 0)
								{
									vector.X += x * baseScale.X * num3;
								}
								if (maxWidth > 0f)
								{
									float num4 = font.MeasureString(array2[j]).X * baseScale.X * num3;
									if (vector.X - position.X + num4 > maxWidth)
									{
										vector.X = position.X;
										vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
										result.Y = Math.Max(result.Y, vector.Y);
										num2 = 0f;
									}
								}
								if (num2 < num3)
								{
									num2 = num3;
								}
								DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, array2[j], vector, color, rotation, origin, baseScale * textSnippet.Scale * num3, 0, 0f);
								Vector2 vector2 = font.MeasureString(array2[j]);
								if (vec.Between(vector, vector + vector2))
								{
									num = i;
								}
								vector.X += vector2.X * baseScale.X * num3;
								result.X = Math.Max(result.X, vector.X);
							}
							if (array.Length > 1 && flag)
							{
								vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
								vector.X = position.X;
								result.Y = Math.Max(result.Y, vector.Y);
								num2 = 0f;
							}
							flag = true;
						}
					}
				}
			}
			hoveredSnippet = num;
			return result;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x004B4A68 File Offset: 0x004B2C68
		public static Vector2 DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, float rotation, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth = -1f, float spread = 2f)
		{
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, Color.Black, rotation, origin, baseScale, maxWidth, spread);
			return ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, Color.White, rotation, origin, baseScale, out hoveredSnippet, maxWidth, false);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x004B4AA8 File Offset: 0x004B2CA8
		public static Vector2 DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, float rotation, Color color, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth = -1f, float spread = 2f)
		{
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, Color.Black, rotation, origin, baseScale, maxWidth, spread);
			return ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, color, rotation, origin, baseScale, out hoveredSnippet, maxWidth, false);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x004B4AE4 File Offset: 0x004B2CE4
		public static Vector2 DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, float rotation, Color color, Color shadowColor, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth = -1f, float spread = 2f)
		{
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, shadowColor, rotation, origin, baseScale, maxWidth, spread);
			return ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, color, rotation, origin, baseScale, out hoveredSnippet, maxWidth, false);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x004B4B1C File Offset: 0x004B2D1C
		public static void DrawColorCodedStringShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
		{
			for (int i = 0; i < ChatManager.ShadowDirections.Length; i++)
			{
				ChatManager.DrawColorCodedString(spriteBatch, font, text, position + ChatManager.ShadowDirections[i] * spread, baseColor, rotation, origin, baseScale, maxWidth, true);
			}
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x004B4B68 File Offset: 0x004B2D68
		public static Vector2 DrawColorCodedString(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, bool ignoreColors = false)
		{
			Vector2 vector = position;
			Vector2 result = vector;
			string[] array4 = text.Split('\n', StringSplitOptions.None);
			float x = font.MeasureString(" ").X;
			Color color = baseColor;
			float num = 1f;
			float num2 = 0f;
			string[] array2 = array4;
			for (int i = 0; i < array2.Length; i++)
			{
				foreach (string text2 in array2[i].Split(':', StringSplitOptions.None))
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
						string[] array3 = text2.Split(' ', StringSplitOptions.None);
						for (int j = 0; j < array3.Length; j++)
						{
							if (j != 0)
							{
								vector.X += x * baseScale.X * num;
							}
							if (maxWidth > 0f)
							{
								float num3 = font.MeasureString(array3[j]).X * baseScale.X * num;
								if (vector.X - position.X + num3 > maxWidth)
								{
									vector.X = position.X;
									vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
									result.Y = Math.Max(result.Y, vector.Y);
									num2 = 0f;
								}
							}
							if (num2 < num)
							{
								num2 = num;
							}
							DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, array3[j], vector, color, rotation, origin, baseScale * num, 0, 0f);
							vector.X += font.MeasureString(array3[j]).X * baseScale.X * num;
							result.X = Math.Max(result.X, vector.X);
						}
					}
				}
				vector.X = position.X;
				vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
				result.Y = Math.Max(result.Y, vector.Y);
				num2 = 0f;
			}
			return result;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x004B4DD8 File Offset: 0x004B2FD8
		public static Vector2 DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
		{
			TextSnippet[] snippets = ChatManager.ParseMessage(text, baseColor).ToArray();
			ChatManager.ConvertNormalSnippets(snippets);
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, new Color(0, 0, 0, (int)baseColor.A), rotation, origin, baseScale, maxWidth, spread);
			int hoveredSnippet;
			return ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, Color.White, rotation, origin, baseScale, out hoveredSnippet, maxWidth, false);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x004B4E34 File Offset: 0x004B3034
		public static Vector2 DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, Color shadowColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
		{
			TextSnippet[] snippets = ChatManager.ParseMessage(text, baseColor).ToArray();
			ChatManager.ConvertNormalSnippets(snippets);
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, shadowColor, rotation, origin, baseScale, maxWidth, spread);
			int num;
			return ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, Color.White, rotation, origin, baseScale, out num, maxWidth, false);
		}

		// Token: 0x040012A0 RID: 4768
		public static readonly ChatCommandProcessor Commands = new ChatCommandProcessor();

		// Token: 0x040012A1 RID: 4769
		private static ConcurrentDictionary<string, ITagHandler> _handlers = new ConcurrentDictionary<string, ITagHandler>();

		// Token: 0x040012A2 RID: 4770
		public static readonly Vector2[] ShadowDirections = new Vector2[]
		{
			-Vector2.UnitX,
			Vector2.UnitX,
			-Vector2.UnitY,
			Vector2.UnitY
		};

		// Token: 0x02000879 RID: 2169
		public static class Regexes
		{
			// Token: 0x040069C5 RID: 27077
			public static readonly Regex Format = new Regex("(?<!\\\\)\\[(?<tag>[a-zA-Z]{1,10})(\\/(?<options>[^:]+))?:(?<text>.+?)(?<!\\\\)\\]", RegexOptions.Compiled);
		}
	}
}
