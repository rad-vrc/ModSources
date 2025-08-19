using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Terraria.Localization
{
	// Token: 0x020000A9 RID: 169
	public class GameCulture
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0049FC39 File Offset: 0x0049DE39
		// (set) Token: 0x06001384 RID: 4996 RVA: 0x0049FC40 File Offset: 0x0049DE40
		public static GameCulture DefaultCulture { get; set; } = GameCulture._NamedCultures[GameCulture.CultureName.English];

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0049FC48 File Offset: 0x0049DE48
		public bool IsActive
		{
			get
			{
				return Language.ActiveCulture == this;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0049FC52 File Offset: 0x0049DE52
		public string Name
		{
			get
			{
				return this.CultureInfo.Name;
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0049FC5F File Offset: 0x0049DE5F
		public static GameCulture FromCultureName(GameCulture.CultureName name)
		{
			if (!GameCulture._NamedCultures.ContainsKey(name))
			{
				return GameCulture.DefaultCulture;
			}
			return GameCulture._NamedCultures[name];
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0049FC7F File Offset: 0x0049DE7F
		public static GameCulture FromLegacyId(int id)
		{
			if (id < 1)
			{
				id = 1;
			}
			if (!GameCulture._legacyCultures.ContainsKey(id))
			{
				return GameCulture.DefaultCulture;
			}
			return GameCulture._legacyCultures[id];
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0049FCA8 File Offset: 0x0049DEA8
		public static GameCulture FromName(string name)
		{
			return GameCulture._legacyCultures.Values.SingleOrDefault((GameCulture culture) => culture.Name == name) ?? GameCulture.DefaultCulture;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0049FDB3 File Offset: 0x0049DFB3
		public GameCulture(string name, int legacyId)
		{
			this.CultureInfo = new CultureInfo(name);
			this.LegacyId = legacyId;
			GameCulture.RegisterLegacyCulture(this, legacyId);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0049FDD5 File Offset: 0x0049DFD5
		private static void RegisterLegacyCulture(GameCulture culture, int legacyId)
		{
			if (GameCulture._legacyCultures == null)
			{
				GameCulture._legacyCultures = new Dictionary<int, GameCulture>();
			}
			GameCulture._legacyCultures.Add(legacyId, culture);
		}

		// Token: 0x040011B1 RID: 4529
		private static Dictionary<GameCulture.CultureName, GameCulture> _NamedCultures = new Dictionary<GameCulture.CultureName, GameCulture>
		{
			{
				GameCulture.CultureName.English,
				new GameCulture("en-US", 1)
			},
			{
				GameCulture.CultureName.German,
				new GameCulture("de-DE", 2)
			},
			{
				GameCulture.CultureName.Italian,
				new GameCulture("it-IT", 3)
			},
			{
				GameCulture.CultureName.French,
				new GameCulture("fr-FR", 4)
			},
			{
				GameCulture.CultureName.Spanish,
				new GameCulture("es-ES", 5)
			},
			{
				GameCulture.CultureName.Russian,
				new GameCulture("ru-RU", 6)
			},
			{
				GameCulture.CultureName.Chinese,
				new GameCulture("zh-Hans", 7)
			},
			{
				GameCulture.CultureName.Portuguese,
				new GameCulture("pt-BR", 8)
			},
			{
				GameCulture.CultureName.Polish,
				new GameCulture("pl-PL", 9)
			}
		};

		// Token: 0x040011B3 RID: 4531
		private static Dictionary<int, GameCulture> _legacyCultures;

		// Token: 0x040011B4 RID: 4532
		public readonly CultureInfo CultureInfo;

		// Token: 0x040011B5 RID: 4533
		public readonly int LegacyId;

		// Token: 0x02000553 RID: 1363
		public enum CultureName
		{
			// Token: 0x040058CF RID: 22735
			English = 1,
			// Token: 0x040058D0 RID: 22736
			German,
			// Token: 0x040058D1 RID: 22737
			Italian,
			// Token: 0x040058D2 RID: 22738
			French,
			// Token: 0x040058D3 RID: 22739
			Spanish,
			// Token: 0x040058D4 RID: 22740
			Russian,
			// Token: 0x040058D5 RID: 22741
			Chinese,
			// Token: 0x040058D6 RID: 22742
			Portuguese,
			// Token: 0x040058D7 RID: 22743
			Polish,
			// Token: 0x040058D8 RID: 22744
			Unknown = 9999
		}
	}
}
