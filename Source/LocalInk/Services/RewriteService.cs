using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalInk.Services
{
    internal class RewriteService
    {
        public string RewriteText(string text, string mode)
        {
            return $"[{mode} Rewrite]\n\n{text}";
        }
    }
}
