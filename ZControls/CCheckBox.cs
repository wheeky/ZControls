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
    public partial class CCheckBox : UserControl,IZwjDefControl
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

        public CCheckBox()
        {
            InitializeComponent();
            checkBox1.CheckedChanged += delegate { RaiseValueChange(); };
        }

        [Browsable(true)]
        [Description("设置勾选框的值")]
        public bool? Value
        {
            get
            {
                return checkBox1.Checked;
            }
            set
            {
                checkBox1.Checked = value ?? false;
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

        public void ValueFor<TEntity>(Expression<Func<TEntity, dynamic>> selectField, bool? fieldValue) where TEntity : class
        {
            var fieldInfo = General.GetPropertyInfo(selectField);
            this.Label = General.GetDisplayName(fieldInfo);
            this.Value = fieldValue;
        }


    }
}
