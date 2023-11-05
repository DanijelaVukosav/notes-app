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

namespace Biljeske
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ucitajSveBiljeske();
        }


        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_all.Visibility = Visibility.Collapsed;
                tt_posao.Visibility = Visibility.Collapsed;
                tt_zabava.Visibility = Visibility.Collapsed;
                tt_ostalo.Visibility = Visibility.Collapsed;
                tt_smece.Visibility = Visibility.Collapsed;
                tt_font.Visibility = Visibility.Collapsed;

            }
            else
            {
                tt_all.Visibility = Visibility.Visible;
                tt_posao.Visibility = Visibility.Visible;
                tt_zabava.Visibility = Visibility.Visible;
                tt_ostalo.Visibility = Visibility.Visible;
                tt_smece.Visibility = Visibility.Visible;
                tt_font.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
            //pomoc.Margin = new Thickness(65, 0, 0, 0);

        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
            pomoc.Margin = new Thickness(230, 0, 0, 0);
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void promjenaFonta(object sender, SelectionChangedEventArgs e)
        {

            String izabraniFont = comboBoxFont.SelectedValue.ToString();
            int redniBroj = comboBoxFont.SelectedIndex;
            this.FontFamily = new FontFamily(izabraniFont);
            Console.WriteLine("UDje da promijeni " + redniBroj + "  " + izabraniFont.Length + " " + izabraniFont.Equals("Arial"));
            switch (redniBroj)
            {
                case 0:
                    this.FontFamily = new FontFamily("Sogoe UI");
                    break;
                case 1:
                    this.FontFamily = new FontFamily("Stencil");
                    break;
                case 2:
                    this.FontFamily = new FontFamily("Arial");
                    break;
                case 3:
                    this.FontFamily = new FontFamily("Blackadder ITC");
                    break;
                case 4:
                    this.FontFamily = new FontFamily("Ink Free");
                    break;
                case 5:
                    this.FontFamily = new FontFamily("Verdana");
                    break;
                default:
                    break;


            }
        }
        private void dodajJednuBiljeskuNaStek(Biljeska biljeska)
        {
            Grid grid = new Grid();
            grid.Name = "biljeska" + biljeska.idBiljeska;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Width = 320;
            grid.Height = 99;
            grid.Background = Brushes.White;
            Ellipse slika = new Ellipse();
            ImageBrush ikonica = new ImageBrush();
            if (biljeska.tip == Tip.Zabava)
                ikonica.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Icons/fun.png", UriKind.RelativeOrAbsolute));
            else if (biljeska.tip == Tip.Posao)
                ikonica.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Icons/icons8-briefcase-32.png", UriKind.RelativeOrAbsolute));
            else
                ikonica.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Icons/icons8-menu-vertical-48 (1).png", UriKind.RelativeOrAbsolute));
            ikonica.Stretch = Stretch.Fill;
            slika.Fill = ikonica;
            slika.HorizontalAlignment = HorizontalAlignment.Left;
            slika.VerticalAlignment = VerticalAlignment.Top;
            slika.Height = 14;
            slika.Width = 14;
            slika.Margin = new Thickness(6, 6, 0, 0);
            TextBlock datum = new TextBlock();
            datum.HorizontalAlignment = HorizontalAlignment.Left;
            datum.Height = 19;
            datum.Margin = new Thickness(26, 5, 0, 0);
            datum.Text = biljeska.vrijeme.ToString();
            datum.VerticalAlignment = VerticalAlignment.Top;
            datum.Width = 150;
            datum.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF424040");
            grid.Children.Add(datum);
            grid.Children.Add(slika);
            TextBlock naslov = new TextBlock();
            naslov.HorizontalAlignment = HorizontalAlignment.Left;
            naslov.Height = 18;
            naslov.Margin = new Thickness(10, 26, 0, 0);
            naslov.TextWrapping = TextWrapping.WrapWithOverflow;
            naslov.Text = biljeska.naslov;
            naslov.VerticalAlignment = VerticalAlignment.Top;
            naslov.Width = 210;
            naslov.FontWeight = FontWeights.Bold;
            grid.Children.Add(naslov);
            TextBlock sadrzaj = new TextBlock();
            sadrzaj.HorizontalAlignment = HorizontalAlignment.Left;
            sadrzaj.Height = 34;
            sadrzaj.Margin = new Thickness(10, 49, 0, 0);
            sadrzaj.TextWrapping = TextWrapping.Wrap;
            sadrzaj.Text = biljeska.sadrzaj;
            sadrzaj.VerticalAlignment = VerticalAlignment.Top;
            sadrzaj.Width = 310;
            grid.Children.Add(sadrzaj);
            grid.PreviewMouseDown += PrikaziBiljesku;
            grid.MouseDown += PrikaziBiljesku;

            listaBiljezaka.Children.Insert(0, grid);

        }
        private void dodajBiljeskeNaStek(List<Biljeska> biljeske)
        {
            listaBiljezaka.Children.Clear();
            foreach (var biljeska in biljeske)
            {
                dodajJednuBiljeskuNaStek(biljeska);
            }

        }
        private void sacuvajBiljesku(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(comboBoxTip.SelectedValue.ToString());
            Biljeska biljeska = new Biljeska(naslovBiljeske.Text, sadrzajBiljeske.Text, (Tip)Enum.Parse(typeof(Tip), comboBoxTip.SelectedValue.ToString().Substring(comboBoxTip.SelectedValue.ToString().IndexOf(' ') + 1), true));
            biljeska = BiljeskeDB.InsertNote(biljeska);
            dodajJednuBiljeskuNaStek(biljeska);

            vrijemeKreiranjaBiljeske.Content = biljeska.vrijeme.ToString();
            idBiljeske.Content = biljeska.idBiljeska;
            sacuvaj.Visibility = Visibility.Hidden;
            vrijemeKreiranjaBiljeske.Visibility = Visibility.Visible;
            naslovLabel.Visibility = Visibility.Hidden;
            sadrzajLabel.Visibility = Visibility.Hidden;
            izbrisi.Visibility = Visibility.Visible;
            izmijeni.Visibility = Visibility.Visible;
        }

        private void PrikaziBiljesku(object sender, MouseButtonEventArgs e)
        {

            Grid grid = sender as Grid;
            String id = grid.Name.Substring(8);
            naslovBiljeske.Visibility = Visibility.Visible;
            sadrzajBiljeske.Visibility = Visibility.Visible;
            comboBoxTip.Visibility = Visibility.Visible;

            vrijemeKreiranjaBiljeske.Visibility = Visibility.Visible;
            Biljeska biljeska = BiljeskeDB.GetNoteById(int.Parse(id));
            naslovBiljeske.Text = biljeska.naslov;
            sadrzajBiljeske.Text = biljeska.sadrzaj;
            vrijemeKreiranjaBiljeske.Content = biljeska.vrijeme.ToString();
            comboBoxTip.SelectedItem = "Zabava";
            comboBoxTip.SelectedIndex = (int)biljeska.tip;
            idBiljeske.Content = id;

            if (biljeska.daLiJeIzbrisano)
            {
                izbrisi.Visibility = Visibility.Hidden;
                izmijeni.Visibility = Visibility.Hidden;
                vratiIzSmeca.Visibility = Visibility.Visible;
                sadrzajBiljeske.IsEnabled = false;
                naslovBiljeske.IsEnabled = false;
                comboBoxTip.IsEnabled = false;

            }
            else
            {
                sadrzajBiljeske.IsEnabled = true;
                naslovBiljeske.IsEnabled = true;
                comboBoxTip.IsEnabled = true;
                izbrisi.Visibility = Visibility.Visible;
                sacuvaj.Visibility = Visibility.Hidden;
                izmijeni.Visibility = Visibility.Visible;
                vratiIzSmeca.Visibility = Visibility.Hidden;
            }
        }

        private void buttonDodajBiljesku(object sender, RoutedEventArgs e)
        {
            sacuvaj.Visibility = Visibility.Visible;
            naslovBiljeske.Visibility = Visibility.Visible;
            sadrzajBiljeske.Visibility = Visibility.Visible;
            izmijeni.Visibility = Visibility.Hidden;
            izbrisi.Visibility = Visibility.Hidden;
            vratiIzSmeca.Visibility = Visibility.Hidden;
            comboBoxTip.Visibility = Visibility.Visible;
            vrijemeKreiranjaBiljeske.Visibility = Visibility.Hidden;
            naslovLabel.Visibility = Visibility.Visible;
            sadrzajLabel.Visibility = Visibility.Visible;

            sadrzajBiljeske.Text = "";
            naslovBiljeske.Text = "";
            idBiljeske.Content = "";


        }

        private void izmijeniBiljesku(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(comboBoxTip.SelectedValue.ToString());
            Biljeska biljeska = new Biljeska(int.Parse(idBiljeske.Content.ToString()), naslovBiljeske.Text, sadrzajBiljeske.Text, (int)Enum.Parse(typeof(Tip), comboBoxTip.SelectedValue.ToString().Substring(comboBoxTip.SelectedValue.ToString().IndexOf(' ') + 1), true), new DateTime(), false);
            // biljeska.tip = biljeska.tip.Substring(biljeska.tip.IndexOf(' ') + 1);
            BiljeskeDB.UpdateNote(biljeska);
            ucitajSveBiljeske();

        }

        private void izbrisiBiljesku(object sender, RoutedEventArgs e)
        {
            BiljeskeDB.DeleteNote(int.Parse(idBiljeske.Content.ToString()));
            ucitajSveBiljeske();
            naslovBiljeske.Visibility = Visibility.Hidden;
            sadrzajBiljeske.Visibility = Visibility.Hidden;
            vrijemeKreiranjaBiljeske.Visibility = Visibility.Hidden;
            izbrisi.Visibility = Visibility.Hidden;
            izmijeni.Visibility = Visibility.Hidden;

        }
        private void ucitajSveBiljeske()
        {
            List<Biljeska> biljeske = BiljeskeDB.GetNotes();
            dodajBiljeskeNaStek(biljeske);
        }

        private void PrikaziUkupno(object sender, MouseButtonEventArgs e)
        {
            ucitajSveBiljeske();

        }

        private void PrikaziBiljeskePosao(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("POZVANA FUNKIJA!");
            dodajBiljeskeNaStek(BiljeskeDB.GetBussinesNotes());
            Tg_Btn.IsChecked = false;
        }

        private void PrikaziBiljeskeZabava(object sender, MouseButtonEventArgs e)
        {
            dodajBiljeskeNaStek(BiljeskeDB.GetFunNotes());
            Tg_Btn.IsChecked = false;
        }

        private void PrikaziBiljeskeOstalo(object sender, MouseButtonEventArgs e)
        {
            dodajBiljeskeNaStek(BiljeskeDB.GetOtherNotes());
            Tg_Btn.IsChecked = false;
        }

        private void PrikaziIzbrisaneBiljeske(object sender, MouseButtonEventArgs e)
        {
            dodajBiljeskeNaStek(BiljeskeDB.GetDeletedNotes());
            Tg_Btn.IsChecked = false;
        }

        private void VratiBiljeskuIzSmeca(object sender, RoutedEventArgs e)
        {
            vratiIzSmeca.Visibility = Visibility.Hidden;
            naslovBiljeske.Visibility = Visibility.Hidden;
            sadrzajBiljeske.Visibility = Visibility.Hidden;
            vrijemeKreiranjaBiljeske.Visibility = Visibility.Hidden;
            comboBoxTip.Visibility = Visibility.Hidden;


            BiljeskeDB.RestoreNote(int.Parse(idBiljeske.Content.ToString()));
            dodajBiljeskeNaStek(BiljeskeDB.GetDeletedNotes());


        }

        private void PretraziBiljeske(object sender, TextChangedEventArgs e)
        {
            String patern = pretraga.Text.ToLower();
            List<Grid> izbaciGridove = new List<Grid>();
            foreach (var temp in listaBiljezaka.Children)
            {
                Grid grid = temp as Grid;

                String id = grid.Name.Substring(8);
                Biljeska biljeska = BiljeskeDB.GetNoteById(int.Parse(id));
                if (!biljeska.naslov.ToLower().Contains(patern))
                {

                    izbaciGridove.Add(grid);
                }
                else
                    grid.Visibility = Visibility.Visible;
            }
            foreach (var grid in izbaciGridove)
            {
                listaBiljezaka.Children.Remove(grid);
                listaBiljezaka.Children.Add(grid);
                grid.Visibility = Visibility.Hidden;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //System.Windows.Application.Current.Shutdown();
        }

        private void ZatvoriAplikaciju(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
