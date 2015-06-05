using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Linq.Expressions;
using Zwj.TEMS.Common;
using TEMS.Service;

namespace TEMS.Controls
{
    public partial class CDropDownBox : UserControl, IZwjDefControl
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

        public CDropDownBox()
        {
            InitializeComponent();
            comboBox1.SelectedValueChanged += delegate { RaiseValueChange(); };
        }

        [Browsable(true)]
        [Description("设置下拉框的值")]
        public object Value
        {
            get
            {
                return comboBox1.SelectedValue;
            }
            set
            {
                if (value != null && comboBox1.Items.Count <= 0)
                {
                    throw new ArgumentException("设置下拉框的值前请先设置其选项!", "Value");
                }
                comboBox1.SelectedValue = value;
            }
        }

        [Browsable(true)]
        [Description("设置下拉框的选项")]
        public IList Items
        {
            get
            {
                return comboBox1.Items;
            }
            set
            {
                object[] items = new object[value.Count];
                value.CopyTo(items, 0);
                comboBox1.Items.AddRange(items);
            }
        }

        [Browsable(true)]
        [Description("设置下拉框样式")]
        public ComboBoxStyle DropDownStyle
        {
            get
            {
                return comboBox1.DropDownStyle;
            }
            set
            {
                comboBox1.DropDownStyle = value;
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

        public void ValueFor<TEntity>(Expression<Func<TEntity, dynamic>> selectField, object fieldValue, IList options, string textMember = null, string valueMember = null, ComboBoxStyle style = ComboBoxStyle.DropDownList) where TEntity : class
        {
            var fieldInfo = General.GetPropertyInfo(selectField);
            this.Label = General.GetDisplayName(fieldInfo);
            comboBox1.DataSource = options;
            if (textMember != null && valueMember != null)
            {
                comboBox1.DisplayMember = textMember;
                comboBox1.ValueMember = valueMember;
            }
            comboBox1.SelectedValue = fieldValue;
            this.DropDownStyle = style;
        }

        public void DataBindToItems(IList<object> source, string text, string value)
        {
            Common.ComboBoxDataBind(comboBox1, source, text, value);
        }
    }
}
