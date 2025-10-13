using UnityEngine;
using UnityEditor;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System;

namespace UnityEditor.Examples
{
    /// <summary>
    /// NativeArrayUnsafeUtility 工具类示例
    /// 提供NativeArray不安全操作相关的实用工具功能
    /// </summary>
    public static class NativeArrayUnsafeUtilityExample
    {
        #region 转换现有数据示例

        /// <summary>
        /// 转换现有数据为NativeArray
        /// </summary>
        public static void ConvertExistingDataToNativeArrayExample()
        {
            unsafe
            {
                int[] managedArray = { 1, 2, 3, 4, 5 };
                
                fixed (int* ptr = managedArray)
                {
                    // 转换为NativeArray
                    NativeArray<int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(
                        ptr, 
                        managedArray.Length, 
                        Allocator.None
                    );
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeArray, AtomicSafetyHandle.Create());
                    #endif
                    
                    Debug.Log($"转换完成:");
                    Debug.Log($"- 原始数组: [{string.Join(", ", managedArray)}]");
                    Debug.Log($"- NativeArray长度: {nativeArray.Length}");
                    Debug.Log($"- NativeArray[0]: {nativeArray[0]}");
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle.Release(NativeArrayUnsafeUtility.GetAtomicSafetyHandle(nativeArray));
                    #endif
                }
            }
        }

        /// <summary>
        /// 转换现有数据（带验证）
        /// </summary>
        public static void ConvertExistingDataWithValidationExample()
        {
            unsafe
            {
                float[] floatArray = { 1.5f, 2.5f, 3.5f, 4.5f, 5.5f };
                
                fixed (float* ptr = floatArray)
                {
                    NativeArray<float> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>(
                        ptr, 
                        floatArray.Length, 
                        Allocator.None
                    );
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle safetyHandle = AtomicSafetyHandle.Create();
                    NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeArray, safetyHandle);
                    #endif
                    
                    // 验证数据
                    bool isValid = true;
                    for (int i = 0; i < nativeArray.Length; i++)
                    {
                        if (Math.Abs(nativeArray[i] - floatArray[i]) > 0.001f)
                        {
                            isValid = false;
                            break;
                        }
                    }
                    
                    Debug.Log($"数据验证: {(isValid ? "通过" : "失败")}");
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle.Release(safetyHandle);
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
            NativeArray<int> nativeArray = new NativeArray<int>(5, Allocator.Temp);
            
            // 填充数据
            for (int i = 0; i < nativeArray.Length; i++)
            {
                nativeArray[i] = i * 10;
            }
            
            unsafe
            {
                void* ptr = NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
                int* intPtr = (int*)ptr;
                
                Debug.Log("NativeArray不安全指针:");
                Debug.Log($"- 指针地址: 0x{((IntPtr)ptr).ToString("X")}");
                Debug.Log($"- 第一个元素: {intPtr[0]}");
                Debug.Log($"- 最后一个元素: {intPtr[nativeArray.Length - 1]}");
            }
            
            nativeArray.Dispose();
        }

        /// <summary>
        /// 获取只读不安全指针
        /// </summary>
        public static void GetUnsafeReadOnlyPtrExample()
        {
            NativeArray<float> nativeArray = new NativeArray<float>(5, Allocator.Temp);
            
            for (int i = 0; i < nativeArray.Length; i++)
            {
                nativeArray[i] = i * 1.5f;
            }
            
            unsafe
            {
                void* ptr = NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(nativeArray);
                float* floatPtr = (float*)ptr;
                
                Debug.Log("NativeArray只读指针:");
                for (int i = 0; i < nativeArray.Length; i++)
                {
                    Debug.Log($"- [{i}] = {floatPtr[i]}");
                }
            }
            
            nativeArray.Dispose();
        }

        #endregion

        #region 获取缓冲区指针示例

        /// <summary>
        /// 获取缓冲区指针
        /// </summary>
        public static void GetUnsafeBufferPointerWithoutChecksExample()
        {
            NativeArray<Vector3> positions = new NativeArray<Vector3>(10, Allocator.Temp);
            
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector3(i, i * 2, i * 3);
            }
            
            unsafe
            {
                void* bufferPtr = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(positions);
                Vector3* vec3Ptr = (Vector3*)bufferPtr;
                
                Debug.Log("NativeArray缓冲区指针:");
                Debug.Log($"- 第一个位置: {vec3Ptr[0]}");
                Debug.Log($"- 中间位置: {vec3Ptr[positions.Length / 2]}");
                Debug.Log($"- 最后位置: {vec3Ptr[positions.Length - 1]}");
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
            NativeArray<int> nativeArray = new NativeArray<int>(5, Allocator.Temp);
            
            #if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle safetyHandle = NativeArrayUnsafeUtility.GetAtomicSafetyHandle(nativeArray);
            
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
                    NativeArray<int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(
                        ptr, 
                        managedArray.Length, 
                        Allocator.None
                    );
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle safetyHandle = AtomicSafetyHandle.Create();
                    NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeArray, safetyHandle);
                    
                    Debug.Log("原子安全句柄已设置");
                    
                    AtomicSafetyHandle.Release(safetyHandle);
                    #else
                    Debug.Log("原子安全检查未启用");
                    #endif
                }
            }
        }

        #endregion

        #region 内存操作示例

        /// <summary>
        /// 直接内存访问
        /// </summary>
        public static void DirectMemoryAccessExample()
        {
            NativeArray<int> nativeArray = new NativeArray<int>(10, Allocator.Temp);
            
            unsafe
            {
                int* ptr = (int*)NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
                
                // 直接写入内存
                for (int i = 0; i < nativeArray.Length; i++)
                {
                    ptr[i] = i * i;
                }
            }
            
            Debug.Log($"直接内存访问结果: [{string.Join(", ", nativeArray.ToArray())}]");
            
            nativeArray.Dispose();
        }

        /// <summary>
        /// 内存复制
        /// </summary>
        public static void MemoryCopyExample()
        {
            NativeArray<float> source = new NativeArray<float>(5, Allocator.Temp);
            NativeArray<float> dest = new NativeArray<float>(5, Allocator.Temp);
            
            // 填充源数组
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = i * 2.5f;
            }
            
            unsafe
            {
                void* srcPtr = NativeArrayUnsafeUtility.GetUnsafePtr(source);
                void* dstPtr = NativeArrayUnsafeUtility.GetUnsafePtr(dest);
                
                UnsafeUtility.MemCpy(dstPtr, srcPtr, source.Length * UnsafeUtility.SizeOf<float>());
            }
            
            Debug.Log($"源数组: [{string.Join(", ", source.ToArray())}]");
            Debug.Log($"目标数组: [{string.Join(", ", dest.ToArray())}]");
            
            source.Dispose();
            dest.Dispose();
        }

        #endregion

        #region 性能优化示例

        /// <summary>
        /// 高性能数据处理
        /// </summary>
        public static void HighPerformanceDataProcessingExample()
        {
            int count = 10000;
            NativeArray<Vector3> positions = new NativeArray<Vector3>(count, Allocator.Temp);
            
            // 初始化数据
            for (int i = 0; i < count; i++)
            {
                positions[i] = new Vector3(i, i, i);
            }
            
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            unsafe
            {
                Vector3* ptr = (Vector3*)NativeArrayUnsafeUtility.GetUnsafePtr(positions);
                
                // 高性能处理
                for (int i = 0; i < count; i++)
                {
                    ptr[i] *= 2.0f;
                }
            }
            
            stopwatch.Stop();
            
            Debug.Log($"高性能数据处理:");
            Debug.Log($"- 处理数量: {count}");
            Debug.Log($"- 耗时: {stopwatch.ElapsedMilliseconds}ms");
            
            positions.Dispose();
        }

        /// <summary>
        /// 批量数据转换
        /// </summary>
        public static void BatchDataConversionExample()
        {
            int batchSize = 1000;
            
            unsafe
            {
                int[] managedData = new int[batchSize];
                for (int i = 0; i < batchSize; i++)
                {
                    managedData[i] = i;
                }
                
                fixed (int* ptr = managedData)
                {
                    NativeArray<int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(
                        ptr, 
                        batchSize, 
                        Allocator.None
                    );
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle safetyHandle = AtomicSafetyHandle.Create();
                    NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeArray, safetyHandle);
                    #endif
                    
                    Debug.Log($"批量数据转换完成: {batchSize}个元素");
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle.Release(safetyHandle);
                    #endif
                }
            }
        }

        #endregion

        #region 综合示例

        /// <summary>
        /// 创建自定义NativeArray包装器
        /// </summary>
        public static void CustomNativeArrayWrapperExample()
        {
            unsafe
            {
                // 分配原始内存
                int size = 100;
                void* memory = UnsafeUtility.Malloc(size * sizeof(int), 4, Allocator.Persistent);
                
                // 转换为NativeArray
                NativeArray<int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(
                    memory, 
                    size, 
                    Allocator.Persistent
                );
                
                #if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle safetyHandle = AtomicSafetyHandle.Create();
                NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeArray, safetyHandle);
                #endif
                
                // 使用NativeArray
                for (int i = 0; i < size; i++)
                {
                    nativeArray[i] = i * 2;
                }
                
                Debug.Log($"自定义NativeArray包装器:");
                Debug.Log($"- 大小: {size}");
                Debug.Log($"- 第一个元素: {nativeArray[0]}");
                Debug.Log($"- 最后一个元素: {nativeArray[size - 1]}");
                
                #if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle.Release(safetyHandle);
                #endif
                
                // 清理
                UnsafeUtility.Free(memory, Allocator.Persistent);
            }
        }

        /// <summary>
        /// NativeArray互操作性
        /// </summary>
        public static void NativeArrayInteropExample()
        {
            // 创建托管数组
            Vector3[] managedPositions = new Vector3[50];
            for (int i = 0; i < managedPositions.Length; i++)
            {
                managedPositions[i] = new Vector3(i, i * 2, i * 3);
            }
            
            unsafe
            {
                fixed (Vector3* ptr = managedPositions)
                {
                    // 转换为NativeArray
                    NativeArray<Vector3> nativePositions = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>(
                        ptr, 
                        managedPositions.Length, 
                        Allocator.None
                    );
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle safetyHandle = AtomicSafetyHandle.Create();
                    NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativePositions, safetyHandle);
                    #endif
                    
                    // 使用NativeArray进行处理
                    Vector3* nativePtr = (Vector3*)NativeArrayUnsafeUtility.GetUnsafePtr(nativePositions);
                    for (int i = 0; i < nativePositions.Length; i++)
                    {
                        nativePtr[i] *= 0.5f;
                    }
                    
                    Debug.Log($"NativeArray互操作性:");
                    Debug.Log($"- 原始第一个: {managedPositions[0]}");
                    Debug.Log($"- 处理后第一个: {nativePositions[0]}");
                    
                    #if ENABLE_UNITY_COLLECTIONS_CHECKS
                    AtomicSafetyHandle.Release(safetyHandle);
                    #endif
                }
            }
        }

        #endregion
    }
}
