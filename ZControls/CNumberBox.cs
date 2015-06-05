using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;
using Zwj.TEMS.Common;

namespace TEMS.Controls
{
    public partial class CNumberBox : UserControl,IZwjDefControl
    {
        [Description("当值改变时触发该事件")]
        public event EventHandler OnValueChange;

        private void RaiseValueChange()
        {
            if (this.OnValueChange != null)
            {
                this.OnValueChange(this, null);
            }
        }

        public CNumberBox()
        {
            InitializeComponent();
            this.Minimum = decimal.MinValue;
            this.Maximum = decimal.MaxValue;
            numericUpDown1.ValueChanged += delegate { RaiseValueChange(); };
        }

        [Browsable(true)]
        [Description("设置数字框的值")]
        public decimal Value
        {
            get
            {
                return numericUpDown1.Value;
            }
            set
            {
                numericUpDown1.Value = value;
                
            }
        }

        [Browsable(true)]
        [Description("设置数字的小数点位数")]
        public int DecimalPlaces
        {
            get
            {
                return numericUpDown1.DecimalPlaces;
            }
            set
            {
                numericUpDown1.DecimalPlaces = value;
            }
        }

        [Browsable(true)]
        [Description("设置允许的最小值")]
        public decimal Minimum
        {
            get
            {
                return numericUpDown1.Minimum;
            }
            set
            {
                numericUpDown1.Minimum = value;
            }
        }

        [Browsable(true)]
        [Description("设置允许的最大值")]
        public decimal Maximum
        {
            get
            {
                return numericUpDown1.Maximum;
            }
            set
            {
                numericUpDown1.Maximum = value;
            }
        }


        [Browsable(true)]
        [Description("设置标签的值")]
        public string Label
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        public void ValueFor<TEntity>(Expression<Func<TEntity, dynamic>> selectField, decimal fieldValue, int decimalPlaces = 0, decimal minimum = decimal.MinValue, decimal maximum=decimal.MaxValue) where TEntity : class
        {
            var fieldInfo = General.GetPropertyInfo(selectField);
            this.Label = General.GetDisplayName(fieldInfo);
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Value = fieldValue;
            this.DecimalPlaces = decimalPlaces;
        }
    }
}
