using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Options
{
    public class JWTOptions
    {
        public string Key { get; set; }
        public int ExpiredHours { get; set; }
    }
}
