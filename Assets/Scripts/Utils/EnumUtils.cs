
using System.ComponentModel;
using System.Reflection;
using System;

public class EnumUtils
{
    //método usado para buscar o atributo "Description" de um tipo como ColetavelName
    public static string GetEnumDescription(Enum en)
    {
        Type type = en.GetType();

        MemberInfo[] memInfo = type.GetMember(en.ToString());

        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return en.ToString();
    }
}
