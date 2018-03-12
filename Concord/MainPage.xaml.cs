using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Storage;
using Windows.Storage.Pickers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Concord {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page {
		public MainPage() {
			InitializeComponent();
		}

		public void PushItem(StorageFolder item) {
			CrumbView.Crumbs.Add(item);
		}
		public void NavigateTo(StorageFolder item) {
			ListFrame.Navigate(typeof(PosterPage), item);
		}

		protected sealed override void OnNavigatedTo(NavigationEventArgs e) {
			StorageFolder folder = (StorageFolder) e.Parameter;
			NavigateTo(folder);
		}
	}
}
