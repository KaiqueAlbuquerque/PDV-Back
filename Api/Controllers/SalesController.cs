using Api.ViewModel;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Service;
using Business.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/sales")]
    public class SalesController : MainController
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SalesController(ISaleService saleService,
                               IMapper mapper,
                               INotification notification) : base(notification)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<SaleViewModel>> Post([FromBody] SaleViewModel saleViewModel)
        {
            var sale = _mapper.Map<Sale>(saleViewModel);

            await _saleService.Add(sale);

            saleViewModel.Id = sale.Id;
            saleViewModel.DateRegister = sale.DateRegister;

            return CustomResponse(saleViewModel);
        }
    }
}
