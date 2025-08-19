using System;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000014 RID: 20
public static class nativefiledialog
{
	// Token: 0x060000A2 RID: 162 RVA: 0x00004AA6 File Offset: 0x00002CA6
	private static int Utf8Size(string str)
	{
		return str.Length * 4 + 1;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00004AB4 File Offset: 0x00002CB4
	private unsafe static byte* Utf8EncodeNullable(string str)
	{
		if (str == null)
		{
			return null;
		}
		int num = nativefiledialog.Utf8Size(str);
		byte* ptr = Marshal.AllocHGlobal(num);
		char* ptr2;
		if (str == null)
		{
			ptr2 = null;
		}
		else
		{
			fixed (char* ptr3 = str.GetPinnableReference())
			{
				ptr2 = ptr3;
			}
		}
		char* chars = ptr2;
		Encoding.UTF8.GetBytes(chars, (str != null) ? (str.Length + 1) : 0, ptr, num);
		char* ptr3 = null;
		return ptr;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00004B08 File Offset: 0x00002D08
	private unsafe static string UTF8_ToManaged(IntPtr s, bool freePtr = false)
	{
		if (s == IntPtr.Zero)
		{
			return null;
		}
		byte* ptr = s;
		while (*ptr != 0)
		{
			ptr++;
		}
		int num = (ptr - s) / 1;
		if (num == 0)
		{
			return string.Empty;
		}
		char* ptr2 = stackalloc char[checked(unchecked((UIntPtr)num) * 2)];
		int chars = Encoding.UTF8.GetChars(s, num, ptr2, num);
		string result = new string(ptr2, 0, chars);
		if (freePtr)
		{
			nativefiledialog.free(s);
		}
		return result;
	}

	// Token: 0x060000A5 RID: 165
	[DllImport("msvcrt", CallingConvention = 2)]
	private static extern void free(IntPtr ptr);

	// Token: 0x060000A6 RID: 166
	[DllImport("nfd", CallingConvention = 2, EntryPoint = "NFD_OpenDialog")]
	private unsafe static extern nativefiledialog.nfdresult_t INTERNAL_NFD_OpenDialog(byte* filterList, byte* defaultPath, out IntPtr outPath);

	// Token: 0x060000A7 RID: 167 RVA: 0x00004B64 File Offset: 0x00002D64
	public unsafe static nativefiledialog.nfdresult_t NFD_OpenDialog(string filterList, string defaultPath, out string outPath)
	{
		byte* ptr2 = nativefiledialog.Utf8EncodeNullable(filterList);
		byte* ptr = nativefiledialog.Utf8EncodeNullable(defaultPath);
		IntPtr outPath2;
		nativefiledialog.nfdresult_t result = nativefiledialog.INTERNAL_NFD_OpenDialog(ptr2, ptr, out outPath2);
		Marshal.FreeHGlobal(ptr2);
		Marshal.FreeHGlobal(ptr);
		outPath = nativefiledialog.UTF8_ToManaged(outPath2, false);
		return result;
	}

	// Token: 0x060000A8 RID: 168
	[DllImport("nfd", CallingConvention = 2, EntryPoint = "NFD_OpenDialogMultiple")]
	private unsafe static extern nativefiledialog.nfdresult_t INTERNAL_NFD_OpenDialogMultiple(byte* filterList, byte* defaultPath, out nativefiledialog.nfdpathset_t outPaths);

	// Token: 0x060000A9 RID: 169 RVA: 0x00004BA0 File Offset: 0x00002DA0
	public unsafe static nativefiledialog.nfdresult_t NFD_OpenDialogMultiple(string filterList, string defaultPath, out nativefiledialog.nfdpathset_t outPaths)
	{
		byte* ptr2 = nativefiledialog.Utf8EncodeNullable(filterList);
		byte* ptr = nativefiledialog.Utf8EncodeNullable(defaultPath);
		nativefiledialog.nfdresult_t result = nativefiledialog.INTERNAL_NFD_OpenDialogMultiple(ptr2, ptr, out outPaths);
		Marshal.FreeHGlobal(ptr2);
		Marshal.FreeHGlobal(ptr);
		return result;
	}

	// Token: 0x060000AA RID: 170
	[DllImport("nfd", CallingConvention = 2, EntryPoint = "NFD_SaveDialog")]
	private unsafe static extern nativefiledialog.nfdresult_t INTERNAL_NFD_SaveDialog(byte* filterList, byte* defaultPath, out IntPtr outPath);

	// Token: 0x060000AB RID: 171 RVA: 0x00004BD0 File Offset: 0x00002DD0
	public unsafe static nativefiledialog.nfdresult_t NFD_SaveDialog(string filterList, string defaultPath, out string outPath)
	{
		byte* ptr2 = nativefiledialog.Utf8EncodeNullable(filterList);
		byte* ptr = nativefiledialog.Utf8EncodeNullable(defaultPath);
		IntPtr outPath2;
		nativefiledialog.nfdresult_t result = nativefiledialog.INTERNAL_NFD_SaveDialog(ptr2, ptr, out outPath2);
		Marshal.FreeHGlobal(ptr2);
		Marshal.FreeHGlobal(ptr);
		outPath = nativefiledialog.UTF8_ToManaged(outPath2, true);
		return result;
	}

	// Token: 0x060000AC RID: 172
	[DllImport("nfd", CallingConvention = 2, EntryPoint = "NFD_PickFolder")]
	private unsafe static extern nativefiledialog.nfdresult_t INTERNAL_NFD_PickFolder(byte* defaultPath, out IntPtr outPath);

	// Token: 0x060000AD RID: 173 RVA: 0x00004C0C File Offset: 0x00002E0C
	public unsafe static nativefiledialog.nfdresult_t NFD_PickFolder(string defaultPath, out string outPath)
	{
		byte* ptr = nativefiledialog.Utf8EncodeNullable(defaultPath);
		IntPtr outPath2;
		nativefiledialog.nfdresult_t result = nativefiledialog.INTERNAL_NFD_PickFolder(ptr, out outPath2);
		Marshal.FreeHGlobal(ptr);
		outPath = nativefiledialog.UTF8_ToManaged(outPath2, true);
		return result;
	}

	// Token: 0x060000AE RID: 174
	[DllImport("nfd", CallingConvention = 2, EntryPoint = "NFD_GetError")]
	private static extern IntPtr INTERNAL_NFD_GetError();

	// Token: 0x060000AF RID: 175 RVA: 0x00004C37 File Offset: 0x00002E37
	public static string NFD_GetError()
	{
		return nativefiledialog.UTF8_ToManaged(nativefiledialog.INTERNAL_NFD_GetError(), false);
	}

	// Token: 0x060000B0 RID: 176
	[DllImport("nfd", CallingConvention = 2)]
	public static extern IntPtr NFD_PathSet_GetCount(ref nativefiledialog.nfdpathset_t pathset);

	// Token: 0x060000B1 RID: 177
	[DllImport("nfd", CallingConvention = 2, EntryPoint = "NFD_PathSet_GetPath")]
	private static extern IntPtr INTERNAL_NFD_PathSet_GetPath(ref nativefiledialog.nfdpathset_t pathset, IntPtr index);

	// Token: 0x060000B2 RID: 178 RVA: 0x00004C44 File Offset: 0x00002E44
	public static string NFD_PathSet_GetPath(ref nativefiledialog.nfdpathset_t pathset, IntPtr index)
	{
		return nativefiledialog.UTF8_ToManaged(nativefiledialog.INTERNAL_NFD_PathSet_GetPath(ref pathset, index), false);
	}

	// Token: 0x060000B3 RID: 179
	[DllImport("nfd", CallingConvention = 2)]
	public static extern void NFD_PathSet_Free(ref nativefiledialog.nfdpathset_t pathset);

	// Token: 0x0400005A RID: 90
	private const string nativeLibName = "nfd";

	// Token: 0x0200077D RID: 1917
	public enum nfdresult_t
	{
		// Token: 0x04006580 RID: 25984
		NFD_ERROR,
		// Token: 0x04006581 RID: 25985
		NFD_OKAY,
		// Token: 0x04006582 RID: 25986
		NFD_CANCEL
	}

	// Token: 0x0200077E RID: 1918
	public struct nfdpathset_t
	{
		// Token: 0x04006583 RID: 25987
		private IntPtr buf;

		// Token: 0x04006584 RID: 25988
		private IntPtr indices;

		// Token: 0x04006585 RID: 25989
		private IntPtr count;
	}
}
