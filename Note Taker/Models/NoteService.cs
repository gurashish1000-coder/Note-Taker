using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Note_Taker.Models
{
    public class NoteService
    {
        NotesDataAccess ObjContext;
        private static List<NoteDTO> ObjNotesList;
        public NoteService()
        {
            ObjContext = new NotesDataAccess();
            ObjNotesList = new List<NoteDTO>();

        }
        #region GetAll method implementation
        /**
         * This method gets all the Note items from the database and puts it into 
         * ObjNotesList. This list is recreated everytime GetAll is called.
         * A new NoteDTO object is created before the objects is added to list ObjNotesList
         * which is of type List<NoteDTO>. And then the list is returned.
         **/
        public List<NoteDTO> GetAll()
        {
            // Okay so this works.           
            List<Note> testList = ObjContext.LoadNotes();
            ObjNotesList = new List<NoteDTO>();
            for (int i = 0; i < testList.Count; i++)
            {
                Note Current = testList[i];
                ObjNotesList.Add(new NoteDTO { Id = Current.Id, Date = Current.Date, Title = Current.Title, Content = Current.Content, Type = Current.Type });
            }
            return ObjNotesList;
        }
        #endregion

        #region Add method implementation
        /**
         * This method adds the new note object in the database.
         * It first creates a new Note objects and then manually assigns
         * the values NoteDTO object to this object. (NoteDTO - Note Data Transfer Object)
         **/
        public bool Add(NoteDTO ObjNewNote)
        {
            // Note to be added to the database  
            Note NewNote = new Note();
            ObjNewNote.Date = DateTime.Today.ToString();
            NewNote.Title = ObjNewNote.Title;
            NewNote.Type = ObjNewNote.Type;
            NewNote.Date = ObjNewNote.Date;
            NewNote.Content = ObjNewNote.Content;

            //ObjNotesList.Add(ObjNewNote);
            ObjContext.SaveNote(NewNote);
            return true;
        }
        #endregion

        #region Update method implementation
        /**
         * This method will be worked on later
         * 
         * This method adds the new note object in the database.
         * It first creates a new Note objects and then manually assigns
         * the values NoteDTO object to this object. (NoteDTO - Note Data Transfer Object)
         **/
        public bool Update(NoteDTO NoteToUpdate)
        {
            // Note to be updated in the database 
            Note NewNote = new Note();

            NewNote.Id = NoteToUpdate.Id;
            NewNote.Title = NoteToUpdate.Title;
            NewNote.Type = NoteToUpdate.Type;
            NewNote.Date = DateTime.Today.ToString();       // Since the Note is getting updated, the update time will be shown. 
            NewNote.Content = NoteToUpdate.Content;

            // Calling htemethod from NoteDataAcces to updated the note in the database. 
            if (ObjContext.UpdateNote(NewNote))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Delete method implementation
        public bool Delete(int Id)
        {
            // Need to do error checking here. 
            bool result = false;
            result = ObjContext.DeleteNote(Id);
            return result;
        }
        #endregion

        #region Search method implementation
        // Returns the Note object from the list
        public Object Search(int Id)
        {
            // Need to do error checking here. 

            for (int i = 0; i < ObjNotesList.Count; i++)
            {
                if (ObjNotesList[i].Id == Id)
                {
                    return ObjNotesList[i];
                }
            }
            return null;
        }
        #endregion

    }
}
