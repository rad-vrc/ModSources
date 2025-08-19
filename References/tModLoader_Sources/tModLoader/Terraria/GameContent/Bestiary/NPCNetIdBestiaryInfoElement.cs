using System;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200069E RID: 1694
	public class NPCNetIdBestiaryInfoElement : IBestiaryInfoElement, IBestiaryEntryDisplayIndex
	{
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600482C RID: 18476 RVA: 0x00647A34 File Offset: 0x00645C34
		// (set) Token: 0x0600482D RID: 18477 RVA: 0x00647A3C File Offset: 0x00645C3C
		public int NetId { get; private set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600482E RID: 18478 RVA: 0x00647A45 File Offset: 0x00645C45
		public int BestiaryDisplayIndex
		{
			get
			{
				return ContentSamples.NpcBestiarySortingId[this.NetId];
			}
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x00647A57 File Offset: 0x00645C57
		public NPCNetIdBestiaryInfoElement(int npcNetId)
		{
			this.NetId = npcNetId;
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x00647A66 File Offset: 0x00645C66
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}
	}
}
