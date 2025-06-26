# .NET 序列化 API 参考文档

本文档基于 `SerializationExample.cs` 文件，详细介绍了 .NET 序列化相关的所有常用 API。

## 目录
- [JSON序列化](#json序列化)
- [XML序列化](#xml序列化)
- [二进制序列化](#二进制序列化)
- [数据契约序列化](#数据契约序列化)
- [自定义序列化](#自定义序列化)
- [序列化属性](#序列化属性)
- [文件操作](#文件操作)

---

## JSON序列化

### JsonUtility 操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `JsonUtility.ToJson` | 将对象序列化为JSON字符串 |
| `JsonUtility.FromJson` | 从JSON字符串反序列化对象 |
| `JsonUtility.ToJsonOverwrite` | 将对象序列化并覆盖现有JSON |

### System.Text.Json 操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `JsonSerializer.Serialize` | 将对象序列化为JSON字符串 |
| `JsonSerializer.Deserialize` | 从JSON字符串反序列化对象 |
| `JsonSerializer.SerializeToUtf8Bytes` | 序列化为UTF-8字节数组 |
| `JsonSerializer.DeserializeFromUtf8Bytes` | 从UTF-8字节数组反序列化 |

### JSON配置选项

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `JsonSerializerOptions.WriteIndented` | 设置是否格式化输出 |
| `JsonSerializerOptions.PropertyNamingPolicy` | 设置属性命名策略 |
| `JsonSerializerOptions.IgnoreNullValues` | 设置是否忽略null值 |
| `JsonSerializerOptions.Encoder` | 设置字符编码器 |

---

## XML序列化

### XmlSerializer 操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `XmlSerializer.Serialize` | 将对象序列化为XML |
| `XmlSerializer.Deserialize` | 从XML反序列化对象 |
| `XmlSerializer.CanDeserialize` | 检查是否可以反序列化 |

### XML序列化属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `[XmlRoot]` | 指定XML根元素 |
| `[XmlElement]` | 指定XML元素 |
| `[XmlAttribute]` | 指定XML属性 |
| `[XmlIgnore]` | 忽略序列化 |
| `[XmlArray]` | 指定数组元素 |
| `[XmlArrayItem]` | 指定数组项元素 |

### XML配置选项

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `XmlWriterSettings.Indent` | 设置是否缩进 |
| `XmlWriterSettings.Encoding` | 设置编码格式 |
| `XmlReaderSettings.IgnoreWhitespace` | 忽略空白字符 |

---

## 二进制序列化

### BinaryFormatter 操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `BinaryFormatter.Serialize` | 将对象序列化为二进制 |
| `BinaryFormatter.Deserialize` | 从二进制反序列化对象 |
| `BinaryFormatter.SurrogateSelector` | 设置代理选择器 |

### 二进制序列化属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `[Serializable]` | 标记类为可序列化 |
| `[NonSerialized]` | 标记字段不序列化 |
| `[OptionalField]` | 标记可选字段 |

### 自定义二进制序列化

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ISerializable.GetObjectData` | 自定义序列化逻辑 |
| `ISerializable` | 自定义序列化接口 |

---

## 数据契约序列化

### DataContractSerializer 操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `DataContractSerializer.WriteObject` | 序列化对象 |
| `DataContractSerializer.ReadObject` | 反序列化对象 |
| `DataContractSerializer.KnownTypes` | 设置已知类型 |

### 数据契约属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `[DataContract]` | 标记数据契约类 |
| `[DataMember]` | 标记数据成员 |
| `[IgnoreDataMember]` | 忽略数据成员 |
| `[EnumMember]` | 标记枚举成员 |

### 数据契约配置

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `DataContractSerializerSettings.MaxItemsInObjectGraph` | 设置最大对象图项数 |
| `DataContractSerializerSettings.PreserveObjectReferences` | 保持对象引用 |

---

## 自定义序列化

### ISerializable 接口

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ISerializable.GetObjectData` | 自定义序列化数据 |
| `ISerializable` | 自定义序列化接口 |

### IFormatter 接口

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `IFormatter.Serialize` | 序列化对象 |
| `IFormatter.Deserialize` | 反序列化对象 |
| `IFormatter.Binder` | 设置绑定器 |

### 序列化代理

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `ISerializationSurrogate.GetObjectData` | 代理序列化 |
| `ISerializationSurrogate.SetObjectData` | 代理反序列化 |

---

## 序列化属性

### 通用序列化属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `[Serializable]` | 标记可序列化 |
| `[NonSerialized]` | 标记不序列化 |
| `[OptionalField]` | 标记可选字段 |
| `[OnSerializing]` | 序列化前调用 |
| `[OnSerialized]` | 序列化后调用 |
| `[OnDeserializing]` | 反序列化前调用 |
| `[OnDeserialized]` | 反序列化后调用 |

### JSON序列化属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `[JsonPropertyName]` | 指定JSON属性名 |
| `[JsonIgnore]` | 忽略JSON序列化 |
| `[JsonConverter]` | 指定JSON转换器 |

### XML序列化属性

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `[XmlRoot]` | 指定XML根元素 |
| `[XmlElement]` | 指定XML元素 |
| `[XmlAttribute]` | 指定XML属性 |
| `[XmlIgnore]` | 忽略XML序列化 |

---

## 文件操作

### 序列化到文件

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `File.WriteAllText` | 写入文本文件 |
| `File.ReadAllText` | 读取文本文件 |
| `File.WriteAllBytes` | 写入二进制文件 |
| `File.ReadAllBytes` | 读取二进制文件 |

### 流操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `FileStream` | 文件流操作 |
| `MemoryStream` | 内存流操作 |
| `StreamWriter` | 文本写入流 |
| `StreamReader` | 文本读取流 |

### 异步文件操作

| OperationFuncName | FuncDesc |
|-------------------|----------|
| `File.WriteAllTextAsync` | 异步写入文本 |
| `File.ReadAllTextAsync` | 异步读取文本 |
| `File.WriteAllBytesAsync` | 异步写入字节 |
| `File.ReadAllBytesAsync` | 异步读取字节 |

---

## 枚举值说明

### JsonIgnoreCondition 枚举

| 值 | 说明 |
|----|------|
| `Never` | 从不忽略 |
| `Always` | 总是忽略 |
| `WhenWritingDefault` | 写入默认值时忽略 |
| `WhenWritingNull` | 写入null时忽略 |

### JsonTokenType 枚举

| 值 | 说明 |
|----|------|
| `None` | 无 |
| `StartObject` | 对象开始 |
| `EndObject` | 对象结束 |
| `StartArray` | 数组开始 |
| `EndArray` | 数组结束 |
| `PropertyName` | 属性名 |
| `String` | 字符串 |
| `Number` | 数字 |
| `True` | 真值 |
| `False` | 假值 |
| `Null` | 空值 |

### XmlNodeType 枚举

| 值 | 说明 |
|----|------|
| `None` | 无 |
| `Element` | 元素 |
| `Attribute` | 属性 |
| `Text` | 文本 |
| `CDATA` | CDATA |
| `EntityReference` | 实体引用 |
| `Entity` | 实体 |
| `ProcessingInstruction` | 处理指令 |
| `Comment` | 注释 |
| `Document` | 文档 |
| `DocumentType` | 文档类型 |
| `DocumentFragment` | 文档片段 |
| `Notation` | 符号 |
| `Whitespace` | 空白 |
| `SignificantWhitespace` | 重要空白 |
| `EndElement` | 元素结束 |
| `EndEntity` | 实体结束 |
| `XmlDeclaration` | XML声明 |

---

## 使用注意事项

1. **性能考虑**：JSON序列化通常比XML和二进制序列化更快
2. **跨平台兼容**：JSON和XML更适合跨平台数据交换
3. **安全性**：二进制序列化可能存在安全风险，不建议用于不受信任的数据
4. **版本控制**：数据契约序列化支持版本控制，适合长期维护
5. **内存使用**：大对象序列化时注意内存使用情况
6. **编码格式**：文本序列化时指定正确的编码格式
7. **循环引用**：注意处理对象间的循环引用问题
8. **异常处理**：序列化操作可能抛出异常，需要适当的异常处理

---

## 示例代码

完整的示例代码请参考 `SerializationExample.cs` 文件，其中包含了所有API的详细使用示例和中文注释。 