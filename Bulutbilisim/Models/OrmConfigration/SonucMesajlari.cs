using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Models.OrmConfigration
{

		public class SonucMesajlari
		{
			public string DonusLinki { get; set; }


			public string Baslik { get; set; }


			public string Mesaj { get; set; }


			public string Durum { get; set; }


			public int ZamanAsimi { get; set; }


		}
		public static class SonucMesajlariUI
		{
			public static SonucMesajlari Response(int _donusKodu, string _mesaj, string _donusLinki = null)
			{
				SonucMesajlari sonucMesaji = null;
				switch (_donusKodu)
				{
					case 100:
						sonucMesaji = Success(_mesaj, _donusLinki);
						break;
					case 101:
						sonucMesaji = Error(_mesaj);
						break;
					case 200:
						sonucMesaji = Success(_mesaj, _donusLinki);
						break;
					case 300:
						sonucMesaji = Warning(_mesaj, _donusLinki);
						break;
					case 400:
						sonucMesaji = Error(_mesaj);
						break;
					case 440:
						sonucMesaji = Captcha(_mesaj, _donusLinki);
						break;
					default:
						sonucMesaji = Warning(_mesaj, _donusLinki);
						break;
				}
				return sonucMesaji;
			}
			public static SonucMesajlari Success(string msg = "", string _donusLinki = null)
			{
				SonucMesajlari result = new SonucMesajlari
				{
					DonusLinki = (_donusLinki ?? string.Empty),
					Mesaj = msg,
					Baslik = "Bilgilendirme",
					Durum = "success",
					ZamanAsimi = 10
				};
				return result;
			}

			public static SonucMesajlari Error(string msg = "İstek işlenirken sorun oluştu. Lütfen tekrar deneyin.")
			{
				SonucMesajlari result = new SonucMesajlari
				{
					DonusLinki = "",
					Mesaj = msg,
					Baslik = "Sistem Uyarısı",
					Durum = "error",
					ZamanAsimi = 10
				};
				return result;
			}

			public static SonucMesajlari Warning(string msg = "", string _donusLinki = null)
			{
				SonucMesajlari result = new SonucMesajlari
				{
					DonusLinki = (_donusLinki ?? string.Empty),
					Mesaj = msg,
					Baslik = "İşlem Eksik Gerçekleşti",
					Durum = "warning",
					ZamanAsimi = 10
				};
				return result;
			}

			public static SonucMesajlari Captcha(string msg = "", string _donusLinki = null)
			{
				SonucMesajlari result = new SonucMesajlari
				{
					DonusLinki = (_donusLinki ?? string.Empty),
					Mesaj = msg,
					Baslik = "Sistem Uyarısı",
					Durum = "captcha",
					ZamanAsimi = 10
				};
				return result;
			}
		}
	}

