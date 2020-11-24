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
            int[] currentArray = new int[MatrixA.GetLength(1)]; 
            for (int a = 0; a < MatrixA.GetLength(0); a++)
            {
                for (int b = 0; b < MatrixA.GetLength(1); b++)
                {

                    currentArray[b] = MatrixA[a, b];
                }
                slicedArray.Add(currentArray);
                currentArray = new int[MatrixA.GetLength(1)];
            }
            return slicedArray;
        }
        private List<int[]> multiplyMatrices(int[,] KeyWordMatrix, int[,] StringToEncryptMatrix)
        {
            List<int[]> listToReturn = new List<int[]>();
            int[][] slicedStringToEncryptMatrix = sliceArray(StringToEncryptMatrix).ToArray();//3*2
            //int[][] slicedKeyWordMatrix = sliceArray(KeyWordMatrix).ToArray();
            int[] currentSumArray = new int[(int)Math.Sqrt(KeyWordMatrix.Length)];
            int currentSum = 0;
            for (int c = 0;c<StringToEncryptMatrix.GetLength(0);c++) {
                for (int b = 0; b < KeyWordMatrix.GetLength(1); b++)
                {
                    for (int a = 0; a < KeyWordMatrix.GetLength(1); a++) {

                        currentSum += slicedStringToEncryptMatrix[c][a] * KeyWordMatrix[a, b];
                    }
                    currentSumArray[b] = currentSum;
                    currentSum = 0;
                }
                listToReturn.Add(currentSumArray);
                currentSumArray = new int[(int)Math.Sqrt(KeyWordMatrix.Length)];
            }
            return listToReturn;
        }
        private void TurnIntoNumericAnalog(List<int> NumericAnalogForStringToEncrypt, string StringToEncrypt)
        {
            foreach (char c in StringToEncrypt)
            {
                NumericAnalogForStringToEncrypt.Add(Alphabet.IndexOf(c));
            }
        }//TODO - rewrite using lambda
        private int[,] GetCorrectMatrixForStringToEncrypt(List<int> KeyWordList, int lenght)//lenght is .GetLength(0); of the main matrix
        {
            int counter = 0;
            while(((double)KeyWordList.ToArray().Length / (double)lenght)%1!= 0)//Math.Sqrt(KeyWordList.ToArray().Length) != lenght)
            {
                KeyWordList.Add(35);
            }

            int[,] keyWordMatrix = new int[KeyWordList.ToArray().Length/lenght, lenght];
            //List<int>

            for (int b=0;b< keyWordMatrix.GetLength(0)/*KeyWordList.ToArray().GetLength(0)*//*KeyWordList.ToArray().Length / lenght*/; b++) {
                for (int a = 0; a < lenght; a++)
                {
                    keyWordMatrix[b, a] = KeyWordList.ToArray()[counter];
                    counter++;
                } 
            }
            return keyWordMatrix;
        }
        private int[,] GetCorrectMatrixForKeyWord(List<int> ListVariant)
        {
            int[] ArrayVariant = ListVariant.ToArray();
            int[,] ArrayToReturn;
            int counter = 0;
            int len = 0;
            for (; ; )
            {
                if ((int)Math.Sqrt(ListVariant.ToArray().Length) - Math.Sqrt(ListVariant.ToArray().Length) != 0)
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
        private int getColumnLength(int[][] jaggedArray, int columnIndex)
        {
            int count = 0;
            foreach (int[] row in jaggedArray)
            {
                if (columnIndex < row.Length) count++;
            }
            return count;
        }
        private T[,] To2D<T>(T[][] source)
        {

                int FirstDim = source.Length;
                int SecondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var result = new T[FirstDim, SecondDim];
                for (int i = 0; i < FirstDim; ++i)
                    for (int j = 0; j < SecondDim; ++j)
                        result[i, j] = source[i][j];

                return result;
        }
        private string divideByModule(List<int[]> listToDivideByModule) {
            int[,] divided = To2D(listToDivideByModule.ToArray());
            
            //List<int[]> meta = new List<int[]>();
            string result="";
            for (int a =0;a< divided.GetLength(0); a++) {
                for (int b=0;b< divided.GetLength(1); b++) {
                    for(; ; ) {
                        if (!(divided[a,b] - Alphabet.Length < 0))
                        {

                            divided[a,b] -= Alphabet.Length;
                        }
                        else {
                            break;
                        }
                    }
                }
            }
            for (int a=0;a<divided.GetLength(0);a++) {

                for (int b = 0; b < divided.GetLength(1); b++)
                {
                    result += Alphabet[divided[a, b]];
                }
            }
            return result;
        }
        public string Encrypt(string StringToEncrypt, string KeyWord)
        {
            StringToEncrypt = StringToEncrypt.ToUpper();
            KeyWord = KeyWord.ToUpper();
            Validate(KeyWord);
            Validate(StringToEncrypt);
            List<int> NumericAnalogForStringToEncrypt = new List<int>();
            TurnIntoNumericAnalog(NumericAnalogForStringToEncrypt, StringToEncrypt);
            List<int> NumericAnalogForKeyWord = new List<int>();
            TurnIntoNumericAnalog(NumericAnalogForKeyWord, KeyWord);
            int[,] StringKeyWord = GetCorrectMatrixForKeyWord(NumericAnalogForKeyWord);//АЛЬПИНИЗМ
            int[,] StringToEncryptMatrix = GetCorrectMatrixForStringToEncrypt(NumericAnalogForStringToEncrypt, StringKeyWord.GetLength(1));
            List<int[]> multiplicationResult= multiplyMatrices(StringKeyWord, StringToEncryptMatrix);//if doesn't work - try swapping arguments
            string result=divideByModule(multiplicationResult);
            Console.WriteLine(result);
            return "";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Crypter crypter = new Crypter();
            crypter.Encrypt("КИМОНО", "КОЛОВОРОТ");
            crypter.Encrypt("ШИФР", "АЛЬПИНИЗМ");//"АЛЬПИНИЗМ"=ключ             n^3 
        }
    }
}
