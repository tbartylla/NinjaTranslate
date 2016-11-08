using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

using PatrixiaTrie;

namespace NinjaTranslate {
    class TranslationCenter : ITranslationService {

        /// <summary>
        /// tree used to translate a given input
        /// </summary>
        private PatriciaTrie translationTree;

        /// <summary>
        /// filter to be applied to the input string. TODO Change when using a better filter pattern
        /// </summary>
        private Normalizer filter;

        public void SetFilter(Normalizer filter) {
            this.filter = filter;
        }

        public void SetTranslationTree(PatriciaTrie transTree) {
            this.translationTree = transTree;
        }

        public String Translate(string input) {
            if (input == null)
                return "no processible content";
            input = this.ApplyFilter(input);
            Node treeNode = this.translationTree.processQuery(input);
            if (treeNode != null) {
                String translations = "";
                foreach (string translation in treeNode.getTranslations()) {
                    translations += "\t" + translation + Environment.NewLine;
                }

                return "word: " + Environment.NewLine + "\t" + treeNode.getQueryBottomUp() + Environment.NewLine + "translations: " + Environment.NewLine + translations;
            }
            return "Couldn't find the word '" + input + "' in the dictionary.";
        }        

        protected String ApplyFilter(String input) {
            return this.filter.normalizeSearchInput(input);
        }
    }
}
