using System;
using System.IO;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using TranslationManagement.Data.Configuration;
using TranslationManagement.Services.Contracts;
using TranslationManagement.Services.DTO;

namespace TranslationManagement.Services
{
    public class FileService : IFileService
    {
        public T ProcessFile<T>(IFormFile file, T dto, string customer) where T : CreateTranslatorJobRequestDto
        {
            using var reader = new StreamReader(file.OpenReadStream());

            if (file.FileName.EndsWith(".txt"))
            {
                dto.OriginalContent = reader.ReadToEnd();
                dto.CustomerName = customer;
            }
            else if (file.FileName.EndsWith(".xml"))
            {
                var xdoc = XDocument.Parse(reader.ReadToEnd());
                dto.OriginalContent = xdoc.Root.Element("Content").Value;
                dto.CustomerName = xdoc.Root.Element("Customer").Value.Trim();
            }
            else
            {
                throw new NotSupportedException(Constants.unsupportedFileFormat);
            }

            return dto;
        }
    }
}
