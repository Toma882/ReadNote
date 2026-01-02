# UI 系统架构设计

## 按钮设计架构图
```mermaid
graph TB
    subgraph Client["客户端"]
        Context["ButtonContext<br/>themeId + colorStyle + state"]
    end
    
    subgraph ButtonBase["ButtonBase<br/>按钮基类"]
        GetTheme["获取主题配置"]
        GetSprite["根据colorStyle<br/>获取颜色风格贴图"]
        ApplySprite["应用贴图"]
        UpdateState["UpdateState<br/>Normal/Gray"]
    end
    
    subgraph ThemeSystem["主题系统"]
        ThemeManager["ButtonThemeConfig"]
        Theme1[黄→贴图名<br/>绿→贴图名<br/>红→贴图名<br/>灰→贴图名]
        Theme2[黄→贴图名2<br/>绿→贴图名2<br/>红→贴图名2<br/>灰→贴图名2]
    end
    
    subgraph StateSystem["状态系统"]
        Normal["Normal<br/>原色可用"]
        Gray["Gray<br/>灰色不可用<br/>GrayComponentSette"]
    end
    
    subgraph UIComponent["UI 组件"]
        Image["bgImage"]
        Text["text"]
    end
    
    Context --> ButtonBase
    ButtonBase --> GetTheme
    GetTheme --> ThemeManager
    ThemeManager --> Theme1
    ThemeManager --> Theme2
    GetTheme --> GetSprite
    GetSprite --> ApplySprite
    ApplySprite --> UIComponent
    ApplySprite --> UpdateState
    UpdateState --> Normal
    UpdateState --> Gray
    Gray --> UIComponent
```

### 核心设计说明

**主题系统**：定义颜色风格贴图映射（黄/绿/红/灰 → 贴图名），通过 `themeId` 选择主题，通过 `colorStyle` 选择颜色风格。

**状态系统**：控制按钮可用性（Normal=原色可用，Gray=灰色不可用），独立于主题系统。

**ButtonBase**：统一基类，根据主题配置应用贴图，根据状态控制可用性。

