using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Chat;

namespace PetRenamer.UI.MouseoverUI
{
	// Token: 0x0200000D RID: 13
	internal class MouseoverUI : UIState
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00004218 File Offset: 0x00002418
		private string GetPetName()
		{
			string ret = string.Empty;
			Rectangle mouse;
			mouse..ctor((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1);
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj.active && proj.owner < 255)
				{
					Player player = Main.player[proj.owner];
					if (player.active)
					{
						PRPlayer petPlayer = player.GetModPlayer<PRPlayer>();
						string petName;
						if (proj.type == petPlayer.petTypeLight)
						{
							petName = petPlayer.petNameLight;
						}
						else
						{
							if (proj.type != petPlayer.petTypeVanity)
							{
								goto IL_10B;
							}
							petName = petPlayer.petNameVanity;
						}
						Rectangle projRect = proj.getRect();
						projRect.Inflate(2, 2);
						if (Array.BinarySearch<int>(PetRenamer.ACTPetsWithSmallVerticalHitbox, proj.type) >= 0)
						{
							int offset = (int)(6f * proj.scale);
							projRect.Y -= offset;
							projRect.Height += offset;
						}
						if (mouse.Intersects(projRect))
						{
							MouseoverUI.drawColor = Main.MouseTextColorReal;
							ret = petName;
							break;
						}
					}
				}
				IL_10B:;
			}
			return ret;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004344 File Offset: 0x00002544
		public override void Update(GameTime gameTime)
		{
			if (Main.hoverItemName != string.Empty || Main.LocalPlayer.mouseInterface || Main.mouseText)
			{
				return;
			}
			base.Update(gameTime);
			int lastMouseX = Main.lastMouseX;
			int lastMouseYbak = Main.lastMouseY;
			int mouseXbak = Main.mouseX;
			int mouseYbak = Main.mouseY;
			int screenWidthbak = Main.screenWidth;
			int screenHeightbak = Main.screenHeight;
			PlayerInput.SetZoom_Unscaled();
			PlayerInput.SetZoom_MouseInWorld();
			MouseoverUI.drawString = this.GetPetName();
			Main.lastMouseX = lastMouseX;
			Main.lastMouseY = lastMouseYbak;
			Main.mouseX = mouseXbak;
			Main.mouseY = mouseYbak;
			Main.screenWidth = screenWidthbak;
			Main.screenHeight = screenHeightbak;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000043DC File Offset: 0x000025DC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (Main.hoverItemName != string.Empty || MouseoverUI.drawString == string.Empty || Main.LocalPlayer.mouseInterface || Main.mouseText)
			{
				return;
			}
			base.DrawSelf(spriteBatch);
			Main.LocalPlayer.cursorItemIconEnabled = false;
			Vector2 mousePos;
			mousePos..ctor((float)Main.mouseX, (float)Main.mouseY);
			mousePos.X += 10f;
			mousePos.Y += 10f;
			if (Main.ThickMouse)
			{
				mousePos.X += 6f;
				mousePos.Y += 6f;
			}
			DynamicSpriteFont value = FontAssets.MouseText.Value;
			Vector2 vector = value.MeasureString(MouseoverUI.drawString);
			if (mousePos.X + vector.X + 4f > (float)Main.screenWidth)
			{
				mousePos.X = (float)((int)((float)Main.screenWidth - vector.X - 4f));
			}
			if (mousePos.Y + vector.Y + 4f > (float)Main.screenHeight)
			{
				mousePos.Y = (float)((int)((float)Main.screenHeight - vector.Y - 4f));
			}
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, value, MouseoverUI.drawString, mousePos, MouseoverUI.drawColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
		}

		// Token: 0x04000050 RID: 80
		internal static string drawString = string.Empty;

		// Token: 0x04000051 RID: 81
		internal static Color drawColor = Color.White;
	}
}
