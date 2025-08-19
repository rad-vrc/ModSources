using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader.Default;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000251 RID: 593
	public class UIModNetDiagnostics : INetDiagnosticsUI
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x0051730C File Offset: 0x0051550C
		private Asset<DynamicSpriteFont> FontAsset
		{
			get
			{
				Asset<DynamicSpriteFont> result;
				if ((result = this.fontAsset) == null)
				{
					result = (this.fontAsset = FontAssets.MouseText);
				}
				return result;
			}
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x00517334 File Offset: 0x00515534
		public UIModNetDiagnostics(IEnumerable<Mod> mods)
		{
			this.Mods = (from mod in mods
			where mod != ModContent.GetInstance<ModLoaderMod>()
			select mod).ToArray<Mod>();
			this.Reset();
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x0051738C File Offset: 0x0051558C
		public void Reset()
		{
			this.CounterByModNetId = new UIModNetDiagnostics.CounterForMessage[this.Mods.Length];
			DynamicSpriteFont font = this.FontAsset.Value;
			this.FirstColumnWidth = font.MeasureString("Mod").X;
			for (int i = 0; i < this.Mods.Length; i++)
			{
				float length = font.MeasureString(this.Mods[i].Name).X;
				if (this.FirstColumnWidth < length)
				{
					this.FirstColumnWidth = length;
				}
			}
			this.FirstColumnWidth += font.MeasureString(": ").X + 2f;
			this.FirstColumnWidth *= 0.7f;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x00517440 File Offset: 0x00515640
		public void CountReadMessage(int messageId, int messageLength)
		{
			int index = Array.FindIndex<Mod>(this.Mods, (Mod mod) => (int)mod.netID == messageId);
			if (index > -1)
			{
				this.CounterByModNetId[index].CountReadMessage(messageLength);
			}
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x00517488 File Offset: 0x00515688
		public void CountSentMessage(int messageId, int messageLength)
		{
			int index = Array.FindIndex<Mod>(this.Mods, (Mod mod) => (int)mod.netID == messageId);
			if (index > -1)
			{
				this.CounterByModNetId[index].CountSentMessage(messageLength);
			}
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x005174D0 File Offset: 0x005156D0
		public void Draw(SpriteBatch spriteBatch)
		{
			int count = this.CounterByModNetId.Length;
			int numCols = (count - 1) / 50;
			int x = 190;
			int xBuf = x + 10;
			int y = 110;
			int yBuf = y + 10;
			int width = 232;
			width += (int)(this.FirstColumnWidth + this.fontAsset.Value.MeasureString(this.HighestFoundSentBytes.ToString()).X * 0.7f);
			int widthBuf = width + 10;
			int lineHeight = 13;
			for (int i = 0; i <= numCols; i++)
			{
				int lineCountInCol = (i == numCols) ? (1 + (count - 1) % 50) : 50;
				int heightBuf = lineHeight * (lineCountInCol + 2) + 10;
				if (i == 0)
				{
					Utils.DrawInvBG(spriteBatch, x + widthBuf * i, y - 20, width, heightBuf + 20, default(Color));
					DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Language.GetTextValue("tModLoader.PressXToClose", 119), new Vector2(200f, 96f), Color.White, 0f, default(Vector2), 1f, 0, 0f);
				}
				else
				{
					Utils.DrawInvBG(spriteBatch, x + widthBuf * i, y, width, heightBuf, default(Color));
				}
				Vector2 modPos;
				modPos..ctor((float)(xBuf + widthBuf * i), (float)yBuf);
				Vector2 headerPos = modPos + new Vector2(this.FirstColumnWidth, 0f);
				this.DrawText(spriteBatch, "Received(#, Bytes)       Sent(#, Bytes)", headerPos, Color.White);
				this.DrawText(spriteBatch, "Mod", modPos, Color.White);
			}
			Vector2 position = default(Vector2);
			for (int j = 0; j < count; j++)
			{
				int colNum = j / 50;
				int lineNum = j - colNum * 50;
				position.X = (float)(xBuf + colNum * widthBuf);
				position.Y = (float)(yBuf + lineHeight + lineNum * lineHeight);
				this.DrawCounter(spriteBatch, this.CounterByModNetId[j], this.Mods[j].Name, position);
			}
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x005176D4 File Offset: 0x005158D4
		private void DrawCounter(SpriteBatch spriteBatch, UIModNetDiagnostics.CounterForMessage counter, string title, Vector2 position)
		{
			if (this.HighestFoundSentBytes < counter.BytesSent)
			{
				this.HighestFoundSentBytes = counter.BytesSent;
			}
			if (this.HighestFoundReadBytes < counter.BytesReceived)
			{
				this.HighestFoundReadBytes = counter.BytesReceived;
			}
			Vector2 pos = position;
			string text = title + ": ";
			float num = Utils.Remap((float)counter.BytesReceived, 0f, (float)this.HighestFoundReadBytes, 0f, 1f, true);
			Color color = Main.hslToRgb(0.3f * (1f - num), 1f, 0.5f, byte.MaxValue);
			string drawText = text;
			this.DrawText(spriteBatch, drawText, pos, color);
			pos.X += this.FirstColumnWidth;
			drawText = "rx:" + string.Format("{0,0}", counter.TimesReceived);
			this.DrawText(spriteBatch, drawText, pos, color);
			pos.X += 70f;
			drawText = string.Format("{0,0}", counter.BytesReceived);
			this.DrawText(spriteBatch, drawText, pos, color);
			pos.X += 70f;
			drawText = "tx:" + string.Format("{0,0}", counter.TimesSent);
			this.DrawText(spriteBatch, drawText, pos, color);
			pos.X += 70f;
			drawText = string.Format("{0,0}", counter.BytesSent);
			this.DrawText(spriteBatch, drawText, pos, color);
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0051784C File Offset: 0x00515A4C
		private void DrawText(SpriteBatch spriteBatch, string text, Vector2 pos, Color color)
		{
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, this.FontAsset.Value, text, pos, color, 0f, Vector2.Zero, 0.7f, 0, 0f);
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x00517883 File Offset: 0x00515A83
		public void CountReadModuleMessage(int moduleMessageId, int messageLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x0051788A File Offset: 0x00515A8A
		public void CountSentModuleMessage(int moduleMessageId, int messageLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001AC3 RID: 6851
		private const float TextScale = 0.7f;

		// Token: 0x04001AC4 RID: 6852
		private const string Suffix = ": ";

		// Token: 0x04001AC5 RID: 6853
		private const string ModString = "Mod";

		// Token: 0x04001AC6 RID: 6854
		private const string RxTxString = "Received(#, Bytes)       Sent(#, Bytes)";

		// Token: 0x04001AC7 RID: 6855
		private Asset<DynamicSpriteFont> fontAsset;

		// Token: 0x04001AC8 RID: 6856
		private readonly Mod[] Mods;

		// Token: 0x04001AC9 RID: 6857
		private UIModNetDiagnostics.CounterForMessage[] CounterByModNetId;

		// Token: 0x04001ACA RID: 6858
		private int HighestFoundSentBytes = 1;

		// Token: 0x04001ACB RID: 6859
		private int HighestFoundReadBytes = 1;

		// Token: 0x04001ACC RID: 6860
		private float FirstColumnWidth;

		// Token: 0x02000A03 RID: 2563
		private struct CounterForMessage
		{
			// Token: 0x06005767 RID: 22375 RVA: 0x0069DC1B File Offset: 0x0069BE1B
			public void Reset()
			{
				this.TimesReceived = 0;
				this.TimesSent = 0;
				this.BytesReceived = 0;
				this.BytesSent = 0;
			}

			// Token: 0x06005768 RID: 22376 RVA: 0x0069DC39 File Offset: 0x0069BE39
			public void CountReadMessage(int messageLength)
			{
				this.TimesReceived++;
				this.BytesReceived += messageLength;
			}

			// Token: 0x06005769 RID: 22377 RVA: 0x0069DC57 File Offset: 0x0069BE57
			public void CountSentMessage(int messageLength)
			{
				this.TimesSent++;
				this.BytesSent += messageLength;
			}

			// Token: 0x04006C21 RID: 27681
			public int TimesReceived;

			// Token: 0x04006C22 RID: 27682
			public int TimesSent;

			// Token: 0x04006C23 RID: 27683
			public int BytesReceived;

			// Token: 0x04006C24 RID: 27684
			public int BytesSent;
		}
	}
}
