using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NiboOFX.Models;

namespace NiboOFX.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly ApplicationContext db;

        public SampleDataController(ApplicationContext dataBase)
        {
            db = dataBase;
        }

        [HttpGet("[action]")]
        public List<TransactionT> Transactions()
        {
            return db.Transactions.ToList();
        }

        [HttpPost("[action]")]
        public void PostFile()
        {
            var transactionsList = new HashSet<TransactionT>(new TransactionTComparer());

            var files = Request.Form.Files;

            foreach (var file in files)
            {
                try
                {
                    var fileStream = new StreamReader(file.OpenReadStream());
                    string line = "";
                    string concatenatedValues = "";
                    int count = 0;

                    while (!fileStream.EndOfStream || line.Contains("</STMTTRN>"))
                    {
                        line = fileStream.ReadLine();
                        if (line.Contains("<TRNTYPE>") || line.Contains("<DTPOSTED>") || line.Contains("<TRNAMT>") || line.Contains("<MEMO>"))
                        {
                            concatenatedValues += line.Split('>')[1] + ",";
                            count++;
                        }
                        if (count == 4)
                        {
                            var arrayValues = concatenatedValues.Split(',');
                            transactionsList.Add(new TransactionT(arrayValues[0], DateTime.ParseExact(arrayValues[1].Substring(0, 8), "yyyyMMdd", null), Convert.ToDecimal(arrayValues[2]), arrayValues[3]));
                            count = 0;
                            concatenatedValues = "";
                        }
                    }
                }
                catch (Exception e)
                {
                    // log write -> e.message 
                }

                
                db.Transactions.AddRange(transactionsList);
                db.SaveChanges();
            }
        }
    }
}
