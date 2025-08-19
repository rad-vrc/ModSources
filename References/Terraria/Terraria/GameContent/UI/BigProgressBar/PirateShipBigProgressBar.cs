using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003B4 RID: 948
	public class PirateShipBigProgressBar : IBigProgressBar
	{
		// Token: 0x06002A03 RID: 10755 RVA: 0x00596550 File Offset: 0x00594750
		public PirateShipBigProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x0059657C File Offset: 0x0059477C
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active || npc.type != 491)
			{
				if (!this.TryFindingAnotherPirateShipPiece(ref info))
				{
					return false;
				}
				npc = Main.npc[info.npcIndexToAimAt];
			}
			int num = 0;
			this._referenceDummy.SetDefaults(492, npc.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax * 4;
			float num2 = 0f;
			for (int i = 0; i < 4; i++)
			{
				int num3 = (int)npc.ai[i];
				if (Main.npc.IndexInRange(num3))
				{
					NPC npc2 = Main.npc[num3];
					if (npc2.active && npc2.type == 492)
					{
						num2 += (float)npc2.life;
					}
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x0059666C File Offset: 0x0059486C
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[491];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x005966C0 File Offset: 0x005948C0
		private bool TryFindingAnotherPirateShipPiece(ref BigProgressBarInfo info)
		{
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && this.ValidIds.Contains(npc.type))
				{
					info.npcIndexToAimAt = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004CD5 RID: 19669
		private BigProgressBarCache _cache;

		// Token: 0x04004CD6 RID: 19670
		private NPC _referenceDummy;

		// Token: 0x04004CD7 RID: 19671
		private HashSet<int> ValidIds = new HashSet<int>
		{
			491
		};
	}
}
