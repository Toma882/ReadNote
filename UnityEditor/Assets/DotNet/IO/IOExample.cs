// IOExample.cs
// .NET IO API使用详解示例
// 包含文件操作、流操作、异步IO等常用功能
// 每个方法、关键步骤、枚举值均有详细中文注释
// 适合.NET初学者学习和查阅

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Net.Sockets;
using UnityEngine;
using System.Linq;

namespace DotNet.IO
{
    /// <summary>
    /// .NET IO API使用详解示例
    /// 演示文件、目录、流、异步IO等常用操作
    /// </summary>
    public class IOExample : MonoBehaviour
    {
        [Header("IO示例")]
        [Tooltip("是否自动运行所有示例代码")]
        [SerializeField] private bool runExamples = true;
        
        // 测试用的文件和目录路径
        // 注意：在Unity中使用Application.persistentDataPath确保有写入权限
        private string testFilePath;
        private string testDirectoryPath;

        private void Start()
        {
            // Application.persistentDataPath 是Unity推荐的可写目录
            // 在不同平台下路径不同：
            // Windows: C:/Users/用户名/AppData/LocalLow/公司名/项目名/
            // macOS: ~/Library/Application Support/公司名/项目名/
            // Android: /storage/emulated/0/Android/data/包名/files/
            // iOS: /var/mobile/Containers/Data/Application/应用ID/Documents/
            testFilePath = Path.Combine(Application.persistentDataPath, "test_file.txt");
            testDirectoryPath = Path.Combine(Application.persistentDataPath, "test_directory");
            
            if (runExamples)
            {
                RunAllExamples();
            }
        }

        /// <summary>
        /// 运行所有IO相关示例
        /// 按顺序执行：路径操作 -> 文件操作 -> 目录操作 -> 流操作 -> 异步IO -> FileInfo -> DirectoryInfo -> DriveInfo -> 文件监控 -> 压缩文件 -> 高级流操作
        /// </summary>
        private async void RunAllExamples()
        {
            Debug.Log("=== .NET IO API示例开始 ===");
            
            PathExample();      // 路径操作（Path类）- 基础操作，放在最前面
            FileExample();      // 文件基本操作（File类、File.Open、FileMode等）
            DirectoryExample(); // 目录操作（Directory类）
            StreamExample();    // 流操作（FileStream、MemoryStream、StreamReader等）
            await AsyncIOExample(); // 异步IO（async/await）
            FileInfoExample();  // FileInfo用法（文件信息获取）
            DirectoryInfoExample(); // DirectoryInfo用法（目录信息获取）
            DriveInfoExample(); // DriveInfo用法（驱动器信息）
            FileSystemWatcherExample(); // 文件系统监控
            CompressionExample(); // 压缩文件操作
            AdvancedStreamExample(); // 高级流操作（缓冲流、网络流等）
            
            Debug.Log("=== .NET IO API示例结束 ===");
        }

        // ================= 文件操作 =================
        /// <summary>
        /// File类常用API演示，包括File.Open、FileMode、FileAccess、FileShare等
        /// File类是静态类，提供文件操作的便捷方法
        /// </summary>
        private void FileExample()
        {
            Debug.Log("--- File类示例 ---");
            
            try
            {
                // ========== 基本文件操作 ==========
                
                // 写入文件（覆盖）
                // WriteAllText: 将字符串写入文件，如果文件存在则覆盖
                // 参数：文件路径，内容，编码格式（推荐UTF8以支持中文）
                string content = "Hello, .NET IO!\n这是第二行内容。\n第三行包含中文。";
                File.WriteAllText(testFilePath, content, Encoding.UTF8);
                Debug.Log($"文件已写入: {testFilePath}");
                
                // 读取整个文件内容
                // ReadAllText: 读取文件的所有内容为字符串
                // 返回值：文件内容的字符串
                string readContent = File.ReadAllText(testFilePath, Encoding.UTF8);
                Debug.Log($"文件内容:\n{readContent}");
                
                // 按行读取
                // ReadAllLines: 读取文件的所有行到字符串数组
                // 返回值：字符串数组，每个元素是一行内容
                string[] lines = File.ReadAllLines(testFilePath, Encoding.UTF8);
                Debug.Log($"文件行数: {lines.Length}");
                for (int i = 0; i < lines.Length; i++)
                {
                    Debug.Log($"第{i + 1}行: {lines[i]}");
                }
                
                // 按字节读取
                // ReadAllBytes: 读取文件的所有字节
                // 返回值：字节数组，适用于二进制文件
                byte[] bytes = File.ReadAllBytes(testFilePath);
                Debug.Log($"文件字节数: {bytes.Length}");
                
                // 追加内容到文件末尾
                // AppendAllText: 在文件末尾追加内容，不会覆盖原内容
                File.AppendAllText(testFilePath, "\n这是追加的内容", Encoding.UTF8);
                Debug.Log("已追加内容到文件");
                
                // 检查文件是否存在
                // Exists: 检查指定路径的文件是否存在
                // 返回值：true表示存在，false表示不存在
                bool exists = File.Exists(testFilePath);
                Debug.Log($"文件是否存在: {exists}");
                
                // 获取文件信息（通过FileInfo类）
                FileInfo fileInfo = new FileInfo(testFilePath);
                Debug.Log($"文件大小: {fileInfo.Length} 字节");
                Debug.Log($"创建时间: {fileInfo.CreationTime}");
                Debug.Log($"最后修改时间: {fileInfo.LastWriteTime}");
                
                // 复制文件
                // Copy: 复制文件到新位置
                // 参数：源文件路径，目标文件路径，是否覆盖（可选）
                string copyPath = testFilePath.Replace(".txt", "_copy.txt");
                File.Copy(testFilePath, copyPath, overwrite: true);
                Debug.Log($"文件已复制到: {copyPath}");
                
                // 移动文件
                // Move: 移动文件到新位置（相当于剪切+粘贴）
                string movePath = testFilePath.Replace(".txt", "_moved.txt");
                File.Move(copyPath, movePath);
                Debug.Log($"文件已移动到: {movePath}");
                
                // 删除文件
                // Delete: 删除指定文件
                // 注意：删除后无法恢复，请谨慎使用
                File.Delete(movePath);
                Debug.Log("移动的文件已删除");

                // ========== File.Open 相关演示 ==========
                // File.Open 提供了更精细的文件操作控制
                
                // FileMode 枚举常用值说明：
                //   Create      - 创建新文件（已存在则覆盖）
                //   CreateNew   - 创建新文件（已存在则抛异常）
                //   Open        - 打开已存在文件（不存在则抛异常）
                //   OpenOrCreate- 存在则打开，不存在则创建
                //   Append      - 打开文件并定位到末尾，不存在则创建
                //   Truncate    - 打开并清空文件内容
                // FileAccess 枚举说明：
                //   Read        - 只读模式
                //   Write       - 只写模式
                //   ReadWrite   - 读写模式
                // FileShare 枚举说明：
                //   None        - 不共享，其他进程无法访问
                //   Read        - 允许其他进程读取
                //   Write       - 允许其他进程写入
                //   ReadWrite   - 允许其他进程读写
                //   Delete      - 允许其他进程删除
                
                // 用File.Open以只读方式打开
                // 返回FileStream对象，需要手动关闭
                using (FileStream fs = File.Open(testFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string openContent = Encoding.UTF8.GetString(buffer);
                    Debug.Log($"File.Open读取内容: {openContent}");
                }
                
                // 用File.OpenWrite以只写方式打开（会覆盖原内容）
                // 简化版本，默认FileMode.OpenOrCreate，FileAccess.Write
                using (FileStream fs = File.OpenWrite(testFilePath))
                {
                    string writeContent = "通过OpenWrite写入的内容";
                    byte[] bytes2 = Encoding.UTF8.GetBytes(writeContent);
                    fs.Write(bytes2, 0, bytes2.Length);
                    Debug.Log("File.OpenWrite写入完成");
                }
                
                // 用File.OpenRead以只读方式打开
                // 简化版本，默认FileMode.Open，FileAccess.Read
                using (FileStream fs = File.OpenRead(testFilePath))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string openReadContent = Encoding.UTF8.GetString(buffer);
                    Debug.Log($"File.OpenRead读取内容: {openReadContent}");
                }
                
                // 用File.OpenText读取文本
                // 返回StreamReader对象，专门用于文本读取
                using (StreamReader reader = File.OpenText(testFilePath))
                {
                    string text = reader.ReadToEnd();
                    Debug.Log($"File.OpenText读取内容: {text}");
                }
                
                // 用File.CreateText写入文本（覆盖）
                // 返回StreamWriter对象，专门用于文本写入
                string createTextPath = testFilePath.Replace(".txt", "_createtext.txt");
                using (StreamWriter writer = File.CreateText(createTextPath))
                {
                    writer.WriteLine("第一行");
                    writer.WriteLine("第二行");
                    writer.WriteLine("第三行");
                    Debug.Log("File.CreateText写入完成");
                }
                
                // 用File.AppendText追加文本
                // 在文件末尾追加内容，不会覆盖原内容
                using (StreamWriter writer = File.AppendText(testFilePath))
                {
                    writer.WriteLine("这是追加的文本行");
                    Debug.Log("File.AppendText追加完成");
                }
                
                // 清理测试文件
                if (File.Exists(createTextPath)) File.Delete(createTextPath);
                
                // ========== 文件属性操作 ==========
                
                // 获取文件属性
                // GetAttributes: 获取文件或目录的属性
                FileAttributes attributes = File.GetAttributes(testFilePath);
                Debug.Log($"文件属性: {attributes}");
                
                // 检查特定属性
                bool isReadOnly = (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
                bool isHidden = (attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                bool isSystem = (attributes & FileAttributes.System) == FileAttributes.System;
                Debug.Log($"是否只读: {isReadOnly}");
                Debug.Log($"是否隐藏: {isHidden}");
                Debug.Log($"是否系统文件: {isSystem}");
                
                // 设置文件属性
                // SetAttributes: 设置文件或目录的属性
                File.SetAttributes(testFilePath, FileAttributes.ReadOnly);
                Debug.Log("已设置文件为只读");
                
                // 移除只读属性
                File.SetAttributes(testFilePath, FileAttributes.Normal);
                Debug.Log("已移除只读属性");
                
                // ========== 文件时间操作 ==========
                
                // 获取文件时间
                DateTime creationTime = File.GetCreationTime(testFilePath);
                DateTime lastWriteTime = File.GetLastWriteTime(testFilePath);
                DateTime lastAccessTime = File.GetLastAccessTime(testFilePath);
                Debug.Log($"文件创建时间: {creationTime}");
                Debug.Log($"文件最后写入时间: {lastWriteTime}");
                Debug.Log($"文件最后访问时间: {lastAccessTime}");
                
                // 设置文件时间
                // SetCreationTime: 设置文件创建时间
                // SetLastWriteTime: 设置文件最后写入时间
                // SetLastAccessTime: 设置文件最后访问时间
                DateTime newTime = DateTime.Now.AddHours(-1);
                File.SetLastWriteTime(testFilePath, newTime);
                Debug.Log($"已设置文件最后写入时间为: {newTime}");
                
                // ========== 文件大小和空间操作 ==========
                
                // 获取文件大小
                long fileSize = new FileInfo(testFilePath).Length;
                Debug.Log($"文件大小: {fileSize} 字节");
                
                // 获取磁盘空间信息
                string drivePath = Path.GetPathRoot(testFilePath);
                DriveInfo drive = new DriveInfo(drivePath);
                Debug.Log($"驱动器 {drive.Name} 可用空间: {drive.AvailableFreeSpace} 字节");
                Debug.Log($"驱动器 {drive.Name} 总空间: {drive.TotalSize} 字节");
                
                // ========== 文件锁定和共享操作 ==========
                
                // 使用文件锁定
                using (FileStream lockStream = File.Open(testFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    Debug.Log("文件已被锁定，其他进程无法访问");
                    // 在这里文件被锁定，其他进程无法访问
                }
                Debug.Log("文件锁定已释放");
                
                // ========== 文件比较操作 ==========
                
                // 创建两个相同内容的文件进行比较
                string file1Path = testFilePath.Replace(".txt", "_1.txt");
                string file2Path = testFilePath.Replace(".txt", "_2.txt");
                File.WriteAllText(file1Path, "相同内容", Encoding.UTF8);
                File.WriteAllText(file2Path, "相同内容", Encoding.UTF8);
                
                // 比较文件内容
                byte[] file1Bytes = File.ReadAllBytes(file1Path);
                byte[] file2Bytes = File.ReadAllBytes(file2Path);
                bool areEqual = file1Bytes.Length == file2Bytes.Length;
                if (areEqual)
                {
                    for (int i = 0; i < file1Bytes.Length; i++)
                    {
                        if (file1Bytes[i] != file2Bytes[i])
                        {
                            areEqual = false;
                            break;
                        }
                    }
                }
                Debug.Log($"两个文件内容是否相同: {areEqual}");
                
                // 清理测试文件
                if (File.Exists(file1Path)) File.Delete(file1Path);
                if (File.Exists(file2Path)) File.Delete(file2Path);
            }
            catch (Exception ex)
            {
                Debug.LogError($"File操作出错: {ex.Message}");
            }
        }

        // ================= 目录操作 =================
        /// <summary>
        /// Directory类常用API演示
        /// Directory类是静态类，提供目录操作的便捷方法
        /// </summary>
        private void DirectoryExample()
        {
            Debug.Log("--- Directory类示例 ---");
            try
            {
                // 创建目录（已存在不会报错）
                // CreateDirectory: 创建目录，如果已存在则不会报错
                if (!Directory.Exists(testDirectoryPath))
                {
                    Directory.CreateDirectory(testDirectoryPath);
                    Debug.Log($"目录已创建: {testDirectoryPath}");
                }
                
                // 创建子目录
                // Path.Combine: 安全地组合路径，自动处理路径分隔符
                string subDirPath = Path.Combine(testDirectoryPath, "subdir");
                Directory.CreateDirectory(subDirPath);
                Debug.Log($"子目录已创建: {subDirPath}");
                
                // 在目录中创建文件
                string testFileInDir = Path.Combine(testDirectoryPath, "test_in_dir.txt");
                File.WriteAllText(testFileInDir, "这是目录中的测试文件", Encoding.UTF8);
                
                // 获取目录中的文件
                // GetFiles: 获取指定目录中的文件
                // 参数：目录路径，搜索模式（可选），搜索选项（可选）
                string[] files = Directory.GetFiles(testDirectoryPath);
                Debug.Log($"目录中的文件数量: {files.Length}");
                foreach (string file in files)
                    Debug.Log($"文件: {Path.GetFileName(file)}");
                
                // 获取目录中的子目录
                // GetDirectories: 获取指定目录中的子目录
                string[] subDirs = Directory.GetDirectories(testDirectoryPath);
                Debug.Log($"子目录数量: {subDirs.Length}");
                foreach (string dir in subDirs)
                    Debug.Log($"子目录: {Path.GetFileName(dir)}");
                
                // 获取所有文件（包括子目录）
                // SearchOption.AllDirectories: 搜索所有子目录
                // SearchOption.TopDirectoryOnly: 只搜索当前目录（默认）
                string[] allFiles = Directory.GetFiles(testDirectoryPath, "*", SearchOption.AllDirectories);
                Debug.Log($"所有文件数量（包括子目录）: {allFiles.Length}");
                
                // 检查目录是否存在
                // Exists: 检查指定路径的目录是否存在
                bool exists = Directory.Exists(testDirectoryPath);
                Debug.Log($"目录是否存在: {exists}");
                
                // 获取目录信息（通过DirectoryInfo类）
                DirectoryInfo dirInfo = new DirectoryInfo(testDirectoryPath);
                Debug.Log($"目录创建时间: {dirInfo.CreationTime}");
                Debug.Log($"目录最后访问时间: {dirInfo.LastAccessTime}");
                
                // 移动目录
                // Move: 移动目录到新位置
                string moveDirPath = testDirectoryPath.Replace("test_directory", "test_directory_moved");
                Directory.Move(testDirectoryPath, moveDirPath);
                Debug.Log($"目录已移动到: {moveDirPath}");
                
                // 删除目录（包括内容）
                // Delete: 删除目录
                // 参数：目录路径，是否递归删除（删除所有子目录和文件）
                Directory.Delete(moveDirPath, true);
                Debug.Log("目录已删除");
                
                // ========== 目录路径操作 ==========
                
                // 获取当前工作目录
                // GetCurrentDirectory: 获取当前工作目录
                string currentDir = Directory.GetCurrentDirectory();
                Debug.Log($"当前工作目录: {currentDir}");
                
                // 设置当前工作目录
                // SetCurrentDirectory: 设置当前工作目录
                // 注意：在Unity中不建议更改当前目录
                // Directory.SetCurrentDirectory(testDirectoryPath);
                // Debug.Log($"已设置当前工作目录为: {testDirectoryPath}");
                
                // 获取逻辑驱动器
                // GetLogicalDrives: 获取计算机上的逻辑驱动器名称
                string[] drives = Directory.GetLogicalDrives();
                Debug.Log($"逻辑驱动器数量: {drives.Length}");
                foreach (string drive in drives)
                {
                    Debug.Log($"驱动器: {drive}");
                }
                
                // ========== 目录搜索操作 ==========
                
                // 使用搜索模式获取文件
                // 搜索模式支持通配符：* 和 ?
                // * 匹配任意数量的字符
                // ? 匹配单个字符
                string[] txtFiles = Directory.GetFiles(Application.persistentDataPath, "*.txt");
                Debug.Log($"找到的txt文件数量: {txtFiles.Length}");
                
                string[] testFiles = Directory.GetFiles(Application.persistentDataPath, "test*");
                Debug.Log($"找到的test开头的文件数量: {testFiles.Length}");
                
                // 使用搜索选项
                // SearchOption.AllDirectories: 搜索所有子目录
                // SearchOption.TopDirectoryOnly: 只搜索当前目录（默认）
                string[] allTxtFiles = Directory.GetFiles(Application.persistentDataPath, "*.txt", SearchOption.AllDirectories);
                Debug.Log($"所有txt文件数量（包括子目录）: {allTxtFiles.Length}");
                
                // ========== 目录时间操作 ==========
                
                // 获取目录时间
                DateTime dirCreationTime = Directory.GetCreationTime(testDirectoryPath);
                DateTime dirLastWriteTime = Directory.GetLastWriteTime(testDirectoryPath);
                DateTime dirLastAccessTime = Directory.GetLastAccessTime(testDirectoryPath);
                Debug.Log($"目录创建时间: {dirCreationTime}");
                Debug.Log($"目录最后写入时间: {dirLastWriteTime}");
                Debug.Log($"目录最后访问时间: {dirLastAccessTime}");
                
                // 设置目录时间
                // SetCreationTime: 设置目录创建时间
                // SetLastWriteTime: 设置目录最后写入时间
                // SetLastAccessTime: 设置目录最后访问时间
                DateTime newDirTime = DateTime.Now.AddHours(-1);
                Directory.SetLastWriteTime(testDirectoryPath, newDirTime);
                Debug.Log($"已设置目录最后写入时间为: {newDirTime}");
                
                // ========== 目录属性操作 ==========
                
                // 获取目录属性
                FileAttributes dirAttributes = File.GetAttributes(testDirectoryPath);
                Debug.Log($"目录属性: {dirAttributes}");
                
                // 检查目录是否为系统目录
                bool isSystemDir = (dirAttributes & FileAttributes.System) == FileAttributes.System;
                bool isHiddenDir = (dirAttributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                Debug.Log($"是否系统目录: {isSystemDir}");
                Debug.Log($"是否隐藏目录: {isHiddenDir}");
                
                // ========== 目录枚举操作 ==========
                
                // 枚举目录中的文件（使用IEnumerable）
                // EnumerateFiles: 返回可枚举的文件集合，比GetFiles更高效
                var enumeratedFiles = Directory.EnumerateFiles(Application.persistentDataPath, "*.txt");
                int fileCount = 0;
                foreach (string file in enumeratedFiles)
                {
                    fileCount++;
                    if (fileCount <= 5) // 只显示前5个文件
                    {
                        Debug.Log($"枚举文件 {fileCount}: {Path.GetFileName(file)}");
                    }
                }
                Debug.Log($"总共枚举到 {fileCount} 个txt文件");
                
                // 枚举目录中的子目录
                var enumeratedDirs = Directory.EnumerateDirectories(Application.persistentDataPath);
                int dirCount = 0;
                foreach (string dir in enumeratedDirs)
                {
                    dirCount++;
                    Debug.Log($"枚举目录 {dirCount}: {Path.GetFileName(dir)}");
                }
                Debug.Log($"总共枚举到 {dirCount} 个子目录");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Directory操作出错: {ex.Message}");
            }
        }

        // ================= 流操作 =================
        /// <summary>
        /// Stream/FileStream/MemoryStream/StreamReader/StreamWriter/BinaryReader/BinaryWriter用法
        /// 流操作提供了更底层的文件访问控制
        /// </summary>
        private void StreamExample()
        {
            Debug.Log("--- Stream示例 ---");
            try
            {
                // ========== FileStream 文件流操作 ==========
                
                // FileStream写入
                // FileStream: 文件流，用于文件的读写操作
                // 参数：文件路径，文件模式，访问模式
                using (FileStream fileStream = new FileStream(testFilePath, FileMode.Create, FileAccess.Write))
                {
                    string content = "这是通过FileStream写入的内容";
                    byte[] bytes = Encoding.UTF8.GetBytes(content);
                    fileStream.Write(bytes, 0, bytes.Length);
                    Debug.Log("通过FileStream写入文件完成");
                }
                
                // FileStream读取
                using (FileStream fileStream = new FileStream(testFilePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    string content = Encoding.UTF8.GetString(buffer);
                    Debug.Log($"通过FileStream读取的内容: {content}");
                }
                
                // ========== MemoryStream 内存流操作 ==========
                
                // MemoryStream演示
                // MemoryStream: 内存流，数据存储在内存中，速度快
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    string content = "这是MemoryStream中的内容";
                    byte[] bytes = Encoding.UTF8.GetBytes(content);
                    memoryStream.Write(bytes, 0, bytes.Length);
                    memoryStream.Position = 0; // 读写指针重置到开始位置
                    byte[] readBuffer = new byte[memoryStream.Length];
                    memoryStream.Read(readBuffer, 0, readBuffer.Length);
                    string readContent = Encoding.UTF8.GetString(readBuffer);
                    Debug.Log($"MemoryStream内容: {readContent}");
                }
                
                // ========== StreamReader/StreamWriter 文本流操作 ==========
                
                // StreamWriter写入
                // StreamWriter: 专门用于文本写入的流
                // 参数：文件路径，是否追加（false=覆盖），编码格式
                using (StreamWriter writer = new StreamWriter(testFilePath, false, Encoding.UTF8))
                {
                    writer.WriteLine("第一行");
                    writer.WriteLine("第二行");
                    writer.WriteLine("第三行");
                    Debug.Log("通过StreamWriter写入文件完成");
                }
                
                // StreamReader读取
                // StreamReader: 专门用于文本读取的流
                using (StreamReader reader = new StreamReader(testFilePath, Encoding.UTF8))
                {
                    string line;
                    int lineNumber = 1;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Debug.Log($"第{lineNumber}行: {line}");
                        lineNumber++;
                    }
                }
                
                // ========== BinaryReader/BinaryWriter 二进制流操作 ==========
                
                // BinaryWriter写入二进制
                // BinaryWriter: 专门用于二进制数据写入
                using (BinaryWriter writer = new BinaryWriter(new FileStream(testFilePath, FileMode.Create)))
                {
                    writer.Write(42); // int类型
                    writer.Write(3.14); // double类型
                    writer.Write("Hello Binary"); // string类型
                    writer.Write(true); // bool类型
                    Debug.Log("通过BinaryWriter写入二进制数据完成");
                }
                
                // BinaryReader读取二进制
                // BinaryReader: 专门用于二进制数据读取
                // 注意：读取顺序必须与写入顺序一致
                using (BinaryReader reader = new BinaryReader(new FileStream(testFilePath, FileMode.Open)))
                {
                    int intValue = reader.ReadInt32();
                    double doubleValue = reader.ReadDouble();
                    string stringValue = reader.ReadString();
                    bool boolValue = reader.ReadBoolean();
                    Debug.Log($"读取的整数值: {intValue}");
                    Debug.Log($"读取的双精度值: {doubleValue}");
                    Debug.Log($"读取的字符串: {stringValue}");
                    Debug.Log($"读取的布尔值: {boolValue}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Stream操作出错: {ex.Message}");
            }
        }

        // ================= 异步IO =================
        /// <summary>
        /// 异步IO操作演示（async/await）
        /// 异步IO可以避免阻塞主线程，提高应用程序响应性
        /// </summary>
        private async Task AsyncIOExample()
        {
            Debug.Log("--- 异步IO示例 ---");
            try
            {
                // ========== 异步文件操作 ==========
                
                // 异步写入
                // WriteAllTextAsync: 异步写入文件内容
                string asyncContent = "这是异步写入的内容\n第二行\n第三行";
                await File.WriteAllTextAsync(testFilePath, asyncContent, Encoding.UTF8);
                Debug.Log("异步写入文件完成");
                
                // 异步读取
                // ReadAllTextAsync: 异步读取文件内容
                string asyncReadContent = await File.ReadAllTextAsync(testFilePath, Encoding.UTF8);
                Debug.Log($"异步读取的内容:\n{asyncReadContent}");
                
                // 异步按行读取
                // ReadAllLinesAsync: 异步读取文件的所有行
                string[] asyncLines = await File.ReadAllLinesAsync(testFilePath, Encoding.UTF8);
                Debug.Log($"异步读取的行数: {asyncLines.Length}");
                
                // ========== 异步流操作 ==========
                
                // 异步流写入
                // WriteLineAsync: 异步写入一行文本
                using (StreamWriter writer = new StreamWriter(testFilePath, false, Encoding.UTF8))
                {
                    await writer.WriteLineAsync("异步写入第一行");
                    await writer.WriteLineAsync("异步写入第二行");
                    await writer.WriteLineAsync("异步写入第三行");
                    Debug.Log("异步StreamWriter写入完成");
                }
                
                // 异步流读取
                // ReadLineAsync: 异步读取一行文本
                using (StreamReader reader = new StreamReader(testFilePath, Encoding.UTF8))
                {
                    string line;
                    int lineNumber = 1;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        Debug.Log($"异步读取第{lineNumber}行: {line}");
                        lineNumber++;
                    }
                }
                
                // ========== 异步FileStream操作 ==========
                
                // 异步FileStream写入
                // 参数：文件路径，文件模式，访问模式，共享模式，缓冲区大小，是否异步
                using (FileStream fileStream = new FileStream(testFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                {
                    string content = "异步FileStream写入的内容";
                    byte[] bytes = Encoding.UTF8.GetBytes(content);
                    await fileStream.WriteAsync(bytes, 0, bytes.Length);
                    Debug.Log("异步FileStream写入完成");
                }
                
                // 异步FileStream读取
                using (FileStream fileStream = new FileStream(testFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    await fileStream.ReadAsync(buffer, 0, buffer.Length);
                    string content = Encoding.UTF8.GetString(buffer);
                    Debug.Log($"异步FileStream读取的内容: {content}");
                }
                
                // ========== 异步文件操作高级功能 ==========
                
                // 异步文件复制（模拟）
                // 注意：.NET没有内置的异步文件复制方法，这里演示如何实现
                string asyncCopyPath = testFilePath.Replace(".txt", "_async_copy.txt");
                await CopyFileAsync(testFilePath, asyncCopyPath);
                Debug.Log($"异步文件复制完成: {asyncCopyPath}");
                
                // 异步文件比较
                bool areEqualAsync = await CompareFilesAsync(testFilePath, asyncCopyPath);
                Debug.Log($"异步文件比较结果: {areEqualAsync}");
                
                // 异步读取文件信息
                FileInfo fileInfo = new FileInfo(testFilePath);
                long fileSizeAsync = fileInfo.Length;
                DateTime lastWriteTimeAsync = fileInfo.LastWriteTime;
                Debug.Log($"异步获取文件大小: {fileSizeAsync} 字节");
                Debug.Log($"异步获取文件最后写入时间: {lastWriteTimeAsync}");
                
                // 异步批量文件操作
                string[] testFiles = { 
                    testFilePath.Replace(".txt", "_1.txt"),
                    testFilePath.Replace(".txt", "_2.txt"),
                    testFilePath.Replace(".txt", "_3.txt")
                };
                
                // 异步创建多个文件
                var createTasks = testFiles.Select(async filePath =>
                {
                    await File.WriteAllTextAsync(filePath, $"异步创建的文件: {Path.GetFileName(filePath)}", Encoding.UTF8);
                    return filePath;
                });
                
                string[] createdFiles = await Task.WhenAll(createTasks);
                Debug.Log($"异步创建了 {createdFiles.Length} 个文件");
                
                // 异步读取多个文件
                var readTasks = testFiles.Select(async filePath =>
                {
                    return await File.ReadAllTextAsync(filePath, Encoding.UTF8);
                });
                
                string[] fileContents = await Task.WhenAll(readTasks);
                Debug.Log($"异步读取了 {fileContents.Length} 个文件的内容");
                
                // 异步删除多个文件
                var deleteTasks = testFiles.Select(async filePath =>
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        return true;
                    }
                    return false;
                });
                
                bool[] deleteResults = await Task.WhenAll(deleteTasks);
                Debug.Log($"异步删除了 {deleteResults.Count(r => r)} 个文件");
                
                // 清理测试文件
                if (File.Exists(asyncCopyPath)) File.Delete(asyncCopyPath);
            }
            catch (Exception ex)
            {
                Debug.LogError($"异步IO操作出错: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 异步文件复制方法
        /// </summary>
        private async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            using (FileStream destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                await sourceStream.CopyToAsync(destinationStream);
            }
        }
        
        /// <summary>
        /// 异步文件比较方法
        /// </summary>
        private async Task<bool> CompareFilesAsync(string file1Path, string file2Path)
        {
            using (FileStream stream1 = new FileStream(file1Path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            using (FileStream stream2 = new FileStream(file2Path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                if (stream1.Length != stream2.Length)
                    return false;
                
                byte[] buffer1 = new byte[4096];
                byte[] buffer2 = new byte[4096];
                
                while (true)
                {
                    int bytesRead1 = await stream1.ReadAsync(buffer1, 0, buffer1.Length);
                    int bytesRead2 = await stream2.ReadAsync(buffer2, 0, buffer2.Length);
                    
                    if (bytesRead1 != bytesRead2)
                        return false;
                    
                    if (bytesRead1 == 0)
                        break;
                    
                    for (int i = 0; i < bytesRead1; i++)
                    {
                        if (buffer1[i] != buffer2[i])
                            return false;
                    }
                }
                
                return true;
            }
        }

        // ================= 路径操作 =================
        /// <summary>
        /// Path类常用API演示
        /// Path类是静态类，提供路径操作的便捷方法
        /// 跨平台兼容，自动处理不同操作系统的路径分隔符
        /// </summary>
        private void PathExample()
        {
            Debug.Log("--- Path类示例 ---");
            
            // ========== 路径解析操作 ==========
            
            // 路径操作相关API
            string fullPath = @"C:\\Users\\Username\\Documents\\test.txt";
            
            // GetFileName: 获取文件名（包含扩展名）
            string fileName = Path.GetFileName(fullPath);
            
            // GetFileNameWithoutExtension: 获取不带扩展名的文件名
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPath);
            
            // GetExtension: 获取文件扩展名（包含点号）
            string extension = Path.GetExtension(fullPath);
            
            // GetDirectoryName: 获取目录名
            string directoryName = Path.GetDirectoryName(fullPath);
            
            // GetPathRoot: 获取根目录
            string root = Path.GetPathRoot(fullPath);
            
            Debug.Log($"完整路径: {fullPath}");
            Debug.Log($"文件名: {fileName}");
            Debug.Log($"不带扩展名的文件名: {fileNameWithoutExtension}");
            Debug.Log($"文件扩展名: {extension}");
            Debug.Log($"目录名: {directoryName}");
            Debug.Log($"根目录: {root}");
            
            // ========== 路径组合操作 ==========
            
            // 路径组合
            // Combine: 安全地组合路径，自动处理路径分隔符
            string combinedPath = Path.Combine("C:", "Users", "Username", "Documents", "test.txt");
            Debug.Log($"组合路径: {combinedPath}");
            
            // ========== 路径验证操作 ==========
            
            // 路径验证
            // HasExtension: 检查路径是否有扩展名
            bool hasExtension = Path.HasExtension(fullPath);
            
            // IsPathRooted: 检查是否是绝对路径
            bool isPathRooted = Path.IsPathRooted(fullPath);
            
            Debug.Log($"是否有扩展名: {hasExtension}");
            Debug.Log($"是否是绝对路径: {isPathRooted}");
            
            // ========== 临时路径操作 ==========
            
            // 临时路径
            // GetTempPath: 获取系统临时目录路径
            string tempPath = Path.GetTempPath();
            
            // GetTempFileName: 获取临时文件名（会创建空文件）
            string tempFileName = Path.GetTempFileName();
            
            // GetRandomFileName: 获取随机文件名（不会创建文件）
            string randomFileName = Path.GetRandomFileName();
            
            Debug.Log($"临时目录: {tempPath}");
            Debug.Log($"临时文件: {tempFileName}");
            Debug.Log($"随机文件名: {randomFileName}");
            
            // ========== 路径规范化操作 ==========
            
            // 路径规范化
            // GetFullPath: 获取完整路径，解析相对路径和特殊字符
            string normalizedPath = Path.GetFullPath(@"C:\\Users\\..\\Users\\Username\\.\\Documents\\test.txt");
            Debug.Log($"规范化路径: {normalizedPath}");
            
            // ========== 路径验证操作 ==========
            
            // 获取无效路径字符
            // GetInvalidPathChars: 获取不允许在路径中使用的字符
            char[] invalidPathChars = Path.GetInvalidPathChars();
            Debug.Log($"无效路径字符数量: {invalidPathChars.Length}");
            Debug.Log($"无效路径字符: {string.Join(", ", invalidPathChars)}");
            
            // 获取无效文件名字符
            // GetInvalidFileNameChars: 获取不允许在文件名中使用的字符
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            Debug.Log($"无效文件名字符数量: {invalidFileNameChars.Length}");
            Debug.Log($"无效文件名字符: {string.Join(", ", invalidFileNameChars)}");
            
            // 检查路径是否完全限定
            // IsPathFullyQualified: 检查路径是否为完全限定路径（绝对路径）
            bool isFullyQualified = Path.IsPathFullyQualified(fullPath);
            bool isRelativeFullyQualified = Path.IsPathFullyQualified("relative/path");
            Debug.Log($"绝对路径是否完全限定: {isFullyQualified}");
            Debug.Log($"相对路径是否完全限定: {isRelativeFullyQualified}");
            
            // 检查路径是否为空或仅包含空白字符
            // IsPathRooted: 检查路径是否为根路径
            bool isEmptyOrWhiteSpace = string.IsNullOrWhiteSpace("");
            bool isRooted = Path.IsPathRooted(fullPath);
            Debug.Log($"空路径: {isEmptyOrWhiteSpace}");
            Debug.Log($"是否为根路径: {isRooted}");
            
            // ========== 路径分隔符操作 ==========
            
            // 获取路径分隔符
            // DirectorySeparatorChar: 目录分隔符（Windows: \, Unix: /）
            // AltDirectorySeparatorChar: 替代目录分隔符（Windows: /, Unix: /）
            // PathSeparator: 路径分隔符（Windows: ;, Unix: :）
            // VolumeSeparatorChar: 卷分隔符（Windows: :, Unix: :）
            Debug.Log($"目录分隔符: {Path.DirectorySeparatorChar}");
            Debug.Log($"替代目录分隔符: {Path.AltDirectorySeparatorChar}");
            Debug.Log($"路径分隔符: {Path.PathSeparator}");
            Debug.Log($"卷分隔符: {Path.VolumeSeparatorChar}");
            
            // ========== 路径转换操作 ==========
            
            // 更改路径分隔符
            // 统一使用正斜杠
            string windowsPath = @"C:\Users\Username\Documents\test.txt";
            string unixPath = windowsPath.Replace('\\', '/');
            Debug.Log($"Windows路径: {windowsPath}");
            Debug.Log($"Unix路径: {unixPath}");
            
            // 获取相对路径
            // 注意：GetRelativePath需要.NET Core 2.1+或.NET Framework 4.6.2+
            // 这里演示手动计算相对路径的方法
            string basePath = @"C:\Users\Username\Documents";
            string targetPath = @"C:\Users\Username\Documents\SubFolder\file.txt";
            if (targetPath.StartsWith(basePath))
            {
                string relativePath = targetPath.Substring(basePath.Length).TrimStart('\\', '/');
                Debug.Log($"相对路径: {relativePath}");
            }
            
            // ========== 路径组合高级操作 ==========
            
            // 组合多个路径段
            string[] pathSegments = { "C:", "Users", "Username", "Documents", "SubFolder", "file.txt" };
            string combinedPath2 = Path.Combine(pathSegments);
            Debug.Log($"组合路径2: {combinedPath2}");
            
            // 处理空路径段
            string pathWithEmpty = Path.Combine("C:", "", "Users", "Username", "Documents");
            Debug.Log($"包含空段的路径: {pathWithEmpty}");
            
            // ========== 路径环境变量操作 ==========
            
            // 获取特殊文件夹路径
            // 注意：在Unity中，某些特殊文件夹可能不可用
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string tempPath2 = Environment.GetFolderPath(Environment.SpecialFolder.Temp);
                
                Debug.Log($"桌面路径: {desktopPath}");
                Debug.Log($"文档路径: {documentsPath}");
                Debug.Log($"临时文件夹路径: {tempPath2}");
            }
            catch (Exception ex)
            {
                Debug.Log($"获取特殊文件夹路径失败: {ex.Message}");
            }
        }

        // ================= FileInfo操作 =================
        /// <summary>
        /// FileInfo类常用API演示
        /// FileInfo类提供文件的详细信息，比File类提供更多功能
        /// </summary>
        private void FileInfoExample()
        {
            Debug.Log("--- FileInfo示例 ---");
            try
            {
                // 创建测试文件
                File.WriteAllText(testFilePath, "FileInfo测试内容", Encoding.UTF8);
                
                // 获取FileInfo对象
                // FileInfo: 提供文件的详细信息，包括属性、方法等
                FileInfo fileInfo = new FileInfo(testFilePath);
                
                // ========== 基本属性 ==========
                
                // Name: 文件名（不包含路径）
                Debug.Log($"文件名: {fileInfo.Name}");
                
                // FullName: 完整路径
                Debug.Log($"完整路径: {fileInfo.FullName}");
                
                // DirectoryName: 目录名
                Debug.Log($"目录名: {fileInfo.DirectoryName}");
                
                // Length: 文件大小（字节）
                Debug.Log($"文件大小: {fileInfo.Length} 字节");
                
                // Extension: 文件扩展名
                Debug.Log($"文件扩展名: {fileInfo.Extension}");
                
                // ========== 时间属性 ==========
                
                // CreationTime: 创建时间
                Debug.Log($"创建时间: {fileInfo.CreationTime}");
                
                // LastAccessTime: 最后访问时间
                Debug.Log($"最后访问时间: {fileInfo.LastAccessTime}");
                
                // LastWriteTime: 最后写入时间
                Debug.Log($"最后写入时间: {fileInfo.LastWriteTime}");
                
                // LastWriteTimeUtc: 最后写入时间（UTC）
                Debug.Log($"最后写入时间(UTC): {fileInfo.LastWriteTimeUtc}");
                
                // ========== 文件属性 ==========
                
                // IsReadOnly: 是否只读
                Debug.Log($"是否只读: {fileInfo.IsReadOnly}");
                
                // Exists: 文件是否存在
                Debug.Log($"是否存在: {fileInfo.Exists}");
                
                // ========== 文件操作方法 ==========
                
                // 复制文件
                // CopyTo: 复制文件到新位置
                // 参数：目标路径，是否覆盖
                string copyPath = testFilePath.Replace(".txt", "_fileinfo_copy.txt");
                FileInfo copyInfo = fileInfo.CopyTo(copyPath, true);
                Debug.Log($"复制的文件: {copyInfo.FullName}");
                
                // 移动文件
                // MoveTo: 移动文件到新位置
                string movePath = testFilePath.Replace(".txt", "_fileinfo_moved.txt");
                fileInfo.MoveTo(movePath);
                Debug.Log($"移动后的文件: {fileInfo.FullName}");
                
                // 删除文件
                // Delete: 删除文件
                fileInfo.Delete();
                copyInfo.Delete();
                Debug.Log("FileInfo测试文件已删除");
            }
            catch (Exception ex)
            {
                Debug.LogError($"FileInfo操作出错: {ex.Message}");
            }
        }

        // ================= DirectoryInfo操作 =================
        /// <summary>
        /// DirectoryInfo类常用API演示
        /// DirectoryInfo类提供目录的详细信息，比Directory类提供更多功能
        /// </summary>
        private void DirectoryInfoExample()
        {
            Debug.Log("--- DirectoryInfo示例 ---");
            try
            {
                // 创建测试目录
                Directory.CreateDirectory(testDirectoryPath);
                
                // 获取DirectoryInfo对象
                // DirectoryInfo: 提供目录的详细信息，包括属性、方法等
                DirectoryInfo dirInfo = new DirectoryInfo(testDirectoryPath);
                
                // ========== 基本属性 ==========
                
                // Name: 目录名（不包含路径）
                Debug.Log($"目录名: {dirInfo.Name}");
                
                // FullName: 完整路径
                Debug.Log($"完整路径: {dirInfo.FullName}");
                
                // Parent: 父目录
                Debug.Log($"父目录: {dirInfo.Parent}");
                
                // Root: 根目录
                Debug.Log($"根目录: {dirInfo.Root}");
                
                // ========== 时间属性 ==========
                
                // CreationTime: 创建时间
                Debug.Log($"创建时间: {dirInfo.CreationTime}");
                
                // LastAccessTime: 最后访问时间
                Debug.Log($"最后访问时间: {dirInfo.LastAccessTime}");
                
                // LastWriteTime: 最后写入时间
                Debug.Log($"最后写入时间: {dirInfo.LastWriteTime}");
                
                // ========== 目录属性 ==========
                
                // Exists: 目录是否存在
                Debug.Log($"是否存在: {dirInfo.Exists}");
                
                // IsReadOnly: 是否只读
                Debug.Log($"是否只读: {dirInfo.IsReadOnly}");
                
                // ========== 目录操作方法 ==========
                
                // 创建子目录
                // CreateSubdirectory: 创建子目录
                string subDirPath = Path.Combine(testDirectoryPath, "subdir");
                DirectoryInfo subDirInfo = dirInfo.CreateSubdirectory(subDirPath);
                Debug.Log($"子目录已创建: {subDirInfo.FullName}");
                
                // 获取子目录
                // GetDirectories: 获取指定目录中的子目录
                string[] subDirs = dirInfo.GetDirectories();
                Debug.Log($"子目录数量: {subDirs.Length}");
                foreach (DirectoryInfo dir in subDirs)
                    Debug.Log($"子目录: {dir.Name}");
                
                // 获取所有文件（包括子目录）
                // SearchOption.AllDirectories: 搜索所有子目录
                // SearchOption.TopDirectoryOnly: 只搜索当前目录（默认）
                string[] allFiles = Directory.GetFiles(testDirectoryPath, "*", SearchOption.AllDirectories);
                Debug.Log($"所有文件数量（包括子目录）: {allFiles.Length}");
                
                // 移动目录
                // MoveTo: 移动目录到新位置
                string moveDirPath = testDirectoryPath.Replace("test_directory", "test_directory_moved");
                dirInfo.MoveTo(moveDirPath);
                Debug.Log($"目录已移动到: {dirInfo.FullName}");
                
                // 删除目录（包括内容）
                // Delete: 删除目录
                // 参数：目录路径，是否递归删除（删除所有子目录和文件）
                dirInfo.Delete(true);
                Debug.Log("目录已删除");
            }
            catch (Exception ex)
            {
                Debug.LogError($"DirectoryInfo操作出错: {ex.Message}");
            }
        }

        // ================= DriveInfo操作 =================
        /// <summary>
        /// DriveInfo类常用API演示
        /// DriveInfo类提供驱动器的详细信息，比DirectoryInfo类提供更多功能
        /// </summary>
        private void DriveInfoExample()
        {
            Debug.Log("--- DriveInfo示例 ---");
            try
            {
                // 获取DriveInfo对象
                // DriveInfo: 提供驱动器的详细信息，包括属性、方法等
                DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(testFilePath));
                
                // ========== 基本属性 ==========
                
                // Name: 驱动器名
                Debug.Log($"驱动器名: {driveInfo.Name}");
                
                // VolumeLabel: 卷标
                Debug.Log($"卷标: {driveInfo.VolumeLabel}");
                
                // DriveFormat: 文件系统格式
                Debug.Log($"文件系统格式: {driveInfo.DriveFormat}");
                
                // AvailableFreeSpace: 可用空间（字节）
                Debug.Log($"可用空间: {driveInfo.AvailableFreeSpace} 字节");
                
                // TotalFreeSpace: 总空间（字节）
                Debug.Log($"总空间: {driveInfo.TotalFreeSpace} 字节");
                
                // TotalSize: 总大小（字节）
                Debug.Log($"总大小: {driveInfo.TotalSize} 字节");
                
                // ========== 驱动器属性 ==========
                
                // IsReady: 驱动器是否可用
                Debug.Log($"是否可用: {driveInfo.IsReady}");
                
                // DriveType: 驱动器类型
                Debug.Log($"驱动器类型: {driveInfo.DriveType}");
                
                // ========== 驱动器操作方法 ==========
                
                // 获取驱动器中的文件
                // GetFiles: 获取指定驱动器中的文件
                // 参数：驱动器路径，搜索模式（可选），搜索选项（可选）
                string[] files = Directory.GetFiles(driveInfo.Name);
                Debug.Log($"驱动器中的文件数量: {files.Length}");
                foreach (string file in files)
                    Debug.Log($"文件: {Path.GetFileName(file)}");
                
                // 获取驱动器中的子目录
                // GetDirectories: 获取指定驱动器中的子目录
                string[] subDirs = Directory.GetDirectories(driveInfo.Name);
                Debug.Log($"子目录数量: {subDirs.Length}");
                foreach (string dir in subDirs)
                    Debug.Log($"子目录: {Path.GetFileName(dir)}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"DriveInfo操作出错: {ex.Message}");
            }
        }

        // ================= FileSystemWatcher操作 =================
        /// <summary>
        /// FileSystemWatcher类常用API演示
        /// FileSystemWatcher类用于监控文件系统变化
        /// </summary>
        private void FileSystemWatcherExample()
        {
            Debug.Log("--- FileSystemWatcher示例 ---");
            try
            {
                // 创建测试目录
                Directory.CreateDirectory(testDirectoryPath);
                
                // 创建FileSystemWatcher对象
                // FileSystemWatcher: 用于监控文件系统变化
                FileSystemWatcher watcher = new FileSystemWatcher(testDirectoryPath);
                
                // 配置FileSystemWatcher
                watcher.Filter = "*.*"; // 监控所有文件
                watcher.IncludeSubdirectories = true; // 监控子目录
                watcher.EnableRaisingEvents = true; // 启用事件
                
                // 添加事件处理程序
                watcher.Created += OnCreated;
                watcher.Deleted += OnDeleted;
                watcher.Changed += OnChanged;
                watcher.Renamed += OnRenamed;
                
                Debug.Log("文件系统监控已启动");
                
                // 创建测试文件来触发事件
                string testFile = Path.Combine(testDirectoryPath, "watcher_test.txt");
                File.WriteAllText(testFile, "测试文件监控");
                
                // 等待一下让事件触发
                System.Threading.Thread.Sleep(100);
                
                // 停止监控
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                
                // 清理测试文件
                if (File.Exists(testFile)) File.Delete(testFile);
            }
            catch (Exception ex)
            {
                Debug.LogError($"FileSystemWatcher操作出错: {ex.Message}");
            }
        }

        // 事件处理程序
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Debug.Log($"创建了文件: {e.FullPath}");
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Debug.Log($"删除了文件: {e.FullPath}");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Debug.Log($"修改了文件: {e.FullPath}");
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            Debug.Log($"重命名了文件: {e.OldFullPath} -> {e.FullPath}");
        }

        // ================= 压缩文件操作 =================
        /// <summary>
        /// 压缩文件操作演示
        /// 使用System.IO.Compression命名空间下的类
        /// </summary>
        private void CompressionExample()
        {
            Debug.Log("--- 压缩文件操作示例 ---");
            try
            {
                // 创建测试文件
                string testContent = "这是要压缩的测试内容\n包含多行文本\n用于测试压缩功能";
                File.WriteAllText(testFilePath, testContent, Encoding.UTF8);
                
                // ========== GZip压缩 ==========
                
                // GZip压缩文件
                // GZipStream: 用于GZip压缩和解压缩
                string gzipPath = testFilePath.Replace(".txt", ".gz");
                using (FileStream originalFileStream = File.OpenRead(testFilePath))
                using (FileStream compressedFileStream = File.Create(gzipPath))
                using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                {
                    originalFileStream.CopyTo(compressionStream);
                }
                Debug.Log($"文件已GZip压缩到: {gzipPath}");
                
                // GZip解压缩文件
                string gzipDecompressedPath = testFilePath.Replace(".txt", "_gzip_decompressed.txt");
                using (FileStream compressedFileStream = File.OpenRead(gzipPath))
                using (FileStream decompressedFileStream = File.Create(gzipDecompressedPath))
                using (GZipStream decompressionStream = new GZipStream(compressedFileStream, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(decompressedFileStream);
                }
                Debug.Log($"文件已GZip解压缩到: {gzipDecompressedPath}");
                
                // ========== Deflate压缩 ==========
                
                // Deflate压缩文件
                // DeflateStream: 用于Deflate压缩和解压缩
                string deflatePath = testFilePath.Replace(".txt", ".deflate");
                using (FileStream originalFileStream = File.OpenRead(testFilePath))
                using (FileStream compressedFileStream = File.Create(deflatePath))
                using (DeflateStream compressionStream = new DeflateStream(compressedFileStream, CompressionMode.Compress))
                {
                    originalFileStream.CopyTo(compressionStream);
                }
                Debug.Log($"文件已Deflate压缩到: {deflatePath}");
                
                // Deflate解压缩文件
                string deflateDecompressedPath = testFilePath.Replace(".txt", "_deflate_decompressed.txt");
                using (FileStream compressedFileStream = File.OpenRead(deflatePath))
                using (FileStream decompressedFileStream = File.Create(deflateDecompressedPath))
                using (DeflateStream decompressionStream = new DeflateStream(compressedFileStream, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(decompressedFileStream);
                }
                Debug.Log($"文件已Deflate解压缩到: {deflateDecompressedPath}");
                
                // ========== ZipArchive压缩 ==========
                
                // 创建ZIP文件
                // ZipArchive: 用于创建和管理ZIP文件
                string zipPath = testFilePath.Replace(".txt", ".zip");
                using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    // 添加文件到ZIP
                    archive.CreateEntryFromFile(testFilePath, "test_file.txt");
                    Debug.Log($"文件已添加到ZIP: {zipPath}");
                }
                
                // 从ZIP提取文件
                string zipExtractedPath = testFilePath.Replace(".txt", "_zip_extracted.txt");
                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    ZipArchiveEntry entry = archive.GetEntry("test_file.txt");
                    if (entry != null)
                    {
                        entry.ExtractToFile(zipExtractedPath, true);
                        Debug.Log($"文件已从ZIP提取到: {zipExtractedPath}");
                    }
                }
                
                // 清理测试文件
                if (File.Exists(gzipPath)) File.Delete(gzipPath);
                if (File.Exists(gzipDecompressedPath)) File.Delete(gzipDecompressedPath);
                if (File.Exists(deflatePath)) File.Delete(deflatePath);
                if (File.Exists(deflateDecompressedPath)) File.Delete(deflateDecompressedPath);
                if (File.Exists(zipPath)) File.Delete(zipPath);
                if (File.Exists(zipExtractedPath)) File.Delete(zipExtractedPath);
            }
            catch (Exception ex)
            {
                Debug.LogError($"压缩文件操作出错: {ex.Message}");
            }
        }

        // ================= 高级流操作 =================
        /// <summary>
        /// 高级流操作演示
        /// 包括BufferedStream、NetworkStream、CryptoStream等
        /// </summary>
        private void AdvancedStreamExample()
        {
            Debug.Log("--- 高级流操作示例 ---");
            try
            {
                // ========== BufferedStream 缓冲流操作 ==========
                
                // BufferedStream: 为其他流提供缓冲功能，提高性能
                string bufferedFilePath = testFilePath.Replace(".txt", "_buffered.txt");
                using (FileStream fileStream = new FileStream(bufferedFilePath, FileMode.Create))
                using (BufferedStream bufferedStream = new BufferedStream(fileStream, 4096)) // 4KB缓冲区
                {
                    string content = "这是通过BufferedStream写入的内容";
                    byte[] bytes = Encoding.UTF8.GetBytes(content);
                    bufferedStream.Write(bytes, 0, bytes.Length);
                    bufferedStream.Flush(); // 确保数据写入磁盘
                    Debug.Log("通过BufferedStream写入数据完成");
                }
                
                // 读取缓冲流
                using (FileStream fileStream = new FileStream(bufferedFilePath, FileMode.Open))
                using (BufferedStream bufferedStream = new BufferedStream(fileStream, 4096))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    bufferedStream.Read(buffer, 0, buffer.Length);
                    string readContent = Encoding.UTF8.GetString(buffer);
                    Debug.Log($"通过BufferedStream读取的内容: {readContent}");
                }
                
                // ========== MemoryStream 高级操作 ==========
                
                // MemoryStream的ToArray方法
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    string content = "MemoryStream高级操作测试";
                    byte[] writeBytes = Encoding.UTF8.GetBytes(content);
                    memoryStream.Write(writeBytes, 0, writeBytes.Length);
                    
                    // ToArray: 将内存流内容复制到新数组
                    byte[] array = memoryStream.ToArray();
                    string arrayContent = Encoding.UTF8.GetString(array);
                    Debug.Log($"MemoryStream.ToArray内容: {arrayContent}");
                    
                    // GetBuffer: 获取底层缓冲区（可能包含未使用的空间）
                    byte[] buffer = memoryStream.GetBuffer();
                    Debug.Log($"MemoryStream缓冲区大小: {buffer.Length}");
                    Debug.Log($"MemoryStream实际长度: {memoryStream.Length}");
                }
                
                // ========== Stream的Seek操作 ==========
                
                // Seek: 设置流的位置
                using (MemoryStream seekStream = new MemoryStream())
                {
                    string content = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    byte[] bytes = Encoding.UTF8.GetBytes(content);
                    seekStream.Write(bytes, 0, bytes.Length);
                    
                    // 设置位置到开头
                    seekStream.Seek(0, SeekOrigin.Begin);
                    byte[] firstChar = new byte[1];
                    seekStream.Read(firstChar, 0, 1);
                    Debug.Log($"第一个字符: {Encoding.UTF8.GetString(firstChar)}");
                    
                    // 设置位置到中间
                    seekStream.Seek(10, SeekOrigin.Begin);
                    byte[] middleChar = new byte[1];
                    seekStream.Read(middleChar, 0, 1);
                    Debug.Log($"第11个字符: {Encoding.UTF8.GetString(middleChar)}");
                    
                    // 从当前位置向后移动
                    seekStream.Seek(5, SeekOrigin.Current);
                    byte[] currentChar = new byte[1];
                    seekStream.Read(currentChar, 0, 1);
                    Debug.Log($"当前位置字符: {Encoding.UTF8.GetString(currentChar)}");
                }
                
                // ========== Stream的CopyTo操作 ==========
                
                // CopyTo: 将当前流的内容复制到目标流
                using (MemoryStream sourceStream = new MemoryStream())
                using (MemoryStream targetStream = new MemoryStream())
                {
                    string sourceContent = "源流内容";
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(sourceContent);
                    sourceStream.Write(sourceBytes, 0, sourceBytes.Length);
                    sourceStream.Position = 0; // 重置位置
                    
                    // 复制到目标流
                    sourceStream.CopyTo(targetStream);
                    targetStream.Position = 0; // 重置位置
                    
                    byte[] targetBytes = new byte[targetStream.Length];
                    targetStream.Read(targetBytes, 0, targetBytes.Length);
                    string targetContent = Encoding.UTF8.GetString(targetBytes);
                    Debug.Log($"复制后的内容: {targetContent}");
                }
                
                // ========== Stream的Flush操作 ==========
                
                // Flush: 清除缓冲区，确保数据写入
                using (FileStream flushStream = new FileStream(testFilePath, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(flushStream))
                {
                    writer.Write("第一行内容");
                    writer.Flush(); // 确保数据立即写入
                    writer.Write("第二行内容");
                    writer.Flush(); // 再次确保数据写入
                    Debug.Log("StreamWriter.Flush操作完成");
                }
                
                // 清理测试文件
                if (File.Exists(bufferedFilePath)) File.Delete(bufferedFilePath);
            }
            catch (Exception ex)
            {
                Debug.LogError($"高级流操作出错: {ex.Message}");
            }
        }
    }
} 