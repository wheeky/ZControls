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
    public partial class CDisplayBox : UserControl,IZwjDefControl
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

        public CDisplayBox()
        {
            InitializeComponent();
            textBox1.TextChanged += delegate { RaiseValueChange(); };
        }

        [Browsable(true)]
        [Description("设置显示的值")]
        public new string Text
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        [Browsable(true)]
        [Description("设置实际的值")]
        public object Value
        {
            get;
            set;
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
        [Description("设置是否允许多行")]
        public bool AllowMultiline
        {
            get
            {
                return textBox1.Multiline;
            }
            set
            {
                textBox1.Multiline = value;
                if (textBox1.Multiline)
                {
                    textBox1.ScrollBars = ScrollBars.Vertical;
                }
            }
        }

        public void ValueFor<TEntity>(Expression<Func<TEntity, dynamic>> selectField, string text, object value = null, bool allowMultiline = false) where TEntity : class
        {
            var fieldInfo = General.GetPropertyInfo(selectField);
            this.Label = General.GetDisplayName(fieldInfo);
            this.Text = text;
            if (value == null)
            {
                this.Value = this.Text;
            }
            else
            {
                this.Value = value;
            }
            this.AllowMultiline = allowMultiline;
        }

    }
}
