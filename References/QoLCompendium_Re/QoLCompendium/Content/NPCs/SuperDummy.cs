using System;
using Microsoft.Xna.Framework;
using QoLCompendium.Core.Changes.NPCChanges;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Content.NPCs
{
	// Token: 0x020001FA RID: 506
	public class SuperDummy : ModNPC
	{
		// Token: 0x06000B59 RID: 2905 RVA: 0x00049D64 File Offset: 0x00047F64
		public override void SetDefaults()
		{
			base.NPC.CloneDefaults(488);
			base.NPC.lifeMax = int.MaxValue;
			base.NPC.aiStyle = -1;
			base.NPC.width = 28;
			base.NPC.height = 50;
			base.NPC.immortal = false;
			base.NPC.npcSlots = 0f;
			base.NPC.dontCountMe = true;
			base.NPC.noGravity = true;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return new bool?(false);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00049DEC File Offset: 0x00047FEC
		public override void OnSpawn(IEntitySource source)
		{
			base.NPC.life = (base.NPC.lifeMax = int.MaxValue);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00049E18 File Offset: 0x00048018
		public override void AI()
		{
			base.NPC.life = (base.NPC.lifeMax = int.MaxValue);
			if (SpawnRateEdits.AnyBossAlive())
			{
				base.NPC.life = 0;
				base.NPC.HitEffect(0, 10.0, null);
				base.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0f, null, false, 0f, true);
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0000404D File Offset: 0x0000224D
		public override bool CheckDead()
		{
			return false;
		}
	}
}
