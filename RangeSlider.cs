using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SliderDemo
{
    /// <summary>
    /// 自定义RangeSlider控件，用于在一个范围内选择两个值
    /// </summary>
    public class RangeSlider : Control
    {
        static RangeSlider()
        {
            // 重写默认样式键，使控件使用Generic.xaml中定义的样式
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RangeSlider), 
                new FrameworkPropertyMetadata(typeof(RangeSlider)));
        }

        #region 依赖属性

        /// <summary>
        /// 最小值依赖属性
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(RangeSlider),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 最大值依赖属性
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(RangeSlider),
                new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 下限值依赖属性
        /// </summary>
        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double), typeof(RangeSlider),
                new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.AffectsRender, OnLowerValueChanged));

        /// <summary>
        /// 上限值依赖属性
        /// </summary>
        public static readonly DependencyProperty UpperValueProperty =
            DependencyProperty.Register("UpperValue", typeof(double), typeof(RangeSlider),
                new FrameworkPropertyMetadata(80.0, FrameworkPropertyMetadataOptions.AffectsRender, OnUpperValueChanged));

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置最小值
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// 获取或设置最大值
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// 获取或设置下限值，会自动限制在最小值到上限值之间
        /// </summary>
        public double LowerValue
        {
            get { return (double)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, Math.Max(Minimum, Math.Min(UpperValue, value))); }
        }

        /// <summary>
        /// 获取或设置上限值，会自动限制在下限值到最大值之间
        /// </summary>
        public double UpperValue
        {
            get { return (double)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, Math.Min(Maximum, Math.Max(LowerValue, value))); }
        }

        #endregion

        #region 值变化事件

        /// <summary>
        /// 下限值变化路由事件
        /// </summary>
        public static readonly RoutedEvent LowerValueChangedEvent =
            EventManager.RegisterRoutedEvent("LowerValueChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<double>), typeof(RangeSlider));

        /// <summary>
        /// 上限值变化路由事件
        /// </summary>
        public static readonly RoutedEvent UpperValueChangedEvent =
            EventManager.RegisterRoutedEvent("UpperValueChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<double>), typeof(RangeSlider));

        /// <summary>
        /// 下限值变化事件
        /// </summary>
        public event RoutedPropertyChangedEventHandler<double> LowerValueChanged
        {
            add { AddHandler(LowerValueChangedEvent, value); }
            remove { RemoveHandler(LowerValueChangedEvent, value); }
        }

        /// <summary>
        /// 上限值变化事件
        /// </summary>
        public event RoutedPropertyChangedEventHandler<double> UpperValueChanged
        {
            add { AddHandler(UpperValueChangedEvent, value); }
            remove { RemoveHandler(UpperValueChangedEvent, value); }
        }

        /// <summary>
        /// 下限值变化回调
        /// </summary>
        private static void OnLowerValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider slider = (RangeSlider)d;
            slider.OnLowerValueChanged((double)e.OldValue, (double)e.NewValue);
        }

        /// <summary>
        /// 上限值变化回调
        /// </summary>
        private static void OnUpperValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider slider = (RangeSlider)d;
            slider.OnUpperValueChanged((double)e.OldValue, (double)e.NewValue);
        }

        /// <summary>
        /// 触发下限值变化事件
        /// </summary>
        protected virtual void OnLowerValueChanged(double oldValue, double newValue)
        {
            RoutedPropertyChangedEventArgs<double> args = new RoutedPropertyChangedEventArgs<double>(oldValue, newValue);
            args.RoutedEvent = LowerValueChangedEvent;
            RaiseEvent(args);
        }

        /// <summary>
        /// 触发上限值变化事件
        /// </summary>
        protected virtual void OnUpperValueChanged(double oldValue, double newValue)
        {
            RoutedPropertyChangedEventArgs<double> args = new RoutedPropertyChangedEventArgs<double>(oldValue, newValue);
            args.RoutedEvent = UpperValueChangedEvent;
            RaiseEvent(args);
        }

        #endregion

        #region 重写方法

        /// <summary>
        /// 当模板应用到控件时调用
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // 这里可以获取模板中的元素并添加事件处理
            // 在完整实现中，需要处理LowerThumb和UpperThumb的拖动事件
        }

        #endregion
    }
} 