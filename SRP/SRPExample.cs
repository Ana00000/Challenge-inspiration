using System;
using System.Collections.Generic;

namespace ExamplesApp.SRP.Violation
{
    /// <summary>
    /// 1) Describe the Book class using the heuristic for describing a module's responsibilities while avoiding generic words.
    /// 2) Use the related algorithm to examine if your definition still stands when some you remove a field or method.
    /// 3) Imagine a new requirement - tracking if a reader has read the book multiple times. How would you support it?
    /// 4) Imagine a new requirement - tracking when a reader started and finished reading a book. How would you support it?
    /// 5) List the concerns of this class. Use extract class to separate these concerns. Describe how you would support the requirement from the previous step.
    /// </summary>
    class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string Abstract { get; }
        public Dictionary<int, string> Pages { get; }
        public Dictionary<int, int> LastPageReadByReader { get; }

        public string StartReading(int readerId)
        {
            bool newReader = LastPageReadByReader.TryAdd(readerId, 0);
            return newReader ? Pages[0] : ContinueReading(readerId);
        }

        public string ContinueReading(int readerId)
        {
            int lastPageNumber = LastPageReadByReader[readerId];
            return Pages[lastPageNumber];
        }

        public string TurnPage(int newPage, int readerId)
        {
            if(!Pages.ContainsKey(newPage)) throw new InvalidOperationException("Book does not contain the requested page.");
            LastPageReadByReader[readerId] = newPage;
            return Pages[newPage];
        }
    }
}
