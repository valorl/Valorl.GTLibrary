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
        public static DtoItem ToDto(this DbItem dbItem)
        {
            var dtoItem = new DtoItem()
            {
                ISBN = dbItem.ISBN,
                Author = dbItem.Author,
                Title = dbItem.Title,
                Description = dbItem.Description,
                IsLendable = dbItem.IsLendable,
                SubjectArea = new DtoSubjectArea()
                {
                    Id = dbItem.SubjectArea.Id,
                    Name = dbItem.SubjectArea.Name
                }
            };
            return dtoItem;
        }

        public static DbItem ToDb(this DtoItem dtoItem)
        {
            var dbItem = new DbItem()
            {
                ISBN = dtoItem.ISBN,
                Author = dtoItem.Author,
                Title = dtoItem.Title,
                Description = dtoItem.Description,
                IsLendable = dtoItem.IsLendable,
                SubjectArea = new DbSubjectArea()
                {
                    Id = dtoItem.SubjectArea.Id,
                    Name = dtoItem.SubjectArea.Name
                }
            };
            return dbItem;
        }
    }
}
