using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200037B RID: 891
	public class ManaRegenerationEffect : IPermanentBuff
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x0008DF0D File Offset: 0x0008C10D
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[6] && !PermanentBuffPlayer.PermanentBuffsBools[33])
			{
				player.Player.manaRegenBuff = true;
				player.Player.buffImmune[6] = true;
			}
		}
	}
}
