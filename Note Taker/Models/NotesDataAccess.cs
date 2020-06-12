using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Note_Taker.Models
{
    public class NotesDataAccess
    {
        // testing statement- System.Diagnostics.Debug.WriteLine("");
        // TEST, TYPE, DATE ,CONTENT
        #region LoadNotes
        /**
         * Loads all the notes in the database in output and is sent back in the list format. 
         **/
        public List<Note> LoadNotes()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Note>("select * from NoteTable", new DynamicParameters());
                return output.ToList();
            }
        }
        #endregion

        #region SaveNote
        /**
         * Saves a note in the database based on  values provided by the CurrentNote object.
         **/
        public void SaveNote(Note CurrentNote)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into NoteTable(Title, Type, Date, Content) values(@Title, @Type, @Date, @Content)", CurrentNote);
            }
        }
        #endregion

        #region DeleteNote
        /**
         * Deletes the note from the database based on the Id provided.
         * MIght have to implement more error checking.
         **/
        public bool DeleteNote(int IdToBeDeleted)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Execute("DELETE FROM NoteTable WHERE Id =" + IdToBeDeleted);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion

        #region Update Note
        /**
         * Updates the note in the database Based on the object provided.
         * The id cannot be updated and will remain the same. SO we look for that object and 
         * update all the fields.
         * MIght have to implement more error checking.
         **/
        public bool UpdateNote(Note CurrentNote)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Execute("update NoteTable set Title = '" + CurrentNote.Title + "', Type = '" + CurrentNote.Type + "', " +
                        "Date = '" + CurrentNote.Date + "', Content = '" + CurrentNote.Content + "' WHERE Id = " + CurrentNote.Id);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion

        // We might actually not need this. let's see if we will need this in future.
        #region Search
        /**
         * Searches for the Note in the database based on the Id provided.
         * MIght have to implement more error checking.
         **/
        public bool SearchNote(int Id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Execute("");
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion

        // Gets the right connection string from app.config file. 
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
