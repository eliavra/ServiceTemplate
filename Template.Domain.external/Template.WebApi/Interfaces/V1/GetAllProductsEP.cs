using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.external.Template.WebApi.Interfaces.V1
{
    public class GetAllProductsEP
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
