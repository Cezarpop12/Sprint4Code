using InterfaceLib;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALMSSQLSERVER
{
    public class OnderdeelMSSQLDAL : Database, IOnderdeelContainer
    {
        /// <summary>
        ///Voeg de waardes in OutfitTabel, specificeer welk gebruikerID de outfit toevoegd dmv de alias
        /// </summary>

        public void VoegOnderdeelToe(GebruikerDTO gebruiker, OnderdeelDTO onderdeel)
        {
            try
            {
                OpenConnection();
                if (!BestaandeTitleNaamOnder(onderdeel.Titel))
                {
                    OpenConnection();
                    string query = @"INSERT INTO Onderdeel VALUES((SELECT GebrID FROM Gebruiker WHERE Alias = @alias), @titel, @prijs, @category, @path)";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    command.Parameters.AddWithValue("@alias", gebruiker.Alias);
                    command.Parameters.AddWithValue("@titel", onderdeel.Titel);
                    command.Parameters.AddWithValue("@prijs", onderdeel.Prijs);
                    command.Parameters.AddWithValue("@category", onderdeel.DeCategory.ToString());
                    command.Parameters.AddWithValue("@path", onderdeel.FileAdress);
                    command.ExecuteNonQuery();
                }
                CloseConnection();
            }
            catch (InvalidOperationException ex)
            {
                throw new TemporaryExceptions("Fout met de verbinding");
            }
            catch (Exception ex) //Toegang tot de exceptie class
            {
                throw new PermanentExceptions("Iets gaat hier fout!");
            }
        }
        
        public List<OnderdeelDTO> GetAllOnderdelenVanGebr(string alias)
        {
            try
            {
                List<OnderdeelDTO> Onderdelen = new List<OnderdeelDTO>();
                OpenConnection();
                SqlCommand command = new SqlCommand(@"SELECT * FROM Onderdeel WHERE GebrID = @id", this.connection);
                command.Parameters.AddWithValue("@id", GetUserID(alias));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Onderdelen.Add(new OnderdeelDTO(
                        reader["Titel"].ToString(),
                        Convert.ToInt32(reader["Prijs"]),
                        reader["FileAdress"].ToString(),
                        (OnderdeelDTO.OnderdeelCategory)Enum.Parse(typeof(OnderdeelDTO.OnderdeelCategory), reader["Categorie"].ToString())));
                    }
                }
                CloseConnection();
                return Onderdelen;
            }
            catch (InvalidOperationException ex)
            {
                throw new TemporaryExceptions("Fout met de verbinding");
            }
            catch (Exception ex) //Toegang tot de exceptie class
            {
                throw new PermanentExceptions("Iets gaat hier fout!");
            }
        }

        public List<OnderdeelDTO> GetAllOnderdelen()
        {
            try
            {
                List<OnderdeelDTO> Onderdelen = new List<OnderdeelDTO>();
                OpenConnection();
                SqlCommand command = new SqlCommand(@"SELECT * FROM Onderdeel", this.connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Onderdelen.Add(new OnderdeelDTO(
                        reader["Titel"].ToString(),
                        Convert.ToInt32(reader["Prijs"]),
                        reader["FileAdress"].ToString(),
                        (OnderdeelDTO.OnderdeelCategory)Enum.Parse(typeof(OnderdeelDTO.OnderdeelCategory), reader["Categorie"].ToString())));
                    }
                }
                CloseConnection();
                return Onderdelen;
            }
            catch (InvalidOperationException ex)
            {
                throw new TemporaryExceptions("Fout met de verbinding");
            }
            catch (Exception ex) //Toegang tot de exceptie class
            {
                throw new PermanentExceptions("Iets gaat hier fout!");
            }
        }
        
        public void DeleteOnderdeel(string titel)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand(@"DELETE FROM Onderdeel WHERE ID = @id", this.connection);
                command.Parameters.AddWithValue("@id", GetOnderdeelID(titel));
                command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (InvalidOperationException ex)
            {
                throw new TemporaryExceptions("Fout met de verbinding");
            }
            catch (Exception ex) //Toegang tot de exceptie class
            {
                throw new PermanentExceptions("Iets gaat hier fout!");
            }
        }
        
        public void UpdateOnderdeel(OnderdeelDTO onderdeel)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand(@"UPDATE Onderdeel SET Prijs = @prijs, Categorie = @categorie WHERE Titel = @titel", this.connection);
                command.Parameters.AddWithValue("@titel", onderdeel.Titel);
                command.Parameters.AddWithValue("@prijs", onderdeel.Prijs);
                command.Parameters.AddWithValue("@categorie", onderdeel.DeCategory);
                command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (InvalidOperationException ex)
            {
                throw new TemporaryExceptions("Fout met de verbinding");
            }
            catch (Exception ex) //Toegang tot de exceptie class
            {
                throw new PermanentExceptions("Iets gaat hier fout!");
            }
        }

        public bool IsOnderdeel(string titel)
        {
            try
            {
                bool check = false;
                OpenConnection();
                string query = @"SELECT * FROM Onderdeel WHERE Titel = @titel";
                SqlCommand command = new SqlCommand(query, this.connection);
                command.Parameters.AddWithValue("@titel", titel);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    check = true;
                }
                CloseConnection();
                return check;
            }
            catch (InvalidOperationException ex)
            {
                throw new TemporaryExceptions("Fout met de verbinding");
            }
            catch (Exception ex) //Toegang tot de exceptie class
            {
                throw new PermanentExceptions("Iets gaat hier fout!");
            }
        }

        /// <summary>
        /// GetOnderdeel ga ik later gebruiken voor filteren bijv, zoeken op naam etc.
        /// </summary>

        public OnderdeelDTO GetOnderdeel(string titel)
        {
            try
            {
                OnderdeelDTO onderdeel = null;
                //Recap
                OpenConnection();
                SqlCommand command = new SqlCommand(@"SELECT * FROM Onderdeel WHERE Titel = @titel", this.connection);
                command.Parameters.AddWithValue("@titel", titel);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        onderdeel = new OnderdeelDTO(
                        reader["Titel"].ToString(),
                        Convert.ToInt32(reader["Prijs"]),
                        reader["FileAdress"].ToString(),
                        (OnderdeelDTO.OnderdeelCategory)Enum.Parse(typeof(OnderdeelDTO.OnderdeelCategory), reader["Categorie"].ToString()));
                    }
                }
                CloseConnection();
                return onderdeel;
            }
            catch (InvalidOperationException ex)
            {
                throw new TemporaryExceptions("Fout met de verbinding");
            }
            catch (Exception ex) //Toegang tot de exceptie class
            {
                throw new PermanentExceptions("Iets gaat hier fout!");
            }
        }
    }

}
