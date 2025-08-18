using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using Humanizer;
using Terraria.GameContent.Events;
using Terraria.ID;
using System.Collections.Generic;

namespace BInfoAcc.Common
{
	[LegacyName ("LuckInfoDisplay")]
	public class InfoDisplay0 : InfoDisplay
	{
		
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().luckDisplay;
		}

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            float luckValue = Main.LocalPlayer.luck;
            luckValue = (float)Math.Round(luckValue, 2);

            if (luckValue == 0)
            {
                displayColor = InactiveInfoTextColor;
            }
            else if (luckValue > 0)
            {
                displayColor = new Color(120, 190, 120);
            }
            else if (luckValue < 0)
            {
                displayColor = new Color(190, 120, 120);
            }

            return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.LuckDisplay").FormatWith(luckValue);
        }
	}

    [LegacyName("MinionInfoDisplay")]
    public class InfoDisplay7 : InfoDisplay
	{
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().minionDisplay;
		}

		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			float minionCount = 0;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj.active && proj.minionSlots > 0f && proj.owner == Main.myPlayer)
				{
					minionCount += proj.minionSlots;
				}
			}

			minionCount = (float)Math.Round(minionCount, 2);

			int maxMinions = Main.LocalPlayer.maxMinions;

			if (minionCount == 0)
			{
				displayColor = InactiveInfoTextColor;
			}

			return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.MinionDisplay").FormatWith(minionCount, maxMinions);
        }
	}

    [LegacyName("SentryInfoDisplay")]
    public class InfoDisplay8 : InfoDisplay
	{
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().sentryDisplay;
		}

		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			int sentryCount = 0;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj.active && proj.sentry && !DD2SentryCheck(proj) && proj.owner == Main.myPlayer)
				{
					sentryCount ++;
				}
			}

			int maxSentries = Main.LocalPlayer.maxTurrets;

			if (sentryCount == 0)
			{
				displayColor = InactiveInfoTextColor;
			}

			return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SentryDisplay").FormatWith(sentryCount, maxSentries);
		}

		private static bool DD2SentryCheck(Projectile proj)
		{
			if(DD2Event.Ongoing &&
				(proj.type == ProjectileID.DD2BallistraTowerT1 || proj.type == ProjectileID.DD2BallistraTowerT2 || proj.type == ProjectileID.DD2BallistraTowerT3
				|| proj.type == ProjectileID.DD2FlameBurstTowerT1 || proj.type == ProjectileID.DD2FlameBurstTowerT2 || proj.type == ProjectileID.DD2FlameBurstTowerT3
                || proj.type == ProjectileID.DD2ExplosiveTrapT1 || proj.type == ProjectileID.DD2ExplosiveTrapT2 || proj.type == ProjectileID.DD2ExplosiveTrapT3
                || proj.type == ProjectileID.DD2LightningAuraT1 || proj.type == ProjectileID.DD2LightningAuraT2 || proj.type == ProjectileID.DD2LightningAuraT3))
			{
				return true;
			}
			return false;
		}
	}

    [LegacyName("RegenInfoDisplay")]
    public class InfoDisplay1 : InfoDisplay
	{
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().regenDisplay;
		}

		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			float lifeRegen = Main.LocalPlayer.lifeRegen;
			lifeRegen *= 0.5f;
			lifeRegen = (float)Math.Round(lifeRegen, 2);

            if (Main.LocalPlayer.statLife >= Main.LocalPlayer.statLifeMax2)
            {
                return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.RegenDisplayFull");
            }
            else if (lifeRegen == 0)
			{
				displayColor = InactiveInfoTextColor;
			}
			else if (lifeRegen > 0)
			{			
				displayColor = new Color(120, 190, 120);
			}
			else if (lifeRegen < 0)
			{
				displayColor = new Color(190, 120, 120);
			}

			return lifeRegen > 0 ? Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.RegenDisplayPos").FormatWith(lifeRegen) : Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.RegenDisplayNeg").FormatWith(lifeRegen);
        }
	}

    [LegacyName("SpawnRateInfoDisplay")]
    public class InfoDisplay3 : InfoDisplay
	{
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().spawnRateDisplay;
		}

		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			int spawnRateRaw = Main.LocalPlayer.GetModPlayer<InfoPlayer>().spawnRate;
            int spawnCap = Main.LocalPlayer.GetModPlayer<InfoPlayer>().spawnCap;

            // Spawn Rate is the chance for an enemy spawn per tick; this translates that chance to be per second instead
            float spawnRateAdapted = spawnRateRaw == 0 ? 0 : 60f / spawnRateRaw;
			spawnRateAdapted = (float)Math.Round(spawnRateAdapted, 2);

			// Manual pluarlization since our input is a float
			string spawnRatePluralized = ModContent.GetInstance<ConfigServer>().simpleDisplay ? "" : "~";
            spawnRatePluralized += spawnRateAdapted == 1 ? Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SpawnRateDisplayS").FormatWith(spawnRateAdapted) : Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SpawnRateDisplayP").FormatWith(spawnRateAdapted);
            if(spawnRateRaw == 0)
			{
				spawnRatePluralized = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SpawnRateDisplayError");
            }

            string spawnCapString = ModContent.GetInstance<ConfigServer>().simpleDisplay ? "" : "~";
            spawnCapString += Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SpawnCapDisplay").FormatWith(spawnCap);
			

            InfoPlayer player = Main.LocalPlayer.GetModPlayer<InfoPlayer>();
			bool altDisplay = (player.displayTimer / 1000) % 2 == 0;

			string textToShow = altDisplay ? spawnRatePluralized : spawnCapString;

            if (!ModContent.GetInstance<ConfigServer>().simpleDisplay)
            {
                textToShow += " +"; // Adds a '+' on to the end of the line, indicating there is more information (Like Weather Radio during a Sandstorm)
            }

            return textToShow;
        }
	}

    [LegacyName("ComboInfoDisplay")]
    public class InfoDisplay6 : InfoDisplay
	{
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().comboDisplay;
		}

		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			InfoPlayer player = Main.LocalPlayer.GetModPlayer<InfoPlayer>();

			int hits = player.hitCounter;
			int dmg = player.comboDmg;
			float time = player.comboTime / 60f;
			time = (float)Math.Round(time, 1);

			if (hits == 0)
			{
				displayColor = InactiveInfoTextColor;
				return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ComboDisplayNone");
            }

			// Convert to strings
			string hitCount = CompactNumbers(hits);
			string timeCount = CompactTime(time);
			string dmgCount = CompactNumbers(dmg);

            bool altDisplay = (player.displayTimer / 1000) % 2 == 0;
            string text = altDisplay ? Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ComboDisplayDmg").FormatWith(hitCount, dmgCount)
							  : Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ComboDisplayTime").FormatWith(hitCount ,timeCount);

			if (!ModContent.GetInstance<ConfigServer>().simpleDisplay)
			{
				text += " +"; // Adds a '+' on to the end of the line, indicating there is more information (Like Weather Radio during a Sandstorm)
			}
			return text;
        }

		private static string CompactNumbers(int baseInt)
        {
			float baseFloat = (float)baseInt;
			string suffix = "";
			if(baseFloat < 0)
            {
				return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ComboError");
            }

			if (baseFloat >= 100000000f)
			{
				suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixBillion");
                baseFloat /= 1000000000f;
				baseFloat = (float)Math.Round(baseFloat, 1);
			}
			else if (baseFloat >= 100000f)
			{
				suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixMillion");
                baseFloat /= 1000000f;
				baseFloat = (float)Math.Round(baseFloat, 1);
			}
			else if (baseFloat >= 1000f)
			{
				suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixThousand");
                baseFloat /= 1000f;
				baseFloat = (float)Math.Round(baseFloat, 1);
			}
			return $"{baseFloat}" + suffix;
		}

		private static string CompactTime(float baseNum)
        {
			string suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixSeconds");
            if (baseNum >= 3600f)
			{
				suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixHours");
                baseNum /= 3600f;
				baseNum = (float)Math.Round(baseNum, 1);
			}
			else if (baseNum >= 60f)
            {
				suffix = Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.SuffixMinutes");
                baseNum /= 60f;
				baseNum = (float)Math.Round(baseNum, 1);
			}
			return $"{baseNum}" + suffix;
		}
	}

    [LegacyName("BiomeInfoDisplay")]
    public class InfoDisplay4 : InfoDisplay
    {
        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<InfoPlayer>().biomeDisplay;
        }

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            InfoPlayer player = Main.LocalPlayer.GetModPlayer<InfoPlayer>();

			string biomeToDisplay = Language.GetTextValue("Mods.BInfoAcc.Biomes.Neutral");

			List<string> biomeNames = new List<string>();
			if (player.biomeNames != null)
			{
				biomeNames = player.biomeNames;
			}	
            if (biomeNames.Count > 0)
			{
				int stateTimer = player.displayTimer;
				while(stateTimer > 2000) // Effectivley caps displayTimer (Which goes from 0-4000) to 0-1000 whilst preserving resultant cycle speed
                {
					stateTimer -= 2000;
				}

                int displayState = -1;
				while(stateTimer > 0)
				{
                    stateTimer -= 2000 / biomeNames.Count;
					displayState++;
                }

				if (displayState > -1 && displayState < biomeNames.Count)
				{
					if (biomeNames[displayState] != null && biomeNames[displayState] != "")
					{
                        biomeToDisplay = biomeNames[displayState];
                    }
				}
            }

			if(!ModContent.GetInstance<ConfigServer>().simpleDisplay && biomeNames.Count > 1)
			{
                biomeToDisplay += " +"; // Adds a '+' on to the end of the line, indicating there is more information (Like Weather Radio during a Sandstorm)
            }

			return biomeToDisplay;
        }
    }

    [LegacyName("AggroInfoDisplay")]
    public class InfoDisplay5 : InfoDisplay
    {
        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<InfoPlayer>().aggroDisplay;
        }

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            int aggroValue = Main.LocalPlayer.aggro;

            if (aggroValue == 0)
            {
                displayColor = InactiveInfoTextColor;
            }
            else if (aggroValue < 0)
            {
                displayColor = new Color(120, 190, 120);
            }
            else if (aggroValue > 0)
            {
                displayColor = new Color(190, 120, 120);
            }

            return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.AggroDisplay").FormatWith(aggroValue);
        }
    }

    [LegacyName("ManaInfoDisplay")]
    public class InfoDisplay2 : InfoDisplay
    {
        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<InfoPlayer>().manaDisplay;
        }

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
			int manaUsage = Main.LocalPlayer.GetModPlayer<InfoPlayer>().GetMPS();
            int naturalRegen = Main.LocalPlayer.manaRegen;
			if(naturalRegen > 1) // 2 points of manaRegen == 1 mana/sec
			{
				naturalRegen /= 2;
			}

			int totalMPS;
			if(naturalRegen > 0 && Main.LocalPlayer.manaRegenDelay <= 0)
			{
				totalMPS = naturalRegen;
            }
			else
			{
				totalMPS = -manaUsage + naturalRegen;
            }

            if (Main.LocalPlayer.statMana >= Main.LocalPlayer.statManaMax2)
            {
                return Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ManaDisplayFull");
            }
            else if (totalMPS == 0)
            {
                displayColor = InactiveInfoTextColor;
            }
            else if (totalMPS > 0)
            {
                displayColor = new Color(120, 190, 120);
            }
            else if (totalMPS < 0)
            {
                displayColor = new Color(190, 120, 120);
            }

            return totalMPS > 0 ? Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ManaDisplayPos").FormatWith(totalMPS) : Language.GetTextValue("Mods.BInfoAcc.CommonItemtooltip.ManaDisplayNeg").FormatWith(totalMPS);
        }
    }
}