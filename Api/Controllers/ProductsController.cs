using Api.ViewModel;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Service;
using Business.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/products")]
    public class ProductsController : MainController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService,
                                  IMapper mapper,
                                  INotification notification) : base(notification)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductViewModel>>> Get()
        {
            var response = _mapper.Map<List<ProductViewModel>>(await _productService.GetAll());
            return CustomResponse(response);
        }

        [HttpGet("getByBarCode")]
        public async Task<ActionResult<ProductViewModel>> GetByBarCode([FromQuery] string barCode)
        {
            var result = await _productService.Get(p => p.BarCode == barCode && p.Active);

            if (result == null || result.Count() == 0)
                return BadRequest();

            var response = _mapper.Map<ProductViewModel>(result.FirstOrDefault());
            return CustomResponse(response);
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Post([FromBody] ProductViewModel productViewModel)
        {
            productViewModel.Active = true;
            var product = _mapper.Map<Product>(productViewModel);

            await _productService.Add(product);

            productViewModel.Id = product.Id;
            productViewModel.DateRegister = product.DateRegister;
            productViewModel.Quantity = product.Quantity;
            productViewModel.Value = product.Value;
            productViewModel.NoteDescription = product.NoteDescription;

            return CustomResponse(productViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Put(Guid id, [FromBody] ProductViewModel product)
        {
            if (product.Id != id)
            {
                NotificationError("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(product);
            }

            await _productService.Update(_mapper.Map<Product>(product));
            return CustomResponse(product);
        }
    }
}
