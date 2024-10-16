using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ToDoApp.ViewModels
{
    [ObservableRecipient]
    public partial class AddNotePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _noteTitle;

        [ObservableProperty]
        private string? _noteContent;

        [ObservableProperty]
        private IBrush _selectedColor = Brushes.Gray;

        [RelayCommand(CanExecute =nameof(CanConfirm))]
        private void Confirm()
        {
            // Handle note confirmation logic here
        }
        private bool CanConfirm => !string.IsNullOrEmpty(NoteTitle);

        [RelayCommand]
        private void Cancel()
        {
            // Handle cancel logic here
        }

        [RelayCommand]
        private void ChangeColor(object sender) 
        {
            RadioButton? rd = sender as RadioButton;
            string color = rd?.Background?.ToString() ?? "Gray";
            switch (color) 
            {
                case ("Gray"):
                    SelectedColor = Brushes.Gray;
                    return;
                case ("Yellow"):
                    SelectedColor = Brushes.Yellow;
                    return;
                case ("LightGreen"):
                    SelectedColor = Brushes.LightGreen;
                    return;
                case ("LightBlue"):
                    SelectedColor = Brushes.LightBlue;
                    return;
                case ("Purple"):
                    SelectedColor = Brushes.Purple;
                    return;
                case ("Red"):
                    SelectedColor = Brushes.Red;
                    return;
            }
        }
    }
}
