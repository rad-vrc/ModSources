using System;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000243 RID: 579
	internal class DebugKeyboard : RgbDevice
	{
		// Token: 0x06001F28 RID: 7976 RVA: 0x00511182 File Offset: 0x0050F382
		private DebugKeyboard(Fragment fragment) : base(4, 6, fragment, new DeviceColorProfile())
		{
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00511194 File Offset: 0x0050F394
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

		// Token: 0x06001F2A RID: 7978 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Present()
		{
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00511244 File Offset: 0x0050F444
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
