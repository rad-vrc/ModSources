using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.WorldChanges
{
	// Token: 0x02000216 RID: 534
	public class TimeRateModifiers : ModSystem
	{
		// Token: 0x06000D07 RID: 3335 RVA: 0x00065254 File Offset: 0x00063454
		public override void ModifyTimeRate(ref double timeRate, ref double tileUpdateRate, ref double eventUpdateRate)
		{
			QoLCPlayer mPlayer;
			if (Main.LocalPlayer.TryGetModPlayer<QoLCPlayer>(out mPlayer) && mPlayer.pausePedestal && Main.LocalPlayer.active && !Main.dedServ && !Main.gameMenu)
			{
				if (CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().Enabled)
				{
					return;
				}
				timeRate = 0.0;
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000652B0 File Offset: 0x000634B0
		public override void PreUpdateTime()
		{
			if (Main.LocalPlayer.GetModPlayer<QoLCPlayer>().sunPedestal && !Main.dayTime)
			{
				Main.dayTime = true;
				Main.time = 0.0;
			}
			if (Main.LocalPlayer.GetModPlayer<QoLCPlayer>().moonPedestal && Main.dayTime)
			{
				Main.dayTime = false;
				Main.time = 0.0;
			}
			if (!Main.dayTime && Main.LocalPlayer.GetModPlayer<QoLCPlayer>().bloodMoonPedestal)
			{
				Main.bloodMoon = true;
			}
			if (Main.dayTime && Main.LocalPlayer.GetModPlayer<QoLCPlayer>().eclipsePedestal)
			{
				Main.eclipse = true;
			}
		}
	}
}
