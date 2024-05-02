using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MemoryGame.Pages;

public partial class ModerateDif : Page {
    private List<UIElement> _buttons = [];
    private List<Button> _buttonList = [];
    private readonly int _maxLen = 10;
    private int _currentId = 0;
    private readonly Random _random = new Random();

    private bool _canPass = false;
    private bool _gameOver = false;

    private Button GetRandButton() {
        var len = _buttons.Count;
        
        return (Button)_buttons[_random.Next(0, len)];
    }

    private void DisableAllButtons() {
        if (_buttons.Count != 0) {
            this.Dispatcher.Invoke(() => {
                foreach (UIElement button in _buttons) {
                    button.IsEnabled = false;
                }
            });
        }
    }
    
    private void EnableAllButtons() {
        if (_buttons.Count != 0) {
            this.Dispatcher.Invoke(() => {
                foreach (UIElement button in _buttons) {
                    button.IsEnabled = true;
                }
            });
        }
    }

    private void FindAllChildren() {
        this.Dispatcher.Invoke(() =>{
            var grid = ModerateButtons;

            foreach (Button button in grid.Children) {
                _buttons.Add(button);
            }
        });
    }

    public ModerateDif() {
        InitializeComponent();
    }

    private void FlashButtons() {
        Dispatcher.Invoke(DispatcherPriority.Background, async () => {
            DisableAllButtons();
            foreach (Button button in _buttonList) {
                button.Opacity = 0;
                await Task.Delay(1000);
                button.Opacity = 1;
                await Task.Delay(500);
            }
            EnableAllButtons();
        });
    }

    public void Game() {
        FindAllChildren();

        _buttonList.Add(GetRandButton());

        FlashButtons();

        do {
            if (!_canPass) continue;
            if (_gameOver) {
                DisableAllButtons();
                MessageBox.Show("Game over!", "Failed", MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            _canPass = false;
            _currentId++;

            if (_currentId == _buttonList.Count && _buttonList.Count < _maxLen) {
                _currentId = 0;
                _buttonList.Add(GetRandButton());

                FlashButtons();
            }
        }  while (_buttonList.Count < _maxLen);

        this.Dispatcher.Invoke(() => {
            ModerateButtons.Visibility = Visibility.Hidden;
        });
        MessageBox.Show("You won!", "Success", MessageBoxButton.OK,MessageBoxImage.Information);
    }


    private void CheckButton(object sender, RoutedEventArgs e) {
        if (sender.Equals(_buttonList[_currentId])) {
            _canPass = true;
        }
        else {
            _canPass = true;
            _gameOver = true;
        }
    }
}