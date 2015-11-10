using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFootball.Core.Abstract
{
    public interface ISubject
    {
        void AttachObserver(IObserver observer);
        void DetachObserver(IObserver observer);
        void Notify();
    } 
}
