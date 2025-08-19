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
	// Token: 0x02000441 RID: 1089
	public class TitleLinkButton
	{
		// Token: 0x06002BAF RID: 11183 RVA: 0x0059EAC0 File Offset: 0x0059CCC0
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
			spriteBatch.Draw(value, anchorPosition, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() / 2f, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x0059EBBC File Offset: 0x0059CDBC
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

		// Token: 0x06002BB1 RID: 11185 RVA: 0x0059EC2F File Offset: 0x0059CE2F
		private void TryClicking()
		{
			if (PlayerInput.IgnoreMouseInterface)
			{
				return;
			}
			if (!Main.mouseLeft || !Main.mouseLeftRelease)
			{
				return;
			}
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.mouseLeftRelease = false;
			this.OpenLink();
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x0059EC6C File Offset: 0x0059CE6C
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

		// Token: 0x04004FD9 RID: 20441
		private static Item _fakeItem = new Item();

		// Token: 0x04004FDA RID: 20442
		public string TooltipTextKey;

		// Token: 0x04004FDB RID: 20443
		public string LinkUrl;

		// Token: 0x04004FDC RID: 20444
		public Asset<Texture2D> Image;

		// Token: 0x04004FDD RID: 20445
		public Rectangle? FrameWhenNotSelected;

		// Token: 0x04004FDE RID: 20446
		public Rectangle? FrameWehnSelected;
	}
}
