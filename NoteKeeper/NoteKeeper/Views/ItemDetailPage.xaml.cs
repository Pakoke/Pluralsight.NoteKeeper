using NoteKeeper.Models;
using NoteKeeper.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NoteKeeper.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }

        private void NoteCourse_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Picker oPicker = (Picker)sender;
            //oPicker..Text = ((Course)oPicker.SelectedItem).Id.ToString();
        }
    }
}