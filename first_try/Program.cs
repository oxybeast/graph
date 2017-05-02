using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphSolver
{
    internal partial class Program
    {

        private static void Main(string[] args)
        {
            var check = new Solver();
            check.solve();
            check.WriteAllPaths();
        }
    }
    
}
