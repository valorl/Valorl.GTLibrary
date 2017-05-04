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
        public async Task Successful_Acquirement()
        {
            await Runner.RunScenarioActionsAsync(
                Given_the_item_exists,
                And_the_item_has_an_available_copy,
                And_the_library_exists,
                When_new_acquirement_is_submitted,
                Then_response_should_be_successful,
                And_created_acquirement_should_have_correct_values,
                And_copy_should_no_longer_be_available);
        }
    }
}
