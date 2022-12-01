using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Dtos
{
    public record CreateItmDto
    {
        public string Name { get; init; }
        public decimal Price { get; set; }

    }
}