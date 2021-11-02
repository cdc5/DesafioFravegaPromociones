using PromocionesFravega.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PromocionesFravega.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        ErrorExceptionStructure validation = null;
        int statusCode;
        void IExceptionFilter.OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(BusinessException))
            {
                var exception = (BusinessException)context.Exception;
                if (exception.Message != null)
                    validation = new ErrorExceptionStructure(400, "Solicitud Inválida", exception.Message);
                //else
                //    validation = new ErrorExceptionStructure(400, "Solicitud Inválida", "Api Senasa no retorna error");
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                //validation = new ErrorExceptionStructure(500, "Ocurrió un Error", "Estamos trabajando para solucionarlo");
                validation = new ErrorExceptionStructure(500, "Ocurrió un Error", context.Exception.Message);
                statusCode = (int)HttpStatusCode.InternalServerError;
            }

            var json = new
            {
                errors = new[] { validation }
            };
            context.Result = new BadRequestObjectResult(json);
            context.HttpContext.Response.StatusCode = statusCode;
            context.ExceptionHandled = true;
        }
    }

    public class ErrorExceptionStructure
    {
        public int status { get; set; }
        public string titulo { get; set; }
        public string detalle { get; set; }

        public ErrorExceptionStructure(int _status,string _titulo,string _detalle)
        {
            status = _status;
            titulo = _titulo;
            detalle = _detalle;
        }
    }

}
