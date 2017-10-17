using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class BinaryTree<TItem> : IEnumerable<TItem> where TItem : IComparable<TItem>
    {
        private BinaryTreeNode<TItem> _head;

        private int _count;
        public int Count => _count;
        //head
        public BinaryTreeNode<TItem> Head => _head;

        public delegate void delegateEventInvoker();

        public event delegateEventInvoker OnElementAdded;
        

        #region Constructors

        public BinaryTree()
        {
            _head = null;
            _count = 0;
        }

        public BinaryTree(TItem item)
        {
            if (item != null)
                Add(item);
        }

        public BinaryTree(params TItem[] items)
        {
            if (items != null)
                foreach (var item in items)
                {
                    if (item != null)
                        Add(item);
                }
        }
        #endregion

        #region Add
        public void Add(TItem value)
        {
            if (_head == null)
            {
                _head = new BinaryTreeNode<TItem>(value);
            }
            else
            {
                AddTo(_head, value);
            }
            OnElementAdded.Invoke();
            _count++;
        }
        private void AddTo(BinaryTreeNode<TItem> node, TItem value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<TItem>(value);
                }
                else
                {
                    AddTo(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<TItem>(value);
                }
                else
                {
                    AddTo(node.Right, value);
                }
            }
        }
        #endregion

        #region Contains
        public bool Contains(TItem value)
        {
            BinaryTreeNode<TItem> parent;
            return FindWithParent(value, out parent) != null;
        }
        private BinaryTreeNode<TItem> FindWithParent(TItem value, out BinaryTreeNode<TItem> parent)
        {
            BinaryTreeNode<TItem> current = _head;
            parent = null;

            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }
        #endregion

        #region Remove
        public bool Remove(TItem value)
        {
            BinaryTreeNode<TItem> current;
            BinaryTreeNode<TItem> parent;

            current = FindWithParent(value, out parent);

            if (current == null)
            {
                return false;
            }
            _count--;
            if (current.Right == null)
            {
                if (parent == null)
                {
                    _head = current.Left;
                }

                else
                {
                    int result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }

                    else if (result < 0)
                    {
                        parent.Right = current.Left;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                {
                    _head = current.Right;
                }

                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;

                    }
                }
            }
            else
            {
                BinaryTreeNode<TItem> leftmost = current.Right.Left;
                BinaryTreeNode<TItem> leftmostParent = current.Right;
                while (leftmost.Left != null)

                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }
                leftmostParent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                {
                    _head = leftmost;
                }

                else

                {
                    int result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = leftmost;
                    }

                    else if (result < 0)

                    {
                        parent.Right = leftmost;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Enumerator

        public IEnumerator<TItem> GetEnumerator()
        {
            return InOrderTraversal();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<TItem> InOrderTraversal()
        {
            if (_head != null)
            {
                Stack<BinaryTreeNode<TItem>> stack = new Stack<BinaryTreeNode<TItem>>();
                BinaryTreeNode<TItem> current = _head;
                bool goLeftNext = true;
                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }
                    yield return current.Value;
                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }
        #endregion
    }
}