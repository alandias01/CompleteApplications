using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ContractCompareLoanetFile.LoanetBook;
using WPFUtils;

namespace ContractCompareLoanetFile
{

    /* Steps
     * 1.  Get all books
     * 2.  Foreach Book, Construct the Object Creator
     * 2.1 Object creator will Get raw data and apply business rules
     
     * get the data and store in List<spHeliumDataForContractCompareLoanetFileObject>
     * 2.1 Iterate through the List and Apply the business rules to the data and put in new List<Z4FileSpec>
     * 
     */


    public class FileCreator
    {
        public bool SendTextOnError = false;
        IObjectCreator OC;

        //Here we iterate through each book and create a file
        public FileCreator(DateTime inputdate)
        {
            LoanetBookFactory lbf = new LoanetBookFactory();
            List<LoanetBookObject> LBOList = new List<LoanetBookObject>();
            
            try
            {
                lbf.GetAllLoanetBook(LBOList);
            }
            catch (Exception ex)
            {
                Utils.LogError(ex.Message);
                return;
            }

            if (LBOList.Count<1)
            {
                Utils.LogError("Process will not run.  There were no loanet books in table ContractCompare.LoanetBook");
                return;
            }
                        

            LBOList = LBOList.Where(x => x.Active.Value == true).ToList();

            foreach (LoanetBookObject item in LBOList)
            {
                string data = string.Empty;
                
                if (
                    string.IsNullOrEmpty(item.BookName)
                    || string.IsNullOrEmpty(item.BookNumber)
                    || string.IsNullOrEmpty(item.LoanetZone)
                    || string.IsNullOrEmpty(item.FileName)
                    )
                {
                    WPFUtils.Utils.LogError("Please check that all fields in ContractCompare.LoanetBook table are filled");
                    continue;
                }

                
                if (item.LoanetZone == "4")
                {
                    OC = new Z4ObjectCreator(inputdate, item.BookName, item.BookNumber, 4, ref SendTextOnError);
                }

                else if (item.LoanetZone == "3")
                {
                    //OC = new Z3ObjectCreator(inputdate, BookName, BookNumber, 3, ref SendTextOnError);
                }

                data = OC.GetDataForFile();
                File.WriteAllText(item.FileName, data);
            }


            if (SendTextOnError)
            {
                WPFUtils.Utils.SendEmail(Properties.Settings.Default.EmailToText, "Contract Compare Issues", "Check Loanet Files", true);
            }

        }

    }

    
    
}
