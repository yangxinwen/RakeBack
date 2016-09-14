using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RakeBack.Content.Common
{
    public partial class Exit : RakeBack.BaseForm
    {
        public Exit()
        {
            InitializeComponent();

            Application.Exit();
        }
    }
}
