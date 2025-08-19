using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.UI
{
	// Token: 0x02000088 RID: 136
	public class LegacyNetDiagnosticsUI : INetDiagnosticsUI
	{
		// Token: 0x06001205 RID: 4613 RVA: 0x00490A18 File Offset: 0x0048EC18
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

		// Token: 0x06001206 RID: 4614 RVA: 0x00490A6D File Offset: 0x0048EC6D
		public void CountReadMessage(int messageId, int messageLength)
		{
			LegacyNetDiagnosticsUI.rxMsg++;
			LegacyNetDiagnosticsUI.rxData += messageLength;
			LegacyNetDiagnosticsUI.rxMsgType[messageId]++;
			LegacyNetDiagnosticsUI.rxDataType[messageId] += messageLength;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00490AA7 File Offset: 0x0048ECA7
		public void CountSentMessage(int messageId, int messageLength)
		{
			LegacyNetDiagnosticsUI.txMsg++;
			LegacyNetDiagnosticsUI.txData += messageLength;
			LegacyNetDiagnosticsUI.txMsgType[messageId]++;
			LegacyNetDiagnosticsUI.txDataType[messageId] += messageLength;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00490AE1 File Offset: 0x0048ECE1
		public void Draw(SpriteBatch spriteBatch)
		{
			LegacyNetDiagnosticsUI.DrawTitles(spriteBatch);
			LegacyNetDiagnosticsUI.DrawMesageLines(spriteBatch);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00490AF0 File Offset: 0x0048ECF0
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

		// Token: 0x0600120A RID: 4618 RVA: 0x00490B3C File Offset: 0x0048ED3C
		private static void DrawTitles(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < 4; i++)
			{
				string text = "";
				int num = 20;
				int num2 = 220;
				if (i == 0)
				{
					text = "RX Msgs: " + string.Format("{0:0,0}", LegacyNetDiagnosticsUI.rxMsg);
					num2 += i * 20;
				}
				else if (i == 1)
				{
					text = "RX Bytes: " + string.Format("{0:0,0}", LegacyNetDiagnosticsUI.rxData);
					num2 += i * 20;
				}
				else if (i == 2)
				{
					text = "TX Msgs: " + string.Format("{0:0,0}", LegacyNetDiagnosticsUI.txMsg);
					num2 += i * 20;
				}
				else if (i == 3)
				{
					text = "TX Bytes: " + string.Format("{0:0,0}", LegacyNetDiagnosticsUI.txData);
					num2 += i * 20;
				}
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)num, (float)num2), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00490C54 File Offset: 0x0048EE54
		private static void PrintNetDiagnosticsLineForMessage(SpriteBatch spriteBatch, int msgId, int x, int y)
		{
			float num = 0.7f;
			string text = msgId + ": ";
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), num, SpriteEffects.None, 0f);
			x += 30;
			text = "rx:" + string.Format("{0:0,0}", LegacyNetDiagnosticsUI.rxMsgType[msgId]);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), num, SpriteEffects.None, 0f);
			x += 70;
			text = string.Format("{0:0,0}", LegacyNetDiagnosticsUI.rxDataType[msgId]);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), num, SpriteEffects.None, 0f);
			x += 70;
			text = msgId + ": ";
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), num, SpriteEffects.None, 0f);
			x += 30;
			text = "tx:" + string.Format("{0:0,0}", LegacyNetDiagnosticsUI.txMsgType[msgId]);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), num, SpriteEffects.None, 0f);
			x += 70;
			text = string.Format("{0:0,0}", LegacyNetDiagnosticsUI.txDataType[msgId]);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)x, (float)y), Color.White, 0f, default(Vector2), num, SpriteEffects.None, 0f);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void CountReadModuleMessage(int moduleMessageId, int messageLength)
		{
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void CountSentModuleMessage(int moduleMessageId, int messageLength)
		{
		}

		// Token: 0x04000FF5 RID: 4085
		public static bool netDiag;

		// Token: 0x04000FF6 RID: 4086
		public static int txData = 0;

		// Token: 0x04000FF7 RID: 4087
		public static int rxData = 0;

		// Token: 0x04000FF8 RID: 4088
		public static int txMsg = 0;

		// Token: 0x04000FF9 RID: 4089
		public static int rxMsg = 0;

		// Token: 0x04000FFA RID: 4090
		private static readonly int maxMsg = (int)(MessageID.Count + 1);

		// Token: 0x04000FFB RID: 4091
		public static int[] rxMsgType = new int[LegacyNetDiagnosticsUI.maxMsg];

		// Token: 0x04000FFC RID: 4092
		public static int[] rxDataType = new int[LegacyNetDiagnosticsUI.maxMsg];

		// Token: 0x04000FFD RID: 4093
		public static int[] txMsgType = new int[LegacyNetDiagnosticsUI.maxMsg];

		// Token: 0x04000FFE RID: 4094
		public static int[] txDataType = new int[LegacyNetDiagnosticsUI.maxMsg];
	}
}
