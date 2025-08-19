using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// A tooltip is the text shown to the user in the ModConfig UI next to the cursor when they hover over the annotated member (property, field, or class). This can be longer and more descriptive than the Label. <br />
	/// This attribute sets a custom localization key for the tooltip of the annotated property, field, or class. <br />
	/// The provided localization key must start with "$". <br />
	/// Without this attribute, the localization key "Mods.{ModName}.Configs.{ConfigName}.{MemberName}.Tooltip" will be assumed for members of ModConfig classes. <br />
	/// Annotations on members of non-ModConfig classes need to supply a custom localization key using this attribute to be localized, no localization key is assumed.
	/// If the translation value of a property or field that is an object is an empty string, the tooltip of the class will be used instead.
	/// Passing in just "$" will result in no tooltip entry being added to the localization files.<br />
	/// Values can be interpolated into the resulting label text using <see cref="T:Terraria.ModLoader.Config.TooltipArgsAttribute" />. <br />
	/// </summary>
	// Token: 0x02000371 RID: 881
	public class TooltipKeyAttribute : ConfigKeyAttribute
	{
		// Token: 0x06003092 RID: 12434 RVA: 0x0053CF56 File Offset: 0x0053B156
		public TooltipKeyAttribute(string key) : base(key)
		{
		}
	}
}
