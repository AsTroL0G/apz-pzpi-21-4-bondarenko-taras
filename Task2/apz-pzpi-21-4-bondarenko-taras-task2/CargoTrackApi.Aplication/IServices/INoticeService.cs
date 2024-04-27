using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IServices
{
    public interface INoticeService
    {
        Task<NoticeDto> AddNoticeAsync(NoticeCreateDto noticeCreateDto, CancellationToken cancellationToken);

        Task<NoticeDto> DeleteNotice(string noticeId, CancellationToken cancellationToken);

        Task<NoticeDto> UpdateNotice(NoticeDto noticeDto, CancellationToken cancellationToken);

        Task<NoticeDto> GetNotice(string noticeId, CancellationToken cancellationToken);
    }
}
