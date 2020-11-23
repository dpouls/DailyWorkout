using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace DlyWrkOut
{
    public partial class MainPage : ContentPage
    {
        //int variable for the total workout minutes
        int workoutMinutes = 0;
        //string variable for path to the selected file
        string storedFile = "";
        public MainPage()
        {
            InitializeComponent();
            // create new variable that displays today's date in a shorter format.
            string curDate = DateTime.Now.ToShortDateString();
            LblDate.Text = $"Date: {curDate}";
            //make a string that replaces the / with _ for the file name.
            string fileDate = curDate.Replace('/', '_');

            //use LoadSavedFile to load the selected file for this date.
            LoadSavedFile(fileDate);

            //set the results label to the workout minutes string value
            LblResults.Text = workoutMinutes.ToString();
        }
        /// <summary>
        /// we will find and select the file, else create a file.
        /// </summary>
        /// <param name="fileDate"></param>
        private void LoadSavedFile(string fileDate)
        {
            //find the document folder path.
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // give the file a name with the current date
            string fileName = $"{fileDate}wom.txt";
            // put the named file at the docpath
           storedFile = Path.Combine(docPath, fileName);
            //check if the file exists
            if (File.Exists(storedFile))
            {
                //if true
                //load the file and parse the read file to an integer and assign the value to the workoutminutes.
                workoutMinutes = int.Parse(File.ReadAllText(storedFile));
            }
            else
            {
                //else
                //create the file with zero minutes to start
                File.WriteAllText(storedFile, workoutMinutes.ToString());
            }
        }

        /// <summary>
        /// add to current workout minutes and save the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddMinutes_Clicked(object sender, EventArgs e)
        {
            //make sure something was selected on the picker.
            if (PckMinutes.SelectedIndex > -1)
            {
                //if something was selected, convert the selected item to an int and add it to the workout minutes
                workoutMinutes += int.Parse(PckMinutes.SelectedItem.ToString());
                //update the file by writing the value of workoutMinutes
                File.WriteAllText(storedFile, workoutMinutes.ToString());
                //update the results text to the updated workoutminutes
                LblResults.Text = workoutMinutes.ToString();
            }
            else
            {
                //if nothing selected, alert the user to select the amount of minutes to add. 
                DisplayAlert("Invalid input", "Please select minutes to add", "Close");
            }
        }
    }
}
