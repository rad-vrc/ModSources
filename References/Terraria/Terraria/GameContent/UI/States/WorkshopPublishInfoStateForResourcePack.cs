using System;
using System.Collections.Generic;
using Terraria.IO;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.UI;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x02000340 RID: 832
	public class WorkshopPublishInfoStateForResourcePack : AWorkshopPublishInfoState<ResourcePack>
	{
		// Token: 0x0600257B RID: 9595 RVA: 0x0056CB00 File Offset: 0x0056AD00
		public WorkshopPublishInfoStateForResourcePack(UIState stateToGoBackTo, ResourcePack resourcePack) : base(stateToGoBackTo, resourcePack)
		{
			this._instructionsTextKey = "Workshop.ResourcePackPublishDescription";
			this._publishedObjectNameDescriptorTexKey = "Workshop.ResourcePackName";
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x0056CB20 File Offset: 0x0056AD20
		protected override string GetPublishedObjectDisplayName()
		{
			if (this._dataObject == null)
			{
				return "null";
			}
			return this._dataObject.Name;
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x0056CB3C File Offset: 0x0056AD3C
		protected override void GoToPublishConfirmation()
		{
			if (SocialAPI.Workshop != null && this._dataObject != null)
			{
				SocialAPI.Workshop.PublishResourcePack(this._dataObject, base.GetPublishSettings());
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._previousUIState);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x0056CB88 File Offset: 0x0056AD88
		protected override List<WorkshopTagOption> GetTagsToShow()
		{
			return SocialAPI.Workshop.SupportedTags.ResourcePackTags;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x0056CB99 File Offset: 0x0056AD99
		protected override bool TryFindingTags(out FoundWorkshopEntryInfo info)
		{
			return SocialAPI.Workshop.TryGetInfoForResourcePack(this._dataObject, out info);
		}
	}
}
