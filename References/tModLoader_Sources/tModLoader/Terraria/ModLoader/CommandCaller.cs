using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x020001A7 RID: 423
	public interface CommandCaller
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06002053 RID: 8275
		CommandType CommandType { get; }

		/// <summary>
		/// The Player object corresponding to the Player that invoked this command. Use this when the Player is needed. Don't use Main.LocalPlayer because that would be incorrect for various CommandTypes.
		/// </summary>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06002054 RID: 8276
		Player Player { get; }

		/// <summary>
		/// Use this to respond to the Player that invoked this command. This method handles writing to the console, writing to chat, or sending messages over the network for you depending on the CommandType used. Avoid using Main.NewText, Console.WriteLine, or NetMessage.SendChatMessageToClient directly because the logic would change depending on CommandType.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="color"></param>
		// Token: 0x06002055 RID: 8277
		void Reply(string text, Color color = default(Color));
	}
}
