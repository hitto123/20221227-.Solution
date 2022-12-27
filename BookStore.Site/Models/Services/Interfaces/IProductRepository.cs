﻿using BookStore.Site.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Site.Models.Services.Interfaces
{
	public interface IProductRepository
	{
		/// <summary>
		/// 篩選商品
		/// </summary>
		/// <param name="categoryId"></param>
		/// <param name="productName"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		IEnumerable<ProductDto> Search(int categoryId, string productName, bool? status);

		/// <summary>
		/// 傳回一筆商品
		/// </summary>
		/// <param name="productId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		ProductDto Load(int productId, bool? status);
	}
}
