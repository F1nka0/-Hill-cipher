using System;
using System.Collections.Generic;
using System.Linq;
namespace Crypto
{
    
    class Crypter
    {
        private static string Alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ., ?";
        private void Validate(string StringToValidate)
        {

            foreach (char ch in StringToValidate.ToUpper())
            {

                if (!Alphabet.Contains(ch))
                {
                    throw new Exception("String was not valid");
                }
            }
        }
        private void TurnIntoNumericAnalog(List<int> NumericAnalogForStringToEncrypt,string StringToEncrypt){
            foreach (char c in StringToEncrypt)
            {
                NumericAnalogForStringToEncrypt.Add(Alphabet.IndexOf(c));
            }
        }
        public string Encrypt(string StringToEncrypt,string KeyWord)
        {
            Validate(KeyWord);
            Validate(StringToEncrypt);
            List<int> NumericAnalogForStringToEncrypt = new List<int>();
            TurnIntoNumericAnalog(NumericAnalogForStringToEncrypt,StringToEncrypt);
            List<int> NumericAnalogForKeyWord = new List<int>();
            TurnIntoNumericAnalog(NumericAnalogForKeyWord,KeyWord);
            List<int> 
            return ""; 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Crypter crypter = new Crypter();
            crypter.Encrypt("ШИФР","АЛЬПИНИЗМ");
        }
    }
}
