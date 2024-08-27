using LibraryDAL.Model;
using BDLearn.Models;

namespace BDLearn.response
{
    public class Recommendations
    {
        public List<Item> FilterBooksAsync(List<Item> books, List<string> UserBookIds)
        {
            return books.Where(book => !UserBookIds.Contains(book.Id)).ToList();
        }
    }
}
