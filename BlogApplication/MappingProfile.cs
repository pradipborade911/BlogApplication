using AutoMapper;
using BlogApplication.DTOs;
using BlogApplication.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogApplication
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDetailsDTO>();
            CreateMap<PostRequestDTO, Post>();

            CreateMap<Comment, CommentDetailsDTO>();
            CreateMap<CommentRequestDTO, Comment>();

            CreateMap<User, UserDetailsDTO>();
            CreateMap<UserRegistrationDTO, User>();

            CreateMap<Tag, Tag>();
        }
    }

}
