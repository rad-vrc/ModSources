using System;
using QoLCompendium.Core;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Magnets
{
	// Token: 0x020001AC RID: 428
	public class MagnetGlobals : GlobalItem
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
		{
			MagnetPlayer mPlayer = Main.LocalPlayer.GetModPlayer<MagnetPlayer>();
			if (item.active && Main.LocalPlayer.whoAmI == Main.myPlayer)
			{
				if (Common.PowerUpItems.Contains(item.type))
				{
					return;
				}
				if (item.noGrabDelay != 0 || item.playerIndexTheItemIsReservedFor != Main.LocalPlayer.whoAmI)
				{
					return;
				}
				if (Main.LocalPlayer.Distance(item.Center) <= 8400f && mPlayer.BaseMagnet)
				{
					item.beingGrabbed = true;
					item.Center = Main.LocalPlayer.Center;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(21, -1, -1, null, item.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (Main.LocalPlayer.Distance(item.Center) <= 16800f && mPlayer.HellstoneMagnet)
				{
					item.beingGrabbed = true;
					item.Center = Main.LocalPlayer.Center;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(21, -1, -1, null, item.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (Main.LocalPlayer.Distance(item.Center) <= 33600f && mPlayer.SoulMagnet)
				{
					item.beingGrabbed = true;
					item.Center = Main.LocalPlayer.Center;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(21, -1, -1, null, item.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (Main.LocalPlayer.Distance(item.Center) <= 67200f && mPlayer.SpectreMagnet)
				{
					item.beingGrabbed = true;
					item.Center = Main.LocalPlayer.Center;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(21, -1, -1, null, item.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (Main.LocalPlayer.Distance(item.Center) <= 268800f && mPlayer.LunarMagnet)
				{
					item.beingGrabbed = true;
					item.Center = Main.LocalPlayer.Center;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(21, -1, -1, null, item.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x04000041 RID: 65
		public const int BaseMagnetRange = 8400;

		// Token: 0x04000042 RID: 66
		public const int HellstoneMagnetRange = 16800;

		// Token: 0x04000043 RID: 67
		public const int SoulMagnetMagnetRange = 33600;

		// Token: 0x04000044 RID: 68
		public const int SpectreMagnetRange = 67200;

		// Token: 0x04000045 RID: 69
		public const int LunarMagnetRange = 268800;
	}
}
