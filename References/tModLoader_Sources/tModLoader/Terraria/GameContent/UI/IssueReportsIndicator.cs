using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004CD RID: 1229
	public class IssueReportsIndicator
	{
		// Token: 0x06003AB7 RID: 15031 RVA: 0x005ABDEC File Offset: 0x005A9FEC
		public void AttemptLettingPlayerKnow()
		{
			this.Setup();
			this._shouldBeShowing = true;
			if (SoundEngine.LegacySoundPlayer != null)
			{
				SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, null, null);
			}
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x005ABE22 File Offset: 0x005AA022
		public void Hide()
		{
			this._shouldBeShowing = false;
			this._displayUpPercent = 0f;
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x005ABE36 File Offset: 0x005AA036
		private void OpenUI()
		{
			this.Setup();
			Main.OpenReportsMenu();
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x005ABE43 File Offset: 0x005AA043
		private void Setup()
		{
			this._buttonTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/IssueButton");
			this._buttonOutlineTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/IssueButton_Outline");
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x005ABE70 File Offset: 0x005AA070
		public void Draw(SpriteBatch spriteBatch)
		{
			bool shouldBeShowing = this._shouldBeShowing;
			this._displayUpPercent = MathHelper.Clamp(this._displayUpPercent + (float)shouldBeShowing.ToDirectionInt(), 0f, 1f);
			if (this._displayUpPercent == 0f)
			{
				return;
			}
			Texture2D value = this._buttonTexture.Value;
			Vector2 vector3 = Main.ScreenSize.ToVector2() + new Vector2(40f, -80f);
			Vector2 value2 = vector3 + new Vector2(-80f, 0f);
			Vector2 vector2 = Vector2.Lerp(vector3, value2, this._displayUpPercent);
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			Vector2 origin = rectangle.Size() / 2f;
			bool flag = false;
			if (Utils.CenteredRectangle(vector2, rectangle.Size()).Contains(Main.MouseScreen.ToPoint()))
			{
				flag = true;
				string textValue = Language.GetTextValue("UI.IssueReporterHasThingsToShow");
				Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
				if (Main.mouseLeft)
				{
					this.OpenUI();
					this.Hide();
					return;
				}
			}
			float scale = 1f;
			spriteBatch.Draw(value, vector2, new Rectangle?(rectangle), Color.White, 0f, origin, scale, 0, 0f);
			if (flag)
			{
				Texture2D value3 = this._buttonOutlineTexture.Value;
				Rectangle rectangle2 = value3.Frame(1, 1, 0, 0, 0, 0);
				spriteBatch.Draw(value3, vector2, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() / 2f, scale, 0, 0f);
			}
		}

		// Token: 0x040054BC RID: 21692
		private float _displayUpPercent;

		// Token: 0x040054BD RID: 21693
		private bool _shouldBeShowing;

		// Token: 0x040054BE RID: 21694
		private Asset<Texture2D> _buttonTexture;

		// Token: 0x040054BF RID: 21695
		private Asset<Texture2D> _buttonOutlineTexture;
	}
}
