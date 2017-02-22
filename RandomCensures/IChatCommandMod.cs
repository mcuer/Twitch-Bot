using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomCensures
{
    public interface IChatCommandMod
    {
        string ProcessMessage(string speaker, string message);
    }
}
