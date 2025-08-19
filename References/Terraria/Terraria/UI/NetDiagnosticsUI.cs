using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.UI
{
	// Token: 0x0200008A RID: 138
	public class NetDiagnosticsUI : INetDiagnosticsUI
	{
		// Token: 0x06001216 RID: 4630 RVA: 0x00490EC8 File Offset: 0x0048F0C8
		public void Reset()
		{
			for (int i = 0; i < this._counterByMessageId.Length; i++)
			{
				this._counterByMessageId[i].Reset();
			}
			this._counterByModuleId.Clear();
			this._counterByMessageId[10].exemptFromBadScoreTest = true;
			this._counterByMessageId[82].exemptFromBadScoreTest = true;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00490F2A File Offset: 0x0048F12A
		public void CountReadMessage(int messageId, int messageLength)
		{
			this._counterByMessageId[messageId].CountReadMessage(messageLength);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00490F3E File Offset: 0x0048F13E
		public void CountSentMessage(int messageId, int messageLength)
		{
			this._counterByMessageId[messageId].CountSentMessage(messageLength);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00490F54 File Offset: 0x0048F154
		public void CountReadModuleMessage(int moduleMessageId, int messageLength)
		{
			NetDiagnosticsUI.CounterForMessage value;
			this._counterByModuleId.TryGetValue(moduleMessageId, out value);
			value.CountReadMessage(messageLength);
			this._counterByModuleId[moduleMessageId] = value;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00490F88 File Offset: 0x0048F188
		public void CountSentModuleMessage(int moduleMessageId, int messageLength)
		{
			NetDiagnosticsUI.CounterForMessage value;
			this._counterByModuleId.TryGetValue(moduleMessageId, out value);
			value.CountSentMessage(messageLength);
			this._counterByModuleId[moduleMessageId] = value;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00490FBC File Offset: 0x0048F1BC
		public void Draw(SpriteBatch spriteBatch)
		{
			int num = this._counterByMessageId.Length + this._counterByModuleId.Count;
			for (int i = 0; i <= num / 51; i++)
			{
				Utils.DrawInvBG(spriteBatch, 190 + 400 * i, 110, 390, 683, default(Color));
			}
			Vector2 position;
			for (int j = 0; j < this._counterByMessageId.Length; j++)
			{
				int num2 = j / 51;
				int num3 = j - num2 * 51;
				position.X = (float)(200 + num2 * 400);
				position.Y = (float)(120 + num3 * 13);
				this.DrawCounter(spriteBatch, ref this._counterByMessageId[j], j.ToString(), position);
			}
			int num4 = this._counterByMessageId.Length + 1;
			foreach (KeyValuePair<int, NetDiagnosticsUI.CounterForMessage> keyValuePair in this._counterByModuleId)
			{
				int num5 = num4 / 51;
				int num6 = num4 - num5 * 51;
				position.X = (float)(200 + num5 * 400);
				position.Y = (float)(120 + num6 * 13);
				NetDiagnosticsUI.CounterForMessage value = keyValuePair.Value;
				this.DrawCounter(spriteBatch, ref value, ".." + keyValuePair.Key.ToString(), position);
				num4++;
			}
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00491134 File Offset: 0x0048F334
		private void DrawCounter(SpriteBatch spriteBatch, ref NetDiagnosticsUI.CounterForMessage counter, string title, Vector2 position)
		{
			if (!counter.exemptFromBadScoreTest)
			{
				if (this._highestFoundReadCount < counter.timesReceived)
				{
					this._highestFoundReadCount = counter.timesReceived;
				}
				if (this._highestFoundReadBytes < counter.bytesReceived)
				{
					this._highestFoundReadBytes = counter.bytesReceived;
				}
			}
			Vector2 pos = position;
			string text = title + ": ";
			float num = Utils.Remap((float)counter.bytesReceived, 0f, (float)this._highestFoundReadBytes, 0f, 1f, true);
			Color color = Main.hslToRgb(0.3f * (1f - num), 1f, 0.5f, byte.MaxValue);
			if (counter.exemptFromBadScoreTest)
			{
				color = Color.White;
			}
			string text2 = text;
			this.DrawText(spriteBatch, text2, pos, color);
			pos.X += 30f;
			text2 = "rx:" + string.Format("{0,0}", counter.timesReceived);
			this.DrawText(spriteBatch, text2, pos, color);
			pos.X += 70f;
			text2 = string.Format("{0,0}", counter.bytesReceived);
			this.DrawText(spriteBatch, text2, pos, color);
			pos.X += 70f;
			text2 = text;
			this.DrawText(spriteBatch, text2, pos, color);
			pos.X += 30f;
			text2 = "tx:" + string.Format("{0,0}", counter.timesSent);
			this.DrawText(spriteBatch, text2, pos, color);
			pos.X += 70f;
			text2 = string.Format("{0,0}", counter.bytesSent);
			this.DrawText(spriteBatch, text2, pos, color);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x004912E0 File Offset: 0x0048F4E0
		private void DrawText(SpriteBatch spriteBatch, string text, Vector2 pos, Color color)
		{
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, pos, color, 0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0f);
		}

		// Token: 0x04000FFF RID: 4095
		private NetDiagnosticsUI.CounterForMessage[] _counterByMessageId = new NetDiagnosticsUI.CounterForMessage[(int)(MessageID.Count + 1)];

		// Token: 0x04001000 RID: 4096
		private Dictionary<int, NetDiagnosticsUI.CounterForMessage> _counterByModuleId = new Dictionary<int, NetDiagnosticsUI.CounterForMessage>();

		// Token: 0x04001001 RID: 4097
		private int _highestFoundReadBytes = 1;

		// Token: 0x04001002 RID: 4098
		private int _highestFoundReadCount = 1;

		// Token: 0x0200053F RID: 1343
		private struct CounterForMessage
		{
			// Token: 0x060030BC RID: 12476 RVA: 0x005E3DE1 File Offset: 0x005E1FE1
			public void Reset()
			{
				this.timesReceived = 0;
				this.timesSent = 0;
				this.bytesReceived = 0;
				this.bytesSent = 0;
			}

			// Token: 0x060030BD RID: 12477 RVA: 0x005E3DFF File Offset: 0x005E1FFF
			public void CountReadMessage(int messageLength)
			{
				this.timesReceived++;
				this.bytesReceived += messageLength;
			}

			// Token: 0x060030BE RID: 12478 RVA: 0x005E3E1D File Offset: 0x005E201D
			public void CountSentMessage(int messageLength)
			{
				this.timesSent++;
				this.bytesSent += messageLength;
			}

			// Token: 0x0400584C RID: 22604
			public int timesReceived;

			// Token: 0x0400584D RID: 22605
			public int timesSent;

			// Token: 0x0400584E RID: 22606
			public int bytesReceived;

			// Token: 0x0400584F RID: 22607
			public int bytesSent;

			// Token: 0x04005850 RID: 22608
			public bool exemptFromBadScoreTest;
		}
	}
}
