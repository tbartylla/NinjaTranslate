using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaTranslate {
    public partial class splashForm : Form {
        public splashForm() {
            InitializeComponent();
            progressBar1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5487a6"); // blue
            progressBar1.Style = ProgressBarStyle.Continuous;
        }

        public ProgressBar getProgressBar(){
            return progressBar1;
        }
    }
}
