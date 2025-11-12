# .NET IO API 参考文档

本文档基于 `IOExample.cs` 文件，详细介绍了 .NET IO 相关的所有常用 API。

## 目录
- [路径操作 (Path类)](#路径操作-path类)
- [文件操作 (File类)](#文件操作-file类)
- [目录操作 (Directory类)](#目录操作-directory类)
- [流操作 (Stream类)](#流操作-stream类)
- [异步IO操作](#异步io操作)
- [文件信息 (FileInfo类)](#文件信息-fileinfo类)
- [目录信息 (DirectoryInfo类)](#目录信息-directoryinfo类)
- [驱动器信息 (DriveInfo类)](#驱动器信息-driveinfo类)
- [文件系统监控 (FileSystemWatcher类)](#文件系统监控-filesystemwatcher类)
- [压缩文件操作](#压缩文件操作)
- [高级流操作](#高级流操作)

---

## 路径操作 (Path类)

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Path.GetFileName` | 获取文件名（包含扩展名） |
| `Path.GetFileNameWithoutExtension` | 获取不带扩展名的文件名 |
| `Path.GetExtension` | 获取文件扩展名（包含点号） |
| `Path.GetDirectoryName` | 获取目录名 |
| `Path.GetPathRoot` | 获取根目录 |
| `Path.Combine` | 安全地组合路径，自动处理路径分隔符 |
| `Path.HasExtension` | 检查路径是否有扩展名 |
| `Path.IsPathRooted` | 检查是否是绝对路径 |
| `Path.GetTempPath` | 获取系统临时目录路径 |
| `Path.GetTempFileName` | 获取临时文件名（会创建空文件） |
| `Path.GetRandomFileName` | 获取随机文件名（不会创建文件） |
| `Path.GetFullPath` | 获取完整路径，解析相对路径和特殊字符 |
| `Path.GetInvalidPathChars` | 获取不允许在路径中使用的字符 |
| `Path.GetInvalidFileNameChars` | 获取不允许在文件名中使用的字符 |
| `Path.IsPathFullyQualified` | 检查路径是否为完全限定路径（绝对路径） |
| `Path.DirectorySeparatorChar` | 目录分隔符（Windows: \, Unix: /） |
| `Path.AltDirectorySeparatorChar` | 替代目录分隔符（Windows: /, Unix: /） |
| `Path.PathSeparator` | 路径分隔符（Windows: ;, Unix: :） |
| `Path.VolumeSeparatorChar` | 卷分隔符（Windows: :, Unix: :） |

---

## 文件操作 (File类)

### 基本文件操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `File.WriteAllText` | 将字符串写入文件，如果文件存在则覆盖 |
| `File.ReadAllText` | 读取文件的所有内容为字符串 |
| `File.ReadAllLines` | 读取文件的所有行到字符串数组 |
| `File.ReadAllBytes` | 读取文件的所有字节 |
| `File.AppendAllText` | 在文件末尾追加内容，不会覆盖原内容 |
| `File.Exists` | 检查指定路径的文件是否存在 |
| `File.Copy` | 复制文件到新位置 |
| `File.Move` | 移动文件到新位置（相当于剪切+粘贴） |
| `File.Delete` | 删除指定文件 |

### File.Open 相关操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `File.Open` | 以指定模式打开文件，返回FileStream |
| `File.OpenWrite` | 以只写方式打开文件（会覆盖原内容） |
| `File.OpenRead` | 以只读方式打开文件 |
| `File.OpenText` | 以文本模式打开文件，返回StreamReader |
| `File.CreateText` | 创建文本文件，返回StreamWriter |
| `File.AppendText` | 以追加模式打开文本文件，返回StreamWriter |

### 文件属性操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `File.GetAttributes` | 获取文件或目录的属性 |
| `File.SetAttributes` | 设置文件或目录的属性 |
| `File.GetCreationTime` | 获取文件创建时间 |
| `File.SetCreationTime` | 设置文件创建时间 |
| `File.GetLastWriteTime` | 获取文件最后写入时间 |
| `File.SetLastWriteTime` | 设置文件最后写入时间 |
| `File.GetLastAccessTime` | 获取文件最后访问时间 |
| `File.SetLastAccessTime` | 设置文件最后访问时间 |

---

## 目录操作 (Directory类)

### 基本目录操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Directory.CreateDirectory` | 创建目录，如果已存在则不会报错 |
| `Directory.Exists` | 检查指定路径的目录是否存在 |
| `Directory.Move` | 移动目录到新位置 |
| `Directory.Delete` | 删除目录 |

### 目录内容操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Directory.GetFiles` | 获取指定目录中的文件 |
| `Directory.GetDirectories` | 获取指定目录中的子目录 |
| `Directory.EnumerateFiles` | 返回可枚举的文件集合，比GetFiles更高效 |
| `Directory.EnumerateDirectories` | 返回可枚举的目录集合 |

### 目录路径操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Directory.GetCurrentDirectory` | 获取当前工作目录 |
| `Directory.SetCurrentDirectory` | 设置当前工作目录 |
| `Directory.GetLogicalDrives` | 获取计算机上的逻辑驱动器名称 |

### 目录时间操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Directory.GetCreationTime` | 获取目录创建时间 |
| `Directory.SetCreationTime` | 设置目录创建时间 |
| `Directory.GetLastWriteTime` | 获取目录最后写入时间 |
| `Directory.SetLastWriteTime` | 设置目录最后写入时间 |
| `Directory.GetLastAccessTime` | 获取目录最后访问时间 |
| `Directory.SetLastAccessTime` | 设置目录最后访问时间 |

---

## 流操作 (Stream类)

### FileStream 文件流操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `FileStream` | 文件流，用于文件的读写操作 |
| `FileStream.Read` | 从流中读取数据 |
| `FileStream.Write` | 向流中写入数据 |
| `FileStream.Seek` | 设置流的位置 |
| `FileStream.Flush` | 清除缓冲区，确保数据写入 |
| `FileStream.CopyTo` | 将当前流的内容复制到目标流 |

### MemoryStream 内存流操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `MemoryStream` | 内存流，数据存储在内存中，速度快 |
| `MemoryStream.ToArray` | 将内存流内容复制到新数组 |
| `MemoryStream.GetBuffer` | 获取底层缓冲区（可能包含未使用的空间） |
| `MemoryStream.Position` | 获取或设置流中的当前位置 |

### StreamReader/StreamWriter 文本流操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `StreamReader` | 专门用于文本读取的流 |
| `StreamReader.ReadLine` | 读取一行文本 |
| `StreamReader.ReadToEnd` | 读取所有剩余文本 |
| `StreamWriter` | 专门用于文本写入的流 |
| `StreamWriter.WriteLine` | 写入一行文本 |
| `StreamWriter.Write` | 写入文本 |
| `StreamWriter.Flush` | 确保数据立即写入 |

### BinaryReader/BinaryWriter 二进制流操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `BinaryReader` | 专门用于二进制数据读取 |
| `BinaryReader.ReadInt32` | 读取32位整数 |
| `BinaryReader.ReadDouble` | 读取双精度浮点数 |
| `BinaryReader.ReadString` | 读取字符串 |
| `BinaryReader.ReadBoolean` | 读取布尔值 |
| `BinaryWriter` | 专门用于二进制数据写入 |
| `BinaryWriter.Write` | 写入各种类型的数据 |

---

## 异步IO操作

### 异步文件操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `File.WriteAllTextAsync` | 异步写入文件内容 |
| `File.ReadAllTextAsync` | 异步读取文件内容 |
| `File.ReadAllLinesAsync` | 异步读取文件的所有行 |
| `File.AppendAllTextAsync` | 异步追加内容到文件 |

### 异步流操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `StreamWriter.WriteLineAsync` | 异步写入一行文本 |
| `StreamReader.ReadLineAsync` | 异步读取一行文本 |
| `StreamReader.ReadToEndAsync` | 异步读取所有剩余文本 |
| `FileStream.WriteAsync` | 异步写入数据到文件流 |
| `FileStream.ReadAsync` | 异步从文件流读取数据 |
| `Stream.CopyToAsync` | 异步将流内容复制到目标流 |

### 异步批量操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Task.WhenAll` | 等待多个异步任务完成 |
| `CopyFileAsync` | 异步文件复制方法 |
| `CompareFilesAsync` | 异步文件比较方法 |

---

## 文件信息 (FileInfo类)

### 基本属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `FileInfo.Name` | 文件名（不包含路径） |
| `FileInfo.FullName` | 完整路径 |
| `FileInfo.DirectoryName` | 目录名 |
| `FileInfo.Length` | 文件大小（字节） |
| `FileInfo.Extension` | 文件扩展名 |
| `FileInfo.Exists` | 文件是否存在 |
| `FileInfo.IsReadOnly` | 是否只读 |

### 时间属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `FileInfo.CreationTime` | 创建时间 |
| `FileInfo.LastAccessTime` | 最后访问时间 |
| `FileInfo.LastWriteTime` | 最后写入时间 |
| `FileInfo.LastWriteTimeUtc` | 最后写入时间（UTC） |

### 文件操作方法

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `FileInfo.CopyTo` | 复制文件到新位置 |
| `FileInfo.MoveTo` | 移动文件到新位置 |
| `FileInfo.Delete` | 删除文件 |

---

## 目录信息 (DirectoryInfo类)

### 基本属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `DirectoryInfo.Name` | 目录名（不包含路径） |
| `DirectoryInfo.FullName` | 完整路径 |
| `DirectoryInfo.Parent` | 父目录 |
| `DirectoryInfo.Root` | 根目录 |
| `DirectoryInfo.Exists` | 目录是否存在 |
| `DirectoryInfo.IsReadOnly` | 是否只读 |

### 时间属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `DirectoryInfo.CreationTime` | 创建时间 |
| `DirectoryInfo.LastAccessTime` | 最后访问时间 |
| `DirectoryInfo.LastWriteTime` | 最后写入时间 |

### 目录操作方法

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `DirectoryInfo.CreateSubdirectory` | 创建子目录 |
| `DirectoryInfo.GetDirectories` | 获取指定目录中的子目录 |
| `DirectoryInfo.MoveTo` | 移动目录到新位置 |
| `DirectoryInfo.Delete` | 删除目录 |

---

## 驱动器信息 (DriveInfo类)

### 基本属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `DriveInfo.Name` | 驱动器名 |
| `DriveInfo.VolumeLabel` | 卷标 |
| `DriveInfo.DriveFormat` | 文件系统格式 |
| `DriveInfo.AvailableFreeSpace` | 可用空间（字节） |
| `DriveInfo.TotalFreeSpace` | 总空间（字节） |
| `DriveInfo.TotalSize` | 总大小（字节） |
| `DriveInfo.IsReady` | 驱动器是否可用 |
| `DriveInfo.DriveType` | 驱动器类型 |

---

## 文件系统监控 (FileSystemWatcher类)

### 基本配置

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `FileSystemWatcher.Filter` | 设置文件过滤器 |
| `FileSystemWatcher.IncludeSubdirectories` | 是否监控子目录 |
| `FileSystemWatcher.EnableRaisingEvents` | 启用或禁用事件 |

### 事件处理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `FileSystemWatcher.Created` | 文件或目录创建事件 |
| `FileSystemWatcher.Deleted` | 文件或目录删除事件 |
| `FileSystemWatcher.Changed` | 文件或目录修改事件 |
| `FileSystemWatcher.Renamed` | 文件或目录重命名事件 |

---

## 压缩文件操作

### GZip压缩

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `GZipStream` | 用于GZip压缩和解压缩 |
| `GZipStream(stream, CompressionMode.Compress)` | 创建压缩流 |
| `GZipStream(stream, CompressionMode.Decompress)` | 创建解压缩流 |

### Deflate压缩

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `DeflateStream` | 用于Deflate压缩和解压缩 |
| `DeflateStream(stream, CompressionMode.Compress)` | 创建压缩流 |
| `DeflateStream(stream, CompressionMode.Decompress)` | 创建解压缩流 |

### ZIP文件操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ZipArchive` | 用于创建和管理ZIP文件 |
| `ZipFile.Open` | 打开ZIP文件 |
| `ZipArchive.CreateEntryFromFile` | 添加文件到ZIP |
| `ZipArchiveEntry.ExtractToFile` | 从ZIP提取文件 |

---

## 高级流操作

### BufferedStream 缓冲流操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `BufferedStream` | 为其他流提供缓冲功能，提高性能 |
| `BufferedStream(stream, bufferSize)` | 创建指定缓冲区大小的缓冲流 |
| `BufferedStream.Flush` | 确保数据写入磁盘 |

### Stream位置操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Stream.Seek` | 设置流的位置 |
| `SeekOrigin.Begin` | 从开始位置计算偏移 |
| `SeekOrigin.Current` | 从当前位置计算偏移 |
| `SeekOrigin.End` | 从结束位置计算偏移 |

### Stream复制操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Stream.CopyTo` | 将当前流的内容复制到目标流 |
| `Stream.CopyToAsync` | 异步将流内容复制到目标流 |

### Stream刷新操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Stream.Flush` | 清除缓冲区，确保数据写入 |
| `StreamWriter.Flush` | 确保文本数据立即写入 |

---

## 枚举值说明

### FileMode 枚举

| 值 | 说明 |
|----|------|
| `Create` | 创建新文件（已存在则覆盖） |
| `CreateNew` | 创建新文件（已存在则抛异常） |
| `Open` | 打开已存在文件（不存在则抛异常） |
| `OpenOrCreate` | 存在则打开，不存在则创建 |
| `Append` | 打开文件并定位到末尾，不存在则创建 |
| `Truncate` | 打开并清空文件内容 |

### FileAccess 枚举

| 值 | 说明 |
|----|------|
| `Read` | 只读模式 |
| `Write` | 只写模式 |
| `ReadWrite` | 读写模式 |

### FileShare 枚举

| 值 | 说明 |
|----|------|
| `None` | 不共享，其他进程无法访问 |
| `Read` | 允许其他进程读取 |
| `Write` | 允许其他进程写入 |
| `ReadWrite` | 允许其他进程读写 |
| `Delete` | 允许其他进程删除 |

### SearchOption 枚举

| 值 | 说明 |
|----|------|
| `TopDirectoryOnly` | 只搜索当前目录（默认） |
| `AllDirectories` | 搜索所有子目录 |

### CompressionMode 枚举

| 值 | 说明 |
|----|------|
| `Compress` | 压缩模式 |
| `Decompress` | 解压缩模式 |

---

## 使用注意事项

1. **路径处理**：始终使用 `Path.Combine()` 来组合路径，避免硬编码路径分隔符
2. **资源管理**：使用 `using` 语句确保流资源正确释放
3. **异常处理**：IO操作可能抛出异常，需要适当的异常处理
4. **异步操作**：对于大文件或网络操作，优先使用异步方法
5. **编码格式**：文本操作时指定编码格式，推荐使用 UTF-8
6. **文件锁定**：注意文件共享模式，避免不必要的文件锁定
7. **性能考虑**：对于大文件，使用缓冲流和分块处理
8. **跨平台兼容**：注意不同操作系统的路径分隔符差异

---

## 示例代码

完整的示例代码请参考 `IOExample.cs` 文件，其中包含了所有API的详细使用示例和中文注释。 