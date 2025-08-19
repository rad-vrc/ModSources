using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace QoLCompendium.Core.PermanentBuffSystems
{
	// Token: 0x020002A0 RID: 672
	public class PermanentBuffPlayer : ModPlayer
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x00087185 File Offset: 0x00085385
		public override void ResetEffects()
		{
			this.ResetValues();
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00087185 File Offset: 0x00085385
		public override void UpdateDead()
		{
			this.ResetValues();
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0008718D File Offset: 0x0008538D
		public override void Unload()
		{
			PermanentBuffPlayer.UnloadBools();
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00087194 File Offset: 0x00085394
		public void ResetValues()
		{
			this.buffActive = false;
			this.potionEffects.Clear();
			this.modPotionEffects.Clear();
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000871B4 File Offset: 0x000853B4
		public override void PostUpdateEquips()
		{
			this.CheckForPotions(base.Player.bank.item);
			this.CheckForPotions(base.Player.bank2.item);
			this.CheckForPotions(base.Player.bank3.item);
			this.CheckForPotions(base.Player.bank4.item);
			this.UpdatePotions();
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00087220 File Offset: 0x00085420
		public void UpdatePotions()
		{
			foreach (IPermanentBuff permanentBuff in this.potionEffects)
			{
				permanentBuff.ApplyEffect(this);
			}
			foreach (IPermanentModdedBuff permanentModdedBuff in this.modPotionEffects)
			{
				permanentModdedBuff.ApplyEffect(this);
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000872B4 File Offset: 0x000854B4
		public void CheckForPotions(Item[] inventory)
		{
			foreach (Item item in inventory)
			{
				if (!item.IsAir)
				{
					IPermanentBuffItem pBuffItem = item.ModItem as IPermanentBuffItem;
					if (pBuffItem != null)
					{
						pBuffItem.ApplyBuff(this);
					}
				}
				if (!item.IsAir)
				{
					IPermanentModdedBuffItem pModBuffItem = item.ModItem as IPermanentModdedBuffItem;
					if (pModBuffItem != null)
					{
						pModBuffItem.ApplyBuff(this);
					}
				}
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00087314 File Offset: 0x00085514
		public override void SaveData(TagCompound tag)
		{
			List<string> buffsEnabled = new List<string>();
			for (int i = 0; i < PermanentBuffPlayer.PermanentBuffsBools.Length; i++)
			{
				if (PermanentBuffPlayer.PermanentBuffsBools[i])
				{
					buffsEnabled.Add("QoLCPBuff" + i.ToString());
				}
			}
			tag.Add("QoLCPBuff", buffsEnabled);
			List<string> calamityBuffsEnabled = new List<string>();
			for (int j = 0; j < PermanentBuffPlayer.PermanentCalamityBuffsBools.Length; j++)
			{
				if (PermanentBuffPlayer.PermanentCalamityBuffsBools[j])
				{
					calamityBuffsEnabled.Add("QoLCPCalamityBuff" + j.ToString());
				}
			}
			tag.Add("QoLCPCalamityBuff", calamityBuffsEnabled);
			List<string> martinsOrderBuffsEnabled = new List<string>();
			for (int k = 0; k < PermanentBuffPlayer.PermanentMartinsOrderBuffsBools.Length; k++)
			{
				if (PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[k])
				{
					martinsOrderBuffsEnabled.Add("QoLCPMartinsOrderBuff" + k.ToString());
				}
			}
			tag.Add("QoLCPMartinsOrderBuff", martinsOrderBuffsEnabled);
			List<string> sotsBuffsEnabled = new List<string>();
			for (int l = 0; l < PermanentBuffPlayer.PermanentSOTSBuffsBools.Length; l++)
			{
				if (PermanentBuffPlayer.PermanentSOTSBuffsBools[l])
				{
					sotsBuffsEnabled.Add("QoLCPSOTSBuff" + l.ToString());
				}
			}
			tag.Add("QoLCPSOTSBuff", sotsBuffsEnabled);
			List<string> spiritClassicBuffsEnabled = new List<string>();
			for (int m = 0; m < PermanentBuffPlayer.PermanentSpiritClassicBuffsBools.Length; m++)
			{
				if (PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[m])
				{
					spiritClassicBuffsEnabled.Add("QoLCPSpiritClassicBuff" + m.ToString());
				}
			}
			tag.Add("QoLCPSpiritClassicBuff", spiritClassicBuffsEnabled);
			List<string> thoriumBuffsEnabled = new List<string>();
			for (int n = 0; n < PermanentBuffPlayer.PermanentThoriumBuffsBools.Length; n++)
			{
				if (PermanentBuffPlayer.PermanentThoriumBuffsBools[n])
				{
					thoriumBuffsEnabled.Add("QoLCPThoriumBuff" + n.ToString());
				}
			}
			tag.Add("QoLCPThoriumBuff", thoriumBuffsEnabled);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000874E0 File Offset: 0x000856E0
		public override void LoadData(TagCompound tag)
		{
			IList<string> buffsEnabled = tag.GetList<string>("QoLCPBuff");
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			for (int i = 0; i < PermanentBuffPlayer.PermanentBuffsBools.Length; i++)
			{
				bool[] array = PermanentBuffPlayer.PermanentBuffsBools;
				int num = i;
				ICollection<string> collection = buffsEnabled;
				defaultInterpolatedStringHandler..ctor(9, 1);
				defaultInterpolatedStringHandler.AppendLiteral("QoLCPBuff");
				defaultInterpolatedStringHandler.AppendFormatted<int>(i);
				array[num] = collection.Contains(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			IList<string> calamityBuffsEnabled = tag.GetList<string>("QoLCPCalamityBuff");
			for (int j = 0; j < PermanentBuffPlayer.PermanentCalamityBuffsBools.Length; j++)
			{
				bool[] array2 = PermanentBuffPlayer.PermanentCalamityBuffsBools;
				int num2 = j;
				ICollection<string> collection2 = calamityBuffsEnabled;
				defaultInterpolatedStringHandler..ctor(17, 1);
				defaultInterpolatedStringHandler.AppendLiteral("QoLCPCalamityBuff");
				defaultInterpolatedStringHandler.AppendFormatted<int>(j);
				array2[num2] = collection2.Contains(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			IList<string> martinsOrderBuffsEnabled = tag.GetList<string>("QoLCPMartinsOrderBuff");
			for (int k = 0; k < PermanentBuffPlayer.PermanentMartinsOrderBuffsBools.Length; k++)
			{
				bool[] array3 = PermanentBuffPlayer.PermanentMartinsOrderBuffsBools;
				int num3 = k;
				ICollection<string> collection3 = martinsOrderBuffsEnabled;
				defaultInterpolatedStringHandler..ctor(21, 1);
				defaultInterpolatedStringHandler.AppendLiteral("QoLCPMartinsOrderBuff");
				defaultInterpolatedStringHandler.AppendFormatted<int>(k);
				array3[num3] = collection3.Contains(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			IList<string> sotsBuffsEnabled = tag.GetList<string>("QoLCPSOTSBuff");
			for (int l = 0; l < PermanentBuffPlayer.PermanentSOTSBuffsBools.Length; l++)
			{
				bool[] array4 = PermanentBuffPlayer.PermanentSOTSBuffsBools;
				int num4 = l;
				ICollection<string> collection4 = sotsBuffsEnabled;
				defaultInterpolatedStringHandler..ctor(13, 1);
				defaultInterpolatedStringHandler.AppendLiteral("QoLCPSOTSBuff");
				defaultInterpolatedStringHandler.AppendFormatted<int>(l);
				array4[num4] = collection4.Contains(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			IList<string> spiritClassicBuffsEnabled = tag.GetList<string>("QoLCPSpiritClassicBuff");
			for (int m = 0; m < PermanentBuffPlayer.PermanentSpiritClassicBuffsBools.Length; m++)
			{
				bool[] array5 = PermanentBuffPlayer.PermanentSpiritClassicBuffsBools;
				int num5 = m;
				ICollection<string> collection5 = spiritClassicBuffsEnabled;
				defaultInterpolatedStringHandler..ctor(22, 1);
				defaultInterpolatedStringHandler.AppendLiteral("QoLCPSpiritClassicBuff");
				defaultInterpolatedStringHandler.AppendFormatted<int>(m);
				array5[num5] = collection5.Contains(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			IList<string> thoriumBuffsEnabled = tag.GetList<string>("QoLCPThoriumBuff");
			for (int n = 0; n < PermanentBuffPlayer.PermanentThoriumBuffsBools.Length; n++)
			{
				bool[] array6 = PermanentBuffPlayer.PermanentThoriumBuffsBools;
				int num6 = n;
				ICollection<string> collection6 = thoriumBuffsEnabled;
				defaultInterpolatedStringHandler..ctor(16, 1);
				defaultInterpolatedStringHandler.AppendLiteral("QoLCPThoriumBuff");
				defaultInterpolatedStringHandler.AppendFormatted<int>(n);
				array6[num6] = collection6.Contains(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000876F5 File Offset: 0x000858F5
		public static void UnloadBools()
		{
			PermanentBuffPlayer.PermanentBuffsBools = null;
			PermanentBuffPlayer.PermanentCalamityBuffsBools = null;
			PermanentBuffPlayer.PermanentMartinsOrderBuffsBools = null;
			PermanentBuffPlayer.PermanentSOTSBuffsBools = null;
			PermanentBuffPlayer.PermanentSpiritClassicBuffsBools = null;
			PermanentBuffPlayer.PermanentThoriumBuffsBools = null;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0008771B File Offset: 0x0008591B
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x00087722 File Offset: 0x00085922
		public static bool[] PermanentBuffsBools
		{
			get
			{
				return PermanentBuffPlayer.permanentBuffsBools;
			}
			set
			{
				PermanentBuffPlayer.permanentBuffsBools = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0008772A File Offset: 0x0008592A
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x00087731 File Offset: 0x00085931
		public static bool[] PermanentCalamityBuffsBools
		{
			get
			{
				return PermanentBuffPlayer.permanentCalamityBuffsBools;
			}
			set
			{
				PermanentBuffPlayer.permanentCalamityBuffsBools = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00087739 File Offset: 0x00085939
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x00087740 File Offset: 0x00085940
		public static bool[] PermanentMartinsOrderBuffsBools
		{
			get
			{
				return PermanentBuffPlayer.permanentMartinsOrderBuffsBools;
			}
			set
			{
				PermanentBuffPlayer.permanentMartinsOrderBuffsBools = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00087748 File Offset: 0x00085948
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x0008774F File Offset: 0x0008594F
		public static bool[] PermanentSOTSBuffsBools
		{
			get
			{
				return PermanentBuffPlayer.permanentSOTSBuffsBools;
			}
			set
			{
				PermanentBuffPlayer.permanentSOTSBuffsBools = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x00087757 File Offset: 0x00085957
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x0008775E File Offset: 0x0008595E
		public static bool[] PermanentSpiritClassicBuffsBools
		{
			get
			{
				return PermanentBuffPlayer.permanentSpiritClassicBuffsBools;
			}
			set
			{
				PermanentBuffPlayer.permanentSpiritClassicBuffsBools = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x00087766 File Offset: 0x00085966
		// (set) Token: 0x0600116B RID: 4459 RVA: 0x0008776D File Offset: 0x0008596D
		public static bool[] PermanentThoriumBuffsBools
		{
			get
			{
				return PermanentBuffPlayer.permanentThoriumBuffsBools;
			}
			set
			{
				PermanentBuffPlayer.permanentThoriumBuffsBools = value;
			}
		}

		// Token: 0x0400075D RID: 1885
		public bool buffActive;

		// Token: 0x0400075E RID: 1886
		public SortedSet<IPermanentBuff> potionEffects = new SortedSet<IPermanentBuff>();

		// Token: 0x0400075F RID: 1887
		public SortedSet<IPermanentModdedBuff> modPotionEffects = new SortedSet<IPermanentModdedBuff>();

		// Token: 0x04000760 RID: 1888
		public static bool[] permanentBuffsBools = new bool[Enum.GetValues(typeof(PermanentBuffPlayer.PermanentBuffs)).Length];

		// Token: 0x04000761 RID: 1889
		public static bool[] permanentCalamityBuffsBools = new bool[Enum.GetValues(typeof(PermanentBuffPlayer.PermanentCalamityBuffs)).Length];

		// Token: 0x04000762 RID: 1890
		public static bool[] permanentMartinsOrderBuffsBools = new bool[Enum.GetValues(typeof(PermanentBuffPlayer.PermanentMartinsOrderBuffs)).Length];

		// Token: 0x04000763 RID: 1891
		public static bool[] permanentSOTSBuffsBools = new bool[Enum.GetValues(typeof(PermanentBuffPlayer.PermanentSOTSBuffs)).Length];

		// Token: 0x04000764 RID: 1892
		public static bool[] permanentSpiritClassicBuffsBools = new bool[Enum.GetValues(typeof(PermanentBuffPlayer.PermanentSpiritClassicBuffs)).Length];

		// Token: 0x04000765 RID: 1893
		public static bool[] permanentThoriumBuffsBools = new bool[Enum.GetValues(typeof(PermanentBuffPlayer.PermanentThoriumBuffs)).Length];

		// Token: 0x02000560 RID: 1376
		public enum PermanentBuffs
		{
			// Token: 0x04000F2F RID: 3887
			BastStatue,
			// Token: 0x04000F30 RID: 3888
			Campfire,
			// Token: 0x04000F31 RID: 3889
			GardenGnome,
			// Token: 0x04000F32 RID: 3890
			HeartLantern,
			// Token: 0x04000F33 RID: 3891
			Honey,
			// Token: 0x04000F34 RID: 3892
			PeaceCandle,
			// Token: 0x04000F35 RID: 3893
			ShadowCandle,
			// Token: 0x04000F36 RID: 3894
			StarInABottle,
			// Token: 0x04000F37 RID: 3895
			Sunflower,
			// Token: 0x04000F38 RID: 3896
			WaterCandle,
			// Token: 0x04000F39 RID: 3897
			AmmoReservation,
			// Token: 0x04000F3A RID: 3898
			Archery,
			// Token: 0x04000F3B RID: 3899
			Battle,
			// Token: 0x04000F3C RID: 3900
			BiomeSight,
			// Token: 0x04000F3D RID: 3901
			Builder,
			// Token: 0x04000F3E RID: 3902
			Calm,
			// Token: 0x04000F3F RID: 3903
			Crate,
			// Token: 0x04000F40 RID: 3904
			Dangersense,
			// Token: 0x04000F41 RID: 3905
			Endurance,
			// Token: 0x04000F42 RID: 3906
			ExquisitelyStuffed,
			// Token: 0x04000F43 RID: 3907
			Featherfall,
			// Token: 0x04000F44 RID: 3908
			Fishing,
			// Token: 0x04000F45 RID: 3909
			Flipper,
			// Token: 0x04000F46 RID: 3910
			Gills,
			// Token: 0x04000F47 RID: 3911
			Gravitation,
			// Token: 0x04000F48 RID: 3912
			Heartreach,
			// Token: 0x04000F49 RID: 3913
			Hunter,
			// Token: 0x04000F4A RID: 3914
			Inferno,
			// Token: 0x04000F4B RID: 3915
			Invisibility,
			// Token: 0x04000F4C RID: 3916
			Ironskin,
			// Token: 0x04000F4D RID: 3917
			Lifeforce,
			// Token: 0x04000F4E RID: 3918
			Lucky,
			// Token: 0x04000F4F RID: 3919
			MagicPower,
			// Token: 0x04000F50 RID: 3920
			ManaRegeneration,
			// Token: 0x04000F51 RID: 3921
			Mining,
			// Token: 0x04000F52 RID: 3922
			NightOwl,
			// Token: 0x04000F53 RID: 3923
			ObsidianSkin,
			// Token: 0x04000F54 RID: 3924
			PlentySatisfied,
			// Token: 0x04000F55 RID: 3925
			Rage,
			// Token: 0x04000F56 RID: 3926
			Regeneration,
			// Token: 0x04000F57 RID: 3927
			Shine,
			// Token: 0x04000F58 RID: 3928
			Sonar,
			// Token: 0x04000F59 RID: 3929
			Spelunker,
			// Token: 0x04000F5A RID: 3930
			Summoning,
			// Token: 0x04000F5B RID: 3931
			Swiftness,
			// Token: 0x04000F5C RID: 3932
			Thorns,
			// Token: 0x04000F5D RID: 3933
			Tipsy,
			// Token: 0x04000F5E RID: 3934
			Titan,
			// Token: 0x04000F5F RID: 3935
			Warmth,
			// Token: 0x04000F60 RID: 3936
			WaterWalking,
			// Token: 0x04000F61 RID: 3937
			WellFed,
			// Token: 0x04000F62 RID: 3938
			Wrath,
			// Token: 0x04000F63 RID: 3939
			AmmoBox,
			// Token: 0x04000F64 RID: 3940
			BewitchingTable,
			// Token: 0x04000F65 RID: 3941
			CrystalBall,
			// Token: 0x04000F66 RID: 3942
			SharpeningStation,
			// Token: 0x04000F67 RID: 3943
			SliceOfCake,
			// Token: 0x04000F68 RID: 3944
			WarTable
		}

		// Token: 0x02000561 RID: 1377
		public enum PermanentCalamityBuffs
		{
			// Token: 0x04000F6A RID: 3946
			ChaosCandle,
			// Token: 0x04000F6B RID: 3947
			CorruptionEffigy,
			// Token: 0x04000F6C RID: 3948
			CrimsonEffigy,
			// Token: 0x04000F6D RID: 3949
			EffigyOfDecay,
			// Token: 0x04000F6E RID: 3950
			ResilientCandle,
			// Token: 0x04000F6F RID: 3951
			SpitefulCandle,
			// Token: 0x04000F70 RID: 3952
			TranquilityCandle,
			// Token: 0x04000F71 RID: 3953
			VigorousCandle,
			// Token: 0x04000F72 RID: 3954
			WeightlessCandle,
			// Token: 0x04000F73 RID: 3955
			AnechoicCoating,
			// Token: 0x04000F74 RID: 3956
			AstralInjection,
			// Token: 0x04000F75 RID: 3957
			Baguette,
			// Token: 0x04000F76 RID: 3958
			Bloodfin,
			// Token: 0x04000F77 RID: 3959
			Bounding,
			// Token: 0x04000F78 RID: 3960
			Calcium,
			// Token: 0x04000F79 RID: 3961
			CeaselessHunger,
			// Token: 0x04000F7A RID: 3962
			GravityNormalizer,
			// Token: 0x04000F7B RID: 3963
			Omniscience,
			// Token: 0x04000F7C RID: 3964
			Photosynthesis,
			// Token: 0x04000F7D RID: 3965
			Shadow,
			// Token: 0x04000F7E RID: 3966
			Soaring,
			// Token: 0x04000F7F RID: 3967
			Sulphurskin,
			// Token: 0x04000F80 RID: 3968
			Tesla,
			// Token: 0x04000F81 RID: 3969
			Zen,
			// Token: 0x04000F82 RID: 3970
			Zerg,
			// Token: 0x04000F83 RID: 3971
			AstraJelly,
			// Token: 0x04000F84 RID: 3972
			Astracola,
			// Token: 0x04000F85 RID: 3973
			ExoBaguette,
			// Token: 0x04000F86 RID: 3974
			SupremeLuck,
			// Token: 0x04000F87 RID: 3975
			TitanScale,
			// Token: 0x04000F88 RID: 3976
			VoidCandle,
			// Token: 0x04000F89 RID: 3977
			SoyMilk,
			// Token: 0x04000F8A RID: 3978
			YharimsStimulants
		}

		// Token: 0x02000562 RID: 1378
		public enum PermanentMartinsOrderBuffs
		{
			// Token: 0x04000F8C RID: 3980
			BlackHole,
			// Token: 0x04000F8D RID: 3981
			Charging,
			// Token: 0x04000F8E RID: 3982
			Defender,
			// Token: 0x04000F8F RID: 3983
			Empowerment,
			// Token: 0x04000F90 RID: 3984
			Evocation,
			// Token: 0x04000F91 RID: 3985
			GourmetFlavor,
			// Token: 0x04000F92 RID: 3986
			Haste,
			// Token: 0x04000F93 RID: 3987
			Healing,
			// Token: 0x04000F94 RID: 3988
			Rockskin,
			// Token: 0x04000F95 RID: 3989
			Shooter,
			// Token: 0x04000F96 RID: 3990
			Soul,
			// Token: 0x04000F97 RID: 3991
			SpellCaster,
			// Token: 0x04000F98 RID: 3992
			Starreach,
			// Token: 0x04000F99 RID: 3993
			Sweeper,
			// Token: 0x04000F9A RID: 3994
			Thrower,
			// Token: 0x04000F9B RID: 3995
			Whipper,
			// Token: 0x04000F9C RID: 3996
			ZincPill,
			// Token: 0x04000F9D RID: 3997
			Archeology,
			// Token: 0x04000F9E RID: 3998
			SporeFarm
		}

		// Token: 0x02000563 RID: 1379
		public enum PermanentSOTSBuffs
		{
			// Token: 0x04000FA0 RID: 4000
			Assassination,
			// Token: 0x04000FA1 RID: 4001
			Bluefire,
			// Token: 0x04000FA2 RID: 4002
			Brittle,
			// Token: 0x04000FA3 RID: 4003
			DoubleVision,
			// Token: 0x04000FA4 RID: 4004
			Harmony,
			// Token: 0x04000FA5 RID: 4005
			Nightmare,
			// Token: 0x04000FA6 RID: 4006
			Ripple,
			// Token: 0x04000FA7 RID: 4007
			Roughskin,
			// Token: 0x04000FA8 RID: 4008
			SoulAccess,
			// Token: 0x04000FA9 RID: 4009
			Vibe,
			// Token: 0x04000FAA RID: 4010
			DigitalDisplay
		}

		// Token: 0x02000564 RID: 1380
		public enum PermanentSpiritClassicBuffs
		{
			// Token: 0x04000FAC RID: 4012
			CoiledEnergizer,
			// Token: 0x04000FAD RID: 4013
			KoiTotem,
			// Token: 0x04000FAE RID: 4014
			SunPot,
			// Token: 0x04000FAF RID: 4015
			TheCouch,
			// Token: 0x04000FB0 RID: 4016
			Jump,
			// Token: 0x04000FB1 RID: 4017
			MirrorCoat,
			// Token: 0x04000FB2 RID: 4018
			MoonJelly,
			// Token: 0x04000FB3 RID: 4019
			Runescribe,
			// Token: 0x04000FB4 RID: 4020
			Soulguard,
			// Token: 0x04000FB5 RID: 4021
			Spirit,
			// Token: 0x04000FB6 RID: 4022
			Soaring,
			// Token: 0x04000FB7 RID: 4023
			Sporecoid,
			// Token: 0x04000FB8 RID: 4024
			Starburn,
			// Token: 0x04000FB9 RID: 4025
			Steadfast,
			// Token: 0x04000FBA RID: 4026
			Toxin,
			// Token: 0x04000FBB RID: 4027
			Zephyr
		}

		// Token: 0x02000565 RID: 1381
		public enum PermanentThoriumBuffs
		{
			// Token: 0x04000FBD RID: 4029
			Mistletoe,
			// Token: 0x04000FBE RID: 4030
			AquaAffinity,
			// Token: 0x04000FBF RID: 4031
			Arcane,
			// Token: 0x04000FC0 RID: 4032
			Artillery,
			// Token: 0x04000FC1 RID: 4033
			Assassin,
			// Token: 0x04000FC2 RID: 4034
			BloodRush,
			// Token: 0x04000FC3 RID: 4035
			BouncingFlame,
			// Token: 0x04000FC4 RID: 4036
			CactusFruit,
			// Token: 0x04000FC5 RID: 4037
			Conflagration,
			// Token: 0x04000FC6 RID: 4038
			Creativity,
			// Token: 0x04000FC7 RID: 4039
			Earworm,
			// Token: 0x04000FC8 RID: 4040
			Frenzy,
			// Token: 0x04000FC9 RID: 4041
			Glowing,
			// Token: 0x04000FCA RID: 4042
			Holy,
			// Token: 0x04000FCB RID: 4043
			Hydration,
			// Token: 0x04000FCC RID: 4044
			InspirationalReach,
			// Token: 0x04000FCD RID: 4045
			Kinetic,
			// Token: 0x04000FCE RID: 4046
			Warmonger,
			// Token: 0x04000FCF RID: 4047
			BatRepellent,
			// Token: 0x04000FD0 RID: 4048
			FishRepellent,
			// Token: 0x04000FD1 RID: 4049
			InsectRepellent,
			// Token: 0x04000FD2 RID: 4050
			SkeletonRepellent,
			// Token: 0x04000FD3 RID: 4051
			ZombieRepellent,
			// Token: 0x04000FD4 RID: 4052
			Altar,
			// Token: 0x04000FD5 RID: 4053
			ConductorsStand,
			// Token: 0x04000FD6 RID: 4054
			NinjaRack,
			// Token: 0x04000FD7 RID: 4055
			Deathsinger,
			// Token: 0x04000FD8 RID: 4056
			InspirationRegeneration
		}
	}
}
