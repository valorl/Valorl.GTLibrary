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
    [Route("v1/members")]
    public class MembersController : Controller
    {
        private readonly IMemberRepository _memberRepository;

        public MembersController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> PostMember([FromBody] MemberDto memberDto)
        {
            var member = memberDto.ToDbModel();
            var created = await _memberRepository.Create(member);
            return Created($"/{created.SSN}", created.ToDto());
        }

        [Route("{ssn}")]
        [HttpGet]
        public async Task<IActionResult> GetMember(string ssn)
        {
            var member = await _memberRepository.GetOneBySsn(ssn);
            var dto = member.ToDto();
            return Ok(dto);
        }
    }
}
