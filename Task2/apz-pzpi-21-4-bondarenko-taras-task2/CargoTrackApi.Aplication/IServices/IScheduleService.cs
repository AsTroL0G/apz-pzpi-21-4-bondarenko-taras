using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Aplication.IServices
{
    public interface IScheduleService
    {
        Task<ScheduleDto> AddScheduleAsync(ScheduleCreateDto scheduleCreateDto, CancellationToken cancellationToken);

        Task<ScheduleDto> DeleteSchedule(string scheduleId, CancellationToken cancellationToken);

        Task<ScheduleDto> UpdateSchedule(ScheduleDto scheduleDto, CancellationToken cancellationToken);

        Task<ScheduleDto> GetSchedule(string scheduleId, CancellationToken cancellationToken);

        Task <List<ScheduleDto>> GetScheduleByTripId(string scheduleDto, CancellationToken cancellationToken);

    }
}
