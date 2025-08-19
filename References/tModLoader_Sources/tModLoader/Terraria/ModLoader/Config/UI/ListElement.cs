using System;
using System.Collections;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003AB RID: 939
	internal class ListElement : CollectionElement
	{
		// Token: 0x06003260 RID: 12896 RVA: 0x00543601 File Offset: 0x00541801
		protected override void PrepareTypes()
		{
			this.listType = base.MemberInfo.Type.GetGenericArguments()[0];
			base.JsonDefaultListValueAttribute = ConfigManager.GetCustomAttributeFromCollectionMemberThenElementType<JsonDefaultListValueAttribute>(base.MemberInfo.MemberInfo, this.listType);
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x00543637 File Offset: 0x00541837
		protected override void AddItem()
		{
			((IList)base.Data).Add(base.CreateCollectionElementInstance(this.listType));
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x00543656 File Offset: 0x00541856
		protected override void InitializeCollection()
		{
			base.Data = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
			{
				this.listType
			}));
			this.SetObject(base.Data);
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x0054368D File Offset: 0x0054188D
		protected override void ClearCollection()
		{
			((IList)base.Data).Clear();
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x005436A0 File Offset: 0x005418A0
		protected override void SetupList()
		{
			base.DataList.Clear();
			int top = 0;
			if (base.Data != null)
			{
				for (int i = 0; i < ((IList)base.Data).Count; i++)
				{
					int index = i;
					Tuple<UIElement, UIElement> tuple = UIModConfig.WrapIt(base.DataList, ref top, base.MemberInfo, base.Item, 0, base.Data, this.listType, index);
					UIElement item = tuple.Item2;
					item.Left.Pixels = item.Left.Pixels + 24f;
					UIElement item2 = tuple.Item2;
					item2.Width.Pixels = item2.Width.Pixels - 30f;
					UIModConfigHoverImage deleteButton = new UIModConfigHoverImage(base.DeleteTexture, Language.GetTextValue("tModLoader.ModConfigRemove"));
					deleteButton.VAlign = 0.5f;
					deleteButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
					{
						((IList)this.Data).RemoveAt(index);
						this.SetupList();
						Interface.modConfig.SetPendingChanges(true);
					};
					tuple.Item1.Append(deleteButton);
				}
			}
		}

		// Token: 0x04001D9E RID: 7582
		private Type listType;
	}
}
