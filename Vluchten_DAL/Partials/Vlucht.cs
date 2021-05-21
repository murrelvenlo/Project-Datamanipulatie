using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vluchten_Models;

namespace Vluchten_DAL
{
    public partial class Vlucht
    {
        public override bool Equals(object obj)
        {
            return obj is Vlucht vlucht &&
                   id == vlucht.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + EqualityComparer<string>.Default.GetHashCode(id);
        }

        public override string ToString()
        {
            return this.Luchthaven1.stad;
        }

        public Vlucht(decimal vluchtprijs)
        {
            this.vluchtprijs = vluchtprijs;
        }

    }
}
