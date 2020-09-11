using Business.Interfaces;
using Business.Interfaces.Repository;
using Business.Interfaces.Service;
using Business.Model;
using Business.Model.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository,
                              INotification notification) : base(notification)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            var productExists = _productRepository.Get(p => p.BarCode == product.BarCode && p.Active).Result.FirstOrDefault();

            if(productExists != null)
            {
                productExists.Quantity += product.Quantity;
                productExists.Description = product.Description;
                productExists.NoteDescription = product.NoteDescription;
                productExists.Value = product.Value;

                await _productRepository.Update(productExists);
            }
            else
                await _productRepository.Add(product);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }

        public async Task<IEnumerable<Product>> Get(Expression<Func<Product, bool>> predicate)
        {
            return await _productRepository.Get(predicate);
        }

        public async Task<List<Product>> GetAll()
        {
            return _productRepository.Get(p => p.Active).Result.ToList();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Update(product);
        }
    }
}