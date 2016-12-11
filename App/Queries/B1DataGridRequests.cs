using App.Extensions;
using Repositories;
using Services;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Queries
{
    public static class B1DataGridRequests
    {
        public static void Query1(Database database, IProductsService srv)
        {
            var req = new GridRequestWithAdditionalPayload<TextSearchPayload>()
            {
                Skip = 3,
                Take = 1,
                Sort = new GridRequestSort[]
            {
                    new GridRequestSort()
                    {
                        PropName = "ModelName",
                        IsDescending = true
                    },
                    new GridRequestSort()
                    {
                        PropName = "ProductNumber",
                        IsDescending = true
                    },
            },
                Payload = new TextSearchPayload()
                {
                    TextSearch = "frame"
                }
            };

            database.LogSQLToFile("B2DataGridRequests-query1-log.html", () =>
            {
                var result1 = srv.GetProductsForGrid(req).ToArray();
                result1.SaveAsHtmlTableFile("B2DataGridRequests-query1-result.html");
            });
        }

        public static void Query2(Database database, IProductsService srv)
        {
            var req = new GridRequestWithAdditionalPayload<TextSearchPayload>()
            {
                Filter = new GridRequestFilter[]
                {
                      new GridRequestFilter()
                      {
                          PropName = "ProductId",
                          Operand = "Eq",
                          JsonValue = "764"
                      }
                },
                Payload = new TextSearchPayload()
            };

            database.LogSQLToFile("B2DataGridRequests-query2-log.html", () =>
            {
                var result1 = srv.GetProductsForGrid(req).ToArray();
                result1.SaveAsHtmlTableFile("B2DataGridRequests-query2-result.html");
            });
        }

        public static void Query3(Database database, IProductsService srv)
        {
            var req = new GridRequestWithAdditionalPayload<TextSearchPayload>()
            {
                Filter = new GridRequestFilter[]
                           {
                    new GridRequestFilter()
                    {
                        PropName = "ProductNumber",
                        Operand = "Contains",
                        JsonValue = "'FR-R92B'"
                    }
                           },
                Payload = new TextSearchPayload()
            };

            database.LogSQLToFile("B2DataGridRequests-query3-log.html", () =>
            {
                var result1 = srv.GetProductsForGrid(req).ToArray();
                result1.SaveAsHtmlTableFile("B2DataGridRequests-query3-result.html");
            });
        }

        public static void Query4(Database database, IProductsService srv)
        {
            var req = new GridRequestWithAdditionalPayload<TextSearchPayload>()
            {
                Skip = 2,
                Take = 2,
                Filter = new GridRequestFilter[]
                {
                    new GridRequestFilter()
                    {
                        PropName = "ProductNumber",
                        Operand = "Contains",
                        JsonValue = "'-54'"
                    }
                },
                Sort = new GridRequestSort[]
                {
                    new GridRequestSort()
                    {
                        PropName = "ProductId",
                        IsDescending = true
                    },
                },
                Payload = new TextSearchPayload()
                {
                    TextSearch = "frame"
                }
            };

            database.LogSQLToFile("B2DataGridRequests-query4-log.html", () =>
            {
                var result1 = srv.GetProductsForGrid(req).ToArray();
                result1.SaveAsHtmlTableFile("B2DataGridRequests-query4-result.html");
            });
        }
    }
}
