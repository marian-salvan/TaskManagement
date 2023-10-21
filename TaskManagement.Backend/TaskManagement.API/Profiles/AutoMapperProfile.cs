using AutoMapper;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Requests;
using TaskManagement.Core.Responses;

namespace TaskManagement.API.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUpdateUserRequest, UserEntity>()
                .ForMember(destination => destination.Id, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()))
                .ForMember(destination => destination.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<CreateUpdateTaskRequest, TaskEntity>()
                .ForMember(destination => destination.Id, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()))
                .ForMember(destination => destination.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UserEntity, GetUserResponse>();

            CreateMap<TaskEntity, GetTaskResponse>();
        }
    }
}
