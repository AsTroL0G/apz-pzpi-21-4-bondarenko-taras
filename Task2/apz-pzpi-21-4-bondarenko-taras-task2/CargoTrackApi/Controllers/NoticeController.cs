using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{

    [Route("notice")]
    public class NoticeController : BaseController
    {

        private readonly INoticeService _noticeService;

        public NoticeController(INoticeService notice)
        {
            _noticeService = notice;
        }

        [HttpGet("get/{noticeId}")]
        public async Task<NoticeDto> GetNotice(string noticeId, CancellationToken cancellationToken)
        {
            return await _noticeService.GetNotice(noticeId, cancellationToken);
        }

        [HttpPost("create")]
        public async Task<ActionResult<NoticeDto>> AddNoticeAsync([FromBody] NoticeCreateDto noticeCreate, CancellationToken cancellationToken)
        {
            await _noticeService.AddNoticeAsync(noticeCreate, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<NoticeDto>> UpdateNotice([FromBody] NoticeDto noticeUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _noticeService.UpdateNotice(noticeUpdateDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{cityId}")]
        public async Task<ActionResult<NoticeDto>> DeleteNotice(string noticeId, CancellationToken cancellationToken)
        {
            var result = await _noticeService.DeleteNotice(noticeId, cancellationToken);
            return Ok(result);
        }
    }
}
