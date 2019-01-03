using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PhoneBookApi.Models;
using System.Xml.Linq;
using System;

namespace PhoneBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        public PhoneBookController()
        {
        }

        /// <summary>
        /// Retrieve all items from PhoneBook.
        /// </summary>
        /// <returns>PhoneBook items List</returns>
        // GET: api/PhoneBook
        [HttpGet]
        public IActionResult GetPhoneBookItems()
        {
            List<PhoneBookItem> PhoneBookItems = new List<PhoneBookItem>();
            XDocument doc = XDocument.Load("data.xml");
            foreach (XElement element in doc.Descendants("phonebookitems")
                .Descendants("phonebookitem"))
            {
                PhoneBookItem phonebookitem = new PhoneBookItem
                {
                    Id = Convert.ToInt64(element.Element("id").Value),
                    Name = element.Element("name").Value,
                    Type = (PhoneTypeEnumeration.PhoneType)Enum.Parse(typeof(PhoneTypeEnumeration.PhoneType), element.Element("type").Value),
                    Number = element.Element("number").Value
                };

                PhoneBookItems.Add(phonebookitem);
                PhoneBookItems = PhoneBookItems.OrderBy(p => p.Name).ToList();
            }

            return Ok(PhoneBookItems);
        }

        /// <summary>
        /// Returns a PhoneBook item matching the given id.
        /// </summary>
        /// <param name="id">Id of item to be retrieved</param>
        /// <returns>PhoneBook item</returns>
        // GET: api/PhoneBook/5
        [HttpGet("{id}")]
        public IActionResult GetPhoneBookItem(long id)
        {
            XDocument doc = XDocument.Load("data.xml");
            XElement element = doc.Element("phonebookitems").Elements("phonebookitem").Elements("id").SingleOrDefault(x => x.Value == id.ToString());

            XElement parent = element.Parent;

            PhoneBookItem phonebookitem = new PhoneBookItem
            {
                Id = Convert.ToInt64(parent.Element("id").Value),
                Name = parent.Element("name").Value,
                Type = (PhoneTypeEnumeration.PhoneType)Enum.Parse(typeof(PhoneTypeEnumeration.PhoneType), parent.Element("type").Value),
                Number = parent.Element("number").Value
            };
            return Ok(phonebookitem);
        }

        /// <summary>
        /// Insert a PhoneBook item.
        /// </summary>
        /// <returns>Inserts new PhoneBook item in data.xml and saves the file</returns>
        //POST: api/PhoneBook
        [HttpPost]
        public void PostPhoneBookItem(PhoneBookItem PhoneBookItem)
        {
            int maxId;
            XDocument doc = XDocument.Load("data.xml");
            bool elementExist = doc.Descendants("phonebookitem").Any();
            if (elementExist) { 
            maxId = doc.Descendants("phonebookitem").Max(x => (int)x.Element("id"));
            } else
            {
                maxId = 0;
            }
            XElement root = new XElement("phonebookitem");
            root.Add(new XElement("id", maxId + 1));
            root.Add(new XElement("name", PhoneBookItem.Name));
            root.Add(new XElement("type", PhoneBookItem.Type));
            root.Add(new XElement("number", PhoneBookItem.Number));
            doc.Element("phonebookitems").Add(root);
            doc.Save("data.xml");
        }

        /// <summary>
        /// Updates a PhoneBook item matching the given id.
        /// </summary>
        /// <param name="id">Id of item to be retrieved</param>
        /// <param name="PhoneBookItem">Retrieved PhoneBook item</param>
        /// <returns>Updates PhoneBook item in data.xml and saves the file</returns>
        //PUT: api/PhoneBook/5
        [HttpPut("{id}")]
        public void PostPhoneBookItem(long id, PhoneBookItem PhoneBookItem)
        {
            XDocument doc = XDocument.Load("data.xml");

            var items = from item in doc.Descendants("phonebookitem")
                        where item.Element("id").Value == id.ToString()
                        select item;

            foreach (XElement itemElement in items)
            {
                itemElement.SetElementValue("name", PhoneBookItem.Name);
                itemElement.SetElementValue("type", PhoneBookItem.Type);
                itemElement.SetElementValue("number", PhoneBookItem.Number);
            }

            doc.Save("data.xml");
        }

        /// <summary>
        /// Delete a PhoneBook item matching the given id.
        /// </summary>
        /// <param name="id">Id of item to be retrieved</param>
        /// <returns>Deletes item from data.xml and saves the file</returns>
        // DELETE: api/PhoneBook/5
        [HttpDelete("{id}")]
        public void DeletePhoneBookItem(long id) {

            XDocument doc = XDocument.Load("data.xml");
            var elementToDelete = from item in doc.Descendants("phonebookitem")
                                   where item.Element("id").Value == id.ToString()
                                   select item;

            elementToDelete.Remove();

            doc.Save("data.xml");
        }
    }
}