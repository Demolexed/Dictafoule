using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TextToTask.WebJob
{
    class FouleFactoryAccountModel
    {
        [JsonProperty("IdGender")]
        public int IdGender { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }
    }

    public class FouleFactoryWalletModel
    {
        [JsonProperty("AmountWithoutTax")]
        public int AmountWithoutTax { get; set; }
        [JsonProperty("AmountTaxes")]
        public int AmountTaxes { get; set; }
    }
}
