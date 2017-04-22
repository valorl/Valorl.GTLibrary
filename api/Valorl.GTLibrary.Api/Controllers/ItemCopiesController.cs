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
    [Route("v1/items/{isbn}/copies")]
    public class ItemCopiesController : Controller
    {
        private readonly IItemCopyRepository _copyRepository;

        public ItemCopiesController(IItemCopyRepository copyRepository)
        {
            _copyRepository = copyRepository;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetCopies(string isbn)
        {
            var copies = await _copyRepository.GetMany(isbn);
            var dtos = copies.Select(x => x.ToDto());
            return Ok(dtos);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> PostCopy(string isbn, [FromBody] ItemCopyDto copyDto)
        {
            var copy = copyDto.ToDbModel();
            var created = await _copyRepository.Create(copy.Number, copy.ISBN, copy.IsAvailable, copy.Type);
            return Created($"/{copy.Number}", copyDto);
        }

        [Route("batch")]
        [HttpPost]
        public async Task<IActionResult> PostCopies(string isbn, [FromBody] ItemCopyDto[] copyDtos)
        {
            var copies = copyDtos.Select(x => x.ToDbModel());
            await _copyRepository.CreateMany(copies);
            return Ok();
        }

        [Route("{number}")]
        [HttpGet]
        public async Task<IActionResult> GetCopyByNumber(string isbn, int number)
        {
            var copy = await _copyRepository.GetOne(number, isbn);
            var dto = copy.ToDto();
            return Ok(dto);
        }


    }
}
