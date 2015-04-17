using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ValidateParseXMLWithSchema
{
    class Program
    {
        static void Main(string[] args)
        {
            string errors;
            bool isValid = IsXmlFileValid("Message.xml", out errors);
            Console.WriteLine("document Message.xsd {0}", isValid ? "validated" : "did not validate");
            Console.WriteLine(errors);
            Console.WriteLine();

            isValid = IsXmlFileValid("MessageInvalid.xml", out errors);
            Console.WriteLine("document MessageInvalid.xml {0}", isValid ? "validated" : "did not validate");
            Console.WriteLine(errors);

            //**************************** overloads ****************************

            //read XML file to string
            string xml = File.ReadAllText("Message.xml");
            isValid = IsXmlValid(xml, out errors);
            Console.WriteLine("document Message.xsd {0}", isValid ? "validated" : "did not validate");
            Console.WriteLine(errors);
            Console.WriteLine();

            xml = File.ReadAllText("MessageInvalid.xml");
            isValid = IsXmlValid(xml, out errors);
            Console.WriteLine("document Message.xsd {0}", isValid ? "validated" : "did not validate");
            Console.WriteLine(errors);
            Console.WriteLine();

            Console.ReadKey();
        }

        private static bool IsXmlFileValid(string xmlFileNameToValidate, out string errors)
        {
            var schemaSet = new XmlSchemaSet();
            schemaSet.Add("", "Message.xsd");

            var document = XDocument.Load(xmlFileNameToValidate);
            bool isValid = true;
            var errorList = new StringBuilder();

            document.Validate(schemaSet, (o, e) =>
            {
                errorList.AppendLine(e.Message);
                isValid = false;
            });

            errors = errorList.ToString();

            return isValid;
        }

        private static bool IsXmlValid(string xmlToValidate, out string errors)
        {
            var schemaSet = new XmlSchemaSet();
            schemaSet.Add("", "Message.xsd");

            XDocument document = XDocument.Parse(xmlToValidate);

            bool isValid = true;
            var errorList = new StringBuilder();

            document.Validate(schemaSet, (o, e) =>
            {
                errorList.AppendLine(e.Message);
                isValid = false;
            });

            errors = errorList.ToString();

            return isValid;
        }

    }
}
