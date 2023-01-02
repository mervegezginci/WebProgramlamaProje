using Microsoft.Build.Framework;
using System.ComponentModel;

namespace Web.Models
{
	public class Kitap
	{
		public int Id { get; set; }
        [Required]
        public string KitapAdi { get; set; }
		public int sayfaSayisi { get; set; }
		public int? basimYili { get; set; }
        [Required]
        public string konu { get; set; }

		//[DisplayName("Dosya Yükleme")]
		//public IFormFile resimDosyasi { get; set; }

		public int? KategoriId { get; set; }
		public Kategori Kategori { get; set; }
		public int? DilId { get; set; }
		public Dil dil { get; set; }
		public ICollection<Yorumlar> yorumlar { get; set; }
	}
}
