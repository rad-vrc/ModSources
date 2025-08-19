using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x0200035D RID: 861
	public class VisionEffect : IPermanentBuff
	{
		// Token: 0x060012E7 RID: 4839 RVA: 0x0008D314 File Offset: 0x0008B514
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new BiomeSightEffect().ApplyEffect(player);
			new DangersenseEffect().ApplyEffect(player);
			new HunterEffect().ApplyEffect(player);
			new InvisibilityEffect().ApplyEffect(player);
			new NightOwlEffect().ApplyEffect(player);
			new ShineEffect().ApplyEffect(player);
			new SpelunkerEffect().ApplyEffect(player);
		}
	}
}
