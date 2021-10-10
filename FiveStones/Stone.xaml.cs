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
using FiveStones;

namespace FiveStones
{
    /// <summary>
    /// Stone.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Stone : UserControl
    {
        StoneColor color = StoneColor.White;

        public Stone()
        {
            Width = 32;
            Height = 32;
            InitializeComponent();
        }

        public StoneColor Color
        {
            get
            {
                return color;
            }
            set
            {
                this.color = value;
                if(value == StoneColor.Black)
                {
                    Black.Opacity = 1;
                    White.Opacity = 0;
                }
                else if(value == StoneColor.White)
                {
                    Black.Opacity = 0;
                    White.Opacity = 1;
                }
                else
                {
                    Black.Opacity = 0;
                    White.Opacity = 0;
                }
            }
        }
    }
}
