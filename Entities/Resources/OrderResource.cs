using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class OrderResource
    {
        public int Id { get; set; }
        public UserResource User { get; set; }
        public Status Status { get; set; }
        public string Reference { get; set; }
        public ShippingMethods ShippingMethod { get; set; }
        public ICollection<ItemResource> Items { get; set; }
        public double ItemsTotal { get; set; }
        public double ShippingTotal { get; set; }
        public double PurchaseTotal { get; set; }
        public DateTime Created { get; set; }

        public OrderResource()
        {
            Items = new Collection<ItemResource>();
        }
    }
}