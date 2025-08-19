using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003B6 RID: 950
	internal class ProjectileDefinitionOptionElement : DefinitionOptionElement<ProjectileDefinition>
	{
		// Token: 0x06003295 RID: 12949 RVA: 0x00544AF0 File Offset: 0x00542CF0
		public ProjectileDefinitionOptionElement(ProjectileDefinition definition, float scale = 0.75f) : base(definition, scale)
		{
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x00544AFC File Offset: 0x00542CFC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetInnerDimensions();
			spriteBatch.Draw(base.BackgroundTexture.Value, dimensions.Position(), null, Color.White, 0f, Vector2.Zero, base.Scale, 0, 0f);
			if (base.Definition != null)
			{
				int type = base.Unloaded ? 0 : base.Type;
				if (TextureAssets.Projectile[type].State == null)
				{
					Main.Assets.Request<Texture2D>(TextureAssets.Projectile[type].Name, 2);
				}
				Texture2D projectileTexture = TextureAssets.Projectile[type].Value;
				int num = Interface.modConfig.UpdateCount / 4;
				int frames = Main.projFrames[type];
				if (base.Unloaded)
				{
					projectileTexture = TextureAssets.Item[ModContent.ItemType<UnloadedItem>()].Value;
					frames = 1;
				}
				int height = projectileTexture.Height / frames;
				int width = projectileTexture.Width;
				int frame = num % frames;
				int y = height * frame;
				Rectangle rectangle2;
				rectangle2..ctor(0, y, width, height);
				float drawScale = 1f;
				float availableWidth = (float)DefinitionOptionElement<ProjectileDefinition>.DefaultBackgroundTexture.Width() * base.Scale;
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
				Vector2 position2 = dimensions.Position() + vector / 2f - rectangle2.Size() * drawScale / 2f;
				Vector2 origin = rectangle2.Size() * 0f;
				spriteBatch.Draw(projectileTexture, position2, new Rectangle?(rectangle2), Color.White, 0f, origin, drawScale, 0, 0f);
			}
			if (base.IsMouseHovering)
			{
				UIModConfig.Tooltip = base.Tooltip;
			}
		}
	}
}
