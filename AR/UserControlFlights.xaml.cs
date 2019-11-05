using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

//فضای نام را تعریف میکنیم
namespace AR
{

    //کلاس اصلی را تعریف میکنیم که از کلاس UserControl ارث بری میکند
    public partial class UserControlFlights : UserControl
    {
        string connStr;

        //سازنده کلاس را تعریف میکنیم
        public UserControlFlights()
        {
            //تابع فراخوانی عناصر ظاهری برنامه را صدا میزنیم
            InitializeComponent();
            //رشته اتصال به پایگاه داده را ایجاد میکنیم
            connStr = "server=localhost;user=root;port=3306;database=airline_reservation;charset=utf8";
            //تابع load را صدا میزنیم
            load(null, null);
        }
        //برای ویرایش پرواز
        //تابعی تعریف میکنیم که خالی نبودن فیلد ها را چک کند
        private bool checkEditReserveEmpty()
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
            if (FlightCodeEditTB.Text == "")
            {
                show_snackbar("لطفا کد پرواز را وارد نمایید");
                FlightCodeEditTB.Focus();
                return false;
            }
            if (FlightDateEditTB.Text == "")
            {
                show_snackbar("لطفا تاریخ پرواز را وارد نمایید");
                FlightDateEditTB.Focus();
                return false;
            }
            if (DepartureTimeEditTB.Text == "")
            {
                show_snackbar("لطفا زمان پرواز را وارد نمایید");
                DepartureTimeEditTB.Focus();
                return false;
            }
            if (ArrivalTimeEditTB.Text == "")
            {
                show_snackbar("لطفا زمان رسیدن به مقصد را وارد نمایید");
                ArrivalTimeEditTB.Focus();
                return false;
            }
            if (DestinationEditTB.Text == "")
            {
                show_snackbar("لطفا مقصد را وارد نمایید");
                DestinationEditTB.Focus();
                return false;
            }
            if (SourceEditTB.Text == "")
            {
                show_snackbar("لطفا مبدا را وارد نمایید");
                SourceEditTB.Focus();
                return false;
            }
            return true;
        }
        //برای افزودن پرواز
        //تابعی تعریف میکنیم که خالی نبودن فیلد ها را چک کند
        private bool checkAddReserveEmpty()
        {
            if (AirlineIDAddTB.Text == "")
            {
                show_snackbar("لطفا شناسه فرودگاه را وارد نمایید");
                AirlineIDAddTB.Focus();
                return false;
            }
            if (FlightCodeAddTB.Text == "")
            {
                show_snackbar("لطفا کد پرواز را وارد نمایید");
                FlightCodeAddTB.Focus();
                return false;
            }
            if (FlightDateAddTB.Text == "")
            {
                show_snackbar("لطفا تاریخ پرواز را وارد نمایید");
                FlightDateAddTB.Focus();
                return false;
            }
            if (DepartureTimeAddTB.Text == "")
            {
                show_snackbar("لطفا زمان پرواز را وارد نمایید");
                DepartureTimeAddTB.Focus();
                return false;
            }
            if (ArrivalTimeAddTB.Text == "")
            {
                show_snackbar("لطفا زمان رسیدن به مقصد را وارد نمایید");
                ArrivalTimeAddTB.Focus();
                return false;
            }
            if (DestinationAddTB.Text == "")
            {
                show_snackbar("لطفا مقصد را وارد نمایید");
                DestinationAddTB.Focus();
                return false;
            }
            if (SourceAddTB.Text == "")
            {
                show_snackbar("لطفا مبدا را وارد نمایید");
                SourceAddTB.Focus();
                return false;
            }
            return true;
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
                string queryy = "SELECT * FROM flights";
                //یک دستور mysql ایجاد میکنیم
                MySqlCommand ccmd = new MySqlCommand(queryy, conn);
                //یک آداپتور ایجاد میکنیم برای دریافت اطلاعات و نمایش آن ها
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryy, conn);
                //یک جدول مجازی ایجاد میکنیم
                DataTable dt = new DataTable("flights");
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

        //تابعی تعریف میکنیم که وظیفه افزودن پرواز را برعهده دارد
        private void FlightAddbtn_OnClick(object sender, RoutedEventArgs e)
        {
            //اگر تابع چک کردن خالی نبودن فیلد های مقدار صحیح را برگرداند 
            if (checkAddReserveEmpty())
            {
                try
                {
                    //یک اتصال به پایگاه داده ایجاد میکنیم
                    MySqlConnection conn = new MySqlConnection(connStr);
                    //اتصال را باز میکنیم
                    conn.Open();

                    //کوئری افزودن پرواز را ایجاد میکنیم
                    string query = @"INSERT INTO flights (AirlineID,FlightCode,FlightDate,DepartureTime,ArrivalTime,Destination,Source) VALUES ('" + AirlineIDAddTB.Text + "','" + FlightCodeAddTB.Text + "','" + FlightDateAddTB.Text + "','" + DepartureTimeAddTB.Text + "','" + ArrivalTimeAddTB.Text + "','" + DestinationAddTB.Text + "','" + SourceAddTB.Text + "')";
                    //یک دستور mysql ایجاد میکنیم
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    //دستور را ایجاد میکنیم
                    cmd.ExecuteNonQuery();
                    //اسنک بار را مقدار داده شده صدا میزنیم
                    show_snackbar("پرواز با موفقیت اضافه شد");
                    //اتصال را قطع میکنیم
                    conn.Close();
                    //تابع load را صدا میزنیم
                    load(null, null);
                }
                catch (Exception exception)
                {
                    //در غیر این صورت اسنک بار را مقدار داده شده صدا میزنیم
                    show_snackbar("افزودن پرواز ناموفق بود");
                }
            }

        }

        //تابعی تعریف میکنیم که در هنگام کلیک بر دکمه افزودن پرواز اجرا میشود
        private void BtnAddFlightShowDialog_OnClick(object sender, RoutedEventArgs e)
        {
            //مقادیر فیلد ها را برابر خالی قرار میدهیم
            AirlineIDAddTB.Text = "";
            FlightCodeAddTB.Text = "";
            DepartureTimeAddTB.Text = "";
            ArrivalTimeAddTB.Text = "";
            DestinationAddTB.Text = "";
            SourceAddTB.Text = "";
            FlightDateAddTB.Text = "";
        }

        //تابعی تعریف میکنیم که وظیفه ویرایش پرواز را برعهده دارد
        private void FlightEditbtn_OnClick(object sender, RoutedEventArgs e)
        {
            //اگر سطری از جدول انتخاب نشده بود
            if (dg.SelectedIndex == -1 || dg.SelectedItem == null) return;

            //اگر تابع چک کردن خالی نبودن فیلد های مقدار صحیح را برگرداند
            if (checkEditReserveEmpty())
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

                        //کوئری ویرایش پرواز را ایجاد میکنیم
                        string query = "UPDATE flights SET AirlineID ='" + AirlineIDEditTB.Text + "', FlightCode ='" + FlightCodeEditTB.Text + "', FlightDate ='" + FlightDateEditTB.Text + "', DepartureTime ='" + DepartureTimeEditTB.Text + "', ArrivalTime ='" + ArrivalTimeEditTB.Text+ "', Destination ='" + DestinationEditTB.Text + "', Source ='" + SourceEditTB.Text + "' WHERE AirlineID = " + ID;
                        //یک دستور mysql ایجاد میکنیم
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        //دستور را اجرا میکنیم
                        cmd.ExecuteNonQuery();

                        //اسنک بار را با مقدار داده شده صدا میزنیم
                        show_snackbar("پرواز با موفقیت ویرایش شد");
                        //اتصال را قطع میکنیم
                        conn.Close();
                        //تابع load را صدا میزنیم
                        load(null, null);
                    }
                    catch (Exception exception)
                    {
                        //اسنک بار را با مقدار داده شده صدا میزنیم
                        show_snackbar("ویرایش پرواز ناموفق بود");
                    }


                }
                else
                {
                    //در غیر این صورت اسنک بار را این مقدار صدا میزنیم
                    show_snackbar("موردی برای ویرایش وجود ندارد");
                }


            }

        }

        //تابعی تعریف میکنیم که در هنگام کلیک بر دکمه ویرایش پرواز اجرا میشود
        private void BtnEditShowDialog_OnClick(object sender, RoutedEventArgs e)
        {
            //مقادیر ستون های جدول را از جدول گرفته و در فیلد های ورودی قرار میدهیم
            AirlineIDEditTB.Text = (dg.SelectedCells[0].Column.GetCellContent(dg.SelectedCells[0].Item) as TextBlock).Text;
            FlightCodeEditTB.Text = (dg.SelectedCells[1].Column.GetCellContent(dg.SelectedCells[1].Item) as TextBlock).Text;
            FlightDateEditTB.Text = (dg.SelectedCells[2].Column.GetCellContent(dg.SelectedCells[2].Item) as TextBlock).Text;
            DepartureTimeEditTB.Text = (dg.SelectedCells[3].Column.GetCellContent(dg.SelectedCells[3].Item) as TextBlock).Text;
            ArrivalTimeEditTB.Text = (dg.SelectedCells[4].Column.GetCellContent(dg.SelectedCells[4].Item) as TextBlock).Text;
            DestinationEditTB.Text = (dg.SelectedCells[5].Column.GetCellContent(dg.SelectedCells[5].Item) as TextBlock).Text;
            SourceEditTB.Text = (dg.SelectedCells[6].Column.GetCellContent(dg.SelectedCells[6].Item) as TextBlock).Text;
        }

        //تابعی تعریف میکنیم که وظیفه حذف پرواز را برعهده دارد
        private void FlightDeletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            //اگر تعداد سطر های جدول بیشتر از 0 بود
            if (dg.Items.Count > 0)
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
                string query = "DELETE FROM flights WHERE AirlineID = '" + AR + "'";
                //یک دستور mysql ایجاد میکنیم
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //دستور را اجرا می کنیم
                cmd.ExecuteNonQuery();
                //اسنک بار را نمایش میدهیم با مقدار داده شده
                show_snackbar("پرواز با موفقیت حذف شد");
                //تابع load را صدا میزنیم
                load(null, null);
            }
            else
            {
                //در غیر این صورت اسنک بار را با این مقدار صدا میزنیم
                show_snackbar("موردی برای حذف وجود ندارد");
            }

        }

        //تابعی که در هنگام نوشتن عبارتی در بخش جستجو اجرا می شود
        private void FlightSearchTB_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //اگر فیلد جستجو خالی بود
                if (FlightSearchTB.Text == "")
                    //تابع load را صدا بزن
                    load(null, null);

                //یک اتصال به پایگاه داده ایجاد میکنیم
                MySqlConnection conn = new MySqlConnection(connStr);
                //اتصال را باز میکنیم
                conn.Open();
                //کوئری جستجو را ایجاد میکنیم
                string query = "SELECT * FROM flights where AirlineID like  '%" + FlightSearchTB.Text +
                               "%' or FlightCode like '%" + FlightSearchTB.Text + "%' or FlightDate like '%" + FlightSearchTB.Text + "%' or DepartureTime like '%" + FlightSearchTB.Text + "%' or ArrivalTime like '%" + FlightSearchTB.Text+ "%' or Destination like '%" + FlightSearchTB.Text + "%' or Source like '%" + FlightSearchTB.Text + "%'";
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

    }
}
