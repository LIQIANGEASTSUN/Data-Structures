using DataStruct.Log;

namespace DataStruct.Sort
{
    // 选择排序
    class SelectionSort
    {
        public void Test()
        {
            int[] arr = new int[] { 6, 9, 2, 5, 0, 1, 7, 3, 8 };

            SelectionSortFun(arr);

            LogArr.Log(arr);
        }

        /// <summary>
        /// 选择排序
        /// </summary>
        /// <param name="arr"></param>
        public void SelectionSortFun(int[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                // 记录最小值下标
                int minIndex = i;

                for (int j = i + 1; j < arr.Length; ++j)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        // 替换最小值下标
                        minIndex = j;
                    }
                }

                // 将最小值替换入第 i 个位置
                if (i != minIndex)
                {
                    int temp = arr[i];
                    arr[i] = arr[minIndex];
                    arr[minIndex] = temp;
                }
            }
        }
    }
}
