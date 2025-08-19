using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Represents an informational display toggle typically provided by <see href="https://terraria.wiki.gg/wiki/Informational_Accessories">informational accessories</see>.<para />
	/// The <see cref="M:Terraria.ModLoader.InfoDisplay.Active" /> property determines if the InfoDisplay could be shown to the user. The game tracks the players desired visibility of active InfoDisplay through the <see cref="F:Terraria.Player.hideInfo" /> array.
	/// </summary>
	// Token: 0x02000180 RID: 384
	public abstract class InfoDisplay : ModTexturedType, ILocalizedModType, IModType
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x004D4EE7 File Offset: 0x004D30E7
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x004D4EEE File Offset: 0x004D30EE
		public static InfoDisplay Watches { get; private set; } = new WatchesInfoDisplay();

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x004D4EF6 File Offset: 0x004D30F6
		// (set) Token: 0x06001E07 RID: 7687 RVA: 0x004D4EFD File Offset: 0x004D30FD
		public static InfoDisplay WeatherRadio { get; private set; } = new WeatherRadioInfoDisplay();

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x004D4F05 File Offset: 0x004D3105
		// (set) Token: 0x06001E09 RID: 7689 RVA: 0x004D4F0C File Offset: 0x004D310C
		public static InfoDisplay Sextant { get; private set; } = new SextantInfoDisplay();

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x004D4F14 File Offset: 0x004D3114
		// (set) Token: 0x06001E0B RID: 7691 RVA: 0x004D4F1B File Offset: 0x004D311B
		public static InfoDisplay FishFinder { get; private set; } = new FishFinderInfoDisplay();

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x004D4F23 File Offset: 0x004D3123
		// (set) Token: 0x06001E0D RID: 7693 RVA: 0x004D4F2A File Offset: 0x004D312A
		public static InfoDisplay MetalDetector { get; private set; } = new MetalDetectorInfoDisplay();

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x004D4F32 File Offset: 0x004D3132
		// (set) Token: 0x06001E0F RID: 7695 RVA: 0x004D4F39 File Offset: 0x004D3139
		public static InfoDisplay LifeformAnalyzer { get; private set; } = new LifeformAnalyzerInfoDisplay();

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x004D4F41 File Offset: 0x004D3141
		// (set) Token: 0x06001E11 RID: 7697 RVA: 0x004D4F48 File Offset: 0x004D3148
		public static InfoDisplay Radar { get; private set; } = new RadarInfoDisplay();

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x004D4F50 File Offset: 0x004D3150
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x004D4F57 File Offset: 0x004D3157
		public static InfoDisplay TallyCounter { get; private set; } = new TallyCounterInfoDisplay();

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x004D4F5F File Offset: 0x004D315F
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x004D4F66 File Offset: 0x004D3166
		internal static InfoDisplay Dummy { get; private set; } = new DummyInfoDisplay();

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x004D4F6E File Offset: 0x004D316E
		// (set) Token: 0x06001E17 RID: 7703 RVA: 0x004D4F75 File Offset: 0x004D3175
		public static InfoDisplay DPSMeter { get; private set; } = new DPSMeterInfoDisplay();

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x004D4F7D File Offset: 0x004D317D
		// (set) Token: 0x06001E19 RID: 7705 RVA: 0x004D4F84 File Offset: 0x004D3184
		public static InfoDisplay Stopwatch { get; private set; } = new StopwatchInfoDisplay();

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x004D4F8C File Offset: 0x004D318C
		// (set) Token: 0x06001E1B RID: 7707 RVA: 0x004D4F93 File Offset: 0x004D3193
		public static InfoDisplay Compass { get; private set; } = new CompassInfoDisplay();

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x004D4F9B File Offset: 0x004D319B
		// (set) Token: 0x06001E1D RID: 7709 RVA: 0x004D4FA2 File Offset: 0x004D31A2
		public static InfoDisplay DepthMeter { get; private set; } = new DepthMeterInfoDisplay();

		/// <summary>
		/// The color when no valuable information is displayed.
		/// </summary>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x004D4FAA File Offset: 0x004D31AA
		public static Color InactiveInfoTextColor
		{
			get
			{
				return new Color(100, 100, 100, (int)Main.mouseTextColor);
			}
		}

		/// <summary>
		/// The golden color variant of the displays text.<br />
		/// Used by the Lifeform Analyzer.
		/// </summary>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x004D4FBC File Offset: 0x004D31BC
		public static Color GoldInfoTextColor
		{
			get
			{
				return new Color((int)Main.OurFavoriteColor.R, (int)Main.OurFavoriteColor.G, (int)Main.OurFavoriteColor.B, (int)Main.mouseTextColor);
			}
		}

		/// <summary>
		/// The golden color variant of the displays text shadow.<br />
		/// Used by the Lifeform Analyzer.
		/// </summary>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x004D4FE6 File Offset: 0x004D31E6
		public static Color GoldInfoTextShadowColor
		{
			get
			{
				return new Color((int)(Main.OurFavoriteColor.R / 10), (int)(Main.OurFavoriteColor.G / 10), (int)(Main.OurFavoriteColor.B / 10), (int)Main.mouseTextColor);
			}
		}

		/// <summary>
		/// The path to the texture vanilla info displays use when hovering over an info display.
		/// </summary>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x004D5019 File Offset: 0x004D3219
		public static string VanillaHoverTexture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_13";
			}
		}

		/// <summary>
		/// This is the internal ID of this InfoDisplay.<para />
		/// Also serves as the index for <see cref="F:Terraria.Player.hideInfo" />.
		/// </summary>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x004D5020 File Offset: 0x004D3220
		// (set) Token: 0x06001E23 RID: 7715 RVA: 0x004D5028 File Offset: 0x004D3228
		public int Type { get; internal set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x004D5031 File Offset: 0x004D3231
		public virtual string LocalizationCategory
		{
			get
			{
				return "InfoDisplays";
			}
		}

		/// <summary>
		/// The outline texture drawn when the icon is hovered and toggleable. By default a circular outline texture is used. Override this method and return <c>Texture + "_Hover"</c> or any other texture path to specify a custom outline texture for use with icons that are not circular.
		/// </summary>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x004D5038 File Offset: 0x004D3238
		public virtual string HoverTexture
		{
			get
			{
				return InfoDisplay.VanillaHoverTexture;
			}
		}

		/// <summary>
		/// This is the name that will show up when hovering over this info display.
		/// </summary>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x004D503F File Offset: 0x004D323F
		public virtual LocalizedText DisplayName
		{
			get
			{
				return this.GetLocalization("DisplayName", new Func<string>(base.PrettyPrintName));
			}
		}

		/// <summary>
		/// This dictates whether or not this info display should be active.<para />
		/// This is usually determined by player-specific values, typically set in <see cref="M:Terraria.ModLoader.ModItem.UpdateInventory(Terraria.Player)" />.
		/// </summary>
		// Token: 0x06001E27 RID: 7719 RVA: 0x004D5058 File Offset: 0x004D3258
		public virtual bool Active()
		{
			return false;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x004D505B File Offset: 0x004D325B
		[Obsolete]
		private string DisplayValue_Obsolete(ref Color displayColor)
		{
			return this.DisplayValue(ref displayColor);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x004D5064 File Offset: 0x004D3264
		[Obsolete("Use the (ref Color displayColor, ref Color displayShadowColor) overload", true)]
		public virtual string DisplayValue(ref Color displayColor)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// This is the value that will show up when viewing this display in normal play, right next to the icon.
		/// <br />Set <paramref name="displayColor" /> to <see cref="P:Terraria.ModLoader.InfoDisplay.InactiveInfoTextColor" /> if your display value is "zero"/shows no valuable information.
		/// </summary>
		/// <param name="displayColor">The color the text is displayed as.</param>
		/// <param name="displayShadowColor">The outline color text is displayed as.</param>
		// Token: 0x06001E2A RID: 7722 RVA: 0x004D506B File Offset: 0x004D326B
		public virtual string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			return this.DisplayValue_Obsolete(ref displayColor);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x004D5074 File Offset: 0x004D3274
		public sealed override void SetupContent()
		{
			LocalizedText displayName = this.DisplayName;
			ModContent.Request<Texture2D>(this.Texture, 2);
			this.SetStaticDefaults();
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x004D5090 File Offset: 0x004D3290
		protected override void ValidateType()
		{
			base.ValidateType();
			this.MustOverrideDisplayValue();
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x004D50A0 File Offset: 0x004D32A0
		[Obsolete]
		private unsafe void MustOverrideDisplayValue()
		{
			if (!LoaderUtils.HasOverride<InfoDisplay, InfoDisplay.DisplayValueMethodType>(this, (InfoDisplay t) => (InfoDisplay.DisplayValueMethodType)methodof(InfoDisplay.DisplayValue(Color*, Color*)).CreateDelegate(typeof(InfoDisplay.DisplayValueMethodType), t)) && !LoaderUtils.HasOverride<InfoDisplay, InfoDisplay.OldDisplayValueMethodType>(this, (InfoDisplay t) => (InfoDisplay.OldDisplayValueMethodType)methodof(InfoDisplay.DisplayValue(Color*)).CreateDelegate(typeof(InfoDisplay.OldDisplayValueMethodType), t)))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 2);
				defaultInterpolatedStringHandler.AppendFormatted<Type>(base.GetType());
				defaultInterpolatedStringHandler.AppendLiteral(" must override ");
				defaultInterpolatedStringHandler.AppendFormatted("DisplayValue");
				defaultInterpolatedStringHandler.AppendLiteral(".");
				throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x004D521E File Offset: 0x004D341E
		protected sealed override void Register()
		{
			ModTypeLookup<InfoDisplay>.Register(this);
			this.Type = InfoDisplayLoader.Add(this);
		}

		// Token: 0x020008D6 RID: 2262
		// (Invoke) Token: 0x0600527E RID: 21118
		private delegate string DisplayValueMethodType(ref Color displayColor, ref Color displayShadowColor);

		// Token: 0x020008D7 RID: 2263
		// (Invoke) Token: 0x06005282 RID: 21122
		private delegate string OldDisplayValueMethodType(ref Color displayColor);
	}
}
