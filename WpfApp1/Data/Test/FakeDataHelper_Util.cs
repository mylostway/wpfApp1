using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bogus;

namespace WpfApp1.Data
{
    internal class NameStruct
    {
        public string FullName { get; set; }
    }

    class PhoneStruct
    {
        public string Phone { get; set; }
    }

    class CertStruct
    {
        public string Cert { get; set; }
    }

    class DateStruct
    {
        public DateTime Date { get; set; }
    }

    class CountryStruct
    {
        public string Country { get; set; }
    }


    class OtherStringStruct
    {
        public string Value { get; set; }
    }



    public partial class FakeDataHelper
    {
        const string DEFAULT_FAKER_LOCALE = "zh_CN";

        const int DEFAULT_GEN_INSTANCE_COUNT = 79;

        protected static List<string> FakeNamesList = null;
        protected static List<string> FakePhoneList = null;
        protected static List<string> FakeCertList = null;
        protected static List<DateTime> FakeDateList = null;
        protected static List<string> FakeCountryList = null;
        protected static List<string> FakeStringList = null;
        protected static List<string> FakeWordList = null;


        static FakeDataHelper()
        {
            // 生成人名
            var faker = new Faker<NameStruct>(DEFAULT_FAKER_LOCALE).StrictMode(true)
                .RuleFor(x => x.FullName, f => f.Person.LastName);
            FakeNamesList = faker.Generate(DEFAULT_GEN_INSTANCE_COUNT).Select(x => x.FullName).ToList();


            // 生成电话
            var faker_phone = new Faker<PhoneStruct>(DEFAULT_FAKER_LOCALE).StrictMode(true)
                .RuleFor(x => x.Phone, f => f.Person.Phone);
            FakePhoneList = faker_phone.Generate(DEFAULT_GEN_INSTANCE_COUNT).Select(x => x.Phone).ToList();

            // 生成身份证
            var faker_cert = new Faker<CertStruct>(DEFAULT_FAKER_LOCALE).StrictMode(true)
                .RuleFor(x => x.Cert, f => f.Commerce.Ean13());
            FakeCertList = faker_cert.Generate(DEFAULT_GEN_INSTANCE_COUNT).Select(x => x.Cert).ToList();


            // 生成日期
            var faker_date = new Faker<DateStruct>(DEFAULT_FAKER_LOCALE).StrictMode(true)
                .RuleFor(x => x.Date, f => f.Person.DateOfBirth);
            FakeDateList = faker_date.Generate(DEFAULT_GEN_INSTANCE_COUNT).Select(x => x.Date).ToList();


            // 生成国家
            var faker_addr = new Faker<CountryStruct>(DEFAULT_FAKER_LOCALE).StrictMode(true)
                .RuleFor(x => x.Country, f => f.Locale);
            FakeCountryList = faker_addr.Generate(DEFAULT_GEN_INSTANCE_COUNT).Select(x => x.Country).ToList();


            // 生成随机字符串
            var faker_str = new Faker<OtherStringStruct>(DEFAULT_FAKER_LOCALE).StrictMode(true)
                .RuleFor(x => x.Value, f => string.Join(" ", f.Lorem.Words(4)));
            FakeStringList = faker_str.Generate(DEFAULT_GEN_INSTANCE_COUNT).Select(x => x.Value).ToList();

            // 生成随机单词
            var faker_word = new Faker<OtherStringStruct>(DEFAULT_FAKER_LOCALE).StrictMode(true)
                .RuleFor(x => x.Value, f => string.Join(" ", f.Lorem.Words(1)));
            FakeWordList = faker_word.Generate(DEFAULT_GEN_INSTANCE_COUNT * 3).Select(x => x.Value).ToList();
        }

        
        public static string GenRandomName()
        {
            return FakeNamesList.ElementAt(Instance.GenRandomInt(FakeNamesList.Count - 1));
        }

        public static string GenRandomPhone()
        {
            return FakePhoneList.ElementAt(Instance.GenRandomInt(FakePhoneList.Count - 1));
        }

        public static string GenRandomCert()
        {
            return FakeCertList.ElementAt(Instance.GenRandomInt(FakeCertList.Count - 1));
        }

        public static DateTime GenRandomDate()
        {
            return FakeDateList.ElementAt(Instance.GenRandomInt(FakeDateList.Count - 1));
        }

        public static string GenRandomCountry()
        {
            return FakeCountryList.ElementAt(Instance.GenRandomInt(FakeCountryList.Count - 1));
        }


        public static string GenRandomString()
        {
            return FakeStringList.ElementAt(Instance.GenRandomInt(FakeStringList.Count - 1));
        }


        public static string GenRandomWord()
        {
            return FakeWordList.ElementAt(Instance.GenRandomInt(FakeStringList.Count - 1));
        }
    }
}
