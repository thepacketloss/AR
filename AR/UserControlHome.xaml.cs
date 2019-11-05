using System.Windows;
using System.Windows.Controls;

//فضای نام را تعریف میکنیم
namespace AR
{
    /// <summary>
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    /// 


//کلاس اصلی را تعریف میکنیم که از کلاس UserControl ارث بری میکند
    public partial class UserControlHome : UserControl
    {
        //سازنده کلاس را تعریف میکنیم
        public UserControlHome()
        {
            //تابع فراخوانی عناصر ظاهری برنامه را صدا میزنیم
            InitializeComponent();
        }

        //تابعی را تعریف میکنیم که به هنگام اجرای نرم افزار اجرا میشود
        private void home_loaded(object sender, RoutedEventArgs e)
        {
 
        }
    }
}
