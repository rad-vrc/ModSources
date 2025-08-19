using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Exceptions;

namespace Terraria.ModLoader
{
	// Token: 0x020001DC RID: 476
	public sealed class MusicLoader : ILoader
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x004EAFA7 File Offset: 0x004E91A7
		// (set) Token: 0x060024F5 RID: 9461 RVA: 0x004EAFAE File Offset: 0x004E91AE
		public static int MusicCount { get; private set; } = 92;

		/// <summary> Gets the music id of the track with the specified mod path. The path must not have a file extension.
		/// <para /> <MusicAutoloadReminder>
		/// 		Remember that unless you manually register music via <see cref="M:Terraria.ModLoader.MusicLoader.AddMusic(Terraria.ModLoader.Mod,System.String)" />, only files found in a folder or subfolder of a folder named "Music" will be autoloaded as music.
		/// 	</MusicAutoloadReminder> </summary>
		// Token: 0x060024F6 RID: 9462 RVA: 0x004EAFB6 File Offset: 0x004E91B6
		public static int GetMusicSlot(Mod mod, string musicPath)
		{
			return MusicLoader.GetMusicSlot(mod.Name + "/" + musicPath);
		}

		/// <summary> Gets the music id of the track with the specified full path. The path must be prefixed with a mod name and must not have a file extension.
		/// <para /> <MusicAutoloadReminder>
		/// 		Remember that unless you manually register music via <see cref="M:Terraria.ModLoader.MusicLoader.AddMusic(Terraria.ModLoader.Mod,System.String)" />, only files found in a folder or subfolder of a folder named "Music" will be autoloaded as music.
		/// 	</MusicAutoloadReminder> </summary>
		// Token: 0x060024F7 RID: 9463 RVA: 0x004EAFCE File Offset: 0x004E91CE
		public static int GetMusicSlot(string musicPath)
		{
			if (MusicLoader.musicByPath.ContainsKey(musicPath))
			{
				return MusicLoader.musicByPath[musicPath];
			}
			return 0;
		}

		/// <summary> Returns whether or not a music track with the specified mod path exists. The path must not have a file extension. </summary>
		// Token: 0x060024F8 RID: 9464 RVA: 0x004EAFEA File Offset: 0x004E91EA
		public static bool MusicExists(Mod mod, string musicPath)
		{
			return MusicLoader.MusicExists(mod.Name + "/" + musicPath);
		}

		/// <summary> Returns whether or not a music track with the specified path exists. The path must be prefixed with a mod name and must not have a file extension.</summary>
		// Token: 0x060024F9 RID: 9465 RVA: 0x004EB002 File Offset: 0x004E9202
		public static bool MusicExists(string musicPath)
		{
			return MusicLoader.GetMusicSlot(musicPath) > 0;
		}

		/// <summary> Gets the music track with the specified mod path. The path must not have a file extension. </summary>
		// Token: 0x060024FA RID: 9466 RVA: 0x004EB00D File Offset: 0x004E920D
		public static IAudioTrack GetMusic(Mod mod, string musicPath)
		{
			return MusicLoader.GetMusic(mod.Name + "/" + musicPath);
		}

		/// <summary> Gets the music track with the specified full path. The path must be prefixed with a mod name and must not have a file extension. </summary>
		// Token: 0x060024FB RID: 9467 RVA: 0x004EB028 File Offset: 0x004E9228
		public static IAudioTrack GetMusic(string musicPath)
		{
			if (Main.dedServ)
			{
				return null;
			}
			int slot = MusicLoader.GetMusicSlot(musicPath);
			if (slot != 0)
			{
				LegacyAudioSystem audioSystem = Main.audioSystem as LegacyAudioSystem;
				if (audioSystem != null)
				{
					IAudioTrack[] audioTracks = audioSystem.AudioTracks;
					int num = slot;
					if (audioTracks[num] == null)
					{
						audioTracks[num] = MusicLoader.LoadMusic(musicPath, MusicLoader.musicExtensions[musicPath]);
					}
					return ((LegacyAudioSystem)Main.audioSystem).AudioTracks[slot];
				}
			}
			return null;
		}

		/// <summary>
		/// Registers a new music track with the provided mod and its local path to the sound file.
		/// <para /> Use this for any music not autoloaded by the <see cref="P:Terraria.ModLoader.Mod.MusicAutoloadingEnabled" /> logic.
		/// </summary>
		/// <param name="mod"> The mod that owns the music track. </param>
		/// <param name="musicPath"> The provided mod's local path to the music track file, case-sensitive and without extensions. </param>
		// Token: 0x060024FC RID: 9468 RVA: 0x004EB08C File Offset: 0x004E928C
		public static void AddMusic(Mod mod, string musicPath)
		{
			if (!mod.loading)
			{
				throw new Exception("AddMusic can only be called during mod loading.");
			}
			int id = MusicLoader.ReserveMusicID();
			string chosenExtension = "";
			IEnumerable<string> source = MusicLoader.supportedExtensions;
			Func<string, bool> <>9__0;
			Func<string, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((string extension) => mod.FileExists(musicPath + extension)));
			}
			using (IEnumerator<string> enumerator = source.Where(predicate).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					chosenExtension = enumerator.Current;
				}
			}
			if (string.IsNullOrEmpty(chosenExtension))
			{
				throw new ArgumentException("Given path found no files matching the extensions [ " + string.Join(", ", MusicLoader.supportedExtensions) + " ]");
			}
			musicPath = mod.Name + "/" + musicPath;
			MusicLoader.musicByPath[musicPath] = id;
			MusicLoader.musicExtensions[musicPath] = chosenExtension;
			MusicLoader.musicSkipsVolumeRemap[id] = mod.MusicSkipsVolumeRemap;
			MusicID.Search.Add(musicPath, id);
		}

		/// <summary>
		/// Allows you to tie a music ID, and item ID, and a tile ID together to form a music box.
		/// <br /> When music with the given ID is playing, equipped music boxes have a chance to change their ID to the given item type.
		/// <br /> When an item with the given item type is equipped, it will play the music that has musicSlot as its ID.
		/// <br /> When a tile with the given type and Y-frame is nearby, if its X-frame is &gt;= 36, it will play the music that has musicSlot as its ID.
		/// </summary>
		/// <param name="mod"> The music slot. </param>
		/// <param name="musicSlot"> The music slot. </param>
		/// <param name="itemType"> Type of the item. </param>
		/// <param name="tileType"> Type of the tile. </param>
		/// <param name="tileFrameY"> The tile frame y. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// Cannot assign music box to vanilla music id.
		/// or
		/// The provided music id does not exist.
		/// or
		/// Cannot assign music box to a vanilla item id.
		/// or
		/// The provided item id does not exist.
		/// or
		/// Cannot assign music box to a vanilla tile id.
		/// or
		/// The provided tile id does not exist
		/// </exception>
		/// <exception cref="T:System.ArgumentException">
		/// The provided music id has already been assigned a music box.
		/// or
		/// The provided item id has already been assigned a music.
		/// or
		/// Y-frame must be divisible by 36
		/// </exception>
		// Token: 0x060024FD RID: 9469 RVA: 0x004EB1C8 File Offset: 0x004E93C8
		public static void AddMusicBox(Mod mod, int musicSlot, int itemType, int tileType, int tileFrameY = 0)
		{
			if (musicSlot < Main.maxMusic && (!Main.dedServ || musicSlot != 0))
			{
				if (musicSlot == 0)
				{
					throw new ArgumentOutOfRangeException("An invalid music audio file was provided. Note that when using GetMusicSlot the file extension should not be included and that by default only .mp3, .wav, and .ogg are supported audio file formats. Double check the GetMusicSlot documentation to ensure that the path you are providing matches the expected input.");
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Cannot assign music box to vanilla music ID ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(musicSlot);
				throw new ArgumentOutOfRangeException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			else
			{
				if (musicSlot >= MusicLoader.MusicCount)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Music ID ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(musicSlot);
					defaultInterpolatedStringHandler.AppendLiteral(" does not exist");
					throw new ArgumentOutOfRangeException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (itemType < (int)ItemID.Count)
				{
					throw new ArgumentOutOfRangeException("Cannot assign music box to vanilla item ID " + itemType.ToString());
				}
				if (ItemLoader.GetItem(itemType) == null)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Item ID ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(itemType);
					defaultInterpolatedStringHandler.AppendLiteral(" does not exist");
					throw new ArgumentOutOfRangeException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (tileType < (int)TileID.Count)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Cannot assign music box to vanilla tile ID ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(tileType);
					throw new ArgumentOutOfRangeException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (TileLoader.GetTile(tileType) == null)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Tile ID ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(tileType);
					defaultInterpolatedStringHandler.AppendLiteral(" does not exist");
					throw new ArgumentOutOfRangeException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (MusicLoader.musicToItem.ContainsKey(musicSlot))
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Music ID ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(musicSlot);
					defaultInterpolatedStringHandler.AppendLiteral(" has already been assigned a music box");
					throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (MusicLoader.itemToMusic.ContainsKey(itemType))
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Item ID ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(itemType);
					defaultInterpolatedStringHandler.AppendLiteral(" has already been assigned a music");
					throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				Dictionary<int, int> tileToMusicDictionary;
				if (!MusicLoader.tileToMusic.TryGetValue(tileType, out tileToMusicDictionary))
				{
					tileToMusicDictionary = (MusicLoader.tileToMusic[tileType] = new Dictionary<int, int>());
				}
				if (tileToMusicDictionary.ContainsKey(tileFrameY))
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(56, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Y-frame ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(tileFrameY);
					defaultInterpolatedStringHandler.AppendLiteral(" of tile type ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(tileType);
					defaultInterpolatedStringHandler.AppendLiteral(" has already been assigned a music");
					throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (tileFrameY % 36 != 0)
				{
					throw new ArgumentException("Y-frame must be divisible by 36");
				}
				if (!Main.dedServ)
				{
					MusicLoader.musicToItem[musicSlot] = itemType;
					MusicLoader.itemToMusic[itemType] = musicSlot;
				}
				MusicLoader.tileToMusic[tileType][tileFrameY] = musicSlot;
				return;
			}
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x004EB474 File Offset: 0x004E9674
		internal static void AutoloadMusic(Mod mod)
		{
			if (mod.File == null)
			{
				return;
			}
			foreach (string musicPath in from path in mod.RootContentSource.EnumerateAssets()
			where MusicLoader.supportedExtensions.Contains(Path.GetExtension(path)) && (path.StartsWith("Music/") || path.Contains("/Music/"))
			select path)
			{
				MusicLoader.AddMusic(mod, Path.ChangeExtension(musicPath, null));
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x004EB4FC File Offset: 0x004E96FC
		internal static int ReserveMusicID()
		{
			return MusicLoader.MusicCount++;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x004EB50C File Offset: 0x004E970C
		internal static IAudioTrack LoadMusic(string path, string extension)
		{
			path = "tmod:" + path + extension;
			Stream stream = ModContent.OpenRead(path, true);
			IAudioTrack result;
			if (!(extension == ".wav"))
			{
				if (!(extension == ".mp3"))
				{
					if (!(extension == ".ogg"))
					{
						throw new ResourceLoadException("Unknown music extension " + extension, null);
					}
					result = new OGGAudioTrack(stream);
				}
				else
				{
					result = new MP3AudioTrack(stream);
				}
			}
			else
			{
				result = new WAVAudioTrack(stream);
			}
			return result;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x004EB588 File Offset: 0x004E9788
		internal static void CloseModStreams(Mod mod)
		{
			if (!Program.IsMainThread)
			{
				Main.RunOnMainThread(delegate()
				{
					MusicLoader.CloseModStreams(mod);
				}).GetAwaiter().GetResult();
				return;
			}
			string prefix = mod.Name + "/";
			IEnumerable<string> keys = MusicLoader.musicByPath.Keys;
			Func<string, bool> <>9__1;
			Func<string, bool> predicate;
			if ((predicate = <>9__1) == null)
			{
				predicate = (<>9__1 = ((string x) => x.StartsWith(prefix)));
			}
			foreach (string musicPath in keys.Where(predicate))
			{
				MusicLoader.CloseStream(musicPath);
			}
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x004EB648 File Offset: 0x004E9848
		internal static void CloseStream(string musicPath)
		{
			LegacyAudioSystem legacyAudioSystem = Main.audioSystem as LegacyAudioSystem;
			if (legacyAudioSystem == null)
			{
				return;
			}
			int slot = MusicLoader.musicByPath[musicPath];
			if (slot < legacyAudioSystem.AudioTracks.Length)
			{
				IAudioTrack audioTrack = legacyAudioSystem.AudioTracks[slot];
				if (audioTrack == null)
				{
					return;
				}
				audioTrack.Dispose();
			}
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x004EB690 File Offset: 0x004E9890
		void ILoader.ResizeArrays()
		{
			LegacyAudioSystem legacyAudioSystem = Main.audioSystem as LegacyAudioSystem;
			if (legacyAudioSystem == null)
			{
				return;
			}
			LoaderUtils.ResetStaticMembers(typeof(MusicID), true);
			Array.Resize<IAudioTrack>(ref legacyAudioSystem.AudioTracks, MusicLoader.MusicCount);
			Array.Resize<float>(ref Main.musicFade, MusicLoader.MusicCount);
			Array.Resize<bool>(ref Main.musicNoCrossFade, MusicLoader.MusicCount);
			foreach (string sound in MusicLoader.musicByPath.Keys)
			{
				int slot = MusicLoader.GetMusicSlot(sound);
				if (Main.audioSystem is DisabledAudioSystem)
				{
					return;
				}
				legacyAudioSystem.AudioTracks[slot] = MusicLoader.GetMusic(sound);
				MusicID.Sets.SkipsVolumeRemap[slot] = MusicLoader.musicSkipsVolumeRemap[slot];
			}
			Main.audioSystem = legacyAudioSystem;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x004EB768 File Offset: 0x004E9968
		void ILoader.Unload()
		{
			MusicLoader.musicToItem.Clear();
			MusicLoader.itemToMusic.Clear();
			MusicLoader.tileToMusic.Clear();
			MusicLoader.musicByPath.Clear();
			MusicLoader.musicExtensions.Clear();
			MusicLoader.musicSkipsVolumeRemap.Clear();
			MusicLoader.MusicCount = 92;
		}

		// Token: 0x0400174F RID: 5967
		internal static readonly string[] supportedExtensions = new string[]
		{
			".mp3",
			".ogg",
			".wav"
		};

		/// <summary>Unloaded server side </summary>
		// Token: 0x04001750 RID: 5968
		internal static readonly Dictionary<int, int> musicToItem = new Dictionary<int, int>();

		/// <summary>Unloaded server side </summary>
		// Token: 0x04001751 RID: 5969
		internal static readonly Dictionary<int, int> itemToMusic = new Dictionary<int, int>();

		/// <summary>Only Loads the two keys, Tile type and Tile Y frame server side, the value is set to 0</summary>
		// Token: 0x04001752 RID: 5970
		internal static readonly Dictionary<int, Dictionary<int, int>> tileToMusic = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04001753 RID: 5971
		internal static readonly Dictionary<string, int> musicByPath = new Dictionary<string, int>();

		// Token: 0x04001754 RID: 5972
		internal static readonly Dictionary<string, string> musicExtensions = new Dictionary<string, string>();

		// Token: 0x04001755 RID: 5973
		internal static readonly Dictionary<int, bool> musicSkipsVolumeRemap = new Dictionary<int, bool>();
	}
}
