using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000542 RID: 1346
	public class LegacyChatMonitor : IChatMonitor
	{
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x005DD6ED File Offset: 0x005DB8ED
		public int TextMaxLengthForScreen
		{
			get
			{
				return Main.screenWidth - 320;
			}
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x005DD6FA File Offset: 0x005DB8FA
		public void OnResolutionChange()
		{
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x005DD6FC File Offset: 0x005DB8FC
		public LegacyChatMonitor()
		{
			this.showCount = 10;
			this.numChatLines = 500;
			this.chatLength = 600;
			this.chatLine = new ChatLine[this.numChatLines];
			for (int i = 0; i < this.numChatLines; i++)
			{
				this.chatLine[i] = new ChatLine();
			}
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x005DD75C File Offset: 0x005DB95C
		public void Clear()
		{
			for (int i = 0; i < this.numChatLines; i++)
			{
				this.chatLine[i] = new ChatLine();
			}
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x005DD787 File Offset: 0x005DB987
		public void ResetOffset()
		{
			this.startChatLine = 0;
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x005DD790 File Offset: 0x005DB990
		public void Update()
		{
			for (int i = 0; i < this.numChatLines; i++)
			{
				this.chatLine[i].UpdateTimeLeft();
			}
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x005DD7BC File Offset: 0x005DB9BC
		public void Offset(int linesOffset)
		{
			this.showCount = (int)((float)(Main.screenHeight / 3) / FontAssets.MouseText.Value.MeasureString("1").Y) - 1;
			if (linesOffset != -1)
			{
				if (linesOffset == 1)
				{
					this.startChatLine++;
					if (this.startChatLine + this.showCount >= this.numChatLines - 1)
					{
						this.startChatLine = this.numChatLines - this.showCount - 1;
					}
					if (this.chatLine[this.startChatLine + this.showCount].originalText == "")
					{
						this.startChatLine--;
						return;
					}
				}
			}
			else
			{
				this.startChatLine--;
				if (this.startChatLine < 0)
				{
					this.startChatLine = 0;
				}
			}
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x005DD88B File Offset: 0x005DBA8B
		public void NewText(string newText, byte R = 255, byte G = 255, byte B = 255)
		{
			this.NewTextMultiline(newText, false, new Color((int)R, (int)G, (int)B), -1);
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x005DD8A0 File Offset: 0x005DBAA0
		public void NewTextInternal(string newText, byte R = 255, byte G = 255, byte B = 255, bool force = false)
		{
			int num = 80;
			if (!force && newText.Length > num)
			{
				string oldText = this.TrimIntoMultipleLines(R, G, B, num, newText);
				if (oldText.Length > 0)
				{
					this.NewTextInternal(oldText, R, G, B, true);
				}
				return;
			}
			for (int num2 = this.numChatLines - 1; num2 > 0; num2--)
			{
				this.chatLine[num2].Copy(this.chatLine[num2 - 1]);
			}
			this.chatLine[0].color = new Color((int)R, (int)G, (int)B);
			this.chatLine[0].originalText = newText;
			this.chatLine[0].parsedText = ChatManager.ParseMessage(this.chatLine[0].originalText, this.chatLine[0].color).ToArray();
			this.chatLine[0].showTime = this.chatLength;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x005DD98C File Offset: 0x005DBB8C
		private string TrimIntoMultipleLines(byte R, byte G, byte B, int maxTextSize, string oldText)
		{
			while (oldText.Length > maxTextSize)
			{
				int num = maxTextSize;
				int num2 = num;
				while (oldText.Substring(num2, 1) != " ")
				{
					num2--;
					if (num2 < 1)
					{
						break;
					}
				}
				if (num2 == 0)
				{
					while (oldText.Substring(num, 1) != " ")
					{
						num++;
						if (num >= oldText.Length - 1)
						{
							break;
						}
					}
				}
				else
				{
					num = num2;
				}
				if (num >= oldText.Length - 1)
				{
					num = oldText.Length;
				}
				string newText = oldText.Substring(0, num);
				this.NewTextInternal(newText, R, G, B, true);
				oldText = oldText.Substring(num);
				if (oldText.Length > 0)
				{
					while (oldText.Substring(0, 1) == " ")
					{
						oldText = oldText.Substring(1);
					}
				}
			}
			return oldText;
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x005DDA60 File Offset: 0x005DBC60
		public void NewTextMultiline(string text, bool force = false, Color c = default(Color), int WidthLimit = -1)
		{
			if (c == default(Color))
			{
				c = Color.White;
			}
			List<List<TextSnippet>> list = (WidthLimit == -1) ? Utils.WordwrapStringSmart(text, c, FontAssets.MouseText.Value, this.TextMaxLengthForScreen, 10) : Utils.WordwrapStringSmart(text, c, FontAssets.MouseText.Value, WidthLimit, 10);
			for (int i = 0; i < list.Count; i++)
			{
				this.NewText(list[i]);
			}
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x005DDADC File Offset: 0x005DBCDC
		public void NewText(List<TextSnippet> snippets)
		{
			for (int num = this.numChatLines - 1; num > 0; num--)
			{
				this.chatLine[num].Copy(this.chatLine[num - 1]);
			}
			this.chatLine[0].originalText = "this is a hack because draw checks length is higher than 0";
			this.chatLine[0].parsedText = snippets.ToArray();
			this.chatLine[0].showTime = this.chatLength;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x005DDB60 File Offset: 0x005DBD60
		public void DrawChat(bool drawingPlayerChat)
		{
			int num = this.startChatLine;
			int num2 = this.startChatLine + this.showCount;
			if (num2 >= this.numChatLines)
			{
				int num6 = this.numChatLines - 1;
				this.numChatLines = num6;
				num2 = num6;
				num = num2 - this.showCount;
			}
			int num3 = 0;
			int num4 = -1;
			int num5 = -1;
			for (int i = num; i < num2; i++)
			{
				if (drawingPlayerChat || (this.chatLine[i].showTime > 0 && this.chatLine[i].parsedText.Length != 0))
				{
					int hoveredSnippet = -1;
					ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, this.chatLine[i].parsedText, new Vector2(88f, (float)(Main.screenHeight - 30 - 28 - num3 * 21)), 0f, Vector2.Zero, Vector2.One, out hoveredSnippet, -1f, 2f);
					if (hoveredSnippet >= 0 && this.chatLine[i].parsedText[hoveredSnippet].CheckForHover)
					{
						num4 = i;
						num5 = hoveredSnippet;
					}
				}
				num3++;
			}
			if (num4 > -1)
			{
				this.chatLine[num4].parsedText[num5].OnHover();
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					this.chatLine[num4].parsedText[num5].OnClick();
				}
			}
		}

		// Token: 0x04005835 RID: 22581
		private int numChatLines;

		// Token: 0x04005836 RID: 22582
		private ChatLine[] chatLine;

		// Token: 0x04005837 RID: 22583
		private int chatLength;

		// Token: 0x04005838 RID: 22584
		private int showCount;

		// Token: 0x04005839 RID: 22585
		private int startChatLine;
	}
}
