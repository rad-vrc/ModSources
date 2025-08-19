using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003AE RID: 942
	internal class NPCDefinitionOptionElement : DefinitionOptionElement<NPCDefinition>
	{
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600326C RID: 12908 RVA: 0x005439BC File Offset: 0x00541BBC
		// (set) Token: 0x0600326D RID: 12909 RVA: 0x005439C4 File Offset: 0x00541BC4
		public int Order { get; set; }

		// Token: 0x0600326E RID: 12910 RVA: 0x005439CD File Offset: 0x00541BCD
		public NPCDefinitionOptionElement(int order, NPCDefinition definition, float scale = 0.75f) : base(definition, scale)
		{
			this.Order = order;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x005439E0 File Offset: 0x00541BE0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetInnerDimensions();
			spriteBatch.Draw(base.BackgroundTexture.Value, dimensions.Position(), null, Color.White, 0f, Vector2.Zero, base.Scale, 0, 0f);
			if (base.Definition != null)
			{
				int type = base.Unloaded ? 0 : NPCID.FromNetId(base.Type);
				if (TextureAssets.Npc[type].State == null)
				{
					Main.Assets.Request<Texture2D>(TextureAssets.Npc[type].Name, 2);
				}
				Texture2D npcTexture = TextureAssets.Npc[type].Value;
				int num = Interface.modConfig.UpdateCount / 8;
				int frames = Main.npcFrameCount[type];
				if (base.Unloaded)
				{
					npcTexture = TextureAssets.Item[ModContent.ItemType<UnloadedItem>()].Value;
					frames = 1;
				}
				int height = npcTexture.Height / frames;
				int width = npcTexture.Width;
				int frame = num % frames;
				int y = height * frame;
				Rectangle rectangle2;
				rectangle2..ctor(0, y, width, height);
				float drawScale = 1f;
				float availableWidth = (float)DefinitionOptionElement<NPCDefinition>.DefaultBackgroundTexture.Width() * base.Scale;
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
				NPC npc = ContentSamples.NpcsByNetId[base.Unloaded ? 0 : base.Type];
				spriteBatch.Draw(npcTexture, position2, new Rectangle?(rectangle2), Color.White, 0f, origin, drawScale, 0, 0f);
				if (npc.color != default(Color))
				{
					spriteBatch.Draw(npcTexture, position2, new Rectangle?(rectangle2), npc.GetColor(Color.White), 0f, origin, drawScale, 0, 0f);
				}
			}
			if (base.IsMouseHovering)
			{
				UIModConfig.Tooltip = base.Tooltip;
			}
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x00543C28 File Offset: 0x00541E28
		public override int CompareTo(object obj)
		{
			NPCDefinitionOptionElement other = obj as NPCDefinitionOptionElement;
			if (other == null)
			{
				return base.CompareTo(obj);
			}
			if (this.Order == 0)
			{
				return -1;
			}
			if (other.Order == 0)
			{
				return 1;
			}
			int sortValue;
			bool hasSort = ContentSamples.NpcBestiarySortingId.TryGetValue(base.Type, out sortValue);
			int otherSortValue;
			bool otherHasSort = ContentSamples.NpcBestiarySortingId.TryGetValue(other.Type, out otherSortValue);
			if (hasSort && otherHasSort)
			{
				return sortValue.CompareTo(otherSortValue);
			}
			if (hasSort)
			{
				return -1;
			}
			if (otherHasSort)
			{
				return 1;
			}
			return this.Order.CompareTo(other.Order);
		}
	}
}
