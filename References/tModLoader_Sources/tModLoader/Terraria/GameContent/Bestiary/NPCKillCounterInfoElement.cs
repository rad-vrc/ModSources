using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200069C RID: 1692
	public class NPCKillCounterInfoElement : IBestiaryInfoElement
	{
		// Token: 0x0600481E RID: 18462 RVA: 0x00647478 File Offset: 0x00645678
		public NPCKillCounterInfoElement(int npcNetId)
		{
			this._instance = new NPC();
			this._instance.SetDefaults(npcNetId, new NPCSpawnParams
			{
				gameModeData = new GameModeData?(GameModeData.NormalMode),
				strengthMultiplierOverride = null
			});
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x006474CC File Offset: 0x006456CC
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
				int value = killCount.Value;
			}
			int num = 0;
			int num2 = 30;
			int num3 = num2;
			string text = killCount.Value.ToString();
			int length = text.Length;
			int num4 = Math.Max(0, -48 + 8 * text.Length);
			num4 = -3;
			float num5 = 1f;
			int num6 = 8;
			UIElement uIElement = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel"), null, 12, 7)
			{
				Width = new StyleDimension((float)(-8 + num4), num5),
				Height = new StyleDimension((float)num2, 0f),
				BackgroundColor = new Color(43, 56, 101),
				BorderColor = Color.Transparent,
				Top = new StyleDimension((float)num, 0f),
				Left = new StyleDimension((float)(-(float)num6), 0f),
				HAlign = 1f
			};
			uIElement.SetPadding(0f);
			uIElement.PaddingRight = 5f;
			uielement.Append(uIElement);
			uIElement.OnUpdate += this.ShowDescription;
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
			uIElement.Append(element2);
			uIElement.Append(element);
			return uielement;
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x006476FC File Offset: 0x006458FC
		private void ShowDescription(UIElement element)
		{
			if (element.IsMouseHovering)
			{
				Main.instance.MouseText(Language.GetTextValue("BestiaryInfo.Slain"), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x0064772C File Offset: 0x0064592C
		private int? GetKillCount()
		{
			return new int?(Main.BestiaryTracker.Kills.GetKillCount(this._instance));
		}

		// Token: 0x04005C16 RID: 23574
		private NPC _instance;
	}
}
