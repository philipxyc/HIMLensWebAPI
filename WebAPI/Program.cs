using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.ProjectOxford.Face;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI
{
    public class Program
    {
        const string AZURE_STORE_CONN_STR = "YOUR AZURE STORE CONNECT STRING";

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
        public static async Task<Guid> GetId(string _key, HttpRequest _req)
        {
            var faceServiceClient = new FaceServiceClient(_key);
            Microsoft.ProjectOxford.Face.Contract.Face[] faces;
            using (MemoryStream ms = new MemoryStream())
            {
                _req.Body.CopyTo(ms);
                //Important!! Reset Position
                ms.Position = 0;
                faces = await faceServiceClient.DetectAsync(ms);
            }
            if (faces.GetLength(0) == 0)
            {
                return Guid.Empty;
            }
            else
            {
                return faces[0].FaceId;
            }
        }
        public static async Task<List<string>> GetTags(string _key, Guid dId, HimContext _context)
        {
            var faceServiceClient = new FaceServiceClient(_key);
            var result = await faceServiceClient.FindSimilarAsync(dId, "himlens", 1);
            var per = _context.Persons.Single(m => m.fid == result[0].PersistedFaceId.ToString());
            return per.tags.Split(';').ToList<string>();
        }
        public static async Task<List<string>> UpdateTags(string _key, Guid dId, HimContext _context, string tags)
        {
            var faceServiceClient = new FaceServiceClient(_key);
            var result = await faceServiceClient.FindSimilarAsync(dId, "himlens", 1);
            var per = _context.Persons.Single(m => m.fid == result[0].PersistedFaceId.ToString());
            per.tags += ";" + tags;
            _context.Persons.Update(per);
            _context.SaveChanges();
            return per.tags.Split(';').ToList<string>();
        }
        public static async Task<List<string>> DelTags(string _key, Guid dId, HimContext _context, string tags)
        {
            var faceServiceClient = new FaceServiceClient(_key);
            var result = await faceServiceClient.FindSimilarAsync(dId, "himlens", 1);
            var per = _context.Persons.Single(m => m.fid == result[0].PersistedFaceId.ToString());
            var tags_full = per.tags.Split(';').ToList<string>();
            var ft = new List<string>();
            bool shalladd = true;
            foreach (var ot in tags_full)
            {
                ot.Replace('+', ' ');
                shalladd = true;
                foreach (var dt in tags.Split(';'))
                {
                    if (ot == dt)
                    {
                        shalladd = false;
                        break;
                    }
                }
                if (shalladd)
                {
                    ft.Add(ot);
                }
            }
            per.tags = String.Join(";", ft);
            _context.Persons.Update(per);
            _context.SaveChanges();
            return per.tags.Split(';').ToList<string>();
        }
        public static async Task CreatPerson(string _key, HttpRequest _req, HimContext _context, string _tags)
        {
            var faceServiceClient = new FaceServiceClient(_key);
            using (MemoryStream msb = new MemoryStream())
            {
                using (MemoryStream msf = new MemoryStream())
                {
                    _req.Body.CopyTo(msb);
                    msb.Position = 0;
                    msb.CopyTo(msf);
                    //Important!! Reset Position
                    msf.Position = 0;
                    var faceTask = faceServiceClient.AddFaceToFaceListAsync("himlens", msf, _tags.Split(';')[0]);
                    //init azure blob
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AZURE_STORE_CONN_STR);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference("him");
                    var face = await faceTask;
                    var blob = container.GetBlockBlobReference(face.PersistedFaceId.ToString());

                    msb.Position = 0;
                    var blobTask = blob.UploadFromStreamAsync(msb);
                    _context.Persons.Add(new Person { fid = face.PersistedFaceId.ToString(), tags = _tags });
                    _context.SaveChanges();
                    blobTask.Wait();
                }
            }
        }
        public static async Task DelPerson(string _key, Guid dId, HimContext _context)
        {
            var faceServiceClient = new FaceServiceClient(_key);
            var result = await faceServiceClient.FindSimilarAsync(dId, "himlens", 1);
            await faceServiceClient.DeleteFaceFromFaceListAsync("himlens", result[0].PersistedFaceId);//No need to await
            //init azure blob
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AZURE_STORE_CONN_STR);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("him");
            var blob = container.GetBlockBlobReference(result[0].PersistedFaceId.ToString());
            blob.Delete();
            var per = _context.Persons.Single(m => m.fid == result[0].PersistedFaceId.ToString());
            _context.Remove(per);
            _context.SaveChanges();
        }

        public static IQueryable<Person> ListAll(HimContext _context)
        {
            return _context.Set<Person>();
        }

    }
}