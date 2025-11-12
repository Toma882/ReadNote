# .NET Networking API 参考文档

本文档基于 `NetworkingExample.cs` 文件，详细介绍了 .NET Networking 相关的所有常用 API。

## 目录
- [HttpClient HTTP客户端](#httpclient-http客户端)
- [异步HTTP操作](#异步http操作)
- [WebClient 网络客户端](#webclient-网络客户端)
- [Socket 套接字](#socket-套接字)
- [WebSocket 网络套接字](#websocket-网络套接字)
- [网络工具类](#网络工具类)
- [高级网络操作](#高级网络操作)

---

## HttpClient HTTP客户端

### 创建和配置HttpClient

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new HttpClient()` | 创建HttpClient实例 |
| `HttpClient.Timeout` | 设置超时时间 |
| `HttpClient.DefaultRequestHeaders.Add(key, value)` | 添加默认请求头 |
| `HttpClient.DefaultRequestHeaders` | 获取默认请求头集合 |

### GET请求操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HttpClient.GetStringAsync(url)` | 异步获取字符串响应 |
| `HttpClient.GetAsync(url)` | 异步获取HTTP响应 |
| `HttpClient.GetByteArrayAsync(url)` | 异步获取字节数组响应 |
| `HttpClient.GetStreamAsync(url)` | 异步获取流响应 |

### POST请求操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HttpClient.PostAsync(url, content)` | 异步发送POST请求 |
| `new StringContent(content, encoding, mediaType)` | 创建字符串内容 |
| `new FormUrlEncodedContent(data)` | 创建表单编码内容 |
| `new MultipartFormDataContent()` | 创建多部分表单数据内容 |
| `MultipartFormDataContent.Add(content, name, filename)` | 添加文件到多部分内容 |

### 响应处理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HttpResponseMessage.StatusCode` | 获取响应状态码 |
| `HttpResponseMessage.IsSuccessStatusCode` | 检查响应是否成功 |
| `HttpResponseMessage.Headers` | 获取响应头 |
| `HttpResponseMessage.Content.Headers` | 获取内容头 |
| `HttpResponseMessage.Content.ReadAsStringAsync()` | 异步读取响应内容为字符串 |
| `HttpResponseMessage.Content.ReadAsByteArrayAsync()` | 异步读取响应内容为字节数组 |
| `HttpResponseMessage.Content.ReadAsStreamAsync()` | 异步读取响应内容为流 |

### 错误处理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HttpRequestException` | HTTP请求异常 |
| `HttpResponseMessage.ReasonPhrase` | 获取响应原因短语 |

---

## 异步HTTP操作

### 基本异步操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `await HttpClient.GetStringAsync(url)` | 异步等待获取字符串 |
| `await HttpClient.PostAsync(url, content)` | 异步等待POST请求 |
| `await Task.Delay(milliseconds)` | 异步延迟 |

### 并发请求

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Task.WhenAll(tasks)` | 等待所有任务完成 |
| `Task.WhenAny(tasks)` | 等待任意一个任务完成 |

### 超时处理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HttpClient.Timeout = TimeSpan.FromSeconds(seconds)` | 设置客户端超时 |
| `TaskCanceledException` | 任务取消异常（超时） |

### 取消令牌

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `CancellationTokenSource(TimeSpan)` | 创建超时取消令牌源 |
| `HttpClient.GetStringAsync(url, cancellationToken)` | 带取消令牌的请求 |

### 流式下载

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `HttpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead)` | 获取响应头后立即返回 |
| `HttpResponseMessage.Content.ReadAsStreamAsync()` | 异步读取响应流 |

---

## WebClient 网络客户端

### 基本配置

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new WebClient()` | 创建WebClient实例 |
| `WebClient.Headers.Add(key, value)` | 添加请求头 |
| `WebClient.Timeout` | 设置超时时间 |

### 下载操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `WebClient.DownloadString(url)` | 下载字符串 |
| `WebClient.DownloadData(url)` | 下载字节数组 |
| `WebClient.DownloadFile(url, path)` | 下载文件 |
| `WebClient.DownloadStringTaskAsync(url)` | 异步下载字符串 |
| `WebClient.DownloadDataTaskAsync(url)` | 异步下载字节数组 |
| `WebClient.DownloadFileTaskAsync(url, path)` | 异步下载文件 |

### 上传操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `WebClient.UploadString(url, data)` | 上传字符串 |
| `WebClient.UploadData(url, data)` | 上传字节数组 |
| `WebClient.UploadFile(url, path)` | 上传文件 |
| `WebClient.UploadStringTaskAsync(url, data)` | 异步上传字符串 |
| `WebClient.UploadDataTaskAsync(url, data)` | 异步上传字节数组 |
| `WebClient.UploadFileTaskAsync(url, path)` | 异步上传文件 |

### 事件处理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `WebClient.DownloadProgressChanged` | 下载进度变化事件 |
| `WebClient.DownloadDataCompleted` | 下载数据完成事件 |
| `WebClient.UploadProgressChanged` | 上传进度变化事件 |
| `WebClient.UploadDataCompleted` | 上传数据完成事件 |

---

## Socket 套接字

### TCP Socket

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new TcpClient()` | 创建TCP客户端 |
| `TcpClient.Connect(host, port)` | 连接到服务器 |
| `TcpClient.GetStream()` | 获取网络流 |
| `NetworkStream.Write(buffer, offset, count)` | 写入数据到流 |
| `NetworkStream.Read(buffer, offset, count)` | 从流读取数据 |

### UDP Socket

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new UdpClient()` | 创建UDP客户端 |
| `UdpClient.Connect(host, port)` | 连接到服务器 |
| `UdpClient.Send(data, length)` | 发送数据 |
| `UdpClient.Receive(ref endPoint)` | 接收数据 |

### Socket选项

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new Socket(addressFamily, socketType, protocolType)` | 创建Socket |
| `Socket.SetSocketOption(level, name, value)` | 设置Socket选项 |
| `Socket.SendTimeout` | 设置发送超时 |
| `Socket.ReceiveTimeout` | 设置接收超时 |

---

## WebSocket 网络套接字

### 创建和连接

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new ClientWebSocket()` | 创建WebSocket客户端 |
| `ClientWebSocket.ConnectAsync(uri, cancellationToken)` | 异步连接到服务器 |
| `ClientWebSocket.Options.KeepAliveInterval` | 设置保活间隔 |
| `ClientWebSocket.Options.SetRequestHeader(key, value)` | 设置请求头 |

### 发送和接收

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ClientWebSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken)` | 异步发送消息 |
| `ClientWebSocket.ReceiveAsync(buffer, cancellationToken)` | 异步接收消息 |
| `WebSocketMessageType.Text` | 文本消息类型 |
| `WebSocketMessageType.Binary` | 二进制消息类型 |

### 连接管理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ClientWebSocket.State` | 获取WebSocket状态 |
| `ClientWebSocket.CloseAsync(status, description, cancellationToken)` | 异步关闭连接 |
| `WebSocketCloseStatus.NormalClosure` | 正常关闭状态 |

---

## 网络工具类

### Uri

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new Uri(uriString)` | 创建Uri |
| `Uri.Scheme` | 获取方案 |
| `Uri.Host` | 获取主机 |
| `Uri.Port` | 获取端口 |
| `Uri.AbsolutePath` | 获取绝对路径 |
| `Uri.Query` | 获取查询字符串 |
| `Uri.Fragment` | 获取片段 |
| `new UriBuilder()` | 创建Uri构建器 |
| `UriBuilder.Scheme` | 设置方案 |
| `UriBuilder.Host` | 设置主机 |
| `UriBuilder.Path` | 设置路径 |
| `UriBuilder.Query` | 设置查询字符串 |
| `Uri.Equals(uri1, uri2)` | 比较Uri |

### IPAddress

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IPAddress.Parse(address)` | 解析IP地址 |
| `IPAddress.TryParse(address, out result)` | 尝试解析IP地址 |
| `IPAddress.AddressFamily` | 获取地址族 |
| `IPAddress.Loopback` | 获取回环地址 |
| `IPAddress.Any` | 获取任意地址 |
| `IPAddress.Broadcast` | 获取广播地址 |
| `IPAddress.GetAddressBytes()` | 获取地址字节数组 |
| `new IPAddress(bytes)` | 从字节数组创建IP地址 |

### DNS

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `Dns.GetHostEntry(hostName)` | 获取主机条目 |
| `Dns.GetHostEntryAsync(hostName)` | 异步获取主机条目 |
| `Dns.GetHostName()` | 获取本地主机名 |
| `IPHostEntry.HostName` | 获取主机名 |
| `IPHostEntry.AddressList` | 获取IP地址列表 |
| `IPHostEntry.Aliases` | 获取别名列表 |

### 网络信息

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `NetworkInterface.GetAllNetworkInterfaces()` | 获取所有网络接口 |
| `NetworkInterface.Name` | 获取接口名称 |
| `NetworkInterface.Description` | 获取接口描述 |
| `NetworkInterface.NetworkInterfaceType` | 获取接口类型 |
| `NetworkInterface.OperationalStatus` | 获取操作状态 |
| `NetworkInterface.Speed` | 获取接口速度 |
| `NetworkInterface.GetPhysicalAddress()` | 获取MAC地址 |
| `NetworkInterface.GetIPProperties()` | 获取IP属性 |
| `IPInterfaceProperties.UnicastAddresses` | 获取单播地址 |
| `IPInterfaceProperties.GatewayAddresses` | 获取网关地址 |
| `IPInterfaceProperties.DnsAddresses` | 获取DNS地址 |

### Ping

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new Ping()` | 创建Ping实例 |
| `Ping.Send(address, timeout)` | 发送Ping请求 |
| `PingReply.Status` | 获取Ping状态 |
| `PingReply.RoundtripTime` | 获取往返时间 |
| `IPStatus.Success` | 成功状态 |

---

## 高级网络操作

### 代理设置

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `new WebProxy(address, port)` | 创建Web代理 |
| `HttpClientHandler.Proxy` | 设置HttpClient代理 |
| `WebClient.Proxy` | 设置WebClient代理 |

### SSL/TLS设置

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ServicePointManager.SecurityProtocol` | 设置安全协议 |
| `ServicePointManager.ServerCertificateValidationCallback` | 设置证书验证回调 |

### 网络监控

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `NetworkChange.NetworkAvailabilityChanged` | 网络可用性变化事件 |
| `NetworkChange.NetworkAddressChanged` | 网络地址变化事件 |

### 网络配置

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ServicePointManager.DefaultConnectionLimit` | 设置默认连接限制 |
| `ServicePointManager.MaxServicePointIdleTime` | 设置最大服务点空闲时间 |

### 网络统计

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IPGlobalProperties.GetIPGlobalProperties()` | 获取IP全局属性 |
| `IPGlobalProperties.GetTcpIPv4Statistics()` | 获取TCP IPv4统计 |
| `IPGlobalProperties.GetUdpIPv4Statistics()` | 获取UDP IPv4统计 |
| `TcpStatistics.CurrentConnections` | 当前TCP连接数 |
| `TcpStatistics.CurrentListening` | 当前TCP监听数 |
| `UdpStatistics.CurrentListening` | 当前UDP监听数 |

---

## 使用注意事项

1. **HttpClient重用**：HttpClient应该重用而不是每次创建新实例
2. **异步操作**：优先使用异步方法避免阻塞主线程
3. **资源管理**：及时释放网络资源，使用using语句
4. **错误处理**：正确处理网络异常和HTTP错误状态码
5. **超时设置**：为网络操作设置合理的超时时间
6. **取消令牌**：为长时间运行的操作提供取消机制
7. **SSL/TLS**：在生产环境中使用HTTPS和适当的SSL/TLS配置
8. **网络监控**：监控网络状态变化，提供用户友好的错误信息
9. **性能优化**：使用连接池、压缩等技术优化网络性能
10. **安全性**：验证SSL证书，防止中间人攻击

---

## 示例代码

完整的示例代码请参考 `NetworkingExample.cs` 文件，其中包含了所有API的详细使用示例和中文注释。 