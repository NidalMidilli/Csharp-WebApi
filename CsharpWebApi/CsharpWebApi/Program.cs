using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CsharpWebApi
{
     class Program
    {
        static void Main(string[] args)
        {
            string apiUrl = "https://jsonplaceholder.typicode.com/posts";
            var data = HttpHelper.GetDataFromApi<List<Post>>(apiUrl).Result;

            Console.Read();
        }
    }


    /*
     -----Http Helper sınıfımın içerisinde tanımladıgımız metodda 
    Http Client ile metoda gonderilen urlden dönen JSON verisinin 
    oluşturulan Post classındaki verilerle çalışmasını sağlamak için jsonconvert.Deserialize
    yapısı kullanılmıştır.


    ------GetDataFromApinin bulunduğu yerde ise dönüş tipim List<Post> tur.
    Apiden gelen JSON verisi Post sınıfına eşlenerek bir liste haline gelmiştir.

     

     */

    public class HttpHelper
    {
        public static async Task<T> GetDataFromApi<T>(string url)
        {
            using(var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }


        }


    }



    public class Post
    {

        public int userId { get; set; }
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

    }
}
