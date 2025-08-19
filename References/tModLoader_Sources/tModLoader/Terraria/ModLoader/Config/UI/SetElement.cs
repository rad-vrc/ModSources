using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003BC RID: 956
	internal class SetElement : CollectionElement
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x0054565C File Offset: 0x0054385C
		// (set) Token: 0x060032C5 RID: 12997 RVA: 0x00545664 File Offset: 0x00543864
		public List<ISetElementWrapper> DataWrapperList { get; set; }

		// Token: 0x060032C6 RID: 12998 RVA: 0x0054566D File Offset: 0x0054386D
		protected override void PrepareTypes()
		{
			this.setType = base.MemberInfo.Type.GetGenericArguments()[0];
			base.JsonDefaultListValueAttribute = ConfigManager.GetCustomAttributeFromCollectionMemberThenElementType<JsonDefaultListValueAttribute>(base.MemberInfo.MemberInfo, this.setType);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x005456A4 File Offset: 0x005438A4
		protected override void AddItem()
		{
			base.Data.GetType().GetMethods().FirstOrDefault((MethodInfo m) => m.Name == "Add").Invoke(base.Data, new object[]
			{
				base.CreateCollectionElementInstance(this.setType)
			});
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x00545706 File Offset: 0x00543906
		protected override void InitializeCollection()
		{
			base.Data = Activator.CreateInstance(typeof(HashSet<>).MakeGenericType(new Type[]
			{
				this.setType
			}));
			this.SetObject(base.Data);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x00545740 File Offset: 0x00543940
		protected override void ClearCollection()
		{
			base.Data.GetType().GetMethods().FirstOrDefault((MethodInfo m) => m.Name == "Clear").Invoke(base.Data, new object[0]);
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x00545794 File Offset: 0x00543994
		protected override void SetupList()
		{
			base.DataList.Clear();
			int top = 0;
			Type genericType = typeof(SetElementWrapper<>).MakeGenericType(new Type[]
			{
				this.setType
			});
			this.DataWrapperList = new List<ISetElementWrapper>();
			if (base.Data != null)
			{
				IEnumerator valuesEnumerator = ((IEnumerable)base.Data).GetEnumerator();
				int i = 0;
				while (valuesEnumerator.MoveNext())
				{
					ISetElementWrapper proxy = (ISetElementWrapper)Activator.CreateInstance(genericType, new object[]
					{
						valuesEnumerator.Current,
						base.Data
					});
					this.DataWrapperList.Add(proxy);
					PropertyFieldWrapper wrappermemberInfo = ConfigManager.GetFieldsAndProperties(this).ToList<PropertyFieldWrapper>().First((PropertyFieldWrapper x) => x.Name == "DataWrapperList");
					Tuple<UIElement, UIElement> tuple = UIModConfig.WrapIt(base.DataList, ref top, wrappermemberInfo, this, 0, this.DataWrapperList, genericType, i);
					UIElement item = tuple.Item2;
					item.Left.Pixels = item.Left.Pixels + 24f;
					UIElement item2 = tuple.Item2;
					item2.Width.Pixels = item2.Width.Pixels - 24f;
					UIModConfigHoverImage deleteButton = new UIModConfigHoverImage(base.DeleteTexture, Language.GetTextValue("tModLoader.ModConfigRemove"));
					deleteButton.VAlign = 0.5f;
					object o = valuesEnumerator.Current;
					deleteButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
					{
						this.Data.GetType().GetMethods().FirstOrDefault((MethodInfo m) => m.Name == "Remove").Invoke(this.Data, new object[]
						{
							o
						});
						this.SetupList();
						Interface.modConfig.SetPendingChanges(true);
					};
					tuple.Item1.Append(deleteButton);
					i++;
				}
			}
		}

		// Token: 0x04001DBD RID: 7613
		private Type setType;
	}
}
