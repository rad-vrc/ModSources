using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI
{
	// Token: 0x02000338 RID: 824
	public class GameTipsDisplay
	{
		// Token: 0x0600251D RID: 9501 RVA: 0x00568B58 File Offset: 0x00566D58
		public GameTipsDisplay()
		{
			this._tipsDefault = Language.FindAll(Lang.CreateDialogFilter("LoadingTips_Default."));
			this._tipsGamepad = Language.FindAll(Lang.CreateDialogFilter("LoadingTips_GamePad."));
			this._tipsKeyboard = Language.FindAll(Lang.CreateDialogFilter("LoadingTips_Keyboard."));
			this._lastTip = null;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00568BBC File Offset: 0x00566DBC
		public void Update()
		{
			double time = Main.gameTimeCache.TotalGameTime.TotalSeconds;
			this._currentTips.RemoveAll((GameTipsDisplay.GameTip x) => x.IsExpired(time));
			bool flag = true;
			using (List<GameTipsDisplay.GameTip>.Enumerator enumerator = this._currentTips.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsExpiring(time))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				this.AddNewTip(time);
			}
			foreach (GameTipsDisplay.GameTip gameTip in this._currentTips)
			{
				gameTip.Update(time);
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x00568CA4 File Offset: 0x00566EA4
		public void ClearTips()
		{
			this._currentTips.Clear();
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x00568CB4 File Offset: 0x00566EB4
		public void Draw()
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			float num = (float)Main.screenWidth;
			float y = (float)(Main.screenHeight - 150);
			float num2 = (float)Main.screenWidth * 0.5f;
			foreach (GameTipsDisplay.GameTip gameTip in this._currentTips)
			{
				if (gameTip.ScreenAnchorX >= -0.5f && gameTip.ScreenAnchorX <= 1.5f)
				{
					DynamicSpriteFont value = FontAssets.MouseText.Value;
					string text = value.CreateWrappedText(gameTip.Text, num2, Language.ActiveCulture.CultureInfo);
					if (text.Split(new char[]
					{
						'\n'
					}).Length > 2)
					{
						text = value.CreateWrappedText(gameTip.Text, num2 * 1.5f - 50f, Language.ActiveCulture.CultureInfo);
					}
					if (WorldGen.getGoodWorldGen)
					{
						string text2 = "";
						for (int i = text.Length - 1; i >= 0; i--)
						{
							text2 += text.Substring(i, 1);
						}
						text = text2;
					}
					else if (WorldGen.drunkWorldGenText)
					{
						text = string.Concat(Main.rand.Next(999999999));
						for (int j = 0; j < 14; j++)
						{
							if (Main.rand.Next(2) == 0)
							{
								text += Main.rand.Next(999999999);
							}
						}
					}
					Vector2 vector = value.MeasureString(text);
					float num3 = 1.1f;
					float num4 = 110f;
					if (vector.Y > num4)
					{
						num3 = num4 / vector.Y;
					}
					Vector2 vector2 = new Vector2(num * gameTip.ScreenAnchorX, y);
					vector2 -= vector * num3 * 0.5f;
					if (WorldGen.tenthAnniversaryWorldGen && !WorldGen.remixWorldGen)
					{
						ChatManager.DrawColorCodedStringWithShadow(spriteBatch, value, text, vector2, Color.HotPink, 0f, Vector2.Zero, new Vector2(num3, num3), -1f, 2f);
					}
					else
					{
						ChatManager.DrawColorCodedStringWithShadow(spriteBatch, value, text, vector2, Color.White, 0f, Vector2.Zero, new Vector2(num3, num3), -1f, 2f);
					}
				}
			}
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00568F2C File Offset: 0x0056712C
		private void AddNewTip(double currentTime)
		{
			string textKey = "UI.Back";
			List<LocalizedText> list = new List<LocalizedText>();
			list.AddRange(this._tipsDefault);
			if (PlayerInput.UsingGamepad)
			{
				list.AddRange(this._tipsGamepad);
			}
			else
			{
				list.AddRange(this._tipsKeyboard);
			}
			if (this._lastTip != null)
			{
				list.Remove(this._lastTip);
			}
			LocalizedText localizedText;
			if (list.Count == 0)
			{
				localizedText = LocalizedText.Empty;
			}
			else
			{
				localizedText = list[Main.rand.Next(list.Count)];
			}
			this._lastTip = localizedText;
			string key = localizedText.Key;
			if (Language.Exists(key))
			{
				textKey = key;
			}
			this._currentTips.Add(new GameTipsDisplay.GameTip(textKey, currentTime));
		}

		// Token: 0x0400490A RID: 18698
		private LocalizedText[] _tipsDefault;

		// Token: 0x0400490B RID: 18699
		private LocalizedText[] _tipsGamepad;

		// Token: 0x0400490C RID: 18700
		private LocalizedText[] _tipsKeyboard;

		// Token: 0x0400490D RID: 18701
		private readonly List<GameTipsDisplay.GameTip> _currentTips = new List<GameTipsDisplay.GameTip>();

		// Token: 0x0400490E RID: 18702
		private LocalizedText _lastTip;

		// Token: 0x0200071C RID: 1820
		private class GameTip
		{
			// Token: 0x1700040A RID: 1034
			// (get) Token: 0x060037C7 RID: 14279 RVA: 0x00611726 File Offset: 0x0060F926
			public string Text
			{
				get
				{
					if (this._textKey == null)
					{
						return "What?!";
					}
					return this._formattedText;
				}
			}

			// Token: 0x060037C8 RID: 14280 RVA: 0x0061173C File Offset: 0x0060F93C
			public bool IsExpired(double currentTime)
			{
				return currentTime >= this.SpawnTime + (double)this.Duration;
			}

			// Token: 0x060037C9 RID: 14281 RVA: 0x00611752 File Offset: 0x0060F952
			public bool IsExpiring(double currentTime)
			{
				return currentTime >= this.SpawnTime + (double)this.Duration - 1.0;
			}

			// Token: 0x060037CA RID: 14282 RVA: 0x00611774 File Offset: 0x0060F974
			public GameTip(string textKey, double spawnTime)
			{
				this._textKey = Language.GetText(textKey);
				this.SpawnTime = spawnTime;
				this.ScreenAnchorX = 2.5f;
				this.Duration = 11.5f;
				object obj = Lang.CreateDialogSubstitutionObject(null);
				this._formattedText = this._textKey.FormatWith(obj);
				this._formattedText = Lang.SupportGlyphs(this._formattedText);
			}

			// Token: 0x060037CB RID: 14283 RVA: 0x006117DC File Offset: 0x0060F9DC
			public void Update(double currentTime)
			{
				double num = currentTime - this.SpawnTime;
				if (num < 0.5)
				{
					this.ScreenAnchorX = MathHelper.SmoothStep(2.5f, 0.5f, (float)Utils.GetLerpValue(0.0, 0.5, num, true));
					return;
				}
				if (num >= (double)(this.Duration - 1f))
				{
					this.ScreenAnchorX = MathHelper.SmoothStep(0.5f, -1.5f, (float)Utils.GetLerpValue((double)(this.Duration - 1f), (double)this.Duration, num, true));
					return;
				}
				this.ScreenAnchorX = 0.5f;
			}

			// Token: 0x04006337 RID: 25399
			private const float APPEAR_FROM = 2.5f;

			// Token: 0x04006338 RID: 25400
			private const float APPEAR_TO = 0.5f;

			// Token: 0x04006339 RID: 25401
			private const float DISAPPEAR_TO = -1.5f;

			// Token: 0x0400633A RID: 25402
			private const float APPEAR_TIME = 0.5f;

			// Token: 0x0400633B RID: 25403
			private const float DISAPPEAR_TIME = 1f;

			// Token: 0x0400633C RID: 25404
			private const float DURATION = 11.5f;

			// Token: 0x0400633D RID: 25405
			private LocalizedText _textKey;

			// Token: 0x0400633E RID: 25406
			private string _formattedText;

			// Token: 0x0400633F RID: 25407
			public float ScreenAnchorX;

			// Token: 0x04006340 RID: 25408
			public readonly float Duration;

			// Token: 0x04006341 RID: 25409
			public readonly double SpawnTime;
		}
	}
}
