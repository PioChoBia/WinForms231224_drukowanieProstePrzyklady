using System.Drawing.Printing;

namespace WinForms231224_drukowanieProstePrzyklady
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //najprostsze drukowanie
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, 
            System.Drawing.Printing.PrintPageEventArgs e)
        {
            //najprostsze drukowanie
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




        private void button2_Click(object sender, EventArgs e)
        {
            //drukowanie z kodu, nie wykorzystuj¹c kontrolki
            //tworzymy obiekt PrintDocument
            PrintDocument pD2= new PrintDocument();

            //nazwa dokumentu w kolejce wydruku
            pD2.DocumentName = "to jest pD2";

            pD2.PrintPage += new PrintPageEventHandler(pD2_PrintPage);
            pD2.Print();
        }

        private void pD2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //tekst do druku
            string s1 =
                "Ala ma psa\noraz kota\ni\npiêæ myszy..";

            e.Graphics.DrawString(
                s1, 
                new Font("Arial",20, FontStyle.Underline), 
                Brushes.Blue,
                20,
                10);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //drukowanie z wykorzystaniem PrintDialog/wybór drukarki, iloœci stron, itd/
            PrintDocument pD3 = new PrintDocument();
            pD3.DocumentName = "to jest pD3 z stron¹ pD2_PrintPage";
            pD3.PrintPage += new PrintPageEventHandler(pD2_PrintPage);

            //drukoje z wykorzystaniem PrintDialog
            //mo¿na skorzystaæ z kontolki PrintDialog opisanej jako printDialog1
            printDialog1.Document = printDocument1;

            //lub samemu stworzyæ obiekt PrintDialog
            //PrintDialog pDialog1 = new PrintDialog();

            //przekazaæ PD3 do printDialog1
            printDialog1.Document = pD3;

            //je¿eli w PrintDialog wybrano przycisk OK
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                pD3.Print();
            }
            else return;//wyjœcie z procedury
        }



        PrintDocument printDocument = new PrintDocument();
        PageSetupDialog pageSetupDialog = new PageSetupDialog();
        PrintDialog printDialog = new PrintDialog();




        private void button4_Click(object sender, EventArgs e)
        {
            printDocument.DocumentName = "u¿ywam PageSetupDialog";
            printDocument.PrintPage += new PrintPageEventHandler(pD2_PrintPage);
            printDocument.DefaultPageSettings.Margins.Top = 50;
            printDocument.PrinterSettings.DefaultPageSettings.Margins.Top = 100;
            printDocument.Print();
            
            pageSetupDialog.Document = printDocument;
            printDialog.Document = printDocument;

            /*
            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "u¿ywam PageSetupDialog";
            printDocument.PrintPage += new PrintPageEventHandler(pD2_PrintPage);
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            //ustawianie w³aœciwoœci strony
            //za pomoc¹ PageSetupDialog
            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            
            
            //do³aczamy nasz drukowany dokument
            pageSetupDialog.Document = printDocument;
            

            //ustawiamy wartoœci domyœlne PageStttings i PrinterSetings poprzez new
            pageSetupDialog.PageSettings = new System.Drawing.Printing.PageSettings();
            pageSetupDialog.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
           */

            /*
            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.DefaultPageSettings = DialogResult.;
                printDocument.Print();

                
              
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    
                    printDocument.DefaultPageSettings = pageSetupDialog.PageSettings;

                    printDocument.DefaultPageSettings.Margins.Top = 100;

                    printDocument.PrinterSettings=pageSetupDialog.PrinterSettings;
                    

                    printDocument.Print();
                }
                else
                {
                    //wyjœcie z procedury
                    return;
                }
            
            }       
            
            */

        }







        private void wieleStron_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //drukowanie du¿ego tekstu na kilka stron
            //formatuje stronê

            //tekst do druku
            string s1 = "";
            for (int i = 0; i < 20; i++)
            {
                s1 += "Ala ma psa\noraz kota\ni\npiêæ myszy..\n\n";
            }

            //jakim fontem ma drukowaæ
            Font font = new Font("Arial", 12);

            //prostok¹tny obszar w którym drukuje
            //RectangleF tworzy 4 punkty, naro¿niki prostok¹ta
            //e.MarginBounds to wbudowane marginesy kartki, tylko do odczytu
            RectangleF rectangleF = new RectangleF(
                e.MarginBounds.X,
                e.MarginBounds.Y,
                e.MarginBounds.Width,
                e.MarginBounds.Height
                );

            //ile tekstu zmieœci siê na stronie
            //minus 1 wiersz
            //dlatego tak font.GetHeight(e.Graphics) bo w kontekœcie e.Graphics
            SizeF ileNaStronie = new SizeF(
                e.MarginBounds.Width,
                e.MarginBounds.Height - font.GetHeight(e.Graphics) );

            StringFormat stringFormat = new StringFormat();
            //dzielenie d³ugich wierszy pomiêdzy wyrazmi
            stringFormat.Trimming = System.Drawing.StringTrimming.Word;

            //ile znaków i wiersy ma jedna strona
            int ileZnakow, ileWierszy;
            e.Graphics.MeasureString(
                s1, font, ileNaStronie, stringFormat, out ileZnakow, out ileWierszy);

            //ile znaków ma bie¿¹ca stona
            string biezacaStrona = s1.Substring(0, ileZnakow);

            //drukowanie bie¿acej strony
            e.Graphics.DrawString(
                biezacaStrona,
                font,
                Brushes.Blue,
                rectangleF,
                stringFormat);

            //je¿eli mamy wiecej ni¿ jeden¹ stronê
            //drukuje dalej bo u¿ywamy e.HasMorePages = true;
            //aby zakoñczyæ drukowanie trzeba daæ e.HasMorePages=false;
            if (ileZnakow < s1.Length)
            {
                s1 = s1.Substring(ileZnakow);
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }

        }







        private void button5_Click(object sender, EventArgs e)
        {
            PrintDocument pD5= new PrintDocument();

            PageSettings pageSettings = new PageSettings();
            pD5.DefaultPageSettings = pageSettings; 
            
            pD5.DocumentName = "to jest pD5";
            pD5.PrintPage += new PrintPageEventHandler(wieleStron_PrintPage);

            PrintPreviewDialog printPreviewDialog1 =
                new PrintPreviewDialog();
            printPreviewDialog1.Document= pD5;

            //wyœwietla podgl¹d
            printPreviewDialog1.ShowDialog();

        }
    }
}