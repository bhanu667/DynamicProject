using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE1.Models
{
    public class OrderAPI
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string PayerId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Intent { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string PaymentMethod { get; set; }
        public string Amount { get; set; }
        public string ShippingAddress { get; set; }
        public string TransactionFee { get; set; }
        public string SaleId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
