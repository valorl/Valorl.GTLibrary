using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.Models;

namespace Valorl.GTLibrary.Api.Mappings
{
    public static class ItemMapping
    {
        public static ItemDto ToDto(this DbItem dbItem)
        {
            var dtoItem = new ItemDto()
            {
                ISBN = dbItem.ISBN,
                Author = dbItem.Author,
                Title = dbItem.Title,
                Description = dbItem.Description,
                IsLendable = dbItem.IsLendable,
                SubjectArea = new SubjectAreaDto()
                {
                    Id = dbItem.SubjectArea.Id,
                    Name = dbItem.SubjectArea.Name
                }
            };
            return dtoItem;
        }

        public static DbItem ToDb(this ItemDto itemDto)
        {
            var dbItem = new DbItem()
            {
                ISBN = itemDto.ISBN,
                Author = itemDto.Author,
                Title = itemDto.Title,
                Description = itemDto.Description,
                IsLendable = itemDto.IsLendable,
                SubjectArea = new DbSubjectArea()
                {
                    Id = itemDto.SubjectArea.Id,
                    Name = itemDto.SubjectArea.Name
                }
            };
            return dbItem;
        }
    }
}
