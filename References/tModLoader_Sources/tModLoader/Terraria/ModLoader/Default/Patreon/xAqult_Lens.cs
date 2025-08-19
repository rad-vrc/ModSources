using System;
using System.Runtime.CompilerServices;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000338 RID: 824
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Face
	})]
	internal class xAqult_Lens : PatreonItem
	{
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x00532F9F File Offset: 0x0053119F
		public override LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x00532FCC File Offset: 0x005311CC
		public override void Load()
		{
			if (Main.netMode == 2)
			{
				return;
			}
			Mod mod = base.Mod;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 2);
			defaultInterpolatedStringHandler.AppendFormatted(this.Texture);
			defaultInterpolatedStringHandler.AppendLiteral("_Blue_");
			defaultInterpolatedStringHandler.AppendFormatted<EquipType>(EquipType.Face);
			EquipLoader.AddEquipTexture(mod, defaultInterpolatedStringHandler.ToStringAndClear(), EquipType.Face, null, this.Name + "_Blue", null);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x00533034 File Offset: 0x00531234
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			if (Main.netMode == 2)
			{
				return;
			}
			ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[base.Item.faceSlot] = true;
			ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[EquipLoader.GetEquipSlot(base.Mod, "xAqult_Lens_Blue", EquipType.Face)] = true;
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x00533070 File Offset: 0x00531270
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 18;
			base.Item.height = 20;
			base.Item.accessory = true;
		}
	}
}
