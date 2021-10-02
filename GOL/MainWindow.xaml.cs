using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GOL
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Load grid of cells:
            for (int i = 0; i < 30; i++) {
                for (int j = 0; j < 30; j++) {
                    Button cell = new Button();
                    cell.Background = Brushes.Black;
                    cell.Name = "cell_"+((15*i) + j).ToString();
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    gridMain.Children.Add(cell);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button cell = (Button)sender;

            if (cell.Name.Equals("start"))
            {
                ;
            }
            else if (cell.Background.Equals(Brushes.Black))
                cell.Background = Brushes.White;
            else
                cell.Background = Brushes.Black;
        }
    }
}
