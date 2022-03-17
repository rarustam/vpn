using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPN
{
    public partial class loginForm : Form
    {
        // Перемени
        public static string SetValueForLogin = "", SetValueForVPN = "";
        string[] img;
        public loginForm()
        {
            InitializeComponent();
        }

        // После нажать клавиш Enter курсор перейти на другой поля
        private void iconComboCountry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtLogin.Focus();
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtPassword.Focus();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnConnect.Focus();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            mainForm main = new mainForm(this);
            if (txtLogin.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                SetValueForLogin = txtLogin.Text.Trim();
                SetValueForVPN = iconComboCountry.Text.Trim();
                main.ShowDialog();
            }
        }

        // Определить код страны
        public string GetCountryCode(string country)
        {
            string countryCode = "";
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.LCID);
                if (region.EnglishName.ToUpper().Contains(country.ToUpper()))
                {
                    countryCode = region.TwoLetterISORegionName;
                }
            }
            return countryCode;
        }

        //Скачать иконка флаг из интернет
        public Image DownloadImage(string fromUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                using (Stream stream = webClient.OpenRead(fromUrl))
                {
                    return Image.FromStream(stream);
                }
            }
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            // Список страны для выбор VPN
            string[] ct = new string[] {"Bangladesh", "Brazil", "China", "Congo",
                        "Egypt", "Ethiopia", "France", "Germany",
                        "India", "Indonesia", "Iran", "Iraq", "Italy",
                        "Japan", "Korea", "Mexico", "Myanmar", "Nigeria",
                        "Pakistan", "Philippines", "Russian Federation",
                        "South Africa", "Spain", "Thailand", "Turkey",
                        "Tajikistan", "United Kingdom", "United States", "Viet Nam"};

            for (int i = 0; i < ct.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(GetCountryCode(ct[i])))
                {
                    imageCountry.Images.Add(DownloadImage("http://www.geognos.com/api/en/countries/flag/" + GetCountryCode(ct[i]) + ".png"));
                }
            }

            img = new string[imageCountry.Images.Count - 1];
            for (int i = 0; i < imageCountry.Images.Count - 1; i++)
            {
                img[i] = ct[i];
            }

            iconComboCountry.Items.AddRange(img);
            iconComboCountry.DrawMode = DrawMode.OwnerDrawVariable;
            iconComboCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            iconComboCountry.Width = imageCountry.ImageSize.Width + 186;
            iconComboCountry.MaxDropDownItems = 4;
        }

        private void iconComboCountry_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                e.Graphics.DrawImage(imageCountry.Images[e.Index], e.Bounds.Left, e.Bounds.Top, 20, 20);
                e.Graphics.DrawString(img[e.Index], e.Font, new SolidBrush(Color.Red), 55, e.Bounds.Top + 2);
            }
        }

        private void iconComboCountry_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = imageCountry.ImageSize.Height + 20;
            e.ItemWidth = imageCountry.ImageSize.Width + 20;
        }
    }
}
