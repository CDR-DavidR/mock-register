﻿using CDR.Register.Admin.API.Business.Model;
using CDR.Register.Admin.API.Business.Validators;
using CDR.Register.API.Infrastructure.Models;
using CDR.Register.Domain.ValueObjects;
using System.Linq;

namespace CDR.Register.Admin.API
{
    public static class Extensions
    {
        public static ResponseErrorList ToResponseErrorList(this BusinessRuleError businessRuleError)
        {
            var responseErrorList = new ResponseErrorList();
            responseErrorList.Errors.Add(new Error
            {
                Code = businessRuleError.Code,
                Detail = businessRuleError.Detail,
                Title  = businessRuleError.Title,
                Meta = new object()
            });
            return responseErrorList;
        }

        public static ResponseErrorList GetValidationErrors(this LegalEntity legalEntity, LegalEntityValidator validator)
        {
            var result = validator.Validate(legalEntity);
            var responseErrorList = new ResponseErrorList();

            if (!result.IsValid)
            {
                responseErrorList.Errors.AddRange(result.Errors.Select(error => new Error
                {
                    Code = error.ErrorCode,
                    Title = error.ErrorMessage,
                    Detail = error.CustomState?.ToString() ?? error.PropertyName
                }));
            }

            return responseErrorList;
        }
    }
}
