using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200031C RID: 796
	public class NPCKillCounterInfoElement : IBestiaryInfoElement
	{
		// Token: 0x06002435 RID: 9269 RVA: 0x0055941C File Offset: 0x0055761C
		public NPCKillCounterInfoElement(int npcNetId)
		{
			this._instance = new NPC();
			this._instance.SetDefaults(npcNetId, new NPCSpawnParams
			{
				gameModeData = GameModeData.NormalMode,
				strengthMultiplierOverride = null
			});
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00559468 File Offset: 0x00557668
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			int? killCount = this.GetKillCount();
			if (killCount == null || killCount.Value < 1)
			{
				return null;
			}
			UIElement uielement = new UIElement();
			uielement.Width = new StyleDimension(0f, 1f);
			uielement.Height = new StyleDimension(109f, 0f);
			if (killCount != null)
			{
				bool flag = killCount.Value > 0;
			}
			int num = 0;
			int num2 = 30;
			int num3 = num2;
			string text = killCount.Value.ToString();
			int length = text.Length;
			int num4 = Math.Max(0, -48 + 8 * text.Length);
			num4 = -3;
			float precent = 1f;
			int num5 = 8;
			UIElement uielement2 = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", 1), null, 12, 7)
			{
				Width = new StyleDimension((float)(-8 + num4), precent),
				Height = new StyleDimension((float)num2, 0f),
				BackgroundColor = new Color(43, 56, 101),
				BorderColor = Color.Transparent,
				Top = new StyleDimension((float)num, 0f),
				Left = new StyleDimension((float)(-(float)num5), 0f),
				HAlign = 1f
			};
			uielement2.SetPadding(0f);
			uielement2.PaddingRight = 5f;
			uielement.Append(uielement2);
			uielement2.OnUpdate += this.ShowDescription;
			float textScale = 0.85f;
			UIText element = new UIText(text, textScale, false)
			{
				HAlign = 1f,
				VAlign = 0.5f,
				Left = new StyleDimension(-3f, 0f),
				Top = new StyleDimension(0f, 0f)
			};
			Item item = new Item();
			item.SetDefaults(321);
			item.scale = 0.8f;
			UIItemIcon element2 = new UIItemIcon(item, false)
			{
				IgnoresMouseInteraction = true,
				HAlign = 0f,
				Left = new StyleDimension(-1f, 0f)
			};
			uielement.Height.Pixels = (float)num3;
			uielement2.Append(element2);
			uielement2.Append(element);
			return uielement;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x005596A4 File Offset: 0x005578A4
		private void ShowDescription(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Slain"), 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x005596D5 File Offset: 0x005578D5
		private int? GetKillCount()
		{
			return new int?(Main.BestiaryTracker.Kills.GetKillCount(this._instance));
		}

		// Token: 0x04004878 RID: 18552
		private NPC _instance;
	}
}
