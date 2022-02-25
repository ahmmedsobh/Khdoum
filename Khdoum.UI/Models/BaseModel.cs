using Khdoum.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.UI.Models
{
    public class BaseModel
    {
        public string BaseAddres { get; set; } = Constant.BaseAddress;

        
        public string AccessToken { get; set; }
    }
}
