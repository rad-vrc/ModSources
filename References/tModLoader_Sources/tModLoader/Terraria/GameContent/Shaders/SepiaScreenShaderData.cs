using System;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000577 RID: 1399
	public class SepiaScreenShaderData : ScreenShaderData
	{
		// Token: 0x060041D8 RID: 16856 RVA: 0x005F1178 File Offset: 0x005EF378
		public SepiaScreenShaderData(string passName) : base(passName)
		{
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x005F1184 File Offset: 0x005EF384
		public override void Update(GameTime gameTime)
		{
			float x = (Main.screenPosition.Y + (float)(Main.screenHeight / 2)) / 16f;
			float num = 1f - Utils.SmoothStep((float)Main.worldSurface, (float)Main.worldSurface + 30f, x);
			Vector3 value;
			value..ctor(0.191f, -0.054f, -0.221f);
			Vector3 vector = value;
			Vector3 value2 = vector * 0.5f;
			Vector3 value3;
			value3..ctor(0f, -0.03f, 0.15f);
			Vector3 value4;
			value4..ctor(-0.11f, 0.01f, 0.16f);
			float cloudAlpha = Main.cloudAlpha;
			float nightlightPower;
			float daylightPower;
			float moonPower;
			float dawnPower;
			SepiaScreenShaderData.GetDaylightPowers(out nightlightPower, out daylightPower, out moonPower, out dawnPower);
			float num2 = nightlightPower * 0.13f;
			if (Main.starGame)
			{
				float num3 = (float)Main.starGameMath(1.0) - 1f;
				nightlightPower = num3;
				daylightPower = 1f - num3;
				moonPower = num3;
				dawnPower = 1f - num3;
				num2 = nightlightPower * 0.13f;
			}
			else if (!Main.dayTime)
			{
				if (Main.GetMoonPhase() == MoonPhase.Full)
				{
					value..ctor(-0.19f, 0.01f, 0.22f);
					num2 += 0.07f * moonPower;
				}
				if (Main.bloodMoon)
				{
					value..ctor(0.2f, -0.1f, -0.221f);
					num2 = 0.2f;
				}
			}
			nightlightPower *= num;
			daylightPower *= num;
			moonPower *= num;
			dawnPower *= num;
			base.UseOpacity(1f);
			base.UseIntensity(1.4f - daylightPower * 0.2f);
			float value5 = 0.3f - num2 * nightlightPower;
			value5 = MathHelper.Lerp(value5, 0.1f, cloudAlpha);
			float value6 = 0.2f;
			value5 = MathHelper.Lerp(value5, value6, 1f - num);
			base.UseProgress(value5);
			Vector3 value7 = Vector3.Lerp(vector, value, moonPower);
			value7 = Vector3.Lerp(value7, value3, dawnPower);
			value7 = Vector3.Lerp(value7, value4, cloudAlpha);
			value7 = Vector3.Lerp(value7, value2, 1f - num);
			base.UseColor(value7);
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x005F1388 File Offset: 0x005EF588
		private static void GetDaylightPowers(out float nightlightPower, out float daylightPower, out float moonPower, out float dawnPower)
		{
			nightlightPower = 0f;
			daylightPower = 0f;
			moonPower = 0f;
			Vector2 dayTimeAsDirectionIn24HClock4 = Utils.GetDayTimeAsDirectionIn24HClock();
			Vector2 dayTimeAsDirectionIn24HClock2 = Utils.GetDayTimeAsDirectionIn24HClock(4.5f);
			Vector2 dayTimeAsDirectionIn24HClock3 = Utils.GetDayTimeAsDirectionIn24HClock(0f);
			float fromValue = Vector2.Dot(dayTimeAsDirectionIn24HClock4, dayTimeAsDirectionIn24HClock3);
			float fromValue2 = Vector2.Dot(dayTimeAsDirectionIn24HClock4, dayTimeAsDirectionIn24HClock2);
			nightlightPower = Utils.Remap(fromValue, -0.2f, 0.1f, 0f, 1f, true);
			daylightPower = Utils.Remap(fromValue, 0.1f, -1f, 0f, 1f, true);
			dawnPower = Utils.Remap(fromValue2, 0.66f, 1f, 0f, 1f, true);
			if (!Main.dayTime)
			{
				float num = (float)(Main.time / 32400.0) * 2f;
				if (num > 1f)
				{
					num = 2f - num;
				}
				moonPower = Utils.Remap(num, 0f, 0.25f, 0f, 1f, true);
			}
		}
	}
}
