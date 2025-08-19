using System;
using SDL2;

namespace System.Windows.Forms
{
	// Token: 0x02000020 RID: 32
	public static class MessageBox
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00005B0C File Offset: 0x00003D0C
		public static DialogResult Show(string msg, string title, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
		{
			SDL.SDL_MessageBoxData sdl_MessageBoxData = default(SDL.SDL_MessageBoxData);
			sdl_MessageBoxData.flags = icon;
			sdl_MessageBoxData.message = msg;
			sdl_MessageBoxData.title = title;
			SDL.SDL_MessageBoxButtonData[] buttons2;
			switch (buttons)
			{
			case MessageBoxButtons.OK:
				buttons2 = new SDL.SDL_MessageBoxButtonData[]
				{
					MessageBox.OKButton
				};
				break;
			case MessageBoxButtons.OKCancel:
				buttons2 = new SDL.SDL_MessageBoxButtonData[]
				{
					MessageBox.CancelButton,
					MessageBox.OKButton
				};
				break;
			case MessageBoxButtons.YesNoCancel:
				buttons2 = new SDL.SDL_MessageBoxButtonData[]
				{
					MessageBox.CancelButton,
					MessageBox.NoButton,
					MessageBox.YesButton
				};
				break;
			case MessageBoxButtons.YesNo:
				buttons2 = new SDL.SDL_MessageBoxButtonData[]
				{
					MessageBox.NoButton,
					MessageBox.YesButton
				};
				break;
			case MessageBoxButtons.RetryCancel:
				buttons2 = new SDL.SDL_MessageBoxButtonData[]
				{
					MessageBox.CancelButton,
					MessageBox.RetryButton
				};
				break;
			default:
				throw new NotImplementedException();
			}
			sdl_MessageBoxData.buttons = buttons2;
			SDL.SDL_MessageBoxData msgBox = sdl_MessageBoxData;
			msgBox.numbuttons = msgBox.buttons.Length;
			int buttonid;
			SDL.SDL_ShowMessageBox(ref msgBox, ref buttonid);
			return (DialogResult)buttonid;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005C2C File Offset: 0x00003E2C
		// Note: this type is marked as 'beforefieldinit'.
		static MessageBox()
		{
			SDL.SDL_MessageBoxButtonData sdl_MessageBoxButtonData = default(SDL.SDL_MessageBoxButtonData);
			sdl_MessageBoxButtonData.flags = 1;
			sdl_MessageBoxButtonData.buttonid = 1;
			sdl_MessageBoxButtonData.text = "OK";
			MessageBox.OKButton = sdl_MessageBoxButtonData;
			sdl_MessageBoxButtonData = default(SDL.SDL_MessageBoxButtonData);
			sdl_MessageBoxButtonData.flags = 2;
			sdl_MessageBoxButtonData.buttonid = 2;
			sdl_MessageBoxButtonData.text = "Cancel";
			MessageBox.CancelButton = sdl_MessageBoxButtonData;
			sdl_MessageBoxButtonData = default(SDL.SDL_MessageBoxButtonData);
			sdl_MessageBoxButtonData.flags = 1;
			sdl_MessageBoxButtonData.buttonid = 6;
			sdl_MessageBoxButtonData.text = "Yes";
			MessageBox.YesButton = sdl_MessageBoxButtonData;
			sdl_MessageBoxButtonData = default(SDL.SDL_MessageBoxButtonData);
			sdl_MessageBoxButtonData.buttonid = 7;
			sdl_MessageBoxButtonData.text = "No";
			MessageBox.NoButton = sdl_MessageBoxButtonData;
			sdl_MessageBoxButtonData = default(SDL.SDL_MessageBoxButtonData);
			sdl_MessageBoxButtonData.flags = 1;
			sdl_MessageBoxButtonData.buttonid = 4;
			sdl_MessageBoxButtonData.text = "Retry";
			MessageBox.RetryButton = sdl_MessageBoxButtonData;
		}

		// Token: 0x04000080 RID: 128
		private static SDL.SDL_MessageBoxButtonData OKButton;

		// Token: 0x04000081 RID: 129
		private static SDL.SDL_MessageBoxButtonData CancelButton;

		// Token: 0x04000082 RID: 130
		private static SDL.SDL_MessageBoxButtonData YesButton;

		// Token: 0x04000083 RID: 131
		private static SDL.SDL_MessageBoxButtonData NoButton;

		// Token: 0x04000084 RID: 132
		private static SDL.SDL_MessageBoxButtonData RetryButton;
	}
}
