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
using System.Collections.ObjectModel;
using Windows.Storage.Pickers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Concord {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CrumbView : Page {
		public ObservableCollection<StorageFolder> Crumbs;
		public CrumbView() {
			InitializeComponent();
			Crumbs = new ObservableCollection<StorageFolder>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			Crumbs.Clear();
			Crumbs.Add((StorageFolder) e.Parameter);
		}

		private void crumbs_ItemClick(object sender, ItemClickEventArgs e) {
			bool foo = false;
			for (int i = Crumbs.Count; i-- > 0;) {
				StorageFolder crumb = Crumbs[i];
				if (crumb == e.ClickedItem)
					break;
				Crumbs.RemoveAt(i);
				foo = true;
			}
			if (foo)
				((MainPage)((Grid)Parent).Parent).NavigateTo((StorageFolder)e.ClickedItem);
			else if (Crumbs.Count == 1)
				App.NewRoot();
			else
				((MainPage)((Grid) Parent).Parent).RefreshList();
		}
	}

}
