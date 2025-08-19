using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.UI
{
	// Token: 0x020000A5 RID: 165
	public interface INetDiagnosticsUI
	{
		// Token: 0x060014F5 RID: 5365
		void Reset();

		// Token: 0x060014F6 RID: 5366
		void Draw(SpriteBatch spriteBatch);

		// Token: 0x060014F7 RID: 5367
		void CountReadMessage(int messageId, int messageLength);

		// Token: 0x060014F8 RID: 5368
		void CountSentMessage(int messageId, int messageLength);

		// Token: 0x060014F9 RID: 5369
		void CountReadModuleMessage(int moduleMessageId, int messageLength);

		// Token: 0x060014FA RID: 5370
		void CountSentModuleMessage(int moduleMessageId, int messageLength);
	}
}
