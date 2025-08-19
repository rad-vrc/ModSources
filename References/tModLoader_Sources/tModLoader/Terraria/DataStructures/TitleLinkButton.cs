using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x0200073C RID: 1852
	public class TitleLinkButton
	{
		// Token: 0x06004B3B RID: 19259 RVA: 0x00669AE0 File Offset: 0x00667CE0
		public void Draw(SpriteBatch spriteBatch, Vector2 anchorPosition)
		{
			Rectangle r = this.Image.Frame(1, 1, 0, 0, 0, 0);
			if (this.FrameWhenNotSelected != null)
			{
				r = this.FrameWhenNotSelected.Value;
			}
			Vector2 vector = r.Size();
			Vector2 vector2 = anchorPosition - vector / 2f;
			bool flag = false;
			if (Main.MouseScreen.Between(vector2, vector2 + vector))
			{
				Main.LocalPlayer.mouseInterface = true;
				flag = true;
				this.DrawTooltip();
				this.TryClicking();
			}
			Rectangle? rectangle = flag ? this.FrameWehnSelected : this.FrameWhenNotSelected;
			Rectangle rectangle2 = this.Image.Frame(1, 1, 0, 0, 0, 0);
			if (rectangle != null)
			{
				rectangle2 = rectangle.Value;
			}
			Texture2D value = this.Image.Value;
			spriteBatch.Draw(value, anchorPosition, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() / 2f, 1f, 0, 0f);
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x00669BDC File Offset: 0x00667DDC
		private void DrawTooltip()
		{
			Item fakeItem = TitleLinkButton._fakeItem;
			fakeItem.SetDefaults(0, true, null);
			string textValue = Language.GetTextValue(this.TooltipTextKey);
			fakeItem.SetNameOverride(textValue);
			fakeItem.type = 1;
			fakeItem.scale = 0f;
			fakeItem.rare = 8;
			fakeItem.value = -1;
			Main.HoverItem = TitleLinkButton._fakeItem;
			Main.instance.MouseText("", 0, 0, -1, -1, -1, -1, 0);
			Main.mouseText = true;
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x00669C4F File Offset: 0x00667E4F
		private void TryClicking()
		{
			if (!PlayerInput.IgnoreMouseInterface && Main.mouseLeft && Main.mouseLeftRelease)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.mouseLeftRelease = false;
				this.OpenLink();
			}
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x00669C88 File Offset: 0x00667E88
		private void OpenLink()
		{
			try
			{
				Platform.Get<IPathService>().OpenURL(this.LinkUrl);
			}
			catch
			{
				Console.WriteLine("Failed to open link?!");
			}
		}

		// Token: 0x04006060 RID: 24672
		private static Item _fakeItem = new Item();

		// Token: 0x04006061 RID: 24673
		public string TooltipTextKey;

		// Token: 0x04006062 RID: 24674
		public string LinkUrl;

		// Token: 0x04006063 RID: 24675
		public Asset<Texture2D> Image;

		// Token: 0x04006064 RID: 24676
		public Rectangle? FrameWhenNotSelected;

		// Token: 0x04006065 RID: 24677
		public Rectangle? FrameWehnSelected;
	}
}
