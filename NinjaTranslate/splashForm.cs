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
        }

        public ProgressBar getProgressBar(){
            return progressBar1;
        }
    }
}
