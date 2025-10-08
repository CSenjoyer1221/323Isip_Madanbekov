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
    }
}