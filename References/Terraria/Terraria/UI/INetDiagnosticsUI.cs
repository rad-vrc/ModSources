using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.UI
{
	// Token: 0x02000087 RID: 135
	public interface INetDiagnosticsUI
	{
		// Token: 0x060011FE RID: 4606
		void Reset();

		// Token: 0x060011FF RID: 4607
		void Draw(SpriteBatch spriteBatch);

		// Token: 0x06001200 RID: 4608
		void CountReadMessage(int messageId, int messageLength);

		// Token: 0x06001201 RID: 4609
		void CountSentMessage(int messageId, int messageLength);

		// Token: 0x06001202 RID: 4610
		void CountReadModuleMessage(int moduleMessageId, int messageLength);

		// Token: 0x06001203 RID: 4611
		void CountSentModuleMessage(int moduleMessageId, int messageLength);
	}
}
