using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Label = System.Reflection.Emit.Label;
using MemoryGame.Pages;

namespace MemoryGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }

    private void StartGame(object sender, RoutedEventArgs e) {
        Console.WriteLine(Diffuculty.Text);
        if (Diffuculty.Text == "") {
            FlashLabel();
        }
        else if (Diffuculty.Text == "Easy") {
            Game.Content = new EasyDif();
        }
        else if (Diffuculty.Text == "Moderate") {
            Game.Content = new ModerateDif();
        }
        else if (Diffuculty.Text == "Hard") {
            Game.Content = new HardDif();
        }
    }

    private void FlashLabel() {
        var flashStoryboard = FindResource("FlashStoryboard") as Storyboard;
        flashStoryboard?.Begin();
    }
}