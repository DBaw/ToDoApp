using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using ToDoApp.ViewModels;

namespace ToDoApp
{
    public class ViewLocator : IDataTemplate
    {

        public Control? Build(object? data)
        {
            if (data is null)
                return null;
            var fullname = data.GetType().FullName!;
            var name = fullname.Replace("ViewModel", "View", StringComparison.Ordinal);
            var type = Type.GetType(name);

            if (type != null)
            {
                var control = (Control)Activator.CreateInstance(type)!;
                control.DataContext = data;
                return control;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object? data)
        {
            return data is ObservableObject;
        }
    }
}
