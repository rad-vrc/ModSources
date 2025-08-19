using System;
using System.Collections.Generic;
using System.IO;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
	// Token: 0x020002C0 RID: 704
	public interface ICreativePower
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600225C RID: 8796
		// (set) Token: 0x0600225D RID: 8797
		ushort PowerId { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600225E RID: 8798
		// (set) Token: 0x0600225F RID: 8799
		string ServerConfigName { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06002260 RID: 8800
		// (set) Token: 0x06002261 RID: 8801
		PowerPermissionLevel CurrentPermissionLevel { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06002262 RID: 8802
		// (set) Token: 0x06002263 RID: 8803
		PowerPermissionLevel DefaultPermissionLevel { get; set; }

		// Token: 0x06002264 RID: 8804
		void DeserializeNetMessage(BinaryReader reader, int userId);

		// Token: 0x06002265 RID: 8805
		void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements);

		// Token: 0x06002266 RID: 8806
		bool GetIsUnlocked();
	}
}
