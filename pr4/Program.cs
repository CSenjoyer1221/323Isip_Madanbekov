using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement
{
    // COMMIT: СОЗДАНИЕ ПЕРЕЧИСЛЕНИЯ ЖАНРОВ
    // Добавляем enum Genre с тремя вариантами жанров для книг
    public enum Genre
    { 
        Thriller,
        Detetive,
        Action
    }
    // Создаем класс Book, со всеми данными
    public class Book

    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Genre Genre { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }


        public Book(int id, string title, string author, Genre genre, int year, decimal price)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Price = price;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Название:\"{Title}\",Автор: {Author}, Жанр: {Genre}, Год: {Year}, Цена: {Price:C}";
        }
    }

    public class Library
    {
        private List<Book> books = new List<Book>();
        private int nextId = 1;

        public void AddBook(string title, string author, Genre genre, int year, decimal price)
        {
            var book = new Book(nextId++, title, author, genre, year, price);
            books.Add(book);
            Console.WriteLine($"\nКнига добавлена: {book}");
        }

        public void RemoveBook(int id)
        {
            var book = FindBookById(id);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine($"\nКнига c ID {id} удалена");
            }
            else
            {
                Console.WriteLine($"\nКнига с ID {id} не найдена");
            }
        }
        private Book FindBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }

        public void FindBookByTitle(string title)
        {
            var foundBooks = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            DisplaySearchResults(foundBooks, $"по названию \"{title}\"");
        }

        public void FindBooksByAuthor(string author)
        {
            var foundBooks = books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
            DisplaySearchResults(foundBooks, $"по автору \"{author}\"");
        }

        public void FindBooksByGenre(Genre genre)
        {
            var foundBooks = books.Where(b => b.Genre == genre).ToList();
            DisplaySearchResults(foundBooks, $"по жанру \"{genre}\" ");
        }

        private void DisplaySearchResults(List<Book> foundBooks, string searchCriteria)
        {
            if (foundBooks.Any())
            {
                Console.WriteLine($"\nНайдено книг {searchCriteria}: {foundBooks.Count}");
                foreach (var book in foundBooks)
                {
                    Console.WriteLine(book);
                }
            }
            else
            {
                Console.WriteLine($"\nКниги {searchCriteria} не найдены");
            }
        }
        
        public void SortBooksByTitle()
        {
            var sortedBooks = books.OrderBy(b => b.Title).ToList();
            DisplaySortedBooks(sortedBooks, "по названию");
        }

        public void SortByYear()
        {
            var sortedBooks = books.OrderBy(b => b.Year).ToList();
            DisplaySortedBooks(sortedBooks, "по году издания");
        }

        private void DisplaySortedBooks(List<Book> sortedBooks, string sortCriteria)
        {
            Console.WriteLine($"\nКниги отсортированы {sortCriteria}:");
            foreach (var book in sortedBooks)
            {
                Console.WriteLine(book);
            }
        }


    }
}