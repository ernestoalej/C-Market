﻿using C_Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C_Market.ViewModels
{
    public class OrderView
    {
        public Customer Customer { get; set; }
        public List<ProductOrder> Products { get; set; }
    }
}