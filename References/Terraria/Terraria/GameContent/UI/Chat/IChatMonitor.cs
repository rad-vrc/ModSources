using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000399 RID: 921
	public interface IChatMonitor
	{
		// Token: 0x0600298C RID: 10636
		void NewText(string newText, byte R = 255, byte G = 255, byte B = 255);

		// Token: 0x0600298D RID: 10637
		void NewTextMultiline(string text, bool force = false, Color c = default(Color), int WidthLimit = -1);

		// Token: 0x0600298E RID: 10638
		void DrawChat(bool drawingPlayerChat);

		// Token: 0x0600298F RID: 10639
		void Clear();

		// Token: 0x06002990 RID: 10640
		void Update();

		// Token: 0x06002991 RID: 10641
		void Offset(int linesOffset);

		// Token: 0x06002992 RID: 10642
		void ResetOffset();

		// Token: 0x06002993 RID: 10643
		void OnResolutionChange();
	}
}
