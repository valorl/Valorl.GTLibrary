using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using LightBDD.XUnit2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Valorl.GTLibrary.Api;
using Valorl.GTLibrary.DTOs;
using Valorl.GTLibrary.DTOs.Enums;
using Valorl.GTLibrary.Models;
using Valorl.GTLibrary.SystemTests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace Valorl.GTLibrary.SystemTests.Acceptance
{
    public partial class ItemAcquirements : FeatureFixture, IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly DatabaseTestHelper _dbTestHelper;

        private ItemDto _itemDto;
        private ItemCopyDto _itemCopyDto;
        private LibraryDto _libraryDto;

        private HttpResponseMessage _response;

        private AcquirementDto _acquirement;

        public ItemAcquirements(ITestOutputHelper output) : base(output)
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            _dbTestHelper = new DatabaseTestHelper();
        }

        private async void Given_the_item_exists()
        {
            _itemDto = new ItemDto()
            {
                ISBN = "9781613776056",
                Author = "TestAuthor",
                Title = "TestTitle",
                IsLendable = true,
                SubjectArea = new SubjectAreaDto()
                {
                    Id = 4,
                    Name = "TestArea"
                }
            };

            var itemDtoJson = JsonConvert.SerializeObject(_itemDto, new StringEnumConverter());
            var response = await _client.PostAsync("/v1/items", new StringContent(itemDtoJson, Encoding.UTF8, "application/json"));
        }

        private async void And_the_item_has_an_available_copy()
        {
            _itemCopyDto = new ItemCopyDto()
            {
                ISBN = _itemDto.ISBN,
                Number = 1,
                Type = EItemCopyTypeDto.Normal,
                IsAvailable = true
            };

            var itemCopyDtoJson = JsonConvert.SerializeObject(_itemCopyDto, new StringEnumConverter());
            var response = await _client.PostAsync("/v1/items/9781613776056/copies", new StringContent(itemCopyDtoJson, Encoding.UTF8, "application/json"));
        }

        private async void And_the_library_exists()
        {
            var addressDto = new AddressDto()
            {
                Type = EAddressTypeDto.Library,
                Street = "TestStreet",
                Number = "123",
                City = "TestCity",
                ZipCode = "12345",
                Country = "TestCountry"
            };

            var libraryDto = new LibraryDto()
            {
                Name = "TestLibrary",
                Address = addressDto
            };

            var libDtoJson = JsonConvert.SerializeObject(libraryDto, new StringEnumConverter());
            var response = await _client.PostAsync("/v1/libraries", new StringContent(libDtoJson, Encoding.UTF8, "application/json"));
            var createdDto = JsonConvert.DeserializeObject<LibraryDto>(await response.Content.ReadAsStringAsync());

            _libraryDto = createdDto;
        }

        private async void When_new_acquirement_is_submitted()
        {
            var dto = new NewAcquirementDto()
            {
                ItemIsbn = _itemDto.ISBN,
                ItemCopyNumbers = new [] { _itemCopyDto.Number },
                LibraryId = _libraryDto.Id
            };

            var dtoJson = JsonConvert.SerializeObject(dto, new StringEnumConverter());
            _response = await _client.PostAsync("/v1/acquirements", new StringContent(dtoJson, Encoding.UTF8, "application/json"));
        }

        private void Then_response_should_be_successful()
        {
            _response.EnsureSuccessStatusCode();
        }

        private async void And_created_acquirement_should_have_correct_values()
        {
            _acquirement =
                JsonConvert.DeserializeObject<AcquirementDto>(await _response.Content.ReadAsStringAsync());

            Assert.Equal(_itemDto.ISBN, _acquirement.Item.ISBN);
            Assert.Equal(_itemCopyDto.Number, _acquirement.ItemCopies.Single().Number);
            Assert.Equal(_libraryDto.Id, _acquirement.Receiver.Id);
        }

        private async void And_copy_should_no_longer_be_available()
        {
            var copyNumber = _acquirement.ItemCopies.Single().Number;
            var copyResponse = await _client.GetAsync($"/v1/items/{_itemDto.ISBN}/copies/{copyNumber}");
            var copy = JsonConvert.DeserializeObject<ItemCopyDto>(await copyResponse.Content.ReadAsStringAsync());

            Assert.False(copy.IsAvailable);
        }

        public void Dispose()
        {
            if (_acquirement != null) _dbTestHelper.DeleteAcquirement(_acquirement.Id).Wait();
            if (_itemDto != null)  _dbTestHelper.DeleteItem(_itemDto.ISBN).Wait();
            if (_libraryDto != null) _dbTestHelper.DeleteLibrary(_libraryDto.Id).Wait();
            if (_libraryDto?.Address != null) _dbTestHelper.DeleteAddress(_libraryDto.Address.Id).Wait();
        }
    }
}

