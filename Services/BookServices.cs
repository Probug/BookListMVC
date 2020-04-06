using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using BookListMVC.Models;

namespace BookListMVC.Services
{
    public class BookService
    {
    
        private readonly IMongoCollection<Book> books;

           public BookService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("BookListMVCDb"));
            IMongoDatabase database = client.GetDatabase("BookListMVCDb");
            books = database.GetCollection<Book>("Books");
        }

         public List<Book> Get() =>

            books.Find(book => true).ToList();

        public List<Book> FindByTitle(string title) =>

            books.Find(book => book.Name.ToLower().Contains(title.ToLower())).ToList();

        public Book Get(string id) =>
            books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            books.InsertOne(book);
            return book;
        }



        public void Update(string id, Book bookIn) =>
            books.ReplaceOne(book => book.Id == id, bookIn);



        public void Remove(Book bookIn) =>
            books.DeleteOne(book => book.Id == bookIn.Id);


        public void Remove(string id) =>
            books.DeleteOne(book => book.Id == id);

    }
}