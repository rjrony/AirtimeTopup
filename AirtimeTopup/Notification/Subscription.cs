using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AirtimeTopup.Notification
{
    public class Subscription<Tmessage> : IDisposable
    {
        public readonly MethodInfo MethodInfo;
        private readonly EventAggregator EventAggregator;
        public readonly WeakReference TargetObjet;
        public readonly bool IsStatic;

        private bool isDisposed;
        public Subscription(Action<Tmessage> action, EventAggregator eventAggregator)
        {
            MethodInfo = action.Method;
            if (action.Target == null)
                IsStatic = true;
            TargetObjet = new WeakReference(action.Target);
            EventAggregator = eventAggregator;
        }

        ~Subscription()
        {
            if (!isDisposed)
                Dispose();
        }

        public void Dispose()
        {
            EventAggregator.UnSbscribe(this);
            isDisposed = true;
        }

        public Action<Tmessage> CreatAction()
        {
            if (TargetObjet.Target != null && TargetObjet.IsAlive)
                return (Action<Tmessage>)Delegate.CreateDelegate(typeof(Action<Tmessage>), TargetObjet.Target, MethodInfo);
            if (this.IsStatic)
                return (Action<Tmessage>)Delegate.CreateDelegate(typeof(Action<Tmessage>), MethodInfo);

            return null;
        }
    }
}
