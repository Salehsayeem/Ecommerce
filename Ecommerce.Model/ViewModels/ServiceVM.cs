﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model.ViewModels
{
    public class ServiceVM
    {
        public Service service { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FrequencyList { get; set; }
    }
}
