﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaTranslate.Resources.Forms {
    public partial class AddDictForm : Form {
        MainWindow mwf;

        public AddDictForm(MainWindow mwf) {
            this.mwf = mwf;
            InitializeComponent();
        }

        private void btn_browse_dict_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files|*.txt";
            openFileDialog1.Title = "Select a Dictionary";

            // Show the Dialog.
            // If the user clicked OK in the dialog then paste the path to its textbox.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                pathTextBox.Text = openFileDialog1.FileName;
            }
        }

        private void addButton_Click(object sender, EventArgs e) {
            if (mwf.addDictionary(this.keyTextBox.Text, this.pathTextBox.Text))
                this.Close();
            else
                System.Windows.MessageBox.Show("This name seems to be not available.", "Error");
        }
    }
}
