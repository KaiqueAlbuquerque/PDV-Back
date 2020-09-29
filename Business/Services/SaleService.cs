using Business.Interfaces;
using Business.Interfaces.Repository;
using Business.Interfaces.Service;
using Business.Model;
using Business.Model.Validations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vip.Printer;
using Vip.Printer.Enums;

namespace Business.Services
{
    public class SaleService : BaseService, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IConfiguration _configuration;


        public SaleService(ISaleRepository saleRepository,
                           IProductRepository productRepository,
                           INotification notification,
                           IConfiguration configuration) : base(notification)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _configuration = configuration;
        }

        public async Task Add(Sale sale)
        {
            //if (!ExecuteValidation(new SaleValidation(), sale)) return;

            await _saleRepository.Add
            (
                new Sale()
                {
                    TotalValue = sale.TotalValue,
                    FormOfPayment = sale.FormOfPayment,
                    AmountPaid = sale.AmountPaid,
                    Change = sale.Change,
                    ProductSales = sale.ProductSales
                                    .GroupBy(ps => ps.ProductId)
                                    .Select(p => new ProductSale()
                                    {
                                        SaleId = p.First().SaleId,
                                        Sale = p.First().Sale,
                                        ProductId = p.First().ProductId,
                                        Product = p.First().Product,
                                        Total = p.Sum(x => x.Total),
                                        Quantity = p.Sum(x => x.Quantity)
                                    }).ToList()
                }    
            );

            foreach (var product in sale.ProductSales)
            {
                product.Product = await _productRepository.GetById(product.ProductId);
                product.Product.Quantity -= product.Quantity;
                await _productRepository.Update(product.Product);
            }

            try
            {
                string printerName = _configuration["Printer:PrinterName"];
                var printer = new Printer(printerName, PrinterType.Epson);

                sale.ProductSales = sale.ProductSales
                    .GroupBy(s => s.ProductId)
                    .Select(g => new ProductSale
                    {
                        ProductId = g.First().ProductId,
                        SaleId = g.First().SaleId,
                        Sale = g.First().Sale,
                        Product = g.First().Product,
                        Quantity = g.Sum(s => s.Quantity),
                        Total = g.Sum(s => s.Total)
                    }).ToList();

                sale.ProductSales = sale.ProductSales.OrderBy(c => c.Product.ExternalId.Length).ThenBy(c => c.Product.ExternalId).ToList();

                printer.PrintOut(sale);
                printer.PartialPaperCut();
                printer.PrintDocument();
                printer.PrintDocument();
            }
            catch (Exception ex)
            {
                
            }
        }

        public void Dispose()
        {
            _saleRepository?.Dispose();
        }

        public async Task<IEnumerable<Sale>> Get(Expression<Func<Sale, bool>> predicate)
        {
            return await _saleRepository.Get(predicate);
        }

        public async Task<List<Sale>> GetAll()
        {
            return await _saleRepository.GetAll();
        }

        public async Task<Sale> GetById(Guid id)
        {
            return await _saleRepository.GetById(id);
        }

        public async Task Update(Sale sale)
        {
            if (!ExecuteValidation(new SaleValidation(), sale)) return;

            await _saleRepository.Update(sale);
        }
    }
}
