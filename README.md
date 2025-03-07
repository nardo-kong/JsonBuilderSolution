# JSON Generator 说明书

## 一、项目简介

**JSON Generator** 是一款功能强大的 JSON 生成器工具。它内置了多个固定的模板，用户只需选择相应模板并填写相关信息，即可自动生成所需的 JSON 数据。此外，本工具还提供了扩展功能，用户可以轻松增加新的模板以满足个性化需求。

## 二、功能特点

- **内置模板**：提供多种常用 JSON 数据结构模板，涵盖常见的场景和格式。
- **灵活扩展**：支持用户自定义模板，方便处理特殊需求。
- **简单易用**：操作界面简洁直观，无需复杂的配置步骤即可上手。
- **实时生成**：填写信息后立即生成 JSON 数据，方便用户快速验证和使用。

## 三、安装与使用

### 安装
1. 下载项目代码或安装包。
2. 如果是源码，确保已安装 .NET 6.0 或以上版本。
3. 打开项目并通过 Visual Studio 或其他 IDE 编译运行。

### 使用
1. 打开程序后，点击“模板选择”下拉菜单，选择需要的 JSON 模板。
2. 根据界面上的提示填写相关信息。
3. 填写完成后，点击“生成 JSON”按钮，即可在窗口下方看到生成的 JSON 数据。
4. 生成的 JSON 数据可直接复制粘贴使用。

## 四、核心功能说明

### 模板选择
- 从下拉菜单中选择内置模板。
- 每个模板对应一种特定的 JSON 结构。

### 信息填充
- 填写对应模板所需的字段信息。
- 支持数字、字符串、布尔值等多种数据类型。

### JSON 生成
- 根据所选模板和填写的信息，按照既定格式生成 JSON 数据。
- 自动生成完整的 JSON 对象，包括括号、逗号等格式。

## 五、如何增加模板

若需新增模板，请按照以下步骤操作：

1. 打开项目代码，在指定目录下新建一个模板文件（例如 `TemplateName.json`），定义模板结构。

2. 根据新模板的结构，修改以下文件和代码：
   - **`MessageBase` 的子类**
   - **`MessageParamBase` 的子类**
   - **`xaml` 文件**：在界面上为新模板增加相应的输入布局（`Input Layout`），对应字段的输入控件。
   - **`MainViewModel.cs`**：在 `InitialMessageType` 方法中添加新模板的预定义类型。
   - **`JsonGenerator.cs`**：在 `Parse` 方法中增加对新模板的解析逻辑，将输入信息转化为 JSON。

3. 重新编译项目后，新模板即可在“模板选择”菜单中出现。
