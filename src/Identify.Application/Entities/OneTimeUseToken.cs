using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Application.Entities
{
    public class OneTimeUseToken
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpires { get; set; }
        public bool Consumed { get; set; }
    }
}
