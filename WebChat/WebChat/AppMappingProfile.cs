using AutoMapper;
using System;
using WebChat.Models;

namespace WebChat
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<HistoryModel, MapHistoryModel>();
        }
    }
}
