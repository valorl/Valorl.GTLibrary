using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Valorl.GTLibrary.Api.Mappings;
using Valorl.GTLibrary.DataAccess.Interfaces;

namespace Valorl.GTLibrary.Api.Controllers
{
    [Route("v1/items")]
    public class ItemsController : Controller
    {
        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var dbItems = await _itemRepository.GetAll();
            var dtoItems = dbItems.Select(x => x.ToDto());
            return Ok(dtoItems);
        }
    }
}
