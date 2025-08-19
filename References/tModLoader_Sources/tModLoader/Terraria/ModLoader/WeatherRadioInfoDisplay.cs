using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000227 RID: 551
	public class WeatherRadioInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x0050B6CF File Offset: 0x005098CF
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_1";
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x0050B6D6 File Offset: 0x005098D6
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.96";
			}
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x0050B6DD File Offset: 0x005098DD
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accWeatherRadio;
		}
	}
}
