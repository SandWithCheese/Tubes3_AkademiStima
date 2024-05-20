using System;
using System.Collections.Generic;

namespace src.MVVM.Model;

public partial class Biodata
{
    public string Nik { get; set; } = null!;

    public string? Nama { get; set; }

    public string? TempatLahir { get; set; }

    public DateOnly? TanggalLahir { get; set; }

    public string? JenisKelamin { get; set; }

    public string? GolonganDarah { get; set; }

    public string? Alamat { get; set; }

    public string? Agama { get; set; }

    public string? StatusPerkawinan { get; set; }

    public string? Pekerjaan { get; set; }

    public string? Kewarganegaraan { get; set; }
}
