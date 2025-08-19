using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x0200028B RID: 651
	public class InfoPlayer : ModPlayer
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x00085E20 File Offset: 0x00084020
		public override void ResetEffects()
		{
			this.Reset();
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00085E20 File Offset: 0x00084020
		public override void UpdateDead()
		{
			this.Reset();
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00085E28 File Offset: 0x00084028
		public override void Initialize()
		{
			for (int i = 1; i < DamageClassLoader.DamageClassCount; i++)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<DamageClass>(DamageClassLoader.GetDamageClass(i));
				string[] array = defaultInterpolatedStringHandler.ToStringAndClear().Split(".", StringSplitOptions.None);
				List<string> list = this.classString;
				string[] array2 = array;
				list.Add(array2[array2.Length - 1].Replace("DamageClass", "") + ((array[0] != "Terraria") ? ("(" + array[0] + ")") : ""));
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00085EC0 File Offset: 0x000840C0
		public unsafe override void PostUpdate()
		{
			this.armorPenetrationStat = *base.Player.GetArmorPenetration(DamageClass.Generic);
			this.critChanceStat = *base.Player.GetCritChance(DamageClass.Generic);
			this.damageStat = base.Player.GetDamage(DamageClass.Generic).ApplyTo(1f);
			if (this.classIndex == 0 || this.classIndex != 1)
			{
				int num = (this.classIndex == 0) ? base.Player.HeldItem.DamageType.Type : this.classIndex;
				num -= ((num == 3 || num == 7) ? 1 : 0);
				this.damageStat += base.Player.GetDamage(DamageClassLoader.GetDamageClass(num)).ApplyTo(1f) - 1f;
				this.critChanceStat += *base.Player.GetCritChance(DamageClassLoader.GetDamageClass(num));
				this.armorPenetrationStat += *base.Player.GetArmorPenetration(DamageClassLoader.GetDamageClass(num));
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00085FD0 File Offset: 0x000841D0
		public void Reset()
		{
			this.battalionLog = false;
			this.harmInducer = false;
			this.headCounter = false;
			this.kettlebell = false;
			this.luckyDie = false;
			this.metallicClover = false;
			this.plateCracker = false;
			this.regenerator = false;
			this.reinforcedPanel = false;
			this.replenisher = false;
			this.trackingDevice = false;
			this.wingTimer = false;
			this.anglerRadar = false;
			this.deteriorationDisplay = false;
			this.skullWatch = false;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00086046 File Offset: 0x00084246
		public InfoPlayer()
		{
			List<string> list = new List<string>();
			list.Add("Automatic");
			this.classString = list;
			base..ctor();
		}

		// Token: 0x04000719 RID: 1817
		public bool battalionLog;

		// Token: 0x0400071A RID: 1818
		public bool harmInducer;

		// Token: 0x0400071B RID: 1819
		public bool headCounter;

		// Token: 0x0400071C RID: 1820
		public bool kettlebell;

		// Token: 0x0400071D RID: 1821
		public bool luckyDie;

		// Token: 0x0400071E RID: 1822
		public bool metallicClover;

		// Token: 0x0400071F RID: 1823
		public bool plateCracker;

		// Token: 0x04000720 RID: 1824
		public bool regenerator;

		// Token: 0x04000721 RID: 1825
		public bool reinforcedPanel;

		// Token: 0x04000722 RID: 1826
		public bool replenisher;

		// Token: 0x04000723 RID: 1827
		public bool trackingDevice;

		// Token: 0x04000724 RID: 1828
		public bool wingTimer;

		// Token: 0x04000725 RID: 1829
		public bool anglerRadar;

		// Token: 0x04000726 RID: 1830
		public bool deteriorationDisplay;

		// Token: 0x04000727 RID: 1831
		public bool skullWatch;

		// Token: 0x04000728 RID: 1832
		public float armorPenetrationStat;

		// Token: 0x04000729 RID: 1833
		public float critChanceStat;

		// Token: 0x0400072A RID: 1834
		public float damageStat;

		// Token: 0x0400072B RID: 1835
		private List<string> classString;

		// Token: 0x0400072C RID: 1836
		public int classIndex;
	}
}
