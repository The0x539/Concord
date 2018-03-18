﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.Storage.Streams;

namespace Concord {
	class PosterItem : ListItem {
		static readonly BitmapImage fallback = new BitmapImage(new Uri("/Assets/StoreLogo.png"));
		public BitmapImage Image { get; private set; }

		public PosterItem(IStorageItem item) : base(item) {
			Image = new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png", UriKind.Absolute));
			GetImage();
		}

		private async void GetImage() {
			if (StorageItem is StorageFile file) {
				const uint requestedSize = 512;
				const ThumbnailMode thumbnailMode = ThumbnailMode.SingleItem;
				const ThumbnailOptions thumbnailOptions = ThumbnailOptions.UseCurrentScale;
				StorageItemThumbnail thumbnail = await file.GetThumbnailAsync(thumbnailMode, requestedSize, thumbnailOptions);
				Image.DecodePixelWidth = (int) thumbnail.OriginalWidth;
				Image.DecodePixelHeight = (int) thumbnail.OriginalHeight;
				await Image.SetSourceAsync(thumbnail);
			} else if (StorageItem is StorageFolder folder) {
				StorageFile img;
				try {
					img = await folder.GetFileAsync("poster.jpg");
				} catch {
					return;
				}
				IRandomAccessStream stream = await img.OpenAsync(FileAccessMode.Read);
				await Image.SetSourceAsync(stream);
			}
		}
	}
}
