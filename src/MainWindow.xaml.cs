using Microsoft.Win32;
using src.Encryption;
using src.MVVM.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using src.Algorithm;


namespace src
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Biodata> Biodata { get; set; }
        public ObservableCollection<SidikJari> SidikJari { get; set; }
        private readonly BiodataRepository _biodataRepository;
        private readonly SidikJariRepository _sidikJariRepository;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _biodataRepository = new BiodataRepository("Database/database.db");
            Biodata = new ObservableCollection<Biodata>(_biodataRepository.GetAll());

            _sidikJariRepository = new SidikJariRepository("Database/database.db");
            SidikJari = new ObservableCollection<SidikJari>(_sidikJariRepository.GetAll());

            DotNetEnv.Env.Load(".env");
            byte[] key = Convert.FromBase64String(DotNetEnv.Env.GetString("AES_KEY"));
            byte[] iv = Convert.FromBase64String(DotNetEnv.Env.GetString("AES_IV"));

            // foreach (var biodata in Biodata)
            // {
            //     Console.WriteLine(biodata.Nik);
            //     Console.WriteLine(biodata.Nama);
            //     Console.WriteLine(Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.Alamat!), key, iv)));
            // }

            // foreach (var sidikJari in SidikJari)
            // {

            //     string imagePath = "test/557__M_Left_little_finger.BMP";
            //     // string imagePath2 = "test/95__M_Left_little_finger.BMP";
            //     // string imagePath2 = "test/9__M_Left_little_finger.BMP";
            //     string imagePath2 = sidikJari.BerkasCitra!;
            //     string ascii = Converter.ConvertImgToAsciiFromBottomCenter(imagePath);
            //     string ascii2 = Converter.ConvertImgToAscii(imagePath2);

            //     bool kmpRes = KnuthMorrisPratt.KMPSearch(ascii2, ascii);
            //     bool bmRes = BoyerMoore.BMSearch(ascii2, ascii);

            //     if (kmpRes)
            //     {
            //         Console.WriteLine("KMP: Matched");
            //         Console.WriteLine("Matched with: " + sidikJari.Nama);
            //     }
            //     // else
            //     // {
            //     //     Console.WriteLine("KMP: Not Matched");
            //     //     string ascii3 = Converter.ConvertImgToAscii(imagePath);
            //     //     double similarity = LongestCommonSubsequence.CalculateSimilarity(ascii3, ascii2);
            //     //     Console.WriteLine($"Similarity: {similarity}%");
            //     // }

            //     if (bmRes)
            //     {
            //         Console.WriteLine("BM: Matched");
            //         Console.WriteLine("Matched with: " + sidikJari.Nama);
            //     }
            //     // else
            //     // {
            //     //     Console.WriteLine("BM: Not Matched");
            //     //     string ascii3 = Converter.ConvertImgToAscii(imagePath);
            //     //     double similarity = LongestCommonSubsequence.CalculateSimilarity(ascii3, ascii2);
            //     //     Console.WriteLine($"Similarity: {similarity}%");
            //     // }
            // }

        }

        private void uploadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            // Call the ShowDialog method to show the dialog box.
            bool? result = openFileDialog.ShowDialog();

            // Process input if the user clicked OK.
            if (result == true)
            {
                // Open the selected file to read.
                string filePath = openFileDialog.FileName;
                string fileName = openFileDialog.SafeFileName;

                sourceImage.Source = new BitmapImage(new Uri(filePath));
            }
            else
            {
                MessageBox.Show("No file selected.");
            }
        }

        private void algorithmChecked(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRadioButton = sender as RadioButton;

            if (selectedRadioButton != null)
            {
                string selectedAlgorithm = selectedRadioButton.Content.ToString();
                // You can use 'selectedAlgorithm' here as needed
            }
        }

        private void searchImage(object sender, RoutedEventArgs e)
        {
            if (sourceImage.Source == null)
            {
                MessageBox.Show("Please upload an image first.");
                return;
            }

            Biodata.Clear();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string selectedAlgorithm = "";

            // If BM is checked
            if (BM.IsChecked == true)
            {
                Biodata.Add(new Biodata
                {
                    Nik = "1234567890123456",
                    Nama = "BM",
                    TempatLahir = "Jakarta",
                    TanggalLahir = DateOnly.FromDateTime(DateTime.Now),
                    JenisKelamin = "Male",
                    GolonganDarah = "O",
                    Alamat = "Jl. Dummy Address",
                    Agama = "Islam",
                    StatusPerkawinan = "Married",
                    Pekerjaan = "Software Engineer",
                    Kewarganegaraan = "Indonesian"
                });
            }

            // If KMP is checked
            if (KMP.IsChecked == true)
            {
                Biodata.Add(new Biodata
                {
                    Nik = "1234567890123456",
                    Nama = "KMP",
                    TempatLahir = "Jakarta",
                    TanggalLahir = DateOnly.FromDateTime(DateTime.Now),
                    JenisKelamin = "Male",
                    GolonganDarah = "O",
                    Alamat = "Jl. Dummy Address",
                    Agama = "Islam",
                    StatusPerkawinan = "Married",
                    Pekerjaan = "Software Engineer",
                    Kewarganegaraan = "Indonesian"
                });
            }

            stopwatch.Stop();

            // Display execution time and match percentage
            executionTimeText.Text = $"Execution Time: {stopwatch.ElapsedMilliseconds} ms";
            matchPercentageText.Text = "Matches Percentage: 100%";  // Example match percentage
        }
    }
}