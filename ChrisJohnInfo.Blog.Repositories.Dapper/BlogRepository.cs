using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;

namespace ChrisJohnInfo.Blog.Repositories.Dapper
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IDbConnection _connection;

        public BlogRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<PostViewModel>> GetPosts(bool publishedOnly)
        {
            var sql = GetBaseGetQuery();
            if (publishedOnly)
            {
                sql += "WHERE p.[DatePublished] IS NOT NULL ";
            }

            return await _connection.QueryAsync<PostViewModel>(sql);
        }

        public async Task<PostViewModel> GetPost(Guid postId)
        {
            var sql = GetBaseGetQuery();
            sql += "WHERE p.[PostId] = @PostId";
            return await _connection.QuerySingleAsync<PostViewModel>(sql, new {@PostId = postId});
        }

        private string GetBaseGetQuery()
        {
            return @"
SELECT
    p.[PostId],
    p.[Title],
    p.[Content],
    p.[DatePublished],
    p.[RenderedHtml],
    'AuthorName' = 
        CASE  
            WHEN a.[NickName] IS NULL THEN a.[FirstName] + ' ' + a.[LastName]
            ELSE a.[NickName]
        END
FROM 
    [Post] p
    INNER JOIN [Author] a ON p.[AuthorId] = a.[AuthorId]
";
        }
    }
}
