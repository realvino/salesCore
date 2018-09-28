using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using tibs.stem.IO;
using tibs.stem.Web.Helpers;
using tibs.stem.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace tibs.stem.Web.Controllers
{
    public abstract class ProfileControllerBase : stemControllerBase
    {
        private readonly IAppFolders _appFolders;  
        protected ProfileControllerBase(IAppFolders appFolders)
        {
            _appFolders = appFolders;  
        }

        public JsonResult UploadProfilePicture()
        {
            try
            {
                var profilePictureFile = Request.Form.Files.First();

                //Check input
                if (profilePictureFile == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                if (profilePictureFile.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                byte[] fileBytes;
                using (var stream = profilePictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception("Uploaded file is not an accepted image file !");
                }

                //Delete old temp profile pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder, "userProfileImage_" + AbpSession.GetUserId());

                //Save new picture
                var fileInfo = new FileInfo(profilePictureFile.FileName);
                var tempFileName = "userProfileImage_" + AbpSession.GetUserId() + fileInfo.Extension;
                var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        // 19.10.2017
        public JsonResult UploadProductPicture(int ProductId, string ImgPath)
        {
            try
            {
                var productPictureFile = Request.Form.Files.First();

                //Check input
                if (productPictureFile == null)
                {
                    throw new UserFriendlyException(L("ProductPicture_Change_Error"));
                }

                if (productPictureFile.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProductPicture_Warn_SizeLimit"));
                }

                byte[] fileBytes;
                using (var stream = productPictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception("Uploaded file is not an accepted image file !");
                }

                var Product = _appFolders.ProductFilePath;

                if (!Directory.Exists(Product))
                {
                    Directory.CreateDirectory(Product);
                }
                //Delete old Product pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.FindFilePath, ImgPath);

                //Save new picture
                var fileInfo = new FileInfo(productPictureFile.FileName);
                var tempFileName = productPictureFile.FileName;
                var tempFilePath = Path.Combine(Product, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = "Common/Images/Product/" + tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }

            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadProductGroupPicture(int ProductGroupId, string ImgPath)
        {
            try
            {
                var productGroupPictureFile = Request.Form.Files.First();

                //Check input
                if (productGroupPictureFile == null)
                {
                    throw new UserFriendlyException(L("ProductGroupPicture_Change_Error"));
                }

                if (productGroupPictureFile.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProductGroupPicture_Warn_SizeLimit"));
                }

                byte[] fileBytes;
                using (var stream = productGroupPictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception("Uploaded file is not an accepted image file !");
                }

                var ProductGroup = _appFolders.ProductGroupFilePath;

                if (!Directory.Exists(ProductGroup))
                {
                    Directory.CreateDirectory(ProductGroup);
                }

                //Delete old ProductGroup pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.FindFilePath, ImgPath);

                //Save new picture
                var fileInfo = new FileInfo(productGroupPictureFile.FileName);
                var tempFileName = productGroupPictureFile.FileName + fileInfo.Extension;
                var tempFilePath = Path.Combine(ProductGroup, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = "Common/Images/ProductGroup/" + tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadProductSubGroupPicture(int ProductSubGroupId, string ImgPath)
        {
            try
            {
                var productSubGroupPictureFile = Request.Form.Files.First();

                //Check input
                if (productSubGroupPictureFile == null)
                {
                    throw new UserFriendlyException(L("ProductSubGroupPicture_Change_Error"));
                }

                if (productSubGroupPictureFile.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProductSubGroupPicture_Warn_SizeLimit"));
                }

                byte[] fileBytes;
                using (var stream = productSubGroupPictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception("Uploaded file is not an accepted image file !");
                }

                var ProductSubGroup = _appFolders.ProductSubGroupFilePath;

                if (!Directory.Exists(ProductSubGroup))
                {
                    Directory.CreateDirectory(ProductSubGroup);
                }

                //Delete old ProductSubGroup pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.FindFilePath, ImgPath);

                //Save new picture
                var fileInfo = new FileInfo(productSubGroupPictureFile.FileName);
                var tempFileName = productSubGroupPictureFile.FileName + fileInfo.Extension;
                var tempFilePath = Path.Combine(ProductSubGroup, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new AjaxResponse(new { fileName = "Common/Images/ProductSubGroup/" + tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }
    }
}