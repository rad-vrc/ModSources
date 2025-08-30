using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace PetRenamer
{
	// Token: 0x02000002 RID: 2
	public class Config : ModConfig
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override ConfigScope Mode
		{
			get
			{
				return ConfigScope.ClientSide;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002053 File Offset: 0x00000253
		public static Config Instance
		{
			get
			{
				return ModContent.GetInstance<Config>();
			}
		}

		// Token: 0x04000001 RID: 1
		[DefaultValue(true)]
		public bool EnableChatAutofill;

		// Token: 0x04000002 RID: 2
		[ReloadRequired]
		[DefaultValue(true)]
		public bool ShowPetNameInSelectScreen;
	}
}
