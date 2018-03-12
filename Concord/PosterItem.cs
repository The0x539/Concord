using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;

namespace Concord {
	class PosterItem : ListItem {
		static readonly BitmapImage fallback = new BitmapImage(new Uri("/Assets/StoreLogo.png"));
		public BitmapImage image { get; }

		public PosterItem(IStorageItem item) : base(item) {
			image = new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png", UriKind.Absolute));
		}
	}
}
