using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	/// <summary>
	/// A text panel that supports hover and click sounds, hover colors, and alternate colors.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	// Token: 0x0200023F RID: 575
	public class UIButton<T> : UIAutoScaleTextTextPanel<T>
	{
		// Token: 0x0600290C RID: 10508 RVA: 0x00510DE8 File Offset: 0x0050EFE8
		public UIButton(T text, float textScaleMax = 1f, bool large = false) : base(text, textScaleMax, large)
		{
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00510E3C File Offset: 0x0050F03C
		public override void Recalculate()
		{
			base.Recalculate();
			Color value = this._panelColor.GetValueOrDefault();
			if (this._panelColor == null)
			{
				value = this.BackgroundColor;
				this._panelColor = new Color?(value);
			}
			value = this._borderColor.GetValueOrDefault();
			if (this._borderColor == null)
			{
				value = this.BorderColor;
				this._borderColor = new Color?(value);
			}
			value = this.AltPanelColor.GetValueOrDefault();
			if (this.AltPanelColor == null)
			{
				value = this.BackgroundColor;
				this.AltPanelColor = new Color?(value);
			}
			value = this.AltBorderColor.GetValueOrDefault();
			if (this.AltBorderColor == null)
			{
				value = this.BorderColor;
				this.AltBorderColor = new Color?(value);
			}
			value = this.AltHoverPanelColor.GetValueOrDefault();
			if (this.AltHoverPanelColor == null)
			{
				value = this.HoverPanelColor;
				this.AltHoverPanelColor = new Color?(value);
			}
			value = this.AltHoverBorderColor.GetValueOrDefault();
			if (this.AltHoverBorderColor == null)
			{
				value = this.HoverBorderColor;
				this.AltHoverBorderColor = new Color?(value);
			}
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00510F58 File Offset: 0x0050F158
		protected void SetPanelColors()
		{
			bool altCondition = this.UseAltColors();
			if (base.IsMouseHovering)
			{
				this.BackgroundColor = (altCondition ? this.AltHoverPanelColor.Value : this.HoverPanelColor);
				this.BorderColor = (altCondition ? this.AltHoverBorderColor.Value : this.HoverBorderColor);
				return;
			}
			this.BackgroundColor = (altCondition ? this.AltPanelColor.Value : this._panelColor.Value);
			this.BorderColor = (altCondition ? this.AltBorderColor.Value : this._borderColor.Value);
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00510FF4 File Offset: 0x0050F1F4
		public override void OnActivate()
		{
			this.SetPanelColors();
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x00510FFC File Offset: 0x0050F1FC
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetPanelColors();
			if (base.IsMouseHovering)
			{
				string text2;
				if (!this.UseAltColors())
				{
					ref T ptr = ref this.HoverText;
					T t = default(T);
					if (t == null)
					{
						t = this.HoverText;
						ptr = ref t;
						if (t == null)
						{
							text2 = null;
							goto IL_91;
						}
					}
					text2 = ptr.ToString();
				}
				else
				{
					ref T ptr2 = ref this.AltHoverText;
					T t = default(T);
					if (t == null)
					{
						t = this.AltHoverText;
						ptr2 = ref t;
						if (t == null)
						{
							text2 = null;
							goto IL_91;
						}
					}
					text2 = ptr2.ToString();
				}
				IL_91:
				string text = text2;
				if (text == null)
				{
					return;
				}
				if (this.TooltipText)
				{
					UICommon.TooltipMouseText(text);
					return;
				}
				Main.instance.MouseText(text, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x005110C0 File Offset: 0x0050F2C0
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			if (this.HoverSound != null)
			{
				SoundStyle value = this.HoverSound.Value;
				SoundEngine.PlaySound(value, null, null);
			}
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x00511100 File Offset: 0x0050F300
		public override void LeftClick(UIMouseEvent evt)
		{
			base.LeftClick(evt);
			if (this.ClickSound != null)
			{
				SoundStyle value = this.ClickSound.Value;
				SoundEngine.PlaySound(value, null, null);
			}
		}

		// Token: 0x04001A03 RID: 6659
		public SoundStyle? HoverSound;

		// Token: 0x04001A04 RID: 6660
		public SoundStyle? ClickSound;

		// Token: 0x04001A05 RID: 6661
		public T HoverText;

		// Token: 0x04001A06 RID: 6662
		public T AltHoverText;

		// Token: 0x04001A07 RID: 6663
		public bool TooltipText;

		// Token: 0x04001A08 RID: 6664
		public Color HoverPanelColor = UICommon.DefaultUIBlue;

		// Token: 0x04001A09 RID: 6665
		public Color HoverBorderColor = UICommon.DefaultUIBorderMouseOver;

		// Token: 0x04001A0A RID: 6666
		public Color? AltPanelColor;

		// Token: 0x04001A0B RID: 6667
		public Color? AltBorderColor;

		// Token: 0x04001A0C RID: 6668
		public Color? AltHoverPanelColor;

		// Token: 0x04001A0D RID: 6669
		public Color? AltHoverBorderColor;

		// Token: 0x04001A0E RID: 6670
		public Func<bool> UseAltColors = () => false;

		// Token: 0x04001A0F RID: 6671
		private Color? _panelColor;

		// Token: 0x04001A10 RID: 6672
		private Color? _borderColor;
	}
}
