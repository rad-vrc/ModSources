using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000540 RID: 1344
	public interface IChatMonitor
	{
		// Token: 0x06003FEF RID: 16367
		void NewText(string newText, byte R = 255, byte G = 255, byte B = 255);

		// Token: 0x06003FF0 RID: 16368
		void NewTextMultiline(string text, bool force = false, Color c = default(Color), int WidthLimit = -1);

		// Token: 0x06003FF1 RID: 16369
		void DrawChat(bool drawingPlayerChat);

		// Token: 0x06003FF2 RID: 16370
		void Clear();

		// Token: 0x06003FF3 RID: 16371
		void Update();

		// Token: 0x06003FF4 RID: 16372
		void Offset(int linesOffset);

		// Token: 0x06003FF5 RID: 16373
		void ResetOffset();

		// Token: 0x06003FF6 RID: 16374
		void OnResolutionChange();
	}
}
