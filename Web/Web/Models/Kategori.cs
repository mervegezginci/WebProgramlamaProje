using Microsoft.Build.Framework;
namespace Web.Models
{
	public class Kategori
	{
		public int Id { get; set; }
        [Required]
        public string KategoriAd { get; set; }
		//public ICollection<Kitap> Kitap { get; set; }
	}
}
