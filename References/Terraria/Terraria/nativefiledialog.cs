using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000010 RID: 16
public static class nativefiledialog
{
	// Token: 0x06000088 RID: 136 RVA: 0x00004B96 File Offset: 0x00002D96
	private static int Utf8Size(string str)
	{
		return str.Length * 4 + 1;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00004BA4 File Offset: 0x00002DA4
	private unsafe static byte* Utf8EncodeNullable(string str)
	{
		if (str == null)
		{
			return null;
		}
		int num = nativefiledialog.Utf8Size(str);
		byte* ptr = (byte*)((void*)Marshal.AllocHGlobal(num));
		fixed (string text = str)
		{
			char* ptr2 = text;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			Encoding.UTF8.GetBytes(ptr2, (str != null) ? (str.Length + 1) : 0, ptr, num);
		}
		return ptr;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00004BFC File Offset: 0x00002DFC
	private unsafe static string UTF8_ToManaged(IntPtr s, bool freePtr = false)
	{
		if (s == IntPtr.Zero)
		{
			return null;
		}
		byte* ptr = (byte*)((void*)s);
		while (*ptr != 0)
		{
			ptr++;
		}
		int num = (int)((long)((byte*)ptr - (byte*)((void*)s)));
		if (num == 0)
		{
			return string.Empty;
		}
		char* ptr2 = stackalloc char[checked(unchecked((UIntPtr)num) * 2)];
		int chars = Encoding.UTF8.GetChars((byte*)((void*)s), num, ptr2, num);
		string result = new string(ptr2, 0, chars);
		if (freePtr)
		{
			nativefiledialog.free(s);
		}
		return result;
	}

	// Token: 0x0600008B RID: 139
	[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
	private static extern void free(IntPtr ptr);

	// Token: 0x0600008C RID: 140
	[DllImport("nfd", CallingConvention = CallingConvention.Cdecl, EntryPoint = "NFD_OpenDialog")]
	private unsafe static extern nativefiledialog.nfdresult_t INTERNAL_NFD_OpenDialog(byte* filterList, byte* defaultPath, out IntPtr outPath);

	// Token: 0x0600008D RID: 141 RVA: 0x00004C6C File Offset: 0x00002E6C
	public unsafe static nativefiledialog.nfdresult_t NFD_OpenDialog(string filterList, string defaultPath, out string outPath)
	{
		byte* ptr = nativefiledialog.Utf8EncodeNullable(filterList);
		byte* ptr2 = nativefiledialog.Utf8EncodeNullable(defaultPath);
		IntPtr s;
		nativefiledialog.nfdresult_t result = nativefiledialog.INTERNAL_NFD_OpenDialog(ptr, ptr2, out s);
		Marshal.FreeHGlobal((IntPtr)((void*)ptr));
		Marshal.FreeHGlobal((IntPtr)((void*)ptr2));
		outPath = nativefiledialog.UTF8_ToManaged(s, true);
		return result;
	}

	// Token: 0x0600008E RID: 142
	[DllImport("nfd", CallingConvention = CallingConvention.Cdecl, EntryPoint = "NFD_OpenDialogMultiple")]
	private unsafe static extern nativefiledialog.nfdresult_t INTERNAL_NFD_OpenDialogMultiple(byte* filterList, byte* defaultPath, out nativefiledialog.nfdpathset_t outPaths);

	// Token: 0x0600008F RID: 143 RVA: 0x00004CB0 File Offset: 0x00002EB0
	public unsafe static nativefiledialog.nfdresult_t NFD_OpenDialogMultiple(string filterList, string defaultPath, out nativefiledialog.nfdpathset_t outPaths)
	{
		byte* ptr = nativefiledialog.Utf8EncodeNullable(filterList);
		byte* ptr2 = nativefiledialog.Utf8EncodeNullable(defaultPath);
		nativefiledialog.nfdresult_t result = nativefiledialog.INTERNAL_NFD_OpenDialogMultiple(ptr, ptr2, out outPaths);
		Marshal.FreeHGlobal((IntPtr)((void*)ptr));
		Marshal.FreeHGlobal((IntPtr)((void*)ptr2));
		return result;
	}

	// Token: 0x06000090 RID: 144
	[DllImport("nfd", CallingConvention = CallingConvention.Cdecl, EntryPoint = "NFD_SaveDialog")]
	private unsafe static extern nativefiledialog.nfdresult_t INTERNAL_NFD_SaveDialog(byte* filterList, byte* defaultPath, out IntPtr outPath);

	// Token: 0x06000091 RID: 145 RVA: 0x00004CEC File Offset: 0x00002EEC
	public unsafe static nativefiledialog.nfdresult_t NFD_SaveDialog(string filterList, string defaultPath, out string outPath)
	{
		byte* ptr = nativefiledialog.Utf8EncodeNullable(filterList);
		byte* ptr2 = nativefiledialog.Utf8EncodeNullable(defaultPath);
		IntPtr s;
		nativefiledialog.nfdresult_t result = nativefiledialog.INTERNAL_NFD_SaveDialog(ptr, ptr2, out s);
		Marshal.FreeHGlobal((IntPtr)((void*)ptr));
		Marshal.FreeHGlobal((IntPtr)((void*)ptr2));
		outPath = nativefiledialog.UTF8_ToManaged(s, true);
		return result;
	}

	// Token: 0x06000092 RID: 146
	[DllImport("nfd", CallingConvention = CallingConvention.Cdecl, EntryPoint = "NFD_PickFolder")]
	private unsafe static extern nativefiledialog.nfdresult_t INTERNAL_NFD_PickFolder(byte* defaultPath, out IntPtr outPath);

	// Token: 0x06000093 RID: 147 RVA: 0x00004D30 File Offset: 0x00002F30
	public unsafe static nativefiledialog.nfdresult_t NFD_PickFolder(string defaultPath, out string outPath)
	{
		byte* ptr = nativefiledialog.Utf8EncodeNullable(defaultPath);
		IntPtr s;
		nativefiledialog.nfdresult_t result = nativefiledialog.INTERNAL_NFD_PickFolder(ptr, out s);
		Marshal.FreeHGlobal((IntPtr)((void*)ptr));
		outPath = nativefiledialog.UTF8_ToManaged(s, true);
		return result;
	}

	// Token: 0x06000094 RID: 148
	[DllImport("nfd", CallingConvention = CallingConvention.Cdecl, EntryPoint = "NFD_GetError")]
	private static extern IntPtr INTERNAL_NFD_GetError();

	// Token: 0x06000095 RID: 149 RVA: 0x00004D60 File Offset: 0x00002F60
	public static string NFD_GetError()
	{
		return nativefiledialog.UTF8_ToManaged(nativefiledialog.INTERNAL_NFD_GetError(), false);
	}

	// Token: 0x06000096 RID: 150
	[DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr NFD_PathSet_GetCount(ref nativefiledialog.nfdpathset_t pathset);

	// Token: 0x06000097 RID: 151
	[DllImport("nfd", CallingConvention = CallingConvention.Cdecl, EntryPoint = "NFD_PathSet_GetPath")]
	private static extern IntPtr INTERNAL_NFD_PathSet_GetPath(ref nativefiledialog.nfdpathset_t pathset, IntPtr index);

	// Token: 0x06000098 RID: 152 RVA: 0x00004D6D File Offset: 0x00002F6D
	public static string NFD_PathSet_GetPath(ref nativefiledialog.nfdpathset_t pathset, IntPtr index)
	{
		return nativefiledialog.UTF8_ToManaged(nativefiledialog.INTERNAL_NFD_PathSet_GetPath(ref pathset, index), false);
	}

	// Token: 0x06000099 RID: 153
	[DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
	public static extern void NFD_PathSet_Free(ref nativefiledialog.nfdpathset_t pathset);

	// Token: 0x04000050 RID: 80
	private const string nativeLibName = "nfd";

	// Token: 0x02000498 RID: 1176
	public enum nfdresult_t
	{
		// Token: 0x040055B4 RID: 21940
		NFD_ERROR,
		// Token: 0x040055B5 RID: 21941
		NFD_OKAY,
		// Token: 0x040055B6 RID: 21942
		NFD_CANCEL
	}

	// Token: 0x02000499 RID: 1177
	public struct nfdpathset_t
	{
		// Token: 0x040055B7 RID: 21943
		private IntPtr buf;

		// Token: 0x040055B8 RID: 21944
		private IntPtr indices;

		// Token: 0x040055B9 RID: 21945
		private IntPtr count;
	}
}
