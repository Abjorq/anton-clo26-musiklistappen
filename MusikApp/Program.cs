using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using MusikApp;

// Läser inställningar från appsettings.json (samma sätt som i hero_databas).
// Databasens sökväg ligger där, så du slipper skriva den i koden.
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Hittar ingen connection string 'Default' i appsettings.json.");

// Börja här. Den här raden visar att inställningarna lästes in, ta bort den när du vill.
Console.WriteLine($"Musiklistappen: börja med att få menyn att fungera!  (databas: {connectionString})");

// Tips på ordning (se UPPGIFT.md):
//   1. Rita upp huvudmenyn (alternativ numrerade 1-N) och läs in ett val med Console.ReadLine().
//      Skriv ut "Här kommer en lista senare" tills databasen är kopplad.
//   2. Skapa ett DbHandler-objekt: new DbHandler(connectionString)
//   3. Anropa DbHandlers metoder från menyn. Här ska ALDRIG SQL stå.
//   4. Avsluta snyggt när användaren väljer "Avsluta".

DbHandler handler = new DbHandler(connectionString);

Console.WriteLine("Välkommen, vad vill du se?");
Console.WriteLine("Visa låtar/album");
Console.WriteLine("Lägg till låt/album");
Console.WriteLine("Ta bort en post");
Console.WriteLine("Avsluta");

int choice = Convert.ToInt32(Console.ReadLine());




while (choice != 4)
{

    Console.WriteLine("Visa låtar/album");
    Console.WriteLine("Lägg till låt/album");
    Console.WriteLine("Ta bort en post");
    Console.WriteLine("Avsluta");

    switch (choice)
    {
        case 1:
            Console.WriteLine("Visar alla låtar/album");
            break;

        case 2:
            Console.WriteLine("La till en ny låt eller album");

            break;

        case 3:
            Console.WriteLine("Tog bort en post");
            break;

        case 4:
            Console.WriteLine("Avslutat.");
            break;

        default:
            Console.WriteLine("Ett fel uppstod");
            break;
    }
}