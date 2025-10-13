using UnityEngine;
using UnityEditor;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System;

namespace UnityEditor.Examples
{
    /// <summary>
    /// UnsafeUtility 工具类示例
    /// 提供不安全内存操作相关的实用工具功能
    /// </summary>
    public static class UnsafeUtilityExample
    {
        #region 内存分配示例

        /// <summary>
        /// 分配内存
        /// </summary>
        public static void MallocExample()
        {
            int size = 1024; // 1KB
            Allocator allocator = Allocator.Temp;
            
            // 分配内存
            unsafe
            {
                void* ptr = UnsafeUtility.Malloc(size, 16, allocator);
                
                if (ptr != null)
                {
                    Debug.Log($"内存分配成功:");
                    Debug.Log($"- 大小: {size} 字节");
                    Debug.Log($"- 对齐: 16 字节");
                    Debug.Log($"- 分配器: {allocator}");
                    
                    // 释放内存
                    UnsafeUtility.Free(ptr, allocator);
                    Debug.Log("内存已释放");
                }
            }
        }

        /// <summary>
        /// 释放内存
        /// </summary>
        public static void FreeExample()
        {
            unsafe
            {
                // 分配内存
                void* ptr = UnsafeUtility.Malloc(512, 4, Allocator.Temp);
                
                Debug.Log("内存已分配: 512字节");
                
                // 释放内存
                UnsafeUtility.Free(ptr, Allocator.Temp);
                
                Debug.Log("内存已释放");
            }
        }

        #endregion

        #region 内存复制示例

        /// <summary>
        /// 内存复制
        /// </summary>
        public static void MemCpyExample()
        {
            unsafe
            {
                // 源数据
                int[] sourceArray = { 1, 2, 3, 4, 5 };
                int[] destArray = new int[5];
                
                fixed (int* srcPtr = sourceArray)
                fixed (int* dstPtr = destArray)
                {
                    // 复制内存
                    UnsafeUtility.MemCpy(dstPtr, srcPtr, sourceArray.Length * sizeof(int));
                }
                
                Debug.Log("内存复制完成:");
                Debug.Log($"- 源数组: [{string.Join(", ", sourceArray)}]");
                Debug.Log($"- 目标数组: [{string.Join(", ", destArray)}]");
            }
        }

        /// <summary>
        /// 内存移动
        /// </summary>
        public static void MemMoveExample()
        {
            unsafe
            {
                int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                
                fixed (int* ptr = array)
                {
                    // 移动内存（支持重叠区域）
                    UnsafeUtility.MemMove(ptr + 2, ptr, 5 * sizeof(int));
                }
                
                Debug.Log($"内存移动完成: [{string.Join(", ", array)}]");
            }
        }

        /// <summary>
        /// 内存清零
        /// </summary>
        public static void MemClearExample()
        {
            unsafe
            {
                int[] array = { 1, 2, 3, 4, 5 };
                
                Debug.Log($"清零前: [{string.Join(", ", array)}]");
                
                fixed (int* ptr = array)
                {
                    // 清零内存
                    UnsafeUtility.MemClear(ptr, array.Length * sizeof(int));
                }
                
                Debug.Log($"清零后: [{string.Join(", ", array)}]");
            }
        }

        #endregion

        #region 内存比较示例

        /// <summary>
        /// 内存比较
        /// </summary>
        public static void MemCmpExample()
        {
            unsafe
            {
                int[] array1 = { 1, 2, 3, 4, 5 };
                int[] array2 = { 1, 2, 3, 4, 5 };
                int[] array3 = { 1, 2, 3, 4, 6 };
                
                fixed (int* ptr1 = array1)
                fixed (int* ptr2 = array2)
                fixed (int* ptr3 = array3)
                {
                    int result1 = UnsafeUtility.MemCmp(ptr1, ptr2, array1.Length * sizeof(int));
                    int result2 = UnsafeUtility.MemCmp(ptr1, ptr3, array1.Length * sizeof(int));
                    
                    Debug.Log("内存比较结果:");
                    Debug.Log($"- array1 vs array2: {(result1 == 0 ? "相同" : "不同")}");
                    Debug.Log($"- array1 vs array3: {(result2 == 0 ? "相同" : "不同")}");
                }
            }
        }

        #endregion

        #region 类型大小和对齐示例

        /// <summary>
        /// 获取类型大小
        /// </summary>
        public static void SizeOfExample()
        {
            int intSize = UnsafeUtility.SizeOf<int>();
            int floatSize = UnsafeUtility.SizeOf<float>();
            int vector3Size = UnsafeUtility.SizeOf<Vector3>();
            int quaternionSize = UnsafeUtility.SizeOf<Quaternion>();
            
            Debug.Log("类型大小:");
            Debug.Log($"- int: {intSize} 字节");
            Debug.Log($"- float: {floatSize} 字节");
            Debug.Log($"- Vector3: {vector3Size} 字节");
            Debug.Log($"- Quaternion: {quaternionSize} 字节");
        }

        /// <summary>
        /// 获取类型对齐
        /// </summary>
        public static void AlignOfExample()
        {
            int intAlign = UnsafeUtility.AlignOf<int>();
            int floatAlign = UnsafeUtility.AlignOf<float>();
            int vector3Align = UnsafeUtility.AlignOf<Vector3>();
            
            Debug.Log("类型对齐:");
            Debug.Log($"- int: {intAlign} 字节");
            Debug.Log($"- float: {floatAlign} 字节");
            Debug.Log($"- Vector3: {vector3Align} 字节");
        }

        #endregion

        #region 指针操作示例

        /// <summary>
        /// 地址转换
        /// </summary>
        public static void AddressOfExample()
        {
            unsafe
            {
                int value = 42;
                void* ptr = UnsafeUtility.AddressOf(ref value);
                
                Debug.Log($"变量地址: 0x{((IntPtr)ptr).ToString("X")}");
                Debug.Log($"变量值: {value}");
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        public static void ReadArrayElementExample()
        {
            unsafe
            {
                int[] array = { 10, 20, 30, 40, 50 };
                
                fixed (int* ptr = array)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        int value = UnsafeUtility.ReadArrayElement<int>(ptr, i);
                        Debug.Log($"数组[{i}] = {value}");
                    }
                }
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        public static void WriteArrayElementExample()
        {
            unsafe
            {
                int[] array = new int[5];
                
                fixed (int* ptr = array)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        UnsafeUtility.WriteArrayElement(ptr, i, i * 10);
                    }
                }
                
                Debug.Log($"写入后的数组: [{string.Join(", ", array)}]");
            }
        }

        #endregion

        #region NativeArray操作示例

        /// <summary>
        /// NativeArray内存操作
        /// </summary>
        public static void NativeArrayMemoryExample()
        {
            NativeArray<int> nativeArray = new NativeArray<int>(10, Allocator.Temp);
            
            // 填充数据
            for (int i = 0; i < nativeArray.Length; i++)
            {
                nativeArray[i] = i * 2;
            }
            
            unsafe
            {
                void* ptr = nativeArray.GetUnsafePtr();
                int size = UnsafeUtility.SizeOf<int>() * nativeArray.Length;
                
                Debug.Log($"NativeArray内存信息:");
                Debug.Log($"- 长度: {nativeArray.Length}");
                Debug.Log($"- 总大小: {size} 字节");
                Debug.Log($"- 指针地址: 0x{((IntPtr)ptr).ToString("X")}");
            }
            
            nativeArray.Dispose();
        }

        /// <summary>
        /// NativeArray复制
        /// </summary>
        public static void NativeArrayCopyExample()
        {
            NativeArray<float> source = new NativeArray<float>(5, Allocator.Temp);
            NativeArray<float> dest = new NativeArray<float>(5, Allocator.Temp);
            
            // 填充源数组
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = i * 1.5f;
            }
            
            unsafe
            {
                void* srcPtr = source.GetUnsafePtr();
                void* dstPtr = dest.GetUnsafePtr();
                
                // 复制内存
                UnsafeUtility.MemCpy(dstPtr, srcPtr, source.Length * UnsafeUtility.SizeOf<float>());
            }
            
            Debug.Log("NativeArray复制完成:");
            Debug.Log($"- 源数组: [{string.Join(", ", source.ToArray())}]");
            Debug.Log($"- 目标数组: [{string.Join(", ", dest.ToArray())}]");
            
            source.Dispose();
            dest.Dispose();
        }

        #endregion

        #region 性能测试示例

        /// <summary>
        /// 内存操作性能测试
        /// </summary>
        public static void MemoryPerformanceTestExample()
        {
            int size = 1024 * 1024; // 1MB
            int iterations = 100;
            
            unsafe
            {
                void* ptr1 = UnsafeUtility.Malloc(size, 16, Allocator.Temp);
                void* ptr2 = UnsafeUtility.Malloc(size, 16, Allocator.Temp);
                
                // 测试MemCpy性能
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                for (int i = 0; i < iterations; i++)
                {
                    UnsafeUtility.MemCpy(ptr2, ptr1, size);
                }
                stopwatch.Stop();
                
                Debug.Log($"内存复制性能测试 ({iterations}次, {size}字节):");
                Debug.Log($"- 总耗时: {stopwatch.ElapsedMilliseconds}ms");
                Debug.Log($"- 平均耗时: {stopwatch.ElapsedMilliseconds / (float)iterations}ms");
                
                UnsafeUtility.Free(ptr1, Allocator.Temp);
                UnsafeUtility.Free(ptr2, Allocator.Temp);
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 自定义内存池
        /// </summary>
        public static void CustomMemoryPoolExample()
        {
            unsafe
            {
                int poolSize = 1024;
                void* memoryPool = UnsafeUtility.Malloc(poolSize, 16, Allocator.Persistent);
                
                Debug.Log($"内存池已创建: {poolSize}字节");
                
                // 使用内存池
                int* intPtr = (int*)memoryPool;
                for (int i = 0; i < 10; i++)
                {
                    intPtr[i] = i * 10;
                }
                
                Debug.Log("内存池数据:");
                for (int i = 0; i < 10; i++)
                {
                    Debug.Log($"  - [{i}] = {intPtr[i]}");
                }
                
                // 清理内存池
                UnsafeUtility.Free(memoryPool, Allocator.Persistent);
                Debug.Log("内存池已释放");
            }
        }

        /// <summary>
        /// 批量数据处理
        /// </summary>
        public static void BatchDataProcessingExample()
        {
            int count = 1000;
            NativeArray<Vector3> positions = new NativeArray<Vector3>(count, Allocator.Temp);
            
            // 初始化数据
            for (int i = 0; i < count; i++)
            {
                positions[i] = new Vector3(i, i * 2, i * 3);
            }
            
            unsafe
            {
                Vector3* ptr = (Vector3*)positions.GetUnsafePtr();
                
                // 批量处理
                for (int i = 0; i < count; i++)
                {
                    ptr[i] *= 2.0f;
                }
            }
            
            Debug.Log($"批量处理完成: {count}个Vector3");
            Debug.Log($"第一个元素: {positions[0]}");
            Debug.Log($"最后一个元素: {positions[count - 1]}");
            
            positions.Dispose();
        }

        #endregion
    }
}
