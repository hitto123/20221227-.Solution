using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Site.Models.DTOs
{
	public class ProductDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public CategoryDto Category { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public bool Status { get; set; }
		public string ProductImage { get; set; }
		public int Stock { get; set; }
	}
}