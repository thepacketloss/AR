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
    public partial class UserControlTravelAgency : UserControl
    {
        string connStr;

        //سازنده کلاس را تعریف میکنیم
        public UserControlTravelAgency()
        {
            //تابع فراخوانی عناصر ظاهری برنامه را صدا میزنیم
            InitializeComponent();
            //رشته اتصال به پایگاه داده را ایجاد میکنیم
            connStr = "server=localhost;user=root;port=3306;database=airline_reservation;charset=utf8";
            //تابع load را صدا میزنیم
            load(null, null);
        }
        //برای ویرایش آژانس مسافرتی
        //تابعی تعریف میکنیم که خالی نبودن فیلد ها را چک کند

        private bool checkEditReserveEmpty()
        {
            //اگر فیلد شناسه آژانس مسافرتی خالی بود
            if (AgencyIDEditTB.Text == "")
            {
                //اسنک بار را با این پیام نمایش بده
                show_snackbar("لطفا شناسه آژانس را وارد نمایید");
                //برروی فیلد شناسه آژانس مسافرتی فوکس کن
                AgencyIDEditTB.Focus();
                //مقدار false را برگردان
                return false;
            }
            if (AgencyNameEditTB.Text == "")
            {
                show_snackbar("لطفا نام آژانس را وارد نمایید");
                AgencyNameEditTB.Focus();
                return false;
            }
            if (ContactPersonNameEditTB.Text == "")
            {
                show_snackbar("لطفا نام متصدی را وارد نمایید");
                ContactPersonNameEditTB.Focus();
                return false;
            }
            if (PhoneNumberEditTB.Text == "")
            {
                show_snackbar("لطفا شماره تلفن را وارد نمایید");
                PhoneNumberEditTB.Focus();
                return false;
            }
            if (AddressEditTB.Text == "")
            {
                show_snackbar("لطفا آدرس را وارد نمایید");
                AddressEditTB.Focus();
                return false;
            }
            return true;
        }
        //برای افزودن آژانس مسافرتی
        //تابعی تعریف میکنیم که خالی نبودن فیلد ها را چک کند
        private bool checkAddReserveEmpty()
        {
            if (AgencyIDAddTB.Text == "")
            {
                show_snackbar("لطفا شناسه آژانس را وارد نمایید");
                AgencyIDAddTB.Focus();
                return false;
            }
            if (AgencyNameAddTB.Text == "")
            {
                show_snackbar("لطفا نام آژانس را وارد نمایید");
                AgencyNameAddTB.Focus();
                return false;
            }
            if (ContactPersonNameAddTB.Text == "")
            {
                show_snackbar("لطفا نام متصدی را وارد نمایید");
                ContactPersonNameAddTB.Focus();
                return false;
            }
            if (PhoneNumberAddTB.Text == "")
            {
                show_snackbar("لطفا شماره تلفن را وارد نمایید");
                PhoneNumberAddTB.Focus();
                return false;
            }
            if (AddressAddTB.Text == "")
            {
                show_snackbar("لطفا آدرس را وارد نمایید");
                AddressAddTB.Focus();
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
                string queryy = "SELECT * FROM travelagency";
                //یک دستور mysql ایجاد میکنیم
                MySqlCommand ccmd = new MySqlCommand(queryy, conn);
                //یک آداپتور ایجاد میکنیم برای دریافت اطلاعات و نمایش آن ها
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryy, conn);
                //یک جدول مجازی ایجاد میکنیم
                DataTable dt = new DataTable("travelagency");
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

        //تابعی تعریف میکنیم که وظیفه افزودن آژانس مسافرتی را برعهده دارد
        private void TravelAgencyAddbtn_OnClick(object sender, RoutedEventArgs e)
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

                    //کوئری افزودن آژانس مسافرتی را ایجاد میکنیم
                    string query = @"INSERT INTO travelagency (AgencyID,AgencyName,ContactPersonName,PhoneNumber,Address) VALUES ('" + AgencyIDAddTB.Text + "','" + AgencyNameAddTB.Text + "','" + ContactPersonNameAddTB.Text + "','" + PhoneNumberAddTB.Text + "','" + AddressAddTB.Text + "')";
                    //یک دستور mysql ایجاد میکنیم
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    //دستور را ایجاد میکنیم
                    cmd.ExecuteNonQuery();
                    //اسنک بار را مقدار داده شده صدا میزنیم
                    show_snackbar("آژانس مسافرتی با موفقیت اضافه شد");
                    //اتصال را قطع میکنیم
                    conn.Close();
                    //تابع load را صدا میزنیم
                    load(null, null);
                }
                catch (Exception exception)
                {
                    //در غیر این صورت اسنک بار را مقدار داده شده صدا میزنیم
                    show_snackbar("افزودن آژانس مسافرتی ناموفق بود");
                }
            }

        }

        //تابعی تعریف میکنیم که در هنگام کلیک بر دکمه افزودن آژانس مسافرتی اجرا میشود
        private void BtnAddTravelAgencyShowDialog_OnClick(object sender, RoutedEventArgs e)
        {
            //مقادیر فیلد ها را برابر خالی قرار میدهیم
            AgencyIDAddTB.Text = "";
            AgencyNameAddTB.Text = "";
            ContactPersonNameAddTB.Text = "";
            PhoneNumberAddTB.Text = "";
            AddressAddTB.Text = "";
        }

        //تابعی تعریف میکنیم که وظیفه ویرایش آژانس مسافرتی را برعهده دارد
        private void TravelAgencyEditbtn_OnClick(object sender, RoutedEventArgs e)
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

                        //کوئری ویرایش آژانس مسافرتی را ایجاد میکنیم
                        string query = "UPDATE travelagency SET AgencyID='" + AgencyIDEditTB.Text + "', AgencyName='" + AgencyNameEditTB.Text + "', ContactPersonName='" + ContactPersonNameEditTB.Text + "', PhoneNumber='" + PhoneNumberEditTB.Text + "', Address='" + AddressEditTB.Text + "' WHERE AgencyID = " + ID;
                        //یک دستور mysql ایجاد میکنیم
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        //دستور را اجرا میکنیم
                        cmd.ExecuteNonQuery();

                        //اسنک بار را با مقدار داده شده صدا میزنیم
                        show_snackbar("آژانس مسافرتی با موفقیت ویرایش شد");
                        //اتصال را قطع میکنیم
                        conn.Close();
                        //تابع load را صدا میزنیم
                        load(null, null);
                    }
                    catch (Exception exception)
                    {
                        //اسنک بار را با مقدار داده شده صدا میزنیم
                        show_snackbar("ویرایش آژانس مسافرتی ناموفق بود");
                    }


                }
                else
                {
                    //در غیر این صورت اسنک بار را این مقدار صدا میزنیم
                    show_snackbar("موردی برای ویرایش وجود ندارد");
                }


            }

        }

        //تابعی تعریف میکنیم که در هنگام کلیک بر دکمه ویرایش آژانس مسافرتی اجرا میشود
        private void BtnEditShowDialog_OnClick(object sender, RoutedEventArgs e)
        {
            //مقادیر ستون های جدول را از جدول گرفته و در فیلد های ورودی قرار میدهیم
            AgencyIDEditTB.Text = (dg.SelectedCells[0].Column.GetCellContent(dg.SelectedCells[0].Item) as TextBlock).Text;
            AgencyNameEditTB.Text = (dg.SelectedCells[1].Column.GetCellContent(dg.SelectedCells[1].Item) as TextBlock).Text;
            ContactPersonNameEditTB.Text = (dg.SelectedCells[2].Column.GetCellContent(dg.SelectedCells[2].Item) as TextBlock).Text;
            PhoneNumberEditTB.Text = (dg.SelectedCells[3].Column.GetCellContent(dg.SelectedCells[3].Item) as TextBlock).Text;
            AddressEditTB.Text = (dg.SelectedCells[4].Column.GetCellContent(dg.SelectedCells[4].Item) as TextBlock).Text;

        }

        //تابعی تعریف میکنیم که وظیفه حذف آژانس مسافرتی را برعهده دارد
        private void TravelAgencyDeletebtn_OnClick(object sender, RoutedEventArgs e)
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
                string query = "DELETE FROM travelagency WHERE AgencyID = '" + AR + "'";
                //یک دستور mysql ایجاد میکنیم
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //دستور را اجرا می کنیم
                cmd.ExecuteNonQuery();
                //اسنک بار را نمایش میدهیم با مقدار داده شده
                show_snackbar("آژانس مسافرتی با موفقیت حذف شد");
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
        private void TravelAgencySearchTB_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //اگر فیلد جستجو خالی بود
                if (TravelAgencySearchTB.Text == "")
                    //تابع load را صدا بزن
                    load(null, null);

                //یک اتصال به پایگاه داده ایجاد میکنیم
                MySqlConnection conn = new MySqlConnection(connStr);
                //اتصال را باز میکنیم
                conn.Open();
                //کوئری جستجو را ایجاد میکنیم
                string query = "SELECT * FROM travelagency where AgencyID like  '%" + TravelAgencySearchTB.Text +
                               "%' or AgencyName like '%" + TravelAgencySearchTB.Text + "%' or ContactPersonName like '%" + TravelAgencySearchTB.Text + "%' or PhoneNumber like '%" + TravelAgencySearchTB.Text + "%' or Address like '%" + TravelAgencySearchTB.Text + "%'";
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

        private void TravelAgencyLoad(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
