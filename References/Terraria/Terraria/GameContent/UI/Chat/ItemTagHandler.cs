using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.ID;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x0200039F RID: 927
	public class ItemTagHandler : ITagHandler
	{
		// Token: 0x060029B6 RID: 10678 RVA: 0x00594AB4 File Offset: 0x00592CB4
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			Item item = new Item();
			int type;
			if (int.TryParse(text, out type))
			{
				item.netDefaults(type);
			}
			if (item.type <= 0)
			{
				return new TextSnippet(text);
			}
			item.stack = 1;
			if (options != null)
			{
				string[] array = options.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Length != 0)
					{
						char c = array[i][0];
						int value2;
						if (c != 'p')
						{
							int value;
							if ((c == 's' || c == 'x') && int.TryParse(array[i].Substring(1), out value))
							{
								item.stack = Utils.Clamp<int>(value, 1, item.maxStack);
							}
						}
						else if (int.TryParse(array[i].Substring(1), out value2))
						{
							item.Prefix((int)((byte)Utils.Clamp<int>(value2, 0, PrefixID.Count)));
						}
					}
				}
			}
			string str = "";
			if (item.stack > 1)
			{
				str = " (" + item.stack + ")";
			}
			return new ItemTagHandler.ItemSnippet(item)
			{
				Text = "[" + item.AffixName() + str + "]",
				CheckForHover = true,
				DeleteWhole = true
			};
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x00594BF4 File Offset: 0x00592DF4
		public static string GenerateTag(Item I)
		{
			string text = "[i";
			if (I.prefix != 0)
			{
				text = text + "/p" + I.prefix;
			}
			if (I.stack != 1)
			{
				text = text + "/s" + I.stack;
			}
			return string.Concat(new object[]
			{
				text,
				":",
				I.netID,
				"]"
			});
		}

		// Token: 0x02000759 RID: 1881
		private class ItemSnippet : TextSnippet
		{
			// Token: 0x060038BE RID: 14526 RVA: 0x0061427D File Offset: 0x0061247D
			public ItemSnippet(Item item) : base("")
			{
				this._item = item;
				this.Color = ItemRarity.GetColor(item.rare);
			}

			// Token: 0x060038BF RID: 14527 RVA: 0x006142A4 File Offset: 0x006124A4
			public override void OnHover()
			{
				Main.HoverItem = this._item.Clone();
				Main.instance.MouseText(this._item.Name, this._item.rare, 0, -1, -1, -1, -1, 0);
			}

			// Token: 0x060038C0 RID: 14528 RVA: 0x006142E8 File Offset: 0x006124E8
			public override bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default(Vector2), Color color = default(Color), float scale = 1f)
			{
				float num = 1f;
				float num2 = 1f;
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
				num2 *= scale;
				num *= num2;
				if (num > 0.75f)
				{
					num = 0.75f;
				}
				if (!justCheckingString && color != Color.Black)
				{
					float inventoryScale = Main.inventoryScale;
					Main.inventoryScale = scale * num;
					ItemSlot.Draw(spriteBatch, ref this._item, 14, position - new Vector2(10f) * scale * num, Color.White);
					Main.inventoryScale = inventoryScale;
				}
				size = new Vector2(32f) * scale * num;
				return true;
			}

			// Token: 0x060038C1 RID: 14529 RVA: 0x00614401 File Offset: 0x00612601
			public override float GetStringLength(DynamicSpriteFont font)
			{
				return 32f * this.Scale * 0.65f;
			}

			// Token: 0x04006440 RID: 25664
			private Item _item;
		}
	}
}
