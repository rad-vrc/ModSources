using BInfoAcc.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BInfoAcc.Common
{
	class BInfoAccShop : GlobalNPC
	{
		public override void ModifyShop(NPCShop shop)
		{
			if(shop.NpcType == NPCID.GoblinTinkerer)
            {
				shop.Add(ModContent.ItemType<EngiRegistry>());
            }
            if (shop.NpcType == NPCID.ArmsDealer)
            {
                shop.Add(ModContent.ItemType<WantedPoster>());
            }
            if (shop.NpcType == NPCID.SkeletonMerchant)
			{
                var safteyScannerCondition = new Condition("Mods.BInfoAcc.CommonItemtooltip.ScannerCondition", () => 
				Condition.EclipseOrBloodMoon.IsMet() || 
				((Condition.InRockLayerHeight.IsMet() || Condition.InDirtLayerHeight.IsMet() || Condition.InUnderworldHeight.IsMet()) && 
				(Condition.InUndergroundDesert.IsMet() || Condition.InSnow.IsMet() || Condition.InJungle.IsMet() || 
				Condition.InHallow.IsMet() || Condition.InCorrupt.IsMet() || Condition.InCrimson.IsMet()))
				);

				shop.Add(ModContent.ItemType<SafteyScanner>(),safteyScannerCondition);
			}

			if (shop.NpcType == NPCID.Merchant)
			{
				var merchantSalesCondition = new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantSaleCondition", () => ModContent.GetInstance<ConfigServer>().easySell);

				shop.Add(ModContent.ItemType<SmartHeart>(),new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionG", () => 
														merchantSalesCondition.IsMet() && Condition.MoonPhaseWaningGibbous.IsMet()));
                shop.Add(ModContent.ItemType<Magimeter>(), new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionG", () =>
                                                        merchantSalesCondition.IsMet() && Condition.MoonPhaseWaxingCrescent.IsMet()));
                shop.Add(ModContent.ItemType<HitMarker>(), new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionG", () =>
                                                        merchantSalesCondition.IsMet() && Condition.MoonPhaseWaxingCrescent.IsMet()));


                shop.Add(ModContent.ItemType<AttendanceLog>(),new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionQ", () =>
														merchantSalesCondition.IsMet() && Condition.MoonPhaseThirdQuarter.IsMet()));
				shop.Add(ModContent.ItemType<EngiRegistry>(),new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionQ", () =>
														merchantSalesCondition.IsMet() && Condition.MoonPhaseFirstQuarter.IsMet()));
				shop.Add(ModContent.ItemType<FortuneMirror>(),new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionQ", () =>
														merchantSalesCondition.IsMet() && Condition.MoonPhaseWaxingGibbous.IsMet()));


                shop.Add(ModContent.ItemType<SafteyScanner>(), new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionC", () =>
                                                        merchantSalesCondition.IsMet() && Condition.MoonPhaseWaningCrescent.IsMet()));
                shop.Add(ModContent.ItemType<BiomeCrystal>(), new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionC", () =>
                                                        merchantSalesCondition.IsMet() && Condition.MoonPhaseWaningCrescent.IsMet()));
                shop.Add(ModContent.ItemType<WantedPoster>(), new Condition("Mods.BInfoAcc.CommonItemtooltip.MerchantAccConditionC", () =>
                                                        merchantSalesCondition.IsMet() && Condition.MoonPhaseWaningCrescent.IsMet()));
            }
		}
    }
}
