using System;
using Microsoft.Xna.Framework;
using Terraria.Enums;

namespace Terraria.GameContent
{
	// Token: 0x020001D0 RID: 464
	public class DontStarveSeed
	{
		// Token: 0x06001BF4 RID: 7156 RVA: 0x004F0C48 File Offset: 0x004EEE48
		public static void ModifyNightColor(ref Color bgColorToSet, ref Color moonColor)
		{
			if (Main.GetMoonPhase() == MoonPhase.Full)
			{
				return;
			}
			float fromValue = (float)(Main.time / 32400.0);
			Color value = bgColorToSet;
			Color black = Color.Black;
			Color value2 = bgColorToSet;
			float amount = Utils.Remap(fromValue, 0f, 0.5f, 0f, 1f, true);
			float amount2 = Utils.Remap(fromValue, 0.5f, 1f, 0f, 1f, true);
			Color color = Color.Lerp(Color.Lerp(value, black, amount), value2, amount2);
			bgColorToSet = color;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x004F0CD4 File Offset: 0x004EEED4
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

		// Token: 0x06001BF6 RID: 7158 RVA: 0x004F0D45 File Offset: 0x004EEF45
		public static void FixBiomeDarkness(ref Color bgColor, ref int R, ref int G, ref int B)
		{
			if (!Main.dontStarveWorld)
			{
				return;
			}
			R = (int)((byte)Math.Min((int)bgColor.R, R));
			G = (int)((byte)Math.Min((int)bgColor.G, G));
			B = (int)((byte)Math.Min((int)bgColor.B, B));
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x004F0D7F File Offset: 0x004EEF7F
		public static void Initialize()
		{
			Player.Hooks.OnEnterWorld += DontStarveSeed.Hook_OnEnterWorld;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x004F0D92 File Offset: 0x004EEF92
		private static void Hook_OnEnterWorld(Player player)
		{
			player.UpdateStarvingState(false);
		}
	}
}
