using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vluchten_Models;

namespace Vluchten_DAL
{
    public partial class Reservering
    {
        public override string ToString()
        {
            return this.boekingscode;
        }
    }
}
