using AutoMapper;
using BlogApplication.DTOs;
using BlogApplication.Models;

namespace BlogApplication
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostSummaryDTO>();
            CreateMap<Post, PostDetailsDTO>();
            CreateMap<PostRequestDTO, Post>();
            CreateMap<Post, PostRequestDTO>();

            CreateMap<Comment, CommentDetailsDTO>();
            CreateMap<CommentRequestDTO, Comment>();

            CreateMap<User, UserDetailsDTO>();
            CreateMap<UserRegistrationDTO, User>();

            CreateMap<Tag, Tag>();
        }
    }

}
