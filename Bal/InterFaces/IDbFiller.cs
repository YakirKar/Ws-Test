using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bal.InterFaces
{
    public interface IDbFiller
    {
         object FillRow(Dictionary<string, object> row);
    }
}
