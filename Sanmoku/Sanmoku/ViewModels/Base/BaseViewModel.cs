using System.ComponentModel;

namespace Sanmoku.ViewModels.Base
{
    /// <summary>
    /// ViewModelクラスの共通項目を管理
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// ViewModelのプロパティが更新された時のイベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// <see cref="PropertyChanged">に登録されたイベントを発火
        /// </summary>
        /// <param name="propertyName">更新があったプロパティ名</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
