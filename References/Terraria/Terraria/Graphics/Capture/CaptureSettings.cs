using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Capture
{
	// Token: 0x020000FD RID: 253
	public class CaptureSettings
	{
		// Token: 0x06001640 RID: 5696 RVA: 0x004C7BD4 File Offset: 0x004C5DD4
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

		// Token: 0x04001337 RID: 4919
		public Rectangle Area;

		// Token: 0x04001338 RID: 4920
		public bool UseScaling = true;

		// Token: 0x04001339 RID: 4921
		public string OutputName;

		// Token: 0x0400133A RID: 4922
		public bool CaptureEntities = true;

		// Token: 0x0400133B RID: 4923
		public CaptureBiome Biome = CaptureBiome.DefaultPurity;

		// Token: 0x0400133C RID: 4924
		public bool CaptureMech;

		// Token: 0x0400133D RID: 4925
		public bool CaptureBackground;
	}
}
