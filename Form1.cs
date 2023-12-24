namespace WinForms231224_drukowanieProstePrzyklady
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //tekst do druku
            string s1 = 
                "Ala ma psa\noraz kota\ni\npiêæ myszy..";
            
            //ustawinie fontu
            Font f1=new Font("Arial",10, FontStyle.Bold);

            //kolor pêdzla
            Brush b1 = Brushes.Red;

            //polozenia na kartce
            Point p1 = new Point(10, 20);

            e.Graphics.DrawString(s1,f1,b1,p1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }
    }
}