using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMigrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets("bed0fc87-47c8-44ae-8d72-b372a19f649b")
                .Build();

            IRepository source = new Repository(configuration["remote"]);
            IRepository dest = new Repository(configuration["local"]);

            Console.Write("Deleting posts...");
            await dest.DeletePostsAsync();
            Console.WriteLine("done.");

            Console.Write("Deleting authors...");
            await dest.DeleteAuthorsAsync();
            Console.WriteLine("done.");

            Console.Write("Retrieving authors...");
            var authors = await source.GetAuthorsAsync();
            Console.WriteLine("done.");

            Console.Write("Inserting authors...");
            await dest.InsertAuthorsAsync(authors);
            Console.WriteLine("done.");

            Console.Write("Retrieving posts...");
            IEnumerable<Post> posts = await source.GetPostsAsync();
            Console.WriteLine("done.");

            Console.Write("Inserting posts...");
            await dest.InsertPostsAsync(posts);
            Console.WriteLine("done...");
        }
    }

    public interface IRepository
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task DeleteAuthorsAsync();
        Task InsertAuthorsAsync(IEnumerable<Author> authors);
        Task DeletePostsAsync();
        Task<IEnumerable<Post>> GetPostsAsync();
        Task InsertPostsAsync(IEnumerable<Post> posts);
    }

    public class Repository : IRepository
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            var result = new List<Author>();
            var sql = "select authorid, firstname, lastname, nickname from author";
            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new Author
                {
                    AuthorId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    NickName = reader.GetString(3)
                });
            }

            return result;
        }

        public async Task DeleteAuthorsAsync()
        {
            var sql = "delete from author";
            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

        }

        public async Task InsertAuthorsAsync(IEnumerable<Author> authors)
        {
            var sql = "set identity_insert author on;insert into author(authorid,firstname,lastname,nickname)values(@authorid,@firstname,@lastname,@nickname);";
            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            foreach (var author in authors)
            {
                cmd.Parameters.AddWithValue("@authorid", author.AuthorId);
                cmd.Parameters.AddWithValue("@firstname", author.FirstName);
                cmd.Parameters.AddWithValue("@lastname", author.LastName);
                cmd.Parameters.AddWithValue("@nickname",  author.NickName);
                await cmd.ExecuteNonQueryAsync();
                cmd.Parameters.Clear();
            }
        }

        public async Task DeletePostsAsync()
        {
            var sql = "delete from post";
            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            var result = new List<Post>();
            var sql = "select postid, authorid, title, content, datepublished from post";
            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new Post
                {
                    PostId = reader.GetGuid(0),
                    AuthorId = reader.GetInt32(1),
                    Title = reader.GetString(2),
                    Content = reader.GetString(3),
                    DatePublished = reader.GetFieldValue<DateTime?>(4)
                });
            }

            return result;
        }

        public async Task InsertPostsAsync(IEnumerable<Post> posts)
        {
            var sql = "insert into post(postid,authorid,title,content,datepublished)values(@postid,@authorid,@title,@content,@datepublished)";
            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            foreach (var post in posts)
            {
                cmd.Parameters.AddWithValue("@postid", post.PostId);
                cmd.Parameters.AddWithValue("@authorid", post.AuthorId);
                cmd.Parameters.AddWithValue("@title", post.Title);
                cmd.Parameters.AddWithValue("@content", post.Content);
                cmd.Parameters.AddWithValue("@datepublished", post.DatePublished);
                await cmd.ExecuteNonQueryAsync();
                cmd.Parameters.Clear();
            }
        }
    }

    
}
