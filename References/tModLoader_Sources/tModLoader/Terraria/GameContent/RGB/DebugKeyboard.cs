using System;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000581 RID: 1409
	internal class DebugKeyboard : RgbDevice
	{
		// Token: 0x060041FD RID: 16893 RVA: 0x005F333B File Offset: 0x005F153B
		private DebugKeyboard(Fragment fragment) : base(4, 6, fragment, new DeviceColorProfile())
		{
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x005F334C File Offset: 0x005F154C
		public static DebugKeyboard Create()
		{
			int num = 400;
			int num2 = 100;
			Point[] array = new Point[num * num2];
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					array[i * num + j] = new Point(j / 10, i / 10);
				}
			}
			Vector2[] array2 = new Vector2[num * num2];
			for (int k = 0; k < num2; k++)
			{
				for (int l = 0; l < num; l++)
				{
					array2[k * num + l] = new Vector2((float)l / (float)num2, (float)k / (float)num2);
				}
			}
			return new DebugKeyboard(Fragment.FromCustom(array, array2));
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x005F33FA File Offset: 0x005F15FA
		public override void Present()
		{
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x005F33FC File Offset: 0x005F15FC
		public override void DebugDraw(IDebugDrawer drawer, Vector2 position, float scale)
		{
			for (int i = 0; i < base.LedCount; i++)
			{
				Vector2 ledCanvasPosition = base.GetLedCanvasPosition(i);
				drawer.DrawSquare(new Vector4(ledCanvasPosition * scale + position, scale / 100f, scale / 100f), new Color(base.GetUnprocessedLedColor(i)));
			}
		}
	}
}
