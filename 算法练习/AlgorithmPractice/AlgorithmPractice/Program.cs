using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections; //使用Hashtable时，必须引入这个命名空间

namespace AlgorithmPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int[] nums = { 2, 7, 11, 15 };
                int target = 9;
                foreach (int item in twoSum3(nums, target))
                {
                    Console.WriteLine(item);
                }
              
                Console.WriteLine();
                Console.ReadLine();
            }
            catch { }

        }

        /// <summary>
        /// 暴力法
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum(int[] nums, int target)
        {
            int[] result = new int[2];
            if (nums != null)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    for (int j = i + 1; j < nums.Length; j++)
                    {
                        if (nums[i] + nums[j] == target)
                        {
                            result[0] = nums[i];
                            result[1] = nums[j];
                            break;
                        }
                    }
                }
            }
            return result;
        }

        //两遍哈希表
        public static int[] twoSum(int[] nums, int target)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < nums.Length; i++)
            {
                ht.Add(nums[i], i);
            }
            //遍历哈希表
            //遍历哈希表需要用到DictionaryEntry Object
            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];
                foreach (DictionaryEntry de in ht)
                {
                    if ((int)de.Key == complement)
                    {
                        return new int[] { i ,(int)de.Value};
                    }
                }
            }
            return new int[] { };
        }
        //一遍哈希表
        //在进行迭代并将元素插入到表中的同时，回过头来检查表中是否已经存在当前元素所对应的目标元素。
        //如果它存在，那找到了对应解，并立即将其返回。
        public static int[] twoSum3(int[] nums, int target)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];
                //if (ht.ContainsKey(complement))
                //{
                //    return new int[] { nums[i], i };
                //}
                foreach (DictionaryEntry de in ht)
                {
                    if ((int)de.Key == complement)
                    {
                        return new int[] { i, (int)de.Value };
                    }
                }
                ht.Add(nums[i], i);
            }
            return new int[] { };
        }
    }
}
