using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomCensures
{
    public class Votant
    {
        public string speaker { get; set; }
        public int voteValue { get; set; }

        public Votant(string speaker, int voteValue)
        {
            this.speaker = speaker;
            this.voteValue = voteValue;
        }
    }
}
