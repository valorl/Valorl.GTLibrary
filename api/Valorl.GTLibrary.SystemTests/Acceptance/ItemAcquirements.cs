using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Basic;
using LightBDD.XUnit2;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;

[assembly: LightBddScope]
namespace Valorl.GTLibrary.SystemTests.Acceptance
{
    [FeatureDescription(@"A partnered library should be able to register an acquirement of one or more copies of an item")]
    [Label("GTL-25")]
    public partial class ItemAcquirements
    {

        [Scenario]
        [Label("TC1")]
        public async Task Successful_Acquirement()
        {
            await Runner.RunScenarioActionsAsync(
                Given_the_item_exists,
                And_the_item_copies_are_available,
                And_the_library_exists,
                When_new_acquirement_is_submitted,
                Then_response_should_be_successful,
                And_created_acquirement_should_have_correct_values,
                And_copy_should_no_longer_be_available
            );
        }

        [Scenario]
        [Label("TC2")]
        public async Task MissingItem_Acquirement()
        {
            await Runner.RunScenarioActionsAsync(
                Given_the_item_is_missing,
                When_new_acquirement_is_submitted,
                Then_response_should_be_BadRequest
            );
        }


        [Scenario]
        [Label("TC3")]
        public async Task UnavailableCopy_Acquirement()
        {
            await Runner.RunScenarioActionsAsync(
                Given_the_item_exists,
                And_an_item_copy_is_not_avaiable,
                When_new_acquirement_is_submitted,
                Then_response_should_be_BadRequest
            );
        }

        [Scenario]
        [Label("TC4")]
        public async Task InvalidCopy_Acquirement()
        {
            await Runner.RunScenarioActionsAsync(
                Given_the_item_exists,
                And_an_item_copy_is_invalid,
                When_new_acquirement_is_submitted,
                Then_response_should_be_BadRequest
            );
        }

        [Scenario]
        [Label("TC5")]
        public async Task InvalidLibrary_Acquirement()
        {
            await Runner.RunScenarioActionsAsync(
                Given_the_item_exists,
                And_the_item_copies_are_available,
                And_the_library_does_not_exist,
                When_new_acquirement_is_submitted,
                Then_response_should_be_BadRequest
            );
        }
    }
}
