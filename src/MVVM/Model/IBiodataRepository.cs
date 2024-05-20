namespace src.MVVM.Model
{
    public interface IBiodataRepository : IRepository<Biodata>
    {
        public IEnumerable<Biodata> GetByNik(string nik);
        public void DeleteByNik(string nik);
    }
}
