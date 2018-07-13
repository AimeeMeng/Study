using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections; //使用Hashtable时，必须引入这个命名空间

namespace AlgorithmPractice.CodePractice
{
    /*  
     *  给定一个整数数组和一个目标值，找出数组中和为目标值的两个数的数组下标。
     *  你可以假设每个输入只对应一种答案，且同样的元素不能被重复利用。
     *  示例:   给定 nums = [2, 7, 11, 15], target = 9
     *  因为 nums[0] + nums[1] = 2 + 7 = 9  所以返回 [0, 1]      
     *  Given an array of integers, return indices of the two numbers such that they add up to a specific target.
     *  You may assume that each input would have exactly one solution, and you may not use the same element twice.
     */
    public class TwoSum
    {
        public static void main()
        {
            int[] nums = { 2, 7, 11, 15 };
            int target = 9;
            foreach (int item in twoSum3(nums, target))
            {
                Console.WriteLine(item);
            }
        }
        
        /// <summary>
        /// 暴力法
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static int[] twoSum1(int[] nums, int target)
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
        private static int[] twoSum2(int[] nums, int target)
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
                        return new int[] { i, (int)de.Value };
                    }
                }
            }
            return new int[] { };
        }

        //一遍哈希表
        //在进行迭代并将元素插入到表中的同时，回过头来检查表中是否已经存在当前元素所对应的目标元素。
        //如果它存在，那找到了对应解，并立即将其返回。
        private static int[] twoSum3(int[] nums, int target)
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
