using System;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x02000393 RID: 915
	internal class ArrayElement : CollectionElement
	{
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x0053F874 File Offset: 0x0053DA74
		protected override bool CanAdd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x0053F877 File Offset: 0x0053DA77
		protected override void AddItem()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x0053F87E File Offset: 0x0053DA7E
		protected override void ClearCollection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x0053F885 File Offset: 0x0053DA85
		protected override void InitializeCollection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x0053F88C File Offset: 0x0053DA8C
		protected override void NullCollection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x0053F893 File Offset: 0x0053DA93
		protected override void PrepareTypes()
		{
			this.itemType = base.MemberInfo.Type.GetElementType();
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x0053F8AC File Offset: 0x0053DAAC
		protected override void SetupList()
		{
			base.DataList.Clear();
			int count = (base.MemberInfo.GetValue(base.Item) as Array).Length;
			int top = 0;
			for (int i = 0; i < count; i++)
			{
				int index = i;
				UIModConfig.WrapIt(base.DataList, ref top, base.MemberInfo, base.Item, 0, base.Data, this.itemType, index);
			}
		}

		// Token: 0x04001D43 RID: 7491
		private Type itemType;
	}
}
