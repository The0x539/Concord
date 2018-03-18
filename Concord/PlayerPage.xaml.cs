using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Concord {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class PlayerPage : Page {
		public MediaPlayer PlayerObj { get; private set; }
		public PlayerPage() {
			PlayerObj = new MediaPlayer();
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			PlayerObj.Source = MediaSource.CreateFromStorageFile(e.Parameter as StorageFile);
			PlayerElement.SetMediaPlayer(PlayerObj);
		}

		private void Close(object sender, RoutedEventArgs e) {
			(Window.Current.Content as Frame).Navigate(typeof(MainPage), App.RootFolder);
			PlayerObj.Dispose();
		}
	}
}
