using MahApps.Metro.Controls;
using Scripter.Settings;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Scripter.DialogWindow
{
    /// <summary>
    /// Логика взаимодействия для NewScriptFound.xaml
    /// </summary>
    public partial class NewScriptFound : MetroWindow
    {
        public StateScript State = StateScript.Pase;
        public string FileName;
        public string NameScript;
        public string Description;
        public int Color;
        public bool EnableScript;

        public NewScriptFound(string fileName)
        {
            InitializeComponent();
            FileName = fileName;
            FullName.Text = $@"Файл: {FileName}";
            NameScript = System.IO.Path.GetFileNameWithoutExtension(FileName);
            NameScriptText.Text = NameScript;
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color = int.Parse(ClrPcker_Background.A.ToString() + ClrPcker_Background.R.ToString() + ClrPcker_Background.G.ToString() + ClrPcker_Background.B.ToString());
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            State = StateScript.Add;
            NameScript = System.IO.Path.GetFileNameWithoutExtension(FileName);
            Description = TextDescription.Text;
            EnableScript = ToogleEnableScript.IsOn;
            Close();
        }

        private void Allow_btn_Click(object sender, RoutedEventArgs e)
        {
            State = StateScript.Pase;
            NameScript = System.IO.Path.GetFileNameWithoutExtension(FileName);
            Description = TextDescription.Text;
            EnableScript = ToogleEnableScript.IsOn;
            Close();
        }

        private void GoToContainer_btn_Click(object sender, RoutedEventArgs e)
        {
            State = StateScript.Container;
            NameScript = System.IO.Path.GetFileNameWithoutExtension(FileName);
            Description = TextDescription.Text;
            EnableScript = ToogleEnableScript.IsOn;
            Close();
        }
    }
}
