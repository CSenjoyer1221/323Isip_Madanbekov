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

        public void FindMostExpensiveAndCheapestBooks()
        {
            if (!books.Any())
            {
                Console.WriteLine("\nВ библиотеке нет книг");
                return;
            }

            var mostExpensive = books.OrderByDescending(b => b.Price).First();
            var mostCheapest = books.OrderBy(b => b.Price).First();

            Console.WriteLine("\nСамая дорогая книга: ", mostExpensive);
            Console.WriteLine("\nСамая дешевая книга: ", mostCheapest);
        }

        public void GroupBooksByAuthor()
        {
            var groupedBooks = books.GroupBy(b => b.Author).OrderByDescending(g => g.Count());
            Console.WriteLine("\nКоличество книг по авторам: ");
            foreach (var group in groupedBooks)
            {
                Console.WriteLine($"Автор: {group.Key}, Количество книг: {group.Count()}");
                foreach (var book in group)
                {
                    Console.WriteLine($"-{ book.Title} ({ book.Year})");
                }
                Console.WriteLine();
            }
        }

        public void DisplayAllBooks()
        {
            if (!books.Any())
            {
                Console.WriteLine("\nВ библиотеке нет книг");
                return;
            }
            Console.WriteLine("\nВсе книги в библиотеке: ");
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var library = new Library();
            bool exit = false;

            Console.WriteLine("=== СИСТЕМАА УЧЕТА БИБЛИОТЕКИ ===");

            while (!exit)
            {
                DisplayMenu();
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddBookCommand(library); break;
                    case "2": RemoveBookCommand(library); break;
                    case "3": SearchBooksCommand(library); break;
                    case "4": SortBooksCommand(library); break;
                    case "5": library.FindMostExpensiveAndCheapestBooks(); break;
                    case "6": library.GroupBooksByAuthor(); break;
                    case "7": library.DisplayAllBooks(); break;
                    case "0": exit = true;
                        Console.WriteLine("Выход из программы..."); break;
                    default: Console.WriteLine("Неверная команда. Попробуйте снова"); break;
                }
            }

            static void DisplayMenu()
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу по ID");
                Console.WriteLine("3. Найти книги");
                Console.WriteLine("4. Отсортировать книги");
                Console.WriteLine("5. Самая дорогая/дешевая книга");
                Console.WriteLine("6. Группировать по авторам");
                Console.WriteLine("7. Показать все книги");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите команду: ");
            }
        }

    }

}