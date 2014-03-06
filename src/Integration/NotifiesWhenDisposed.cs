namespace Ninject.Integration
{
    using System;
    using Ninject.Infrastructure.Disposal;

    public class NotifiesWhenDisposed : DisposableObject, INotifyWhenDisposed
    {
        public event EventHandler Disposed;

        public override void Dispose(bool disposing)
        {
            lock (this)
            {
                if (disposing && !this.IsDisposed)
                {
                    var evt = this.Disposed;
                    if (evt != null)
                    {
                        evt(this, EventArgs.Empty);
                    }

                    this.Disposed = null;
                }

                base.Dispose(disposing);
            }
        }
    }
}