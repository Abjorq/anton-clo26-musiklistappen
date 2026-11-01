using Microsoft.Data.Sqlite;

namespace MusikApp;

///<summary>
/// ALL databaskod bor här. Program.cs ska aldrig innehålla SQL eller SqliteConnection.
///
/// Det här är ett skelett: metoderna finns, kommentarerna visar vad varje metod ska göra,
/// men koden är din. Alla metoder kastar NotImplementedException tills du har skrivit dem.
///
/// ADO.NET i fem steg (samma i varje metod som pratar med databasen):
///   1. Öppna en anslutning        -> new SqliteConnection(...) och connection.Open()
///   2. Skapa ett kommando         -> connection.CreateCommand()
///   3. Skriv SQL:en               -> command.CommandText = "..."
///   4. Skicka in värden säkert    -> command.Parameters.AddWithValue("@namn", värde)
///   5. Kör kommandot              -> ExecuteReader() för SELECT, ExecuteNonQuery() för INSERT/DELETE
///
/// Använd "using var" på anslutning och kommando så stängs de av sig själva.
/// Bygg SQL:en med parametrar (@titel), aldrig genom att klistra ihop strängar.
/// 
/// 
///</summary>


public class DbHandler
{

    private readonly string thisConnectionString;

    // Tips: spara bara connection string här. Den kommer från appsettings.json
    // via Program.cs (t.ex. "Data Source=musik.db").
    // Öppna sedan en NY anslutning inuti varje metod nedan.

    public DbHandler(string connectionString)
    {

        thisConnectionString = connectionString;

        // TODO: spara connectionString i ett fält som metoderna nedan kan använda.
        //throw new NotImplementedException();
    }

    // ---------------------------------------------------------------
    // G-nivå: du behöver minst tre av de fem här (välj själv vilka)
    // ---------------------------------------------------------------

    /// <summary>Hämtar alla låtar, sorterade på titel.</summary>
    public List<Song> GetAllSongs()
    {
        var songs = new List<Song>();

        using var connection = new SqliteConnection(thisConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT songid, name, playTime, albumid FROM SONGS ORDER BY name";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var song = new Song
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                PlayTime = reader.GetInt32(2),
                AlbumId = reader.GetInt32(3)
            };
            songs.Add(song);
        }

        return songs;
        //throw new NotImplementedException();
    }

    /// <summary>Hämtar en specifik låt. Returnerar null om den inte finns.</summary>
    public Song? GetSongById(int id)
    {
        // TODO: SELECT ... WHERE Id = @id (parameter!).
        // TODO: if (reader.Read()) { ... } annars returnera null.
        throw new NotImplementedException();
    }

    /// <summary>Söker låtar där titeln innehåller söktexten.</summary>
    public List<Song> SearchSongs(string searchText)
    {
        // TODO: SELECT ... WHERE Titel LIKE @sok.
        // Tips: parametervärdet ska vara "%" + searchText + "%" så att LIKE matchar delar av titeln.
        throw new NotImplementedException();
    }

    /// <summary>Lägger till en ny låt.</summary>
    public void AddSong(Song song)
    {

        using var connection = new SqliteConnection(thisConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO SONGS (name, playTime, albumid) VALUES (@name, @playTime, @albumid)";

        command.Parameters.AddWithValue("@name", song.Name);
        command.Parameters.AddWithValue("@playTime", song.PlayTime);
        command.Parameters.AddWithValue("@albumid", song.AlbumId);

        command.ExecuteNonQuery();
        // TODO: INSERT INTO ... VALUES (@titel, @langd, ...) med parametrar.
        // Tips: INSERT ger ingen data tillbaka, så använd ExecuteNonQuery().
        //throw new NotImplementedException();
    }

    /// <summary>Tar bort en låt. Returnerar true om en rad faktiskt togs bort.</summary>
    public bool DeleteSong(int id)
    {

        using var connection = new SqliteConnection(thisConnectionString);
        connection.Open();

        using var deleteLinks = connection.CreateCommand();
        deleteLinks.CommandText = "DELETE FROM SONG_CONTRIBUTORS WHERE songid = @id";
        deleteLinks.Parameters.AddWithValue("@id", id);
        deleteLinks.ExecuteNonQuery();

        using var deleteSong = connection.CreateCommand();
        deleteSong.CommandText = "DELETE FROM SONGS WHERE songid = @id";
        deleteSong.Parameters.AddWithValue("@id", id);
        int rowsAffected = deleteSong.ExecuteNonQuery();

        return rowsAffected > 0;
        // TODO: DELETE FROM ... WHERE Id = @id.
        // Tips: ExecuteNonQuery() returnerar antalet påverkade rader.
        // Menyn i Program.cs ska fråga "Är du säker? j/n" innan den här metoden anropas.
        //throw new NotImplementedException();
    }

    // ---------------------------------------------------------------
    // VG-nivå: du behöver minst EN av dessa två (och reflektionen i README)
    // ---------------------------------------------------------------

    /// <summary>(VG, alternativ A) Låtar tillsammans med albumets namn. Kräver en JOIN.</summary>
    public List<(string SongTitle, string AlbumName)> GetSongsWithAlbum()
    {
        // TODO: SELECT Låtar.Titel, Album.Namn FROM Låtar JOIN Album ON ... ORDER BY ...
        throw new NotImplementedException();
    }

    /// <summary>(VG, alternativ B) Låtar som är längre än medellängden. Kräver en subquery.</summary>
    public List<Song> GetSongsLongerThanAverage()
    {
        // TODO: SELECT ... WHERE Längd > (SELECT AVG(Längd) FROM Låtar).
        throw new NotImplementedException();
    }
}
