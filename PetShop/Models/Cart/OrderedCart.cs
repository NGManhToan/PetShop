﻿namespace PetShop.Models.Cart
{
    public class OrderedCart
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get ; set; }
		public int IdProduct { get; set; }
	}
}
