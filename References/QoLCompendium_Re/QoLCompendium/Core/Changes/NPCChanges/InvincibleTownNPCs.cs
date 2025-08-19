using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x0200023E RID: 574
	public class InvincibleTownNPCs : GlobalNPC
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00002430 File Offset: 0x00000630
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0006D260 File Offset: 0x0006B460
		public override void SetDefaults(NPC npc)
		{
			bool dontTakeDamageFromHostiles = npc.dontTakeDamageFromHostiles;
			if (npc.type == 548 && QoLCompendium.mainConfig.TownNPCsDontDie)
			{
				npc.dontTakeDamageFromHostiles = false;
				return;
			}
			npc.dontTakeDamageFromHostiles = dontTakeDamageFromHostiles;
			if (npc.friendly && QoLCompendium.mainConfig.TownNPCsDontDie)
			{
				npc.dontTakeDamageFromHostiles = true;
				return;
			}
			npc.dontTakeDamageFromHostiles = dontTakeDamageFromHostiles;
		}
	}
}
