using System;
using System.Collections.Generic;
using Terraria.IO;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.UI;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004EB RID: 1259
	public class WorkshopPublishInfoStateForWorld : AWorkshopPublishInfoState<WorldFileData>
	{
		// Token: 0x06003D1C RID: 15644 RVA: 0x005C8220 File Offset: 0x005C6420
		public WorkshopPublishInfoStateForWorld(UIState stateToGoBackTo, WorldFileData world) : base(stateToGoBackTo, world)
		{
			this._instructionsTextKey = "Workshop.WorldPublishDescription";
			this._publishedObjectNameDescriptorTexKey = "Workshop.WorldName";
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x005C8240 File Offset: 0x005C6440
		protected override string GetPublishedObjectDisplayName()
		{
			if (this._dataObject == null)
			{
				return "null";
			}
			return this._dataObject.Name;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x005C825C File Offset: 0x005C645C
		protected override void GoToPublishConfirmation()
		{
			if (SocialAPI.Workshop != null && this._dataObject != null)
			{
				SocialAPI.Workshop.PublishWorld(this._dataObject, base.GetPublishSettings());
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._previousUIState);
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x005C82A8 File Offset: 0x005C64A8
		protected override List<WorkshopTagOption> GetTagsToShow()
		{
			return SocialAPI.Workshop.SupportedTags.WorldTags;
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x005C82B9 File Offset: 0x005C64B9
		protected override bool TryFindingTags(out FoundWorkshopEntryInfo info)
		{
			return SocialAPI.Workshop.TryGetInfoForWorld(this._dataObject, out info);
		}
	}
}
