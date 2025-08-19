using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terraria.Social.Base
{
	// Token: 0x02000185 RID: 389
	public abstract class AWorkshopEntry
	{
		// Token: 0x06001AFB RID: 6907 RVA: 0x004E6A10 File Offset: 0x004E4C10
		public static string ReadHeader(string jsonText)
		{
			JToken jtoken;
			if (!JObject.Parse(jsonText).TryGetValue("ContentType", ref jtoken))
			{
				return null;
			}
			return jtoken.ToObject<string>();
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x004E6A3C File Offset: 0x004E4C3C
		protected static string CreateHeaderJson(string contentTypeName, ulong workshopEntryId, string[] tags, WorkshopItemPublicSettingId publicity, string previewImagePath)
		{
			new JObject();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["WorkshopPublishedVersion"] = 1;
			dictionary["ContentType"] = contentTypeName;
			dictionary["SteamEntryId"] = workshopEntryId;
			if (tags != null && tags.Length != 0)
			{
				dictionary["Tags"] = JArray.FromObject(tags);
			}
			dictionary["Publicity"] = publicity;
			return JsonConvert.SerializeObject(dictionary, AWorkshopEntry.SerializerSettings);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x004E6AB8 File Offset: 0x004E4CB8
		public static bool TryReadingManifest(string filePath, out FoundWorkshopEntryInfo info)
		{
			info = null;
			if (!File.Exists(filePath))
			{
				return false;
			}
			string text = File.ReadAllText(filePath);
			info = new FoundWorkshopEntryInfo();
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(text, AWorkshopEntry.SerializerSettings);
			if (dictionary == null)
			{
				return false;
			}
			if (!AWorkshopEntry.TryGet<ulong>(dictionary, "SteamEntryId", out info.workshopEntryId))
			{
				return false;
			}
			int publishedVersion;
			if (!AWorkshopEntry.TryGet<int>(dictionary, "WorkshopPublishedVersion", out publishedVersion))
			{
				publishedVersion = 1;
			}
			info.publishedVersion = publishedVersion;
			JArray jarray;
			if (AWorkshopEntry.TryGet<JArray>(dictionary, "Tags", out jarray))
			{
				info.tags = jarray.ToObject<string[]>();
			}
			int publicity;
			if (AWorkshopEntry.TryGet<int>(dictionary, "Publicity", out publicity))
			{
				info.publicity = (WorkshopItemPublicSettingId)publicity;
			}
			AWorkshopEntry.TryGet<string>(dictionary, "PreviewImagePath", out info.previewImagePath);
			return true;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x004E6B68 File Offset: 0x004E4D68
		protected static bool TryGet<T>(Dictionary<string, object> dict, string name, out T outputValue)
		{
			outputValue = default(T);
			bool result;
			try
			{
				object obj;
				if (dict.TryGetValue(name, out obj))
				{
					if (obj is T)
					{
						outputValue = (T)((object)obj);
						result = true;
					}
					else if (obj is JObject)
					{
						outputValue = JsonConvert.DeserializeObject<T>(((JObject)obj).ToString());
						result = true;
					}
					else
					{
						outputValue = (T)((object)Convert.ChangeType(obj, typeof(T)));
						result = true;
					}
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040015E9 RID: 5609
		public const int CurrentWorkshopPublishVersion = 1;

		// Token: 0x040015EA RID: 5610
		public const string ContentTypeName_World = "World";

		// Token: 0x040015EB RID: 5611
		public const string ContentTypeName_ResourcePack = "ResourcePack";

		// Token: 0x040015EC RID: 5612
		protected const string HeaderFileName = "Workshop.json";

		// Token: 0x040015ED RID: 5613
		protected const string ContentTypeJsonCategoryField = "ContentType";

		// Token: 0x040015EE RID: 5614
		protected const string WorkshopPublishedVersionField = "WorkshopPublishedVersion";

		// Token: 0x040015EF RID: 5615
		protected const string WorkshopEntryField = "SteamEntryId";

		// Token: 0x040015F0 RID: 5616
		protected const string TagsField = "Tags";

		// Token: 0x040015F1 RID: 5617
		protected const string PreviewImageField = "PreviewImagePath";

		// Token: 0x040015F2 RID: 5618
		protected const string PublictyField = "Publicity";

		// Token: 0x040015F3 RID: 5619
		protected static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = 0,
			MetadataPropertyHandling = 1,
			Formatting = 1
		};
	}
}
