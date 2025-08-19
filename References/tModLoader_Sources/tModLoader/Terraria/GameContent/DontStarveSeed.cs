using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Enums;

namespace Terraria.GameContent
{
	// Token: 0x02000495 RID: 1173
	public class DontStarveSeed
	{
		// Token: 0x06003911 RID: 14609 RVA: 0x005963A8 File Offset: 0x005945A8
		public static void ModifyNightColor(ref Color bgColorToSet, ref Color moonColor)
		{
			if (Main.GetMoonPhase() != MoonPhase.Full)
			{
				float fromValue = (float)(Main.time / 32400.0);
				Color value = bgColorToSet;
				Color black = Color.Black;
				Color value2 = bgColorToSet;
				float amount = Utils.Remap(fromValue, 0f, 0.5f, 0f, 1f, true);
				float amount2 = Utils.Remap(fromValue, 0.5f, 1f, 0f, 1f, true);
				Color color = Color.Lerp(Color.Lerp(value, black, amount), value2, amount2);
				bgColorToSet = color;
			}
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x00596434 File Offset: 0x00594634
		public static void ModifyMinimumLightColorAtNight(ref byte minimalLight)
		{
			switch (Main.GetMoonPhase())
			{
			case MoonPhase.Full:
				minimalLight = 45;
				break;
			case MoonPhase.ThreeQuartersAtLeft:
			case MoonPhase.ThreeQuartersAtRight:
				minimalLight = 1;
				break;
			case MoonPhase.HalfAtLeft:
			case MoonPhase.HalfAtRight:
				minimalLight = 1;
				break;
			case MoonPhase.QuarterAtLeft:
			case MoonPhase.QuarterAtRight:
				minimalLight = 1;
				break;
			case MoonPhase.Empty:
				minimalLight = 1;
				break;
			}
			if (Main.bloodMoon)
			{
				minimalLight = Utils.Max<byte>(new byte[]
				{
					minimalLight,
					35
				});
			}
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x005964A5 File Offset: 0x005946A5
		public static void FixBiomeDarkness(ref Color bgColor, ref int R, ref int G, ref int B)
		{
			if (Main.dontStarveWorld)
			{
				R = (int)((byte)Math.Min((int)bgColor.R, R));
				G = (int)((byte)Math.Min((int)bgColor.G, G));
				B = (int)((byte)Math.Min((int)bgColor.B, B));
			}
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x005964DE File Offset: 0x005946DE
		public static void Initialize()
		{
			Action<Player> value;
			if ((value = DontStarveSeed.<>O.<0>__Hook_OnEnterWorld) == null)
			{
				value = (DontStarveSeed.<>O.<0>__Hook_OnEnterWorld = new Action<Player>(DontStarveSeed.Hook_OnEnterWorld));
			}
			Player.Hooks.OnEnterWorld += value;
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x00596500 File Offset: 0x00594700
		private static void Hook_OnEnterWorld(Player player)
		{
			player.UpdateStarvingState(false);
		}

		// Token: 0x02000B9E RID: 2974
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040076A3 RID: 30371
			public static Action<Player> <0>__Hook_OnEnterWorld;
		}
	}
}
