using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000073 RID: 115
	public class GenerationProgress
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0049F720 File Offset: 0x0049D920
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x0049F738 File Offset: 0x0049D938
		public string Message
		{
			get
			{
				return string.Format(this._message, this.Value);
			}
			set
			{
				this._message = value.Replace("%", "{0:0.0%}");
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0049F750 File Offset: 0x0049D950
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x0049F758 File Offset: 0x0049D958
		public double Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = Utils.Clamp<double>(value, 0.0, 1.0);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0049F778 File Offset: 0x0049D978
		public double TotalProgress
		{
			get
			{
				if (this.TotalWeight == 0.0)
				{
					return 0.0;
				}
				return (this.Value * this.CurrentPassWeight + this._totalProgress) / this.TotalWeight;
			}
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0049F7B0 File Offset: 0x0049D9B0
		public void Set(double value)
		{
			this.Value = value;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0049F7B9 File Offset: 0x0049D9B9
		public void Start(double weight)
		{
			this.CurrentPassWeight = weight;
			this._value = 0.0;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0049F7D1 File Offset: 0x0049D9D1
		public void End()
		{
			this._totalProgress += this.CurrentPassWeight;
			this._value = 0.0;
		}

		// Token: 0x04000FE5 RID: 4069
		private string _message = "";

		// Token: 0x04000FE6 RID: 4070
		private double _value;

		// Token: 0x04000FE7 RID: 4071
		private double _totalProgress;

		// Token: 0x04000FE8 RID: 4072
		public double TotalWeight;

		// Token: 0x04000FE9 RID: 4073
		public double CurrentPassWeight = 1.0;
	}
}
