using Repositories;
using Services;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Queries
{
    public static class B1DataGridRequests
    {
        public static void Query1(IProductsService srv)
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

            var result1 = srv.GetProductsForGrid(req).ToArray();
        }

        public static void Query2(IProductsService srv)
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

            var result1 = srv.GetProductsForGrid(req).ToArray();
        }

        public static void Query3(IProductsService srv)
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

            var result1 = srv.GetProductsForGrid(req).ToArray();
        }

        public static void Query4(IProductsService srv)
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

            var result1 = srv.GetProductsForGrid(req).ToArray();
        }


    }
}
