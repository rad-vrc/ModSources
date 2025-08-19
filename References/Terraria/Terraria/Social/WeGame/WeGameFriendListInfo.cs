using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200016C RID: 364
	[DataContract]
	public class WeGameFriendListInfo
	{
		// Token: 0x0400158F RID: 5519
		[DataMember]
		public List<RailFriendInfo> _friendList;
	}
}
