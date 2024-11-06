## Constants de Missatges

````
const string MSGHELLO = "Benvingut al Mastermind!";
const string MSGDIFICULTY = "Selecciona el nivell de dificultat: ...";
const string MSGERROR = "Error, introdueix un valor vàlid.";
const string MSGERRORFORMAT = "Error. S'han d'introduir 4 valors separats per un espai.";
const string MSGINTENTS = "Selecciona el número d'intents: ";
const string MSGINTENT = "Intent número ";
const string MSGASK = "Quin és el codi secret?";
const string MSGPISTA = "Casi! Pista: ";
const string MSGINTRODUIT = "Has introduït: ";
const string MSGVICTORIA = "Felicitats! Has endevinat la combinació secreta!";
const string MSGDEFEAT = "No has endevinat la combinació secreta!";
````

# Variables

````
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
````

Aqui es defineixen les variables que es fan servir dins del programa:

- **option**: La opció seleccionada per l'usuari.

- **intents**: El número d'intents que li queden al jugador.

- **maxIntents**: El número màxim d'intents permesos segons la dificultat.

- **numEncertsPosicio**: Número d'encerts en la posició correcta.

- **numEncertsSensePosicio**: Número d'encerts sense considerar la posició.

- **intentUsuari**: Array que emmagatzema els números introduïts per el jugador a cada intento.

- **combinacioSecreta**: La combinació secreta que ha d'endevinar el jugador.

- **input**: L'entrada de l'usuari (format cadena).

- **pista**: La pista que es dona al jugador després de cada intent.


# Benvinguda i Selecció de Dificultat

````
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
````

En aquesta part, es dona la benvinguda al jugador i es demana que seleccioni el nivell de dificultat. Depenent de l'opció seleccionada, s'assignarà un número d'intents màxims. Si el jugador escull l'opció de dificultat personalitzada, podrà introduïr un número d'intents personalitzat.


# Bucle Principal del Joc

````
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
````

Durant cada iteració d'aquest bucle:

- Es demana al jugador que introdueixi una combinació.

- La combinació es valida utilizant el mètode ValidarInput. Si l'intent és vàlid, es compara amb la combinació secreta.

- Si el jugador endevina la combinació correctament, es mostra un missatge de victòria i acaba el joc. Si no, se li proporciona una pista.

- La pista es genera cada vegada amb el mètode ActualitzarPista, que evalúa quants números estàn correctes en la posició correcta i quants estàn en la combinació però en la posició incorrecta.


# Mètodes Auxiliars

**ValidarInput**: Verifica que l'input del jugador sigui válid (que contingui exactament 4 números entre 1 i 6).

````
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
````

**ContarEncertsPosicio**: Conta quants números de l'intent están en la posició correcta en comparació amb la combinació secreta.

````
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
````

**ActualitzarPista**: Genera una pista basada en els intents del jugador. La pista utilitza:

- O per els encerts en la posició correcta.

- 0 per els encerts en la combinació però en la posició incorrecta.

- x per els números que no estàn en la combinació.

````
public static string ActualitzarPista(int[] intent, int[] combinacioSecreta)
{
    // Lògica per generar la pista
}
````

**ImprimirColor**: Imprimeix el número en un color específic segons el seu valor.

````
public static void ImprimirColor(int numero)
{
    switch(numero)
    {
        case 1:
            Console.ForegroundColor = ConsoleColor.Magenta;
            break;
        // Més colors
        default:
            Console.ForegroundColor = ConsoleColor.White;
            break;
    }
    Console.Write(numero);
    Console.ResetColor();
}
````
