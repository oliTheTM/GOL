using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GOL
{
    public partial class MainWindow : Window
    {
        private const int LENGTH = 30;

        private Thread gameThread;

        public MainWindow()
        {
            gameThread = null;
            InitializeComponent();

            //Load the game grid:
            Button cell;
            for (int i = 0; i < LENGTH; ++i) {
                for (int j = 0; j < LENGTH; ++j) {
                    cell = new Button();
                    cell.Background = Brushes.Black;
                    cell.Name = "cell_"+((LENGTH * i) + j).ToString();
                    cell.Click += new RoutedEventHandler(Button_Click);
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    //adds children in same order as in name
                    GOL.Children.Add(cell);
                }
            }

            //finally add start button:
            Button start = new Button();
            start.Name = "start";
            start.Content = "GO!";
            start.Click += new RoutedEventHandler(Button_Start);
            Grid.SetRow(start, (LENGTH + 1));
            Grid.SetColumn(start, (LENGTH/2));
            GOL.Children.Add(start);
        }

        /**
         * Changes state of cell
         */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button cell = (Button)sender;
            if (cell.Background.Equals(Brushes.Black))
                cell.Background = Brushes.White;
            else
                cell.Background = Brushes.Black;
        }

        /**
         * 
         */
        private void Button_Start(object sender, RoutedEventArgs e)
        {
            Button start = (Button)sender;
            if (start.Content.Equals("GO!")) {
                start.Content = "STOP";
                gameThread = new Thread(game);
                gameThread.Start();
            }
            else {
                start.Content = "GO!";
                try {
                    gameThread.Abort();
                    gameThread.Join();
                } catch (ThreadAbortException){
                    ;//Assumption: gameThread halted
                }
            }
        }

        private void game()  {Dispatcher.Invoke(() =>
        {
            int neighbours;
            Button cell;
    
            while (true) {
                //forAll <i, j> of the board:
                for (int i = 0; (i < LENGTH); ++i)
                {
                    for (int j = 0; (j < LENGTH); ++j)
                    {
                        //reference cell (i, j)
                        cell = (Button)GOL.Children[(LENGTH * i) + j];
                        //get neighbour count
                        neighbours = liveNeighbours(i, j);
                        //State transition w/r neighbour count:
                        switch (neighbours)
                        {
                            case 0:
                                cell.Background = Brushes.Black;
                                break;
                            case 1:
                                cell.Background = Brushes.Black;
                                break;
                            case 2:
                                //no change
                                break;
                            case 3:
                                cell.Background = Brushes.White;
                                break;
                            case 4:
                                cell.Background = Brushes.Black;
                                break;
                            case 5:
                                cell.Background = Brushes.Black;
                                break;
                            case 6:
                                cell.Background = Brushes.Black;
                                break;
                            case 7:
                                cell.Background = Brushes.Black;
                                break;
                            case 8:
                                cell.Background = Brushes.Black;
                                break;
                        }
                    }
                }
        }});}

        private int liveNeighbours(int x, int y) 
        {
            int living = 0, location;
            for (int Xoff = -1; (Xoff < 2); ++Xoff) {
                for (int Yoff = -1; (Yoff < 2); ++Yoff) {
                    location = (x + Xoff) * LENGTH + y + Yoff;
                    if ((Xoff == 0) && (Yoff == Xoff))
                        ;//skip if self
                    else if ((location < 0) || (location > (Math.Pow(LENGTH, 2) - 1)))
                        ;//skip if outside grid
                    else if (((Button)GOL.Children[location]).Background.Equals(Brushes.White))
                        ++living;//add to living if white
                }
            }
            return living;
        }
    }
}