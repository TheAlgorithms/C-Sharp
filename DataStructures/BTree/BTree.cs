using System;
using System.Linq;
using static System.Diagnostics.Debug;

namespace DataStructures.BTree
{
    /// <summary>
    /// BTree. Self-balancing tree data structure that maintains sorted data and allows searches,
    /// sequential access, insertions, and deletions in logarithmic time.
    /// </summary>
    /// <remarks>
    /// Based on BTree chapter in "Introduction to Algorithms", by Thomas Cormen, Charles Leiserson, Ronald Rivest.
    /// This implementation is not thread-safe, and user must handle thread-safety.
    /// </remarks>
    /// <typeparam name="TK">Type of BTree Key.</typeparam>
    public class BTree<TK> where TK : IComparable<TK>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BTree{TK}"/> class.
        /// </summary>
        /// <param name="degree">A degree (t) has the following properties:
        /// - the root contains from 1 to 2t-1 keys
        /// - any other node contains from t-1 to 2t-1 keys.</param>
        /// <exception cref="ArgumentException">BTree degree must be at least 2.</exception>
        public BTree(int degree)
        {
            if (degree < 2)
            {
                throw new ArgumentException("BTree degree must be at least 2", "degree");
            }

            Root = new BTreeNode<TK>(degree);
            Degree = degree;
            Height = 1;
        }

        /// <summary>
        /// Gets the root element from the tree.
        /// </summary>
        public BTreeNode<TK> Root { get; private set; }

        /// <summary>
        /// Gets the degree (t) has the following properties:
        /// - the root contains from 1 to 2t-1 keys
        /// - any other node contains from t-1 to 2t-1 keys.
        /// </summary>
        public int Degree { get; private set; }

        /// <summary>
        /// Gets the distance from the root to the farthest leaf.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Searches a key in the BTree.
        /// </summary>
        /// <param name="key">Key being searched.</param>
        /// <returns>True if contains the key.</returns>
        public bool Search(TK key)
        {
            return SearchInternal(Root, key);
        }

        /// <summary>
        /// Inserts a new key in the BTree. This
        /// operation splits nodes as required to keep the BTree properties.
        /// </summary>
        /// <param name="newKey">Key to be inserted.</param>
        public void Insert(TK newKey)
        {
            // there is space in the root node
            if (!Root.HasReachedMaxEntries)
            {
                InsertNonFull(Root, newKey);
                return;
            }

            // need to create new node and have it split
            var oldRoot = Root;
            Root = new BTreeNode<TK>(Degree);
            Root.Children.Add(oldRoot);
            SplitChild(Root, 0, oldRoot);
            InsertNonFull(Root, newKey);

            Height++;
        }

        /// <summary>
        /// Deletes a key from the BTree. This operations moves keys and nodes
        /// as required to keep the BTree properties.
        /// </summary>
        /// <param name="keyToDelete">Key to be deleted.</param>
        public void Delete(TK keyToDelete)
        {
            DeleteInternal(Root, keyToDelete);

            // if root's last entry was moved to a child node, remove it
            if (Root.Entries.Count == 0 && !Root.IsLeaf)
            {
                Root = Root.Children.Single();
                Height--;
            }
        }

        /// <summary>
        /// Internal method to delete keys from the BTree.
        /// </summary>
        /// <param name="node">Node to use to start search for the key.</param>
        /// <param name="keyToDelete">Key to be deleted.</param>
        private void DeleteInternal(BTreeNode<TK> node, TK keyToDelete)
        {
            var i = node.Entries.TakeWhile(entry => keyToDelete.CompareTo(entry) > 0).Count();

            // found key in node, so delete if from it
            if (i < node.Entries.Count && node.Entries[i].CompareTo(keyToDelete) == 0)
            {
                DeleteKeyFromNode(node, keyToDelete, i);
                return;
            }

            // delete key from subtree
            if (!node.IsLeaf)
            {
                DeleteKeyFromSubtree(node, keyToDelete, i);
            }
        }

        /// <summary>
        /// Helper method that deletes a key from a subtree.
        /// </summary>
        /// <param name="parentNode">Parent node used to start search for the key.</param>
        /// <param name="keyToDelete">Key to be deleted.</param>
        /// <param name="subtreeIndexInNode">Index of subtree node in the parent node.</param>
        private void DeleteKeyFromSubtree(BTreeNode<TK> parentNode, TK keyToDelete, int subtreeIndexInNode)
        {
            var childNode = parentNode.Children[subtreeIndexInNode];

            // node has reached min # of entries, and removing any from it will break the btree property,
            // so this block makes sure that the "child" has at least "degree" # of nodes by moving an
            // entry from a sibling node or merging nodes
            if (childNode.HasReachedMinEntries)
            {
                var leftIndex = subtreeIndexInNode - 1;
                var leftSibling = subtreeIndexInNode > 0 ? parentNode.Children[leftIndex] : null;

                var rightIndex = subtreeIndexInNode + 1;
                var rightSibling = subtreeIndexInNode < parentNode.Children.Count - 1
                                                ? parentNode.Children[rightIndex]
                                                : null;

                if (leftSibling != null && leftSibling.Entries.Count > Degree - 1)
                {
                    MoveLeftNodeIntoParent(parentNode, leftSibling, subtreeIndexInNode);
                }
                else if (rightSibling != null && rightSibling.Entries.Count > Degree - 1)
                {
                    MoveRightNodeIntoParent(parentNode, rightSibling, subtreeIndexInNode);
                }
                else
                {
                    MergesSiblingIntoCurrentNode(parentNode, subtreeIndexInNode);
                }
            }

            // at this point, we know that "child" has at least "degree" nodes, so we can
            // move on - this guarantees that if any node needs to be removed from it to
            // guarantee BTree's property, we will be fine with that
            DeleteInternal(childNode, keyToDelete);
        }

        private void MoveLeftNodeIntoParent(BTreeNode<TK> parentNode, BTreeNode<TK> leftSibling, int subtreeIndexInNode)
        {
            var childNode = parentNode.Children[subtreeIndexInNode];

            // left sibling has a node to spare, so this moves one node from left sibling
            // into parent's node and one node from parent into this current node ("child")
            childNode.Entries.Insert(0, parentNode.Entries[subtreeIndexInNode]);
            parentNode.Entries[subtreeIndexInNode] = leftSibling.Entries.Last();
            leftSibling.Entries.RemoveAt(leftSibling.Entries.Count - 1);

            if (!leftSibling.IsLeaf)
            {
                childNode.Children.Insert(0, leftSibling.Children.Last());
                leftSibling.Children.RemoveAt(leftSibling.Children.Count - 1);
            }
        }

        private void MergesSiblingIntoCurrentNode(BTreeNode<TK> parentNode, int subtreeIndexInNode)
        {
            var childNode = parentNode.Children[subtreeIndexInNode];
            var leftIndex = subtreeIndexInNode - 1;
            var leftSibling = subtreeIndexInNode > 0 ? parentNode.Children[leftIndex] : null;

            // this block merges either left or right sibling into the current node "child"
            if (leftSibling != null)
            {
                childNode.Entries.Insert(0, parentNode.Entries[subtreeIndexInNode]);
                var oldEntries = childNode.Entries;
                childNode.Entries = leftSibling.Entries;
                childNode.Entries.AddRange(oldEntries);
                if (!leftSibling.IsLeaf)
                {
                    var oldChildren = childNode.Children;
                    childNode.Children = leftSibling.Children;
                    childNode.Children.AddRange(oldChildren);
                }

                parentNode.Children.RemoveAt(leftIndex);
                parentNode.Entries.RemoveAt(subtreeIndexInNode);
            }
            else
            {
                var rightIndex = subtreeIndexInNode + 1;
                var rightSibling = subtreeIndexInNode < parentNode.Children.Count - 1
                    ? parentNode.Children[rightIndex]
                    : null;

                Assert(rightSibling != null, "Node should have at least one sibling");
                childNode.Entries.Add(parentNode.Entries[subtreeIndexInNode]);
                childNode.Entries.AddRange(rightSibling.Entries);
                if (!rightSibling.IsLeaf)
                {
                    childNode.Children.AddRange(rightSibling.Children);
                }

                parentNode.Children.RemoveAt(rightIndex);
                parentNode.Entries.RemoveAt(subtreeIndexInNode);
            }
        }

        private void MoveRightNodeIntoParent(BTreeNode<TK> parentNode, BTreeNode<TK> rightSibling, int subtreeIndexInNode)
        {
            var childNode = parentNode.Children[subtreeIndexInNode];

            // right sibling has a node to spare, so this moves one node from right sibling
            // into parent's node and one node from parent into this current node ("child")
            childNode.Entries.Add(parentNode.Entries[subtreeIndexInNode]);
            parentNode.Entries[subtreeIndexInNode] = rightSibling.Entries.First();
            rightSibling.Entries.RemoveAt(0);

            if (!rightSibling.IsLeaf)
            {
                childNode.Children.Add(rightSibling.Children.First());
                rightSibling.Children.RemoveAt(0);
            }
        }

        /// <summary>
        /// Helper method that deletes key from a node that contains it, be this
        /// node a leaf node or an internal node.
        /// </summary>
        /// <param name="node">Node that contains the key.</param>
        /// <param name="keyToDelete">Key to be deleted.</param>
        /// <param name="keyIndexInNode">Index of key within the node.</param>
        private void DeleteKeyFromNode(BTreeNode<TK> node, TK keyToDelete, int keyIndexInNode)
        {
            // if leaf, just remove it from the list of entries (we're guaranteed to have
            // at least "degree" # of entries, to BTree property is maintained
            if (node.IsLeaf)
            {
                node.Entries.RemoveAt(keyIndexInNode);
                return;
            }

            var predecessorChild = node.Children[keyIndexInNode];
            if (predecessorChild.Entries.Count >= Degree)
            {
                var predecessor = DeletePredecessor(predecessorChild);
                node.Entries[keyIndexInNode] = predecessor;
            }
            else
            {
                var successorChild = node.Children[keyIndexInNode + 1];
                if (successorChild.Entries.Count >= Degree)
                {
                    var successor = DeleteSuccessor(predecessorChild);
                    node.Entries[keyIndexInNode] = successor;
                }
                else
                {
                    predecessorChild.Entries.Add(node.Entries[keyIndexInNode]);
                    predecessorChild.Entries.AddRange(successorChild.Entries);
                    predecessorChild.Children.AddRange(successorChild.Children);

                    node.Entries.RemoveAt(keyIndexInNode);
                    node.Children.RemoveAt(keyIndexInNode + 1);

                    DeleteInternal(predecessorChild, keyToDelete);
                }
            }
        }

        /// <summary>
        /// Helper method that deletes a predecessor key (i.e. rightmost key) for a given node.
        /// </summary>
        /// <param name="node">Node for which the predecessor will be deleted.</param>
        /// <returns>Predecessor entry that got deleted.</returns>
        private TK DeletePredecessor(BTreeNode<TK> node)
        {
            if (node.IsLeaf)
            {
                var result = node.Entries[node.Entries.Count - 1];
                node.Entries.RemoveAt(node.Entries.Count - 1);
                return result;
            }

            return DeletePredecessor(node.Children.Last());
        }

        /// <summary>
        /// Helper method that deletes a successor key (i.e. leftmost key) for a given node.
        /// </summary>
        /// <param name="node">Node for which the successor will be deleted.</param>
        /// <returns>Successor entry that got deleted.</returns>
        private TK DeleteSuccessor(BTreeNode<TK> node)
        {
            if (node.IsLeaf)
            {
                var result = node.Entries[0];
                node.Entries.RemoveAt(0);
                return result;
            }

            return DeletePredecessor(node.Children.First());
        }

        /// <summary>
        /// Helper method that search for a key in a given BTree.
        /// </summary>
        /// <param name="node">Node used to start the search.</param>
        /// <param name="key">Key to be searched.</param>
        /// <returns>True if contains the key.</returns>
        private bool SearchInternal(BTreeNode<TK> node, TK key)
        {
            int i = node.Entries.TakeWhile(entry => key.CompareTo(entry) > 0).Count();

            if (i < node.Entries.Count && node.Entries[i].CompareTo(key) == 0)
            {
                return true;
            }

            return !node.IsLeaf && SearchInternal(node.Children[i], key);
        }

        /// <summary>
        /// Helper method that splits a full node into two nodes.
        /// </summary>
        /// <param name="parentNode">Parent node that contains node to be split.</param>
        /// <param name="nodeToBeSplitIndex">Index of the node to be split within parent.</param>
        /// <param name="nodeToBeSplit">Node to be split.</param>
        private void SplitChild(BTreeNode<TK> parentNode, int nodeToBeSplitIndex, BTreeNode<TK> nodeToBeSplit)
        {
            var newNode = new BTreeNode<TK>(Degree);

            parentNode.Entries.Insert(nodeToBeSplitIndex, nodeToBeSplit.Entries[Degree - 1]);
            parentNode.Children.Insert(nodeToBeSplitIndex + 1, newNode);

            newNode.Entries.AddRange(nodeToBeSplit.Entries.GetRange(Degree, Degree - 1));

            // remove also Entries[Degree - 1], which is the one to move up to the parent
            nodeToBeSplit.Entries.RemoveRange(Degree - 1, Degree);

            if (!nodeToBeSplit.IsLeaf)
            {
                newNode.Children.AddRange(nodeToBeSplit.Children.GetRange(Degree, Degree));
                nodeToBeSplit.Children.RemoveRange(Degree, Degree);
            }
        }

        private void InsertNonFull(BTreeNode<TK> node, TK newKey)
        {
            var positionToInsert = node.Entries.TakeWhile(entry => newKey.CompareTo(entry) >= 0).Count();

            // if it is a leaf then just insert the value
            if (node.IsLeaf)
            {
                node.Entries.Insert(positionToInsert, newKey);
                return;
            }

            var child = node.Children[positionToInsert];
            if (child.HasReachedMaxEntries)
            {
                SplitChild(node, positionToInsert, child);
                if (newKey.CompareTo(node.Entries[positionToInsert]) > 0)
                {
                    positionToInsert++;
                }
            }

            InsertNonFull(node.Children[positionToInsert], newKey);
        }
    }
}
