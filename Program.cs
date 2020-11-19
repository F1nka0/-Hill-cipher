using System;
using System.Collections.Generic;
using System.Linq;
//using System.Windows.Media;
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
        }//TODO - rewrite using lambda
        private int[,] GetCorrectMatrix(List<int> ListVariant) {
            int[] ArrayVariant = ListVariant.ToArray();
            int[,] ArrayToReturn;
            int counter=0;
            int len = 0;
            for (; ; ) {

                if ((int)Math.Sqrt(ListVariant.ToArray().Length)- Math.Sqrt(ListVariant.ToArray().Length)!=0) {

                    ListVariant.Add(35);
                    
                }
                else{
                    len = (int)Math.Sqrt(ListVariant.ToArray().Length);
                    ArrayToReturn = new int[len,len];
                    //return ListVariant.ToArray();
                    for (int a=0;a<len;a++) {
                        for (int b=0;b<len;b++) {
                            ArrayToReturn[a, b] = ArrayVariant[counter];

                             counter++;
                        }
                    }
                    break;
                }
            }
            return ArrayToReturn;
        }
        public string Encrypt(string StringToEncrypt,string KeyWord)
        {
            Validate(KeyWord);
            Validate(StringToEncrypt);
            List<int> NumericAnalogForStringToEncrypt = new List<int>();
            TurnIntoNumericAnalog(NumericAnalogForStringToEncrypt,StringToEncrypt);
            List<int> NumericAnalogForKeyWord = new List<int>();
            TurnIntoNumericAnalog(NumericAnalogForKeyWord,KeyWord);
            int[,] StringToEncryptMatrix = new int[,] { };
            int[,] StringKeyWord= new int[,] { };
            GetCorrectMatrix(NumericAnalogForKeyWord);
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
