using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Questions
{
    public class GetQuestionsInput: PagedResultRequestDto
    {
        public Guid LiraryId { get; set; }
    }
}
