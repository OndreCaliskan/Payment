namespace Payment.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }  // Toplam öğe sayısı
        public int ItemsPerPage { get; set; }  // Sayfa başına öğe sayısı
        public int CurrentPage { get; set; }  // Şu anki sayfa numarası
        public int TotalPages { get; set; }   // Toplam sayfa sayısı
    }
}
