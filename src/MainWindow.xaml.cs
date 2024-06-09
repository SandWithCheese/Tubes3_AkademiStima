using Microsoft.Win32;
using src.Algorithm;
using src.Encryption;
using src.MVVM.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;


namespace src
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Biodata> Biodata { get; set; }
        public ObservableCollection<SidikJari> SidikJari { get; set; }
        public ObservableCollection<Biodata> Result { get; set; }
        private readonly BiodataRepository _biodataRepository;
        private readonly SidikJariRepository _sidikJariRepository;
        private readonly byte[] _aesKey;
        private readonly byte[] _aesIv;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _biodataRepository = new BiodataRepository("Database/database.db");
            Biodata = new ObservableCollection<Biodata>(_biodataRepository.GetAll());

            _sidikJariRepository = new SidikJariRepository("Database/database.db");
            SidikJari = new ObservableCollection<SidikJari>(_sidikJariRepository.GetAll());

            Result = [];

            DotNetEnv.Env.Load(".env");
            _aesKey = Convert.FromBase64String(DotNetEnv.Env.GetString("AES_KEY"));
            _aesIv = Convert.FromBase64String(DotNetEnv.Env.GetString("AES_IV"));
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
            }
        }

        private Tuple<SidikJari?, double> FindSidikJariKMP()
        {
            string imagePath = new Uri(sourceImage.Source.ToString()!).LocalPath;
            string ascii = Converter.ConvertImgToAsciiFromBottomCenter(imagePath);
            SidikJari? mostSimilarSidikJari = null;
            double highestSimilarity = 0;

            foreach (var sidikJari in SidikJari)
            {
                string imagePath2 = sidikJari.BerkasCitra!;
                string ascii2 = Converter.ConvertImgToAscii(imagePath2);

                bool kmpRes = KnuthMorrisPratt.KMPSearch(ascii2, ascii);

                if (kmpRes)
                {
                    return new Tuple<SidikJari?, double>(sidikJari, 1);
                }

                double similarity = LongestCommonSubsequence.CalculateSimilarity(ascii, ascii2);
                if (similarity > highestSimilarity)
                {
                    mostSimilarSidikJari = sidikJari;
                    highestSimilarity = similarity;
                }
            }

            return new Tuple<SidikJari?, double>(mostSimilarSidikJari, highestSimilarity);
        }

        private Tuple<SidikJari?, double> FindSidikJariBM()
        {
            string imagePath = new Uri(sourceImage.Source.ToString()!).LocalPath;
            string ascii = Converter.ConvertImgToAsciiFromBottomCenter(imagePath);
            SidikJari? mostSimilarSidikJari = null;
            double highestSimilarity = 0;

            foreach (var sidikJari in SidikJari)
            {
                string imagePath2 = sidikJari.BerkasCitra!;
                string ascii2 = Converter.ConvertImgToAscii(imagePath2);

                bool bmRes = BoyerMoore.BMSearch(ascii2, ascii);

                if (bmRes)
                {
                    return new Tuple<SidikJari?, double>(sidikJari, 1);
                }

                double similarity = LongestCommonSubsequence.CalculateSimilarity(ascii, ascii2);
                if (similarity > highestSimilarity)
                {
                    mostSimilarSidikJari = sidikJari;
                    highestSimilarity = similarity;
                }
            }

            return new Tuple<SidikJari?, double>(mostSimilarSidikJari, highestSimilarity);
        }

        private Biodata? FindBiodataFromSidikJari(SidikJari sidikJari)
        {
            string nama = sidikJari.Nama!;

            foreach (var biodata in Biodata)
            {
                string correctedNama = RegexGaming.FixAlayWord(nama, biodata.Nama!);

                Console.WriteLine(nama);
                Console.WriteLine(correctedNama);

                if (correctedNama == nama)
                {
                    return biodata;
                }
            }

            return null;
        }

        private void searchImage(object sender, RoutedEventArgs e)
        {
            if (sourceImage.Source == null)
            {
                MessageBox.Show("Please upload an image first.");
                return;
            }

            if (BM.IsChecked == false && KMP.IsChecked == false)
            {
                MessageBox.Show("Please select a search method (BM or KMP).");
                return;
            }

            resultImage.Source = null;
            Result.Clear();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double similarity = 0;
            bool foundMatch = false;

            // If BM is checked
            if (BM.IsChecked == true)
            {
                Tuple<SidikJari?, double> result = FindSidikJariBM();
                SidikJari? sidikJari = result.Item1;
                similarity = result.Item2;

                if (sidikJari != null)
                {
                    Biodata? biodata = FindBiodataFromSidikJari(sidikJari);

                    if (biodata != null)
                    {
                        Biodata fixedBiodata = new()
                        {
                            Nik = biodata.Nik,
                            Nama = sidikJari.Nama!,
                            TempatLahir = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.TempatLahir!), _aesKey, _aesIv)),
                            TanggalLahir = biodata.TanggalLahir,
                            JenisKelamin = biodata.JenisKelamin,
                            GolonganDarah = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.GolonganDarah!), _aesKey, _aesIv)),
                            Alamat = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.Alamat!), _aesKey, _aesIv)),
                            Agama = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.Agama!), _aesKey, _aesIv)),
                            StatusPerkawinan = biodata.StatusPerkawinan,
                            Pekerjaan = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.Pekerjaan!), _aesKey, _aesIv)),
                            Kewarganegaraan = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.Kewarganegaraan!), _aesKey, _aesIv)),
                        };

                        Result.Add(fixedBiodata);

                        string imagePath = Path.GetFullPath(sidikJari.BerkasCitra!);

                        Uri imageUri;
                        if (Uri.TryCreate(imagePath, UriKind.Absolute, out imageUri!))
                        {
                            resultImage.Source = new BitmapImage(imageUri);
                        }

                        foundMatch = true;
                    }
                }
            }

            // If KMP is checked
            if (KMP.IsChecked == true)
            {
                Tuple<SidikJari?, double> result = FindSidikJariKMP();
                SidikJari? sidikJari = result.Item1;
                similarity = result.Item2;

                if (sidikJari != null)
                {
                    Biodata? biodata = FindBiodataFromSidikJari(sidikJari);

                    if (biodata != null)
                    {
                        Biodata fixedBiodata = new()
                        {
                            Nik = biodata.Nik,
                            Nama = sidikJari.Nama!,
                            TempatLahir = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.TempatLahir!), _aesKey, _aesIv)),
                            TanggalLahir = biodata.TanggalLahir,
                            JenisKelamin = biodata.JenisKelamin,
                            GolonganDarah = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.GolonganDarah!), _aesKey, _aesIv)),
                            Alamat = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.Alamat!), _aesKey, _aesIv)),
                            Agama = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.Agama!), _aesKey, _aesIv)),
                            StatusPerkawinan = biodata.StatusPerkawinan,
                            Pekerjaan = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.Pekerjaan!), _aesKey, _aesIv)),
                            Kewarganegaraan = Encoding.UTF8.GetString(AES.Decrypt(Convert.FromBase64String(biodata.Kewarganegaraan!), _aesKey, _aesIv)),
                        };

                        Result.Add(fixedBiodata);

                        string imagePath = Path.GetFullPath(sidikJari.BerkasCitra!);

                        Uri imageUri;
                        if (Uri.TryCreate(imagePath, UriKind.Absolute, out imageUri!))
                        {
                            resultImage.Source = new BitmapImage(imageUri);
                        }

                        foundMatch = true;
                    }
                }
            }

            stopwatch.Stop();

            executionTimeText.Text = $"Execution Time: {stopwatch.ElapsedMilliseconds} ms";
            matchPercentageText.Text = foundMatch ? $"Matches Percentage: {similarity * 100}%" : "Matches Percentage: -";

            if (!foundMatch)
            {
                MessageBox.Show("No match found.");
            }
        }
    }
}