using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000DF RID: 223
	[DataContract]
	public class WeGameFriendListInfo
	{
		// Token: 0x04001313 RID: 4883
		[DataMember]
		public List<RailFriendInfo> _friendList;
	}
}
