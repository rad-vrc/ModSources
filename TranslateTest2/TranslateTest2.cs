using InventoryDrag;
using InventoryDrag.Config;
using InventoryDrag.Compatability;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI; // needed for ItemSlot & On_ItemSlot hooks
using MonoMod.RuntimeDetour; // for MonoModHooks
using MonoMod.RuntimeDetour.HookGen; // ensure hookgen symbols
using TranslateTest2.Core;
using TranslateTest2.Config;

namespace TranslateTest2
{
	using System.Reflection;
	public class TranslateTest2 : Mod
	{
		public static TranslateTest2 Instance { get; private set; }
		// Failsafe: cap how many extra AI steps a minion projectile may take per tick
		private const int MaxAIIterationsPerTick = 8;
		public static MethodInfo ItemLoader_CanRightClick = typeof(ItemLoader).GetMethod("CanRightClick", BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Static);
		public static MethodInfo PlayerLoader_ShiftClickSlot = typeof(PlayerLoader).GetMethod("ShiftClickSlot", BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Static);
		// InventoryDrag original delegate signatures for MonoModHooks
		private delegate bool orig_ItemLoader_CanRightClick(Item item);
		private delegate bool orig_PlayerLoader_ShiftClickSlot(Player player, Item[] inventory, int context, int slot);
		
		public override void Load()
		{
			try
			{
				Instance = this;
				
				TooltipTranslator.Load(this);
				var cfg = ModContent.GetInstance<ClientConfig>();
				DeepLTranslator.ApplyConfig(cfg);

				// Hook registration with additional safety checks and failure recovery
				bool whipHookSuccess = false;
				bool aiHookSuccess = false;

				try
				{
					On_Projectile.GetWhipSettings += hook_get_whip_settings;
					whipHookSuccess = true;
					Logger?.Info("WhipSettings hook registered successfully");
				}
				catch (Exception ex)
				{
					Logger?.Error($"Failed to register WhipSettings hook: {ex.Message}");
				}

				try
				{
					On_Projectile.AI += hook_projectile_ai;
					aiHookSuccess = true;
					Logger?.Info("Projectile AI hook registered successfully");
				}
				catch (Exception ex)
				{
					Logger?.Error($"Failed to register Projectile AI hook: {ex.Message}");
				}

				// 診断情報を出力
				Logger?.Info($"Hook registration status: WhipSettings={whipHookSuccess}, ProjectileAI={aiHookSuccess}");
				
				if (!whipHookSuccess && !aiHookSuccess)
				{
					Logger?.Warn("Critical: No hooks were registered successfully. Some features may not work.");
				}
				
				Logger?.Info("TranslateTest2 loaded successfully");
			}
			catch (Exception ex)
			{
				Logger?.Error($"Critical error loading TranslateTest2: {ex}");
				// クリティカルエラーでも完全に失敗させない
				try
				{
					Instance = this; // 最低限のインスタンス設定は維持
				}
				catch { }
			}
			
			// InventoryDrag integration begin
			try
			{
				On_ItemSlot.MouseHover_ItemArray_int_int += Hook_ItemSlot_MouseHover;
				On_ItemSlot.RightClick_ItemArray_int_int += Hook_ItemSlot_RightClick;
				On_ItemSlot.Handle_refItem_int += Hook_ItemSlot_Handle;
				On_ItemSlot.LeftClick_ItemArray_int_int += Hook_ItemSlot_LeftClick;
				On_Main.DrawInventory += Hook_Main_DrawInventory;
				// MonoModHooks.Add with (origDelegate, args...) signature to mirror original mod implementation exactly
				if (ItemLoader_CanRightClick != null)
					MonoModHooks.Add(ItemLoader_CanRightClick, (orig_ItemLoader_CanRightClick orig, Item item) => On_ItemLoader_CanRightClick_Item(orig, item));
				else
					Logger?.Warn("Reflection failed: ItemLoader.CanRightClick not found; skipping hook");

				if (PlayerLoader_ShiftClickSlot != null)
					MonoModHooks.Add(PlayerLoader_ShiftClickSlot, (orig_PlayerLoader_ShiftClickSlot orig, Player pl, Item[] inv, int ctx, int sl) => On_PlayerLoader_ShiftClickSlot(orig, pl, inv, ctx, sl));
				else
					Logger?.Warn("Reflection failed: PlayerLoader.ShiftClickSlot not found; skipping hook");
				AndroLib.Load(this);
			}
			catch (System.Exception ex)
			{
				Logger?.Error($"InventoryDrag integration failed: {ex.Message}");
			}
			// InventoryDrag integration end
		}

		public override void Unload()
		{
			try
			{
				TooltipTranslator.Unload();
				DeepLTranslator.Unload();
				
				// 安全なフック解除
				try { On_Projectile.GetWhipSettings -= hook_get_whip_settings; } catch { }
				try { On_Projectile.AI -= hook_projectile_ai; } catch { }
				
				// InventoryDrag integration unload begin
				try
				{
					On_ItemSlot.MouseHover_ItemArray_int_int -= Hook_ItemSlot_MouseHover;
					On_ItemSlot.RightClick_ItemArray_int_int -= Hook_ItemSlot_RightClick;
					On_ItemSlot.Handle_refItem_int -= Hook_ItemSlot_Handle;
					On_ItemSlot.LeftClick_ItemArray_int_int -= Hook_ItemSlot_LeftClick;
					On_Main.DrawInventory -= Hook_Main_DrawInventory;
					AndroLib.Unload(this);
				}
				catch { }
				// InventoryDrag integration unload end
				
				Logger?.Info("TranslateTest2 unloaded successfully");
			}
			catch (Exception ex)
			{
				Logger?.Warn($"Error during unload: {ex.Message}");
			}
			finally
			{
				Instance = null;
			}
		}

		private static readonly MethodInfo HandleMovementMI = typeof(Projectile).GetMethod("HandleMovement", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		private void hook_projectile_ai(On_Projectile.orig_AI orig, Projectile self)
		{
			if (self == null) return;
			if (self.minion || self.sentry)
			{
				var gp = self.GetGlobalProjectile<SPGlobalProj>();
				// Clamp speed and counter to avoid runaway iterations
				float step = gp.MinionUpdateSpeed;
				if (!float.IsFinite(step) || step <= 0f) step = 1f;
				if (step > MaxAIIterationsPerTick) step = MaxAIIterationsPerTick;
				gp.MinionUpdateSpeed = step;
				gp.UpdateCounter = Math.Min(gp.UpdateCounter + step, MaxAIIterationsPerTick * 4f);

				int iter = 0;
				while (gp.UpdateCounter >= 1f && iter++ < MaxAIIterationsPerTick)
				{
					gp.UpdateCounter -= 1f;
					orig(self);
					if (gp.UpdateCounter >= 1f && HandleMovementMI != null)
					{
						try { HandleMovementMI.Invoke(self, new object[] { self.velocity, null, null }); } catch { }
					}
				}
			}
			else
			{
				orig(self);
			}
		}

		private void hook_get_whip_settings(
			On_Projectile.orig_GetWhipSettings orig,
			Projectile proj,
			out float timeToFlyOut,
			out int segments,
			out float rangeMultiplier)
		{
			orig(proj, out timeToFlyOut, out segments, out rangeMultiplier);
			if (!proj.friendly || proj.owner == 255) return;
			var player = GetSafePlayer(proj.owner);
			if (player != null && player.HeldItem != null && !player.HeldItem.IsAir)
				rangeMultiplier *= player.HeldItem.global().WhipRangeMult;
		}

		// プレイヤーインデックスの安全な検証（強化版）
		private static bool IsValidPlayerIndex(int playerIndex)
		{
			try
			{
				return playerIndex >= 0 && 
					   playerIndex < Main.maxPlayers && 
					   playerIndex != 255 &&
					   Main.player != null &&
					   playerIndex < Main.player.Length &&
					   Main.player[playerIndex] != null;
			}
			catch
			{
				return false;
			}
		}

		// プレイヤーの安全な取得（例外処理強化版）
		private static Player GetSafePlayer(int playerIndex)
		{
			try
			{
				if (!IsValidPlayerIndex(playerIndex))
					return null;

				// 二重チェックで安全性を確保
				var players = Main.player;
				if (players == null || playerIndex >= players.Length)
					return null;

				var player = players[playerIndex];
				return player?.active == true ? player : null;
			}
			catch (IndexOutOfRangeException)
			{
				TranslateTest2.Instance?.Logger?.Debug($"Player index out of range: {playerIndex}");
				return null;
			}
			catch (System.Exception ex)
			{
				TranslateTest2.Instance?.Logger?.Debug($"Error getting player {playerIndex}: {ex.Message}");
				return null;
			}
		}

		// モジュール状態の診断用
		public override void PostSetupContent()
		{
			try
			{
				Logger?.Info($"PostSetupContent: Translator loaded={TooltipTranslator.IsLoaded}, DeepL enabled={DeepLTranslator.IsEnabled}");
				
				// Prefix登録状況の診断とCategory検証
				var prefixTypes = new[]
				{
					"Abusive", "Affable", "Blessing", "Brisk", "Contract", "Deviation", "Devoted",
					"Eager", "Echo", "Electrified", "Extended", "Fabled", "Focused", "Focused_Whip",
					"Huge", "Loyal", "Mega", "Oracle", "Overload", "Steady", "Vengeful"
				};
				
				int registeredCount = 0;
				int meleeCount = 0;
				int magicCount = 0;
				
				foreach (var prefixName in prefixTypes)
				{
					try
					{
						var prefixType = ModContent.Find<ModPrefix>($"TranslateTest2/{prefixName}");
						if (prefixType != null)
						{
							registeredCount++;
							string category = prefixType.Category.ToString();
							if (prefixType.Category == PrefixCategory.Melee) meleeCount++;
							else if (prefixType.Category == PrefixCategory.Magic) magicCount++;
							
							Logger?.Debug($"Prefix registered: {prefixName} (ID: {prefixType.Type}, Category: {category})");
						}
						else
						{
							Logger?.Warn($"Prefix NOT registered: {prefixName}");
						}
					}
					catch (Exception ex)
					{
						Logger?.Error($"Error checking prefix {prefixName}: {ex.Message}");
					}
				}
				
				Logger?.Info($"Registered Prefixes: {registeredCount}/{prefixTypes.Length} (Melee: {meleeCount}, Magic: {magicCount})");
				
				// Prefix適用テスト結果の要約
				Logger?.Info("Prefix Category Assignment:");
				Logger?.Info("- Whip Prefixes (Melee): Focused_Whip, Devoted, Extended, Vengeful, Abusive, Oracle");
				Logger?.Info("- Summon Prefixes (Magic): Focused, Affable, Blessing, Brisk, Contract, Deviation, Eager, Echo, Electrified, Fabled, Huge, Loyal, Mega, Overload, Steady");
				
				// ローカリゼーション対応Prefixのツールチップ診断
				Logger?.Info("Localized Prefix Tooltips:");
				DiagnosePrefixTooltips();
			}
			catch (Exception ex)
			{
				Logger?.Warn($"PostSetupContent error: {ex.Message}");
			}
		}

		private void DiagnosePrefixTooltips()
		{
			try
			{
				Logger?.Info("=== Localization Diagnosis ===");
				
				// 現在の言語設定確認
				Logger?.Info($"Current Language: {Language.ActiveCulture?.Name ?? "Unknown"}");
				
				// ファイルの存在確認と内容の一部確認
				var localizationFiles = new[]
				{
					"Localization/en-US_Mods.TranslateTest2.hjson",
					"Localization/ja-JP_Mods.TranslateTest2.hjson"
				};
				
				foreach (var file in localizationFiles)
				{
					try
					{
						using var stream = TranslateTest2.Instance.GetFileStream(file);
						if (stream != null)
						{
							Logger?.Info($"  File {file}: EXISTS");
							
							// ファイル内容の一部を確認
							using var reader = new System.IO.StreamReader(stream);
							var content = reader.ReadToEnd();
							
							// PrefixBriskDescrが含まれているか確認
							if (content.Contains("PrefixBriskDescr"))
							{
								Logger?.Info($"    - Contains PrefixBriskDescr: YES");
								
								// 該当行を抽出
								var lines = content.Split('\n');
								foreach (var line in lines)
								{
									if (line.Contains("PrefixBriskDescr") && !line.Trim().StartsWith("//") && !line.Trim().StartsWith("/*"))
									{
										Logger?.Info($"    - Line: {line.Trim()}");
									}
								}
							}
							else
							{
								Logger?.Warn($"    - Contains PrefixBriskDescr: NO");
							}
						}
						else
						{
							Logger?.Warn($"  File {file}: NOT FOUND");
						}
					}
					catch (Exception ex)
					{
						Logger?.Warn($"  File {file}: ERROR ({ex.Message})");
					}
				}
				
				// 直接的なキーテスト
				var directTests = new[]
				{
					"Mods.TranslateTest2.PrefixBriskDescr",
					"Mods.TranslateTest2.PrefixAffableDescr", 
					"Mods.TranslateTest2.PrefixFocusedDescr",
					"Mods.TranslateTest2.PrefixExtendedDescr",
					"Mods.TranslateTest2.PrefixOracleDescr"
				};
				
				Logger?.Info("Direct Key Tests:");
				foreach (var testKey in directTests)
				{
					try
					{
						var value = Language.GetTextValue(testKey);
						bool isResolved = !string.IsNullOrEmpty(value) && value != testKey;
						Logger?.Info($"  {testKey}: {(isResolved ? "RESOLVED" : "UNRESOLVED")} -> \"{value}\"");
						
						// 特に「攻撃範囲」の文言があるかチェック
						if (isResolved && value.Contains("攻撃範囲"))
						{
							Logger?.Info($"    ✓ Contains '攻撃範囲' - Localization applied successfully!");
						}
					}
					catch (Exception ex)
					{
						Logger?.Error($"  {testKey}: EXCEPTION -> {ex.Message}");
					}
				}
				
			}
			catch (Exception ex)
			{
				Logger?.Warn($"Error diagnosing tooltip localization: {ex.Message}");
			}
		}
		
		private static bool On_ItemLoader_CanRightClick_Item(orig_ItemLoader_CanRightClick orig, Item item)
		{
			bool flag = orig(item);
			return (!InventoryConfig.Instance.SplittableGrabBags.Enabled || !ItemSlot.ShiftInUse || Main.ItemDropsDB.GetRulesForItemID(item.type).Count <= 0) && flag;
		}
		private static bool On_PlayerLoader_ShiftClickSlot(orig_PlayerLoader_ShiftClickSlot orig, Player player, Item[] inventory, int context, int slot)
		{
			bool flag = orig(player, inventory, context, slot);
			if (player.TryGetModPlayer<InventoryPlayer>(out var ip)) ip.overrideShiftLeftClick = flag; return flag;
		}
		private static void Hook_ItemSlot_MouseHover(On_ItemSlot.orig_MouseHover_ItemArray_int_int orig, Item[] inv, int context, int slot)
		{
			if (Main.LocalPlayer.TryGetModPlayer<InventoryPlayer>(out var ip))
				ip.OverrideHover(inv, context, slot);
			orig(inv, context, slot);
		}
		private static void Hook_ItemSlot_RightClick(On_ItemSlot.orig_RightClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
		{
			if (Main.LocalPlayer.TryGetModPlayer<InventoryPlayer>(out var ip)) ip.rightClickCache = Main.mouseRightRelease;
			orig(inv, context, slot);
		}
		private void Hook_ItemSlot_LeftClick(On_ItemSlot.orig_LeftClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
		{
			if (Main.LocalPlayer.TryGetModPlayer<InventoryPlayer>(out var ip)) ip.leftClickCache = Main.mouseLeftRelease;
			orig(inv, context, slot);
		}
		private void Hook_ItemSlot_Handle(On_ItemSlot.orig_Handle_refItem_int orig, ref Item inv, int context)
		{
			if (Main.LocalPlayer.TryGetModPlayer<InventoryPlayer>(out var ip)) ip.noSlot = false;
			orig(ref inv, context);
		}
		private void Hook_Main_DrawInventory(On_Main.orig_DrawInventory orig, Main self)
		{
			var lp = Main.LocalPlayer;
			if (lp != null && lp.TryGetModPlayer<InventoryPlayer>(out var mp))
			{
				mp.hovering = false;
				orig(self);
				mp.noSlot = !mp.hovering;
			}
			else
			{
				orig(self);
			}
		}
		public static void DebugInChat(string text)
		{
			if (!InventoryConfig.Instance.DebugMessages) return;
			Main.NewText(text, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
	}
}
