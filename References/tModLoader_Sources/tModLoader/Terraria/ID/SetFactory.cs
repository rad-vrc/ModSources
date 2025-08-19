using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using ReLogic.Reflection;
using ReLogic.Utilities;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Terraria.ID
{
	/// <summary>
	/// SetFactory is responsible for creating "custom ID sets" for content. "Custom ID sets" refers to arrays indexed by content ids. The ID set contains data applying to all instances of content of a specific type. This is typically metadata or data controlling how code will interact with each type of content. Each vanilla ID class contains a SetFactory instance called "Factory" which is used to initialize the ID sets contained within the ID class.
	/// <para /> For example <see cref="F:Terraria.ID.ItemID.Sets.Factory" /> is used to initialize <see cref="F:Terraria.ID.ItemID.Sets.IsFood" /> with true values for food items such as <see cref="F:Terraria.ID.ItemID.PadThai" />. Modded content updates ID sets in <see cref="M:Terraria.ModLoader.ModType.SetStaticDefaults" />: <c>ItemID.Sets.IsFood[Type] = true;</c>. Code in tModLoader and individual mods might consult the data in <see cref="F:Terraria.ID.ItemID.Sets.IsFood" /> for whatever purpose they want.
	/// <para /> Mods can make their own custom ID sets through the methods of this class. The <see cref="M:Terraria.ID.SetFactory.CreateNamedSet(System.String)" /> methods create custom ID sets that facilitate collaborative "named ID sets". Mods using the same "named ID set" will share a reference to the same array merging together all the entries and changes. More information can be found in the <see href="https://github.com/tModLoader/tModLoader/pull/4381">Custom and Named ID Sets pull request</see>.
	/// </summary>
	// Token: 0x02000427 RID: 1063
	public class SetFactory
	{
		// Token: 0x06003556 RID: 13654 RVA: 0x00572728 File Offset: 0x00570928
		[Obsolete("Use the new overloads to support named sets", true)]
		public SetFactory(int size)
		{
			if (size == 0)
			{
				throw new ArgumentOutOfRangeException("size cannot be 0, the intializer for Count must run first");
			}
			this._size = size;
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x00572794 File Offset: 0x00570994
		protected bool[] GetBoolBuffer()
		{
			object queueLock = this._queueLock;
			bool[] result;
			lock (queueLock)
			{
				if (this._boolBufferCache.Count == 0)
				{
					result = new bool[this._size];
				}
				else
				{
					result = this._boolBufferCache.Dequeue();
				}
			}
			return result;
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x005727F8 File Offset: 0x005709F8
		protected int[] GetIntBuffer()
		{
			object queueLock = this._queueLock;
			int[] result;
			lock (queueLock)
			{
				if (this._intBufferCache.Count == 0)
				{
					result = new int[this._size];
				}
				else
				{
					result = this._intBufferCache.Dequeue();
				}
			}
			return result;
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x0057285C File Offset: 0x00570A5C
		protected ushort[] GetUshortBuffer()
		{
			object queueLock = this._queueLock;
			ushort[] result;
			lock (queueLock)
			{
				if (this._ushortBufferCache.Count == 0)
				{
					result = new ushort[this._size];
				}
				else
				{
					result = this._ushortBufferCache.Dequeue();
				}
			}
			return result;
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x005728C0 File Offset: 0x00570AC0
		protected float[] GetFloatBuffer()
		{
			object queueLock = this._queueLock;
			float[] result;
			lock (queueLock)
			{
				if (this._floatBufferCache.Count == 0)
				{
					result = new float[this._size];
				}
				else
				{
					result = this._floatBufferCache.Dequeue();
				}
			}
			return result;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x00572924 File Offset: 0x00570B24
		public void Recycle<T>(T[] buffer)
		{
			object queueLock = this._queueLock;
			lock (queueLock)
			{
				if (typeof(T).Equals(typeof(bool)))
				{
					this._boolBufferCache.Enqueue((bool[])buffer);
				}
				else if (typeof(T).Equals(typeof(int)))
				{
					this._intBufferCache.Enqueue((int[])buffer);
				}
			}
		}

		/// <summary> Creates and returns a bool array ID set. All values will be false except the indexes passed in (<paramref name="types" />) will be true. </summary>
		// Token: 0x0600355C RID: 13660 RVA: 0x005729B8 File Offset: 0x00570BB8
		public bool[] CreateBoolSet(params int[] types)
		{
			return this.CreateBoolSet(false, types);
		}

		/// <summary> Creates and returns a bool array ID set. All values will be <paramref name="defaultState" /> except the indexes passed in (<paramref name="types" />) will be the opposite of <paramref name="defaultState" />. </summary>
		// Token: 0x0600355D RID: 13661 RVA: 0x005729C4 File Offset: 0x00570BC4
		public bool[] CreateBoolSet(bool defaultState, params int[] types)
		{
			bool[] boolBuffer = this.GetBoolBuffer();
			for (int i = 0; i < boolBuffer.Length; i++)
			{
				boolBuffer[i] = defaultState;
			}
			for (int j = 0; j < types.Length; j++)
			{
				if (ContentCache.contentLoadingFinished || types[j] < this._size)
				{
					boolBuffer[types[j]] = !defaultState;
				}
			}
			return boolBuffer;
		}

		/// <summary> Creates and returns an int array ID set. All values are -1 except for the values passed in as <paramref name="types" />. The <paramref name="types" /> contain index value pairs listed one after the other. For example <c>CreateIntSet([ItemID.CopperOre, 10, ItemID.SilverOre, 20])</c> will result in an array filled with -1 except the CopperOre index will have a value of 10 and the SilverOre index a value of 20. </summary>
		// Token: 0x0600355E RID: 13662 RVA: 0x00572A14 File Offset: 0x00570C14
		public int[] CreateIntSet(params int[] types)
		{
			return this.CreateIntSet(-1, types);
		}

		/// <summary> Creates and returns an int array ID set. All values are <paramref name="defaultState" /> except for the values passed in as <paramref name="inputs" />. The <paramref name="inputs" /> contain index value pairs listed one after the other. For example <c>CreateIntSet(-1, ItemID.CopperOre, 10, ItemID.SilverOre, 20)</c> will result in an array filled with -1 except the CopperOre index will have a value of 10 and the SilverOre index a value of 20. </summary>
		// Token: 0x0600355F RID: 13663 RVA: 0x00572A20 File Offset: 0x00570C20
		public int[] CreateIntSet(int defaultState, params int[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateArraySet");
			}
			int[] intBuffer = this.GetIntBuffer();
			for (int i = 0; i < intBuffer.Length; i++)
			{
				intBuffer[i] = defaultState;
			}
			for (int j = 0; j < inputs.Length; j += 2)
			{
				if (ContentCache.contentLoadingFinished || inputs[j] < this._size)
				{
					intBuffer[inputs[j]] = inputs[j + 1];
				}
			}
			return intBuffer;
		}

		/// <summary> Creates and returns a ushort array ID set. All values are <paramref name="defaultState" /> except for the values passed in as <paramref name="inputs" />. The <paramref name="inputs" /> contain index value pairs listed one after the other. For example <c>CreateUshortSet(0, ItemID.CopperOre, 10, ItemID.SilverOre, 20)</c> will result in an array filled with 0 except the CopperOre index will have a value of 10 and the SilverOre index a value of 20. </summary>
		// Token: 0x06003560 RID: 13664 RVA: 0x00572A84 File Offset: 0x00570C84
		public ushort[] CreateUshortSet(ushort defaultState, params ushort[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateArraySet");
			}
			ushort[] ushortBuffer = this.GetUshortBuffer();
			for (int i = 0; i < ushortBuffer.Length; i++)
			{
				ushortBuffer[i] = defaultState;
			}
			for (int j = 0; j < inputs.Length; j += 2)
			{
				if (ContentCache.contentLoadingFinished || (int)inputs[j] < this._size)
				{
					ushortBuffer[(int)inputs[j]] = inputs[j + 1];
				}
			}
			return ushortBuffer;
		}

		/// <summary> Creates and returns a float array ID set. All values are <paramref name="defaultState" /> except for the values passed in as <paramref name="inputs" />. The <paramref name="inputs" /> contain index value pairs listed one after the other. For example <c>CreateFloatSet(1f, ItemID.CopperOre, 1.5f, ItemID.SilverOre, 2.3f)</c> will result in an array filled with 1f except the CopperOre index will have a value of 1.5f and the SilverOre index a value of 2.3f. </summary>
		// Token: 0x06003561 RID: 13665 RVA: 0x00572AE8 File Offset: 0x00570CE8
		public float[] CreateFloatSet(float defaultState, params float[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateArraySet");
			}
			float[] floatBuffer = this.GetFloatBuffer();
			for (int i = 0; i < floatBuffer.Length; i++)
			{
				floatBuffer[i] = defaultState;
			}
			for (int j = 0; j < inputs.Length; j += 2)
			{
				if (ContentCache.contentLoadingFinished || inputs[j] < (float)this._size)
				{
					floatBuffer[(int)inputs[j]] = inputs[j + 1];
				}
			}
			return floatBuffer;
		}

		/// <summary>
		/// Creates and returns an array ID set of the supplied Type. This should be used for creating ID sets not covered by the other methods, such as for classes (string, List&lt;T&gt;), nullable primitives (bool?), and structs (Color).
		/// <para /> All values are <paramref name="defaultState" /> except for the values passed in as <paramref name="inputs" />. The <paramref name="inputs" /> contain index value pairs listed one after the other. For example <c>CreateCustomSet&lt;string&gt;(null, ItemID.CopperOre, "Ugly", ItemID.SilverOre, "Pretty")</c> will result in an array filled with null except the CopperOre index will have a value of "Ugly" and the SilverOre index a value of "Pretty".
		/// <para /> Note that for non-null class values used for <paramref name="defaultState" />, the reference will be shared with all default entries, so be mindful when working with these sets to not accidentally affect the shared default state object. For example passing in <c>new List&lt;int&gt;</c> as <paramref name="defaultState" /> will make it very likely to be misused on accident.
		/// </summary>
		// Token: 0x06003562 RID: 13666 RVA: 0x00572B50 File Offset: 0x00570D50
		public T[] CreateCustomSet<T>(T defaultState, params object[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateCustomSet");
			}
			T[] array = new T[this._size];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = defaultState;
			}
			if (inputs != null)
			{
				int j = 0;
				while (j < inputs.Length)
				{
					if (ContentCache.contentLoadingFinished)
					{
						goto IL_B2;
					}
					object obj = inputs[j];
					if (obj is ushort)
					{
						ushort a = (ushort)obj;
						if ((int)a >= this._size)
						{
							goto IL_18C;
						}
					}
					obj = inputs[j];
					if (obj is int)
					{
						int b = (int)obj;
						if (b >= this._size)
						{
							goto IL_18C;
						}
					}
					obj = inputs[j];
					if (!(obj is short))
					{
						goto IL_B2;
					}
					short c = (short)obj;
					if ((int)c < this._size)
					{
						goto IL_B2;
					}
					IL_18C:
					j += 2;
					continue;
					IL_B2:
					T val = typeof(T).IsPrimitive ? ((T)((object)inputs[j + 1])) : ((typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>)) ? ((T)((object)inputs[j + 1])) : ((!typeof(T).IsClass) ? ((T)((object)Convert.ChangeType(inputs[j + 1], typeof(T)))) : ((T)((object)inputs[j + 1]))));
					if (inputs[j] is ushort)
					{
						array[(int)((ushort)inputs[j])] = val;
						goto IL_18C;
					}
					if (inputs[j] is int)
					{
						array[(int)inputs[j]] = val;
						goto IL_18C;
					}
					array[(int)((short)inputs[j])] = val;
					goto IL_18C;
				}
			}
			return array;
		}

		/// <summary>
		/// Causes sets registered with the provided keys (and matching SetFactory and Type) to be merged as if they are registered with the same key. This is useful for situations where established set keys are determined to have identical meaning but the involved mods are incapable of updating to collaborate on the shared key, either due to dependent mods or inactivity.
		/// <para /> Essentially, the sets will be merged and share the same data. The default value must still be consistent between the sets.
		/// <para /> This must be called before the ResizeArrays stage of mod loading, such as in a Load method.
		/// </summary>
		// Token: 0x06003563 RID: 13667 RVA: 0x00572CF8 File Offset: 0x00570EF8
		public void MergeNamedSets<T>(params string[] inputSetNames)
		{
			if (ContentCache.contentLoadingFinished)
			{
				throw new Exception("MergeSets can only be called before sets are initialized, such as in Load.");
			}
			if (inputSetNames == null || inputSetNames.Length <= 1)
			{
				return;
			}
			Dictionary<string, HashSet<string>> mergeRegistry = SetFactory.MergedSets.GetOrAdd(new SetFactory.SetFactoryNameTypePair(this.ContainingClassName, typeof(T)), ([Nullable(1)] SetFactory.SetFactoryNameTypePair _) => new Dictionary<string, HashSet<string>>());
			HashSet<string> merged = inputSetNames.ToHashSet<string>();
			string requestedMerge = string.Join(", ", merged);
			foreach (string name in inputSetNames)
			{
				HashSet<string> existingMerge;
				if (mergeRegistry.TryGetValue(name, out existingMerge))
				{
					merged.UnionWith(existingMerge);
				}
			}
			foreach (string name2 in merged)
			{
				mergeRegistry[name2] = merged;
			}
			string finalMerge = string.Join(", ", merged);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 4);
			defaultInterpolatedStringHandler.AppendLiteral("Custom ");
			defaultInterpolatedStringHandler.AppendFormatted(this.ContainingClassName);
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			defaultInterpolatedStringHandler.AppendFormatted(this.FullTypeName(typeof(T)));
			defaultInterpolatedStringHandler.AppendLiteral("[] sets {");
			defaultInterpolatedStringHandler.AppendFormatted(requestedMerge);
			defaultInterpolatedStringHandler.AppendLiteral("} merged by ");
			defaultInterpolatedStringHandler.AppendFormatted(ModContent.CurrentlyLoadingMod);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			string log = defaultInterpolatedStringHandler.ToStringAndClear();
			if (finalMerge != requestedMerge)
			{
				log = log + " Result: {" + finalMerge + "}";
			}
			Logging.tML.Debug(log);
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x00572EA8 File Offset: 0x005710A8
		public static void ResizeArrays(bool unloading)
		{
			SetFactory.SetFactories.Clear();
			if (unloading)
			{
				SetFactory.MergedSets = new ConcurrentDictionary<SetFactory.SetFactoryNameTypePair, Dictionary<string, HashSet<string>>>();
			}
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x00572EC4 File Offset: 0x005710C4
		public SetFactory(int size, string idClassName, IdDictionary search) : this(size, idClassName, delegate(int id)
		{
			string name;
			if (!search.TryGetName(id, ref name))
			{
				return null;
			}
			return name;
		})
		{
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x00572EF4 File Offset: 0x005710F4
		public SetFactory(int size, string idClassName, Func<int, string> getName = null)
		{
			ArgumentNullException.ThrowIfNull(idClassName, "idClassName");
			this.ContainingClassName = idClassName;
			this.GetName = getName;
			if (SetFactory.SetFactories.Any((SetFactory x) => x.ContainingClassName == this.ContainingClassName))
			{
				throw new Exception("SetFactory instances must have unique names");
			}
			SetFactory.SetFactories.Add(this);
			this._size = size;
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x00572F97 File Offset: 0x00571197
		public void Clear()
		{
			this.namedSets.Clear();
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x00572FA4 File Offset: 0x005711A4
		internal string FullTypeName(Type type)
		{
			if (!type.IsGenericType)
			{
				return type.Name;
			}
			return type.Name + "(" + string.Join(", ", from x in type.GenericTypeArguments
			select x.Name) + ")";
		}

		/// <summary>
		/// <CreateNamedXSetNotes>
		/// 		<para /> This method creates a custom ID set as a "named ID set". This named ID set will be shared and merged with other named ID sets registered by other mods with the same final key, default state, and Type.
		/// 		<para /> This method can be chained to the <see cref="M:Terraria.ID.SetFactory.NamedSetKey.Description(System.String)" /> method to register a description about this named ID set. 
		/// 		<para /> Finally, the <c>RegisterXSet</c> method must be chained to provide the default value and initial data for this set, resulting in the final array being returned.
		/// 		<para /> For example: <code>public static bool[] FlamingWeapon = ItemID.Sets.Factory.CreateNamedSet("IsSpicy")
		/// 		.Description("Food items in this set cause the On Fire debuff to be applied to the player when eaten")
		/// 		.RegisterBoolSet(false, ItemID.PadThai, ModContent.ItemType&lt;ExampleFoodItem&gt;());</code>
		/// 		<para /> In advanced cases this can be used to call <see cref="M:Terraria.ID.SetFactory.RegisterNamedCustomSet``1(Terraria.ID.SetFactory.NamedSetKey,``0,``0[]@)" /> directly rather than using the <c>RegisterXSet</c> method to generate the data array.
		/// 	<para /> More information on properly using these methods can be found in the <see href="https://github.com/tModLoader/tModLoader/pull/4381">Custom and Named ID Sets pull request</see>. Named ID set keys used by other mods are listed in the <see href="https://github.com/tModLoader/tModLoader/wiki/Named-ID-Sets">Named ID Sets wiki page</see>, please read and consult this page to collaborate on named ID set key meanings.
		/// 	</CreateNamedXSetNotes>
		/// <para /> The final key for this named ID set using this overload will be <c>"{key}"</c> directly if it contains a "/". Otherwise, the final key will be derived automatically from the currently loading mod: <c>"{loadingMod.Name}/{key}"</c>
		/// </summary>
		// Token: 0x06003569 RID: 13673 RVA: 0x00573009 File Offset: 0x00571209
		public SetFactory.NamedSetKey CreateNamedSet(string fullKey)
		{
			return new SetFactory.NamedSetKey(this, fullKey);
		}

		/// <summary>
		/// <CreateNamedXSetNotes>
		/// 		<para /> This method creates a custom ID set as a "named ID set". This named ID set will be shared and merged with other named ID sets registered by other mods with the same final key, default state, and Type.
		/// 		<para /> This method can be chained to the <see cref="M:Terraria.ID.SetFactory.NamedSetKey.Description(System.String)" /> method to register a description about this named ID set. 
		/// 		<para /> Finally, the <c>RegisterXSet</c> method must be chained to provide the default value and initial data for this set, resulting in the final array being returned.
		/// 		<para /> For example: <code>public static bool[] FlamingWeapon = ItemID.Sets.Factory.CreateNamedSet("IsSpicy")
		/// 		.Description("Food items in this set cause the On Fire debuff to be applied to the player when eaten")
		/// 		.RegisterBoolSet(false, ItemID.PadThai, ModContent.ItemType&lt;ExampleFoodItem&gt;());</code>
		/// 		<para /> In advanced cases this can be used to call <see cref="M:Terraria.ID.SetFactory.RegisterNamedCustomSet``1(Terraria.ID.SetFactory.NamedSetKey,``0,``0[]@)" /> directly rather than using the <c>RegisterXSet</c> method to generate the data array.
		/// 	<para /> More information on properly using these methods can be found in the <see href="https://github.com/tModLoader/tModLoader/pull/4381">Custom and Named ID Sets pull request</see>. Named ID set keys used by other mods are listed in the <see href="https://github.com/tModLoader/tModLoader/wiki/Named-ID-Sets">Named ID Sets wiki page</see>, please read and consult this page to collaborate on named ID set key meanings.
		/// 	</CreateNamedXSetNotes>
		/// <para /> The final key for this named ID set using this overload will be: <c>"{modName}/{key}"</c>
		/// </summary>
		// Token: 0x0600356A RID: 13674 RVA: 0x00573012 File Offset: 0x00571212
		public SetFactory.NamedSetKey CreateNamedSet(string modName, string key)
		{
			return new SetFactory.NamedSetKey(this, modName, key);
		}

		/// <summary>
		/// <CreateNamedXSetNotes>
		/// 		<para /> This method creates a custom ID set as a "named ID set". This named ID set will be shared and merged with other named ID sets registered by other mods with the same final key, default state, and Type.
		/// 		<para /> This method can be chained to the <see cref="M:Terraria.ID.SetFactory.NamedSetKey.Description(System.String)" /> method to register a description about this named ID set. 
		/// 		<para /> Finally, the <c>RegisterXSet</c> method must be chained to provide the default value and initial data for this set, resulting in the final array being returned.
		/// 		<para /> For example: <code>public static bool[] FlamingWeapon = ItemID.Sets.Factory.CreateNamedSet("IsSpicy")
		/// 		.Description("Food items in this set cause the On Fire debuff to be applied to the player when eaten")
		/// 		.RegisterBoolSet(false, ItemID.PadThai, ModContent.ItemType&lt;ExampleFoodItem&gt;());</code>
		/// 		<para /> In advanced cases this can be used to call <see cref="M:Terraria.ID.SetFactory.RegisterNamedCustomSet``1(Terraria.ID.SetFactory.NamedSetKey,``0,``0[]@)" /> directly rather than using the <c>RegisterXSet</c> method to generate the data array.
		/// 	<para /> More information on properly using these methods can be found in the <see href="https://github.com/tModLoader/tModLoader/pull/4381">Custom and Named ID Sets pull request</see>. Named ID set keys used by other mods are listed in the <see href="https://github.com/tModLoader/tModLoader/wiki/Named-ID-Sets">Named ID Sets wiki page</see>, please read and consult this page to collaborate on named ID set key meanings.
		/// 	</CreateNamedXSetNotes>
		/// <para /> The final key for this named ID set using this overload will be: <c>"{mod.Name}/{key}"</c>
		/// <see cref="M:Terraria.ID.SetFactory.CreateNamedSet(System.String)" />
		/// </summary>
		// Token: 0x0600356B RID: 13675 RVA: 0x0057301C File Offset: 0x0057121C
		public SetFactory.NamedSetKey CreateNamedSet(Mod mod, string key)
		{
			return new SetFactory.NamedSetKey(this, mod, key);
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x00573026 File Offset: 0x00571226
		private T[] RegisterNamedCustomSet<T>(SetFactory.NamedSetKey setKey, T defaultValue, T[] input)
		{
			this.RegisterNamedCustomSet<T>(setKey, defaultValue, ref input);
			return input;
		}

		/// <summary>
		/// Manually registers a named ID set. This is typically done through the <c>Terraria.ID.XID.Sets.Factory.CreateNamedSet().RegisterXSet()</c> methods, but this method can be used for manually initialized arrays.
		/// <para /> The set reference passed in might be changed by this method when merging with existing data.
		/// <para /> Throws an exception if the data length or default value does not match a named ID set with the same key registered before this.
		/// </summary>
		/// <remarks> <!-- No matching elements were found for the following include tag --><include file="CommonDocs.xml" path="Common/CreateNamedXSetFinalKeyC" /> </remarks>
		// Token: 0x0600356D RID: 13677 RVA: 0x00573034 File Offset: 0x00571234
		public void RegisterNamedCustomSet<T>(SetFactory.NamedSetKey setKey, T defaultValue, ref T[] input)
		{
			if (this.ContainingClassName == null)
			{
				throw new ArgumentException("Cannot register named sets on a SetFactory whith no name (using the obsolete constructor)");
			}
			string unmergedKey = setKey.fullKey;
			string key = setKey.fullKey;
			string description = setKey.description;
			if (!ContentCache.contentLoadingFinished)
			{
				if (!new StackTrace().GetFrames().Any((StackFrame frame) => SetFactory.<RegisterNamedCustomSet>g__IsReinitArraysCctor|38_1<T>(frame.GetMethod())))
				{
					throw new Exception("Custom sets cannot be initialized during Load phase, except via the static constructor of a class with [ReinitializeDuringResizeArrays].\r\nThis ensures that all content has been registered and that the custom set will have the correct length");
				}
				return;
			}
			else
			{
				Dictionary<string, HashSet<string>> mergeRegistry;
				HashSet<string> merged;
				if (SetFactory.MergedSets.TryGetValue(new SetFactory.SetFactoryNameTypePair(this.ContainingClassName, typeof(T)), out mergeRegistry) && mergeRegistry.TryGetValue(key, out merged))
				{
					key = "{" + string.Join(", ", merged) + "}";
				}
				T[] originalInput = input;
				SetFactory.SetMetadata<T> metadata = (SetFactory.SetMetadata<T>)this.namedSets.GetOrAdd(new ValueTuple<string, Type>(key, typeof(T)), ([TupleElementNames(new string[]
				{
					"name",
					"type"
				})] ValueTuple<string, Type> _) => new SetFactory.SetMetadata<T>(key, typeof(T), defaultValue, originalInput));
				if (!EqualityComparer<T>.Default.Equals(defaultValue, metadata.defaultValue))
				{
					string conflictMergeInfo = "";
					if (metadata.registeredNames.Any<string>() && !metadata.registeredNames.Contains(unmergedKey))
					{
						conflictMergeInfo = "\nThis set has been merged with [" + string.Join(", ", metadata.registeredNames) + "]. Check the logs to see if another mod is causing this conflict.";
					}
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(114, 6);
					defaultInterpolatedStringHandler.AppendLiteral("Failed to register ");
					defaultInterpolatedStringHandler.AppendFormatted(this.ContainingClassName);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted(this.FullTypeName(typeof(T)));
					defaultInterpolatedStringHandler.AppendLiteral("[] set ");
					defaultInterpolatedStringHandler.AppendFormatted(unmergedKey);
					defaultInterpolatedStringHandler.AppendLiteral(". Default value (");
					defaultInterpolatedStringHandler.AppendFormatted<object>(defaultValue ?? "null");
					defaultInterpolatedStringHandler.AppendLiteral(") conflicts with existing default value (");
					defaultInterpolatedStringHandler.AppendFormatted<object>(metadata.DefaultValue ?? "null");
					defaultInterpolatedStringHandler.AppendLiteral(") registered by the mod(s) [");
					defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", metadata.involvedMods));
					defaultInterpolatedStringHandler.AppendLiteral("]");
					throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear() + conflictMergeInfo + "\n\nIf you are the developer of this mod, please visit https://github.com/tModLoader/tModLoader/wiki/Named-ID-Sets to see how existing mods are using named ID sets and adjust accordingly.");
				}
				T[] value = metadata.array;
				if (value != input)
				{
					if (value.Length != input.Length)
					{
						throw new Exception("Input set and existing set are of different lengths.");
					}
					for (int i = 0; i < input.Length; i++)
					{
						if (!EqualityComparer<T>.Default.Equals(input[i], defaultValue))
						{
							value[i] = input[i];
						}
					}
					if (ModCompile.activelyModding && !metadata.involvedMods.Contains(ModContent.CurrentlyLoadingMod))
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 4);
						defaultInterpolatedStringHandler.AppendLiteral("Custom ");
						defaultInterpolatedStringHandler.AppendFormatted(this.ContainingClassName);
						defaultInterpolatedStringHandler.AppendLiteral(" ");
						defaultInterpolatedStringHandler.AppendFormatted(this.FullTypeName(typeof(T)));
						defaultInterpolatedStringHandler.AppendLiteral("[] set ");
						defaultInterpolatedStringHandler.AppendFormatted(key);
						defaultInterpolatedStringHandler.AppendLiteral(" registered by ");
						defaultInterpolatedStringHandler.AppendFormatted(ModContent.CurrentlyLoadingMod);
						defaultInterpolatedStringHandler.AppendLiteral(".");
						string log = defaultInterpolatedStringHandler.ToStringAndClear();
						log = log + " Previously registered by [" + string.Join(", ", metadata.involvedMods) + "]";
						Logging.tML.Debug(log);
					}
				}
				metadata.registeredNames.Add(unmergedKey);
				metadata.involvedMods.Add(ModContent.CurrentlyLoadingMod);
				if (!string.IsNullOrWhiteSpace(description))
				{
					metadata.descriptions[ModContent.CurrentlyLoadingMod] = description;
				}
				input = value;
				return;
			}
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x00573420 File Offset: 0x00571620
		internal string CustomMetadataInfo(string searchTerm, bool printValues)
		{
			SetFactory.<>c__DisplayClass39_0 CS$<>8__locals1 = new SetFactory.<>c__DisplayClass39_0();
			CS$<>8__locals1.searchTerm = searchTerm;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.printValues = printValues;
			StringBuilder sb = new StringBuilder();
			IEnumerable<SetFactory.SetMetadata> sets = from x in this.namedSets.Values
			orderby x.name
			select x;
			if (CS$<>8__locals1.searchTerm != null)
			{
				sets = sets.Where(delegate(SetFactory.SetMetadata set)
				{
					IEnumerable<string> involvedMods = set.involvedMods;
					Func<string, bool> predicate;
					if ((predicate = CS$<>8__locals1.<>9__3) == null)
					{
						predicate = (CS$<>8__locals1.<>9__3 = ((string s) => s.Equals(CS$<>8__locals1.searchTerm, StringComparison.OrdinalIgnoreCase)));
					}
					return involvedMods.Any(predicate) || set.name.Contains(CS$<>8__locals1.searchTerm, StringComparison.OrdinalIgnoreCase);
				});
			}
			foreach (SetFactory.SetMetadata set2 in sets)
			{
				CS$<>8__locals1.<CustomMetadataInfo>g__OutputText|2(sb, set2);
			}
			return sb.ToString();
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x00573509 File Offset: 0x00571709
		[CompilerGenerated]
		internal static bool <RegisterNamedCustomSet>g__IsReinitArraysCctor|38_1<T>(MethodBase method)
		{
			if (method != null && method.MemberType == MemberTypes.Constructor && method.IsStatic)
			{
				Type declaringType = method.DeclaringType;
				return ((declaringType != null) ? AttributeUtilities.GetAttribute<ReinitializeDuringResizeArraysAttribute>(declaringType) : null) != null;
			}
			return false;
		}

		// Token: 0x04004889 RID: 18569
		protected int _size;

		// Token: 0x0400488A RID: 18570
		private readonly Queue<int[]> _intBufferCache = new Queue<int[]>();

		// Token: 0x0400488B RID: 18571
		private readonly Queue<ushort[]> _ushortBufferCache = new Queue<ushort[]>();

		// Token: 0x0400488C RID: 18572
		private readonly Queue<bool[]> _boolBufferCache = new Queue<bool[]>();

		// Token: 0x0400488D RID: 18573
		private readonly Queue<float[]> _floatBufferCache = new Queue<float[]>();

		// Token: 0x0400488E RID: 18574
		private object _queueLock = new object();

		// Token: 0x0400488F RID: 18575
		internal static List<SetFactory> SetFactories = new List<SetFactory>();

		// Token: 0x04004890 RID: 18576
		internal static ConcurrentDictionary<SetFactory.SetFactoryNameTypePair, Dictionary<string, HashSet<string>>> MergedSets = new ConcurrentDictionary<SetFactory.SetFactoryNameTypePair, Dictionary<string, HashSet<string>>>();

		// Token: 0x04004891 RID: 18577
		private readonly string ContainingClassName;

		// Token: 0x04004892 RID: 18578
		private readonly Func<int, string> GetName;

		// Token: 0x04004893 RID: 18579
		[TupleElementNames(new string[]
		{
			"name",
			"type"
		})]
		private readonly ConcurrentDictionary<ValueTuple<string, Type>, SetFactory.SetMetadata> namedSets = new ConcurrentDictionary<ValueTuple<string, Type>, SetFactory.SetMetadata>();

		/// <summary>
		/// Used to construct the key for this "named ID set". Must be chained with a <c>RegisterXSet</c> method to create and register the set for sharing.
		/// </summary>
		// Token: 0x02000B5D RID: 2909
		public class NamedSetKey
		{
			// Token: 0x06005C87 RID: 23687 RVA: 0x006C3070 File Offset: 0x006C1270
			internal NamedSetKey(SetFactory factory, string fullKey)
			{
				this.factory = factory;
				if (!fullKey.Contains('/'))
				{
					fullKey = ModContent.CurrentlyLoadingMod + "/" + fullKey;
				}
				if (fullKey.Contains(' '))
				{
					throw new ArgumentException("Set names may not contain spaces");
				}
				this.fullKey = fullKey;
			}

			// Token: 0x06005C88 RID: 23688 RVA: 0x006C30C2 File Offset: 0x006C12C2
			internal NamedSetKey(SetFactory factory, string modName, string key) : this(factory, modName + "/" + key)
			{
			}

			// Token: 0x06005C89 RID: 23689 RVA: 0x006C30D7 File Offset: 0x006C12D7
			internal NamedSetKey(SetFactory factory, Mod mod, string key) : this(factory, mod.Name, key)
			{
			}

			/// <summary>
			/// Adds a description to this named ID set.
			/// <para /> This description serves to communicate to other mod makers interested in interfacing with this set what the entries in the set mean and what your mod does with entries in the set. Multiple mods can register a description and they will all be available to view. Modders can use the "/customsets" chat command to output a complete listing of descriptions for all named ID sets to "CustomSets.txt" in the logs directory.
			/// </summary>
			// Token: 0x06005C8A RID: 23690 RVA: 0x006C30E7 File Offset: 0x006C12E7
			public SetFactory.NamedSetKey Description(string description)
			{
				this.description = description;
				return this;
			}

			/// <summary> <inheritdoc cref="M:Terraria.ID.SetFactory.CreateCustomSet``1(``0,System.Object[])" /> <RegisterXSetNotes>
			/// 		<para /> This method registers the created array as a "named ID set" using a final key determined by the <c>CreateNamedSet</c> method.
			/// 	</RegisterXSetNotes> </summary>
			// Token: 0x06005C8B RID: 23691 RVA: 0x006C30F1 File Offset: 0x006C12F1
			public T[] RegisterCustomSet<T>(T defaultState, params object[] inputs)
			{
				return this.factory.RegisterNamedCustomSet<T>(this, defaultState, this.factory.CreateCustomSet<T>(defaultState, inputs));
			}

			/// <summary> <inheritdoc cref="M:Terraria.ID.SetFactory.CreateFloatSet(System.Single,System.Single[])" /> <RegisterXSetNotes>
			/// 		<para /> This method registers the created array as a "named ID set" using a final key determined by the <c>CreateNamedSet</c> method.
			/// 	</RegisterXSetNotes> </summary>
			// Token: 0x06005C8C RID: 23692 RVA: 0x006C310D File Offset: 0x006C130D
			public float[] RegisterFloatSet(float defaultState, params float[] inputs)
			{
				return this.factory.RegisterNamedCustomSet<float>(this, defaultState, this.factory.CreateFloatSet(defaultState, inputs));
			}

			/// <summary> <inheritdoc cref="M:Terraria.ID.SetFactory.CreateUshortSet(System.UInt16,System.UInt16[])" /> <RegisterXSetNotes>
			/// 		<para /> This method registers the created array as a "named ID set" using a final key determined by the <c>CreateNamedSet</c> method.
			/// 	</RegisterXSetNotes> </summary>
			// Token: 0x06005C8D RID: 23693 RVA: 0x006C3129 File Offset: 0x006C1329
			public ushort[] RegisterUshortSet(ushort defaultState, params ushort[] inputs)
			{
				return this.factory.RegisterNamedCustomSet<ushort>(this, defaultState, this.factory.CreateUshortSet(defaultState, inputs));
			}

			/// <summary> <inheritdoc cref="M:Terraria.ID.SetFactory.CreateIntSet(System.Int32,System.Int32[])" /> <RegisterXSetNotes>
			/// 		<para /> This method registers the created array as a "named ID set" using a final key determined by the <c>CreateNamedSet</c> method.
			/// 	</RegisterXSetNotes> </summary>
			// Token: 0x06005C8E RID: 23694 RVA: 0x006C3145 File Offset: 0x006C1345
			public int[] RegisterIntSet(int defaultState, params int[] inputs)
			{
				return this.factory.RegisterNamedCustomSet<int>(this, defaultState, this.factory.CreateIntSet(defaultState, inputs));
			}

			/// <summary> <inheritdoc cref="M:Terraria.ID.SetFactory.CreateIntSet(System.Int32[])" /> <RegisterXSetNotes>
			/// 		<para /> This method registers the created array as a "named ID set" using a final key determined by the <c>CreateNamedSet</c> method.
			/// 	</RegisterXSetNotes> </summary>
			// Token: 0x06005C8F RID: 23695 RVA: 0x006C3161 File Offset: 0x006C1361
			public int[] RegisterIntSet(params int[] types)
			{
				return this.RegisterIntSet(-1, types);
			}

			/// <summary> <inheritdoc cref="M:Terraria.ID.SetFactory.CreateBoolSet(System.Int32[])" /> <RegisterXSetNotes>
			/// 		<para /> This method registers the created array as a "named ID set" using a final key determined by the <c>CreateNamedSet</c> method.
			/// 	</RegisterXSetNotes> </summary>
			// Token: 0x06005C90 RID: 23696 RVA: 0x006C316B File Offset: 0x006C136B
			public bool[] RegisterBoolSet(params int[] types)
			{
				return this.RegisterBoolSet(false, types);
			}

			/// <summary> <inheritdoc cref="M:Terraria.ID.SetFactory.CreateBoolSet(System.Boolean,System.Int32[])" /> <RegisterXSetNotes>
			/// 		<para /> This method registers the created array as a "named ID set" using a final key determined by the <c>CreateNamedSet</c> method.
			/// 	</RegisterXSetNotes> </summary>
			// Token: 0x06005C91 RID: 23697 RVA: 0x006C3175 File Offset: 0x006C1375
			public bool[] RegisterBoolSet(bool defaultState, params int[] types)
			{
				return this.factory.RegisterNamedCustomSet<bool>(this, defaultState, this.factory.CreateBoolSet(defaultState, types));
			}

			// Token: 0x0400751D RID: 29981
			private readonly SetFactory factory;

			// Token: 0x0400751E RID: 29982
			internal readonly string fullKey;

			// Token: 0x0400751F RID: 29983
			internal string description;
		}

		// Token: 0x02000B5E RID: 2910
		private abstract class SetMetadata
		{
			// Token: 0x06005C92 RID: 23698 RVA: 0x006C3191 File Offset: 0x006C1391
			protected SetMetadata(string name, Type type)
			{
				this.name = name;
				this.type = type;
			}

			// Token: 0x17000940 RID: 2368
			// (get) Token: 0x06005C93 RID: 23699
			public abstract object DefaultValue { get; }

			// Token: 0x06005C94 RID: 23700
			[return: TupleElementNames(new string[]
			{
				"i",
				"v"
			})]
			public abstract IEnumerable<ValueTuple<int, object>> EnumerateNonDefaultValues();

			// Token: 0x04007520 RID: 29984
			public readonly string name;

			// Token: 0x04007521 RID: 29985
			public readonly Type type;

			// Token: 0x04007522 RID: 29986
			public readonly HashSet<string> registeredNames = new HashSet<string>();

			// Token: 0x04007523 RID: 29987
			public readonly HashSet<string> involvedMods = new HashSet<string>();

			// Token: 0x04007524 RID: 29988
			public readonly Dictionary<string, string> descriptions = new Dictionary<string, string>();
		}

		// Token: 0x02000B5F RID: 2911
		private class SetMetadata<T> : SetFactory.SetMetadata
		{
			// Token: 0x06005C95 RID: 23701 RVA: 0x006C31C8 File Offset: 0x006C13C8
			public SetMetadata(string name, Type type, T defaultValue, T[] array) : base(name, type)
			{
				this.defaultValue = defaultValue;
				this.array = array;
			}

			// Token: 0x17000941 RID: 2369
			// (get) Token: 0x06005C96 RID: 23702 RVA: 0x006C31E1 File Offset: 0x006C13E1
			public override object DefaultValue
			{
				get
				{
					return this.defaultValue;
				}
			}

			// Token: 0x06005C97 RID: 23703 RVA: 0x006C31EE File Offset: 0x006C13EE
			public override IEnumerable<ValueTuple<int, object>> EnumerateNonDefaultValues()
			{
				int num;
				for (int i = 0; i < this.array.Length; i = num + 1)
				{
					T v = this.array[i];
					if (!EqualityComparer<T>.Default.Equals(this.defaultValue, v))
					{
						yield return new ValueTuple<int, object>(i, v);
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06005C98 RID: 23704 RVA: 0x006C3200 File Offset: 0x006C1400
			public override int GetHashCode()
			{
				T t = this.defaultValue;
				return t.GetHashCode() ^ this.array.GetHashCode();
			}

			// Token: 0x06005C99 RID: 23705 RVA: 0x006C3230 File Offset: 0x006C1430
			public override bool Equals(object obj)
			{
				SetFactory.SetMetadata<T> metadata = obj as SetFactory.SetMetadata<T>;
				return metadata != null && EqualityComparer<T>.Default.Equals(this.defaultValue, metadata.defaultValue) && this.array == metadata.array;
			}

			// Token: 0x04007525 RID: 29989
			public readonly T defaultValue;

			// Token: 0x04007526 RID: 29990
			public readonly T[] array;
		}

		// Token: 0x02000B60 RID: 2912
		internal class SetFactoryNameTypePair : IEquatable<SetFactory.SetFactoryNameTypePair>
		{
			// Token: 0x06005C9A RID: 23706 RVA: 0x006C326F File Offset: 0x006C146F
			public SetFactoryNameTypePair(string setFactoryName, Type type)
			{
				this.setFactoryName = setFactoryName;
				this.type = type;
				base..ctor();
			}

			// Token: 0x17000942 RID: 2370
			// (get) Token: 0x06005C9B RID: 23707 RVA: 0x006C3285 File Offset: 0x006C1485
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof(SetFactory.SetFactoryNameTypePair);
				}
			}

			// Token: 0x17000943 RID: 2371
			// (get) Token: 0x06005C9C RID: 23708 RVA: 0x006C3291 File Offset: 0x006C1491
			// (set) Token: 0x06005C9D RID: 23709 RVA: 0x006C3299 File Offset: 0x006C1499
			public string setFactoryName { get; set; }

			// Token: 0x17000944 RID: 2372
			// (get) Token: 0x06005C9E RID: 23710 RVA: 0x006C32A2 File Offset: 0x006C14A2
			// (set) Token: 0x06005C9F RID: 23711 RVA: 0x006C32AA File Offset: 0x006C14AA
			public Type type { get; set; }

			// Token: 0x06005CA0 RID: 23712 RVA: 0x006C32B4 File Offset: 0x006C14B4
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("SetFactoryNameTypePair");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06005CA1 RID: 23713 RVA: 0x006C3300 File Offset: 0x006C1500
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("setFactoryName = ");
				builder.Append(this.setFactoryName);
				builder.Append(", type = ");
				builder.Append(this.type);
				return true;
			}

			// Token: 0x06005CA2 RID: 23714 RVA: 0x006C333A File Offset: 0x006C153A
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(SetFactory.SetFactoryNameTypePair left, SetFactory.SetFactoryNameTypePair right)
			{
				return !(left == right);
			}

			// Token: 0x06005CA3 RID: 23715 RVA: 0x006C3346 File Offset: 0x006C1546
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(SetFactory.SetFactoryNameTypePair left, SetFactory.SetFactoryNameTypePair right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x06005CA4 RID: 23716 RVA: 0x006C335A File Offset: 0x006C155A
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<setFactoryName>k__BackingField)) * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(this.<type>k__BackingField);
			}

			// Token: 0x06005CA5 RID: 23717 RVA: 0x006C339A File Offset: 0x006C159A
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as SetFactory.SetFactoryNameTypePair);
			}

			// Token: 0x06005CA6 RID: 23718 RVA: 0x006C33A8 File Offset: 0x006C15A8
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(SetFactory.SetFactoryNameTypePair other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<setFactoryName>k__BackingField, other.<setFactoryName>k__BackingField) && EqualityComparer<Type>.Default.Equals(this.<type>k__BackingField, other.<type>k__BackingField));
			}

			// Token: 0x06005CA8 RID: 23720 RVA: 0x006C3409 File Offset: 0x006C1609
			[CompilerGenerated]
			protected SetFactoryNameTypePair([Nullable(1)] SetFactory.SetFactoryNameTypePair original)
			{
				this.setFactoryName = original.<setFactoryName>k__BackingField;
				this.type = original.<type>k__BackingField;
			}

			// Token: 0x06005CA9 RID: 23721 RVA: 0x006C3429 File Offset: 0x006C1629
			[CompilerGenerated]
			public void Deconstruct(out string setFactoryName, out Type type)
			{
				setFactoryName = this.setFactoryName;
				type = this.type;
			}
		}
	}
}
