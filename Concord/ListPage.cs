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
using System.Threading.Tasks;
using System.Text;
using System.Collections.ObjectModel;
using Windows.System;

namespace Concord {
	public abstract class ListPage : Page {
		protected readonly ObservableCollection<ListItem> Items = new ObservableCollection<ListItem>();
		protected StorageFolder Folder;

		public async void Populate() {
			var tFolders = Folder.GetFoldersAsync();
			var tFiles = Folder.GetFilesAsync();

			var folders = new List<ListItem>();
			var files = new List<ListItem>();

			foreach (var folder in await tFolders)
				folders.Add(MakeItem(folder));
			folders.Sort();

			var items = new List<ListItem>();

			foreach (var folder in folders)
				items.Add(folder);

			foreach (var file in await tFiles)
				files.Add(MakeItem(file));
			files.Sort();

			foreach (var file in files)
				items.Add(file);

			Items.Clear();
			foreach (var item in items)
				Items.Add(item);
		}

		protected virtual ListItem MakeItem(IStorageItem item) => new ListItem(item);

		protected sealed override void OnNavigatedTo(NavigationEventArgs e) {
			Folder = (StorageFolder) e.Parameter;
			Populate();
		}

		protected async void listview_ItemClick(object sender, ItemClickEventArgs e) {
			ListItem clickedItem = (ListItem) e.ClickedItem;
			if (clickedItem.IsFolder) {
				var mainPage = (MainPage) ((Grid) Frame.Parent).Parent;
				mainPage.PushItem((StorageFolder) clickedItem.StorageItem);
				Frame.Navigate(GetType(), clickedItem.StorageItem);
			} else {
				await Launcher.LaunchFileAsync((StorageFile) clickedItem.StorageItem);
			}
		}
	}
}
