using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helpers
{
    public static class RandomCodeGenerator
    {
        public static string Generate(int length = 8)
        {
            return Guid.NewGuid().ToString("N")[..length].ToUpper(); // Örneğin: "A1B2C3D4"
        }
    }
}
