using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000541 RID: 1345
	public class ItemTagHandler : ITagHandler
	{
		// Token: 0x06003FF7 RID: 16375 RVA: 0x005DD4D8 File Offset: 0x005DB6D8
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			Item item = new Item();
			int result;
			if (int.TryParse(text, out result) && result < ItemLoader.ItemCount)
			{
				item.netDefaults(result);
			}
			if (ItemID.Search.TryGetId(text, ref result))
			{
				item.netDefaults(result);
			}
			if (item.type <= 0)
			{
				return new TextSnippet(text);
			}
			item.stack = 1;
			if (options != null)
			{
				string[] array = options.Split(',', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Length != 0)
					{
						char c = array[i][0];
						if (c <= 'p')
						{
							if (c != 'd')
							{
								if (c == 'p')
								{
									int result2;
									if (int.TryParse(array[i].Substring(1), out result2))
									{
										item.Prefix((int)((byte)Utils.Clamp<int>(result2, 0, PrefixLoader.PrefixCount)));
									}
								}
							}
							else
							{
								item = ItemIO.FromBase64(array[i].Substring(1));
							}
						}
						else if (c == 's' || c == 'x')
						{
							int result3;
							if (int.TryParse(array[i].Substring(1), out result3))
							{
								item.stack = Utils.Clamp<int>(result3, 1, item.maxStack);
							}
						}
					}
				}
			}
			string text2 = "";
			if (item.stack > 1)
			{
				text2 = " (" + item.stack.ToString() + ")";
			}
			return new ItemTagHandler.ItemSnippet(item)
			{
				Text = "[" + item.AffixName() + text2 + "]",
				CheckForHover = true,
				DeleteWhole = true
			};
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x005DD654 File Offset: 0x005DB854
		public static string GenerateTag(Item I)
		{
			string text = "[i";
			if (ItemLoader.NeedsModSaving(I) || ItemIO.SaveGlobals(I) != null)
			{
				text = text + "/d" + ItemIO.ToBase64(I);
			}
			else
			{
				if (I.prefix != 0)
				{
					text = text + "/p" + I.prefix.ToString();
				}
				if (I.stack != 1)
				{
					text = text + "/s" + I.stack.ToString();
				}
			}
			return text + ":" + I.netID.ToString() + "]";
		}

		// Token: 0x02000C28 RID: 3112
		private class ItemSnippet : TextSnippet
		{
			// Token: 0x06005F2F RID: 24367 RVA: 0x006CD4F2 File Offset: 0x006CB6F2
			public ItemSnippet(Item item) : base("")
			{
				this._item = item;
				this.Color = ItemRarity.GetColor(item.rare);
			}

			// Token: 0x06005F30 RID: 24368 RVA: 0x006CD518 File Offset: 0x006CB718
			public override void OnHover()
			{
				Main.HoverItem = this._item.Clone();
				Main.instance.MouseText(this._item.Name, this._item.rare, 0, -1, -1, -1, -1, 0);
			}

			// Token: 0x06005F31 RID: 24369 RVA: 0x006CD55C File Offset: 0x006CB75C
			public override bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default(Vector2), Color color = default(Color), float scale = 1f)
			{
				if (Main.netMode != 2 && !Main.dedServ)
				{
					Main.instance.LoadItem(this._item.type);
					Texture2D value = TextureAssets.Item[this._item.type].Value;
					if (Main.itemAnimations[this._item.type] != null)
					{
						Main.itemAnimations[this._item.type].GetFrame(value, -1);
					}
					else
					{
						value.Frame(1, 1, 0, 0, 0, 0);
					}
				}
				float num = scale * 0.75f;
				if (!justCheckingString && (color.R != 0 || color.G != 0 || color.B != 0))
				{
					float inventoryScale = Main.inventoryScale;
					Main.inventoryScale = num;
					ItemSlot.Draw(spriteBatch, ref this._item, 14, position - new Vector2(10f) * num, Color.White);
					Main.inventoryScale = inventoryScale;
				}
				size = new Vector2(32f) * num;
				return true;
			}

			// Token: 0x06005F32 RID: 24370 RVA: 0x006CD65D File Offset: 0x006CB85D
			public override float GetStringLength(DynamicSpriteFont font)
			{
				return 32f * this.Scale * 0.65f;
			}

			// Token: 0x04007892 RID: 30866
			private Item _item;
		}
	}
}
