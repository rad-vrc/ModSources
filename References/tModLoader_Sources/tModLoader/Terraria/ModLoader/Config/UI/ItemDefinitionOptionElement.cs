using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader.Default;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003AA RID: 938
	internal class ItemDefinitionOptionElement : DefinitionOptionElement<ItemDefinition>
	{
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x005432B4 File Offset: 0x005414B4
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x005432BC File Offset: 0x005414BC
		public Item Item { get; set; }

		// Token: 0x0600325D RID: 12893 RVA: 0x005432C5 File Offset: 0x005414C5
		public ItemDefinitionOptionElement(ItemDefinition definition, float scale = 0.75f) : base(definition, scale)
		{
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x005432CF File Offset: 0x005414CF
		public override void SetItem(ItemDefinition definition)
		{
			base.SetItem(definition);
			this.Item = new Item();
			this.Item.SetDefaults(base.Type);
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x005432F4 File Offset: 0x005414F4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this.Item != null)
			{
				CalculatedStyle dimensions = base.GetInnerDimensions();
				spriteBatch.Draw(base.BackgroundTexture.Value, dimensions.Position(), null, Color.White, 0f, Vector2.Zero, base.Scale, 0, 0f);
				if (!this.Item.IsAir || base.Unloaded)
				{
					int type = base.Unloaded ? ModContent.ItemType<UnloadedItem>() : this.Item.type;
					if (TextureAssets.Item[type].State == null)
					{
						Main.Assets.Request<Texture2D>(TextureAssets.Item[type].Name, 2);
					}
					Texture2D itemTexture = TextureAssets.Item[type].Value;
					Rectangle rectangle2;
					if (Main.itemAnimations[type] != null)
					{
						rectangle2 = Main.itemAnimations[type].GetFrame(itemTexture, -1);
					}
					else
					{
						rectangle2 = itemTexture.Frame(1, 1, 0, 0, 0, 0);
					}
					Color newColor = Color.White;
					float pulseScale = 1f;
					ItemSlot.GetItemLight(ref newColor, ref pulseScale, this.Item, false);
					int height = rectangle2.Height;
					int width = rectangle2.Width;
					float drawScale = 1f;
					float availableWidth = (float)DefinitionOptionElement<ItemDefinition>.DefaultBackgroundTexture.Width() * base.Scale;
					if ((float)width > availableWidth || (float)height > availableWidth)
					{
						if (width > height)
						{
							drawScale = availableWidth / (float)width;
						}
						else
						{
							drawScale = availableWidth / (float)height;
						}
					}
					drawScale *= base.Scale;
					Vector2 vector = base.BackgroundTexture.Size() * base.Scale;
					Vector2 position2 = dimensions.Position() + vector / 2f;
					Vector2 origin = rectangle2.Size() / 2f;
					if (ItemLoader.PreDrawInInventory(this.Item, spriteBatch, position2, rectangle2, this.Item.GetAlpha(newColor), this.Item.GetColor(Color.White), origin, drawScale * pulseScale))
					{
						spriteBatch.Draw(itemTexture, position2, new Rectangle?(rectangle2), this.Item.GetAlpha(newColor), 0f, origin, drawScale * pulseScale, 0, 0f);
						if (this.Item.color != Color.Transparent)
						{
							spriteBatch.Draw(itemTexture, position2, new Rectangle?(rectangle2), this.Item.GetColor(Color.White), 0f, origin, drawScale * pulseScale, 0, 0f);
						}
					}
					ItemLoader.PostDrawInInventory(this.Item, spriteBatch, position2, rectangle2, this.Item.GetAlpha(newColor), this.Item.GetColor(Color.White), origin, drawScale * pulseScale);
					if (ItemID.Sets.TrapSigned[type])
					{
						spriteBatch.Draw(TextureAssets.Wire.Value, dimensions.Position() + new Vector2(40f, 40f) * base.Scale, new Rectangle?(new Rectangle(4, 58, 8, 8)), Color.White, 0f, new Vector2(4f), 1f, 0, 0f);
					}
				}
			}
			if (base.IsMouseHovering)
			{
				UIModConfig.Tooltip = base.Tooltip;
			}
		}
	}
}
