using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200022E RID: 558
	public class DummyInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x0050B81F File Offset: 0x00509A1F
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_8";
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x0050B826 File Offset: 0x00509A26
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.101";
			}
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x0050B82D File Offset: 0x00509A2D
		public override bool Active()
		{
			return false;
		}
	}
}
