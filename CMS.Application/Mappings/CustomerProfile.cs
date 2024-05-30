using AutoMapper;
using CMS.Application.CQRS.Commands;
using CMS.Application.DTOs;
//using CMS.Core.DTOs;
using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<RegisterReqDto, RegisterResDto>().ReverseMap();
            CreateMap<RegisterReqDto, Customer>().ReverseMap();
            //CreateMap<Customer, CustomerDto>();
            //CreateMap<CustomerDto, Customer>();
            //CreateMap<Customer, CustomerDto>()
            //.ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            //.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            //.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            //.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)).ReverseMap();

        }
    }
}
