using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Terraria.ModLoader
{
	// Token: 0x02000200 RID: 512
	public class TopoSort<T>
	{
		// Token: 0x060027CC RID: 10188 RVA: 0x00508EB0 File Offset: 0x005070B0
		public TopoSort(IEnumerable<T> elements, Func<T, IEnumerable<T>> dependencies = null, Func<T, IEnumerable<T>> dependents = null)
		{
			this.list = elements.ToList<T>().AsReadOnly();
			if (dependencies != null)
			{
				foreach (T t in this.list)
				{
					foreach (T dependency in dependencies(t))
					{
						this.AddEntry(dependency, t);
					}
				}
			}
			if (dependents != null)
			{
				foreach (T t2 in this.list)
				{
					foreach (T dependent in dependents(t2))
					{
						this.AddEntry(t2, dependent);
					}
				}
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x00508FE0 File Offset: 0x005071E0
		public void AddEntry(T dependency, T dependent)
		{
			List<T> list;
			if (!this.dependencyDict.TryGetValue(dependent, out list))
			{
				list = (this.dependencyDict[dependent] = new List<T>());
			}
			list.Add(dependency);
			if (!this.dependentDict.TryGetValue(dependency, out list))
			{
				list = (this.dependentDict[dependency] = new List<T>());
			}
			list.Add(dependent);
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x00509044 File Offset: 0x00507244
		private static void BuildSet(T t, IDictionary<T, List<T>> dict, ISet<T> set)
		{
			List<T> list;
			if (!dict.TryGetValue(t, out list))
			{
				return;
			}
			foreach (T entry in dict[t])
			{
				if (set.Add(entry))
				{
					TopoSort<T>.BuildSet(entry, dict, set);
				}
			}
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x005090B0 File Offset: 0x005072B0
		public List<T> Dependencies(T t)
		{
			List<T> list;
			if (!this.dependencyDict.TryGetValue(t, out list))
			{
				return new List<T>();
			}
			return list;
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x005090D4 File Offset: 0x005072D4
		public List<T> Dependents(T t)
		{
			List<T> list;
			if (!this.dependentDict.TryGetValue(t, out list))
			{
				return new List<T>();
			}
			return list;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x005090F8 File Offset: 0x005072F8
		public ISet<T> AllDependencies(T t)
		{
			HashSet<T> set = new HashSet<T>();
			TopoSort<T>.BuildSet(t, this.dependencyDict, set);
			return set;
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x0050911C File Offset: 0x0050731C
		public ISet<T> AllDependendents(T t)
		{
			HashSet<T> set = new HashSet<T>();
			TopoSort<T>.BuildSet(t, this.dependentDict, set);
			return set;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x00509140 File Offset: 0x00507340
		public List<T> Sort()
		{
			TopoSort<T>.SortingException ex = new TopoSort<T>.SortingException();
			Stack<T> visiting = new Stack<T>();
			List<T> sorted = new List<T>();
			Action<T> Visit = null;
			Visit = delegate(T t)
			{
				if (sorted.Contains(t) || ex.set.Contains(t))
				{
					return;
				}
				visiting.Push(t);
				using (List<T>.Enumerator enumerator2 = this.Dependencies(t).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						T dependency = enumerator2.Current;
						if (visiting.Contains(dependency))
						{
							List<T> cycle = new List<T>();
							cycle.Add(dependency);
							cycle.AddRange(visiting.TakeWhile((T entry) => !EqualityComparer<T>.Default.Equals(entry, dependency)));
							cycle.Add(dependency);
							cycle.Reverse();
							ex.Add(cycle);
						}
						else
						{
							Visit(dependency);
						}
					}
				}
				visiting.Pop();
				sorted.Add(t);
			};
			foreach (T t2 in this.list)
			{
				Visit(t2);
			}
			if (ex.set.Count > 0)
			{
				throw ex;
			}
			return sorted;
		}

		// Token: 0x04001933 RID: 6451
		public readonly ReadOnlyCollection<T> list;

		// Token: 0x04001934 RID: 6452
		private IDictionary<T, List<T>> dependencyDict = new Dictionary<T, List<T>>();

		// Token: 0x04001935 RID: 6453
		private IDictionary<T, List<T>> dependentDict = new Dictionary<T, List<T>>();

		// Token: 0x020009DD RID: 2525
		public class SortingException : Exception
		{
			// Token: 0x06005687 RID: 22151 RVA: 0x0069C5D4 File Offset: 0x0069A7D4
			private string CycleToString(List<T> cycle)
			{
				return "Dependency Cycle: " + string.Join<T>(" -> ", cycle);
			}

			// Token: 0x170008F7 RID: 2295
			// (get) Token: 0x06005688 RID: 22152 RVA: 0x0069C5EB File Offset: 0x0069A7EB
			public override string Message
			{
				get
				{
					return string.Join(Environment.NewLine, this.cycles.Select(new Func<List<T>, string>(this.CycleToString)));
				}
			}

			// Token: 0x06005689 RID: 22153 RVA: 0x0069C610 File Offset: 0x0069A810
			public void Add(List<T> cycle)
			{
				this.cycles.Add(cycle);
				foreach (T e in cycle)
				{
					this.set.Add(e);
				}
			}

			// Token: 0x04006BC6 RID: 27590
			public ISet<T> set = new HashSet<T>();

			// Token: 0x04006BC7 RID: 27591
			public IList<List<T>> cycles = new List<List<T>>();
		}
	}
}
