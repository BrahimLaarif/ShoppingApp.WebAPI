using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShoppingApp.WebAPI.Entities.Models;
using ShoppingApp.WebAPI.Entities.Resources;

namespace ShoppingApp.WebAPI.Common.Utilities
{
    public class UrlResolver : IValueResolver<Photo, PhotoResource, string>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(Photo source, PhotoResource destination, string destMember, ResolutionContext context)
        {
            var uriBuilder = new UriBuilder()
            {
                Path = source.FilePath,
                Port = httpContextAccessor.HttpContext.Connection.LocalPort
            };

            return uriBuilder.ToString();
        }
    }
}