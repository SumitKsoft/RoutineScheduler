﻿using AutoMapper;
using RMSAPI.Controllers.DTO;
using RMSAPI.Data.Entities;

namespace RMSAPI.Helper;

public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<RegisterDTO, AppUser>().
            ForMember(d => d.DateOfBirth, o => o.MapFrom(s => s.DateOfBirth.ToDateOnly("dd/MM/yyyy"))).ReverseMap();


        CreateMap<AppUser, UserDTO>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToList()))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.SingleOrDefault(p => p.IsMain).Url));
        CreateMap<UserDTO, AppUser>();

        //Adding depertment mapping 
        CreateMap<Depertment, DepertmentDTO>().ReverseMap();
        CreateMap<Depertment, DepertmentDataDTO>()
            .ForMember(d => d.TotalBatches, o => o.MapFrom(s => s.Batches.Count()))
            .ForMember(d => d.TotalTeachers, o => o.MapFrom(s => s.Teachers.Count()))
            .ForMember(d => d.TotalSubjects, o => o.MapFrom(s => s.Batches.Sum(b => b.BatchSubjects.Count())))
            .ForMember(d => d.TotalStudents, o => o.MapFrom(s => s.Batches.Sum(b => b.BatchStudents != null ? b.BatchStudents.Count() : 0)))
            .ReverseMap();
        CreateMap<Teacher, TeacherDataDTO>()
            .ForMember(d => d.DepertmentName, o => o.MapFrom(s => s.Department.Name))
            .ForMember(d => d.DepertmentID, o => o.MapFrom(s => s.Department.Id))
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.AppUser.FirstName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.AppUser.LastName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.AppUser.Email))
            .ForMember(d => d.Subjects, o => o.MapFrom(s => s.TeacherSubjects.Select(s => s.Subject.Name)));
        
    }
}
