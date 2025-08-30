using System;
using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace StackablePetItems
{
	// StackablePetItemsSystem — RightClickのILを書き換え、スタック可能なペット/マウント等を装備操作に対応させる
	// 注意: RadQoL.dll には Mod は RadQoL のみとするため、ModSystem として実装
	public class StackablePetItemsSystem : ModSystem
	{
		private static ILContext.Manipulator s_RightClickManipulator;

		public override void Load()
		{
			// 単純なキャッシュでOK（関数ポインタ互換）
			s_RightClickManipulator ??= RightClickILEdit;
			IL_ItemSlot.RightClick_ItemArray_int_int += s_RightClickManipulator;
		}

		public override void Unload()
		{
			if (s_RightClickManipulator != null)
			{
				try { IL_ItemSlot.RightClick_ItemArray_int_int -= s_RightClickManipulator; }
				catch { /* ignore on unload ordering */ }
			}
			s_RightClickManipulator = null;
		}

		// 元コードのまま（intで返却：IL上でldfldの置換先として使用）
		private static int CanEquipItem(Item item)
		{
			if (item.stack != 1)
				return 0;
			if (item.accessory)
				return 1;
			if (item.headSlot != -1)
				return 1;
			if (item.bodySlot != -1)
				return 1;
			if (item.legSlot != -1)
				return 1;
			if (item.dye > 0)
				return 1;
			if (Main.projHook[item.shoot])
				return 1;
			if (item.mountType != -1)
				return 1;
			if (item.buffType <= 0)
				return 0;
			if (Main.lightPet[item.buffType])
				return 1;
			if (Main.vanityPet[item.buffType])
				return 1;
			return 0;
		}

		// Item.maxStack の読み出し箇所を CanEquipItem(item) 呼び出しへ差し替える
	private static void RightClickILEdit(ILContext il)
		{
			var cursor = new ILCursor(il);
			var maxStackField = typeof(Item).GetField(nameof(Item.maxStack));
			if (maxStackField == null)
				return; // 仕様変更に備え安全に抜ける

			if (cursor.TryGotoNext(i => ILPatternMatchingExt.MatchLdfld(i, maxStackField)))
			{
				// 直前の '... ldarg.?, ldfld Item.maxStack' の ldfld を除去し、
				// スタックに残った Item を引数に CanEquipItem を呼び出して int を積む
				cursor.Remove();
				cursor.EmitDelegate<Func<Item, int>>(CanEquipItem);
			}
		}
	}
}
