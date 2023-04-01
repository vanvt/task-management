using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using TaskManagement.Entities;

namespace TaskManagement.Repository
{
    public interface IRepository<T>
    {
        public void Add(T items);
        public void AddRange(List<T> items);
        public List<T> Get();
        public void Update(List<T> items);
        public void Delete(T item);
    }
    public class GenericRepository<T>:IRepository<T>   
    {
        private string path = typeof(T).ToString()+".json";

        public void Add(T ticket)
        {
            var currtnList = Get();
            currtnList.Add(ticket);
            string json = JsonSerializer.Serialize(currtnList);
            File.Delete(path);
            File.WriteAllText(path, json);
        }
        public void AddRange(List<T> items)
        {
            var currtnList = Get();
            currtnList.AddRange(items);
            string json = JsonSerializer.Serialize(currtnList);
            File.Delete(path);
            File.WriteAllText(path, json);
        }
        public void Delete(T item)
        {
            var currtnList = Get();
            var deleteObject = currtnList.Where(t => 
                JsonSerializer.Serialize(t).GetHashCode() ==
                JsonSerializer.Serialize(t).GetHashCode()).First();
            currtnList.Remove(deleteObject);
            string json = JsonSerializer.Serialize(currtnList);
            File.Delete(path);
            File.WriteAllText(path, json);
        }

        public List<T> Get()
        {
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    List<T> items = JsonSerializer.Deserialize<List<T>>(json);
                    
                    return items??new List<T>();
                }
            }
            catch (Exception)
            {
                return new List<T>();
            }
  
        }

        public void Update(List<T> items)
        {
            string json = JsonSerializer.Serialize(items);
            File.Delete(path);
            File.WriteAllText(path, json);
        }
    }
}
