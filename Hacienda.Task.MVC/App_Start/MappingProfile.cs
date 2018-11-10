using AutoMapper;
using Hacienda.Task.DAL;
using Hacienda.Task.MVC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hacienda.Task.MVC.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Person, PersonDTO>();
            Mapper.CreateMap<PersonDTO, Person>();
        }
    }
}