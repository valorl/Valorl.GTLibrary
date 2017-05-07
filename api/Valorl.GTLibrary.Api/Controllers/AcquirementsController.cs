using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Valorl.GTLibrary.Api.Filters;
using Valorl.GTLibrary.Api.Mappings;
using Valorl.GTLibrary.Api.Utils;
using Valorl.GTLibrary.DataAccess.Interfaces;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.DTOs.Enums;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.Api.Controllers
{
    public class AcquirementsController : Controller
    {
        private readonly IAcquirementRepository _acquirementRepository;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IItemCopyRepository _itemCopyRepository;

        public AcquirementsController(IAcquirementRepository acquirementRepository, ILibraryRepository libraryRepository, 
                                      IItemRepository itemRepository, IItemCopyRepository itemCopyRepository)
        {
            _acquirementRepository = acquirementRepository;
            _libraryRepository = libraryRepository;
            _itemRepository = itemRepository;
            _itemCopyRepository = itemCopyRepository;
        }

        [Route("integration/acquirements")]
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> PostAcquirement([FromBody] NewAcquirementDto dto)
        {
            var item = await _itemRepository.GetOneByIsbn(dto.ItemIsbn);
            if (item == null)
            {
                return BadRequest($"Item (ISBN: {dto.ItemIsbn}) not found.");
            }

            var copies = new List<DbItemCopy>();
            foreach (var copyNr in dto.ItemCopyNumbers)
            {
                var copy = await _itemCopyRepository.GetOne(copyNr, item.ISBN);
                if (copy == null)
                {
                    return BadRequest($"Copy (ISBN: {item.ISBN}, Number: {copyNr}) not found.");
                }
                if (!copy.IsAvailable)
                {
                    return BadRequest($"Copy (ISBN: {item.ISBN}, Number: {copyNr}) is not available right now.");
                }
                copies.Add(copy);
            }

            var receiverLibrary = await _libraryRepository.GetOne(dto.LibraryId);
            if (receiverLibrary == null)
            {
                return BadRequest($"Library (Id: {dto.LibraryId}) not found.");
            }

            var senderLibrary = await _libraryRepository.GetOne(LibraryInfo.Id);

            var acquirementId = Guid.NewGuid();
            var date = DateTime.UtcNow;
            var status = EDbAcquirementStatus.Created;

            await _acquirementRepository.Create(
                acquirementId, 
                date,
                status, 
                dto.ItemIsbn,
                dto.ItemCopyNumbers, 
                dto.LibraryId, 
                LibraryInfo.Id);


            var resultDto =  new AcquirementDto()
            {
                Id = acquirementId,
                DateUtc = date,
                Status = (EAcquirementStatusDto)(int)status,
                Item = item.ToDto(),
                Receiver = receiverLibrary.ToDto(),
                Sender = senderLibrary.ToDto(),
                ItemCopies = copies.Select(x => x.ToDto())
            };

            return Created($"/{acquirementId}", resultDto);
        }

        [Route("acquirements/{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> GetAcquirement(Guid id)
        {
            var dbAcquirement = await _acquirementRepository.GetOne(id);
            if (dbAcquirement == null)
            {
                return NotFound();
            }

            var dbItem = await _itemRepository.GetOneByIsbn(dbAcquirement.ItemIsbn);
            if (dbItem == null)
            {
                return InternalServerError($"The acquired item (ISBN: {dbAcquirement.ItemIsbn}) is missing.");
            }

            var copyTasks = dbAcquirement.CopyNumbers
                .Select(nr => _itemCopyRepository.GetOne(nr, dbAcquirement.ItemIsbn))
                .ToArray();
            var dbCopies  = await Task.WhenAll(copyTasks);

            if (dbCopies.Any(c => c == null))
            {
                return InternalServerError($"One or more acquired item copies are missing in the system");
            }

            var dbReceiving = await _libraryRepository.GetOne(dbAcquirement.ReceivingLibraryId);
            if (dbReceiving == null)
            {
                return InternalServerError($"The receiver library is missing in the system.");
            }

            var dbGiving = await _libraryRepository.GetOne(dbAcquirement.GivingLibraryId);
            if (dbGiving == null)
            {
                return InternalServerError($"The sender library is missing in the system");
            }

            var dto = dbAcquirement.ToDto(dbItem, dbCopies, dbReceiving, dbGiving);
            return Ok(dto);
        }


        private IActionResult InternalServerError(string message = null)
        {
            const int code = StatusCodes.Status500InternalServerError;
            if (message != null) return StatusCode(code, message);
            return StatusCode(code);
        }

    }
}
