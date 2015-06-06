using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zwj.TEMS.Common;
using System.Linq.Expressions;

namespace TEMS.Controls
{
    /// <summary>
    /// 日期时间选择框
    /// </summary>
    public partial class CDateTimePicker : UserControl, IZwjDefControl
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

        public CDateTimePicker()
        {
            InitializeComponent();
            dateTimePicker1.ValueChanged += delegate { RaiseValueChange(); };
        }

        [Browsable(true)]
        [Description("设置实际的值")]
        public DateTime Value
        {
            get
            {
                return dateTimePicker1.Value;
            }
            set
            {
                dateTimePicker1.Value = value;
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

        [Browsable(true)]
        [Description("设置可选最小的日期")]
        public DateTime MinDate
        {
            get
            {
                return dateTimePicker1.MinDate;
            }
            set
            {
                dateTimePicker1.MinDate = value;
            }
        }

        [Browsable(true)]
        [Description("设置可选最大的日期")]
        public DateTime MaxDate
        {
            get
            {
                return dateTimePicker1.MaxDate;
            }
            set
            {
                dateTimePicker1.MaxDate = value;
            }
        }

        [Browsable(true)]
        [Description("设置日期显示格式")]
        public DateTimePickerFormat Format
        {
            get
            {
                return dateTimePicker1.Format;
            }
            set
            {
                dateTimePicker1.Format = value;
            }
        }


        public void ValueFor<TEntity>(Expression<Func<TEntity, dynamic>> selectField, DateTime? value, DateTimePickerFormat format = DateTimePickerFormat.Long, DateTime? minDate = null, DateTime? maxDate = null) where TEntity : class
        {
            var fieldInfo = General.GetPropertyInfo(selectField);
            this.Label = General.GetDisplayName(fieldInfo);
            if (minDate != null)
            {
                this.MinDate = (DateTime)minDate;
            }
            if (maxDate != null)
            {
                this.MaxDate = (DateTime)maxDate;
            }
            this.Format = format;
            this.Value = value ?? this.MinDate;
        }

    }
}
