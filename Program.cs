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
        private List<int[]> sliceArray(int[,] MatrixA)
        {
            List<int[]> slicedArray = new List<int[]>();
            int[] currentArray = new int[(int)Math.Sqrt(MatrixA.Length)]; 
            for (int a = 0; a < currentArray.Length; a++)
            {
                for (int b = 0; b < currentArray.Length; b++)
                {

                    currentArray[b] = MatrixA[a, b];
                }
                slicedArray.Add(currentArray);
                currentArray = new int[(int)Math.Sqrt(MatrixA.Length)];
            }
            return slicedArray;
        }
        private List<int[]> multiplyMatrices(int[,] StringToEncryptMatrix, int[,] KeyWordMatrix)
        {//------------------

            int[][] slicedStringToEncryptMatrix = sliceArray(StringToEncryptMatrix).ToArray();
            int[][] slicedKeyWordMatrix = sliceArray(KeyWordMatrix).ToArray();
            List<int[]> solution = new List<int[]>();
            int[] currentArray;//= new int[slicedArrayA.ToArray().Length];
            int sum = 0;
            for (int a = 0; a < (int)Math.Sqrt(StringToEncryptMatrix.Length); a++)
            {
                currentArray = slicedKeyWordMatrix[a];
                for (int b = 0; b < (int)Math.Sqrt(StringToEncryptMatrix.Length); b++)
                {
                    sum += currentArray[b] * slicedStringToEncryptMatrix[b][a];
                }
                currentArray[a] = sum;
                sum = 0;
            }

            return new List<int[]>();
        }
        private void TurnIntoNumericAnalog(List<int> NumericAnalogForStringToEncrypt, string StringToEncrypt)
        {
            foreach (char c in StringToEncrypt)
            {
                NumericAnalogForStringToEncrypt.Add(Alphabet.IndexOf(c));
            }
        }//TODO - rewrite using lambda
        private int[,] GetCorrectMatrix(List<int> ListVariant,int IfHasTextMatrixDone=-1)
        {
            int[] ArrayVariant = ListVariant.ToArray();
            int[,] ArrayToReturn;
            int counter = 0;
            int len = 0;
            for (; ; )
            {
                if (IfHasTextMatrixDone!=-1) {
                    len = IfHasTextMatrixDone;
                    ArrayToReturn = new int[len, len];
                    //return ListVariant.ToArray();
                    for (int a = 0; a < len; a++)
                    {
                        for (int b = 0; b < len; b++)
                        {
                            ArrayToReturn[a, b] = ArrayVariant[counter];

                            counter++;
                        }
                    }
                    break;
                }
                else if ((int)Math.Sqrt(ListVariant.ToArray().Length) - Math.Sqrt(ListVariant.ToArray().Length) != 0)
                {

                    ListVariant.Add(35);

                }
                else
                {
                    len = (int)Math.Sqrt(ListVariant.ToArray().Length);
                    ArrayToReturn = new int[len, len];
                    //return ListVariant.ToArray();
                    for (int a = 0; a < len; a++)
                    {
                        for (int b = 0; b < len; b++)
                        {
                            ArrayToReturn[a, b] = ArrayVariant[counter];

                            counter++;
                        }
                    }
                    break;
                }
            }
            return ArrayToReturn;
        }
        public string Encrypt(string StringToEncrypt, string KeyWord)
        {
            Validate(KeyWord);
            Validate(StringToEncrypt);
            List<int> NumericAnalogForStringToEncrypt = new List<int>();
            TurnIntoNumericAnalog(NumericAnalogForStringToEncrypt, StringToEncrypt);
            List<int> NumericAnalogForKeyWord = new List<int>();
            TurnIntoNumericAnalog(NumericAnalogForKeyWord, KeyWord);
            int[,] StringToEncryptMatrix = GetCorrectMatrix(NumericAnalogForStringToEncrypt);
            int[,] StringKeyWord = GetCorrectMatrix(NumericAnalogForKeyWord);
            multiplyMatrices(StringKeyWord, StringToEncryptMatrix);//if doesn't work - try swapping arguments
            return "";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Crypter crypter = new Crypter();
            crypter.Encrypt("ШИФР", "АЛЬПИНИЗМ");
        }
    }
}
