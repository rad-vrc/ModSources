using System;
using System.Collections.Generic;
using System.IO;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000648 RID: 1608
	public interface ICreativePower
	{
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x0600468F RID: 18063
		// (set) Token: 0x06004690 RID: 18064
		ushort PowerId { get; set; }

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06004691 RID: 18065
		// (set) Token: 0x06004692 RID: 18066
		string ServerConfigName { get; set; }

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06004693 RID: 18067
		// (set) Token: 0x06004694 RID: 18068
		PowerPermissionLevel CurrentPermissionLevel { get; set; }

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06004695 RID: 18069
		// (set) Token: 0x06004696 RID: 18070
		PowerPermissionLevel DefaultPermissionLevel { get; set; }

		// Token: 0x06004697 RID: 18071
		void DeserializeNetMessage(BinaryReader reader, int userId);

		// Token: 0x06004698 RID: 18072
		void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements);

		// Token: 0x06004699 RID: 18073
		bool GetIsUnlocked();
	}
}
