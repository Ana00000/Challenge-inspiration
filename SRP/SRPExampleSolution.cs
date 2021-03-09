using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamplesApp.SRP
{
    class BookReading
    {
        public Book ReadBook { get; }
        public int ReaderId { get; }

        private BookReadingStatus _status;

        public int LastPageRead { get; private set;  }
        public DateTime Started { get; private set; }
        public DateTime Finished { get; private set; }

        public BookReading(Book readBook, int readerId)
        {
            ReadBook = readBook;
            ReaderId = readerId;
            _status = BookReadingStatus.InLibrary;
        }

        private void StartReading()
        {
            if(_status != BookReadingStatus.InLibrary) throw new InvalidOperationException("Already started reading.");
            _status = BookReadingStatus.Started;
            Started = DateTime.Now;
            LastPageRead = 0;
        }

        public string ContinueReading()
        {
            return ReadBook.GetPage(LastPageRead);
        }

        public string TurnPage(int newPage, int readerId)
        {
            if(_status != BookReadingStatus.Started) throw new InvalidOperationException("Pages can be turned only for active readings.");
            LastPageRead = newPage;
            return ContinueReading();
        }

        public void EndReading()
        {
            if(_status != BookReadingStatus.Started) throw new InvalidOperationException("Cannot finish book before starting.");
            _status = BookReadingStatus.Finished;
            Finished = DateTime.Now;
        }
    }

    internal enum BookReadingStatus
    {
        InLibrary, Started, Finished
    }

    class Book
    {
        public int Id { get; }
        public string Title { get; }
        public string Author { get; }
        public string Abstract { get; }

        private Dictionary<int, string> _pages;

        public string GetPage(int lastPageNumber)
        {
            if (!_pages.ContainsKey(lastPageNumber)) throw new InvalidOperationException("Book does not contain the requested page.");
            return _pages[lastPageNumber];
        }
    }

    class ReadingService
    {
        //Instead of a separate repository or loading from file, we use this dummy list
        private List<BookReading> _readingRepository = new List<BookReading>();

        public int GetNumberOfReads(int readerId, int bookId)
        {
            return _readingRepository.Count(book => book.ReadBook.Id == bookId && book.ReaderId == readerId);
        }
    }
}
