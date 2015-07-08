using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PatrixiaTrie {
    public class Node {

        protected List<Node> children = new List<Node>();
        protected Node parent;
        protected String symbol;
        protected List<String> translation = new List<string>();
        protected bool queryFinished = false;

        public Node() { }


        public Node(List<Node> children, Node parent, String symbol, bool finished, List<String> translation) {
            this.children = children;
            this.parent = parent;
            this.symbol = symbol;
            this.queryFinished = finished;
            this.translation = translation;
        }

        public Node getParent() {
            return parent;
        }

        public List<String> getTranslations() {
            return translation;
        }

        public void addTranslation(String translation) {
            if (translation != null)
                this.translation.Add(translation);
        }

        public void setTranslation(List<String> translation) {
            this.translation = translation;
        }

        private Node findChild(char c) {
            if (this.children == null)
                return null;
            foreach (Node child in this.children) {
                if (child.getSymbol().Length != 0)
                    if (child.getSymbol()[0] == c) {
                        return child;
                    }
            }
            return null;
        }

        public ArrayList getAllPossibleQueries() {
            String preQuery = this.getQueryBottomUp();
            ArrayList queryList = new ArrayList();
            foreach (Node child in this.children) {
                if (!child.getIsQueryFinished())
                    continue;
                foreach (String postQuery in child.getQueryTopDown()) {
                    String query = preQuery + postQuery;
                    queryList.Add(query);
                }
            }
            return queryList;
        }

        public ArrayList getQueryTopDown() {
            ArrayList queryList = new ArrayList();
            String query = this.symbol;
            if (this.queryFinished) {
                queryList.Add(query);
            }
            if (this.children.Count > 0) {
                foreach (Node child in this.children) {
                    foreach (String childQuery in child.getQueryTopDown())
                        queryList.Add(string.Concat(query, childQuery));
                }
            }
            return queryList;
        }

        public String getQueryBottomUp() {
            String query = "";
            if (this.parent != null)
                query = string.Concat(query, this.parent.getQueryBottomUp());
            query = string.Concat(query, this.symbol);
            return query;
        }

        public String getSymbol() {
            return this.symbol;
        }

        public int getSize() {
            int size = 1;
            foreach (Node child in this.children) {
                size += child.getSize();
            }
            return size;
        }

        public void addChild(Node child) {
            child.setParent(this);
            this.children.Add(child);
        }

        public void setParent(Node parent) {
            this.parent = parent;
        }

        public void setSymbol(String symbol) {
            this.symbol = symbol;
        }

        public void setIsFinished(bool isIt) {
            this.queryFinished = isIt;
        }

        public bool getIsQueryFinished() {
            return this.queryFinished;
        }

        public List<Node> getChildren() {
            return this.children;
        }

        public void setChildren(List<Node> children) {
            this.children = children;
        }

        public void removeChildBySymbol(String symbol) {
            List<Node> nodes = this.children;
            nodes.RemoveAll(node => node.getSymbol().Equals(symbol));
        }

        public Node goToMatchingNode(String query, bool exactMatch) {
            if (query.Count() > 0) {
                Node child = this.findChild(query.ToString()[0]);
                if (child == null) {
                    return null;
                }
                // filter wrong nodes
                if (!query.StartsWith(child.getSymbol())) {
                    if (exactMatch)
                        return null;
                    else return child;
                }
                query = query.Substring(child.getSymbol().Count());
                Node nextChild = child.goToMatchingNode(query);
                if (nextChild == null)
                    return child;
                else
                    return nextChild;
            }
            return null;
        }

        public Node goToMatchingNode(String query) {
            return this.goToMatchingNode(query, true);
        }
    }
}
