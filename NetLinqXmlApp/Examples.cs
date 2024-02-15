using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NetLinqXmlApp
{
    static class Examples
    {
        public static void CreateDocument()
        {
            //XDocument document = new();

            //XElement bob = new("user");
            //XAttribute state = new("state", "admin");
            //XElement name = new("name", "Bobby");
            //XElement age = new("age", 34);
            //bob.Add(state);
            //bob.Add(name);
            //bob.Add(age);

            //XElement tom = new("user");
            //state = new("state", "member");
            //name = new("name", "Tommy");
            //age = new("age", 25);
            //tom.Add(state);
            //tom.Add(name);
            //tom.Add(age);

            //XElement root = new("users");
            //root.Add(bob);
            //root.Add(tom);

            //XDocument document = new XDocument(new XElement("users",
            //    new XElement("user",
            //        new XAttribute("state", "admin"),
            //        new XElement("name", "Bobby"),
            //        new XElement("age", 34)),
            //    new XElement("user",
            //        new XAttribute("state", "member"),
            //        new XElement("name", "Tommy"),
            //        new XElement("age", 25))
            //    ));

            //document.Save("usersWow.xml");

            //document.Save("users.xml");
        }
        public static void QueryDocument() 
        {
            XDocument document = XDocument.Load("users.xml");

            var root = document.Root;

            if (root is not null)
            {
                foreach (var user in root.Elements())
                {
                    Console.WriteLine($"User name: {user.Element("name")?.Value}");

                    var state = user.Attribute("state")?.Value;
                    Console.WriteLine($"\tstate: {state}");

                    Console.WriteLine($"\tage: {user.Element("age")?.Value}");

                    Console.WriteLine();
                }
            }

            var startB = root?.Elements("user")
                              .Where(u => u.Element("name")!.Value.ToUpper().StartsWith("B"))
                              .Select(u => new
                              {
                                  Name = u.Element("name")?.Value,
                                  Age = u.Element("age")?.Value,
                                  State = u.Attribute("state")?.Value,
                              });

            if (startB is not null)
            {
                foreach (var user in startB)
                    Console.WriteLine($"User name: {user.Name}, state: {user.State}, age: {user.Age}");
            }
            Console.WriteLine();

            var members = root?.Elements("user")
                               .Where(u => u.Attribute("state").Value == "member")
                               .Select(u => new
                               {
                                   Name = u.Element("name")?.Value,
                                   Age = u.Element("age")?.Value,
                                   State = u.Attribute("state")?.Value,
                               });

            if (members is not null)
            {
                foreach (var user in members)
                    Console.WriteLine($"User name: {user.Name}, state: {user.State}, age: {user.Age}");
            }
        }
        public static void EditDocument()
        {
            XDocument document = XDocument.Load("users.xml");

            var root = document.Root;

            // Add new
            /*
            List<User> users = new List<User>()
            {
                new User(){ Name = "Leo", Age = 38 },
                new User(){ Name = "Kim", Age = 24, Admin = true },
                new User(){ Name = "Ben", Age = 22 },
            };



            foreach(var user in users)
            {
                XElement xuser = new("user",
                    new XAttribute("state", ((user.Admin) ? "admin" : "member")),
                    new XElement("name", user.Name),
                    new XElement("age", user.Age)
                    );
                root?.Add(xuser);
            }

            document.Save("users.xml");
            */


            // Update
            /*
            var sam = root.Elements("user")?
                          .FirstOrDefault(u => u.Element("name")?.Value == "Sam");

            if(sam is not null)
            {
                sam.Attribute("state")!.Value = "admin";
                sam.Element("age")!.Value = 40.ToString();

                document.Save("users.xml");
            }
            */


            // Delete

            var kim = root.Elements("user")
                          .FirstOrDefault(u => u.Element("name")!.Value == "Kim");
            if (kim is not null)
            {
                kim.Remove();
                document.Save("users.xml");
            }
        }
    }
}
