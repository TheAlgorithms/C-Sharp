using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Heap
{
    /// <summary>
    /// BINOMIAL MIN HEAP Data Structure
    /// </summary>
    public class BinomialMinHeap<T> : IMinHeap<T> where T : IComparable<T>
    {
        /// <summary>
        /// The Heap Node class.
        /// </summary>
        private class BinomialNode<T> where T : IComparable<T>
        {
            public T Value { get; set; }
            public BinomialNode<T> Parent { get; set; }
            public BinomialNode<T> Sibling { get; set; }    // Right-Sibling
            public BinomialNode<T> Child { get; set; }      // Left-Child

            // Constructors
            public BinomialNode() : this(default(T), null, null, null) { }
            public BinomialNode(T value) : this(value, null, null, null) { }
            public BinomialNode(T value, BinomialNode<T> parent, BinomialNode<T> sibling, BinomialNode<T> child)
            {
                Value = value;
                Parent = parent;
                Sibling = sibling;
                Child = child;
            }

            // Helper boolean flags
            public bool HasSiblings
            {
                get { return this.Sibling != null; }
            }

            public bool HasChildren
            {
                get { return this.Child != null; }
            }
        }


        /// <summary>
        /// INSTANCE VARIABLES
        /// </summary>
        private int _size { get; set; }
        private const int _defaultCapacity = 8;
        private ArrayList<BinomialNode<T>> _forest { get; set; }


        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public BinomialMinHeap()
            : this(8)
        {
            // Empty constructor
        }

        public BinomialMinHeap(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException();

            capacity = (capacity < _defaultCapacity ? _defaultCapacity : capacity);

            _size = 0;
            _forest = new ArrayList<BinomialNode<T>>(capacity);
        }


        /************************************************************************************************/
        /** PRIVATE HELPER FUNCTIONS                                                                    */


        /// <summary>
        /// Removes root of tree at he specified index.
        /// </summary>
        private void _removeAtIndex(int minIndex)
        {
            // Get the deletedTree
            // The min-root lies at _forest[minIndex]
            BinomialNode<T> deletedTreeRoot = _forest[minIndex].Child;

            // Exit if there was no children under old-min-root
            if (deletedTreeRoot == null)
                return;

            // CONSTRUCT H'' (double-prime)
            BinomialMinHeap<T> deletedForest = new BinomialMinHeap<T>();
            deletedForest._forest.Resize(minIndex + 1);
            deletedForest._size = (1 << minIndex) - 1;

            for (int i = (minIndex - 1); i >= 0; --i)
            {
                deletedForest._forest[i] = deletedTreeRoot;
                deletedTreeRoot = deletedTreeRoot.Sibling;
                deletedForest._forest[i].Sibling = null;
            }

            // CONSTRUCT H' (single-prime)
            _forest[minIndex] = null;
            _size = deletedForest._size + 1;

            Merge(deletedForest);

            // Decrease the size
            --_size;
        }

        /// <summary>
        /// Returns index of the tree with the minimum root's value.
        /// </summary>
        private int _findMinIndex()
        {
            int i, minIndex;

            // Loop until you reach a slot in the _forest that is not null.
            // The final value of "i" will be pointing to the non-null _forest slot.
            for (i = 0; i < _forest.Count && _forest[i] == null; ++i) ;

            // Loop over the trees in forest, and return the index of the slot that has the tree with the min-valued root
            for (minIndex = i; i < _forest.Count; ++i)
                if (_forest[i] != null && (_forest[i].Value.IsLessThan(_forest[minIndex].Value)))
                    minIndex = i;

            return minIndex;
        }

        /// <summary>
        /// Combines two trees and returns the new tree root node.
        /// </summary>
        private BinomialNode<T> _combineTrees(BinomialNode<T> firstTreeRoot, BinomialNode<T> secondTreeRoot)
        {
            if (firstTreeRoot == null || secondTreeRoot == null)
                throw new ArgumentNullException("Either one of the nodes or both are null.");

            if (secondTreeRoot.Value.IsLessThan(firstTreeRoot.Value))
                return _combineTrees(secondTreeRoot, firstTreeRoot);

            secondTreeRoot.Sibling = firstTreeRoot.Child;
            firstTreeRoot.Child = secondTreeRoot;
            secondTreeRoot.Parent = firstTreeRoot;

            return firstTreeRoot;
        }

        /// <summary>
        /// Clones a tree, given it's root node.
        /// </summary>
        private BinomialNode<T> _cloneTree(BinomialNode<T> treeRoot)
        {
            if (treeRoot == null)
                return null;
            return new BinomialNode<T>() { Value = treeRoot.Value, Child = _cloneTree(treeRoot.Child), Sibling = _cloneTree(treeRoot.Sibling) };
        }


        /************************************************************************************************/
        /** PUBLIC API FUNCTIONS                                                                        */


        /// <summary>
        /// Returns count of elements in heap.
        /// </summary>
        public int Count
        {
            get { return _size; }
        }

        /// <summary>
        /// Checks if heap is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty
        {
            get { return (_size == 0); }
        }

        /// <summary>
        /// Initializes this heap with a collection of elements.
        /// </summary>
        public void Initialize(IList<T> newCollection)
        {
            if (newCollection == null)
                throw new ArgumentNullException();

            if (newCollection.Count > ArrayList<T>.MAXIMUM_ARRAY_LENGTH_x64)
                throw new OverflowException();

            _forest = new ArrayList<BinomialNode<T>>(newCollection.Count + 1);

            for (int i = 0; i < newCollection.Count; ++i)
                this.Add(newCollection[i]);
        }

        /// <summary>
        /// Inserts a new item to heap.
        /// </summary>
        public void Add(T heapKey)
        {
            var tempHeap = new BinomialMinHeap<T>();
            tempHeap._forest.Add(new BinomialNode<T>(heapKey));
            tempHeap._size = 1;

            // Merge this with tempHeap
            Merge(tempHeap);

            // Increase the _size
            ++_size;
        }

        /// <summary>
        /// Return the min element.
        /// </summary>
        public T Peek()
        {
            if (IsEmpty)
                throw new Exception("Heap is empty.");

            int minIndex = _findMinIndex();
            var minValue = _forest[minIndex].Value;

            return minValue;
        }

        /// <summary>
        /// Remove the min element from heap.
        /// </summary>
        public void RemoveMin()
        {
            if (IsEmpty)
                throw new Exception("Heap is empty.");

            _removeAtIndex(_findMinIndex());
        }

        /// <summary>
        /// Return the min element and then remove it from heap.
        /// </summary>
        public T ExtractMin()
        {
            if (IsEmpty)
                throw new Exception("Heap is empty.");

            // Get the min-node index
            int minIndex = _findMinIndex();
            var minValue = _forest[minIndex].Value;

            // Remove min from heap
            _removeAtIndex(minIndex);

            return minValue;
        }

        /// <summary>
        /// Merges the elements of another heap with this heap.
        /// </summary>
        public void Merge(BinomialMinHeap<T> otherHeap)
        {
            // Avoid aliasing problems
            if (this == otherHeap)
                return;

            // Avoid null or empty cases
            if (otherHeap == null || otherHeap.IsEmpty)
                return;

            BinomialNode<T> carryNode = null;
            _size = _size + otherHeap._size;

            // One capacity-change step
            if (_size > _forest.Count)
            {
                int newSize = Math.Max(this._forest.Count, otherHeap._forest.Count) + 1;
                this._forest.Resize(newSize);
            }

            for (int i = 0, j = 1; j <= _size; i++, j *= 2)
            {
                BinomialNode<T> treeRoot1 = (_forest.IsEmpty == true ? null : _forest[i]);
                BinomialNode<T> treeRoot2 = (i < otherHeap._forest.Count ? otherHeap._forest[i] : null);

                int whichCase = (treeRoot1 == null ? 0 : 1);
                whichCase += (treeRoot2 == null ? 0 : 2);
                whichCase += (carryNode == null ? 0 : 4);

                switch (whichCase)
                {
                    /*** SINGLE CASES ***/
                    case 0:     /* No trees */
                    case 1:     /* Only this */
                        break;
                    case 2:     /* Only otherHeap */
                        this._forest[i] = treeRoot2;
                        otherHeap._forest[i] = null;
                        break;
                    case 4:     /* Only carryNode */
                        this._forest[i] = carryNode;
                        carryNode = null;
                        break;

                    /*** BINARY CASES ***/
                    case 3:     /* this and otherHeap */
                        carryNode = _combineTrees(treeRoot1, treeRoot2);
                        this._forest[i] = otherHeap._forest[i] = null;
                        break;
                    case 5:     /* this and carryNode */
                        carryNode = _combineTrees(treeRoot1, carryNode);
                        this._forest[i] = null;
                        break;
                    case 6:     /* otherHeap and carryNode */
                        carryNode = _combineTrees(treeRoot2, carryNode);
                        otherHeap._forest[i] = null;
                        break;
                    case 7:     /* all the nodes */
                        this._forest[i] = carryNode;
                        carryNode = _combineTrees(treeRoot1, treeRoot2);
                        otherHeap._forest[i] = null;
                        break;
                }//end-switch
            }//end-for

            // Clear otherHeap
            otherHeap.Clear();
        }

        /// <summary>
        /// Returns an array copy of heap.
        /// </summary>
        public T[] ToArray()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Rebuilds the heap.
        /// </summary>
        public void RebuildHeap()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list copy of heap.
        /// </summary>
        public List<T> ToList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a binomial max-heap copy of this instance.
        /// </summary>
        public IMaxHeap<T> ToMaxHeap()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        public void Clear()
        {
            _size = 0;
            _forest.Clear();
        }

    }

}