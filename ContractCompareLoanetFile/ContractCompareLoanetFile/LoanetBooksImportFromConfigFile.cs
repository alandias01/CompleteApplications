using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContractCompareLoanetFile.LoanetBook;

namespace ContractCompareLoanetFile
{
    //Not being used
    public class LoanetBooksImportFromConfigFile 
    {
        public List<LoanetBookObject> GetLoanetBooks()
        {
            try
            {
                //string[] BooksRaw = Properties.Settings.Default.BooksToUseForImport.Split(',');
                string[] BooksRaw = "XX_ECO5239 4, XX_ECO0269 4, XX_ECO0516 4".Split(',');

                List<string> Books = BooksRaw.Select(x => x.Trim()).ToList();

                List<LoanetBookObject> LoanetBooks = new List<LoanetBookObject>();

                foreach (string item in Books)
                {
                    string[] BookClearingno = item.Split(' ');

                    //LoanetBooks.Add(new LoanetBook(BookClearingno[0], BookClearingno[1], loanetzone, BookClearingno[3]));
                    LoanetBooks.Add(new LoanetBookObject("", "", BookClearingno[1], BookClearingno[0], true));
                }

                return LoanetBooks;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
