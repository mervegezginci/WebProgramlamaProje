using Microsoft.Build.Framework;
namespace Web.Models
{
	public class Yazar
	{
		public int Id { get; set; }
        [Required]
        public string Ad { get; set; }
		public string Soyad { get; set; }
		public string AdSoyad { get { return Ad + " " + Soyad; } }
	}
}
