using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.IO;
using Terraria.ModLoader.Utilities;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central class from which NPC-related functions are carried out. It also stores a list of mod NPCs by ID.
	/// </summary>
	// Token: 0x020001DF RID: 479
	public static class NPCLoader
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x004EBD23 File Offset: 0x004E9F23
		// (set) Token: 0x0600251A RID: 9498 RVA: 0x004EBD2A File Offset: 0x004E9F2A
		public static int NPCCount { get; private set; } = (int)NPCID.Count;

		// Token: 0x0600251B RID: 9499 RVA: 0x004EBD34 File Offset: 0x004E9F34
		private static GlobalHookList<GlobalNPC> AddHook<F>(Expression<Func<GlobalNPC, F>> func) where F : Delegate
		{
			GlobalHookList<GlobalNPC> hook = GlobalHookList<GlobalNPC>.Create<F>(func);
			NPCLoader.hooks.Add(hook);
			return hook;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x004EBD54 File Offset: 0x004E9F54
		public static T AddModHook<T>(T hook) where T : GlobalHookList<GlobalNPC>
		{
			NPCLoader.modHooks.Add(hook);
			return hook;
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x004EBD67 File Offset: 0x004E9F67
		internal static int Register(ModNPC npc)
		{
			NPCLoader.npcs.Add(npc);
			return NPCLoader.NPCCount++;
		}

		/// <summary>
		/// Gets the ModNPC template instance corresponding to the specified type (not the clone/new instance which gets added to NPCs as the game is played).
		/// </summary>
		/// <param name="type">The type of the npc</param>
		/// <returns>The ModNPC instance in the <see cref="F:Terraria.ModLoader.NPCLoader.npcs" /> array, null if not found.</returns>
		// Token: 0x0600251E RID: 9502 RVA: 0x004EBD81 File Offset: 0x004E9F81
		public static ModNPC GetNPC(int type)
		{
			if (type < (int)NPCID.Count || type >= NPCLoader.NPCCount)
			{
				return null;
			}
			return NPCLoader.npcs[type - (int)NPCID.Count];
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x004EBDA8 File Offset: 0x004E9FA8
		internal static void ResizeArrays(bool unloading)
		{
			if (!unloading)
			{
				GlobalList<GlobalNPC>.FinishLoading(NPCLoader.NPCCount);
			}
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Npc, NPCLoader.NPCCount);
			LoaderUtils.ResetStaticMembers(typeof(NPCID), true);
			Main.ShopHelper.ReinitializePersonalityDatabase();
			NPCHappiness.RegisterVanillaNpcRelationships();
			Array.Resize<bool>(ref Main.townNPCCanSpawn, NPCLoader.NPCCount);
			Array.Resize<bool>(ref Main.slimeRainNPC, NPCLoader.NPCCount);
			Array.Resize<bool>(ref Main.npcCatchable, NPCLoader.NPCCount);
			Array.Resize<int>(ref Main.npcFrameCount, NPCLoader.NPCCount);
			Array.Resize<bool>(ref Main.SceneMetrics.NPCBannerBuff, NPCLoader.NPCCount);
			Array.Resize<int>(ref NPC.killCount, NPCLoader.NPCCount);
			Array.Resize<bool>(ref NPC.ShimmeredTownNPCs, NPCLoader.NPCCount);
			Array.Resize<bool>(ref NPC.npcsFoundForCheckActive, NPCLoader.NPCCount);
			Array.Resize<LocalizedText>(ref Lang._npcNameCache, NPCLoader.NPCCount);
			Array.Resize<int>(ref EmoteBubble.CountNPCs, NPCLoader.NPCCount);
			Array.Resize<bool>(ref WorldGen.TownManager._hasRoom, NPCLoader.NPCCount);
			Player[] player = Main.player;
			for (int j = 0; j < player.Length; j++)
			{
				Array.Resize<bool>(ref player[j].npcTypeNoAggro, NPCLoader.NPCCount);
			}
			for (int i = (int)NPCID.Count; i < NPCLoader.NPCCount; i++)
			{
				Main.npcFrameCount[i] = 1;
				Lang._npcNameCache[i] = LocalizedText.Empty;
			}
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x004EBEF0 File Offset: 0x004EA0F0
		internal static void FinishSetup()
		{
			NPC temp = new NPC();
			GlobalLoaderUtils<GlobalNPC, NPC>.BuildTypeLookups(delegate(int type)
			{
				temp.SetDefaults(type, default(NPCSpawnParams));
			});
			NPCLoader.UpdateHookLists();
			GlobalTypeLookups<GlobalNPC>.LogStats();
			foreach (ModNPC npc in NPCLoader.npcs)
			{
				Lang._npcNameCache[npc.Type] = npc.DisplayName;
				NPCLoader.RegisterTownNPCMoodLocalizations(npc);
				if ((npc.BannerItem != 0 && npc.Banner == 0) || (npc.Banner >= (int)NPCID.Count && (!NPCLoader.bannerToItem.ContainsKey(npc.Banner) || (npc.BannerItem != 0 && NPCLoader.bannerToItem[npc.Banner] != npc.BannerItem))))
				{
					Logging.tML.Warn(Language.GetTextValue("tModLoader.LoadWarningBannerOrBannerItemNotSet", npc.Mod.Name, npc.Name));
				}
			}
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x004EBFF4 File Offset: 0x004EA1F4
		private static void UpdateHookLists()
		{
			foreach (GlobalHookList<GlobalNPC> globalHookList in NPCLoader.hooks.Union(NPCLoader.modHooks))
			{
				globalHookList.Update();
			}
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x004EC048 File Offset: 0x004EA248
		internal static void RegisterTownNPCMoodLocalizations(ModNPC npc)
		{
			if (npc.NPC.townNPC && !NPCID.Sets.IsTownPet[npc.NPC.type] && !NPCID.Sets.NoTownNPCHappiness[npc.NPC.type])
			{
				string prefix = npc.GetLocalizationKey("TownNPCMood");
				List<string> keys = new List<string>
				{
					"Content",
					"NoHome",
					"FarFromHome",
					"LoveSpace",
					"DislikeCrowded",
					"HateCrowded"
				};
				PersonalityProfile personalityProfile;
				if (Main.ShopHelper._database.TryGetProfileByNPCID(npc.NPC.type, out personalityProfile))
				{
					List<IShopPersonalityTrait> shopModifiers = personalityProfile.ShopModifiers;
					BiomePreferenceListTrait biomePreferenceList = (BiomePreferenceListTrait)shopModifiers.SingleOrDefault((IShopPersonalityTrait t) => t is BiomePreferenceListTrait);
					if (biomePreferenceList != null)
					{
						if (biomePreferenceList.Preferences.Any((BiomePreferenceListTrait.BiomePreference x) => x.Affection == AffectionLevel.Love))
						{
							keys.Add("LoveBiome");
						}
						if (biomePreferenceList.Preferences.Any((BiomePreferenceListTrait.BiomePreference x) => x.Affection == AffectionLevel.Like))
						{
							keys.Add("LikeBiome");
						}
						if (biomePreferenceList.Preferences.Any((BiomePreferenceListTrait.BiomePreference x) => x.Affection == AffectionLevel.Dislike))
						{
							keys.Add("DislikeBiome");
						}
						if (biomePreferenceList.Preferences.Any((BiomePreferenceListTrait.BiomePreference x) => x.Affection == AffectionLevel.Hate))
						{
							keys.Add("HateBiome");
						}
					}
					if (shopModifiers.Any(delegate(IShopPersonalityTrait t)
					{
						NPCPreferenceTrait npcpreferenceTrait = t as NPCPreferenceTrait;
						return npcpreferenceTrait != null && npcpreferenceTrait.Level == AffectionLevel.Love;
					}))
					{
						keys.Add("LoveNPC");
					}
					if (shopModifiers.Any(delegate(IShopPersonalityTrait t)
					{
						NPCPreferenceTrait npcpreferenceTrait = t as NPCPreferenceTrait;
						return npcpreferenceTrait != null && npcpreferenceTrait.Level == AffectionLevel.Like;
					}))
					{
						keys.Add("LikeNPC");
					}
					if (shopModifiers.Any(delegate(IShopPersonalityTrait t)
					{
						NPCPreferenceTrait npcpreferenceTrait = t as NPCPreferenceTrait;
						return npcpreferenceTrait != null && npcpreferenceTrait.Level == AffectionLevel.Dislike;
					}))
					{
						keys.Add("DislikeNPC");
					}
					if (shopModifiers.Any(delegate(IShopPersonalityTrait t)
					{
						NPCPreferenceTrait npcpreferenceTrait = t as NPCPreferenceTrait;
						return npcpreferenceTrait != null && npcpreferenceTrait.Level == AffectionLevel.Hate;
					}))
					{
						keys.Add("HateNPC");
					}
				}
				keys.Add("LikeNPC_Princess");
				keys.Add("Princess_LovesNPC");
				foreach (string key in keys)
				{
					string oldKey = npc.Mod.GetLocalizationKey("TownNPCMood." + npc.Name + "." + key);
					if (key == "Princess_LovesNPC")
					{
						oldKey = "TownNPCMood_Princess.LoveNPC_" + npc.FullName;
					}
					string key2 = prefix + "." + key;
					string defaultValueKey = "TownNPCMood." + key;
					Language.GetOrRegister(key2, delegate()
					{
						if (!Language.Exists(oldKey))
						{
							return Language.GetTextValue(defaultValueKey);
						}
						return "{$" + oldKey + "}";
					});
				}
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x004EC3C0 File Offset: 0x004EA5C0
		internal static void Unload()
		{
			NPCLoader.NPCCount = (int)NPCID.Count;
			NPCLoader.npcs.Clear();
			GlobalList<GlobalNPC>.Reset();
			NPCLoader.bannerToItem.Clear();
			NPCLoader.modHooks.Clear();
			NPCLoader.UpdateHookLists();
			if (!Main.dedServ)
			{
				TownNPCProfiles.Instance.ResetTexturesAccordingToVanillaProfiles();
			}
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x004EC410 File Offset: 0x004EA610
		internal static bool IsModNPC(NPC npc)
		{
			return npc.type >= (int)NPCID.Count;
		}

		/// <summary>
		/// Returns the type of a ModNPC corresponding to the provided banner item type. Note that this is equivalent to the banner type as well, since ModNPC type and banner type are the same. Returns -1 if not found.
		/// </summary>
		// Token: 0x06002525 RID: 9509 RVA: 0x004EC424 File Offset: 0x004EA624
		public static int BannerItemToNPC(int itemType)
		{
			int id;
			if (!NPCLoader.itemToBanner.TryGetValue(itemType, out id))
			{
				return -1;
			}
			return id;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x004EC444 File Offset: 0x004EA644
		internal static void SetDefaults(NPC npc, bool createModNPC = true)
		{
			if (NPCLoader.IsModNPC(npc))
			{
				if (createModNPC)
				{
					npc.ModNPC = NPCLoader.GetNPC(npc.type).NewInstance(npc);
				}
				else
				{
					Array.Resize<bool>(ref npc.buffImmune, BuffLoader.BuffCount);
				}
			}
			GlobalLoaderUtils<GlobalNPC, NPC>.SetDefaults(npc, ref npc._globals, delegate(NPC n)
			{
				ModNPC modNPC = n.ModNPC;
				if (modNPC == null)
				{
					return;
				}
				modNPC.SetDefaults();
			});
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x004EC4B0 File Offset: 0x004EA6B0
		internal static void SetDefaultsFromNetId(NPC npc)
		{
			foreach (GlobalNPC globalNPC in NPCLoader.HookSetVariantDefaults.Enumerate(npc))
			{
				globalNPC.SetDefaultsFromNetId(npc);
			}
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x004EC4EC File Offset: 0x004EA6EC
		internal static void OnSpawn(NPC npc, IEntitySource source)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.OnSpawn(source);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookOnSpawn.Enumerate(npc))
			{
				globalNPC.OnSpawn(npc, source);
			}
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x004EC538 File Offset: 0x004EA738
		public static void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.ApplyDifficultyAndPlayerScaling(numPlayers, balance, bossAdjustment);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookApplyDifficultyAndPlayerScaling.Enumerate(npc))
			{
				globalNPC.ApplyDifficultyAndPlayerScaling(npc, numPlayers, balance, bossAdjustment);
			}
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x004EC588 File Offset: 0x004EA788
		public static void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			if (NPCLoader.IsModNPC(npc))
			{
				bestiaryEntry.Info.Add(npc.ModNPC.Mod.ModSourceBestiaryInfoElement);
				foreach (int type in npc.ModNPC.SpawnModBiomes)
				{
					bestiaryEntry.Info.Add(LoaderManager.Get<BiomeLoader>().Get(type).ModBiomeBestiaryInfoElement);
				}
			}
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.SetBestiary(database, bestiaryEntry);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookSetBestiary.Enumerate(npc))
			{
				globalNPC.SetBestiary(npc, database, bestiaryEntry);
			}
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x004EC634 File Offset: 0x004EA834
		public static void ModifyTownNPCProfile(NPC npc, ref ITownNPCProfile profile)
		{
			ModNPC modNPC = npc.ModNPC;
			profile = (((modNPC != null) ? modNPC.TownNPCProfile() : null) ?? profile);
			foreach (GlobalNPC g in NPCLoader.HookModifyTownNPCProfile.Enumerate(npc))
			{
				profile = (g.ModifyTownNPCProfile(npc) ?? profile);
			}
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x004EC694 File Offset: 0x004EA894
		public static void ResetEffects(NPC npc)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.ResetEffects();
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookResetEffects.Enumerate(npc))
			{
				globalNPC.ResetEffects(npc);
			}
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x004EC6E0 File Offset: 0x004EA8E0
		public static void NPCAI(NPC npc)
		{
			if (NPCLoader.PreAI(npc))
			{
				int type = npc.type;
				bool flag = npc.ModNPC != null && npc.ModNPC.AIType > 0;
				if (flag)
				{
					npc.type = npc.ModNPC.AIType;
				}
				npc.VanillaAI();
				if (flag)
				{
					npc.type = type;
				}
				NPCLoader.AI(npc);
			}
			NPCLoader.PostAI(npc);
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x004EC744 File Offset: 0x004EA944
		public static bool PreAI(NPC npc)
		{
			bool result = true;
			foreach (GlobalNPC g in NPCLoader.HookPreAI.Enumerate(npc))
			{
				result &= g.PreAI(npc);
			}
			if (result && npc.ModNPC != null)
			{
				return npc.ModNPC.PreAI();
			}
			return result;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x004EC79C File Offset: 0x004EA99C
		public static void AI(NPC npc)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.AI();
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookAI.Enumerate(npc))
			{
				globalNPC.AI(npc);
			}
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x004EC7E8 File Offset: 0x004EA9E8
		public static void PostAI(NPC npc)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.PostAI();
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookPostAI.Enumerate(npc))
			{
				globalNPC.PostAI(npc);
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x004EC832 File Offset: 0x004EAA32
		public static void SendExtraAI(BinaryWriter writer, byte[] extraAI)
		{
			writer.Write7BitEncodedInt(extraAI.Length);
			if (extraAI.Length != 0)
			{
				writer.Write(extraAI);
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x004EC848 File Offset: 0x004EAA48
		public static byte[] WriteExtraAI(NPC npc)
		{
			byte[] result;
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter modWriter = new BinaryWriter(stream))
				{
					using (MemoryStream bufferedStream = new MemoryStream())
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(bufferedStream))
						{
							BitWriter bitWriter = new BitWriter();
							ModNPC modNPC = npc.ModNPC;
							if (modNPC != null)
							{
								modNPC.SendExtraAI(binaryWriter);
							}
							foreach (GlobalNPC globalNPC in NPCLoader.HookSendExtraAI.Enumerate(npc))
							{
								globalNPC.SendExtraAI(npc, bitWriter, binaryWriter);
							}
							bitWriter.Flush(modWriter);
							modWriter.Write(bufferedStream.ToArray());
							byte[] bytes = stream.ToArray();
							if (bytes.Length == 1)
							{
								result = null;
							}
							else
							{
								result = bytes;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x004EC948 File Offset: 0x004EAB48
		public static byte[] ReadExtraAI(BinaryReader reader)
		{
			return reader.ReadBytes(reader.Read7BitEncodedInt());
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x004EC958 File Offset: 0x004EAB58
		public static void ReceiveExtraAI(NPC npc, byte[] extraAI)
		{
			using (MemoryStream stream = extraAI.ToMemoryStream(false))
			{
				using (BinaryReader modReader = new BinaryReader(stream))
				{
					GlobalNPC lastGlobalNPC = null;
					try
					{
						BitReader bitReader = new BitReader(modReader);
						long bitReaderEnd = stream.Position;
						ModNPC modNPC = npc.ModNPC;
						if (modNPC != null)
						{
							modNPC.ReceiveExtraAI(modReader);
						}
						foreach (GlobalNPC globalNPC in NPCLoader.HookReceiveExtraAI.Enumerate(npc))
						{
							(lastGlobalNPC = globalNPC).ReceiveExtraAI(npc, bitReader, modReader);
						}
						if (bitReader.BitsRead < bitReader.MaxBits)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(70, 2);
							defaultInterpolatedStringHandler.AppendLiteral("Read underflow ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(bitReader.MaxBits - bitReader.BitsRead);
							defaultInterpolatedStringHandler.AppendLiteral(" of ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(bitReader.MaxBits);
							defaultInterpolatedStringHandler.AppendLiteral(" compressed bits in ReceiveExtraAI, more info below");
							throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear());
						}
						if (stream.Position < stream.Length)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 2);
							defaultInterpolatedStringHandler.AppendLiteral("Read underflow ");
							defaultInterpolatedStringHandler.AppendFormatted<long>(stream.Length - stream.Position);
							defaultInterpolatedStringHandler.AppendLiteral(" of ");
							defaultInterpolatedStringHandler.AppendFormatted<long>(stream.Length - bitReaderEnd);
							defaultInterpolatedStringHandler.AppendLiteral(" bytes in ReceiveExtraAI, more info below");
							throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear());
						}
					}
					catch (Exception)
					{
						string str = "Error in ReceiveExtraAI for NPC ";
						ModNPC modNPC2 = npc.ModNPC;
						string message = str + (((modNPC2 != null) ? modNPC2.FullName : null) ?? npc.TypeName);
						if (lastGlobalNPC != null)
						{
							message += ", may be caused by one of these GlobalNPCs:";
							foreach (GlobalNPC g in NPCLoader.HookReceiveExtraAI.Enumerate(npc))
							{
								message = message + "\n\t" + g.FullName;
								if (lastGlobalNPC == g)
								{
									break;
								}
							}
						}
						Logging.tML.Error(message);
					}
				}
			}
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x004ECB94 File Offset: 0x004EAD94
		public static void FindFrame(NPC npc, int frameHeight)
		{
			bool isLikeATownNPC = npc.isLikeATownNPC;
			ModNPC modNPC = npc.ModNPC;
			npc.VanillaFindFrame(frameHeight, isLikeATownNPC, (((modNPC != null) ? new int?(modNPC.AnimationType) : null) > 0) ? npc.ModNPC.AnimationType : npc.type);
			ModNPC modNPC2 = npc.ModNPC;
			if (modNPC2 != null)
			{
				modNPC2.FindFrame(frameHeight);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookFindFrame.Enumerate(npc))
			{
				globalNPC.FindFrame(npc, frameHeight);
			}
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x004ECC34 File Offset: 0x004EAE34
		public static void HitEffect(NPC npc, in NPC.HitInfo hit)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.HitEffect(hit);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookHitEffect.Enumerate(npc))
			{
				globalNPC.HitEffect(npc, hit);
			}
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x004ECC8C File Offset: 0x004EAE8C
		public static void UpdateLifeRegen(NPC npc, ref int damage)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.UpdateLifeRegen(ref damage);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookUpdateLifeRegen.Enumerate(npc))
			{
				globalNPC.UpdateLifeRegen(npc, ref damage);
			}
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x004ECCD8 File Offset: 0x004EAED8
		public static bool CheckActive(NPC npc)
		{
			if (npc.ModNPC != null && !npc.ModNPC.CheckActive())
			{
				return false;
			}
			EntityGlobalsEnumerator<GlobalNPC> enumerator = NPCLoader.HookCheckActive.Enumerate(npc).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CheckActive(npc))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x004ECD30 File Offset: 0x004EAF30
		public static bool CheckDead(NPC npc)
		{
			bool result = true;
			if (npc.ModNPC != null)
			{
				result = npc.ModNPC.CheckDead();
			}
			foreach (GlobalNPC g in NPCLoader.HookCheckDead.Enumerate(npc))
			{
				result &= g.CheckDead(npc);
			}
			return result;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x004ECD88 File Offset: 0x004EAF88
		public static bool SpecialOnKill(NPC npc)
		{
			EntityGlobalsEnumerator<GlobalNPC> enumerator = NPCLoader.HookSpecialOnKill.Enumerate(npc).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.SpecialOnKill(npc))
				{
					return true;
				}
			}
			return npc.ModNPC != null && npc.ModNPC.SpecialOnKill();
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x004ECDDC File Offset: 0x004EAFDC
		public static bool PreKill(NPC npc)
		{
			bool result = true;
			foreach (GlobalNPC g in NPCLoader.HookPreKill.Enumerate(npc))
			{
				result &= g.PreKill(npc);
			}
			if (result && npc.ModNPC != null)
			{
				result = npc.ModNPC.PreKill();
			}
			if (!result)
			{
				NPCLoader.blockLoot.Clear();
				return false;
			}
			return true;
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x004ECE44 File Offset: 0x004EB044
		public static void OnKill(NPC npc)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.OnKill();
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookOnKill.Enumerate(npc))
			{
				globalNPC.OnKill(npc);
			}
			NPCLoader.blockLoot.Clear();
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x004ECE98 File Offset: 0x004EB098
		public static void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.ModifyNPCLoot(npcLoot);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookModifyNPCLoot.Enumerate(npc))
			{
				globalNPC.ModifyNPCLoot(npc, npcLoot);
			}
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x004ECEE4 File Offset: 0x004EB0E4
		public unsafe static void ModifyGlobalLoot(GlobalLoot globalLoot)
		{
			ReadOnlySpan<GlobalNPC> readOnlySpan = NPCLoader.HookModifyGlobalLoot.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ModifyGlobalLoot(globalLoot);
			}
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x004ECF1D File Offset: 0x004EB11D
		public static void BossLoot(NPC npc, ref string name, ref int potionType)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.BossLoot(ref name, ref potionType);
			}
			ModNPC modNPC2 = npc.ModNPC;
			if (modNPC2 == null)
			{
				return;
			}
			modNPC2.BossLoot(ref potionType);
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x004ECF44 File Offset: 0x004EB144
		public static bool? CanFallThroughPlatforms(NPC npc)
		{
			ModNPC modNPC = npc.ModNPC;
			bool? flag = (modNPC != null) ? modNPC.CanFallThroughPlatforms() : null;
			bool? ret = (flag != null) ? flag : null;
			if (ret != null)
			{
				if (!ret.Value)
				{
					return new bool?(false);
				}
				ret = new bool?(true);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookCanFallThroughPlatforms.Enumerate(npc))
			{
				bool? globalRet = globalNPC.CanFallThroughPlatforms(npc);
				if (globalRet != null)
				{
					if (!globalRet.Value)
					{
						return new bool?(false);
					}
					ret = new bool?(true);
				}
			}
			return ret;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x004ECFF4 File Offset: 0x004EB1F4
		public static bool? CanBeCaughtBy(NPC npc, Item item, Player player)
		{
			bool? canBeCaughtOverall = null;
			foreach (GlobalNPC globalNPC in NPCLoader.HookCanBeCaughtBy.Enumerate(npc))
			{
				bool? canBeCaughtFromGlobalNPC = globalNPC.CanBeCaughtBy(npc, item, player);
				if (canBeCaughtFromGlobalNPC != null)
				{
					if (!canBeCaughtFromGlobalNPC.Value)
					{
						return new bool?(false);
					}
					canBeCaughtOverall = new bool?(true);
				}
			}
			if (npc.ModNPC != null)
			{
				bool? canBeCaughtAsModNPC = npc.ModNPC.CanBeCaughtBy(item, player);
				if (canBeCaughtAsModNPC != null)
				{
					if (!canBeCaughtAsModNPC.Value)
					{
						return new bool?(false);
					}
					canBeCaughtOverall = new bool?(true);
				}
			}
			return canBeCaughtOverall;
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x004ED094 File Offset: 0x004EB294
		public static void OnCaughtBy(NPC npc, Player player, Item item, bool failed)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.OnCaughtBy(player, item, failed);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookOnCaughtBy.Enumerate(npc))
			{
				globalNPC.OnCaughtBy(npc, player, item, failed);
			}
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x004ED0E4 File Offset: 0x004EB2E4
		public static int? PickEmote(NPC npc, Player closestPlayer, List<int> emoteList, WorldUIAnchor anchor)
		{
			int? result = null;
			if (npc.ModNPC != null)
			{
				result = npc.ModNPC.PickEmote(closestPlayer, emoteList, anchor);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookPickEmote.Enumerate(npc))
			{
				int? emote = globalNPC.PickEmote(npc, closestPlayer, emoteList, anchor);
				if (emote != null)
				{
					result = emote;
				}
			}
			return result;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x004ED14C File Offset: 0x004EB34C
		public static bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
		{
			EntityGlobalsEnumerator<GlobalNPC> enumerator = NPCLoader.HookCanHitPlayer.Enumerate(npc).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanHitPlayer(npc, target, ref cooldownSlot))
				{
					return false;
				}
			}
			return npc.ModNPC == null || npc.ModNPC.CanHitPlayer(target, ref cooldownSlot);
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x004ED1A4 File Offset: 0x004EB3A4
		public static void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.ModifyHitPlayer(target, ref modifiers);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookModifyHitPlayer.Enumerate(npc))
			{
				globalNPC.ModifyHitPlayer(npc, target, ref modifiers);
			}
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x004ED1F4 File Offset: 0x004EB3F4
		public static void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.OnHitPlayer(target, hurtInfo);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookOnHitPlayer.Enumerate(npc))
			{
				globalNPC.OnHitPlayer(npc, target, hurtInfo);
			}
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x004ED244 File Offset: 0x004EB444
		public static bool CanHitNPC(NPC npc, NPC target)
		{
			EntityGlobalsEnumerator<GlobalNPC> enumerator = NPCLoader.HookCanHitNPC.Enumerate(npc).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanHitNPC(npc, target))
				{
					return false;
				}
			}
			enumerator = NPCLoader.HookCanBeHitByNPC.Enumerate(target).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.CanBeHitByNPC(target, npc))
				{
					return false;
				}
			}
			ModNPC modNPC = npc.ModNPC;
			if (!(((modNPC != null) ? new bool?(modNPC.CanHitNPC(target)) : null) ?? true))
			{
				return false;
			}
			ModNPC modNPC2 = target.ModNPC;
			return modNPC2 == null || modNPC2.CanBeHitByNPC(npc);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x004ED2FC File Offset: 0x004EB4FC
		public static void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.ModifyHitNPC(target, ref modifiers);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookModifyHitNPC.Enumerate(npc))
			{
				globalNPC.ModifyHitNPC(npc, target, ref modifiers);
			}
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x004ED34C File Offset: 0x004EB54C
		public static void OnHitNPC(NPC npc, NPC target, in NPC.HitInfo hit)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.OnHitNPC(target, hit);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookOnHitNPC.Enumerate(npc))
			{
				globalNPC.OnHitNPC(npc, target, hit);
			}
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x004ED3A4 File Offset: 0x004EB5A4
		public static bool? CanBeHitByItem(NPC npc, Player player, Item item)
		{
			bool? flag = null;
			foreach (GlobalNPC globalNPC in NPCLoader.HookCanBeHitByItem.Enumerate(npc))
			{
				bool? canHit = globalNPC.CanBeHitByItem(npc, player, item);
				if (canHit != null)
				{
					if (!canHit.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			if (npc.ModNPC != null)
			{
				bool? canHit2 = npc.ModNPC.CanBeHitByItem(player, item);
				if (canHit2 != null)
				{
					if (!canHit2.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			return flag;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x004ED444 File Offset: 0x004EB644
		public static bool? CanCollideWithPlayerMeleeAttack(NPC npc, Player player, Item item, Rectangle meleeAttackHitbox)
		{
			bool? flag = null;
			foreach (GlobalNPC globalNPC in NPCLoader.HookCanCollideWithPlayerMeleeAttack.Enumerate(npc))
			{
				bool? canCollide = globalNPC.CanCollideWithPlayerMeleeAttack(npc, player, item, meleeAttackHitbox);
				if (canCollide != null)
				{
					if (!canCollide.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			if (npc.ModNPC != null)
			{
				bool? canHit = npc.ModNPC.CanCollideWithPlayerMeleeAttack(player, item, meleeAttackHitbox);
				if (canHit != null)
				{
					if (!canHit.Value)
					{
						return new bool?(false);
					}
					flag = new bool?(true);
				}
			}
			return flag;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x004ED4E4 File Offset: 0x004EB6E4
		public static void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.ModifyHitByItem(player, item, ref modifiers);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookModifyHitByItem.Enumerate(npc))
			{
				globalNPC.ModifyHitByItem(npc, player, item, ref modifiers);
			}
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x004ED534 File Offset: 0x004EB734
		public static void OnHitByItem(NPC npc, Player player, Item item, in NPC.HitInfo hit, int damageDone)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.OnHitByItem(player, item, hit, damageDone);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookOnHitByItem.Enumerate(npc))
			{
				globalNPC.OnHitByItem(npc, player, item, hit, damageDone);
			}
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x004ED594 File Offset: 0x004EB794
		public static bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
		{
			bool? flag = null;
			foreach (GlobalNPC globalNPC in NPCLoader.HookCanBeHitByProjectile.Enumerate(npc))
			{
				bool? canHit = globalNPC.CanBeHitByProjectile(npc, projectile);
				if (canHit != null && !canHit.Value)
				{
					return new bool?(false);
				}
				if (canHit != null)
				{
					flag = new bool?(canHit.Value);
				}
			}
			if (npc.ModNPC != null)
			{
				bool? canHit2 = npc.ModNPC.CanBeHitByProjectile(projectile);
				if (canHit2 != null && !canHit2.Value)
				{
					return new bool?(false);
				}
				if (canHit2 != null)
				{
					flag = new bool?(canHit2.Value);
				}
			}
			return flag;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x004ED650 File Offset: 0x004EB850
		public static void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.ModifyHitByProjectile(projectile, ref modifiers);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookModifyHitByProjectile.Enumerate(npc))
			{
				globalNPC.ModifyHitByProjectile(npc, projectile, ref modifiers);
			}
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x004ED6A0 File Offset: 0x004EB8A0
		public static void OnHitByProjectile(NPC npc, Projectile projectile, in NPC.HitInfo hit, int damageDone)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.OnHitByProjectile(projectile, hit, damageDone);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookOnHitByProjectile.Enumerate(npc))
			{
				globalNPC.OnHitByProjectile(npc, projectile, hit, damageDone);
			}
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x004ED6FC File Offset: 0x004EB8FC
		public static void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.ModifyIncomingHit(ref modifiers);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookAddModifyIncomingHit.Enumerate(npc))
			{
				globalNPC.ModifyIncomingHit(npc, ref modifiers);
			}
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x004ED748 File Offset: 0x004EB948
		public static void BossHeadSlot(NPC npc, ref int index)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.BossHeadSlot(ref index);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookBossHeadSlot.Enumerate(npc))
			{
				globalNPC.BossHeadSlot(npc, ref index);
			}
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x004ED794 File Offset: 0x004EB994
		public static void BossHeadRotation(NPC npc, ref float rotation)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.BossHeadRotation(ref rotation);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookBossHeadRotation.Enumerate(npc))
			{
				globalNPC.BossHeadRotation(npc, ref rotation);
			}
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x004ED7E0 File Offset: 0x004EB9E0
		public static void BossHeadSpriteEffects(NPC npc, ref SpriteEffects spriteEffects)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.BossHeadSpriteEffects(ref spriteEffects);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookBossHeadSpriteEffects.Enumerate(npc))
			{
				globalNPC.BossHeadSpriteEffects(npc, ref spriteEffects);
			}
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x004ED82C File Offset: 0x004EBA2C
		public static Color? GetAlpha(NPC npc, Color lightColor)
		{
			foreach (GlobalNPC globalNPC in NPCLoader.HookGetAlpha.Enumerate(npc))
			{
				Color? color = globalNPC.GetAlpha(npc, lightColor);
				if (color != null)
				{
					return new Color?(color.Value);
				}
			}
			ModNPC modNPC = npc.ModNPC;
			if (modNPC == null)
			{
				return null;
			}
			return modNPC.GetAlpha(lightColor);
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x004ED898 File Offset: 0x004EBA98
		public static void DrawEffects(NPC npc, ref Color drawColor)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.DrawEffects(ref drawColor);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookDrawEffects.Enumerate(npc))
			{
				globalNPC.DrawEffects(npc, ref drawColor);
			}
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x004ED8E4 File Offset: 0x004EBAE4
		public static bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			bool result = true;
			foreach (GlobalNPC g in NPCLoader.HookPreDraw.Enumerate(npc))
			{
				result &= g.PreDraw(npc, spriteBatch, screenPos, drawColor);
			}
			if (result && npc.ModNPC != null)
			{
				return npc.ModNPC.PreDraw(spriteBatch, screenPos, drawColor);
			}
			return result;
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x004ED944 File Offset: 0x004EBB44
		public static void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.PostDraw(spriteBatch, screenPos, drawColor);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookPostDraw.Enumerate(npc))
			{
				globalNPC.PostDraw(npc, spriteBatch, screenPos, drawColor);
			}
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x004ED994 File Offset: 0x004EBB94
		internal static void DrawBehind(NPC npc, int index)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.DrawBehind(index);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookDrawBehind.Enumerate(npc))
			{
				globalNPC.DrawBehind(npc, index);
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x004ED9E0 File Offset: 0x004EBBE0
		public static bool DrawHealthBar(NPC npc, ref float scale)
		{
			Vector2 position;
			position..ctor(npc.position.X + (float)(npc.width / 2), npc.position.Y + npc.gfxOffY);
			if (Main.HealthBarDrawSettings == 1)
			{
				position.Y += (float)npc.height + 10f + Main.NPCAddHeight(npc);
			}
			else if (Main.HealthBarDrawSettings == 2)
			{
				position.Y -= 24f + Main.NPCAddHeight(npc) / 2f;
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookDrawHealthBar.Enumerate(npc))
			{
				bool? result = globalNPC.DrawHealthBar(npc, Main.HealthBarDrawSettings, ref scale, ref position);
				if (result != null)
				{
					if (result.Value)
					{
						NPCLoader.DrawHealthBar(npc, position, scale);
					}
					return false;
				}
			}
			if (NPCLoader.IsModNPC(npc))
			{
				bool? result2 = npc.ModNPC.DrawHealthBar(Main.HealthBarDrawSettings, ref scale, ref position);
				if (result2 != null)
				{
					if (result2.Value)
					{
						NPCLoader.DrawHealthBar(npc, position, scale);
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x004EDAF8 File Offset: 0x004EBCF8
		private static void DrawHealthBar(NPC npc, Vector2 position, float scale)
		{
			float alpha = Lighting.Brightness((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f));
			Main.instance.DrawHealthBar(position.X, position.Y, npc.life, npc.lifeMax, alpha, scale, false);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x004EDB54 File Offset: 0x004EBD54
		public unsafe static void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			ReadOnlySpan<GlobalNPC> readOnlySpan = NPCLoader.HookEditSpawnRate.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->EditSpawnRate(player, ref spawnRate, ref maxSpawns);
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x004EDB90 File Offset: 0x004EBD90
		public unsafe static void EditSpawnRange(Player player, ref int spawnRangeX, ref int spawnRangeY, ref int safeRangeX, ref int safeRangeY)
		{
			ReadOnlySpan<GlobalNPC> readOnlySpan = NPCLoader.HookEditSpawnRange.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->EditSpawnRange(player, ref spawnRangeX, ref spawnRangeY, ref safeRangeX, ref safeRangeY);
			}
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x004EDBD0 File Offset: 0x004EBDD0
		public unsafe static int? ChooseSpawn(NPCSpawnInfo spawnInfo)
		{
			NPCSpawnHelper.Reset();
			NPCSpawnHelper.DoChecks(spawnInfo);
			IDictionary<int, float> pool = new Dictionary<int, float>();
			pool[0] = 1f;
			foreach (ModNPC npc in NPCLoader.npcs)
			{
				float weight = npc.SpawnChance(spawnInfo);
				if (weight > 0f)
				{
					pool[npc.NPC.type] = weight;
				}
			}
			ReadOnlySpan<GlobalNPC> readOnlySpan = NPCLoader.HookEditSpawnPool.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->EditSpawnPool(pool, spawnInfo);
			}
			float totalWeight = 0f;
			foreach (int type in pool.Keys)
			{
				if (pool[type] < 0f)
				{
					pool[type] = 0f;
				}
				totalWeight += pool[type];
			}
			float choice = (float)Main.rand.NextDouble() * totalWeight;
			foreach (int type2 in pool.Keys)
			{
				float weight2 = pool[type2];
				if (choice < weight2)
				{
					return new int?(type2);
				}
				choice -= weight2;
			}
			return null;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x004EDD6C File Offset: 0x004EBF6C
		public static int SpawnNPC(int type, int tileX, int tileY)
		{
			int npc = (type >= (int)NPCID.Count) ? NPCLoader.GetNPC(type).SpawnNPC(tileX, tileY) : NPC.NewNPC(NPC.GetSpawnSourceForNaturalSpawn(), tileX * 16 + 8, tileY * 16, type, 0, 0f, 0f, 0f, 0f, 255);
			foreach (GlobalNPC globalNPC in NPCLoader.HookSpawnNPC.Enumerate(Main.npc[npc]))
			{
				globalNPC.SpawnNPC(npc, tileX, tileY);
			}
			return npc;
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x004EDDF8 File Offset: 0x004EBFF8
		public static void CanTownNPCSpawn(int numTownNPCs)
		{
			foreach (ModNPC modNPC in NPCLoader.npcs)
			{
				NPC npc = modNPC.NPC;
				if (npc.townNPC && NPC.TypeToDefaultHeadIndex(npc.type) >= 0 && !NPC.AnyNPCs(npc.type) && modNPC.CanTownNPCSpawn(numTownNPCs))
				{
					Main.townNPCCanSpawn[npc.type] = true;
					if (WorldGen.prioritizedTownNPCType == 0)
					{
						WorldGen.prioritizedTownNPCType = npc.type;
					}
				}
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x004EDE90 File Offset: 0x004EC090
		public static bool CheckConditions(int type)
		{
			ModNPC npc = NPCLoader.GetNPC(type);
			return npc == null || npc.CheckConditions(WorldGen.roomX1, WorldGen.roomX2, WorldGen.roomY1, WorldGen.roomY2);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x004EDEB8 File Offset: 0x004EC0B8
		public static string ModifyTypeName(NPC npc, string typeName)
		{
			if (npc.ModNPC != null)
			{
				npc.ModNPC.ModifyTypeName(ref typeName);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookModifyTypeName.Enumerate(npc))
			{
				globalNPC.ModifyTypeName(npc, ref typeName);
			}
			return typeName;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x004EDF0C File Offset: 0x004EC10C
		public static void ModifyHoverBoundingBox(NPC npc, ref Rectangle boundingBox)
		{
			if (npc.ModNPC != null)
			{
				npc.ModNPC.ModifyHoverBoundingBox(ref boundingBox);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookModifyHoverBoundingBox.Enumerate(npc))
			{
				globalNPC.ModifyHoverBoundingBox(npc, ref boundingBox);
			}
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x004EDF5C File Offset: 0x004EC15C
		public static List<string> ModifyNPCNameList(NPC npc, List<string> nameList)
		{
			if (npc.ModNPC != null)
			{
				nameList = npc.ModNPC.SetNPCNameList();
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookModifyNPCNameList.Enumerate(npc))
			{
				globalNPC.ModifyNPCNameList(npc, nameList);
			}
			return nameList;
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x004EDFAC File Offset: 0x004EC1AC
		public static bool UsesPartyHat(NPC npc)
		{
			ModNPC modNPC = npc.ModNPC;
			return modNPC == null || modNPC.UsesPartyHat();
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x004EDFC0 File Offset: 0x004EC1C0
		public static bool? CanChat(NPC npc)
		{
			ModNPC modNPC = npc.ModNPC;
			bool? ret = (modNPC != null) ? new bool?(modNPC.CanChat()) : null;
			foreach (GlobalNPC globalNPC in NPCLoader.HookCanChat.Enumerate(npc))
			{
				bool? flag = globalNPC.CanChat(npc);
				if (flag != null)
				{
					if (!flag.GetValueOrDefault())
					{
						return new bool?(false);
					}
					ret = new bool?(true);
				}
			}
			return ret;
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x004EE040 File Offset: 0x004EC240
		public static void GetChat(NPC npc, ref string chat)
		{
			if (npc.ModNPC != null)
			{
				chat = npc.ModNPC.GetChat();
			}
			else if (chat.Equals(""))
			{
				chat = Language.GetTextValue("tModLoader.DefaultTownNPCChat");
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookGetChat.Enumerate(npc))
			{
				globalNPC.GetChat(npc, ref chat);
			}
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x004EE0AB File Offset: 0x004EC2AB
		public static void SetChatButtons(ref string button, ref string button2)
		{
			NPC talkNPC = Main.LocalPlayer.TalkNPC;
			if (talkNPC == null)
			{
				return;
			}
			ModNPC modNPC = talkNPC.ModNPC;
			if (modNPC == null)
			{
				return;
			}
			modNPC.SetChatButtons(ref button, ref button2);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x004EE0D0 File Offset: 0x004EC2D0
		public static bool PreChatButtonClicked(bool firstButton)
		{
			NPC npc = Main.LocalPlayer.TalkNPC;
			bool result = true;
			foreach (GlobalNPC g in NPCLoader.HookPreChatButtonClicked.Enumerate(npc))
			{
				result &= g.PreChatButtonClicked(npc, firstButton);
			}
			if (!result)
			{
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
				return false;
			}
			return true;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x004EE13C File Offset: 0x004EC33C
		public static void OnChatButtonClicked(bool firstButton)
		{
			NPC npc = Main.LocalPlayer.TalkNPC;
			string shopName = null;
			if (npc.ModNPC != null)
			{
				npc.ModNPC.OnChatButtonClicked(firstButton, ref shopName);
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
				if (shopName != null)
				{
					Main.playerInventory = true;
					Main.stackSplit = 9999;
					Main.npcChatText = "";
					Main.SetNPCShopIndex(1);
					Main.instance.shop[Main.npcShop].SetupShop(NPCShopDatabase.GetShopName(npc.type, shopName), npc);
				}
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookOnChatButtonClicked.Enumerate(npc))
			{
				globalNPC.OnChatButtonClicked(npc, firstButton);
			}
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x004EE1F4 File Offset: 0x004EC3F4
		public static void AddShops(int type)
		{
			ModNPC npc = NPCLoader.GetNPC(type);
			if (npc == null)
			{
				return;
			}
			npc.AddShops();
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x004EE208 File Offset: 0x004EC408
		public unsafe static void ModifyShop(NPCShop shop)
		{
			ReadOnlySpan<GlobalNPC> readOnlySpan = NPCLoader.HookModifyShop.Enumerate(shop.NpcType);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->ModifyShop(shop);
			}
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x004EE248 File Offset: 0x004EC448
		public static void ModifyActiveShop(NPC npc, string shopName, Item[] shopContents)
		{
			ModNPC npc2 = NPCLoader.GetNPC(npc.type);
			if (npc2 != null)
			{
				npc2.ModifyActiveShop(shopName, shopContents);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookModifyActiveShop.Enumerate(npc))
			{
				globalNPC.ModifyActiveShop(npc, shopName, shopContents);
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x004EE29C File Offset: 0x004EC49C
		public unsafe static void SetupTravelShop(int[] shop, ref int nextSlot)
		{
			ReadOnlySpan<GlobalNPC> readOnlySpan = NPCLoader.HookSetupTravelShop.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->SetupTravelShop(shop, ref nextSlot);
			}
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x004EE2D8 File Offset: 0x004EC4D8
		public static bool? CanGoToStatue(NPC npc, bool toKingStatue)
		{
			ModNPC modNPC = npc.ModNPC;
			bool? ret = (modNPC != null) ? new bool?(modNPC.CanGoToStatue(toKingStatue)) : null;
			foreach (GlobalNPC globalNPC in NPCLoader.HookCanGoToStatue.Enumerate(npc))
			{
				bool? flag = globalNPC.CanGoToStatue(npc, toKingStatue);
				if (flag != null)
				{
					if (!flag.GetValueOrDefault())
					{
						return new bool?(false);
					}
					ret = new bool?(true);
				}
			}
			return ret;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x004EE35C File Offset: 0x004EC55C
		public static void OnGoToStatue(NPC npc, bool toKingStatue)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.OnGoToStatue(toKingStatue);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookOnGoToStatue.Enumerate(npc))
			{
				globalNPC.OnGoToStatue(npc, toKingStatue);
			}
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x004EE3A8 File Offset: 0x004EC5A8
		public unsafe static void BuffTownNPC(ref float damageMult, ref int defense)
		{
			ReadOnlySpan<GlobalNPC> readOnlySpan = NPCLoader.HookBuffTownNPC.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readOnlySpan[i]->BuffTownNPC(ref damageMult, ref defense);
			}
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x004EE3E4 File Offset: 0x004EC5E4
		public unsafe static bool ModifyDeathMessage(NPC npc, ref NetworkText customText, ref Color color)
		{
			ReadOnlySpan<GlobalNPC> readOnlySpan = NPCLoader.HookModifyDeathMessage.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				if (!readOnlySpan[i]->ModifyDeathMessage(npc, ref customText, ref color))
				{
					return true;
				}
			}
			ModNPC modNPC = npc.ModNPC;
			return modNPC != null && !modNPC.ModifyDeathMessage(ref customText, ref color);
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x004EE43C File Offset: 0x004EC63C
		public static void TownNPCAttackStrength(NPC npc, ref int damage, ref float knockback)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.TownNPCAttackStrength(ref damage, ref knockback);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookTownNPCAttackStrength.Enumerate(npc))
			{
				globalNPC.TownNPCAttackStrength(npc, ref damage, ref knockback);
			}
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x004EE48C File Offset: 0x004EC68C
		public static void TownNPCAttackCooldown(NPC npc, ref int cooldown, ref int randExtraCooldown)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.TownNPCAttackCooldown(ref cooldown, ref randExtraCooldown);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookTownNPCAttackCooldown.Enumerate(npc))
			{
				globalNPC.TownNPCAttackCooldown(npc, ref cooldown, ref randExtraCooldown);
			}
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x004EE4DC File Offset: 0x004EC6DC
		public static void TownNPCAttackProj(NPC npc, ref int projType, ref int attackDelay)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.TownNPCAttackProj(ref projType, ref attackDelay);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookTownNPCAttackProj.Enumerate(npc))
			{
				globalNPC.TownNPCAttackProj(npc, ref projType, ref attackDelay);
			}
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x004EE52C File Offset: 0x004EC72C
		public static void TownNPCAttackProjSpeed(NPC npc, ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.TownNPCAttackProjSpeed(ref multiplier, ref gravityCorrection, ref randomOffset);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookTownNPCAttackProjSpeed.Enumerate(npc))
			{
				globalNPC.TownNPCAttackProjSpeed(npc, ref multiplier, ref gravityCorrection, ref randomOffset);
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x004EE57C File Offset: 0x004EC77C
		public static void TownNPCAttackShoot(NPC npc, ref bool inBetweenShots)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.TownNPCAttackShoot(ref inBetweenShots);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookTownNPCAttackShoot.Enumerate(npc))
			{
				globalNPC.TownNPCAttackShoot(npc, ref inBetweenShots);
			}
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x004EE5C8 File Offset: 0x004EC7C8
		public static void TownNPCAttackMagic(NPC npc, ref float auraLightMultiplier)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.TownNPCAttackMagic(ref auraLightMultiplier);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookTownNPCAttackMagic.Enumerate(npc))
			{
				globalNPC.TownNPCAttackMagic(npc, ref auraLightMultiplier);
			}
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x004EE614 File Offset: 0x004EC814
		public static void TownNPCAttackSwing(NPC npc, ref int itemWidth, ref int itemHeight)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.TownNPCAttackSwing(ref itemWidth, ref itemHeight);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookTownNPCAttackSwing.Enumerate(npc))
			{
				globalNPC.TownNPCAttackSwing(npc, ref itemWidth, ref itemHeight);
			}
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x004EE664 File Offset: 0x004EC864
		public static void DrawTownAttackGun(NPC npc, ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.DrawTownAttackGun(ref item, ref itemFrame, ref scale, ref horizontalHoldoutOffset);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookDrawTownAttackGun.Enumerate(npc))
			{
				globalNPC.DrawTownAttackGun(npc, ref item, ref itemFrame, ref scale, ref horizontalHoldoutOffset);
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x004EE6B8 File Offset: 0x004EC8B8
		public static void DrawTownAttackSwing(NPC npc, ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.DrawTownAttackSwing(ref item, ref itemFrame, ref itemSize, ref scale, ref offset);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookDrawTownAttackSwing.Enumerate(npc))
			{
				globalNPC.DrawTownAttackSwing(npc, ref item, ref itemFrame, ref itemSize, ref scale, ref offset);
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x004EE710 File Offset: 0x004EC910
		public static bool ModifyCollisionData(NPC npc, Rectangle victimHitbox, ref int immunityCooldownSlot, ref float damageMultiplier, ref Rectangle npcHitbox)
		{
			MultipliableFloat damageMult = MultipliableFloat.One;
			bool result = true;
			foreach (GlobalNPC g in NPCLoader.HookModifyCollisionData.Enumerate(npc))
			{
				result &= g.ModifyCollisionData(npc, victimHitbox, ref immunityCooldownSlot, ref damageMult, ref npcHitbox);
			}
			if (result && npc.ModNPC != null)
			{
				result = npc.ModNPC.ModifyCollisionData(victimHitbox, ref immunityCooldownSlot, ref damageMult, ref npcHitbox);
			}
			damageMultiplier *= damageMult.Value;
			return result;
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x004EE788 File Offset: 0x004EC988
		public static bool SavesAndLoads(NPC npc)
		{
			if (npc.townNPC && npc.type != 368)
			{
				return true;
			}
			if (!NPCID.Sets.SavesAndLoads[npc.type])
			{
				ModNPC modNPC = npc.ModNPC;
				if (modNPC == null || !modNPC.NeedSaving())
				{
					EntityGlobalsEnumerator<GlobalNPC> enumerator = NPCLoader.HookNeedSaving.Enumerate(npc).GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.NeedSaving(npc))
						{
							return true;
						}
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x004EE804 File Offset: 0x004ECA04
		public static void ChatBubblePosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.ChatBubblePosition(ref position, ref spriteEffects);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookChatBubblePosition.Enumerate(npc))
			{
				globalNPC.ChatBubblePosition(npc, ref position, ref spriteEffects);
			}
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x004EE854 File Offset: 0x004ECA54
		public static void PartyHatPosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.PartyHatPosition(ref position, ref spriteEffects);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookPartyHatPosition.Enumerate(npc))
			{
				globalNPC.PartyHatPosition(npc, ref position, ref spriteEffects);
			}
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x004EE8A4 File Offset: 0x004ECAA4
		public static void EmoteBubblePosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects)
		{
			ModNPC modNPC = npc.ModNPC;
			if (modNPC != null)
			{
				modNPC.EmoteBubblePosition(ref position, ref spriteEffects);
			}
			foreach (GlobalNPC globalNPC in NPCLoader.HookEmoteBubblePosition.Enumerate(npc))
			{
				globalNPC.EmoteBubblePosition(npc, ref position, ref spriteEffects);
			}
		}

		// Token: 0x04001762 RID: 5986
		internal static readonly IList<ModNPC> npcs = new List<ModNPC>();

		// Token: 0x04001763 RID: 5987
		internal static readonly IDictionary<int, int> bannerToItem = new Dictionary<int, int>();

		// Token: 0x04001764 RID: 5988
		internal static readonly IDictionary<int, int> itemToBanner = new Dictionary<int, int>();

		/// <summary>
		/// Allows you to stop an NPC from dropping specific loot by adding item IDs to this list. This list will be cleared whenever NPCLoot ends. Useful for dynamically removing an item in the NPC's loot table. To remove an item drop use the <see cref="M:Terraria.ModLoader.ModNPC.PreKill" /> hook to add the item's ID to this list. Editing the drop rules themselves is usually the better and more compatible approach, however.
		/// </summary>
		// Token: 0x04001765 RID: 5989
		public static readonly IList<int> blockLoot = new List<int>();

		// Token: 0x04001766 RID: 5990
		private static readonly List<GlobalHookList<GlobalNPC>> hooks = new List<GlobalHookList<GlobalNPC>>();

		// Token: 0x04001767 RID: 5991
		private static readonly List<GlobalHookList<GlobalNPC>> modHooks = new List<GlobalHookList<GlobalNPC>>();

		// Token: 0x04001768 RID: 5992
		private static GlobalHookList<GlobalNPC> HookSetVariantDefaults = NPCLoader.AddHook<Action<NPC>>((GlobalNPC g) => (Action<NPC>)methodof(GlobalNPC.SetDefaultsFromNetId(NPC)).CreateDelegate(typeof(Action<NPC>), g));

		// Token: 0x04001769 RID: 5993
		private static GlobalHookList<GlobalNPC> HookOnSpawn = NPCLoader.AddHook<Action<NPC, IEntitySource>>((GlobalNPC g) => (Action<NPC, IEntitySource>)methodof(GlobalNPC.OnSpawn(NPC, IEntitySource)).CreateDelegate(typeof(Action<NPC, IEntitySource>), g));

		// Token: 0x0400176A RID: 5994
		private static GlobalHookList<GlobalNPC> HookApplyDifficultyAndPlayerScaling = NPCLoader.AddHook<Action<NPC, int, float, float>>((GlobalNPC g) => (Action<NPC, int, float, float>)methodof(GlobalNPC.ApplyDifficultyAndPlayerScaling(NPC, int, float, float)).CreateDelegate(typeof(Action<NPC, int, float, float>), g));

		// Token: 0x0400176B RID: 5995
		private static GlobalHookList<GlobalNPC> HookSetBestiary = NPCLoader.AddHook<NPCLoader.DelegateSetBestiary>((GlobalNPC g) => (NPCLoader.DelegateSetBestiary)methodof(GlobalNPC.SetBestiary(NPC, BestiaryDatabase, BestiaryEntry)).CreateDelegate(typeof(NPCLoader.DelegateSetBestiary), g));

		// Token: 0x0400176C RID: 5996
		private static GlobalHookList<GlobalNPC> HookModifyTownNPCProfile = NPCLoader.AddHook<NPCLoader.DelegateModifyTownNPCProfile>((GlobalNPC g) => (NPCLoader.DelegateModifyTownNPCProfile)methodof(GlobalNPC.ModifyTownNPCProfile(NPC)).CreateDelegate(typeof(NPCLoader.DelegateModifyTownNPCProfile), g));

		// Token: 0x0400176D RID: 5997
		private static GlobalHookList<GlobalNPC> HookResetEffects = NPCLoader.AddHook<Action<NPC>>((GlobalNPC g) => (Action<NPC>)methodof(GlobalNPC.ResetEffects(NPC)).CreateDelegate(typeof(Action<NPC>), g));

		// Token: 0x0400176E RID: 5998
		private static GlobalHookList<GlobalNPC> HookPreAI = NPCLoader.AddHook<Func<NPC, bool>>((GlobalNPC g) => (Func<NPC, bool>)methodof(GlobalNPC.PreAI(NPC)).CreateDelegate(typeof(Func<NPC, bool>), g));

		// Token: 0x0400176F RID: 5999
		private static GlobalHookList<GlobalNPC> HookAI = NPCLoader.AddHook<Action<NPC>>((GlobalNPC g) => (Action<NPC>)methodof(GlobalNPC.AI(NPC)).CreateDelegate(typeof(Action<NPC>), g));

		// Token: 0x04001770 RID: 6000
		private static GlobalHookList<GlobalNPC> HookPostAI = NPCLoader.AddHook<Action<NPC>>((GlobalNPC g) => (Action<NPC>)methodof(GlobalNPC.PostAI(NPC)).CreateDelegate(typeof(Action<NPC>), g));

		// Token: 0x04001771 RID: 6001
		private static GlobalHookList<GlobalNPC> HookSendExtraAI = NPCLoader.AddHook<Action<NPC, BitWriter, BinaryWriter>>((GlobalNPC g) => (Action<NPC, BitWriter, BinaryWriter>)methodof(GlobalNPC.SendExtraAI(NPC, BitWriter, BinaryWriter)).CreateDelegate(typeof(Action<NPC, BitWriter, BinaryWriter>), g));

		// Token: 0x04001772 RID: 6002
		private static GlobalHookList<GlobalNPC> HookReceiveExtraAI = NPCLoader.AddHook<Action<NPC, BitReader, BinaryReader>>((GlobalNPC g) => (Action<NPC, BitReader, BinaryReader>)methodof(GlobalNPC.ReceiveExtraAI(NPC, BitReader, BinaryReader)).CreateDelegate(typeof(Action<NPC, BitReader, BinaryReader>), g));

		// Token: 0x04001773 RID: 6003
		private static GlobalHookList<GlobalNPC> HookFindFrame = NPCLoader.AddHook<Action<NPC, int>>((GlobalNPC g) => (Action<NPC, int>)methodof(GlobalNPC.FindFrame(NPC, int)).CreateDelegate(typeof(Action<NPC, int>), g));

		// Token: 0x04001774 RID: 6004
		private static GlobalHookList<GlobalNPC> HookHitEffect = NPCLoader.AddHook<Action<NPC, NPC.HitInfo>>((GlobalNPC g) => (Action<NPC, NPC.HitInfo>)methodof(GlobalNPC.HitEffect(NPC, NPC.HitInfo)).CreateDelegate(typeof(Action<NPC, NPC.HitInfo>), g));

		// Token: 0x04001775 RID: 6005
		private static GlobalHookList<GlobalNPC> HookUpdateLifeRegen = NPCLoader.AddHook<NPCLoader.DelegateUpdateLifeRegen>((GlobalNPC g) => (NPCLoader.DelegateUpdateLifeRegen)methodof(GlobalNPC.UpdateLifeRegen(NPC, int*)).CreateDelegate(typeof(NPCLoader.DelegateUpdateLifeRegen), g));

		// Token: 0x04001776 RID: 6006
		private static GlobalHookList<GlobalNPC> HookCheckActive = NPCLoader.AddHook<Func<NPC, bool>>((GlobalNPC g) => (Func<NPC, bool>)methodof(GlobalNPC.CheckActive(NPC)).CreateDelegate(typeof(Func<NPC, bool>), g));

		// Token: 0x04001777 RID: 6007
		private static GlobalHookList<GlobalNPC> HookCheckDead = NPCLoader.AddHook<Func<NPC, bool>>((GlobalNPC g) => (Func<NPC, bool>)methodof(GlobalNPC.CheckDead(NPC)).CreateDelegate(typeof(Func<NPC, bool>), g));

		// Token: 0x04001778 RID: 6008
		private static GlobalHookList<GlobalNPC> HookSpecialOnKill = NPCLoader.AddHook<Func<NPC, bool>>((GlobalNPC g) => (Func<NPC, bool>)methodof(GlobalNPC.SpecialOnKill(NPC)).CreateDelegate(typeof(Func<NPC, bool>), g));

		// Token: 0x04001779 RID: 6009
		private static GlobalHookList<GlobalNPC> HookPreKill = NPCLoader.AddHook<Func<NPC, bool>>((GlobalNPC g) => (Func<NPC, bool>)methodof(GlobalNPC.PreKill(NPC)).CreateDelegate(typeof(Func<NPC, bool>), g));

		// Token: 0x0400177A RID: 6010
		private static GlobalHookList<GlobalNPC> HookOnKill = NPCLoader.AddHook<Action<NPC>>((GlobalNPC g) => (Action<NPC>)methodof(GlobalNPC.OnKill(NPC)).CreateDelegate(typeof(Action<NPC>), g));

		// Token: 0x0400177B RID: 6011
		private static GlobalHookList<GlobalNPC> HookModifyNPCLoot = NPCLoader.AddHook<Action<NPC, NPCLoot>>((GlobalNPC g) => (Action<NPC, NPCLoot>)methodof(GlobalNPC.ModifyNPCLoot(NPC, NPCLoot)).CreateDelegate(typeof(Action<NPC, NPCLoot>), g));

		// Token: 0x0400177C RID: 6012
		private static GlobalHookList<GlobalNPC> HookModifyGlobalLoot = NPCLoader.AddHook<Action<GlobalLoot>>((GlobalNPC g) => (Action<GlobalLoot>)methodof(GlobalNPC.ModifyGlobalLoot(GlobalLoot)).CreateDelegate(typeof(Action<GlobalLoot>), g));

		// Token: 0x0400177D RID: 6013
		private static GlobalHookList<GlobalNPC> HookCanFallThroughPlatforms = NPCLoader.AddHook<Func<NPC, bool?>>((GlobalNPC g) => (Func<NPC, bool?>)methodof(GlobalNPC.CanFallThroughPlatforms(NPC)).CreateDelegate(typeof(Func<NPC, bool?>), g));

		// Token: 0x0400177E RID: 6014
		private static GlobalHookList<GlobalNPC> HookCanBeCaughtBy = NPCLoader.AddHook<Func<NPC, Item, Player, bool?>>((GlobalNPC g) => (Func<NPC, Item, Player, bool?>)methodof(GlobalNPC.CanBeCaughtBy(NPC, Item, Player)).CreateDelegate(typeof(Func<NPC, Item, Player, bool?>), g));

		// Token: 0x0400177F RID: 6015
		private static GlobalHookList<GlobalNPC> HookOnCaughtBy = NPCLoader.AddHook<Action<NPC, Player, Item, bool>>((GlobalNPC g) => (Action<NPC, Player, Item, bool>)methodof(GlobalNPC.OnCaughtBy(NPC, Player, Item, bool)).CreateDelegate(typeof(Action<NPC, Player, Item, bool>), g));

		// Token: 0x04001780 RID: 6016
		private static GlobalHookList<GlobalNPC> HookPickEmote = NPCLoader.AddHook<Func<NPC, Player, List<int>, WorldUIAnchor, int?>>((GlobalNPC g) => (Func<NPC, Player, List<int>, WorldUIAnchor, int?>)methodof(GlobalNPC.PickEmote(NPC, Player, List<int>, WorldUIAnchor)).CreateDelegate(typeof(Func<NPC, Player, List<int>, WorldUIAnchor, int?>), g));

		// Token: 0x04001781 RID: 6017
		private static GlobalHookList<GlobalNPC> HookCanHitPlayer = NPCLoader.AddHook<NPCLoader.DelegateCanHitPlayer>((GlobalNPC g) => (NPCLoader.DelegateCanHitPlayer)methodof(GlobalNPC.CanHitPlayer(NPC, Player, int*)).CreateDelegate(typeof(NPCLoader.DelegateCanHitPlayer), g));

		// Token: 0x04001782 RID: 6018
		private static GlobalHookList<GlobalNPC> HookModifyHitPlayer = NPCLoader.AddHook<NPCLoader.DelegateModifyHitPlayer>((GlobalNPC g) => (NPCLoader.DelegateModifyHitPlayer)methodof(GlobalNPC.ModifyHitPlayer(NPC, Player, Player.HurtModifiers*)).CreateDelegate(typeof(NPCLoader.DelegateModifyHitPlayer), g));

		// Token: 0x04001783 RID: 6019
		private static GlobalHookList<GlobalNPC> HookOnHitPlayer = NPCLoader.AddHook<Action<NPC, Player, Player.HurtInfo>>((GlobalNPC g) => (Action<NPC, Player, Player.HurtInfo>)methodof(GlobalNPC.OnHitPlayer(NPC, Player, Player.HurtInfo)).CreateDelegate(typeof(Action<NPC, Player, Player.HurtInfo>), g));

		// Token: 0x04001784 RID: 6020
		private static GlobalHookList<GlobalNPC> HookCanHitNPC = NPCLoader.AddHook<Func<NPC, NPC, bool>>((GlobalNPC g) => (Func<NPC, NPC, bool>)methodof(GlobalNPC.CanHitNPC(NPC, NPC)).CreateDelegate(typeof(Func<NPC, NPC, bool>), g));

		// Token: 0x04001785 RID: 6021
		private static GlobalHookList<GlobalNPC> HookCanBeHitByNPC = NPCLoader.AddHook<Func<NPC, NPC, bool>>((GlobalNPC g) => (Func<NPC, NPC, bool>)methodof(GlobalNPC.CanBeHitByNPC(NPC, NPC)).CreateDelegate(typeof(Func<NPC, NPC, bool>), g));

		// Token: 0x04001786 RID: 6022
		private static GlobalHookList<GlobalNPC> HookModifyHitNPC = NPCLoader.AddHook<NPCLoader.DelegateModifyHitNPC>((GlobalNPC g) => (NPCLoader.DelegateModifyHitNPC)methodof(GlobalNPC.ModifyHitNPC(NPC, NPC, NPC.HitModifiers*)).CreateDelegate(typeof(NPCLoader.DelegateModifyHitNPC), g));

		// Token: 0x04001787 RID: 6023
		private static GlobalHookList<GlobalNPC> HookOnHitNPC = NPCLoader.AddHook<Action<NPC, NPC, NPC.HitInfo>>((GlobalNPC g) => (Action<NPC, NPC, NPC.HitInfo>)methodof(GlobalNPC.OnHitNPC(NPC, NPC, NPC.HitInfo)).CreateDelegate(typeof(Action<NPC, NPC, NPC.HitInfo>), g));

		// Token: 0x04001788 RID: 6024
		private static GlobalHookList<GlobalNPC> HookCanBeHitByItem = NPCLoader.AddHook<Func<NPC, Player, Item, bool?>>((GlobalNPC g) => (Func<NPC, Player, Item, bool?>)methodof(GlobalNPC.CanBeHitByItem(NPC, Player, Item)).CreateDelegate(typeof(Func<NPC, Player, Item, bool?>), g));

		// Token: 0x04001789 RID: 6025
		private static GlobalHookList<GlobalNPC> HookCanCollideWithPlayerMeleeAttack = NPCLoader.AddHook<Func<NPC, Player, Item, Rectangle, bool?>>((GlobalNPC g) => (Func<NPC, Player, Item, Rectangle, bool?>)methodof(GlobalNPC.CanCollideWithPlayerMeleeAttack(NPC, Player, Item, Rectangle)).CreateDelegate(typeof(Func<NPC, Player, Item, Rectangle, bool?>), g));

		// Token: 0x0400178A RID: 6026
		private static GlobalHookList<GlobalNPC> HookModifyHitByItem = NPCLoader.AddHook<NPCLoader.DelegateModifyHitByItem>((GlobalNPC g) => (NPCLoader.DelegateModifyHitByItem)methodof(GlobalNPC.ModifyHitByItem(NPC, Player, Item, NPC.HitModifiers*)).CreateDelegate(typeof(NPCLoader.DelegateModifyHitByItem), g));

		// Token: 0x0400178B RID: 6027
		private static GlobalHookList<GlobalNPC> HookOnHitByItem = NPCLoader.AddHook<Action<NPC, Player, Item, NPC.HitInfo, int>>((GlobalNPC g) => (Action<NPC, Player, Item, NPC.HitInfo, int>)methodof(GlobalNPC.OnHitByItem(NPC, Player, Item, NPC.HitInfo, int)).CreateDelegate(typeof(Action<NPC, Player, Item, NPC.HitInfo, int>), g));

		// Token: 0x0400178C RID: 6028
		private static GlobalHookList<GlobalNPC> HookCanBeHitByProjectile = NPCLoader.AddHook<Func<NPC, Projectile, bool?>>((GlobalNPC g) => (Func<NPC, Projectile, bool?>)methodof(GlobalNPC.CanBeHitByProjectile(NPC, Projectile)).CreateDelegate(typeof(Func<NPC, Projectile, bool?>), g));

		// Token: 0x0400178D RID: 6029
		private static GlobalHookList<GlobalNPC> HookModifyHitByProjectile = NPCLoader.AddHook<NPCLoader.DelegateModifyHitByProjectile>((GlobalNPC g) => (NPCLoader.DelegateModifyHitByProjectile)methodof(GlobalNPC.ModifyHitByProjectile(NPC, Projectile, NPC.HitModifiers*)).CreateDelegate(typeof(NPCLoader.DelegateModifyHitByProjectile), g));

		// Token: 0x0400178E RID: 6030
		private static GlobalHookList<GlobalNPC> HookOnHitByProjectile = NPCLoader.AddHook<Action<NPC, Projectile, NPC.HitInfo, int>>((GlobalNPC g) => (Action<NPC, Projectile, NPC.HitInfo, int>)methodof(GlobalNPC.OnHitByProjectile(NPC, Projectile, NPC.HitInfo, int)).CreateDelegate(typeof(Action<NPC, Projectile, NPC.HitInfo, int>), g));

		// Token: 0x0400178F RID: 6031
		private static GlobalHookList<GlobalNPC> HookAddModifyIncomingHit = NPCLoader.AddHook<NPCLoader.DelegateAddModifyIncomingHit>((GlobalNPC g) => (NPCLoader.DelegateAddModifyIncomingHit)methodof(GlobalNPC.ModifyIncomingHit(NPC, NPC.HitModifiers*)).CreateDelegate(typeof(NPCLoader.DelegateAddModifyIncomingHit), g));

		// Token: 0x04001790 RID: 6032
		private static GlobalHookList<GlobalNPC> HookBossHeadSlot = NPCLoader.AddHook<NPCLoader.DelegateBossHeadSlot>((GlobalNPC g) => (NPCLoader.DelegateBossHeadSlot)methodof(GlobalNPC.BossHeadSlot(NPC, int*)).CreateDelegate(typeof(NPCLoader.DelegateBossHeadSlot), g));

		// Token: 0x04001791 RID: 6033
		private static GlobalHookList<GlobalNPC> HookBossHeadRotation = NPCLoader.AddHook<NPCLoader.DelegateBossHeadRotation>((GlobalNPC g) => (NPCLoader.DelegateBossHeadRotation)methodof(GlobalNPC.BossHeadRotation(NPC, float*)).CreateDelegate(typeof(NPCLoader.DelegateBossHeadRotation), g));

		// Token: 0x04001792 RID: 6034
		private static GlobalHookList<GlobalNPC> HookBossHeadSpriteEffects = NPCLoader.AddHook<NPCLoader.DelegateBossHeadSpriteEffects>((GlobalNPC g) => (NPCLoader.DelegateBossHeadSpriteEffects)methodof(GlobalNPC.BossHeadSpriteEffects(NPC, SpriteEffects*)).CreateDelegate(typeof(NPCLoader.DelegateBossHeadSpriteEffects), g));

		// Token: 0x04001793 RID: 6035
		private static GlobalHookList<GlobalNPC> HookGetAlpha = NPCLoader.AddHook<Func<NPC, Color, Color?>>((GlobalNPC g) => (Func<NPC, Color, Color?>)methodof(GlobalNPC.GetAlpha(NPC, Color)).CreateDelegate(typeof(Func<NPC, Color, Color?>), g));

		// Token: 0x04001794 RID: 6036
		private static GlobalHookList<GlobalNPC> HookDrawEffects = NPCLoader.AddHook<NPCLoader.DelegateDrawEffects>((GlobalNPC g) => (NPCLoader.DelegateDrawEffects)methodof(GlobalNPC.DrawEffects(NPC, Color*)).CreateDelegate(typeof(NPCLoader.DelegateDrawEffects), g));

		// Token: 0x04001795 RID: 6037
		private static GlobalHookList<GlobalNPC> HookPreDraw = NPCLoader.AddHook<NPCLoader.DelegatePreDraw>((GlobalNPC g) => (NPCLoader.DelegatePreDraw)methodof(GlobalNPC.PreDraw(NPC, SpriteBatch, Vector2, Color)).CreateDelegate(typeof(NPCLoader.DelegatePreDraw), g));

		// Token: 0x04001796 RID: 6038
		private static GlobalHookList<GlobalNPC> HookPostDraw = NPCLoader.AddHook<NPCLoader.DelegatePostDraw>((GlobalNPC g) => (NPCLoader.DelegatePostDraw)methodof(GlobalNPC.PostDraw(NPC, SpriteBatch, Vector2, Color)).CreateDelegate(typeof(NPCLoader.DelegatePostDraw), g));

		// Token: 0x04001797 RID: 6039
		private static GlobalHookList<GlobalNPC> HookDrawBehind = NPCLoader.AddHook<Action<NPC, int>>((GlobalNPC g) => (Action<NPC, int>)methodof(GlobalNPC.DrawBehind(NPC, int)).CreateDelegate(typeof(Action<NPC, int>), g));

		// Token: 0x04001798 RID: 6040
		private static GlobalHookList<GlobalNPC> HookDrawHealthBar = NPCLoader.AddHook<NPCLoader.DelegateDrawHealthBar>((GlobalNPC g) => (NPCLoader.DelegateDrawHealthBar)methodof(GlobalNPC.DrawHealthBar(NPC, byte, float*, Vector2*)).CreateDelegate(typeof(NPCLoader.DelegateDrawHealthBar), g));

		// Token: 0x04001799 RID: 6041
		private static GlobalHookList<GlobalNPC> HookEditSpawnRate = NPCLoader.AddHook<NPCLoader.DelegateEditSpawnRate>((GlobalNPC g) => (NPCLoader.DelegateEditSpawnRate)methodof(GlobalNPC.EditSpawnRate(Player, int*, int*)).CreateDelegate(typeof(NPCLoader.DelegateEditSpawnRate), g));

		// Token: 0x0400179A RID: 6042
		private static GlobalHookList<GlobalNPC> HookEditSpawnRange = NPCLoader.AddHook<NPCLoader.DelegateEditSpawnRange>((GlobalNPC g) => (NPCLoader.DelegateEditSpawnRange)methodof(GlobalNPC.EditSpawnRange(Player, int*, int*, int*, int*)).CreateDelegate(typeof(NPCLoader.DelegateEditSpawnRange), g));

		// Token: 0x0400179B RID: 6043
		private static GlobalHookList<GlobalNPC> HookEditSpawnPool = NPCLoader.AddHook<Action<Dictionary<int, float>, NPCSpawnInfo>>((GlobalNPC g) => (Action<Dictionary<int, float>, NPCSpawnInfo>)methodof(GlobalNPC.EditSpawnPool(IDictionary<int, float>, NPCSpawnInfo)).CreateDelegate(typeof(Action<Dictionary<int, float>, NPCSpawnInfo>), g));

		// Token: 0x0400179C RID: 6044
		private static GlobalHookList<GlobalNPC> HookSpawnNPC = NPCLoader.AddHook<Action<int, int, int>>((GlobalNPC g) => (Action<int, int, int>)methodof(GlobalNPC.SpawnNPC(int, int, int)).CreateDelegate(typeof(Action<int, int, int>), g));

		// Token: 0x0400179D RID: 6045
		private static GlobalHookList<GlobalNPC> HookModifyTypeName = NPCLoader.AddHook<NPCLoader.DelegateModifyTypeName>((GlobalNPC g) => (NPCLoader.DelegateModifyTypeName)methodof(GlobalNPC.ModifyTypeName(NPC, string*)).CreateDelegate(typeof(NPCLoader.DelegateModifyTypeName), g));

		// Token: 0x0400179E RID: 6046
		private static GlobalHookList<GlobalNPC> HookModifyHoverBoundingBox = NPCLoader.AddHook<NPCLoader.DelegateModifyHoverBoundingBox>((GlobalNPC g) => (NPCLoader.DelegateModifyHoverBoundingBox)methodof(GlobalNPC.ModifyHoverBoundingBox(NPC, Rectangle*)).CreateDelegate(typeof(NPCLoader.DelegateModifyHoverBoundingBox), g));

		// Token: 0x0400179F RID: 6047
		private static GlobalHookList<GlobalNPC> HookModifyNPCNameList = NPCLoader.AddHook<Action<NPC, List<string>>>((GlobalNPC g) => (Action<NPC, List<string>>)methodof(GlobalNPC.ModifyNPCNameList(NPC, List<string>)).CreateDelegate(typeof(Action<NPC, List<string>>), g));

		// Token: 0x040017A0 RID: 6048
		private static GlobalHookList<GlobalNPC> HookCanChat = NPCLoader.AddHook<Func<NPC, bool?>>((GlobalNPC g) => (Func<NPC, bool?>)methodof(GlobalNPC.CanChat(NPC)).CreateDelegate(typeof(Func<NPC, bool?>), g));

		// Token: 0x040017A1 RID: 6049
		private static GlobalHookList<GlobalNPC> HookGetChat = NPCLoader.AddHook<NPCLoader.DelegateGetChat>((GlobalNPC g) => (NPCLoader.DelegateGetChat)methodof(GlobalNPC.GetChat(NPC, string*)).CreateDelegate(typeof(NPCLoader.DelegateGetChat), g));

		// Token: 0x040017A2 RID: 6050
		private static GlobalHookList<GlobalNPC> HookPreChatButtonClicked = NPCLoader.AddHook<Func<NPC, bool, bool>>((GlobalNPC g) => (Func<NPC, bool, bool>)methodof(GlobalNPC.PreChatButtonClicked(NPC, bool)).CreateDelegate(typeof(Func<NPC, bool, bool>), g));

		// Token: 0x040017A3 RID: 6051
		private static GlobalHookList<GlobalNPC> HookOnChatButtonClicked = NPCLoader.AddHook<NPCLoader.DelegateOnChatButtonClicked>((GlobalNPC g) => (NPCLoader.DelegateOnChatButtonClicked)methodof(GlobalNPC.OnChatButtonClicked(NPC, bool)).CreateDelegate(typeof(NPCLoader.DelegateOnChatButtonClicked), g));

		// Token: 0x040017A4 RID: 6052
		private static GlobalHookList<GlobalNPC> HookModifyShop = NPCLoader.AddHook<NPCLoader.DelegateModifyShop>((GlobalNPC g) => (NPCLoader.DelegateModifyShop)methodof(GlobalNPC.ModifyShop(NPCShop)).CreateDelegate(typeof(NPCLoader.DelegateModifyShop), g));

		// Token: 0x040017A5 RID: 6053
		private static GlobalHookList<GlobalNPC> HookModifyActiveShop = NPCLoader.AddHook<NPCLoader.DelegateModifyActiveShop>((GlobalNPC g) => (NPCLoader.DelegateModifyActiveShop)methodof(GlobalNPC.ModifyActiveShop(NPC, string, Item[])).CreateDelegate(typeof(NPCLoader.DelegateModifyActiveShop), g));

		// Token: 0x040017A6 RID: 6054
		private static GlobalHookList<GlobalNPC> HookSetupTravelShop = NPCLoader.AddHook<NPCLoader.DelegateSetupTravelShop>((GlobalNPC g) => (NPCLoader.DelegateSetupTravelShop)methodof(GlobalNPC.SetupTravelShop(int[], int*)).CreateDelegate(typeof(NPCLoader.DelegateSetupTravelShop), g));

		// Token: 0x040017A7 RID: 6055
		private static GlobalHookList<GlobalNPC> HookCanGoToStatue = NPCLoader.AddHook<Func<NPC, bool, bool?>>((GlobalNPC g) => (Func<NPC, bool, bool?>)methodof(GlobalNPC.CanGoToStatue(NPC, bool)).CreateDelegate(typeof(Func<NPC, bool, bool?>), g));

		// Token: 0x040017A8 RID: 6056
		private static GlobalHookList<GlobalNPC> HookOnGoToStatue = NPCLoader.AddHook<Action<NPC, bool>>((GlobalNPC g) => (Action<NPC, bool>)methodof(GlobalNPC.OnGoToStatue(NPC, bool)).CreateDelegate(typeof(Action<NPC, bool>), g));

		// Token: 0x040017A9 RID: 6057
		private static GlobalHookList<GlobalNPC> HookBuffTownNPC = NPCLoader.AddHook<NPCLoader.DelegateBuffTownNPC>((GlobalNPC g) => (NPCLoader.DelegateBuffTownNPC)methodof(GlobalNPC.BuffTownNPC(float*, int*)).CreateDelegate(typeof(NPCLoader.DelegateBuffTownNPC), g));

		// Token: 0x040017AA RID: 6058
		private static GlobalHookList<GlobalNPC> HookModifyDeathMessage = NPCLoader.AddHook<NPCLoader.DelegateModifyDeathMessage>((GlobalNPC g) => (NPCLoader.DelegateModifyDeathMessage)methodof(GlobalNPC.ModifyDeathMessage(NPC, NetworkText*, Color*)).CreateDelegate(typeof(NPCLoader.DelegateModifyDeathMessage), g));

		// Token: 0x040017AB RID: 6059
		private static GlobalHookList<GlobalNPC> HookTownNPCAttackStrength = NPCLoader.AddHook<NPCLoader.DelegateTownNPCAttackStrength>((GlobalNPC g) => (NPCLoader.DelegateTownNPCAttackStrength)methodof(GlobalNPC.TownNPCAttackStrength(NPC, int*, float*)).CreateDelegate(typeof(NPCLoader.DelegateTownNPCAttackStrength), g));

		// Token: 0x040017AC RID: 6060
		private static GlobalHookList<GlobalNPC> HookTownNPCAttackCooldown = NPCLoader.AddHook<NPCLoader.DelegateTownNPCAttackCooldown>((GlobalNPC g) => (NPCLoader.DelegateTownNPCAttackCooldown)methodof(GlobalNPC.TownNPCAttackCooldown(NPC, int*, int*)).CreateDelegate(typeof(NPCLoader.DelegateTownNPCAttackCooldown), g));

		// Token: 0x040017AD RID: 6061
		private static GlobalHookList<GlobalNPC> HookTownNPCAttackProj = NPCLoader.AddHook<NPCLoader.DelegateTownNPCAttackProj>((GlobalNPC g) => (NPCLoader.DelegateTownNPCAttackProj)methodof(GlobalNPC.TownNPCAttackProj(NPC, int*, int*)).CreateDelegate(typeof(NPCLoader.DelegateTownNPCAttackProj), g));

		// Token: 0x040017AE RID: 6062
		private static GlobalHookList<GlobalNPC> HookTownNPCAttackProjSpeed = NPCLoader.AddHook<NPCLoader.DelegateTownNPCAttackProjSpeed>((GlobalNPC g) => (NPCLoader.DelegateTownNPCAttackProjSpeed)methodof(GlobalNPC.TownNPCAttackProjSpeed(NPC, float*, float*, float*)).CreateDelegate(typeof(NPCLoader.DelegateTownNPCAttackProjSpeed), g));

		// Token: 0x040017AF RID: 6063
		private static GlobalHookList<GlobalNPC> HookTownNPCAttackShoot = NPCLoader.AddHook<NPCLoader.DelegateTownNPCAttackShoot>((GlobalNPC g) => (NPCLoader.DelegateTownNPCAttackShoot)methodof(GlobalNPC.TownNPCAttackShoot(NPC, bool*)).CreateDelegate(typeof(NPCLoader.DelegateTownNPCAttackShoot), g));

		// Token: 0x040017B0 RID: 6064
		private static GlobalHookList<GlobalNPC> HookTownNPCAttackMagic = NPCLoader.AddHook<NPCLoader.DelegateTownNPCAttackMagic>((GlobalNPC g) => (NPCLoader.DelegateTownNPCAttackMagic)methodof(GlobalNPC.TownNPCAttackMagic(NPC, float*)).CreateDelegate(typeof(NPCLoader.DelegateTownNPCAttackMagic), g));

		// Token: 0x040017B1 RID: 6065
		private static GlobalHookList<GlobalNPC> HookTownNPCAttackSwing = NPCLoader.AddHook<NPCLoader.DelegateTownNPCAttackSwing>((GlobalNPC g) => (NPCLoader.DelegateTownNPCAttackSwing)methodof(GlobalNPC.TownNPCAttackSwing(NPC, int*, int*)).CreateDelegate(typeof(NPCLoader.DelegateTownNPCAttackSwing), g));

		// Token: 0x040017B2 RID: 6066
		private static GlobalHookList<GlobalNPC> HookDrawTownAttackGun = NPCLoader.AddHook<NPCLoader.DelegateDrawTownAttackGun>((GlobalNPC g) => (NPCLoader.DelegateDrawTownAttackGun)methodof(GlobalNPC.DrawTownAttackGun(NPC, Texture2D*, Rectangle*, float*, int*)).CreateDelegate(typeof(NPCLoader.DelegateDrawTownAttackGun), g));

		// Token: 0x040017B3 RID: 6067
		private static GlobalHookList<GlobalNPC> HookDrawTownAttackSwing = NPCLoader.AddHook<NPCLoader.DelegateDrawTownAttackSwing>((GlobalNPC g) => (NPCLoader.DelegateDrawTownAttackSwing)methodof(GlobalNPC.DrawTownAttackSwing(NPC, Texture2D*, Rectangle*, int*, float*, Vector2*)).CreateDelegate(typeof(NPCLoader.DelegateDrawTownAttackSwing), g));

		// Token: 0x040017B4 RID: 6068
		private static GlobalHookList<GlobalNPC> HookModifyCollisionData = NPCLoader.AddHook<NPCLoader.DelegateModifyCollisionData>((GlobalNPC g) => (NPCLoader.DelegateModifyCollisionData)methodof(GlobalNPC.ModifyCollisionData(NPC, Rectangle, int*, MultipliableFloat*, Rectangle*)).CreateDelegate(typeof(NPCLoader.DelegateModifyCollisionData), g));

		// Token: 0x040017B5 RID: 6069
		private static GlobalHookList<GlobalNPC> HookNeedSaving = NPCLoader.AddHook<NPCLoader.DelegateNeedSaving>((GlobalNPC g) => (NPCLoader.DelegateNeedSaving)methodof(GlobalNPC.NeedSaving(NPC)).CreateDelegate(typeof(NPCLoader.DelegateNeedSaving), g));

		// Token: 0x040017B6 RID: 6070
		internal static GlobalHookList<GlobalNPC> HookSaveData = NPCLoader.AddHook<Action<NPC, TagCompound>>((GlobalNPC g) => (Action<NPC, TagCompound>)methodof(GlobalNPC.SaveData(NPC, TagCompound)).CreateDelegate(typeof(Action<NPC, TagCompound>), g));

		// Token: 0x040017B7 RID: 6071
		private static GlobalHookList<GlobalNPC> HookChatBubblePosition = NPCLoader.AddHook<NPCLoader.DelegateChatBubblePosition>((GlobalNPC g) => (NPCLoader.DelegateChatBubblePosition)methodof(GlobalNPC.ChatBubblePosition(NPC, Vector2*, SpriteEffects*)).CreateDelegate(typeof(NPCLoader.DelegateChatBubblePosition), g));

		// Token: 0x040017B8 RID: 6072
		private static GlobalHookList<GlobalNPC> HookPartyHatPosition = NPCLoader.AddHook<NPCLoader.DelegatePartyHatPosition>((GlobalNPC g) => (NPCLoader.DelegatePartyHatPosition)methodof(GlobalNPC.PartyHatPosition(NPC, Vector2*, SpriteEffects*)).CreateDelegate(typeof(NPCLoader.DelegatePartyHatPosition), g));

		// Token: 0x040017B9 RID: 6073
		private static GlobalHookList<GlobalNPC> HookEmoteBubblePosition = NPCLoader.AddHook<NPCLoader.DelegateEmoteBubblePosition>((GlobalNPC g) => (NPCLoader.DelegateEmoteBubblePosition)methodof(GlobalNPC.EmoteBubblePosition(NPC, Vector2*, SpriteEffects*)).CreateDelegate(typeof(NPCLoader.DelegateEmoteBubblePosition), g));

		// Token: 0x0200094C RID: 2380
		// (Invoke) Token: 0x06005427 RID: 21543
		private delegate void DelegateSetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry);

		// Token: 0x0200094D RID: 2381
		// (Invoke) Token: 0x0600542B RID: 21547
		private delegate ITownNPCProfile DelegateModifyTownNPCProfile(NPC npc);

		// Token: 0x0200094E RID: 2382
		// (Invoke) Token: 0x0600542F RID: 21551
		private delegate void DelegateUpdateLifeRegen(NPC npc, ref int damage);

		// Token: 0x0200094F RID: 2383
		// (Invoke) Token: 0x06005433 RID: 21555
		private delegate bool DelegateCanHitPlayer(NPC npc, Player target, ref int cooldownSlot);

		// Token: 0x02000950 RID: 2384
		// (Invoke) Token: 0x06005437 RID: 21559
		private delegate void DelegateModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers);

		// Token: 0x02000951 RID: 2385
		// (Invoke) Token: 0x0600543B RID: 21563
		private delegate void DelegateModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers);

		// Token: 0x02000952 RID: 2386
		// (Invoke) Token: 0x0600543F RID: 21567
		private delegate void DelegateModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers);

		// Token: 0x02000953 RID: 2387
		// (Invoke) Token: 0x06005443 RID: 21571
		private delegate void DelegateModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers);

		// Token: 0x02000954 RID: 2388
		// (Invoke) Token: 0x06005447 RID: 21575
		private delegate void DelegateAddModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers);

		// Token: 0x02000955 RID: 2389
		// (Invoke) Token: 0x0600544B RID: 21579
		private delegate void DelegateBossHeadSlot(NPC npc, ref int index);

		// Token: 0x02000956 RID: 2390
		// (Invoke) Token: 0x0600544F RID: 21583
		private delegate void DelegateBossHeadRotation(NPC npc, ref float rotation);

		// Token: 0x02000957 RID: 2391
		// (Invoke) Token: 0x06005453 RID: 21587
		private delegate void DelegateBossHeadSpriteEffects(NPC npc, ref SpriteEffects spriteEffects);

		// Token: 0x02000958 RID: 2392
		// (Invoke) Token: 0x06005457 RID: 21591
		private delegate void DelegateDrawEffects(NPC npc, ref Color drawColor);

		// Token: 0x02000959 RID: 2393
		// (Invoke) Token: 0x0600545B RID: 21595
		private delegate bool DelegatePreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor);

		// Token: 0x0200095A RID: 2394
		// (Invoke) Token: 0x0600545F RID: 21599
		private delegate void DelegatePostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor);

		// Token: 0x0200095B RID: 2395
		// (Invoke) Token: 0x06005463 RID: 21603
		private delegate bool? DelegateDrawHealthBar(NPC npc, byte bhPosition, ref float scale, ref Vector2 position);

		// Token: 0x0200095C RID: 2396
		// (Invoke) Token: 0x06005467 RID: 21607
		private delegate void DelegateEditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns);

		// Token: 0x0200095D RID: 2397
		// (Invoke) Token: 0x0600546B RID: 21611
		private delegate void DelegateEditSpawnRange(Player player, ref int spawnRangeX, ref int spawnRangeY, ref int safeRangeX, ref int safeRangeY);

		// Token: 0x0200095E RID: 2398
		// (Invoke) Token: 0x0600546F RID: 21615
		private delegate void DelegateModifyTypeName(NPC npc, ref string typeName);

		// Token: 0x0200095F RID: 2399
		// (Invoke) Token: 0x06005473 RID: 21619
		private delegate void DelegateModifyHoverBoundingBox(NPC npc, ref Rectangle boundingBox);

		// Token: 0x02000960 RID: 2400
		// (Invoke) Token: 0x06005477 RID: 21623
		private delegate void DelegateGetChat(NPC npc, ref string chat);

		// Token: 0x02000961 RID: 2401
		// (Invoke) Token: 0x0600547B RID: 21627
		private delegate void DelegateOnChatButtonClicked(NPC npc, bool firstButton);

		// Token: 0x02000962 RID: 2402
		// (Invoke) Token: 0x0600547F RID: 21631
		private delegate void DelegateModifyShop(NPCShop shop);

		// Token: 0x02000963 RID: 2403
		// (Invoke) Token: 0x06005483 RID: 21635
		private delegate void DelegateModifyActiveShop(NPC npc, string shopName, Item[] items);

		// Token: 0x02000964 RID: 2404
		// (Invoke) Token: 0x06005487 RID: 21639
		private delegate void DelegateSetupTravelShop(int[] shop, ref int nextSlot);

		// Token: 0x02000965 RID: 2405
		// (Invoke) Token: 0x0600548B RID: 21643
		private delegate void DelegateBuffTownNPC(ref float damageMult, ref int defense);

		// Token: 0x02000966 RID: 2406
		// (Invoke) Token: 0x0600548F RID: 21647
		private delegate bool DelegateModifyDeathMessage(NPC npc, ref NetworkText custom, ref Color color);

		// Token: 0x02000967 RID: 2407
		// (Invoke) Token: 0x06005493 RID: 21651
		private delegate void DelegateTownNPCAttackStrength(NPC npc, ref int damage, ref float knockback);

		// Token: 0x02000968 RID: 2408
		// (Invoke) Token: 0x06005497 RID: 21655
		private delegate void DelegateTownNPCAttackCooldown(NPC npc, ref int cooldown, ref int randExtraCooldown);

		// Token: 0x02000969 RID: 2409
		// (Invoke) Token: 0x0600549B RID: 21659
		private delegate void DelegateTownNPCAttackProj(NPC npc, ref int projType, ref int attackDelay);

		// Token: 0x0200096A RID: 2410
		// (Invoke) Token: 0x0600549F RID: 21663
		private delegate void DelegateTownNPCAttackProjSpeed(NPC npc, ref float multiplier, ref float gravityCorrection, ref float randomOffset);

		// Token: 0x0200096B RID: 2411
		// (Invoke) Token: 0x060054A3 RID: 21667
		private delegate void DelegateTownNPCAttackShoot(NPC npc, ref bool inBetweenShots);

		// Token: 0x0200096C RID: 2412
		// (Invoke) Token: 0x060054A7 RID: 21671
		private delegate void DelegateTownNPCAttackMagic(NPC npc, ref float auraLightMultiplier);

		// Token: 0x0200096D RID: 2413
		// (Invoke) Token: 0x060054AB RID: 21675
		private delegate void DelegateTownNPCAttackSwing(NPC npc, ref int itemWidth, ref int itemHeight);

		// Token: 0x0200096E RID: 2414
		// (Invoke) Token: 0x060054AF RID: 21679
		private delegate void DelegateDrawTownAttackGun(NPC npc, ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset);

		// Token: 0x0200096F RID: 2415
		// (Invoke) Token: 0x060054B3 RID: 21683
		private delegate void DelegateDrawTownAttackSwing(NPC npc, ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset);

		// Token: 0x02000970 RID: 2416
		// (Invoke) Token: 0x060054B7 RID: 21687
		private delegate bool DelegateModifyCollisionData(NPC npc, Rectangle victimHitbox, ref int immunityCooldownSlot, ref MultipliableFloat damageMultiplier, ref Rectangle npcHitbox);

		// Token: 0x02000971 RID: 2417
		// (Invoke) Token: 0x060054BB RID: 21691
		private delegate bool DelegateNeedSaving(NPC npc);

		// Token: 0x02000972 RID: 2418
		// (Invoke) Token: 0x060054BF RID: 21695
		private delegate void DelegateChatBubblePosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects);

		// Token: 0x02000973 RID: 2419
		// (Invoke) Token: 0x060054C3 RID: 21699
		private delegate void DelegatePartyHatPosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects);

		// Token: 0x02000974 RID: 2420
		// (Invoke) Token: 0x060054C7 RID: 21703
		private delegate void DelegateEmoteBubblePosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects);
	}
}
