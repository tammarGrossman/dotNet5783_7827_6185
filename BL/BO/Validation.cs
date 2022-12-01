
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace BO;

public static class Validation
{
    /// <summary>
    /// the function check the validation of name and adress
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool NameAdress(string name)
    {
        if (name == "")
            return false;
        else if (name.Length < 3)
            return false;
        else if (name.Any(char.IsDigit))
            return false;
        else if (name.All(char.IsLetter))
            return true;
        return true;
    }
    /// <summary>
    /// the function check the validation of email 
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool Email(string email)
    {
        if (email == "")
            return false;
        else if (email.Length < 11)
            return false;
        else if (!email.Contains('@'))
            return false;
        else if (!email.Contains('.'))
            return false;
        else if (IsDigit(email[0]))
            return false; 
          return true;
    }
    /// <summary>
    /// help dunction to check if char isa digit
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool IsDigit(char c)
    {
        bool digit = false;
        for (int i = 0; i < 10; i++)
        {
            if ((int)c == i)
                digit = true;
        }
        return digit;

    }
    /// <summary>
    /// the function check the validation of id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool ID(int id)
    {
        return id < 0 ? false : true;
    }
    public static bool Price(double p)
        {
            if (p < 0)
                return false;
            return true;
        }
    /// <summary>
    /// the function check the validation of the amount in stock
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool InStock(int s)
        {
            return s < 0 ? false: true;
        }

}
