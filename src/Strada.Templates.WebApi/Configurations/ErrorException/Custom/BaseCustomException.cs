﻿// <copyright file="BaseCustomException.cs" company="Strada">
// Copyright (c) Strada. All rights reserved.
// </copyright>

namespace Strada.Templates.WebApi.Configurations.ErrorException.Custom
{
    public class BaseCustomException : Exception
    {
        public BaseCustomException(string message)
            : base(message)
        {
        }
    }
}
