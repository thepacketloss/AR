using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;

//فضای نام را تعریف میکنیم
namespace AR
{
    //کلاس اصلی را تعریف میکنیم که از کلاس UserControl ارث بری میکند
    public partial class UserControlAirline : UserControl
    {
        string connStr;

        //سازنده کلاس را تعریف میکنیم
        public UserControlAirline()
        {
            //تابع فراخوانی عناصر ظاهری برنامه را صدا میزنیم
            InitializeComponent();

            //رشته اتصال به پایگاه داده را ایجاد میکنیم
            connStr = "server=localhost;user=root;port=3306;database=airline_reservation;charset=utf8";
            
            //تابع load را صدا میزنیم
            load(null, null);
        }

        //برای ویرایش فرودگاه
        //تابعی تعریف میکنیم که خالی نبودن فیلد ها را چک کند
        private bool checkEditAirlineEmpty()
        {
            //اگر فیلد شناسه فرودگاه خالی بود
            if (AirlineIDEditTB.Text == "")
            {
                //اسنک بار را با این پیام نمایش بده
                show_snackbar("لطفا شناسه فرودگاه را وارد نمایید");
                //برروی فیلد شناسه فرودگاه فوکس کن
                AirlineIDEditTB.Focus();
                //مقدار false را برگردان
                return false;
            }
            if (AirlineNameEditTB.Text == "")
            {
                show_snackbar("لطفا نام فرودگاه را وارد نمایید");
                AirlineNameEditTB.Focus();
                return false;
            }
            if (AirlineAddressEditTB.Text == "")
            {
                show_snackbar("لطفا آدرس فرودگاه را وارد نمایید");
                AirlineAddressEditTB.Focus();
                return false;
            }
            return true;
        }

        //برای افزودن فرودگاه
        //تابعی تعریف میکنیم که خالی نبودن فیلد ها را چک کند
        private bool checkAddAirlineEmpty()
        {
            if (AirlineIDAddTB.Text == "")
            {
                show_snackbar("لطفا شناسه فرودگاه را وارد نمایید");
                AirlineIDAddTB.Focus();
                return false;
            }
            if (AirlineNameAddTB.Text == "")
            {
                show_snackbar("لطفا نام فرودگاه را وارد نمایید");
                AirlineNameAddTB.Focus();
                return false;
            }
            if (AirlineAddressAddTB.Text == "")
            {
                show_snackbar("لطفا آدرس فرودگاه را وارد نمایید");
                AirlineAddressAddTB.Focus();
                return false;
            }
            return true;
        }

        private void airline_load(object sender, RoutedEventArgs e)
        {
        }

        //تابعی که به هنگام لود شدن صفحه اجرا میشود
        private void load(object sender, RoutedEventArgs e)
        {
            try
            {
                //یک اتصال به پایگاه داده ایجاد میکنیم
                MySqlConnection conn = new MySqlConnection(connStr);
                
                //اتصال را باز میکنیم
                conn.Open();
                //رشته کوئری را ایجاد میکنیم برای دریافت تمامی رکورد های جدول فرودگاه ها
                string queryy = "SELECT * FROM airline";
                
                //یک دستور mysql ایجاد میکنیم
                MySqlCommand ccmd = new MySqlCommand(queryy, conn);
                //یک آداپتور ایجاد میکنیم برای دریافت اطلاعات و نمایش آن ها
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryy, conn);
                
                //یک جدول مجازی ایجاد میکنیم
                DataTable dt = new DataTable("airline");
                //با استفاده از آداپتور این جدول را با اطلاعات دریافت شده پر میکنیم
                adapter.Fill(dt);
                
                //منبع جدولی که در برنامه قرار دارد و میخواهیم اطلاعات را در آن نمایش دهیم را این جدول مجازی قرار میدهیم
                dg.ItemsSource = dt.DefaultView;
                
                //سطر یک جدول را درحالت انتخاب شده قرار میدهیم
                dg.SelectedIndex = 0;
            }
            catch
            {
                // در غیر این صورت اگر خطایی رخ داد منبع این جدول را نال یا خالی قرار میدهیم
                dg.ItemsSource = null;
            }
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

        //تابعی که در هنگام نوشتن عبارتی در بخش جستجو اجرا می شود
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //اگر فیلد جستجو خالی بود
                if (AirlineSearchTB.Text == "")
                  //تابع load را صدا بزن
                    load(null, null);


                //یک اتصال به پایگاه داده ایجاد میکنیم
                MySqlConnection conn = new MySqlConnection(connStr);
                //اتصال را باز میکنیم
                conn.Open();
                //کوئری جستجو را ایجاد میکنیم
                string query = "SELECT * FROM airline where AirlineID like  '%" + AirlineSearchTB.Text +
                               "%' or Name like '%" + AirlineSearchTB.Text + "%' or Address like '%" +
                               AirlineSearchTB.Text + "%'";
                //یک دستور mysql ایجاد میکنیم
                MySqlCommand cmd = new MySqlCommand(query, conn);
                
                //یک دیتاریدر تعریف کرده و دستور را اجرا کرده و مقادیر دریافت شده را درآن قرار میدهیم
                MySqlDataReader dataReader = cmd.ExecuteReader();
                
                //اگر این دیتاریدر دارای سطری بود
                if (dataReader.HasRows)
                {
                    //یک جدول مجازی ایجاد میکنیم
                    DataTable dt = new DataTable();
                    //اطلاعات دیتاریدر را در آن لود میکنیم
                    dt.Load(dataReader);
                    
                    //منبع جدولی که میخواهیم در آن اطلاعات را نمایش دهیم را این جدومل مجازی قرار میدهیم
                    dg.ItemsSource = dt.DefaultView;
                }
                //دیتاریدر را میبندیم
                dataReader.Close();
            }
            catch (MySqlException)
            {
                //اسنک بار را مقدار داده شده نمایش بده
                show_snackbar("جستجو ناموفق بود");
            }
        }

        //تابعی تعریف میکنیم که وظیفه حذف فرودگاه را برعهده دارد
        private void AirlineDeletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            //اگر تعداد سطر های جدول بیشتر از 0 بود
            if (dg.Items.Count>0)
            {
              //اگر سطری از جدول انتخاب نشده بود ادامه نده
                if (dg.SelectedItem == null) return;

                //یک متغیر تعریف کرده و ستون اول سطر انتخاب شده را دریافت کرده متنش را در متغیر قرار میدهیم
                var AR = (dg.SelectedCells[0].Column.GetCellContent(dg.SelectedCells[0].Item) as TextBlock).Text;
                //یک اتصال به پایگاه داده ایجاد میکنیم
                MySqlConnection conn = new MySqlConnection(connStr);
                //اتصال را باز میکنیم
                conn.Open();
                
                //کوئری حذف از پایگاه داده را ایجاد میکنیم
                string query = "DELETE FROM airline WHERE AirlineID = '" + AR + "'";
                //یک دستور mysql ایجاد میکنیم
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //دستور را اجرا می کنیم
                cmd.ExecuteNonQuery();
               
                //اسنک بار را نمایش میدهیم با مقدار داده شده
                show_snackbar("فرودگاه با موفقیت حذف شد");
                
                //تابع load را صدا میزنیم
                load(null, null);
            }
            else
            {
                //در غیر این صورت اسنک بار را با این مقدار صدا میزنیم
                show_snackbar("موردی برای حذف وجود ندارد");
            }

        }

        //تابعی تعریف میکنیم که وظیفه افزودن فرودگاه را برعهده دارد
        private void AirlineAddbtn_OnClick(object sender, RoutedEventArgs e)
        {
            //اگر تابع چک کردن خالی نبودن فیلد های مقدار صحیح را برگرداند 
            if (checkAddAirlineEmpty())
            {
                try
                {
                    //یک اتصال به پایگاه داده ایجاد میکنیم
                    MySqlConnection conn = new MySqlConnection(connStr);
                    //اتصال را باز میکنیم
                    conn.Open();

                   //کوئری افزودن فرودگاه را ایجاد میکنیم
                    string query = @"INSERT INTO airline (AirlineID,Name,Address) VALUES ('" + AirlineIDAddTB.Text + "','" + AirlineNameAddTB.Text + "','" + AirlineAddressAddTB.Text + "')";
                    //یک دستور mysql ایجاد میکنیم
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                   //دستور را ایجاد میکنیم
                    cmd.ExecuteNonQuery();
                    //اسنک بار را مقدار داده شده صدا میزنیم
                    show_snackbar("فرودگاه با موفقیت اضافه شد");
                    
                    //اتصال را قطع میکنیم
                    conn.Close();
                    //تابع load را صدا میزنیم
                    load(null, null);
                }
                catch (Exception exception)
                {
                    //در غیر این صورت اسنک بار را مقدار داده شده صدا میزنیم
                    show_snackbar("افزودن فرودگاه ناموفق بود");
                }
            }
        }


        //تابعی تعریف میکنیم که وظیفه ویرایش فرودگاه را برعهده دارد
        private void AirlineEditbtn_OnClick(object sender, RoutedEventArgs e)
        {
            //اگر سطری از جدول انتخاب نشده بود
            if (dg.SelectedIndex == -1 || dg.SelectedItem == null) return;

            //اگر تابع چک کردن خالی نبودن فیلد های مقدار صحیح را برگرداند
            if (checkEditAirlineEmpty())
            {
                //اگر تعداد سطر های جدول بیشتر از 0 بود
                if (dg.Items.Count > 0)
                {

                    try
                    {
                        //یک اتصال به پایگاه داده ایجاد میکنیم
                        MySqlConnection conn = new MySqlConnection(connStr);
                        //اتصال را باز میکنیم
                        conn.Open();

                        //یک متغیر تعریف کرده و ستون اول سطر انتخاب شده را دریافت کرده متنش را در متغیر قرار میدهیم
                        var ID = (dg.SelectedCells[0].Column.GetCellContent(dg.SelectedCells[0].Item) as TextBlock).Text;

                        //کوئری ویرایش فرودگاه را ایجاد میکنیم
                        string query = "UPDATE airline SET AirlineID='" + AirlineIDEditTB.Text + "', Name='" + AirlineNameEditTB.Text + "', Address='" + AirlineAddressEditTB.Text + "' WHERE AirlineID = " + ID;
                        //یک دستور mysql ایجاد میکنیم
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                       //دستور را اجرا میکنیم
                        cmd.ExecuteNonQuery();

                        //اسنک بار را با مقدار داده شده صدا میزنیم
                        show_snackbar("فرودگاه با موفقیت ویرایش شد");
                        //اتصال را قطع میکنیم
                        conn.Close();
                        //تابع load را صدا میزنیم
                        load(null, null);
                    }
                    catch (Exception exception)
                    {
                        //اسنک بار را با مقدار داده شده صدا میزنیم
                        show_snackbar("ویرایش فرودگاه ناموفق بود");
                    }


                }
                else
                {
                    //در غیر این صورت اسنک بار را این مقدار صدا میزنیم
                    show_snackbar("موردی برای ویرایش وجود ندارد");
                }


            }
        }

        //تابعی تعریف میکنیم که در هنگام کلیک بر دکمه ویرایش فرودگاه اجرا میشود
        private void BtnEditShowDialog_OnClick(object sender, RoutedEventArgs e)
        {
            //مقادیر ستون های جدول را از جدول گرفته و در فیلد های ورودی قرار میدهیم
            AirlineIDEditTB.Text = (dg.SelectedCells[0].Column.GetCellContent(dg.SelectedCells[0].Item) as TextBlock).Text;
            AirlineNameEditTB.Text = (dg.SelectedCells[1].Column.GetCellContent(dg.SelectedCells[1].Item) as TextBlock).Text;
            AirlineAddressEditTB.Text = (dg.SelectedCells[2].Column.GetCellContent(dg.SelectedCells[2].Item) as TextBlock).Text;
        }

        //تابعی تعریف میکنیم که در هنگام کلیک بر دکمه افزودن فرودگاه اجرا میشود
        private void BtnAddAirShowDialog_OnClick(object sender, RoutedEventArgs e)
        {
            //مقادیر فیلد ها را برابر خالی قرار میدهیم
            AirlineIDAddTB.Text = "";
            AirlineNameAddTB.Text = "";
            AirlineAddressAddTB.Text = "";
        }
    }
}