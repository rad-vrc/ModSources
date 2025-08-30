using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PetRenamer
{
	// Token: 0x02000004 RID: 4
	internal class PRBuff : GlobalBuff
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002359 File Offset: 0x00000559
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002360 File Offset: 0x00000560
		public static LocalizedText NameText { get; private set; }

		// Token: 0x06000011 RID: 17 RVA: 0x00002368 File Offset: 0x00000568
		public override void Load()
		{
			string category = "Buffs.PetBuff.";
			if (PRBuff.NameText == null)
			{
				PRBuff.NameText = Language.GetOrRegister(base.Mod.GetLocalizationKey(category + "Name"), null);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023A8 File Offset: 0x000005A8
		public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
		{
			PRPlayer petPlayer = Main.LocalPlayer.GetModPlayer<PRPlayer>();
			if (Main.vanityPet[type] && petPlayer.petNameVanity != string.Empty)
			{
				tip = tip + "\n" + PRBuff.NameText.Format(new object[]
				{
					petPlayer.petNameVanity
				});
				return;
			}
			if (Main.lightPet[type] && petPlayer.petNameLight != string.Empty)
			{
				tip = tip + "\n" + PRBuff.NameText.Format(new object[]
				{
					petPlayer.petNameLight
				});
			}
		}
	}
}
