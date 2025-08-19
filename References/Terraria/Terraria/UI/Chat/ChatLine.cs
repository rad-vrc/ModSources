using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
	// Token: 0x0200009B RID: 155
	public class ChatLine
	{
		// Token: 0x0600130E RID: 4878 RVA: 0x0049D3FC File Offset: 0x0049B5FC
		public void UpdateTimeLeft()
		{
			if (this.showTime > 0)
			{
				this.showTime--;
			}
			if (this.needsParsing)
			{
				this.needsParsing = false;
			}
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0049D424 File Offset: 0x0049B624
		public void Copy(ChatLine other)
		{
			this.needsParsing = other.needsParsing;
			this.parsingPixelLimit = other.parsingPixelLimit;
			this.originalText = other.originalText;
			this.parsedText = other.parsedText;
			this.showTime = other.showTime;
			this.color = other.color;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0049D479 File Offset: 0x0049B679
		public void FlagAsNeedsReprocessing()
		{
			this.needsParsing = true;
		}

		// Token: 0x04001068 RID: 4200
		public Color color = Color.White;

		// Token: 0x04001069 RID: 4201
		public int showTime;

		// Token: 0x0400106A RID: 4202
		public string originalText = "";

		// Token: 0x0400106B RID: 4203
		public TextSnippet[] parsedText = new TextSnippet[0];

		// Token: 0x0400106C RID: 4204
		private int? parsingPixelLimit;

		// Token: 0x0400106D RID: 4205
		private bool needsParsing;
	}
}
