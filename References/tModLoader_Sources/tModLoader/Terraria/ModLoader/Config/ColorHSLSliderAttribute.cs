using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Add this attribute to a Color item and the UI will present a Hue, Saturation, and Lightness sliders rather than Red, Green, and Blue sliders. Pass in false to skip Saturation and Lightness.
	/// </summary>
	// Token: 0x02000382 RID: 898
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class ColorHSLSliderAttribute : Attribute
	{
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x0053D549 File Offset: 0x0053B749
		public bool ShowSaturationAndLightness { get; }

		// Token: 0x060030D7 RID: 12503 RVA: 0x0053D551 File Offset: 0x0053B751
		public ColorHSLSliderAttribute(bool showSaturationAndLightness = true)
		{
			this.ShowSaturationAndLightness = showSaturationAndLightness;
		}
	}
}
