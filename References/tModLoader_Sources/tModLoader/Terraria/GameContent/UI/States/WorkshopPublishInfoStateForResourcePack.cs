using System;
using System.Collections.Generic;
using Terraria.IO;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.UI;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004EA RID: 1258
	public class WorkshopPublishInfoStateForResourcePack : AWorkshopPublishInfoState<ResourcePack>
	{
		// Token: 0x06003D17 RID: 15639 RVA: 0x005C8174 File Offset: 0x005C6374
		public WorkshopPublishInfoStateForResourcePack(UIState stateToGoBackTo, ResourcePack resourcePack) : base(stateToGoBackTo, resourcePack)
		{
			this._instructionsTextKey = "Workshop.ResourcePackPublishDescription";
			this._publishedObjectNameDescriptorTexKey = "Workshop.ResourcePackName";
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x005C8194 File Offset: 0x005C6394
		protected override string GetPublishedObjectDisplayName()
		{
			if (this._dataObject == null)
			{
				return "null";
			}
			return this._dataObject.Name;
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x005C81B0 File Offset: 0x005C63B0
		protected override void GoToPublishConfirmation()
		{
			if (SocialAPI.Workshop != null && this._dataObject != null)
			{
				SocialAPI.Workshop.PublishResourcePack(this._dataObject, base.GetPublishSettings());
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._previousUIState);
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x005C81FC File Offset: 0x005C63FC
		protected override List<WorkshopTagOption> GetTagsToShow()
		{
			return SocialAPI.Workshop.SupportedTags.ResourcePackTags;
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x005C820D File Offset: 0x005C640D
		protected override bool TryFindingTags(out FoundWorkshopEntryInfo info)
		{
			return SocialAPI.Workshop.TryGetInfoForResourcePack(this._dataObject, out info);
		}
	}
}
