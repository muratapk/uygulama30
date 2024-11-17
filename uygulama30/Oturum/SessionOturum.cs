using Newtonsoft.Json;
namespace uygulama30.Oturum
{
    public static class SessionOturum
    {
        //burada session içindeki veri Json Formatına dönüştürüyoruz.
        public static void SetJson(this ISession session, string key,object value)
        {
            session.SetString(key,JsonConvert.SerializeObject(value));
        }
        //session içindeki json yapısını deserialized ile obje dönüştürüyoruz.
        public static T GetJson<T>(this ISession session, string key)
        {

            var sessionData=session.GetString(key);
            return sessionData==null?default(T):JsonConvert.DeserializeObject<T>(sessionData);
            //a==b?doğru:yanlış buda bir if yapısı kısa if yapısı burada kullanılan budur
        }

        
    }
}
