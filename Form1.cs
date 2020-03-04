using System;
using System.Text;
using System.Windows.Forms;

namespace AfmRequestFromAade_dbSoft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Το παρακάτω dll είναι πνευματικής ιδιοκτησίας της εταιρείας dbSoft
        //Κάτοχος της εταιρείας Χριστοφορίδης Διογένης
      
        private async void btnRequest_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
          
            dbSoft.Library service = new dbSoft.Library();
            var result = await service.AfmRequestAsync(
                txtAfm.Text,
                txtUsername.Text,
                txtPassword.Text);

            //Το ResponseCode αναφέρεται σε τρεις καταστάσεις
            //00 Επιτυχής αποτέλεσμα
            //01 Το Αφμ δεν υπάρχει
            //02 Λάθος Username ή Κωδικός
            if (result.colResponseCode == "01")
                MessageBox.Show("Δεν βρέθηκε το Αφμ");
            else if (result.colResponseCode == "02")
                MessageBox.Show("Λάθος Usename ή Κωδικός");
            else if (result.colResponseCode == "00")
            {
                var query = new StringBuilder();
               
                query.Append("\nΤίτλος:" + result.colTitle);
                query.Append("\nΌνομα:" + result.colName);
                query.Append("\nΔιεύθυνση:" + result.colAddress);
                query.Append("\nΕπάγγελμα:" + result.colJobTitle);
                query.Append("\nΔού:" + result.colDoy);

                richTextBox1.Text = query.ToString();
            }
            Cursor.Current = Cursors.Default;

        }
    }
}
