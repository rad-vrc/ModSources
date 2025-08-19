using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Humanizer;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Items.Accessories.Fishing;
using QoLCompendium.Content.Items.Tools.Fishing;
using QoLCompendium.Content.Items.Tools.Usables;
using QoLCompendium.Core.UI.Panels;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace QoLCompendium.Core
{
	// Token: 0x0200020B RID: 523
	public class QoLCPlayer : ModPlayer
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x0005AAB4 File Offset: 0x00058CB4
		public override void Load()
		{
			On_ItemSlot.hook_RightClick_ItemArray_int_int hook_RightClick_ItemArray_int_int;
			if ((hook_RightClick_ItemArray_int_int = QoLCPlayer.<>O.<0>__ItemSlot_RightClick) == null)
			{
				hook_RightClick_ItemArray_int_int = (QoLCPlayer.<>O.<0>__ItemSlot_RightClick = new On_ItemSlot.hook_RightClick_ItemArray_int_int(QoLCPlayer.ItemSlot_RightClick));
			}
			On_ItemSlot.RightClick_ItemArray_int_int += hook_RightClick_ItemArray_int_int;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0005AAD6 File Offset: 0x00058CD6
		public override void Unload()
		{
			On_ItemSlot.hook_RightClick_ItemArray_int_int hook_RightClick_ItemArray_int_int;
			if ((hook_RightClick_ItemArray_int_int = QoLCPlayer.<>O.<0>__ItemSlot_RightClick) == null)
			{
				hook_RightClick_ItemArray_int_int = (QoLCPlayer.<>O.<0>__ItemSlot_RightClick = new On_ItemSlot.hook_RightClick_ItemArray_int_int(QoLCPlayer.ItemSlot_RightClick));
			}
			On_ItemSlot.RightClick_ItemArray_int_int -= hook_RightClick_ItemArray_int_int;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0005AAF8 File Offset: 0x00058CF8
		public override void ResetEffects()
		{
			this.Reset();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0005AAF8 File Offset: 0x00058CF8
		public override void UpdateDead()
		{
			this.Reset();
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0005AB00 File Offset: 0x00058D00
		public override void SaveData(TagCompound tag)
		{
			tag.Add("flaskEffectMode", this.flaskEffectMode);
			tag.Add("thoriumCoatingMode", this.thoriumCoatingMode);
			tag.Add("SelectedBiome", this.selectedBiome);
			tag.Add("SelectedSpawnModifier", this.selectedSpawnModifier);
			tag.Add("bossToSpawn", this.bossToSpawn);
			tag.Add("bossSpawn", this.bossSpawn);
			tag.Add("eventToSpawn", this.eventToSpawn);
			tag.Add("eventSpawn", this.eventSpawn);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0005ABC0 File Offset: 0x00058DC0
		public override void LoadData(TagCompound tag)
		{
			this.flaskEffectMode = tag.GetInt("flaskEffectMode");
			this.thoriumCoatingMode = tag.GetInt("thoriumCoatingMode");
			this.selectedBiome = tag.GetInt("SelectedBiome");
			this.selectedSpawnModifier = tag.GetInt("SelectedSpawnModifier");
			this.bossToSpawn = tag.GetInt("bossToSpawn");
			this.bossSpawn = tag.GetBool("bossSpawn");
			this.eventToSpawn = tag.GetInt("eventToSpawn");
			this.eventSpawn = tag.GetBool("eventSpawn");
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0005AC55 File Offset: 0x00058E55
		public override void PreUpdate()
		{
			if (this.spawnRateUpdateTimer > 0)
			{
				this.spawnRateUpdateTimer--;
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0005AC70 File Offset: 0x00058E70
		public override void PostUpdate()
		{
			if (ModConditions.reforgedLoaded && !ModAccessorySlot.Player.equippedWings.social)
			{
				ModConditions.reforgedMod.Call(new object[]
				{
					"PostUpdateModPlayer",
					Main.LocalPlayer.whoAmI,
					ModAccessorySlot.Player.equippedWings
				});
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0005ACD0 File Offset: 0x00058ED0
		public override void PostUpdateMiscEffects()
		{
			if (this.spawnRateUpdateTimer <= 0)
			{
				this.spawnRateUpdateTimer = 60;
				QoLCPlayer.spawnRateFieldInfo = typeof(NPC).GetField("spawnRate", BindingFlags.Static | BindingFlags.NonPublic);
				this.spawnRate = (int)QoLCPlayer.spawnRateFieldInfo.GetValue(null);
			}
			if (base.Player.whoAmI != Main.myPlayer || !Main.mapFullscreen || !Main.mouseLeft || !Main.mouseLeftRelease || !this.warpMirror)
			{
				return;
			}
			PlayerInput.SetZoom_Unscaled();
			float scale = 16f / Main.mapFullscreenScale;
			float minX = Main.mapFullscreenPos.X * 16f - 10f;
			float num = Main.mapFullscreenPos.Y * 16f - 21f;
			float mouseX = (float)(Main.mouseX - Main.screenWidth / 2);
			float mouseY = (float)(Main.mouseY - Main.screenHeight / 2);
			float cursorOnMapX = minX + mouseX * scale;
			float cursorOnMapY = num + mouseY * scale;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC teleportNPC = Main.npc[i];
				if (teleportNPC.active && teleportNPC.townNPC)
				{
					float minClickX = teleportNPC.position.X - 14f * scale;
					float minClickY = teleportNPC.position.Y - 14f * scale;
					float maxClickX = teleportNPC.position.X + 14f * scale;
					float maxClickY = teleportNPC.position.Y + 14f * scale;
					if (cursorOnMapX >= minClickX && cursorOnMapX <= maxClickX && cursorOnMapY >= minClickY && cursorOnMapY <= maxClickY)
					{
						Main.mouseLeftRelease = false;
						Main.mapFullscreen = false;
						base.Player.Teleport(teleportNPC.position + new Vector2(0f, -6f), 0, 0);
						PlayerInput.SetZoom_Unscaled();
						return;
					}
				}
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0005AEA0 File Offset: 0x000590A0
		public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
		{
			if (!QoLCompendium.itemConfig.FishingAccessories)
			{
				return;
			}
			if (base.Player.anglerQuestsFinished == 1)
			{
				rewardItems.Add(new Item(ModContent.ItemType<AnglerRadar>(), 1, 0)
				{
					stack = 1
				});
				return;
			}
			if (base.Player.anglerQuestsFinished >= 1 && Main.rand.NextBool(10))
			{
				rewardItems.Add(new Item(ModContent.ItemType<AnglerRadar>(), 1, 0)
				{
					stack = 1
				});
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0005AF1C File Offset: 0x0005911C
		public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
		{
			if (!attempt.inLava && !attempt.inHoney && Main.bloodMoon && attempt.crate && QoLCompendium.itemConfig.BottomlessBuckets && !attempt.uncommon && !attempt.rare && (attempt.veryrare || attempt.legendary) && Main.rand.NextBool())
			{
				itemDrop = ModContent.ItemType<BottomlessChumBucket>();
				return;
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0005AF90 File Offset: 0x00059190
		public override void OnHurt(Player.HurtInfo info)
		{
			if (this.sillySlapper)
			{
				base.Player.KillMe(PlayerDeathReason.ByCustomReason(NetworkText.FromKey(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.SillySlapper"), new object[]
				{
					base.Player.name
				}), Array.Empty<object>())), 2147483647.0, 0, false);
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0005AFED File Offset: 0x000591ED
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			if (QoLCompendium.itemConfig.StarterBag)
			{
				return new <>z__ReadOnlySingleElementList<Item>(new Item(ModContent.ItemType<StarterBag>(), 1, 0));
			}
			return Array.Empty<Item>();
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0005B014 File Offset: 0x00059214
		private static void ItemSlot_RightClick(On_ItemSlot.orig_RightClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
		{
			if (Main.mouseRight && Main.mouseRightRelease)
			{
				Player player = Main.LocalPlayer;
				QoLCPlayer qPlayer;
				player.TryGetModPlayer<QoLCPlayer>(out qPlayer);
				Common.IRightClickOverrideWhenHeld rightClickOverride = Main.mouseItem.ModItem as Common.IRightClickOverrideWhenHeld;
				if (rightClickOverride != null && rightClickOverride.RightClickOverrideWhileHeld(ref Main.mouseItem, inv, context, slot, player, qPlayer))
				{
					return;
				}
				if (context == 0 && GoldenLockpick.UseKey(inv, slot, player, qPlayer))
				{
					return;
				}
			}
			orig.Invoke(inv, context, slot);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0005B07C File Offset: 0x0005927C
		public void Reset()
		{
			this.sunPedestal = false;
			this.moonPedestal = false;
			this.bloodMoonPedestal = false;
			this.eclipsePedestal = false;
			this.pausePedestal = false;
			this.increasedSpawns = false;
			this.decreasedSpawns = false;
			this.noSpawns = false;
			this.sillySlapper = false;
			this.warpMirror = false;
			this.HasGoldenLockpick = false;
			this.activeItems.Clear();
			this.activeBuffItems.Clear();
			this.activeBuffs.Clear();
			Common.Reset();
			if (Main.netMode != 2 && Main.player[Main.myPlayer].talkNPC == -1)
			{
				Mod terraguardians;
				if (ModLoader.TryGetMod("terraguardians", out terraguardians) && !(bool)terraguardians.Call(new object[]
				{
					"IsPC",
					Main.LocalPlayer
				}))
				{
					return;
				}
				BlackMarketDealerNPCUI.visible = false;
				EtherealCollectorNPCUI.visible = false;
			}
		}

		// Token: 0x04000560 RID: 1376
		public bool sunPedestal;

		// Token: 0x04000561 RID: 1377
		public bool moonPedestal;

		// Token: 0x04000562 RID: 1378
		public bool bloodMoonPedestal;

		// Token: 0x04000563 RID: 1379
		public bool eclipsePedestal;

		// Token: 0x04000564 RID: 1380
		public bool pausePedestal;

		// Token: 0x04000565 RID: 1381
		public bool increasedSpawns;

		// Token: 0x04000566 RID: 1382
		public bool decreasedSpawns;

		// Token: 0x04000567 RID: 1383
		public bool noSpawns;

		// Token: 0x04000568 RID: 1384
		public int selectedSpawnModifier;

		// Token: 0x04000569 RID: 1385
		public int spawnRate;

		// Token: 0x0400056A RID: 1386
		public int spawnRateUpdateTimer;

		// Token: 0x0400056B RID: 1387
		private static FieldInfo spawnRateFieldInfo;

		// Token: 0x0400056C RID: 1388
		public int bossToSpawn;

		// Token: 0x0400056D RID: 1389
		public bool bossSpawn;

		// Token: 0x0400056E RID: 1390
		public int eventToSpawn;

		// Token: 0x0400056F RID: 1391
		public bool eventSpawn;

		// Token: 0x04000570 RID: 1392
		public int flaskEffectMode;

		// Token: 0x04000571 RID: 1393
		public int thoriumCoatingMode;

		// Token: 0x04000572 RID: 1394
		public bool sillySlapper;

		// Token: 0x04000573 RID: 1395
		public bool warpMirror;

		// Token: 0x04000574 RID: 1396
		public bool HasGoldenLockpick;

		// Token: 0x04000575 RID: 1397
		public List<int> activeItems = new List<int>();

		// Token: 0x04000576 RID: 1398
		public List<int> activeBuffItems = new List<int>();

		// Token: 0x04000577 RID: 1399
		public List<int> activeBuffs = new List<int>();

		// Token: 0x04000578 RID: 1400
		public int selectedBiome;

		// Token: 0x02000537 RID: 1335
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000D4E RID: 3406
			public static On_ItemSlot.hook_RightClick_ItemArray_int_int <0>__ItemSlot_RightClick;
		}
	}
}
