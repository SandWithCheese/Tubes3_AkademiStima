using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using src.MVVM.Model;

namespace src;

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

        foreach (var biodata in Biodata)
        {
            Console.WriteLine(biodata.Nik);
            Console.WriteLine(biodata.Nama);
        }

        // foreach (var sidikJari in SidikJari)
        // {
        //     Console.WriteLine(sidikJari.BerkasCitra);
        //     Console.WriteLine(sidikJari.Nama);
        // }
    }
}