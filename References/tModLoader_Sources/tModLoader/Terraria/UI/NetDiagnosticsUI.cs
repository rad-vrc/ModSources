using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.UI
{
	// Token: 0x020000AF RID: 175
	public class NetDiagnosticsUI : INetDiagnosticsUI
	{
		// Token: 0x06001588 RID: 5512 RVA: 0x004B0054 File Offset: 0x004AE254
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

		// Token: 0x06001589 RID: 5513 RVA: 0x004B00B6 File Offset: 0x004AE2B6
		public void CountReadMessage(int messageId, int messageLength)
		{
			if (messageId >= this._counterByMessageId.Length)
			{
				return;
			}
			this._counterByMessageId[messageId].CountReadMessage(messageLength);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x004B00D6 File Offset: 0x004AE2D6
		public void CountSentMessage(int messageId, int messageLength)
		{
			this._counterByMessageId[messageId].CountSentMessage(messageLength);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x004B00EC File Offset: 0x004AE2EC
		public void CountReadModuleMessage(int moduleMessageId, int messageLength)
		{
			NetDiagnosticsUI.CounterForMessage value;
			this._counterByModuleId.TryGetValue(moduleMessageId, out value);
			value.CountReadMessage(messageLength);
			this._counterByModuleId[moduleMessageId] = value;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x004B0120 File Offset: 0x004AE320
		public void CountSentModuleMessage(int moduleMessageId, int messageLength)
		{
			NetDiagnosticsUI.CounterForMessage value;
			this._counterByModuleId.TryGetValue(moduleMessageId, out value);
			value.CountSentMessage(messageLength);
			this._counterByModuleId[moduleMessageId] = value;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x004B0154 File Offset: 0x004AE354
		public void Draw(SpriteBatch spriteBatch)
		{
			int num = this._counterByMessageId.Length + this._counterByModuleId.Count;
			for (int i = 0; i <= num / 51; i++)
			{
				if (i == 0)
				{
					Utils.DrawInvBG(spriteBatch, 190 + 400 * i, 90, 390, 703, default(Color));
					DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Language.GetTextValue("tModLoader.PressXToClose", 119), new Vector2(200f, 96f), Color.White, 0f, default(Vector2), 1f, 0, 0f);
				}
				else
				{
					Utils.DrawInvBG(spriteBatch, 190 + 400 * i, 110, 390, 683, default(Color));
				}
			}
			Vector2 position = default(Vector2);
			for (int j = 0; j < this._counterByMessageId.Length; j++)
			{
				int num2 = j / 51;
				int num3 = j - num2 * 51;
				position.X = (float)(200 + num2 * 400);
				position.Y = (float)(120 + num3 * 13);
				this.DrawCounter(spriteBatch, ref this._counterByMessageId[j], j.ToString(), position);
			}
			int num4 = this._counterByMessageId.Length + 1;
			foreach (KeyValuePair<int, NetDiagnosticsUI.CounterForMessage> item in this._counterByModuleId)
			{
				int num5 = num4 / 51;
				int num6 = num4 - num5 * 51;
				position.X = (float)(200 + num5 * 400);
				position.Y = (float)(120 + num6 * 13);
				NetDiagnosticsUI.CounterForMessage counter = item.Value;
				this.DrawCounter(spriteBatch, ref counter, ".." + item.Key.ToString(), position);
				num4++;
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x004B0358 File Offset: 0x004AE558
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
			string text3 = title + ": ";
			float num = Utils.Remap((float)counter.bytesReceived, 0f, (float)this._highestFoundReadBytes, 0f, 1f, true);
			Color color = Main.hslToRgb(0.3f * (1f - num), 1f, 0.5f, byte.MaxValue);
			if (counter.exemptFromBadScoreTest)
			{
				color = Color.White;
			}
			string text2 = text3;
			this.DrawText(spriteBatch, text2, pos, color);
			pos.X += 30f;
			string str = "rx:";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<int>(counter.timesReceived);
			text2 = str + defaultInterpolatedStringHandler.ToStringAndClear();
			this.DrawText(spriteBatch, text2, pos, color);
			pos.X += 70f;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<int>(counter.bytesReceived);
			text2 = defaultInterpolatedStringHandler.ToStringAndClear();
			this.DrawText(spriteBatch, text2, pos, color);
			pos.X += 70f;
			text2 = text3;
			this.DrawText(spriteBatch, text2, pos, color);
			pos.X += 30f;
			string str2 = "tx:";
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<int>(counter.timesSent);
			text2 = str2 + defaultInterpolatedStringHandler.ToStringAndClear();
			this.DrawText(spriteBatch, text2, pos, color);
			pos.X += 70f;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<int>(counter.bytesSent);
			text2 = defaultInterpolatedStringHandler.ToStringAndClear();
			this.DrawText(spriteBatch, text2, pos, color);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x004B0524 File Offset: 0x004AE724
		private void DrawText(SpriteBatch spriteBatch, string text, Vector2 pos, Color color)
		{
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, pos, color, 0f, Vector2.Zero, 0.7f, 0, 0f);
		}

		// Token: 0x0400110E RID: 4366
		private NetDiagnosticsUI.CounterForMessage[] _counterByMessageId = new NetDiagnosticsUI.CounterForMessage[(int)(MessageID.Count + 1)];

		// Token: 0x0400110F RID: 4367
		private Dictionary<int, NetDiagnosticsUI.CounterForMessage> _counterByModuleId = new Dictionary<int, NetDiagnosticsUI.CounterForMessage>();

		// Token: 0x04001110 RID: 4368
		private int _highestFoundReadBytes = 1;

		// Token: 0x04001111 RID: 4369
		private int _highestFoundReadCount = 1;

		// Token: 0x02000870 RID: 2160
		private struct CounterForMessage
		{
			// Token: 0x0600516D RID: 20845 RVA: 0x00697433 File Offset: 0x00695633
			public void Reset()
			{
				this.timesReceived = 0;
				this.timesSent = 0;
				this.bytesReceived = 0;
				this.bytesSent = 0;
			}

			// Token: 0x0600516E RID: 20846 RVA: 0x00697451 File Offset: 0x00695651
			public void CountReadMessage(int messageLength)
			{
				this.timesReceived++;
				this.bytesReceived += messageLength;
			}

			// Token: 0x0600516F RID: 20847 RVA: 0x0069746F File Offset: 0x0069566F
			public void CountSentMessage(int messageLength)
			{
				this.timesSent++;
				this.bytesSent += messageLength;
			}

			// Token: 0x04006987 RID: 27015
			public int timesReceived;

			// Token: 0x04006988 RID: 27016
			public int timesSent;

			// Token: 0x04006989 RID: 27017
			public int bytesReceived;

			// Token: 0x0400698A RID: 27018
			public int bytesSent;

			// Token: 0x0400698B RID: 27019
			public bool exemptFromBadScoreTest;
		}
	}
}
