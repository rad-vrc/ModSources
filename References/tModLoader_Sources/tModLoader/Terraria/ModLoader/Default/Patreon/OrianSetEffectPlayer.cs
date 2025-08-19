using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000311 RID: 785
	internal class OrianSetEffectPlayer : ModPlayer
	{
		// Token: 0x06002E9B RID: 11931 RVA: 0x00532198 File Offset: 0x00530398
		public override void ResetEffects()
		{
			this.IsActive = false;
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x005321A4 File Offset: 0x005303A4
		public override void PostUpdate()
		{
			if (!this.IsActive)
			{
				return;
			}
			Player player = base.Player;
			Vector2 playerCenter = player.Center;
			Main.npc.Any((NPC x) => x != Main.npc[Main.maxNPCs] && x.active && !x.friendly && !NPCID.Sets.TownCritter[x.type] && x.type != 488 && x.WithinRange(player.position, 300f));
			float maxIntensity = 0f;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.damage > 0 && !npc.friendly && !NPCID.Sets.TownCritter[npc.type] && npc.type != 488)
				{
					float distanceSquared = npc.DistanceSQ(playerCenter);
					float intensity = 1f - MathF.Min(1f, distanceSquared / 262144f);
					intensity *= intensity;
					maxIntensity = MathF.Max(maxIntensity, intensity);
				}
			}
			if (maxIntensity > 0f)
			{
				float pulse = MathF.Sin(Main.GameUpdateCount / 17f) * 0.25f + 0.75f;
				Lighting.AddLight(playerCenter, Color.DeepSkyBlue.ToVector3() * maxIntensity * pulse * 1.5f);
			}
		}

		// Token: 0x04001C7E RID: 7294
		public bool IsActive;
	}
}
