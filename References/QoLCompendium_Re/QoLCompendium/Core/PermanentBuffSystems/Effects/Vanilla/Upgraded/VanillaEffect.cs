using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x0200035C RID: 860
	public class VanillaEffect : IPermanentBuff
	{
		// Token: 0x060012E5 RID: 4837 RVA: 0x0008D2A4 File Offset: 0x0008B4A4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			new AquaticEffect().ApplyEffect(player);
			new ArenaEffect().ApplyEffect(player);
			new ConstructionEffect().ApplyEffect(player);
			new DamageEffect().ApplyEffect(player);
			new DefenseEffect().ApplyEffect(player);
			new MovementEffect().ApplyEffect(player);
			new StationEffect().ApplyEffect(player);
			new TrawlerEffect().ApplyEffect(player);
			new VisionEffect().ApplyEffect(player);
		}
	}
}
