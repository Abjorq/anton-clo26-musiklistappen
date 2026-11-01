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

// Tips på ordning (se UPPGIFT.md):
//   1. Rita upp huvudmenyn (alternativ numrerade 1-N) och läs in ett val med Console.ReadLine().
//      Skriv ut "Här kommer en lista senare" tills databasen är kopplad.
//   2. Skapa ett DbHandler-objekt: new DbHandler(connectionString)
//   3. Anropa DbHandlers metoder från menyn. Här ska ALDRIG SQL stå.
//   4. Avsluta snyggt när användaren väljer "Avsluta".

DbHandler handler = new DbHandler(connectionString);

int choice = 0;

while (choice != 4)
{

    Console.WriteLine("Välkommen, vad vill du se?");
    Console.WriteLine("1.Visa låtar/album");
    Console.WriteLine("2.Lägg till låt/album");
    Console.WriteLine("3.Ta bort en post");
    Console.WriteLine("4.Avsluta");

    choice = Convert.ToInt32(Console.ReadLine());


    switch (choice)
    {
        case 1:
            Console.WriteLine("Visar alla låtar/album");
            var songs = handler.GetAllSongs();
            foreach (var song in songs)
            {
                Console.WriteLine($"{song.Id}. {song.Name} ({song.PlayTime} sekunder)");
            }
            break;

        case 2:
            Console.WriteLine("Lägg till en ny låt eller album");
            Console.Write("Namn: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("playtime: ");
            int playTime = Convert.ToInt32(Console.ReadLine());
            Console.Write("Albumid: ");
            int albumid = Convert.ToInt32(Console.ReadLine());

            var newSong = new Song
            {
                Name = name,
                PlayTime = playTime,
                AlbumId = albumid

            };

            handler.AddSong(newSong);
            Console.Write($"la till låten {name}");
            break;

        case 3:
            Console.WriteLine("Ta bort en post");
            var allSongs = handler.GetAllSongs();
            foreach (var s in allSongs)
            {
                Console.WriteLine($"{s.Id}. {s.Name}");
            }

            Console.Write("Id på låten som ska tas bort: ");
            int deleteId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Är du säker? (ja/nej): ");
            string answer = Console.ReadLine() ?? "";

            if (answer.ToLower() == "ja")
            {
                bool deleted = handler.DeleteSong(deleteId);
                if (deleted)
                {
                    Console.WriteLine("Låten togs bort.");
                }
                else
                {
                    Console.WriteLine("Hittade ingen låt med det id:t.");
                }
            }
            else
            {
                Console.WriteLine("Avbröt.");
            }
            break;

        case 4:
            Console.WriteLine("Avslutat.");
            break;

        default:
            Console.WriteLine("Ett fel uppstod");
            break;
    }
}