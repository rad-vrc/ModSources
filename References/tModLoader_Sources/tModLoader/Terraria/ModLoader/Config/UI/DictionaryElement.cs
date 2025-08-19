using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003A0 RID: 928
	internal class DictionaryElement : CollectionElement
	{
		// Token: 0x06003209 RID: 12809 RVA: 0x00541D94 File Offset: 0x0053FF94
		protected override void PrepareTypes()
		{
			this.keyType = base.MemberInfo.Type.GetGenericArguments()[0];
			this.valueType = base.MemberInfo.Type.GetGenericArguments()[1];
			base.JsonDefaultListValueAttribute = ConfigManager.GetCustomAttributeFromCollectionMemberThenElementType<JsonDefaultListValueAttribute>(base.MemberInfo.MemberInfo, this.valueType);
			this.defaultDictionaryKeyValueAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<DefaultDictionaryKeyValueAttribute>(base.MemberInfo, null, null);
			this.jsonDefaultDictionaryKeyValueAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<JsonDefaultDictionaryKeyValueAttribute>(base.MemberInfo, null, null);
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00541E14 File Offset: 0x00540014
		protected override void AddItem()
		{
			try
			{
				object keyValue;
				if (this.defaultDictionaryKeyValueAttribute != null)
				{
					keyValue = this.defaultDictionaryKeyValueAttribute.Value;
				}
				else
				{
					keyValue = ConfigManager.AlternateCreateInstance(this.keyType);
					if (!this.keyType.IsValueType && this.keyType != typeof(string))
					{
						JsonDefaultDictionaryKeyValueAttribute jsonDefaultDictionaryKeyValueAttribute = this.jsonDefaultDictionaryKeyValueAttribute;
						JsonConvert.PopulateObject(((jsonDefaultDictionaryKeyValueAttribute != null) ? jsonDefaultDictionaryKeyValueAttribute.Json : null) ?? "{}", keyValue, ConfigManager.serializerSettings);
					}
				}
				((IDictionary)base.Data).Add(keyValue, base.CreateCollectionElementInstance(this.valueType));
			}
			catch (Exception e)
			{
				Interface.modConfig.SetMessage("Error: " + e.Message, Color.Red);
			}
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x00541EE0 File Offset: 0x005400E0
		protected override void InitializeCollection()
		{
			base.Data = Activator.CreateInstance(typeof(Dictionary<, >).MakeGenericType(new Type[]
			{
				this.keyType,
				this.valueType
			}));
			this.SetObject(base.Data);
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x00541F20 File Offset: 0x00540120
		protected override void ClearCollection()
		{
			((IDictionary)base.Data).Clear();
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x00541F34 File Offset: 0x00540134
		protected override void SetupList()
		{
			base.DataList.Clear();
			int top = 0;
			this.dataWrapperList = new List<IDictionaryElementWrapper>();
			Type genericType = typeof(DictionaryElementWrapper<, >).MakeGenericType(new Type[]
			{
				this.keyType,
				this.valueType
			});
			if (base.Data != null)
			{
				ICollection keys = ((IDictionary)base.Data).Keys;
				IEnumerable values = ((IDictionary)base.Data).Values;
				IEnumerator keysEnumerator = keys.GetEnumerator();
				IEnumerator valuesEnumerator = values.GetEnumerator();
				int i = 0;
				while (keysEnumerator.MoveNext())
				{
					valuesEnumerator.MoveNext();
					IDictionaryElementWrapper proxy = (IDictionaryElementWrapper)Activator.CreateInstance(genericType, new object[]
					{
						keysEnumerator.Current,
						valuesEnumerator.Current,
						(IDictionary)base.Data
					});
					this.dataWrapperList.Add(proxy);
					Type type = base.MemberInfo.Type.GetGenericArguments()[0];
					PropertyFieldWrapper wrappermemberInfo = ConfigManager.GetFieldsAndProperties(this).ToList<PropertyFieldWrapper>()[0];
					Tuple<UIElement, UIElement> tuple = UIModConfig.WrapIt(base.DataList, ref top, wrappermemberInfo, this, 0, this.dataWrapperList, genericType, i);
					UIElement item = tuple.Item2;
					item.Left.Pixels = item.Left.Pixels + 24f;
					UIElement item2 = tuple.Item2;
					item2.Width.Pixels = item2.Width.Pixels - 24f;
					UIModConfigHoverImage deleteButton = new UIModConfigHoverImage(base.DeleteTexture, Language.GetTextValue("tModLoader.ModConfigRemove"));
					deleteButton.VAlign = 0.5f;
					object o = keysEnumerator.Current;
					deleteButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
					{
						((IDictionary)this.Data).Remove(o);
						this.SetupList();
						Interface.modConfig.SetPendingChanges(true);
					};
					tuple.Item1.Append(deleteButton);
					i++;
				}
			}
		}

		// Token: 0x04001D85 RID: 7557
		internal Type keyType;

		// Token: 0x04001D86 RID: 7558
		internal Type valueType;

		// Token: 0x04001D87 RID: 7559
		internal UIText save;

		// Token: 0x04001D88 RID: 7560
		public List<IDictionaryElementWrapper> dataWrapperList;

		// Token: 0x04001D89 RID: 7561
		protected DefaultDictionaryKeyValueAttribute defaultDictionaryKeyValueAttribute;

		// Token: 0x04001D8A RID: 7562
		protected JsonDefaultDictionaryKeyValueAttribute jsonDefaultDictionaryKeyValueAttribute;
	}
}
