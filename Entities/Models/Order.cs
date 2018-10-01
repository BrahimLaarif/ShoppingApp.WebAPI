using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Entities.Models
{
    public enum Status
    {
        Pending,
        AwaitingPayment,
        AwaitingShipment,
        Shipped,
        Delivered,
        Canceled,
        Declined,
    }

    public enum ShippingMethods
    {
        Poste,
        UPS,
        FedEX
    }

    public static class ShippingCost
    {
        public static readonly double Poste = 30;
        public static readonly double UPS = 60;
        public static readonly double FedEX = 70;

        public static double GetCost(Enum shippingMethod)
        {
            switch(shippingMethod)
            {
                case(ShippingMethods.Poste):
                    return Poste;
                case(ShippingMethods.UPS):
                    return UPS;
                case(ShippingMethods.FedEX):
                    return FedEX;
            }

            return 0;
        }
    }

    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public ShippingMethods ShippingMethod { get; set; }

        public ICollection<Item> Items { get; set; }
        
        public double ItemsTotal { get; set; }
        
        public double ShippingTotal { get; set; }

        public double PurchaseTotal { get; set; }

        public DateTime Created { get; set; }

        public Order()
        {
            Items = new Collection<Item>();
            Created = DateTime.Now;
        }
    }
}