using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terraria.Social.Base
{
	// Token: 0x020000F7 RID: 247
	public abstract class AWorkshopEntry
	{
		// Token: 0x060018AA RID: 6314 RVA: 0x004BE074 File Offset: 0x004BC274
		public static string ReadHeader(string jsonText)
		{
			JToken value;
			if (!JObject.Parse(jsonText).TryGetValue("ContentType", ref value))
			{
				return null;
			}
			return value.ToObject<string>();
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x004BE0A0 File Offset: 0x004BC2A0
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

		// Token: 0x060018AC RID: 6316 RVA: 0x004BE11C File Offset: 0x004BC31C
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
			int outputValue;
			if (!AWorkshopEntry.TryGet<int>(dictionary, "WorkshopPublishedVersion", out outputValue))
			{
				outputValue = 1;
			}
			info.publishedVersion = outputValue;
			JArray outputValue2;
			if (AWorkshopEntry.TryGet<JArray>(dictionary, "Tags", out outputValue2))
			{
				info.tags = outputValue2.ToObject<string[]>();
			}
			int outputValue3;
			if (AWorkshopEntry.TryGet<int>(dictionary, "Publicity", out outputValue3))
			{
				info.publicity = (WorkshopItemPublicSettingId)outputValue3;
			}
			AWorkshopEntry.TryGet<string>(dictionary, "PreviewImagePath", out info.previewImagePath);
			return true;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x004BE1CC File Offset: 0x004BC3CC
		protected static bool TryGet<T>(Dictionary<string, object> dict, string name, out T outputValue)
		{
			outputValue = default(T);
			bool result;
			try
			{
				object value;
				if (dict.TryGetValue(name, out value))
				{
					if (value is T)
					{
						outputValue = (T)((object)value);
						result = true;
					}
					else if (value is JObject)
					{
						outputValue = JsonConvert.DeserializeObject<T>(((JObject)value).ToString());
						result = true;
					}
					else
					{
						outputValue = (T)((object)Convert.ChangeType(value, typeof(T)));
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

		// Token: 0x04001378 RID: 4984
		public const int CurrentWorkshopPublishVersion = 1;

		// Token: 0x04001379 RID: 4985
		public const string ContentTypeName_World = "World";

		// Token: 0x0400137A RID: 4986
		public const string ContentTypeName_ResourcePack = "ResourcePack";

		// Token: 0x0400137B RID: 4987
		protected const string HeaderFileName = "Workshop.json";

		// Token: 0x0400137C RID: 4988
		protected const string ContentTypeJsonCategoryField = "ContentType";

		// Token: 0x0400137D RID: 4989
		protected const string WorkshopPublishedVersionField = "WorkshopPublishedVersion";

		// Token: 0x0400137E RID: 4990
		protected const string WorkshopEntryField = "SteamEntryId";

		// Token: 0x0400137F RID: 4991
		protected const string TagsField = "Tags";

		// Token: 0x04001380 RID: 4992
		protected const string PreviewImageField = "PreviewImagePath";

		// Token: 0x04001381 RID: 4993
		protected const string PublictyField = "Publicity";

		// Token: 0x04001382 RID: 4994
		protected static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = 0,
			MetadataPropertyHandling = 1,
			Formatting = 1
		};
	}
}
