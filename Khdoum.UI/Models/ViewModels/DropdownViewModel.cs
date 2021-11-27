using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.UI.Models.ViewModels
{
    public class DropdownViewModel<T>
    {
        public  T Value { get; set; }
        public  string Name { get; set; }
    }
}
