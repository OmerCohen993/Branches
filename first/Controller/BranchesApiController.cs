using Microsoft.AspNetCore.Mvc;
using first.Repository;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using first.View_Models;

namespace first.Controller
{
    public class BranchApiController : UmbracoApiController
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IUmbracoMapper _mapper;

        public BranchApiController(IBranchRepository branchRepository, IUmbracoMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        [HttpGet("api/branches")]
        public IActionResult GetBranches()
        {
            var branches = _branchRepository.GetBranches();
            if (branches == null)
            {
                return NotFound();
            }

            var MappedBranch = _mapper.MapEnumerable<Branch, BranchApiResponseItem>(branches);
            return Ok(MappedBranch);
        }

        [HttpGet("api/branch")]
        public IActionResult GetBranche([FromQuery] int Id)
        {
            var branch = _branchRepository.GetBranch(Id);
            if (branch == null)
            {
                return NotFound();
            }

            var MappedBranch = _mapper.Map<Branch, BranchApiResponseItem>(branch);

            return Ok(MappedBranch);
        }

        [HttpPost("api/branch")]
        public IActionResult CreateBranch([FromBody] BranchCreateItem request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            var branch = _branchRepository.CreateBranch(request);

            if (branch == null)
            {
                return BadRequest("Invalid request");
            }

            return Ok(_mapper.Map<Branch, BranchApiResponseItem>(branch));
        }

        [HttpPut("api/branch")]
        public IActionResult UpdateBranch([FromQuery] int Id, [FromBody] BranchUpdateItem request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            if (_branchRepository.GetBranch(Id) == null)
            {
                return NotFound();
            }

            var branch = _branchRepository.UpdateBranch(Id, request);

            if (branch == null)
            {
                return BadRequest("Invalid request");
            }

            return Ok(_mapper.Map<Branch, BranchApiResponseItem>(branch));
        }

        [HttpDelete("api/branch")]
        public IActionResult DeleteBranch([FromQuery] int Id)
        {
            if (_branchRepository.GetBranch(Id) == null)
            {
                return NotFound();
            }

            var result = _branchRepository.DeleteBranch(Id);
            if (result)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, $"Could not delete branch by Id {Id}");
        }

    }
}