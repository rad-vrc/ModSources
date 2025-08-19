using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena
{
	// Token: 0x02000398 RID: 920
	public class GardenGnomeEffect : IPermanentBuff
	{
		// Token: 0x0600135D RID: 4957 RVA: 0x0008E8D2 File Offset: 0x0008CAD2
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!PermanentBuffPlayer.PermanentBuffsBools[2])
			{
				player.Player.luck += 0.2f;
			}
		}
	}
}
