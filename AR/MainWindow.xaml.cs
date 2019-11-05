using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

//فضای نام را تعریف میکنیم
namespace AR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
//    کلاس اصلی را تعریف میکنیم که از کلاس Window ارث بری میکند
    public partial class MainWindow : Window
    {
        //سازنده کلاس را تعریف میکنیم
        public MainWindow()
        {
            //تابع فراخوانی عناصر ظاهری برنامه را صدا میزنیم
            InitializeComponent();
            //دستور بستن پنل کناری
            BeginStoryboard(CloseMenu_BeginStoryboard.Storyboard);
        }

        //تابعی تعریف میکنیم که وظیف نمایش اسنک بار را دارد
        public void show_snackbar(string message)
        {
            //یک متغیر برای نگه داشتن پیام اسنک بار تعریف میکنیم و مدت زمان 2 ثانیه را برای نمایش پیام به آن می دهیم
            var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            snack.MessageQueue = myMessageQueue;
            //دستور نمایش پیام
            myMessageQueue.Enqueue(message);
        }

        //یک تابع تعریف میکنیم که در هنگام لود شدن این صفحه اجرا میشود
        private void GridBackground_Loaded(object sender, RoutedEventArgs e)
        {
            //یک نمونه از کلاس userControl تعریف میکنیم
            UserControl usc = null;
           
            //محتویات این صفحه را حذف میکنیم
            GridBackground.Children.Clear();
            
            //یک نمونه از کلاس UserConrtolHome تعریف کرده و در این صفحه قرار میدهیم
            usc = new UserControlHome();
            GridBackground.Children.Add(usc);

            //تابع نمایش اسنک بار را صدا زده رشته خوش آمدید را به آن میدهیم
            show_snackbar("خوش آمدید");
        }
        //یه تابع که به هنگام زدن دکمه خروج اجرا میشود
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //دستور بستن برنامه
            Application.Current.Shutdown();
        }

        //تابعی تعریف میکنیم که به هنگام کلیک بر گزینه های منوی سمت راست اجرا میشود
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //یک نمونه از کلاس userControl تعریف میکنیم
            UserControl usc = null;

            //محتویات این صفحه را حذف میکنیم
            GridBackground.Children.Clear();

            //نام گزینه انتخاب شده را دریافت کرده روی آن سوییچ میزنیم
            //به ازای هرکدام نمونه از کلاس مورد نظر ایجاد کرده و در این صفحه قرار میدهیم
            //در نهایت پنل کناری را میبندیم
            switch (((ListViewItem) ((ListView) sender).SelectedItem).Name)
            {
                case "Main":
                    usc = new UserControlHome();
                    GridBackground.Children.Add(usc);
                    BeginStoryboard(CloseMenu_BeginStoryboard.Storyboard);
                    break;
                case "Airline":
                    usc = new UserControlAirline();
                    GridBackground.Children.Add(usc);
                    BeginStoryboard(CloseMenu_BeginStoryboard.Storyboard);
                    break;
                case "Flights":
                    usc = new UserControlFlights();
                    GridBackground.Children.Add(usc);
                    BeginStoryboard(CloseMenu_BeginStoryboard.Storyboard);
                    break;
                case "Passengers":
                    usc = new UserControlPassanger();
                    GridBackground.Children.Add(usc);
                    BeginStoryboard(CloseMenu_BeginStoryboard.Storyboard);
                    break;
                case "Agencys":
                    usc = new UserControlTravelAgency();
                    GridBackground.Children.Add(usc);
                    BeginStoryboard(CloseMenu_BeginStoryboard.Storyboard);
                    break;
                case "Reserves":
                    usc = new UserControlReservation();
                    GridBackground.Children.Add(usc);
                    BeginStoryboard(CloseMenu_BeginStoryboard.Storyboard);
                    break;
                default:
                    usc = new UserControlHome();
                    GridBackground.Children.Add(usc);
                    BeginStoryboard(CloseMenu_BeginStoryboard.Storyboard);
                    break;
            }
        }
        //یک تابع تعریف میکنیم که برای انتقال پنجره یا همان درگ کردن صفحه اجرا میشود
        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //اگر کلیک چپ ماوس فشار داده شد انتقال صفحه انجام شود
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}