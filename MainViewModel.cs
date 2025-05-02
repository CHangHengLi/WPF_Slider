using System.ComponentModel;

namespace SliderDemo
{
    /// <summary>
    /// ViewModel类，用于数据绑定示例
    /// 实现INotifyPropertyChanged接口以支持UI更新
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private double _sliderValue = 50; // 默认值为50

        /// <summary>
        /// 滑块值属性
        /// </summary>
        public double SliderValue
        {
            get => _sliderValue;
            set
            {
                if (_sliderValue != value)
                {
                    _sliderValue = value;
                    // 触发属性变更通知
                    OnPropertyChanged(nameof(SliderValue));
                }
            }
        }

        /// <summary>
        /// PropertyChanged事件，当属性值变化时触发
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 触发PropertyChanged事件的方法
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 