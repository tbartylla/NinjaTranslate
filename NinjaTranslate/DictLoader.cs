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
    class DictLoader {
        /// <summary>
        /// Any form to report progress to user
        /// </summary>
        SplashForm sF;

        /// <summary>
        /// TODO
        /// This is a temporary solution to be replaced with a more advanced filter system in future.
        /// </summary>
        Normalizer filter;

        public void SetSplashForm(SplashForm form) {
            this.sF = form;
        }

        public void SetFilter(Normalizer filter) {
            this.filter = filter;
        }

        /// <summary>
        /// Creates a PatrixiaTrie that will be used for translation etc.
        /// </summary>
        /// <param name="fileToLoad">Path to a tvs source file to be parsed.</param>
        /// <param name="serializedFile">Path to a serialized version of the fileToLoad, will be prefered when found</param>
        /// <returns></returns>
        public PatriciaTrie LoadDictData(String fileToLoad, String serializedFile) {
            PatriciaTrie pTrie = new PatriciaTrie();
            //serialized file found, use this one
            if (File.Exists(serializedFile)) {
                //reset status informations of FileMapper for next run
                PatrixiaTrie.PatrixiaTrieFileMapper.ResetDeserializationProgress();

                //load via backgroundWorker, report progress to splashform
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                //the following handler will be executed asynchronous
                bw.DoWork += new DoWorkEventHandler(
                    delegate(object sender, DoWorkEventArgs args) {
                        byte[] data = File.ReadAllBytes(serializedFile);
                        pTrie.setRootNode(PatrixiaTrie.PatrixiaTrieFileMapper.Deserialize(data));
                    });
                bw.RunWorkerAsync();

                //show SplashForm
                if (sF != null)
                    sF.Show();
                //update progressbar while loading
                while (PatrixiaTrie.PatrixiaTrieFileMapper.deserializationProgress < 100) {
                    sF.getProgressBar().Value = PatrixiaTrie.PatrixiaTrieFileMapper.deserializationProgress;
                    Thread.Sleep(100);
                }

                //hide SplashForm
                if (sF != null)
                    sF.Close();
            }
            //no serialized file found, try reading the raw file
            else if (File.Exists(fileToLoad)) {
                using (StreamReader sr = new StreamReader(fileToLoad)) {
                    string line = sr.ReadLine();
                    int counter = 1;
                    int lineCount = File.ReadLines(@fileToLoad).Count();
                    string[] wordArray;
                    pTrie.init("");

                    if (sF != null)
                        sF.Show();
                    
                    while (line != null) {
                        wordArray = line.Split('\t');
                        if (wordArray.Count() > 1) {
                            String word = wordArray[0];
                            String translation = wordArray[1];

                            //TODO pretty magic, this number, maybe sth with lineCount in it?
                            if (counter % (10000) == 0) {
                                sF.getProgressBar().Value = (int)((counter * 100) / lineCount); //sets value of progressbar
                            }
                            //apply filter
                            String filteredWord = this.ApplyFilters(word);
                            if (filteredWord != null) {
                                pTrie.insertQuery(filteredWord, translation);
                            }
                            counter++;
                        }
                        line = sr.ReadLine();
                    }

                    if (sF != null)
                        sF.Close();
     
                    //serialize tree for next start
                    byte[] serialization = PatrixiaTrie.PatrixiaTrieFileMapper.Serialize(pTrie.getRootNode());
                    SaveData(serializedFile, serialization);
                }
            }

            return pTrie;
        }

        protected String ApplyFilters(string word) {
            return this.filter.normalize(word);
        }


        protected bool SaveData(string fileName, byte[] data) {
            BinaryWriter Writer = null;
            
            try {
                Writer = new BinaryWriter(File.OpenWrite(fileName));              
                Writer.Write(data);
                Writer.Flush();
                Writer.Close();
            } catch (Exception e){
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }
    }
}
