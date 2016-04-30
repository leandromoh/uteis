using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleInputHelper
{
    public static class ConsoleHelper
    {
        private static Regex _currentRegex;
        private static Regex _regexNumber = new Regex(@"^\d+$");
        private static Regex _regexDate = new Regex(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}$");
        private static Regex _regexString = new Regex(@"\w+");
        private static int _i;
        private static string _input, _mensagem;
        
        public static string defaultErrorMsg = "Msg Default de Erro ainda não setada";

        public static int getNumber(string mensagem, string specificErrorMsg = null)
        {
            return Int32.Parse(setProperties(_regexNumber, mensagem, specificErrorMsg));
        }

        public static string getDate(string mensagem, string specificErrorMsg = null)
        {
            return setProperties(_regexDate, mensagem, specificErrorMsg);
        }

        public static string getString(string mensagem, string specificErrorMsg = null)
        {
            return setProperties(_regexString, mensagem, specificErrorMsg);
        }

        public static string setProperties(Regex regex, string mensagem, string specificErrorMsg) 
        {
            _currentRegex = regex;
            _mensagem = mensagem;
            _getInput(getErrorMsg(specificErrorMsg));

            return _input; 
        }

        private static string getErrorMsg(string errorMsg)
        {
            return !String.IsNullOrEmpty(errorMsg)? errorMsg : defaultErrorMsg;
        }

        private static void _getInput(string strError)
        {
            Console.Write(_mensagem);

            _input = Console.ReadLine();

            while (!_currentRegex.IsMatch(_input))
            {
                Console.Write(strError);
                _input = Console.ReadLine();
            }
        }
    }
}
