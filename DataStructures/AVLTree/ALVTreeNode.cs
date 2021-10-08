namespace DataStructures.AVLTree
{
    public class AVLTreeNode<TKey>
    {
        public AVLTreeNode(TKey key)
        {
            Key = key;
            BalanceFactor = 0;
            Left = null;
            Right = null;
        }

        public TKey Key { get; set; }

        public sbyte BalanceFactor { get; set; }

        public AVLTreeNode<TKey>? Left { get; set; }

        public AVLTreeNode<TKey>? Right { get; set; }
    }
}
