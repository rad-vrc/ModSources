using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x02000396 RID: 918
	internal class BuffDefinitionOptionElement : DefinitionOptionElement<BuffDefinition>
	{
		// Token: 0x06003172 RID: 12658 RVA: 0x0053FBE8 File Offset: 0x0053DDE8
		public BuffDefinitionOptionElement(BuffDefinition definition, float scale = 0.5f) : base(definition, scale)
		{
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x0053FBF2 File Offset: 0x0053DDF2
		public override void SetItem(BuffDefinition definition)
		{
			base.SetItem(definition);
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x0053FBFC File Offset: 0x0053DDFC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetInnerDimensions();
			spriteBatch.Draw(base.BackgroundTexture.Value, dimensions.Position(), null, Color.White, 0f, Vector2.Zero, base.Scale, 0, 0f);
			if (base.Definition != null)
			{
				int type = base.Unloaded ? 0 : base.Type;
				Texture2D buffTexture;
				if (type == 0)
				{
					buffTexture = TextureAssets.Item[0].Value;
				}
				else
				{
					buffTexture = TextureAssets.Buff[type].Value;
				}
				int num = Interface.modConfig.UpdateCount / 4;
				int frames = 1;
				if (base.Unloaded)
				{
					buffTexture = TextureAssets.Item[ModContent.ItemType<UnloadedItem>()].Value;
					frames = 1;
				}
				int height = buffTexture.Height / frames;
				int width = buffTexture.Width;
				int frame = num % frames;
				int y = height * frame;
				Rectangle rectangle2;
				rectangle2..ctor(0, y, width, height);
				float drawScale = 1f;
				float availableWidth = (float)DefinitionOptionElement<BuffDefinition>.DefaultBackgroundTexture.Width() * base.Scale;
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
				Vector2 vector = base.BackgroundTexture.Size() * base.Scale;
				Vector2 position2 = dimensions.Position() + vector / 2f - rectangle2.Size() * drawScale / 2f;
				Vector2 origin = rectangle2.Size() * 0f;
				spriteBatch.Draw(buffTexture, position2, new Rectangle?(rectangle2), Color.White, 0f, origin, drawScale, 0, 0f);
			}
			if (base.IsMouseHovering)
			{
				UIModConfig.Tooltip = base.Tooltip;
			}
		}
	}
}
