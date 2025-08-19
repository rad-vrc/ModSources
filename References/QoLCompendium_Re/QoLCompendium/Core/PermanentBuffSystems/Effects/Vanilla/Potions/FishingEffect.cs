using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200036F RID: 879
	public class FishingEffect : IPermanentBuff
	{
		// Token: 0x0600130B RID: 4875 RVA: 0x0008D98D File Offset: 0x0008BB8D
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[121] && !PermanentBuffPlayer.PermanentBuffsBools[22])
			{
				player.Player.fishingSkill += 15;
				player.Player.buffImmune[121] = true;
			}
		}
	}
}
