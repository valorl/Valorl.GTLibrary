using System;
using System.Collections.Generic;
using System.Text;
using GenFu;
using Moq;
using Valorl.GTLibrary.DataAccess.Interfaces;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.Models.Enums;

namespace Valorl.GTLibrary.UnitTests.Utils
{
    public static class MockRepositories
    {
        public static class Acquirements
        {
            public static Guid AllGoodAcquirementId => new Guid("ea6d2a6b-01dc-4341-a7d7-2148da31988e");
            public static Guid AcquirementIdWithMissingItem => new Guid("1273f66d-1011-4dea-96f0-c7a9ba655666");
            public static Guid AcquirementIdWithMissingCopy => new Guid("26bf9877-2b35-4cd5-b3f3-75150105d328");
            public static Guid AcquirementIdWithMissingSender => new Guid("b7eb19c6-5af7-491b-9169-0fdcbd0c9ef2");
            public static Guid AcquirementIdWithMissingReceiver => new Guid("797e8b7b-0747-4e29-96cd-92e2b5dcb760");
            public static Guid ANonExistingAcquirement => new Guid("50c9aff6-5afb-4011-824e-28164ec0e26c");

            public static Mock<IAcquirementRepository> GetRepository()
            {
                var mock = new Mock<IAcquirementRepository>();
                mock.Setup(m => m.GetOne(AllGoodAcquirementId))
                    .ReturnsAsync(() => new DbAcquirement()
                    {
                        Id = AllGoodAcquirementId,
                        AcquirementDateUtc = new DateTime(2017, 05, 03, 0, 0, 0, DateTimeKind.Utc),
                        ItemIsbn = Items.AnExistingIsbn,
                        CopyNumbers = new[] {ItemCopies.AnAvailableCopyNr},
                        Status = EDbAcquirementStatus.Created,
                        GivingLibraryId = Libraries.ThisLibraryId,
                        ReceivingLibraryId = Libraries.AnExistingLibraryId
                    });
                mock.Setup(m => m.GetOne(AcquirementIdWithMissingItem))
                    .ReturnsAsync(() => new DbAcquirement()
                    {
                        Id = AcquirementIdWithMissingItem,
                        AcquirementDateUtc = new DateTime(2017, 05, 03, 0, 0, 0, DateTimeKind.Utc),
                        ItemIsbn = Items.ANonExistingIsbn,
                        CopyNumbers = new[] {ItemCopies.AnAvailableCopyNr},
                        Status = EDbAcquirementStatus.Created,
                        GivingLibraryId = Libraries.ThisLibraryId,
                        ReceivingLibraryId = Libraries.AnExistingLibraryId
                    });
                mock.Setup(m => m.GetOne(AcquirementIdWithMissingCopy))
                    .ReturnsAsync(() => new DbAcquirement()
                    {
                        Id = AcquirementIdWithMissingItem,
                        AcquirementDateUtc = new DateTime(2017, 05, 03, 0, 0, 0, DateTimeKind.Utc),
                        ItemIsbn = Items.AnExistingIsbn,
                        CopyNumbers = new[] {ItemCopies.ANonExistingCopyNr},
                        Status = EDbAcquirementStatus.Created,
                        GivingLibraryId = Libraries.ThisLibraryId,
                        ReceivingLibraryId = Libraries.AnExistingLibraryId
                    });
                mock.Setup(m => m.GetOne(AcquirementIdWithMissingSender))
                    .ReturnsAsync(() => new DbAcquirement()
                    {
                        Id = AcquirementIdWithMissingItem,
                        AcquirementDateUtc = new DateTime(2017, 05, 03, 0, 0, 0, DateTimeKind.Utc),
                        ItemIsbn = Items.AnExistingIsbn,
                        CopyNumbers = new[] { ItemCopies.AnAvailableCopyNr },
                        Status = EDbAcquirementStatus.Created,
                        GivingLibraryId = Libraries.ANonExistingLibraryId,
                        ReceivingLibraryId = Libraries.AnExistingLibraryId
                    });
                mock.Setup(m => m.GetOne(AcquirementIdWithMissingReceiver))
                    .ReturnsAsync(() => new DbAcquirement()
                    {
                        Id = AcquirementIdWithMissingItem,
                        AcquirementDateUtc = new DateTime(2017, 05, 03, 0, 0, 0, DateTimeKind.Utc),
                        ItemIsbn = Items.AnExistingIsbn,
                        CopyNumbers = new[] { ItemCopies.AnAvailableCopyNr },
                        Status = EDbAcquirementStatus.Created,
                        GivingLibraryId = Libraries.AnExistingLibraryId,
                        ReceivingLibraryId = Libraries.ANonExistingLibraryId
                    });
                return mock;
            }
        }

        public static class Items
        {
            public static string AnExistingIsbn => "9781613776056";
            public static string ANonExistingIsbn => "12345670123";

            public static Mock<IItemRepository> GetRepository()
            {
                var mock = new Mock<IItemRepository>();
                mock.Setup(m => m.GetOneByIsbn(AnExistingIsbn))
                    .ReturnsAsync(() => new DbItem()
                    {
                        ISBN = AnExistingIsbn,
                        Author = "Katie Cook",
                        Title = "My Little Pony: Friendship is Magic Volume 1",
                        IsLendable = true,
                        SubjectArea = A.New<DbSubjectArea>()
                    });

                return mock;
            }
        }

        public static class ItemCopies
        {
            public static int AnAvailableCopyNr => 1;
            public static int AnUnavailableCopyNr => 2;
            public static int ANonExistingCopyNr => 0;

            public static Mock<IItemCopyRepository> GetRepository()
            {
                var mock = new Mock<IItemCopyRepository>();
                mock.Setup(m => m.GetOne(1, Items.AnExistingIsbn))
                    .ReturnsAsync(() => new DbItemCopy()
                    {
                        Number = 1,
                        ISBN = Items.AnExistingIsbn,
                        IsAvailable = true,
                        Type = EDbItemCopyType.Normal
                    });
                mock.Setup(m => m.GetOne(2, Items.AnExistingIsbn))
                    .ReturnsAsync(() => new DbItemCopy()
                    {
                        Number = 2,
                        ISBN = Items.AnExistingIsbn,
                        IsAvailable = false,
                        Type = EDbItemCopyType.Normal
                    });
                return mock;
            }
        }

        public static class Libraries
        {
            public static Guid AnExistingLibraryId => new Guid("18344ac0-24fd-438f-ad65-37dd15b11c5c");
            public static Guid ANonExistingLibraryId => new Guid("35b5aa07-36b8-432d-bb9c-1a18dcf49ea5");
            public static Guid ThisLibraryId => new Guid("84d0bbc8-ba5d-43b7-bf1d-3e9153726747");

            public static Mock<ILibraryRepository> GetRepository()
            {
                var mock = new Mock<ILibraryRepository>();
                mock.Setup(m => m.GetOne(AnExistingLibraryId))
                    .ReturnsAsync(() => new DbLibrary()
                    {
                        Id = AnExistingLibraryId,
                        Name = "AnExistingLibrary",
                        Address = A.New<DbAddress>()
                    });
                mock.Setup(m => m.GetOne(ThisLibraryId))
                    .ReturnsAsync(() => new DbLibrary()
                    {
                        Id = ThisLibraryId,
                        Name = "Valer's Library",
                        Address = A.New<DbAddress>()
                    });
                return mock;
            }
        }



    }
}
