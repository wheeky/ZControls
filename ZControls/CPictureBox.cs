using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TEMS.Service;
using System.Linq.Expressions;
using Zwj.TEMS.Common;

namespace TEMS.Controls
{
    public partial class CPictureBox : UserControl, IZwjDefControl
    {
        private byte[] imageBytes = null;
        private string typeFilter = "图片格式|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.icon";
        private Size imageMaxSize = new Size(100, 100);

        [Description("当值改变时触发该事件")]
        public event EventHandler OnValueChange;

        private void RaiseValueChange()
        {
            if (this.OnValueChange != null)
            {
                this.OnValueChange(this, null);
            }
        }

        public CPictureBox()
        {
            InitializeComponent();
            this.Value = null;

        }

        [Browsable(true)]
        [Description("设置文本框的值")]
        public byte[] Value
        {
            get
            {
                return imageBytes;
            }
            set
            {
                Image oldImage = pictureBox1.Image;
                imageBytes = value;
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    pictureBox1.Image = ImageHelper.BytesToImage(imageBytes);
                    button2.Enabled = true;
                }
                else
                {
                    pictureBox1.Image = null;
                    button2.Enabled = false;
                }
                if (oldImage != pictureBox1.Image)
                {
                    RaiseValueChange();
                }
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
        [Description("设置上传图片可选类型")]
        public string TypeFilter
        {
            get
            {
                return typeFilter;
            }
            set
            {
                typeFilter = value;
            }
        }

        [Browsable(true)]
        [Description("设置上传图片最大规格")]
        public Size ImageMaxSize
        {
            get
            {
                return imageMaxSize;
            }
            set
            {
                imageMaxSize = value;
            }
        }

        public void ValueFor<TEntity>(Expression<Func<TEntity, dynamic>> selectField, byte[] fieldValue, string typeFilter = null, Size? imageMaxSize = null) where TEntity : class
        {
            var fieldInfo = General.GetPropertyInfo(selectField);
            this.Label = General.GetDisplayName(fieldInfo);
            this.Value = fieldValue;
            if (!string.IsNullOrEmpty(typeFilter))
            {
                this.TypeFilter = typeFilter;
            }

            if (imageMaxSize != null)
            {
                this.ImageMaxSize = (Size)imageMaxSize;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = typeFilter;
            openFileDlg.FilterIndex = 0;
            openFileDlg.InitialDirectory = Common.DesktopDirectory;
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDlg.FileName);
                if (img.Size.Width > imageMaxSize.Width || img.Size.Height > imageMaxSize.Height)
                {
                    throw new Exception(string.Format("上传的图片规格超过最大限制(宽度：{0}px;高度：{1}px)", imageMaxSize.Width, imageMaxSize.Height));
                }
                this.Value = ImageHelper.ImageToBytes(img);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Value = null;
        }


    }
}
