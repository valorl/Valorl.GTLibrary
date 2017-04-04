using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Valorl.GTLibrary.Api.Mappings;
using Valorl.GTLibrary.DataAccess.Interfaces;
using Valorl.GTLibrary.DTOs;

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

        [HttpGet]
        [Route("{isbn}")]
        public async Task<IActionResult> GetItemByIsbn(string isbn)
        {
            var dbItem = await _itemRepository.GetOneByIsbn(isbn);
            if (dbItem == null)
            {
                return NotFound();
            }
            return Ok(dbItem.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> PostItem([FromBody] ItemDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbItem = itemDto.ToDb();
            var created = await _itemRepository.Create(dbItem);

            return Created($"/{itemDto.ISBN}", created.ToDto());
        }
    }
}
