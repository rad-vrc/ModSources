using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000322 RID: 802
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Saethar_Head : PatreonItem
	{
		// Token: 0x06002ECD RID: 11981 RVA: 0x00532840 File Offset: 0x00530A40
		public override bool IsVanitySet(int head, int body, int legs)
		{
			if (Saethar_Head.equipSlots == null)
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
				Saethar_Head.equipSlots = array;
			}
			return head == Saethar_Head.equipSlots[0];
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x0053292C File Offset: 0x00530B2C
		[NullableContext(1)]
		public override void UpdateVanitySet(Player player)
		{
			SaetharSetEffectPlayer modPlayer;
			if (player.TryGetModPlayer<SaetharSetEffectPlayer>(out modPlayer))
			{
				modPlayer.IsActive = true;
			}
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x0053294A File Offset: 0x00530B4A
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(34f);
		}

		// Token: 0x04001C83 RID: 7299
		[Nullable(2)]
		private static int[] equipSlots;
	}
}
