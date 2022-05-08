using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bulutbilisim.Models.OrmConfigration
{
    public class ResponseModel
    {
        public ResponseModel() { }

        public int Code { get; set; }
        public string Message { get; set; }
        public long id { get; set; }

        public static ResponseModel Basarili(long id = 0)
        {
            ResponseModel response = new ResponseModel();

            response.Code = 100;
            response.Message = "İşlem Başarılı!";
            response.id = id;
          

            return response;
        }


        public static ResponseModel Hatali()
        {
            ResponseModel response = new ResponseModel();

            response.Code = 100;
            response.Message = "İşlem Başarılı!";
            

            return response;
        }
    }
}
