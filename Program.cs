using System;
namespace PR1TuneuBaucellsAleix
{
    public class Program
    {
        public static void Main()
        {
            const string MSGHELLO = "Benvingut al Mastermind!";
            const string MSGDIFICULTY = "Selecciona el nivell de dificultat:\n" +
                                        "1. Dificultat novell: 10 intents \n" +
                                        "2. Dificultat aficionat: 6 intents \n" +
                                        "3. Dificultat expert: 4 intents \n" +
                                        "4. Dificultat Màster: 3 intents \n" +
                                        "5. Dificultat personalitzada ";
            const string MSGERROR = "Error, introdueix un valor vàlid.";
            const string MSGERRORFORMAT = "Error. S'han d'introduir 4 valors separats per un espai.";
            const string MSGINTENTS = "Selecciona el número d'intents: ";
            const string MSGINTENT = "Intent número ";
            const string MSGASK = "Quin és el codi secret?";
            const string MSGPISTA = "Casi! Pista: ";
            const string MSGINTRODUIT = "Has introduït: ";
            const string MSGVICTORIA = "Felicitats! Has endevinat la combinació secreta!";
            const string MSGDEFEAT = "No has endevinat la combinació secreta!";
            
            int option = 0;
            int intents = 0;
            int maxIntents = 0;
            int numEncertsPosicio = 0;
            int numEncertsSensePosicio = 0;
            int[] intentUsuari = new int[4];
            int[] combinacioSecreta = { 6, 5, 1, 2 };
            
            string input = "";
            string pista = "";

            bool opcioValida = false;
            bool victoria = false;


            Console.WriteLine(MSGHELLO);
            Console.WriteLine();
            
            // Selecció de dificultat
            while (!opcioValida)
            {
                Console.WriteLine(MSGDIFICULTY);
                option = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        maxIntents = 10;
                        opcioValida = true;
                        break;
                    case 2:
                        maxIntents = 6;
                        opcioValida = true;
                        break;
                    case 3:
                        maxIntents = 4;
                        opcioValida = true;
                        break;
                    case 4:
                        maxIntents = 3;
                        opcioValida = true;
                        break;
                    case 5:
                        Console.WriteLine(MSGINTENTS);
                        maxIntents = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        opcioValida = true;
                        break;
                    default:
                        Console.WriteLine(MSGERROR);
                        Console.WriteLine();
                        break;
                }
            }

            // Inici de la partida 
            while ((!victoria) && (intents < maxIntents))
            {
                Console.WriteLine(MSGINTENT + $"{intents + 1}.");
                Console.WriteLine();
                Console.WriteLine(MSGASK);
                input = Console.ReadLine();
                intentUsuari = ValidarInput(input);
                Console.WriteLine();

                if (intentUsuari != null)
                {
                    Console.WriteLine(MSGINTRODUIT);
                    foreach (int num in intentUsuari)
                    {
                        ImprimirColor(num);
                        Console.Write(" ");
                    }

                    Console.WriteLine();
                    Console.WriteLine();

                    intents++;
                    numEncertsPosicio = ContarEncertsPosicio(intentUsuari, combinacioSecreta);
                
                    if (numEncertsPosicio == 4)
                    {
                        victoria = true;
                        Console.WriteLine(MSGVICTORIA);
                        Console.WriteLine();
                    }
                    else
                    {
                        pista = ActualitzarPista(intentUsuari, combinacioSecreta);
                        Console.WriteLine(MSGPISTA + $"{pista}");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine(MSGERRORFORMAT);
                    Console.WriteLine();
                }
            }

            if (!victoria)
            {
                Console.WriteLine(MSGDEFEAT);
            }
            
        }

        public static int[] ValidarInput(string input)
        {
            string[] numeros = input.Split(' ');
            int[] intent = new int[4];
            int numero = 0;

            if (numeros.Length != 4)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (!int.TryParse(numeros[i], out numero) || numero < 1 || numero > 6)
                    {
                        return null;
                    }
                    intent[i] = numero;
                }
                return intent;
            }
        }

        public static int ContarEncertsPosicio(int[] intent, int[] combinacioSecreta)
        {
            int encerts = 0;

            for (int i = 0; i < 4; i++)
            {
                if (intent[i] == combinacioSecreta[i])
                {
                    encerts++;
                }
            }
            return encerts;
        }

        public static string ActualitzarPista(int[] intent, int[] combinacioSecreta)
        {
            string pista = "";
            bool[] combinacioMarcada = new bool[4];
            bool[] intentMarcat = new bool[4];
            bool trobat;

            // Buscar posicions correctes i marcar amb O
            for (int i = 0; i < 4; i++)
            {
                if (intent[i] == combinacioSecreta[i])
                {
                    pista += 'O';
                    combinacioMarcada[i] = true;
                    intentMarcat[i] = true;
                }
            }

            // Buscar números correctes en posicions incorrectes
            for (int i = 0; i < 4; i++)
            {
                trobat = false;

                if (!intentMarcat[i])
                {
                    for (int j = 0; j < 4 && !trobat; j++)
                    {
                        if (!combinacioMarcada[j] && intent[i] == combinacioSecreta[j])
                        {
                            pista += '0';
                            combinacioMarcada[j] = true;
                            intentMarcat[i] = true;
                            trobat = true;
                        } 
                    }

                    if (!trobat)
                    {
                        pista += 'x';
                    }
                }
            }

            return pista;
        }

        // Mostrar números de l'input a color
        public static void ImprimirColor(int numero)
        {
            switch(numero)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.Write(numero);
            Console.ResetColor();
        }
    }
}