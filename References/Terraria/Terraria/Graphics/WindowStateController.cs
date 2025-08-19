using System;
using System.Drawing;
using System.Windows.Forms;
using ReLogic.OS;

namespace Terraria.Graphics
{
	// Token: 0x020000F6 RID: 246
	public class WindowStateController
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x004C4B16 File Offset: 0x004C2D16
		public bool CanMoveWindowAcrossScreens
		{
			get
			{
				return Platform.IsWindows;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x004C4B1D File Offset: 0x004C2D1D
		public string ScreenDeviceName
		{
			get
			{
				if (!Platform.IsWindows)
				{
					return "";
				}
				return Main.instance.Window.ScreenDeviceName;
			}
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x004C4B3C File Offset: 0x004C2D3C
		public void TryMovingToScreen(string screenDeviceName)
		{
			if (!this.CanMoveWindowAcrossScreens)
			{
				return;
			}
			Rectangle rectangle;
			if (!this.TryGetBounds(screenDeviceName, out rectangle))
			{
				return;
			}
			if (!this.IsVisibleOnAnyScreen(rectangle))
			{
				return;
			}
			Form form = (Form)Control.FromHandle(Main.instance.Window.Handle);
			if (!this.WouldViewFitInScreen(form.Bounds, rectangle))
			{
				return;
			}
			form.Location = new Point(rectangle.Width / 2 - form.Width / 2 + rectangle.X, rectangle.Height / 2 - form.Height / 2 + rectangle.Y);
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x004C4BD4 File Offset: 0x004C2DD4
		private bool TryGetBounds(string screenDeviceName, out Rectangle bounds)
		{
			bounds = default(Rectangle);
			foreach (Screen screen in Screen.AllScreens)
			{
				if (screen.DeviceName == screenDeviceName)
				{
					bounds = screen.Bounds;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x004C4C1D File Offset: 0x004C2E1D
		private bool WouldViewFitInScreen(Rectangle view, Rectangle screen)
		{
			return view.Width <= screen.Width && view.Height <= screen.Height;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x004C4C44 File Offset: 0x004C2E44
		private bool IsVisibleOnAnyScreen(Rectangle rect)
		{
			Screen[] allScreens = Screen.AllScreens;
			for (int i = 0; i < allScreens.Length; i++)
			{
				if (allScreens[i].WorkingArea.IntersectsWith(rect))
				{
					return true;
				}
			}
			return false;
		}
	}
}
