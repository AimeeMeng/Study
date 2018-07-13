using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmPractice.CodePractice
{
    /* 
     * 给定两个非空链表来表示两个非负整数。位数按照逆序方式存储，它们的每个节点只存储单个数字。将两数相加返回一个新的链表。
     *  你可以假设除了数字 0 之外，这两个数字都不会以零开头。
     *   示例：  输入：(2 -> 4 -> 3) + (5 -> 6 -> 4)
     *           输出：7 -> 0 -> 8
     *           原因：342 + 465 = 807
     *  You are given two non-empty linked lists representing two non-negative integers. 
     *  The digits are stored in reverse order and each of their nodes contain a single digit. 
     *  Add the two numbers and return it as a linked list.
     *  You may assume the two numbers do not contain any leading zero, except the number 0 itself.         
     */
    public class AddTwoNum
    {
        public static void main()
        {
            LinkList<int> L1 = new LinkList<int>();
            LinkList<int> L2 = new LinkList<int>();
            L1.Append(2);
            L1.Append(4);
            L1.Append(3);
            L2.Append(5);
            L2.Append(6);
            L2.Append(4);
            LinkList<int> L3 = AddTwoNumbers(L1, L2);
            L3.Reverse();
        }




        private static LinkList<int> AddTwoNumbers(LinkList<int> l1, LinkList<int> l2)
        {
            int num1 = 0;
            Node<int> p1 = l1.Head;
            int length1 = 0;
            while (p1 != null)
            {
                num1 += p1.Data * (int)Math.Pow(10, length1); 
                p1 = p1.Next;
                length1++;
            }

            int num2 = 0;
            Node<int> p2 = l2.Head;
            int length2 = 0;
            while (p2 != null)
            {
                num2 += p2.Data * (int)Math.Pow(10, length2);
                p2 = p2.Next;
                length2++;
            }

            int num3 = num1 + num2;
            LinkList<int> result = new LinkList<int>();
            int length = length1 <= length2 ? length2 : length1;
            int _num3 = num3;
            while (length > 0)
            {
                int pow = (int)Math.Pow(10, length - 1);
                int _nres = _num3 / pow;
                _num3 = _num3 - _nres * pow;
                result.Append(_nres);
                length--;
            }
            
            return result;
        }
    }

    
  ////Definition for singly-linked list.
  //public class ListNode {
  //    public int val;
  //    public ListNode next;
  //    public ListNode(int x) { val = x; }
  //}


    /// <summary>
    /// 单链表结点类 泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T>
    {
        public T Data { set; get; }          //数据域,当前结点数据
        public Node<T> Next { set; get; }    //位置域,下一个结点地址

        public Node(T item)
        {
            this.Data = item;
            this.Next = null;
        }

        public Node()
        {
            this.Data = default(T);
            this.Next = null;
        }
    }

    /// <summary>
    /// 单链表类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkList<T>
    {
        public Node<T> Head { set; get; } //单链表头

        //构造
        public LinkList()
        {
            Clear();
        }

        /// <summary>
        /// 求单链表的长度
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            Node<T> p = Head;
            int length = 0;
            while (p != null)
            {
                p = p.Next;
                length++;
            }
            return length;
        }

        /// <summary>
        /// 判断单键表是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (Head == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 清空单链表
        /// </summary>
        public void Clear()
        {
            Head = null;
        }

        /// <summary>
        /// 获得当前位置单链表中结点的值
        /// </summary>
        /// <param name="i">结点位置</param>
        /// <returns></returns>
        public T GetNodeValue(int i)
        {
            if (IsEmpty() || i < 1 || i > GetLength())
            {
                Console.WriteLine("单链表为空或结点位置有误！");
                return default(T);
            }

            Node<T> A = new Node<T>();
            A = Head;
            int j = 1;
            while (A.Next != null && j < i)
            {
                A = A.Next;
                j++;
            }

            return A.Data;
        }

        /// <summary>
        /// 增加新元素到单链表末尾
        /// </summary>
        public void Append(T item)
        {
            Node<T> foot = new Node<T>(item);
            Node<T> A = new Node<T>();
            if (Head == null)
            {
                Head = foot;
                return;
            }
            A = Head;
            while (A.Next != null)
            {
                A = A.Next;
            }
            A.Next = foot;
        }

        /// <summary>
        /// 增加单链表插入的位置
        /// </summary>
        /// <param name="item">结点内容</param>
        /// <param name="n">结点插入的位置</param>
        public void Insert(T item, int n)
        {
            if (IsEmpty() || n < 1 || n > GetLength())
            {
                Console.WriteLine("单链表为空或结点位置有误！");
                return;
            }

            if (n == 1)  //增加到头部
            {
                Node<T> H = new Node<T>(item);
                H.Next = Head;
                Head = H;
                return;
            }

            Node<T> A = new Node<T>();
            Node<T> B = new Node<T>();
            B = Head;
            int j = 1;
            while (B.Next != null && j < n)
            {
                A = B;
                B = B.Next;
                j++;
            }

            if (j == n)
            {
                Node<T> C = new Node<T>(item);
                A.Next = C;
                C.Next = B;
            }
        }

        /// <summary>
        /// 删除单链表结点
        /// </summary>
        /// <param name="i">删除结点位置</param>
        /// <returns></returns>
        public void Delete(int i)
        {
            if (IsEmpty() || i < 1 || i > GetLength())
            {
                Console.WriteLine("单链表为空或结点位置有误！");
                return;
            }

            Node<T> A = new Node<T>();
            if (i == 1)   //删除头
            {
                A = Head;
                Head = Head.Next;
                return;
            }
            Node<T> B = new Node<T>();
            B = Head;
            int j = 1;
            while (B.Next != null && j < i)
            {
                A = B;
                B = B.Next;
                j++;
            }
            if (j == i)
            {
                A.Next = B.Next;
            }
        }

        /// <summary>
        /// 显示单链表
        /// </summary>
        public void Dispaly()
        {
            Node<T> A = new Node<T>();
            A = Head;
            while (A != null)
            {
                Console.WriteLine(A.Data);
                A = A.Next;
            }
        }

        #region 面试题
        /// <summary>
        /// 单链表反转
        /// </summary>
        public void Reverse()
        {
            if (GetLength() == 1 || Head == null)
            {
                return;
            }

            Node<T> NewNode = null;
            Node<T> CurrentNode = Head;
            Node<T> TempNode = new Node<T>();

            while (CurrentNode != null)
            {
                TempNode = CurrentNode.Next;
                CurrentNode.Next = NewNode;
                NewNode = CurrentNode;
                CurrentNode = TempNode;
            }
            Head = NewNode;

            Dispaly();
        }

        /// <summary>
        /// 获得单链表中间值
        /// 思路：使用两个指针，第一个每次走一步，第二个每次走两步：
        /// </summary>
        public void GetMiddleValue()
        {
            Node<T> A = Head;
            Node<T> B = Head;

            while (B != null && B.Next != null)
            {
                A = A.Next;
                B = B.Next.Next;
            }
            if (B != null) //奇数
            {
                Console.WriteLine("奇数:中间值为：{0}", A.Data);
            }
            else    //偶数
            {
                Console.WriteLine("偶数:中间值为：{0}和{1}", A.Data, A.Next.Data);
            }
        }

        #endregion

    }

}
