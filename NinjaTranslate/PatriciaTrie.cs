using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using PatrixiaTrie;


namespace NinjaTranslate {
	[Serializable()]
	class PatriciaTrie : ISerializable {

		protected Node root;

		public PatriciaTrie() {	}
	
		public PatriciaTrie(Node root) {
			this.root = root;
		}
	
		public void init(String query) {
			this.root = new Node();
			this.root.setParent(null);
			this.root.setSymbol(query);
		}
	
		public bool insertQuery(String query) {
			return this.insertQuery(query, "");
		}

		public Node getRootNode() {
			return this.root;
		}

        public void setRootNode(Node node) {
            this.root = node;
        }

		public bool insertQuery(String query, String translation) {

			Node lowestMatchingNode = this.root.goToMatchingNode(query);
			Node childWithSamePrefix = null;
			if (lowestMatchingNode == null) {
				lowestMatchingNode = this.root;
			}
		

			// first case: exactly match with node
			if (lowestMatchingNode.getQueryBottomUp().Equals(query)) {
				lowestMatchingNode.setIsFinished(true);
				lowestMatchingNode.addTranslation(translation);
				return true;
			}
		
			//at this point "lowestMatchingNode" doesnt equal the query
			String newQueryPart = query.Substring(lowestMatchingNode.getQueryBottomUp().Length, query.Length-lowestMatchingNode.getQueryBottomUp().Length);

			//from this point: Query has a prefix that exists in the tree
			bool matchingPrefix = false;

			foreach (Node child in lowestMatchingNode.getChildren()) {
				if (child.getSymbol().StartsWith(newQueryPart[0].ToString())) {
					childWithSamePrefix = child;
					matchingPrefix = true;
				}
			}

			// if there is no child with a matching prefix, we simply add a new node
			if (!matchingPrefix) {
				Node child = new Node();
				child.setIsFinished(true);
				child.setSymbol(newQueryPart);
				child.setParent(lowestMatchingNode);
				child.addTranslation(translation);
				lowestMatchingNode.addChild(child);		
				return true;
			}

			// if there is a child with a matching prefix, create a new "prefix node"
			// first detect prefix
			String prefix = "";
			int upperBound;
			bool PrefixIsQuery = false; 
			if (newQueryPart.Length > childWithSamePrefix.getSymbol().Length) {
				upperBound = childWithSamePrefix.getSymbol().Length;		
				if (childWithSamePrefix.getSymbol().StartsWith(newQueryPart))
					PrefixIsQuery = true;
			}
			else {
				upperBound = newQueryPart.Length;
			}
		
			// creates the prefix (= substring that query and another node have in common).
			for (int i = 0; i < upperBound; i++) {
				if (newQueryPart[i] != childWithSamePrefix.getSymbol()[i]) {
					prefix = newQueryPart.Substring(0, i);
					break;
				}
                //if at this point no prefix was created, we need a special case to copy all chars (previos case was for 
                //all but the last char, since we checked for differences)
                if ((i + 1) == upperBound) 
                    prefix = newQueryPart.Substring(0, (i + 1));
			}

			// store prefix node
			Node prefixNode = new Node();
			prefixNode.setSymbol(prefix);
			if (PrefixIsQuery) {
				prefixNode.setIsFinished(true);
				prefixNode.addTranslation(translation);
			} else
				prefixNode.setIsFinished(false);
			prefixNode.setParent(lowestMatchingNode);
			lowestMatchingNode.addChild(prefixNode);
		
			// add new node from query to new prefix node
			if (newQueryPart.Substring(prefix.Length).Length > 0) {
				Node newNode = new Node();
				newNode.setSymbol(newQueryPart.Substring(prefix.Length));
				newNode.setIsFinished(true);
				newNode.addTranslation(translation);
				newNode.setParent(prefixNode);
				prefixNode.addChild(newNode);
			}

			// add rest of old node to prefix node
			if (childWithSamePrefix.getSymbol().Substring(prefix.Length)
					.Length > 0) {
				Node newNode2 = new Node();
				newNode2.setSymbol(childWithSamePrefix.getSymbol().Substring(
						prefix.Length));
				newNode2.setIsFinished(true);
				newNode2.setTranslation(childWithSamePrefix.getTranslations());
				newNode2.setParent(prefixNode);
				prefixNode.addChild(newNode2);
				if (childWithSamePrefix.getChildren() != null)
					newNode2.setChildren(childWithSamePrefix.getChildren());
			}

			// remove "old" query node from parent, add to prefix node
			if (childWithSamePrefix != null)
				lowestMatchingNode.removeChildBySymbol(childWithSamePrefix
						.getSymbol());
				lowestMatchingNode = null;

			return true;
		}
	
		public int getSize() {
			if (this.root == null)
				return 0;
		
			return this.root.getSize();
		}

        //TODO fix for "he" - "hehehehe"
		public Node processQuery(String query) {
			Node node = this.root.goToMatchingNode(query, false);
			if(node != null)
			    if (node.getIsQueryFinished())
				    return node;
			return null;
		}
	
		public void printGraph(int level, Node node){ 
			for (int i = 0; i < level; i++)
				Console.Write("#");
			Console.WriteLine(node.getSymbol() + " - ");
			foreach (Node child in node.getChildren())
				this.printGraph(level + 1, child);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context){
			// AddValue method specifies serialized values.
			info.AddValue("Tree", this.root, typeof(Node));
		}

		// The special constructor is used to deserialize values.
		public PatriciaTrie(SerializationInfo info, StreamingContext context){
			// Reset the property value using the GetValue method.
			this.root = (Node) info.GetValue("Tree", typeof(Node));
		}

	}
}
