using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x0200039A RID: 922
	public abstract class ConfigElement : UIElement
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x00540915 File Offset: 0x0053EB15
		// (set) Token: 0x0600319C RID: 12700 RVA: 0x0054091D File Offset: 0x0053EB1D
		public int Index { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x00540926 File Offset: 0x0053EB26
		// (set) Token: 0x0600319E RID: 12702 RVA: 0x0054092E File Offset: 0x0053EB2E
		public bool Flashing { get; set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x00540937 File Offset: 0x0053EB37
		// (set) Token: 0x060031A0 RID: 12704 RVA: 0x0054093F File Offset: 0x0053EB3F
		protected Asset<Texture2D> PlayTexture { get; set; } = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay");

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x00540948 File Offset: 0x0053EB48
		// (set) Token: 0x060031A2 RID: 12706 RVA: 0x00540950 File Offset: 0x0053EB50
		protected Asset<Texture2D> DeleteTexture { get; set; } = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete");

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x00540959 File Offset: 0x0053EB59
		// (set) Token: 0x060031A4 RID: 12708 RVA: 0x00540961 File Offset: 0x0053EB61
		protected Asset<Texture2D> PlusTexture { get; set; } = UICommon.ButtonPlusTexture;

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x0054096A File Offset: 0x0053EB6A
		// (set) Token: 0x060031A6 RID: 12710 RVA: 0x00540972 File Offset: 0x0053EB72
		protected Asset<Texture2D> UpDownTexture { get; set; } = UICommon.ButtonUpDownTexture;

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x0054097B File Offset: 0x0053EB7B
		// (set) Token: 0x060031A8 RID: 12712 RVA: 0x00540983 File Offset: 0x0053EB83
		protected Asset<Texture2D> CollapsedTexture { get; set; } = UICommon.ButtonCollapsedTexture;

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x0054098C File Offset: 0x0053EB8C
		// (set) Token: 0x060031AA RID: 12714 RVA: 0x00540994 File Offset: 0x0053EB94
		protected Asset<Texture2D> ExpandedTexture { get; set; } = UICommon.ButtonExpandedTexture;

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x0054099D File Offset: 0x0053EB9D
		// (set) Token: 0x060031AC RID: 12716 RVA: 0x005409A5 File Offset: 0x0053EBA5
		protected internal PropertyFieldWrapper MemberInfo { get; set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x005409AE File Offset: 0x0053EBAE
		// (set) Token: 0x060031AE RID: 12718 RVA: 0x005409B6 File Offset: 0x0053EBB6
		protected object Item { get; set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x005409BF File Offset: 0x0053EBBF
		// (set) Token: 0x060031B0 RID: 12720 RVA: 0x005409C7 File Offset: 0x0053EBC7
		protected IList List { get; set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x005409D0 File Offset: 0x0053EBD0
		// (set) Token: 0x060031B2 RID: 12722 RVA: 0x005409D8 File Offset: 0x0053EBD8
		protected bool NullAllowed { get; set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060031B3 RID: 12723 RVA: 0x005409E1 File Offset: 0x0053EBE1
		// (set) Token: 0x060031B4 RID: 12724 RVA: 0x005409E9 File Offset: 0x0053EBE9
		protected internal Func<string> TextDisplayFunction { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x005409F2 File Offset: 0x0053EBF2
		// (set) Token: 0x060031B6 RID: 12726 RVA: 0x005409FA File Offset: 0x0053EBFA
		protected Func<string> TooltipFunction { get; set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x00540A03 File Offset: 0x0053EC03
		// (set) Token: 0x060031B8 RID: 12728 RVA: 0x00540A0B File Offset: 0x0053EC0B
		protected bool DrawLabel { get; set; } = true;

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060031B9 RID: 12729 RVA: 0x00540A14 File Offset: 0x0053EC14
		// (set) Token: 0x060031BA RID: 12730 RVA: 0x00540A1C File Offset: 0x0053EC1C
		protected bool ReloadRequired { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060031BB RID: 12731 RVA: 0x00540A25 File Offset: 0x0053EC25
		// (set) Token: 0x060031BC RID: 12732 RVA: 0x00540A2D File Offset: 0x0053EC2D
		protected bool ShowReloadRequiredTooltip { get; set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060031BD RID: 12733 RVA: 0x00540A36 File Offset: 0x0053EC36
		// (set) Token: 0x060031BE RID: 12734 RVA: 0x00540A3E File Offset: 0x0053EC3E
		protected object OldValue { get; set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060031BF RID: 12735 RVA: 0x00540A47 File Offset: 0x0053EC47
		protected bool ValueChanged
		{
			get
			{
				return !ConfigManager.ObjectEquals(this.OldValue, this.GetObject());
			}
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x00540A60 File Offset: 0x0053EC60
		public ConfigElement()
		{
			this.Width.Set(0f, 1f);
			this.Height.Set(30f, 0f);
		}

		/// <summary>
		/// Bind must always be called after the ctor and serves to facilitate a convenient inheritance workflow for custom ConfigElemets from mods.
		/// </summary>
		// Token: 0x060031C1 RID: 12737 RVA: 0x00540AFA File Offset: 0x0053ECFA
		public void Bind(PropertyFieldWrapper memberInfo, object item, IList array, int index)
		{
			this.MemberInfo = memberInfo;
			this.Item = item;
			this.List = array;
			this.Index = index;
			this.backgroundColor = UICommon.DefaultUIBlue;
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x00540B24 File Offset: 0x0053ED24
		public virtual void OnBind()
		{
			this.LabelAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<LabelKeyAttribute>(this.MemberInfo, this.Item, this.List);
			this.Label = ConfigManager.GetLocalizedLabel(this.MemberInfo);
			this.TextDisplayFunction = (() => this.Label);
			this.TooltipAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<TooltipKeyAttribute>(this.MemberInfo, this.Item, this.List);
			string tooltip = ConfigManager.GetLocalizedTooltip(this.MemberInfo);
			if (tooltip != null)
			{
				this.TooltipFunction = (() => tooltip);
			}
			this.BackgroundColorAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<BackgroundColorAttribute>(this.MemberInfo, this.Item, this.List);
			if (this.BackgroundColorAttribute != null)
			{
				this.backgroundColor = this.BackgroundColorAttribute.Color;
			}
			this.RangeAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<RangeAttribute>(this.MemberInfo, this.Item, this.List);
			this.IncrementAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<IncrementAttribute>(this.MemberInfo, this.Item, this.List);
			this.NullAllowed = (ConfigManager.GetCustomAttributeFromMemberThenMemberType<NullAllowedAttribute>(this.MemberInfo, this.Item, this.List) != null);
			this.JsonDefaultValueAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<JsonDefaultValueAttribute>(this.MemberInfo, this.Item, this.List);
			this.ShowReloadRequiredTooltip = (ConfigManager.GetCustomAttributeFromMemberThenMemberType<ReloadRequiredAttribute>(this.MemberInfo, this.Item, this.List) != null);
			if (this.ShowReloadRequiredTooltip && this.List == null)
			{
				ModConfig modConfig = this.Item as ModConfig;
				if (modConfig != null)
				{
					this.ReloadRequired = true;
					ModConfig loadTimeConfig = ConfigManager.GetLoadTimeConfig(modConfig.Mod, modConfig.Name);
					this.OldValue = this.MemberInfo.GetValue(loadTimeConfig);
				}
			}
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x00540CE0 File Offset: 0x0053EEE0
		protected virtual void SetObject(object value)
		{
			if (this.List != null)
			{
				this.List[this.Index] = value;
				Interface.modConfig.SetPendingChanges(true);
				return;
			}
			if (!this.MemberInfo.CanWrite)
			{
				return;
			}
			this.MemberInfo.SetValue(this.Item, value);
			Interface.modConfig.SetPendingChanges(true);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x00540D3E File Offset: 0x0053EF3E
		protected virtual object GetObject()
		{
			if (this.List != null)
			{
				return this.List[this.Index];
			}
			return this.MemberInfo.GetValue(this.Item);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x00540D6C File Offset: 0x0053EF6C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			float settingsWidth = dimensions.Width + 1f;
			Vector2 vector = new Vector2(dimensions.X, dimensions.Y);
			Vector2 baseScale;
			baseScale..ctor(0.8f);
			Color color = base.IsMouseHovering ? Color.White : Color.White;
			if (!this.MemberInfo.CanWrite)
			{
				color = Color.Gray;
			}
			Color panelColor = base.IsMouseHovering ? this.backgroundColor : this.backgroundColor.MultiplyRGBA(new Color(180, 180, 180));
			Vector2 position = vector;
			if (this.Flashing)
			{
				float ratio = Utils.Turn01ToCyclic010((float)(Interface.modConfig.UpdateCount % 120) / 120f) * 0.5f + 0.5f;
				panelColor = Color.Lerp(panelColor, Color.White, MathF.Pow(ratio, 2f));
			}
			ConfigElement.DrawPanel2(spriteBatch, position, TextureAssets.SettingsPanel.Value, settingsWidth, dimensions.Height, panelColor);
			if (this.DrawLabel)
			{
				position.X += 8f;
				position.Y += 8f;
				string label = this.TextDisplayFunction();
				if (this.ReloadRequired && this.ValueChanged)
				{
					label = label + " - [c/FF0000:" + Language.GetTextValue("tModLoader.ModReloadRequired") + "]";
				}
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, label, position, color, 0f, Vector2.Zero, baseScale, settingsWidth, 2f);
			}
			if (base.IsMouseHovering && this.TooltipFunction != null)
			{
				string tooltip = this.TooltipFunction();
				if (this.ShowReloadRequiredTooltip)
				{
					tooltip += (string.IsNullOrEmpty(tooltip) ? "" : "\n");
					tooltip = string.Concat(new string[]
					{
						tooltip,
						"[c/",
						Color.Orange.Hex3(),
						":",
						Language.GetTextValue("tModLoader.ModReloadRequiredMemberTooltip"),
						"]"
					});
				}
				UIModConfig.Tooltip = tooltip;
			}
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x00540F8D File Offset: 0x0053F18D
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.Flashing = false;
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x00540FA0 File Offset: 0x0053F1A0
		public static void DrawPanel2(SpriteBatch spriteBatch, Vector2 position, Texture2D texture, float width, float height, Color color)
		{
			spriteBatch.Draw(texture, position + new Vector2(0f, 2f), new Rectangle?(new Rectangle(0, 2, 1, 1)), color, 0f, Vector2.Zero, new Vector2(2f, height - 4f), 0, 0f);
			spriteBatch.Draw(texture, position + new Vector2(width - 2f, 2f), new Rectangle?(new Rectangle(0, 2, 1, 1)), color, 0f, Vector2.Zero, new Vector2(2f, height - 4f), 0, 0f);
			spriteBatch.Draw(texture, position + new Vector2(2f, 0f), new Rectangle?(new Rectangle(2, 0, 1, 1)), color, 0f, Vector2.Zero, new Vector2(width - 4f, 2f), 0, 0f);
			spriteBatch.Draw(texture, position + new Vector2(2f, height - 2f), new Rectangle?(new Rectangle(2, 0, 1, 1)), color, 0f, Vector2.Zero, new Vector2(width - 4f, 2f), 0, 0f);
			spriteBatch.Draw(texture, position + new Vector2(2f, 2f), new Rectangle?(new Rectangle(2, 2, 1, 1)), color, 0f, Vector2.Zero, new Vector2(width - 4f, (height - 4f) / 2f), 0, 0f);
			spriteBatch.Draw(texture, position + new Vector2(2f, 2f + (height - 4f) / 2f), new Rectangle?(new Rectangle(2, 16, 1, 1)), color, 0f, Vector2.Zero, new Vector2(width - 4f, (height - 4f) / 2f), 0, 0f);
		}

		// Token: 0x04001D55 RID: 7509
		private Color backgroundColor;

		// Token: 0x04001D57 RID: 7511
		public const int flashRate = 120;

		// Token: 0x04001D62 RID: 7522
		protected LabelKeyAttribute LabelAttribute;

		// Token: 0x04001D63 RID: 7523
		protected string Label;

		// Token: 0x04001D64 RID: 7524
		protected TooltipKeyAttribute TooltipAttribute;

		// Token: 0x04001D65 RID: 7525
		protected BackgroundColorAttribute BackgroundColorAttribute;

		// Token: 0x04001D66 RID: 7526
		protected RangeAttribute RangeAttribute;

		// Token: 0x04001D67 RID: 7527
		protected IncrementAttribute IncrementAttribute;

		// Token: 0x04001D68 RID: 7528
		protected JsonDefaultValueAttribute JsonDefaultValueAttribute;
	}
}
