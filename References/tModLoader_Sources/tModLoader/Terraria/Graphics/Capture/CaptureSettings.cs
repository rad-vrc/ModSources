using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Capture
{
	// Token: 0x0200047A RID: 1146
	public class CaptureSettings
	{
		// Token: 0x06003793 RID: 14227 RVA: 0x00589AA4 File Offset: 0x00587CA4
		public CaptureSettings()
		{
			DateTime dateTime = DateTime.Now.ToLocalTime();
			this.OutputName = string.Concat(new string[]
			{
				"Capture ",
				dateTime.Year.ToString("D4"),
				"-",
				dateTime.Month.ToString("D2"),
				"-",
				dateTime.Day.ToString("D2"),
				" ",
				dateTime.Hour.ToString("D2"),
				"_",
				dateTime.Minute.ToString("D2"),
				"_",
				dateTime.Second.ToString("D2")
			});
		}

		// Token: 0x0400513F RID: 20799
		public Rectangle Area;

		// Token: 0x04005140 RID: 20800
		public bool UseScaling = true;

		// Token: 0x04005141 RID: 20801
		public string OutputName;

		// Token: 0x04005142 RID: 20802
		public bool CaptureEntities = true;

		// Token: 0x04005143 RID: 20803
		public CaptureBiome Biome = CaptureBiome.DefaultPurity;

		// Token: 0x04005144 RID: 20804
		public bool CaptureMech;

		// Token: 0x04005145 RID: 20805
		public bool CaptureBackground;
	}
}
