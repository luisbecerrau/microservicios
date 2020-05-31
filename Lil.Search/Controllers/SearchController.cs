using Lil.Search.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lil.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ICustomersService customersService;
        private readonly IProductsService productsService;
        private readonly ISalesServices salesServices;

        public SearchController(ICustomersService customersService, IProductsService productsService, ISalesServices salesServices)
        {
            this.customersService = customersService;
            this.productsService = productsService;
            this.salesServices = salesServices;
        }

        [HttpGet("customers/{customerId}")]
        public  async Task<IActionResult> SearchAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest(); 
            }

            try
            {
                var customer = await customersService.GetAsync(customerId);

                var sales = await salesServices.GetAsync(customerId);

                foreach (var sale in sales)
                {
                    foreach (var item in sale.Items)
                    {
                        var product = await productsService.GetAsync(item.ProductId);
                        item.Product = product;
                    }
                }

                var result = new
                {
                    Customer = customer,
                    Sales = sales
                };

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }

            ///?
            throw new NotImplementedException();
        }
    }
}
