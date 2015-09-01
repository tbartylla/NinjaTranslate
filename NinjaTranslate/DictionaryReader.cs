using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Threading;

namespace NinjaTranslate {
    class DictionaryReader {
        static PatriciaTrie tree = new PatriciaTrie();
        static Normalizer n = new Normalizer();
        private int lineCount;

        private splashForm sF = new splashForm();
        BackgroundWorker bw = new BackgroundWorker();

        /// <summary>
        /// Loads a serialized tree in the background thread.
        /// </summary>
        void Bw_DoWork(object sender, DoWorkEventArgs e) { 
            if (File.Exists("Patrix.Tree"))
                LoadData("Patrix.Tree");
        }
       
        public void readRawDictionary() {
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(Bw_DoWork);
            bw.RunWorkerAsync();
            
            sF.Show();
            if (File.Exists("Patrix.Tree")) {
                while (PatrixiaTrie.PatrixiaTrieFileMapper.deserializationProgress < 100) {
                    sF.getProgressBar().Value = PatrixiaTrie.PatrixiaTrieFileMapper.deserializationProgress;
                    Thread.Sleep(100);
                }
            } else {
                using (StreamReader sr = new StreamReader(Config.GetValue("path"))) {
                    string line = sr.ReadLine();
                    int counter = 1;
                    string[] wordArray;
                    tree.init("");

                    countLines();
                    while (line != null) {
                        wordArray = line.Split('\t');
                        if (wordArray.Count() > 1) {
                            String word = wordArray[0];
                            String translation = wordArray[1];

                            if (counter % (10000) == 0) {
                                sF.getProgressBar().Value = (int)((counter * 100) / lineCount); //sets value of progressbar
                                Console.WriteLine(counter);    
                            }
                            if (n.normalize(word) != null) {
                                tree.insertQuery(n.normalize(word), translation);
                            }
                            counter++;
                        }
                        line = sr.ReadLine();
                    }
                    sr.Close();
                    byte[] serialization = PatrixiaTrie.PatrixiaTrieFileMapper.Serialize(tree.getRootNode());
                    SaveData("Patrix.Tree", serialization);
                } 
            }
            sF.Close();
        }

        protected bool LoadData(string FileName) {
            byte[] data = File.ReadAllBytes(FileName);
            tree.setRootNode(PatrixiaTrie.PatrixiaTrieFileMapper.Deserialize(data));       
            return true;
        }


        protected bool SaveData(string FileName, byte[] Data) {
            BinaryWriter Writer = null;
            string Name = FileName;

            try {
                Writer = new BinaryWriter(File.OpenWrite(Name));              
                Writer.Write(Data);
                Writer.Flush();
                Writer.Close();
            } catch (Exception e){
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }

        private int countLines() {
            lineCount = 0;
            using (var reader = File.OpenText(@Config.GetValue("path"))) {
                while (reader.ReadLine() != null) {
                    lineCount++;
                }
            }
            return lineCount;
        }

        public int getLineCount(){
            return this.lineCount;
        }
        
        public static String translate(string input) {
            input = n.normalizeSearchInput(input);
            if (input == null)
                return "no processible content";
            if (tree.processQuery(input) != null) {
                String translations = "";
                foreach(string translation in tree.processQuery(input).getTranslations()){
                    translations += " "+translation+",";
                }
                return "word: '"+tree.processQuery(input).getQueryBottomUp()+"' translation: '"+translations+"'";
            }
            return "Couldn't find the word '" + input + "' in the dictionary.";
        }

        bool IsValidFilename(string testName){
            string strTheseAreInvalidFileNameChars = new string( System.IO.Path.GetInvalidFileNameChars() ); 
            Regex regFixFileName = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars ) + "]");
            if (regFixFileName.IsMatch(testName)) { return false; };
            return true;
        }
    }
}
