﻿using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace NinjaTranslate {
    public partial class HistoryForm : Form {
        DataGridViewColumn deleteColumn = new DataGridViewTextBoxColumn();
        String xmlFilePath = "ninjaT.xml";
        // TODO: Read the file path from a settings-class;

        public HistoryForm() {
            InitializeComponent();
            this.dataGridView1.DefaultCellStyle.WrapMode =
            DataGridViewTriState.True;
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);

            RefreshGridView(this, null);

            deleteColumn.Name = "delete_column";
            deleteColumn.HeaderText = "delete";
            deleteColumn.Width = 20;
            deleteColumn.MinimumWidth = 20;
            deleteColumn.Resizable = DataGridViewTriState.False;
        }

        /// <summary>
        /// paints the cells; makes sure we get that shiny images.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
            if (e.ColumnIndex == dataGridView1.Columns["delete_column"].Index && e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1) {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, true);
                // TODO: change path
                Image Img = Image.FromFile(@"C:\Users\Toni\documents\visual studio 2013\Projects\GridProto\GridProto\delete.png");
                e.Graphics.DrawImage(
                    Img,
                    e.CellBounds.X,
                    e.CellBounds.Y,
                    e.CellBounds.Height,
                    e.CellBounds.Height);
                e.PaintContent(e.CellBounds);
            }
            // hides the column header for the delete button column
            if (e.ColumnIndex == dataGridView1.Columns["delete_column"].Index && e.RowIndex == -1) {
                e.Handled = true;
            }
        } 

        /// <summary>
        /// reloads the XML file to shows the current entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshGridView(object sender, EventArgs e) {
            try {
                CreateXMLFile();
                XmlDocument xmlDoc = new XmlDocument();

                DataSet ds = new DataSet();
                ds.ReadXml(xmlFilePath);
                xmlDoc.LoadXml(ds.GetXml());
                if (CountXMLElements(xmlDoc) == 0) {
                    MessageBox.Show("There's nothing to show. No translations saved.");
                } else {
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "translation";
                    if (dataGridView1.Columns["delete_column"] == null) {
                        dataGridView1.Columns.Insert(dataGridView1.ColumnCount, deleteColumn);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// handles the clicks on certain cells.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == dataGridView1.Columns["delete_column"].Index) {
                if (e.RowIndex < dataGridView1.Rows.Count - 1 && e.RowIndex >= 0 && dataGridView1.Rows.Count > 1) { // minus 1, so you won't destroy everything by deleting the last (empty) row.
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    RemoveFromXML(e.RowIndex);
                }
            }
        }

        /// <summary>
        /// Removes an element from the xml-file
        /// </summary>
        /// <param name="nodeRowIndex"></param>
        private void RemoveFromXML(int nodeRowIndex) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            XmlNode parent = xmlDoc.SelectSingleNode("/dictionary");

            if (parent != null) {
                parent.RemoveChild(parent.ChildNodes.Item(nodeRowIndex));
                xmlDoc.Save(xmlFilePath);
            }
        }

        /// <summary>
        /// Creates a new XML if it is non existent.
        /// </summary>
        private void CreateXMLFile() {
            if (!File.Exists(xmlFilePath)) {
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlComment xmlComment = xmlDoc.CreateComment("This XML-File has been automatically generated by NinjaTranslate");
                XmlNode productsNode = xmlDoc.CreateElement("dictionary");
                xmlDoc.AppendChild(docNode);
                xmlDoc.AppendChild(xmlComment);
                xmlDoc.AppendChild(productsNode);

                xmlDoc.Save(xmlFilePath);
            } 
        }

        public int CountXMLElements(XmlDocument xmlDoc) {
            return xmlDoc.GetElementsByTagName("translation").Count;
        }

        /// <summary>
        /// Adds an element to the XML File.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddXMLElement(String from, String to) {
            CreateXMLFile();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            XmlNode rootNode = xmlDoc.DocumentElement;
            XmlElement xmlTranslation = xmlDoc.CreateElement("translation");
            XmlElement xmlFrom = xmlDoc.CreateElement("from");
            XmlElement xmlTo = xmlDoc.CreateElement("to");
            XmlElement xmlGenus = xmlDoc.CreateElement("genus");
            XmlElement xmlWordtype = xmlDoc.CreateElement("wordtype");
            XmlElement xmlDate = xmlDoc.CreateElement("date");

            xmlTranslation.InnerText = "Item";
            xmlFrom.InnerText = from;
            xmlTo.InnerText = to;
            xmlGenus.InnerText = "genus";
            xmlWordtype.InnerText = "wordtype";
            xmlDate.InnerText = "Date";


            xmlTranslation.AppendChild(xmlFrom);
            xmlTranslation.AppendChild(xmlTo);
            xmlTranslation.AppendChild(xmlGenus);
            xmlTranslation.AppendChild(xmlWordtype);
            xmlTranslation.AppendChild(xmlDate);
            rootNode.AppendChild(xmlTranslation);

            xmlDoc.Save(xmlFilePath);

            RefreshGridView(this, null);
        }
    }
}
