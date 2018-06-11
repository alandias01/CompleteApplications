using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFUtils;


namespace ContractCompareLoanetFile
{
    /* Adjustments before going live
     * Importer is reading from static folder
     * 
     * 
     * FileImporter deletes by the date of the input file, make sure it only reads latest file lastTradeDate
     * 
     * Check if ApplyBusinessRulesToRawData puts any data if connection is bad
     * Does this throw error? data = Z4O.GetDataForFile();
     */

    /*
     * APEX-MS5239 5239 4 ECI5239
     * APEX-MS0269 0269 4 ECI0269
     * APEX-MSCL 0516 4 ECI0516
     * APEX-MSCL T008 3 C3IT008
     * APEX-MS5289 5289 3 C3I5289
     
     */


    class Program
    {
        static void Main(string[] args)
        {
			
			/*  If you want to use DI Container
            UnityContainer containerExport = new UnityContainer();
            containerExport.RegisterType<ILoanetBooks, LoanetBooksExportFromConfigFile>();
            DateTime date_export = DateTime.Today;
            InjectionConstructor ic_export = new InjectionConstructor(date_export, typeof(ILoanetBooks));
            containerExport.RegisterType<FileCreator>(ic_export);


            UnityContainer containerImport = new UnityContainer();
            containerImport.RegisterType<ILoanetBooks, LoanetBooksImportFromConfigFile>();
            DateTime date_import = DateTime.Today;
            InjectionConstructor ic_import = new InjectionConstructor(date_import, typeof(ILoanetBooks));
            containerImport.RegisterType<FileImporter>(ic_import);
            
            //case "exportb": containerExport.Resolve<FileCreator>();
            //    break;

            //case "import": containerImport.Resolve<FileImporter>();
            //    break;
            
            */
			
            DateTime date_export = DateTime.Today;
            //DateTime date_import = DateTime.Today;

            if (args.Length == 0)
            {
                Console.WriteLine("export   This will create the output files");
                Console.WriteLine("import   This will import the input files");
            }
            else
            {
                if (args.Length==1)
                {
                    switch (args[0])
                    {
                        
                        case "export": new FileCreator(date_export);
                            break;

                        case "import": new FileImporter();
                            break;

                        default:
                            break;
                    }

                }
            }



        }

                       
    }

    
}
