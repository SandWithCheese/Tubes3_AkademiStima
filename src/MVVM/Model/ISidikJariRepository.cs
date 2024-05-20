namespace src.MVVM.Model
{
    public interface ISidikJariRepository : IRepository<SidikJari>
    {
        public IEnumerable<SidikJari> GetByNama(string nama);
        public IEnumerable<SidikJari> GetByBerkasCitra(string berkasCitra);
        public IEnumerable<SidikJari> GetByNamaAndBerkasCitra(string nama, string berkasCitra);
        public void DeleteByNama(string nama);
        public void DeleteByBerkasCitra(string berkasCitra);
        public void DeleteByNamaAndBerkasCitra(string nama, string berkasCitra);
    }
}
