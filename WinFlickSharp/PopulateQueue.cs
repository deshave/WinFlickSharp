using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace WinFlickSharp
{
	public class PopulateQueue
	{
		private ConcurrentQueue<PhotoPanel.FlickrPhotoPanel> cq;
		public event EventHandler ItemEnqueued;

		public bool IsQueueEmpty;

		public PopulateQueue()
		{
			cq = new ConcurrentQueue<PhotoPanel.FlickrPhotoPanel>();
			IsQueueEmpty = true;
		}

		public void Enqueue(PhotoPanel.FlickrPhotoPanel fpp)
		{
			cq.Enqueue(fpp);
			OnItemEnqueued(this, new EventArgs());
			IsQueueEmpty = false;
		}

		public PhotoPanel.FlickrPhotoPanel Dequeue()
		{
			PhotoPanel.FlickrPhotoPanel fpp;
			if (!cq.TryDequeue(out fpp))
			{
				IsQueueEmpty = true;
				throw new Exception("Queue is empty.");
			}
			return fpp;
		}

		public void OnItemEnqueued(object sender, EventArgs e)
		{
			ItemEnqueued?.Invoke(sender, new EventArgs());
		}
	}
}
