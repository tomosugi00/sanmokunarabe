using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Sanmoku
{
    /// <summary>
    /// 既定の Application クラスを補完するアプリケーション固有の動作を提供します。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        ///コンストラクタ。
        ///<para>※実行される作成したコードの最初の行であることに注意(論理的にはmain()またはWinMain()と等価)</para>
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        public static class BindingUtilities
        {
            public static Visibility BoolToVisibility(bool value) => BoolToVisibility(value, true);

            public static Visibility BoolToVisibility(bool value, bool trueIsVisible)
            {
                if (trueIsVisible)
                {
                    return value ? Visibility.Visible : Visibility.Collapsed;
                }

                return value ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        /// <summary>
        /// アプリケーションがエンド ユーザーによって正常に起動されたときに呼び出されます。
        /// (他のエントリポイント)アプリケーションが特定のファイルを開くために起動された時等
        /// </summary>
        /// <param name="e">起動の要求とプロセスの詳細を表示します。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // ウィンドウにコンテンツが存在しない場合
            if (!(Window.Current.Content is Frame rootFrame))
            {
                // ナビゲーション コンテキストとして動作するフレームを作成し、最初のページに移動します
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                //前回終了時の状態を復元
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO アプリケーションの状態を復元
                }

                Window.Current.Content = rootFrame;
            }

            // 事前起動していない場合
            if (!e.PrelaunchActivated)
            {
                if (rootFrame.Content == null)
                {
                    // 最初のページに画面遷移
                    rootFrame.Navigate(typeof(Views.MainPage), e.Arguments);
                }
                // 現在のウィンドウをアクティブ化
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// 特定のページへの移動が失敗したときに呼び出されます
        /// </summary>
        /// <param name="sender">移動に失敗したフレーム</param>
        /// <param name="e">ナビゲーション エラーの詳細</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// アプリケーションの実行が中断されたときに呼び出されます。
        /// <para>アプリケーションが終了されるか、メモリの内容がそのままで再開されるかに
        /// かかわらず、アプリケーションの状態が保存されます。</para>
        /// </summary>
        /// <param name="sender">中断要求の送信元。</param>
        /// <param name="e">中断要求の詳細。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO アプリケーションの状態を保存
            deferral.Complete();
        }
    }
}
