﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface1;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Filminurk.ApplicationServices.Services
{
    public class FilesServices: IFilesServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly FilminurkTARpe24Context _context;
        
        public FilesServices(IHostEnvironment webHost, FilminurkTARpe24Context context)
        {
            _webHost = webHost;
            _context = context;
        }

        public void FilesToApi(MoviesDTO dto, Movie domain)
        {
            if (dto.Files != null && dto.Files.Count >0)
            { 
                if(!Directory.Exists(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUplodad\\");
                }
                foreach (var file in dto.Files)
                {
                    string uploadFolder = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");
                    string uniqueFileName=Guid.NewGuid().ToString() + "_" +file.FileName;
                    string filepath =Path.Combine(uploadFolder,uniqueFileName);
                    using (var FileStream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(FileStream);
                        FileToApi path = new FileToApi()
                        {
                            ImageID=Guid.NewGuid(),
                            ExistingFilePath=uniqueFileName,
                            MovieID=domain.ID,

                        };
                        _context.FilesToApi.Add(path);
                    }
                }

            
            }

        }

        public async Task<FileToApi> RemoveImageFromApi(FileToApiDTO dto)
        {
            var imageID= await _context.FilesToApi.FirstOrDefaultAsync(x=> x.ImageID==dto.ImageID);
            var filePath = _webHost.ContentRootPath + "\\wwwrooy\\multipleFileUpload\\" + imageID.ExistingFilePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.FilesToApi.Remove(imageID);
            await _context.SaveChangesAsync();
            return null;
        }
        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDTO[] dtos)
        {
            foreach (var dto in dtos)
            {
                RemoveImageFromApi(dto);
            }
            return null;
        }

        public Task<FileToApi> RemoveImagesFromApi(FileToApiDTO dto)
        {
            throw new NotImplementedException();
        }

        Task<FileToApi> IFilesServices.RemoveImagesFromApi(FileToApiDTO[] dto)
        {
            throw new NotImplementedException();
        }
    }
}
