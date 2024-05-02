using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
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
        switch (Diffuculty.Text) {
            case "":
                FlashLabel();
                break;
            case "Easy": {
                var game = new EasyDif();
                Game.Content = game;
            
                new Thread(() => {
                    Thread.CurrentThread.IsBackground = true; 
                    game.Game();
                }).Start();
                break;
            }
            case "Moderate": {
                var game = new ModerateDif();
                Game.Content = game;
                new Thread(() => {
                    Thread.CurrentThread.IsBackground = true;
                    game.Game();
                }).Start();
                break;
            }
            case "Hard": {
                var game = new HardDif();
                Game.Content = game;
                new Thread(() => {
                    Thread.CurrentThread.IsBackground = true;
                    game.Game();
                }).Start();
                break;
            }
        }
    }

    private void FlashLabel() {
        var flashStoryboard = FindResource("FlashStoryboard") as Storyboard;
        flashStoryboard?.Begin();
    }

    private void Game_OnNavigating(object sender, NavigatingCancelEventArgs e) {
        if (e.NavigationMode is NavigationMode.Forward or NavigationMode.Back) {
            e.Cancel = true;
        }
    }
}