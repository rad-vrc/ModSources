using System;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000300 RID: 768
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Guildpack_Head : PatreonItem
	{
		// Token: 0x06002E6B RID: 11883 RVA: 0x005319C4 File Offset: 0x0052FBC4
		public override bool IsVanitySet(int head, int body, int legs)
		{
			if (Guildpack_Head.equipSlots == null)
			{
				int[] array = new int[3];
				int num = 0;
				Mod mod = base.Mod;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(base.InternalSetName);
				defaultInterpolatedStringHandler.AppendLiteral("_");
				defaultInterpolatedStringHandler.AppendFormatted<EquipType>(EquipType.Head);
				array[num] = EquipLoader.GetEquipSlot(mod, defaultInterpolatedStringHandler.ToStringAndClear(), EquipType.Head);
				int num2 = 1;
				Mod mod2 = base.Mod;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(base.InternalSetName);
				defaultInterpolatedStringHandler.AppendLiteral("_");
				defaultInterpolatedStringHandler.AppendFormatted<EquipType>(EquipType.Body);
				array[num2] = EquipLoader.GetEquipSlot(mod2, defaultInterpolatedStringHandler.ToStringAndClear(), EquipType.Body);
				int num3 = 2;
				Mod mod3 = base.Mod;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(base.InternalSetName);
				defaultInterpolatedStringHandler.AppendLiteral("_");
				defaultInterpolatedStringHandler.AppendFormatted<EquipType>(EquipType.Legs);
				array[num3] = EquipLoader.GetEquipSlot(mod3, defaultInterpolatedStringHandler.ToStringAndClear(), EquipType.Legs);
				Guildpack_Head.equipSlots = array;
			}
			return head == Guildpack_Head.equipSlots[0] && body == Guildpack_Head.equipSlots[1] && legs == Guildpack_Head.equipSlots[2];
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x00531AC8 File Offset: 0x0052FCC8
		[NullableContext(1)]
		public override void UpdateVanitySet(Player player)
		{
			GuildpackSetEffectPlayer modPlayer;
			if (player.TryGetModPlayer<GuildpackSetEffectPlayer>(out modPlayer))
			{
				modPlayer.IsActive = true;
			}
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x00531AE6 File Offset: 0x0052FCE6
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 22;
		}

		// Token: 0x04001C7B RID: 7291
		[Nullable(2)]
		private static int[] equipSlots;
	}
}
