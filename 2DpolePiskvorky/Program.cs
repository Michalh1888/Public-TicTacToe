// See https://aka.ms/new-console-template for more information

//vytvoř.prázdného pole
string[,] pole = new string[11, 10];//X=i(poč.sloupců v řádku),Y=j(poč.řádků ve sloupci)
for (int i = 0; i < 11; i++)
    for (int j = 0; j < 10; j++)
        pole[i, j] = " ";
//okraje
for (int i = 1; i < 10; i++)//1.sloupec
    pole[0, i] = i.ToString();
for (int j = 2; j < 11; j++)//1.řádek
    pole[j, 0] = (j-1).ToString();

string hracKolecko = "kolečky";
string hracKrizek = "křížky";
string hracNaTahu = hracKolecko;
string hracCoTahl = "";
int x;
int y;
string znak = "O";
bool souvisle=false;

//herní smyčka
bool run = true;
bool again;
while (run)
{   
    //vykreslení pole
    Console.Clear();
    for (int j = 0; j < pole.GetLength(1); j++)//cyklus řádků-Y
    {
        for (int i = 0; i < pole.GetLength(0); i++)//cyklus sloupců-X
            Console.Write(pole[i, j]);
        Console.WriteLine();
    }
    if (souvisle)
    {
        Console.WriteLine($"Vyhrál hráč s {hracCoTahl}");
        break;
    }
    else
        Console.WriteLine();
        Console.WriteLine($"Na řadě je hráč s {hracNaTahu}");
    do
    {
        again = false;
        Console.Write("Zadej pozici X kam chceš táhnout: ");
        while (!int.TryParse(Console.ReadLine(), out x))
            Console.WriteLine("Zadej prosím celé číslo");
        Console.Write("Zadej pozici Y kam chceš táhnout: ");
        while (!int.TryParse(Console.ReadLine(), out y))
            Console.WriteLine("Zadej prosím celé číslo");
        //kontrola souřadnic
        if ((x < 1) || (x > 9) || (y < 1) || (y > 9) || (pole[x+1, y] != " "))
        {
            again= true;
            Console.WriteLine("Neplatná pozice, zadej ji prosím znovu.");
        }
    }while (again);
    //zapsání znaku
    hracCoTahl = hracNaTahu;
    if (hracNaTahu == hracKolecko)
    {
        znak = "O";
        hracNaTahu = hracKrizek;
    }
    else
    {
        znak = "X";
        hracNaTahu = hracKolecko;
    }
    pole[x+1,y] = znak;
    
    //kontrola výhry
    souvisle = false;
    int pocet = 0;
    for (int i = 0; i < pole.GetLength(0); i++)//X
        if (pole[i, y] == znak)
        {
            pocet++;
            if (pocet == 5) { souvisle = true; }
        }   
        else
            pocet = 0;
    if (!souvisle)
    {
        pocet = 0;
        for (int i = 0; i < pole.GetLength(1); i++)//Y
            if (pole[x+1, i] == znak)
            {
                pocet++;
                if (pocet == 5) { souvisle = true; }
            }
            else
                pocet = 0;

    }
    if (!souvisle)//úhlopříčky
    {
        double poziceXlast = 0;
        double poziceX = 0;
        pocet = 0;
        for (int i = 0; i < pole.GetLength(0); i++)//X
        {
            for (int j = 0; j < pole.GetLength(1); j++)//Y
            {
                if (pole[i,j] == znak)
                {
                    poziceXlast = poziceX;
                    poziceX = i;
                    if (Math.Abs(poziceX - poziceXlast)==1)
                        pocet++;
                    if (pocet == 4) { souvisle = true; }
                }

            }
        }
    }
}

Console.ReadKey();
