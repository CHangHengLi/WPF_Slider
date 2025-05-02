# WPF Slider 控件学习示例

这是一个基于.NET Core 8.0的WPF Slider控件学习示例项目，演示了WPF中Slider控件的各种功能和用法。

## 项目说明

本项目根据《WPF之Slider控件详解》文档创建，包含多个示例演示了Slider控件的基本属性、样式设置、事件处理、数据绑定和实际应用场景。

## 项目结构

- `MainWindow.xaml` - 主窗口UI定义
- `MainWindow.xaml.cs` - 主窗口后台代码
- `MainViewModel.cs` - 数据绑定示例的ViewModel
- `RangeSlider.cs` - 自定义RangeSlider控件
- `Themes/Generic.xaml` - 自定义控件样式模板

## 功能模块

项目按照不同的功能模块组织在多个选项卡中：

### 1. 基本属性

演示Slider的基本属性设置，包括：
- 最小值、最大值和当前值
- 步长设置 (SmallChange, LargeChange, TickFrequency)
- 刻度线位置 (None, TopLeft, BottomRight, Both)
- 滑块吸附到刻度线功能
- 自定义刻度点

### 2. 方向与范围

演示Slider的方向和选择范围功能：
- 水平和垂直方向的Slider
- 设置选择范围，高亮显示指定区域

### 3. 样式与模板

演示如何自定义Slider的外观：
- 使用Style设置基本样式
- 使用ControlTemplate完全自定义Slider的外观

### 4. 应用示例

包含几个实际应用场景：
- 音量控制器 - 带静音功能的音量控制
- 颜色选择器 - 使用RGB三个滑块创建颜色
- 数据可视化 - 使用滑块导航显示数据图表

### 5. 数据绑定

演示Slider与数据绑定的结合：
- 绑定到ViewModel - 使用MVVM模式
- 多控件同步 - 将多个控件绑定到同一个数据源

## 如何运行

1. 确保已安装.NET Core 8.0 SDK
2. 使用Visual Studio 2022或更高版本打开解决方案文件 `SliderDemo.sln`
3. 编译并运行项目

## 学习要点

- Slider的基本属性和事件
- 刻度和步长设置
- 自定义Slider的外观
- 使用数据绑定与Slider交互
- 在实际应用中使用Slider
- 创建自定义RangeSlider控件

## 注意事项

- 本项目仅作为学习示例，展示了WPF Slider控件的基本用法和一些高级特性
- 自定义的RangeSlider控件只实现了基本功能，在实际应用中可能需要更多优化
- 代码中包含详细注释，方便理解每个部分的功能和实现方式

## 参考资料

- WPF Slider类官方文档
- WPF控件自定义指南
- WPF数据绑定概述 