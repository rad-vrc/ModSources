using System;
using System.Collections.Generic;
using Terraria.IO;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.UI;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x0200033F RID: 831
	public class WorkshopPublishInfoStateForWorld : AWorkshopPublishInfoState<WorldFileData>
	{
		// Token: 0x06002576 RID: 9590 RVA: 0x0056CA54 File Offset: 0x0056AC54
		public WorkshopPublishInfoStateForWorld(UIState stateToGoBackTo, WorldFileData world) : base(stateToGoBackTo, world)
		{
			this._instructionsTextKey = "Workshop.WorldPublishDescription";
			this._publishedObjectNameDescriptorTexKey = "Workshop.WorldName";
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0056CA74 File Offset: 0x0056AC74
		protected override string GetPublishedObjectDisplayName()
		{
			if (this._dataObject == null)
			{
				return "null";
			}
			return this._dataObject.Name;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x0056CA90 File Offset: 0x0056AC90
		protected override void GoToPublishConfirmation()
		{
			if (SocialAPI.Workshop != null && this._dataObject != null)
			{
				SocialAPI.Workshop.PublishWorld(this._dataObject, base.GetPublishSettings());
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._previousUIState);
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x0056CADC File Offset: 0x0056ACDC
		protected override List<WorkshopTagOption> GetTagsToShow()
		{
			return SocialAPI.Workshop.SupportedTags.WorldTags;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x0056CAED File Offset: 0x0056ACED
		protected override bool TryFindingTags(out FoundWorkshopEntryInfo info)
		{
			return SocialAPI.Workshop.TryGetInfoForWorld(this._dataObject, out info);
		}
	}
}
