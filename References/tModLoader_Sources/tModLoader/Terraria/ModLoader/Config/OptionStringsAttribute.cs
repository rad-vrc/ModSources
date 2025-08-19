using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// By default, string fields will provide the user with a text input field. Use this attribute to restrict strings to a selection of options.
	/// <para /> <b>This approach is not recommended due to feature deficiencies.</b> It is recommended to use an <see langword="enum" /> instead of a <see langword="string" /> for this type of option selection. By using an <see langword="enum" /> instead of a <see langword="string" /> paired with OptionStringsAttribute, localization is possible and automatic. It is also easier to work with enum values and less prone to errors caused by typos.
	/// <para /> If you want to migrate from a string option entry to an enum in an update to a released mod, you should use the EnumMemberAttribute on the enum fields corresponding to existing string options that had spaces in them previously to support correctly loading the existing config choices of your users when they update the mod:
	/// <code>[EnumMember(Value = "Left Aligned")]
	/// LeftAligned,</code> 
	/// </summary>
	// Token: 0x0200037C RID: 892
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class OptionStringsAttribute : Attribute
	{
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x0053D3C1 File Offset: 0x0053B5C1
		// (set) Token: 0x060030C2 RID: 12482 RVA: 0x0053D3C9 File Offset: 0x0053B5C9
		public string[] OptionLabels { get; set; }

		// Token: 0x060030C3 RID: 12483 RVA: 0x0053D3D2 File Offset: 0x0053B5D2
		public OptionStringsAttribute(string[] optionLabels)
		{
			this.OptionLabels = optionLabels;
		}
	}
}
