using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.UI
{
	// Token: 0x020000AE RID: 174
	public class LegacyNetDiagnosticsUI : INetDiagnosticsUI
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x004AFB24 File Offset: 0x004ADD24
		public void Reset()
		{
			LegacyNetDiagnosticsUI.rxMsg = 0;
			LegacyNetDiagnosticsUI.rxData = 0;
			LegacyNetDiagnosticsUI.txMsg = 0;
			LegacyNetDiagnosticsUI.txData = 0;
			for (int i = 0; i < LegacyNetDiagnosticsUI.maxMsg; i++)
			{
				LegacyNetDiagnosticsUI.rxMsgType[i] = 0;
				LegacyNetDiagnosticsUI.rxDataType[i] = 0;
				LegacyNetDiagnosticsUI.txMsgType[i] = 0;
				LegacyNetDiagnosticsUI.txDataType[i] = 0;
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x004AFB79 File Offset: 0x004ADD79
		public void CountReadMessage(int messageId, int messageLength)
		{
			LegacyNetDiagnosticsUI.rxMsg++;
			LegacyNetDiagnosticsUI.rxData += messageLength;
			LegacyNetDiagnosticsUI.rxMsgType[messageId]++;
			LegacyNetDiagnosticsUI.rxDataType[messageId] += messageLength;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x004AFBB3 File Offset: 0x004ADDB3
		public void CountSentMessage(int messageId, int messageLength)
		{
			LegacyNetDiagnosticsUI.txMsg++;
			LegacyNetDiagnosticsUI.txData += messageLength;
			LegacyNetDiagnosticsUI.txMsgType[messageId]++;
			LegacyNetDiagnosticsUI.txDataType[messageId] += messageLength;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x004AFBED File Offset: 0x004ADDED
		public void Draw(SpriteBatch spriteBatch)
		{
			LegacyNetDiagnosticsUI.DrawTitles(spriteBatch);
			LegacyNetDiagnosticsUI.DrawMesageLines(spriteBatch);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x004AFBFC File Offset: 0x004ADDFC
		private static void DrawMesageLines(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < LegacyNetDiagnosticsUI.maxMsg; i++)
			{
				int num = 200;
				int num2 = 120;
				int num3 = i / 50;
				num += num3 * 400;
				num2 += (i - num3 * 50) * 13;
				LegacyNetDiagnosticsUI.PrintNetDiagnosticsLineForMessage(spriteBatch, i, num, num2);
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x004AFC48 File Offset: 0x004ADE48
		private static void DrawTitles(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < 4; i++)
			{
				string text = "";
				int num = 20;
				int num2 = 220;
				switch (i)
				{
				case 0:
				{
					string str = "RX Msgs: ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<int>(LegacyNetDiagnosticsUI.rxMsg, "0,0");
					text = str + defaultInterpolatedStringHandler.ToStringAndClear();
					num2 += i * 20;
					break;
				}
				case 1:
				{
					string str2 = "RX Bytes: ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<int>(LegacyNetDiagnosticsUI.rxData, "0,0");
					text = str2 + defaultInterpolatedStringHandler.ToStringAndClear();
					num2 += i * 20;
					break;
				}
				case 2:
				{
					string str3 = "TX Msgs: ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<int>(LegacyNetDiagnosticsUI.txMsg, "0,0");
					text = str3 + defaultInterpolatedStringHandler.ToStringAndClear();
					num2 += i * 20;
					break;
				}
				case 3:
				{
					string str4 = "TX Bytes: ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<int>(LegacyNetDiagnosticsUI.txData, "0,0");
					text = str4 + defaultInterpolatedStringHandler.ToStringAndClear();
					num2 += i * 20;
					break;
				}
				}
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)num, (float)num2), Color.White, 0f, default(Vector2), 1f, 0, 0f);
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x004AFDA0 File Offset: 0x004ADFA0
		private static void PrintNetDiagnosticsLineForMessage(SpriteBatch spriteBatch, int msgId, int x, int y)
		{
			float scale = 0.7f;
			string text = msgId.ToString() + ": ";
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), scale, 0, 0f);
			x += 30;
			string str = "rx:";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<int>(LegacyNetDiagnosticsUI.rxMsgType[msgId], "0,0");
			text = str + defaultInterpolatedStringHandler.ToStringAndClear();
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), scale, 0, 0f);
			x += 70;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<int>(LegacyNetDiagnosticsUI.rxDataType[msgId], "0,0");
			text = defaultInterpolatedStringHandler.ToStringAndClear();
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), scale, 0, 0f);
			x += 70;
			text = msgId.ToString() + ": ";
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), scale, 0, 0f);
			x += 30;
			string str2 = "tx:";
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<int>(LegacyNetDiagnosticsUI.txMsgType[msgId], "0,0");
			text = str2 + defaultInterpolatedStringHandler.ToStringAndClear();
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), scale, 0, 0f);
			x += 70;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<int>(LegacyNetDiagnosticsUI.txDataType[msgId], "0,0");
			text = defaultInterpolatedStringHandler.ToStringAndClear();
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), scale, 0, 0f);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x004AFFD7 File Offset: 0x004AE1D7
		public void CountReadModuleMessage(int moduleMessageId, int messageLength)
		{
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x004AFFD9 File Offset: 0x004AE1D9
		public void CountSentModuleMessage(int moduleMessageId, int messageLength)
		{
		}

		// Token: 0x04001104 RID: 4356
		public static bool netDiag;

		// Token: 0x04001105 RID: 4357
		public static int txData = 0;

		// Token: 0x04001106 RID: 4358
		public static int rxData = 0;

		// Token: 0x04001107 RID: 4359
		public static int txMsg = 0;

		// Token: 0x04001108 RID: 4360
		public static int rxMsg = 0;

		// Token: 0x04001109 RID: 4361
		private static readonly int maxMsg = (int)(MessageID.Count + 1);

		// Token: 0x0400110A RID: 4362
		public static int[] rxMsgType = new int[LegacyNetDiagnosticsUI.maxMsg];

		// Token: 0x0400110B RID: 4363
		public static int[] rxDataType = new int[LegacyNetDiagnosticsUI.maxMsg];

		// Token: 0x0400110C RID: 4364
		public static int[] txMsgType = new int[LegacyNetDiagnosticsUI.maxMsg];

		// Token: 0x0400110D RID: 4365
		public static int[] txDataType = new int[LegacyNetDiagnosticsUI.maxMsg];
	}
}
