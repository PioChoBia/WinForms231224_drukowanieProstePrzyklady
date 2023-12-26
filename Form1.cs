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
                      



        private void button4_Click(object sender, EventArgs e)
        {
            //drukowanie z PageSetupDialog - ustawienia strony
            //by dzia³aly marginesy musi byæ printDocument.OriginAtMargins = true;
            //nale¿y do³aczyæ dokument do PegeSetupDialog i PrintDialog

            PrintDocument printDocument = new PrintDocument();
            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            PrintDialog printDialog = new PrintDialog();


            printDocument.DocumentName = "tu z PageSetupDialog i PrintDialog";
            printDocument.PrintPage += new PrintPageEventHandler(pD2_PrintPage);
            //by dzia³aly marginesy w PageSetupDialog
            //musi byæ printDocument.OriginAtMargins = true;
            printDocument.OriginAtMargins = true;


            //ustawiamy wartoœci domyœlne PageStttings i PrinterSetings poprzez new
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
                    //wyjœcie z procedury
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
            //kolejne strony bêd¹ do³¹czane do druku,
            //gdy w³aœciwoœæ e.HasMorePage=True i gdy mamy zewnêtrzn¹ zmienn¹/poza procedur¹/
            //pocz¹tkowo ustawiamy j¹ na 0 nrStrony
            //jak mamy jedn¹ lub ostatni¹ to trzeba ustawiæ e.HasMorePage=False; 
            //i wyzerowaæ nrStrony
            //inaczej zawiesi siê bo bêdzie czeka³ na kolejn¹ stronê

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
            //drukowanie du¿ego tekstu na kilka stron
            //formatuje stronê



            if (nrStrony2 == 0)
            {
                //tekst do druku
                for (int i = 0; i < 35; i++)
                {
                    s1 += "Ala ma psa\noraz kota\ni\npiêæ myszy..\n\n";
                }



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
            //nrStrony2 jest zmienn¹ poza procedury i jej zmiana powoduje wyskok
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