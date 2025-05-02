using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SliderDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;
    private List<double> _dataPoints;
    private double _previousVolume = 50;

    public MainWindow()
    {
        InitializeComponent();

        // 创建ViewModel并设置为DataContext
        _viewModel = new MainViewModel();
        this.DataContext = _viewModel;

        // 初始化数据点
        GenerateDataPoints();

        // 初始化颜色预览
        UpdateColorPreview();

        // 初始化时绘制数据可视化
        this.Loaded += (s, e) => DrawDataVisualization();
    }

    #region 基本属性滑块事件

    /// <summary>
    /// 基本滑块值变化事件处理
    /// </summary>
    private void BasicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        // 更新显示的值
        if (basicSliderValue != null)
        {
            basicSliderValue.Text = $"当前值: {e.NewValue:F1}";
        }
    }

    #endregion

    #region 范围滑块事件

    /// <summary>
    /// 范围滑块值变化事件处理
    /// </summary>
    private void RangeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        // 更新显示的值
        if (rangeText != null)
        {
            rangeText.Text = $"范围: {rangeSlider.SelectionStart:F1} - {rangeSlider.SelectionEnd:F1}, 当前值: {e.NewValue:F1}";
        }
    }

    /// <summary>
    /// 更新范围按钮点击事件
    /// </summary>
    private void UpdateRange_Click(object sender, RoutedEventArgs e)
    {
        // 随机生成新的范围值
        Random random = new Random();
        double start = random.NextDouble() * 40; // 0-40之间的范围起始值
        double end = start + random.NextDouble() * (100 - start - 10) + 10; // 确保结束值比起始值至少大10且不超过100

        // 更新范围
        rangeSlider.SelectionStart = start;
        rangeSlider.SelectionEnd = end;

        // 如果当前值不在范围内，设置为范围的中间值
        if (rangeSlider.Value < start || rangeSlider.Value > end)
        {
            rangeSlider.Value = (start + end) / 2;
        }

        // 更新显示文本
        rangeText.Text = $"范围: {start:F1} - {end:F1}, 当前值: {rangeSlider.Value:F1}";
    }

    #endregion

    #region 音量控制器事件

    /// <summary>
    /// 音量滑块值变化事件处理
    /// </summary>
    private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        // 更新显示的值
        if (volumeValue != null)
        {
            volumeValue.Text = $"{e.NewValue:F0}%";
        }

        // 如果不是静音状态，保存当前音量值
        if (muteCheckBox != null && !muteCheckBox.IsChecked.GetValueOrDefault())
        {
            _previousVolume = e.NewValue;
        }
    }

    /// <summary>
    /// 静音选中事件处理
    /// </summary>
    private void MuteCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        // 保存当前音量值并设置为0
        _previousVolume = volumeSlider.Value;
        volumeSlider.Value = 0;
    }

    /// <summary>
    /// 静音取消选中事件处理
    /// </summary>
    private void MuteCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        // 恢复之前的音量
        volumeSlider.Value = _previousVolume;
    }

    #endregion

    #region 颜色选择器事件

    /// <summary>
    /// 颜色滑块值变化事件处理
    /// </summary>
    private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        // 更新RGB值文本显示
        if (redValue != null && greenValue != null && blueValue != null)
        {
            redValue.Text = ((int)redSlider.Value).ToString();
            greenValue.Text = ((int)greenSlider.Value).ToString();
            blueValue.Text = ((int)blueSlider.Value).ToString();
        }
        
        UpdateColorPreview();
    }

    /// <summary>
    /// 更新颜色预览
    /// </summary>
    private void UpdateColorPreview()
    {
        // 确保控件已初始化
        if (colorPreview == null || hexColorTextBlock == null)
            return;

        // 创建色彩对象
        Color color = Color.FromRgb(
            (byte)redSlider.Value,
            (byte)greenSlider.Value,
            (byte)blueSlider.Value);

        // 更新预览背景色
        colorPreview.Background = new SolidColorBrush(color);

        // 更新16进制值显示
        hexColorTextBlock.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    #endregion

    #region 数据可视化事件

    /// <summary>
    /// 应用示例选项卡切换事件处理
    /// </summary>
    private void AppExamples_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // 获取当前选中的选项卡
        if (sender is TabControl tabControl && tabControl.SelectedItem is TabItem selectedTab)
        {
            // 如果选中的是数据可视化选项卡
            if (selectedTab.Header.ToString() == "数据可视化")
            {
                // 绘制数据可视化
                DrawDataVisualization();
            }
        }
    }

    /// <summary>
    /// 生成随机数据点
    /// </summary>
    private void GenerateDataPoints()
    {
        _dataPoints = new List<double>();
        Random random = new Random();

        for (int i = 0; i < 100; i++)
        {
            _dataPoints.Add(random.NextDouble() * 80 + 10); // 值在10-90之间
        }
    }

    /// <summary>
    /// Canvas大小变化事件处理
    /// </summary>
    private void VisualizationCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        // 当Canvas大小变化时重新绘制数据
        if (e.NewSize.Width > 0 && e.NewSize.Height > 0)
        {
            DrawDataVisualization();
        }
    }

    /// <summary>
    /// 绘制数据可视化
    /// </summary>
    private void DrawDataVisualization()
    {
        // 清除画布
        visualizationCanvas.Children.Clear();

        // 获取画布尺寸
        double width = visualizationCanvas.ActualWidth;
        double height = visualizationCanvas.ActualHeight;

        // 如果画布尺寸无效，则退出
        if (width <= 0 || height <= 0)
            return;

        // 确保数据点已初始化
        if (_dataPoints == null || _dataPoints.Count == 0)
        {
            GenerateDataPoints();
            if (_dataPoints == null || _dataPoints.Count == 0)
                return;
        }

        // 计算最大值和最小值，用于数据缩放
        double maxValue = 0;
        double minValue = 100;
        foreach (var point in _dataPoints)
        {
            maxValue = Math.Max(maxValue, point);
            minValue = Math.Min(minValue, point);
        }
        
        // 确保有数据范围，避免除以零
        double valueRange = maxValue - minValue;
        if (valueRange <= 0)
            valueRange = 1;

        // 计算柱子宽度
        double segmentWidth = width / _dataPoints.Count;

        // 绘制数据柱状图
        for (int i = 0; i < _dataPoints.Count; i++)
        {
            double value = _dataPoints[i];
            
            // 计算高度，添加5%的边距
            double barHeight = (value - minValue) / valueRange * (height * 0.95);

            // 创建矩形表示数据点
            Rectangle rect = new Rectangle
            {
                Width = Math.Max(1, segmentWidth - 1), // 至少1个像素宽
                Height = Math.Max(1, barHeight),       // 至少1个像素高
                Fill = new SolidColorBrush(Color.FromRgb(33, 150, 243)),
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 0.5
            };

            // 设置矩形位置，从底部向上绘制
            Canvas.SetLeft(rect, i * segmentWidth);
            Canvas.SetBottom(rect, 0);

            // 添加到画布
            visualizationCanvas.Children.Add(rect);
        }

        // 绘制当前位置指示器
        DrawPositionIndicator();
    }

    /// <summary>
    /// 绘制位置指示器
    /// </summary>
    private void DrawPositionIndicator()
    {
        if (_dataPoints == null || _dataPoints.Count == 0)
            return;

        double width = visualizationCanvas.ActualWidth;
        double height = visualizationCanvas.ActualHeight;
        
        if (width <= 0 || height <= 0)
            return;
            
        int index = (int)dataSlider.Value;

        if (index >= _dataPoints.Count)
            index = _dataPoints.Count - 1;

        double segmentWidth = width / _dataPoints.Count;

        // 绘制位置指示线
        Line line = new Line
        {
            X1 = index * segmentWidth + segmentWidth / 2,
            Y1 = 0,
            X2 = index * segmentWidth + segmentWidth / 2,
            Y2 = height,
            Stroke = new SolidColorBrush(Colors.Red),
            StrokeThickness = 2
        };

        visualizationCanvas.Children.Add(line);

        // 显示当前值
        TextBlock textBlock = new TextBlock
        {
            Text = $"{_dataPoints[index]:F1}",
            Foreground = new SolidColorBrush(Colors.Black),
            Background = new SolidColorBrush(Color.FromArgb(200, 255, 255, 255)),
            Padding = new Thickness(3)
        };

        // 调整文本框位置，使其不超出画布边界
        double textLeft = index * segmentWidth;
        if (textLeft + 50 > width) // 假设文本框宽度大约50像素
            textLeft = width - 50;
            
        Canvas.SetLeft(textBlock, textLeft);
        Canvas.SetTop(textBlock, 10);

        visualizationCanvas.Children.Add(textBlock);

        // 更新值文本
        dataValueText.Text = $"当前值: {_dataPoints[index]:F1}";
    }

    /// <summary>
    /// 数据滑块值变化事件处理
    /// </summary>
    private void DataSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        DrawDataVisualization();
    }

    /// <summary>
    /// 生成新数据按钮点击事件
    /// </summary>
    private void GenerateNewData_Click(object sender, RoutedEventArgs e)
    {
        // 生成新的随机数据
        GenerateDataPoints();
        
        // 重新绘制可视化
        DrawDataVisualization();
    }

    #endregion

    #region 数据绑定事件

    /// <summary>
    /// 在ViewModel中设置值的按钮点击事件
    /// </summary>
    private void SetViewModel_Click(object sender, RoutedEventArgs e)
    {
        // 通过ViewModel更新值
        _viewModel.SliderValue = 75;
    }

    #endregion
}