using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biljeske
{
    class BiljeskeDB
    {
        public static string mycon= "server =localhost; Uid=root; password = *; persistsecurityinfo = True; database =biljeske; SslMode = none";
        public static MySqlConnection conn = new MySqlConnection(mycon);
        public static MySqlCommand cmd = null;
        public BiljeskeDB()
        {
            mycon = "server =localhost; Uid=root; password = Vukosav99; persistsecurityinfo = True; database =biljeske; SslMode = none";
            conn = new MySqlConnection(mycon);
        }

        public static List<Biljeska> GetBussinesNotes()
        {
            List<Biljeska> rezultat = new List<Biljeska>();
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `biljeska` where tip=@tip ORDER BY vrijemeKreiranja desc";
            cmd.Parameters.AddWithValue("tip", (int)Tip.Posao);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rezultat.Add(new Biljeska(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4), reader.GetDateTime(3), reader.GetBoolean(5)));
                
            }
            reader.Close();
            conn.Close();
            return rezultat;

        }
        public static List<Biljeska> GetFunNotes()
        {
            List<Biljeska> rezultat = new List<Biljeska>();
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `biljeska` where tip=@tip ORDER BY vrijemeKreiranja desc";
            cmd.Parameters.AddWithValue("tip", (int)Tip.Zabava);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rezultat.Add(new Biljeska(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4), reader.GetDateTime(3), reader.GetBoolean(5)));

            }
            reader.Close();
            conn.Close();
            return rezultat;

        }
        public static List<Biljeska> GetOtherNotes()
        {
            List<Biljeska> rezultat = new List<Biljeska>();
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `biljeska` where tip=@tip ORDER BY vrijemeKreiranja desc";
            cmd.Parameters.AddWithValue("tip", (int)Tip.Ostalo);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rezultat.Add(new Biljeska(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4), reader.GetDateTime(3), reader.GetBoolean(5)));

            }
            reader.Close();
            conn.Close();
            return rezultat;

        }
        public static Biljeska GetNoteById(int id)
        {
            Biljeska rezultat =null;
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `biljeska`  where idBiljeska=@ID";
            cmd.Parameters.AddWithValue("@ID", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rezultat = new Biljeska(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4), reader.GetDateTime(3), reader.GetBoolean(5));

            }
            reader.Close();
            conn.Close();
            return rezultat;

        }
        public static List<Biljeska> GetNotes()
        {
            List<Biljeska> rezultat = new List<Biljeska>();
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `biljeska`  where daLiJeIzbrisano=false ORDER BY vrijemeKreiranja asc";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rezultat.Add(new Biljeska(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4), reader.GetDateTime(3), reader.GetBoolean(5)));

            }
            reader.Close();
            conn.Close();
            return rezultat;

        }
        public static List<Biljeska> GetDeletedNotes()
        {
            List<Biljeska> rezultat = new List<Biljeska>();
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `biljeska`  where daLiJeIzbrisano=true ORDER BY vrijemeKreiranja asc";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rezultat.Add(new Biljeska(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4), reader.GetDateTime(3), reader.GetBoolean(5)));

            }
            reader.Close();
            conn.Close();
            return rezultat;

        }
        public static Biljeska InsertNote(Biljeska note)
        {
            conn.Open();

            cmd = conn.CreateCommand();
            cmd.CommandText =
                @"insert into biljeska(naslov,sadrzaj,tip,vrijemeKreiranja) values (@naslov, @sadrzaj, @tip,now())";
            cmd.Parameters.AddWithValue("@naslov", note.naslov);
            cmd.Parameters.AddWithValue("@sadrzaj", note.sadrzaj);
            cmd.Parameters.AddWithValue("@tip",(int) note.tip);
            cmd.ExecuteNonQuery();
           
            //note.idBiljeska = (int)cmd.LastInsertedId;
            conn.Close();
           return GetNoteById((int)cmd.LastInsertedId);

        }
        public static void DeleteNote(int noteId)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "update biljeska set daLiJeIzbrisano='1' where idBiljeska=@ID";
            cmd.Parameters.AddWithValue("@ID", noteId);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateNote(Biljeska note)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "update biljeska set naslov=@naslov,sadrzaj=@sadrzaj,vrijemeKreiranja=now(),tip=@tip where idBiljeska=@ID";
            cmd.Parameters.AddWithValue("@ID", note.idBiljeska);
            cmd.Parameters.AddWithValue("@naslov", note.naslov);
            cmd.Parameters.AddWithValue("@sadrzaj", note.sadrzaj);
            cmd.Parameters.AddWithValue("@tip", note.tip);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        internal static void RestoreNote(int idNote)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "update biljeska set daLiJeIzbrisano=false where idBiljeska=@ID";
            cmd.Parameters.AddWithValue("@ID", idNote);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
