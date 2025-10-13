using UnityEngine;
using UnityEditor;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System;

namespace UnityEditor.Examples
{
    /// <summary>
    /// NativeSliceUnsafeUtility 工具类示例
    /// 提供NativeSlice不安全操作相关的实用工具功能
    /// </summary>
    public static class NativeSliceUnsafeUtilityExample
    {
        #region 转换现有数据示例

        /// <summary>
        /// 转换现有数据为NativeSlice
        /// </summary>
        public static void ConvertExistingDataToNativeSliceExample()
        {
            unsafe
            {
                int[] managedArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                
                fixed (int* ptr = managedArray)
                {
                    // 转换为NativeSlice
                    NativeSlice<int> nativeSlice = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<int>(
                        ptr, 
                        sizeof(int),
                        managedArray.Length
                    );
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    NativeSliceUnsafeUtility.SetAtomicSafetyHandle(ref nativeSlice, AtomicSafetyHandle.Create());
                    #endif
                    
                    Debug.Log($"转换完成:");
                    Debug.Log($"- 原始数组: [{string.Join(", ", managedArray)}]");
                    Debug.Log($"- NativeSlice长度: {nativeSlice.Length}");
                    Debug.Log($"- NativeSlice[0]: {nativeSlice[0]}");
                    Debug.Log($"- NativeSlice[9]: {nativeSlice[9]}");
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle.Release(NativeSliceUnsafeUtility.GetAtomicSafetyHandle(nativeSlice));
                    #endif
                }
            }
        }

        /// <summary>
        /// 转换部分数据为NativeSlice
        /// </summary>
        public static void ConvertPartialDataToNativeSliceExample()
        {
            unsafe
            {
                float[] floatArray = { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f };
                
                fixed (float* ptr = floatArray)
                {
                    // 只转换中间部分（索引2-5）
                    NativeSlice<float> nativeSlice = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<float>(
                        ptr + 2, 
                        sizeof(float),
                        4
                    );
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    NativeSliceUnsafeUtility.SetAtomicSafetyHandle(ref nativeSlice, AtomicSafetyHandle.Create());
                    #endif
                    
                    Debug.Log($"部分数据转换:");
                    Debug.Log($"- 原始数组: [{string.Join(", ", floatArray)}]");
                    Debug.Log($"- NativeSlice长度: {nativeSlice.Length}");
                    Debug.Log($"- NativeSlice内容: [{string.Join(", ", nativeSlice.ToArray())}]");
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle.Release(NativeSliceUnsafeUtility.GetAtomicSafetyHandle(nativeSlice));
                    #endif
                }
            }
        }

        #endregion

        #region 获取不安全指针示例

        /// <summary>
        /// 获取不安全指针
        /// </summary>
        public static void GetUnsafePtrExample()
        {
            NativeArray<int> nativeArray = new NativeArray<int>(10, Allocator.Temp);
            
            for (int i = 0; i < nativeArray.Length; i++)
            {
                nativeArray[i] = i * 10;
            }
            
            // 创建NativeSlice
            NativeSlice<int> nativeSlice = new NativeSlice<int>(nativeArray, 2, 5);
            
            unsafe
            {
                void* ptr = NativeSliceUnsafeUtility.GetUnsafePtr(nativeSlice);
                int* intPtr = (int*)ptr;
                
                Debug.Log("NativeSlice不安全指针:");
                Debug.Log($"- 指针地址: 0x{((IntPtr)ptr).ToString("X")}");
                Debug.Log($"- 第一个元素: {intPtr[0]}");
                Debug.Log($"- 最后一个元素: {intPtr[nativeSlice.Length - 1]}");
            }
            
            nativeArray.Dispose();
        }

        /// <summary>
        /// 获取只读不安全指针
        /// </summary>
        public static void GetUnsafeReadOnlyPtrExample()
        {
            NativeArray<Vector3> positions = new NativeArray<Vector3>(10, Allocator.Temp);
            
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector3(i, i * 2, i * 3);
            }
            
            NativeSlice<Vector3> slice = new NativeSlice<Vector3>(positions, 3, 4);
            
            unsafe
            {
                void* ptr = NativeSliceUnsafeUtility.GetUnsafeReadOnlyPtr(slice);
                Vector3* vec3Ptr = (Vector3*)ptr;
                
                Debug.Log("NativeSlice只读指针:");
                for (int i = 0; i < slice.Length; i++)
                {
                    Debug.Log($"- [{i}] = {vec3Ptr[i]}");
                }
            }
            
            positions.Dispose();
        }

        #endregion

        #region 原子安全句柄示例

        /// <summary>
        /// 获取原子安全句柄
        /// </summary>
        public static void GetAtomicSafetyHandleExample()
        {
            NativeArray<int> nativeArray = new NativeArray<int>(10, Allocator.Temp);
            NativeSlice<int> nativeSlice = new NativeSlice<int>(nativeArray, 0, 5);
            
            #if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle safetyHandle = NativeSliceUnsafeUtility.GetAtomicSafetyHandle(nativeSlice);
            
            Debug.Log("原子安全句柄:");
            Debug.Log($"- 句柄已获取: {safetyHandle.IsValid()}");
            #else
            Debug.Log("原子安全检查未启用");
            #endif
            
            nativeArray.Dispose();
        }

        /// <summary>
        /// 设置原子安全句柄
        /// </summary>
        public static void SetAtomicSafetyHandleExample()
        {
            unsafe
            {
                int[] managedArray = { 10, 20, 30, 40, 50 };
                
                fixed (int* ptr = managedArray)
                {
                    NativeSlice<int> nativeSlice = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<int>(
                        ptr, 
                        sizeof(int),
                        managedArray.Length
                    );
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle safetyHandle = AtomicSafetyHandle.Create();
                    NativeSliceUnsafeUtility.SetAtomicSafetyHandle(ref nativeSlice, safetyHandle);
                    
                    Debug.Log("原子安全句柄已设置");
                    
                    AtomicSafetyHandle.Release(safetyHandle);
                    #else
                    Debug.Log("原子安全检查未启用");
                    #endif
                }
            }
        }

        #endregion

        #region NativeSlice操作示例

        /// <summary>
        /// NativeSlice切片操作
        /// </summary>
        public static void SliceOperationsExample()
        {
            NativeArray<int> nativeArray = new NativeArray<int>(20, Allocator.Temp);
            
            for (int i = 0; i < nativeArray.Length; i++)
            {
                nativeArray[i] = i;
            }
            
            // 创建多个切片
            NativeSlice<int> slice1 = new NativeSlice<int>(nativeArray, 0, 5);
            NativeSlice<int> slice2 = new NativeSlice<int>(nativeArray, 5, 5);
            NativeSlice<int> slice3 = new NativeSlice<int>(nativeArray, 10, 5);
            
            Debug.Log("NativeSlice切片操作:");
            Debug.Log($"- 切片1: [{string.Join(", ", slice1.ToArray())}]");
            Debug.Log($"- 切片2: [{string.Join(", ", slice2.ToArray())}]");
            Debug.Log($"- 切片3: [{string.Join(", ", slice3.ToArray())}]");
            
            nativeArray.Dispose();
        }

        /// <summary>
        /// NativeSlice数据修改
        /// </summary>
        public static void SliceDataModificationExample()
        {
            NativeArray<float> nativeArray = new NativeArray<float>(10, Allocator.Temp);
            
            for (int i = 0; i < nativeArray.Length; i++)
            {
                nativeArray[i] = i * 1.5f;
            }
            
            // 创建切片
            NativeSlice<float> slice = new NativeSlice<float>(nativeArray, 3, 4);
            
            Debug.Log($"修改前: [{string.Join(", ", slice.ToArray())}]");
            
            unsafe
            {
                float* ptr = (float*)NativeSliceUnsafeUtility.GetUnsafePtr(slice);
                
                // 修改切片数据
                for (int i = 0; i < slice.Length; i++)
                {
                    ptr[i] *= 2.0f;
                }
            }
            
            Debug.Log($"修改后: [{string.Join(", ", slice.ToArray())}]");
            Debug.Log($"原数组: [{string.Join(", ", nativeArray.ToArray())}]");
            
            nativeArray.Dispose();
        }

        #endregion

        #region 性能优化示例

        /// <summary>
        /// 高性能切片处理
        /// </summary>
        public static void HighPerformanceSliceProcessingExample()
        {
            int totalSize = 10000;
            int sliceSize = 1000;
            
            NativeArray<Vector3> positions = new NativeArray<Vector3>(totalSize, Allocator.Temp);
            
            // 初始化数据
            for (int i = 0; i < totalSize; i++)
            {
                positions[i] = new Vector3(i, i, i);
            }
            
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            // 分批处理
            for (int offset = 0; offset < totalSize; offset += sliceSize)
            {
                int length = Mathf.Min(sliceSize, totalSize - offset);
                NativeSlice<Vector3> slice = new NativeSlice<Vector3>(positions, offset, length);
                
                unsafe
                {
                    Vector3* ptr = (Vector3*)NativeSliceUnsafeUtility.GetUnsafePtr(slice);
                    
                    for (int i = 0; i < length; i++)
                    {
                        ptr[i] *= 2.0f;
                    }
                }
            }
            
            stopwatch.Stop();
            
            Debug.Log($"高性能切片处理:");
            Debug.Log($"- 总大小: {totalSize}");
            Debug.Log($"- 切片大小: {sliceSize}");
            Debug.Log($"- 耗时: {stopwatch.ElapsedMilliseconds}ms");
            
            positions.Dispose();
        }

        /// <summary>
        /// 批量切片复制
        /// </summary>
        public static void BatchSliceCopyExample()
        {
            NativeArray<int> source = new NativeArray<int>(100, Allocator.Temp);
            NativeArray<int> dest = new NativeArray<int>(100, Allocator.Temp);
            
            // 填充源数据
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = i;
            }
            
            // 分批复制
            int batchSize = 10;
            for (int i = 0; i < source.Length; i += batchSize)
            {
                NativeSlice<int> srcSlice = new NativeSlice<int>(source, i, batchSize);
                NativeSlice<int> dstSlice = new NativeSlice<int>(dest, i, batchSize);
                
                unsafe
                {
                    void* srcPtr = NativeSliceUnsafeUtility.GetUnsafePtr(srcSlice);
                    void* dstPtr = NativeSliceUnsafeUtility.GetUnsafePtr(dstSlice);
                    
                    UnsafeUtility.MemCpy(dstPtr, srcPtr, batchSize * sizeof(int));
                }
            }
            
            Debug.Log($"批量切片复制完成");
            Debug.Log($"- 源数组前10个: [{string.Join(", ", new NativeSlice<int>(source, 0, 10).ToArray())}]");
            Debug.Log($"- 目标数组前10个: [{string.Join(", ", new NativeSlice<int>(dest, 0, 10).ToArray())}]");
            
            source.Dispose();
            dest.Dispose();
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建自定义NativeSlice包装器
        /// </summary>
        public static void CustomNativeSliceWrapperExample()
        {
            unsafe
            {
                // 分配原始内存
                int size = 100;
                void* memory = UnsafeUtility.Malloc(size * sizeof(float), 4, Allocator.Persistent);
                
                // 转换为NativeSlice
                NativeSlice<float> nativeSlice = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<float>(
                    memory, 
                    sizeof(float),
                    size
                );
                
                #if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle safetyHandle = AtomicSafetyHandle.Create();
                NativeSliceUnsafeUtility.SetAtomicSafetyHandle(ref nativeSlice, safetyHandle);
                #endif
                
                // 使用NativeSlice
                float* ptr = (float*)memory;
                for (int i = 0; i < size; i++)
                {
                    ptr[i] = i * 0.5f;
                }
                
                Debug.Log($"自定义NativeSlice包装器:");
                Debug.Log($"- 大小: {size}");
                Debug.Log($"- 第一个元素: {nativeSlice[0]}");
                Debug.Log($"- 最后一个元素: {nativeSlice[size - 1]}");
                
                #if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle.Release(safetyHandle);
                #endif
                
                // 清理
                UnsafeUtility.Free(memory, Allocator.Persistent);
            }
        }

        /// <summary>
        /// NativeSlice与NativeArray互操作
        /// </summary>
        public static void SliceArrayInteropExample()
        {
            NativeArray<Vector3> positions = new NativeArray<Vector3>(50, Allocator.Temp);
            
            // 填充数据
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector3(i, i * 2, i * 3);
            }
            
            // 创建多个切片
            NativeSlice<Vector3> slice1 = new NativeSlice<Vector3>(positions, 0, 10);
            NativeSlice<Vector3> slice2 = new NativeSlice<Vector3>(positions, 10, 10);
            NativeSlice<Vector3> slice3 = new NativeSlice<Vector3>(positions, 20, 10);
            
            unsafe
            {
                // 处理每个切片
                Vector3* ptr1 = (Vector3*)NativeSliceUnsafeUtility.GetUnsafePtr(slice1);
                Vector3* ptr2 = (Vector3*)NativeSliceUnsafeUtility.GetUnsafePtr(slice2);
                Vector3* ptr3 = (Vector3*)NativeSliceUnsafeUtility.GetUnsafePtr(slice3);
                
                for (int i = 0; i < 10; i++)
                {
                    ptr1[i] *= 0.5f;
                    ptr2[i] *= 1.0f;
                    ptr3[i] *= 1.5f;
                }
            }
            
            Debug.Log($"NativeSlice与NativeArray互操作:");
            Debug.Log($"- 切片1第一个: {slice1[0]}");
            Debug.Log($"- 切片2第一个: {slice2[0]}");
            Debug.Log($"- 切片3第一个: {slice3[0]}");
            
            positions.Dispose();
        }

        #endregion
    }
}
