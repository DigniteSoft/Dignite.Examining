using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.Examining.Questions
{
    public class SampleAppService_Tests : ExaminingApplicationTestBase
    {
        private readonly IQuestionAppService _sampleAppService;

        public SampleAppService_Tests()
        {
            _sampleAppService = GetRequiredService<IQuestionAppService>();
        }

        /*
        [Fact]
        public async Task GetAsync()
        {
            var result = await _sampleAppService.GetAsync();
            result.Value.ShouldBe(42);
        }

        [Fact]
        public async Task GetAuthorizedAsync()
        {
            var result = await _sampleAppService.GetAuthorizedAsync();
            result.Value.ShouldBe(42);
        }
        */
    }
}
