using System;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200031A RID: 794
	public class NPCNetIdBestiaryInfoElement : IBestiaryInfoElement, IBestiaryEntryDisplayIndex
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x00558F77 File Offset: 0x00557177
		// (set) Token: 0x0600242D RID: 9261 RVA: 0x00558F7F File Offset: 0x0055717F
		public int NetId { get; private set; }

		// Token: 0x0600242E RID: 9262 RVA: 0x00558F88 File Offset: 0x00557188
		public NPCNetIdBestiaryInfoElement(int npcNetId)
		{
			this.NetId = npcNetId;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x00558F97 File Offset: 0x00557197
		public int BestiaryDisplayIndex
		{
			get
			{
				return ContentSamples.NpcBestiarySortingId[this.NetId];
			}
		}
	}
}
