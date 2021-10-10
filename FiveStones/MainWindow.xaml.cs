using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FiveStones
{
    public enum StoneColor
    {
        Black,
        White,
        None
    }

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        const int STONE_WIDTH = 32;
        const int STONE_BASE_MAGIN = 38;
        const int STONE_COUNT = 19;
        Stone[,] stones = new Stone[STONE_COUNT, STONE_COUNT];
        StoneColor Turn = StoneColor.Black;
        int Turn_Count = 0;

        void Log(string title, object content)
        {
            Console.WriteLine($"[{title}] >> {content}");
        }

        void Reset()
        {
            Log("Reset", "Start");
            Turn_Count = 0;
            Turn = StoneColor.Black;
            for (int i = 0; i < STONE_COUNT; i++)
            {
                for (int j = 0; j < STONE_COUNT; j++)
                {
                    stones[j, i].Width = STONE_WIDTH;
                    stones[j, i].Height = STONE_WIDTH;
                    stones[i, j].Color = StoneColor.None;
                }
            }
            Log("Reset", "Finish");
        }

        void Start()
        {

        }

        void Win(StoneColor color)
        {
            WinTextGrid.Visibility = Visibility.Visible;
            string c = "W H I T E";
            switch (color)
            {
                case StoneColor.Black:
                    c = "B L A C K";
                    break;
                case StoneColor.White:
                    break;
            }
            WinText.Text = $"{c}  W I N !";
            Log("Win", color);
        }

        bool Check(int x, int y)
        {
            int Stack;


            for (int j = 0 ; j < 5; j++)
            {
                Stack = 0;
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        if (stones[x + i - j, y].Color != Turn)
                        {
                            break;
                        }
                        Stack++;
                    }
                    catch
                    {
                        break;
                    }
                }
                if (Stack >= 5)
                {
                    Win(Turn);
                    return true;
                }
                Stack = 0;
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        if (stones[x, y + i - j].Color != Turn)
                        {
                            break;
                        }
                        Stack++;
                    }
                    catch
                    {
                        break;
                    }
                }
                if (Stack >= 5)
                {
                    Win(Turn);
                    return true;
                }
            }
            
            for (int j = 0 ; j < 5; j++)
            {
                Stack = 0;
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        if (stones[x + i - j, y + i - j].Color != Turn)
                        {
                            break;
                        }
                        Stack++;
                    }
                    catch
                    {
                        break;
                    }
                }
                if (Stack >= 5)
                {
                    Win(Turn);
                    return true;
                }
                Stack = 0;
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        if (stones[x - i + j, y + i - j].Color != Turn)
                        {
                            break;
                        }
                        Stack++;
                    }
                    catch
                    {
                        break;
                    }
                }
                if (Stack >= 5)
                {
                    Win(Turn);
                    return true;
                }
                Stack = 0;
            }

            return false;
        }

        void SetColor(int x, int y)
        {
            
            stones[x, y].Color = Turn;
            Log("Droped", $"<{Turn_Count}> {Turn} : ({x}, {y})");
            Check(x, y);
            switch (Turn)
            {
                case StoneColor.Black:
                    Turn = StoneColor.White;
                    break;
                case StoneColor.White:
                    Turn = StoneColor.Black;
                    break;
            }
            Turn_Count++;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Log("Load", "Begin");
            Log("Load", "Setting Stones");
            for (int i = 0; i < STONE_COUNT; i++)
            {
                for (int j = 0; j < STONE_COUNT; j++)
                {
                    stones[j, i] = new Stone();
                    stones[j, i].VerticalAlignment = VerticalAlignment.Top;
                    stones[j, i].HorizontalAlignment = HorizontalAlignment.Left;
                    stones[j, i].Width = STONE_WIDTH;
                    stones[j, i].Height = STONE_WIDTH;
                    stones[j, i].Color = StoneColor.None;
                    stones[j, i].MouseEnter += new MouseEventHandler(Stone_MouseEnter);
                    stones[j, i].MouseLeave += new MouseEventHandler(Stone_MouseLeave);
                    stones[j, i].MouseDown += new MouseButtonEventHandler(Stone_MouseDown);
                    //f(x)=Wx+2x+M- > f(x)=(W+2)x+M
                    int W = STONE_WIDTH;
                    int M = 0;
                    stones[j, i].Margin = new Thickness((W + 2) * j + M, (W + 2) * i + M, 0, 0);
                    Board.Children.Add(stones[j, i]);
                }
            }
            Start();
            Log("Load", "Finish");
        }

        private void TopGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ClosingBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Stone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Stone).Color == StoneColor.None)
            {
                for (int i = 0; i < STONE_COUNT; i++)
                {
                    for (int j = 0; j < STONE_COUNT; j++)
                    {
                        if (sender == stones[j, i])
                        {
                            SetColor(j, i);
                            break;
                        }
                    }
                }
            }

        }

        private void Stone_MouseEnter(object sender, MouseEventArgs e)
        {
            Stone stone = (sender as Stone);
            if (stone.Color == StoneColor.None)
            {
                double opac = 0.5;
                switch (Turn)
                {
                    case StoneColor.Black:
                        stone.Black.Opacity = opac;
                        break;
                    case StoneColor.White:
                        stone.White.Opacity = opac;
                        break;
                }
            }
        }

        private void Stone_MouseLeave(object sender, MouseEventArgs e)
        {
            Stone stone = (sender as Stone);
            if (stone.Color == StoneColor.None)
            {
                double opac = 0;
                switch (Turn)
                {
                    case StoneColor.Black:
                        stone.Black.Opacity = opac;
                        break;
                    case StoneColor.White:
                        stone.White.Opacity = opac;
                        break;
                }
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WinTextGrid.Visibility = Visibility.Hidden;
            Reset();
        }
    }
}
