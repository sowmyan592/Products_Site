using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Products_Site.Models
{
    public class Product_Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_id { get; set; }
        [Required]
        public string Product_type { get; set; }
        [Required]
        public string Product_name { get; set; }
        [Required]
        public decimal Product_price { get; set; }
        [Required]
        public double Product_weight { get; set; }
        [Required]
        public string Product_description { get; set; }


    }
}