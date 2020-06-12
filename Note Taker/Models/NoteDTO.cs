using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace Note_Taker.Models
{
    public class NoteDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        // 4 properties declared for every note type.
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; OnPropertyChanged("Type"); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged("Content"); }
        }





    }
}
