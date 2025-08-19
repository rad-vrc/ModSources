using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x0200039A RID: 922
	public class LegacyChatMonitor : IChatMonitor
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x00593D8D File Offset: 0x00591F8D
		public int TextMaxLengthForScreen
		{
			get
			{
				return Main.screenWidth - 320;
			}
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void OnResolutionChange()
		{
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x00593D9C File Offset: 0x00591F9C
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

		// Token: 0x06002997 RID: 10647 RVA: 0x00593DFC File Offset: 0x00591FFC
		public void Clear()
		{
			for (int i = 0; i < this.numChatLines; i++)
			{
				this.chatLine[i] = new ChatLine();
			}
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x00593E27 File Offset: 0x00592027
		public void ResetOffset()
		{
			this.startChatLine = 0;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x00593E30 File Offset: 0x00592030
		public void Update()
		{
			for (int i = 0; i < this.numChatLines; i++)
			{
				this.chatLine[i].UpdateTimeLeft();
			}
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x00593E5C File Offset: 0x0059205C
		public void Offset(int linesOffset)
		{
			this.showCount = (int)((float)(Main.screenHeight / 3) / FontAssets.MouseText.Value.MeasureString("1").Y) - 1;
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
			else if (linesOffset == -1)
			{
				this.startChatLine--;
				if (this.startChatLine < 0)
				{
					this.startChatLine = 0;
				}
			}
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x00593F28 File Offset: 0x00592128
		public void NewText(string newText, byte R = 255, byte G = 255, byte B = 255)
		{
			this.NewTextMultiline(newText, false, new Color((int)R, (int)G, (int)B), -1);
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x00593F3C File Offset: 0x0059213C
		public void NewTextInternal(string newText, byte R = 255, byte G = 255, byte B = 255, bool force = false)
		{
			int num = 80;
			if (!force && newText.Length > num)
			{
				string text = this.TrimIntoMultipleLines(R, G, B, num, newText);
				if (text.Length > 0)
				{
					this.NewTextInternal(text, R, G, B, true);
				}
				return;
			}
			for (int i = this.numChatLines - 1; i > 0; i--)
			{
				this.chatLine[i].Copy(this.chatLine[i - 1]);
			}
			this.chatLine[0].color = new Color((int)R, (int)G, (int)B);
			this.chatLine[0].originalText = newText;
			this.chatLine[0].parsedText = ChatManager.ParseMessage(this.chatLine[0].originalText, this.chatLine[0].color).ToArray();
			this.chatLine[0].showTime = this.chatLength;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x00594028 File Offset: 0x00592228
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

		// Token: 0x0600299E RID: 10654 RVA: 0x005940FC File Offset: 0x005922FC
		public void NewTextMultiline(string text, bool force = false, Color c = default(Color), int WidthLimit = -1)
		{
			if (c == default(Color))
			{
				c = Color.White;
			}
			List<List<TextSnippet>> list;
			if (WidthLimit != -1)
			{
				list = Utils.WordwrapStringSmart(text, c, FontAssets.MouseText.Value, WidthLimit, 10);
			}
			else
			{
				list = Utils.WordwrapStringSmart(text, c, FontAssets.MouseText.Value, this.TextMaxLengthForScreen, 10);
			}
			for (int i = 0; i < list.Count; i++)
			{
				this.NewText(list[i]);
			}
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x00594178 File Offset: 0x00592378
		public void NewText(List<TextSnippet> snippets)
		{
			for (int i = this.numChatLines - 1; i > 0; i--)
			{
				this.chatLine[i].Copy(this.chatLine[i - 1]);
			}
			this.chatLine[0].originalText = "this is a hack because draw checks length is higher than 0";
			this.chatLine[0].parsedText = snippets.ToArray();
			this.chatLine[0].showTime = this.chatLength;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x005941FC File Offset: 0x005923FC
		public void DrawChat(bool drawingPlayerChat)
		{
			int num = this.startChatLine;
			int num2 = this.startChatLine + this.showCount;
			if (num2 >= this.numChatLines)
			{
				num2 = --this.numChatLines;
				num = num2 - this.showCount;
			}
			int num3 = 0;
			int num4 = -1;
			int num5 = -1;
			for (int i = num; i < num2; i++)
			{
				if (drawingPlayerChat || (this.chatLine[i].showTime > 0 && this.chatLine[i].parsedText.Length != 0))
				{
					int num6 = -1;
					ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, this.chatLine[i].parsedText, new Vector2(88f, (float)(Main.screenHeight - 30 - 28 - num3 * 21)), 0f, Vector2.Zero, Vector2.One, out num6, -1f, 2f);
					if (num6 >= 0 && this.chatLine[i].parsedText[num6].CheckForHover)
					{
						num4 = i;
						num5 = num6;
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

		// Token: 0x04004C9A RID: 19610
		private int numChatLines;

		// Token: 0x04004C9B RID: 19611
		private ChatLine[] chatLine;

		// Token: 0x04004C9C RID: 19612
		private int chatLength;

		// Token: 0x04004C9D RID: 19613
		private int showCount;

		// Token: 0x04004C9E RID: 19614
		private int startChatLine;
	}
}
