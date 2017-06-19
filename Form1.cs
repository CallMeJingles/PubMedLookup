using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Net;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Net.Http;
using System.Windows.Forms;
using mshtml;

/********************************************************
*              Journal Reference Lookup                 *
*          Aug 20/2015 Albertas Tomorrow Project        *
* Simple program to help researchers add and remove     *
* PubMed ID's from a database.  Also allows them to get *
* journal information individually or in bulk           *
********************************************************/

namespace JournalRefLookup
{
    public partial class frmJournalLookup : Form
    {

        string singlePMIDLookup = "";  //Single lookup PMID
        string insertPMID = ""; //Insert PMID
        string removePMID = ""; //Remove PMID
        List<String> bulkPMIDLookup = new List<String>(); //Multiple PMID lookup
        int firstWriteLoop = 0; //Prevents excel headers from being overwritten (PMID)
        int firstWriteLoop2 = 0; //Prevents excel headers from being overwritten (results of lookup)
        int j = 0;  //Position holder for writing to excel cells

        //Init excel application
        Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;


        /************************************************************************************************/
        public frmJournalLookup()
        {
            InitializeComponent();
        }

        /************************************************************************************************/
        private void frmJournalLookup_Load(object sender, EventArgs e)
        {
            //Check if Excel is installed
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        }

        /**************************************************************************************************
        *Gets a single PMID reference.  Uses a single PMID from the textbox and passes it through to a url*
        *which is scraped for the required reference.                                                     *
        *@Param: string singlePMID - 8 digit numeric string                                               *
        *@Return: Void                                                                                    *
        ***************************************************************************************************/
        private void getSingleID(string singlePMID)
        {
            var getHtmlWeb = new HtmlAgilityPack.HtmlWeb(); //Init HTML pack
            string urlBuild = ""; //Init URL to scrape
            string lookupSingleID = singlePMID;  //ID to use for scrape
            string lookupSingleResult = ""; //Result of scrape

            urlBuild = "http://www.ncbi.nlm.nih.gov/pubmed/" + lookupSingleID;

            var document = getHtmlWeb.Load(urlBuild);
            var repeaters = document.DocumentNode.SelectNodes("//*[contains(@class,'cit')]");  //Returns Div -> @Class -> cit from URL

            if (repeaters == null)
            {
                MessageBox.Show("Null");
            }

            if (repeaters != null)
            {
                if (repeaters.Count > 1)  //Makes sure that a proper value was returned
                {
                    lookupSingleResult = repeaters[1].InnerText; //Has to be first array position.  Array position 0 has erroneous content, and 1 contains what we need.
                    if (lookupSingleResult.Length == 0)
                        MessageBox.Show("No Value Found");
                    else
                    {
                        txtTitle.Enabled = true;
                        txtSngResult.Enabled = true;
                        txtTitle.Text = getJournalTitle(txtSngLookup.Text);
                        txtSngResult.Text = lookupSingleResult;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid PMID");
                }
            }
        }

        /************************************************************************************************
         *                      Starts the single lookup process.  Nothing required                     *
         ***********************************************************************************************/
        private void btnSngLookup_Click(object sender, EventArgs e)
        {

            getSingleID(singlePMIDLookup);
        }

        /************************************************************************************************
         * Validates the PMID entered in the text box on the fly.  Valid numbers are 8 digits long      *
         * Will disable if criteria is not met                                                          *
         ***********************************************************************************************/
        private void txtSngLookup_TextChanged(object sender, EventArgs e)
        {
            singlePMIDLookup = txtSngLookup.Text;
            int errorCounter = 0;
            errorCounter = Regex.Matches(singlePMIDLookup, @"[a-zA-Z]").Count; //Counts to see how many letters or symbols appear in the string
            {
                if ((singlePMIDLookup.Length == 8 || singlePMIDLookup.Length == 7) && errorCounter == 0)  //If length isn't 8 and there are no symbols/letters in string
                {
                    btnSngLookup.Enabled = true;
                }
                else
                {
                    btnSngLookup.Enabled = false;
                }
            }
        }

        /*************************************************************************************************
         * Querys tomorrow.tbl_PubMedID table for all pre-entered PMID's in the database and returns them*
         * as a List<String> to be used later in the bulk process                                        *
         * @Param: None                                                                                  *
         * @Return: List<String> PMID of database                                                        *
         *************************************************************************************************/
        private List<String> getPMID_Database()
        {
            using (SqlConnection cnn = new SqlConnection("server=xxxxxxxx;database=xxxxxx;Integrated Security=SSPI"))  //Sets database connection
            {
                SqlDataAdapter da = new SqlDataAdapter("select PMID from tbl_PubMedID order by PMID", cnn); //Get PMID from table
                DataSet ds = new DataSet();
                da.Fill(ds, "PMID");

                List<string> databasePMID = new List<string>();
                foreach (DataRow row in ds.Tables["PMID"].Rows)
                {
                    databasePMID.Add(row["PMID"].ToString()); //Fills dataset with PMID
                }
                return databasePMID;
            }
        }

        /************************************************************************************************
         * Enters the PMID to the database                                                              *
         * @Param: String PMID - PMID to be entered                                                     *
         * @Return: None                                                                                *
         ************************************************************************************************/
        private void insertPMIDDatabase(String PMID)
        {
            string numToBeInserted = PMID;
            string connectionString = "server=xxxxxxx;database=xxxxxxx;Integrated Security=SSPI";

            using (SqlConnection cnn = new SqlConnection(connectionString))  //Sets database connection
            {
                SqlCommand cmd = new SqlCommand("insert into tbl_PubMedID (PMID) values (@numToBeInserted) ", cnn);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                cmd.Parameters.AddWithValue("@numToBeInserted", numToBeInserted);
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Successfully Inserted: " + PMID);
        }

        /**************************************************************************************************
         * Removes the given PMID from the database                                                       *
         * @Param: String PMID to be removed                                                              *
         * @Return:  None                                                                                 *
         **************************************************************************************************/
        private void removePMIDDatabase(String PMID)
        {
            string numToBeDeleted = PMID;
            string connectionString = "server=xxxxxxxx;database=xxxxxx;Integrated Security=SSPI";

            using (SqlConnection cnn = new SqlConnection(connectionString))  //Sets database connection
            {
                SqlCommand cmd = new SqlCommand("delete from tbl_PubMedID where PMID = (@numToBeDeleted) ", cnn);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                cmd.Parameters.AddWithValue("@numToBeDeleted", numToBeDeleted);
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Successfully Removed: " + PMID);
        }


        /*************************************************************************************************
         * Starts the bulk lookup process.                                                               *
         * ***********************************************************************************************/
        private void btnBulk_Click(object sender, EventArgs e)
        {
            createExcelHeaders();  //Init the excel headers
            bulkPMIDLookup = getPMID_Database();  //Get the PMID's from the database
            writePMID(); //Write each PMID to excel
            getPMID_Results(bulkPMIDLookup);  //Uses PMID to scrape webpage for references
          

            //Close the excel workbook
            xlWorkBook.SaveAs("\\User Applications\\PubMedLookup", Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
            Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
            Excel.XlSaveConflictResolution.xlUserResolution, true,
            Missing.Value, Missing.Value, Missing.Value);
            xlWorkBook.Close(0);
            xlApp.Quit();
        }

        /**********************************************************************************************
         * Checks if the PMID exists on the PubMed Site.  No point adding an invalid PMID             *
         * @Param: String PMID to check against                                                       *
         * @Return: Bool if exists                                                                    *
         **********************************************************************************************/
        private bool checkIfExists(String PMID)
        {
            var getHtmlWeb = new HtmlAgilityPack.HtmlWeb();  //Init Agility Pack
            string urlBuild = "";
            bool ifExists = false;

            urlBuild = "http://www.ncbi.nlm.nih.gov/pubmed/" + PMID;
            var document = getHtmlWeb.Load(urlBuild);
            var repeaters = document.DocumentNode.SelectNodes("//div[contains(@class,'cit')]");

            if (repeaters.Count > 1) //Will return < 1 if it doesn't exist
            {
                ifExists = true;
            }
            else
            {
                ifExists = false;
            }
            return ifExists;


        }


        /*************************************************************************************************
         * Uses the databases PMID's to scrape the PubMed website for references.  Also calls the write  *
         * references method.  Can be confusing considering the PMID to excel write is not located here  *
         * @Param: List<String> database PMID's                                                          *
         * @Return: None                                                                                 *
         * ***********************************************************************************************/
        private void getPMID_Results(List<String> databasePMID)
        {
            var getHtmlWeb = new HtmlAgilityPack.HtmlWeb();  //Init Agility Pack
            string urlBuild = "";
            for (int i = 0; i < databasePMID.Count; i++)
            {
                string PMID = databasePMID[i];
                urlBuild = "http://www.ncbi.nlm.nih.gov/pubmed/" + PMID;
                var document = getHtmlWeb.Load(urlBuild);
                string lookupResults = "";

                string journalTitle = "";
                var repeaters = document.DocumentNode.SelectNodes("//div[contains(@class,'cit')]");  //Returns scrape results of div@class=cit
                if (repeaters == null)
                {
                    MessageBox.Show("Invalid Length"); ;

                }
                if (repeaters != null)
                {
                    if (repeaters.Count > 1)  //Makes sure that a proper value was returned
                    {
                        lookupResults = repeaters[1].InnerText; //Has to be first array position.  Array position 0 has erroneous content, and 1 contains what we need.
                        if (lookupResults.Length == 0)
                            MessageBox.Show("No Value Found");
                        else
                        {
                            //Need to split the reference results into three columns in excel.
                            string[] referenceSplit = lookupResults.Split('.');  //Split based on '.' first as common denominator in the string
                            string journalName = referenceSplit[0];  //First element is always the journal name
                            int hasSemiColon = Regex.Matches(referenceSplit[1], @";").Count;  //Check for semicolon.  Will appear after date if available
                            if (hasSemiColon == 1)  //Has semicolon
                            {
                                string[] referenceYearSplit = referenceSplit[1].Split(';');
                                string journalYear = referenceYearSplit[0]; //First element will be the journals year
                                string restOfJournal = referenceYearSplit[1] + string.Join(" ", referenceSplit.Skip(2));  //Grab the rest of the string and put together
                                journalTitle = getJournalTitle(PMID);
                                writeResults(journalTitle,journalName,journalYear,restOfJournal);
                            }
                            else //For journals without a semi colon
                            {
                                int splitCount = referenceSplit.Count() -1;  
                                string journalYear1 = referenceSplit[1]; //Gets journal yaer
                                string restOfJournal = string.Join(" ", referenceSplit.Skip(2));  //Grab rest of journal and put together
                                journalTitle = getJournalTitle(PMID);
                                writeResults(journalTitle,journalName,journalYear1,restOfJournal);
                            }     
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid PMID: " + PMID);
                        j++;  //Need to increment counter to not overwrite excel cell next iteration
                    }
                }
            }
        }

        /****************************************************************************************
         * Writes the headers to the excel file.  In its own method mostly to avoid confusion   *
         * and to get it out of the way                                                         *
         * **************************************************************************************/
        private void createExcelHeaders()
        {
            //Write Column Headers
            xlWorkSheet.Cells[1, 1] = "Pub Med ID";
            xlWorkSheet.Cells[1, 2] = "Journal Title";
            xlWorkSheet.Cells[1, 3] = "Journal Name";
            xlWorkSheet.Cells[1, 4] = "Year";
            xlWorkSheet.Cells[1, 5] = "Rest of Publication";
        }

        /******************************************************************************************
         * Writes the results of the lookup to excel.  Checks first if cell is available and then *
         * writes to that location if possible                                                    *
         * @Param: String PMResults - Individual results from the webpage scrape                  *
         * @Return: None                                                                          *
         * ****************************************************************************************/
        private void writeResults(String name,String title,String year,String rest)
        {
            String journalTitle = title;
            String journalYear = year;
            String journalPublication = rest;
            String journalName = name;

            if (firstWriteLoop2 == 0) //Make sure we're not overwriting Column headers
            {

                xlWorkSheet.Cells[2, 2] = journalName;
                xlWorkSheet.Cells[2, 3] = journalTitle;
                xlWorkSheet.Cells[2, 4] = journalYear;
                xlWorkSheet.Cells[2, 5] = journalPublication;
                firstWriteLoop2++;
                j++;
            }
            else //Write to the next available row
            {
                xlWorkSheet.Cells[j + 2, 2] = journalName;
                xlWorkSheet.Cells[j + 2, 3] = journalTitle;
                xlWorkSheet.Cells[j + 2, 4] = journalYear;
                xlWorkSheet.Cells[j + 2, 5] = journalPublication;
                j = j + 1;
            }
        }

        /******************************************************************************************
         * Writes the PMID's to the next available Excel cell.                                    *
         ******************************************************************************************/
        private void writePMID()
        {
            for (int i = 0; i < bulkPMIDLookup.Count; i++)
            {
                if (firstWriteLoop == 0) //Make sure we're not overwriting Column headers
                {
                    xlWorkSheet.Cells[2, 1] = bulkPMIDLookup[0];
                    firstWriteLoop++;
                }
                else
                {
                    xlWorkSheet.Cells[i + 2, 1] = bulkPMIDLookup[i];
                }
            }
        }

        /*****************************************************************************************
         * Simply clears the text boxes uses for a single lookup                                 *
         *****************************************************************************************/
        private void btnSngClear_Click(object sender, EventArgs e)
        {
            txtSngResult.Text = "";
            txtTitle.Text = "";
            txtSngLookup.Text = "";
            txtTitle.Enabled = false;
            txtSngResult.Enabled = false;
        }

        /*****************************************************************************************
         * Clears the Insert PBID field                                                          *
         *****************************************************************************************/
        private void btnInsertClear_Click(object sender, EventArgs e)
        {
            txtInsert.Text = "";
        }

        /*****************************************************************************************
         * Forces validation on what can be inserted to the database                             *
         * PMID must be 8 digits long                                                            *
         *****************************************************************************************/
        private void txtInsert_TextChanged(object sender, EventArgs e)
        {
            insertPMID = txtInsert.Text;
            int errorCounter = 0;
            errorCounter = Regex.Matches(insertPMID, @"[a-zA-Z]").Count; //Counts to see how many letters or symbols appear in the string
            {
                if ((insertPMID.Length == 8 || singlePMIDLookup.Length == 7) && errorCounter == 0)  //If length isn't 8 and there are no symbols/letters in string
                {
                    btnInsert.Enabled = true;
                }
                else
                {
                    btnInsert.Enabled = false;
                }
            }
        }

        /*****************************************************************************************
         * Checks if value exists in the database, and if not inserts it                         *
         *****************************************************************************************/
        private void btnInsert_Click(object sender, EventArgs e)
        {
            bool alreadyExists = false;
            bool existsInPubMD = checkIfExists(txtInsert.Text);
            alreadyExists = checkInsert(txtInsert.Text); //Check if PMID is already in the database

            if ((!alreadyExists) && (existsInPubMD))
            {
                insertPMIDDatabase(txtInsert.Text);
            }
            else
                MessageBox.Show(txtInsert.Text + " already exists in the database or doesn't exist in PubMed");
        }

        /*********************************************************************************************
         * Checks that the PMID that the user intends to add to the database does not exist there yet*
         * @Param: String PMID to be checked against                                                 *
         * @Return: Bool if success/failure. If false, then procede to insert                        *
         *********************************************************************************************/
        private bool checkInsert(string PMID)
        {
            List<String> checkPMID = getPMID_Database();  //Get List<String> of PMIDs from database to check aganist
            bool alreadyExists = false;

            foreach (string result in checkPMID) //Iterate through each PMID to verify
            {
                if (result == PMID)
                {
                    alreadyExists = true;
                    return alreadyExists;
                }
                else
                {
                    alreadyExists = false;
                }
            }
            return alreadyExists;
        }

        /*********************************************************************************************
         * Removes the text from the Remove text field                                               *
         *********************************************************************************************/
        private void btnRemoveClear_Click(object sender, EventArgs e)
        {
            txtRemove.Text = "";
        }

        /*********************************************************************************************
         * Forces the text in Remove textbox to fit required PMID.  Needs to be a length of 8 and    *
         * only digits.  To be honest, could use some refactoring as i've remade this method three   *
         * times for each type of text box when clearly one method would do the trick.               *
         *********************************************************************************************/
        private void txtRemove_TextChanged(object sender, EventArgs e)
        {
            removePMID = txtRemove.Text;
            int errorCounter = 0;
            errorCounter = Regex.Matches(removePMID, @"[a-zA-Z]").Count; //Counts to see how many letters or symbols appear in the string
            {
                if ((removePMID.Length == 8 || singlePMIDLookup.Length == 7) && errorCounter == 0)  //If length isn't 8 and there are no symbols/letters in string
                {
                    btnRemove.Enabled = true;
                }
                else
                {
                    btnRemove.Enabled = false;
                }
            }
        }

        /*****************************************************************************************
         * Starts the removal process of a PMID.  Checks if it is in the database and if so      *
         * will remove it from the list.                                                         *
         *****************************************************************************************/
        private void btnRemove_Click(object sender, EventArgs e)
        {
            bool alreadyExists = false;

            alreadyExists = checkInsert(txtRemove.Text); //Check if PMID is already in the database

            if (alreadyExists)
            {
                removePMIDDatabase(txtRemove.Text);
            }
            else
                MessageBox.Show(txtRemove.Text + " doesn't exist in the database");
        }

        private string getJournalTitle(String PMID)
        {
            var getHtmlWeb = new HtmlAgilityPack.HtmlWeb();  //Init Agility Pack
            string urlBuild = "";
            string journalTitle = "";

            urlBuild = "http://www.ncbi.nlm.nih.gov/pubmed/" + PMID;
            var document = getHtmlWeb.Load(urlBuild);
            var repeaters = document.DocumentNode.SelectNodes("//*[@id='maincontent']/div/div[5]/div/h1");

            journalTitle = repeaters[0].InnerText;

            return journalTitle;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    

    }
}
