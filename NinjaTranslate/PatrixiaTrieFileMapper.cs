//Serialization for a PatriciaTrie consisting of Node-objects.
//created: 23.06.2015
//author: Patrick Liedtke, p.liedtke1990@gmail.com
//version: 0.5

using NinjaTranslate;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PatrixiaTrie {

    /*
     * 
     * */
    /// <summary>
    /// Contains methods to serialize a tree of Node-objects to an array of bytes, and to deserialize it vice versa. 
    /// </summary>
    /// <remarks>
    /// Restrictions: 
    /// Maximum 255 translations per node
    /// Maximum 2^32 nodes per tree
    /// Maximum 255 children per node
    /// </remarks>
    /// <version>0.5</version>
    public class PatrixiaTrieFileMapper {
        public const int DEFAULT_NODE_ARRAY_SIZE_INIT = 40000000;
        public const int DEFAULT_NODE_ARRAY_SIZE_EXTEND = 100000;

        private static int deserilizationTotal = 1;
        private static int deserilizationDone = 0;

        private PatrixiaTrieFileMapper() {
        }
        /// <summary>
        /// Serializes the structure of the trie and for each node the following fields:
        /// queryFinished, symbol, translations
        /// </summary>
        /// <param name="tree">root Node-object</param>
        /// <returns>Serialization of the tree as a byte array</returns>
        public static byte[] Serialize(Node tree) {
            byte[] structure = GetTreeStructureAsBytes(tree);
            byte[] nodes = GetTreeNodeBytes(tree);

            byte[] resultBytes = new byte[structure.Length + nodes.Length];
            structure.CopyTo(resultBytes, 0);
            nodes.CopyTo(resultBytes, structure.Length);

            return resultBytes;
        }

        /// <summary>
        /// Creates a trie from a given serialization. Structure of the trie will be restored and for each node the following fields:
        /// queryFinished, symbol, translations
        /// </summary>
        /// <param name="bytes">the serialization of a trie</param>
        /// <returns></returns>
        public static Node Deserialize(byte[] bytes) {
            //get size of structure part
            byte[] structureSize = new byte[4];
            Array.Copy(bytes, 0, structureSize, 0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(structureSize);
            int structureLength = BitConverter.ToInt32(structureSize, 0);
            //tree structure informations
            byte[] structure = new byte[structureLength];
            Array.Copy(bytes, 4, structure, 0, structureLength);
            //tree nodes
            byte[] nodes = new byte[bytes.Length - structureLength - 4];
            Array.Copy(bytes, structureLength + 4, nodes, 0, bytes.Length - structureLength - 4);

            deserilizationTotal = structureLength;

            Node root = new Node();
            int nodesOffset = GetNodeFromBits(nodes, ref root);
            int structureOffset = 0; //points to root children number
            SimplePriorityQueue<int, Node> nodeQueue = new SimplePriorityQueue<int, Node>();
            nodeQueue.Enqueue(0, root);
            AddChildrenFromBytes(structure, structureOffset, nodes, nodesOffset, nodeQueue, 0);

            return root;
        }

        private static void AddChildrenFromBytes(byte[] structure, int structureOffset, byte[] nodes, int nodesOffset, SimplePriorityQueue<int, Node> nodeQueue, int level) {
            Node currentNode;
            while (!nodeQueue.IsEmpty) {
                currentNode = nodeQueue.Dequeue();
                for (int i = 0; i < structure[structureOffset]; i++) {
                    Node child = new Node();
                    nodesOffset = GetNodeFromBits(nodes, ref child, nodesOffset);
                    nodeQueue.Enqueue(level + 1, child);

                    //link 
                    currentNode.addChild(child);
                }

                if (nodeQueue.HasPriorityOf(level)) {
                    structureOffset++;
                } else if (!nodeQueue.IsEmpty) {
                    structureOffset++;
                    level++;
                }

                deserilizationDone = structureOffset;
            }
        }

        /*
         * Returns a byte array that contains all informations off all nodes in a level traversal order of the given tree.
         * */
        private static byte[] GetTreeNodeBytes(Node tree) {
            SimplePriorityQueue<int, Node> queue = new SimplePriorityQueue<int, Node>();
            queue = FillTreePriorityQueue(tree, queue, 0);

            byte[] treeNodeBytes = new byte[DEFAULT_NODE_ARRAY_SIZE_INIT];
            int treeNodeBytesPointer = 0;
            int treeNodeNumber = queue.Count;
            for (int i = 0; i < treeNodeNumber; i++) {
                byte[] nodeBytes = GetNodeInformationsAsBytes(queue.Dequeue());
                //check if there is enough space to store the node bytes and extend if necessary
                if ((treeNodeBytes.Length - treeNodeBytesPointer) < nodeBytes.Length) {
                    byte[] temp = new byte[treeNodeBytes.Length + DEFAULT_NODE_ARRAY_SIZE_EXTEND];
                    treeNodeBytes.CopyTo(temp, 0);
                    treeNodeBytes = temp;
                }

                nodeBytes.CopyTo(treeNodeBytes, treeNodeBytesPointer);
                treeNodeBytesPointer += nodeBytes.Length;
            }

            return treeNodeBytes;
        }

        /*
         * Returns a byte array that contains all informations needed to recreate the node.
         * */
        private static byte[] GetNodeInformationsAsBytes(Node node) {
            //IsQueryFinished + ExtensionBit
            byte finishedExtended;
            if (node.getIsQueryFinished())
                finishedExtended = 255;
            else
                finishedExtended = 0;

            //Symbol
            byte[] symbolBytes = ComputeVariableStringBytes(node.getSymbol());

            //Translations
            byte translationCount = Convert.ToByte(node.getTranslations().Count);
            byte[] translationBytes = new byte[0]; //initializing
            foreach (String translation in node.getTranslations()) {
                byte[] singleTranslation = ComputeVariableStringBytes(translation);
                byte[] temp = new byte[translationBytes.Length + singleTranslation.Length];
                translationBytes.CopyTo(temp, 0);
                singleTranslation.CopyTo(temp, translationBytes.Length);
                translationBytes = temp;
            }

            //space for: queryFinished + encoded symbol + translation count + translations
            byte[] complete = new byte[1 + symbolBytes.Length + 1 + translationBytes.Length];
            complete[0] = finishedExtended;
            symbolBytes.CopyTo(complete, 1);
            complete[symbolBytes.Length + 1] = translationCount;
            translationBytes.CopyTo(complete, symbolBytes.Length + 2);

            return complete;
        }

        /*
         * Computes a byte array which encodes the given string at a variable length.
         * */
        private static byte[] ComputeVariableStringBytes(String toEncode) {
            //compute necessary field length (blocks of 8 bits, to be summed up)
            //storage for symbol
            Byte[] symbolStorage;
            //store symbol
            symbolStorage = GetBytesFromString(toEncode);
            int sizeOfOneExtension = 255; //no extension bit, just keep looking until there is a 0 somewhere
            int numberOfMetaDataBlocksRequired = (symbolStorage.Length / sizeOfOneExtension) + 1;
            //calculate meta data
            Byte[] metaDataBytes = new Byte[numberOfMetaDataBlocksRequired];
            int bytesCovered = 0;
            for (int i = 0; i < numberOfMetaDataBlocksRequired; i++) {
                //last extra byte 
                if (i + 1 == numberOfMetaDataBlocksRequired) {
                    int lastByteValue = symbolStorage.Length - bytesCovered;
                    metaDataBytes[i] = Convert.ToByte(lastByteValue);
                }
                    //extra byte that isnt big enough to store all meta data, so every bit will be set to 1 (max. value + cb)
                else {
                    bytesCovered += sizeOfOneExtension;
                    metaDataBytes[i] = 255;
                }
            }

            Byte[] result = new Byte[symbolStorage.Length + metaDataBytes.Length];
            metaDataBytes.CopyTo(result, 0);
            symbolStorage.CopyTo(result, metaDataBytes.Length);

            return result;
        }

        /*
         * Returns the second parameter filled with values encoded in the data bytes.
         * */
        public static int GetNodeFromBits(byte[] data, ref Node node, int offset = 0) {
            //is query finished?
            if (data[offset] == 255)
                node.setIsFinished(true);
            else
                node.setIsFinished(false);

            //symbol
            int nextBlockSize = 0;
            offset++;
            while (data[offset] == 255) {
                nextBlockSize += 255;
                offset++;
            }
            nextBlockSize += data[offset];
            offset++;

            byte[] symbol = new byte[nextBlockSize];
            System.Buffer.BlockCopy(data, offset, symbol, 0, nextBlockSize);

            node.setSymbol(GetStringFromBytes(symbol));
            //adjust offset
            offset += nextBlockSize;

            //translations
            List<String> translations = new List<string>();
            int translationCount = data[offset];
            offset++;
            for (int i = 0; i < translationCount; i++) {
                int currentTranslationSize = 0;
                while (data[offset] == 255) {
                    currentTranslationSize += 255;
                    offset++;
                }

                currentTranslationSize += data[offset];
                offset++;

                byte[] translation = new byte[currentTranslationSize];
                System.Buffer.BlockCopy(data, offset, translation, 0, currentTranslationSize);
                offset += currentTranslationSize;

                translations.Add(GetStringFromBytes(translation));
            }

            node.setTranslation(translations);

            return offset;
        }

        /*
         * Creates a byte array that represents the structure of a tree.
         * */
        public static byte[] GetTreeStructureAsBytes(Node n) {
            byte[] code;
            SimplePriorityQueue<int, Node> queue = new SimplePriorityQueue<int, Node>();
            queue = PatrixiaTrieFileMapper.FillTreePriorityQueue(n, queue, 0);
            code = PatrixiaTrieFileMapper.GetStructureBytesFromPriorityQueue(queue);
            return code;
        }

        /*
         * Fills a priority queue with all nodes. Priority per node equals its level in the tree. This actually shouldn't be
         * done this way... Recursion is no good.
         * */
        private static SimplePriorityQueue<int, Node> FillTreePriorityQueue(Node n, SimplePriorityQueue<int, Node> queue, int level) {
            queue.Enqueue(level, n);
            foreach (Node child in n.getChildren()) {
                PatrixiaTrieFileMapper.FillTreePriorityQueue(child, queue, level + 1);
            }

            return queue;
        }

        /*
         * First four bytes: Length of the structure bytes. Each following byte represents the number of child-nodes. Starting with root node.
         * */
        private static byte[] GetStructureBytesFromPriorityQueue(SimplePriorityQueue<int, Node> queue) {
            byte[] serialization = new byte[4 + queue.Count];
            int queueLength = queue.Count;

            for (int i = 0; i < queueLength; i++) {
                serialization[i + 4] = Convert.ToByte(queue.Dequeue().getChildren().Count);
            }

            //get bytes that represent queue.Count and prepend them
            byte[] intBytes = BitConverter.GetBytes(queueLength);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);
            intBytes.CopyTo(serialization, 0);

            return serialization;
        }

        private static byte[] GetBytesFromString(string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetStringFromBytes(byte[] bytes) {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static int deserializationProgress {
            get {
                if (deserilizationTotal == 0)
                    return 100;
                return (int)((deserilizationDone / (double)deserilizationTotal) * 100 + 0.5);
            }
        }
    }
}
