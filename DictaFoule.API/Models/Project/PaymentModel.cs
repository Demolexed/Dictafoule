using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DictaFoule.API.Models.Project
{
    public class PaymentModel
    {
        public string Token { get; set; }
        public decimal Amount { get; set; }
        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}