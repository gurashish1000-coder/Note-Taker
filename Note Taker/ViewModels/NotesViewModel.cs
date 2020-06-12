using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Note_Taker.Models;
using System.Security.Cryptography.X509Certificates;
using Note_Taker.Commands;
using System.Collections.ObjectModel;
using PropertyChanged;
using System.Printing.Interop;

namespace Note_Taker.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NotesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICollectionView CollectionView { get; set; }
        public System.Windows.Input.ICommand DoubleClickCommand { get; set; }

        NoteService ObjNoteService;
        public NotesViewModel()
        {
            ObjNoteService = new NoteService();
            LoadData();
            CurrentNote = new NoteDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            deleteCommand = new RelayCommand(Delete);
            updateCommand = new RelayCommand(Update);

        }

        #region DataGrid Display Code
        private ObservableCollection<NoteDTO> notesList;
        public ObservableCollection<NoteDTO> NotesList
        {
            get { return notesList; }
            set { notesList = value;  }
        }
        private void LoadData()
        {
            NotesList = new ObservableCollection<NoteDTO>(ObjNoteService.GetAll());
            
        }
        #endregion

        // Current Note property
        #region CurrentNote
        private NoteDTO currentNote;
        public NoteDTO CurrentNote
        {
            get { return currentNote; }
            set { currentNote = value; }
        }
        #endregion

        // Method to display notify message in the app.
        #region Message
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        #endregion

        // IMPLEMENTING THE SAVE COMMAND HERE
        #region SAVE COMMAND 
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            try
            {
                // SO the issue was that I was storing the refernce of the object in the list
                // That is why all the newly added objects in the list were changing. 
                NoteDTO demo = new NoteDTO() { Content = "",Type = "", Id = 0, Title= "", Date = DateTime.Today.ToString() };
                demo.Content = CurrentNote.Content;
                demo.Id = CurrentNote.Id;
                demo.Title = CurrentNote.Title;
                demo.Type = CurrentNote.Type;
                var IsSaved = ObjNoteService.Add(demo);
                if (IsSaved)
                {
                    Message = "Note Saved";
                    LoadData();
                } else
                {
                    Message = "Save failed";
                }
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
        }
        #endregion

        // iMPLEMENTTING THE DELETE COMMAND HERE 
        #region DELETE COMMAND
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void Delete()
        {
            try
            {
                var result = ObjNoteService.Delete(CurrentNote.Id);
                if (result==true)
                {
                    Message = "Note Deleted";
                    LoadData();
                }
                else
                {
                    Message = "Delete Operation failed. (Note May not exist)";
                }

            }
            catch (Exception ex)
            {

                Message = ex.Message;

            }

        }
        #endregion

        // iMPLEMENTTING THE Search COMMAND HERE 
        #region Search COMMAND
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }
        // So what I can implement here is that I open a new window to show the note. 
        // I'll do that later tho.
        public void Search()
        {
            try
            {
                var ObjNote = ObjNoteService.Search(CurrentNote.Id);
                if (ObjNote != null)
                {
                    CurrentNote.Title = ((NoteDTO)ObjNote).Title;
                    CurrentNote.Content = ((NoteDTO)ObjNote).Content;
                    CurrentNote.Date = ((NoteDTO)ObjNote).Date;
                    CurrentNote.Id = ((NoteDTO)ObjNote).Id;
                    CurrentNote.Type = ((NoteDTO)ObjNote).Type;
                    Message = "Note found";
                    LoadData();
                }
                else
                {
                    Message = "Note not found";
                }
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }

        }
        #endregion

        // iMPLEMENTTING THE UPDATE COMMAND HERE 
        #region UPDATE COMMAND
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }
        // So what I can implement here is that I open a new window to show the note. 
        // I'll do that later tho.
        public void Update()
        {
            try
            {
                var IsUpdated = ObjNoteService.Update(CurrentNote);
                if (IsUpdated)
                {
                    Message = "Note Updated";
                    LoadData();
                }
                else
                {
                    Message = "Note not Updated";
                }
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }

        }
        #endregion

    }
}
