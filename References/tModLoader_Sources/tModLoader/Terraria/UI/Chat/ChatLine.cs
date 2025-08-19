using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
	// Token: 0x020000C0 RID: 192
	public class ChatLine
	{
		// Token: 0x06001670 RID: 5744 RVA: 0x004B407E File Offset: 0x004B227E
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

		// Token: 0x06001671 RID: 5745 RVA: 0x004B40A8 File Offset: 0x004B22A8
		public void Copy(ChatLine other)
		{
			this.needsParsing = other.needsParsing;
			this.parsingPixelLimit = other.parsingPixelLimit;
			this.originalText = other.originalText;
			this.parsedText = other.parsedText;
			this.showTime = other.showTime;
			this.color = other.color;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x004B40FD File Offset: 0x004B22FD
		public void FlagAsNeedsReprocessing()
		{
			this.needsParsing = true;
		}

		// Token: 0x0400129A RID: 4762
		public Color color = Color.White;

		// Token: 0x0400129B RID: 4763
		public int showTime;

		// Token: 0x0400129C RID: 4764
		public string originalText = "";

		// Token: 0x0400129D RID: 4765
		public TextSnippet[] parsedText = new TextSnippet[0];

		// Token: 0x0400129E RID: 4766
		private int? parsingPixelLimit;

		// Token: 0x0400129F RID: 4767
		private bool needsParsing;
	}
}
