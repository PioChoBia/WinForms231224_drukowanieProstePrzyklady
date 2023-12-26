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
                "Ala ma psa\noraz kota\ni\npi�� myszy..";
            
            //ustawinie fontu
            Font f1=new Font("Arial",10, FontStyle.Bold);

            //kolor p�dzla
            Brush b1 = Brushes.Red;

            //polozenia na kartce
            Point p1 = new Point(10, 20);

            e.Graphics.DrawString(s1,f1,b1,p1);
        }




        private void button2_Click(object sender, EventArgs e)
        {
            //drukowanie z kodu, nie wykorzystuj�c kontrolki
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
                "Ala ma psa\noraz kota\ni\npi�� myszy..";

            e.Graphics.DrawString(
                s1, 
                new Font("Arial",20, FontStyle.Underline), 
                Brushes.Blue,
                20,
                10);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //drukowanie z wykorzystaniem PrintDialog/wyb�r drukarki, ilo�ci stron, itd/
            PrintDocument pD3 = new PrintDocument();
            pD3.DocumentName = "to jest pD3 z stron� pD2_PrintPage";
            pD3.PrintPage += new PrintPageEventHandler(pD2_PrintPage);

            //drukoje z wykorzystaniem PrintDialog
            //mo�na skorzysta� z kontolki PrintDialog opisanej jako printDialog1
            printDialog1.Document = printDocument1;

            //lub samemu stworzy� obiekt PrintDialog
            //PrintDialog pDialog1 = new PrintDialog();

            //przekaza� PD3 do printDialog1
            printDialog1.Document = pD3;

            //je�eli w PrintDialog wybrano przycisk OK
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                pD3.Print();
            }
            else return;//wyj�cie z procedury
        }
                      



        private void button4_Click(object sender, EventArgs e)
        {
            //drukowanie z PageSetupDialog - ustawienia strony
            //by dzia�aly marginesy musi by� printDocument.OriginAtMargins = true;
            //nale�y do�aczy� dokument do PegeSetupDialog i PrintDialog

            PrintDocument printDocument = new PrintDocument();
            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            PrintDialog printDialog = new PrintDialog();


            printDocument.DocumentName = "tu z PageSetupDialog i PrintDialog";
            printDocument.PrintPage += new PrintPageEventHandler(pD2_PrintPage);
            //by dzia�aly marginesy w PageSetupDialog
            //musi by� printDocument.OriginAtMargins = true;
            printDocument.OriginAtMargins = true;


            //ustawiamy warto�ci domy�lne PageStttings i PrinterSetings poprzez new
            pageSetupDialog.PageSettings = new System.Drawing.Printing.PageSettings();
            pageSetupDialog.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            pageSetupDialog.Document= printDocument;
            printDialog.Document= printDocument;

            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                             
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
                else
                {
                    //wyj�cie z procedury
                    return;
                }            
            }    
                        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //PrintPreviewDialog podglad dokumentu

            PrintDocument printDocument = new PrintDocument();
            PrintPreviewDialog printPreviewDialog =
                new PrintPreviewDialog();

            printDocument.DocumentName = "tu z PrintPreviewDialog";
            printDocument.PrintPage += new PrintPageEventHandler(wieleStron1_PrintPage);
            printPreviewDialog.Document = printDocument;
                        
            printPreviewDialog.ShowDialog();

        }



        int nrStrony = 0;

        private void wieleStron1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //drukowanie wielu stron
            //kolejne strony b�d� do��czane do druku,
            //gdy w�a�ciwo�� e.HasMorePage=True i gdy mamy zewn�trzn� zmienn�/poza procedur�/
            //pocz�tkowo ustawiamy j� na 0 nrStrony
            //jak mamy jedn� lub ostatni� to trzeba ustawi� e.HasMorePage=False; 
            //i wyzerowa� nrStrony
            //inaczej zawiesi si� bo b�dzie czeka� na kolejn� stron�

            //strona 1
            if (nrStrony == 0)
            {
              e.Graphics.DrawString(
                "strona 1",
                new Font("Arial", 50),
                Brushes.Blue,
                20,
                10);
            }

            //strona 2            
            if (nrStrony == 1)
            {
                    e.Graphics.DrawString(
                    "strona 2",
                    new Font("Arial", 50),
                    Brushes.Red,
                    40,
                    30);
            }

            //strona 3
            if (nrStrony == 2)
            {
                e.Graphics.DrawString(
                "strona 3",
                new Font("Arial", 50),
                Brushes.Green,
                60,
                50);
            }
            
            //licznik
            nrStrony++;
            if (nrStrony <3) { 
                e.HasMorePages= true;
            }
            else
            {
                e.HasMorePages= false;
                nrStrony = 0;
            }
        }



        int nrStrony2 = 0;
        string s1 = "";
            private void wieleStron2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //drukowanie du�ego tekstu na kilka stron
            //formatuje stron�



            if (nrStrony2 == 0)
            {
                //tekst do druku
                for (int i = 0; i < 35; i++)
                {
                    s1 += "Ala ma psa\noraz kota\ni\npi�� myszy..\n\n";
                }



            }




            //jakim fontem ma drukowa�
            Font font = new Font("Arial", 12);

            //prostok�tny obszar w kt�rym drukuje
            //RectangleF tworzy 4 punkty, naro�niki prostok�ta
            //e.MarginBounds to wbudowane marginesy kartki, tylko do odczytu
            RectangleF rectangleF = new RectangleF(
                e.MarginBounds.X,
                e.MarginBounds.Y,
                e.MarginBounds.Width,
                e.MarginBounds.Height
                );

            //ile tekstu zmie�ci si� na stronie
            //minus 1 wiersz
            //dlatego tak font.GetHeight(e.Graphics) bo w kontek�cie e.Graphics
            SizeF ileNaStronie = new SizeF(
                e.MarginBounds.Width,
                e.MarginBounds.Height - font.GetHeight(e.Graphics) );

            StringFormat stringFormat = new StringFormat();
            //dzielenie d�ugich wierszy pomi�dzy wyrazmi
            stringFormat.Trimming = System.Drawing.StringTrimming.Word;

            //ile znak�w i wiersy ma jedna strona
            int ileZnakow, ileWierszy;
            e.Graphics.MeasureString(
                s1, font, ileNaStronie, stringFormat, out ileZnakow, out ileWierszy);

            //ile znak�w ma bie��ca stona
            string biezacaStrona = s1.Substring(0, ileZnakow);

            //drukowanie bie�acej strony
            e.Graphics.DrawString(
                biezacaStrona,
                font,
                Brushes.Blue,
                rectangleF,
                stringFormat);

            //je�eli mamy wiecej ni� jeden� stron�
            //drukuje dalej bo u�ywamy e.HasMorePages = true;
            //aby zako�czy� drukowanie trzeba da� e.HasMorePages=false;
            //nrStrony2 jest zmienn� poza procedury i jej zmiana powoduje wyskok
            //e.Graphics startuje ponownie
            if (ileZnakow < s1.Length)
            {
                s1 = s1.Substring(ileZnakow);
                e.HasMorePages = true;
                nrStrony2++;
            }
            else
            {
                e.HasMorePages = false;
                nrStrony2 = 0;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            PrintPreviewDialog printPreviewDialog =
                new PrintPreviewDialog();

            printDocument.DocumentName = "tu z PrintPreviewDialog wiele stron wersja2";
            printDocument.PrintPage += new PrintPageEventHandler(wieleStron2_PrintPage);
            printPreviewDialog.Document = printDocument;

            printPreviewDialog.ShowDialog();
        }
    }
}