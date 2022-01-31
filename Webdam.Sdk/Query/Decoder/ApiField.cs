using System;
using System.Collections.Generic;
using System.Text;

namespace Webdam.Sdk.Query.Decoder
{
    /// <summary>
    /// Class to be used as attributes for properties in queries to specify
    /// if property needs to be send as parameter to the API
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ApiField : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the class
        /// </summary>
        /// <param name="name">Name of the field in the API documentation</param>
        public ApiField(string name)
        {
            ApiName = name;
        }

        /// <summary>
        /// Converter to be used to convert the property
        /// </summary>
        public Type Converter { get; set; }

        /// <summary>
        /// Name of the property in the API documentation.
        /// </summary>
        public string ApiName { get; private set; }
    }
}
