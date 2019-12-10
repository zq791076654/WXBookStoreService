using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using WXBookStoreService.Models;
using WXBookStoreService.Models.DBModels;
using WXBookStoreService.Models.ViewModels;
using WXBookStoreService.Models.WXAPIModels;

namespace WXBookStoreService.Controllers
{
    public class LoginController : ApiController
    {
        BSContext dbContext = new BSContext();
        [HttpGet]
        public IHttpActionResult Login(string code, string rawData, string signature, string encryptedData, string iv)
        {
            VMUserInfo vmUserInfo = new VMUserInfo();
            WXOpenId wxOpenId = JsonConvert.DeserializeObject<WXOpenId>(HttpHelper.HttpGet("https://api.weixin.qq.com/sns/jscode2session?appid=" + HttpHelper.AppId + "&secret=" + HttpHelper.AppSecret + "&js_code=" + code + "&grant_type=authorization_code"));

            //通过签名验证数据是否有效
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] source = Encoding.UTF8.GetBytes(rawData + wxOpenId.session_key);
            byte[] target = sha1.ComputeHash(source);
            if (BitConverter.ToString(target).Replace("-", "").ToLower() == signature)
            {
                //解密数据
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.Mode = CipherMode.CBC;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;

                byte[] byte_encryptedData = Convert.FromBase64String(encryptedData);
                byte[] byte_iv = Convert.FromBase64String(iv);
                byte[] byte_sessionKey = Convert.FromBase64String(wxOpenId.session_key);

                aes.IV = byte_iv;
                aes.Key = byte_sessionKey;
                ICryptoTransform transform = aes.CreateDecryptor();

                byte[] final = transform.TransformFinalBlock(byte_encryptedData, 0, byte_encryptedData.Length);
                WXUserInfo wxUserInfo = JsonConvert.DeserializeObject<WXUserInfo>(Encoding.UTF8.GetString(final));

                UserInfo userInfo = dbContext.UserInfos.FirstOrDefault(t => t.OpenId == wxOpenId.openid);
                if (userInfo == null)
                {
                    userInfo = new UserInfo();
                    userInfo.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    userInfo.OpenId = wxOpenId.openid;
                    userInfo.SessionKey = wxOpenId.session_key;
                    userInfo.Unionid = wxOpenId.unionid;
                    userInfo.NickName = wxUserInfo.nickName;
                    userInfo.Gender = (gender)wxUserInfo.gender;
                    userInfo.City = wxUserInfo.city;
                    userInfo.Province = wxUserInfo.province;
                    userInfo.Country = wxUserInfo.country;
                    userInfo.AvatarUrl = wxUserInfo.avatarUrl;
                    userInfo.AppId = wxUserInfo.watermark.appid;
                    userInfo.TimeStamp = wxUserInfo.watermark.timestamp;
                    dbContext.UserInfos.Add(userInfo);
                }
                else
                {
                    userInfo.SessionKey = wxOpenId.session_key;
                    userInfo.Unionid = wxOpenId.unionid;
                    userInfo.NickName = wxUserInfo.nickName;
                    userInfo.Gender = (gender)wxUserInfo.gender;
                    userInfo.City = wxUserInfo.city;
                    userInfo.Province = wxUserInfo.province;
                    userInfo.Country = wxUserInfo.country;
                    userInfo.AvatarUrl = wxUserInfo.avatarUrl;
                    userInfo.AppId = wxUserInfo.watermark.appid;
                    userInfo.TimeStamp = wxUserInfo.watermark.timestamp;
                }

                dbContext.SaveChanges();
                vmUserInfo = VMUserInfo.GetVMUserInfo(userInfo);
            }

            return Json(vmUserInfo);
        }
    }
}
