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
    [Route("v1/libraries")]
    public class LibraryController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public LibraryController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> PostLibrary([FromBody] LibraryDto dto)
        {
            var dbLibrary = dto.ToDbModel();

            var created = await _libraryRepository.Create(Guid.NewGuid(), dbLibrary.Name, dbLibrary.Address);

            var createdDto = created.ToDto();
            return Created($"/{createdDto.Id}", createdDto);
        }
    }
}
